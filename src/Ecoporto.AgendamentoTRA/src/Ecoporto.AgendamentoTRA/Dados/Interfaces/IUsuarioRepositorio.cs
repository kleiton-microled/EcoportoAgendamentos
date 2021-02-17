using Ecoporto.AgendamentoTRA.Models;

namespace Ecoporto.AgendamentoTRA.Dados.Interfaces
{
    public interface IUsuarioRepositorio
    {
        UsuarioAutenticado ObterUsuarioTRA(int id);
        UsuarioAutenticado ObterUsuarioDEPOT(int id);
        void ExcluirRegistroIntAcesso(int id);
    }
}
