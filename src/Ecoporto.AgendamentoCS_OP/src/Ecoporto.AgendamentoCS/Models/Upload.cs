using System;

namespace Ecoporto.AgendamentoCS.Models
{
    public class Upload : Entidade<Upload>
    {
        public int AgendamentoId { get; set; }

        public int BookingCsItemId { get; set; }

        public int Id { get; set; }

        public string Arquivo { get; set; }

        public string Base64 { get; set; }

        public int TipoDocumentoId { get; set; }

        public string TipoDocumento { get; set; }

        public string Extensao { get; set; }

        public bool Liberado { get; set; }

        public bool Sharepoint { get; set; }

        public DateTime DataEnvio { get; set; }

        public override void Validar()
        {
            throw new NotImplementedException();
        }
    }
}