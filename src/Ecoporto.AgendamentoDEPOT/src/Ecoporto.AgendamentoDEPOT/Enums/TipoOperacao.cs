using System.ComponentModel.DataAnnotations;

namespace Ecoporto.AgendamentoDEPOT.Enums
{
    public enum TipoOperacao
    {
        [Display(Name = "Exportação")]
        Exportacao = 1,
        [Display(Name = "Importação")]
        Importacao
    }
}