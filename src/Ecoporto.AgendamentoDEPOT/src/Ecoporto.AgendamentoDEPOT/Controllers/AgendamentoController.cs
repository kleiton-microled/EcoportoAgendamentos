using Ecoporto.AgendamentoDEPOT.Dados.Interfaces;
using Ecoporto.AgendamentoDEPOT.Enums;
using Ecoporto.AgendamentoDEPOT.Extensions;
using Ecoporto.AgendamentoDEPOT.Filtros;
using Ecoporto.AgendamentoDEPOT.Helpers;
using Ecoporto.AgendamentoDEPOT.Models;
using Ecoporto.AgendamentoDEPOT.Models.DTO;
using Ecoporto.AgendamentoDEPOT.Models.ViewModels;
using MvcRazorToPdf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace Ecoporto.AgendamentoDEPOT.Controllers
{
    [Authorize]
    public class AgendamentoController : BaseController
    {
        private readonly IAgendamentoRepositorio _agendamentoRepositorio;
        private readonly IMotoristaRepositorio _motoristaRepositorio;
        private readonly IVeiculoRepositorio _veiculoRepositorio;
        private readonly IEmpresaRepositorio _empresaRepositorio;
        private readonly IRecintoRepositorio _recintoRepositorio;

        public AgendamentoController(
            IAgendamentoRepositorio agendamentoRepositorio,
            IMotoristaRepositorio motoristaRepositorio,
            IVeiculoRepositorio veiculoRepositorio,
            IEmpresaRepositorio empresaRepositorio,
            IRecintoRepositorio recintoRepositorio)
        {
            _agendamentoRepositorio = agendamentoRepositorio;
            _motoristaRepositorio = motoristaRepositorio;
            _veiculoRepositorio = veiculoRepositorio;
            _empresaRepositorio = empresaRepositorio;
            _recintoRepositorio = recintoRepositorio;
        }

        public ActionResult Index()
        {
            var agendamentos = _agendamentoRepositorio
                .ObterAgendamentos(User.ObterTransportadoraId());

            return View(agendamentos);
        }

        [HttpGet]
        public ActionResult Cadastrar()
        {
            var viewModel = new AgendamentoViewModel
            {
                Motoristas = new List<Motorista>(),
                Veiculos = new List<Veiculo>()
            };

            viewModel.Motoristas = _motoristaRepositorio
                .ObterUltimos5MotoristasAgendados(User.ObterTransportadoraId())
                .ToList();

            viewModel.Veiculos = _veiculoRepositorio
                .ObterUltimos5VeiculosAgendados(User.ObterTransportadoraId())
                .ToList();

            viewModel.RecintosTRA = _recintoRepositorio
                .ObterRecintos(User.ObterTransportadoraId(), TipoAgendamento.TRA)
                .ToList();

            viewModel.RecintosDEPOT = _recintoRepositorio
                .ObterRecintos(User.ObterTransportadoraId(), TipoAgendamento.DEPOT)
                .ToList();

            viewModel.TipoAgendamento = TipoAgendamento.TRA;

            ObterPeriodos(viewModel);

            return View(viewModel);
        }

        public void ValidarBDCCMotorista(Motorista motorista)
        {
            using (var ws = new WsBDCC.WsSincrono())
            {
                var response = ws.ConsultaCpf(motorista.CPF.RemoverCaracteresEspeciais(), 0, "?", 0);

                if (response != null)
                {
                    if (BDCCHelpers.ErroBDCC(response.CodigoRetorno))
                    {
                        throw new Exception(response.DescricaoRetorno);
                    }
                }
            }
        }

        public void ValidarBDCCVeiculo(Veiculo veiculo)
        {
            using (var ws = new WsBDCC.WsSincrono())
            {
                var response = ws.ConsultaRenavam(veiculo.Renavam ?? string.Empty, 0);

                if (response != null)
                {
                    if (BDCCHelpers.ErroBDCC(response.CodigoRetorno))
                    {
                        throw new Exception(response.DescricaoRetorno);
                    }
                }
            }
        }

        [HttpPost]
        public ActionResult Cadastrar([Bind(Include = "MotoristaId, VeiculoId, CTE, PeriodoId, RecintoSelecionadoTRA, RecintoSelecionadoDEPOT, Quantidade, TipoOperacao, TipoAgendamento, IMO, Excesso")] AgendamentoViewModel viewModel)
        {
            try
            {
                var agendamento = new Agendamento(
                    viewModel.MotoristaId,
                    viewModel.VeiculoId,
                    viewModel.CTE,
                    User.ObterTransportadoraId(),
                    viewModel.PeriodoId,
                    viewModel.RecintoSelecionadoTRA,
                    viewModel.RecintoSelecionadoDEPOT,
                    viewModel.TipoOperacao,
                    viewModel.TipoAgendamento,
                    viewModel.Quantidade,
                    viewModel.IMO,
                    viewModel.Excesso,
                    User.ObterId());

                if (!Validar(agendamento))
                    return RetornarErros();

                var totalAgendado = 0;
                var totalEstoque = 0;

                if (viewModel.TipoAgendamento == TipoAgendamento.TRA)
                {
                    totalEstoque = _recintoRepositorio
                        .ObterTotalEstoqueTRA(viewModel.RecintoSelecionadoTRA);

                    totalAgendado = _agendamentoRepositorio
                        .ObterTotalAgendadoTRA(viewModel.RecintoSelecionadoTRA);
                }
                else
                {
                    totalEstoque = _recintoRepositorio
                        .ObterTotalEstoqueDEPOT(viewModel.RecintoSelecionadoDEPOT);

                    totalAgendado = _agendamentoRepositorio
                        .ObterTotalAgendadoDEPOT(viewModel.RecintoSelecionadoDEPOT);
                }

                if (viewModel.Quantidade > (totalEstoque - totalAgendado))
                    throw new Exception("A quantidade informada ultrapassa o total disponível para este Recinto");

                var periodoBusca = _agendamentoRepositorio.ObterPeriodoPorId(viewModel.PeriodoId);

                if (periodoBusca == null)
                    throw new Exception("Período não encontrado");

                var existeAgendamento = _agendamentoRepositorio
                    .ObterAgendamentosPorPeriodoEVeiculo(User.ObterTransportadoraId(), viewModel.PeriodoId, viewModel.VeiculoId, viewModel.TipoAgendamento)
                    .Any();

      //          if (existeAgendamento)
        //            throw new Exception("Já existe um agendamento para o mesmo veículo e período");

                var motoristaBusca = _motoristaRepositorio.ObterMotoristaPorId(viewModel.MotoristaId);

                if (motoristaBusca == null)
                    throw new Exception("Motorista não encontrado ou excluído");

                if (motoristaBusca.Inativo)
                    throw new Exception("Motorista bloqueado no Terminal");

                ValidarBDCCMotorista(motoristaBusca);

                var veiculoBusca = _veiculoRepositorio.ObterVeiculoPorId(viewModel.VeiculoId);

                if (veiculoBusca == null)
                    throw new Exception("Veículo não encontrado ou excluído");

                ValidarBDCCVeiculo(veiculoBusca);

                if (!ModelState.IsValid)
                    return RetornarErros();

                agendamento.Id = viewModel.TipoAgendamento == TipoAgendamento.TRA
                    ? _agendamentoRepositorio.CadastrarAgendamentoTRA(agendamento)
                    : _agendamentoRepositorio.CadastrarAgendamentoDEPOT(agendamento);

                return JavaScript($"window.location = '{Url.Action(nameof(Concluido), new { agendamento.Id, tipoAgendamento = viewModel.TipoAgendamento })}'");
            }
            catch (Exception ex)
            {
                return RetornarErro(ex.Message);
            }
        }

        public AgendamentoViewModel CarregarDadosAgendamento(AgendamentoDTO agendamento)
        {
            var motoristas = new List<Motorista>()
            {
                new Motorista(agendamento.MotoristaId, agendamento.MotoristaDescricao)
            };

            var veiculos = new List<Veiculo>()
            {
                new Veiculo(agendamento.VeiculoId, agendamento.VeiculoDescricao)
            };

            var detalhesMotorista = _motoristaRepositorio.ObterMotoristaPorId(agendamento.MotoristaId);
            var detalhesVeiculo = _veiculoRepositorio.ObterVeiculoPorId(agendamento.VeiculoId);

            var viewModel = new AgendamentoViewModel
            {
                Id = agendamento.Id,
                TipoOperacao = agendamento.TipoOperacao,
                MotoristaId = agendamento.MotoristaId,
                VeiculoId = agendamento.VeiculoId,
                CTE = agendamento.CTE,
                PeriodoId = agendamento.PeriodoId,
                Motoristas = motoristas,
                Veiculos = veiculos,
                RecintoSelecionadoTRA = agendamento.RecintoTRA,
                RecintoSelecionadoDEPOT = agendamento.RecintoDEPOT,
                TipoAgendamento = agendamento.TipoAgendamento,
                IMO = agendamento.IMO,
                Excesso = agendamento.Excesso,
                Quantidade = agendamento.Quantidade,
                DetalhesMotorista = detalhesMotorista,
                DetalhesVeiculo = detalhesVeiculo,
                Protocolo = $"{agendamento.Protocolo}/{agendamento.AnoProtocolo}",
                PossuiEntrada = agendamento.DataEntrada != null
            };

            if (agendamento.TipoAgendamento == TipoAgendamento.TRA)
            {
                viewModel.RecintosTRA = _recintoRepositorio
                    .ObterRecintos(User.ObterTransportadoraId(), TipoAgendamento.TRA)
                    .ToList();

                viewModel.Total = _recintoRepositorio
                   .ObterTotalEstoqueTRA(agendamento.RecintoTRA);

                viewModel.Agendados = _agendamentoRepositorio
                    .ObterTotalAgendadoTRA(agendamento.RecintoTRA);
            }
            else
            {

                viewModel.RecintosDEPOT = _recintoRepositorio
                    .ObterRecintos(User.ObterTransportadoraId(), TipoAgendamento.DEPOT)
                    .ToList();

                viewModel.Total = _recintoRepositorio
                   .ObterTotalEstoqueDEPOT(agendamento.RecintoDEPOT);

                viewModel.Agendados = _agendamentoRepositorio
                    .ObterTotalAgendadoDEPOT(agendamento.RecintoDEPOT);
            }

            viewModel.Disponiveis = viewModel.Total - viewModel.Agendados;

            ObterPeriodos(viewModel);

            return viewModel;
        }

        [HttpGet]
        [ValidaTransportadoraAgendamentoFilter]
        public ActionResult Atualizar(int? id, TipoAgendamento tipoAgendamento)
        {
            if (id == null)
                return RedirectToAction(nameof(Index));

            var agendamento = _agendamentoRepositorio.ObterDetalhesAgendamento(id.Value, tipoAgendamento);

            if (agendamento == null)
                return RegistroNaoEncontrado();

            var dados = CarregarDadosAgendamento(agendamento);

            return View(dados);
        }

        [HttpPost]
        public ActionResult Atualizar([Bind(Include = "Id, MotoristaId, VeiculoId, CTE, PeriodoId, RecintoSelecionadoTRA, RecintoSelecionadoDEPOT, Quantidade, TipoOperacao, TipoAgendamento, IMO, Excesso")] AgendamentoViewModel viewModel, int? id)
        {
            if (id == null)
                return RedirectToAction(nameof(Index));

            var agendamento = _agendamentoRepositorio.ObterAgendamentoPorId(id.Value, viewModel.TipoAgendamento);

            if (agendamento == null)
                return RegistroNaoEncontrado();

            var motoristaAnterior = agendamento.MotoristaId;

            agendamento.Alterar(
                new Agendamento(
                    viewModel.MotoristaId,
                    viewModel.VeiculoId,
                    viewModel.CTE,
                    viewModel.PeriodoId,
                    viewModel.RecintoSelecionadoTRA,
                    viewModel.RecintoSelecionadoDEPOT,
                    viewModel.Quantidade,
                    viewModel.IMO,
                    viewModel.Excesso,
                    agendamento.TipoAgendamento));

            if (!Validar(agendamento))
                return RetornarErros();

            var totalAgendado = 0;
            var totalEstoque = 0;

            if (viewModel.TipoAgendamento == TipoAgendamento.TRA)
            {
                totalEstoque = _recintoRepositorio
                    .ObterTotalEstoqueTRA(viewModel.RecintoSelecionadoTRA);

                totalAgendado = _agendamentoRepositorio
                    .ObterTotalAgendadoTRA(viewModel.RecintoSelecionadoTRA, agendamento.Id);
            }
            else
            {
                totalEstoque = _recintoRepositorio
                    .ObterTotalEstoqueDEPOT(viewModel.RecintoSelecionadoDEPOT);

                totalAgendado = _agendamentoRepositorio
                    .ObterTotalAgendadoDEPOT(viewModel.RecintoSelecionadoDEPOT, agendamento.Id);
            }

            if (viewModel.Quantidade > (totalEstoque - totalAgendado))
                throw new Exception("A quantidade informada ultrapassa o total disponível para este Recinto");

            var periodoBusca = _agendamentoRepositorio.ObterPeriodoPorId(viewModel.PeriodoId);

            if (periodoBusca == null)
                ModelState.AddModelError(string.Empty, "Período não encontrado");

            var existeAgendamento = _agendamentoRepositorio
                .ObterAgendamentosPorPeriodoEVeiculo(User.ObterTransportadoraId(), viewModel.PeriodoId, viewModel.VeiculoId, viewModel.TipoAgendamento)
                .Where(c => c.Id != agendamento.Id)
                .Any();

        //    if (existeAgendamento)
          //      ModelState.AddModelError(string.Empty, "Já existe um agendamento para o mesmo veículo e período");

            var motoristaBusca = _motoristaRepositorio.ObterMotoristaPorId(viewModel.MotoristaId);

            if (motoristaBusca == null)
                agendamento.AdicionarNotificacao("Motorista não encontrado ou excluído");

            if (viewModel.MotoristaId != motoristaAnterior)
            {
                if (motoristaBusca.Inativo)
                    agendamento.AdicionarNotificacao("Motorista bloqueado no Terminal");
            }

            ValidarBDCCMotorista(motoristaBusca);

            var veiculoBusca = _veiculoRepositorio.ObterVeiculoPorId(viewModel.VeiculoId);

            if (veiculoBusca == null)
                agendamento.AdicionarNotificacao("Veículo não encontrado ou excluído");

            ValidarBDCCVeiculo(veiculoBusca);

            if (!Validar(agendamento))
                return RetornarErros();

            if (viewModel.TipoAgendamento == TipoAgendamento.TRA)
            {
                _agendamentoRepositorio.AtualizarAgendamentoTRA(agendamento);
            }
            else
            {
                _agendamentoRepositorio.AtualizarAgendamentoDEPOT(agendamento);
            }

            return JavaScript($"window.location = '{Url.Action(nameof(Concluido), new { id, tipoAgendamento = viewModel.TipoAgendamento })}'");
        }

        [HttpGet]
        [ValidaTransportadoraAgendamentoFilter]
        public ActionResult Visualizar(int? id, TipoAgendamento tipoAgendamento)
        {
            if (id == null)
                return RedirectToAction(nameof(Index));

            var agendamento = _agendamentoRepositorio.ObterDetalhesAgendamento(id.Value, tipoAgendamento);

            if (agendamento == null)
                return RegistroNaoEncontrado();

            var viewModel = CarregarDadosAgendamento(agendamento);

            return View(viewModel);
        }

        [HttpGet]
        public PartialViewResult ObterMotoristasPorNomeOuCNH(string descricao)
        {
            var motoristas = _motoristaRepositorio.ObterMotoristasPorNomeOuCNH(descricao, User.ObterTransportadoraId());

            return PartialView("_PesquisarMotoristasConsulta", motoristas);
        }

        [HttpGet]
        public PartialViewResult ObterDetalhesMotorista(int motoristaId)
        {
            var motorista = _motoristaRepositorio.ObterMotoristaPorId(motoristaId);

            return PartialView("_DetalhesMotorista", motorista);
        }

        [HttpGet]
        public PartialViewResult ObterVeiculosPorPlaca(string descricao)
        {
            var veiculos = _veiculoRepositorio.ObterVeiculosPorPlaca(descricao, User.ObterTransportadoraId());

            return PartialView("_PesquisarVeiculosConsulta", veiculos);
        }

        [HttpGet]
        public PartialViewResult ObterDetalhesVeiculo(int veiculoId)
        {
            var veiculo = _veiculoRepositorio.ObterVeiculoPorId(veiculoId);

            return PartialView("_DetalhesVeiculo", veiculo);
        }

        [HttpGet]
        public PartialViewResult ObterPeriodos(TipoAgendamento tipoAgendamento)
        {
            var viewModel = new AgendamentoViewModel();
            viewModel.TipoAgendamento = tipoAgendamento;

            ObterPeriodos(viewModel);

            return PartialView("_Periodos", viewModel.Periodos);
        }

        [ValidaTransportadoraAgendamentoFilter]
        public ActionResult Protocolo(int id, TipoAgendamento tipoAgendamento)
        {
            var agendamento = _agendamentoRepositorio.ObterDadosProtocolo(id, tipoAgendamento);

            if (agendamento == null)
                return RedirectToAction(nameof(Index));

            var empresaBusca = _empresaRepositorio.ObterEmpresaPorId(1);

            if (empresaBusca == null)
                throw new Exception($"Empresa não encontrada");

            var viewModel = new AgendamentoViewModel
            {
                Protocolo = string.Concat(agendamento.Protocolo.PadLeft(6, '0'), "/", agendamento.AnoProtocolo),
                MotoristaDescricao = agendamento.MotoristaDescricao,
                MotoristaCPF = agendamento.MotoristaCPF,
                MotoristaCNH = agendamento.MotoristaCNH,
                VeiculoDescricao = agendamento.VeiculoDescricao,
                PeriodoDescricao = agendamento.PeriodoDescricao,
                TransportadoraDescricao = agendamento.TransportadoraDescricao,
                TransportadoraDocumento = agendamento.TransportadoraDocumento,
                RecintoDescricao = agendamento.RecintoDescricao,
                CTE = agendamento.CTE,
                Empresa = empresaBusca,
                Quantidade = agendamento.Quantidade,
                IMO = agendamento.IMO,
                Excesso = agendamento.Excesso,
                DataCadastro = agendamento.DataCriacao,
                TipoAgendamento = agendamento.TipoAgendamento
            };

            viewModel.RecintosTRA = _recintoRepositorio
                .ObterRecintos(User.ObterTransportadoraId(), TipoAgendamento.TRA)
                .ToList();

            viewModel.RecintosDEPOT = _recintoRepositorio
                .ObterRecintos(User.ObterTransportadoraId(), TipoAgendamento.DEPOT)
                .ToList();

            _agendamentoRepositorio.AtualizarProtocoloImpresso(agendamento.Id, agendamento.TipoAgendamento);

            return new PdfActionResult(viewModel);
        }

        [HttpPost]
        [ValidaTransportadoraAgendamentoFilter]
        public ActionResult Excluir(int id, TipoAgendamento tipoAgendamento)
        {
            try
            {
                var agendamento = _agendamentoRepositorio.ObterAgendamentoPorId(id, tipoAgendamento);

                if (agendamento == null)
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Agendamento não encontrado");

                if (agendamento.TransportadoraId != User.ObterTransportadoraId())
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Agendamento não pertence a transportadora");

                if (agendamento.DataEntrada != null)
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Exclusão não permitida. A Carga já entrou no Terminal");

                _agendamentoRepositorio.Excluir(agendamento.Id, agendamento.TipoAgendamento);
            }
            catch
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            return new HttpStatusCodeResult(HttpStatusCode.NoContent);
        }

        [HttpGet]
        [ValidaTransportadoraAgendamentoFilter]
        public ActionResult Concluido(int id, TipoAgendamento tipoAgendamento)
        {
            var agendamento = _agendamentoRepositorio.ObterDetalhesAgendamento(id, tipoAgendamento);

            if (agendamento == null)
                return RedirectToAction(nameof(Index));

            return View(new AgendamentoViewModel
            {
                Id = agendamento.Id,
                Protocolo = string.Concat(agendamento.Protocolo.PadLeft(6, '0'), "/", agendamento.AnoProtocolo),
                MotoristaDescricao = agendamento.MotoristaDescricao,
                VeiculoDescricao = agendamento.VeiculoDescricao,
                PeriodoDescricao = agendamento.PeriodoDescricao,
                TransportadoraDescricao = agendamento.TransportadoraDescricao,
                RecintoDescricao = agendamento.RecintoDescricao,
                DataCadastro = agendamento.DataCriacao,
                TipoAgendamento = tipoAgendamento
            });
        }

        [HttpPost]
        public ActionResult AtualizarMotorista(int agendamentoId, int motoristaId, TipoAgendamento tipoAgendamento)
        {
            var agendamentoBusca = _agendamentoRepositorio.ObterAgendamentoPorId(agendamentoId, tipoAgendamento);

            if (agendamentoBusca == null)
                return RetornarErro("Agendamento inexistente");

            var motoristaBusca = _motoristaRepositorio.ObterMotoristaPorId(motoristaId);

            if (motoristaBusca == null)
                return RetornarErro("Motorista inexistente");

            if (agendamentoBusca.TransportadoraId != User.ObterTransportadoraId())
                return RetornarErro("Este agendamento não pertence a esta Transportadora");

            ValidarBDCCMotorista(motoristaBusca);

            _agendamentoRepositorio.AtualizarMotoristaAgendamento(agendamentoId, motoristaId, agendamentoBusca.TipoAgendamento);

            return new HttpStatusCodeResult(HttpStatusCode.NoContent);
        }

        [HttpPost]
        public ActionResult AtualizarVeiculo(int agendamentoId, int veiculoId, TipoAgendamento tipoAgendamento)
        {
            var agendamentoBusca = _agendamentoRepositorio.ObterAgendamentoPorId(agendamentoId, tipoAgendamento);

            if (agendamentoBusca == null)
                return RetornarErro("Agendamento inexistente");

            var veiculoBusca = _veiculoRepositorio.ObterVeiculoPorId(veiculoId);

            if (veiculoBusca == null)
                return RetornarErro("Veículo inexistente");

            if (agendamentoBusca.TransportadoraId != User.ObterTransportadoraId())
                return RetornarErro("Este agendamento não pertence a esta Transportadora");

            ValidarBDCCVeiculo(veiculoBusca);

            _agendamentoRepositorio.AtualizarVeiculoAgendamento(agendamentoId, veiculoId, agendamentoBusca.TipoAgendamento);

            return new HttpStatusCodeResult(HttpStatusCode.NoContent);
        }

        [HttpGet]
        public ActionResult ObterTotais(string recinto, TipoAgendamento tipoAgendamento)
        {
            var totalAgendado = 0;
            var totalEstoque = 0;

            if (tipoAgendamento == TipoAgendamento.TRA)
            {
                totalEstoque = _recintoRepositorio
                    .ObterTotalEstoqueTRA(recinto.ToInt());

                totalAgendado = _agendamentoRepositorio
                    .ObterTotalAgendadoTRA(recinto.ToInt());
            }
            else
            {
                totalEstoque = _recintoRepositorio
                    .ObterTotalEstoqueDEPOT(recinto);

                totalAgendado = _agendamentoRepositorio
                    .ObterTotalAgendadoDEPOT(recinto);
            }

            var disponiveis = totalEstoque - totalAgendado;

            if (disponiveis < 0)
                disponiveis = 0;

            return Json(new
            {
                Total = totalEstoque,
                TotalAgendado = totalAgendado,
                Disponiveis = disponiveis
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        private void ObterPeriodos(AgendamentoViewModel viewModel)
        {
            var periodos = _agendamentoRepositorio
                    .ObterPeriodos(viewModel.TipoAgendamento)
                    .Where(c => c.PeriodoInicial > DateTime.Now.AddHours(0))
                    .ToList();

            if (viewModel.Id > 0)
            {
                var periodoAgendamento = _agendamentoRepositorio.ObterPeriodoPorId(viewModel.PeriodoId);

                if (!periodos.Any(c => c.Id == viewModel.PeriodoId))
                    periodos.Add(periodoAgendamento);

                foreach (var periodo in periodos)
                    periodo.PeriodoAgendado = viewModel.PeriodoId == periodo.Id;
            }

            viewModel.Periodos = periodos.ToList().OrderBy(c => c.PeriodoInicial).ToList();
        }
    }
}