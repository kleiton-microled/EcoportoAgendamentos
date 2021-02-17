using Ecoporto.AgendamentoTRA.Models;
using System.Collections.Generic;

namespace Ecoporto.AgendamentoTRA.Dados.Interfaces
{
    public interface ITransportadoraRepositorio
    {
        IEnumerable<TransportadoraRecintoDTO> ObterTransportadoras(string filtro);
        TransportadoraRecintoDTO ObterTransportadorasPorId(int id);
    }
}
