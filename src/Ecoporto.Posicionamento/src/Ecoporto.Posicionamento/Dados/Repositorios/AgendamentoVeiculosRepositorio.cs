using Dapper;
using Ecoporto.Posicionamento.Config;
using Ecoporto.Posicionamento.Dados.Interfaces;
using Ecoporto.Posicionamento.Enums;
using Ecoporto.Posicionamento.Models;
using Ecoporto.Posicionamento.Models.DTO;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Ecoporto.Posicionamento.Dados.Repositorios
{
    public class AgendamentoVeiculosRepositorio : IAgendamentoVeiculosRepositorio
    {
        public Agendamento ObterPosicionamentoPorId(int id)
        {
            using (OracleConnection con = new OracleConnection(AppConfig.StringConexao()))
            {
                var parametros = new DynamicParameters();
                parametros.Add(name: "Id", value: id, direction: ParameterDirection.Input);

                return con.Query<Agendamento>($@"
                    SELECT
                        A.AUTONUM As Id,
                        A.PROTOCOLO_UNIFICADO As ProtocoloUnificado,
                        A.ID_STATUS_AGENDAMENTO As Status,    
                        C.DESCR AS MotivoDescricao,
                        D.QUANTIDADE,
                        EMB.DESCR AS EMBALAGEM,
                        D.MARCA,
                        D.MODELO,
                        A.DT_PREVISTA AS DataPrevista,
                        G.RAZAO AS Exportador,
                        G.CGC As ExportadorCnpj,
                        F.NOME AS Navio,
                        E.VIAGEM AS Viagem,
                        D.REFERENCE AS Reserva,
                        D.POD As Porto,
                        D.LINE,
                        A.DATA_CANCELAMENTO As DataCancelamento,
                        A.MOTIVO_CANCELAMENTO AS MotivoCancelamento,
                        H.FANTASIA As Cliente,
                        H.CGC As ClienteCpfCnpj,
                        A.AUTONUM_CLIENTE As DespachanteId,
                        J.NOME || ' / ' || I.VIAGEM As NovaViagemDescricao
                    FROM
                        SGIPA.TB_AGENDAMENTO_POSICAO A
                    INNER JOIN 
                        SGIPA.TB_AGENDA_POSICAO_MOTIVO B ON A.AUTONUM = B.AUTONUM_AGENDA_POSICAO
                    INNER JOIN 
                        SGIPA.TB_MOTIVO_POSICAO C ON B.MOTIVO_POSICAO = C.CODE
                    INNER JOIN 
                        OPERADOR.TB_CS_BOOKING_ITEM_ID BID ON A.AUTONUM_ITEM_ID = BID.AUTONUM
                    INNER JOIN 
                        OPERADOR.TB_CS_PATIO D ON BID.AUTONUM_CS_PATIO = D.AUTONUM
                    LEFT JOIN 
                        OPERADOR.DTE_TB_EMBALAGENS EMB ON D.AUTONUM_CS_EMBALAGEM = EMB.CODE
                    LEFT JOIN 
                        OPERADOR.TB_VIAGENS E ON D.AUTONUMVIAGEM = E.AUTONUM
                    LEFT JOIN 
                        OPERADOR.TB_CAD_NAVIOS F ON E.NAVIO = F.AUTONUM
                    LEFT JOIN 
                        OPERADOR.TB_CAD_CLIENTES G ON D.SHIPPER = G.AUTONUM
                    LEFT JOIN 
                        OPERADOR.TB_CAD_CLIENTES H ON A.AUTONUM_FATURA = H.AUTONUM 
                    LEFT JOIN 
                        OPERADOR.TB_VIAGENS I ON A.VIAGEM_NOVA = I.AUTONUM
                    LEFT JOIN 
                        OPERADOR.TB_CAD_NAVIOS J ON I.NAVIO = J.AUTONUM
                    WHERE
                        A.AUTONUM = :Id", parametros).FirstOrDefault();
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
                        Id,
                        Reserva,
                        VIAGEM,
                        LINE,
                        Exportador,
                        Chassis,
                        Embalagem,
                        Marca,    
                        Modelo,
                        Motivo,
                        DataPrevista,
                        Cancelado,
                        Status,
                        DataCancelamento
                    FROM 
                        SGIPA.VW_AGENDA_POSICAO_WEB_VEICULOS
                    WHERE
                        AUTONUM_CLIENTE = :DespachanteId
                    AND
                        PROTOCOLO_UNIFICADO > 0", parametros);
            }
        }

        public Agendamento ObterDetalhesVeiculoPorReserva(string reserva)
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
                        OPERADOR.VW_POSICIONA_VEICULOS
                    WHERE 
                        RESERVA = :Reserva", parametros).FirstOrDefault();
            }
        }

        public IEnumerable<CargaSolta> ObterVeiculoPorReserva(string reserva)
        {
            using (OracleConnection con = new OracleConnection(AppConfig.StringConexao()))
            {
                var parametros = new DynamicParameters();
                parametros.Add(name: "Reserva", value: reserva, direction: ParameterDirection.Input);

                return con.Query<CargaSolta>(@"
                    SELECT
                        DISTINCT
                            Reserva,
                            BookingCsItemChassi, 
                            Chassis,
                            Embalagem, 
                            Marca, 
                            Modelo
                        FROM 
                            OPERADOR.VW_POSICIONA_VEICULOS
                        WHERE 
                            RESERVA = :Reserva", parametros);
            }
        }

        public VeiculoAgendadoDTO ObterVeiculoAgendamento(int agendamentoId)
        {
            using (OracleConnection con = new OracleConnection(AppConfig.StringConexao()))
            {
                var parametros = new DynamicParameters();
                parametros.Add(name: "AgendamentoId", value: agendamentoId, direction: ParameterDirection.Input);

                return con.Query<VeiculoAgendadoDTO>(@"
                    SELECT 
                        A.AUTONUM As AgendamentoId,                        
                        D.CHASSIS,
                        E.MARCA,
                        E.MODELO,
                        F.DESCR As Embalagem
                    FROM 
                        SGIPA.TB_AGENDAMENTO_POSICAO A
                    INNER JOIN 
                        SGIPA.TB_AGENDA_POSICAO_MOTIVO B ON A.AUTONUM = B.AUTONUM_AGENDA_POSICAO
                    INNER JOIN 
                        SGIPA.TB_MOTIVO_POSICAO C ON B.MOTIVO_POSICAO = C.CODE
                    INNER JOIN 
                        OPERADOR.TB_CS_BOOKING_ITEM_ID D ON A.AUTONUM_ITEM_ID = D.AUTONUM
                    INNER JOIN 
                        OPERADOR.TB_CS_PATIO E ON D.AUTONUM_CS_PATIO = E.AUTONUM
                    LEFT JOIN 
                        OPERADOR.DTE_TB_EMBALAGENS F ON E.AUTONUM_CS_EMBALAGEM = F.CODE    
                    WHERE 
                        A.AUTONUM = :AgendamentoId", parametros).FirstOrDefault();
            }
        }

        public IEnumerable<VeiculoAgendadoDTO> ObterItensVeiculoAgendamento(int protocoloUnificado)
        {
            using (OracleConnection con = new OracleConnection(AppConfig.StringConexao()))
            {
                var parametros = new DynamicParameters();
                parametros.Add(name: "ProtocoloUnificado", value: protocoloUnificado, direction: ParameterDirection.Input);

                return con.Query<VeiculoAgendadoDTO>(@"
                    SELECT                         
                        Id As AgendamentoId,                        
                        Marca,    
                        Modelo,
                        Embalagem,
                        Chassis,
                        Status
                    FROM 
                        SGIPA.VW_AGENDA_POSICAO_WEB_VEICULOS
                    WHERE
                        PROTOCOLO_UNIFICADO = :ProtocoloUnificado", parametros);
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

        public int GravarPosicionamentoVeiculo(Agendamento agendamento)
        {
            using (OracleConnection con = new OracleConnection(AppConfig.StringConexao()))
            {
                con.Open();

                using (var transaction = con.BeginTransaction())
                {
                    var protocoloUnificado = con.Query<int>($@"SELECT SGIPA.TB_AGENDA_POSICAO_PROTO_UNIF.NEXTVAL FROM DUAL", transaction).Single();

                    foreach (var veiculo in agendamento.VeiculosSelecionados)
                    {
                        var parametros = new DynamicParameters();

                        parametros.Add(name: "BookingCsItemChassi", value: veiculo, direction: ParameterDirection.Input);
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
                                    AUTONUM_ITEM_ID, 
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
                                    :BookingCsItemChassi, 
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

        public AgendamentoDTO ExistePosicionamento(int motivo, DateTime dataPrevista, int itemId)
        {
            using (OracleConnection con = new OracleConnection(AppConfig.StringConexao()))
            {
                var parametros = new DynamicParameters();

                parametros.Add(name: "MotivoId", value: motivo, direction: ParameterDirection.Input);
                parametros.Add(name: "DataPrevista", value: dataPrevista, direction: ParameterDirection.Input);
                parametros.Add(name: "ItemId", value: itemId, direction: ParameterDirection.Input);

                return con.Query<AgendamentoDTO>(@"
                    SELECT
                        A.AUTONUM As Id,
                        BID.Chassis,
                        C.DESCR AS Motivo
                    FROM
                        SGIPA.TB_AGENDAMENTO_POSICAO A
                    INNER JOIN 
                        SGIPA.TB_AGENDA_POSICAO_MOTIVO B ON A.AUTONUM = B.AUTONUM_AGENDA_POSICAO
                    INNER JOIN 
                        SGIPA.TB_MOTIVO_POSICAO C ON B.MOTIVO_POSICAO = C.CODE
                    INNER JOIN 
                        OPERADOR.TB_CS_BOOKING_ITEM_ID BID ON A.AUTONUM_ITEM_ID = BID.AUTONUM 
                    WHERE 
                        B.MOTIVO_POSICAO = :MotivoId
                    AND 
                        A.AUTONUM_ITEM_ID = :ItemId
                    AND 
                        A.DT_PREVISTA = :DataPrevista
                    AND
                        A.CANCELADO = 0", parametros).FirstOrDefault();
            }
        }
    }
}