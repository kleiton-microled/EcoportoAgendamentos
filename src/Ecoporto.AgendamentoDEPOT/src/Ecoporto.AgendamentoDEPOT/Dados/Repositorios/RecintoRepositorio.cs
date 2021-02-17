using Dapper;
using Ecoporto.AgendamentoDEPOT.Config;
using Ecoporto.AgendamentoDEPOT.Enums;
using Ecoporto.AgendamentoDEPOT.Models;
using Oracle.ManagedDataAccess.Client;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Ecoporto.AgendamentoDEPOT.Dados.Interfaces
{
    public class RecintoRepositorio : IRecintoRepositorio
    {
        public IEnumerable<Recinto> ObterRecintos(int transportadoraId, TipoAgendamento tipoAgendamento)
        {
            using (OracleConnection con = new OracleConnection(AppConfig.StringConexao()))
            {
                var parametros = new DynamicParameters();
                parametros.Add(name: "TransportadoraId", value: transportadoraId, direction: ParameterDirection.Input);

                if (tipoAgendamento == TipoAgendamento.TRA)
                {
                    return con.Query<Recinto>($@"
                        SELECT
                            DISTINCT
                                B.CODE As Id,
                                TRIM(B.DESCR) || ' (' || B.CGC || ')' As Descricao
                        FROM
                            OPERADOR.TB_TRANSPORTE_RECINTO A
                        INNER JOIN
                            OPERADOR.DTE_TB_RECINTOS B ON A.RECINTO = B.CODE
                        WHERE
                            A.TRANSPORTADORA = :TransportadoraId
                        AND
                            NVL(A.FLAG_ATIVO, 0) = 1
                        AND
                            B.CODE IN (SELECT CODRECINTO FROM OPERADOR.VW_ESTOQUE_OP_CNTR_RECINTO)", parametros);
                }
                else
                {
                    return con.Query<Recinto>($@"
                        SELECT
                            DISTINCT
                                B.AUTONUM As Id,
                                B.ID As Codigo,
                                TRIM(B.NOME) || ' (' || B.CNPJ || ')' As Descricao
                        FROM
                            OPERADOR.TB_TRANSPORTE_DEPOT A
                        INNER JOIN
                            OPERADOR.VW_NAVIS_DEPOT B ON A.RECINTO = B.AUTONUM
                        WHERE
                            A.TRANSPORTADORA = :TransportadoraId
                        AND
                            NVL(A.FLAG_ATIVO, 0) = 1
                        AND
                            B.ID IN (SELECT DISTINCT DEPOT FROM OPERADOR.VW_ESTOQUE_OP_CNTR_VZ_IMP)", parametros);
                }                
            }
        }
        
        public int ObterTotalEstoqueTRA(int recintoId)
        {
            using (OracleConnection con = new OracleConnection(AppConfig.StringConexao()))
            {
                var parametros = new DynamicParameters();
                parametros.Add(name: "RecintoId", value: recintoId, direction: ParameterDirection.Input);

                return con.Query<int>($@"SELECT COUNT(1) As Total FROM OPERADOR.VW_ESTOQUE_OP_CNTR_RECINTO WHERE CODRECINTO = :RecintoId", parametros).Single();
            }
        }

        public int ObterTotalEstoqueDEPOT(string recinto)
        {
            using (OracleConnection con = new OracleConnection(AppConfig.StringConexao()))
            {
                var parametros = new DynamicParameters();
                parametros.Add(name: "Recinto", value: recinto, direction: ParameterDirection.Input);

                return con.Query<int>($@"SELECT COUNT(1) As Total FROM OPERADOR.VW_ESTOQUE_OP_CNTR_VZ_IMP WHERE DEPOT = :Recinto", parametros).Single();
            }
        }
    }
}