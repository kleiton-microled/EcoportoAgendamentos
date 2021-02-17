using Ecoporto.AgendamentoTRA.Config;
using Ecoporto.AgendamentoTRA.Dados.Interfaces;
using Ecoporto.AgendamentoTRA.Helpers;
using Newtonsoft.Json;
using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Ecoporto.AgendamentoTRA.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUsuarioRepositorio _usuarioRepositorio;

        public HomeController(IUsuarioRepositorio usuarioRepositorio)
        {
            _usuarioRepositorio = usuarioRepositorio;
        }

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult Login(int? id)
        {
            if (id == null)
                return Redirect(AppConfig.UrlICC());

            var usuario = _usuarioRepositorio.ObterUsuarioDEPOT(id.Value);

            if (usuario == null)
            {
                usuario = _usuarioRepositorio.ObterUsuarioTRA(id.Value);

                if (usuario == null)
                    return Redirect(AppConfig.UrlICC());
            }

            _usuarioRepositorio.ExcluirRegistroIntAcesso(id.Value);

            if (usuario.Ativo == false)
                throw new Exception($"O usuário está inativo no sistema. Entrar em contato com o suporte técnico.");

            var usuarioJson = JsonConvert
                .SerializeObject(new
                {
                    usuario.Id,
                    usuario.Nome,
                    usuario.Email,
                    usuario.CPF,
                    usuario.Ativo,
                    usuario.RecintoId,
                    usuario.RecintoDescricao,
                    usuario.RecintoCNPJ,
                    usuario.DEPOT
                });

            Identity.Logout();

            FormsAuthenticationTicket authTicket = new FormsAuthenticationTicket(
                1,
                usuario.Nome,
                DateTime.Now,
                DateTime.Now.AddMinutes(480),
                true,
                usuarioJson);

            Response.Cookies.Add(
                new HttpCookie(
                    FormsAuthentication.FormsCookieName,
                    FormsAuthentication.Encrypt(authTicket)));

            return RedirectToAction("Index", "Vinculo");
        }

        [HttpGet]
        public ActionResult Erro()
        {
            return View();
        }

        [AllowAnonymous]
        public ActionResult Logout()
        {
            Identity.Logout();

            return Redirect(AppConfig.UrlICC());
        }
    }
}