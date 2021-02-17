using Ecoporto.Posicionamento.Enums;
using System;

namespace Ecoporto.Posicionamento.Models.DTO
{
    public class AgendamentoDTO
    {
        public int Id { get; set; }

        public int Cntr { get; set; }

        public string Sigla { get; set; }

        public string Tipo { get; set; }

        public string TipoDocumento { get; set; }

        public string NumeroDocumento { get; set; }

        public int Tamanho { get; set; }

        public int Quantidade { get; set; }

        public int QuantidadeAgendada { get; set; }

        public string Embalagem { get; set; }

        public string Marca { get; set; }

        public string Modelo { get; set; }

        public int Patio { get; set; }

        public string Viagem { get; set; }

        public DateTime DataEntrada { get; set; }

        public DateTime DataAtualiza { get; set; }

        public DateTime DataCancelamento { get; set; }

        public bool Cancelado { get; set; }

        public string Motivo { get; set; }

        public string Protocolo { get; set; }

        public string Importador { get; set; }

        public string Despachante { get; set; }

        public string Transportadora { get; set; }

        public int TransportadoraId { get; set; }

        public string MotivoCancelamento { get; set; }

        public DateTime DataPrevista { get; set; }

        public StatusPosicionamento Status { get; set; }

        public string StatusDescricao { get; set; }

        public string DataCadastro { get; set; }

        public string Email { get; set; }

        public string Reserva { get; set; }

        public string Line { get; set; }

        public string Exportador { get; set; }

        public string Chassis { get; set; }

        public bool Liberado { get; set; }

        public bool Recusado { get; set; }
    }
}