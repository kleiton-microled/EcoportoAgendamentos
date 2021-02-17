using Ecoporto.AgendamentoConteiner.Enums;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Ecoporto.AgendamentoConteiner.Models.ViewModels
{
    public class AgendamentoUploadViewModel
    {
        public int? AgendamentoId { get; set; }

        [Display(Name = "Selecione o Arquivo")]
        public string Upload { get; set; }

        public int BookingId { get; set; }

        public string UploadSiglaConteiner { get; set; }

        public IEnumerable<Upload> Uploads { get; set; } = new List<Upload>();

        [Display(Name = "Documento")]
        public TipoDocumentoUpload TiposDocumentos { get; set; }
    }
}