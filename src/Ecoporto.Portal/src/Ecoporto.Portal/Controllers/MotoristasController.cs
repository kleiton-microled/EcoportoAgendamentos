using Ecoporto.Portal.Dados.Interfaces;
using Ecoporto.Portal.Extensions;
using Ecoporto.Portal.Helpers;
using Ecoporto.Portal.Models;
using Ecoporto.Portal.Models.ViewModels;
using System;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace Ecoporto.Portal.Controllers
{
    [Authorize]
    public class MotoristasController : BaseController
    {
        private readonly IMotoristaRepositorio _motoristaRepositorio;

        public MotoristasController(IMotoristaRepositorio motoristaRepositorio)
        {
            _motoristaRepositorio = motoristaRepositorio;
        }

        [HttpGet]
        public ActionResult Index()
        {
            var resultado = _motoristaRepositorio
                .ObterMotoristas(User.ObterEmpresaId()).ToList();

            return View(resultado);
        }

        [HttpGet]
        public ActionResult Cadastrar()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Cadastrar([Bind(Include = "Nome, CNH, ValidadeCNH, RG, CPF, Celular, Nextel, MOP")] MotoristaViewModel viewModel)
        {
            var motorista = new Motorista(
                User.ObterEmpresaId(),
                viewModel.Nome,
                viewModel.CNH,
                viewModel.ValidadeCNH,
                viewModel.RG,
                viewModel.CPF,
                viewModel.Celular,
                viewModel.Nextel,
                viewModel.MOP);

            try
            {
                var motoristaComCNH = _motoristaRepositorio.ObterMotoristaPorCNH(viewModel.CNH, User.ObterEmpresaId());

                if (motoristaComCNH != null)
                    return RetornarErro($"Já existe um outro motorista cadastrado com a CNH {viewModel.CNH}");

                var motoristaComCPF = _motoristaRepositorio.ObterMotoristaPorCPF(viewModel.CPF, User.ObterEmpresaId());

                if (motoristaComCPF != null)
                    return RetornarErro($"Já existe um outro motorista cadastrado com o CPF {viewModel.CPF}");

                if (!Validar(motorista))
                    return RetornarErros();

                _motoristaRepositorio.Cadastrar(motorista);
            }
            catch (Exception ex)
            {
                return RetornarErro(ex.Message);
            }

            return new HttpStatusCodeResult(HttpStatusCode.NoContent);
        }

        [HttpGet]
        public ActionResult Atualizar(int? id)
        {
            if (id == null)
                return RedirectToAction(nameof(Index));

            var motoristaBusca = _motoristaRepositorio.ObterMotoristaPorId(id.Value);

            if (motoristaBusca == null)
                return RegistroNaoEncontrado();

            if (motoristaBusca.TransportadoraId != User.ObterEmpresaId())
                return RedirectToAction(nameof(Index));

            var viewModel = new MotoristaViewModel
            {
                Id = motoristaBusca.Id,
                Nome = motoristaBusca.Nome,
                CNH = motoristaBusca.CNH,
                ValidadeCNH = motoristaBusca.ValidadeCNH,
                RG = motoristaBusca.RG,
                CPF = motoristaBusca.CPF,
                Celular = motoristaBusca.Celular,
                Nextel = motoristaBusca.Nextel,
                MOP = motoristaBusca.MOP,
                UltimaAlteracao = motoristaBusca.UltimaAlteracao.DataHoraFormatada()
            };

            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Atualizar([Bind(Include = "Id, Nome, CNH, ValidadeCNH, RG, CPF, Celular, Nextel, MOP")] MotoristaViewModel viewModel, int? id)
        {
            if (id == null)
                return RedirectToAction(nameof(Index));

            var motoristaBusca = _motoristaRepositorio.ObterMotoristaPorId(id.Value);

            if (motoristaBusca == null)
                return RegistroNaoEncontrado();

            motoristaBusca.Alterar(new Motorista(
                User.ObterEmpresaId(),
                viewModel.Nome,
                viewModel.CNH,
                viewModel.ValidadeCNH,
                viewModel.RG,
                viewModel.CPF,
                viewModel.Celular,
                viewModel.Nextel,
                viewModel.MOP));

            var motoristaComCNH = _motoristaRepositorio.ObterMotoristaPorCNH(viewModel.CNH, User.ObterEmpresaId());

            if (motoristaComCNH != null)
            {
                if (motoristaComCNH.Id != motoristaBusca.Id)
                {
                    ModelState.AddModelError(string.Empty, $"Já existe um outro motorista cadastrado com a CNH {viewModel.CNH}");

                    return View(viewModel);
                }
            }

            var motoristaComCPF = _motoristaRepositorio.ObterMotoristaPorCPF(viewModel.CPF, User.ObterEmpresaId());

            if (motoristaComCPF != null)
            {
                if (motoristaComCPF.Id != motoristaBusca.Id)
                {
                    ModelState.AddModelError(string.Empty, $"Já existe um outro motorista cadastrado com o CPF {viewModel.CPF}");

                    return View(viewModel);
                }
            }

            if (Validar(motoristaBusca))
            {
                _motoristaRepositorio.Atualizar(motoristaBusca);
                TempData["Sucesso"] = true;
            }

            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Excluir(int id)
        {
            try
            {
                var motoristaBusca = _motoristaRepositorio.ObterMotoristaPorId(id);

                if (motoristaBusca == null)
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Motorista não encontrado");

                if (motoristaBusca.TransportadoraId != User.ObterEmpresaId())
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Motorista não pertence a transportadora");

                _motoristaRepositorio.Excluir(motoristaBusca.Id);
            }
            catch
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            return new HttpStatusCodeResult(HttpStatusCode.NoContent);
        }

        [HttpGet]
        public ActionResult PesquisarCNH(string cnh)
        {
            if (string.IsNullOrWhiteSpace(cnh))
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "CNH não informada");

            var motoristaChronos = _motoristaRepositorio.ObterMotoristaChronosPorCNH(cnh);

            if (motoristaChronos != null)
            {
                return Json(new
                {
                    motoristaChronos.Nome,
                    motoristaChronos.CNH,
                    motoristaChronos.ValidadeCNH,
                    motoristaChronos.RG,
                    motoristaChronos.CPF,
                    motoristaChronos.Celular,
                    UltimaAlteracao = motoristaChronos.UltimaAlteracao.DataHoraFormatada()
                }, JsonRequestBehavior.AllowGet);
            }

            return null;
        }
    }
}