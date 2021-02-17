using Ecoporto.AgendamentoConteiner.Config;
using Ecoporto.AgendamentoConteiner.Dados.Interfaces;
using Ecoporto.AgendamentoConteiner.Enums;
using Ecoporto.AgendamentoConteiner.Extensions;
using Ecoporto.AgendamentoConteiner.Filtros;
using Ecoporto.AgendamentoConteiner.Helpers;
using Ecoporto.AgendamentoConteiner.Models;
using Ecoporto.AgendamentoConteiner.Models.DTO;
using Ecoporto.AgendamentoConteiner.Models.ViewModels;
using MvcRazorToPdf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNetCore.Http;
using System.Xml;
using System.Threading.Tasks;

namespace Ecoporto.AgendamentoConteiner.Controllers
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
        [HttpPost]
        public async Task<JsonResult> UploadHomeReport(string id)
        {
            try
            {
                foreach (string file in Request.Files)
                {
                    var fileContent = Request.Files[file];
                    if (fileContent != null && fileContent.ContentLength > 0)
                    {
                        // get a stream
                        var stream = fileContent.InputStream;
                        // and optionally write the file to disk
                        var fileName = Path.GetFileName(file);
                        var path = Path.Combine(Server.MapPath("~/App_Data/Images"), fileName);
                        //using (var fileStream = File.Create(path))
                        //{
                        //    stream.CopyTo(fileStream);
                        //}
                    }
                }
            }
            catch (Exception)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json("Upload failed");
            }

            return Json("File uploaded successfully");
        }
        [HttpPost]
        public ActionResult UploadDanfe(IFormFile xmlFile)
        {
            try
            {
                var path = Path.Combine(Directory.GetCurrentDirectory(), "Content/xmls", xmlFile.FileName);
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    xmlFile.CopyTo(stream);
                }
                ViewBag.xml = ReaderXml(path);
                ViewBag.result = "Leitura OK";
                ReaderXml(path);
            }
            catch (Exception ex)
            {
                ViewBag.result = ex.Message;
            }
            return View();
        }
        private NotaFiscal ReaderXml(string path)
        {
            NotaFiscal nf = new NotaFiscal();
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(path);
            XmlNodeList list = xmlDoc.GetElementsByTagName("ide");
            foreach (XmlNode item in list)
            {
                nf.SerieNF = item["serie"].InnerText;
                nf.NumeroNF = item["cNF"].InnerText;
            }
            string xmlClob = ConvertXmlToString(xmlDoc);
            //List<XmlDocument> xmlDocuments = xDocument.Descendants("xml").Select(
            //    p=> new XmlDocument()
            //    {
            //        ID = p.Element("Id").Value,
            //        Serie = int.Parse(p.Element("ide").Value),
            //        Versao = int.Parse(p.Element("versao").Value),
            //        Numero = int.Parse(p.Element("cNF").Value)
            //    }).ToList();
            return nf;
        }
        private string ConvertXmlToString(XmlDocument xmlFile)
        {
            return xmlFile.OuterXml;
        }

        public bool ReservaEncerrada(DateTime data, DateTime fim)
        {
            //return data >= inicio && data <= fim;
            return data > fim;
        }

        [HttpGet]
        public ActionResult ObterDetalhesReserva(string reserva)
        {
            var detalhes = _reservaRepositorio.ObterDetalhesReserva(reserva.Trim());

            if (detalhes == null)
                return RetornarErro($"Reserva {reserva} não encontrada");

            if (!Enum.IsDefined(typeof(TipoAgendamentoConteiner), detalhes.EF))
                return RetornarErro($"Reserva {reserva} inválida");

            if (DateTimeHelpers.IsNullDate(detalhes.Abertura))
                return RetornarErro("Viagem sem Abertura de Praça");

            if (!detalhes.LateArrival)
            {
                if (ReservaEncerrada(DateTime.Now, detalhes.Fechamento))
                    return RetornarErro("Já consta Fechamento de Praça para a reserva!");
            }

            //var imos = _reservaRepositorio.ObterIMOs(reserva);
            //var onus = _reservaRepositorio.ObterONUs(reserva);

            return Json(new
            {
                BookingId = detalhes.BookingId,
                Navio = detalhes.Navio,
                ViagemId = detalhes.ViagemId,
                Viagem = detalhes.Viagem,
                Exportador = detalhes.Exportador,
                EF = detalhes.EF,
                NavioViagem = $"{detalhes.Navio}/{detalhes.Viagem}",
                Abertura = detalhes.Abertura.DataHoraFormatada(),
                Fechamento = detalhes.Fechamento.DataHoraFormatada(),
                IMO1 = detalhes.IMO1,
                IMO2 = detalhes.IMO2,
                IMO3 = detalhes.IMO3,
                IMO4 = detalhes.IMO4,
                ONU1 = detalhes.ONU1,
                ONU2 = detalhes.ONU2,
                ONU3 = detalhes.ONU3,
                ONU4 = detalhes.ONU4
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        private void ObterPeriodos(string reserva, AgendamentoViewModel viewModel)
        {
            var reservaBusca = _reservaRepositorio.ObterDetalhesReserva(reserva);

            if (reservaBusca != null)
            {
                var maiorData = reservaBusca.Fechamento;

                var limiteHoraLateArrival = reservaBusca.DeltaHoras;

                if (maiorData == DateTime.MinValue)
                    maiorData = DateTime.Now.AddDays(15);

                maiorData = maiorData.AddSeconds(59);

                var periodos = _agendamentoRepositorio
                    .ObterPeriodos(reservaBusca.Abertura, maiorData, reservaBusca.EF)
                    .Where(c => c.PeriodoInicial > DateTime.Now.AddHours(limiteHoraLateArrival))
                    .ToList();

                if (viewModel.Id > 0)
                {
                    var periodoAgendamento = _agendamentoRepositorio.ObterPeriodoPorId(viewModel.PeriodoId);

                    if (periodoAgendamento != null)
                    {
                        if (!periodos.Any(c => c.Id == viewModel.PeriodoId))
                            periodos.Add(periodoAgendamento);

                        foreach (var periodo in periodos)
                            periodo.PeriodoAgendado = viewModel.PeriodoId == periodo.Id;
                    }
                }

                viewModel.Periodos = periodos.ToList().OrderBy(c => c.PeriodoInicial).ToList();
            }
        }

        [HttpGet]
        public ActionResult Cadastrar()
        {
            var viewModel = new AgendamentoViewModel
            {
                Id = 0,
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
            ObterTiposConteiner(viewModel);

            return View(viewModel);
        }

        public void ValidarBDCCMotorista(Motorista motorista)
        {
            if (AppConfig.ValidarBDCC())
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
        }

        public void ValidarBDCCVeiculo(Veiculo veiculo)
        {
            if (AppConfig.ValidarBDCC())
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
        }

        private void UploadDocumentos(int agendamentoId)
        {
            //foreach (var documento in GerenciadorDeEstado<Upload>.RetornarTodos())
            //{
            //    if (!_uploadRepositorio.DocumentoJaExistente(agendamentoId, documento.TipoDocumentoId))
            //    {
            //        WsSharepoint.ImagemAverbacao img = new WsSharepoint.ImagemAverbacao()
            //        {
            //            NomeImagem = $"{documento.Sigla}_{documento.TipoDocumento.ToName()}{documento.Extensao}",
            //            IdTipoDocUpload = documento.TipoDocumentoId,
            //            Lote = documento.BookingId,
            //            TipoDocumento = documento.TipoDocumento.ToValue().ToString(),
            //            _byteArrayImagem = Convert.FromBase64String(documento.Base64),
            //            CaminhoImagem = " ",
            //            DataInclusao = DateTime.Now,
            //            AutonumAgendamento = agendamentoId,
            //            TipoDocAgendamento = "CNOP"
            //        };

            //        WsSharepoint.WsIccSharepoint ws = new WsSharepoint.WsIccSharepoint();

            //        try
            //        {
            //            var imgId = ws.EnviarImagemDocAverbacaoRetID(img);
            //        }
            //        catch (Exception ex)
            //        {
            //            throw new Exception($"Falha na comunicação com o Web Service Sharepoint - {ex.Message}");
            //        }
            //    }
            //}
        }

        public ActionResult ValidarConteineresNotasFiscais(int agendamentoId)
        {
            var conteineres = _agendamentoRepositorio
               .ObterConteineresAgendamento(agendamentoId).ToList();

            var notasFiscais = _agendamentoRepositorio
                .ObterNotasFiscaisAgendamento(agendamentoId).ToList();

            foreach (var conteiner in conteineres)
            {
                if (conteiner.Reserva.EF == TipoAgendamentoConteiner.Cheio)
                {
                    var notas = notasFiscais
                        .Where(c => c.SiglaConteiner == conteiner.Sigla).ToList();

                    if (notas.Count == 0)
                    {
                        if (conteiner.ExigeNF)
                        {
                            if (conteiner.Reserva.Bagagem == false)
                                ModelState.AddModelError(string.Empty, $"Nenhuma DANFE informada para o Contêiner {conteiner.Sigla} / Reserva {conteiner.Reserva.Descricao}");
                        }
                    }

                    // Esta é uma funcionalidade que vai existir futuramente por decisão da área eles irão amadurecer a ideia para implementar isso no site para o cliente.

                    //var uploads = GerenciadorDeEstado<Upload>.RetornarTodos()
                    //    .Where(c => c.BookingId == conteiner.Reserva.BookingId);

                    //if (conteiner.Bagagem)
                    //{
                    //    if (!uploads.Any(c => c.TipoDocumento == TipoDocumentoUpload.AUTORIZACAO_BAGAGEM))
                    //        ModelState.AddModelError(string.Empty, $"O Contêiner {conteiner.Sigla} exige upload de {TipoDocumentoUpload.AUTORIZACAO_BAGAGEM.ToName()}");

                    //    if (!uploads.Any(c => c.TipoDocumento == TipoDocumentoUpload.PACKING_LIST))
                    //        ModelState.AddModelError(string.Empty, $"O Contêiner {conteiner.Sigla} exige upload de {TipoDocumentoUpload.PACKING_LIST.ToName()}");
                    //}
                }
            }

            if (!ModelState.IsValid)
                return RetornarErros();

            return new HttpStatusCodeResult(HttpStatusCode.NoContent);
        }

        [HttpPost]
        public ActionResult Cadastrar([Bind(Include = "MotoristaId, VeiculoId, CTE, Reserva, ViagemId, BookingCsId, PeriodoId, Cegonha, EmailRegistro, TipoOperacao")] AgendamentoViewModel viewModel)
        {
            try
            {
                var agendamento = new Agendamento(
                    viewModel.MotoristaId,
                    viewModel.VeiculoId,
                    viewModel.CTE,
                    viewModel.Cegonha,
                    User.ObterTransportadoraId(),
                    viewModel.TipoOperacao,
                    User.ObterId());

                if (!Validar(agendamento))
                    return RetornarErros();

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

                agendamento.Id = _agendamentoRepositorio.Cadastrar(agendamento);

                return JavaScript($"window.location = '{nameof(Atualizar)}/{ agendamento.Id }#carregamento'");
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
                Navio = agendamento.Navio,
                Viagem = agendamento.Viagem,
                Abertura = agendamento.Abertura,
                Fechamento = agendamento.Fechamento,
                Cegonha = agendamento.Cegonha,
                EmailRegistro = agendamento.EmailRegistro,
                DetalhesMotorista = detalhesMotorista,
                DetalhesVeiculo = detalhesVeiculo,
                Protocolo = $"{agendamento.Protocolo}/{agendamento.AnoProtocolo}",
                PossuiEntrada = agendamento.DataEntrada != null
            };

            viewModel.ItensReserva = _agendamentoRepositorio
                .ObterItensAgendamento(agendamento.Id).ToList();

            var conteineres = _agendamentoRepositorio
                .ObterConteineresAgendamento(agendamento.Id).ToList();

            var danfes = _agendamentoRepositorio
                .ObterNotasFiscaisAgendamento(agendamento.Id).ToList();

            foreach (var conteiner in conteineres)
            {
                foreach (var danfe in danfes)
                {
                    if (danfe.SiglaConteiner == conteiner.Sigla)
                    {
                        conteiner.NotasFiscais.Add(danfe);
                    }
                }
            }

            viewModel.Conteineres = conteineres;

            //var uploads = _uploadRepositorio
            //    .ObterUploads(agendamento.Id).ToList();

            ObterCFOPS(viewModel);
            ObterTiposConteiner(viewModel);

            var reserva = viewModel.ItensReserva.Select(c => c.Descricao).FirstOrDefault();

            ObterPeriodos(reserva, viewModel);

            return viewModel;
        }

        [HttpGet]
        [ValidaTransportadoraAgendamentoFilter]
        public ActionResult Atualizar(int? id)
        {
            if (id == null)
                return RedirectToAction(nameof(Index));

            var agendamento = _agendamentoRepositorio.ObterDetalhesAgendamento(id.Value);

            if (agendamento == null)
                return RegistroNaoEncontrado();

            var dados = CarregarDadosAgendamento(agendamento);

            return View(dados);
        }

        [HttpPost]
        public ActionResult Atualizar([Bind(Include = "Id, MotoristaId, VeiculoId, CTE, Reserva, PeriodoId, ItensReserva, Cegonha, Desembaracada, EmailRegistro, TipoOperacao, DueDesembaracada")] AgendamentoViewModel viewModel, int? id)
        {
            if (id == null)
                return RedirectToAction(nameof(Index));

            var agendamento = _agendamentoRepositorio.ObterAgendamentoPorId(id.Value);

            if (agendamento == null)
                return RegistroNaoEncontrado();

            var agendamentoObj = new Agendamento(
                    agendamento.Id,
                    viewModel.MotoristaId,
                    viewModel.VeiculoId,
                    viewModel.CTE,
                    viewModel.Cegonha,
                    User.ObterTransportadoraId(),
                    viewModel.PeriodoId,
                    viewModel.EmailRegistro,
                    viewModel.TipoOperacao,
                    viewModel.DueDesembaracada,
                    User.ObterId());

            agendamento.Alterar(agendamentoObj);

            var conteineres = _agendamentoRepositorio
                .ObterConteineresAgendamento(agendamento.Id).ToList();

            var danfes = _agendamentoRepositorio
                .ObterNotasFiscaisAgendamento(agendamento.Id).ToList();

            agendamentoObj.AdicionarConteineres(conteineres);
            agendamentoObj.AdicionarNotasFiscais(danfes);

            if (!Validar(agendamentoObj))
                return RetornarErros();

            var periodoBusca = _agendamentoRepositorio.ObterPeriodoPorId(viewModel.PeriodoId);

            if (periodoBusca == null)
                throw new Exception("Período não encontrado");

            var existeAgendamento = _agendamentoRepositorio
                .ObterAgendamentosPorPeriodoEVeiculo(User.ObterTransportadoraId(), viewModel.PeriodoId, viewModel.VeiculoId)
                .Where(c => c.Id != agendamento.Id)
                .Any();

            //if (existeAgendamento)
              //  throw new Exception("Já existe um agendamento para o mesmo veículo e período");

            var motoristaBusca = _motoristaRepositorio.ObterMotoristaPorId(viewModel.MotoristaId);

            if (motoristaBusca == null)
                throw new Exception("Motorista não encontrado ou excluído");

            ValidarBDCCMotorista(motoristaBusca);

            var veiculoBusca = _veiculoRepositorio.ObterVeiculoPorId(viewModel.VeiculoId);

            if (veiculoBusca == null)
                throw new Exception("Veículo não encontrado ou excluído");

            ValidarBDCCVeiculo(veiculoBusca);

            _agendamentoRepositorio.Atualizar(agendamentoObj);

            UploadDocumentos(agendamento.Id);

            return JavaScript($"window.location = '{Url.Action(nameof(Concluido), new { id })}'");

        }

        [HttpGet]
        [ValidaTransportadoraAgendamentoFilter]
        public ActionResult Visualizar(int? id)
        {
            if (id == null)
                return RedirectToAction(nameof(Index));

            var agendamento = _agendamentoRepositorio.ObterDetalhesAgendamento(id.Value);

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
        public void ObterCFOPS(AgendamentoViewModel viewModel)
        {
            viewModel.CFOPS = _agendamentoRepositorio
                .ObterCFOP().ToList();
        }

        [HttpGet]
        public void ObterTiposConteiner(AgendamentoViewModel viewModel)
        {
            viewModel.TiposConteiner = _agendamentoRepositorio
                .ObterTiposConteiner().ToList();
        }

        [HttpGet]
        public void ObterUploads(AgendamentoViewModel viewModel)
        {
            viewModel.AgendamentoUploadViewModel.Uploads = _uploadRepositorio
                .ObterUploads(viewModel.Id);
        }

        [HttpGet]
        public JsonResult ObterDocsTransito()
        {
            var tiposDocTransito = _agendamentoRepositorio.ObterTiposDocumentoTransito();

            return Json(tiposDocTransito, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public int ObterSaldoItemBooking(int bookingId, int agendamentoId)
        {
            var item = _reservaRepositorio.ObterItemReservaPorBokingId(bookingId);

            if (item != null)
            {
                return item.Saldo;
            }

            return 0;
        }

        [HttpGet]
        public int ObterSaldoDaReserva(string reserva, int viagemId, int agendamentoId)
        {
            var saldo = _reservaRepositorio
                .ObterItensReserva(reserva, viagemId)
                .Sum(c => c.Saldo);

            if (saldo <= 0)
            {
                var agendamentoBusca = _agendamentoRepositorio.ObterAgendamentoPorId(agendamentoId);

                if (agendamentoBusca != null)
                {
                    if (string.IsNullOrEmpty(agendamentoBusca.Protocolo))
                        _agendamentoRepositorio.Excluir(agendamentoId);
                }
            }

            return saldo;
        }

        [HttpGet]
        public PartialViewResult ObterItensReserva(string reserva, int viagemId)
        {
            var itens = _reservaRepositorio.ObterItensReserva(reserva, viagemId);

            return PartialView("_ItensReservaConsulta", itens);
        }

        //[HttpGet]
        //public ActionResult ObterConteineresPorItemId(int bookingId, int agendamentoId)
        //{
        //    var conteineresAdicionados = _agendamentoRepositorio
        //       .ObterConteineresAgendamento(agendamentoId)
        //       .Where(c => c.Reserva.BookingId == bookingId);

        //    return PartialView("_ConteineresConsulta", conteineresAdicionados);
        //}

        [HttpGet]
        public ActionResult ObterDanfesReadOnlyPorItemId(int agendamentoId, int BookingId)
        {
            var danfes = _agendamentoRepositorio.ObterNotasFiscaisAgendamento(agendamentoId);

            danfes = danfes.Where(c => c.BookingId == BookingId).ToList();

            return PartialView("_DanfesConsultaReadOnly", danfes);
        }

        [HttpGet]
        public ActionResult ObterDanfesPorConteiner(string sigla, int agendamentoId)
        {
            var danfes = _agendamentoRepositorio
                .ObterNotasFiscaisAgendamento(agendamentoId)
                .Where(c => c.SiglaConteiner == sigla);

            return PartialView("_DanfesConsulta", danfes);
        }

        [HttpGet]
        public JsonResult ObterDadosConteiner(int id)
        {
            var conteiner = _agendamentoRepositorio
                .ObterConteinerAgendamentoPorId(id);

            return Json(new
            {
                Id = conteiner.Id,
                Sigla = conteiner.Sigla,
                BookingId = conteiner.Reserva.BookingId,
                Bagagem = conteiner.Reserva.Bagagem,
                Volumes = conteiner.Volumes,
                Tamanho = conteiner.Reserva.Tamanho,
                TipoBasico = conteiner.Reserva.Tipo,
                Tara = conteiner.Tara,
                Bruto = conteiner.Bruto,
                PesoLiquido = conteiner.PesoLiquido,
                ONU1 = conteiner.ONU1,
                ONU2 = conteiner.ONU2,
                ONU3 = conteiner.ONU3,
                ONU4 = conteiner.ONU4,
                IMO1 = conteiner.IMO1,
                IMO2 = conteiner.IMO2,
                IMO3 = conteiner.IMO3,
                IMO4 = conteiner.IMO4,
                Temp = conteiner.Temp,
                Escala = conteiner.Escala,
                ISO = conteiner.ISO,
                Umidade = conteiner.Umidade,
                Ventilacao = conteiner.Ventilacao,
                Comprimento = conteiner.Comprimento,
                Altura = conteiner.Altura,
                LateralDireita = conteiner.LateralDireita,
                LateralEsquerda = conteiner.LateralEsquerda,
                LacreArmador1 = conteiner.LacreArmador1,
                LacreArmador2 = conteiner.LacreArmador2,
                OutrosLacres1 = conteiner.OutrosLacres1,
                OutrosLacres2 = conteiner.OutrosLacres2,
                OutrosLacres3 = conteiner.OutrosLacres3,
                OutrosLacres4 = conteiner.OutrosLacres4,
                LacreExportador = conteiner.LacreExportador,
                LacreSIF = conteiner.LacreSIF,
                TipoDocTransitoId = conteiner.TipoDocTransitoId,
                NumDocTransitoDUE = conteiner.NumDocTransitoDUE,
                DataDocTransitoDUE = conteiner.DataDocTransitoDUE,
                Observacoes = conteiner.Observacoes,
                ReeferLigado = conteiner.ReeferLigado
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult ObterDadosConteinerReserva(string hash)
        {
            var conteiner = _agendamentoRepositorio.ObterInformacoesConteinerReserva(hash);

            if (conteiner == null)
                return null;

            return Json(new
            {
                Comprimento = conteiner.Comprimento,
                Altura = conteiner.Altura,
                LateralDireita = conteiner.LateralDireita,
                LateralEsquerda = conteiner.LateralEsquerda
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public PartialViewResult ObterPeriodos(string reserva)
        {
            var viewModel = new AgendamentoViewModel();

            ObterPeriodos(reserva, viewModel);

            return PartialView("_Periodos", viewModel.Periodos);
        }

        [ValidaTransportadoraAgendamentoFilter]
        public ActionResult Protocolo(int id)
        {
            var agendamento = _agendamentoRepositorio.ObterDadosProtocolo(id);

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
                Navio = agendamento.Navio,
                CTE = agendamento.CTE,
                Empresa = empresaBusca,
                DataCadastro = agendamento.DataCriacao,
                TipoAgendamentoConteiner = agendamento.TipoAgendamentoConteiner
            };

            var conteineres = _agendamentoRepositorio
                .ObterConteineresAgendamento(agendamento.Id).ToList();

            var notas = _agendamentoRepositorio
                .ObterNotasFiscaisAgendamento(agendamento.Id).ToList();

            foreach (var conteiner in conteineres)
            {
                conteiner.NotasFiscais = notas.Where(c => c.SiglaConteiner == conteiner.Sigla).ToList();
            }

            viewModel.Conteineres = conteineres;
            _agendamentoRepositorio.AtualizarProtocoloImpresso(agendamento.Id);

            return new PdfActionResult(viewModel);
        }

        [HttpPost]
        [ValidaTransportadoraAgendamentoFilter]
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
        [ValidaTransportadoraAgendamentoFilter]
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

        [HttpPost]
        public ActionResult CadastrarDocumento([Bind(Include = "AgendamentoId, DocumentoUploadId, BookingId, UploadSiglaConteiner")] AgendamentoUploadViewModel viewModel)
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

            if (viewModel.BookingId == 0)
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

                        var uploadObj = new Upload
                        {
                            Arquivo = $"{viewModel.UploadSiglaConteiner}_{viewModel.TiposDocumentos.ToName()}{arquivo.Extension}",
                            Base64 = Convert.ToBase64String(dados),
                            DataEnvio = DateTime.Now,
                            Sharepoint = false,
                            Sigla = viewModel.UploadSiglaConteiner,
                            TipoDocumentoId = tipoDocumento.ToValue(),
                            TipoDocumento = tipoDocumento,
                            Extensao = arquivo.Extension,
                            BookingId = viewModel.BookingId
                        };

                        if (viewModel.AgendamentoId.HasValue)
                        {
                            if (viewModel.AgendamentoId.Value > 0)
                            {
                                WsSharepoint.ImagemAverbacao img = new WsSharepoint.ImagemAverbacao()
                                {
                                    NomeImagem = uploadObj.Arquivo,
                                    IdTipoDocUpload = uploadObj.TipoDocumentoId,
                                    Lote = uploadObj.BookingId,
                                    TipoDocumento = uploadObj.TipoDocumento.ToValue().ToString(),
                                    _byteArrayImagem = Convert.FromBase64String(uploadObj.Base64),
                                    CaminhoImagem = " ",
                                    DataInclusao = DateTime.Now,
                                    AutonumAgendamento = viewModel.AgendamentoId.Value,
                                    TipoDocAgendamento = "CNOP"
                                };

                                WsSharepoint.WsIccSharepoint ws = new WsSharepoint.WsIccSharepoint();

                                try
                                {
                                    uploadObj.Id = (int)ws.EnviarImagemDocAverbacaoRetID(img);
                                }
                                catch (Exception ex)
                                {
                                    return RetornarErro($"Falha na comunicação com o Web Service Sharepoint - {ex.Message}");
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    return RetornarErro(ex.Message);
                }
            }

            //var documentos = GerenciadorDeEstado<Upload>.RetornarTodos()
            //    .Where(c => c.Sigla == viewModel.UploadSiglaConteiner).ToList();

            return PartialView("_VincularDocumentosConsulta", null);
        }

        [HttpGet]
        public ActionResult ObterUploadsConteiner(string sigla)
        {
            //var uploadsAdicionados = GerenciadorDeEstado<Upload>.RetornarTodos().ToList();
            //var uploadsAdicionados = null;

            return PartialView("_VincularDocumentosConsulta", null);
        }

        [HttpPost]
        public ActionResult ExcluirUpload(int id, int? agendamentoId = 0)
        {
            //GerenciadorDeEstado<Upload>.Remover(id);

            //var uploads = GerenciadorDeEstado<Upload>.RetornarTodos();

            //if (agendamentoId.Value > 0)
            //{
            //    var uploadBuscaDb = _uploadRepositorio.ObterUploadPorId(id);

            //    if (uploadBuscaDb != null)
            //    {
            //        if (uploadBuscaDb.AgendamentoId == agendamentoId.Value)
            //        {
            //            _uploadRepositorio.ExcluirDocumentosPorId(id);
            //        }
            //    }
            //}

            return PartialView("_VincularDocumentosConsulta", null);
        }

        public List<string> ArquivosPermitidos => new List<string>
        {
            ".pdf",".xls",".xlsx",".doc",".jpg",".jpeg",".png",".gif",".msg",".tif",".txt"
        };
        [HttpPost]
        public ActionResult CadastrarDanfes([Bind(Include = "AgendamentoId, ConteinerId, Danfe, CFOP, SiglaConteiner")] NotaFiscal nf)
        {
            var agendamentoBusca = _agendamentoRepositorio.ObterAgendamentoPorId(nf.AgendamentoId.Value);

            var danfeBusca = _agendamentoRepositorio.ObterNotasFiscaisPorDanfe(nf.Danfe)
                .Where(c => c.AgendamentoId != nf.AgendamentoId.Value);

            if (danfeBusca.Any())
            {
                if (agendamentoBusca.DueDesembaracada == false)
                {
                    return RetornarErro("Danfe já cadastrada em outro agendamento");
                }
                else
                {
                    foreach (var danfe in danfeBusca)
                    {
                        var cntrBuscaDb = _agendamentoRepositorio.ObterConteinerPorId(nf.ConteinerId);

                        if (danfe.BookingId != cntrBuscaDb.Reserva.BookingId)
                        {
                            return RetornarErro("Danfe já cadastrada em outro agendamento");

                            break;
                        }
                    }
                }               
            }

            var danfesAdicionadas = _agendamentoRepositorio
                .ObterNotasFiscaisAgendamento(nf.AgendamentoId.Value).ToList();

            if (danfesAdicionadas.Any(c => c.Danfe == nf.Danfe && c.SiglaConteiner == nf.SiglaConteiner))
                return RetornarErro($"A Danfe {nf.Danfe} já foi adicionada no Contêiner {nf.SiglaConteiner}");

            if (nf.AgendamentoId.HasValue)
            {
                if (nf.AgendamentoId.Value > 0)
                {
                    var cntrBuscaDb = _agendamentoRepositorio.ObterConteinerPorId(nf.ConteinerId);

                    if (cntrBuscaDb != null)
                        nf.Id = _agendamentoRepositorio.CadastrarDanfe(nf);
                }
            }

            var retorno = _agendamentoRepositorio
                .ObterNotasFiscaisAgendamento(nf.AgendamentoId.Value)
                .Where(c => c.SiglaConteiner == nf.SiglaConteiner)
                .ToList();

            return PartialView("_DanfesConsulta", retorno);
        }

        [HttpPost]
        public ActionResult ExcluirDanfe(int id, string sigla, int? agendamentoId = 0)
        {
            var danfes = _agendamentoRepositorio
                .ObterNotasFiscaisAgendamento(agendamentoId.Value)
                .Where(c => c.SiglaConteiner == sigla).ToList();

            if (danfes.Count == 1)
                return RetornarErro($"Exclusão não permitida por ser a única DANFE vinculada ao Contêiner. Cadastre a nova DANFE para ser possível a exclusão");

            if (agendamentoId.Value > 0)
            {
                var danfeBuscaDb = _agendamentoRepositorio.ObterDanfePorId(id);

                if (danfeBuscaDb != null)
                {
                    if (danfeBuscaDb.AgendamentoId == agendamentoId.Value)
                    {
                        _agendamentoRepositorio.ExcluirDanfe(id);
                    }
                }
            }

            danfes = _agendamentoRepositorio
                .ObterNotasFiscaisAgendamento(agendamentoId.Value)
                .Where(c => c.SiglaConteiner == sigla).ToList();

            return PartialView("_DanfesConsulta", danfes);
        }

        [HttpPost]
        public ActionResult ValidarConteiner(Conteiner conteiner)
        {
            var reservaBusca = _reservaRepositorio
                .ObterItemReservaPorId(conteiner.Reserva.BookingId, conteiner.Reserva.Tamanho);

            var umidade = conteiner.Umidade.ToInt() == 0 ? "OFF" : conteiner.Umidade;
            var ventilacao = conteiner.Ventilacao.ToInt() == 0 ? "CLOSED" : conteiner.Ventilacao;

            if (reservaBusca != null)
            {
                var cntrObj = new Conteiner
                {
                    Reserva = reservaBusca,
                    Sigla = conteiner.Sigla,
                    ISO = conteiner.ISO,
                    Volumes = conteiner.Volumes,
                    Tara = conteiner.Tara,
                    PesoLiquido = conteiner.PesoLiquido,
                    Bruto = conteiner.Bruto,
                    ReeferLigado = conteiner.ReeferLigado,
                    Temp = conteiner.Temp,
                    Escala = conteiner.Escala,
                    Umidade = umidade,
                    Ventilacao = ventilacao,
                    ONU1 = conteiner.ONU1,
                    ONU2 = conteiner.ONU2,
                    ONU3 = conteiner.ONU3,
                    ONU4 = conteiner.ONU4,
                    IMO1 = conteiner.IMO1,
                    IMO2 = conteiner.IMO2,
                    IMO3 = conteiner.IMO3,
                    IMO4 = conteiner.IMO4,
                    LacreArmador1 = conteiner.LacreArmador1,
                    DataDocTransitoDUE = conteiner.DataDocTransitoDUE,
                    Comprimento = conteiner.Comprimento,
                    Altura = conteiner.Altura,
                    LateralDireita = conteiner.LateralDireita,
                    LateralEsquerda = conteiner.LateralEsquerda
                };

                if (!Validar(cntrObj))
                    return RetornarErros();

                return Json(true);
            }

            return Json(false);
        }

        [HttpPost]
        public ActionResult CadastrarConteiner([Bind(Include = "Id, AgendamentoId, Reserva, Sigla, Volumes, ReeferLigado, Tara, Bruto, PesoLiquido, ONU1, ONU2, ONU3, ONU4, IMO1, IMO2, IMO3, IMO4, Temp, Escala, Umidade, Ventilacao, Comprimento, Altura, LateralDireita, LateralEsquerda, LacreArmador1, LacreArmador2, OutrosLacres1, OutrosLacres2, OutrosLacres3, OutrosLacres4, LacreExportador, LacreSIF, Observacoes, TipoDocTransitoId, NumDocTransitoDUE, DataDocTransitoDUE, ISO")] Conteiner conteiner)
        {
            var reservaBusca = _reservaRepositorio.ObterItemReservaPorId(conteiner.Reserva.BookingId, conteiner.Reserva.Tamanho);

            if (reservaBusca != null)
            {
                var exigeNF = _agendamentoRepositorio.ConteinerExigeNF(reservaBusca.Descricao, reservaBusca.ViagemId);

                var umidade = conteiner.Umidade.ToInt() == 0 ? "OFF" : conteiner.Umidade;
                var ventilacao = conteiner.Ventilacao.ToInt() == 0 ? "CLOSED" : conteiner.Ventilacao;

                var cntrObj = new Conteiner
                {
                    Id = conteiner.Id,
                    AgendamentoId = conteiner.AgendamentoId,
                    Reserva = reservaBusca,
                    Sigla = conteiner.Sigla,
                    Volumes = conteiner.Volumes,
                    Tara = conteiner.Tara,
                    Bruto = conteiner.Bruto,
                    PesoLiquido = conteiner.PesoLiquido,
                    ONU1 = conteiner.ONU1,
                    ONU2 = conteiner.ONU2,
                    ONU3 = conteiner.ONU3,
                    ONU4 = conteiner.ONU4,
                    IMO1 = conteiner.IMO1,
                    IMO2 = conteiner.IMO2,
                    IMO3 = conteiner.IMO3,
                    IMO4 = conteiner.IMO4,
                    Temp = conteiner.Temp,
                    Escala = conteiner.Escala,
                    ISO = conteiner.ISO,
                    Umidade = umidade,
                    Ventilacao = ventilacao,
                    Comprimento = conteiner.Comprimento,
                    Altura = conteiner.Altura,
                    LateralDireita = conteiner.LateralDireita,
                    LateralEsquerda = conteiner.LateralEsquerda,
                    LacreArmador1 = conteiner.LacreArmador1,
                    LacreArmador2 = conteiner.LacreArmador2,
                    OutrosLacres1 = conteiner.OutrosLacres1,
                    OutrosLacres2 = conteiner.OutrosLacres2,
                    OutrosLacres3 = conteiner.OutrosLacres3,
                    OutrosLacres4 = conteiner.OutrosLacres4,
                    LacreExportador = conteiner.LacreExportador,
                    LacreSIF = conteiner.LacreSIF,
                    Bagagem = reservaBusca.Bagagem,
                    ReeferLigado = conteiner.ReeferLigado,
                    Observacoes = conteiner.Observacoes,
                    TipoDocTransitoId = conteiner.TipoDocTransitoId,
                    NumDocTransitoDUE = conteiner.NumDocTransitoDUE,
                    DataDocTransitoDUE = conteiner.DataDocTransitoDUE,
                    ExigeNF = exigeNF
                };

                if (!Validar(cntrObj))
                    return RetornarErros();

                var tipoCntr = _agendamentoRepositorio.ObterConteinerPorISO(conteiner.ISO);

                if (tipoCntr == null)
                    return RetornarErro($"Nenhum contêiner cadastrado para o ISO {conteiner.ISO}");

                if (tipoCntr.Tamanho != reservaBusca.Tamanho || tipoCntr.Tipo != reservaBusca.Tipo)
                    return RetornarErro($"Código ISO inválido para esta Reserva");

                if (reservaBusca.LateArrival)
                {
                    var conteinerLate = _agendamentoRepositorio.ExisteConteinerLateArrival(conteiner.Sigla, reservaBusca.Descricao, reservaBusca.ViagemId);

                    if (conteinerLate == false)
                    {
                        return RetornarErro($"Conteiner {conteiner.Sigla} não cadastrado na lista de Late Arrival do Terminal");
                    }
                }

                if (cntrObj.Id == 0)
                {
                    var conteineresAdicionados = _agendamentoRepositorio
                        .ObterConteineresAgendamento(cntrObj.AgendamentoId.Value).ToList();

                    if (reservaBusca.Tipo != "FR")
                    {
                        if (conteineresAdicionados.Where(c => c.Reserva.Tipo != "FR").Count() == 2)
                            return RetornarErro($"Número máximo de Contêineres adicionados para este Agendamento");
                    }

                    if (reservaBusca.Tipo == "FR")
                    {
                        if (conteineresAdicionados.Where(c => c.Reserva.Tipo == "FR").Count() == 6)
                            return RetornarErro($"Número máximo de Contêineres adicionados para este Agendamento");
                    }

                    if (conteineresAdicionados.Any(c => c.Reserva.BookingId == conteiner.Reserva.BookingId && c.Sigla == conteiner.Sigla))
                        return RetornarErro($"O Contêiner {conteiner.Sigla} já foi adicionado");

                    if (conteineresAdicionados.Where(c => c.Reserva.EF == TipoAgendamentoConteiner.Vazio).Any())
                        return RetornarErro($"Não é permitido Contêineres Cheios e Vazios no mesmo Agendamento");

                    if (reservaBusca.Tamanho == 20)
                    {
                        var conteineres40Adicionados = conteineresAdicionados.Where(c => c.Reserva.Tamanho == 40).Count();

                        if (conteineres40Adicionados > 0)
                            return RetornarErro($"Já existe um Contêiner de 40 adicionado.");

                        var conteineres20Adicionados = conteineresAdicionados.Where(c => c.Reserva.Tamanho == 20).Count();

                        if (conteineres20Adicionados > 0 && conteineres20Adicionados >= 2)
                            return RetornarErro($"É permitido adicionar apenas 2 Contêineres 20 por agendamento");
                    }

                    if (reservaBusca.Tamanho == 40 && conteineresAdicionados.Any())
                    {
                        return RetornarErro($"É permitido adicionar apenas 1 contêiner de 40 ou 2 de 20. Para continuar remova o contêiner de tamanho diferente já adicionado");
                    }

                    if (reservaBusca.Saldo <= 0)
                    {
                        return RetornarErro($"Saldo insuficiente");
                    }

                    if (conteiner.AgendamentoId.HasValue)
                    {
                        if (conteiner.AgendamentoId.Value > 0)
                        {
                            cntrObj.Id = _agendamentoRepositorio.CadastrarConteiner(cntrObj);
                        }
                    }
                }
                else
                {
                    if (conteiner.AgendamentoId.HasValue)
                    {
                        if (conteiner.AgendamentoId.Value > 0)
                        {
                            var cntrBuscaDb = _agendamentoRepositorio.ObterConteinerPorId(cntrObj.Id);

                            if (cntrBuscaDb != null)
                            {
                                if (cntrBuscaDb.AgendamentoId == conteiner.AgendamentoId.Value)
                                    _agendamentoRepositorio.AtualizarConteiner(cntrObj);
                            }
                        }
                    }
                }
            }

            var retorno = _agendamentoRepositorio
                .ObterConteineresAgendamento(conteiner.AgendamentoId.Value)
                .ToList();

            return PartialView("_ConteineresConsulta", retorno);
        }

        [HttpPost]
        public ActionResult CadastrarConteinerVazio([Bind(Include = "Id, AgendamentoId, Reserva, QuantidadeVazios")] ConteinerVazio conteiner)
        {
            var reservaBusca = _reservaRepositorio.ObterItemReservaPorId(conteiner.Reserva.BookingId, conteiner.Reserva.Tamanho);

            if (reservaBusca != null)
            {
                var cntrObj = new ConteinerVazio
                {
                    Id = conteiner.Id,
                    AgendamentoId = conteiner.AgendamentoId,
                    Reserva = reservaBusca,
                    QuantidadeVazios = conteiner.QuantidadeVazios
                };

                if (!Validar(cntrObj))
                    return RetornarErros();

                if (cntrObj.Id == 0)
                {
                    var conteineresAdicionados = _agendamentoRepositorio
                        .ObterConteineresAgendamento(cntrObj.AgendamentoId.Value).ToList();

                    if (conteineresAdicionados.Where(c => c.Reserva.EF == TipoAgendamentoConteiner.Cheio).Any())
                        return RetornarErro($"Não é permitido Contêineres Cheios e Vazios no mesmo Agendamento");

                    if (conteineresAdicionados.Sum(c => c.QuantidadeVazios) + cntrObj.QuantidadeVazios > 2)
                        return RetornarErro($"Permitido apenas 2 unidades por Agendamento");

                    if (reservaBusca.Saldo < conteiner.QuantidadeVazios)
                        return RetornarErro($"Saldo insuficiente para Agendamento. Escolha uma quantidade menor");

                    if (conteiner.AgendamentoId.HasValue)
                    {
                        if (conteiner.AgendamentoId.Value > 0)
                        {
                            cntrObj.Id = _agendamentoRepositorio.CadastrarConteinerVazio(cntrObj);
                        }
                    }
                }
                else
                {
                    if (conteiner.AgendamentoId.HasValue)
                    {
                        if (conteiner.AgendamentoId.Value > 0)
                        {
                            var cntrBuscaDb = _agendamentoRepositorio.ObterConteinerPorId(cntrObj.Id);

                            if (cntrBuscaDb != null)
                            {
                                if (cntrBuscaDb.AgendamentoId == conteiner.AgendamentoId.Value)
                                    _agendamentoRepositorio.AtualizarConteinerVazio(cntrObj);
                            }
                        }
                    }
                }
            }

            var retorno = _agendamentoRepositorio
                .ObterConteineresAgendamento(conteiner.AgendamentoId.Value)
                .ToList();

            return PartialView("_ConteineresConsulta", retorno);
        }

        [HttpPost]
        public ActionResult ExcluirConteiner(int id, int? agendamentoId = 0)
        {
            List<Conteiner> conteineres = _agendamentoRepositorio
                .ObterConteineresAgendamento(agendamentoId.Value)
                .ToList();

            var conteinerBusca = conteineres
                .Where(c => c.Id == id).FirstOrDefault();

            if (conteinerBusca == null)
                return RetornarErro("Contêiner não encontrado ou já excluído");

            if (agendamentoId.Value > 0)
            {
                if (conteineres.Count == 1)
                    return RetornarErro("Existe apenas um contêiner vinculado ao agendamento exclusão não permitida, favor excluir todo o agendamento");

                var conteinerBuscaDb = _agendamentoRepositorio.ObterConteinerPorId(id);

                if (conteinerBuscaDb != null)
                {
                    if (conteinerBuscaDb.AgendamentoId == agendamentoId.Value)
                    {
                        _agendamentoRepositorio.ExcluirConteiner(id);
                    }
                }
            }

            conteineres = _agendamentoRepositorio
                .ObterConteineresAgendamento(agendamentoId.Value)
                .ToList();

            return PartialView("_ConteineresConsulta", conteineres);
        }

        [HttpPost]
        public ActionResult AtualizarMotorista(int agendamentoId, int motoristaId)
        {
            var agendamentoBusca = _agendamentoRepositorio.ObterAgendamentoPorId(agendamentoId);

            if (agendamentoBusca == null)
                return RetornarErro("Agendamento inexistente");

            var motoristaBusca = _motoristaRepositorio.ObterMotoristaPorId(motoristaId);

            if (motoristaBusca == null)
                return RetornarErro("Motorista inexistente");

            if (agendamentoBusca.TransportadoraId != User.ObterTransportadoraId())
                return RetornarErro("Este agendamento não pertence a esta Transportadora");

            ValidarBDCCMotorista(motoristaBusca);

            _agendamentoRepositorio.AtualizarMotoristaAgendamento(agendamentoId, motoristaId);

            return new HttpStatusCodeResult(HttpStatusCode.NoContent);
        }

        [HttpPost]
        public ActionResult AtualizarVeiculo(int agendamentoId, int veiculoId)
        {
            var agendamentoBusca = _agendamentoRepositorio.ObterAgendamentoPorId(agendamentoId);

            if (agendamentoBusca == null)
                return RetornarErro("Agendamento inexistente");

            var veiculoBusca = _veiculoRepositorio.ObterVeiculoPorId(veiculoId);

            if (veiculoBusca == null)
                return RetornarErro("Veículo inexistente");

            if (agendamentoBusca.TransportadoraId != User.ObterTransportadoraId())
                return RetornarErro("Este agendamento não pertence a esta Transportadora");

            ValidarBDCCVeiculo(veiculoBusca);

            _agendamentoRepositorio.AtualizarVeiculoAgendamento(agendamentoId, veiculoId);

            return new HttpStatusCodeResult(HttpStatusCode.NoContent);
        }

        [HttpPost]
        public ActionResult AtualizarDueDesembaracada(int agendamentoId, bool check)
        {
            var agendamentoBusca = _agendamentoRepositorio.ObterAgendamentoPorId(agendamentoId);

            if (agendamentoBusca == null)
                return RetornarErro("Agendamento inexistente");

            _agendamentoRepositorio.AtualizarFlagDueDesembaracada(agendamentoBusca.Id, check);

            return new HttpStatusCodeResult(HttpStatusCode.NoContent);
        }
    }
}