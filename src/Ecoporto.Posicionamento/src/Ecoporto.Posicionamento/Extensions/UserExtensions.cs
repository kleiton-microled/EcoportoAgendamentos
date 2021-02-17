using System.Security.Claims;
using System.Security.Principal;

namespace Ecoporto.Posicionamento.Extensions
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

        public static int ObterDespachanteId(this IPrincipal user)
        {
            var claim = ((ClaimsIdentity)user.Identity).FindFirst("DespachanteId");

            return claim?.Value != null
                ? claim.Value.ToInt()
                : 0;
        }

        public static string ObterDespachanteDescricao(this IPrincipal user)
        {
            var claim = ((ClaimsIdentity)user.Identity).FindFirst("DespachanteDescricao");

            return claim?.Value ?? string.Empty;
        }

        public static string ObterDespachanteCnpj(this IPrincipal user)
        {
            var claim = ((ClaimsIdentity)user.Identity).FindFirst("DespachanteCNPJ");

            return claim?.Value ?? string.Empty;
        }

        public static bool SiteSemMenu(this IPrincipal user)
        {
            var claim = ((ClaimsIdentity)user.Identity).FindFirst("SiteSemMenu");

            return claim?.Value.ToInt() > 0;
        }
    }
}