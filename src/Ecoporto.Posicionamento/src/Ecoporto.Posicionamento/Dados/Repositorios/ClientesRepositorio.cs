using Dapper;
using Ecoporto.Posicionamento.Config;
using Ecoporto.Posicionamento.Dados.Interfaces;
using Ecoporto.Posicionamento.Models;
using Oracle.ManagedDataAccess.Client;
using System.Data;
using System.Linq;

namespace Ecoporto.Posicionamento.Dados.Repositorios
{
    public class ClientesRepositorio : IClientesRepositorio
    {
        public Cliente ObterClientePorDocumento(string documento)
        {
            using (OracleConnection con = new OracleConnection(AppConfig.StringConexao()))
            {
                var parametros = new DynamicParameters();
                parametros.Add(name: "Documento", value: documento, direction: ParameterDirection.Input);

                return con.Query<Cliente>(@"SELECT AUTONUM As Id, Razao FROM OPERADOR.VW_CLIENTE_POS WHERE REPLACE(REPLACE(REPLACE(CGC, '.', ''), '/', ''), '-', '') = :Documento", parametros).FirstOrDefault();
            }
        }
    }
}