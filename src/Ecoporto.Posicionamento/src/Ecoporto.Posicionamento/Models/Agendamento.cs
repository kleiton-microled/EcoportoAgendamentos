using Ecoporto.Posicionamento.Enums;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Ecoporto.Posicionamento.Models
{
    public class Agendamento : Entidade<Agendamento>
    {
        public Agendamento() { }

        public Agendamento(
            int motivoId,
            DateTime dataPrevista,
            string clienteCpfCnpj,
            int novaViagemId,
            TipoAgendamento tipoAgendamento,
            int despachanteId)
        {
            MotivoId = motivoId;
            DataPrevista = dataPrevista;
            ClienteCpfCnpj = clienteCpfCnpj;
            NovaViagemId = novaViagemId;
            TipoAgendamento = tipoAgendamento;
            DespachanteId = despachanteId;
        }

        public string Reserva { get; set; }

        public string Navio { get; set; }

        public string Exportador { get; set; }

        public string ExportadorCnpj { get; set; }

        public string Viagem { get; set; }

        public int NovaViagemId { get; set; }

        public string NovaViagemDescricao { get; set; }

        public string Porto { get; set; }

        public string Line { get; set; }

        public int MotivoId { get; set; }

        public string MotivoDescricao { get; set; }

        public DateTime DataPrevista { get; set; }

        public int ClienteId { get; set; }

        public string Cliente { get; set; }

        public string ClienteCpfCnpj { get; set; }

        public string RazaoSocial { get; set; }

        public DateTime DataCancelamento { get; set; }

        public string MotivoCancelamento { get; set; }

        public int[] ConteineresSelecionados { get; set; }

        public int[] VeiculosSelecionados { get; set; }

        public int ProtocoloUnificado { get; set; }

        public int DespachanteId { get; set; }

        public StatusPosicionamento Status { get; set; }

        public TipoAgendamento TipoAgendamento { get; set; }

        public List<Conteiner> Conteineres { get; set; } = new List<Conteiner>();

        public List<CargaSolta> ItensCargaSolta { get; set; } = new List<CargaSolta>();

        public List<CargaSolta> Veiculos { get; set; } = new List<CargaSolta>();

        public string NavioViagem => $"{Navio} / {Viagem}";

        public void SetarCliente(int clienteId)
            => ClienteId = clienteId;

        public void AdicionarConteineres(int[] conteineresSelecionados)
        {
            if (conteineresSelecionados != null)
                ConteineresSelecionados = conteineresSelecionados;
        }

        public void AdicionarItensCargaSolta(List<CargaSolta> itensCargaSolta)
        {
            if (itensCargaSolta != null)
                ItensCargaSolta = itensCargaSolta
                    .Where(c => c.Selecionado)
                    .ToList();
        }

        public void AdicionarVeiculos(int[] veiculosSelecionados)
        {
            if (veiculosSelecionados != null)
                VeiculosSelecionados = veiculosSelecionados;
        }

        public override void Validar()
        {
            RuleFor(c => c.MotivoId)
                .GreaterThan(0)
                .WithMessage("Selecione um Motivo");

            RuleFor(c => c.DataPrevista)
                .NotEmpty()
                .WithMessage("Selecione a Data Prevista");

            RuleFor(c => c.ClienteCpfCnpj)
                .NotNull()
                .WithMessage("Informe o CPF/CNPJ do Cliente");

            RuleFor(c => c.NovaViagemId)
                .GreaterThan(0)
                .When(c => c.MotivoId == 29)
                .WithMessage("Informe a Viagem");

            RuleFor(c => c.ConteineresSelecionados)
               .NotNull()
               .When(c => c.TipoAgendamento == TipoAgendamento.Conteiner)
               .WithMessage("Selecione um ou mais Contêineres para Posicionamento");

            RuleFor(c => c.ItensCargaSolta)
               .Cascade(CascadeMode.StopOnFirstFailure)
               .Must(c => c.Any(item => item.Selecionado))
               .When(c => c.TipoAgendamento == TipoAgendamento.CargaSolta)
               .WithMessage("Selecione um ou mais Itens de Carga Solta para Posicionamento")
               .Must(c => !c.Any(item => item.QuantidadeAgendada > item.Quantidade))
               .When(c => c.TipoAgendamento == TipoAgendamento.CargaSolta)
               .WithMessage("A Quantidade informada não deve ultrapassar o Saldo disponível do item");

            RuleFor(c => c.VeiculosSelecionados)
               .NotNull()
               .When(c => c.TipoAgendamento == TipoAgendamento.Veiculos)
               .WithMessage("Selecione um ou mais Chassis para Posicionamento");

            RuleFor(c => c.DespachanteId)
                .GreaterThan(0)
                .WithMessage("Despachante Id não informado");

            ValidationResult = Validate(this);
        }
    }
}