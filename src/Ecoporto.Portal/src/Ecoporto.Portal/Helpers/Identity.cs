using Ecoporto.Portal.Extensions;
using Ecoporto.Portal.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Security.Claims;
using System.Web;
using System.Web.Security;

namespace Ecoporto.Portal.Helpers
{
    public class Identity
    {
        public static void Autenticar()
        {
            var authCookie = HttpContext.Current.Request.Cookies[FormsAuthentication.FormsCookieName];

            if (authCookie != null)
            {
                var ticket = FormsAuthentication.Decrypt(authCookie.Value);

                FormsIdentity formsIdentity = new FormsIdentity(ticket);

                ClaimsIdentity claimsIdentity = new ClaimsIdentity(formsIdentity);

                var usuario = JsonConvert.DeserializeObject<Usuario>(ticket.UserData);

                if (usuario != null)
                {
                    claimsIdentity.AddClaims(ObterClaims(usuario));
                }

                ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

                HttpContext.Current.User = claimsPrincipal;
            }
        }

        public static List<Claim> ObterClaims(Usuario usuario)
        {
            var claims = new List<Claim>
            {
                new Claim("Id", usuario.Id.ToString()),
                new Claim("Nome", usuario.Nome),
                new Claim("Email", usuario.Email),
                new Claim("CPF", usuario.CPF),
                new Claim("Ativo", usuario.Ativo.ToString()),
                new Claim("EmpresaId", usuario.EmpresaId.ToString()),
                new Claim("EmpresaLoginId", usuario.EmpresaLoginId.ToString()),
                new Claim("EmpresaDescricao", usuario.EmpresaDescricao),
                new Claim("EmpresaDescricaoCurta", usuario.EmpresaDescricaoCurta),                
                new Claim("EmpresaCnpj", usuario.EmpresaCnpj)
            };

            if (usuario.Administrador)
                claims.Add(new Claim(ClaimTypes.Role, "Administrador"));

            claims.Add(new Claim(ClaimTypes.Role, usuario.Perfil.ToName()));

            return claims;
        }
    }
}