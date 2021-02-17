using Dapper;
using Ecoporto.AgendamentoCS.Config;
using Ecoporto.AgendamentoCS.Dados.Interfaces;
using Ecoporto.AgendamentoCS.Models;
using Oracle.ManagedDataAccess.Client;
using System.Data;
using System.Linq;

namespace Ecoporto.AgendamentoCS.Dados.Repositorios
{
    public class UsuarioRepositorio : IUsuarioRepositorio
    {
        public Usuario ObterUsuarioICC(int id)
        {
            using (OracleConnection con = new OracleConnection(AppConfig.StringConexao()))
            {
                var parametros = new DynamicParameters();
                parametros.Add(name: "Id", value: id, direction: ParameterDirection.Input);

                return con.Query<Usuario>(@"
                    SELECT 
                        IUSID As Id, 
                        NVL(TRIM(UPPER(SUBSTR(B.IUSNOME, 1, INSTR(B.IUSNOME, ' ', 1, 1)-1))), ' ') AS Nome,
                        IUSCPF As CPF,
                        IUSATIVO As Ativo,
                        IUSEMAIL As Email,
                        C.AUTONUM As TransportadoraId,
                        SUBSTR(NVL(C.FANTASIA, ' '), 0, 22) As TransportadoraDescricao,
                        NVL(C.CGC, ' ') As TransportadoraCNPJ
                    FROM
                        INTERNET.TB_INT_ACESSO A 
                    INNER JOIN 
                        INTERNET.TB_INT_USER B ON A.USRID = B.IUSID
                    INNER JOIN
                        OPERADOR.TB_CAD_TRANSPORTADORAS C ON A.TIACNPJ = C.CGC
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