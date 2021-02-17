using Ecoporto.Posicionamento.Models;

namespace Ecoporto.Posicionamento.Dados.Interfaces
{
    public interface IUsuarioRepositorio
    {
        Usuario ObterUsuarioICC(int id);
        void ExcluirRegistroIntAcesso(int id);
    }
}
