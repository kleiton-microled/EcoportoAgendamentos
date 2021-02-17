using System.ComponentModel.DataAnnotations;

namespace Ecoporto.AgendamentoConteiner.Enums
{
    public enum StatusAgendamento
    {
        [Display(Name = "Pendente")]
        Pendente = 1,
        [Display(Name = "Concluído")]
        Concluido,
             [Display(Name = "teste")]
        teste
    }
}