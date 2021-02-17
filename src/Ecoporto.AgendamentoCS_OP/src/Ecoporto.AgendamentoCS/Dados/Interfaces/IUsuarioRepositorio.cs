using Ecoporto.AgendamentoCS.Models;

namespace Ecoporto.AgendamentoCS.Dados.Interfaces
{
    public interface IUsuarioRepositorio
    {
        Usuario ObterUsuarioICC(int id);
        void ExcluirRegistroIntAcesso(int id);
    }
}
