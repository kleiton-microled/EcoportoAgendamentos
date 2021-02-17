using System.ComponentModel.DataAnnotations;

namespace Ecoporto.Posicionamento.Enums
{
    public enum StatusPosicionamento
    {
        [Display(Name = "Pendente")]
        NaoIniciado,
        [Display(Name = "Iniciado")]
        Iniciado,
        [Display(Name = "Encerrado")]
        Encerrado,
        [Display(Name = "Faturado")]
        Faturado,
        [Display(Name = "Cancelado")]
        Cancelado        
    }
}