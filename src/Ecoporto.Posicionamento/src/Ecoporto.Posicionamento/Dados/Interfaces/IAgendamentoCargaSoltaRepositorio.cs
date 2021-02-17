using Ecoporto.Posicionamento.Models;
using Ecoporto.Posicionamento.Models.DTO;
using System.Collections.Generic;

namespace Ecoporto.Posicionamento.Dados.Interfaces
{
    public interface IAgendamentoCargaSoltaRepositorio
    {
        Agendamento ObterPosicionamentoPorId(int posicionamentoId);
        IEnumerable<AgendamentoDTO> ObterAgendamentos(int despachanteId);
        Agendamento ObterDetalhesCargaSoltaPorReserva(string reserva);
        IEnumerable<CargaSolta> ObterCargaSoltaPorReserva(string reserva);
        IEnumerable<CargaSolta> ObterItensCargaSoltaAgendamento(int protocoloUnificado);
        CargaSolta ObterCargaSoltaAgendamento(int agendamentoId);
        int GravarPosicionamentoCargaSolta(Agendamento agendamento);
        void CancelarAgendamento(int id, string motivo);
    }
}
