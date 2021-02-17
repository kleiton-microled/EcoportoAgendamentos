using System.ComponentModel.DataAnnotations;

namespace Ecoporto.AgendamentoConteiner.Enums
{
    public enum TipoOperacao
    {
        [Display(Name = "Exportação")]
        Exportacao = 1,
        [Display(Name = "Importação")]
        Importacao
    }
}