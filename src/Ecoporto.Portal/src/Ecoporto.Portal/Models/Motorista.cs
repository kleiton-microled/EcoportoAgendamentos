using Ecoporto.Portal.Helpers;
using FluentValidation;
using System;

namespace Ecoporto.Portal.Models
{
    public class Motorista : Entidade<Motorista>
    {
        public Motorista()
        {

        }

        public Motorista(int id, string nome)
        {
            Id = id;
            Nome = nome;
        }

        public Motorista(int transportadoraId, string nome, string cnh, DateTime? validadeCNH, string rg, string cpf, string celular, string nextel, string mop)
        {
            TransportadoraId = transportadoraId;
            Nome = nome;
            CNH = cnh;
            ValidadeCNH = validadeCNH;
            RG = rg;
            CPF = cpf;
            Celular = celular;
            Nextel = nextel;
            MOP = mop;
        }

        public int TransportadoraId { get; set; }

        public string Nome { get; set; }

        public string CNH { get; set; }

        public DateTime? ValidadeCNH { get; set; }

        public string RG { get; set; }

        public string CPF { get; set; }

        public string Celular { get; set; }

        public string Nextel { get; set; }

        public string MOP { get; set; }

        public string Descricao { get; set; }

        public DateTime UltimaAlteracao { get; set; }

        public bool CNHVencida => !Validation.CNHEmDia(ValidadeCNH);

        public void Alterar(Motorista motorista)
        {
            Nome = motorista.Nome;
            CNH = motorista.CNH;
            ValidadeCNH = motorista.ValidadeCNH;
            RG = motorista.RG;
            CPF = motorista.CPF;
            Celular = motorista.Celular;
            Nextel = motorista.Nextel;
            MOP = motorista.MOP;
        }

        public override void Validar()
        {
            RuleFor(c => c.TransportadoraId)
                .GreaterThan(0)
                .WithMessage("Transportadora não informada");

            RuleFor(c => c.Nome)
                .NotEmpty()
                .WithMessage("O Nome do motorista deve ter no máximo 50 caracteres")
                .MinimumLength(3)
                .WithMessage("O Nome do motorista deve ter no mínimo 3 caracteres")
                .MaximumLength(50)
                .WithMessage("Nome não informado");

            RuleFor(c => c.CNH)
                .NotEmpty()
                .WithMessage("CNH não informada")
                .MaximumLength(15)
                .WithMessage("A CNH deve ter no máximo 15 caracteres");

            RuleFor(c => c.CNH)
                .Must(Validation.CNHValida)
                .When(c => !string.IsNullOrEmpty(c.CNH))
                .WithMessage("Número de CNH inválida");

            RuleFor(c => c.ValidadeCNH)
                .NotNull()
                .WithMessage("Validade da CNH não informada");

            RuleFor(c => c.ValidadeCNH)
                .Must(Validation.CNHEmDia)
                .When(c => c.ValidadeCNH != null)
                .WithMessage("A CNH do Motorista está vencida");

            RuleFor(c => c.RG)
                .NotEmpty()
                .WithMessage("RG não informado")
                .MaximumLength(15)
                .WithMessage("A CNH deve ter no máximo 15 caracteres");

            RuleFor(c => c.CPF)
                .NotEmpty()
                .WithMessage("CPF não informado")
                .MaximumLength(14)
                .WithMessage("O CPF deve ter no máximo 14 caracteres");

            RuleFor(c => c.CPF)
                .Must(Validation.CPFValido)
                .When(c => !string.IsNullOrEmpty(c.CPF))
                .WithMessage("CPF inválido");

            RuleFor(c => c.Celular)
                .NotEmpty()
                .WithMessage("Celular não informado")
                .MaximumLength(15)
                .WithMessage("O Celular deve ter no máximo 15 caracteres");

            ValidationResult = Validate(this);
        }
    }
}