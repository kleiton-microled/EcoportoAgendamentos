using Dapper;
using Ecoporto.AgendamentoCS.Config;
using Ecoporto.AgendamentoCS.Dados.Interfaces;
using Ecoporto.AgendamentoCS.Models;
using Oracle.ManagedDataAccess.Client;
using System.Data;
using System.Linq;

namespace Ecoporto.AgendamentoCS.Dados.Repositorios
{
    public class EmpresaRepositorio : IEmpresaRepositorio
    {
        public Empresa ObterEmpresaPorId(int id)
        {
            using (OracleConnection con = new OracleConnection(AppConfig.StringConexao()))
            {
                var parametros = new DynamicParameters();
                parametros.Add(name: "Id", value: id, direction: ParameterDirection.Input);

                return con.Query<Empresa>(@"SELECT ENDERECO, BAIRRO, CIDADE,ESTADO FROM SGIPA.TB_EMPRESAS WHERE AUTONUM = :Id", parametros).FirstOrDefault();
            }
        }
    }
}