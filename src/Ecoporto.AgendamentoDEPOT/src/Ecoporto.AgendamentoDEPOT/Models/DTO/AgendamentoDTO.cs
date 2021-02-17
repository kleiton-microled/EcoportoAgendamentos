using Ecoporto.AgendamentoDEPOT.Enums;
using System;

namespace Ecoporto.AgendamentoDEPOT.Models.DTO
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

        public int RecintoTRA { get; set; }

        public string RecintoDEPOT { get; set; }

        public int Quantidade { get; set; }

        public string RecintoDescricao { get; set; }

        public string Protocolo { get; set; }

        public string AnoProtocolo { get; set; }

        public int UsuarioId { get; set; }

        public bool Impresso { get; set; }

        public DateTime DataCriacao { get; set; }

        public DateTime? DataEntrada { get; set; }

        public TipoAgendamento TipoAgendamento { get; set; }

        public bool IMO { get; set; }

        public bool Excesso { get; set; }

        public bool PossuiEntrada => DataEntrada != null;
    }
}