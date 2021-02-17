using System.ComponentModel.DataAnnotations;

namespace Ecoporto.Portal.Enums
{
    public enum Perfil
    {
        [Display(Name = "Transportadora")]
        Transportadora = 2,
        [Display(Name = "Despachante")]
        Despachante = 3
    }
}