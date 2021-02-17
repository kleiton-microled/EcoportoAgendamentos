using Ecoporto.AgendamentoConteiner.Models;
using System.Collections.Generic;

namespace Ecoporto.AgendamentoConteiner.Dados.Interfaces
{
    public interface IReservaRepositorio
    {
        Reserva ObterDetalhesReserva(string reserva);
        IEnumerable<Reserva> ObterItensReserva(string reserva, int viagemId);
        Reserva ObterItemReservaPorId(int bookingId, int tamanho);
        Reserva ObterDetalhesItem(string reserva, int bookingCsId);
        Reserva AberturaPraca(int bookingId);
        Reserva ObterItemReservaPorBokingId(int bookingId);
        IEnumerable<string> ObterIMOs(string reserva);
        IEnumerable<string> ObterONUs(string reserva);
    }
}