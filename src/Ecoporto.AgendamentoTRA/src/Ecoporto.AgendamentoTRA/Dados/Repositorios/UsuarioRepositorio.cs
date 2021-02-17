using Dapper;
using Ecoporto.AgendamentoTRA.Config;
using Ecoporto.AgendamentoTRA.Dados.Interfaces;
using Ecoporto.AgendamentoTRA.Models;
using Oracle.ManagedDataAccess.Client;
using System.Data;
using System.Linq;

namespace Ecoporto.AgendamentoTRA.Dados.Repositorios
{
    public class UsuarioRepositorio : IUsuarioRepositorio
    {
        public UsuarioAutenticado ObterUsuarioTRA(int id)
        {
            using (OracleConnection con = new OracleConnection(AppConfig.StringConexao()))
            {
                var parametros = new DynamicParameters();
                parametros.Add(name: "Id", value: id, direction: ParameterDirection.Input);

                return con.Query<UsuarioAutenticado>(@"
                    SELECT 
                        B.IUSID As Id, 
                        NVL(TRIM(UPPER(SUBSTR(B.IUSNOME, 1, INSTR(B.IUSNOME, ' ', 1, 1)-1))), ' ') AS Nome,
                        B.IUSCPF As CPF,
                        B.IUSATIVO As Ativo,
                        B.IUSEMAIL As Email,
                        C.Code As RecintoId,
                        C.DESCR As RecintoDescricao,
                        C.CGC As RecintoCnpj,
                        0 As DEPOT
                    FROM
                        INTERNET.TB_INT_ACESSO A 
                    INNER JOIN 
                        INTERNET.TB_INT_USER B ON A.USRID = B.IUSID
                    INNER JOIN
                        OPERADOR.DTE_TB_RECINTOS C ON REPLACE(REPLACE(REPLACE(A.TIACNPJ,'.',''),'/',''),'-','') = REPLACE(REPLACE(REPLACE(C.CGC,'.',''),'/',''),'-','')
                    WHERE
                        A.TIAID = :Id", parametros).FirstOrDefault();
            }
        }

        public UsuarioAutenticado ObterUsuarioDEPOT(int id)
        {
            using (OracleConnection con = new OracleConnection(AppConfig.StringConexao()))
            {
                var parametros = new DynamicParameters();
                parametros.Add(name: "Id", value: id, direction: ParameterDirection.Input);

                return con.Query<UsuarioAutenticado>(@"
                    SELECT 
                        B.IUSID As Id, 
                        NVL(TRIM(UPPER(SUBSTR(B.IUSNOME, 1, INSTR(B.IUSNOME, ' ', 1, 1)-1))), ' ') AS Nome,
                        B.IUSCPF As CPF,
                        B.IUSATIVO As Ativo,
                        B.IUSEMAIL As Email,
                        C.AUTONUM As RecintoId,
                        C.NOME As RecintoDescricao,
                        C.CNPJ As RecintoCnpj,
                        1 As DEPOT
                    FROM
                        INTERNET.TB_INT_ACESSO A 
                    INNER JOIN 
                        INTERNET.TB_INT_USER B ON A.USRID = B.IUSID
                    INNER JOIN
                        OPERADOR.VW_NAVIS_DEPOT C ON REPLACE(REPLACE(REPLACE(A.TIACNPJ,'.',''),'/',''),'-','') = REPLACE(REPLACE(REPLACE(C.CNPJ,'.',''),'/',''),'-','')
                    WHERE
                        A.TIAID = :Id", parametros).FirstOrDefault();
            }
        }

        public void ExcluirRegistroIntAcesso(int id)
        {
            using (OracleConnection con = new OracleConnection(AppConfig.StringConexao()))
            {
                var parametros = new DynamicParameters();
                parametros.Add(name: "Id", value: id, direction: ParameterDirection.Input);

                con.Execute(@"DELETE FROM INTERNET.TB_INT_ACESSO WHERE TIAID = :Id", parametros);
            }
        }
    }
}