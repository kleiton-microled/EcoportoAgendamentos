using System;

namespace Ecoporto.AgendamentoCS.Models
{
    public class Reserva
    {
        public int Id { get; set; }

        public string Descricao { get; set; }

        public string Navio { get; set; }

        public string Viagem { get; set; }

        public string Exportador { get; set; }

        public int ViagemId { get; set; }

        public DateTime Abertura { get; set; }

        public DateTime Fechamento { get; set; }

        public bool Desembaracada { get; set; }

        public int BookingCsId { get; set; }

        public int LimiteHoraLateArrival { get; set; }
    }
}