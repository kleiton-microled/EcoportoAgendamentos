namespace Ecoporto.AgendamentoConteiner.Models
{
    public class NotaFiscal : Entidade<NotaFiscal>
    {
        public NotaFiscal()
        {

        }

        public NotaFiscal(string danfe, string numeroNF, string serieNF, string xmlDanfeCompleta)
        {
            Danfe = danfe;
            NumeroNF = numeroNF;
            SerieNF = serieNF;
            XmlDanfeCompleta = xmlDanfeCompleta;
        }

        public int? AgendamentoId { get; set; }

        public string Danfe { get; set; }
        public string XmlDanfeCompleta { get; set; }

        public string NumeroNF { get; set; }

        public string SerieNF { get; set; }
        public string Emissor { get; set; }
        public string DataEmissao { get; set; }
        public int Quantidade { get; set; }
        public string Valor { get; set; }
        public string Peso { get; set; }
        public string MetroCubico { get; set; }
        public string CFOP { get; set; }

        public int BookingId { get; set; }

        public string Reserva { get; set; }

        public int ConteinerId { get; set; }

        public string SiglaConteiner { get; set; }        

        public override void Validar()
        {
            throw new System.NotImplementedException();
        }
    }
}