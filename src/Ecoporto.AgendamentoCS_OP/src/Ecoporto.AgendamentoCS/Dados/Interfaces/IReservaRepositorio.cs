using Ecoporto.AgendamentoCS.Models;
using System.Collections.Generic;

namespace Ecoporto.AgendamentoCS.Dados.Interfaces
{
    public interface IReservaRepositorio
    {
        Reserva ObterDetalhesReserva(string reserva);
        IEnumerable<ReservaItem> ObterItensReserva(string reserva, int viagemId);
        ReservaItem ObterItemReservaPorId(int bookingCsItemId);
        ReservaItem ObterDetalhesItem(string reserva, int bookingCsId);
        Reserva AberturaPraca(string reserva);
        int ObterSaldoTotalReserva(string reserva, int viagemId);
        bool CargaExigeNF(int bookingCsItemId);
        bool CargaExigeChassi(int bookingCsItemId);
    }
}