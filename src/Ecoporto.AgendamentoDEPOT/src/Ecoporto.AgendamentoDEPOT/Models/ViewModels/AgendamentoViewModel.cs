using Ecoporto.AgendamentoDEPOT.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Ecoporto.AgendamentoDEPOT.Models.ViewModels
{
    public class AgendamentoViewModel
    {
        public AgendamentoViewModel()
        {
            Motoristas = new List<Motorista>();
            Veiculos = new List<Veiculo>();
            Periodos = new List<Janela>();
            RecintosTRA = new List<Recinto>();
            RecintosDEPOT = new List<Recinto>();
        }

        public int Id { get; set; }

        public TipoOperacao TipoOperacao { get; set; }

        public TipoAgendamento TipoAgendamento { get; set; }

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

        public int UsuarioId { get; set; }

        [Display(Name = "Selecione o Recinto")]
        public int RecintoSelecionadoTRA { get; set; }

        [Display(Name = "Selecione o Recinto")]
        public string RecintoSelecionadoDEPOT { get; set; }

        public string RecintoDescricao { get; set; }

        public bool IMO { get; set; }

        public bool Excesso { get; set; }

        public int Total { get; set; }
        
        public int Agendados { get; set; }

        public int Disponiveis { get; set; }

        public int Quantidade { get; set; }

        public string Protocolo { get; set; }     

        public bool Impresso { get; set; }

        public bool Concluido { get; set; }

        [Display(Name = "CTE")]
        public string CTE { get; set; }

        [Display(Name = "Email para contato (Para informar mais que um email, separe por vírgula)")]
        public string EmailRegistro { get; set; }

        public bool PossuiEntrada { get; set; }        

        public Empresa Empresa { get; set; }

        public DateTime DataCadastro { get; set; }

        public Motorista DetalhesMotorista { get; set; }

        public Veiculo DetalhesVeiculo { get; set; }

        public List<Motorista> Motoristas { get; set; }

        public List<Veiculo> Veiculos { get; set; }

        public List<Janela> Periodos { get; set; }

        public List<Recinto> RecintosTRA { get; set; }

        public List<Recinto> RecintosDEPOT { get; set; }
    }
}