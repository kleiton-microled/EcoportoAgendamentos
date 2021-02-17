using Ecoporto.Posicionamento.Models;

namespace Ecoporto.Posicionamento.Dados.Interfaces
{
    public interface IEmpresaRepositorio
    {
        Empresa ObterEmpresaPorId(int id);
    }
}