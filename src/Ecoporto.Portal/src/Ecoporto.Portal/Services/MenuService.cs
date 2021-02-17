using Ecoporto.Portal.Dados.Interfaces;
using Ecoporto.Portal.Models;
using Ecoporto.Portal.Services.Interfaces;
using System.Collections.Generic;

namespace Ecoporto.Portal.Services
{
    public class MenuService : IMenuService
    {
        private readonly IMenuRepositorio _menuRepositorio;

        public MenuService( IMenuRepositorio menuRepositorio)
        {
            _menuRepositorio = menuRepositorio;
        }

        public IEnumerable<Menu> ObterMenus()
        {
            var menus = _menuRepositorio.ObterMenus();

            foreach (var menu in menus)
            {
                var subMenus = _menuRepositorio.ObterSubMenus(menu.Id);

                menu.SubMenus.AddRange(subMenus);
            }

            return menus;
        }
    }
}