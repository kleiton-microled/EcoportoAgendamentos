using FluentValidation;

namespace Ecoporto.AgendamentoConteiner.Models
{
    public class ConteinerVazio : Entidade<ConteinerVazio>
    {
        public Reserva Reserva { get; set; }

        public int? AgendamentoId { get; set; }
        
        public int QuantidadeVazios { get; set; }

        public override void Validar()
        {
            RuleFor(c => c.Reserva)
                .NotNull()
                .WithMessage("Reserva não informada");

            RuleFor(c => c.AgendamentoId)
                .GreaterThan(0)
                .WithMessage("ID do Agendamento não informada");

            RuleFor(c => c.QuantidadeVazios)
                .GreaterThan(0)
                .WithMessage("Quantidade de Contêineres inválida");

            RuleFor(c => c.QuantidadeVazios)
                .LessThanOrEqualTo(2)
                .WithMessage("É permitido no máximo 2 unidades por agendamento");

            ValidationResult = Validate(this);
        }
    }
}