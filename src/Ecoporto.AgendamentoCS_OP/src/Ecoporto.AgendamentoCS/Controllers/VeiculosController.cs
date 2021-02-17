using Ecoporto.AgendamentoCS.Dados.Interfaces;
using Ecoporto.AgendamentoCS.Extensions;
using Ecoporto.AgendamentoCS.Helpers;
using Ecoporto.AgendamentoCS.Models;
using Ecoporto.AgendamentoCS.Models.ViewModels;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace Ecoporto.AgendamentoCS.Controllers
{
    [Authorize]
    public class VeiculosController : BaseController
    {
        private readonly IVeiculoRepositorio _veiculoRepositorio;

        public VeiculosController(IVeiculoRepositorio veiculoRepositorio)
        {
            _veiculoRepositorio = veiculoRepositorio;
        }

        [HttpGet]
        public ActionResult Index()
        {
            var resultado = _veiculoRepositorio
                .ObterVeiculos(User.ObterTransportadoraId()).ToList();

            return View(resultado);
        }

        private void ObterTiposVeiculos(VeiculoViewModel viewModel)
        {
            viewModel.TiposVeiculos = _veiculoRepositorio
                .ObterTiposVeiculos().ToList();
        }

        [HttpGet]
        public ActionResult Cadastrar()
        {
            var viewModel = new VeiculoViewModel();

            ObterTiposVeiculos(viewModel);

            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Cadastrar([Bind(Include = "TipoCaminhaoId, Cavalo, Carreta, Chassi, Renavam, Tara, Modelo, Cor")] VeiculoViewModel viewModel)
        {
            var veiculo = new Veiculo(
                User.ObterTransportadoraId(),
                viewModel.TipoCaminhaoId,
                viewModel.Cavalo,
                viewModel.Carreta,
                viewModel.Chassi,
                viewModel.Renavam,
                viewModel.Tara,
                viewModel.Modelo,
                viewModel.Cor);           

            if (Validar(veiculo))
            {
                _veiculoRepositorio.Cadastrar(veiculo);
                TempData["Sucesso"] = true;
            }

            ObterTiposVeiculos(viewModel);

            return View(viewModel);
        }

        [HttpGet]
        public ActionResult Atualizar(int? id)
        {
            if (id == null)
                return RedirectToAction(nameof(Index));

            var veiculoBusca = _veiculoRepositorio.ObterVeiculoPorId(id.Value);

            if (veiculoBusca == null)
                return RegistroNaoEncontrado();

            if (veiculoBusca.TransportadoraId != User.ObterTransportadoraId())
                return RedirectToAction(nameof(Index));

            var viewModel = new VeiculoViewModel
            {
                Id = veiculoBusca.Id,
                TipoCaminhaoId = veiculoBusca.TipoCaminhaoId,
                Cavalo = veiculoBusca.Cavalo,
                Carreta = veiculoBusca.Carreta,
                Chassi = veiculoBusca.Chassi,
                Renavam = veiculoBusca.Renavam,
                Tara = veiculoBusca.Tara,
                Modelo = veiculoBusca.Modelo,
                Cor = veiculoBusca.Cor,
                UltimaAlteracao = veiculoBusca.UltimaAlteracao.DataHoraFormatada()
            };

            ObterTiposVeiculos(viewModel);

            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Atualizar([Bind(Include = "Id, TipoCaminhaoId, Cavalo, Carreta, Chassi, Renavam, Tara, Modelo, Cor")] VeiculoViewModel viewModel, int? id)
        {
            if (id == null)
                return RedirectToAction(nameof(Index));

            var veiculoBusca = _veiculoRepositorio.ObterVeiculoPorId(id.Value);

            if (veiculoBusca == null)
                return RegistroNaoEncontrado();

            veiculoBusca.Alterar(new Veiculo(
                User.ObterTransportadoraId(),
                viewModel.TipoCaminhaoId,
                viewModel.Cavalo,
                viewModel.Carreta,
                viewModel.Chassi,
                viewModel.Renavam,
                viewModel.Tara,
                viewModel.Modelo,
                viewModel.Cor));            

            if (Validar(veiculoBusca))
            {
                _veiculoRepositorio.Atualizar(veiculoBusca);
                TempData["Sucesso"] = true;
            }

            ObterTiposVeiculos(viewModel);

            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Excluir(int id)
        {
            try
            {
                var veiculoBusca = _veiculoRepositorio.ObterVeiculoPorId(id);

                if (veiculoBusca == null)
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Veículo não encontrado");

                if (veiculoBusca.TransportadoraId != User.ObterTransportadoraId())
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Veículo não pertence a transportadora");

                _veiculoRepositorio.Excluir(veiculoBusca.Id);
            }
            catch
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            return new HttpStatusCodeResult(HttpStatusCode.NoContent);
        }
    }
}