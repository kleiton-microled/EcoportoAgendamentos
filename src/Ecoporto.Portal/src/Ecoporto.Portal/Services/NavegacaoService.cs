using Dapper;
using Ecoporto.Portal.Config;
using Ecoporto.Portal.Extensions;
using Ecoporto.Portal.Services.Interfaces;
using Oracle.ManagedDataAccess.Client;
using System.Data;
using System.Security.Principal;

namespace Ecoporto.Portal.Services
{
    public class NavegacaoService : INavegacaoService
    {
        public long SolicitarAcesso(IPrincipal usuario)
        {
            using (OracleConnection con = new OracleConnection(AppConfig.StringConexao()))
            {
                var parametros = new DynamicParameters();

                parametros.Add(name: "UsrId", value: usuario.ObterId(), direction: ParameterDirection.Input);
                parametros.Add(name: "TiaCnpj", value: usuario.ObterEmpresaCNPJ(), direction: ParameterDirection.Input);
                parametros.Add(name: "TiaEmpresa", value: usuario.ObterEmpresaLoginId(), direction: ParameterDirection.Input);

                parametros.Add(name: "Id", dbType: DbType.Int32, direction: ParameterDirection.Output);

                con.Execute(@"
                    INSERT 
                        INTO INTERNET.TB_INT_ACESSO 
                            (
                                TIAID, 
                                USRID, 
                                TIACNPJ, 
                                TIAEMPRESA
                            ) VALUES (
                                INTERNET.SEQ_INT_ACESSO.NEXTVAL, 
                                :UsrId, 
                                :TiaCnpj, 
                                :TiaEmpresa
                            ) RETURNING TIAID INTO :Id", parametros);

                return parametros.Get<int>("Id");
            }
        }
    }
}