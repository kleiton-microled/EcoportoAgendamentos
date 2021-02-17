using Dapper;
using Ecoporto.AgendamentoCS.Config;
using Ecoporto.AgendamentoCS.Dados.Interfaces;
using Ecoporto.AgendamentoCS.Extensions;
using Ecoporto.AgendamentoCS.Models;
using Ecoporto.AgendamentoCS.Models.DTO;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Ecoporto.AgendamentoCS.Dados.Repositorios
{
    public class AgendamentoRepositorio : IAgendamentoRepositorio
    {
        public int Cadastrar(Agendamento agendamento)
        {
            using (OracleConnection con = new OracleConnection(AppConfig.StringConexao()))
            {
                con.Open();

                using (var transaction = con.BeginTransaction())
                {
                    var parametros = new DynamicParameters();

                    parametros.Add(name: "MotoristaId", value: agendamento.MotoristaId, direction: ParameterDirection.Input);
                    parametros.Add(name: "VeiculoId", value: agendamento.VeiculoId, direction: ParameterDirection.Input);
                    parametros.Add(name: "Cegonha", value: agendamento.Cegonha.ToInt(), direction: ParameterDirection.Input);
                    parametros.Add(name: "Desembaracada", value: agendamento.Desembaracada.ToInt(), direction: ParameterDirection.Input);
                    parametros.Add(name: "TransportadoraId", value: agendamento.TransportadoraId, direction: ParameterDirection.Input);
                    parametros.Add(name: "PeriodoId", value: agendamento.PeriodoId, direction: ParameterDirection.Input);
                    parametros.Add(name: "Reserva", value: agendamento.Reserva, direction: ParameterDirection.Input);
                    parametros.Add(name: "BookingCsId", value: agendamento.BookingCsId, direction: ParameterDirection.Input);
                    parametros.Add(name: "UsuarioId", value: agendamento.UsuarioId, direction: ParameterDirection.Input);
                    parametros.Add(name: "CTE", value: agendamento.CTE, direction: ParameterDirection.Input);
                    parametros.Add(name: "EmailRegistro", value: agendamento.EmailRegistro, direction: ParameterDirection.Input);

                    parametros.Add(name: "Id", dbType: DbType.Int32, direction: ParameterDirection.Output);

                    con.Execute(@"
                        INSERT INTO OPERADOR.TB_AGENDAMENTO_CS
                            ( 
                                AUTONUM,
                                AUTONUM_MOTORISTA,
                                AUTONUM_VEICULO,                                
                                FLAG_CEGONHA,
                                FLAG_DESEMBARACADO,
                                AUTONUM_TRANSPORTADORA,
                                AUTONUM_PERIODO,
                                RESERVA,
                                AUTONUM_CS_BOOKING,
                                AUTONUM_USUARIO,
                                PROTOCOLO,
                                ANO_PROTOCOLO,
                                CTE,
                                EMAIL_REGISTRO
                            ) VALUES ( 
                                OPERADOR.SEQ_AGENDAMENTO_CS.NEXTVAL, 
                                :MotoristaId,
                                :VeiculoId,
                                :Cegonha,
                                :Desembaracada,
                                :TransportadoraId,
                                :PeriodoId,
                                :Reserva,
                                :BookingCsId,
                                :UsuarioId,
                                OPERADOR.SEQ_PROTOCOLO_AG_GERAL.NEXTVAL,
                                TO_CHAR(SYSDATE, 'YYYY'),
                                :CTE,
                                :EmailRegistro
                            ) RETURNING AUTONUM INTO :Id", parametros, transaction);

                    var id = parametros.Get<int>("Id");

                    foreach (var item in agendamento.Itens)
                    {
                        parametros = new DynamicParameters();

                        parametros.Add(name: "AgendamentoId", value: id, direction: ParameterDirection.Input);
                        parametros.Add(name: "BookingCsItemId", value: item.BookingCsItemId, direction: ParameterDirection.Input);
                        parametros.Add(name: "Qtde", value: item.Qtde, direction: ParameterDirection.Input);
                        parametros.Add(name: "Chassis", value: item.Chassis, direction: ParameterDirection.Input);

                        parametros.Add(name: "Id", dbType: DbType.Int32, direction: ParameterDirection.Output);

                        con.Execute(@"
                                INSERT INTO OPERADOR.TB_AGENDAMENTO_CS_ITENS
                                    ( 
                                        AUTONUM,
                                        AUTONUM_AGENDAMENTO,
                                        AUTONUM_CS_BOOKING_ITEM,
                                        QTDE,
                                        CHASSIS
                                    ) VALUES ( 
                                        OPERADOR.SEQ_AGENDAMENTO_CS_ITENS.NEXTVAL, 
                                        :AgendamentoId,
                                        :BookingCsItemId,
                                        :Qtde,
                                        :Chassis
                                    ) RETURNING AUTONUM INTO :Id", parametros, transaction);

                        var itemId = parametros.Get<int>("Id");

                        if (!string.IsNullOrEmpty(item.Chassis))
                        {
                            var chassis = item.Chassis.Split(',');

                            foreach (var chassi in chassis)
                            {
                                parametros = new DynamicParameters();

                                parametros.Add(name: "ItemId", value: itemId, direction: ParameterDirection.Input);
                                parametros.Add(name: "Chassi", value: chassi.RemoverEspacos(), direction: ParameterDirection.Input);

                                con.Execute(@"INSERT INTO OPERADOR.TB_AGENDAMENTO_CS_ITENS_CHASSI (AUTONUM, AUTONUM_AGENDAMENTO_ITEM, CHASSI) VALUES (OPERADOR.SEQ_AG_CS_ITENS_CHASSIS.NEXTVAL, :ItemId, :Chassi)", parametros, transaction);
                            }
                        }                        

                        foreach (var nota in item.NotasFiscais)
                        {
                            parametros = new DynamicParameters();

                            parametros.Add(name: "ItemId", value: itemId, direction: ParameterDirection.Input);
                            parametros.Add(name: "Danfe", value: nota.Danfe, direction: ParameterDirection.Input);
                            parametros.Add(name: "CFOP", value: nota.CFOP, direction: ParameterDirection.Input);

                            con.Execute(@"INSERT INTO OPERADOR.TB_AGENDAMENTO_CS_ITENS_DANFES (AUTONUM, AUTONUM_AGENDAMENTO_ITEM, DANFE, CFOP) VALUES (OPERADOR.SEQ_AG_CS_ITENS_DANFES.NEXTVAL, :ItemId, :Danfe, :CFOP)", parametros, transaction);
                        }
                    }

                    transaction.Commit();

                    return id;
                }
            }
        }

        public void Atualizar(Agendamento agendamento)
        {
            using (OracleConnection con = new OracleConnection(AppConfig.StringConexao()))
            {
                con.Open();

                using (var transaction = con.BeginTransaction())
                {
                    var parametros = new DynamicParameters();

                    parametros.Add(name: "MotoristaId", value: agendamento.MotoristaId, direction: ParameterDirection.Input);
                    parametros.Add(name: "VeiculoId", value: agendamento.VeiculoId, direction: ParameterDirection.Input);
                    parametros.Add(name: "Cegonha", value: agendamento.Cegonha.ToInt(), direction: ParameterDirection.Input);
                    parametros.Add(name: "Desembaracada", value: agendamento.Desembaracada.ToInt(), direction: ParameterDirection.Input);
                    parametros.Add(name: "PeriodoId", value: agendamento.PeriodoId, direction: ParameterDirection.Input);
                    parametros.Add(name: "CTE", value: agendamento.CTE, direction: ParameterDirection.Input);
                    parametros.Add(name: "EmailRegistro", value: agendamento.EmailRegistro, direction: ParameterDirection.Input);
                    parametros.Add(name: "Id", value: agendamento.Id, direction: ParameterDirection.Input);

                    con.Execute(@"UPDATE OPERADOR.TB_AGENDAMENTO_CS SET AUTONUM_MOTORISTA = :MotoristaId, AUTONUM_VEICULO = :VeiculoId, FLAG_CEGONHA = :Cegonha, FLAG_DESEMBARACADO = :Desembaracada, AUTONUM_PERIODO = :PeriodoId, CTE = :CTE, EMAIL_REGISTRO = :EmailRegistro WHERE AUTONUM = :Id", parametros, transaction);

                    con.Execute(@"DELETE FROM OPERADOR.TB_AGENDAMENTO_CS_ITENS_DANFES WHERE AUTONUM_AGENDAMENTO_ITEM IN (SELECT AUTONUM FROM OPERADOR.TB_AGENDAMENTO_CS_ITENS WHERE AUTONUM_AGENDAMENTO = :Id)", parametros, transaction);
                    con.Execute(@"DELETE FROM OPERADOR.TB_AGENDAMENTO_CS_ITENS_CHASSI WHERE AUTONUM_AGENDAMENTO_ITEM IN (SELECT AUTONUM FROM OPERADOR.TB_AGENDAMENTO_CS_ITENS WHERE AUTONUM_AGENDAMENTO = :Id)", parametros, transaction);
                    con.Execute(@"DELETE FROM OPERADOR.TB_AGENDAMENTO_CS_ITENS WHERE AUTONUM_AGENDAMENTO = :Id", parametros, transaction);

                    foreach (var item in agendamento.Itens)
                    {
                        parametros = new DynamicParameters();

                        parametros.Add(name: "AgendamentoId", value: agendamento.Id, direction: ParameterDirection.Input);
                        parametros.Add(name: "BookingCsItemId", value: item.BookingCsItemId, direction: ParameterDirection.Input);
                        parametros.Add(name: "Qtde", value: item.Qtde, direction: ParameterDirection.Input);
                        parametros.Add(name: "Chassis", value: item.Chassis, direction: ParameterDirection.Input);

                        parametros.Add(name: "Id", dbType: DbType.Int32, direction: ParameterDirection.Output);

                        con.Execute(@"
                                INSERT INTO OPERADOR.TB_AGENDAMENTO_CS_ITENS
                                    ( 
                                        AUTONUM,
                                        AUTONUM_AGENDAMENTO,
                                        AUTONUM_CS_BOOKING_ITEM,
                                        QTDE,
                                        CHASSIS
                                    ) VALUES ( 
                                        OPERADOR.SEQ_AGENDAMENTO_CS_ITENS.NEXTVAL, 
                                        :AgendamentoId,
                                        :BookingCsItemId,
                                        :Qtde,
                                        :Chassis
                                    ) RETURNING AUTONUM INTO :Id", parametros, transaction);

                        var itemId = parametros.Get<int>("Id");

                        if (!string.IsNullOrEmpty(item.Chassis))
                        {
                            var chassis = item.Chassis.Split(',');

                            foreach (var chassi in chassis)
                            {
                                parametros = new DynamicParameters();

                                parametros.Add(name: "ItemId", value: itemId, direction: ParameterDirection.Input);
                                parametros.Add(name: "Chassi", value: chassi.RemoverEspacos(), direction: ParameterDirection.Input);

                                con.Execute(@"INSERT INTO OPERADOR.TB_AGENDAMENTO_CS_ITENS_CHASSI (AUTONUM, AUTONUM_AGENDAMENTO_ITEM, CHASSI) VALUES (OPERADOR.SEQ_AG_CS_ITENS_CHASSIS.NEXTVAL, :ItemId, :Chassi)", parametros, transaction);
                            }
                        }

                        foreach (var nota in item.NotasFiscais)
                        {
                            parametros = new DynamicParameters();

                            parametros.Add(name: "ItemId", value: itemId, direction: ParameterDirection.Input);
                            parametros.Add(name: "Danfe", value: nota.Danfe, direction: ParameterDirection.Input);
                            parametros.Add(name: "CFOP", value: nota.CFOP, direction: ParameterDirection.Input);

                            con.Execute(@"INSERT INTO OPERADOR.TB_AGENDAMENTO_CS_ITENS_DANFES (AUTONUM, AUTONUM_AGENDAMENTO_ITEM, DANFE, CFOP) VALUES (OPERADOR.SEQ_AG_CS_ITENS_DANFES.NEXTVAL, :ItemId, :Danfe, :CFOP)", parametros, transaction);
                        }
                    }

                    transaction.Commit();
                }
            }
        }

        public void AtualizarProtocoloImpresso(int id)
        {
            using (OracleConnection con = new OracleConnection(AppConfig.StringConexao()))
            {
                var parametros = new DynamicParameters();
                parametros.Add(name: "AgendamentoId", value: id, direction: ParameterDirection.Input);

                con.Execute(@"UPDATE OPERADOR.TB_AGENDAMENTO_CS SET FLAG_IMPRESSO = 1 WHERE AUTONUM = :AgendamentoId", parametros);
            }
        }

        public void Excluir(int id)
        {
            using (OracleConnection con = new OracleConnection(AppConfig.StringConexao()))
            {
                con.Open();

                using (var transaction = con.BeginTransaction())
                {
                    var parametros = new DynamicParameters();
                    parametros.Add(name: "Id", value: id, direction: ParameterDirection.Input);

                    con.Execute(@"DELETE FROM OPERADOR.TB_AGENDAMENTO_CS_ITENS_DANFES WHERE AUTONUM_AGENDAMENTO_ITEM IN (SELECT AUTONUM FROM TB_AGENDAMENTO_CS_ITENS WHERE AUTONUM_AGENDAMENTO = :Id)", parametros, transaction);
                    con.Execute(@"DELETE FROM OPERADOR.TB_AGENDAMENTO_CS_ITENS_CHASSI WHERE AUTONUM_AGENDAMENTO_ITEM IN (SELECT AUTONUM FROM TB_AGENDAMENTO_CS_ITENS WHERE AUTONUM_AGENDAMENTO = :Id)", parametros, transaction);
                    con.Execute(@"DELETE FROM OPERADOR.TB_AGENDAMENTO_CS_ITENS WHERE AUTONUM_AGENDAMENTO = :Id", parametros, transaction);
                    con.Execute(@"DELETE FROM OPERADOR.TB_AGENDAMENTO_CS WHERE AUTONUM = :Id", parametros, transaction);

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

                return con.Query<AgendamentoDTO>(@"
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
                        OPERADOR.TB_AGENDAMENTO_CS A
                    INNER JOIN
                        OPERADOR.TB_AG_MOTORISTAS B ON A.AUTONUM_MOTORISTA = B.AUTONUM
                    INNER JOIN
                        OPERADOR.TB_AG_VEICULOS C ON A.AUTONUM_VEICULO = C.AUTONUM    
                    INNER JOIN
                        OPERADOR.TB_CAD_TRANSPORTADORAS D ON A.AUTONUM_TRANSPORTADORA = D.AUTONUM        
                    INNER JOIN
                        OPERADOR.TB_GD_RESERVA E ON A.AUTONUM_PERIODO = E.AUTONUM_GD_RESERVA
                    INNER JOIN
                        OPERADOR.VW_WEB_CAB_RESERVA F ON A.AUTONUM_CS_BOOKING = F.AUTONUM_CS_BOOKING
                    WHERE
                        A.AUTONUM_TRANSPORTADORA = :TransportadoraId
                    AND
                        A.DATA_HORA >= SYSDATE - 90
                    ORDER BY
                        A.AUTONUM DESC", parametros);
            }
        }

        public IEnumerable<AgendamentoDTO> ObterAgendamentosPorPeriodoEVeiculo(int transportadoraId, int periodoId, int veiculoId)
        {
            using (OracleConnection con = new OracleConnection(AppConfig.StringConexao()))
            {
                var parametros = new DynamicParameters();

                parametros.Add(name: "TransportadoraId", value: transportadoraId, direction: ParameterDirection.Input);
                parametros.Add(name: "PeriodoId", value: periodoId, direction: ParameterDirection.Input);
                parametros.Add(name: "VeiculoId", value: veiculoId, direction: ParameterDirection.Input);

                return con.Query<AgendamentoDTO>(@"
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
                        OPERADOR.TB_AGENDAMENTO_CS A
                    INNER JOIN
                        OPERADOR.TB_AG_MOTORISTAS B ON A.AUTONUM_MOTORISTA = B.AUTONUM
                    INNER JOIN
                        OPERADOR.TB_AG_VEICULOS C ON A.AUTONUM_VEICULO = C.AUTONUM    
                    INNER JOIN
                        OPERADOR.TB_CAD_TRANSPORTADORAS D ON A.AUTONUM_TRANSPORTADORA = D.AUTONUM        
                    INNER JOIN
                        OPERADOR.TB_GD_RESERVA E ON A.AUTONUM_PERIODO = E.AUTONUM_GD_RESERVA
                    INNER JOIN
                        OPERADOR.VW_WEB_CAB_RESERVA F ON A.AUTONUM_CS_BOOKING = F.AUTONUM_CS_BOOKING
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

        public AgendamentoDTO ObterDetalhesAgendamento(int id)
        {
            using (OracleConnection con = new OracleConnection(AppConfig.StringConexao()))
            {
                var parametros = new DynamicParameters();
                parametros.Add(name: "Id", value: id, direction: ParameterDirection.Input);

                return con.Query<AgendamentoDTO>(@"
                    SELECT    
                        A.AUTONUM As Id,
                        B.AUTONUM As MotoristaId,
                        B.NOME || ' ' || B.CNH As MotoristaDescricao,
                        C.AUTONUM AS VeiculoId,
                        'Cavalo: ' || C.PLACA_CAVALO || ' | Carreta: ' || C.PLACA_CARRETA As VeiculoDescricao,
                        D.AUTONUM As TransportadoraId,
                        D.RAZAO As TransportadoraDescricao,
                        E.AUTONUM_GD_RESERVA AS PeriodoId,
                        TO_CHAR(E.PERIODO_INICIAL, 'DD/MM/YYYY HH24:MI') || ' - ' || TO_CHAR(E.PERIODO_FINAL, 'HH24:MI') PeriodoDescricao,
                        F.Reserva,
                        A.Protocolo,
                        A.ANO_PROTOCOLO As AnoProtocolo,
                        A.AUTONUM_USUARIO AS UsuarioId,
                        A.FLAG_IMPRESSO AS Impresso,
                        F.NAVIO, 
                        F.VIAGEM, 
                        F.DT_ABERTURA_PRACA As Abertura, 
                        F.DT_FECHAMENTO_PRACA AS Fechamento,
                        A.AUTONUM_CS_BOOKING As BookingCsId,
                        A.FLAG_CEGONHA As Cegonha,
                        A.FLAG_DESEMBARACADO As Desembaracada,
                        A.DATA_HORA As DataCriacao,
                        A.CTE,
                        A.EMAIL_REGISTRO As EmailRegistro
                    FROM
                        OPERADOR.TB_AGENDAMENTO_CS A
                    INNER JOIN
                        OPERADOR.TB_AG_MOTORISTAS B ON A.AUTONUM_MOTORISTA = B.AUTONUM
                    INNER JOIN
                        OPERADOR.TB_AG_VEICULOS C ON A.AUTONUM_VEICULO = C.AUTONUM    
                    INNER JOIN
                        OPERADOR.TB_CAD_TRANSPORTADORAS D ON A.AUTONUM_TRANSPORTADORA = D.AUTONUM        
                    INNER JOIN
                        OPERADOR.TB_GD_RESERVA E ON A.AUTONUM_PERIODO = E.AUTONUM_GD_RESERVA
                    INNER JOIN
                        OPERADOR.VW_WEB_CAB_RESERVA F ON A.AUTONUM_CS_BOOKING = F.AUTONUM_CS_BOOKING
                    LEFT JOIN
                        OPERADOR.TB_TIPOS_CAMINHAO G ON C.ID_TIPO_CAMINHAO = G.AUTONUM
                    WHERE
                        A.AUTONUM = :Id", parametros).FirstOrDefault();
            }
        }

        public Agendamento ObterAgendamentoPorId(int id)
        {
            using (OracleConnection con = new OracleConnection(AppConfig.StringConexao()))
            {
                var parametros = new DynamicParameters();
                parametros.Add(name: "Id", value: id, direction: ParameterDirection.Input);

                return con.Query<Agendamento>(@"
                    SELECT    
                        A.AUTONUM As Id,
                        B.AUTONUM As MotoristaId,
                        C.AUTONUM AS VeiculoId,
                        D.AUTONUM As TransportadoraId,
                        E.AUTONUM_GD_RESERVA AS PeriodoId,
                        A.Protocolo,
                        F.REFERENCE AS Reserva,
                        A.AUTONUM_USUARIO AS UsuarioId,
                        A.FLAG_IMPRESSO AS Impresso,                       
                        A.AUTONUM_CS_BOOKING As BookingCsId,
                        A.DT_ENTRADA AS DataEntrada
                    FROM
                        OPERADOR.TB_AGENDAMENTO_CS A
                    INNER JOIN
                        OPERADOR.TB_AG_MOTORISTAS B ON A.AUTONUM_MOTORISTA = B.AUTONUM
                    INNER JOIN
                        OPERADOR.TB_AG_VEICULOS C ON A.AUTONUM_VEICULO = C.AUTONUM    
                    INNER JOIN
                        OPERADOR.TB_CAD_TRANSPORTADORAS D ON A.AUTONUM_TRANSPORTADORA = D.AUTONUM        
                    INNER JOIN
                        OPERADOR.TB_GD_RESERVA E ON A.AUTONUM_PERIODO = E.AUTONUM_GD_RESERVA
                    INNER JOIN
                        OPERADOR.TB_CS_BOOKING F ON A.AUTONUM_CS_BOOKING = F.AUTONUM
                    WHERE
                        A.AUTONUM = :Id", parametros).FirstOrDefault();
            }
        }

        public IEnumerable<ReservaItem> ObterItensAgendamento(int id)
        {
            using (OracleConnection con = new OracleConnection(AppConfig.StringConexao()))
            {
                var parametros = new DynamicParameters();
                parametros.Add(name: "Id", value: id, direction: ParameterDirection.Input);

                return con.Query<ReservaItem>(@"
                    SELECT 
                        DISTINCT
                            A.AUTONUM As Id,
                            A.AUTONUM_CS_BOOKING_ITEM As BookingCsItemId,
                            C.Quantidade As QtdeReserva, 
                            A.QTDE,
                            E.DESCR As EMBALAGEM, 
                            C.PESO_BRUTO_UNIT As PesoUnitario, 
                            C.MARCA, 
                            D.RESERVA,
                            C.CONTRAMARCA,
                            NVL(F.FLAG_UPLOAD_PACKLIST, 0) As PackingList,
                            NVL(F.FLAG_UPLOAD_IMAGEM_CARGA, 0) As ImagemCarga,
                            NVL(F.FLAG_UPLOAD_D_TECNICO, 0) As DesenhoTecnico,
                            A.CHASSIS,
                            TO_CHAR(C.quantidade) || ' ' || E.DESCR || ' ' || C.MARCA || ' ' || C.CONTRAMARCA || ' ' || C.PESO_BRUTO_UNIT || 'Kg' || ' Saldo: ' || (C.quantidade - NVL ((SELECT SUM (qtde) FROM tb_agendamento_cs_itens WHERE autonum_cs_booking_item = C.autonum), 0)) As Descricao,
                            A.CHASSIS,
                            ROW_NUMBER() OVER (ORDER BY A.AUTONUM_CS_BOOKING_ITEM) AS Item,
                            F.AUTONUM As ClassificacaoId,
                            F.DESCR As Classificacao
                    FROM
                        TB_AGENDAMENTO_CS_ITENS A
                    INNER JOIN
                        TB_AGENDAMENTO_CS B ON A.AUTONUM_AGENDAMENTO = B.AUTONUM
                    INNER JOIN
                        TB_CS_BOOKING_ITEM C ON A.AUTONUM_CS_BOOKING_ITEM = C.AUTONUM
                    INNER JOIN
                        VW_WEB_CAB_RESERVA D ON C.AUTONUM_CS_BOOKING = D.AUTONUM_CS_BOOKING
                    LEFT JOIN
                        SGIPA.DTE_TB_EMBALAGENS E ON C.AUTONUM_CS_EMBALAGEM = E.CODE
                    LEFT JOIN
                        TB_CAD_CLASS_CARGO F ON C.AUTONUM_CARGO_CLASS = F.AUTONUM
                    WHERE
                        B.AUTONUM = :Id", parametros);
            }
        }

        public IEnumerable<NotaFiscal> ObterNotasFiscaisPorDanfe(string danfe)
        {
            using (OracleConnection con = new OracleConnection(AppConfig.StringConexao()))
            {
                var parametros = new DynamicParameters();
                parametros.Add(name: "Danfe", value: danfe, direction: ParameterDirection.Input);

                return con.Query<NotaFiscal>(@"
                    SELECT 
                        AUTONUM As Id,
                        DANFE,    
                        CFOP
                    FROM 
                        TB_AGENDAMENTO_CS_ITENS_DANFES
                    WHERE 
                        DANFE = :Danfe", parametros);
            }
        }

        public IEnumerable<NotaFiscal> ObterNotasFiscaisPorItemAgendamento(int agendamentoId, int itemId)
        {
            using (OracleConnection con = new OracleConnection(AppConfig.StringConexao()))
            {
                var parametros = new DynamicParameters();

                parametros.Add(name: "AgendamentoId", value: agendamentoId, direction: ParameterDirection.Input);
                parametros.Add(name: "ItemId", value: itemId, direction: ParameterDirection.Input);

                return con.Query<NotaFiscal>(@"
                    SELECT
                        A.DANFE 
                    FROM
                        TB_AGENDAMENTO_CS_ITENS_DANFES A
                    INNER  JOIN
                        TB_AGENDAMENTO_CS_ITENS B ON A.AUTONUM_AGENDAMENTO_ITEM = B.AUTONUM
                    INNER JOIN
                        TB_AGENDAMENTO_CS C ON B.AUTONUM_AGENDAMENTO = C.AUTONUM
                    WHERE
                        C.AUTONUM = :AgendamentoId AND B.AUTONUM = :ItemId", parametros);
            }
        }

        public IEnumerable<NotaFiscal> ObterNotasFiscaisAgendamento(int agendamentoId)
        {
            using (OracleConnection con = new OracleConnection(AppConfig.StringConexao()))
            {
                var parametros = new DynamicParameters();
                parametros.Add(name: "AgendamentoId", value: agendamentoId, direction: ParameterDirection.Input);

                return con.Query<NotaFiscal>(@"
                    SELECT
                        A.AUTONUM As Id,
                        A.DANFE,
                        B.AUTONUM_CS_BOOKING_ITEM As BookingCsItemId,
                        A.CFOP,
                        D.REFERENCE As Reserva
                    FROM
                        TB_AGENDAMENTO_CS_ITENS_DANFES A
                    INNER JOIN
                        TB_AGENDAMENTO_CS_ITENS B ON A.AUTONUM_AGENDAMENTO_ITEM = B.AUTONUM
                    INNER JOIN
                        TB_CS_BOOKING_ITEM C ON B.AUTONUM_CS_BOOKING_ITEM = C.AUTONUM
                    INNER JOIN 
                        TB_CS_BOOKING D ON C.AUTONUM_CS_BOOKING = D.AUTONUM
                    INNER JOIN
                        TB_AGENDAMENTO_CS E ON B.AUTONUM_AGENDAMENTO = E.AUTONUM    
                    WHERE
                        E.AUTONUM = :AgendamentoId", parametros);
            }
        }

        public IEnumerable<Janela> ObterPeriodos(DateTime inicio, DateTime fim, bool cargaProjeto)
        {
            using (OracleConnection con = new OracleConnection(AppConfig.StringConexao()))
            {
                var parametros = new DynamicParameters();

                parametros.Add(name: "Inicio", value: inicio, direction: ParameterDirection.Input);
                parametros.Add(name: "Fim", value: fim, direction: ParameterDirection.Input);
                parametros.Add(name: "ServicoGate", value: cargaProjeto ? "P" : "O", direction: ParameterDirection.Input);

                return con.Query<Janela>(@"
                    SELECT
                        AUTONUM_GD_RESERVA As Id,
                        PERIODO_INICIAL As PeriodoInicial,
                        PERIODO_FINAL As PeriodoFinal,
                        LIMITE_MOVIMENTOS - (SELECT COUNT(1) FROM OPERADOR.TB_AGENDAMENTO_CS WHERE AUTONUM_PERIODO = AUTONUM_GD_RESERVA) AS Saldo
                    FROM
                        OPERADOR.TB_GD_RESERVA
                    WHERE
                        SERVICO_GATE = :ServicoGate
                    AND
                        PERIODO_INICIAL >= SYSDATE
                    AND
                        PERIODO_INICIAL >= :Inicio AND PERIODO_FINAL <= :Fim
                    AND
                        (LIMITE_MOVIMENTOS - 
                            (SELECT TO_NUMBER(COUNT(1)) FROM OPERADOR.TB_AGENDAMENTO_CS WHERE AUTONUM_PERIODO = AUTONUM_GD_RESERVA)) > 0
                    ORDER BY
                        PERIODO_INICIAL ASC", parametros);
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
                        LIMITE_MOVIMENTOS - (SELECT COUNT(1) FROM TB_AGENDAMENTO_CS WHERE AUTONUM_PERIODO = AUTONUM_GD_RESERVA) AS Saldo
                    FROM
                        OPERADOR.TB_GD_RESERVA
                    WHERE
                        AUTONUM_GD_RESERVA = :Id", parametros).FirstOrDefault();
            }
        }

        public IEnumerable<CFOP> ObterCFOP()
        {
            using (OracleConnection con = new OracleConnection(AppConfig.StringConexao()))
            {
                return con.Query<CFOP>(@"SELECT CODIGO As Id, DESCR As Descricao FROM OPERADOR.TB_CAD_CFOP WHERE FLAG_EXPORTACAO_OP_PORT = 1 ORDER BY CODIGO");
            }
        }

        public IEnumerable<Reserva> ObterReservasAgendamento(int id)
        {
            using (OracleConnection con = new OracleConnection(AppConfig.StringConexao()))
            {
                var parametros = new DynamicParameters();
                parametros.Add(name: "Id", value: id, direction: ParameterDirection.Input);

                return con.Query<Reserva>(@"
                    SELECT
                        DISTINCT
                            B.REFERENCE As Descricao,
                            CASE NVL(B.DT_OP_INICIO, SYSDATE) WHEN SYSDATE THEN C.DATA_OP_INICIOBBK ELSE B.DT_OP_INICIO END Abertura,
                            CASE NVL(B.DT_OP_FIM, SYSDATE) WHEN SYSDATE THEN C.DATA_OP_FIMBBK ELSE B.DT_OP_FIM END Fechamento,
                            F.DELTA_HORAS As LimiteHoraLateArrival
                    FROM
                        OPERADOR.TB_CS_BOOKING_ITEM A
                    INNER JOIN
                        OPERADOR.TB_CS_BOOKING B ON A.AUTONUM_CS_BOOKING = B.AUTONUM
                    INNER JOIN
                        OPERADOR.TB_VIAGENS C ON B.AUTONUMVIAGEM = C.AUTONUM 
                    INNER JOIN
                        OPERADOR.TB_AGENDAMENTO_CS_ITENS D ON A.AUTONUM = D.AUTONUM_CS_BOOKING_ITEM
                    INNER JOIN
                        OPERADOR.TB_AGENDAMENTO_CS E ON D.AUTONUM_AGENDAMENTO = E.AUTONUM
                    INNER JOIN
                        OPERADOR.VW_WEB_CAB_RESERVA F ON B.AUTONUM = F.AUTONUM_CS_BOOKING
                    WHERE
                        E.AUTONUM = :Id", parametros);
            }
        }

        public IEnumerable<Reserva> ObterPeriodosReservasCargaSolta(string[] reservas)
        {
            using (OracleConnection con = new OracleConnection(AppConfig.StringConexao()))
            {
                var parametros = new DynamicParameters();
                parametros.Add(name: "Reservas", value: reservas, direction: ParameterDirection.Input);
                if (reservas.Length > 0)
                {
                    return con.Query<Reserva>(@"
                    SELECT
                        A.REFERENCE As Descricao,
                        CASE NVL(A.DT_OP_INICIO, SYSDATE) WHEN SYSDATE THEN B.DATA_OP_INICIOBBK ELSE A.DT_OP_INICIO END Abertura,
                        CASE NVL(A.DT_OP_FIM, SYSDATE) WHEN SYSDATE THEN B.DATA_OP_FIMBBK ELSE A.DT_OP_FIM END Fechamento,
                        C.DELTA_HORAS As LimiteHoraLateArrival
                    FROM
                        OPERADOR.TB_CS_BOOKING A
                    INNER JOIN
                        OPERADOR.TB_VIAGENS B ON A.AUTONUMVIAGEM = B.AUTONUM
                    INNER JOIN
                        OPERADOR.VW_WEB_CAB_RESERVA C ON A.AUTONUM = C.AUTONUM_CS_BOOKING
                    WHERE
                        A.REFERENCE IN :Reservas AND ROWNUM = 1
                    ORDER BY
                        B.AUTONUM DESC", parametros);
                }
                else
                {
                    return con.Query<Reserva>(@"
                    SELECT
                        A.REFERENCE As Descricao,
                        CASE NVL(A.DT_OP_INICIO, SYSDATE) WHEN SYSDATE THEN B.DATA_OP_INICIOBBK ELSE A.DT_OP_INICIO END Abertura,
                        CASE NVL(A.DT_OP_FIM, SYSDATE) WHEN SYSDATE THEN B.DATA_OP_FIMBBK ELSE A.DT_OP_FIM END Fechamento,
                        C.DELTA_HORAS As LimiteHoraLateArrival
                    FROM
                        OPERADOR.TB_CS_BOOKING A
                    INNER JOIN
                        OPERADOR.TB_VIAGENS B ON A.AUTONUMVIAGEM = B.AUTONUM
                    INNER JOIN
                        OPERADOR.VW_WEB_CAB_RESERVA C ON A.AUTONUM = C.AUTONUM_CS_BOOKING
                    WHERE
                        A.REFERENCE ='z'  AND ROWNUM = 1
                    ORDER BY
                        B.AUTONUM DESC", parametros);
                }

            }
        }

        public AgendamentoDTO ObterDadosProtocolo(int id)
        {
            using (OracleConnection con = new OracleConnection(AppConfig.StringConexao()))
            {
                var parametros = new DynamicParameters();
                parametros.Add(name: "Id", value: id, direction: ParameterDirection.Input);

                return con.Query<AgendamentoDTO>(@"
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
                        E.AUTONUM_GD_RESERVA AS PeriodoId,
                        TO_CHAR(E.PERIODO_INICIAL, 'DD/MM/YYYY HH24:MI') || ' - ' || TO_CHAR(E.PERIODO_FINAL, 'HH24:MI') PeriodoDescricao,
                        A.Protocolo,
                        A.ANO_PROTOCOLO As AnoProtocolo,
                        A.AUTONUM_USUARIO AS UsuarioId,
                        A.FLAG_IMPRESSO AS Impresso,    
                        A.AUTONUM_CS_BOOKING As BookingCsId,
                        A.FLAG_CEGONHA As Cegonha,
                        A.FLAG_DESEMBARACADO As Desembaracada,
                        A.DATA_HORA As DataCriacao,
                        A.CTE,
                        A.EMAIL_REGISTRO As EmailRegistro,
                        (     
                        SELECT  
                            RTRIM(XMLAGG(XMLELEMENT(E,(NAVIO) || ' | ')).extract('//text()'), ' | ') NAVIO 
                        FROM
                            (
                                SELECT  
                                    DISTINCT
                                        N.NOME || ' / ' || V.VIAGEM AS NAVIO
                                FROM 
                                    TB_CS_BOOKING B 
                                INNER JOIN 
                                    TB_VIAGENS V ON B.AUTONUMVIAGEM = V.AUTONUM
                                INNER JOIN 
                                    TB_CS_BOOKING_ITEM C ON B.AUTONUM = C.AUTONUM_CS_BOOKING
                                INNER JOIN 
                                    TB_CAD_NAVIOS N ON V.NAVIO = N.AUTONUM
                                WHERE
                                    C.AUTONUM IN (SELECT AUTONUM_CS_BOOKING_ITEM FROM OPERADOR.TB_AGENDAMENTO_CS_ITENS WHERE AUTONUM_AGENDAMENTO = :Id)
                            )        
                        ) Navio
                    FROM
                        OPERADOR.TB_AGENDAMENTO_CS A
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

        public bool CargaExigeNF(string reserva, int viagem)
        {
            using (OracleConnection con = new OracleConnection(AppConfig.StringConexao()))
            {
                var parametros = new DynamicParameters();

                parametros.Add(name: "Reserva", value: reserva, direction: ParameterDirection.Input);
                parametros.Add(name: "Viagem", value: viagem, direction: ParameterDirection.Input);

                var resultado = con.Query<int>("SELECT NVL(MAX(MOTIVO_DESPACHO_SEM_NF), 0) MOTIVO_DESPACHO_SEM_NF FROM OPERADOR.TB_BOOKING WHERE REFERENCE = :Reserva AND AUTONUMVIAGEM = :Viagem", parametros).Single();

                return resultado == 0;
            }
        }

        public ChassiProtocoloDTO ChassiEmOutroAgendamento(string chassi)
        {
            using (OracleConnection con = new OracleConnection(AppConfig.StringConexao()))
            {
                var parametros = new DynamicParameters();
                parametros.Add(name: "Chassi", value: chassi, direction: ParameterDirection.Input);

                return con.Query<ChassiProtocoloDTO>(@"
                    SELECT A.Chassi, C.PROTOCOLO || '/' || C.ANO_PROTOCOLO As Protocolo FROM OPERADOR.TB_AGENDAMENTO_CS_ITENS_CHASSI A 
                        INNER JOIN OPERADOR.TB_AGENDAMENTO_CS_ITENS B ON A.AUTONUM_AGENDAMENTO_ITEM = B.AUTONUM
                            INNER JOIN OPERADOR.TB_AGENDAMENTO_CS C ON B.AUTONUM_AGENDAMENTO = C.AUTONUM 
                    WHERE TRIM(A.CHASSI) = :Chassi", parametros).FirstOrDefault();
            }
        }

        public bool ItemCargaProjeto(int id)
        {
            using (OracleConnection con = new OracleConnection(AppConfig.StringConexao()))
            {
                var parametros = new DynamicParameters();
                parametros.Add(name: "BookingCsId", value: id, direction: ParameterDirection.Input);

                var resultado = con.Query<int>(@"
                    SELECT 
                        COUNT(1) As Total
                    FROM 
                        OPERADOR.TB_CS_BOOKING_ITEM
                    WHERE
                        AUTONUM = :BookingCsId
                    AND 
                        AUTONUM_CARGO_CLASS = 2", parametros).FirstOrDefault();

                return resultado > 0;
            }
        }
    }
}