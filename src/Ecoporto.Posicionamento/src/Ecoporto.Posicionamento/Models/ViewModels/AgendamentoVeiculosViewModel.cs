using Ecoporto.Posicionamento.Models.DTO;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Ecoporto.Posicionamento.Models.ViewModels
{
    public class AgendamentoVeiculosViewModel : AgendamentoViewModel
    {        
        [Display(Name = "Selecione o(s) Chassi(s)")]
        public List<CargaSolta> Veiculos { get; set; } = new List<CargaSolta>();

        public List<VeiculoAgendadoDTO> VeiculosAgendados { get; set; } = new List<VeiculoAgendadoDTO>();

        public int[] VeiculosSelecionados { get; set; }
    }
}