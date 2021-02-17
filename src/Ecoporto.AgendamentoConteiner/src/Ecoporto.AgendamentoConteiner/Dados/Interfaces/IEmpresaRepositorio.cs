using Ecoporto.AgendamentoConteiner.Models;

namespace Ecoporto.AgendamentoConteiner.Dados.Interfaces
{
    public interface IEmpresaRepositorio
    {
        Empresa ObterEmpresaPorId(int id);
    }
}