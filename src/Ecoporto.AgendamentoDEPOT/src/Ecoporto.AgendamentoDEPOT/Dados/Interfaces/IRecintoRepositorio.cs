using Ecoporto.AgendamentoDEPOT.Enums;
using Ecoporto.AgendamentoDEPOT.Models;
using System.Collections.Generic;

namespace Ecoporto.AgendamentoDEPOT.Dados.Interfaces
{
    public interface IRecintoRepositorio
    {
        IEnumerable<Recinto> ObterRecintos(int transportadoraId, TipoAgendamento tipoAgendamento);
        int ObterTotalEstoqueTRA(int recintoId);
        int ObterTotalEstoqueDEPOT(string recinto);
    }
}
