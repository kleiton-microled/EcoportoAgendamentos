using Ecoporto.AgendamentoCS.Extensions;
using System.Configuration;

namespace Ecoporto.AgendamentoCS.Config
{
    public static class AppConfig
    {
        public static string StringConexao() 
            => ConfigurationManager.ConnectionStrings["StringConexaoOracle"].ConnectionString;

        public static string UrlICC()
            => ConfigurationManager.AppSettings["UrlICC"].ToString();

        public static bool ValidarBDCC()
            => ConfigurationManager.AppSettings["ValidarBDCC"].ToInt() > 0;
    }
}