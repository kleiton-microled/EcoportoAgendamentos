using Dapper;
using Ecoporto.AgendamentoConteiner.Config;
using Ecoporto.AgendamentoConteiner.Dados.Interfaces;
using Ecoporto.AgendamentoConteiner.Models;
using Oracle.ManagedDataAccess.Client;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Ecoporto.AgendamentoConteiner.Dados.Repositorios
{
    public class ReservaRepositorio : IReservaRepositorio
    {
        public Reserva ObterDetalhesReserva(string reserva)
        {
            using (OracleConnection con = new OracleConnection(AppConfig.StringConexao()))
            {
                var parametros = new DynamicParameters();
                parametros.Add(name: "Reserva", value: reserva, direction: ParameterDirection.Input);

                return con.Query<Reserva>(@"SELECT NAVIO, AUTONUMVIAGEM As ViagemId, VIAGEM, Abertura, Fechamento, AUTONUM_BOOKING As BookingId, Exportador, EF, LateArrival, DeltaHoras FROM OPERADOR.VW_AGENDAMENTO_BOOKING WHERE RESERVA = :Reserva ORDER BY AUTONUMVIAGEM DESC", parametros).FirstOrDefault();
            }
        }

        public IEnumerable<Reserva> ObterItensReserva(string reserva, int viagemId)
        {
            using (OracleConnection con = new OracleConnection(AppConfig.StringConexao()))
            {
                var parametros = new DynamicParameters();

                parametros.Add(name: "Reserva", value: reserva, direction: ParameterDirection.Input);
                parametros.Add(name: "ViagemId", value: viagemId, direction: ParameterDirection.Input);

                return con.Query<Reserva>(@"
                    SELECT                         
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
                        RESERVA = :Reserva 
                    AND
                        AUTONUMVIAGEM = :ViagemId
                    AND 
                        NVL(EF, 0) > 0
                    ORDER BY 
                        AUTONUMVIAGEM DESC", parametros);
            }
        }

        public Reserva ObterItemReservaPorBokingId(int bookingId)
        {
            using (OracleConnection con = new OracleConnection(AppConfig.StringConexao()))
            {
                var parametros = new DynamicParameters();
                parametros.Add(name: "BookingId", value: bookingId, direction: ParameterDirection.Input);

                return con.Query<Reserva>(@"
                    SELECT                         
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
                        Hash
                    FROM 
                        OPERADOR.VW_AGENDAMENTO_BOOKING 
                    WHERE 
                        AUTONUM_BOOKING = :BookingId", parametros).FirstOrDefault();
            }
        }

        public Reserva ObterItemReservaPorId(int bookingId, int tamanho)
        {
            using (OracleConnection con = new OracleConnection(AppConfig.StringConexao()))
            {
                var parametros = new DynamicParameters();
                parametros.Add(name: "BookingId", value: bookingId, direction: ParameterDirection.Input);
                
                string filtro = tamanho == 20 ? " AND Tamanho = 20 " : " AND Tamanho = 40 ";

                return con.Query<Reserva>($@"
                    SELECT 
                        AUTONUM_BOOKING As BookingId,
                        Navio, 
                        AUTONUMVIAGEM As ViagemId, 
                        Viagem, 
                        Tipo, 
                        Tamanho, 
                        Remarks,
                        IMO1, 
                        IMO2,
                        IMO3,
                        IMO4,
                        ONU1,
                        ONU2,
                        ONU3,
                        ONU4,
                        POD,
                        ShipperOwner,
                        Reserva As Descricao,
                        EF,
                        Saldo,
                        ReeferDesligado,
                        Bagagem,
                        LateArrival,
                        DeltaHoras,
                        Altura,
                        LateralDireita,
                        LateralEsquerda,
                        Comprimento,
                        Temp, 
                        Escala, 
                        CASE WHEN Ventilacao = '0' THEN 'CLOSED' ELSE Ventilacao END Ventilacao,
                        CASE WHEN Umidade = '0' THEN 'OFF' ELSE Umidade END Umidade
                    FROM 
                        OPERADOR.VW_AGENDAMENTO_BOOKING 
                    WHERE 
                        AUTONUM_BOOKING = :BookingId {filtro}", parametros).FirstOrDefault();
            }
        }
        public Reserva ObterDetalhesItem(string reserva, int bookingCsId)
        {
            using (OracleConnection con = new OracleConnection(AppConfig.StringConexao()))
            {
                var parametros = new DynamicParameters();
                parametros.Add(name: "Reserva", value: reserva, direction: ParameterDirection.Input);
                parametros.Add(name: "BookingCsId", value: bookingCsId, direction: ParameterDirection.Input);

                return con.Query<Reserva>(@"
                    SELECT 
                        NVL(A.FLAG_VEICULO, 0) As Veiculo,
                        NVL(FLAG_UPLOAD_PACKLIST, 0) As PackingList,
                        NVL(FLAG_UPLOAD_IMAGEM_CARGA, 0) As ImagemCarga,
                        NVL(FLAG_UPLOAD_D_TECNICO, 0) As DesenhoTecnico
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

        public Reserva AberturaPraca(int bookingId)
        {
            using (OracleConnection con = new OracleConnection(AppConfig.StringConexao()))
            {
                var parametros = new DynamicParameters();
                parametros.Add(name: "BookingId", value: bookingId, direction: ParameterDirection.Input);

                return con.Query<Reserva>(@"SELECT Abertura, Fechamento FROM VW_AGENDAMENTO_BOOKING WHERE AUTONUM_BOOKING = :BookingId ORDER BY AUTONUMVIAGEM DESC", parametros).FirstOrDefault();
            }
        }

        public IEnumerable<string> ObterIMOs(string reserva)
        {
            using (OracleConnection con = new OracleConnection(AppConfig.StringConexao()))
            {
                var parametros = new DynamicParameters();
                parametros.Add(name: "Reserva", value: reserva, direction: ParameterDirection.Input);

                return con.Query<string>(@"
                    SELECT 
                        DISTINCT IMO 
                    FROM 
                        (
                            SELECT IMO1 As IMO FROM OPERADOR.TB_BOOKING WHERE REFERENCE = :Reserva
                        UNION ALL
                            SELECT IMO2 As IMO FROM OPERADOR.TB_BOOKING WHERE REFERENCE = :Reserva
                        UNION ALL
                            SELECT IMO3 As IMO FROM OPERADOR.TB_BOOKING WHERE REFERENCE = :Reserva
                        UNION ALL
                            SELECT IMO4 As IMO FROM OPERADOR.TB_BOOKING WHERE REFERENCE = :Reserva
                        ) WHERE IMO IS NOT NULL ORDER BY IMO", parametros);
            }
        }

        public IEnumerable<string> ObterONUs(string reserva)
        {
            using (OracleConnection con = new OracleConnection(AppConfig.StringConexao()))
            {
                var parametros = new DynamicParameters();
                parametros.Add(name: "Reserva", value: reserva, direction: ParameterDirection.Input);

                return con.Query<string>(@"
                    SELECT 
                        DISTINCT ONU 
                    FROM 
                        (
                            SELECT UN1 As ONU FROM OPERADOR.TB_BOOKING WHERE REFERENCE = :Reserva
                        UNION ALL
                            SELECT UN2 As ONU FROM OPERADOR.TB_BOOKING WHERE REFERENCE = :Reserva
                        UNION ALL
                            SELECT UN3 As ONU FROM OPERADOR.TB_BOOKING WHERE REFERENCE = :Reserva
                        UNION ALL
                            SELECT UN4 As ONU FROM OPERADOR.TB_BOOKING WHERE REFERENCE = :Reserva
                        ) WHERE ONU IS NOT NULL ORDER BY ONU", parametros);
            }
        }
    }
}