namespace Ecoporto.Portal.Models
{
    public class SubMenu
    {
        public int Id { get; set; }

        public int MenuId { get; set; }

        public string Descricao { get; set; }

        public string Url { get; set; }

        public bool Ativo { get; set; }

        public string Parametros { get; set; }
    }
}