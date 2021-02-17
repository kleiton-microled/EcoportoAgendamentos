using Ecoporto.Posicionamento.Config;
using Ecoporto.Posicionamento.Dados.Interfaces;
using Ecoporto.Posicionamento.Helpers;
using Newtonsoft.Json;
using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Ecoporto.Posicionamento.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly IUsuarioRepositorio _usuarioRepositorio;

        public HomeController(IUsuarioRepositorio usuarioRepositorio)
        {
            _usuarioRepositorio = usuarioRepositorio;
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult Login(int? id, int? siteSemMenu = 0)
        {
            id = 6465838;

            if (id == null)
                return Redirect(AppConfig.UrlICC());

            var usuario = _usuarioRepositorio.ObterUsuarioICC(id.Value);

            if (usuario == null)
                return Redirect(AppConfig.UrlICC());

            //_usuarioRepositorio.ExcluirRegistroIntAcesso(id.Value);

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
                    usuario.DespachanteId,
                    usuario.DespachanteDescricao,
                    usuario.DespachanteCNPJ,
                    SiteSemMenu = siteSemMenu
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

            return RedirectToAction("Consultar", "AgendamentoConteiner");
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

            return RedirectToAction("Index", "Home");
        }
    }
}