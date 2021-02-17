using System.Web.Mvc;
using System.Web.Routing;

namespace Ecoporto.AgendamentoDEPOT
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Atualizar",
                url: "{controller}/{action}/{id}/{tipoAgendamento}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional, tipoAgendamento = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
