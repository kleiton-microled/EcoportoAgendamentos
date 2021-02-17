using Dapper;
using Ecoporto.Portal.Config;
using Ecoporto.Portal.Dados.Interfaces;
using Ecoporto.Portal.Models;
using Oracle.ManagedDataAccess.Client;
using System.Data;
using System.Linq;

namespace Ecoporto.Portal.Dados.Repositorios
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