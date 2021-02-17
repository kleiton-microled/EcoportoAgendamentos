using System.Security.Claims;
using System.Security.Principal;

namespace Ecoporto.AgendamentoCS.Extensions
{
    public static class UserExtensions
    {
        public static int ObterId(this IPrincipal user)
        {
            var claim = ((ClaimsIdentity)user.Identity).FindFirst("Id");

            return claim == null
                ? 0
                : claim.Value.ToInt();
        }

        public static string ObterNome(this IPrincipal user)
        {
            var claim = ((ClaimsIdentity)user.Identity).FindFirst("Nome");

            return claim?.Value;
        }

        public static string ObterEmail(this IPrincipal user)
        {
            var claim = ((ClaimsIdentity)user.Identity).FindFirst("Email");

            return claim?.Value;
        }

        public static string ObterCpf(this IPrincipal user)
        {
            var claim = ((ClaimsIdentity)user.Identity).FindFirst("CPF");

            return claim?.Value;
        }

        public static int ObterTransportadoraId(this IPrincipal user)
        {
            var claim = ((ClaimsIdentity)user.Identity).FindFirst("TransportadoraId");

            return claim?.Value != null
                ? claim.Value.ToInt()
                : 0;
        }

        public static string ObterTransportadoraDescricao(this IPrincipal user)
        {
            var claim = ((ClaimsIdentity)user.Identity).FindFirst("TransportadoraDescricao");

            return claim?.Value ?? string.Empty;
        }

        public static string ObterTransportadoraCnpj(this IPrincipal user)
        {
            var claim = ((ClaimsIdentity)user.Identity).FindFirst("TransportadoraCNPJ");

            return claim?.Value ?? string.Empty;
        }
    }
}