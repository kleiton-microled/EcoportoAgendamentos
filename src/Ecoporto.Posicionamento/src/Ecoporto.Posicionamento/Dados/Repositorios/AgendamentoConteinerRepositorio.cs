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
    public class AgendamentoConteinerRepositorio : IAgendamentoConteinerRepositorio
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
                        D.TIPOBASICO AS Tipo,
                        C.CODE As MotivoId,
                        C.DESCR AS MotivoDescricao,
                        D.TAMANHO AS Tamanho,
                        D.ID_CONTEINER AS Sigla,
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
                        OPERADOR.TB_PATIO D ON A.AUTONUMPATIO = D.AUTONUM
                    INNER JOIN 
                        OPERADOR.TB_VIAGENS E ON D.AUTONUMVIAGEM = E.AUTONUM
                    INNER JOIN 
                        OPERADOR.TB_CAD_NAVIOS F ON E.NAVIO = F.AUTONUM 
                    INNER JOIN 
                        OPERADOR.TB_CAD_CLIENTES G ON D.SHIPPER = G.AUTONUM
                    INNER JOIN 
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
                        AUTONUM As Id,
                        BOOKING As Reserva,
                        VIAGEM,
                        LINE,
                        Exportador,
                        ID_CONTEINER As Sigla,
                        TAMANHO,
                        TIPOCNTR as Tipo,    
                        MOTIVOPOS As Motivo,
                        DT_PREVISTA As DataPrevista,
                        Cancelado,
                        Status,
                        Data_cancelamento As DataCancelamento
                    FROM 
                        SGIPA.VW_AGENDA_POSICAO_WEB
                    WHERE
                        AUTONUM_CLIENTE = :DespachanteId
                    AND
                        PROTOCOLO_UNIFICADO > 0", parametros);
            }
        }

        public Agendamento ObterDetalhesConteinerPorReserva(string reserva)
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
                        OPERADOR.VW_POSICIONA_CNTR
                    WHERE 
                        RESERVA = :Reserva", parametros).FirstOrDefault();
            }
        }

        public IEnumerable<Conteiner> ObterConteineresPorReserva(string reserva)
        {
            using (OracleConnection con = new OracleConnection(AppConfig.StringConexao()))
            {
                var parametros = new DynamicParameters();
                parametros.Add(name: "Reserva", value: reserva, direction: ParameterDirection.Input);

                return con.Query<Conteiner>(@"
                    SELECT 
                        A.AUTONUM As Id,
                        A.Conteiner As Sigla,
                        A.Tamanho,
                        A.TipoBasico As Tipo,
                        B.DESCR As TipoDescricao,
                        A.Tamanho                      
                    FROM 
                        OPERADOR.VW_POSICIONA_CNTR A
                    LEFT JOIN
                        SGIPA.DTE_TB_TIPOS_CONTEINER B ON A.TipoBasico = B.Codigo
                    WHERE 
                        A.Reserva = :Reserva", parametros);
            }
        }

        public IEnumerable<Conteiner> ObterConteineresAgendamento(int protocoloUnificado)
        {
            using (OracleConnection con = new OracleConnection(AppConfig.StringConexao()))
            {
                var parametros = new DynamicParameters();
                parametros.Add(name: "ProtocoloUnificado", value: protocoloUnificado, direction: ParameterDirection.Input);
                
                return con.Query<Conteiner>(@"
                    SELECT 
                        B.AUTONUM AS AgendamentoId, 
                        A.ID_CONTEINER AS Sigla, 
                        A.TipoBasico As Tipo, 
                        C.DESCR As TipoDescricao,
                        A.Tamanho 
                    FROM 
                        OPERADOR.TB_PATIO A
                    INNER JOIN
                        SGIPA.TB_AGENDAMENTO_POSICAO B ON A.AUTONUM = B.AUTONUMPATIO
                    LEFT JOIN
                        SGIPA.DTE_TB_TIPOS_CONTEINER C ON A.TipoBasico = C.Codigo
                    WHERE 
                        B.PROTOCOLO_UNIFICADO = :ProtocoloUnificado
                    ORDER BY
                        A.ID_CONTEINER", parametros);
            }
        }

        public Conteiner ObterConteinerAgendamento(int agendamentoId)
        {
            using (OracleConnection con = new OracleConnection(AppConfig.StringConexao()))
            {
                var parametros = new DynamicParameters();
                parametros.Add(name: "AgendamentoId", value: agendamentoId, direction: ParameterDirection.Input);

                return con.Query<Conteiner>(@"
                    SELECT 
                        A.AUTONUM As Id,
                        A.ID_CONTEINER AS Sigla, 
                        A.TipoBasico As Tipo,
                        C.DESCR As TipoDescricao,
                        A.Tamanho 
                    FROM 
                        OPERADOR.TB_PATIO A
                    INNER JOIN
                        SGIPA.TB_AGENDAMENTO_POSICAO B ON A.AUTONUM = B.AUTONUMPATIO
                    LEFT JOIN
                        SGIPA.DTE_TB_TIPOS_CONTEINER C ON A.TipoBasico = C.Codigo
                    WHERE 
                        B.AUTONUM = :AgendamentoId", parametros).FirstOrDefault();
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

        public AgendamentoDTO ExistePosicionamento(int motivo, DateTime dataPrevista, int conteiner)
        {
            using (OracleConnection con = new OracleConnection(AppConfig.StringConexao()))
            {
                var parametros = new DynamicParameters();

                parametros.Add(name: "MotivoId", value: motivo, direction: ParameterDirection.Input);
                parametros.Add(name: "DataPrevista", value: dataPrevista, direction: ParameterDirection.Input);
                parametros.Add(name: "Conteiner", value: conteiner, direction: ParameterDirection.Input);

                return con.Query<AgendamentoDTO>(@"
                    SELECT 
                        A.AUTONUM As Id,
                        D.ID_CONTEINER As Sigla,
                        C.DESCR AS Motivo
                    FROM 
                        SGIPA.TB_AGENDAMENTO_POSICAO A
                    INNER JOIN    
                        SGIPA.TB_AGENDA_POSICAO_MOTIVO B ON A.AUTONUM = B.AUTONUM_AGENDA_POSICAO 
                    INNER JOIN 
                        SGIPA.TB_MOTIVO_POSICAO C ON B.MOTIVO_POSICAO = C.CODE    
                    INNER JOIN
                        OPERADOR.TB_PATIO D ON A.AUTONUMPATIO = D.AUTONUM 
                    WHERE 
                        B.MOTIVO_POSICAO = :MotivoId
                    AND 
                        A.AUTONUMPATIO = :Conteiner
                    AND 
                        A.DT_PREVISTA = :DataPrevista
                    AND
                        A.CANCELADO = 0", parametros).FirstOrDefault();
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
    }
}