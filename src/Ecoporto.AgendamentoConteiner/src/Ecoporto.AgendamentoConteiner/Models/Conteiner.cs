using Ecoporto.AgendamentoConteiner.Enums;
using Ecoporto.AgendamentoConteiner.Helpers;
using FluentValidation;
using System;
using System.Collections.Generic;

namespace Ecoporto.AgendamentoConteiner.Models
{
    public class Conteiner : Entidade<Conteiner>
    {
        public Reserva Reserva { get; set; }

        public int? AgendamentoId { get; set; }

        public string Sigla { get; set; }

        public int Volumes { get; set; }

        public decimal Tara { get; set; }

        public decimal PesoLiquido { get; set; }

        public string ISO { get; set; }

        public decimal Bruto { get; set; }

        public string IMO1 { get; set; }

        public string IMO2 { get; set; }

        public string IMO3 { get; set; }

        public string IMO4 { get; set; }

        public string ONU1 { get; set; }

        public string ONU2 { get; set; }

        public string ONU3 { get; set; }

        public string ONU4 { get; set; }

        public decimal? Temp { get; set; }

        public string Escala { get; set; }

        public string Umidade { get; set; }

        public string Ventilacao { get; set; }

        public int Comprimento { get; set; }

        public int Altura { get; set; }

        public int LateralDireita { get; set; }

        public int LateralEsquerda { get; set; }

        public string LacreArmador1 { get; set; }

        public string LacreArmador2 { get; set; }

        public string OutrosLacres1 { get; set; }

        public string OutrosLacres2 { get; set; }

        public string OutrosLacres3 { get; set; }

        public string OutrosLacres4 { get; set; }

        public string LacreExportador { get; set; }

        public string LacreSIF { get; set; }

        public bool Bagagem { get; set; }

        public bool ReeferLigado { get; set; }

        public string DAT { get; set; }

        public int QuantidadeVazios { get; set; }

        public int TipoDocTransitoId { get; set; }

        public string TipoDocTransitoDescricao { get; set; }

        public string NumDocTransitoDUE { get; set; }

        public string DataDocTransitoDUE { get; set; }

        public string Observacoes { get; set; }
	
        public bool ExigeNF { get; set; }

        public List<NotaFiscal> NotasFiscais { get; set; } = new List<NotaFiscal>();

        public bool IsIMO
            => !string.IsNullOrEmpty(Reserva.IMO1 + Reserva.IMO2 + Reserva.IMO3 + Reserva.IMO4);

        public bool IsONU
            => !string.IsNullOrEmpty(Reserva.ONU1 + Reserva.ONU2 + Reserva.ONU3 + Reserva.ONU4);

        public override void Validar()
        {
            RuleFor(c => c.Sigla)
                .NotEmpty()
                .When(c => c.Reserva.EF == TipoAgendamentoConteiner.Cheio)
                .WithMessage("Informe a Sigla do Contêiner");

            RuleFor(c => c.Sigla)
               .Length(12)
               .When(c => c.Reserva.EF == TipoAgendamentoConteiner.Cheio)
               .WithMessage("Digite a Sigla do Contêiner corretamente");

            RuleFor(c => c.Reserva.Tamanho)
                .NotEmpty()
                .WithMessage("Tamanho do Contêiner não informado na Reserva");

            RuleFor(c => c.Reserva.Tipo)
                .NotEmpty()
                .WithMessage("Tipo do Contêiner não informado na Reserva");

            RuleFor(c => c.ISO)
                .NotEmpty()
                .WithMessage("Informe o código ISO");

            RuleFor(c => c.Tara)
                .GreaterThan(0)
                .When(c => c.Reserva.EF == TipoAgendamentoConteiner.Cheio)
                .WithMessage("O Campo Tara é obrigatório");

            RuleFor(c => c.Tara)
                .LessThanOrEqualTo(c => c.Bruto)
                .When(c => c.Bruto > 0)
                .WithMessage("O Peso Tara deve ser inferior ao Peso Bruto");

            RuleFor(c => c.PesoLiquido)
                .GreaterThan(0)
                .When(c => c.Reserva.EF == TipoAgendamentoConteiner.Cheio)
                .WithMessage("O Campo Peso Liquido é obrigatório");

            RuleFor(c => c.Bruto)
                .GreaterThan(0)
                .When(c => c.Reserva.EF == TipoAgendamentoConteiner.Cheio)
                .WithMessage("O Campo Peso Bruto é obrigatório");

            RuleFor(c => c.Bruto)
                .Equal(c => c.Tara + c.PesoLiquido)
                .When(c => c.Reserva.EF == TipoAgendamentoConteiner.Cheio && c.Tara > 0 && c.Bruto > 0)
                .WithMessage("Divergência entre os valores informados para a Tara, Peso Líquido e Peso Bruto");

            RuleFor(c => c.Volumes)
                .GreaterThan(0)
                .When(c => c.Reserva.EF == TipoAgendamentoConteiner.Cheio)
                .WithMessage("O Campo Volumes é obrigatório");

            RuleFor(c => c.ReeferLigado)
                .NotEqual(true)
                .When(c => !ConteinerHelper.Reefer(this.Reserva.Tipo))
                .WithMessage("O Tipo de Contêiner selecionado não é Reefer");

            RuleFor(c => c.ReeferLigado)
                .Equal(true)
                .When(c => c.Reserva.ReeferLigado && ConteinerHelper.Reefer(c.Reserva.Tipo))
                .WithMessage("Divergência quanto a Refrigeração (Ligado/Desligado) informada no cadastro do booking");

            RuleFor(c => c.ReeferLigado)
                .Equal(false)
                .When(c => c.Reserva.ReeferLigado == false && ConteinerHelper.Reefer(c.Reserva.Tipo))
                .WithMessage("Divergência quanto a Refrigeração (Ligado/Desligado) informada no cadastro do booking");

            RuleFor(c => c.Temp)
                .Equal(c => c.Reserva.Temp)
                .When(c => c.Reserva.ReeferLigado && ConteinerHelper.Reefer(c.Reserva.Tipo))
                .WithMessage("Divergência com a Temperatura informada no cadastro do booking");

            RuleFor(c => c.Escala)
                .Equal(c => c.Reserva.Escala)
                .When(c => c.Reserva.ReeferLigado && ConteinerHelper.Reefer(c.Reserva.Tipo))
                .WithMessage("Divergência com a Escala informada no cadastro do booking");

            RuleFor(c => c.Umidade)
                .Equal(c => c.Reserva.Umidade)
                .When(c => c.Reserva.ReeferLigado && ConteinerHelper.Reefer(c.Reserva.Tipo))
                .WithMessage("Divergência com a Umidade informada no cadastro do booking");

            RuleFor(c => c.Ventilacao)
                .Equal(c => c.Reserva.Ventilacao)
                .When(c => c.Reserva.ReeferLigado && ConteinerHelper.Reefer(c.Reserva.Tipo))
                .WithMessage("Divergência com a Ventilação informada no cadastro do booking");

            RuleFor(c => c.Comprimento)
                .Equal(c => c.Reserva.Comprimento)
                .When(c => ConteinerHelper.Excesso(c.Reserva.Tipo))
                .WithMessage("Divergência com o Comprimento (OL) informado no cadastro do booking");

            RuleFor(c => c.Altura)
                .Equal(c => c.Reserva.Altura)
                .When(c => ConteinerHelper.Excesso(c.Reserva.Tipo))
                .WithMessage("Divergência com a Altura (OH) informada no cadastro do booking");

            RuleFor(c => c.LateralDireita)
                .Equal(c => c.Reserva.LateralDireita)
                .When(c => ConteinerHelper.Excesso(c.Reserva.Tipo))
                .WithMessage("Divergência com a Lateral Direita (OW) informada no cadastro do booking");

            RuleFor(c => c.LateralEsquerda)
                .Equal(c => c.Reserva.LateralEsquerda)
                .When(c => ConteinerHelper.Excesso(c.Reserva.Tipo))
                .WithMessage("Divergência com a Lateral Esquerda (OWL) informada no cadastro do booking");

            RuleFor(c => c.IMO1)
                .Must(c => ValidarIMO())
                .When(c => c.IsIMO)
                .WithMessage("Divergência com o IMO informado no cadastro do Booking");

            RuleFor(c => c.ONU1)
                  .Must(c => ValidarONU())
                  .When(c => c.IsONU)
                  .WithMessage("Divergência com o ONU informado no cadastro do Booking");

            RuleFor(c => c.LacreArmador1)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotNull()
                .WithMessage("Informe o Lacre Armador 1")
                .NotEmpty()
                .WithMessage("Informe o Lacre Armador 1")
                .When(c => c.Reserva.EF == TipoAgendamentoConteiner.Cheio && c.Reserva.ShipperOwner == false);

            RuleFor(c => c.DataDocTransitoDUE)
               .Must(DateTimeHelpers.IsDate)
               .When(c => !string.IsNullOrEmpty(c.DataDocTransitoDUE))
               .WithMessage("A Data do Documento Trânsito DUE é inválida");

            ValidationResult = Validate(this);
        }

        private bool RegraConteinerReefer()
        {
            return ((Reserva.Tipo == "RE" || Reserva.Tipo == "HR") && Reserva.EF == TipoAgendamentoConteiner.Cheio && Reserva.ReeferLigado);
        }

        private bool ValidarTemperatura(decimal? temp)
        {
            if (RegraConteinerReefer())
                return temp.HasValue;

            return true;
        }

        private bool ValidarUmidade(decimal? umidade)
        {
            if (RegraConteinerReefer())
                return umidade.HasValue;

            return true;
        }

        private bool ValidarEscala(string escala)
        {
            if (RegraConteinerReefer())
                return !string.IsNullOrEmpty(escala);

            return true;
        }

        private bool ValidarVentilacao(string ventilacao)
        {
            if (RegraConteinerReefer())
                return !string.IsNullOrEmpty(ventilacao);

            return true;
        }

        public bool ValidarIMO()
        {
            if (IsIMO && string.IsNullOrEmpty(IMO1) && string.IsNullOrEmpty(IMO2) && string.IsNullOrEmpty(IMO3) && string.IsNullOrEmpty(IMO4))
                return false;

            return IMO1 == Reserva.IMO1 || IMO2 == Reserva.IMO2 || IMO3 == Reserva.IMO3 || IMO4 == Reserva.IMO4;
        }

        public bool ValidarONU()
        {
            if (IsONU && string.IsNullOrEmpty(ONU1) && string.IsNullOrEmpty(ONU2) && string.IsNullOrEmpty(ONU3) && string.IsNullOrEmpty(ONU4))
                return false;

            return ONU1 == Reserva.ONU1 || ONU2 == Reserva.ONU2 || ONU3 == Reserva.ONU3 || ONU4 == Reserva.ONU4;
        }
    }
}