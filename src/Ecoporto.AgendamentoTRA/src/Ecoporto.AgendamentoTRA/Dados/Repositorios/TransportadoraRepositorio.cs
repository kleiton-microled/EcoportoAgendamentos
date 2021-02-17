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
    public class TransportadoraRepositorio : ITransportadoraRepositorio
    {               
        public IEnumerable<TransportadoraRecintoDTO> ObterTransportadoras(string filtro)
        {
            using (OracleConnection con = new OracleConnection(AppConfig.StringConexao()))
            {
                var parametros = new DynamicParameters();
                parametros.Add(name: "Termo", value: $"%{filtro}%", direction: ParameterDirection.Input);

                return con.Query<TransportadoraRecintoDTO>(@"
                    SELECT
                        AUTONUM As TransportadoraId,
                        CGC As CNPJ,
                        RAZAO As RazaoSocial
                    FROM
                        OPERADOR.TB_CAD_TRANSPORTADORAS
                    WHERE
                        RAZAO IS NOT NULL AND (CGC IS NOT NULL AND CGC <> '__.___.___/____-__' AND CGC <> '0' AND CGC <> '00.000.000/0000-00')
                    AND
                        (UPPER(RAZAO) LIKE :Termo OR CGC LIKE :Termo)
                    ORDER BY
                        RAZAO", parametros);
            }
        }

        public TransportadoraRecintoDTO ObterTransportadorasPorId(int id)
        {
            using (OracleConnection con = new OracleConnection(AppConfig.StringConexao()))
            {
                var parametros = new DynamicParameters();
                parametros.Add(name: "Id", value: id, direction: ParameterDirection.Input);

                return con.Query<TransportadoraRecintoDTO>(@"
                    SELECT
                        AUTONUM As Id,
                        CGC As CNPJ,
                        RAZAO As RazaoSocial
                    FROM
                        OPERADOR.TB_CAD_TRANSPORTADORAS                    
                    WHERE
                        AUTONUM = :Id", parametros).FirstOrDefault();
            }
        }
    }
}