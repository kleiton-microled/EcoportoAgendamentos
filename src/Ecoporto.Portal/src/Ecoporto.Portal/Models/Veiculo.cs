using Ecoporto.Portal.Helpers;
using FluentValidation;
using System;

namespace Ecoporto.Portal.Models
{
    public class Veiculo : Entidade<Veiculo>
    {
        public Veiculo()
        {

        }

        public Veiculo(int id, string descricao)
        {
            Id = id;
            Descricao = descricao;
        }

        public Veiculo(
            int transportadoraId, 
            int tipoCaminhaoId, 
            string cavalo, 
            string carreta, 
            string chassi,
            string renavam, 
            decimal? tara, 
            string modelo, 
            string cor)
        {
            TransportadoraId = transportadoraId;
            TipoCaminhaoId = tipoCaminhaoId;
            Cavalo = cavalo;
            Carreta = carreta;
            Chassi = chassi;
            Renavam = renavam;
            Tara = tara;
            Modelo = modelo;
            Cor = cor;
        }

        public int TransportadoraId { get; set; }

        public int TipoCaminhaoId { get; set; }

        public string TipoCaminhaoDescricao { get; set; }

        public string Cavalo { get; set; }

        public string Carreta { get; set; }

        public string Chassi { get; set; }

        public string Renavam { get; set; }

        public decimal? Tara { get; set; }

        public string Modelo { get; set; }

        public string Cor { get; set; }

        public string Descricao { get; set; }

        public DateTime UltimaAlteracao { get; set; }

        public void Alterar(Veiculo veiculo)
        {
            TipoCaminhaoId = veiculo.TipoCaminhaoId;
            Cavalo = veiculo.Cavalo;
            Carreta = veiculo.Carreta;
            Chassi = veiculo.Chassi;
            Renavam = veiculo.Renavam;
            Tara = veiculo.Tara;
            Modelo = veiculo.Modelo;
            Cor = veiculo.Cor;
        }

        public override void Validar()
        {
            RuleFor(c => c.TransportadoraId)
                .GreaterThan(0)
                .WithMessage("Transportadora não informada");

            RuleFor(c => c.TipoCaminhaoId)
                .GreaterThan(0)
                .WithMessage("Tipo de Veículo não informado");

            RuleFor(c => c.Cavalo)
                .NotEmpty()
                .WithMessage("Placa do Cavalo não informada corretamente")
                .Length(8)
                .WithMessage("Placa do cavalo é inválida");

            RuleFor(c => c.Carreta)                
                .Length(8)
                .When(c => !string.IsNullOrEmpty(c.Carreta))
                .WithMessage("Placa da Carreta não informada corretamente");

            RuleFor(c => c.Renavam)
                .NotEmpty()               
                .WithMessage("Renavam não informado");

            RuleFor(c => c.Renavam)
                .Must(Validation.RenavamValido)
                .When(c => !string.IsNullOrEmpty(c.Renavam))
                .WithMessage("Renavam inválido");

            ValidationResult = Validate(this);
        }

        public static bool CNHValida(DateTime validadeCNH)
        {
            if (DateTimeHelpers.IsDate(validadeCNH))
            {
                return validadeCNH > DateTime.Now.AddDays(-30);
            }

            return false;
        }
    }
}