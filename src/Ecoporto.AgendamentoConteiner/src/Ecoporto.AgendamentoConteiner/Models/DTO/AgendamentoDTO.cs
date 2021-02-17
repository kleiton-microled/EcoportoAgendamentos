using Ecoporto.AgendamentoConteiner.Enums;
using System;

namespace Ecoporto.AgendamentoConteiner.Models.DTO
{
    public class AgendamentoDTO
    {
        public int Id { get; set; }

        public TipoOperacao TipoOperacao { get; set; }

        public int MotoristaId { get; set; }

        public string MotoristaDescricao { get; set; }

        public string MotoristaCPF { get; set; }

        public string MotoristaCNH { get; set; }

        public int VeiculoId { get; set; }

        public string CTE { get; set; }

        public string VeiculoDescricao { get; set; }

        public int TransportadoraId { get; set; }

        public string TransportadoraDescricao { get; set; }

        public string TransportadoraDocumento { get; set; }

        public int PeriodoId { get; set; }

        public int BookingCsId { get; set; }

        public string PeriodoDescricao { get; set; }

        public string Reserva { get; set; }

        public string Protocolo { get; set; }

        public string AnoProtocolo { get; set; }

        public string Navio { get; set; }

        public string Viagem { get; set; }

        public string Abertura { get; set; }

        public string Fechamento { get; set; }

        public int UsuarioId { get; set; }

        public bool Impresso { get; set; }

        public bool Cegonha { get; set; }

        public bool Desembaracada { get; set; }

        public string EmailRegistro { get; set; }

        public string Conteineres { get; set; }

        public DateTime DataCriacao { get; set; }

        public DateTime? DataEntrada { get; set; }

        public StatusAgendamento StatusAgendamento { get; set; }

        public TipoAgendamentoConteiner TipoAgendamentoConteiner { get; set; }

        public bool PossuiEntrada => DataEntrada != null;
    }
}