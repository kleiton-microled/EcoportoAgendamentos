using Ecoporto.Posicionamento.Models.DTO;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Ecoporto.Posicionamento.Models.ViewModels
{
    public class AgendamentoCargaSoltaViewModel : AgendamentoViewModel
    {             
        [Display(Name = "Selecione o(s) Item(s) de Carga Solta")]
        public List<CargaSolta> ItensCargaSolta { get; set; } = new List<CargaSolta>();

        public List<CargaSolta> ItensCargaSoltaAgendados { get; set; } = new List<CargaSolta>();
    }
}