using System.Collections.Generic;

namespace Ecoporto.AgendamentoCS.Models
{
    public class NotaFiscal : Entidade<NotaFiscal>
    {
        public NotaFiscal()
        {

        }

        public NotaFiscal(string danfe, string numeroNF, string serieNF)
        {
            Danfe = danfe;
            NumeroNF = numeroNF;
            SerieNF = serieNF;
        }

        public string Danfe { get; set; }

        public string NumeroNF { get; set; }

        public string SerieNF { get; set; }

        public string CFOP { get; set; }

        public int BookingCsItemId { get; set; }

        public string Reserva { get; set; }

        public override void Validar()
        {
            throw new System.NotImplementedException();
        }
    }
}