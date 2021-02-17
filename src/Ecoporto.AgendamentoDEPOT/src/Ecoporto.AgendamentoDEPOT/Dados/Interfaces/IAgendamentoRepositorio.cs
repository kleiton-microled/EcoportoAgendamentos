using Ecoporto.AgendamentoDEPOT.Enums;
using Ecoporto.AgendamentoDEPOT.Models;
using Ecoporto.AgendamentoDEPOT.Models.DTO;
using System.Collections.Generic;

namespace Ecoporto.AgendamentoDEPOT.Dados.Interfaces
{
    public interface IAgendamentoRepositorio
    {
        int CadastrarAgendamentoTRA(Agendamento agendamento);
        int CadastrarAgendamentoDEPOT(Agendamento agendamento);
        void AtualizarAgendamentoTRA(Agendamento agendamento);
        void AtualizarAgendamentoDEPOT(Agendamento agendamento);
        void Excluir(int id, TipoAgendamento tipoAgendamento);
        void AtualizarProtocoloImpresso(int id, TipoAgendamento tipoAgendamento);
        void AtualizarMotoristaAgendamento(int agendamentoId, int motoristaId, TipoAgendamento tipoAgendamento);
        void AtualizarVeiculoAgendamento(int agendamentoId, int veiculoId, TipoAgendamento tipoAgendamento);
        IEnumerable<AgendamentoDTO> ObterAgendamentos(int transportadoraId);
        IEnumerable<AgendamentoDTO> ObterAgendamentosPorPeriodoEVeiculo(int transportadoraId, int periodoId, int veiculoId, TipoAgendamento tipoAgendamento);
        Agendamento ObterAgendamentoPorId(int id, TipoAgendamento tipoAgendamento);
        AgendamentoDTO ObterDetalhesAgendamento(int id, TipoAgendamento tipoAgendamento);
        IEnumerable<Janela> ObterPeriodos(TipoAgendamento tipoAgendamento);        
        Janela ObterPeriodoPorId(int id);
        AgendamentoDTO ObterDadosProtocolo(int id, TipoAgendamento tipoAgendamento);
        int ObterTotalAgendadoTRA(int recintoId, int id = 0);
        int ObterTotalAgendadoDEPOT(string recinto, int id = 0);
    }
}
