using Ecoporto.AgendamentoTRA.Models;
using System.Collections.Generic;

namespace Ecoporto.AgendamentoTRA.Dados.Interfaces
{
    public interface IVinculoBaseRepositorio
    {
        IEnumerable<TransportadoraRecintoDTO> ObterVinculos(int pagina, int registrosPorPagina, string orderBy, out int totalFiltro, int recintoId, string filtro);
        int ObterTotalVinculos(int recintoId);
        void Bloquear(int id);
        void Habilitar(int id);
        TransportadoraRecintoDTO ObterVinculoPorId(int id);
        TransportadoraRecintoDTO ObterVinculoPorTransportadoraERecinto(int transportadoraId, int recintoId);
        void VincularTransportadora(int tansportadoraId, int recintoId);
        void ExcluirVinculo(int id);
        bool ExisteAgendamentoNoRecintoTRA(int recintoId, int transportadoraId);
        bool ExisteAgendamentoNoRecintoDEPOT(int recintoId, int transportadoraId);
    }
}
