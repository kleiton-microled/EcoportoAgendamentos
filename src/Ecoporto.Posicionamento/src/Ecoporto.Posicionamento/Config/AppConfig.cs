using Ecoporto.Posicionamento.Extensions;
using System.Configuration;

namespace Ecoporto.Posicionamento.Config
{
    public static class AppConfig
    {
        public static string StringConexao() 
            => ConfigurationManager.ConnectionStrings["StringConexaoOracle"].ConnectionString;

        public static string UrlICC()
            => ConfigurationManager.AppSettings["UrlICC"].ToString();

        public static bool HabilitaMenuConteiner()
            => ConfigurationManager.AppSettings["HabilitaMenuConteiner"]
            .ToInt()
            .ToBoolean();

        public static bool HabilitaMenuCargaSolta()
            => ConfigurationManager.AppSettings["HabilitaMenuCargaSolta"]
            .ToInt()
            .ToBoolean();

        public static bool HabilitaMenuVeiculos()
            => ConfigurationManager.AppSettings["HabilitaMenuVeiculos"]
            .ToInt()
            .ToBoolean();
    }
}