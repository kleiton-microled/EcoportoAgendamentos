using Ecoporto.Portal.Models;
using System.Collections.Generic;

namespace Ecoporto.Portal.Dados.Interfaces
{
    public interface IMenuRepositorio
    {
        IEnumerable<Menu> ObterMenus();
        Menu ObterMenuPorId(int id);
        IEnumerable<SubMenu> ObterSubMenus(int menuId);
        Menu ObterSubMenuPorId(int id);
    }
}