using Dapper;
using Ecoporto.Portal.Config;
using Ecoporto.Portal.Dados.Interfaces;
using Ecoporto.Portal.Models;
using Oracle.ManagedDataAccess.Client;
using System.Data;
using System.Linq;

namespace Ecoporto.Portal.Dados.Repositorios
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
                        A.TiaEmpresa As EmpresaLoginId,
                        B.IUSID As Id, 
                        NVL(TRIM(UPPER(SUBSTR(B.IUSNOME, 1, INSTR(B.IUSNOME, ' ', 1, 1)-1))), ' ') AS Nome,
                        B.IUSCPF As CPF,
                        B.IUSATIVO As Ativo,
                        B.IUSEMAIL As Email,
                        C.Id As EmpresaId,
                        C.Descricao As EmpresaDescricao,
                        SUBSTR(C.Descricao, 1, 20) As EmpresaDescricaoCurta,
                        C.CNPJ As EmpresaCnpj,
                        B.IUSADMIN As Administrador,
                        C.Perfil
                    FROM
                        INTERNET.TB_INT_ACESSO A 
                    INNER JOIN 
                        INTERNET.TB_INT_USER B ON A.USRID = B.IUSID
                    LEFT JOIN
                        SGIPA.VW_PORTAL_EMPRESAS C ON REPLACE(REPLACE(REPLACE(TRIM(A.TIACNPJ),'.',''),'/',''),'-','') = REPLACE(REPLACE(REPLACE(TRIM(C.CNPJ),'.',''),'/',''),'-','')
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