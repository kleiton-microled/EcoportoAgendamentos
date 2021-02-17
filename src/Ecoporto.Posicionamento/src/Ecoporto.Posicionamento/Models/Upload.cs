using System;

namespace Ecoporto.Posicionamento.Models
{
    public class Upload
    {
        public int PosicionamentoId { get; set; }

        public int Id { get; set; }

        public string Arquivo { get; set; }

        public string Base64 { get; set; }

        public string TipoDocumento { get; set; }

        public string Extensao { get; set; }

        public bool Liberado { get; set; }

        public DateTime DataEnvio { get; set; }
    }
}