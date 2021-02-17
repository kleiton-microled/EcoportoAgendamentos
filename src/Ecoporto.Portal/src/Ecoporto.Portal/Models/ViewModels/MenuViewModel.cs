using System.Collections.Generic;

namespace Ecoporto.Portal.Models.ViewModels
{
    public class MenuViewModel
    {
        public string Url { get; set; }
        public IEnumerable<Menu> Menus { get; set; } = new List<Menu>();
    }
}