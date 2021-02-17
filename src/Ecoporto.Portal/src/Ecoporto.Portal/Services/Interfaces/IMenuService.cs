using Ecoporto.Portal.Models;
using System.Collections.Generic;

namespace Ecoporto.Portal.Services.Interfaces
{
    public interface IMenuService
    {
        IEnumerable<Menu> ObterMenus();
    }
}