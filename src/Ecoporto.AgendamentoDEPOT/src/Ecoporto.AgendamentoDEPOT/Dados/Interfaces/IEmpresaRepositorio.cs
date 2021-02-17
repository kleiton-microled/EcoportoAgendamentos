using Ecoporto.AgendamentoDEPOT.Models;

namespace Ecoporto.AgendamentoDEPOT.Dados.Interfaces
{
    public interface IEmpresaRepositorio
    {
        Empresa ObterEmpresaPorId(int id);
    }
}