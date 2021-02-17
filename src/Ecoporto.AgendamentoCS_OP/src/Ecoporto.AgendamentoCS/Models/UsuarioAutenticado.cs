namespace Ecoporto.AgendamentoCS.Models
{
    public class UsuarioAutenticado
    {
        public string Id { get; set; }

        public string Nome { get; set; }

        public string Email { get; set; }

        public int TransportadoraId { get; set; }

        public string TransportadoraDescricao { get; set; }

        public string TransportadoraCNPJ { get; set; }

        public bool Ativo { get; set; }

        public string CPF { get; set; }
    }
}