using Ecoporto.AgendamentoCS.Models;
using Ecoporto.AgendamentoCS.Models.DTO;
using System;
using System.Collections.Generic;

namespace Ecoporto.AgendamentoCS.Dados.Interfaces
{
    public interface IAgendamentoRepositorio
    {
        int Cadastrar(Agendamento agendamento);
        void Atualizar(Agendamento agendamento);
        void Excluir(int id);
        IEnumerable<AgendamentoDTO> ObterAgendamentos(int transportadoraId);
        IEnumerable<AgendamentoDTO> ObterAgendamentosPorPeriodoEVeiculo(int transportadoraId, int periodoId, int veiculoId);
        Agendamento ObterAgendamentoPorId(int id);
        AgendamentoDTO ObterDetalhesAgendamento(int id);
        IEnumerable<Janela> ObterPeriodos(DateTime inicio, DateTime fim, bool cargaProjeto);
        IEnumerable<ReservaItem> ObterItensAgendamento(int id);
        void AtualizarProtocoloImpresso(int id);
        Janela ObterPeriodoPorId(int id);
        IEnumerable<NotaFiscal> ObterNotasFiscaisPorDanfe(string danfe);
        IEnumerable<NotaFiscal> ObterNotasFiscaisPorItemAgendamento(int agendamentoId, int itemId);
        IEnumerable<CFOP> ObterCFOP();
        IEnumerable<NotaFiscal> ObterNotasFiscaisAgendamento(int agendamentoId);
        IEnumerable<Reserva> ObterReservasAgendamento(int id);
        IEnumerable<Reserva> ObterPeriodosReservasCargaSolta(string[] reservas);
        AgendamentoDTO ObterDadosProtocolo(int id);
        bool CargaExigeNF(string reserva, int viagem);
        ChassiProtocoloDTO ChassiEmOutroAgendamento(string chassi);
        bool ItemCargaProjeto(int id);
    }
}
