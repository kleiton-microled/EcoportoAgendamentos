using System.ComponentModel.DataAnnotations;

namespace Ecoporto.AgendamentoConteiner.Enums
{
    public enum TipoAgendamentoConteiner
    {
        [Display(Name = "E")]
        Vazio = 1,
        [Display(Name = "F")]
        Cheio
    }
}