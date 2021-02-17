using System;
using System.Web;

namespace Ecoporto.Portal.Helpers
{
    public class Cookies
    {
        public static void GravarCookie(string chave, string valor, DateTime expira)
        {
            HttpCookie userid = new HttpCookie(chave, valor)
            {
                Expires = expira
            };

            HttpContext.Current.Response.Cookies.Add(userid);
        }

        public static object ObterCookie(string chave)
        {
            try
            {
                var valor = HttpContext.Current.Request.Cookies[chave].Value;

                if (string.IsNullOrEmpty(valor))
                    throw new Exception("Nenhum valor obtido");

                return valor;
            }
            catch
            {
                return null;
            }
        }
    }
}