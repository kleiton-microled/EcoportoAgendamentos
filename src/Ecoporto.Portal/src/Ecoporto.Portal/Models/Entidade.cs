using FluentValidation;
using FluentValidation.Results;

namespace Ecoporto.Portal.Models
{
    public abstract class Entidade<T> : AbstractValidator<T>
    {
        public Entidade()
        {
            ValidationResult = new ValidationResult();
        }

        public int Id { get; set; }

        public ValidationResult ValidationResult { get; set; }

        public bool Valido => ValidationResult.IsValid;

        public bool Invalido => !ValidationResult.IsValid;

        public abstract void Validar();
    }
}