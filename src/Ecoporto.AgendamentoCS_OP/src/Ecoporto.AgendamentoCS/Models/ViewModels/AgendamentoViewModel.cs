using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Ecoporto.AgendamentoCS.Models.ViewModels
{
    public class AgendamentoViewModel
    {
        public AgendamentoViewModel()
        {
            Motoristas = new List<Motorista>();
            Veiculos = new List<Veiculo>();
            Reservas = new List<Reserva>();
            ItensReserva = new List<ReservaItem>();
            Periodos = new List<Janela>();
        }

        public int Id { get; set; }

        [Display(Name = "Selecione o Motorista:")]
        public int MotoristaId { get; set; }

        public string MotoristaDescricao { get; set; }

        public string MotoristaCPF { get; set; }

        public string MotoristaCNH { get; set; }

        [Display(Name = "Selecione o Veículo:")]
        public int VeiculoId { get; set; }

        public string VeiculoDescricao { get; set; }

        public int TransportadoraId { get; set; }

        public string TransportadoraDescricao { get; set; }

        public string TransportadoraDocumento { get; set; }

        [Display(Name = "Período")]
        public int PeriodoId { get; set; }

        public string PeriodoDescricao { get; set; }

        [Display(Name = "Reserva")]
        public int BookingCsId { get; set; }

        [Display(Name = "Item Reserva")]
        public int BookingCsItemId { get; set; }

        public int UsuarioId { get; set; }

        public string Protocolo { get; set; }

        [Display(Name = "Navio / Viagem")]
        public string Navio { get; set; }

        public string Viagem { get; set; }

        public int ViagemId { get; set; }

        public string Abertura { get; set; }

        public string Reserva { get; set; }

        public string Exportador { get; set; }

        public string Fechamento { get; set; }

        [Display(Name = "Desembaraçado?")]
        public bool Desembaracada { get; set; }

        public bool Impresso { get; set; }

        public bool Cegonha { get; set; }

        public bool Concluido { get; set; }

        public bool CargaProjeto { get; set; }

        public int ClassificacaoId { get; set; }

        [Display(Name = "Item Reserva")]
        public int ItemReservaId { get; set; }

        [Display(Name = "Qtde")]
        public int QuantidadeItemReserva { get; set; }

        [Display(Name = "Chassis (Para informar mais que um Chassis, separe por vírgula)")]
        public string Chassis { get; set; }

        [Display(Name = "Danfe")]
        public string Danfe { get; set; }
        public string XmlDanfe { get; set; }


        [Display(Name = "CFOP")]
        public string CFOP { get; set; }

        [Display(Name = "CTE")]
        public string CTE { get; set; }

        [Display(Name = "Email para contato (Para informar mais que um email, separe por vírgula)")]
        public string EmailRegistro { get; set; }

        public bool PossuiEntrada { get; set; }

        public Empresa Empresa { get; set; }

        public DateTime DataCadastro { get; set; }

        public Motorista DetalhesMotorista { get; set; }

        public Veiculo DetalhesVeiculo { get; set; }

        public List<CFOP> CFOPS { get; set; }

        public List<Motorista> Motoristas { get; set; }

        public List<Veiculo> Veiculos { get; set; }

        public List<Reserva> Reservas { get; set; }

        public List<Janela> Periodos { get; set; }

        public List<ReservaItem> ItensReserva { get; set; }

        public List<NotaFiscal> NotasFiscais { get; set; } = new List<NotaFiscal>();

        public AgendamentoUploadViewModel AgendamentoUploadViewModel { get; set; } = new AgendamentoUploadViewModel();
    }
}