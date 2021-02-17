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
    public class VinculoRepositorio : IVinculoTRARepositorio, IVinculoDEPOTRepositorio
    {
        private readonly string _tabela = string.Empty;
        private readonly string _sequencia = string.Empty;
        private readonly TipoVinculo _tipoVinculo;

        public VinculoRepositorio(TipoVinculo tipoVinculo)
        {
            if (tipoVinculo == TipoVinculo.TRA)
            {
                _tabela = "TB_TRANSPORTE_RECINTO";
                _sequencia = "SEQ_TRANSPORTE_RECINTO";
            }
            else
            {
                _tabela = "TB_TRANSPORTE_DEPOT";
                _sequencia = "SEQ_TRANSPORTE_DEPOT";
            }

            _tipoVinculo = tipoVinculo;
        }

        public IEnumerable<TransportadoraRecintoDTO> ObterVinculos(int pagina, int registrosPorPagina, string orderBy, out int totalFiltro, int recintoId, string filtro)
        {
            using (OracleConnection con = new OracleConnection(AppConfig.StringConexao()))
            {
                var parametros = new DynamicParameters();

                parametros.Add(name: "RecintoId", value: recintoId, direction: ParameterDirection.Input);

                var filtroSQL = string.Empty;

                if (!string.IsNullOrEmpty(filtro))
                {
                    parametros.Add(name: "Filtro", value: $"%{filtro.ToUpper()}%", direction: ParameterDirection.Input);
                    filtroSQL += " AND (UPPER(B.RAZAO) LIKE :Filtro OR B.CGC LIKE :Filtro) ";
                }

                var sql = $@"
                    SELECT * FROM (
                        SELECT Vinculos.*, ROWNUM row_num
                            FROM (
                                SELECT
                                    A.AUTONUM As Id,
                                    A.TRANSPORTADORA As TransportadoraId,
                                    B.CGC As CNPJ,
                                    B.RAZAO As RazaoSocial,
                                    A.FLAG_ATIVO As Ativo,
                                    count(*) over() TotalLinhas
                                FROM
                                    OPERADOR.{_tabela} A
                                INNER JOIN
                                    OPERADOR.TB_CAD_TRANSPORTADORAS B ON A.TRANSPORTADORA = B.AUTONUM
                                WHERE
                                    A.RECINTO = :RecintoId {filtroSQL}
                                {orderBy}) Vinculos
                            WHERE ROWNUM < (({pagina} * {registrosPorPagina}) + 1)) 
                        WHERE row_num >= ((({pagina} - 1) * {registrosPorPagina}) + 1)";


                var query = con.Query<TransportadoraRecintoDTO>(sql, parametros);

                totalFiltro = query.Select(c => c.TotalLinhas).FirstOrDefault();

                return query;
            }
        }

        public int ObterTotalVinculos(int recintoId)
        {
            using (OracleConnection con = new OracleConnection(AppConfig.StringConexao()))
            {
                var parametros = new DynamicParameters();
                parametros.Add(name: "RecintoId", value: recintoId, direction: ParameterDirection.Input);

                return con.Query<int>($@"SELECT COUNT(1) FROM OPERADOR.{_tabela} WHERE RECINTO = :RecintoId", parametros).Single();
            }
        }

        public void Bloquear(int id)
        {
            using (OracleConnection con = new OracleConnection(AppConfig.StringConexao()))
            {
                var parametros = new DynamicParameters();
                parametros.Add(name: "Id", value: id, direction: ParameterDirection.Input);

                con.Execute($"UPDATE OPERADOR.{_tabela} SET FLAG_ATIVO = 0 WHERE AUTONUM = :Id", parametros);
            }
        }

        public void Habilitar(int id)
        {
            using (OracleConnection con = new OracleConnection(AppConfig.StringConexao()))
            {
                var parametros = new DynamicParameters();
                parametros.Add(name: "Id", value: id, direction: ParameterDirection.Input);

                con.Execute($"UPDATE OPERADOR.{_tabela} SET FLAG_ATIVO = 1 WHERE AUTONUM = :Id", parametros);
            }
        }

        public TransportadoraRecintoDTO ObterVinculoPorId(int id)
        {
            using (OracleConnection con = new OracleConnection(AppConfig.StringConexao()))
            {
                var parametros = new DynamicParameters();
                parametros.Add(name: "Id", value: id, direction: ParameterDirection.Input);

                return con.Query<TransportadoraRecintoDTO>($"SELECT AUTONUM As Id, TRANSPORTADORA As TransportadoraId, RECINTO AS RecintoId FROM OPERADOR.{_tabela} WHERE AUTONUM = :Id", parametros).FirstOrDefault();
            }
        }

        public TransportadoraRecintoDTO ObterVinculoPorTransportadoraERecinto(int transportadoraId, int recintoId)
        {
            using (OracleConnection con = new OracleConnection(AppConfig.StringConexao()))
            {
                var parametros = new DynamicParameters();

                parametros.Add(name: "TransportadoraId", value: transportadoraId, direction: ParameterDirection.Input);
                parametros.Add(name: "RecintoId", value: recintoId, direction: ParameterDirection.Input);

                return con.Query<TransportadoraRecintoDTO>($"SELECT AUTONUM As Id, TRANSPORTADORA As TransportadoraId, RECINTO AS RecintoId FROM OPERADOR.{_tabela} WHERE TRANSPORTADORA = :TransportadoraId AND RECINTO = :RecintoId", parametros).FirstOrDefault();
            }
        }

        public void VincularTransportadora(int tansportadoraId, int recintoId)
        {
            using (OracleConnection con = new OracleConnection(AppConfig.StringConexao()))
            {
                var parametros = new DynamicParameters();

                parametros.Add(name: "TansportadoraId", value: tansportadoraId, direction: ParameterDirection.Input);
                parametros.Add(name: "RecintoId", value: recintoId, direction: ParameterDirection.Input);

                con.Execute($"INSERT INTO OPERADOR.{_tabela} (AUTONUM, TRANSPORTADORA, RECINTO, FLAG_ATIVO) VALUES (OPERADOR.{_sequencia}.NEXTVAL, :TansportadoraId, :RecintoId, 1)", parametros);
            }
        }

        public void ExcluirVinculo(int id)
        {
            using (OracleConnection con = new OracleConnection(AppConfig.StringConexao()))
            {
                var parametros = new DynamicParameters();
                parametros.Add(name: "Id", value: id, direction: ParameterDirection.Input);

                con.Execute($"DELETE FROM OPERADOR.{_tabela} WHERE AUTONUM = :Id", parametros);
            }
        }

        public bool ExisteAgendamentoNoRecintoTRA(int recintoId, int transportadoraId)
        {
            using (OracleConnection con = new OracleConnection(AppConfig.StringConexao()))
            {
                var parametros = new DynamicParameters();

                parametros.Add(name: "RecintoId", value: recintoId, direction: ParameterDirection.Input);
                parametros.Add(name: "TransportadoraId", value: transportadoraId, direction: ParameterDirection.Input);

                return con.Query<bool>("SELECT AUTONUM FROM OPERADOR.TB_AGENDAMENTO_TRA WHERE AUTONUM_RECINTO = :RecintoId AND AUTONUM_TRANSPORTADORA = :TransportadoraId", parametros).Any();
            }
        }

        public bool ExisteAgendamentoNoRecintoDEPOT(int recintoId, int transportadoraId)
        {
            using (OracleConnection con = new OracleConnection(AppConfig.StringConexao()))
            {
                var parametros = new DynamicParameters();

                parametros.Add(name: "RecintoId", value: recintoId, direction: ParameterDirection.Input);
                parametros.Add(name: "TransportadoraId", value: transportadoraId, direction: ParameterDirection.Input);

                return con.Query<bool>("SELECT A.AUTONUM FROM OPERADOR.TB_AGENDAMENTO_DEPOT A INNER JOIN OPERADOR.VW_NAVIS_DEPOT B ON A.RECINTO = B.ID WHERE B.AUTONUM = :RecintoId AND AUTONUM_TRANSPORTADORA = :TransportadoraId", parametros).Any();
            }
        }
    }
}