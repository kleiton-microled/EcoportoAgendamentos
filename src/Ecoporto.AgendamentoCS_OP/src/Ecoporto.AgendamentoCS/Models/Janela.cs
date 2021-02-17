using System;

namespace Ecoporto.AgendamentoCS.Models
{
    public class Janela
    {
        public int Id { get; set; }

        public DateTime PeriodoInicial { get; set; }

        public DateTime PeriodoFinal { get; set; }

        public int Saldo { get; set; }

        public bool PeriodoAgendado { get; set; }
    }
}