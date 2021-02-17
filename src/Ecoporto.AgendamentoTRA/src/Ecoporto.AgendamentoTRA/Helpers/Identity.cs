using Ecoporto.AgendamentoTRA.Config;
using Ecoporto.AgendamentoTRA.Extensions;
using Ecoporto.AgendamentoTRA.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Web;
using System.Web.Security;

namespace Ecoporto.AgendamentoTRA.Helpers
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

                var usuario = JsonConvert.DeserializeObject<UsuarioAutenticado>(ticket.UserData);

                if (usuario != null)
                {
                    claimsIdentity.AddClaims(ObterClaims(usuario));
                }

                ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

                HttpContext.Current.User = claimsPrincipal;
            }
        }

        public static List<Claim> ObterClaims(UsuarioAutenticado usuario)
        {
            try
            {
                var claims = new List<Claim>
                {
                    new Claim("Id", usuario.Id.ToString()),
                    new Claim("Nome", usuario.Nome),
                    new Claim("Email", usuario.Email),
                    new Claim("CPF", usuario.CPF),
                    new Claim("Ativo", usuario.Ativo.ToString()),
                    new Claim("RecintoId", usuario.RecintoId ?? string.Empty),
                    new Claim("RecintoDescricao", usuario.RecintoDescricao ?? string.Empty),
                    new Claim("RecintoCNPJ", usuario.RecintoCNPJ ?? string.Empty),
                    new Claim("DEPOT", usuario.DEPOT.ToInt().ToString()),
                };

                return claims;
            }
            catch
            {
                Identity.Logout();

                HttpContext.Current.Response.Redirect(AppConfig.UrlICC(), true);
            }

            return null;
        }

        public static void Logout()
        {
            FormsAuthentication.SignOut();
        }
    }
}