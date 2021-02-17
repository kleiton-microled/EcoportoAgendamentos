using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Ecoporto.AgendamentoTRA.Controllers
{
    public class BaseController : Controller
    {      
        public ActionResult RetornarErros()
        {
            Response.StatusCode = (int)HttpStatusCode.BadRequest;
            
            var erros = ModelState.Values.SelectMany(v => v.Errors);

            return Json(new
            {
                erros
            }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult RetornarErro(string mensagem)
        {
            ModelState.Clear();

            Response.StatusCode = (int)HttpStatusCode.BadRequest;

            ModelState.AddModelError(string.Empty, mensagem);

            return Json(new
            {
                erros = ModelState.Values.SelectMany(v => v.Errors)
            }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult RegistroNaoEncontrado() =>
            throw new HttpException(404, "Registro não encontrado");
    }
}