namespace Ecoporto.AgendamentoTRA.Models
{
    public class UsuarioAutenticado
    {
        public int Id { get; set; }

        public string Nome { get; set; }

        public string Email { get; set; }

        public bool Ativo { get; set; }

        public string CPF { get; set; }

        public string RecintoId { get; set; }
        
        public string RecintoDescricao { get; set; }

        public string RecintoCNPJ { get; set; }

        public bool DEPOT { get; set; }
    }
}