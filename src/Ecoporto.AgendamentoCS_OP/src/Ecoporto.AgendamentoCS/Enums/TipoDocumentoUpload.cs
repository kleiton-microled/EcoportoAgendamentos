using System.ComponentModel.DataAnnotations;

namespace Ecoporto.AgendamentoCS.Enums
{
    public enum TipoDocumentoUpload
    {
        [Display(Name = "Packing List")]
        PACKING_LIST = 1,
        [Display(Name = "Desenho Técnico")]
        DESENHO_TECNICO,
        [Display(Name = "Imagem")]
        IMAGEM
    }
}