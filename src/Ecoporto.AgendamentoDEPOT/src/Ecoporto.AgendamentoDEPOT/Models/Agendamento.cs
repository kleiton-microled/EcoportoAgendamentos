using Ecoporto.AgendamentoDEPOT.Enums;
using FluentValidation;
using System;

namespace Ecoporto.AgendamentoDEPOT.Models
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
            int periodoId,
            int recintoTRA,
            string recintoDEPOT,
            int quantidade,
            bool imo,
            bool excesso,
            TipoAgendamento tipoAgendamento)
        {
            MotoristaId = motoristaId;
            VeiculoId = veiculoId;
            CTE = cte;
            PeriodoId = periodoId;
            RecintoTRA = recintoTRA;
            RecintoDEPOT = recintoDEPOT;
            Quantidade = quantidade;
            IMO = imo;
            Excesso = excesso;
            TipoAgendamento = tipoAgendamento;
        }

        public Agendamento(
            int motoristaId,
            int veiculoId,
            string cte,
            int transportadoraId,
            int periodoId,
            int recintoTRA,
            string recintoDEPOT,
            TipoOperacao tipoOperacao,
            TipoAgendamento tipoAgendamento,
            int quantidade,
            bool imo,
            bool excesso,
            int usuarioId)
        {
            MotoristaId = motoristaId;
            VeiculoId = veiculoId;
            CTE = cte;
            TransportadoraId = transportadoraId;
            PeriodoId = periodoId;
            RecintoTRA = recintoTRA;
            RecintoDEPOT = recintoDEPOT;
            TipoOperacao = tipoOperacao;
            TipoAgendamento = tipoAgendamento;
            Quantidade = quantidade;
            IMO = imo;
            Excesso = excesso;
            UsuarioId = usuarioId;
        }

        public int MotoristaId { get; set; }

        public int VeiculoId { get; set; }

        public int TransportadoraId { get; set; }

        public int PeriodoId { get; set; }

        public int UsuarioId { get; set; }

        public string Protocolo { get; set; }

        public bool Impresso { get; set; }

        public string CTE { get; set; }

        public TipoOperacao TipoOperacao { get; set; }

        public TipoAgendamento TipoAgendamento { get; set; }

        public int Quantidade { get; set; }

        public bool IMO { get; set; }

        public bool Excesso { get; set; }

        public int RecintoTRA { get; set; }

        public string RecintoDEPOT { get; set; }

        public DateTime DataCadastro { get; set; }

        public DateTime? DataEntrada { get; set; }

        public void Alterar(Agendamento agendamento)
        {
            MotoristaId = agendamento.MotoristaId;
            VeiculoId = agendamento.VeiculoId;
            CTE = agendamento.CTE;
            PeriodoId = agendamento.PeriodoId;
            RecintoTRA = agendamento.RecintoTRA;
            RecintoDEPOT = agendamento.RecintoDEPOT;
            Quantidade = agendamento.Quantidade;
            IMO = agendamento.IMO;
            Excesso = agendamento.Excesso;
            TipoAgendamento = agendamento.TipoAgendamento;
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

            RuleFor(c => c.UsuarioId)
                .GreaterThan(0)
                .WithMessage("Usuário não informado");

            RuleFor(c => c.PeriodoId)
                .GreaterThan(0)
                .WithMessage("Período não informado");

            RuleFor(c => c.RecintoTRA)
                .GreaterThan(0)
                .When(c => c.TipoAgendamento == TipoAgendamento.TRA)
                .WithMessage("Recinto não informado");

            RuleFor(c => c.RecintoDEPOT)
                .NotEmpty()
                .When(c => c.TipoAgendamento == TipoAgendamento.DEPOT)
                .WithMessage("Recinto não informado");

            RuleFor(c => c.Quantidade)
                .GreaterThan(0)
                .WithMessage("A Quantidade deve ser maior que 0")
                .LessThanOrEqualTo(2)
                .WithMessage("A Quantidade não pode ser maior que 2");

            ValidationResult = Validate(this);
        }
    }
}