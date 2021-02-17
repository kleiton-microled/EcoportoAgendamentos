using Ecoporto.Posicionamento.Models;
using Ecoporto.Posicionamento.Models.DTO;
using System;
using System.Collections.Generic;

namespace Ecoporto.Posicionamento.Dados.Interfaces
{
    public interface IAgendamentoVeiculosRepositorio
    {
        Agendamento ObterPosicionamentoPorId(int posicionamentoId);
        IEnumerable<AgendamentoDTO> ObterAgendamentos(int despachanteId);
        Agendamento ObterDetalhesVeiculoPorReserva(string reserva);
        IEnumerable<CargaSolta> ObterVeiculoPorReserva(string reserva);
        IEnumerable<VeiculoAgendadoDTO> ObterItensVeiculoAgendamento(int protocoloUnificado);
        VeiculoAgendadoDTO ObterVeiculoAgendamento(int agendamentoId);
        int GravarPosicionamentoVeiculo(Agendamento agendamento);
        void CancelarAgendamento(int id, string motivo);
        AgendamentoDTO ExistePosicionamento(int motivo, DateTime dataPrevista, int itemId);
    }
}
