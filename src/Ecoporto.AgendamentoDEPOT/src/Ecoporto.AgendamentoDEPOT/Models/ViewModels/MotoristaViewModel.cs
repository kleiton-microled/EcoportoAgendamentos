using System;
using System.ComponentModel.DataAnnotations;

namespace Ecoporto.AgendamentoDEPOT.Models.ViewModels
{
    public class MotoristaViewModel
    {
        public int Id { get; set; }

        public int TransportadoraId { get; set; }

        public string Nome { get; set; }

        public string CNH { get; set; }

        [Display(Name = "Validade CNH")]
        public DateTime? ValidadeCNH { get; set; }

        public string RG { get; set; }

        public string CPF { get; set; }

        public string Celular { get; set; }

        public string Nextel { get; set; }

        public string MOP { get; set; }

        [Display(Name = "Última Alteração")]
        public string UltimaAlteracao { get; set; }
    }
}