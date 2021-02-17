using Ecoporto.AgendamentoDEPOT.App_Start;
using Ecoporto.AgendamentoDEPOT.Extensions;
using Ecoporto.AgendamentoDEPOT.Helpers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Ecoporto.AgendamentoDEPOT
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            ModelBinders.Binders.Add(
               typeof(decimal), new DecimalModelBinder());
            ModelBinders.Binders.Add(
                typeof(decimal?), new DecimalModelBinder());

            ViewEngines.Engines.Clear();
            ViewEngines.Engines.Add(new RazorViewEngine());
        }

        protected void Application_Error(object sender, EventArgs e)
        {
            Exception exception = Server.GetLastError();

            var httpContext = ((HttpApplication)sender).Context;
            var httpRequest = new HttpRequestWrapper(Request);

            httpContext.Response.Clear();
            httpContext.ClearError();
            httpContext.Response.TrySkipIisCustomErrors = true;

            if (httpRequest.IsAjaxRequest())
            {
                Response.ContentType = "application/json";
                Response.StatusCode = 400;

                Response.Write(
                    JsonConvert.SerializeObject(new
                    {
                        erros = new[]
                        {
                            new {
                                ErrorMessage = exception.Message
                            }
                        }
                    }));
            }
            else
            {
                var rota = new RouteData();

                rota.Values["Controller"] = "Home";
                rota.Values["Action"] = "Erro";

                HttpContext.Current.Session["LastException"] = exception.Message;
                HttpContext.Current.Session["LastStackTrace"] = exception.StackTrace;

                httpContext.Response.RedirectToRoute("Default", rota.Values);
            }
        }

        protected void Application_AuthenticateRequest(Object sender, EventArgs e)
        {
            Identity.Autenticar();
        }
    }
}
