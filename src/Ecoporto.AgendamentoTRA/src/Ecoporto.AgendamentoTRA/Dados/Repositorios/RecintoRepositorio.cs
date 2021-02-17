using Dapper;
using Ecoporto.AgendamentoTRA.Config;
using Ecoporto.AgendamentoTRA.Dados.Interfaces;
using Ecoporto.AgendamentoTRA.Models;
using Oracle.ManagedDataAccess.Client;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Ecoporto.AgendamentoTRA.Dados.Repositorios
{
    public class RecintoRepositorio : IRecintoRepositorio
    {
        public IEnumerable<Recinto> ObterRecintosDaTransportadora(int transportadoraId)
        {
            using (OracleConnection con = new OracleConnection(AppConfig.StringConexao()))
            {
                var parametros = new DynamicParameters();
                parametros.Add(name: "TransportadoraId", value: transportadoraId, direction: ParameterDirection.Input);

                return con.Query<Recinto>($@"
                   SELECT
                        B.CODE As Id,
                        B.DESCR As Descricao
                    FROM
                        OPERADOR.TB_TRANSPORTE_RECINTO A
                    INNER JOIN
                        OPERADOR.DTE_TB_RECINTOS B ON A.RECINTO = B.CODE
                    WHERE
                        A.TRANSPORTADORA = :TransportadoraId", parametros);
            }
        }        
    }
}