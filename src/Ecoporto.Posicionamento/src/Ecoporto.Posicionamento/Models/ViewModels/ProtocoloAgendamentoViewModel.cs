using Ecoporto.Posicionamento.Models.DTO;
using System;
using System.Collections.Generic;

namespace Ecoporto.Posicionamento.Models.ViewModels
{
    public class ProtocoloAgendamentoViewModel
    {
        public int Id { get; set; }

        public int ProtocoloUnificado { get; set; }

        public string Reserva { get; set; }

        public string Navio { get; set; }

        public string Exportador { get; set; }

        public string ExportadorCnpj { get; set; }

        public string Line { get; set; }

        public string Viagem { get; set; }

        public string Porto { get; set; }

        public DateTime DataPrevista { get; set; }

        public DateTime DataCancelamento { get; set; }

        public string DespachanteCnpj { get; set; }

        public string DespachanteRazaoSocial { get; set; }

        public string Cliente { get; set; }

        public string ClienteCpfCnpj { get; set; }

        public string MotivoCancelamento { get; set; }

        public List<Conteiner> Conteineres { get; set; } = new List<Conteiner>();

        public List<CargaSolta> ItensCargaSolta { get; set; } = new List<CargaSolta>();

        public List<VeiculoAgendadoDTO> Veiculos { get; set; } = new List<VeiculoAgendadoDTO>();

        public string MotivoDescricao { get; set; }
    }
}