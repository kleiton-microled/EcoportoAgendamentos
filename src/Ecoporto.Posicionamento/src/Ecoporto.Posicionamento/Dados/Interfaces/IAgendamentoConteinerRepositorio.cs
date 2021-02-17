using Ecoporto.Posicionamento.Models;
using Ecoporto.Posicionamento.Models.DTO;
using System;
using System.Collections.Generic;

namespace Ecoporto.Posicionamento.Dados.Interfaces
{
    public interface IAgendamentoConteinerRepositorio
    {
        Agendamento ObterPosicionamentoPorId(int posicionamentoId);
        IEnumerable<AgendamentoDTO> ObterAgendamentos(int despachanteId);
        Agendamento ObterDetalhesConteinerPorReserva(string reserva);
        IEnumerable<Conteiner> ObterConteineresPorReserva(string reserva);
        int GravarPosicionamentoCntr(Agendamento agendamento);
        IEnumerable<Conteiner> ObterConteineresAgendamento(int protocoloUnificado);
        Conteiner ObterConteinerAgendamento(int agendamentoId);
        void CancelarAgendamento(int id, string motivo);
        AgendamentoDTO ExistePosicionamento(int motivo, DateTime dataPrevista, int conteiner);
    }
}
