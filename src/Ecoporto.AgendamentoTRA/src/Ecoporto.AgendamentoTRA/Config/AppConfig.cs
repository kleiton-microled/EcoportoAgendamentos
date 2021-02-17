using System.Configuration;

namespace Ecoporto.AgendamentoTRA.Config
{
    public static class AppConfig
    {
        public static string StringConexao() 
            => ConfigurationManager.ConnectionStrings["StringConexaoOracle"].ConnectionString;

        public static string UrlICC()
            => ConfigurationManager.AppSettings["UrlICC"].ToString();      
    }
}