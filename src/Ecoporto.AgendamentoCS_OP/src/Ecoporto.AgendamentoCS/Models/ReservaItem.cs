using Ecoporto.AgendamentoCS.Enums;
using System.Collections.Generic;

namespace Ecoporto.AgendamentoCS.Models
{
    public class ReservaItem : Entidade<ReservaItem>
    {
        public string Reserva { get; set; }

        public int ViagemId { get; set; }

        public int BookingCsItemId { get; set; }

        public int Item { get; set; }

        public int BookingCsId { get; set; }

        public int Qtde { get; set; }

        public int Saldo { get; set; }

        public int QtdeReserva { get; set; }

        public string Embalagem { get; set; }

        public string TipoCarga { get; set; }

        public decimal PesoUnitario { get; set; }

        public string Marca { get; set; }
        
        public string ContraMarca { get; set; }

        public string Descricao { get; set; }

        public bool Veiculo { get; set; }

        public string Chassis { get; set; }

        public bool PackingList { get; set; }

        public bool ImagemCarga { get; set; }

        public bool DesenhoTecnico { get; set; }

        public string Classificacao { get; set; }

        public ClassificacaoCarga ClassificacaoId { get; set; }

        public bool ExigeNF { get; set; }

        public bool ExigeChassi { get; set; }

        public List<NotaFiscal> NotasFiscais { get; set; } = new List<NotaFiscal>();

        public override string ToString()
        {
            return $"{QtdeReserva} {Embalagem} {Marca} {ContraMarca} {PesoUnitario}Kg";
        }

        public override void Validar()
        {
            throw new System.NotImplementedException();
        }
    }
}