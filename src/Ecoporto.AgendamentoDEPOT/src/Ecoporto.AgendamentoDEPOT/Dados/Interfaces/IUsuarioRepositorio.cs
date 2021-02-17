using Ecoporto.AgendamentoDEPOT.Models;

namespace Ecoporto.AgendamentoDEPOT.Dados.Interfaces
{
    public interface IUsuarioRepositorio
    {
        Usuario ObterUsuarioICC(int id);
        void ExcluirRegistroIntAcesso(int id);
    }
}
