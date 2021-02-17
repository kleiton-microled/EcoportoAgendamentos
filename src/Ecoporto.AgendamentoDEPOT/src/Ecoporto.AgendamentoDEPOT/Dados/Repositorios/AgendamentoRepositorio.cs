using Dapper;
using Ecoporto.AgendamentoDEPOT.Config;
using Ecoporto.AgendamentoDEPOT.Dados.Interfaces;
using Ecoporto.AgendamentoDEPOT.Enums;
using Ecoporto.AgendamentoDEPOT.Extensions;
using Ecoporto.AgendamentoDEPOT.Models;
using Ecoporto.AgendamentoDEPOT.Models.DTO;
using Oracle.ManagedDataAccess.Client;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Ecoporto.AgendamentoDEPOT.Dados.Repositorios
{
    public class AgendamentoRepositorio : IAgendamentoRepositorio
    {
        public int CadastrarAgendamentoTRA(Agendamento agendamento)
        {
            using (OracleConnection con = new OracleConnection(AppConfig.StringConexao()))
            {
                var parametros = new DynamicParameters();

                parametros.Add(name: "MotoristaId", value: agendamento.MotoristaId, direction: ParameterDirection.Input);
                parametros.Add(name: "VeiculoId", value: agendamento.VeiculoId, direction: ParameterDirection.Input);
                parametros.Add(name: "TransportadoraId", value: agendamento.TransportadoraId, direction: ParameterDirection.Input);
                parametros.Add(name: "PeriodoId", value: agendamento.PeriodoId, direction: ParameterDirection.Input);
                parametros.Add(name: "RecintoTRA", value: agendamento.RecintoTRA, direction: ParameterDirection.Input);
                parametros.Add(name: "Quantidade", value: agendamento.Quantidade, direction: ParameterDirection.Input);
                parametros.Add(name: "IMO", value: agendamento.IMO.ToInt(), direction: ParameterDirection.Input);
                parametros.Add(name: "Excesso", value: agendamento.Excesso.ToInt(), direction: ParameterDirection.Input);
                parametros.Add(name: "UsuarioId", value: agendamento.UsuarioId, direction: ParameterDirection.Input);
                parametros.Add(name: "CTE", value: agendamento.CTE, direction: ParameterDirection.Input);
                parametros.Add(name: "TipoOperacao", value: agendamento.TipoOperacao, direction: ParameterDirection.Input);

                parametros.Add(name: "Id", dbType: DbType.Int32, direction: ParameterDirection.Output);

                con.Execute(@"
                        INSERT INTO OPERADOR.TB_AGENDAMENTO_TRA
                            ( 
                                AUTONUM,
                                AUTONUM_MOTORISTA,
                                AUTONUM_VEICULO,                                
                                AUTONUM_TRANSPORTADORA,
                                AUTONUM_PERIODO,
                                AUTONUM_USUARIO,
                                AUTONUM_RECINTO,
                                QUANTIDADE,
                                IMO,
                                EXCESSO,
                                PROTOCOLO,
                                ANO_PROTOCOLO,
                                CTE,
                                TIPO_OPERACAO
                            ) VALUES ( 
                                OPERADOR.SEQ_AGENDAMENTO_TRA.NEXTVAL, 
                                :MotoristaId,
                                :VeiculoId,
                                :TransportadoraId,
                                :PeriodoId,
                                :UsuarioId,
                                :RecintoTRA,
                                :Quantidade,
                                :IMO,
                                :Excesso,
                                OPERADOR.SEQ_PROTOCOLO_AG_GERAL.NEXTVAL,
                                TO_CHAR(SYSDATE, 'YYYY'),
                                :CTE,
                                :TipoOperacao
                            ) RETURNING AUTONUM INTO :Id", parametros);

                var id = parametros.Get<int>("Id");

                return id;
            }
        }

        public int CadastrarAgendamentoDEPOT(Agendamento agendamento)
        {
            using (OracleConnection con = new OracleConnection(AppConfig.StringConexao()))
            {
                var parametros = new DynamicParameters();

                parametros.Add(name: "MotoristaId", value: agendamento.MotoristaId, direction: ParameterDirection.Input);
                parametros.Add(name: "VeiculoId", value: agendamento.VeiculoId, direction: ParameterDirection.Input);
                parametros.Add(name: "TransportadoraId", value: agendamento.TransportadoraId, direction: ParameterDirection.Input);
                parametros.Add(name: "PeriodoId", value: agendamento.PeriodoId, direction: ParameterDirection.Input);
                parametros.Add(name: "RecintoDEPOT", value: agendamento.RecintoDEPOT, direction: ParameterDirection.Input);
                parametros.Add(name: "Quantidade", value: agendamento.Quantidade, direction: ParameterDirection.Input);
                parametros.Add(name: "IMO", value: agendamento.IMO.ToInt(), direction: ParameterDirection.Input);
                parametros.Add(name: "Excesso", value: agendamento.Excesso.ToInt(), direction: ParameterDirection.Input);
                parametros.Add(name: "UsuarioId", value: agendamento.UsuarioId, direction: ParameterDirection.Input);
                parametros.Add(name: "CTE", value: agendamento.CTE, direction: ParameterDirection.Input);
                parametros.Add(name: "TipoOperacao", value: agendamento.TipoOperacao, direction: ParameterDirection.Input);

                parametros.Add(name: "Id", dbType: DbType.Int32, direction: ParameterDirection.Output);


                con.Execute(@"
                        INSERT INTO OPERADOR.TB_AGENDAMENTO_DEPOT
                            ( 
                                AUTONUM,
                                AUTONUM_MOTORISTA,
                                AUTONUM_VEICULO,                                
                                AUTONUM_TRANSPORTADORA,
                                AUTONUM_PERIODO,
                                AUTONUM_USUARIO,
                                RECINTO,
                                QUANTIDADE,
                                PROTOCOLO,
                                ANO_PROTOCOLO,
                                CTE,
                                TIPO_OPERACAO
                            ) VALUES ( 
                                OPERADOR.SEQ_AGENDAMENTO_DEPOT.NEXTVAL, 
                                :MotoristaId,
                                :VeiculoId,
                                :TransportadoraId,
                                :PeriodoId,
                                :UsuarioId,
                                :RecintoDEPOT,
                                :Quantidade,
                                OPERADOR.SEQ_PROTOCOLO_AG_GERAL.NEXTVAL,
                                TO_CHAR(SYSDATE, 'YYYY'),
                                :CTE,
                                :TipoOperacao
                            ) RETURNING AUTONUM INTO :Id", parametros);


                var id = parametros.Get<int>("Id");

                return id;
            }
        }

        public void AtualizarAgendamentoTRA(Agendamento agendamento)
        {
            using (OracleConnection con = new OracleConnection(AppConfig.StringConexao()))
            {
                var parametros = new DynamicParameters();

                parametros.Add(name: "MotoristaId", value: agendamento.MotoristaId, direction: ParameterDirection.Input);
                parametros.Add(name: "VeiculoId", value: agendamento.VeiculoId, direction: ParameterDirection.Input);
                parametros.Add(name: "PeriodoId", value: agendamento.PeriodoId, direction: ParameterDirection.Input);
                parametros.Add(name: "CTE", value: agendamento.CTE, direction: ParameterDirection.Input);
                parametros.Add(name: "RecintoTRA", value: agendamento.RecintoTRA, direction: ParameterDirection.Input);
                parametros.Add(name: "Quantidade", value: agendamento.Quantidade, direction: ParameterDirection.Input);
                parametros.Add(name: "IMO", value: agendamento.IMO.ToInt(), direction: ParameterDirection.Input);
                parametros.Add(name: "Excesso", value: agendamento.Excesso.ToInt(), direction: ParameterDirection.Input);
                parametros.Add(name: "Id", value: agendamento.Id, direction: ParameterDirection.Input);

                con.Execute(@"UPDATE OPERADOR.TB_AGENDAMENTO_TRA SET AUTONUM_MOTORISTA = :MotoristaId, AUTONUM_VEICULO = :VeiculoId, AUTONUM_PERIODO = :PeriodoId, CTE = :CTE, AUTONUM_RECINTO = :RecintoTRA, QUANTIDADE = :Quantidade, IMO = :IMO, Excesso = :Excesso WHERE AUTONUM = :Id", parametros);
            }
        }

        public void AtualizarAgendamentoDEPOT(Agendamento agendamento)
        {
            using (OracleConnection con = new OracleConnection(AppConfig.StringConexao()))
            {
                var parametros = new DynamicParameters();

                parametros.Add(name: "MotoristaId", value: agendamento.MotoristaId, direction: ParameterDirection.Input);
                parametros.Add(name: "VeiculoId", value: agendamento.VeiculoId, direction: ParameterDirection.Input);
                parametros.Add(name: "PeriodoId", value: agendamento.PeriodoId, direction: ParameterDirection.Input);
                parametros.Add(name: "CTE", value: agendamento.CTE, direction: ParameterDirection.Input);
                parametros.Add(name: "RecintoDEPOT", value: agendamento.RecintoDEPOT, direction: ParameterDirection.Input);
                parametros.Add(name: "Quantidade", value: agendamento.Quantidade, direction: ParameterDirection.Input);
                parametros.Add(name: "IMO", value: agendamento.IMO.ToInt(), direction: ParameterDirection.Input);
                parametros.Add(name: "Excesso", value: agendamento.Excesso.ToInt(), direction: ParameterDirection.Input);
                parametros.Add(name: "Id", value: agendamento.Id, direction: ParameterDirection.Input);

                con.Execute(@"UPDATE OPERADOR.TB_AGENDAMENTO_DEPOT SET AUTONUM_MOTORISTA = :MotoristaId, AUTONUM_VEICULO = :VeiculoId, AUTONUM_PERIODO = :PeriodoId, CTE = :CTE, RECINTO = :RecintoDEPOT, QUANTIDADE = :Quantidade WHERE AUTONUM = :Id", parametros);
            }
        }

        public void AtualizarProtocoloImpresso(int id, TipoAgendamento tipoAgendamento)
        {
            using (OracleConnection con = new OracleConnection(AppConfig.StringConexao()))
            {
                var parametros = new DynamicParameters();
                parametros.Add(name: "AgendamentoId", value: id, direction: ParameterDirection.Input);

                if (tipoAgendamento == TipoAgendamento.TRA)               
                    con.Execute(@"UPDATE OPERADOR.TB_AGENDAMENTO_TRA SET FLAG_IMPRESSO = 1 WHERE AUTONUM = :AgendamentoId", parametros);
                else
                    con.Execute(@"UPDATE OPERADOR.TB_AGENDAMENTO_DEPOT SET FLAG_IMPRESSO = 1 WHERE AUTONUM = :AgendamentoId", parametros);
            }
        }

        public void Excluir(int id, TipoAgendamento tipoAgendamento)
        {
            using (OracleConnection con = new OracleConnection(AppConfig.StringConexao()))
            {
                con.Open();

                using (var transaction = con.BeginTransaction())
                {
                    var parametros = new DynamicParameters();
                    parametros.Add(name: "Id", value: id, direction: ParameterDirection.Input);

                    if (tipoAgendamento == TipoAgendamento.TRA)
                    {
                        con.Execute(@"DELETE FROM OPERADOR.TB_AGENDAMENTO_TRA WHERE AUTONUM = :Id", parametros, transaction);
                    }
                    else
                    {
                        con.Execute(@"DELETE FROM OPERADOR.TB_AGENDAMENTO_DEPOT WHERE AUTONUM = :Id", parametros, transaction);
                    }

                    transaction.Commit();
                }
            }
        }

        public IEnumerable<AgendamentoDTO> ObterAgendamentos(int transportadoraId)
        {
            using (OracleConnection con = new OracleConnection(AppConfig.StringConexao()))
            {
                var parametros = new DynamicParameters();
                parametros.Add(name: "TransportadoraId", value: transportadoraId, direction: ParameterDirection.Input);

                return con.Query<AgendamentoDTO>($@"
                    SELECT
                        Id,
                        MotoristaId,
                        MotoristaDescricao,
                        VeiculoId,
                        VeiculoDescricao,
                        TransportadoraId,
                        TransportadoraDescricao,
                        PeriodoId,
                        PeriodoDescricao,
                        Protocolo,
                        AnoProtocolo,
                        UsuarioId,
                        Impresso,
                        DataEntrada,
                        RecintoTRA,
                        RecintoDEPOT,
                        RecintoDescricao,
                        Quantidade,
                        TipoAgendamento
                    FROM
                        (
                            SELECT    
                                A.AUTONUM As Id,
                                B.AUTONUM As MotoristaId,
                                B.NOME As MotoristaDescricao,
                                C.AUTONUM AS VeiculoId,
                                C.PLACA_CAVALO || ' / ' || C.PLACA_CARRETA As VeiculoDescricao,
                                A.AUTONUM As TransportadoraId,
                                D.RAZAO As TransportadoraDescricao,
                                E.AUTONUM_GD_RESERVA AS PeriodoId,
                                TO_CHAR(E.PERIODO_INICIAL, 'DD/MM HH24:MI') || ' - ' || TO_CHAR(E.PERIODO_FINAL, 'HH24:MI') PeriodoDescricao,
                                A.Protocolo,
                                TO_CHAR(A.DATA_HORA, 'YYYY') As AnoProtocolo,
                                A.AUTONUM_USUARIO AS UsuarioId,
                                A.FLAG_IMPRESSO AS Impresso,
                                A.DT_ENTRADA AS DataEntrada,
                                A.AUTONUM_RECINTO As RecintoTRA,
                                NULL As RecintoDEPOT,
                                F.DESCR As RecintoDescricao,
                                A.QUANTIDADE,
                                1 As TipoAgendamento
                            FROM
                                OPERADOR.TB_AGENDAMENTO_TRA A
                            INNER JOIN
                                OPERADOR.TB_AG_MOTORISTAS B ON A.AUTONUM_MOTORISTA = B.AUTONUM
                            INNER JOIN
                                OPERADOR.TB_AG_VEICULOS C ON A.AUTONUM_VEICULO = C.AUTONUM    
                            INNER JOIN
                                OPERADOR.TB_CAD_TRANSPORTADORAS D ON A.AUTONUM_TRANSPORTADORA = D.AUTONUM        
                            INNER JOIN
                                OPERADOR.TB_GD_RESERVA E ON A.AUTONUM_PERIODO = E.AUTONUM_GD_RESERVA
                            INNER JOIN
                                OPERADOR.DTE_TB_RECINTOS F ON A.AUTONUM_RECINTO = F.CODE
                            WHERE
                                A.AUTONUM_TRANSPORTADORA = :TransportadoraId
                            AND
                                A.DATA_HORA >= SYSDATE - 90

                            UNION

                                SELECT    
                                A.AUTONUM As Id,
                                B.AUTONUM As MotoristaId,
                                B.NOME As MotoristaDescricao,
                                C.AUTONUM AS VeiculoId,
                                C.PLACA_CAVALO || ' / ' || C.PLACA_CARRETA As VeiculoDescricao,
                                A.AUTONUM As TransportadoraId,
                                D.RAZAO As TransportadoraDescricao,
                                E.AUTONUM_GD_RESERVA AS PeriodoId,
                                TO_CHAR(E.PERIODO_INICIAL, 'DD/MM HH24:MI') || ' - ' || TO_CHAR(E.PERIODO_FINAL, 'HH24:MI') PeriodoDescricao,
                                A.Protocolo,
                                TO_CHAR(A.DATA_HORA, 'YYYY') As AnoProtocolo,
                                A.AUTONUM_USUARIO AS UsuarioId,
                                A.FLAG_IMPRESSO AS Impresso,
                                A.DT_ENTRADA AS DataEntrada,
                                NULL As RecintoTRA,
                                A.RECINTO As RecintoDEPOT,
                                F.NOME As RecintoDescricao,
                                A.QUANTIDADE,
                                2 As TipoAgendamento
                            FROM
                                OPERADOR.TB_AGENDAMENTO_DEPOT A
                            INNER JOIN
                                OPERADOR.TB_AG_MOTORISTAS B ON A.AUTONUM_MOTORISTA = B.AUTONUM
                            INNER JOIN
                                OPERADOR.TB_AG_VEICULOS C ON A.AUTONUM_VEICULO = C.AUTONUM    
                            INNER JOIN
                                OPERADOR.TB_CAD_TRANSPORTADORAS D ON A.AUTONUM_TRANSPORTADORA = D.AUTONUM        
                            INNER JOIN
                                OPERADOR.TB_GD_RESERVA E ON A.AUTONUM_PERIODO = E.AUTONUM_GD_RESERVA
                            INNER JOIN
                                OPERADOR.VW_NAVIS_DEPOT F ON A.RECINTO = F.ID
                            WHERE
                                A.AUTONUM_TRANSPORTADORA = :TransportadoraId
                            AND
                                A.DATA_HORA >= SYSDATE - 90
                        ) ORDER BY Id", parametros);
            }
        }

        public IEnumerable<AgendamentoDTO> ObterAgendamentosPorPeriodoEVeiculo(int transportadoraId, int periodoId, int veiculoId, TipoAgendamento tipoAgendamento)
        {
            using (OracleConnection con = new OracleConnection(AppConfig.StringConexao()))
            {
                var parametros = new DynamicParameters();

                parametros.Add(name: "TransportadoraId", value: transportadoraId, direction: ParameterDirection.Input);
                parametros.Add(name: "PeriodoId", value: periodoId, direction: ParameterDirection.Input);
                parametros.Add(name: "VeiculoId", value: veiculoId, direction: ParameterDirection.Input);

                var tabela = tipoAgendamento == TipoAgendamento.TRA
                    ? "TB_AGENDAMENTO_TRA"
                    : "TB_AGENDAMENTO_DEPOT";

                return con.Query<AgendamentoDTO>($@"
                    SELECT    
                        A.AUTONUM As Id,
                        B.AUTONUM As MotoristaId,
                        B.NOME As MotoristaDescricao,
                        C.AUTONUM AS VeiculoId,
                        C.PLACA_CAVALO || ' / ' || C.PLACA_CARRETA As VeiculoDescricao,
                        A.AUTONUM As TransportadoraId,
                        D.RAZAO As TransportadoraDescricao,
                        E.AUTONUM_GD_RESERVA AS PeriodoId,
                        TO_CHAR(E.PERIODO_INICIAL, 'DD/MM HH24:MI') || ' - ' || TO_CHAR(E.PERIODO_FINAL, 'HH24:MI') PeriodoDescricao,
                        A.Protocolo,
                        TO_CHAR(A.DATA_HORA, 'YYYY') As AnoProtocolo,
                        A.AUTONUM_USUARIO AS UsuarioId,
                        A.FLAG_IMPRESSO AS Impresso,
                        A.DT_ENTRADA AS DataEntrada
                    FROM
                        OPERADOR.{tabela} A
                    INNER JOIN
                        OPERADOR.TB_AG_MOTORISTAS B ON A.AUTONUM_MOTORISTA = B.AUTONUM
                    INNER JOIN
                        OPERADOR.TB_AG_VEICULOS C ON A.AUTONUM_VEICULO = C.AUTONUM    
                    INNER JOIN
                        OPERADOR.TB_CAD_TRANSPORTADORAS D ON A.AUTONUM_TRANSPORTADORA = D.AUTONUM        
                    INNER JOIN
                        OPERADOR.TB_GD_RESERVA E ON A.AUTONUM_PERIODO = E.AUTONUM_GD_RESERVA
                    WHERE
                        A.AUTONUM_TRANSPORTADORA = :TransportadoraId
                    AND
                        A.AUTONUM_VEICULO = :VeiculoId
                    AND
                        A.AUTONUM_PERIODO = :PeriodoId
                    ORDER BY
                        A.AUTONUM DESC", parametros);
            }
        }

        public AgendamentoDTO ObterDetalhesAgendamento(int id, TipoAgendamento tipoAgendamento)
        {
            using (OracleConnection con = new OracleConnection(AppConfig.StringConexao()))
            {
                var parametros = new DynamicParameters();
                parametros.Add(name: "Id", value: id, direction: ParameterDirection.Input);

                if (tipoAgendamento == TipoAgendamento.TRA)
                {
                    return con.Query<AgendamentoDTO>($@"
                        SELECT    
                            A.AUTONUM As Id,
                            A.TIPO_OPERACAO As TipoOperacao,
                            B.AUTONUM As MotoristaId,
                            B.NOME || ' ' || B.CNH As MotoristaDescricao,
                            C.AUTONUM AS VeiculoId,
                            'Cavalo: ' || C.PLACA_CAVALO || ' | Carreta: ' || C.PLACA_CARRETA As VeiculoDescricao,
                            D.AUTONUM As TransportadoraId,
                            D.RAZAO As TransportadoraDescricao,
                            E.AUTONUM_GD_RESERVA AS PeriodoId,
                            TO_CHAR(E.PERIODO_INICIAL, 'DD/MM/YYYY HH24:MI') || ' - ' || TO_CHAR(E.PERIODO_FINAL, 'HH24:MI') PeriodoDescricao,
                            A.Protocolo,
                            NVL(A.IMO, 0) As IMO,
                            NVL(A.EXCESSO, 0) As Excesso,
                            A.ANO_PROTOCOLO As AnoProtocolo,
                            A.AUTONUM_USUARIO AS UsuarioId,
                            A.FLAG_IMPRESSO AS Impresso,                        
                            A.DATA_HORA As DataCriacao,                                
                            A.AUTONUM_RECINTO As RecintoTRA,
                            NULL As RecintoDEPOT,
                            F.DESCR As RecintoDescricao,                                
                            A.QUANTIDADE,
                            A.CTE,
                            1 As TipoAgendamento
                        FROM
                            OPERADOR.TB_AGENDAMENTO_TRA A
                        INNER JOIN
                            OPERADOR.TB_AG_MOTORISTAS B ON A.AUTONUM_MOTORISTA = B.AUTONUM
                        INNER JOIN
                            OPERADOR.TB_AG_VEICULOS C ON A.AUTONUM_VEICULO = C.AUTONUM    
                        INNER JOIN
                            OPERADOR.TB_CAD_TRANSPORTADORAS D ON A.AUTONUM_TRANSPORTADORA = D.AUTONUM        
                        INNER JOIN
                            OPERADOR.TB_GD_RESERVA E ON A.AUTONUM_PERIODO = E.AUTONUM_GD_RESERVA
                        INNER JOIN
                            OPERADOR.DTE_TB_RECINTOS F ON A.AUTONUM_RECINTO = F.CODE
                        LEFT JOIN
                            OPERADOR.TB_TIPOS_CAMINHAO G ON C.ID_TIPO_CAMINHAO = G.AUTONUM
                        WHERE
                            A.AUTONUM = :Id", parametros).FirstOrDefault();
                }
                else
                {
                    return con.Query<AgendamentoDTO>($@"
                        SELECT    
                            A.AUTONUM As Id,
                            A.TIPO_OPERACAO As TipoOperacao,
                            B.AUTONUM As MotoristaId,
                            B.NOME || ' ' || B.CNH As MotoristaDescricao,
                            C.AUTONUM AS VeiculoId,
                            'Cavalo: ' || C.PLACA_CAVALO || ' | Carreta: ' || C.PLACA_CARRETA As VeiculoDescricao,
                            D.AUTONUM As TransportadoraId,
                            D.RAZAO As TransportadoraDescricao,
                            E.AUTONUM_GD_RESERVA AS PeriodoId,
                            TO_CHAR(E.PERIODO_INICIAL, 'DD/MM/YYYY HH24:MI') || ' - ' || TO_CHAR(E.PERIODO_FINAL, 'HH24:MI') PeriodoDescricao,
                            A.Protocolo,
                            NULL As IMO,
                            NULL As EXCESSO,
                            A.ANO_PROTOCOLO As AnoProtocolo,
                            A.AUTONUM_USUARIO AS UsuarioId,
                            A.FLAG_IMPRESSO AS Impresso,
                            A.DATA_HORA As DataCriacao,
                            NULL As RecintoTRA,
                            A.RECINTO As RecintoDEPOT,
                            F.NOME As RecintoDescricao,
                            A.QUANTIDADE,
                            A.CTE,
                            2 As TipoAgendamento
                        FROM
                            OPERADOR.TB_AGENDAMENTO_DEPOT A
                        INNER JOIN
                            OPERADOR.TB_AG_MOTORISTAS B ON A.AUTONUM_MOTORISTA = B.AUTONUM
                        INNER JOIN
                            OPERADOR.TB_AG_VEICULOS C ON A.AUTONUM_VEICULO = C.AUTONUM    
                        INNER JOIN
                            OPERADOR.TB_CAD_TRANSPORTADORAS D ON A.AUTONUM_TRANSPORTADORA = D.AUTONUM        
                        INNER JOIN
                            OPERADOR.TB_GD_RESERVA E ON A.AUTONUM_PERIODO = E.AUTONUM_GD_RESERVA
                        INNER JOIN
                            OPERADOR.VW_NAVIS_DEPOT F ON A.RECINTO = F.ID
                        LEFT JOIN
                            OPERADOR.TB_TIPOS_CAMINHAO G ON C.ID_TIPO_CAMINHAO = G.AUTONUM
                        WHERE
                            A.AUTONUM = :Id", parametros).FirstOrDefault();
                }                
            }
        }

        public Agendamento ObterAgendamentoPorId(int id, TipoAgendamento tipoAgendamento)
        {
            using (OracleConnection con = new OracleConnection(AppConfig.StringConexao()))
            {
                var parametros = new DynamicParameters();
                parametros.Add(name: "Id", value: id, direction: ParameterDirection.Input);

                if (tipoAgendamento == TipoAgendamento.TRA)
                {
                    return con.Query<Agendamento>($@"
                        SELECT    
                            A.AUTONUM As Id,
                            B.AUTONUM As MotoristaId,
                            C.AUTONUM AS VeiculoId,
                            D.AUTONUM As TransportadoraId,
                            E.AUTONUM_GD_RESERVA AS PeriodoId,
                            A.Protocolo,
                            A.AUTONUM_USUARIO AS UsuarioId,
                            A.FLAG_IMPRESSO AS Impresso,
                            A.DT_ENTRADA AS DataEntrada,
                            A.Quantidade,
                            1 As TipoAgendamento
                        FROM
                            OPERADOR.TB_AGENDAMENTO_TRA A
                        INNER JOIN
                            OPERADOR.TB_AG_MOTORISTAS B ON A.AUTONUM_MOTORISTA = B.AUTONUM
                        INNER JOIN
                            OPERADOR.TB_AG_VEICULOS C ON A.AUTONUM_VEICULO = C.AUTONUM    
                        INNER JOIN
                            OPERADOR.TB_CAD_TRANSPORTADORAS D ON A.AUTONUM_TRANSPORTADORA = D.AUTONUM        
                        INNER JOIN
                            OPERADOR.TB_GD_RESERVA E ON A.AUTONUM_PERIODO = E.AUTONUM_GD_RESERVA
                        WHERE
                            A.AUTONUM = :Id", parametros).FirstOrDefault();
                }
                else
                {
                    return con.Query<Agendamento>($@"
                        SELECT    
                            A.AUTONUM As Id,
                            B.AUTONUM As MotoristaId,
                            C.AUTONUM AS VeiculoId,
                            D.AUTONUM As TransportadoraId,
                            E.AUTONUM_GD_RESERVA AS PeriodoId,
                            A.Protocolo,
                            A.AUTONUM_USUARIO AS UsuarioId,
                            A.FLAG_IMPRESSO AS Impresso,
                            A.DT_ENTRADA AS DataEntrada,
                            A.Quantidade,
                            2 As TipoAgendamento
                        FROM
                            OPERADOR.TB_AGENDAMENTO_DEPOT A
                        INNER JOIN
                            OPERADOR.TB_AG_MOTORISTAS B ON A.AUTONUM_MOTORISTA = B.AUTONUM
                        INNER JOIN
                            OPERADOR.TB_AG_VEICULOS C ON A.AUTONUM_VEICULO = C.AUTONUM    
                        INNER JOIN
                            OPERADOR.TB_CAD_TRANSPORTADORAS D ON A.AUTONUM_TRANSPORTADORA = D.AUTONUM        
                        INNER JOIN
                            OPERADOR.TB_GD_RESERVA E ON A.AUTONUM_PERIODO = E.AUTONUM_GD_RESERVA
                        WHERE
                            A.AUTONUM = :Id", parametros).FirstOrDefault();
                }
                
            }
        }

        public IEnumerable<Janela> ObterPeriodos(TipoAgendamento tipoAgendamento)
        {
            using (OracleConnection con = new OracleConnection(AppConfig.StringConexao()))
            {
                var parametros = new DynamicParameters();

                if (tipoAgendamento == TipoAgendamento.TRA)
                {
                    return con.Query<Janela>(@"
                        SELECT
                            AUTONUM_GD_RESERVA As Id,
                            PERIODO_INICIAL As PeriodoInicial,
                            PERIODO_FINAL As PeriodoFinal,
                            LIMITE_MOVIMENTOS - (SELECT TO_NUMBER(COUNT(1)) FROM OPERADOR.TB_AGENDAMENTO_TRA WHERE AUTONUM_PERIODO = AUTONUM_GD_RESERVA) AS Saldo
                        FROM
                            OPERADOR.TB_GD_RESERVA
                        WHERE
                            SERVICO_GATE = 'T'
                        AND
                            PERIODO_INICIAL > SYSDATE
                        AND
                            (LIMITE_MOVIMENTOS - 
                                (SELECT TO_NUMBER(COUNT(1)) FROM OPERADOR.TB_AGENDAMENTO_TRA
                                    WHERE AUTONUM_PERIODO = AUTONUM_GD_RESERVA)) > 0
                        ORDER BY
                            PERIODO_INICIAL ASC", parametros);
                }
                else
                {
                    return con.Query<Janela>(@"
                        SELECT
                            AUTONUM_GD_RESERVA As Id,
                            PERIODO_INICIAL As PeriodoInicial,
                            PERIODO_FINAL As PeriodoFinal,
                            LIMITE_MOVIMENTOS - (SELECT TO_NUMBER(COUNT(1)) FROM OPERADOR.TB_AGENDAMENTO_DEPOT WHERE AUTONUM_PERIODO = AUTONUM_GD_RESERVA) AS Saldo
                        FROM
                            OPERADOR.TB_GD_RESERVA
                        WHERE
                            SERVICO_GATE = 'D'
                        AND
                            PERIODO_INICIAL > SYSDATE
                        AND
                            (LIMITE_MOVIMENTOS - 
                                (SELECT TO_NUMBER(COUNT(1)) FROM OPERADOR.TB_AGENDAMENTO_DEPOT
                                    WHERE AUTONUM_PERIODO = AUTONUM_GD_RESERVA)) > 0
                        ORDER BY
                            PERIODO_INICIAL ASC", parametros);
                }
            }
        }

        public Janela ObterPeriodoPorId(int id)
        {
            using (OracleConnection con = new OracleConnection(AppConfig.StringConexao()))
            {
                var parametros = new DynamicParameters();
                parametros.Add(name: "Id", value: id, direction: ParameterDirection.Input);

                return con.Query<Janela>(@"
                    SELECT
                        AUTONUM_GD_RESERVA As Id,
                        PERIODO_INICIAL As PeriodoInicial,
                        PERIODO_FINAL As PeriodoFinal,
                        LIMITE_MOVIMENTOS - (SELECT COUNT(1) FROM TB_AGENDAMENTO_CNTR WHERE AUTONUM_PERIODO = AUTONUM_GD_RESERVA) AS Saldo
                    FROM
                        OPERADOR.TB_GD_RESERVA
                    WHERE
                        AUTONUM_GD_RESERVA = :Id", parametros).FirstOrDefault();
            }
        }

        public AgendamentoDTO ObterDadosProtocolo(int id, TipoAgendamento tipoAgendamento)
        {
            using (OracleConnection con = new OracleConnection(AppConfig.StringConexao()))
            {
                var parametros = new DynamicParameters();
                parametros.Add(name: "Id", value: id, direction: ParameterDirection.Input);

                if (tipoAgendamento == TipoAgendamento.TRA)
                {
                    return con.Query<AgendamentoDTO>($@"
                        SELECT    
                            A.AUTONUM As Id,
                            B.AUTONUM As MotoristaId,
                            B.NOME As MotoristaDescricao,
                            B.CPF As MotoristaCPF,
                            B.CNH As MotoristaCNH,
                            C.AUTONUM AS VeiculoId,
                            'Cavalo: ' || C.PLACA_CAVALO || ' | Carreta: ' || C.PLACA_CARRETA As VeiculoDescricao,
                            D.AUTONUM As TransportadoraId,
                            D.RAZAO As TransportadoraDescricao,
                            D.CGC As TransportadoraDocumento,
                            F.DESCR As RecintoDescricao,
                            A.AUTONUM_PERIODO AS PeriodoId,
                            TO_CHAR(E.PERIODO_INICIAL, 'DD/MM/YYYY HH24:MI') || ' - ' || TO_CHAR(E.PERIODO_FINAL, 'HH24:MI') PeriodoDescricao,
                            A.Protocolo,
                            A.ANO_PROTOCOLO As AnoProtocolo,
                            A.AUTONUM_USUARIO AS UsuarioId,
                            A.FLAG_IMPRESSO AS Impresso,    
                            A.DATA_HORA As DataCriacao,
                            A.AUTONUM_RECINTO As RecintoTRA,
                            NULL As RecintoDEPOT,
                            A.QUANTIDADE,
                            NVL(A.IMO, 0) IMO,
                            NVL(A.EXCESSO, 0) EXCESSO,
                            A.CTE,
                            1 As TipoAgendamento
                        FROM
                            OPERADOR.TB_AGENDAMENTO_TRA A
                        INNER JOIN
                            OPERADOR.TB_AG_MOTORISTAS B ON A.AUTONUM_MOTORISTA = B.AUTONUM
                        INNER JOIN
                            OPERADOR.TB_AG_VEICULOS C ON A.AUTONUM_VEICULO = C.AUTONUM    
                        INNER JOIN
                            OPERADOR.TB_CAD_TRANSPORTADORAS D ON A.AUTONUM_TRANSPORTADORA = D.AUTONUM  
                        INNER JOIN
                            OPERADOR.TB_GD_RESERVA E ON A.AUTONUM_PERIODO = E.AUTONUM_GD_RESERVA   
                        INNER JOIN
                            OPERADOR.DTE_TB_RECINTOS F ON A.AUTONUM_RECINTO = F.CODE
                        WHERE
                            A.AUTONUM = :Id", parametros).FirstOrDefault();
                }
                else
                {
                    return con.Query<AgendamentoDTO>($@"
                        SELECT    
                            A.AUTONUM As Id,
                            B.AUTONUM As MotoristaId,
                            B.NOME As MotoristaDescricao,
                            B.CPF As MotoristaCPF,
                            B.CNH As MotoristaCNH,
                            C.AUTONUM AS VeiculoId,
                            'Cavalo: ' || C.PLACA_CAVALO || ' | Carreta: ' || C.PLACA_CARRETA As VeiculoDescricao,
                            D.AUTONUM As TransportadoraId,
                            D.RAZAO As TransportadoraDescricao,
                            D.CGC As TransportadoraDocumento,
                            F.NOME As RecintoDescricao,
                            A.AUTONUM_PERIODO AS PeriodoId,
                            TO_CHAR(E.PERIODO_INICIAL, 'DD/MM/YYYY HH24:MI') || ' - ' || TO_CHAR(E.PERIODO_FINAL, 'HH24:MI') PeriodoDescricao,
                            A.Protocolo,
                            A.ANO_PROTOCOLO As AnoProtocolo,
                            A.AUTONUM_USUARIO AS UsuarioId,
                            A.FLAG_IMPRESSO AS Impresso,    
                            A.DATA_HORA As DataCriacao,
                            NULL As RecintoTRA,
                            A.RECINTO As RecintoDEPOT,
                            A.QUANTIDADE,
                            NULL IMO,
                            NULL EXCESSO,
                            A.CTE,
                            2 As TipoAgendamento
                        FROM
                            OPERADOR.TB_AGENDAMENTO_DEPOT A
                        INNER JOIN
                            OPERADOR.TB_AG_MOTORISTAS B ON A.AUTONUM_MOTORISTA = B.AUTONUM
                        INNER JOIN
                            OPERADOR.TB_AG_VEICULOS C ON A.AUTONUM_VEICULO = C.AUTONUM    
                        INNER JOIN
                            OPERADOR.TB_CAD_TRANSPORTADORAS D ON A.AUTONUM_TRANSPORTADORA = D.AUTONUM  
                        INNER JOIN
                            OPERADOR.TB_GD_RESERVA E ON A.AUTONUM_PERIODO = E.AUTONUM_GD_RESERVA  
                        INNER JOIN
                            OPERADOR.VW_NAVIS_DEPOT F ON A.RECINTO = F.ID
                        WHERE
                            A.AUTONUM = :Id", parametros).FirstOrDefault();
                }                
            }
        }

        public void AtualizarMotoristaAgendamento(int agendamentoId, int motoristaId, TipoAgendamento tipoAgendamento)
        {
            using (OracleConnection con = new OracleConnection(AppConfig.StringConexao()))
            {
                var parametros = new DynamicParameters();

                parametros.Add(name: "AgendamentoId", value: agendamentoId, direction: ParameterDirection.Input);
                parametros.Add(name: "MotoristaId", value: motoristaId, direction: ParameterDirection.Input);

                if (tipoAgendamento == TipoAgendamento.TRA)
                {
                    con.Execute("UPDATE OPERADOR.TB_AGENDAMENTO_TRA SET AUTONUM_MOTORISTA = :MotoristaId WHERE AUTONUM = :AgendamentoId", parametros);
                }
                else
                {
                    con.Execute("UPDATE OPERADOR.TB_AGENDAMENTO_DEPOT SET AUTONUM_MOTORISTA = :MotoristaId WHERE AUTONUM = :AgendamentoId", parametros);
                }
            }
        }

        public void AtualizarVeiculoAgendamento(int agendamentoId, int veiculoId, TipoAgendamento tipoAgendamento)
        {
            using (OracleConnection con = new OracleConnection(AppConfig.StringConexao()))
            {
                var parametros = new DynamicParameters();

                parametros.Add(name: "AgendamentoId", value: agendamentoId, direction: ParameterDirection.Input);
                parametros.Add(name: "VeiculoId", value: veiculoId, direction: ParameterDirection.Input);

                if (tipoAgendamento == TipoAgendamento.TRA)
                {
                    con.Execute("UPDATE OPERADOR.TB_AGENDAMENTO_TRA SET AUTONUM_VEICULO = :VeiculoId WHERE AUTONUM = :AgendamentoId", parametros);
                }
                else
                {
                    con.Execute("UPDATE OPERADOR.TB_AGENDAMENTO_DEPOT SET AUTONUM_VEICULO = :VeiculoId WHERE AUTONUM = :AgendamentoId", parametros);
                }
            }
        }
        
        public int ObterTotalAgendadoTRA(int recintoId, int id = 0)
        {
            using (OracleConnection con = new OracleConnection(AppConfig.StringConexao()))
            {
                var parametros = new DynamicParameters();

                parametros.Add(name: "RecintoId", value: recintoId, direction: ParameterDirection.Input);
                parametros.Add(name: "Id", value: id, direction: ParameterDirection.Input);

                if (id == 0)
                {
                    return con.Query<int>($@"SELECT NVL(SUM(QUANTIDADE),0) As Total FROM OPERADOR.TB_AGENDAMENTO_TRA WHERE AUTONUM_RECINTO = :RecintoId AND NVL(AUTONUM_GATE, 0) = 0", parametros).Single();
                }
                else
                {
                    return con.Query<int>($@"SELECT NVL(SUM(QUANTIDADE),0) As Total FROM OPERADOR.TB_AGENDAMENTO_TRA WHERE AUTONUM_RECINTO = :RecintoId AND AUTONUM <> :Id AND NVL(AUTONUM_GATE, 0) = 0", parametros).Single();
                }                
            }
        }

        public int ObterTotalAgendadoDEPOT(string recinto, int id = 0)
        {
            using (OracleConnection con = new OracleConnection(AppConfig.StringConexao()))
            {
                var parametros = new DynamicParameters();

                parametros.Add(name: "Recinto", value: recinto, direction: ParameterDirection.Input);
                parametros.Add(name: "Id", value: id, direction: ParameterDirection.Input);

                if (id == 0)
                {
                    return con.Query<int>($@"SELECT NVL(SUM(QUANTIDADE),0) As Total FROM OPERADOR.TB_AGENDAMENTO_DEPOT WHERE RECINTO = :Recinto and NVL(AUTONUM_GATE,0)=0 ", parametros).Single();
                }
                else
                {
                    return con.Query<int>($@"SELECT NVL(SUM(QUANTIDADE),0) As Total FROM OPERADOR.TB_AGENDAMENTO_DEPOT WHERE RECINTO = :Recinto AND AUTONUM <> :Id and NVL(AUTONUM_GATE,0)=0", parametros).Single();
                }
            }
        }
    }
}