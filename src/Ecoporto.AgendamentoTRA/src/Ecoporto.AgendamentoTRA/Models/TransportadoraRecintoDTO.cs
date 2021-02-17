using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Ecoporto.AgendamentoTRA.Models
{
    public class TransportadoraRecintoDTO
    {
        public int Id { get; set; }

        public TipoVinculo TipoVinculo { get; set; }

        public int TransportadoraId { get; set; }

        public int RecintoId { get; set; }

        public string CNPJ { get; set; }

        public string RazaoSocial { get; set; }

        public bool Ativo { get; set; }

        [Display(Name = "Selecione:")]
        public int TransportadoraSelecionadaId { get; set; }

        public int TotalLinhas { get; set; }

        public List<TransportadoraRecintoDTO> TransportadorasVinculadas { get; set; } = new List<TransportadoraRecintoDTO>();

        public List<TransportadoraRecintoDTO> TransportadorasDisponiveis { get; set; } = new List<TransportadoraRecintoDTO>();
    }
}