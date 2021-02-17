using Ecoporto.AgendamentoCS.Extensions;
using Ecoporto.AgendamentoCS.Helpers;
using FluentValidation;
using System;
using System.Collections.Generic;

namespace Ecoporto.AgendamentoCS.Models
{
    public class Agendamento : Entidade<Agendamento>
    {
        public Agendamento()
        {

        }

        public Agendamento(
            int motoristaId, 
            int veiculoId,
            string cte,
            bool cegonha,
            bool desembaracada,
            int transportadoraId, 
            string reserva, 
            int bookingCsId,
            int periodoId, 
            string emailRegistro,
            int usuarioId)
        {
            MotoristaId = motoristaId;
            VeiculoId = veiculoId;
            CTE = cte;
            Cegonha = cegonha;
            Desembaracada = desembaracada;
            TransportadoraId = transportadoraId;
            Reserva = reserva;
            BookingCsId = bookingCsId;
            PeriodoId = periodoId;
            EmailRegistro = emailRegistro;
            UsuarioId = usuarioId;
        }

        public int MotoristaId { get; set; }

        public int VeiculoId { get; set; }

        public int TransportadoraId { get; set; }

        public int PeriodoId { get; set; }

        public bool Cegonha { get; set; }

        public bool Desembaracada { get; set; }

        public int BookingCsId { get; set; }

        public string Reserva { get; set; }

        public int UsuarioId { get; set; }

        public string Protocolo { get; set; }

        public bool Impresso { get; set; }

        public string CTE { get; set; }

        public string EmailRegistro { get; set; }

        public DateTime DataCadastro { get; set; }

        public DateTime? DataEntrada { get; set; }

        public List<ReservaItem> Itens { get; set; } = new List<ReservaItem>();

        public void AdicionarItens(List<ReservaItem> itens)
        {
            if (itens != null)
                Itens.AddRange(itens);
        }

        public void Alterar(Agendamento agendamento)
        {
            MotoristaId = agendamento.MotoristaId;
            VeiculoId = agendamento.VeiculoId;
            CTE = agendamento.CTE;
            PeriodoId = agendamento.PeriodoId;
            Cegonha = agendamento.Cegonha;
            Desembaracada = agendamento.Desembaracada;
            EmailRegistro = agendamento.EmailRegistro;
        }

        public override void Validar()
        {
            RuleFor(c => c.MotoristaId)
                .GreaterThan(0)
                .WithMessage("Motorista não informado");

            RuleFor(c => c.VeiculoId)
                .GreaterThan(0)
                .WithMessage("Veículo não informado");

            RuleFor(c => c.CTE)
                .NotEmpty()
                .WithMessage("O campo CTE é obrigatório");

            RuleFor(c => c.TransportadoraId)
                .GreaterThan(0)
                .WithMessage("Transportadora não informada");

            RuleFor(c => c.Reserva)
                .NotEmpty()
                .WithMessage("Reserva não informada");

            RuleFor(c => c.UsuarioId)
                .GreaterThan(0)
                .WithMessage("Usuário não informado");

            RuleFor(c => c.PeriodoId)
                .GreaterThan(0)
                .WithMessage("Período não informado");

            var notificacoes = ValidationResult.Errors;

            ValidationResult = Validate(this);

            AdicionarNotificacoes(notificacoes);
        }
    }
}