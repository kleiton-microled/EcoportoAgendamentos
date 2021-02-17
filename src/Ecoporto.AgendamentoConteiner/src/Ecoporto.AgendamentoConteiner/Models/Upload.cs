using Ecoporto.AgendamentoConteiner.Enums;
using System;

namespace Ecoporto.AgendamentoConteiner.Models
{
    public class Upload : Entidade<Upload>
    {
        public int AgendamentoId { get; set; }

        public int BookingId { get; set; }

        public string Sigla { get; set; }

        public string Arquivo { get; set; }

        public string Base64 { get; set; }

        public string Extensao { get; set; }

        public int TipoDocumentoId { get; set; }

        public TipoDocumentoUpload TipoDocumento { get; set; }

        public bool Liberado { get; set; }

        public bool Sharepoint { get; set; }

        public DateTime DataEnvio { get; set; }

        public override void Validar()
        {
            throw new NotImplementedException();
        }
    }
}