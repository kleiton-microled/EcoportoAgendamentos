using Ecoporto.Posicionamento.Dados.Interfaces;
using System.Web.Mvc;

namespace Ecoporto.Posicionamento.Controllers
{
    public class ViagemController : BaseController
    {
        private readonly IViagensRepositorio _viagensRepositorio;

        public ViagemController(IViagensRepositorio viagensRepositorio)
        {            
            _viagensRepositorio = viagensRepositorio;
        }

        [HttpGet]
        public ActionResult ObterViagensEmOperacao()
        {
            var viagens = _viagensRepositorio.ObterViagensEmOperacao();

            return Json(viagens, JsonRequestBehavior.AllowGet);
        }
    }
}