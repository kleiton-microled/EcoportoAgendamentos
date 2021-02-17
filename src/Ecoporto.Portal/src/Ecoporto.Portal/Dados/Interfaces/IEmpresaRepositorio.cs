using Ecoporto.Portal.Models;

namespace Ecoporto.Portal.Dados.Interfaces
{
    public interface IEmpresaRepositorio
    {
        Empresa ObterEmpresaPorId(int id);
    }
}