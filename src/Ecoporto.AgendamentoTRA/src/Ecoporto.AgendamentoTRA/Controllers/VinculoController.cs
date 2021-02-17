using Ecoporto.AgendamentoTRA.Dados.Interfaces;
using Ecoporto.AgendamentoTRA.Extensions;
using Ecoporto.AgendamentoTRA.Models;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace Ecoporto.AgendamentoTRA.Controllers
{
    [Authorize]
    public class VinculoController : Controller
    {
        private readonly ITransportadoraRepositorio _transportadoraRepositorio;
        private readonly IRecintoRepositorio _recintoRepositorio;
        private readonly IVinculoBaseRepositorio _vinculoRepositorio;

        public VinculoController(
            ITransportadoraRepositorio transportadoraRepositorio,
            IRecintoRepositorio recintoRepositorio,
            IVinculoDEPOTRepositorio vinculoDEPOTRepositorio,
            IVinculoTRARepositorio vinculoTRARepositorio)
        {
            _transportadoraRepositorio = transportadoraRepositorio;
            _recintoRepositorio = recintoRepositorio;

            if (System.Web.HttpContext.Current.User.RecintoDEPOT())
            {
                _vinculoRepositorio = vinculoDEPOTRepositorio;
            }
            else
            {
                _vinculoRepositorio = vinculoTRARepositorio;
            }
        }

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult ListarTransportadoras(string criterio)
        {
            var resultados = _transportadoraRepositorio.ObterTransportadoras(criterio?.ToUpper() ?? string.Empty);

            return Json(new
            {
                criterio,
                resultados
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult ConsultaVinculos(JQueryDataTablesParamViewModel Params)
        {
            var vinculos = _vinculoRepositorio
                .ObterVinculos(Params.Pagina, Params.iDisplayLength, Params.OrderBy, out int totalFiltro, User.ObterRecinto(), Params.sSearch)
                .Select(c => new
                {
                    c.Id,
                    c.RazaoSocial,
                    c.CNPJ,
                    c.Ativo
                });

            var totalRegistros = _vinculoRepositorio.ObterTotalVinculos(User.ObterRecinto());

            var resultado = new
            {
                Params.sEcho,
                iTotalRecords = totalRegistros,
                iTotalDisplayRecords = totalFiltro,
                aaData = vinculos
            };

            return Json(resultado, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Index(int transportadoraId)
        {
            var transportadoraBusca = _transportadoraRepositorio.ObterTransportadorasPorId(transportadoraId);

            if (transportadoraBusca == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Transportadora inexistente");

            var busca = _vinculoRepositorio.ObterVinculoPorTransportadoraERecinto(transportadoraId, User.ObterRecinto());

            if (busca != null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Já existe um Vínculo para esta Transportadora");

            _vinculoRepositorio.VincularTransportadora(transportadoraId, User.ObterRecinto());

            return new HttpStatusCodeResult(HttpStatusCode.NoContent);
        }

        public void PopularRecintos(SelecionarRecintoViewModel viewModel)
        {
            var recintos = _recintoRepositorio
                .ObterRecintosDaTransportadora(User.ObterTransportadoraId()).ToList();

            viewModel.Recintos = recintos;
        }

        [HttpPost]
        public ActionResult Bloquear(int id)
        {
            var vinculoBusca = _vinculoRepositorio.ObterVinculoPorId(id);

            if (vinculoBusca == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Vínculo inexistente");

            if (vinculoBusca.RecintoId != User.ObterRecinto())
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Vínculo não pertence a este Recinto");

            _vinculoRepositorio.Bloquear(id);

            return new HttpStatusCodeResult(HttpStatusCode.NoContent);
        }

        [HttpPost]
        public ActionResult Habilitar(int id)
        {
            var vinculoBusca = _vinculoRepositorio.ObterVinculoPorId(id);

            if (vinculoBusca == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Vínculo inexistente");

            if (vinculoBusca.RecintoId != User.ObterRecinto())
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Vínculo não pertence a este Recinto");

            _vinculoRepositorio.Habilitar(id);

            return new HttpStatusCodeResult(HttpStatusCode.NoContent);
        }

        [HttpPost]
        public ActionResult Excluir(int id)
        {
            var vinculoBusca = _vinculoRepositorio.ObterVinculoPorId(id);

            if (vinculoBusca == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Vínculo inexistente");

            if (vinculoBusca.RecintoId != User.ObterRecinto())
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Vínculo não pertence a este Recinto");

            if (User.RecintoDEPOT())
            {
                if (_vinculoRepositorio.ExisteAgendamentoNoRecintoDEPOT(User.ObterRecinto(), vinculoBusca.TransportadoraId))
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Existem agendamentos utilizando este Recinto x Transportadora");
            }
            else
            {
                if (_vinculoRepositorio.ExisteAgendamentoNoRecintoTRA(User.ObterRecinto(), vinculoBusca.TransportadoraId))
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Existem agendamentos utilizando este Recinto x Transportadora");
            }

            _vinculoRepositorio.ExcluirVinculo(id);

            return new HttpStatusCodeResult(HttpStatusCode.NoContent);
        }
    }
}