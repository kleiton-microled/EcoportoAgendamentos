using Dapper;
using Ecoporto.AgendamentoConteiner.Config;
using Ecoporto.AgendamentoConteiner.Dados.Interfaces;
using Ecoporto.AgendamentoConteiner.Enums;
using Ecoporto.AgendamentoConteiner.Extensions;
using Ecoporto.AgendamentoConteiner.Models;
using Ecoporto.AgendamentoConteiner.Models.DTO;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Ecoporto.AgendamentoConteiner.Dados.Repositorios
{
    public class AgendamentoRepositorio : IAgendamentoRepositorio
    {
        public int Cadastrar(Agendamento agendamento)
        {
            using (OracleConnection con = new OracleConnection(AppConfig.StringConexao()))
            {
                var parametros = new DynamicParameters();

                parametros.Add(name: "MotoristaId", value: agendamento.MotoristaId, direction: ParameterDirection.Input);
                parametros.Add(name: "VeiculoId", value: agendamento.VeiculoId, direction: ParameterDirection.Input);
                parametros.Add(name: "Cegonha", value: agendamento.Cegonha.ToInt(), direction: ParameterDirection.Input);
                parametros.Add(name: "TransportadoraId", value: agendamento.TransportadoraId, direction: ParameterDirection.Input);
                parametros.Add(name: "UsuarioId", value: agendamento.UsuarioId, direction: ParameterDirection.Input);
                parametros.Add(name: "CTE", value: agendamento.CTE, direction: ParameterDirection.Input);
                parametros.Add(name: "TipoOperacao", value: agendamento.TipoOperacao, direction: ParameterDirection.Input);                

                parametros.Add(name: "Id", dbType: DbType.Int32, direction: ParameterDirection.Output);

                con.Execute(@"
                        INSERT INTO OPERADOR.TB_AGENDAMENTO_CNTR
                            ( 
                                AUTONUM,
                                AUTONUM_MOTORISTA,
                                AUTONUM_VEICULO,                                
                                FLAG_CEGONHA,
                                AUTONUM_TRANSPORTADORA,
                                AUTONUM_USUARIO,                                
                                CTE,
                                TIPO_OPERACAO                                
                            ) VALUES ( 
                                OPERADOR.SEQ_AGENDAMENTO_CNTR.NEXTVAL, 
                                :MotoristaId,
                                :VeiculoId,
                                :Cegonha,
                                :TransportadoraId,
                                :UsuarioId,
                                :CTE,
                                :TipoOperacao
                            ) RETURNING AUTONUM INTO :Id", parametros);

                return parametros.Get<int>("Id");
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
                    parametros.Add(name: "PeriodoId", value: agendamento.PeriodoId, direction: ParameterDirection.Input);
                    parametros.Add(name: "CTE", value: agendamento.CTE, direction: ParameterDirection.Input);
                    parametros.Add(name: "EmailRegistro", value: agendamento.EmailRegistro, direction: ParameterDirection.Input);
                    parametros.Add(name: "AnoProtocolo", value: DateTime.Now.Year, direction: ParameterDirection.Input);
                    parametros.Add(name: "DueDesembaracada", value: agendamento.DueDesembaracada.ToInt(), direction: ParameterDirection.Input);
                    parametros.Add(name: "Id", value: agendamento.Id, direction: ParameterDirection.Input);

                    con.Execute(@"UPDATE OPERADOR.TB_AGENDAMENTO_CNTR SET FLAG_DUE_DESEMBARACADA = :DueDesembaracada, AUTONUM_PERIODO = :PeriodoId, EMAIL_REGISTRO = :EmailRegistro, PROTOCOLO = OPERADOR.SEQ_PROTOCOLO_AG_GERAL.NEXTVAL, ANO_PROTOCOLO = :AnoProtocolo WHERE AUTONUM = :Id", parametros, transaction);

                    con.Execute(@"DELETE FROM OPERADOR.TB_AG_CNTR_ITENS_DANFES WHERE AUTONUM_AGENDAMENTO_ITEM_CNTR IN (SELECT AUTONUM FROM OPERADOR.TB_AGENDAMENTO_CNTR_ITEM_CNTR WHERE AUTONUM_AGENDAMENTO = :Id)", parametros, transaction);
                    con.Execute(@"DELETE FROM OPERADOR.TB_AGENDAMENTO_CNTR_ITEM_CNTR WHERE AUTONUM_AGENDAMENTO = :Id", parametros, transaction);

                    foreach (var conteiner in agendamento.Conteineres)
                    {
                        parametros = new DynamicParameters();

                        parametros.Add(name: "AgendamentoId", value: agendamento.Id, direction: ParameterDirection.Input);
                        parametros.Add(name: "BookingId", value: conteiner.Reserva.BookingId, direction: ParameterDirection.Input);
                        parametros.Add(name: "Sigla", value: conteiner.Sigla, direction: ParameterDirection.Input);                        
                        parametros.Add(name: "IMO1", value: conteiner.IMO1, direction: ParameterDirection.Input);
                        parametros.Add(name: "IMO2", value: conteiner.IMO2, direction: ParameterDirection.Input);
                        parametros.Add(name: "IMO3", value: conteiner.IMO3, direction: ParameterDirection.Input);
                        parametros.Add(name: "IMO4", value: conteiner.IMO4, direction: ParameterDirection.Input);
                        parametros.Add(name: "ONU1", value: conteiner.ONU1, direction: ParameterDirection.Input);
                        parametros.Add(name: "ONU2", value: conteiner.ONU2, direction: ParameterDirection.Input);
                        parametros.Add(name: "ONU3", value: conteiner.ONU3, direction: ParameterDirection.Input);
                        parametros.Add(name: "ONU4", value: conteiner.ONU4, direction: ParameterDirection.Input);
                        parametros.Add(name: "Lacre1", value: conteiner.LacreArmador1, direction: ParameterDirection.Input);
                        parametros.Add(name: "Lacre2", value: conteiner.LacreArmador2, direction: ParameterDirection.Input);
                        parametros.Add(name: "Lacre3", value: conteiner.OutrosLacres1, direction: ParameterDirection.Input);
                        parametros.Add(name: "Lacre4", value: conteiner.OutrosLacres2, direction: ParameterDirection.Input);
                        parametros.Add(name: "Lacre5", value: conteiner.OutrosLacres3, direction: ParameterDirection.Input);
                        parametros.Add(name: "Lacre6", value: conteiner.OutrosLacres4, direction: ParameterDirection.Input);
                        parametros.Add(name: "Lacre7", value: conteiner.LacreExportador, direction: ParameterDirection.Input);
                        parametros.Add(name: "OH", value: conteiner.Altura, direction: ParameterDirection.Input);
                        parametros.Add(name: "OW", value: conteiner.LateralDireita, direction: ParameterDirection.Input);
                        parametros.Add(name: "OWL", value: conteiner.LateralEsquerda, direction: ParameterDirection.Input);
                        parametros.Add(name: "OL", value: conteiner.Comprimento, direction: ParameterDirection.Input);
                        parametros.Add(name: "Tamanho", value: conteiner.Reserva.Tamanho, direction: ParameterDirection.Input);
                        parametros.Add(name: "TipoBasico", value: conteiner.Reserva.Tipo, direction: ParameterDirection.Input);
                        parametros.Add(name: "Volumes", value: conteiner.Volumes, direction: ParameterDirection.Input);                        
                        parametros.Add(name: "LacreSIF", value: conteiner.LacreSIF, direction: ParameterDirection.Input);
                        parametros.Add(name: "Tara", value: conteiner.Tara, direction: ParameterDirection.Input);
                        parametros.Add(name: "Bruto", value: conteiner.Bruto, direction: ParameterDirection.Input);
                        parametros.Add(name: "PesoLiquido", value: conteiner.PesoLiquido, direction: ParameterDirection.Input);
                        parametros.Add(name: "DAT", value: conteiner.DAT, direction: ParameterDirection.Input);
                        parametros.Add(name: "EF", value: conteiner.Reserva.EF.ToName(), direction: ParameterDirection.Input);
                        parametros.Add(name: "OBS", value: conteiner.Observacoes, direction: ParameterDirection.Input);
                        parametros.Add(name: "Qtde", value: conteiner.QuantidadeVazios, direction: ParameterDirection.Input);
                        parametros.Add(name: "TipoDocTransitoId", value: conteiner.TipoDocTransitoId, direction: ParameterDirection.Input);
                        parametros.Add(name: "NumDocTransitoDUE", value: conteiner.NumDocTransitoDUE, direction: ParameterDirection.Input);
                        parametros.Add(name: "DataDocTransitoDUE", value: conteiner.DataDocTransitoDUE, direction: ParameterDirection.Input);
                        parametros.Add(name: "ReeferLigado", value: conteiner.ReeferLigado.ToInt(), direction: ParameterDirection.Input);
                        parametros.Add(name: "ISO", value: conteiner.ISO, direction: ParameterDirection.Input);

                        if (conteiner.ReeferLigado)
                        {
                            parametros.Add(name: "Temperatura", value: conteiner.Temp, direction: ParameterDirection.Input);
                            parametros.Add(name: "Umidade", value: conteiner.Umidade, direction: ParameterDirection.Input);
                            parametros.Add(name: "Ventilacao", value: conteiner.Ventilacao, direction: ParameterDirection.Input);
                            parametros.Add(name: "Escala", value: conteiner.Escala, direction: ParameterDirection.Input);
                        }
                        else
                        {
                            parametros.Add(name: "Temperatura", value: null, direction: ParameterDirection.Input);
                            parametros.Add(name: "Umidade", value: null, direction: ParameterDirection.Input);
                            parametros.Add(name: "Ventilacao", value: null, direction: ParameterDirection.Input);
                            parametros.Add(name: "Escala", value: null, direction: ParameterDirection.Input);
                        }

                        parametros.Add(name: "Id", dbType: DbType.Int32, direction: ParameterDirection.Output);

                        con.Execute(@"
                                INSERT INTO
                                    OPERADOR.TB_AGENDAMENTO_CNTR_ITEM_CNTR 
                                        (
                                            AUTONUM,
                                            AUTONUM_AGENDAMENTO,
                                            AUTONUM_BOOKING,
                                            SIGLA,
                                            TEMPERATURA,
                                            ESCALA,
                                            IMO1,
                                            IMO2,
                                            IMO3,
                                            IMO4,
                                            ONU1,
                                            ONU2,
                                            ONU3,
                                            ONU4,
                                            LACRE1,
                                            LACRE2,
                                            LACRE3,
                                            LACRE4,
                                            LACRE5,
                                            LACRE6,
                                            LACRE7,
                                            OH,
                                            OW,
                                            OWL,
                                            OL,
                                            TAMANHO,
                                            TIPOBASICO,
                                            VOLUMES,
                                            UMIDADE,
                                            VENTILACAO,
                                            LACRE_SIF,
                                            TARA,
                                            BRUTO,
                                            LIQUIDO,
                                            DAT,
                                            EF,
                                            OBS,
                                            Qtde,
                                            AUTONUM_TIPO_DOC_TRANSITO_DUE,
                                            NR_DOC_TRANSITO_DUE,
                                            DATA_DOC_TRANSITO_DUE,
                                            REEFER_LIGADO,
                                            ISO
                                        ) VALUES (
                                            OPERADOR.SEQ_AGENDAMENTO_CNTR_ITEM_CNTR.NEXTVAL,
                                            :AgendamentoId,
                                            :BookingId,
                                            :Sigla,
                                            :Temperatura,
                                            :Escala,
                                            :IMO1,
                                            :IMO2,  
                                            :IMO3,  
                                            :IMO4,  
                                            :ONU1,
                                            :ONU2,
                                            :ONU3,
                                            :ONU4,
                                            :Lacre1,
                                            :Lacre2,
                                            :Lacre3,
                                            :Lacre4,
                                            :Lacre5,
                                            :Lacre6,
                                            :Lacre7,
                                            :OH,
                                            :OW,
                                            :OWL,
                                            :OL,
                                            :Tamanho,
                                            :TipoBasico,
                                            :Volumes,
                                            :Umidade,
                                            :Ventilacao,
                                            :LacreSIF, 
                                            :Tara, 
                                            :Bruto,
                                            :PesoLiquido,
                                            :DAT,
                                            :EF,
                                            :OBS,
                                            :Qtde,
                                            :TipoDocTransitoId,
                                            :NumDocTransitoDUE,
                                            :DataDocTransitoDUE,
                                            :ReeferLigado,
                                            :ISO
                                        ) RETURNING AUTONUM INTO :Id", parametros, transaction);

                        var conteinerId = parametros.Get<int>("Id");

                        foreach (var nota in conteiner.NotasFiscais)
                        {
                            parametros = new DynamicParameters();

                            parametros.Add(name: "ConteinerId", value: conteinerId, direction: ParameterDirection.Input);
                            parametros.Add(name: "Danfe", value: nota.Danfe, direction: ParameterDirection.Input);
                            parametros.Add(name: "CFOP", value: nota.CFOP, direction: ParameterDirection.Input);

                            con.Execute(@"
                                INSERT INTO OPERADOR.TB_AG_CNTR_ITENS_DANFES
                                    ( 
                                        AUTONUM,
                                        AUTONUM_AGENDAMENTO_ITEM_CNTR,
                                        DANFE,                                
                                        CFOP
                                    ) VALUES ( 
                                        OPERADOR.SEQ_AG_CNTR_ITENS_DANFES.NEXTVAL, 
                                        :ConteinerId,
                                        :Danfe,
                                        :CFOP
                                    )", parametros, transaction);
                        }
                    }

                    transaction.Commit();
                }
            }
        }

        public int CadastrarConteiner(Conteiner conteiner)
        {
            using (OracleConnection con = new OracleConnection(AppConfig.StringConexao()))
            {
                var parametros = new DynamicParameters();

                parametros.Add(name: "AgendamentoId", value: conteiner.AgendamentoId, direction: ParameterDirection.Input);
                parametros.Add(name: "BookingId", value: conteiner.Reserva.BookingId, direction: ParameterDirection.Input);
                parametros.Add(name: "Sigla", value: conteiner.Sigla, direction: ParameterDirection.Input);
                parametros.Add(name: "Temperatura", value: conteiner.Temp, direction: ParameterDirection.Input);
                parametros.Add(name: "Escala", value: conteiner.Escala, direction: ParameterDirection.Input);
                parametros.Add(name: "IMO1", value: conteiner.IMO1, direction: ParameterDirection.Input);
                parametros.Add(name: "IMO2", value: conteiner.IMO2, direction: ParameterDirection.Input);
                parametros.Add(name: "IMO3", value: conteiner.IMO3, direction: ParameterDirection.Input);
                parametros.Add(name: "IMO4", value: conteiner.IMO4, direction: ParameterDirection.Input);
                parametros.Add(name: "ONU1", value: conteiner.ONU1, direction: ParameterDirection.Input);
                parametros.Add(name: "ONU2", value: conteiner.ONU2, direction: ParameterDirection.Input);
                parametros.Add(name: "ONU3", value: conteiner.ONU3, direction: ParameterDirection.Input);
                parametros.Add(name: "ONU4", value: conteiner.ONU4, direction: ParameterDirection.Input);
                parametros.Add(name: "Lacre1", value: conteiner.LacreArmador1, direction: ParameterDirection.Input);
                parametros.Add(name: "Lacre2", value: conteiner.LacreArmador2, direction: ParameterDirection.Input);
                parametros.Add(name: "Lacre3", value: conteiner.OutrosLacres1, direction: ParameterDirection.Input);
                parametros.Add(name: "Lacre4", value: conteiner.OutrosLacres2, direction: ParameterDirection.Input);
                parametros.Add(name: "Lacre5", value: conteiner.OutrosLacres3, direction: ParameterDirection.Input);
                parametros.Add(name: "Lacre6", value: conteiner.OutrosLacres4, direction: ParameterDirection.Input);
                parametros.Add(name: "Lacre7", value: conteiner.LacreExportador, direction: ParameterDirection.Input);
                parametros.Add(name: "OH", value: conteiner.Altura, direction: ParameterDirection.Input);
                parametros.Add(name: "OW", value: conteiner.LateralDireita, direction: ParameterDirection.Input);
                parametros.Add(name: "OWL", value: conteiner.LateralEsquerda, direction: ParameterDirection.Input);
                parametros.Add(name: "OL", value: conteiner.Comprimento, direction: ParameterDirection.Input);
                parametros.Add(name: "Tamanho", value: conteiner.Reserva.Tamanho, direction: ParameterDirection.Input);
                parametros.Add(name: "TipoBasico", value: conteiner.Reserva.Tipo, direction: ParameterDirection.Input);
                parametros.Add(name: "Volumes", value: conteiner.Volumes, direction: ParameterDirection.Input);
                parametros.Add(name: "Umidade", value: conteiner.Umidade, direction: ParameterDirection.Input);
                parametros.Add(name: "Ventilacao", value: conteiner.Ventilacao, direction: ParameterDirection.Input);
                parametros.Add(name: "LacreSIF", value: conteiner.LacreSIF, direction: ParameterDirection.Input);
                parametros.Add(name: "Tara", value: conteiner.Tara, direction: ParameterDirection.Input);
                parametros.Add(name: "Bruto", value: conteiner.Bruto, direction: ParameterDirection.Input);
                parametros.Add(name: "PesoLiquido", value: conteiner.PesoLiquido, direction: ParameterDirection.Input);
                parametros.Add(name: "DAT", value: conteiner.DAT, direction: ParameterDirection.Input);
                parametros.Add(name: "EF", value: conteiner.Reserva.EF.ToName(), direction: ParameterDirection.Input);
                parametros.Add(name: "OBS", value: conteiner.Observacoes, direction: ParameterDirection.Input);
                parametros.Add(name: "Qtde", value: conteiner.QuantidadeVazios, direction: ParameterDirection.Input);
                parametros.Add(name: "TipoDocTransitoId", value: conteiner.TipoDocTransitoId, direction: ParameterDirection.Input);
                parametros.Add(name: "NumDocTransitoDUE", value: conteiner.NumDocTransitoDUE, direction: ParameterDirection.Input);
                parametros.Add(name: "DataDocTransitoDUE", value: conteiner.DataDocTransitoDUE, direction: ParameterDirection.Input);
                parametros.Add(name: "ReeferLigado", value: conteiner.ReeferLigado.ToInt(), direction: ParameterDirection.Input);
                parametros.Add(name: "ISO", value: conteiner.ISO, direction: ParameterDirection.Input);
                parametros.Add(name: "ExigeNF", value: conteiner.ExigeNF.ToInt(), direction: ParameterDirection.Input);
                
                parametros.Add(name: "Id", dbType: DbType.Int32, direction: ParameterDirection.Output);

                con.Execute(@"
                        INSERT INTO
                            OPERADOR.TB_AGENDAMENTO_CNTR_ITEM_CNTR 
                                (
                                    AUTONUM,
                                    AUTONUM_AGENDAMENTO,
                                    AUTONUM_BOOKING,
                                    SIGLA,
                                    TEMPERATURA,
                                    ESCALA,
                                    IMO1,
                                    IMO2,
                                    IMO3,
                                    IMO4,
                                    ONU1,
                                    ONU2,
                                    ONU3,
                                    ONU4,
                                    LACRE1,
                                    LACRE2,
                                    LACRE3,
                                    LACRE4,
                                    LACRE5,
                                    LACRE6,
                                    LACRE7,
                                    OH,
                                    OW,
                                    OWL,
                                    OL,
                                    TAMANHO,
                                    TIPOBASICO,
                                    VOLUMES,
                                    UMIDADE,
                                    VENTILACAO,
                                    LACRE_SIF,
                                    TARA,
                                    BRUTO,
                                    LIQUIDO,
                                    DAT,
                                    EF,
                                    OBS,
                                    Qtde,
                                    AUTONUM_TIPO_DOC_TRANSITO_DUE,
                                    NR_DOC_TRANSITO_DUE,
                                    DATA_DOC_TRANSITO_DUE,
                                    REEFER_LIGADO,
                                    ISO,
                                    EXIGENF
                                ) VALUES (
                                    OPERADOR.SEQ_AGENDAMENTO_CNTR_ITEM_CNTR.NEXTVAL,
                                    :AgendamentoId,
                                    :BookingId,
                                    :Sigla,
                                    :Temperatura,
                                    :Escala,
                                    :IMO1,
                                    :IMO2,
                                    :IMO3,
                                    :IMO4,
                                    :ONU1,
                                    :ONU2,
                                    :ONU3,
                                    :ONU4,
                                    :Lacre1,
                                    :Lacre2,
                                    :Lacre3,
                                    :Lacre4,
                                    :Lacre5,
                                    :Lacre6,
                                    :Lacre7,
                                    :OH,
                                    :OW,
                                    :OWL,
                                    :OL,
                                    :Tamanho,
                                    :TipoBasico,
                                    :Volumes,
                                    :Umidade,
                                    :Ventilacao,
                                    :LacreSIF, 
                                    :Tara, 
                                    :Bruto,
                                    :PesoLiquido,
                                    :DAT,
                                    :EF,
                                    :OBS,
                                    :Qtde,
                                    :TipoDocTransitoId,
                                    :NumDocTransitoDUE,
                                    :DataDocTransitoDUE,
                                    :ReeferLigado,
                                    :ISO,
                                    :ExigeNF
                                ) RETURNING AUTONUM INTO :Id", parametros);

                return parametros.Get<int>("Id");
            }
        }

        public int CadastrarConteinerVazio(ConteinerVazio conteiner)
        {
            using (OracleConnection con = new OracleConnection(AppConfig.StringConexao()))
            {
                var parametros = new DynamicParameters();

                parametros.Add(name: "AgendamentoId", value: conteiner.AgendamentoId, direction: ParameterDirection.Input);
                parametros.Add(name: "BookingId", value: conteiner.Reserva.BookingId, direction: ParameterDirection.Input);                
                parametros.Add(name: "Tamanho", value: conteiner.Reserva.Tamanho, direction: ParameterDirection.Input);
                parametros.Add(name: "TipoBasico", value: conteiner.Reserva.Tipo, direction: ParameterDirection.Input);                
                parametros.Add(name: "EF", value: conteiner.Reserva.EF.ToName(), direction: ParameterDirection.Input);                
                parametros.Add(name: "Qtde", value: conteiner.QuantidadeVazios, direction: ParameterDirection.Input);                

                parametros.Add(name: "Id", dbType: DbType.Int32, direction: ParameterDirection.Output);

                con.Execute(@"
                        INSERT INTO
                            OPERADOR.TB_AGENDAMENTO_CNTR_ITEM_CNTR 
                                (
                                    AUTONUM,
                                    AUTONUM_AGENDAMENTO,
                                    AUTONUM_BOOKING,                                    
                                    TAMANHO,
                                    TIPOBASICO,
                                    EF,
                                    Qtde                                    
                                ) VALUES (
                                    OPERADOR.SEQ_AGENDAMENTO_CNTR_ITEM_CNTR.NEXTVAL,
                                    :AgendamentoId,
                                    :BookingId,                                    
                                    :Tamanho,
                                    :TipoBasico,                                    
                                    :EF,                                    
                                    :Qtde
                                ) RETURNING AUTONUM INTO :Id", parametros);

                return parametros.Get<int>("Id");
            }
        }

        public void AtualizarConteinerVazio(ConteinerVazio conteiner)
        {
            using (OracleConnection con = new OracleConnection(AppConfig.StringConexao()))
            {
                var parametros = new DynamicParameters();
                
                parametros.Add(name: "Qtde", value: conteiner.QuantidadeVazios, direction: ParameterDirection.Input);
                parametros.Add(name: "Id", value: conteiner.Id, direction: ParameterDirection.Input);

                con.Execute("UPDATEOPERADOR.TB_AGENDAMENTO_CNTR_ITEM_CNTR SET Qtde = :Qtde WHERE AUTONUM = :Id", parametros);
            }
        }

        public void AtualizarConteiner(Conteiner conteiner)
        {
            using (OracleConnection con = new OracleConnection(AppConfig.StringConexao()))
            {
                var parametros = new DynamicParameters();

                parametros.Add(name: "Id", value: conteiner.Id, direction: ParameterDirection.Input);
                parametros.Add(name: "Sigla", value: conteiner.Sigla, direction: ParameterDirection.Input);
                parametros.Add(name: "Temperatura", value: conteiner.Temp, direction: ParameterDirection.Input);
                parametros.Add(name: "Escala", value: conteiner.Escala, direction: ParameterDirection.Input);
                parametros.Add(name: "IMO1", value: conteiner.IMO1, direction: ParameterDirection.Input);
                parametros.Add(name: "IMO2", value: conteiner.IMO2, direction: ParameterDirection.Input);
                parametros.Add(name: "IMO3", value: conteiner.IMO3, direction: ParameterDirection.Input);
                parametros.Add(name: "IMO4", value: conteiner.IMO4, direction: ParameterDirection.Input);
                parametros.Add(name: "ONU1", value: conteiner.ONU1, direction: ParameterDirection.Input);
                parametros.Add(name: "ONU2", value: conteiner.ONU2, direction: ParameterDirection.Input);
                parametros.Add(name: "ONU3", value: conteiner.ONU3, direction: ParameterDirection.Input);
                parametros.Add(name: "ONU4", value: conteiner.ONU4, direction: ParameterDirection.Input);
                parametros.Add(name: "Lacre1", value: conteiner.LacreArmador1, direction: ParameterDirection.Input);
                parametros.Add(name: "Lacre2", value: conteiner.LacreArmador2, direction: ParameterDirection.Input);
                parametros.Add(name: "Lacre3", value: conteiner.OutrosLacres1, direction: ParameterDirection.Input);
                parametros.Add(name: "Lacre4", value: conteiner.OutrosLacres2, direction: ParameterDirection.Input);
                parametros.Add(name: "Lacre5", value: conteiner.OutrosLacres3, direction: ParameterDirection.Input);
                parametros.Add(name: "Lacre6", value: conteiner.OutrosLacres4, direction: ParameterDirection.Input);
                parametros.Add(name: "Lacre7", value: conteiner.LacreExportador, direction: ParameterDirection.Input);
                parametros.Add(name: "OH", value: conteiner.Altura, direction: ParameterDirection.Input);
                parametros.Add(name: "OW", value: conteiner.LateralDireita, direction: ParameterDirection.Input);
                parametros.Add(name: "OWL", value: conteiner.LateralEsquerda, direction: ParameterDirection.Input);
                parametros.Add(name: "OL", value: conteiner.Comprimento, direction: ParameterDirection.Input);
                parametros.Add(name: "Tamanho", value: conteiner.Reserva.Tamanho, direction: ParameterDirection.Input);
                parametros.Add(name: "TipoBasico", value: conteiner.Reserva.Tipo, direction: ParameterDirection.Input);
                parametros.Add(name: "Volumes", value: conteiner.Volumes, direction: ParameterDirection.Input);
                parametros.Add(name: "Umidade", value: conteiner.Umidade, direction: ParameterDirection.Input);
                parametros.Add(name: "Ventilacao", value: conteiner.Ventilacao, direction: ParameterDirection.Input);
                parametros.Add(name: "LacreSIF", value: conteiner.LacreSIF, direction: ParameterDirection.Input);
                parametros.Add(name: "Tara", value: conteiner.Tara, direction: ParameterDirection.Input);
                parametros.Add(name: "Bruto", value: conteiner.Bruto, direction: ParameterDirection.Input);
                parametros.Add(name: "PesoLiquido", value: conteiner.PesoLiquido, direction: ParameterDirection.Input);
                parametros.Add(name: "DAT", value: conteiner.DAT, direction: ParameterDirection.Input);
                parametros.Add(name: "OBS", value: conteiner.Observacoes, direction: ParameterDirection.Input);
                parametros.Add(name: "TipoDocTransitoId", value: conteiner.TipoDocTransitoId, direction: ParameterDirection.Input);
                parametros.Add(name: "NumDocTransitoDUE", value: conteiner.NumDocTransitoDUE, direction: ParameterDirection.Input);
                parametros.Add(name: "DataDocTransitoDUE", value: conteiner.DataDocTransitoDUE, direction: ParameterDirection.Input);
                parametros.Add(name: "ReeferLigado", value: conteiner.ReeferLigado.ToInt(), direction: ParameterDirection.Input);
                parametros.Add(name: "ISO", value: conteiner.ISO, direction: ParameterDirection.Input);
                parametros.Add(name: "ExigeNF", value: conteiner.ExigeNF.ToInt(), direction: ParameterDirection.Input);

                con.Execute(@"
                    UPDATE
                        OPERADOR.TB_AGENDAMENTO_CNTR_ITEM_CNTR 
                            SET                                
                                SIGLA = :Sigla,
                                TEMPERATURA = :Temperatura,
                                ESCALA = :Escala,
                                IMO1 = :IMO1,
                                IMO2 = :IMO2,
                                IMO3 = :IMO3,
                                IMO4 = :IMO4,
                                ONU1 = :ONU1,
                                ONU2 = :ONU2,
                                ONU3 = :ONU3,
                                ONU4 = :ONU4,
                                LACRE1 = :Lacre1,
                                LACRE2 = :Lacre2,
                                LACRE3 = :Lacre3,
                                LACRE4 = :Lacre4,
                                LACRE5 = :Lacre5,
                                LACRE6 = :Lacre6,
                                LACRE7 = :Lacre7,
                                OH = :OH,
                                OW = :OW,
                                OWL = :OWL,
                                OL = :OL,
                                VOLUMES = :Volumes,
                                UMIDADE = :Umidade,
                                VENTILACAO = :Ventilacao,
                                LACRE_SIF = :LacreSIF,
                                TARA = :Tara,
                                BRUTO = :Bruto,
                                LIQUIDO = :PesoLiquido,
                                DAT = :DAT,
                                OBS = :OBS,
                                AUTONUM_TIPO_DOC_TRANSITO_DUE = :TipoDocTransitoId,
                                NR_DOC_TRANSITO_DUE = :NumDocTransitoDUE,
                                DATA_DOC_TRANSITO_DUE = :DataDocTransitoDUE,
                                REEFER_LIGADO = :ReeferLigado,
                                ISO = :ISO,
                                EXIGENF = :ExigeNF
                            WHERE
                                AUTONUM = :Id", parametros);
            }
        }

        public int CadastrarDanfe(NotaFiscal nf)
        {
            using (OracleConnection con = new OracleConnection(AppConfig.StringConexao()))
            {
                var parametros = new DynamicParameters();

                parametros.Add(name: "ConteinerId", value: nf.ConteinerId, direction: ParameterDirection.Input);
                parametros.Add(name: "Danfe", value: nf.Danfe, direction: ParameterDirection.Input);
                parametros.Add(name: "CFOP", value: nf.CFOP, direction: ParameterDirection.Input);
                parametros.Add(name: "XmlCompleta", value: nf.XmlDanfeCompleta, direction: ParameterDirection.Input);

                parametros.Add(name: "Id", dbType: DbType.Int32, direction: ParameterDirection.Output);

                con.Execute(@"
                    INSERT INTO OPERADOR.TB_AG_CNTR_ITENS_DANFES
                        ( 
                            AUTONUM,
                            AUTONUM_AGENDAMENTO_ITEM_CNTR,
                            DANFE,                                
                            CFOP,
                            ARQUIVO_DANFE
                            
                        ) VALUES ( 
                            OPERADOR.SEQ_AG_CNTR_ITENS_DANFES.NEXTVAL, 
                            :ConteinerId,
                            :Danfe,
                            :CFOP,
                            :XmlCompleta
                        ) RETURNING AUTONUM INTO :Id", parametros);

                return parametros.Get<int>("Id");
            }
        }

        public void AtualizarProtocoloImpresso(int id)
        {
            using (OracleConnection con = new OracleConnection(AppConfig.StringConexao()))
            {
                var parametros = new DynamicParameters();
                parametros.Add(name: "AgendamentoId", value: id, direction: ParameterDirection.Input);

                con.Execute(@"UPDATE OPERADOR.TB_AGENDAMENTO_CNTR SET FLAG_IMPRESSO = 1 WHERE AUTONUM = :AgendamentoId", parametros);
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

                    con.Execute(@"DELETE FROM OPERADOR.TB_AG_CNTR_ITENS_DANFES WHERE AUTONUM_AGENDAMENTO_ITEM_CNTR IN (SELECT AUTONUM FROM TB_AGENDAMENTO_CNTR_ITEM_CNTR WHERE AUTONUM_AGENDAMENTO = :Id)", parametros, transaction);
                    con.Execute(@"DELETE FROM OPERADOR.TB_AGENDAMENTO_CNTR_ITEM_CNTR WHERE AUTONUM_AGENDAMENTO = :Id", parametros, transaction);
                    con.Execute(@"DELETE FROM OPERADOR.TB_AGENDAMENTO_CNTR WHERE AUTONUM = :Id", parametros, transaction);

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
                        A.DT_ENTRADA AS DataEntrada,
                        (
                            SELECT   
                                RTRIM (XMLAGG (XMLELEMENT (E, BOO.REFERENCE || ', ')).EXTRACT ('//text()'), ', ') Reserva
                            FROM
                                OPERADOR.TB_AGENDAMENTO_CNTR_ITEM_CNTR IT
                            INNER JOIN
                                OPERADOR.TB_BOOKING BOO ON IT.AUTONUM_BOOKING = BOO.AUTONUM
                            WHERE
                                IT.AUTONUM_AGENDAMENTO = A.AUTONUM        
                        ) Reserva,
                        (
                            SELECT   
                                RTRIM (XMLAGG (XMLELEMENT (E, IT.SIGLA || ', ')).EXTRACT ('//text()'), ', ') Conteineres
                            FROM
                                OPERADOR.TB_AGENDAMENTO_CNTR_ITEM_CNTR IT
                            WHERE
                                IT.AUTONUM_AGENDAMENTO = A.AUTONUM        
                        ) Conteineres,
                      DECODE(NVL(A.AUTONUM_PERIODO,0), 0, 1, 2) As StatusAgendamento
                    FROM
                        OPERADOR.TB_AGENDAMENTO_CNTR A
                    INNER JOIN
                        OPERADOR.TB_AG_MOTORISTAS B ON A.AUTONUM_MOTORISTA = B.AUTONUM
                    INNER JOIN
                        OPERADOR.TB_AG_VEICULOS C ON A.AUTONUM_VEICULO = C.AUTONUM    
                    INNER JOIN
                        OPERADOR.TB_CAD_TRANSPORTADORAS D ON A.AUTONUM_TRANSPORTADORA = D.AUTONUM        
                    LEFT JOIN
                        OPERADOR.TB_GD_RESERVA E ON A.AUTONUM_PERIODO = E.AUTONUM_GD_RESERVA
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
                        OPERADOR.TB_AGENDAMENTO_CNTR A
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

        public AgendamentoDTO ObterDetalhesAgendamento(int id)
        {
            using (OracleConnection con = new OracleConnection(AppConfig.StringConexao()))
            {
                var parametros = new DynamicParameters();
                parametros.Add(name: "Id", value: id, direction: ParameterDirection.Input);

                return con.Query<AgendamentoDTO>(@"
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
                        A.ANO_PROTOCOLO As AnoProtocolo,
                        A.AUTONUM_USUARIO AS UsuarioId,
                        A.FLAG_IMPRESSO AS Impresso,                        
                        A.FLAG_CEGONHA As Cegonha,
                        A.DATA_HORA As DataCriacao,
                        A.CTE,
                        A.EMAIL_REGISTRO As EmailRegistro
                    FROM
                        OPERADOR.TB_AGENDAMENTO_CNTR A
                    INNER JOIN
                        OPERADOR.TB_AG_MOTORISTAS B ON A.AUTONUM_MOTORISTA = B.AUTONUM
                    INNER JOIN
                        OPERADOR.TB_AG_VEICULOS C ON A.AUTONUM_VEICULO = C.AUTONUM    
                    INNER JOIN
                        OPERADOR.TB_CAD_TRANSPORTADORAS D ON A.AUTONUM_TRANSPORTADORA = D.AUTONUM        
                    LEFT JOIN
                        OPERADOR.TB_GD_RESERVA E ON A.AUTONUM_PERIODO = E.AUTONUM_GD_RESERVA
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
                        A.AUTONUM_USUARIO AS UsuarioId,
                        A.FLAG_IMPRESSO AS Impresso,
                        A.DT_ENTRADA AS DataEntrada,
                        A.FLAG_DUE_DESEMBARACADA As DueDesembaracada
                    FROM
                        OPERADOR.TB_AGENDAMENTO_CNTR A
                    INNER JOIN
                        OPERADOR.TB_AG_MOTORISTAS B ON A.AUTONUM_MOTORISTA = B.AUTONUM
                    INNER JOIN
                        OPERADOR.TB_AG_VEICULOS C ON A.AUTONUM_VEICULO = C.AUTONUM    
                    INNER JOIN
                        OPERADOR.TB_CAD_TRANSPORTADORAS D ON A.AUTONUM_TRANSPORTADORA = D.AUTONUM        
                    LEFT JOIN
                        OPERADOR.TB_GD_RESERVA E ON A.AUTONUM_PERIODO = E.AUTONUM_GD_RESERVA
                    WHERE
                        A.AUTONUM = :Id", parametros).FirstOrDefault();
            }
        }

        public IEnumerable<Reserva> ObterItensAgendamento(int id)
        {
            using (OracleConnection con = new OracleConnection(AppConfig.StringConexao()))
            {
                var parametros = new DynamicParameters();
                parametros.Add(name: "Id", value: id, direction: ParameterDirection.Input);

                return con.Query<Reserva>(@"
                    SELECT
                        AUTONUM_BOOKING As Id,
                        AUTONUM_BOOKING As BookingId,
                        Navio, 
                        AUTONUMVIAGEM As ViagemId, 
                        Viagem, 
                        Tipo, 
                        Tamanho, 
                        Remarks,
                        POD,
                        Qtde,
                        Reserva As Descricao,
                        EF,
                        Saldo,
                        Bagagem,
                        Hash
                    FROM 
                        OPERADOR.VW_AGENDAMENTO_BOOKING
                    WHERE
                        Reserva IN (
                            SELECT 
                                Reference FROM OPERADOR.TB_BOOKING A 
                            INNER JOIN
                                OPERADOR.TB_AGENDAMENTO_CNTR_ITEM_CNTR B ON A.AUTONUM = B.AUTONUM_BOOKING
                            INNER JOIN
                                OPERADOR.TB_AGENDAMENTO_CNTR C ON B.AUTONUM_AGENDAMENTO = C.AUTONUM
                            WHERE 
                                C.AUTONUM = :Id    
                        )
                    AND
                        NVL(EF, 0) > 0
                    ORDER BY 
                        AUTONUMVIAGEM DESC", parametros);
            }
        }

        public IEnumerable<Conteiner> ObterConteineresAgendamento(int id)
        {
            using (OracleConnection con = new OracleConnection(AppConfig.StringConexao()))
            {
                var parametros = new DynamicParameters();
                parametros.Add(name: "Id", value: id, direction: ParameterDirection.Input);

                return con.Query<Conteiner, Reserva, Conteiner>(@"
                    SELECT 
                        Id,
                        AgendamentoId,
                        Sigla,                        
                        Temp,
                        Escala,
                        ISO,
                        IMO1,
                        IMO2,
                        IMO3,
                        IMO4,
                        ONU1,
                        ONU2,
                        ONU3,
                        ONU4,
                        LacreArmador1,
                        LacreArmador2,
                        OutrosLacres1,
                        OutrosLacres2,
                        OutrosLacres3,
                        OutrosLacres4,
                        LacreExportador,
                        ReeferLigado,
                        Altura,
                        LateralDireita,
                        LateralEsquerda,
                        Comprimento,                                    
                        Volumes,
                        QuantidadeVazios,
                        Umidade,
                        Ventilacao,
                        LacreSIF,
                        Tara,
                        Bruto,
                        PesoLiquido,
                        Observacoes,             
                        ExigeNF,
                        TipoDocTransitoId,
                        TipoDocTransitoDescricao,
                        NumDocTransitoDUE,
                        DataDocTransitoDUE,
                        BookingId,
                        Tamanho,
                        Tipo,
                        Descricao,
                        EF,
                        Bagagem                        
                    FROM
                        (
                            SELECT
                                DISTINCT
                                    A.AUTONUM As Id,
                                    A.AUTONUM_AGENDAMENTO AS AgendamentoId,
                                    A.SIGLA,                                    
                                    A.TEMPERATURA As Temp,
                                    A.ESCALA,
                                    A.ISO,
                                    A.IMO1,
                                    A.IMO2,
                                    A.IMO3,
                                    A.IMO4,
                                    A.ONU1,
                                    A.ONU2,
                                    A.ONU3,
                                    A.ONU4,
                                    A.LACRE1 As LacreArmador1,
                                    A.LACRE2 As LacreArmador2,
                                    A.LACRE3 As OutrosLacres1,
                                    A.LACRE4 As OutrosLacres2,
                                    A.LACRE5 As OutrosLacres3,
                                    A.LACRE6 As OutrosLacres4,
                                    A.LACRE7 As LacreExportador,
                                    A.REEFER_LIGADO As ReeferLigado,
                                    A.OH As Altura,
                                    A.OW As LateralDireita,
                                    A.OWL As LateralEsquerda,
                                    A.OL As Comprimento,                                    
                                    A.VOLUMES,
                                    A.QTDE As QuantidadeVazios,
                                    A.UMIDADE,
                                    A.VENTILACAO,
                                    A.LACRE_SIF As LacreSIF,
                                    A.TARA,
                                    A.BRUTO,
                                    A.LIQUIDO As PesoLiquido,
                                    A.OBS As Observacoes, 
                                    A.ExigeNF,
                                    A.AUTONUM_TIPO_DOC_TRANSITO_DUE As TipoDocTransitoId,
                                    D.DESCR As TipoDocTransitoDescricao,
                                    A.NR_DOC_TRANSITO_DUE As NumDocTransitoDUE,
                                    TO_CHAR(A.DATA_DOC_TRANSITO_DUE, 'DD/MM/YYYY') As DataDocTransitoDUE,
                                    B.AUTONUM As BookingId,
                                    A.Tamanho,
                                    A.TipoBasico As Tipo,
                                    B.REFERENCE AS Descricao,                
                                    DECODE(B.EF, 'E', 1, 'F', 2) EF,
                                    NVL(B.FLAG_BAGAGEM, 0) As Bagagem                                    
                            FROM
                                OPERADOR.TB_AGENDAMENTO_CNTR_ITEM_CNTR A
                            INNER JOIN
                                OPERADOR.TB_BOOKING B ON A.AUTONUM_BOOKING = B.AUTONUM
                            INNER JOIN
                                OPERADOR.TB_AGENDAMENTO_CNTR C ON A.AUTONUM_AGENDAMENTO = C.AUTONUM     
                            LEFT JOIN
                                OPERADOR.TIPO_DOC_TRANSITO_DUE D ON A.AUTONUM_TIPO_DOC_TRANSITO_DUE = D.AUTONUM     
                            WHERE
                                C.AUTONUM = :Id
                        )", (conteiner, reserva) =>
                    {

                        conteiner.Reserva = reserva;

                        return conteiner;
                    }, splitOn: "BookingId", param: parametros);
            }
        }

        public Conteiner ObterConteinerAgendamentoPorId(int id)
        {
            using (OracleConnection con = new OracleConnection(AppConfig.StringConexao()))
            {
                var parametros = new DynamicParameters();
                parametros.Add(name: "Id", value: id, direction: ParameterDirection.Input);

                return con.Query<Conteiner, Reserva, Conteiner>(@"
                    SELECT 
                        Id,
                        AgendamentoId,
                        Sigla,                        
                        Temp,
                        Escala,
                        ISO,
                        IMO1,
                        IMO2,
                        IMO3,
                        IMO4,
                        ONU1,
                        ONU2,
                        ONU3,
                        ONU4,
                        LacreArmador1,
                        LacreArmador2,
                        OutrosLacres1,
                        OutrosLacres2,
                        OutrosLacres3,
                        OutrosLacres4,
                        LacreExportador,
                        ReeferLigado,
                        Altura,
                        LateralDireita,
                        LateralEsquerda,
                        Comprimento,                                    
                        Volumes,
                        QuantidadeVazios,
                        Umidade,
                        Ventilacao,
                        LacreSIF,
                        Tara,
                        Bruto,
                        PesoLiquido,
                        Observacoes, 
                        ExigeNF,
                        TipoDocTransitoId,
                        TipoDocTransitoDescricao,
                        NumDocTransitoDUE,
                        DataDocTransitoDUE,
                        BookingId,
                        Tamanho,
                        Tipo,
                        Descricao,
                        EF,
                        Bagagem                        
                    FROM
                        (
                            SELECT
                                DISTINCT
                                    A.AUTONUM As Id,
                                    A.AUTONUM_AGENDAMENTO AS AgendamentoId,
                                    A.SIGLA,                                    
                                    A.TEMPERATURA As Temp,
                                    A.ESCALA,
                                    A.ISO,
                                    A.IMO1,
                                    A.IMO2,
                                    A.IMO3,
                                    A.IMO4,
                                    A.ONU1,
                                    A.ONU2,
                                    A.ONU3,
                                    A.ONU4,
                                    A.LACRE1 As LacreArmador1,
                                    A.LACRE2 As LacreArmador2,
                                    A.LACRE3 As OutrosLacres1,
                                    A.LACRE4 As OutrosLacres2,
                                    A.LACRE5 As OutrosLacres3,
                                    A.LACRE6 As OutrosLacres4,
                                    A.LACRE7 As LacreExportador,
                                    A.REEFER_LIGADO As ReeferLigado,
                                    A.OH As Altura,
                                    A.OW As LateralDireita,
                                    A.OWL As LateralEsquerda,
                                    A.OL As Comprimento,                                    
                                    A.VOLUMES,
                                    A.QTDE As QuantidadeVazios,
                                    A.UMIDADE,
                                    A.VENTILACAO,
                                    A.LACRE_SIF As LacreSIF,
                                    A.TARA,
                                    A.BRUTO,
                                    A.LIQUIDO As PesoLiquido,
                                    A.OBS As Observacoes,   
                                    A.ExigeNF,
                                    A.AUTONUM_TIPO_DOC_TRANSITO_DUE As TipoDocTransitoId,
                                    D.DESCR As TipoDocTransitoDescricao,
                                    A.NR_DOC_TRANSITO_DUE As NumDocTransitoDUE,
                                    TO_CHAR(A.DATA_DOC_TRANSITO_DUE, 'DD/MM/YYYY') As DataDocTransitoDUE,
                                    B.AUTONUM As BookingId,
                                    A.Tamanho,
                                    A.TipoBasico As Tipo,
                                    B.REFERENCE AS Descricao,                
                                    DECODE(B.EF, 'E', 1, 'F', 2) EF,
                                    NVL(B.FLAG_BAGAGEM, 0) As Bagagem                                    
                            FROM
                                OPERADOR.TB_AGENDAMENTO_CNTR_ITEM_CNTR A
                            INNER JOIN
                                OPERADOR.TB_BOOKING B ON A.AUTONUM_BOOKING = B.AUTONUM
                            INNER JOIN
                                OPERADOR.TB_AGENDAMENTO_CNTR C ON A.AUTONUM_AGENDAMENTO = C.AUTONUM     
                            LEFT JOIN
                                OPERADOR.TIPO_DOC_TRANSITO_DUE D ON A.AUTONUM_TIPO_DOC_TRANSITO_DUE = D.AUTONUM     
                            WHERE
                                A.AUTONUM = :Id
                        )", (conteiner, reserva) =>
                {

                    conteiner.Reserva = reserva;

                    return conteiner;
                }, splitOn: "BookingId", param: parametros).FirstOrDefault();
            }
        }

        public Conteiner ObterInformacoesConteinerReserva(string hash)
        {
            using (OracleConnection con = new OracleConnection(AppConfig.StringConexao()))
            {
                var parametros = new DynamicParameters();
                parametros.Add(name: "Hash", value: hash, direction: ParameterDirection.Input);

                return con.Query<Conteiner>(@"
                    SELECT
                        DISTINCT
                            ReeferDesligado,
                            Altura,
                            LateralDireita,
                            LateralEsquerda,
                            Comprimento,
                            Tipo,
                            Tamanho
                    FROM                            
                        OPERADOR.VW_AGENDAMENTO_BOOKING
                    WHERE
                        HASH = :Hash", parametros).FirstOrDefault();
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
                        A.AUTONUM As Id,
                        B.AUTONUM_AGENDAMENTO As AgendamentoId,
                        A.DANFE,    
                        A.CFOP,
                        B.AUTONUM_BOOKING As BookingId
                    FROM 
                        OPERADOR.TB_AG_CNTR_ITENS_DANFES A
                    INNER JOIN
                        OPERADOR.TB_AGENDAMENTO_CNTR_ITEM_CNTR B ON A.AUTONUM_AGENDAMENTO_ITEM_CNTR = B.AUTONUM
                    WHERE 
                        A.DANFE = :Danfe", parametros);
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
                        TB_AG_CNTR_ITENS_DANFES A
                    INNER  JOIN
                        TB_AGENDAMENTO_CNTR_ITENS B ON A.AUTONUM_AGENDAMENTO_ITEM = B.AUTONUM
                    INNER JOIN
                        TB_AGENDAMENTO_CNTR C ON B.AUTONUM_AGENDAMENTO = C.AUTONUM
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
                        DISTINCT   
                            A.AUTONUM As Id,
                            B.AUTONUM_AGENDAMENTO As AgendamentoId,
                            A.DANFE,
                            A.CFOP,
                            B.AUTONUM_BOOKING As BookingId,
                            C.REFERENCE As Reserva,
                            B.Sigla As SiglaConteiner
                    FROM
                        TB_AG_CNTR_ITENS_DANFES A
                    INNER JOIN
                        TB_AGENDAMENTO_CNTR_ITEM_CNTR B ON A.AUTONUM_AGENDAMENTO_ITEM_CNTR = B.AUTONUM
                    INNER JOIN
                        TB_BOOKING C ON B.AUTONUM_BOOKING = C.AUTONUM
                    INNER JOIN
                        TB_AGENDAMENTO_CNTR D ON B.AUTONUM_AGENDAMENTO = D.AUTONUM
                    WHERE
                        D.AUTONUM = :AgendamentoId", parametros);
            }
        }

        public IEnumerable<Janela> ObterPeriodos(DateTime inicio, DateTime fim, TipoAgendamentoConteiner ef)
        {
            using (OracleConnection con = new OracleConnection(AppConfig.StringConexao()))
            {
                var parametros = new DynamicParameters();

                parametros.Add(name: "Inicio", value: inicio, direction: ParameterDirection.Input);
                parametros.Add(name: "Fim", value: fim, direction: ParameterDirection.Input);
                parametros.Add(name: "ServicoGate", value: ef == TipoAgendamentoConteiner.Cheio ? "E" : "V", direction: ParameterDirection.Input);

                return con.Query<Janela>(@"
                    SELECT
                        AUTONUM_GD_RESERVA As Id,
                        PERIODO_INICIAL As PeriodoInicial,
                        PERIODO_FINAL As PeriodoFinal,
                        LIMITE_MOVIMENTOS - (SELECT COUNT(1) FROM OPERADOR.TB_AGENDAMENTO_CNTR WHERE AUTONUM_PERIODO = AUTONUM_GD_RESERVA) AS Saldo
                    FROM
                        OPERADOR.TB_GD_RESERVA
                    WHERE
                        SERVICO_GATE = :ServicoGate
                    AND
                        PERIODO_INICIAL > SYSDATE
                    AND
                        PERIODO_INICIAL >= :Inicio AND PERIODO_FINAL <= :Fim
                    AND
                        (LIMITE_MOVIMENTOS - 
                            (SELECT TO_NUMBER(COUNT(1)) FROM OPERADOR.TB_AGENDAMENTO_CNTR 
                                WHERE AUTONUM_PERIODO = AUTONUM_GD_RESERVA)) > 0
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
                        LIMITE_MOVIMENTOS - (SELECT COUNT(1) FROM TB_AGENDAMENTO_CNTR WHERE AUTONUM_PERIODO = AUTONUM_GD_RESERVA) AS Saldo
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
                            CASE NVL(B.DT_OP_INICIO, SYSDATE) WHEN SYSDATE THEN C.DATA_OP_INICIO ELSE B.DT_OP_INICIO END Abertura,
                            CASE NVL(B.DT_OP_FIM, SYSDATE) WHEN SYSDATE THEN C.DATA_OP_FIM ELSE B.DT_OP_FIM END Fechamento,
                            0 As LimiteHoraLateArrival,
                            DECODE(A.EF, 'E', 1, 'F', 2) EF
                    FROM
                        OPERADOR.TB_AGENDAMENTO_CNTR_ITEM_CNTR A
                    INNER JOIN
                        OPERADOR.TB_BOOKING B ON A.AUTONUM_BOOKING = B.AUTONUM
                    INNER JOIN
                        OPERADOR.TB_VIAGENS C ON B.AUTONUMVIAGEM = C.AUTONUM 
                    INNER JOIN
                        OPERADOR.TB_AGENDAMENTO_CNTR D ON A.AUTONUM_AGENDAMENTO = D.AUTONUM     
                    WHERE
                        D.AUTONUM = :Id", parametros);
            }
        }

        public IEnumerable<Reserva> ObterPeriodosReservas(string[] reservas)
        {
            using (OracleConnection con = new OracleConnection(AppConfig.StringConexao()))
            {
                var parametros = new DynamicParameters();
                parametros.Add(name: "Reservas", value: reservas, direction: ParameterDirection.Input);

                return con.Query<Reserva>(@"
                    SELECT 
                        A.REFERENCE AS DESCRICAO, 
                        DECODE (A.DT_OP_INICIO, NULL, B.DATA_OP_INICIO, A.DT_OP_INICIO) AS ABERTURA,
                        DECODE (A.DT_OP_FIM, NULL, B.DATA_DEAD_LINE, A.DT_OP_FIM) AS FECHAMENTO,    
                        0 AS LIMITEHORALATEARRIVAL,
                        DECODE(A.EF, 'E', 1, 'F', 2) EF
                    FROM 
                        OPERADOR.TB_BOOKING A
                    INNER JOIN    
                        OPERADOR.TB_VIAGENS B ON A.AUTONUMVIAGEM = B.AUTONUM
                    WHERE 
                        A.REFERENCE IN :Reservas AND B.OPERANDO = 1
                    ORDER BY 
                        B.AUTONUM DESC", parametros);
            }
        }

        public IEnumerable<DocumentoTransito> ObterTiposDocumentoTransito()
        {
            using (OracleConnection con = new OracleConnection(AppConfig.StringConexao()))
            {
                return con.Query<DocumentoTransito>("SELECT AUTONUM As Id, DESCR As Descricao FROM OPERADOR.TIPO_DOC_TRANSITO_DUE");
            }
        }

        public IEnumerable<TipoConteiner> ObterTiposConteiner()
        {
            using (OracleConnection con = new OracleConnection(AppConfig.StringConexao()))
            {
                return con.Query<TipoConteiner>("SELECT CODE As Id, CODIGO As Descricao FROM SGIPA.DTE_TB_TIPOS_CONTEINER ORDER BY CODIGO");
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
                        A.AUTONUM_PERIODO AS PeriodoId,
                        TO_CHAR(E.PERIODO_INICIAL, 'DD/MM/YYYY HH24:MI') || ' - ' || TO_CHAR(E.PERIODO_FINAL, 'HH24:MI') PeriodoDescricao,
                        A.Protocolo,
                        A.ANO_PROTOCOLO As AnoProtocolo,
                        A.AUTONUM_USUARIO AS UsuarioId,
                        A.FLAG_IMPRESSO AS Impresso,    
                        A.FLAG_CEGONHA As Cegonha,
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
                                    TB_BOOKING B 
                                INNER JOIN 
                                    TB_VIAGENS V ON B.AUTONUMVIAGEM = V.AUTONUM
                                INNER JOIN 
                                    TB_CAD_NAVIOS N ON V.NAVIO = N.AUTONUM
                                WHERE
                                    B.AUTONUM IN (SELECT AUTONUM_BOOKING FROM OPERADOR.TB_AGENDAMENTO_CNTR_ITEM_CNTR WHERE AUTONUM_AGENDAMENTO = :Id)
                            )        
                        ) As Navio
                    FROM
                        OPERADOR.TB_AGENDAMENTO_CNTR A
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

        public Conteiner ObterConteinerPorId(int id)
        {
            using (OracleConnection con = new OracleConnection(AppConfig.StringConexao()))
            {
                var parametros = new DynamicParameters();
                parametros.Add(name: "Id", value: id, direction: ParameterDirection.Input);

                return con.Query<Conteiner, Reserva, Conteiner>(@"
                    SELECT 
                        A.AUTONUM As Id, 
                        A.AUTONUM_AGENDAMENTO As AgendamentoId, 
                        A.Sigla, 
                        A.AUTONUM_BOOKING As BookingId,
                        B.REFERENCE As Descricao
                    FROM 
                        OPERADOR.TB_AGENDAMENTO_CNTR_ITEM_CNTR A
                    INNER JOIN
                        OPERADOR.TB_BOOKING B ON A.AUTONUM_BOOKING = B.AUTONUM
                    WHERE 
                        A.AUTONUM = :Id", (conteiner, reserva) =>
                    {

                        conteiner.Reserva = reserva;

                        return conteiner;
                    }, splitOn: "BookingId", param: parametros).FirstOrDefault();
            }
        }

        public void ExcluirConteiner(int id)
        {
            using (OracleConnection con = new OracleConnection(AppConfig.StringConexao()))
            {
                con.Open();

                using (var transaction = con.BeginTransaction())
                {
                    var parametros = new DynamicParameters();
                    parametros.Add(name: "Id", value: id, direction: ParameterDirection.Input);

                    con.Execute(@"DELETE FROM OPERADOR.TB_AG_CNTR_ITENS_DANFES WHERE AUTONUM_AGENDAMENTO_ITEM_CNTR = :Id", parametros, transaction);
                    con.Execute(@"DELETE FROM OPERADOR.TB_AGENDAMENTO_CNTR_ITEM_CNTR WHERE AUTONUM = :Id", parametros, transaction);

                    transaction.Commit();
                }
            }
        }

        public NotaFiscal ObterDanfePorId(int id)
        {
            using (OracleConnection con = new OracleConnection(AppConfig.StringConexao()))
            {
                var parametros = new DynamicParameters();
                parametros.Add(name: "Id", value: id, direction: ParameterDirection.Input);

                return con.Query<NotaFiscal>(@"
                    SELECT 
                        A.AUTONUM As Id, 
                        C.AUTONUM As AgendamentoId, 
                        A.DANFE 
                    FROM 
                        OPERADOR.TB_AG_CNTR_ITENS_DANFES A 
                    INNER JOIN 
                        TB_AGENDAMENTO_CNTR_ITEM_CNTR B ON A.AUTONUM_AGENDAMENTO_ITEM_CNTR = B.AUTONUM
                    INNER JOIN 
                        OPERADOR.TB_AGENDAMENTO_CNTR C ON B.AUTONUM_AGENDAMENTO = C.AUTONUM
                    WHERE
                        A.AUTONUM = :Id", parametros).FirstOrDefault();
            }
        }

        public void ExcluirDanfe(int id)
        {
            using (OracleConnection con = new OracleConnection(AppConfig.StringConexao()))
            {
                var parametros = new DynamicParameters();
                parametros.Add(name: "Id", value: id, direction: ParameterDirection.Input);

                con.Execute(@"DELETE FROM OPERADOR.TB_AG_CNTR_ITENS_DANFES WHERE AUTONUM = :Id", parametros);
            }
        }

        public void AtualizarMotoristaAgendamento(int agendamentoId, int motoristaId)
        {
            using (OracleConnection con = new OracleConnection(AppConfig.StringConexao()))
            {
                var parametros = new DynamicParameters();

                parametros.Add(name: "AgendamentoId", value: agendamentoId, direction: ParameterDirection.Input);
                parametros.Add(name: "MotoristaId", value: motoristaId, direction: ParameterDirection.Input);

                con.Execute("UPDATE OPERADOR.TB_AGENDAMENTO_CNTR SET AUTONUM_MOTORISTA = :MotoristaId WHERE AUTONUM = :AgendamentoId", parametros);
            }
        }

        public void AtualizarVeiculoAgendamento(int agendamentoId, int veiculoId)
        {
            using (OracleConnection con = new OracleConnection(AppConfig.StringConexao()))
            {
                var parametros = new DynamicParameters();

                parametros.Add(name: "AgendamentoId", value: agendamentoId, direction: ParameterDirection.Input);
                parametros.Add(name: "VeiculoId", value: veiculoId, direction: ParameterDirection.Input);

                con.Execute("UPDATE OPERADOR.TB_AGENDAMENTO_CNTR SET AUTONUM_VEICULO = :VeiculoId WHERE AUTONUM = :AgendamentoId", parametros);
            }
        }

        public Reserva ObterConteinerPorISO(string iso)
        {
            using (OracleConnection con = new OracleConnection(AppConfig.StringConexao()))
            {
                var parametros = new DynamicParameters();
                parametros.Add(name: "ISO", value: iso, direction: ParameterDirection.Input);

                return con.Query<Reserva>("SELECT Tamanho, Tipo FROM OPERADOR.TB_CAD_CONTEINERS WHERE ISO = :ISO", parametros).FirstOrDefault();
            }
        }

        public bool ExisteConteinerLateArrival(string sigla, string reserva, int viagem)
        {
            using (OracleConnection con = new OracleConnection(AppConfig.StringConexao()))
            {
                var parametros = new DynamicParameters();

                parametros.Add(name: "Sigla", value: sigla, direction: ParameterDirection.Input);
                parametros.Add(name: "Reserva", value: reserva, direction: ParameterDirection.Input);
                parametros.Add(name: "Viagem", value: viagem, direction: ParameterDirection.Input);

                return con.Query<bool>("SELECT AUTONUM FROM OPERADOR.TB_LATE_ARRIVAL WHERE ID_CONTEINER = :Sigla AND REFERENCE = :Reserva AND AUTONUMVIAGEM = :Viagem", parametros).Any();
            }
        }

        public bool ConteinerExigeNF(string reserva, int viagem)
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

        public void AtualizarFlagDueDesembaracada(int agendamentoId, bool check)
        {
            using (OracleConnection con = new OracleConnection(AppConfig.StringConexao()))
            {
                var parametros = new DynamicParameters();

                parametros.Add(name: "AgendamentoId", value: agendamentoId, direction: ParameterDirection.Input);
                parametros.Add(name: "CheckDue", value: check.ToInt(), direction: ParameterDirection.Input);

                con.Execute("UPDATE OPERADOR.TB_AGENDAMENTO_CNTR SET FLAG_DUE_DESEMBARACADA = :CheckDue WHERE AUTONUM = :AgendamentoId", parametros);
            }
        }
    }
}