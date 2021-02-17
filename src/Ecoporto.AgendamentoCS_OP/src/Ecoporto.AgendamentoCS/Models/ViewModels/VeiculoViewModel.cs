using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Ecoporto.AgendamentoCS.Models.ViewModels
{
    public class VeiculoViewModel
    {
        public VeiculoViewModel()
        {
            TiposVeiculos = new List<TipoVeiculo>();
        }

        public int Id { get; set; }

        public int TransportadoraId { get; set; }

        [Display(Name = "Tipo Veículo")]
        public int TipoCaminhaoId { get; set; }

        public int TipoCaminhaoDescricao { get; set; }

        public string Cavalo { get; set; }

        public string Carreta { get; set; }

        public string Chassi { get; set; }

        public string Renavam { get; set; }

        public decimal? Tara { get; set; }

        public string Modelo { get; set; }

        public string Cor { get; set; }

        [Display(Name = "Última Alteração")]
        public string UltimaAlteracao { get; set; }

        public List<TipoVeiculo> TiposVeiculos { get; set; }
    }
}