using Ecoporto.AgendamentoConteiner.Models;

namespace Ecoporto.AgendamentoConteiner.Dados.Interfaces
{
    public interface IUsuarioRepositorio
    {
        Usuario ObterUsuarioICC(int id);
        void ExcluirRegistroIntAcesso(int id);
    }
}
