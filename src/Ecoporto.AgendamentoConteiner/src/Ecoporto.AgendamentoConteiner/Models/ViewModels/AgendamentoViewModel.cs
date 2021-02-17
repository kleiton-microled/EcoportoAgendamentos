using Ecoporto.AgendamentoConteiner.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Ecoporto.AgendamentoConteiner.Models.ViewModels
{
    public class AgendamentoViewModel
    {
        public AgendamentoViewModel()
        {
            Motoristas = new List<Motorista>();
            Veiculos = new List<Veiculo>();
            Reservas = new List<Reserva>();
            ItensReserva = new List<Reserva>();
            Periodos = new List<Janela>();
        }

        public int Id { get; set; }

        public TipoOperacao TipoOperacao { get; set; }

        public TipoAgendamentoConteiner TipoAgendamentoConteiner { get; set; }

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

        public int BookingId { get; set; }

        public int Tamanho { get; set; }

        public int UsuarioId { get; set; }

        public string Protocolo { get; set; }

        public string Navio { get; set; }

        public string Viagem { get; set; }

        public int ViagemId { get; set; }

        public string Abertura { get; set; }

        public string Reserva { get; set; }

        public string Exportador { get; set; }

        public string Fechamento { get; set; }

        public bool Impresso { get; set; }

        public bool Cegonha { get; set; }

        public bool Concluido { get; set; }

        [Display(Name = "Danfe")]
        public string Danfe { get; set; }

        [Display(Name = "CFOP")]
        public string CFOP { get; set; }

        [Display(Name = "CTE")]
        public string CTE { get; set; }

        [Display(Name = "Email para contato (Para informar mais que um email, separe por vírgula)")]
        public string EmailRegistro { get; set; }

        public bool PossuiEntrada { get; set; }

        public string Sigla { get; set; }

        public string Volumes { get; set; }

        public bool Bagagem { get; set; }

        public bool ReeferLigado { get; set; }

        [Display(Name = "Tipo")]
        public string TipoBasico { get; set; }

        public string Tara { get; set; }

        [Display(Name = "Peso Bruto")]
        public string Bruto { get; set; }

        public string ONU { get; set; }

        public string IMO { get; set; }

        public string Temp { get; set; }

        public string Escala { get; set; }

        public string Umidade { get; set; }

        [Display(Name = "Ventilação")]
        public string Ventilacao { get; set; }

        [Display(Name = "Comp")]
        public string Comprimento { get; set; }

        public string Altura { get; set; }

        [Display(Name = "Lat. Dir")]
        public string LateralDireita { get; set; }

        [Display(Name = "Lat. Esq")]
        public string LateralEsquerda { get; set; }

        [Display(Name = "Lacre Armador 1")]
        public string LacreArmador1 { get; set; }

        [Display(Name = "Lacre Armador 2")]
        public string LacreArmador2 { get; set; }

        [Display(Name = "Lacre 1")]
        public string OutrosLacres1 { get; set; }

        [Display(Name = "Lacre 2")]
        public string OutrosLacres2 { get; set; }

        [Display(Name = "Lacre 3")]
        public string OutrosLacres3 { get; set; }

        [Display(Name = "Lacre 4")]
        public string OutrosLacres4 { get; set; }

        [Display(Name = "Lacre Exportador")]
        public string LacreExportador { get; set; }

        [Display(Name = "Lacre SIF")]
        public string LacreSIF { get; set; }

        [Display(Name = "Navio / Viagem")]
        public string NavioViagem => $"{Navio}/{Viagem}";

        [Display(Name = "DAT")]
        public string DAT { get; set; }

        [Display(Name = "Observações")]
        public string Observacoes { get; set; }

        [Display(Name = "DUE Desembaraçada?")]
        public bool DueDesembaracada { get; set; }

        public Empresa Empresa { get; set; }

        public DateTime DataCadastro { get; set; }

        public Motorista DetalhesMotorista { get; set; }

        public Veiculo DetalhesVeiculo { get; set; }

        public List<CFOP> CFOPS { get; set; }

        public List<TipoConteiner> TiposConteiner { get; set; }

        public List<Motorista> Motoristas { get; set; }

        public List<Veiculo> Veiculos { get; set; }

        public List<Reserva> Reservas { get; set; }

        public List<Janela> Periodos { get; set; }

        public List<Reserva> ItensReserva { get; set; }

        public List<NotaFiscal> NotasFiscais { get; set; } = new List<NotaFiscal>();

        public List<Conteiner> Conteineres { get; set; } = new List<Conteiner>();

        public AgendamentoUploadViewModel AgendamentoUploadViewModel { get; set; } = new AgendamentoUploadViewModel();
    }
}