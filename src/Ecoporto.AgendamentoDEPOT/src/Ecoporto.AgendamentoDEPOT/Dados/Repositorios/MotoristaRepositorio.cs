using Dapper;
using Ecoporto.AgendamentoDEPOT.Config;
using Ecoporto.AgendamentoDEPOT.Dados.Interfaces;
using Ecoporto.AgendamentoDEPOT.Models;
using Oracle.ManagedDataAccess.Client;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Ecoporto.AgendamentoDEPOT.Dados.Repositorios
{
    public class MotoristaRepositorio : IMotoristaRepositorio
    {
        public int Cadastrar(Motorista motorista)
        {
            using (OracleConnection con = new OracleConnection(AppConfig.StringConexao()))
            {
                var parametros = new DynamicParameters();

                parametros.Add(name: "TransportadoraId", value: motorista.TransportadoraId, direction: ParameterDirection.Input);
                parametros.Add(name: "Nome", value: motorista.Nome, direction: ParameterDirection.Input);
                parametros.Add(name: "CNH", value: motorista.CNH, direction: ParameterDirection.Input);
                parametros.Add(name: "ValidadeCNH", value: motorista.ValidadeCNH, direction: ParameterDirection.Input);
                parametros.Add(name: "RG", value: motorista.RG, direction: ParameterDirection.Input);
                parametros.Add(name: "CPF", value: motorista.CPF, direction: ParameterDirection.Input);
                parametros.Add(name: "Celular", value: motorista.Celular, direction: ParameterDirection.Input);
                parametros.Add(name: "Nextel", value: motorista.Nextel, direction: ParameterDirection.Input);
                parametros.Add(name: "MOP", value: motorista.MOP, direction: ParameterDirection.Input);

                parametros.Add(name: "Id", dbType: DbType.Int32, direction: ParameterDirection.Output);

                con.Execute(@"
                        INSERT INTO OPERADOR.TB_AG_MOTORISTAS
                            ( 
                                AUTONUM,
                                ID_TRANSPORTADORA,
                                NOME,
                                CNH,
                                VALIDADE_CNH,
                                RG,
                                CPF,
                                CELULAR,
                                NEXTEL,
                                NUMERO_MOP
                            ) VALUES ( 
                                OPERADOR.SEQ_AG_MOTORISTAS.NEXTVAL, 
                                :TransportadoraId,
                                :Nome,
                                :CNH,
                                :ValidadeCNH,
                                :RG,
                                :CPF,
                                :Celular,
                                :Nextel,
                                :MOP
                            ) RETURNING AUTONUM INTO :Id", parametros);

                return parametros.Get<int>("Id");
            }
        }

        public void Atualizar(Motorista motorista)
        {
            using (OracleConnection con = new OracleConnection(AppConfig.StringConexao()))
            {
                var parametros = new DynamicParameters();
                
                parametros.Add(name: "Nome", value: motorista.Nome, direction: ParameterDirection.Input);
                parametros.Add(name: "CNH", value: motorista.CNH, direction: ParameterDirection.Input);
                parametros.Add(name: "ValidadeCNH", value: motorista.ValidadeCNH, direction: ParameterDirection.Input);
                parametros.Add(name: "RG", value: motorista.RG, direction: ParameterDirection.Input);
                parametros.Add(name: "CPF", value: motorista.CPF, direction: ParameterDirection.Input);
                parametros.Add(name: "Celular", value: motorista.Celular, direction: ParameterDirection.Input);
                parametros.Add(name: "Nextel", value: motorista.Nextel, direction: ParameterDirection.Input);
                parametros.Add(name: "MOP", value: motorista.MOP, direction: ParameterDirection.Input);
                parametros.Add(name: "Id", value: motorista.Id, direction: ParameterDirection.Input);

                con.Execute(@"
                        UPDATE OPERADOR.TB_AG_MOTORISTAS
                            SET 
                                NOME = :Nome,
                                CNH = :CNH,
                                VALIDADE_CNH = :ValidadeCNH,
                                RG = :RG,
                                CPF = :CPF,
                                CELULAR = :Celular,
                                NEXTEL = :Nextel,
                                NUMERO_MOP = :MOP,
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

                con.Execute(@"DELETE FROM OPERADOR.TB_AG_MOTORISTAS WHERE AUTONUM = :Id", parametros);
            }
        }

        public IEnumerable<Motorista> ObterMotoristas(int transportadoraId)
        {
            using (OracleConnection con = new OracleConnection(AppConfig.StringConexao()))
            {
                var parametros = new DynamicParameters();
                parametros.Add(name: "TransportadoraId", value: transportadoraId, direction: ParameterDirection.Input);

                return con.Query<Motorista>(@"
                    SELECT
                        A.AUTONUM As Id,
                        A.ID_TRANSPORTADORA As TransportadoraId,
                        A.NOME,
                        A.CNH,
                        A.VALIDADE_CNH As ValidadeCNH,
                        A.RG,
                        A.CPF,
                        A.Celular,
                        A.Nextel,
                        A.NUMERO_MOP As MOP
                    FROM
                        OPERADOR.TB_AG_MOTORISTAS A
                  
            WHERE
                    ID_TRANSPORTADORA = :TransportadoraId
                    
                    ORDER BY
                        VALIDADE_CNH DESC", parametros);
            }
        }

        public IEnumerable<Motorista> ObterUltimos5MotoristasAgendados(int transportadoraId)
        {
            using (OracleConnection con = new OracleConnection(AppConfig.StringConexao()))
            {
                var parametros = new DynamicParameters();
                parametros.Add(name: "TransportadoraId", value: transportadoraId, direction: ParameterDirection.Input);

                return con.Query<Motorista>(@"
                    SELECT 
                        Id,    
                        NOME,
                        CNH,
                        NOME || ' - ' || CNH As Descricao
                    FROM
                        (
                            SELECT
                                DISTINCT
                                    A.AUTONUM As Id,
                                    A.ID_TRANSPORTADORA As TransportadoraId,
                                    A.NOME,
                                    A.CNH
                            FROM
                                OPERADOR.TB_AG_MOTORISTAS A
                            INNER JOIN
                                OPERADOR.TB_AGENDAMENTO_TRA B ON A.AUTONUM = B.AUTONUM_MOTORISTA AND A.ID_TRANSPORTADORA = B.AUTONUM_TRANSPORTADORA
                            WHERE
                                A.ID_TRANSPORTADORA = :TransportadoraId
                            AND
                                ROWNUM <= 5

                            UNION

                            SELECT
                                DISTINCT
                                    A.AUTONUM As Id,
                                    A.ID_TRANSPORTADORA As TransportadoraId,
                                    A.NOME,
                                    A.CNH
                            FROM
                                OPERADOR.TB_AG_MOTORISTAS A
                            INNER JOIN
                                OPERADOR.TB_AGENDAMENTO_DEPOT B ON A.AUTONUM = B.AUTONUM_MOTORISTA AND A.ID_TRANSPORTADORA = B.AUTONUM_TRANSPORTADORA
                            WHERE
                                A.ID_TRANSPORTADORA = :TransportadoraId
                            AND
                                ROWNUM <= 5                           
                        ) ORDER BY
                            Descricao", parametros);
            }
        }

        public IEnumerable<Motorista> ObterMotoristasPorNomeOuCNH(string descricao, int transportadoraId)
        {
            using (OracleConnection con = new OracleConnection(AppConfig.StringConexao()))
            {
                var criterioDescricao = "%" + descricao.ToUpper() + "%";

                var parametros = new DynamicParameters();

                parametros.Add(name: "Nome", value: criterioDescricao, direction: ParameterDirection.Input);
                parametros.Add(name: "CNH", value: criterioDescricao, direction: ParameterDirection.Input);
                parametros.Add(name: "TransportadoraId", value: transportadoraId, direction: ParameterDirection.Input);

                return con.Query<Motorista>(@"
                    SELECT
                        A.AUTONUM As Id,
                        A.ID_TRANSPORTADORA As TransportadoraId,
                        A.NOME,
                        A.CNH,
                        A.VALIDADE_CNH As ValidadeCNH,
                        A.RG,
                        A.CPF,
                        A.Celular,
                        A.Nextel,
                        A.NUMERO_MOP As MOP,
                        NVL(B.FLAG_INATIVO, 0) As INATIVO
                    FROM
                        OPERADOR.TB_AG_MOTORISTAS A
                    LEFT JOIN
                        OPERADOR.TB_MOTORISTAS B ON REPLACE(REPLACE(A.CPF,'.',''),'-','') = REPLACE(REPLACE(B.CPF,'.',''),'-','')
                    WHERE
                        (A.NOME LIKE :Nome OR A.CNH LIKE :CNH)
                    AND
                        A.ID_TRANSPORTADORA = :TransportadoraId
                    AND 
                        ROWNUM < 10
                    ORDER BY
                        A.VALIDADE_CNH DESC", parametros);
            }
        }

        public Motorista ObterMotoristaPorId(int id)
        {
            using (OracleConnection con = new OracleConnection(AppConfig.StringConexao()))
            {
                var parametros = new DynamicParameters();
                parametros.Add(name: "Id", value: id, direction: ParameterDirection.Input);

                return con.Query<Motorista>(@"
                    SELECT
                        A.AUTONUM As Id,
                        A.ID_TRANSPORTADORA As TransportadoraId,
                        A.NOME,
                        A.CNH,
                        A.VALIDADE_CNH As ValidadeCNH,
                        A.RG,
                        A.CPF,
                        A.Celular,
                        A.Nextel,
                        A.NUMERO_MOP As MOP,
                        A.DT_ULTIMA_ATUALIZACAO As UltimaAlteracao,
                        NVL(B.FLAG_INATIVO, 0) As INATIVO
                    FROM
                        OPERADOR.TB_AG_MOTORISTAS A
                    LEFT JOIN
                        OPERADOR.TB_MOTORISTAS B ON REPLACE(REPLACE(A.CPF,'.',''),'-','') = REPLACE(REPLACE(B.CPF,'.',''),'-','')
                    WHERE
                        A.AUTONUM = :Id", parametros).FirstOrDefault();
            }
        }

        public Motorista ObterMotoristaPorCNH(string cnh, int transportadoraId)
        {
            using (OracleConnection con = new OracleConnection(AppConfig.StringConexao()))
            {
                var parametros = new DynamicParameters();

                parametros.Add(name: "CNH", value: cnh, direction: ParameterDirection.Input);
                parametros.Add(name: "TransportadoraId", value: transportadoraId, direction: ParameterDirection.Input);

                return con.Query<Motorista>(@"
                    SELECT
                        AUTONUM As Id,
                        ID_TRANSPORTADORA As TransportadoraId,
                        NOME,
                        CNH,
                        VALIDADE_CNH As ValidadeCNH,
                        RG,
                        CPF,
                        Celular,
                        Nextel,
                        NUMERO_MOP As MOP
                    FROM
                        OPERADOR.TB_AG_MOTORISTAS
                    WHERE
                        CNH = :CNH
                    AND
                        ID_TRANSPORTADORA = :TransportadoraId", parametros).FirstOrDefault();
            }
        }

        public Motorista ObterMotoristaPorCPF(string cpf, int transportadoraId)
        {
            using (OracleConnection con = new OracleConnection(AppConfig.StringConexao()))
            {
                var parametros = new DynamicParameters();

                parametros.Add(name: "CPF", value: cpf, direction: ParameterDirection.Input);
                parametros.Add(name: "TransportadoraId", value: transportadoraId, direction: ParameterDirection.Input);

                return con.Query<Motorista>(@"
                    SELECT
                        AUTONUM As Id,
                        ID_TRANSPORTADORA As TransportadoraId,
                        NOME,
                        CNH,
                        VALIDADE_CNH As ValidadeCNH,
                        RG,
                        CPF,
                        Celular,
                        Nextel,
                        NUMERO_MOP As MOP
                    FROM
                        OPERADOR.TB_AG_MOTORISTAS
                    WHERE
                        CPF = :CPF
                    AND
                        ID_TRANSPORTADORA = :TransportadoraId", parametros).FirstOrDefault();
            }
        }
    }
}