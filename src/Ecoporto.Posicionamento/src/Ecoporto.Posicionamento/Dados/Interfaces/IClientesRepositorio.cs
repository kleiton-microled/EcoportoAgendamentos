using Ecoporto.Posicionamento.Models;

namespace Ecoporto.Posicionamento.Dados.Interfaces
{
    public interface IClientesRepositorio
    {
        Cliente ObterClientePorDocumento(string documento);
    }
}