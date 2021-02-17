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
    public class AgendamentoConteinerController : BaseController
    {
        private readonly IPeriodosRepositorio _periodosRepositorio;
        private readonly IMotivosRepositorio _motivosRepositorio;
        private readonly IAgendamentoConteinerRepositorio _agendamentoRepositorio;
        private readonly IViagensRepositorio _viagensRepositorio;
        private readonly IClientesRepositorio _clientesRepositorio;

        public AgendamentoConteinerController(
            IPeriodosRepositorio periodosRepositorio,
            IMotivosRepositorio motivosRepositorio,
            IAgendamentoConteinerRepositorio agendamentoRepositorio,
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

            return View(new AgendamentoConteinerViewModel
            {
                Agendados = agendados,
                Cancelados = cancelados
            });
        }

        [HttpGet]
        public ActionResult Cancelados()
        {
            var agendados = _agendamentoRepositorio
                .ObterAgendamentos(User.ObterDespachanteId())
                .Where(c => c.Status == StatusPosicionamento.Cancelado);

            return View(agendados);
        }

        public void PopularPeriodos(AgendamentoConteinerViewModel viewModel)
        {
            viewModel.Periodos = _periodosRepositorio.ObterPeriodos().ToList();
        }

        public void PopularMotivos(AgendamentoConteinerViewModel viewModel)
        {
            viewModel.Motivos = _motivosRepositorio.ObterMotivos().ToList();
        }

        [HttpGet]
        public ActionResult ObterDetalhes(string reserva)
        {
            if (string.IsNullOrWhiteSpace(reserva))
                return RetornarErro("Informe a Reserva");

            var dados = _agendamentoRepositorio.ObterDetalhesConteinerPorReserva(reserva.Trim());

            if (dados == null)
                return RetornarErro("Nenhum registro encontrado com os parâmetros especificados");

            var conteineres = _agendamentoRepositorio
                .ObterConteineresPorReserva(dados.Reserva);

            if (conteineres.Count() == 0)
                return RetornarErro("Nenhum contêiner disponível para posicionamento");

            dados.Conteineres.AddRange(conteineres);

            return PartialView("_DetalhesReserva", new AgendamentoConteinerViewModel
            {
                Navio = dados.Navio,
                Viagem = dados.Viagem,
                Exportador = dados.Exportador,
                Porto = dados.Porto,
                Conteineres = conteineres.ToList()
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
            var viewModel = new AgendamentoConteinerViewModel();

            PopularPeriodos(viewModel);
            PopularMotivos(viewModel);

            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Agendar([Bind(Include = "MotivoId, DataPrevista, CpfCnpj, NovaViagemId, ConteineresSelecionados")] AgendamentoConteinerViewModel viewModel)
        {
            var agendamento = new Agendamento(
                viewModel.MotivoId,
                viewModel.DataPrevista.ToDateTime(),
                viewModel.CpfCnpj,
                viewModel.NovaViagemId,
                TipoAgendamento.Conteiner,
                User.ObterDespachanteId());

            agendamento.AdicionarConteineres(viewModel.ConteineresSelecionados);

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
                foreach (var cntr in viewModel.ConteineresSelecionados)
                {
                    var posicionamentoBusca = _agendamentoRepositorio.ExistePosicionamento(viewModel.MotivoId, viewModel.DataPrevista.ToDateTime(), cntr);

                    if (posicionamentoBusca != null)
                    {                        
                        if (posicionamentoBusca != null)
                        {
                            ModelState.AddModelError(string.Empty, $"Já existe um Agendamento para a Unidade {posicionamentoBusca.Sigla} na Data {viewModel.DataPrevista} para {posicionamentoBusca.Motivo}");
                        }
                    }
                }

                if (ModelState.Values.SelectMany(v => v.Errors).Any())
                {
                    return RetornarErros();
                }

                var protocolo = _agendamentoRepositorio.GravarPosicionamentoCntr(agendamento);

                viewModel.ConteineresAgendados = _agendamentoRepositorio
                    .ObterConteineresAgendamento(protocolo).ToList();

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
            
            var conteinerBusca = _agendamentoRepositorio
                .ObterConteinerAgendamento(id);

            var viewModel = new AgendamentoConteinerViewModel
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

            viewModel.ConteineresAgendados.Add(conteinerBusca?? new Conteiner());

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
                var conteineresBusca = _agendamentoRepositorio
                    .ObterConteineresAgendamento(viewModel.ProtocoloUnificado);

                viewModel.Conteineres.AddRange(conteineresBusca);
            }
            else
            {
                var conteinerBusca = _agendamentoRepositorio
                    .ObterConteinerAgendamento(id);

                viewModel.Conteineres.Add(conteinerBusca);
            }

            return new PdfActionResult(viewModel);
        }

        [ValidaDespachanteAgendamentoFilter]
        public ActionResult ProtocoloCancelamento(int id)
        {
            var viewModel = ObterDadosProtocolo(id);

            var conteinerBusca = _agendamentoRepositorio
                .ObterConteinerAgendamento(id);

            viewModel.Conteineres.Add(conteinerBusca);

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