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
    public class ReservaRepositorio : IReservaRepositorio
    {
        public Reserva ObterDetalhesReserva(string reserva)
        {
            using (OracleConnection con = new OracleConnection(AppConfig.StringConexao()))
            {
                var parametros = new DynamicParameters();
                parametros.Add(name: "Reserva", value: reserva, direction: ParameterDirection.Input);

                return con.Query<Reserva>(@"SELECT NAVIO, AUTONUMVIAGEM As ViagemId, VIAGEM, DT_ABERTURA_PRACA As Abertura, DT_FECHAMENTO_PRACA AS Fechamento, FLAG_DESEMBARACO, AUTONUM_CS_BOOKING As BookingCsId, Exportador FROM OPERADOR.VW_WEB_CAB_RESERVA WHERE RESERVA = :Reserva ORDER BY AUTONUMVIAGEM DESC", parametros).FirstOrDefault();
            }
        }

        public IEnumerable<ReservaItem> ObterItensReserva(string reserva, int viagemId)
        {
            using (OracleConnection con = new OracleConnection(AppConfig.StringConexao()))
            {
                var parametros = new DynamicParameters();

                parametros.Add(name: "Reserva", value: reserva, direction: ParameterDirection.Input);
                parametros.Add(name: "ViagemId", value: viagemId, direction: ParameterDirection.Input);

                return con.Query<ReservaItem>(@"
                    SELECT 
                        ROW_NUMBER() OVER (ORDER BY A.AUTONUM_CS_BOOKING_ITEM) AS Item,
                        A.AUTONUM_CS_BOOKING_ITEM As BookingCsItemId,
                        A.AUTONUM_CS_BOOKING As BookingCsId, 
                        A.QTDE_RESERVA As QtdeReserva, 
                        A.EMBALAGEM, 
                        A.PESO_UNIT As PesoUnitario, 
                        A.MARCA, 
                        A.CONTRAMARCA,
                        TO_CHAR(A.QTDE_RESERVA) || ' ' || A.EMBALAGEM || ' ' || A.MARCA || ' ' ||  A.CONTRAMARCA || ' ' || A.PESO_UNIT || 'Kg' || ' Saldo: ' || A.SALDO As Descricao,
                        A.FLAG_VEICULO As Veiculo,
                        A.SALDO,
                        A.AUTONUM_CLASSIFICACAO As ClassificacaoId
                    FROM 
                        VW_WEB_ITENS_RESERVA A
                    INNER JOIN
                        VW_WEB_CAB_RESERVA B ON A.AUTONUM_CS_BOOKING = B.AUTONUM_CS_BOOKING
                    WHERE 
                        B.RESERVA = :Reserva 
                    AND 
                        B.AUTONUMVIAGEM = :ViagemId
                    AND
                        A.Saldo > 0", parametros);
            }
        }

        public int ObterSaldoTotalReserva(string reserva, int viagemId)
        {
            using (OracleConnection con = new OracleConnection(AppConfig.StringConexao()))
            {
                var parametros = new DynamicParameters();

                parametros.Add(name: "Reserva", value: reserva, direction: ParameterDirection.Input);
                parametros.Add(name: "ViagemId", value: viagemId, direction: ParameterDirection.Input);
         
                return con.Query<int>(@"
                    SELECT                         
                        NVL(MAX(A.Saldo),0) As Saldo
                    FROM 
                        VW_WEB_ITENS_RESERVA A
                    INNER JOIN
                        VW_WEB_CAB_RESERVA B ON A.AUTONUM_CS_BOOKING = B.AUTONUM_CS_BOOKING
                    WHERE 
                        B.RESERVA = :Reserva 
                    AND 
                        B.AUTONUMVIAGEM = :ViagemId
                    AND
                        A.SALDO > 0", parametros).Single();
            }
        }

        public ReservaItem ObterItemReservaPorId(int bookingCsItemId)
        {
            using (OracleConnection con = new OracleConnection(AppConfig.StringConexao()))
            {
                var parametros = new DynamicParameters();
                parametros.Add(name: "BookingCsItemId", value: bookingCsItemId, direction: ParameterDirection.Input);

                return con.Query<ReservaItem>(@"
                    SELECT 
                        ROW_NUMBER() OVER (ORDER BY A.AUTONUM_CS_BOOKING_ITEM) AS Item,
                        A.AUTONUM_CS_BOOKING_ITEM As BookingCsItemId,
                        A.AUTONUM_CS_BOOKING As BookingCsId, 
                        A.QTDE_RESERVA As QtdeReserva, 
                        A.EMBALAGEM, 
                        B.RESERVA,
                        B.AUTONUMVIAGEM As ViagemId,
                        A.PESO_UNIT As PesoUnitario, 
                        A.MARCA, 
                        A.CONTRAMARCA,
                        TO_CHAR(A.QTDE_RESERVA) || ' ' || A.EMBALAGEM || ' ' || A.MARCA || ' ' ||  A.CONTRAMARCA || ' ' || A.PESO_UNIT || 'Kg' || ' Saldo: ' || A.Saldo As Descricao,
                        A.FLAG_VEICULO As Veiculo,
                        NVL(A.FLAG_UPLOAD_PACKLIST, 0) As PackingList,
                        NVL(A.FLAG_UPLOAD_IMAGEM_CARGA, 0) As ImagemCarga,
                        NVL(A.FLAG_UPLOAD_D_TECNICO, 0) As DesenhoTecnico,
                        A.Saldo,
                        A.CLASSIFICACAO_DESCRICAO As Classificacao,
                        A.AUTONUM_CLASSIFICACAO As ClassificacaoId
                    FROM 
                        VW_WEB_ITENS_RESERVA A
                    INNER JOIN
                        VW_WEB_CAB_RESERVA B ON A.AUTONUM_CS_BOOKING = B.AUTONUM_CS_BOOKING
                    WHERE 
                        A.AUTONUM_CS_BOOKING_ITEM = :BookingCsItemId", parametros).FirstOrDefault();
            }
        }        
        public ReservaItem ObterDetalhesItem(string reserva, int bookingCsId)
        {
            using (OracleConnection con = new OracleConnection(AppConfig.StringConexao()))
            {
                var parametros = new DynamicParameters();
                parametros.Add(name: "Reserva", value: reserva, direction: ParameterDirection.Input);
                parametros.Add(name: "BookingCsId", value: bookingCsId, direction: ParameterDirection.Input);

                return con.Query<ReservaItem>(@"
                    SELECT 
                        NVL(A.FLAG_VEICULO, 0) As Veiculo,
                        NVL(FLAG_UPLOAD_PACKLIST, 0) As PackingList,
                        NVL(FLAG_UPLOAD_IMAGEM_CARGA, 0) As ImagemCarga,
                        NVL(FLAG_UPLOAD_D_TECNICO, 0) As DesenhoTecnico,
                        A.AUTONUM_CLASSIFICACAO As ClassificacaoId,
                        A.TRANSP_CHEGADA As TipoCarga
                    FROM 
                        VW_WEB_ITENS_RESERVA A 
                    INNER JOIN 
                        VW_WEB_CAB_RESERVA B ON A.AUTONUM_CS_BOOKING = B.AUTONUM_CS_BOOKING 
                    WHERE 
                        B.RESERVA = :Reserva 
                    AND 
                        A.AUTONUM_CS_BOOKING_ITEM = :BookingCsId", parametros).FirstOrDefault();
            }
        }

        public Reserva AberturaPraca(string reserva)
        {
            using (OracleConnection con = new OracleConnection(AppConfig.StringConexao()))
            {
                var parametros = new DynamicParameters();
                parametros.Add(name: "Reserva", value: reserva, direction: ParameterDirection.Input);
                
                return con.Query<Reserva>(@"SELECT DT_ABERTURA_PRACA As Abertura, DT_FECHAMENTO_PRACA AS Fechamento FROM VW_WEB_CAB_RESERVA WHERE RESERVA = :Reserva ORDER BY AUTONUMVIAGEM DESC", parametros).FirstOrDefault();
            }
        }

        public bool CargaExigeNF(int bookingCsItemId)
        {
            using (OracleConnection con = new OracleConnection(AppConfig.StringConexao()))
            {
                var parametros = new DynamicParameters();
                parametros.Add(name: "BookingCsItemId", value: bookingCsItemId, direction: ParameterDirection.Input);

                var resultado = con.Query<int>("SELECT NVL(MAX(A.MOTIVO_DESPACHO_SEM_NF), 0) MOTIVO_DESPACHO_SEM_NF FROM OPERADOR.TB_CS_BOOKING A INNER JOIN OPERADOR.TB_CS_BOOKING_ITEM B ON A.AUTONUM = B.AUTONUM_CS_BOOKING WHERE B.AUTONUM = :BookingCsItemId", parametros).Single();

                return resultado == 0;
            }
        }

        public bool CargaExigeChassi(int bookingCsItemId)
        {
            using (OracleConnection con = new OracleConnection(AppConfig.StringConexao()))
            {
                var parametros = new DynamicParameters();
                parametros.Add(name: "BookingCsItemId", value: bookingCsItemId, direction: ParameterDirection.Input);

                var resultado = con.Query<string>("SELECT TRANSP_CHEGADA FROM OPERADOR.TB_CS_BOOKING_ITEM WHERE AUTONUM = :BookingCsItemId", parametros).FirstOrDefault();

                if (string.IsNullOrWhiteSpace(resultado))
                    return false;

                return resultado != "BS";
            }
        }
    }
}