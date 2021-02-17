using Ecoporto.AgendamentoCS.Config;
using Ecoporto.AgendamentoCS.Dados.Interfaces;
using Ecoporto.AgendamentoCS.Enums;
using Ecoporto.AgendamentoCS.Extensions;
using Ecoporto.AgendamentoCS.Helpers;
using Ecoporto.AgendamentoCS.Models;
using Ecoporto.AgendamentoCS.Models.ViewModels;
using MvcRazorToPdf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Ecoporto.AgendamentoCS.Controllers
{
    [Authorize]
    public class AgendamentoController : BaseController
    {
        private readonly IAgendamentoRepositorio _agendamentoRepositorio;
        private readonly IMotoristaRepositorio _motoristaRepositorio;
        private readonly IVeiculoRepositorio _veiculoRepositorio;
        private readonly IReservaRepositorio _reservaRepositorio;
        private readonly IEmpresaRepositorio _empresaRepositorio;
        private readonly IUploadRepositorio _uploadRepositorio;

        public AgendamentoController(
            IAgendamentoRepositorio agendamentoRepositorio,
            IMotoristaRepositorio motoristaRepositorio,
            IVeiculoRepositorio veiculoRepositorio,
            IReservaRepositorio reservaRepositorio,
            IEmpresaRepositorio empresaRepositorio,
            IUploadRepositorio uploadRepositorio)
        {
            _agendamentoRepositorio = agendamentoRepositorio;
            _motoristaRepositorio = motoristaRepositorio;
            _veiculoRepositorio = veiculoRepositorio;
            _reservaRepositorio = reservaRepositorio;
            _empresaRepositorio = empresaRepositorio;
            _uploadRepositorio = uploadRepositorio;
        }

        public ActionResult Index()
        {
            var agendamentos = _agendamentoRepositorio
                .ObterAgendamentos(User.ObterTransportadoraId());

            return View(agendamentos);
        }

        [HttpGet]
        private void ObterPeriodos(AgendamentoViewModel viewModel)
        {
            DateTime menorData = DateTime.MinValue;
            DateTime maiorData = DateTime.MaxValue;

            int limiteHoraLateArrival = 0;

            var reservas = viewModel.Reservas;

            if (reservas.Any())
            {
                menorData = reservas.Select(c => c.Abertura).Min();
                maiorData = reservas.Select(c => c.Fechamento).Max().AddSeconds(59);
                limiteHoraLateArrival = reservas.Sum(c => c.LimiteHoraLateArrival);
            }

            if (maiorData == DateTime.MinValue)
                maiorData = DateTime.Now.AddDays(15);

            var periodos = _agendamentoRepositorio
                .ObterPeriodos(menorData, maiorData, viewModel.CargaProjeto);

            if (viewModel.CargaProjeto)
                periodos = periodos.Where(c => c.PeriodoInicial > DateTime.Now.AddHours(36));
            else
                periodos = periodos.Where(c => c.PeriodoInicial > DateTime.Now.AddHours(6));

            var periodosExibicao = periodos.ToList();

            if (viewModel.Id > 0)
            {
                var periodoAgendamento = _agendamentoRepositorio.ObterPeriodoPorId(viewModel.PeriodoId);

                if (!periodosExibicao.Any(c => c.Id == viewModel.PeriodoId))
                    periodosExibicao.Add(periodoAgendamento);

                foreach (var periodo in periodosExibicao)
                    periodo.PeriodoAgendado = viewModel.PeriodoId == periodo.Id;
            }

            viewModel.Periodos = periodosExibicao.OrderBy(c => c.PeriodoInicial).ToList();
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

            ObterCFOPS(viewModel);

            GerenciadorDeEstado<Upload>.RemoverTodos();
            GerenciadorDeEstado<NotaFiscal>.RemoverTodos();
            GerenciadorDeEstado<ReservaItem>.RemoverTodos();

            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Cadastrar([Bind(Include = "MotoristaId, VeiculoId, CTE, Reserva, ViagemId, BookingCsId, PeriodoId, Cegonha, EmailRegistro")] AgendamentoViewModel viewModel)
        {
            try
            {
                var agendamento = new Agendamento(
                viewModel.MotoristaId,
                viewModel.VeiculoId,
                viewModel.CTE,
                viewModel.Cegonha,
                viewModel.Desembaracada,
                User.ObterTransportadoraId(),
                viewModel.Reserva,
                viewModel.BookingCsId,
                viewModel.PeriodoId,
                viewModel.EmailRegistro,
                User.ObterId());

                var itens = GerenciadorDeEstado<ReservaItem>.RetornarTodos();

                if (itens.Any(c => c.ClassificacaoId == ClassificacaoCarga.CargaProjeto))
                {
                    if (itens.Any(c => c.ClassificacaoId != ClassificacaoCarga.CargaProjeto))
                        agendamento.AdicionarNotificacao($"Não é possível agendar Carga Projeto com os demais tipos de cargas!");
                }

                foreach (var item in itens)
                {
                    item.NotasFiscais.Clear();

                    var reservaBusca = _reservaRepositorio.AberturaPraca(item.Reserva);

                    if (reservaBusca == null)
                        agendamento.AdicionarNotificacao($"Reserva {item.Reserva} não encontrada");

                    var notas = GerenciadorDeEstado<NotaFiscal>.RetornarTodos()
                        .Where(c => c.BookingCsItemId == item.BookingCsItemId).ToList();

                    if (notas.Count > 0)
                        item.NotasFiscais.AddRange(notas);
                    else
                    {
                        if (item.ExigeNF)
                        {
                            agendamento.AdicionarNotificacao($"Nenhuma DANFE informada para o item {item.BookingCsItemId} / Reserva {item.Reserva}");
                        }
                    }

                    //if (item.PackingList)
                    //{
                    //    var uploads = GerenciadorDeEstado<Upload>.RetornarTodos()
                    //        .Where(c => c.BookingCsItemId == item.BookingCsItemId && c.TipoDocumentoId == 1).ToList();

                    //    if (uploads.Count == 0)
                    //        agendamento.AdicionarNotificacao($"É necessário anexar o documento de Packing List para o item {item.BookingCsItemId} / Reserva {item.Reserva}");
                    //}

                    //if (item.DesenhoTecnico)
                    //{
                    //    var uploads = GerenciadorDeEstado<Upload>.RetornarTodos()
                    //        .Where(c => c.BookingCsItemId == item.BookingCsItemId && c.TipoDocumentoId == 2).ToList();

                    //    if (uploads.Count == 0)
                    //        agendamento.AdicionarNotificacao($"É necessário anexar o documento de Desenho Técnico para o item {item.BookingCsItemId} / Reserva {item.Reserva}");
                    //}

                    //if (item.ImagemCarga)
                    //{
                    //    var uploads = GerenciadorDeEstado<Upload>.RetornarTodos()
                    //        .Where(c => c.BookingCsItemId == item.BookingCsItemId && c.TipoDocumentoId == 3).ToList();

                    //    if (uploads.Count == 0)
                    //        agendamento.AdicionarNotificacao($"É necessário anexar o documento de Imagem Carga para o item {item.BookingCsItemId} / Reserva {item.Reserva}");
                    //}
                }

                var exigeEmail = itens.Where(c => c.PackingList || c.DesenhoTecnico || c.ImagemCarga).Any();

                if (exigeEmail && string.IsNullOrEmpty(viewModel.EmailRegistro))
                    agendamento.AdicionarNotificacao($"É necessário o Email para contato");

                if (exigeEmail && !string.IsNullOrEmpty(viewModel.EmailRegistro))
                {
                    var retorno = Validation.ValidarListaDeEmails(viewModel.EmailRegistro);

                    foreach (var erro in retorno)
                        agendamento.AdicionarNotificacao(erro.ErrorMessage);
                }

                var periodoBusca = _agendamentoRepositorio.ObterPeriodoPorId(viewModel.PeriodoId);

                if (periodoBusca == null)
                    agendamento.AdicionarNotificacao("Período não encontrado");

                var existeAgendamento = _agendamentoRepositorio
                    .ObterAgendamentosPorPeriodoEVeiculo(User.ObterTransportadoraId(), viewModel.PeriodoId, viewModel.VeiculoId)
                    .Any();

             //   if (existeAgendamento)
               //     agendamento.AdicionarNotificacao("Já existe um agendamento para o mesmo veículo e período");

                var motoristaBusca = _motoristaRepositorio.ObterMotoristaPorId(viewModel.MotoristaId);

                if (motoristaBusca == null)
                    agendamento.AdicionarNotificacao("Motorista não encontrado ou excluído");

                if (motoristaBusca.Inativo)
                    agendamento.AdicionarNotificacao("Motorista bloqueado no Terminal");

                if (AppConfig.ValidarBDCC())
                {
                    using (var ws = new WsBDCC.WsSincrono())
                    {
                        try
                        {
                            var response = ws.ConsultaCpf(motoristaBusca.CPF.RemoverCaracteresEspeciais(), 0, "?", 0);

                            if (response != null)
                            {
                                if (BDCCHelpers.ErroBDCC(response.CodigoRetorno))
                                {
                                    ModelState.AddModelError(string.Empty, response.DescricaoRetorno);
                                    return View(viewModel);
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            agendamento.AdicionarNotificacao("BDCC: " + ex.Message);
                        }
                    }
                }

                var veiculoBusca = _veiculoRepositorio.ObterVeiculoPorId(viewModel.VeiculoId);

                if (veiculoBusca == null)
                    agendamento.AdicionarNotificacao("Veículo não encontrado ou excluído");

                if (AppConfig.ValidarBDCC())
                {
                    using (var ws = new WsBDCC.WsSincrono())
                    {
                        try
                        {
                            var response = ws.ConsultaRenavam(veiculoBusca?.Renavam ?? string.Empty, 0);

                            if (response != null)
                            {
                                if (BDCCHelpers.ErroBDCC(response.CodigoRetorno))
                                {
                                    ModelState.AddModelError(string.Empty, response.DescricaoRetorno);
                                    return View(viewModel);
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            agendamento.AdicionarNotificacao("BDCC: " + ex.Message);
                        }
                    }
                }

                if (!Validar(agendamento))
                    return RetornarErros();

                agendamento.AdicionarItens(itens);

                agendamento.Id = _agendamentoRepositorio.Cadastrar(agendamento);

                foreach (var documento in GerenciadorDeEstado<Upload>.RetornarTodos())
                {
                    if (!_uploadRepositorio.DocumentoJaExistente(agendamento.Id, documento.TipoDocumentoId))
                    {
                        WsSharepoint.ImagemAverbacao img = new WsSharepoint.ImagemAverbacao()
                        {
                            NomeImagem = documento.Arquivo,
                            IdTipoDocUpload = documento.TipoDocumentoId,
                            Lote = documento.BookingCsItemId,
                            TipoDocumento = documento.TipoDocumento,
                            _byteArrayImagem = Convert.FromBase64String(documento.Base64),
                            CaminhoImagem = " ",
                            DataInclusao = DateTime.Now,
                            AutonumAgendamento = agendamento.Id,
                            TipoDocAgendamento = "CSOP"
                        };

                        WsSharepoint.WsIccSharepoint ws = new WsSharepoint.WsIccSharepoint();

                        try
                        {
                            var imgId = ws.EnviarImagemDocAverbacaoRetID(img);
                        }
                        catch (Exception ex)
                        {
                            return RetornarErro("Falha na comunicação com o Web Service Sharepoint.");
                        }
                    }
                }

                return JavaScript($"window.location = '{Url.Action(nameof(Concluido), new { agendamento.Id })}'");
            }
            catch (Exception ex)
            {
                return RetornarErro(ex.ToString());
            }
        }

        [HttpGet]
        public ActionResult Atualizar(int? id)
        {
            if (id == null)
                return RedirectToAction(nameof(Index));

            var agendamento = _agendamentoRepositorio.ObterDetalhesAgendamento(id.Value);

            if (agendamento == null)
                return RegistroNaoEncontrado();

            if (agendamento.TransportadoraId != User.ObterTransportadoraId())
                return RedirectToAction(nameof(Index));

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
                MotoristaId = agendamento.MotoristaId,
                VeiculoId = agendamento.VeiculoId,
                CTE = agendamento.CTE,
                PeriodoId = agendamento.PeriodoId,
                Motoristas = motoristas,
                Veiculos = veiculos,
                Navio = $"{agendamento.Navio} / {agendamento.Viagem}",
                Viagem = agendamento.Viagem,
                Abertura = agendamento.Abertura,
                Fechamento = agendamento.Fechamento,
                Cegonha = agendamento.Cegonha,
                EmailRegistro = agendamento.EmailRegistro,
                Desembaracada = agendamento.Desembaracada,
                DetalhesMotorista = detalhesMotorista,
                DetalhesVeiculo = detalhesVeiculo,
                PossuiEntrada = agendamento.DataEntrada != null
            };

            GerenciadorDeEstado<Upload>.RemoverTodos();
            GerenciadorDeEstado<NotaFiscal>.RemoverTodos();
            GerenciadorDeEstado<ReservaItem>.RemoverTodos();

            var itens = _agendamentoRepositorio
                .ObterItensAgendamento(agendamento.Id).ToList();

            GerenciadorDeEstado<ReservaItem>.ArmazenarLista(itens);

            var danfes = _agendamentoRepositorio
                .ObterNotasFiscaisAgendamento(agendamento.Id).ToList();

            GerenciadorDeEstado<NotaFiscal>.ArmazenarLista(danfes);

            var uploads = _uploadRepositorio
                .ObterUploads(viewModel.Id).ToList();

            GerenciadorDeEstado<Upload>.ArmazenarLista(uploads);

            viewModel.ItensReserva = GerenciadorDeEstado<ReservaItem>.RetornarTodos();

            var reservas = _agendamentoRepositorio.ObterPeriodosReservasCargaSolta(itens.Select(c => c.Reserva).ToArray());

            viewModel.Reservas = reservas.ToList();

            var cargaProjeto = false;

            foreach (var item2 in viewModel.ItensReserva.Select(c => c.BookingCsItemId))
            {
                if (_agendamentoRepositorio.ItemCargaProjeto(item2))
                {
                    cargaProjeto = true;

                    break;
                }
            }

            viewModel.CargaProjeto = cargaProjeto;

            ObterCFOPS(viewModel);
            ObterPeriodos(viewModel);

            return View(viewModel);
        }

        [HttpGet]
        public ActionResult Visualizar(int? id)
        {
            if (id == null)
                return RedirectToAction(nameof(Index));

            var agendamento = _agendamentoRepositorio.ObterDetalhesAgendamento(id.Value);

            if (agendamento == null)
                return RegistroNaoEncontrado();

            if (agendamento.TransportadoraId != User.ObterTransportadoraId())
                return RedirectToAction(nameof(Index));

            var detalhesMotorista = _motoristaRepositorio.ObterMotoristaPorId(agendamento.MotoristaId);
            var detalhesVeiculo = _veiculoRepositorio.ObterVeiculoPorId(agendamento.VeiculoId);

            var viewModel = new AgendamentoViewModel
            {
                Id = agendamento.Id,
                MotoristaId = agendamento.MotoristaId,
                VeiculoId = agendamento.VeiculoId,
                CTE = agendamento.CTE,
                PeriodoId = agendamento.PeriodoId,
                Navio = agendamento.Navio,
                Viagem = agendamento.Viagem,
                Abertura = agendamento.Abertura,
                Fechamento = agendamento.Fechamento,
                Cegonha = agendamento.Cegonha,
                EmailRegistro = agendamento.EmailRegistro,
                Desembaracada = agendamento.Desembaracada,
                DetalhesMotorista = detalhesMotorista,
                DetalhesVeiculo = detalhesVeiculo
            };

            GerenciadorDeEstado<Upload>.RemoverTodos();
            GerenciadorDeEstado<NotaFiscal>.RemoverTodos();
            GerenciadorDeEstado<ReservaItem>.RemoverTodos();

            var itens = _agendamentoRepositorio
                .ObterItensAgendamento(agendamento.Id).ToList();

            GerenciadorDeEstado<ReservaItem>.ArmazenarLista(itens);

            var danfes = _agendamentoRepositorio
                .ObterNotasFiscaisAgendamento(agendamento.Id).ToList();

            GerenciadorDeEstado<NotaFiscal>.ArmazenarLista(danfes);

            var uploads = _uploadRepositorio
                .ObterUploads(viewModel.Id).ToList();

            GerenciadorDeEstado<Upload>.ArmazenarLista(uploads);

            viewModel.ItensReserva = GerenciadorDeEstado<ReservaItem>.RetornarTodos();

            var reservas = _agendamentoRepositorio.ObterReservasAgendamento(agendamento.Id);

            ObterCFOPS(viewModel);

            var periodoAgendamento = _agendamentoRepositorio
                .ObterPeriodoPorId(agendamento.PeriodoId);

            viewModel.Periodos.Add(periodoAgendamento);

            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Atualizar([Bind(Include = "Id, MotoristaId, VeiculoId, CTE, Reserva, PeriodoId, ItensReserva, Cegonha, Desembaracada, EmailRegistro")] AgendamentoViewModel viewModel, int? id)
        {
            if (id == null)
                return RedirectToAction(nameof(Index));

            var agendamento = _agendamentoRepositorio.ObterAgendamentoPorId(id.Value);

            if (agendamento == null)
                return RegistroNaoEncontrado();

            var motoristaAnterior = agendamento.MotoristaId;

            agendamento.Alterar(
                new Agendamento(
                    viewModel.MotoristaId,
                    viewModel.VeiculoId,
                    viewModel.CTE,
                    viewModel.Cegonha,
                    viewModel.Desembaracada,
                    User.ObterTransportadoraId(),
                    viewModel.Reserva,
                    viewModel.BookingCsId,
                    viewModel.PeriodoId,
                    viewModel.EmailRegistro,
                    User.ObterId()));

            var itens = GerenciadorDeEstado<ReservaItem>.RetornarTodos();

            if (itens.Any(c => c.ClassificacaoId == ClassificacaoCarga.CargaProjeto))
            {
                if (itens.Any(c => c.ClassificacaoId != ClassificacaoCarga.CargaProjeto))
                    agendamento.AdicionarNotificacao($"Não é possível agendar Carga Projeto com os demais tipos de cargas!");
            }

            foreach (var item in itens)
            {
                item.NotasFiscais.Clear();

                var reservaBusca = _reservaRepositorio.AberturaPraca(item.Reserva);

                if (reservaBusca == null)
                    agendamento.AdicionarNotificacao($"Reserva {item.Reserva} não encontrada");

                var notas = GerenciadorDeEstado<NotaFiscal>.RetornarTodos()
                    .Where(c => c.BookingCsItemId == item.BookingCsItemId).ToList();

                if (notas.Count > 0)
                    item.NotasFiscais.AddRange(notas);
                else
                {
                    if (item.ExigeNF)
                    {
                        agendamento.AdicionarNotificacao($"Nenhuma DANFE informada para o item {item.BookingCsItemId} / Reserva {item.Reserva}");
                    }
                }

                if (item.PackingList)
                {
                    var uploads = GerenciadorDeEstado<Upload>.RetornarTodos()
                        .Where(c => c.BookingCsItemId == item.BookingCsItemId && c.TipoDocumentoId == 1).ToList();

                    if (uploads.Count == 0)
                        agendamento.AdicionarNotificacao($"É necessário anexar o documento de Packing List para o item {item.BookingCsItemId} / Reserva {item.Reserva}");
                }

                if (item.DesenhoTecnico)
                {
                    var uploads = GerenciadorDeEstado<Upload>.RetornarTodos()
                        .Where(c => c.BookingCsItemId == item.BookingCsItemId && c.TipoDocumentoId == 2).ToList();

                    if (uploads.Count == 0)
                        agendamento.AdicionarNotificacao($"É necessário anexar o documento de Desenho Técnico para o item {item.BookingCsItemId} / Reserva {item.Reserva}");
                }

                if (item.ImagemCarga)
                {
                    var uploads = GerenciadorDeEstado<Upload>.RetornarTodos()
                        .Where(c => c.BookingCsItemId == item.BookingCsItemId && c.TipoDocumentoId == 3).ToList();

                    if (uploads.Count == 0)
                        agendamento.AdicionarNotificacao($"É necessário anexar o documento de Imagem Carga para o item {item.BookingCsItemId} / Reserva {item.Reserva}");
                }
            }

            var exigeEmail = itens.Where(c => c.PackingList || c.DesenhoTecnico || c.ImagemCarga).Any();

            if (exigeEmail && string.IsNullOrEmpty(viewModel.EmailRegistro))
                agendamento.AdicionarNotificacao($"É necessário o Email para contato");

            if (exigeEmail && !string.IsNullOrEmpty(viewModel.EmailRegistro))
            {
                var retorno = Validation.ValidarListaDeEmails(viewModel.EmailRegistro);

                foreach (var erro in retorno)
                    agendamento.AdicionarNotificacao(erro.ErrorMessage);
            }

            var periodoBusca = _agendamentoRepositorio.ObterPeriodoPorId(viewModel.PeriodoId);

            if (periodoBusca == null)
                ModelState.AddModelError(string.Empty, "Período não encontrado");

            var existeAgendamento = _agendamentoRepositorio
                .ObterAgendamentosPorPeriodoEVeiculo(User.ObterTransportadoraId(), viewModel.PeriodoId, viewModel.VeiculoId)
                .Where(c => c.Id != agendamento.Id)
                .Any();

           // if (existeAgendamento)
             //   ModelState.AddModelError(string.Empty, "Já existe um agendamento para o mesmo veículo e período");

            var motoristaBusca = _motoristaRepositorio.ObterMotoristaPorId(viewModel.MotoristaId);

            if (motoristaBusca == null)
                agendamento.AdicionarNotificacao("Motorista não encontrado ou excluído");

            if (viewModel.MotoristaId != motoristaAnterior)
            {
                if (motoristaBusca.Inativo)
                    agendamento.AdicionarNotificacao("Motorista bloqueado no Terminal");
            }
            
            if (AppConfig.ValidarBDCC())
            {
                using (var ws = new WsBDCC.WsSincrono())
                {
                    var response = ws.ConsultaCpf(motoristaBusca.CPF.RemoverCaracteresEspeciais(), 0, "?", 0);

                    if (response != null)
                    {
                        if (BDCCHelpers.ErroBDCC(response.CodigoRetorno))
                        {
                            ModelState.AddModelError(string.Empty, response.DescricaoRetorno);
                            return View(viewModel);
                        }
                    }
                }
            }

            var veiculoBusca = _veiculoRepositorio.ObterVeiculoPorId(viewModel.VeiculoId);

            if (veiculoBusca == null)
                agendamento.AdicionarNotificacao("Veículo não encontrado ou excluído");

            if (AppConfig.ValidarBDCC())
            {
                using (var ws = new WsBDCC.WsSincrono())
                {
                    var response = ws.ConsultaRenavam(veiculoBusca.Renavam ?? string.Empty, 0);

                    if (response != null)
                    {
                        if (BDCCHelpers.ErroBDCC(response.CodigoRetorno))
                        {
                            ModelState.AddModelError(string.Empty, response.DescricaoRetorno);
                            return View(viewModel);
                        }
                    }
                }
            }

            if (!Validar(agendamento))
                return RetornarErros();

            agendamento.AdicionarItens(itens);

            _agendamentoRepositorio.Atualizar(agendamento);

            foreach (var documento in GerenciadorDeEstado<Upload>.RetornarTodos())
            {
                if (!_uploadRepositorio.DocumentoJaExistente(agendamento.Id, documento.TipoDocumentoId))
                {
                    if (!string.IsNullOrEmpty(documento.Base64))
                    {
                        WsSharepoint.ImagemAverbacao img = new WsSharepoint.ImagemAverbacao()
                        {
                            NomeImagem = documento.Arquivo,
                            IdTipoDocUpload = documento.TipoDocumentoId,
                            Lote = documento.BookingCsItemId,
                            TipoDocumento = " ",
                            _byteArrayImagem = Convert.FromBase64String(documento.Base64),
                            CaminhoImagem = " ",
                            DataInclusao = DateTime.Now,
                            AutonumAgendamento = agendamento.Id,
                            TipoDocAgendamento = "CSOP"
                        };

                        WsSharepoint.WsIccSharepoint ws = new WsSharepoint.WsIccSharepoint();

                        try
                        {
                            var imgId = ws.EnviarImagemDocAverbacaoRetID(img);
                        }
                        catch (Exception ex)
                        {
                            throw;
                        }
                    }
                }
            }

            return JavaScript($"window.location = '{Url.Action(nameof(Concluido), new { id })}'");
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
        public void ObterCFOPS(AgendamentoViewModel viewModel)
        {
            viewModel.CFOPS = _agendamentoRepositorio
                .ObterCFOP().ToList();
        }

        [HttpGet]
        public void ObterUploads(AgendamentoViewModel viewModel)
        {
            viewModel.AgendamentoUploadViewModel.Uploads = _uploadRepositorio
                .ObterUploads(viewModel.Id);
        }

        [HttpGet]
        public ActionResult ObterDetalhesReserva(string reserva)
        {
            var detalhes = _reservaRepositorio.ObterDetalhesReserva(reserva);

            if (detalhes == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Reserva não encontrada");

            return Json(new
            {
                BookingCsId = detalhes.BookingCsId,
                Navio = $"{detalhes.Navio} / {detalhes.Viagem}",
                ViagemId = detalhes.ViagemId,
                Viagem = detalhes.Viagem,
                Abertura = detalhes.Abertura.DataHoraFormatada(),
                Fechamento = detalhes.Fechamento.DataHoraFormatada(),
                Exportador = detalhes.Exportador
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult ObterItensReserva(string reserva, int viagemId)
        {
            var itens = _reservaRepositorio.ObterItensReserva(reserva, viagemId).Select(c =>
                new
                {
                    c.BookingCsItemId,
                    c.Descricao,
                    c.ClassificacaoId
                });

            return Json(itens, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult ObterSaldoItensReserva(string reserva, int viagemId)
        {
            var saldoTotal = _reservaRepositorio.ObterSaldoTotalReserva(reserva, viagemId);

            return Json(saldoTotal, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult ObterItemReservaPorId(int bookingCsItemid)
        {
            var itemBusca = _reservaRepositorio.ObterItemReservaPorId(bookingCsItemid);

            if (itemBusca == null)
                return RetornarErro("Item não encontrado");

            return Json(new
            {
                itemBusca.BookingCsItemId,
                itemBusca.Descricao,
                itemBusca.Reserva,
                itemBusca.ViagemId,
                itemBusca.QtdeReserva,
                itemBusca.PackingList,
                itemBusca.ImagemCarga,
                itemBusca.DesenhoTecnico
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult ObterDanfesPorItemId(int bookingCsItemid)
        {
            var danfesAdicionadas = GerenciadorDeEstado<NotaFiscal>.RetornarTodos()
                .Where(c => c.BookingCsItemId == bookingCsItemid);

            return PartialView("_DanfesConsulta", danfesAdicionadas);
        }

        [HttpGet]
        public ActionResult ObterDanfesReadOnlyPorItemId(int agendamentoId, int bookingCsItemid)
        {
            var danfes = _agendamentoRepositorio.ObterNotasFiscaisAgendamento(agendamentoId);

            danfes = danfes.Where(c => c.BookingCsItemId == bookingCsItemid).ToList();

            return PartialView("_DanfesConsultaReadOnly", danfes);
        }

        [HttpGet]
        public PartialViewResult ObterPeriodos()
        {
            var reservas = GerenciadorDeEstado<ReservaItem>
                .RetornarTodos().Select(c => c.Reserva).ToArray();

            var itens = GerenciadorDeEstado<ReservaItem>
                .RetornarTodos().Select(c => c.BookingCsItemId).ToArray();

            var cargaProjeto = false;

            foreach (var item in itens)
            {
                if (_agendamentoRepositorio.ItemCargaProjeto(item))
                {
                    cargaProjeto = true;

                    break;
                }
            }

            var reservasPeriodos = _agendamentoRepositorio.ObterPeriodosReservasCargaSolta(reservas).ToList();

            var viewModel = new AgendamentoViewModel();

            viewModel.CargaProjeto = cargaProjeto;
            viewModel.Reservas = reservasPeriodos;

            ObterPeriodos(viewModel);

            return PartialView("_Periodos", viewModel.Periodos);
        }

        public ActionResult Protocolo(int id)
        {
            var agendamento = _agendamentoRepositorio.ObterDadosProtocolo(id);

            if (agendamento == null)
                return RedirectToAction(nameof(Index));

            if (agendamento.TransportadoraId != User.ObterTransportadoraId())
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
                Navio = agendamento.Navio,
                CTE = agendamento.CTE,
                Empresa = empresaBusca,
                DataCadastro = agendamento.DataCriacao
            };

            var itens = _agendamentoRepositorio
                .ObterItensAgendamento(agendamento.Id)
                .Where(c => c.Qtde > 0)
                .ToList();

            foreach (var item in itens)
            {
                var notas = _agendamentoRepositorio
                    .ObterNotasFiscaisPorItemAgendamento(agendamento.Id, item.Id).ToList();

                item.NotasFiscais.AddRange(notas);
            }

            viewModel.ItensReserva = itens;

            _agendamentoRepositorio.AtualizarProtocoloImpresso(agendamento.Id);

            return new PdfActionResult(viewModel);
        }

        [HttpPost]
        public ActionResult Excluir(int id)
        {
            try
            {
                var agendamento = _agendamentoRepositorio.ObterAgendamentoPorId(id);

                if (agendamento == null)
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Agendamento não encontrado");

                if (agendamento.TransportadoraId != User.ObterTransportadoraId())
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Agendamento não pertence a transportadora");

                if (agendamento.DataEntrada != null)
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Exclusão não permitida. A Carga já entrou no Terminal");

                _agendamentoRepositorio.Excluir(agendamento.Id);
            }
            catch
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            return new HttpStatusCodeResult(HttpStatusCode.NoContent);
        }

        [HttpGet]
        public ActionResult Concluido(int id)
        {
            var agendamento = _agendamentoRepositorio.ObterDetalhesAgendamento(id);

            if (agendamento == null)
                return RedirectToAction(nameof(Index));

            var reservas = _agendamentoRepositorio
                .ObterReservasAgendamento(agendamento.Id).Select(c => c.Descricao);

            return View(new AgendamentoViewModel
            {
                Id = agendamento.Id,
                Protocolo = string.Concat(agendamento.Protocolo.PadLeft(6, '0'), "/", agendamento.AnoProtocolo),
                MotoristaDescricao = agendamento.MotoristaDescricao,
                VeiculoDescricao = agendamento.VeiculoDescricao,
                PeriodoDescricao = agendamento.PeriodoDescricao,
                TransportadoraDescricao = agendamento.TransportadoraDescricao,
                Navio = agendamento.Navio,
                Viagem = agendamento.Viagem,
                Abertura = agendamento.Abertura,
                Fechamento = agendamento.Fechamento,
                Reserva = string.Join(",", reservas),
                DataCadastro = agendamento.DataCriacao
            });
        }

        [HttpGet]
        public ActionResult ObterDetalhesItem(int bookingCsItemId, string reserva)
        {
            var detalhesItem = _reservaRepositorio.ObterDetalhesItem(reserva, bookingCsItemId);

            if (detalhesItem != null)
            {
                return Json(
                    new
                    {
                        Veiculo = detalhesItem.Veiculo,
                        PackingList = detalhesItem.PackingList,
                        ImagemCarga = detalhesItem.ImagemCarga,
                        DesenhoTecnico = detalhesItem.DesenhoTecnico,
                        ClassificacaoId = detalhesItem.ClassificacaoId,
                        TipoCarga = detalhesItem.TipoCarga
                    }, JsonRequestBehavior.AllowGet);
            }

            return null;
        }

        [HttpPost]
        public ActionResult CadastrarDocumento([Bind(Include = "PosicionamentoId, DocumentoUploadId, BookingCsItemId")] AgendamentoUploadViewModel viewModel)
        {
            var arquivoUpload = Request.Form.Keys.Count;

            if (arquivoUpload == 0)
                return RetornarErro("Selecione o Documento");

            var tipoDocumento = (TipoDocumentoUpload)Enum.Parse(typeof(TipoDocumentoUpload), Request.Form[0].ToString(), true);

            if (!Enum.IsDefined(typeof(TipoDocumentoUpload), tipoDocumento))
                return RetornarErro("Selecione o Documento");

            HttpPostedFileBase upload = Request.Files[0];

            if (upload == null)
                return RetornarErro("Selecione o Arquivo");

            if (viewModel.BookingCsItemId == 0)
                return RetornarErro("Selecione um Item na lista");

            if (upload != null && upload.ContentLength > 0)
            {
                var arquivo = new FileInfo(upload.FileName);

                if (upload.ContentLength > 3145728)
                    return RetornarErro("O tamanho do arquivo não deve ultrapassar 3MB");

                if (!ArquivosPermitidos.Contains(arquivo.Extension.ToLower()))
                    return RetornarErro("Extensão de arquivo não permitida");

                try
                {
                    using (var binario = new BinaryReader(upload.InputStream))
                    {
                        byte[] dados = binario.ReadBytes(upload.ContentLength);

                        GerenciadorDeEstado<Upload>.Armazenar(new Upload
                        {
                            Arquivo = Path.GetFileName(upload.FileName),
                            Base64 = Convert.ToBase64String(dados),
                            DataEnvio = DateTime.Now,
                            Sharepoint = false,
                            TipoDocumentoId = tipoDocumento.ToValue(),
                            TipoDocumento = tipoDocumento.ToName(),
                            Extensao = Path.GetExtension(upload.FileName),
                            BookingCsItemId = viewModel.BookingCsItemId
                        });
                    }
                }
                catch (Exception ex)
                {
                    return RetornarErro(ex.Message);
                }
            }

            var documentos = GerenciadorDeEstado<Upload>.RetornarTodos()
                .Where(c => c.BookingCsItemId == viewModel.BookingCsItemId).ToList();

            return PartialView("_VincularDocumentosConsulta", documentos);
        }

        [HttpGet]
        public ActionResult ObterUploadsPorItemId(int bookingCsItemid)
        {
            var uploadsAdicionados = GerenciadorDeEstado<Upload>.RetornarTodos()
                .Where(c => c.BookingCsItemId == bookingCsItemid);

            return PartialView("_VincularDocumentosConsulta", uploadsAdicionados);
        }

        [HttpPost]
        public ActionResult ExcluirUpload(int id)
        {
            GerenciadorDeEstado<Upload>.Remover(id);

            var uploads = GerenciadorDeEstado<Upload>.RetornarTodos();

            return PartialView("_VincularDocumentosConsulta", uploads);
        }

        public List<string> ArquivosPermitidos => new List<string>
        {
            ".pdf",".xls",".xlsx",".doc",".jpg",".jpeg",".png",".gif",".msg",".tif",".txt"
        };

        [HttpPost]
        public ActionResult CadastrarItemReserva([Bind(Include = "BookingCsItemId, Qtde, Chassis, Reserva")] ReservaItem item)
        {
            var itemBusca = _reservaRepositorio.ObterItemReservaPorId(item.BookingCsItemId);

            if (itemBusca != null)
            {
                var itensAdicionados = GerenciadorDeEstado<ReservaItem>.RetornarTodos();

                if (itensAdicionados.Any(c => c.BookingCsItemId == item.BookingCsItemId))
                    return RetornarErro($"O Item {itemBusca.Embalagem} {itemBusca.Marca} já foi adicionado");

                var exigeChassi = _reservaRepositorio.CargaExigeChassi(itemBusca.BookingCsItemId);

                if ((itemBusca.Veiculo || itemBusca.ClassificacaoId == ClassificacaoCarga.CargaProjeto) && string.IsNullOrEmpty(item.Chassis))
                {
                    if (exigeChassi)
                    {
                        return RetornarErro($"O Chassi do veiculo é obrigatório");
                    }
                }

                if (item.Qtde > itemBusca.QtdeReserva)
                    return RetornarErro($"A Quantidade informada não pode ultrapassar a quantidade total do item");

                if (item.Qtde == 0)
                    return RetornarErro($"A Quantidade informada deverá ser maior que zero");

                var qtdeJaAdicionada = itensAdicionados.Where(c => c.BookingCsItemId == itemBusca.BookingCsItemId).Sum(c => c.Qtde);

                if ((item.Qtde + qtdeJaAdicionada) > itemBusca.Saldo)
                    return RetornarErro($"Saldo insuficiente para agendamento");

                if (!string.IsNullOrEmpty(item.Chassis))
                {
                    item.Chassis = item.Chassis.RemoverQuebrasDeLinha();

                    if (item.Chassis.Substring(item.Chassis.Length - 1) == ",")
                    {
                        item.Chassis = item.Chassis.Remove(item.Chassis.Length - 1);
                    }

                    var chassis = item.Chassis.Split(',');

                    if (item.Qtde != chassis.Length)
                        return RetornarErro($"Os chassis informados divergem da quantidade que está sendo agendada");

                    if (chassis != null)
                    {
                        foreach (var chassi in chassis)
                        {
                            var agendamento = _agendamentoRepositorio.ChassiEmOutroAgendamento(chassi);

                            if (agendamento != null)
                                return RetornarErro($"Chassi {agendamento.Chassi} já foi utilizado no agendamento {agendamento.Protocolo}");
                        }
                    }
                }

                var exigeNF = _reservaRepositorio.CargaExigeNF(itemBusca.BookingCsItemId);

                GerenciadorDeEstado<ReservaItem>.Armazenar(
                    new ReservaItem
                    {
                        Reserva = item.Reserva,
                        BookingCsItemId = item.BookingCsItemId,
                        QtdeReserva = itemBusca.QtdeReserva,
                        Qtde = item.Qtde,
                        Embalagem = itemBusca.Embalagem,
                        Marca = itemBusca.Marca,
                        Chassis = item.Chassis,
                        PesoUnitario = itemBusca.PesoUnitario,
                        Classificacao = itemBusca.Classificacao,
                        ClassificacaoId = itemBusca.ClassificacaoId,
                        PackingList = itemBusca.PackingList,
                        DesenhoTecnico = itemBusca.DesenhoTecnico,
                        ImagemCarga = itemBusca.ImagemCarga,
                        ExigeNF = exigeNF,
                        ExigeChassi = exigeChassi
                    });
            }

            var itens = GerenciadorDeEstado<ReservaItem>.RetornarTodos();

            return PartialView("_ItensReservaConsulta", itens);
        }

        [HttpPost]
        public ActionResult ExcluirItemReserva(int id)
        {
            List<ReservaItem> itens = GerenciadorDeEstado<ReservaItem>.RetornarTodos();

            var itemBusca = itens
                .Where(c => c.Id == id).FirstOrDefault();

            GerenciadorDeEstado<ReservaItem>.Remover(itemBusca.Id);

            var danfesDoItem = GerenciadorDeEstado<NotaFiscal>.RetornarTodos()
                .Where(c => c.BookingCsItemId == itemBusca.BookingCsItemId).ToList();

            foreach (var danfe in danfesDoItem)
                GerenciadorDeEstado<NotaFiscal>.Remover(danfe.Id);

            return PartialView("_ItensReservaConsulta", GerenciadorDeEstado<ReservaItem>.RetornarTodos());
        }

        [HttpPost]
        public ActionResult CadastrarDanfes([Bind(Include = "Danfe, Reserva, CFOP, BookingCsItemId")] NotaFiscal nf)
        {
            var danfeBusca = _agendamentoRepositorio.ObterNotasFiscaisPorDanfe(nf.Danfe);

            if (danfeBusca.Any())
                return RetornarErro("Danfe já cadastrada em outro agendamento");

            var danfesAdicionadas = GerenciadorDeEstado<NotaFiscal>.RetornarTodos();

            if (danfesAdicionadas.Any(c => c.Danfe == nf.Danfe && c.BookingCsItemId == nf.BookingCsItemId))
                return RetornarErro($"A Danfe {nf.Danfe} já foi adicionada no item {nf.BookingCsItemId}");

            GerenciadorDeEstado<NotaFiscal>.Armazenar(new NotaFiscal
            {
                Reserva = nf.Reserva,
                Danfe = nf.Danfe,
                CFOP = nf.CFOP,
                BookingCsItemId = nf.BookingCsItemId
            });

            var danfes = GerenciadorDeEstado<NotaFiscal>.RetornarTodos()
                .Where(c => c.BookingCsItemId == nf.BookingCsItemId).ToList();

            return PartialView("_DanfesConsulta", danfes);
        }

        [HttpPost]
        public ActionResult ExcluirDanfe(int id, int bookingCsItemId)
        {
            GerenciadorDeEstado<NotaFiscal>.Remover(id);

            var danfes = GerenciadorDeEstado<NotaFiscal>.RetornarTodos()
                .Where(c => c.BookingCsItemId == bookingCsItemId).ToList();

            return PartialView("_DanfesConsulta", danfes);
        }
    }
}