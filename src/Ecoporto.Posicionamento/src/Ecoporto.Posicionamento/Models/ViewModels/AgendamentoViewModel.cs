using Ecoporto.Posicionamento.Models.DTO;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Ecoporto.Posicionamento.Models.ViewModels
{
    public class AgendamentoViewModel
    {
        public int Id { get; set; }

        public string Reserva { get; set; }

        public string Navio { get; set; }

        public string Exportador { get; set; }

        public string Viagem { get; set; }

        public int NovaViagemId { get; set; }

        [Display(Name = "Nova Viagem")]
        public string NovaViagemDescricao { get; set; }

        [Display(Name = "Porto Destino")]
        public string Porto { get; set; }

        [Display(Name = "Motivo")]
        public int MotivoId { get; set; }

        [Display(Name = "Motivo")]
        public string MotivoDescricao { get; set; }

        [Display(Name = "Data Prevista")]
        public string DataPrevista { get; set; }

        [Display(Name = "CPF/CNPJ")]
        public string CpfCnpj { get; set; }

        [Display(Name = "Razão Social")]
        public string RazaoSocial { get; set; }

        public int ProtocoloUnificado { get; set; }

        [Display(Name = "Data Prevista")]
        public List<Periodo> Periodos { get; set; }

        public List<Motivo> Motivos { get; set; }       

        public IEnumerable<AgendamentoDTO> Agendados { get; set; } = new List<AgendamentoDTO>();

        public IEnumerable<AgendamentoDTO> Cancelados { get; set; } = new List<AgendamentoDTO>();

        [Display(Name = "Navio / Viagem")]
        public string NavioViagem => $"{Navio} / {Viagem}";
    }
}