using Ecoporto.Posicionamento.Models;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Ecoporto.Posicionamento.Controllers
{
    public class BaseController : Controller
    {
        public bool Validar<T>(Entidade<T> entidade)
        {
            ModelState.Clear();

            entidade.Validar();

            foreach (var erro in entidade.ValidationResult.Errors)
                ModelState.AddModelError(erro.PropertyName, erro.ErrorMessage);

            return entidade.Valido;
        }

        public ActionResult RetornarErros()
        {
            Response.StatusCode = (int)HttpStatusCode.BadRequest;

            return Json(new
            {
                erros = ModelState.Values.SelectMany(v => v.Errors)
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