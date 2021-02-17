namespace Ecoporto.Posicionamento.Models
{
    public class CargaSolta
    {
        public int AgendamentoId { get; set; }

        public string Reserva { get; set; }

        public int BookingCsItemId { get; set; }

        public int BookingCsItemChassi { get; set; }

        public string Embalagem { get; set; }

        public string Marca { get; set; }

        public string Modelo { get; set; }

        public int Quantidade { get; set; }

        public int QuantidadeAgendada { get; set; }

        public string Chassis { get; set; }

        public bool Selecionado { get; set; }
    }
}