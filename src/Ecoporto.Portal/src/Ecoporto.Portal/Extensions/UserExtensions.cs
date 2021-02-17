using System.Security.Claims;
using System.Security.Principal;

namespace Ecoporto.Portal.Extensions
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
    
        public static int ObterEmpresaId(this IPrincipal user)
        {
            var claim = ((ClaimsIdentity)user.Identity).FindFirst("EmpresaId");

            return claim == null
                ? 0
                : claim.Value.ToInt();
        }

        public static int ObterEmpresaLoginId(this IPrincipal user)
        {
            var claim = ((ClaimsIdentity)user.Identity).FindFirst("EmpresaLoginId");

            return claim == null
                ? 0
                : claim.Value.ToInt();
        }
        
        public static string ObterEmpresaDescricao(this IPrincipal user)
        {
            var claim = ((ClaimsIdentity)user.Identity).FindFirst("EmpresaDescricao");

            return claim?.Value;
        }

        public static string ObterEmpresaDescricaoCurta(this IPrincipal user)
        {
            var claim = ((ClaimsIdentity)user.Identity).FindFirst("EmpresaDescricaoCurta");

            return claim?.Value;
        }        

        public static string ObterEmpresaCNPJ(this IPrincipal user)
        {
            var claim = ((ClaimsIdentity)user.Identity).FindFirst("EmpresaCnpj");

            return claim?.Value;
        }        
    }
}