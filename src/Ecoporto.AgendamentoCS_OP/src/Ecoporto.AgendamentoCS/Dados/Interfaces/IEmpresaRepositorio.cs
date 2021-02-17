using Ecoporto.AgendamentoCS.Models;

namespace Ecoporto.AgendamentoCS.Dados.Interfaces
{
    public interface IEmpresaRepositorio
    {
        Empresa ObterEmpresaPorId(int id);
    }
}