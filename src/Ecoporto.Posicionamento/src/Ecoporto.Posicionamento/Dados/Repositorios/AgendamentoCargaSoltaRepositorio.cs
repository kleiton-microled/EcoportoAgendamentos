using Dapper;
using Ecoporto.Posicionamento.Config;
using Ecoporto.Posicionamento.Dados.Interfaces;
using Ecoporto.Posicionamento.Enums;
using Ecoporto.Posicionamento.Models;
using Ecoporto.Posicionamento.Models.DTO;
using Oracle.ManagedDataAccess.Client;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Ecoporto.Posicionamento.Dados.Repositorios
{
    public class AgendamentoCargaSoltaRepositorio : IAgendamentoCargaSoltaRepositorio
    {
        public Agendamento ObterPosicionamentoPorId(int id)
        {
            using (OracleConnection con = new OracleConnection(AppConfig.StringConexao()))
            {
                var parametros = new DynamicParameters();
                parametros.Add(name: "Id", value: id, direction: ParameterDirection.Input);

                return con.Query<Agendamento>($@"
                    SELECT 
                        DISTINCT
                            C.AUTONUM As Id,
                            C.PROTOCOLO_UNIFICADO As ProtocoloUnificado,
                            C.ID_STATUS_AGENDAMENTO As Status,   
                            E.CODE As MotivoId,
                            E.DESCR AS MotivoDescricao,
                            C.QUANTIDADE,
                            F.DESCR AS EMBALAGEM,
                            A.MARCA,
                            A.MODELO,
                            C.DT_PREVISTA AS DataPrevista,
                            J.RAZAO AS Exportador,
                            J.CGC As ExportadorCnpj,
                            I.NOME AS Navio,
                            H.VIAGEM AS Viagem,
                            A.REFERENCE AS Reserva,
                            A.POD As Porto,
                            A.LINE,
                            C.DATA_CANCELAMENTO As DataCancelamento,
                            C.MOTIVO_CANCELAMENTO AS MotivoCancelamento,
                            K.FANTASIA As Cliente,
                            K.CGC As ClienteCpfCnpj,
                            C.AUTONUM_CLIENTE As DespachanteId,
                            M.NOME || ' / ' || L.VIAGEM As NovaViagemDescricao
                    FROM 
                        OPERADOR.TB_CS_PATIO A
                    INNER JOIN 
                        OPERADOR.TB_CS_BOOKING_ITEM B ON A.AUTONUM_CS_BOOKING_ITEM = B.AUTONUM        
                    INNER JOIN
                        SGIPA.TB_AGENDAMENTO_POSICAO C ON B.AUTONUM = C.AUTONUM_CSPATIO
                    INNER JOIN
                        SGIPA.TB_AGENDA_POSICAO_MOTIVO D ON C.AUTONUM = D.AUTONUM_AGENDA_POSICAO
                    INNER JOIN
                        SGIPA.TB_MOTIVO_POSICAO E ON D.MOTIVO_POSICAO = E.CODE
                    LEFT JOIN
                        OPERADOR.DTE_TB_EMBALAGENS F ON A.AUTONUM_CS_EMBALAGEM = F.CODE
                    LEFT JOIN 
                        OPERADOR.TB_CAD_PORTOS G ON A.POD = G.CODIGO
                    LEFT JOIN 
                        OPERADOR.TB_VIAGENS H ON A.AUTONUMVIAGEM = H.AUTONUM
                    LEFT JOIN 
                        OPERADOR.TB_CAD_NAVIOS I ON H.NAVIO = I.AUTONUM
                    LEFT JOIN 
                        OPERADOR.TB_CAD_CLIENTES J ON A.SHIPPER = J.AUTONUM
                    LEFT JOIN 
                        OPERADOR.TB_CAD_CLIENTES K ON C.AUTONUM_FATURA = K.AUTONUM
                    LEFT JOIN 
                        OPERADOR.TB_VIAGENS L ON C.VIAGEM_NOVA = L.AUTONUM
                    LEFT JOIN 
                        OPERADOR.TB_CAD_NAVIOS M ON L.NAVIO = M.AUTONUM
                    WHERE
                        C.AUTONUM = :Id", parametros).FirstOrDefault();
            }
        }

        public IEnumerable<AgendamentoDTO> ObterAgendamentos(int despachanteId)
        {
            using (OracleConnection con = new OracleConnection(AppConfig.StringConexao()))
            {
                var parametros = new DynamicParameters();
                parametros.Add(name: "DespachanteId", value: despachanteId, direction: ParameterDirection.Input);

                return con.Query<AgendamentoDTO>($@"
                    SELECT                         
                        AUTONUM As Id,
                        BOOKING As Reserva,
                        VIAGEM,
                        LINE,
                        Exportador,
                        Quantidade,
                        Agendado As QuantidadeAgendada,
                        Embalagem,
                        Marca,    
                        Modelo,
                        Motivo,
                        DT_PREVISTA As DataPrevista,
                        Cancelado,
                        Status,
                        Data_cancelamento As DataCancelamento
                    FROM 
                        SGIPA.VW_AGENDA_POSICAO_WEB_CS
                    WHERE
                        AUTONUM_CLIENTE = :DespachanteId
                    AND
                        PROTOCOLO_UNIFICADO > 0", parametros);
            }
        }

        public Agendamento ObterDetalhesCargaSoltaPorReserva(string reserva)
        {
            using (OracleConnection con = new OracleConnection(AppConfig.StringConexao()))
            {
                var parametros = new DynamicParameters();
                parametros.Add(name: "Reserva", value: reserva, direction: ParameterDirection.Input);

                return con.Query<Agendamento>($@"
                    SELECT
                        Reserva,
                        Exportador, 
                        Viagem, 
                        Navio, 
                        Porto  
                    FROM 
                        OPERADOR.VW_POSICIONA_CS
                    WHERE 
                        RESERVA = :Reserva", parametros).FirstOrDefault();
            }
        }

        public IEnumerable<CargaSolta> ObterCargaSoltaPorReserva(string reserva)
        {
            using (OracleConnection con = new OracleConnection(AppConfig.StringConexao()))
            {
                var parametros = new DynamicParameters();
                parametros.Add(name: "Reserva", value: reserva, direction: ParameterDirection.Input);

                return con.Query<CargaSolta>(@"
                    SELECT    
                        Reserva,
                        BookingCsItemId, 
                        Embalagem, 
                        Marca, 
                        Modelo,
                        SUM(Quantidade) As Quantidade
                    FROM 
                        OPERADOR.VW_POSICIONA_CS
                    WHERE 
                        RESERVA = :Reserva
                    GROUP BY
                        Reserva,
                        BookingCsItemId,
                        Embalagem,
                        Marca,
                        Modelo", parametros);
            }
        }

        public CargaSolta ObterCargaSoltaAgendamento(int agendamentoId)
        {
            using (OracleConnection con = new OracleConnection(AppConfig.StringConexao()))
            {
                var parametros = new DynamicParameters();
                parametros.Add(name: "AgendamentoId", value: agendamentoId, direction: ParameterDirection.Input);

                return con.Query<CargaSolta>(@"
                    SELECT 
                        C.AUTONUM As Id,
                        A.QUANTIDADE,
                        C.QUANTIDADE As QuantidadeAgendada,
                        D.DESCR AS EMBALAGEM,
                        A.MARCA,
                        A.MODELO
                    FROM 
                        OPERADOR.TB_CS_PATIO A
                    INNER JOIN 
                        OPERADOR.TB_CS_BOOKING_ITEM B ON A.AUTONUM_CS_BOOKING_ITEM = B.AUTONUM    
                    INNER JOIN
                        SGIPA.TB_AGENDAMENTO_POSICAO C ON B.AUTONUM = C.AUTONUM_CSPATIO
                    LEFT JOIN
                        OPERADOR.DTE_TB_EMBALAGENS D ON A.AUTONUM_CS_EMBALAGEM = D.CODE
                    WHERE 
                        C.AUTONUM = :AgendamentoId", parametros).FirstOrDefault();
            }
        }

        public IEnumerable<CargaSolta> ObterItensCargaSoltaAgendamento(int protocoloUnificado)
        {
            using (OracleConnection con = new OracleConnection(AppConfig.StringConexao()))
            {
                var parametros = new DynamicParameters();
                parametros.Add(name: "ProtocoloUnificado", value: protocoloUnificado, direction: ParameterDirection.Input);

                return con.Query<CargaSolta>(@"
                    SELECT 
                        C.AUTONUM As AgendamentoId,
                        SUM(A.QUANTIDADE) As Quantidade,
                        C.QUANTIDADE As QuantidadeAgendada,
                        D.DESCR AS EMBALAGEM,
                        A.MARCA,
                        A.MODELO
                    FROM 
                        OPERADOR.TB_CS_PATIO A
                    INNER JOIN 
                        OPERADOR.TB_CS_BOOKING_ITEM B ON A.AUTONUM_CS_BOOKING_ITEM = B.AUTONUM    
                    INNER JOIN
                        SGIPA.TB_AGENDAMENTO_POSICAO C ON B.AUTONUM = C.AUTONUM_CSPATIO
                    LEFT JOIN
                        OPERADOR.DTE_TB_EMBALAGENS D ON A.AUTONUM_CS_EMBALAGEM = D.CODE
                    WHERE 
                        C.PROTOCOLO_UNIFICADO = :ProtocoloUnificado
                    GROUP BY
                        C.AUTONUM,
                        C.QUANTIDADE,
                        D.DESCR,
                        A.MARCA,
                        A.MODELO", parametros);
            }
        }

        public void CancelarAgendamento(int id, string motivo)
        {
            using (OracleConnection con = new OracleConnection(AppConfig.StringConexao()))
            {
                var parametros = new DynamicParameters();

                parametros.Add(name: "Id", value: id, direction: ParameterDirection.Input);
                parametros.Add(name: "Motivo", value: motivo, direction: ParameterDirection.Input);
                parametros.Add(name: "Status", value: StatusPosicionamento.Cancelado, direction: ParameterDirection.Input);

                con.Execute(@"UPDATE SGIPA.TB_AGENDAMENTO_POSICAO SET CANCELADO = 1, MOTIVO_CANCELAMENTO = :Motivo, ID_STATUS_AGENDAMENTO = :Status, DATA_CANCELAMENTO = SYSDATE WHERE AUTONUM = :Id", parametros);
            }
        }

        public int GravarPosicionamentoCntr(Agendamento agendamento)
        {            
            using (OracleConnection con = new OracleConnection(AppConfig.StringConexao()))
            {
                con.Open();

                using (var transaction = con.BeginTransaction())
                {
                    var protocoloUnificado = con.Query<int>($@"SELECT SGIPA.TB_AGENDA_POSICAO_PROTO_UNIF.NEXTVAL FROM DUAL", transaction).Single();

                    foreach (var conteiner in agendamento.ConteineresSelecionados)
                    {
                        var parametros = new DynamicParameters();

                        parametros.Add(name: "Cntr", value: conteiner, direction: ParameterDirection.Input);
                        parametros.Add(name: "DataPrevista", value: agendamento.DataPrevista, direction: ParameterDirection.Input);
                        parametros.Add(name: "DespachanteId", value: agendamento.DespachanteId, direction: ParameterDirection.Input);
                        parametros.Add(name: "ClienteId", value: agendamento.ClienteId, direction: ParameterDirection.Input);
                        parametros.Add(name: "Viagem", value: agendamento.NovaViagemId, direction: ParameterDirection.Input);
                        parametros.Add(name: "ProtocoloUnificado", value: protocoloUnificado, direction: ParameterDirection.Input);
                        parametros.Add(name: "Status", value: agendamento.Status, direction: ParameterDirection.Input);

                        parametros.Add(name: "Id", dbType: DbType.Int32, direction: ParameterDirection.Output);

                        con.Execute($@"INSERT 
                            INTO SGIPA.TB_AGENDAMENTO_POSICAO 
                                (
                                    AUTONUM, 
                                    CNTR, 
                                    AUTONUMPATIO, 
                                    DT_PREVISTA, 
                                    DATA_ATUALIZA, 
                                    PEDIDO_PELA_INTERNET, 
                                    ID_STATUS_AGENDAMENTO, 
                                    AUTONUM_CLIENTE, 
                                    AUTONUM_FATURA, 
                                    VIAGEM_NOVA,
                                    PROTOCOLO_UNIFICADO
                                ) VALUES (
                                    SGIPA.SEQ_AGENDAMENTO_POSICAO.NEXTVAL, 
                                    0, 
                                    :Cntr, 
                                    :DataPrevista, 
                                    SYSDATE, 
                                    'S', 
                                    :Status, 
                                    :DespachanteId, 
                                    :ClienteId, 
                                    :Viagem,
                                    :ProtocoloUnificado
                                ) RETURNING AUTONUM INTO :Id", parametros, transaction);

                        var id = parametros.Get<int>("Id");

                        parametros = new DynamicParameters();

                        parametros.Add(name: "AgendamentoId", value: id, direction: ParameterDirection.Input);
                        parametros.Add(name: "MotivoPosicionamentoId", value: agendamento.MotivoId, direction: ParameterDirection.Input);

                        con.Execute("INSERT INTO SGIPA.TB_AGENDA_POSICAO_MOTIVO (AUTONUM, AUTONUM_AGENDA_POSICAO, MOTIVO_POSICAO) VALUES (SGIPA.SEQ_AGENDA_POSICAO_MOTIVO.NEXTVAL, :AgendamentoId, :MotivoPosicionamentoId)", parametros, transaction);
                    }

                    transaction.Commit();

                    return protocoloUnificado;
                }
            }
        }

        public int GravarPosicionamentoCargaSolta(Agendamento agendamento)
        {
            using (OracleConnection con = new OracleConnection(AppConfig.StringConexao()))
            {
                con.Open();

                using (var transaction = con.BeginTransaction())
                {
                    var protocoloUnificado = con.Query<int>($@"SELECT SGIPA.TB_AGENDA_POSICAO_PROTO_UNIF.NEXTVAL FROM DUAL", transaction).Single();

                    foreach (var itemCs in agendamento.ItensCargaSolta)
                    {
                        var parametros = new DynamicParameters();

                        parametros.Add(name: "BookingCsItemId", value: itemCs.BookingCsItemId, direction: ParameterDirection.Input);
                        parametros.Add(name: "Quantidade", value: itemCs.QuantidadeAgendada, direction: ParameterDirection.Input);
                        parametros.Add(name: "DataPrevista", value: agendamento.DataPrevista, direction: ParameterDirection.Input);
                        parametros.Add(name: "DespachanteId", value: agendamento.DespachanteId, direction: ParameterDirection.Input);
                        parametros.Add(name: "ClienteId", value: agendamento.ClienteId, direction: ParameterDirection.Input);
                        parametros.Add(name: "Viagem", value: agendamento.NovaViagemId, direction: ParameterDirection.Input);
                        parametros.Add(name: "ProtocoloUnificado", value: protocoloUnificado, direction: ParameterDirection.Input);
                        parametros.Add(name: "Status", value: agendamento.Status, direction: ParameterDirection.Input);

                        parametros.Add(name: "Id", dbType: DbType.Int32, direction: ParameterDirection.Output);

                        con.Execute($@"INSERT 
                            INTO SGIPA.TB_AGENDAMENTO_POSICAO 
                                (
                                    AUTONUM, 
                                    CNTR, 
                                    AUTONUM_CSPATIO, 
                                    QUANTIDADE,
                                    DT_PREVISTA, 
                                    DATA_ATUALIZA, 
                                    PEDIDO_PELA_INTERNET, 
                                    ID_STATUS_AGENDAMENTO, 
                                    AUTONUM_CLIENTE, 
                                    AUTONUM_FATURA, 
                                    VIAGEM_NOVA,
                                    PROTOCOLO_UNIFICADO
                                ) VALUES (
                                    SGIPA.SEQ_AGENDAMENTO_POSICAO.NEXTVAL, 
                                    0, 
                                    :BookingCsItemId, 
                                    :Quantidade,
                                    :DataPrevista, 
                                    SYSDATE, 
                                    'S', 
                                    :Status, 
                                    :DespachanteId, 
                                    :ClienteId, 
                                    :Viagem,
                                    :ProtocoloUnificado
                                ) RETURNING AUTONUM INTO :Id", parametros, transaction);

                        var id = parametros.Get<int>("Id");

                        parametros = new DynamicParameters();

                        parametros.Add(name: "AgendamentoId", value: id, direction: ParameterDirection.Input);
                        parametros.Add(name: "MotivoPosicionamentoId", value: agendamento.MotivoId, direction: ParameterDirection.Input);

                        con.Execute("INSERT INTO SGIPA.TB_AGENDA_POSICAO_MOTIVO (AUTONUM, AUTONUM_AGENDA_POSICAO, MOTIVO_POSICAO) VALUES (SGIPA.SEQ_AGENDA_POSICAO_MOTIVO.NEXTVAL, :AgendamentoId, :MotivoPosicionamentoId)", parametros, transaction);
                    }

                    transaction.Commit();

                    return protocoloUnificado;
                }
            }
        }
    }
}