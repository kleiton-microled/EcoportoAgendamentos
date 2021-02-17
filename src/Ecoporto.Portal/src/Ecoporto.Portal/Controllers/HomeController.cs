using Ecoporto.Portal.Config;
using Ecoporto.Portal.Dados.Interfaces;
using Ecoporto.Portal.Models.ViewModels;
using Ecoporto.Portal.Services.Interfaces;
using Newtonsoft.Json;
using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Ecoporto.Portal.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly IUsuarioRepositorio _usuarioRepositorio;
        private readonly IMenuRepositorio _menuRepositorio;
        private readonly IMenuService _menuService;

        public HomeController(
            IUsuarioRepositorio usuarioRepositorio, 
            IMenuRepositorio menuRepositorio,
            IMenuService menuService)
        {
            _usuarioRepositorio = usuarioRepositorio;
            _menuRepositorio = menuRepositorio;
            _menuService = menuService;
        }      

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult Login(int? id)
        {
            id = 6543038; // TODO: Retirar antes de publicar

            if (id == null)
                return Redirect(AppConfig.UrlICC());

            var usuario = _usuarioRepositorio.ObterUsuarioICC(id.Value);

            if (usuario == null)
                return Redirect(AppConfig.UrlICC());

            //_usuarioRepositorio.ExcluirRegistroIntAcesso(id.Value);

            if (usuario.Ativo == false)
                throw new Exception($"O usuário inativo no sistema. Entrar em contato com o suporte técnico.");

            var usuarioJson = JsonConvert
                .SerializeObject(new
                {
                    usuario.Id,
                    usuario.Nome,
                    usuario.Email,
                    usuario.CPF,
                    usuario.Ativo,
                    usuario.EmpresaLoginId,
                    usuario.EmpresaId,
                    usuario.EmpresaDescricao,
                    usuario.EmpresaDescricaoCurta,
                    usuario.EmpresaCnpj,
                    usuario.Administrador,
                    usuario.Perfil
                });

            FormsAuthentication.SignOut();

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

            return RedirectToAction(nameof(Index));
        }

        public ActionResult MenuBlocos()
        {
            return PartialView(new MenuViewModel
            {
                Menus = _menuService.ObterMenus()
            });
        }     

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();

            return Redirect(AppConfig.UrlICC());
        }        
    }
}