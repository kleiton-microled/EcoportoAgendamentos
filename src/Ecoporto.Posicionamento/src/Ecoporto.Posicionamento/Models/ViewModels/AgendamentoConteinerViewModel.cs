using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Ecoporto.Posicionamento.Models.ViewModels
{
    public class AgendamentoConteinerViewModel : AgendamentoViewModel
    {
        [Display(Name = "Selecione o(s) Contêiner(es)")]
        public List<Conteiner> Conteineres { get; set; } = new List<Conteiner>();

        public List<Conteiner> ConteineresAgendados { get; set; } = new List<Conteiner>();

        public int[] ConteineresSelecionados { get; set; }
    }
}