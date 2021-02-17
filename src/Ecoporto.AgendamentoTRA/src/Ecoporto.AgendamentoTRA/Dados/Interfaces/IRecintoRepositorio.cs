using Ecoporto.AgendamentoTRA.Models;
using System.Collections.Generic;

namespace Ecoporto.AgendamentoTRA.Dados.Interfaces
{
    public interface IRecintoRepositorio
    {
        IEnumerable<Recinto> ObterRecintosDaTransportadora(int transportadoraId);
    }
}
