using Ecoporto.AgendamentoTRA.Dados.Interfaces;
using System.Web.Mvc;

namespace Ecoporto.AgendamentoTRA.Controllers
{
    [Authorize]
    public class TransportadoraController : Controller
    {
        private readonly ITransportadoraRepositorio _transportadoraRepositorio;

        public TransportadoraController(ITransportadoraRepositorio transportadoraRepositorio)
        {
            _transportadoraRepositorio = transportadoraRepositorio;
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
    }
}