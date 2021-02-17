using Dapper;
using Ecoporto.AgendamentoCS.Config;
using Ecoporto.AgendamentoCS.Dados.Interfaces;
using Ecoporto.AgendamentoCS.Models;
using Oracle.ManagedDataAccess.Client;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Ecoporto.AgendamentoCS.Dados.Repositorios
{
    public class VeiculoRepositorio : IVeiculoRepositorio
    {
        public int Cadastrar(Veiculo veiculo)
        {
            using (OracleConnection con = new OracleConnection(AppConfig.StringConexao()))
            {
                var parametros = new DynamicParameters();

                parametros.Add(name: "TransportadoraId", value: veiculo.TransportadoraId, direction: ParameterDirection.Input);
                parametros.Add(name: "TipoCaminhaoId", value: veiculo.TipoCaminhaoId, direction: ParameterDirection.Input);
                parametros.Add(name: "Cavalo", value: veiculo.Cavalo, direction: ParameterDirection.Input);
                parametros.Add(name: "Carreta", value: veiculo.Carreta, direction: ParameterDirection.Input);
                parametros.Add(name: "Chassi", value: veiculo.Chassi, direction: ParameterDirection.Input);
                parametros.Add(name: "Renavam", value: veiculo.Renavam, direction: ParameterDirection.Input);
                parametros.Add(name: "Tara", value: veiculo.Tara, direction: ParameterDirection.Input);
                parametros.Add(name: "Modelo", value: veiculo.Modelo, direction: ParameterDirection.Input);
                parametros.Add(name: "Cor", value: veiculo.Cor, direction: ParameterDirection.Input);

                parametros.Add(name: "Id", dbType: DbType.Int32, direction: ParameterDirection.Output);

                con.Execute(@"
                        INSERT INTO OPERADOR.TB_AG_VEICULOS
                            ( 
                                AUTONUM,
                                ID_TRANSPORTADORA,
                                ID_TIPO_CAMINHAO,
                                PLACA_CAVALO,
                                PLACA_CARRETA,
                                CHASSI,
                                RENAVAM,
                                TARA,
                                MODELO,
                                COR,
                                DT_CADASTRO
                            ) VALUES ( 
                                OPERADOR.SEQ_AG_VEICULOS.NEXTVAL, 
                                :TransportadoraId,
                                :TipoCaminhaoId,
                                :Cavalo,
                                :Carreta,
                                :Chassi,
                                :Renavam,
                                :Tara,
                                :Modelo,
                                :Cor,
                                SYSDATE
                            ) RETURNING AUTONUM INTO :Id", parametros);

                return parametros.Get<int>("Id");
            }
        }

        public void Atualizar(Veiculo veiculo)
        {
            using (OracleConnection con = new OracleConnection(AppConfig.StringConexao()))
            {
                var parametros = new DynamicParameters();

                parametros.Add(name: "TransportadoraId", value: veiculo.TransportadoraId, direction: ParameterDirection.Input);
                parametros.Add(name: "TipoCaminhaoId", value: veiculo.TipoCaminhaoId, direction: ParameterDirection.Input);
                parametros.Add(name: "Cavalo", value: veiculo.Cavalo, direction: ParameterDirection.Input);
                parametros.Add(name: "Carreta", value: veiculo.Carreta, direction: ParameterDirection.Input);
                parametros.Add(name: "Chassi", value: veiculo.Chassi, direction: ParameterDirection.Input);
                parametros.Add(name: "Renavam", value: veiculo.Renavam, direction: ParameterDirection.Input);
                parametros.Add(name: "Tara", value: veiculo.Tara, direction: ParameterDirection.Input);
                parametros.Add(name: "Modelo", value: veiculo.Modelo, direction: ParameterDirection.Input);
                parametros.Add(name: "Cor", value: veiculo.Cor, direction: ParameterDirection.Input);
                parametros.Add(name: "Id", value: veiculo.Id, direction: ParameterDirection.Input);

                con.Execute(@"
                        UPDATE OPERADOR.TB_AG_VEICULOS
                            SET 
                                ID_TIPO_CAMINHAO = :TipoCaminhaoId,
                                PLACA_CAVALO = :Cavalo,
                                PLACA_CARRETA = :Carreta,
                                CHASSI = :Chassi,
                                RENAVAM = :Renavam,
                                TARA = :Tara,
                                MODELO = :Modelo,
                                COR = :Cor,
                                DT_ULTIMA_ATUALIZACAO = SYSDATE
                            WHERE
                                AUTONUM = :Id", parametros);
            }
        }

        public void Excluir(int id)
        {
            using (OracleConnection con = new OracleConnection(AppConfig.StringConexao()))
            {
                var parametros = new DynamicParameters();                
                parametros.Add(name: "Id", value: id, direction: ParameterDirection.Input);

                con.Execute(@"DELETE FROM OPERADOR.TB_AG_VEICULOS WHERE AUTONUM = :Id", parametros);
            }
        }

        public IEnumerable<Veiculo> ObterVeiculos(int transportadoraId)
        {
            using (OracleConnection con = new OracleConnection(AppConfig.StringConexao()))
            {
                var parametros = new DynamicParameters();
                parametros.Add(name: "TransportadoraId", value: transportadoraId, direction: ParameterDirection.Input);

                return con.Query<Veiculo>(@"
                    SELECT
                        A.AUTONUM As Id,
                        A.ID_TRANSPORTADORA As TransportadoraId,
                        A.ID_TIPO_CAMINHAO As TipoCaminhaoId,
                        A.PLACA_CAVALO As Cavalo,
                        A.PLACA_CARRETA AS Carreta,
                        A.Chassi,
                        A.Renavam,
                        A.Tara,
                        A.Modelo,
                        A.Cor,
                        B.DESCR As TipoCaminhaoDescricao
                    FROM
                        OPERADOR.TB_AG_VEICULOS A
                    INNER JOIN
                        OPERADOR.TB_TIPOS_CAMINHAO B ON A.ID_TIPO_CAMINHAO = B.AUTONUM
                    WHERE
                        A.ID_TRANSPORTADORA = :TransportadoraId", parametros);
            }
        }

        public IEnumerable<Veiculo> ObterUltimos5VeiculosAgendados(int transportadoraId)
        {
            using (OracleConnection con = new OracleConnection(AppConfig.StringConexao()))
            {
                var parametros = new DynamicParameters();
                parametros.Add(name: "TransportadoraId", value: transportadoraId, direction: ParameterDirection.Input);

                return con.Query<Veiculo>(@"
                    SELECT
                        Id,    
                        Cavalo,
                        Carreta,
                        Cavalo || ' | ' || Carreta As Descricao
                    FROM
                        (
                            SELECT
                                DISTINCT
                                    A.AUTONUM As Id,
                                    A.ID_TRANSPORTADORA As TransportadoraId,
                                    A.ID_TIPO_CAMINHAO As TipoCaminhaoId,
                                    A.PLACA_CAVALO As Cavalo,
                                    A.PLACA_CARRETA AS Carreta,
                                    A.Chassi,
                                    A.Renavam,
                                    A.Tara,
                                    A.Modelo,
                                    A.Cor,
                                    C.DESCR As TipoCaminhaoDescricao
                            FROM
                                OPERADOR.TB_AG_VEICULOS A
                            INNER JOIN
                                OPERADOR.TB_AGENDAMENTO_CS B ON A.AUTONUM = B.AUTONUM_VEICULO AND A.ID_TRANSPORTADORA = B.AUTONUM_TRANSPORTADORA
                            INNER JOIN
                                OPERADOR.TB_TIPOS_CAMINHAO C ON A.ID_TIPO_CAMINHAO = C.AUTONUM
                            WHERE
                                A.ID_TRANSPORTADORA = :TransportadoraId
                        )
                    WHERE
                        ROWNUM <= 5
                    ORDER BY
                        Cavalo", parametros);
            }
        }

        public IEnumerable<Veiculo> ObterVeiculosPorPlaca(string descricao, int transportadoraId)
        {
            using (OracleConnection con = new OracleConnection(AppConfig.StringConexao()))
            {
                var parametros = new DynamicParameters();

                parametros.Add(name: "Placa", value: "%" + descricao.ToUpper() + "%", direction: ParameterDirection.Input);
                parametros.Add(name: "TransportadoraId", value: transportadoraId, direction: ParameterDirection.Input);

                return con.Query<Veiculo>(@"
                    SELECT
                        A.AUTONUM As Id,
                        A.ID_TRANSPORTADORA As TransportadoraId,
                        A.ID_TIPO_CAMINHAO As TipoCaminhaoId,
                        A.PLACA_CAVALO As Cavalo,
                        A.PLACA_CARRETA AS Carreta,
                        A.Chassi,
                        A.Renavam,
                        A.Tara,
                        A.Modelo,
                        A.Cor,
                        B.DESCR As TipoCaminhaoDescricao
                    FROM
                        OPERADOR.TB_AG_VEICULOS A
                    INNER JOIN
                        OPERADOR.TB_TIPOS_CAMINHAO B ON A.ID_TIPO_CAMINHAO = B.AUTONUM                    
                    WHERE
                        (A.PLACA_CAVALO LIKE :Placa OR PLACA_CARRETA LIKE :Placa)
                    AND
                        A.ID_TRANSPORTADORA = :TransportadoraId
                    AND 
                        ROWNUM < 10", parametros);
            }
        }

        public Veiculo ObterVeiculoPorId(int id)
        {
            using (OracleConnection con = new OracleConnection(AppConfig.StringConexao()))
            {
                var parametros = new DynamicParameters();
                parametros.Add(name: "Id", value: id, direction: ParameterDirection.Input);

                return con.Query<Veiculo>(@"
                    SELECT
                        A.AUTONUM As Id,
                        A.ID_TRANSPORTADORA As TransportadoraId,
                        A.ID_TIPO_CAMINHAO As TipoCaminhaoId,
                        A.PLACA_CAVALO As Cavalo,
                        A.PLACA_CARRETA AS Carreta,
                        A.Chassi,
                        A.Renavam,
                        A.Tara,
                        A.Modelo,
                        A.Cor,
                        B.Descr As TipoCaminhaoDescricao
                    FROM
                        OPERADOR.TB_AG_VEICULOS A
                    LEFT JOIN
                        OPERADOR.TB_TIPOS_CAMINHAO B ON A.ID_TIPO_CAMINHAO = B.AUTONUM
                    WHERE
                        A.AUTONUM = :Id", parametros).FirstOrDefault();
            }
        }

        public IEnumerable<TipoVeiculo> ObterTiposVeiculos()
        {
            using (OracleConnection con = new OracleConnection(AppConfig.StringConexao()))
            {
                return con.Query<TipoVeiculo>(@"SELECT AUTONUM As Id, DESCR As Descricao FROM OPERADOR.TB_TIPOS_CAMINHAO WHERE AUTONUM > 0 ORDER BY DESCR");
            }
        }
    }
}