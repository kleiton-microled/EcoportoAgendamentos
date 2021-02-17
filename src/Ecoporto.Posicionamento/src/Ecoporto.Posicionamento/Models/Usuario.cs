namespace Ecoporto.Posicionamento.Models
{
    public class Usuario : Entidade<Usuario>
    {
        public string Nome { get; set; }

        public string Email { get; set; }

        public int DespachanteId { get; set; }

        public string DespachanteDescricao { get; set; }

        public string DespachanteCNPJ { get; set; }

        public bool Ativo { get; set; }

        public string CPF { get; set; }

        public override void Validar()
        {        
            ValidationResult = Validate(this);
        }
    }
}