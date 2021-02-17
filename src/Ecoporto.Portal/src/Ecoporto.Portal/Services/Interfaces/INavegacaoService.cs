using System.Security.Principal;

namespace Ecoporto.Portal.Services.Interfaces
{
    public interface INavegacaoService
    {
        long SolicitarAcesso(IPrincipal usuario);
    }
}