using Ecoporto.AgendamentoConteiner.Enums;
using System;
using System.Collections.Generic;

namespace Ecoporto.AgendamentoConteiner.Models
{
    public class Reserva : Entidade<Reserva>
    {
        public string Descricao { get; set; }

        public string Navio { get; set; }

        public string Viagem { get; set; }

        public string Exportador { get; set; }

        public int ViagemId { get; set; }

        public DateTime Abertura { get; set; }

        public DateTime Fechamento { get; set; }

        public int BookingId { get; set; }

        public int DeltaHoras { get; set; }

        public bool LateArrival { get; set; }

        public TipoAgendamentoConteiner EF { get; set; }

        public bool Bagagem { get; set; }

        public bool ShipperOwner { get; set; }

        public int Item { get; set; }

        public string POD { get; set; }

        public string Qtde { get; set; }

        public string Tipo { get; set; }

        public int Tamanho { get; set; }

        public int Comprimento { get; set; }

        public int Altura { get; set; }

        public int LateralDireita { get; set; }

        public int LateralEsquerda { get; set; }

        public int ISO { get; set; }

        public string Remarks { get; set; }

        public string IMO1 { get; set; }

        public string IMO2 { get; set; }

        public string IMO3 { get; set; }

        public string IMO4 { get; set; }

        public string ONU1 { get; set; }

        public string ONU2 { get; set; }

        public string ONU3 { get; set; }

        public string ONU4 { get; set; }

        public string Hash { get; set; }

        public int Saldo { get; set; }

        public bool ReeferDesligado { get; set; }

        public decimal Temp { get; set; }

        public string Escala { get; set; }

        public string Umidade { get; set; }

        public string Ventilacao { get; set; }

        public bool ReeferLigado => !ReeferDesligado;

        public bool IsIMO => !string.IsNullOrEmpty(IMO1 + IMO2 + IMO3 + IMO4);

        public bool IsUNO => !string.IsNullOrEmpty(ONU1 + ONU2 + ONU3 + ONU4);

        public List<Conteiner> Conteineres { get; set; } = new List<Conteiner>();

        public override void Validar()
        {
            throw new NotImplementedException();
        }
    }
}