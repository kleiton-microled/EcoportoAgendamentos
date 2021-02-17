using Dapper;
using Ecoporto.Posicionamento.Config;
using Ecoporto.Posicionamento.Dados.Interfaces;
using Ecoporto.Posicionamento.Models;
using Oracle.ManagedDataAccess.Client;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Ecoporto.Posicionamento.Dados.Repositorios
{
    public class MotivosRepositorio : IMotivosRepositorio
    {
        public IEnumerable<Motivo> ObterMotivos()
        {
            using (OracleConnection con = new OracleConnection(AppConfig.StringConexao()))
            {
                return con.Query<Motivo>(@"SELECT Code As Id, DESCR As Descricao FROM SGIPA.TB_MOTIVO_POSICAO WHERE NVL(FLAG_OP, 0) = 1 ORDER BY DESCR");
            }
        }

        public Motivo ObterMotivoPorId(int id)
        {
            using (OracleConnection con = new OracleConnection(AppConfig.StringConexao()))
            {
                var parametros = new DynamicParameters();
                parametros.Add(name: "Id", value: id, direction: ParameterDirection.Input);

                return con.Query<Motivo>(@"SELECT Code As Id, DESCR As Descricao FROM SGIPA.TB_MOTIVO_POSICAO Code = :Id", parametros).FirstOrDefault();
            }
        }

        public bool ExigeViagem(int motivoId)
        {
            using (OracleConnection con = new OracleConnection(AppConfig.StringConexao()))
            {
                var parametros = new DynamicParameters();
                parametros.Add(name: "MotivoId", value: motivoId, direction: ParameterDirection.Input);

                return con.Query<bool>(@"SELECT Code FROM SGIPA.TB_MOTIVO_POSICAO WHERE FLAG_MUDA_QUADRA = 1 AND CODE = :MotivoId", parametros).Any();
            }
        }
    }
}