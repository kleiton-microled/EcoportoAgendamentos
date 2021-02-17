using Ecoporto.Portal.Dados.Interfaces;
using Ecoporto.Portal.Extensions;
using Ecoporto.Portal.Models.ViewModels;
using Ecoporto.Portal.Services.Interfaces;
using System.Web.Mvc;

namespace Ecoporto.Portal.Controllers
{
    [Authorize]
    public class MenuController : BaseController
    {
        private readonly IUsuarioRepositorio _usuarioRepositorio;
        private readonly IMenuRepositorio _menuRepositorio;
        private readonly IMenuService _menuService;
        private readonly INavegacaoService _navegacaoService;

        public MenuController(
            IUsuarioRepositorio usuarioRepositorio,
            IMenuRepositorio menuRepositorio,
            IMenuService menuService,
            INavegacaoService navegacaoService)
        {
            _usuarioRepositorio = usuarioRepositorio;
            _menuRepositorio = menuRepositorio;
            _menuService = menuService;
            _navegacaoService = navegacaoService;
        }

        [OutputCache(Duration = 1200)]
        public ActionResult Index()
        {
            return PartialView(new MenuViewModel
            {
                Menus = _menuService.ObterMenus()
            });
        }      

        public ActionResult RedirecionarParaHome()
        {
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public ActionResult Navegar(int? menuId, int? subMenuId)
        {
            if (!menuId.HasValue)
                return RetornarErro($"Menu não informado");

            var menuBusca = _menuRepositorio.ObterMenuPorId(menuId.Value);

            if (menuBusca == null)
                return RetornarErro($"Menu não encontrado");

            long acessoId = 0;

            if (subMenuId.HasValue)
            {
                var subMenuBusca = _menuRepositorio.ObterSubMenuPorId(subMenuId.Value);

                if (subMenuBusca == null)
                    return RetornarErro($"Sub Menu não encontrado");

                acessoId = _navegacaoService.SolicitarAcesso(User);

                var subMenuUrl = subMenuBusca.Url.UltimoCaractere() != "/"
                    ? string.Concat(subMenuBusca.Url, "/")
                    : subMenuBusca.Url;

                var parametrosSubMenu = subMenuBusca.Parametros ?? string.Empty;

                subMenuBusca.Url = string.Concat(subMenuUrl, acessoId, parametrosSubMenu);

                return Redirect(subMenuBusca.Url);
                //return View(new MenuViewModel
                //{
                //    Url = subMenuBusca.Url
                //});
            }

            acessoId = _navegacaoService.SolicitarAcesso(User);

            var menuUrl = menuBusca.Url.UltimoCaractere() != "/"
                ? string.Concat(menuBusca.Url, "/")
                : menuBusca.Url;

            var parametrosMenu = menuBusca.Parametros ?? string.Empty;

            menuBusca.Url = string.Concat(menuUrl, menuBusca.Parametros ?? string.Empty, acessoId, parametrosMenu);

            return Redirect(menuBusca.Url);

            //return View(new MenuViewModel
            //{
            //    Url = menuBusca.Url
            //});
        }
    }
}