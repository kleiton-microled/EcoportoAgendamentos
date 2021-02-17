using Ecoporto.Portal.Models;

namespace Ecoporto.Portal.Dados.Interfaces
{
    public interface IUsuarioRepositorio
    {
        Usuario ObterUsuarioICC(int id);
        void ExcluirRegistroIntAcesso(int id);
    }
}
