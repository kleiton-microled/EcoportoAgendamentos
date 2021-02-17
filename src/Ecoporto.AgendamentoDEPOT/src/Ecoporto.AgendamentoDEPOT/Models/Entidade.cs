using FluentValidation;
using FluentValidation.Results;
using System.Collections.Generic;

namespace Ecoporto.AgendamentoDEPOT.Models
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

        public void AdicionarNotificacao(string mensagem)
            => ValidationResult.Errors.Add(new ValidationFailure(string.Empty, mensagem));

        public void AdicionarNotificacoes(IList<ValidationFailure> notificacoes)
        {
            foreach (var notificacao in notificacoes)
                ValidationResult.Errors.Add(notificacao);
        }

        public abstract void Validar();
    }
}