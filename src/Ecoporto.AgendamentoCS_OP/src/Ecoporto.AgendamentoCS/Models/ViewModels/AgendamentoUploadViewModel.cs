using Ecoporto.AgendamentoCS.Enums;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Ecoporto.AgendamentoCS.Models.ViewModels
{
    public class AgendamentoUploadViewModel
    {
        public int AgendamentoId { get; set; }

        [Display(Name = "Selecione o Arquivo")]
        public string Upload { get; set; }

        public int BookingCsItemId { get; set; }

        public IEnumerable<Upload> Uploads { get; set; } = new List<Upload>();

        [Display(Name = "Documento")]
        public TipoDocumentoUpload TiposDocumentos { get; set; }
    }
}