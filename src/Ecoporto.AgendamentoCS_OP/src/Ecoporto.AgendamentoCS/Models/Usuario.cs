namespace Ecoporto.AgendamentoCS.Models
{
    public class Usuario : Entidade<Usuario>
    {
        public string Nome { get; set; }

        public string Email { get; set; }

        public int TransportadoraId { get; set; }

        public string TransportadoraDescricao { get; set; }

        public string TransportadoraCNPJ { get; set; }

        public bool Ativo { get; set; }

        public string CPF { get; set; }

        public override void Validar()
        {        
            ValidationResult = Validate(this);
        }
    }
}