using Ecoporto.Posicionamento.Dados.Interfaces;
using Ecoporto.Posicionamento.Enums;
using Ecoporto.Posicionamento.Extensions;
using Ecoporto.Posicionamento.Filtros;
using Ecoporto.Posicionamento.Models;
using Ecoporto.Posicionamento.Models.ViewModels;
using MvcRazorToPdf;
using System;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace Ecoporto.Posicionamento.Controllers
{
    public class AgendamentoCargaSoltaController : BaseController
    {
        private readonly IPeriodosRepositorio _periodosRepositorio;
        private readonly IMotivosRepositorio _motivosRepositorio;
        private readonly IAgendamentoCargaSoltaRepositorio _agendamentoRepositorio;
        private readonly IViagensRepositorio _viagensRepositorio;
        private readonly IClientesRepositorio _clientesRepositorio;

        public AgendamentoCargaSoltaController(
            IPeriodosRepositorio periodosRepositorio, 
            IMotivosRepositorio motivosRepositorio, 
            IAgendamentoCargaSoltaRepositorio agendamentoRepositorio,
            IViagensRepositorio viagensRepositorio,
            IClientesRepositorio clientesRepositorio)
        {
            _periodosRepositorio = periodosRepositorio;
            _motivosRepositorio = motivosRepositorio;
            _agendamentoRepositorio = agendamentoRepositorio;
            _viagensRepositorio = viagensRepositorio;
            _clientesRepositorio = clientesRepositorio;
        }

        [HttpGet]
        public ActionResult Consultar()
        {
            var agendados = _agendamentoRepositorio
                .ObterAgendamentos(User.ObterDespachanteId())
                .Where(c => c.Status != StatusPosicionamento.Cancelado);

            var cancelados = _agendamentoRepositorio
                .ObterAgendamentos(User.ObterDespachanteId())
                .Where(c => c.Status == StatusPosicionamento.Cancelado);

            return View(new AgendamentoCargaSoltaViewModel { 
                Agendados = agendados,
                Cancelados = cancelados
            });
        }

        public void PopularPeriodos(AgendamentoCargaSoltaViewModel viewModel)
        {
            viewModel.Periodos = _periodosRepositorio.ObterPeriodos().ToList();
        }

        public void PopularMotivos(AgendamentoCargaSoltaViewModel viewModel)
        {
            viewModel.Motivos = _motivosRepositorio.ObterMotivos().ToList();
        }

        [HttpGet]
        public ActionResult ObterDetalhes(string reserva)
        {
            if (string.IsNullOrEmpty(reserva))
                return RetornarErro("Informe a Reserva");

            var dados = _agendamentoRepositorio.ObterDetalhesCargaSoltaPorReserva(reserva.Trim());

            if (dados == null)
                return RetornarErro("Nenhum registro encontrado com os parâmetros especificados");

            var itensCargaSolta = _agendamentoRepositorio
                .ObterCargaSoltaPorReserva(dados.Reserva);

            if (itensCargaSolta.Count() == 0)
                return RetornarErro("Nenhum item de Carga Solta disponível para posicionamento");

            dados.ItensCargaSolta.AddRange(itensCargaSolta);
          
            return PartialView("_DetalhesReserva", new AgendamentoCargaSoltaViewModel
            {
                Navio = dados.Navio,
                Viagem = dados.Viagem,
                Exportador = dados.Exportador,
                Porto = dados.Porto,
                ItensCargaSolta = itensCargaSolta.ToList()
            });
        }   

        [HttpGet]
        public bool ExigeViagem(int motivoId)
        {
            return _motivosRepositorio.ExigeViagem(motivoId);
        }

        [HttpGet]
        public ActionResult Agendar()
        {
            var viewModel = new AgendamentoCargaSoltaViewModel();

            PopularPeriodos(viewModel);
            PopularMotivos(viewModel);

            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Agendar([Bind(Include = "MotivoId, DataPrevista, CpfCnpj, NovaViagemId, ItensCargaSolta")] AgendamentoCargaSoltaViewModel viewModel)
        {
            var agendamento = new Agendamento(
                viewModel.MotivoId,
                viewModel.DataPrevista.ToDateTime(),
                viewModel.CpfCnpj,
                viewModel.NovaViagemId,
                TipoAgendamento.CargaSolta,
                User.ObterDespachanteId());

            agendamento.AdicionarItensCargaSolta(viewModel.ItensCargaSolta);

            if (!Validar(agendamento))
                return RetornarErros();

            var clienteBusca = _clientesRepositorio
                .ObterClientePorDocumento(viewModel.CpfCnpj.ApenasNumeros());

            if (clienteBusca == null)
                throw new Exception($"Nenhum Cliente foi encontrado com o documento {viewModel.CpfCnpj}");

            agendamento.SetarCliente(clienteBusca.Id);
            agendamento.Status = StatusPosicionamento.NaoIniciado;

            try
            {
                var protocolo = _agendamentoRepositorio.GravarPosicionamentoCargaSolta(agendamento);

                viewModel.ItensCargaSoltaAgendados = _agendamentoRepositorio
                    .ObterItensCargaSoltaAgendamento(protocolo).ToList();

                viewModel.ProtocoloUnificado = protocolo;

                return PartialView("_Concluido", viewModel);
            }
            catch (Exception ex)
            {
                throw new Exception($"Falha ao Gravar o Agendamento {ex.Message}");
            }
        }

        [HttpGet]
        [ValidaDespachanteAgendamentoFilter]
        public ActionResult Visualizar(int id)
        {
            var agendamentoBusca = _agendamentoRepositorio
                .ObterPosicionamentoPorId(id);

            if (agendamentoBusca == null)
                throw new Exception("Posicionamento não encontrado ou excluído");

            var itemCargaSoltaBusca = _agendamentoRepositorio
                .ObterCargaSoltaAgendamento(id);

            var viewModel = new AgendamentoCargaSoltaViewModel
            {
                DataPrevista = agendamentoBusca.DataPrevista.DataFormatada(),
                Porto = agendamentoBusca.Porto,
                Navio = agendamentoBusca.Navio,
                Exportador = agendamentoBusca.Exportador,
                Reserva = agendamentoBusca.Reserva,
                Viagem = agendamentoBusca.Viagem,
                NovaViagemDescricao = agendamentoBusca.NovaViagemDescricao,
                RazaoSocial = agendamentoBusca.Cliente,
                CpfCnpj = agendamentoBusca.ClienteCpfCnpj,
                MotivoId = agendamentoBusca.MotivoId,
                MotivoDescricao = agendamentoBusca.MotivoDescricao
            };

            viewModel.ItensCargaSoltaAgendados.Add(itemCargaSoltaBusca ?? new CargaSolta());

            return View(viewModel);
        }

        public ProtocoloAgendamentoViewModel ObterDadosProtocolo(int id)
        {
            var agendamentoBusca = _agendamentoRepositorio
                .ObterPosicionamentoPorId(id);

            if (agendamentoBusca == null)
                throw new Exception("Posicionamento não encontrado ou excluído");

            return new ProtocoloAgendamentoViewModel()
            {
                Exportador = agendamentoBusca.Exportador,
                ExportadorCnpj = agendamentoBusca.ExportadorCnpj,
                Cliente = agendamentoBusca.Cliente,
                ClienteCpfCnpj = agendamentoBusca.ClienteCpfCnpj,
                MotivoDescricao = agendamentoBusca.MotivoDescricao,
                DataPrevista = agendamentoBusca.DataPrevista,
                Porto = agendamentoBusca.Porto,
                Navio = agendamentoBusca.Navio,
                Viagem = agendamentoBusca.Viagem,
                Reserva = agendamentoBusca.Reserva,
                Line = agendamentoBusca.Line,
                DataCancelamento = agendamentoBusca.DataCancelamento,
                MotivoCancelamento = agendamentoBusca.MotivoCancelamento,
                ProtocoloUnificado = agendamentoBusca.ProtocoloUnificado
            };
        }

        [ValidaDespachanteAgendamentoFilter]
        public ActionResult Protocolo(int id, bool unificado = false)
        {
            var viewModel = ObterDadosProtocolo(id);

            if (unificado)
            {
                var itensCargaSoltaBusca = _agendamentoRepositorio
                    .ObterItensCargaSoltaAgendamento(viewModel.ProtocoloUnificado);

                viewModel.ItensCargaSolta.AddRange(itensCargaSoltaBusca);
            }
            else
            {
                var itemCargaSoltaBusca = _agendamentoRepositorio
                    .ObterCargaSoltaAgendamento(id);

                viewModel.ItensCargaSolta.Add(itemCargaSoltaBusca);
            }

            return new PdfActionResult(viewModel);
        }

        [ValidaDespachanteAgendamentoFilter]
        public ActionResult ProtocoloCancelamento(int id)
        {
            var viewModel = ObterDadosProtocolo(id);

            var itemCargaSoltaBusca = _agendamentoRepositorio
                .ObterCargaSoltaAgendamento(id);

            viewModel.ItensCargaSolta.Add(itemCargaSoltaBusca);

            return new PdfActionResult(viewModel);
        }

        [HttpPost]
        [ValidaDespachanteAgendamentoFilter]
        public ActionResult Cancelar(int id, string motivo)
        {
            var agendamentoBusca = _agendamentoRepositorio
                .ObterPosicionamentoPorId(id);

            if (agendamentoBusca == null)
                return RetornarErro("Posicionamento não encontrado ou excluído");

            if (agendamentoBusca.Status != StatusPosicionamento.NaoIniciado)
                return RetornarErro("Cancelamento não permitido para este Status");

            try
            {
                _agendamentoRepositorio.CancelarAgendamento(agendamentoBusca.Id, motivo);
            }
            catch
            {
                return RetornarErro("Falha ao Cancelar o Agendamento");
            }

            return new HttpStatusCodeResult(HttpStatusCode.NoContent);
        }
    }
}