using System.ComponentModel.DataAnnotations;

namespace Ecoporto.AgendamentoDEPOT.Enums
{
    public enum TipoAgendamento
    {
        [Display(Name = "TRA")]
        TRA = 1,
        [Display(Name = "DEPOT")]
        DEPOT
    }
}