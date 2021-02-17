using System.ComponentModel.DataAnnotations;

namespace Ecoporto.AgendamentoConteiner.Enums
{
    public enum TipoDocumentoUpload
    {
        [Display(Name = "Packing List")]
        PACKING_LIST = 1,
        [Display(Name = "Autorização Bagagem")]
        AUTORIZACAO_BAGAGEM
    }
}