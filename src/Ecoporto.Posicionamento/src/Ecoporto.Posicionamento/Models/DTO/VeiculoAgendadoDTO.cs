using Ecoporto.Posicionamento.Enums;

namespace Ecoporto.Posicionamento.Models.DTO
{
    public class VeiculoAgendadoDTO
    {
        public int AgendamentoId { get; set; }
        
        public string Marca { get; set; }

        public string Modelo { get; set; }

        public string Embalagem { get; set; }

        public string Chassis { get; set; }

        public StatusPosicionamento Status { get; set; }
    }
}