Imports System.Data.OleDb
Imports System.Data.SqlClient
Public Class Exportacao

    Public Function ClienteBloqueado(ByVal Reserva As String) As Boolean

        Dim Rst As New ADODB.Recordset
        Dim SQL As New StringBuilder

        If Banco.BancoEmUso = "ORACLE" Then
            SQL.Append("SELECT ")
            SQL.Append("    FLAG_BLOQUEADO ")
            SQL.Append("FROM ")
            SQL.Append("    OPERADOR.VW_PRACA_ABERTA ")
            SQL.Append("WHERE ")
            SQL.Append("    RESERVA='{0}' ")
        Else
            SQL.Append("SELECT ")
            SQL.Append("    FLAG_BLOQUEADO ")
            SQL.Append("FROM ")
            SQL.Append("    OPERADOR.DBO.VW_PRACA_ABERTA ")
            SQL.Append("WHERE ")
            SQL.Append("    RESERVA='{0}' ")
        End If

        Rst.Open(String.Format(SQL.ToString(), Reserva), Banco.Conexao, 3, 3)

        If Not Rst.EOF Then
            If Rst.Fields("FLAG_BLOQUEADO").Value.ToString() = "1" Then
                Return True
            End If
        End If

        Return False

    End Function

    Public Function NavioIndisponivelParaAgendamento(ByVal Reserva As String) As Boolean

        Dim Rst As New ADODB.Recordset
        Dim SQL As New StringBuilder

        If Banco.BancoEmUso = "ORACLE" Then
            SQL.Append("SELECT ")
            SQL.Append("    RESERVA ")
            SQL.Append("FROM ")
            SQL.Append("    OPERADOR.VW_PRACA_ABERTA ")
            SQL.Append("WHERE ")
            SQL.Append("    (DT_INICIO_PRACA IS NULL OR DT_FIM_PRACA <= SYSDATE) ")
            SQL.Append("AND ")
            SQL.Append("    NVL(FLAG_LATE,0) = 0 ")
            SQL.Append("AND ")
            SQL.Append("    RESERVA='{0}' ")
        Else
            SQL.Append("SELECT ")
            SQL.Append("    RESERVA ")
            SQL.Append("FROM ")
            SQL.Append("    OPERADOR.DBO.VW_PRACA_ABERTA ")
            SQL.Append("WHERE ")
            SQL.Append("    (DT_INICIO_PRACA IS NULL OR DT_FIM_PRACA <= GETDATE()) ")
            SQL.Append("AND ")
            SQL.Append("    ISNULL(FLAG_LATE,0) = 0 ")
            SQL.Append("AND ")
            SQL.Append("    RESERVA='{0}' ")
        End If

        Rst.Open(String.Format(SQL.ToString(), Reserva), Banco.Conexao, 3, 3)

        If Not Rst.EOF Then
            Return True
        End If

        Return False

    End Function

    Public Function ReservaInexistente(ByVal Reserva As String) As Boolean

        Dim Rst As New ADODB.Recordset
        Dim SQL As New StringBuilder

        If Banco.BancoEmUso = "ORACLE" Then
            SQL.Append("SELECT ")
            SQL.Append("    RESERVA ")
            SQL.Append("FROM ")
            SQL.Append("    OPERADOR.VW_PRACA_ABERTA ")
            SQL.Append("WHERE DT_INICIO_PRACA IS NOT NULL AND (DT_FIM_PRACA>SYSDATE OR DT_FIM_PRACA IS NULL)")
            SQL.Append("  AND RESERVA = '{0}' ORDER BY NVL(DT_FIM_PRACA,TO_DATE('01/01/2100')) DESC ")
        Else
            SQL.Append("SELECT ")
            SQL.Append("    RESERVA ")
            SQL.Append("FROM ")
            SQL.Append("    OPERADOR.DBO.VW_PRACA_ABERTA ")
            SQL.Append("WHERE DT_INICIO_PRACA IS NOT NULL AND (DT_FIM_PRACA>SYSDATE OR DT_FIM_PRACA IS NULL)")
            SQL.Append("  AND RESERVA = '{0}' ORDER BY ISNULL(DT_FIM_PRACA,TO_DATE('01/01/2100')) DESC ")
        End If

        Rst.Open(String.Format(SQL.ToString(), Reserva), Banco.Conexao, 3, 3)

        If Rst.EOF Then
            Return True
        End If

        Return False

    End Function

    Public Function IsLate(ByVal Reserva As String) As Boolean

        Dim Rst As New ADODB.Recordset
        Dim SQL As New StringBuilder

        If Banco.BancoEmUso = "ORACLE" Then '
            SQL.Append("SELECT ")
            SQL.Append("    AUTONUM_VIAGEM ")
            SQL.Append("FROM ")
            SQL.Append("    OPERADOR.VW_PRACA_ABERTA ")
            SQL.Append("WHERE ")
            SQL.Append("    NVL(FLAG_LATE,0) = 1 ")
            SQL.Append("AND ")
            SQL.Append("    RESERVA='{0}' ")
        Else
            SQL.Append("SELECT ")
            SQL.Append("    AUTONUM_VIAGEM ")
            SQL.Append("FROM ")
            SQL.Append("    OPERADOR.DBO.VW_PRACA_ABERTA ")
            SQL.Append("WHERE ")
            SQL.Append("    ISNULL(FLAG_LATE,0) = 1 ")
            SQL.Append("AND ")
            SQL.Append("    RESERVA='{0}' ")
        End If

        Rst.Open(String.Format(SQL.ToString(), Reserva), Banco.Conexao, 3, 3)

        If Not Rst.EOF Then
            Return True
        End If

        Return False

    End Function

    Public Function EntradaDireta(ByVal Reserva As String, ByVal Viagem As String) As Boolean

        Dim Rst As New ADODB.Recordset
        Dim SQL As New StringBuilder

        If Banco.BancoEmUso = "ORACLE" Then '
            SQL.Append("SELECT ")
            SQL.Append("    NVL(FLAG_ENTRADA_DIRETA,0) FLAG_ENTRADA_DIRETA ")
            SQL.Append("FROM ")
            SQL.Append("    OPERADOR.TB_BOOKING ")
            SQL.Append("WHERE ")
            SQL.Append("    REFERENCE='{0}' ")
            SQL.Append("AND ")
            SQL.Append("    AUTONUMVIAGEM={1} ")
        Else
            SQL.Append("SELECT ")
            SQL.Append("    NVL(FLAG_ENTRADA_DIRETA,0) FLAG_ENTRADA_DIRETA ")
            SQL.Append("FROM ")
            SQL.Append("    OPERADOR.DBO.TB_BOOKING ")
            SQL.Append("WHERE ")
            SQL.Append("    REFERENCE='{0}' ")
            SQL.Append("AND ")
            SQL.Append("    AUTONUMVIAGEM={1} ")
        End If

        Rst.Open(String.Format(SQL.ToString(), Reserva, Viagem), Banco.Conexao, 3, 3)

        If Not Rst.EOF Then
            If Rst.Fields("FLAG_ENTRADA_DIRETA").Value.ToString() = "1" Then
                Return True
            End If
        End If

        Return False

    End Function

    Public Function ObterCodigoViagem(ByVal Reserva As String) As Integer

        Dim Rst As New ADODB.Recordset
        Dim SQL As New StringBuilder

        If Banco.BancoEmUso = "ORACLE" Then
            SQL.Append("SELECT ")
            SQL.Append("    AUTONUM_VIAGEM ")
            SQL.Append("FROM ")
            SQL.Append("    OPERADOR.VW_PRACA_ABERTA ")
            SQL.Append("WHERE ")
            SQL.Append("    DT_INICIO_PRACA IS NOT NULL AND (DT_FIM_PRACA>SYSDATE OR DT_FIM_PRACA IS NULL) ")
            SQL.Append("          AND RESERVA = '{0}' ORDER BY NVL(DT_FIM_PRACA,TO_DATE('01/01/2100')) DESC ")
        Else
            SQL.Append("SELECT ")
            SQL.Append("    AUTONUM_VIAGEM ")
            SQL.Append("FROM ")
            SQL.Append("    OPERADOR.VW_PRACA_ABERTA ")
            SQL.Append("WHERE ")
            SQL.Append("    DT_INICIO_PRACA IS NOT NULL AND (DT_FIM_PRACA>SYSDATE OR DT_FIM_PRACA IS NULL) ")
            SQL.Append("          AND RESERVA = '{0}' ORDER BY NVL(DT_FIM_PRACA,TO_DATE('01/01/2100')) DESC ")
        End If

        Rst.Open(String.Format(SQL.ToString(), Reserva), Banco.Conexao, 3, 3)

        If Not Rst.EOF Then
            Return Rst.Fields("AUTONUM_VIAGEM").Value.ToString()
        End If

        Return False

    End Function

    Public Function ConsultarRemarks(ByVal Reserva As String, ByVal AutonumViagem As String, ByVal Tipo As String, ByVal Tamanho As String) As List(Of Remark)

        Dim Rst As New ADODB.Recordset
        Dim SQL As New StringBuilder
        Dim Lista As New List(Of Remark)

        If Banco.BancoEmUso = "ORACLE" Then
            SQL.Append("SELECT ")
            SQL.Append("    DISTINCT ")
            SQL.Append("    AUTONUM, ")
            SQL.Append("    REMARKS ")
            SQL.Append("FROM ")
            SQL.Append("    OPERADOR.VW_BOOKING_WEB ")
            SQL.Append("WHERE ")
            SQL.Append("    REFERENCE='{0}' ")
            SQL.Append("AND ")
            SQL.Append("    AUTONUMVIAGEM={1} ")
            SQL.Append("AND ")
            SQL.Append("    TIPOBASICO='{2}' ")
            SQL.Append("AND ")
            SQL.Append("    TAMANHO={3} ")
            SQL.Append("ORDER BY ")
            SQL.Append("    REMARKS DESC ")
        Else
            SQL.Append("SELECT ")
            SQL.Append("    DISTINCT ")
            SQL.Append("    AUTONUM, ")
            SQL.Append("    REMARKS ")
            SQL.Append("FROM ")
            SQL.Append("    OPERADOR.DBO.VW_BOOKING_WEB ")
            SQL.Append("WHERE ")
            SQL.Append("    REFERENCE='{0}' ")
            SQL.Append("AND ")
            SQL.Append("    AUTONUMVIAGEM={1} ")
            SQL.Append("AND ")
            SQL.Append("    TIPOBASICO='{2}' ")
            SQL.Append("AND ")
            SQL.Append("    TAMANHO={3} ")
            SQL.Append("ORDER BY ")
            SQL.Append("    REMARKS DESC ")
        End If

        Rst.Open(String.Format(SQL.ToString(), Reserva, AutonumViagem, Tipo, Tamanho), Banco.Conexao, 3, 3)

        While Not Rst.EOF

            Dim Remark As New Remark

            Remark.Codigo = Rst.Fields("AUTONUM").Value.ToString()
            Remark.Descricao = Rst.Fields("REMARKS").Value.ToString()
            Lista.Add(Remark)

            Remark = Nothing
            Rst.MoveNext()

        End While

        Return Lista

    End Function

    Public Function ConsultarDadosReservaPorID(ByVal Reserva As String, ByVal Viagem As String, ByVal ID_Booking As Integer) As Reserva

        Dim Rst As New ADODB.Recordset
        Dim SQL As New StringBuilder

        If Banco.BancoEmUso = "ORACLE" Then
            SQL.Append("SELECT ")
            SQL.Append("    RESERVA, ")
            SQL.Append("    EXPORTADOR, ")
            SQL.Append("    NAVIOVIAGEM, ")
            SQL.Append("    PORTO_DESCARGA, ")
            SQL.Append("    PORTO_DESTINO, ")
            SQL.Append("    TO_CHAR(DATA_DEAD_LINE,'DD/MM/YYYY HH24:MI') DATA_DEAD_LINE, ")
            SQL.Append("    AUTONUM_VIAGEM, ")
            SQL.Append("    POD, ")
            SQL.Append("    EF, ")
            SQL.Append("    IMO1, ")
            SQL.Append("    IMO2, ")
            SQL.Append("    IMO3, ")
            SQL.Append("    IMO4, ")
            SQL.Append("    UN1, ")
            SQL.Append("    UN2, ")
            SQL.Append("    UN3, ")
            SQL.Append("    UN4, ")
            SQL.Append("    TEMPERATURA, ")
            SQL.Append("    ESCALA, ")
            SQL.Append("    OVERHEIGHT, ")
            SQL.Append("    OVERWIDTH, ")
            SQL.Append("    OVERLENGTH, ")
            SQL.Append("    OVERWIDTHL, ")
            SQL.Append("    OBS ")
            SQL.Append("FROM ")
            SQL.Append("    OPERADOR.VW_PRACA_ABERTA ")
            SQL.Append("WHERE ")
            SQL.Append("    DT_INICIO_PRACA IS NOT NULL AND (DT_FIM_PRACA>SYSDATE OR DT_FIM_PRACA IS NULL) ")
            SQL.Append("        AND RESERVA = '{0}' AND AUTONUM_VIAGEM = {1} AND AUTONUM_BOOKING={2} ORDER BY NVL(DT_FIM_PRACA,TO_DATE('01/01/2100')) DESC ")
        Else
            SQL.Append("SELECT ")
            SQL.Append("    RESERVA, ")
            SQL.Append("    EXPORTADOR, ")
            SQL.Append("    NAVIOVIAGEM, ")
            SQL.Append("    PORTO_DESCARGA, ")
            SQL.Append("    PORTO_DESTINO, ")
            SQL.Append("    DATA_DEAD_LINE, ")
            SQL.Append("    AUTONUM_VIAGEM, ")
            SQL.Append("    POD, ")
            SQL.Append("    EF, ")
            SQL.Append("    IMO1, ")
            SQL.Append("    IMO2, ")
            SQL.Append("    IMO3, ")
            SQL.Append("    IMO4, ")
            SQL.Append("    UN1, ")
            SQL.Append("    UN2, ")
            SQL.Append("    UN3, ")
            SQL.Append("    UN4, ")
            SQL.Append("    TEMPERATURA, ")
            SQL.Append("    ESCALA, ")
            SQL.Append("    OVERHEIGHT, ")
            SQL.Append("    OVERWIDTH, ")
            SQL.Append("    OVERLENGTH, ")
            SQL.Append("    OVERWIDTHL, ")
            SQL.Append("    OBS ")
            SQL.Append("FROM ")
            SQL.Append("    OPERADOR.DBO.VW_PRACA_ABERTA ")
            SQL.Append("WHERE ")
            SQL.Append("    DT_INICIO_PRACA IS NOT NULL AND (DT_FIM_PRACA>GETDATE() OR DT_FIM_PRACA IS NULL) ")
            SQL.Append("        AND RESERVA = '{0}' AND AUTONUM_VIAGEM = {1} AND AUTONUM_BOOKING={2} ORDER BY ISNULL(DT_FIM_PRACA,OPERADOR.TO_DATE('01/01/2100')) DESC ")
        End If

        Rst.Open(String.Format(SQL.ToString(), Reserva, Viagem, ID_Booking), Banco.Conexao, 3, 3)

        If Not Rst.EOF Then

            Dim ReservaOBJ As New Reserva

            ReservaOBJ.Reserva = Rst.Fields("RESERVA").Value.ToString()
            ReservaOBJ.Exportador = Rst.Fields("EXPORTADOR").Value.ToString()
            ReservaOBJ.Navioviagem = Rst.Fields("NAVIOVIAGEM").Value.ToString()
            ReservaOBJ.POD = Rst.Fields("PORTO_DESCARGA").Value.ToString()
            ReservaOBJ.FDES = Rst.Fields("PORTO_DESTINO").Value.ToString()
            ReservaOBJ.Data_dead_line = Rst.Fields("DATA_DEAD_LINE").Value.ToString()
            ReservaOBJ.Autonum_viagem = Rst.Fields("AUTONUM_VIAGEM").Value.ToString()
            ReservaOBJ.EF = Rst.Fields("EF").Value.ToString()
            ReservaOBJ.Imo1 = Rst.Fields("IMO1").Value.ToString()
            ReservaOBJ.Imo2 = Rst.Fields("IMO2").Value.ToString()
            ReservaOBJ.Imo3 = Rst.Fields("IMO3").Value.ToString()
            ReservaOBJ.Imo4 = Rst.Fields("IMO4").Value.ToString()
            ReservaOBJ.Un1 = Rst.Fields("UN1").Value.ToString()
            ReservaOBJ.Un2 = Rst.Fields("UN2").Value.ToString()
            ReservaOBJ.Un3 = Rst.Fields("UN3").Value.ToString()
            ReservaOBJ.Un4 = Rst.Fields("UN4").Value.ToString()
            ReservaOBJ.Temperatura = Rst.Fields("TEMPERATURA").Value.ToString()
            ReservaOBJ.Escala = Rst.Fields("ESCALA").Value.ToString()
            ReservaOBJ.Overheight = Rst.Fields("OVERHEIGHT").Value.ToString()
            ReservaOBJ.Overwidth = Rst.Fields("OVERWIDTH").Value.ToString()
            ReservaOBJ.Overlength = Rst.Fields("OVERLENGTH").Value.ToString()
            ReservaOBJ.Overwidthl = Rst.Fields("OVERWIDTHL").Value.ToString()
            ReservaOBJ.Umidade = ObterUmidade(ReservaOBJ.Reserva, ReservaOBJ.Autonum_viagem)
            ReservaOBJ.Ventilacao = ObterVentilacao(ReservaOBJ.Reserva, ReservaOBJ.Autonum_viagem)

            Return ReservaOBJ

        End If

        Return Nothing

    End Function

    Public Function ObterUmidade(ByVal Reserva As String, ByVal Viagem As String) As String

        Dim Rst As New ADODB.Recordset

        If Banco.BancoEmUso = "ORACLE" Then
            Rst.Open(String.Format("SELECT UMIDADE FROM OPERADOR.TB_BOOKING WHERE REFERENCE='{0}' AND AUTONUMVIAGEM={1}", Reserva, Viagem), Banco.Conexao, 3, 3)
        Else
            Rst.Open(String.Format("SELECT UMIDADE FROM OPERADOR.DBO.TB_BOOKING WHERE REFERENCE='{0}' AND AUTONUMVIAGEM={1}", Reserva, Viagem), Banco.Conexao, 3, 3)
        End If

        If Not Rst.EOF Then
            Return Rst.Fields("UMIDADE").Value.ToString()
        End If

        Return String.Empty

    End Function

    Public Function ObterVentilacao(ByVal Reserva As String, ByVal Viagem As String) As String

        Dim Rst As New ADODB.Recordset

        If Banco.BancoEmUso = "ORACLE" Then
            Rst.Open(String.Format("SELECT VENTILACAO FROM OPERADOR.TB_BOOKING WHERE REFERENCE='{0}' AND AUTONUMVIAGEM={1}", Reserva, Viagem), Banco.Conexao, 3, 3)
        Else
            Rst.Open(String.Format("SELECT VENTILACAO FROM OPERADOR.DBO.TB_BOOKING WHERE REFERENCE='{0}' AND AUTONUMVIAGEM={1}", Reserva, Viagem), Banco.Conexao, 3, 3)
        End If

        If Not Rst.EOF Then
            Return Rst.Fields("VENTILACAO").Value.ToString()
        End If

        Return String.Empty

    End Function

    Public Function ObterTemperatura(ByVal Reserva As String, ByVal Viagem As String) As String

        Dim Rst As New ADODB.Recordset

        If Banco.BancoEmUso = "ORACLE" Then
            Rst.Open(String.Format("SELECT TEMPERATURA FROM OPERADOR.TB_BOOKING WHERE REFERENCE='{0}' AND AUTONUMVIAGEM={1}", Reserva, Viagem), Banco.Conexao, 3, 3)
        Else
            Rst.Open(String.Format("SELECT TEMPERATURA FROM OPERADOR.DBO.TB_BOOKING WHERE REFERENCE='{0}' AND AUTONUMVIAGEM={1}", Reserva, Viagem), Banco.Conexao, 3, 3)
        End If

        If Not Rst.EOF Then
            Return Rst.Fields("TEMPERATURA").Value.ToString()
        End If

        Return String.Empty

    End Function

    Public Function ConsultarDadosParaEdicao(ByVal ID As String) As Reserva

        Dim Rst As New ADODB.Recordset
        Dim SQL As New StringBuilder

        If Banco.BancoEmUso = "ORACLE" Then
            SQL.Append("SELECT ")
            SQL.Append("    * ")
            SQL.Append("FROM ")
            SQL.Append("    OPERADOR.TB_GD_CONTEINER ")
            SQL.Append("WHERE ")
            SQL.Append("    AUTONUM_GD_CNTR={0} ")
        Else
            SQL.Append("SELECT ")
            SQL.Append("    * ")
            SQL.Append("FROM ")
            SQL.Append("    OPERADOR.DBO.TB_GD_CONTEINER ")
            SQL.Append("WHERE ")
            SQL.Append("    AUTONUM_GD_CNTR={0} ")
        End If

        Rst.Open(String.Format(SQL.ToString(), ID), Banco.Conexao, 3, 3)

        If Not Rst.EOF Then

            Dim Reserva As New Reserva

            Reserva.Codigo = Rst.Fields("AUTONUM_GD_CNTR").Value.ToString()
            Reserva.Reserva = Rst.Fields("REFERENCE").Value.ToString()
            Reserva.Autonum_viagem = Rst.Fields("AUTONUMVIAGEM").Value.ToString()
            Reserva.Autonum_reserva = Rst.Fields("AUTONUM_RESERVA").Value.ToString()
            Reserva.EF = Rst.Fields("EF").Value.ToString()
            Reserva.Imo1 = Rst.Fields("IMO1").Value.ToString()
            Reserva.Imo2 = Rst.Fields("IMO2").Value.ToString()
            Reserva.Imo3 = Rst.Fields("IMO3").Value.ToString()
            Reserva.Imo4 = Rst.Fields("IMO4").Value.ToString()
            Reserva.Un1 = Rst.Fields("ONU1").Value.ToString()
            Reserva.Un2 = Rst.Fields("ONU2").Value.ToString()
            Reserva.Un3 = Rst.Fields("ONU3").Value.ToString()
            Reserva.Un4 = Rst.Fields("ONU4").Value.ToString()
            Reserva.Temperatura = Rst.Fields("TEMPERATURA").Value.ToString()
            Reserva.Escala = Rst.Fields("ESCALA").Value.ToString()
            Reserva.PesoBruto = Rst.Fields("BRUTO").Value.ToString()
            Reserva.Overheight = Rst.Fields("OH").Value.ToString()
            Reserva.Overwidth = Rst.Fields("OW").Value.ToString()
            Reserva.Overlength = Rst.Fields("OL").Value.ToString()
            Reserva.Overwidthl = Rst.Fields("OWL").Value.ToString()
            Reserva.Obs = Rst.Fields("OBS").Value.ToString()
            Reserva.Sigla = Rst.Fields("ID_CONTEINER").Value.ToString()
            Reserva.Tara = Rst.Fields("TARA").Value.ToString()
            Reserva.Volumes = Rst.Fields("VOLUMES").Value.ToString()
            Reserva.Transporte = Rst.Fields("TIPO_TRANSPORTE").Value.ToString()
            Reserva.Vagao = Rst.Fields("VAGAO").Value.ToString()
            Reserva.Umidade = Rst.Fields("UMIDADE").Value.ToString()
            Reserva.Ventilacao = Rst.Fields("VENTILACAO").Value.ToString()
            Reserva.Lacre1 = Rst.Fields("LACRE1").Value.ToString()
            Reserva.Lacre2 = Rst.Fields("LACRE2").Value.ToString()
            Reserva.Lacre3 = Rst.Fields("LACRE3").Value.ToString()
            Reserva.Lacre4 = Rst.Fields("LACRE4").Value.ToString()
            Reserva.Lacre5 = Rst.Fields("LACRE5").Value.ToString()
            Reserva.Lacre6 = Rst.Fields("LACRE6").Value.ToString()
            Reserva.Lacre7 = Rst.Fields("LACRE7").Value.ToString()
            Reserva.LacreSIF = Rst.Fields("LACRE_SIF").Value.ToString()
            Reserva.Tamanho = Rst.Fields("TAMANHO").Value.ToString()
            Reserva.Tipo = Rst.Fields("TIPOBASICO").Value.ToString()
            Reserva.POD = Rst.Fields("POD").Value.ToString()

            Return Reserva

        End If

        Return Nothing

    End Function

    Public Function ValidarLacres(ByVal Reserva As String, ByVal Lacre As String) As Integer

        Dim Rst As New ADODB.Recordset
        Dim SQL As New StringBuilder

        If Banco.BancoEmUso = "ORACLE" Then

            SQL.Append("SELECT ")
            SQL.Append("    B.CARRIER ")
            SQL.Append("FROM ")
            SQL.Append("    OPERADOR.TB_BOOKING B ")
            SQL.Append("WHERE ")
            SQL.Append("    B.REFERENCE='{0}' ")

            Rst.Open(String.Format(SQL.ToString(), Reserva), Banco.Conexao, 3, 3)

            If Not Rst.EOF Then

                Using Conexao As New OleDbConnection(Banco.StringConexao(False))
                    Using Comando As New OleDbCommand()

                        Comando.CommandType = CommandType.StoredProcedure
                        Comando.CommandText = "OPERADOR.PROC_CHRONOS_VALIDA_LACRE"
                        Comando.Connection = Conexao

                        Comando.Parameters.Add("@Var_LINE", OleDbType.VarChar).Value = Rst.Fields("CARRIER").Value.ToString()
                        Comando.Parameters.Add("@Var_LACRE", OleDbType.VarChar).Value = Lacre

                        Comando.Parameters.Add("@Var_Autonum", OleDbType.Numeric, 2).Direction = ParameterDirection.Output
                        Comando.Parameters.Add("@ErroCode", OleDbType.VarChar, 250).Direction = ParameterDirection.Output

                        Conexao.Open()
                        Comando.ExecuteNonQuery()
                        Return Comando.Parameters("@Var_Autonum").Value.ToString()
                        Conexao.Close()

                    End Using
                End Using

            End If

        Else

            SQL.Clear()
            SQL.Append("SELECT ")
            SQL.Append("    B.AUTONUM,  ")
            SQL.Append("    ISNULL(B.CARRIER,'') CARRIER ")
            SQL.Append("FROM ")
            SQL.Append("    OPERADOR.DBO.TB_BOOKING B, ")
            SQL.Append("    OPERADOR.DBO.TB_VIAGENS V ")
            SQL.Append("WHERE ")
            SQL.Append("    B.AUTONUMVIAGEM=V.AUTONUM ")
            SQL.Append("AND ")
            SQL.Append("    V.OPERANDO=1 ")
            SQL.Append("AND NOT ")
            SQL.Append("    V.DATA_OP_INICIO IS NULL ")
            SQL.Append("AND ")
            SQL.Append("    (V.DATA_DEAD_LINE IS NULL OR V.DATA_DEAD_LINE > GETDATE()) ")
            SQL.Append("AND ")
            SQL.Append("    B.ATUAL=1 ")
            SQL.Append("AND ")
            SQL.Append("    FLAG_TRANSBORDO=0 ")
            SQL.Append("AND ")
            SQL.Append("    ISNULL(V.FLAG_INTERNA,0)=0 ")
            SQL.Append("AND ")
            SQL.Append("    B.REFERENCE='{0}' ")
            SQL.Append("ORDER BY ")
            SQL.Append("    V.DATA_DEAD_LINE DESC ")

            Rst.Open(String.Format(SQL.ToString(), Reserva), Banco.Conexao, 3, 3)

            If Not Rst.EOF Then

                Using Conexao As New SqlConnection(Banco.StringConexao(False))
                    Using Comando As New SqlCommand()

                        Comando.CommandType = CommandType.StoredProcedure
                        Comando.CommandText = "OPERADOR.DBO.PROC_CHRONOS_VALIDA_LACRE"
                        Comando.Connection = Conexao

                        Comando.Parameters.Add("Var_LINE", SqlDbType.VarChar).Value = Rst.Fields("CARRIER").Value.ToString()
                        Comando.Parameters.Add("Var_LACRE", SqlDbType.VarChar).Value = Lacre

                        Comando.Parameters.Add("Var_Autonum", SqlDbType.Int, 2).Direction = ParameterDirection.Output
                        Comando.Parameters.Add("ErroCode", SqlDbType.VarChar, 250).Direction = ParameterDirection.Output

                        Conexao.Open()
                        Comando.ExecuteNonQuery()
                        Return Convert.ToInt32(Comando.Parameters("Var_Autonum").Value)
                        Conexao.Close()


                    End Using
                End Using

            End If

        End If

        Return False

    End Function

    Public Function ConsultarMotoristas(ByVal ID As String) As DataTable

        Dim Rst As New ADODB.Recordset
        Dim SQL As New StringBuilder

        If Banco.BancoEmUso = "ORACLE" Then
            SQL.Append("SELECT ")
            SQL.Append("   A.AUTONUM, ")
            SQL.Append("   A.NOME || ' - ' || A.CNH AS NOME ")
            SQL.Append("FROM ")
            SQL.Append("   OPERADOR.TB_AG_MOTORISTAS A ")
            SQL.Append("WHERE ")
            SQL.Append("   A.ID_TRANSPORTADORA = {0} ")
            SQL.Append("ORDER BY ")
            SQL.Append("   A.NOME ")
        Else
            SQL.Append("SELECT ")
            SQL.Append("   A.AUTONUM, ")
            SQL.Append("   A.NOME + ' - ' + A.CNH AS NOME ")
            SQL.Append("FROM ")
            SQL.Append("   OPERADOR.DBO.TB_AG_MOTORISTAS A ")
            SQL.Append("WHERE ")
            SQL.Append("   A.ID_TRANSPORTADORA = {0} ")
            SQL.Append("ORDER BY ")
            SQL.Append("   A.NOME ")
        End If

        Rst.Open(String.Format(SQL.ToString(), ID), Banco.Conexao, 3, 3)

        Using Adapter As New OleDbDataAdapter
            Dim Ds As New DataSet
            Adapter.Fill(Ds, Rst, "TB_AG_MOTORISTAS")
            Return Ds.Tables(0)
        End Using

    End Function

    Public Function ConsultarVeiculos(ByVal ID As String) As DataTable

        Dim Rst As New ADODB.Recordset
        Dim SQL As New StringBuilder

        If Banco.BancoEmUso = "ORACLE" Then
            SQL.Append("SELECT ")
            SQL.Append("   A.AUTONUM, ")
            SQL.Append("   A.PLACA_CAVALO || ' - ' || A.PLACA_CARRETA AS PLACAS ")
            SQL.Append("FROM ")
            SQL.Append("   OPERADOR.TB_AG_VEICULOS A ")
            SQL.Append("WHERE ")
            SQL.Append("   A.ID_TRANSPORTADORA = {0} ")
            SQL.Append("ORDER BY ")
            SQL.Append("   A.PLACA_CAVALO ")
        Else
            SQL.Append("SELECT ")
            SQL.Append("   A.AUTONUM, ")
            SQL.Append("   A.PLACA_CAVALO + ' - ' + A.PLACA_CARRETA AS PLACAS ")
            SQL.Append("FROM ")
            SQL.Append("   OPERADOR.DBO.TB_AG_VEICULOS A ")
            SQL.Append("WHERE ")
            SQL.Append("   A.ID_TRANSPORTADORA = {0} ")
            SQL.Append("ORDER BY ")
            SQL.Append("   A.PLACA_CAVALO ")
        End If

        Rst.Open(String.Format(SQL.ToString(), ID), Banco.Conexao, 3, 3)

        Using Adapter As New OleDbDataAdapter
            Dim Ds As New DataSet
            Adapter.Fill(Ds, Rst, "TB_AG_VEICULOS")
            Return Ds.Tables(0)
        End Using

    End Function

    Public Function ObterViagem(ByVal ID_Conteiner As String) As String

        Dim Rst As New ADODB.Recordset

        If Banco.BancoEmUso = "ORACLE" Then
            Rst.Open(String.Format("SELECT NVL(AUTONUMVIAGEM,0) AUTONUMVIAGEM FROM OPERADOR.TB_GD_CONTEINER WHERE AUTONUM_GD_CNTR={0}", ID_Conteiner), Banco.Conexao, 3, 3)
        Else
            Rst.Open(String.Format("SELECT ISNULL(AUTONUMVIAGEM,0) AUTONUMVIAGEM FROM OPERADOR.DBO.TB_GD_CONTEINER WHERE AUTONUM_GD_CNTR={0}", ID_Conteiner), Banco.Conexao, 3, 3)
        End If

        If Not Rst.EOF Then
            Return Rst.Fields("AUTONUMVIAGEM").Value.ToString()
        End If

        Return String.Empty

    End Function

    Public Function ObterLine(ByVal Viagem As String, ByVal Reserva As String) As String

        Dim Rst As New ADODB.Recordset

        Rst.Open(String.Format("select carrier from tb_booking where reference= '{0}' and autonumviagem = '{1}' and atual = 1", Reserva, Viagem), Banco.Conexao, 3, 3)


        If Not Rst.EOF Then
            Return Rst.Fields("CARRIER").Value.ToString()
        End If

        Return String.Empty

    End Function

    Public Function ObterDataLate(ByVal Reserva As String) As String

        Dim Rst As New ADODB.Recordset

        If Banco.BancoEmUso = "ORACLE" Then
            Rst.Open(String.Format("SELECT TO_CHAR(DATA_LATE,'DD/MM/YYYY HH24:MI:SS') DATA_LATE FROM OPERADOR.VW_PRACA_ABERTA WHERE RESERVA = '{0}'", Reserva), Banco.Conexao, 3, 3)
        Else
            Rst.Open(String.Format("SELECT OPERADOR.DBO.TO_CHAR(DATA_LATE,'DD/MM/YYYY HH24:MI:SS') DATA_LATE FROM OPERADOR.DBO.VW_PRACA_ABERTA WHERE RESERVA = '{0}'", Reserva), Banco.Conexao, 3, 3)
        End If

        If Not Rst.EOF Then
            Return Rst.Fields("DATA_LATE").Value.ToString()
        End If

        Return String.Empty

    End Function

    Public Function ConsultarPeriodos(ByVal Conteiner As String, ByVal Reserva As String, ByVal Late As Boolean) As DataTable

        Dim Rst As New ADODB.Recordset
        Dim SQL As New StringBuilder

        Dim Viagem As String = ObterViagem(Conteiner)
        Dim Line As String = ObterLine(Viagem, Reserva)
        Dim DataLate As String = ObterDataLate(Reserva)
        Dim LimiteLate As Integer = 0

        Try
            If Late Then
                LimiteLate = My.Settings.LimiteLate.ToString()
            Else
                LimiteLate = 0
            End If
        Catch ex As Exception
            LimiteLate = 0
        End Try

        If Banco.BancoEmUso = "ORACLE" Then
            SQL.Append("SELECT ")
            SQL.Append("    AUTONUM_GD_RESERVA,")
            SQL.Append("    PERIODO_INICIAL,")
            SQL.Append("    PERIODO_FINAL,")
            SQL.Append("    SALDO, ")
            SQL.Append("    PERFINAL_DATE ")
            SQL.Append("FROM ")
            SQL.Append(" ( ")
            SQL.Append("SELECT ")
            SQL.Append("     A.AUTONUM_GD_RESERVA, ")
            SQL.Append("     TO_CHAR(A.PERIODO_INICIAL, 'DD/MM/YYYY HH24:MI') PERIODO_INICIAL, ")
            SQL.Append("     TO_CHAR(A.PERIODO_FINAL, 'DD/MM/YYYY HH24:MI') PERIODO_FINAL, ")
            SQL.Append("     TO_CHAR(A.LIMITE_MOVIMENTOS + " & LimiteLate & " ")
            SQL.Append("              - (SELECT COUNT(B.AUTONUM_GD_CNTR) ")
            SQL.Append("                   FROM OPERADOR.TB_GD_CONTEINER B ")
            SQL.Append("                  WHERE A.AUTONUM_GD_RESERVA = B.AUTONUM_GD_RESERVA), ")
            SQL.Append("              '000' ")
            SQL.Append("             ) AS SALDO, ")
            SQL.Append("    A.PERIODO_FINAL AS PERFINAL_DATE ")
            SQL.Append("    FROM OPERADOR.TB_GD_RESERVA A ")
            SQL.Append("   WHERE A.SERVICO_GATE = 'E' ")
            SQL.Append("     AND A.LIMITE_MOVIMENTOS > 0 ")
            SQL.Append("     AND A.PERIODO_FINAL > SYSDATE ")
            SQL.Append(" AND TO_CHAR(A.PERIODO_INICIAL,'YYYYMMDDHH24MI') >= TO_CHAR((SELECT NVL(MAX(B.DT_INICIO_PRACA),TO_DATE('01/01/2099','DD/MM/YYYY')) AS DATAINICIO ")
            SQL.Append(" FROM OPERADOR.TB_GD_CONTEINER A, OPERADOR.VW_PRACA_ABERTA B WHERE A.AUTONUMVIAGEM = B.AUTONUM_VIAGEM ")
            SQL.Append(" AND  A.REFERENCE = B.RESERVA AND A.TIPOBASICO=B.TIPOBASICO AND ")
            SQL.Append(" (A.AUTONUM_GD_CNTR=" & Conteiner & ")),'YYYYMMDDHH24MI')")
            SQL.Append(" AND TO_CHAR (A.PERIODO_FINAL, 'YYYYMMDDHH24MI') <= ")
            If Late Then
                If Not DataLate = String.Empty Then
                    SQL.Append(" '" & Format(Convert.ToDateTime(DataLate), "yyyyMMddHH24MM") & "' ")
                Else
                    SQL.Append(" TO_CHAR(SYSDATE,'YYYYMMDDHH24MI')  ")
                End If
            Else
                SQL.Append(" TO_CHAR((SELECT NVL(MAX(B.DT_FIM_PRACA),TO_DATE('01/01/2099','DD/MM/YYYY')) AS DATAINICIO ")
                SQL.Append(" FROM OPERADOR.TB_GD_CONTEINER A, OPERADOR.VW_PRACA_ABERTA B WHERE A.AUTONUMVIAGEM = B.AUTONUM_VIAGEM ")
                SQL.Append(" AND  A.REFERENCE = B.RESERVA AND A.TIPOBASICO=B.TIPOBASICO AND ")
                SQL.Append(" (A.AUTONUM_GD_CNTR=" & Conteiner & ")),'YYYYMMDDHH24MI')")
            End If

            SQL.Append(" AND A.AUTONUM_GD_RESERVA NOT IN( ")
            SQL.Append("    SELECT A.AUTONUM_GD_RESERVA ")
            SQL.Append("        FROM OPERADOR.TB_GPD_BLOQUEIO A ")
            SQL.Append("    WHERE ")
            SQL.Append("        (A.AUTONUM_VIAGEM =  " & Viagem & "  AND A.LINE = '" & Line & "' ) ")
            SQL.Append("    OR ")
            SQL.Append("        (A.AUTONUM_VIAGEM =  " & Viagem & "  AND NVL(A.LINE,' ') = ' ') ")
            SQL.Append("    OR ")
            SQL.Append("        (NVL(A.AUTONUM_VIAGEM,0) =  0  AND A.LINE = '" & Line & "') ")
            SQL.Append(") ")
            SQL.Append(") Q ")
            SQL.Append("WHERE ")
            SQL.Append("    SALDO > 0 ")
            SQL.Append(" ORDER BY to_date(PERIODO_FINAL,'dd/mm/yyyy hh24:mi:ss')")
        End If

        Rst.Open(String.Format(SQL.ToString(), Reserva), Banco.Conexao, 3, 3)

        Using Adapter As New OleDbDataAdapter
            Dim Ds As New DataSet
            Adapter.Fill(Ds, Rst, "TB_GD_RESERVA")
            Return Ds.Tables(0)
        End Using

    End Function

    Public Function Agendar(ByVal ID_Conteiner As String, ByVal ID_Motorista As String, ByVal AUTONUM_VEICULO As String, ByVal ID_Periodo As String, ByVal USRID As String) As Boolean

        Dim Rst As New ADODB.Recordset

        Try

            If Banco.BancoEmUso = "ORACLE" Then
                Rst.Open(String.Format("UPDATE OPERADOR.TB_GD_CONTEINER SET AUTONUM_GD_RESERVA={0}, NUM_PROTOCOLO={1}, ANO_PROTOCOLO={2}, AUTONUM_GD_MOTORISTA={3},AUTONUM_VEICULO={4},USRID={5} WHERE AUTONUM_GD_CNTR={6}", ID_Periodo, "SEQ_GD_PROT_" & Now.Year & ".NEXTVAL", Now.Year, ID_Motorista, AUTONUM_VEICULO, USRID, ID_Conteiner), Banco.Conexao, 3, 3)
            Else

                Dim Protocolo As String = String.Empty

                Rst.Open("SELECT MAX(NUM_PROTOCOLO)+1 AS PROTOCOLO FROM OPERADOR.DBO.TB_GD_CONTEINER", Banco.Conexao, 3, 3)

                If Not Rst.EOF Then
                    Protocolo = Rst.Fields("ID").Value.ToString()
                    Rst.Close()
                End If

                Rst.Open(String.Format("UPDATE OPERADOR.DBO.TB_GD_CONTEINER SET AUTONUM_GD_RESERVA={0}, NUM_PROTOCOLO={1}, ANO_PROTOCOLO={2}, AUTONUM_GD_MOTORISTA={3},AUTONUM_VEICULO={4},USRID={5} WHERE AUTONUM_GD_CNTR={6}", ID_Periodo, Protocolo, Now.Year, ID_Motorista, AUTONUM_VEICULO, USRID, ID_Conteiner), Banco.Conexao, 3, 3)

            End If

            Return True

        Catch ex As Exception
            Throw New Exception(ex.Message) '
        End Try

        Return False

    End Function
   



    Public Function AlterarAgendamento(ByVal ID_Conteiner As String, ByVal ID_Motorista As String, ByVal AUTONUM_VEICULO As String, ByVal ID_Periodo As String, ByVal USRID As String) As Boolean

        Dim Rst As New ADODB.Recordset

        Try

            If Banco.BancoEmUso = "ORACLE" Then
                Rst.Open(String.Format("UPDATE OPERADOR.TB_GD_CONTEINER SET AUTONUM_GD_RESERVA={0},AUTONUM_GD_MOTORISTA={1},AUTONUM_VEICULO={2},USRID={3},NUM_PROTOCOLO=OPERADOR.SEQ_GD_PROT_" & Now.Year & ".NEXTVAL, ANO_PROTOCOLO = " & Now.Year & ",STATUS = '' WHERE AUTONUM_GD_CNTR={4}", ID_Periodo, ID_Motorista, AUTONUM_VEICULO, USRID, ID_Conteiner), Banco.Conexao, 3, 3)
            Else
                Rst.Open(String.Format("UPDATE OPERADOR.DBO.TB_GD_CONTEINER SET AUTONUM_GD_RESERVA={0},AUTONUM_GD_MOTORISTA={1},AUTONUM_VEICULO={2},USRID={3} ,STATUS = '' WHERE AUTONUM_GD_CNTR={4}", ID_Periodo, ID_Motorista, AUTONUM_VEICULO, USRID, ID_Conteiner), Banco.Conexao, 3, 3)
            End If

            Return True

        Catch ex As Exception
            Throw New Exception(ex.Message) '
        End Try

        Return False

    End Function

    Public Function ObterCodigoConteiner(ByVal ID_Conteiner As String) As DataTable

        Dim Rst As New ADODB.Recordset

        If Banco.BancoEmUso = "ORACLE" Then
            Rst.Open(String.Format("SELECT AUTONUM_GD_CNTR,ID_CONTEINER FROM OPERADOR.TB_GD_CONTEINER WHERE ID_CONTEINER='{0}'", ID_Conteiner), Banco.Conexao, 3, 3)
        Else
            Rst.Open(String.Format("SELECT AUTONUM_GD_CNTR,ID_CONTEINER FROM OPERADOR.DBO.TB_GD_CONTEINER WHERE ID_CONTEINER='{0}'", ID_Conteiner), Banco.Conexao, 3, 3)
        End If

        Using Adapter As New OleDbDataAdapter
            Dim Ds As New DataSet
            Adapter.Fill(Ds, Rst, "TB_GD_CONTEINER")
            Return Ds.Tables(0)
        End Using

    End Function

    Public Function VerificarSaldo(ByVal ID As String) As Boolean

        Dim Rst As New ADODB.Recordset
        Dim SQL As New StringBuilder

        If Banco.BancoEmUso = "ORACLE" Then
            SQL.Append("SELECT ")
            SQL.Append("    A.AUTONUM_GD_RESERVA, ")
            SQL.Append("    TO_CHAR(A.LIMITE_MOVIMENTOS - (SELECT COUNT(B.AUTONUM_GD_CNTR) ")
            SQL.Append("FROM ")
            SQL.Append("    OPERADOR.TB_GD_CONTEINER B ")
            SQL.Append("WHERE ")
            SQL.Append("    A.AUTONUM_GD_RESERVA=B.AUTONUM_GD_RESERVA),'000') AS SALDO ")
            SQL.Append("FROM ")
            SQL.Append("    OPERADOR.TB_GD_RESERVA A ")
            SQL.Append("WHERE ")
            SQL.Append("    A.AUTONUM_GD_RESERVA={0} ")
        Else
            SQL.Append("SELECT ")
            SQL.Append("    A.AUTONUM_GD_RESERVA, ")
            SQL.Append("    OPERADOR.DBO.TO_CHAR(A.LIMITE_MOVIMENTOS - (SELECT COUNT(B.AUTONUM_GD_CNTR) ")
            SQL.Append("FROM ")
            SQL.Append("    OPERADOR.DBO.TB_GD_CONTEINER B ")
            SQL.Append("WHERE ")
            SQL.Append("    A.AUTONUM_GD_RESERVA=B.AUTONUM_GD_RESERVA),'000') AS SALDO ")
            SQL.Append("FROM ")
            SQL.Append("    OPERADOR.DBO.TB_GD_RESERVA A ")
            SQL.Append("WHERE ")
            SQL.Append("    A.AUTONUM_GD_RESERVA={0} ")
        End If

        Rst.Open(String.Format(SQL.ToString(), ID), Banco.Conexao, 3, 3)

        If Not Rst.EOF Then
            If Convert.ToInt32(Rst.Fields("SALDO").Value.ToString()) > 0 Then
                Return True
            End If
        End If

        Return False

    End Function

    Public Function Consultar(ByVal ID As String, Optional ByVal Filtro As String = "") As String

        Dim Rst As New ADODB.Recordset
        Dim SQL As New StringBuilder

        If Banco.BancoEmUso = "ORACLE" Then
            SQL.Append("SELECT ")
            SQL.Append("    A.AUTONUM_GD_CNTR, ")
            SQL.Append("    A.REFERENCE, ")
            SQL.Append("    A.ID_CONTEINER || ' ' || A.TAMANHO || ' ' || A.TIPOBASICO || ' ' || DECODE(A.EF,'E','EMPTY','FULL') AS ID_CONTEINER, ")
            SQL.Append("    A.NUM_PROTOCOLO, ")
            SQL.Append("    A.ANO_PROTOCOLO, ")
            SQL.Append("    A.NUM_PROTOCOLO || '/' || A.ANO_PROTOCOLO AS PROTOCOLO, ")
            SQL.Append("    CASE WHEN A.TIPO_TRANSPORTE = 'R' THEN 'RODOVI" & HttpContext.Current.Server.HtmlDecode("&#193;") & "RIO' ELSE 'FERROVI" & HttpContext.Current.Server.HtmlDecode("&#193;") & "RIO' END AS TRANSPORTE, ")
            SQL.Append("    B.VIAGEM, ")
            SQL.Append("    G.PLACA_CAVALO || ' - ' || G.PLACA_CARRETA AS VEICULO, ")
            SQL.Append("    C.NOME, ")
            SQL.Append("    TO_CHAR(D.PERIODO_INICIAL,'DD/MM/YYYY HH24:MI') || '<BR/>' || TO_CHAR(D.PERIODO_FINAL,'DD/MM/YYYY HH24:MI') AS PERIODO, ")
            SQL.Append("    E.NOME AS NAVIO, ")
            SQL.Append("    A.LINE AS LINE, ")
            SQL.Append("    F.NOME AS POD, ")
            SQL.Append("    A.AUTONUM_GD_MOTORISTA, ")
            SQL.Append("    A.AUTONUM_GD_RESERVA, ")
            SQL.Append("    D.PERIODO_INICIAL, ")
            SQL.Append("    D.PERIODO_FINAL, ")
            SQL.Append("    TO_CHAR(A.DT_CHEGADA ,'DD/MM/YYYY HH24:MI') DT_CHEGADA_CNTR, ")
            SQL.Append("    CASE WHEN A.STATUS='IM' THEN 'SIM' ELSE 'N" & HttpContext.Current.Server.HtmlDecode("&#195;") & "O' END AS IMP, ")
            SQL.Append("    G.AUTONUM AS AUTONUM_VEICULO ")
            SQL.Append("FROM ")
            SQL.Append("    OPERADOR.TB_GD_CONTEINER A ")
            SQL.Append("LEFT JOIN ")
            SQL.Append("    OPERADOR.TB_VIAGENS B ON A.AUTONUMVIAGEM = B.AUTONUM ")
            SQL.Append("LEFT JOIN ")
            SQL.Append("    OPERADOR.TB_AG_MOTORISTAS C ON A.AUTONUM_GD_MOTORISTA = C.AUTONUM ")
            SQL.Append("LEFT JOIN ")
            SQL.Append("    OPERADOR.TB_GD_RESERVA D ON A.AUTONUM_GD_RESERVA = D.AUTONUM_GD_RESERVA ")
            SQL.Append("LEFT JOIN ")
            SQL.Append("    OPERADOR.TB_CAD_NAVIOS E ON B.NAVIO = E.AUTONUM ")
            SQL.Append("LEFT JOIN ")
            SQL.Append("    OPERADOR.TB_CAD_PORTOS F ON A.POD = F.CODIGO ")
            SQL.Append("LEFT JOIN ")
            SQL.Append("    OPERADOR.TB_AG_VEICULOS G ON A.AUTONUM_VEICULO = G.AUTONUM ")
            SQL.Append("LEFT JOIN ")
            SQL.Append("    OPERADOR.TB_PATIO P ON A.AUTONUMVIAGEM = P.AUTONUMVIAGEM AND A.ID_CONTEINER = P.ID_CONTEINER ")
            SQL.Append("WHERE ")
            SQL.Append("    A.AUTONUM_TRANSPORTADORA={0} ")
            SQL.Append("AND ")
            SQL.Append("    NVL(A.NUM_PROTOCOLO,0) <> 0 ")
            SQL.Append("AND ")
            SQL.Append("   A.AUTONUMVIAGEM IN (SELECT AUTONUM FROM OPERADOR.TB_VIAGENS WHERE OPERANDO=1)  ")
            SQL.Append(Filtro)
            SQL.Append(" ORDER BY ")
            SQL.Append("    A.ID_CONTEINER ")
        Else
            SQL.Append("SELECT ")
            SQL.Append("    A.AUTONUM_GD_CNTR, ")
            SQL.Append("    A.REFERENCE, ")
            SQL.Append("    A.ID_CONTEINER + ' ' + A.TAMANHO + ' ' + A.TIPOBASICO + ' ' + CASE WHEN A.EF='E' THEN 'EMPTY' ELSE 'FULL' END AS ID_CONTEINER, ")
            SQL.Append("    A.NUM_PROTOCOLO, ")
            SQL.Append("    A.ANO_PROTOCOLO, ")
            SQL.Append("    A.NUM_PROTOCOLO + '/' + A.ANO_PROTOCOLO AS PROTOCOLO, ")
            SQL.Append("    CASE WHEN A.TIPO_TRANSPORTE = 'R' THEN 'RODOVIÁRIO' ELSE 'FERROVIÁRIO' END AS TRANSPORTE, ")
            SQL.Append("    B.VIAGEM, ")
            SQL.Append("    G.PLACA_CAVALO + '<BR/>' + G.PLACA_CARRETA AS VEICULO, ")
            SQL.Append("    C.NOME, ")
            SQL.Append("    TO_CHAR(D.PERIODO_INICIAL,'DD/MM/YYYY HH24:MI') + '<BR/>' + TO_CHAR(D.PERIODO_FINAL,'DD/MM/YYYY HH24:MI') AS PERIODO, ")
            SQL.Append("    E.NOME AS NAVIO, ")
            SQL.Append("    A.LINE AS LINE, ")
            SQL.Append("    F.NOME AS POD, ")
            SQL.Append("    A.AUTONUM_GD_MOTORISTA, ")
            SQL.Append("    A.AUTONUM_GD_RESERVA, ")
            SQL.Append("    D.PERIODO_INICIAL, ")
            SQL.Append("    D.PERIODO_FINAL, ")
            SQL.Append("    OPERADOR.DBO.TO_CHAR(A.DT_CHEGADA ,'DD/MM/YYYY HH24:MI'), ")
            SQL.Append("    CASE WHEN A.STATUS='IM' THEN 'SIM' ELSE 'NÃO' END AS IMP, ")
            SQL.Append("    G.AUTONUM AS AUTONUM_VEICULO ")
            SQL.Append("FROM ")
            SQL.Append("    OPERADOR.TB_GD_CONTEINER A ")
            SQL.Append("LEFT JOIN ")
            SQL.Append("    OPERADOR.TB_VIAGENS B ON A.AUTONUMVIAGEM = B.AUTONUM ")
            SQL.Append("LEFT JOIN ")
            SQL.Append("    OPERADOR.TB_AG_MOTORISTAS C ON A.AUTONUM_GD_MOTORISTA = C.AUTONUM ")
            SQL.Append("LEFT JOIN ")
            SQL.Append("    OPERADOR.TB_GD_RESERVA D ON A.AUTONUM_GD_RESERVA = D.AUTONUM_GD_RESERVA ")
            SQL.Append("LEFT JOIN ")
            SQL.Append("    OPERADOR.TB_CAD_NAVIOS E ON B.NAVIO = E.AUTONUM ")
            SQL.Append("LEFT JOIN ")
            SQL.Append("    OPERADOR.TB_CAD_PORTOS F ON A.POD = F.CODIGO ")
            SQL.Append("LEFT JOIN ")
            SQL.Append("    OPERADOR.TB_AG_VEICULOS G ON A.AUTONUM_VEICULO = G.AUTONUM ")
            SQL.Append("LEFT JOIN ")
            SQL.Append("    OPERADOR.TB_PATIO P ON A.AUTONUMVIAGEM = P.AUTONUMVIAGEM AND A.ID_CONTEINER = P.ID_CONTEINER ")
            SQL.Append("WHERE ")
            SQL.Append("    A.AUTONUM_TRANSPORTADORA={0} ")
            SQL.Append("AND ")
            SQL.Append("    NVL(A.NUM_PROTOCOLO,0) <> 0 ")
            SQL.Append("AND ")
            SQL.Append("    A.AUTONUMVIAGEM IN (SELECT AUTONUM FROM OPERADOR.DBO.TB_VIAGENS WHERE OPERANDO=1) ")
            SQL.Append(Filtro)
            SQL.Append(" ORDER BY ")
            SQL.Append("    A.ID_CONTEINER ")
        End If

        Return String.Format(SQL.ToString(), ID)

    End Function

    Public Function HabilitaEdicaoExclusao(ByVal ID As String) As Boolean

        Dim Rst As New ADODB.Recordset
        Dim SQL As New StringBuilder

        If Banco.BancoEmUso = "ORACLE" Then
            SQL.Append("SELECT ")
            SQL.Append("    NVL(P.DT_REGISTRO,NULL) DT_REGISTRO ")
            SQL.Append("FROM ")
            SQL.Append("    OPERADOR.TB_GD_CONTEINER A ")
            SQL.Append("LEFT JOIN ")
            SQL.Append("    OPERADOR.TB_VIAGENS B ON A.AUTONUMVIAGEM = B.AUTONUM ")
            SQL.Append("LEFT JOIN ")
            SQL.Append("    OPERADOR.TB_AG_MOTORISTAS C ON A.AUTONUM_GD_MOTORISTA = C.AUTONUM ")
            SQL.Append("LEFT JOIN ")
            SQL.Append("    OPERADOR.TB_GD_RESERVA D ON A.AUTONUM_GD_RESERVA = D.AUTONUM_GD_RESERVA ")
            SQL.Append("LEFT JOIN ")
            SQL.Append("    OPERADOR.TB_CAD_NAVIOS E ON B.NAVIO = E.AUTONUM ")
            SQL.Append("LEFT JOIN ")
            SQL.Append("    OPERADOR.TB_CAD_PORTOS F ON A.POD = F.CODIGO ")
            SQL.Append("LEFT JOIN ")
            SQL.Append("    OPERADOR.TB_AG_VEICULOS G ON A.AUTONUM_VEICULO = G.AUTONUM ")
            SQL.Append("LEFT JOIN ")
            SQL.Append("    OPERADOR.TB_PATIO P ON A.AUTONUMVIAGEM = P.AUTONUMVIAGEM AND A.ID_CONTEINER = P.ID_CONTEINER ")
            SQL.Append("WHERE ")
            SQL.Append("    A.ID_CONTEINER = '{0}' ")
        Else
            SQL.Append("SELECT ")
            SQL.Append("    ISNULL(P.DT_REGISTRO,NULL) DT_REGISTRO ")
            SQL.Append("FROM ")
            SQL.Append("    OPERADOR.DBO.TB_GD_CONTEINER A ")
            SQL.Append("LEFT JOIN ")
            SQL.Append("    OPERADOR.DBO.TB_VIAGENS B ON A.AUTONUMVIAGEM = B.AUTONUM ")
            SQL.Append("LEFT JOIN ")
            SQL.Append("    OPERADOR.DBO.TB_AG_MOTORISTAS C ON A.AUTONUM_GD_MOTORISTA = C.AUTONUM ")
            SQL.Append("LEFT JOIN ")
            SQL.Append("    OPERADOR.DBO.TB_GD_RESERVA D ON A.AUTONUM_GD_RESERVA = D.AUTONUM_GD_RESERVA ")
            SQL.Append("LEFT JOIN ")
            SQL.Append("    OPERADOR.DBO.TB_CAD_NAVIOS E ON B.NAVIO = E.AUTONUM ")
            SQL.Append("LEFT JOIN ")
            SQL.Append("    OPERADOR.DBO.TB_CAD_PORTOS F ON A.POD = F.CODIGO ")
            SQL.Append("LEFT JOIN ")
            SQL.Append("    OPERADOR.DBO.TB_AG_VEICULOS G ON A.AUTONUM_VEICULO = G.AUTONUM ")
            SQL.Append("LEFT JOIN ")
            SQL.Append("    OPERADOR.DBO.TB_PATIO P ON A.AUTONUMVIAGEM = P.AUTONUMVIAGEM AND A.ID_CONTEINER = P.ID_CONTEINER ")
            SQL.Append("WHERE ")
            SQL.Append("    A.ID_CONTEINER = '{0}' ")
        End If

        Rst.Open(String.Format(SQL.ToString(), ID), Banco.Conexao, 3, 3)

        If Not Rst.EOF Then
            If Not String.IsNullOrEmpty(Rst.Fields("DT_REGISTRO").Value.ToString()) Then
                Return False
            End If
        End If

        Return True

    End Function

    Public Function AlteraStatusImpressao(ByVal Protocolo As String) As Boolean

        Dim Rst As New ADODB.Recordset

        Try

            If Banco.BancoEmUso = "ORACLE" Then
                Rst.Open(String.Format("UPDATE OPERADOR.TB_GD_CONTEINER SET STATUS = 'IM' WHERE NUM_PROTOCOLO || ANO_PROTOCOLO IN({0})", Protocolo), Banco.Conexao, 3, 3)
            Else
                Rst.Open(String.Format("UPDATE OPERADOR.DBO.TB_GD_CONTEINER SET STATUS = 'IM' WHERE NUM_PROTOCOLO + ANO_PROTOCOLO IN({0})", Protocolo), Banco.Conexao, 3, 3)
            End If

            Return True

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try

        Return False

    End Function

    Public Function GerarProtocolo(ByVal ID As String, ByVal CorPadrao As String, ByVal Empresa As String) As StringBuilder

        Dim Rst1 As New ADODB.Recordset
        Dim Rst2 As New ADODB.Recordset
        Dim Rst3 As New ADODB.Recordset
        Dim Rst4 As New ADODB.Recordset

        Dim RstItens As New ADODB.Recordset

        Dim SQL As New StringBuilder

        Dim Estrutura As New StringBuilder
        Dim Tabela1 As New StringBuilder
        Dim Tabela2 As New StringBuilder
        Dim Header As New StringBuilder
        Dim TabelaItem As New StringBuilder

        Dim Protocolos As String() = ID.Split(",")

        Dim ID_Conteiner As String = String.Empty

        Dim Contador As Integer = 0

        Dim Umidade As String = String.Empty
        Dim Ventilacao As String = String.Empty

        For Each Item In Protocolos

            If Banco.BancoEmUso = "ORACLE" Then
                SQL.Append("SELECT ")
                SQL.Append("    A.AUTONUM_GD_CNTR, ")
                SQL.Append("    A.AUTONUMVIAGEM, ")
                SQL.Append("    A.ID_CONTEINER AS CONTEINER, ")
                SQL.Append("    A.ID_CONTEINER || ' ' ||  A.TAMANHO || ' ' || A.TIPOBASICO AS ID_CONTEINER, ")
                SQL.Append("    A.REFERENCE, ")
                SQL.Append("    A.LINE, ")
                SQL.Append("    A.POD, ")
                SQL.Append("    A.FDES, ")
                SQL.Append("    A.NUM_PROTOCOLO || '/' || ANO_PROTOCOLO AS PROTOCOLO, ")
                SQL.Append("    NVL(A.TARA,0) TARA, ")
                SQL.Append("    NVL(A.BRUTO,0) BRUTO, ")
                SQL.Append("    A.VOLUMES, ")
                SQL.Append("    A.IMO1, ")
                SQL.Append("    A.IMO2, ")
                SQL.Append("    A.IMO3, ")
                SQL.Append("    A.IMO4, ")
                SQL.Append("    A.ONU1, ")
                SQL.Append("    A.ONU2, ")
                SQL.Append("    A.ONU3, ")
                SQL.Append("    A.ONU4, ")
                SQL.Append("    G.TEMPERATURA, ")
                SQL.Append("    A.ESCALA, ")
                SQL.Append("    A.UMIDADE, ")
                SQL.Append("    A.VENTILACAO, ")
                SQL.Append("    NVL(A.OH,0) OH, ")
                SQL.Append("    NVL(A.OL,0) OL, ")
                SQL.Append("    NVL(A.OW,0) OW, ")
                SQL.Append("    NVL(A.OWL,0) OWL, ")
                SQL.Append("    A.LACRE1, ")
                SQL.Append("    A.LACRE2, ")
                SQL.Append("    A.LACRE3, ")
                SQL.Append("    A.LACRE4, ")
                SQL.Append("    A.LACRE5, ")
                SQL.Append("    A.LACRE6, ")
                SQL.Append("    A.LACRE7, ")
                SQL.Append("    A.LACRE_SIF, ")
                SQL.Append("    A.OBS, ")
                SQL.Append("    A.NUM_PROTOCOLO, ")
                SQL.Append("    A.ANO_PROTOCOLO, ")
                SQL.Append("    P.RAZAO AS EXPORTADOR, ")
                SQL.Append("    CASE WHEN A.TIPO_TRANSPORTE = 'R' THEN 'RODOVIÁRIO' ELSE 'FERROVIÁRIO' END AS TRANSPORTE, ")
                SQL.Append("    A.TIPO_TRANSPORTE AS TIPOTRANSP, ")
                SQL.Append("    A.VAGAO AS VAGAO, ")
                SQL.Append("    V.PLACA_CAVALO, ")
                SQL.Append("    V.PLACA_CARRETA, ")
                SQL.Append("    V.MODELO, ")
                SQL.Append("    C.NOME, ")
                SQL.Append("    C.CNH, ")
                SQL.Append("    C.RG, ")
                SQL.Append("    C.NEXTEL, ")
                SQL.Append("    TO_CHAR(D.PERIODO_INICIAL,'DD/MM/YYYY HH24:MI') || ' - ' || TO_CHAR(D.PERIODO_FINAL,'DD/MM/YYYY HH24:MI') AS PERIODO, ")
                SQL.Append("    E.NOME || ' / ' || B.VIAGEM AS NAVIOVIAGEM, ")
                SQL.Append("    F.RAZAO AS TRANSPORTADORA, ")
                SQL.Append("    TO_CHAR(B.DATA_DEAD_LINE,'DD/MM/YYYY HH24:MI') DATA_DEAD_LINE, ")
                SQL.Append("    TO_CHAR(B.DATA_ETA_PRIM,'DD/MM/YYYY HH24:MI') DATA_ETA_PRIM ")
                SQL.Append("FROM ")
                SQL.Append("    OPERADOR.TB_GD_CONTEINER A ")
                SQL.Append("LEFT JOIN ")
                SQL.Append("    OPERADOR.TB_VIAGENS B ON A.AUTONUMVIAGEM = B.AUTONUM ")
                SQL.Append("LEFT JOIN ")
                SQL.Append("    OPERADOR.TB_AG_MOTORISTAS C ON A.AUTONUM_GD_MOTORISTA = C.AUTONUM ")
                SQL.Append("LEFT JOIN ")
                SQL.Append("    OPERADOR.TB_AG_VEICULOS V ON A.AUTONUM_VEICULO = V.AUTONUM ")
                SQL.Append("LEFT JOIN ")
                SQL.Append("    OPERADOR.TB_GD_RESERVA D ON A.AUTONUM_GD_RESERVA = D.AUTONUM_GD_RESERVA ")
                SQL.Append("LEFT JOIN ")
                SQL.Append("    OPERADOR.TB_CAD_NAVIOS E ON B.NAVIO = E.AUTONUM ")
                SQL.Append("LEFT JOIN ")
                SQL.Append("    OPERADOR.TB_CAD_TRANSPORTADORAS F ON A.AUTONUM_TRANSPORTADORA = F.AUTONUM ")
                SQL.Append("LEFT JOIN ")
                SQL.Append("    OPERADOR.TB_BOOKING G ON A.AUTONUM_RESERVA = G.AUTONUM ")
                SQL.Append("LEFT JOIN ")
                SQL.Append("    OPERADOR.TB_CAD_CLIENTES P ON G.SHIPPER = P.AUTONUM ")
                SQL.Append("WHERE ")
                SQL.Append("    A.NUM_PROTOCOLO || A.ANO_PROTOCOLO IN({0}) ")
            Else
                SQL.Append("SELECT ")
                SQL.Append("    A.AUTONUM_GD_CNTR, ")
                SQL.Append("    A.AUTONUMVIAGEM, ")
                SQL.Append("    A.REFERENCE, ")
                SQL.Append("    A.LINE, ")
                SQL.Append("    A.POD, ")
                SQL.Append("    A.FDES, ")
                SQL.Append("    A.NUM_PROTOCOLO + '/' + ANO_PROTOCOLO AS PROTOCOLO, ")
                SQL.Append("    A.TAMANHO, ")
                SQL.Append("    A.TIPOBASICO, ")
                SQL.Append("    ISNULL(A.TARA,0) TARA, ")
                SQL.Append("    ISNULL(A.BRUTO,0) BRUTO, ")
                SQL.Append("    A.VOLUMES, ")
                SQL.Append("    A.IMO1, ")
                SQL.Append("    A.IMO2, ")
                SQL.Append("    A.IMO3, ")
                SQL.Append("    A.IMO4, ")
                SQL.Append("    A.ONU1, ")
                SQL.Append("    A.ONU2, ")
                SQL.Append("    A.ONU3, ")
                SQL.Append("    A.ONU4, ")
                SQL.Append("    G.TEMPERATURA, ")
                SQL.Append("    A.ESCALA, ")
                SQL.Append("    A.UMIDADE, ")
                SQL.Append("    A.VENTILACAO, ")
                SQL.Append("    A.OH, ")
                SQL.Append("    A.OL, ")
                SQL.Append("    A.OW, ")
                SQL.Append("    A.OWL, ")
                SQL.Append("    A.LACRE1, ")
                SQL.Append("    A.LACRE2, ")
                SQL.Append("    A.LACRE3, ")
                SQL.Append("    A.LACRE4, ")
                SQL.Append("    A.LACRE5, ")
                SQL.Append("    A.LACRE6, ")
                SQL.Append("    A.LACRE7, ")
                SQL.Append("    A.LACRE_SIF, ")
                SQL.Append("    A.OBS, ")
                SQL.Append("    P.RAZAO AS EXPORTADOR, ")
                SQL.Append("    CASE WHEN A.TIPO_TRANSPORTE = 'R' THEN 'RODOVIÁRIO' ELSE 'FERROVIÁRIO' END AS TRANSPORTE, ")
                SQL.Append("    V.PLACA_CAVALO, ")
                SQL.Append("    V.PLACA_CARRETA, ")
                SQL.Append("    V.MODELO, ")
                SQL.Append("    C.NOME, ")
                SQL.Append("    C.CNH, ")
                SQL.Append("    C.RG, ")
                SQL.Append("    C.NEXTEL, ")
                SQL.Append("    OPERADOR.DBO.TO_CHAR(D.PERIODO_INICIAL,'DD/MM/YYYY HH24:MI') + ' - ' + OPERADOR.DBO.TO_CHAR(D.PERIODO_FINAL,'DD/MM/YYYY HH24:MI') AS PERIODO, ")
                SQL.Append("    E.NOME + ' / ' + B.VIAGEM AS NAVIOVIAGEM, ")
                SQL.Append("    F.RAZAO AS TRANSPORTADORA, ")
                SQL.Append("    OPERADOR.DBO.TO_CHAR(B.DATA_DEAD_LINE,'DD/MM/YYYY HH24:MI') DATA_DEAD_LINE, ")
                SQL.Append("    OPERADOR.DBO.TO_CHAR(B.DATA_ETA_PRIM,'DD/MM/YYYY HH24:MI') DATA_ETA_PRIM ")
                SQL.Append("FROM ")
                SQL.Append("    OPERADOR.DBO.TB_GD_CONTEINER A ")
                SQL.Append("LEFT JOIN ")
                SQL.Append("    OPERADOR.DBO.TB_VIAGENS B ON A.AUTONUMVIAGEM = B.AUTONUM ")
                SQL.Append("LEFT JOIN ")
                SQL.Append("    OPERADOR.DBO.TB_AG_MOTORISTAS C ON A.AUTONUM_GD_MOTORISTA = C.AUTONUM ")
                SQL.Append("LEFT JOIN ")
                SQL.Append("    OPERADOR.DBO.TB_AG_VEICULOS V ON A.AUTONUM_VEICULO = V.AUTONUM ")
                SQL.Append("LEFT JOIN ")
                SQL.Append("    OPERADOR.DBO.TB_GD_RESERVA D ON A.AUTONUM_GD_RESERVA = D.AUTONUM_GD_RESERVA ")
                SQL.Append("LEFT JOIN ")
                SQL.Append("    OPERADOR.DBO.TB_CAD_NAVIOS E ON B.NAVIO = E.AUTONUM ")
                SQL.Append("LEFT JOIN ")
                SQL.Append("    OPERADOR.DBO.TB_CAD_TRANSPORTADORAS F ON A.AUTONUM_TRANSPORTADORA = F.AUTONUM ")
                SQL.Append("LEFT JOIN ")
                SQL.Append("    OPERADOR.DBO.TB_BOOKING G ON A.AUTONUM_RESERVA = G.AUTONUM ")
                SQL.Append("LEFT JOIN ")
                SQL.Append("    OPERADOR.DBO.TB_CAD_CLIENTES P ON G.SHIPPER = P.AUTONUM ")
                SQL.Append("WHERE ")
                SQL.Append("    A.NUM_PROTOCOLO + A.ANO_PROTOCOLO IN({0}) ")
            End If

            If Rst1.State = 1 Then
                Rst1.Close()
            End If

            Rst1.Open(String.Format(SQL.ToString(), Item), Banco.Conexao, 3, 3)

            ID_Conteiner = Rst1.Fields("CONTEINER").Value.ToString()

            Header.Append("<table id=cabecalho>")
            Header.Append("	<tr>")
            Header.Append("		<td align=left width=180px>")

            If Empresa = "1" Then
                Header.Append("			<img src=css/tecondi/images/LogoTop.png />")
            Else
                Header.Append("			<img src=css/termares/images/LogoTop.png />")
            End If

            Header.Append("		</td>")
            Header.Append("		<td>")
            Header.Append("		<font face=Arial size=3>PROTOCOLO DE AGENDAMENTO DE CONTÊINER (EXPORTAÇÃO)</font>")
            Header.Append("		<br/>")
            Header.Append("        <font face=Arial size=5>Nº: " & Rst1.Fields("NUM_PROTOCOLO").Value.ToString() & "/" & Rst1.Fields("ANO_PROTOCOLO").Value.ToString() & "</font> ")

            If Rst1.Fields("TIPOTRANSP").Value.ToString() = "F" Then
                Header.Append("     <br/><br/>")
            Else
                If Empresa = "1" Then
                    Header.Append("		<br/><br/>Período previsto de chegada no terminal TECONDI:<br/>")
                Else
                    Header.Append("		<br/><br/>Período previsto de chegada no terminal TERMARES:<br/>")
                End If

                Header.Append("        " & Rst1.Fields("PERIODO").Value.ToString())
            End If
            Header.Append("		</td>")
            Header.Append("	</tr>")
            Header.Append("</table>")
            Header.Append("<br/>")

            If Not Rst1.EOF Then

                Tabela1.Append("<table>")
                Tabela1.Append("<caption>Dados do Transporte</caption>")
                Tabela1.Append("    <thead bgcolor=" & CorPadrao & ">")
                Tabela1.Append("        <td>TRANSPORTADORA</td>")

                If Rst1.Fields("TIPOTRANSP").Value.ToString() = "F" Then
                    Tabela1.Append("        <td>VAGAO</td>")
                Else
                    Tabela1.Append("        <td>PLACA CAVALO</td>")
                    Tabela1.Append("        <td>PLACA CARRETA</td>")
                    Tabela1.Append("        <td>MODELO</td>")
                End If

                Tabela1.Append("    </thead>")
                Tabela1.Append("    <tbody>")
                Tabela1.Append("        <td>" & Rst1.Fields("TRANSPORTADORA").Value.ToString() & "</td>")

                If Rst1.Fields("TIPOTRANSP").Value.ToString() = "F" Then
                    Tabela1.Append("        <td>" & Rst1.Fields("VAGAO").Value.ToString() & "</td>")
                Else
                    Tabela1.Append("        <td>" & Rst1.Fields("PLACA_CAVALO").Value.ToString() & "</td>")
                    Tabela1.Append("        <td>" & Rst1.Fields("PLACA_CARRETA").Value.ToString() & "</td>")
                    Tabela1.Append("        <td>" & Rst1.Fields("MODELO").Value.ToString() & "</td>")
                    Tabela1.Append("    </tbody>")
                    Tabela1.Append("    <thead bgcolor=" & CorPadrao & ">")
                    Tabela1.Append("        <td>MOTORISTA</td>")
                    Tabela1.Append("        <td>CNH</td>")
                    Tabela1.Append("        <td>RG</td>")
                    Tabela1.Append("        <td>NEXTEL</td>")
                    Tabela1.Append("    </thead>")
                    Tabela1.Append("    <tbody>")
                    Tabela1.Append("        <td>" & Rst1.Fields("NOME").Value.ToString() & "</td>")
                    Tabela1.Append("        <td>" & Rst1.Fields("CNH").Value.ToString() & "</td>")
                    Tabela1.Append("        <td>" & Rst1.Fields("RG").Value.ToString() & "</td>")
                    Tabela1.Append("        <td>" & Rst1.Fields("NEXTEL").Value.ToString() & "</td>")
                End If

                Tabela1.Append("    </tbody>")
                Tabela1.Append("</table>")

                Tabela1.Append("<table>")
                Tabela1.Append("<caption>Identificação do(s) Container(s)</caption>")
                Tabela1.Append("    <thead bgcolor=" & CorPadrao & ">")
                Tabela1.Append("        <td>RESERVA</td>")
                Tabela1.Append("        <td>CONTÊINER</td>")
                Tabela1.Append("        <td>LINE</td>")
                Tabela1.Append("        <td>PORTO DESCARGA</td>")
                Tabela1.Append("        <td>PORTO DESTINO</td>")
                Tabela1.Append("    </thead>")
                Tabela1.Append("    <tbody>")
                Tabela1.Append("        <td>" & Rst1.Fields("REFERENCE").Value.ToString() & "</td>")
                Tabela1.Append("        <td>" & Rst1.Fields("ID_CONTEINER").Value.ToString() & "</td>")
                Tabela1.Append("        <td>" & Rst1.Fields("LINE").Value.ToString() & "</td>")
                Tabela1.Append("        <td>" & Rst1.Fields("POD").Value.ToString() & "</td>")
                Tabela1.Append("        <td>" & Rst1.Fields("FDES").Value.ToString() & "</td>")
                Tabela1.Append("    </tbody>")
                Tabela1.Append("    <thead bgcolor=" & CorPadrao & ">")
                Tabela1.Append("        <td>NAVIO / VIAGEM</td>")
                Tabela1.Append("        <td>DATA DEAD LINE</td>")
                Tabela1.Append("        <td>TARA (Kg)</td>")
                Tabela1.Append("        <td>PESO BRUTO (Kg)</td>")
                Tabela1.Append("        <td>VOLUMES</td>")
                Tabela1.Append("    </thead>")
                Tabela1.Append("    <tbody>")
                Tabela1.Append("        <td>" & Rst1.Fields("NAVIOVIAGEM").Value.ToString() & "</td>")
                Tabela1.Append("        <td>" & Rst1.Fields("DATA_DEAD_LINE").Value.ToString() & "</td>")
                Tabela1.Append("        <td>" & FormatNumber(Rst1.Fields("TARA").Value.ToString(), 3) & "</td>")
                Tabela1.Append("        <td>" & FormatNumber(Rst1.Fields("BRUTO").Value.ToString(), 3) & "</td>")
                Tabela1.Append("        <td>" & Rst1.Fields("VOLUMES").Value.ToString() & "</td>")
                Tabela1.Append("    </tbody>")
                Tabela1.Append("</table>")

                Umidade = ObterUmidade(Rst1.Fields("REFERENCE").Value.ToString(), Rst1.Fields("AUTONUMVIAGEM").Value.ToString())
                Ventilacao = ObterVentilacao(Rst1.Fields("REFERENCE").Value.ToString(), Rst1.Fields("AUTONUMVIAGEM").Value.ToString())

                Tabela1.Append("<table>")
                Tabela1.Append("<caption>Carga Perigosa / Refrigeração / Excessos (cm) </caption>")
                Tabela1.Append("    <thead bgcolor=" & CorPadrao & ">")
                Tabela1.Append("        <td>IMO1</td>")
                Tabela1.Append("        <td>IMO2</td>")
                Tabela1.Append("        <td>IMO3</td>")
                Tabela1.Append("        <td>IMO4</td>")
                Tabela1.Append("        <td>TEMPERATURA</td>")
                Tabela1.Append("        <td>ESCALA</td>")
                Tabela1.Append("        <td>ALTURA</td>")
                Tabela1.Append("        <td>LATERAL DIREITA</td>")
                Tabela1.Append("    </thead>")
                Tabela1.Append("    <tbody>")
                Tabela1.Append("        <td>" & Rst1.Fields("IMO1").Value.ToString() & "</td>")
                Tabela1.Append("        <td>" & Rst1.Fields("IMO2").Value.ToString() & "</td>")
                Tabela1.Append("        <td>" & Rst1.Fields("IMO3").Value.ToString() & "</td>")
                Tabela1.Append("        <td>" & Rst1.Fields("IMO4").Value.ToString() & "</td>")
                Tabela1.Append("        <td>" & Rst1.Fields("TEMPERATURA").Value.ToString() & "</td>")
                Tabela1.Append("        <td>" & Rst1.Fields("ESCALA").Value.ToString() & "</td>")
                Tabela1.Append("        <td>" & Rst1.Fields("OH").Value.ToString() & "</td>")
                Tabela1.Append("        <td>" & Rst1.Fields("OW").Value.ToString() & "</td>")
                Tabela1.Append("    </tbody>")
                Tabela1.Append("    <thead bgcolor=" & CorPadrao & ">")
                Tabela1.Append("        <td>ONU1</td>")
                Tabela1.Append("        <td>ONU2</td>")
                Tabela1.Append("        <td>ONU3</td>")
                Tabela1.Append("        <td>ONU4</td>")
                Tabela1.Append("        <td>UMIDADE</td>")
                Tabela1.Append("        <td>VENTILAÇÃO</td>")
                Tabela1.Append("        <td>LATERAL ESQUERDA</td>")
                Tabela1.Append("        <td>COMPRIMENTO</td>")
                Tabela1.Append("    </thead>")
                Tabela1.Append("    <tbody>")
                Tabela1.Append("        <td>" & Rst1.Fields("ONU1").Value.ToString() & "</td>")
                Tabela1.Append("        <td>" & Rst1.Fields("ONU2").Value.ToString() & "</td>")
                Tabela1.Append("        <td>" & Rst1.Fields("ONU3").Value.ToString() & "</td>")
                Tabela1.Append("        <td>" & Rst1.Fields("ONU4").Value.ToString() & "</td>")
                Tabela1.Append("        <td>" & Umidade & "</td>")
                Tabela1.Append("        <td>" & Ventilacao & "</td>")
                Tabela1.Append("        <td>" & Rst1.Fields("OWL").Value.ToString() & "</td>")
                Tabela1.Append("        <td>" & Rst1.Fields("OL").Value.ToString() & "</td>")
                Tabela1.Append("    </tbody>")
                Tabela1.Append("</table>")

                Tabela1.Append("<table>")
                Tabela1.Append("<caption>Lacres</caption>")
                Tabela1.Append("    <thead bgcolor=" & CorPadrao & ">")
                Tabela1.Append("        <td>LACRE ARMADOR 1</td>")
                Tabela1.Append("        <td>LACRE ARMADOR 2</td>")
                Tabela1.Append("        <td>LACRE EXPORTADOR</td>")
                Tabela1.Append("        <td>LACRE SIF</td>")
                Tabela1.Append("        <td colspan=4>OUTROS LACRES</td>")
                Tabela1.Append("    </thead>")
                Tabela1.Append("    <tbody>")
                Tabela1.Append("        <td>" & Rst1.Fields("LACRE1").Value.ToString() & "</td>")
                Tabela1.Append("        <td>" & Rst1.Fields("LACRE5").Value.ToString() & "</td>")
                Tabela1.Append("        <td>" & Rst1.Fields("LACRE6").Value.ToString() & "</td>")
                Tabela1.Append("        <td>" & Rst1.Fields("LACRE_SIF").Value.ToString() & "</td>")
                Tabela1.Append("        <td>" & Rst1.Fields("LACRE2").Value.ToString() & "</td>")
                Tabela1.Append("        <td>" & Rst1.Fields("LACRE3").Value.ToString() & "</td>")
                Tabela1.Append("        <td>" & Rst1.Fields("LACRE4").Value.ToString() & "</td>")
                Tabela1.Append("        <td>" & Rst1.Fields("LACRE7").Value.ToString() & "</td>")
                Tabela1.Append("    </tbody>")
                Tabela1.Append("</table>")

                If Rst2.State = 1 Then
                    Rst2.Close()
                End If

                If Banco.BancoEmUso = "ORACLE" Then
                    Rst2.Open(String.Format("SELECT autonum,autonumviagem, REFERENCE, numero, serie, data_emissao, cfop, valor_ipi,valor_icms, valor_total, peso_total, quantidade_emb,autonum_exportador, operador.tb_nota_fiscal.rowid FROM operador.tb_nota_fiscal WHERE autonum IN (SELECT n.autonum FROM operador.tb_gd_conteiner c,operador.tb_nota_fiscal n,operador.tb_nota_fiscal_cntr cn WHERE c.autonumviagem = n.autonumviagem AND c.id_conteiner = cn.id_conteiner AND n.autonum = cn.autonumnf AND c.num_protocolo = {0} AND c.ano_protocolo = {1})", Rst1.Fields("NUM_PROTOCOLO").Value, Rst1.Fields("ANO_PROTOCOLO").Value), Banco.Conexao, 3, 3)
                Else
                    Rst2.Open(String.Format("SELECT AUTONUM,AUTONUMVIAGEM,REFERENCE,NUMERO,SERIE,DATA_EMISSAO,CFOP,VALOR_IPI,VALOR_ICMS,VALOR_TOTAL,PESO_TOTAL,QUANTIDADE_EMB,AUTONUM_EXPORTADOR FROM OPERADOR.DBO.TB_NOTA_FISCAL WHERE autonum IN (select n.autonum FROM operador.dbo.tb_gd_conteiner c,operador.tb_nota_fiscal n,operador.tb_nota_fiscal_cntr cn WHERE (c.autonumviagem = n.autonumviagem) AND c.id_conteiner = cn.id_conteiner AND n.autonum = cn.autonumnf AND c.NUM_PROTOCOLO + c.ANO_PROTOCOLO IN({0}))", Item), Banco.Conexao, 3, 3)
                End If

                If Not Rst2.EOF Then
                    Tabela1.Append("<td style=""font-family:Arial;font-size:11px;font-weight: bold;padding-top:8px;text-align:left;border:0px;margin:10px;""><tr><td>Notas Fiscais do Contêiner: " & Rst1.Fields("ID_CONTEINER").Value.ToString() & "</td>")
                End If


                While Not Rst2.EOF

                    Tabela1.Append("<table>")
                    Tabela1.Append("    <thead bgcolor=" & CorPadrao & ">")
                    Tabela1.Append("        <td>Reserva</td>")
                    Tabela1.Append("        <td>Número</td>")
                    Tabela1.Append("        <td>Série</td>")
                    Tabela1.Append("        <td>Emissão</td>")
                    Tabela1.Append("        <td>CFOP</td>")
                    Tabela1.Append("        <td>IPI</td>")
                    Tabela1.Append("        <td>ICMS</td>")
                    Tabela1.Append("        <td>Valor Total</td>")
                    Tabela1.Append("        <td>Peso Total</td>")
                    Tabela1.Append("        <td>Qtd. Embalagem</td>")
                    Tabela1.Append("    </thead>")

                    Tabela1.Append("    <tbody>")
                    Tabela1.Append("        <td>" & Rst2.Fields("REFERENCE").Value.ToString() & "</td>")
                    Tabela1.Append("        <td>" & Rst2.Fields("NUMERO").Value.ToString() & "</td>")
                    Tabela1.Append("        <td>" & Rst2.Fields("SERIE").Value.ToString() & "</td>")
                    Tabela1.Append("        <td>" & Rst2.Fields("DATA_EMISSAO").Value.ToString() & "</td>")
                    Tabela1.Append("        <td>" & Rst2.Fields("CFOP").Value.ToString() & "</td>")
                    Tabela1.Append("        <td>" & Rst2.Fields("VALOR_IPI").Value.ToString() & "</td>")
                    Tabela1.Append("        <td>" & Rst2.Fields("VALOR_ICMS").Value.ToString() & "</td>")
                    Tabela1.Append("        <td>" & Rst2.Fields("VALOR_TOTAL").Value.ToString() & "</td>")
                    Tabela1.Append("        <td>" & Rst2.Fields("PESO_TOTAL").Value.ToString() & "</td>")
                    Tabela1.Append("        <td>" & Rst2.Fields("QUANTIDADE_EMB").Value.ToString() & "</td>")


                    If RstItens.State = 1 Then
                        RstItens.Close()
                    End If

                    RstItens.Open("SELECT A.NCM,NVL(A.PESO,0) PESO,B.DESCR AS EMBALAGEM,A.QUANTIDADE_EMB FROM TB_NOTA_FISCAL_ITEM A LEFT JOIN DTE_TB_EMBALAGENS B ON A.EMBALAGEM = B.CODE WHERE AUTONUMNF = " & Rst2.Fields("AUTONUM").Value.ToString() & "", Banco.Conexao, 3, 3)

                    While Not RstItens.EOF
                        Tabela1.Append("    <thead bgcolor=" & CorPadrao & ">")
                        Tabela1.Append("        <td colspan=6></td>")
                        Tabela1.Append("        <td>NCM</td>")
                        Tabela1.Append("        <td>Peso</td>")
                        Tabela1.Append("        <td>Embalagem</td>")
                        Tabela1.Append("        <td>Qtd. Embalagem</td>")
                        Tabela1.Append("    </thead>")
                        Tabela1.Append("    <tbody>")
                        Tabela1.Append("        <td colspan=6></td>")
                        Tabela1.Append("        <td>" & RstItens.Fields("NCM").Value.ToString() & "</td>")
                        Tabela1.Append("        <td>" & FormatNumber(RstItens.Fields("PESO").Value.ToString(), 2) & "</td>")
                        Tabela1.Append("        <td>" & RstItens.Fields("EMBALAGEM").Value.ToString() & "</td>")
                        Tabela1.Append("        <td>" & RstItens.Fields("QUANTIDADE_EMB").Value.ToString() & "</td>")
                        Tabela1.Append("    </tbody>")

                        RstItens.MoveNext()

                    End While

                    Tabela1.Append("</table>")

                    Tabela1.Append("    </tbody>")
                    Tabela1.Append("</table>")

                    Rst2.MoveNext()

                    Tabela1.Append("</table>")
                    Tabela1.Append("<br />")

                End While



                Tabela1.Append("<table>")
                Tabela1.Append("    <thead bgcolor=" & CorPadrao & ">")
                Tabela1.Append("        <td width=50%>EXPORTADOR</td>")
                Tabela1.Append("        <td width=50%>OBSERVAÇÕES</td>")
                Tabela1.Append("    </thead>")
                Tabela1.Append("    <tbody>")
                Tabela1.Append("        <td>" & Rst1.Fields("EXPORTADOR").Value.ToString() & "</td>")
                Tabela1.Append("        <td>" & Rst1.Fields("OBS").Value.ToString() & "</td>")
                Tabela1.Append("    </tbody>")
                Tabela1.Append("</table>")
                Tabela1.Append("<br />")


                Tabela1.Append("<br />")


                Tabela1.Append("<table>")
                Tabela1.Append("    <thead bgcolor=" & CorPadrao & ">")
                Tabela1.Append("        <td></td>")
                Tabela1.Append("        <td>CHEGADA NO TERMINAL</td>")
                Tabela1.Append("        <td>SAÍDA DO TERMINAL</td>")
                Tabela1.Append("    </thead>")
                Tabela1.Append("    <tbody>")
                Tabela1.Append("    <td align=""center""><img src=""CodigoBarra.aspx?protocolo=" & Rst1.Fields("NUM_PROTOCOLO").Value.ToString() & "/" & Rst1.Fields("ANO_PROTOCOLO").Value.ToString() & "&modo=E"" /></td> ")
                Tabela1.Append("        <td class=assinatura><br/> ")
                Tabela1.Append("            ___/___/___ ___:___:___ ")
                Tabela1.Append("    <br/> <br/> <br/> <br/> ")
                Tabela1.Append("            _______________________ ")
                Tabela1.Append("        </td>")
                Tabela1.Append("        <td class=assinatura><br/> ")
                Tabela1.Append("            ___/___/___ ___:___:___ ")
                Tabela1.Append("    <br/> <br/> <br/> <br/> ")
                Tabela1.Append("            _______________________ ")
                Tabela1.Append("        </td>")
                Tabela1.Append("    </tbody>")
                Tabela1.Append("</table>")
                Tabela1.Append("<br />")


            End If

            Estrutura.Append(Header.ToString())
            Estrutura.Append(Tabela1.ToString())
            Estrutura.Append(Tabela2.ToString())
            Estrutura.Append("<div class=folha></div>")

            Header.Clear()
            Tabela1.Clear()
            Tabela2.Clear()
            SQL.Clear()

            AlteraStatusImpressao(Item)

        Next

        Return Estrutura

    End Function

    Public Function ObterReservaPorConteiner(ByVal Conteiner As String) As String

        Dim Rst As New ADODB.Recordset

        If Banco.BancoEmUso = "ORACLE" Then
            Rst.Open(String.Format("SELECT REFERENCE FROM OPERADOR.TB_GD_CONTEINER WHERE ID_CONTEINER='{0}'", Conteiner), Banco.Conexao, 3, 3)
        Else
            Rst.Open(String.Format("SELECT REFERENCE FROM OPERADOR.DBO.TB_GD_CONTEINER WHERE ID_CONTEINER='{0}'", Conteiner), Banco.Conexao, 3, 3)
        End If

        If Not Rst.EOF Then
            Return Rst.Fields("REFERENCE").Value.ToString()
        End If

        Return String.Empty

    End Function

    Public Function ConteinerIsLate(ByVal Reserva As String, ByVal Conteiner As String) As Boolean

        Dim Rst As New ADODB.Recordset

        If Banco.BancoEmUso = "ORACLE" Then
            Rst.Open(String.Format("SELECT NVL(FLAG_LATE,0) FLAG_LATE FROM OPERADOR.TB_GD_CONTEINER WHERE REFERENCE='{0}' AND ID_CONTEINER='{1}'", Reserva, Conteiner), Banco.Conexao, 3, 3)
        Else
            Rst.Open(String.Format("SELECT NVL(FLAG_LATE,0) FLAG_LATE FROM OPERADOR.DBO.TB_GD_CONTEINER WHERE REFERENCE='{0}' AND ID_CONTEINER='{1}'", Reserva, Conteiner), Banco.Conexao, 3, 3)
        End If

        If Not Rst.EOF Then
            Return Rst.Fields("FLAG_LATE").Value.ToString()
        End If

        Return False

    End Function

    Public Function DesassociarConteiner(ByVal Conteiner As String) As Boolean

        Dim Rst As New ADODB.Recordset

        If Banco.BancoEmUso = "ORACLE" Then
            Rst.Open(String.Format("UPDATE OPERADOR.TB_GD_CONTEINER SET AUTONUM_GD_RESERVA=0, NUM_PROTOCOLO=0, ANO_PROTOCOLO=0, AUTONUM_GD_MOTORISTA=0,AUTONUM_VEICULO=0,USRID=0 ,STATUS = '' WHERE AUTONUM_GD_CNTR={0}", Conteiner), Banco.Conexao, 3, 3)
        Else
            Rst.Open(String.Format("UPDATE OPERADOR.DBO.TB_GD_CONTEINER SET AUTONUM_GD_RESERVA=0, NUM_PROTOCOLO=0, ANO_PROTOCOLO=0, AUTONUM_GD_MOTORISTA=0,AUTONUM_VEICULO=0,USRID=0 ,STATUS = '' WHERE AUTONUM_GD_CNTR={0}", Conteiner), Banco.Conexao, 3, 3)
        End If

        Return True

    End Function

    Public Function ConteinerCadastrado(ByVal ID_Conteiner As String, ByVal Viagem As String, ByVal ID As String) As Boolean

        Dim Rst As New ADODB.Recordset

        If String.IsNullOrEmpty(ID) Then
            ID = "0"
        End If

        If Banco.BancoEmUso = "ORACLE" Then
            Rst.Open(String.Format("SELECT AUTONUM_GD_CNTR FROM OPERADOR.TB_GD_CONTEINER WHERE ID_CONTEINER='{0}' AND AUTONUMVIAGEM={1} AND AUTONUM_GD_CNTR<>{2}", ID_Conteiner, Viagem, ID), Banco.Conexao, 3, 3)
        Else
            Rst.Open(String.Format("SELECT AUTONUM_GD_CNTR FROM OPERADOR.DBO.TB_GD_CONTEINER WHERE ID_CONTEINER='{0}' AND AUTONUMVIAGEM={1} AND AUTONUM_GD_CNTR<>{2}", ID_Conteiner, Viagem, ID), Banco.Conexao, 3, 3)
        End If

        If Not Rst.EOF Then
            Return True
        End If

        Return False

    End Function

End Class


