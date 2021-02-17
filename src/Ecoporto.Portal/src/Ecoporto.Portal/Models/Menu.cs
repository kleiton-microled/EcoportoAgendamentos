using System.Collections.Generic;

namespace Ecoporto.Portal.Models
{
    public class Menu
    {
        public int Id { get; set; }

        public string Descricao { get; set; }

        public string Detalhes { get; set; }

        public string Url { get; set; }

        public bool Ativo { get; set; }

        public string Icone { get; set; }

        public string Parametros { get; set; }

        public string Cor { get; set; }

        public List<SubMenu> SubMenus { get; set; } = new List<SubMenu>();
    }
}