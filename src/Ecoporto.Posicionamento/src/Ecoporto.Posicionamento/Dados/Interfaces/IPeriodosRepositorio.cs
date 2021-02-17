using Ecoporto.Posicionamento.Models;
using System.Collections.Generic;

namespace Ecoporto.Posicionamento.Dados.Interfaces
{
    public interface IPeriodosRepositorio
    {
        IEnumerable<Periodo> ObterPeriodos();
    }
}
