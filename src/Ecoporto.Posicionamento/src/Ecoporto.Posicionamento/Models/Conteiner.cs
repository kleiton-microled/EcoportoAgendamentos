namespace Ecoporto.Posicionamento.Models
{
    public class Conteiner
    {
        public int Id { get; set; }

        public int AgendamentoId { get; set; }

        public string Sigla { get; set; }

        public int Tamanho { get; set; }

        public string Tipo { get; set; }

        public string TipoDescricao { get; set; }
    }
}