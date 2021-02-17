using Dapper;
using Ecoporto.Posicionamento.Config;
using Ecoporto.Posicionamento.Dados.Interfaces;
using Ecoporto.Posicionamento.Models;
using Oracle.ManagedDataAccess.Client;
using System.Collections.Generic;

namespace Ecoporto.Posicionamento.Dados.Repositorios
{
    public class PeriodosRepositorio : IPeriodosRepositorio
    {
        public IEnumerable<Periodo> ObterPeriodos()
        {
            using (OracleConnection con = new OracleConnection(AppConfig.StringConexao()))
            {
                return con.Query<Periodo>(@"SELECT DATA_INI As Inicio FROM OPERADOR.VW_POSIC_INICIO ORDER BY DATA_ORD");
            }
        }
    }
}