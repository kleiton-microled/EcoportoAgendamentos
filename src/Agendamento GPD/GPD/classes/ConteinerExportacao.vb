Imports System.Data.OleDb

Public Class ConteinerExportacao

    Private _TipoConteiner As String
    Private _CodigoTipoConteiner As String
    Private _Tamanho As String

    Public Property Tamanho() As String
        Get
            Return _Tamanho
        End Get
        Set(ByVal value As String)
            _Tamanho = value
        End Set
    End Property

    Public Function ConsultarTipos(ByVal Reserva As String, ByVal Viagem As String, ByVal Tamanho As String) As List(Of Conteiner)

        Dim Rst As New ADODB.Recordset
        Dim SQL As New StringBuilder
        Dim Lista As New List(Of Conteiner)

        If Banco.BancoEmUso = "ORACLE" Then
            SQL.Append("SELECT ")
            SQL.Append("    C.CODIGO, ")
            SQL.Append("    C.DESCRICAO ")
            SQL.Append("FROM ")
            SQL.Append("    OPERADOR.VW_BOOKING_WEB A ")
            SQL.Append("LEFT JOIN ")
            SQL.Append("    OPERADOR.TB_CAD_TIPO_CONTEINER C ON A.TIPOBASICO = C.CODIGO ")
            SQL.Append("WHERE ")
            SQL.Append("    A.REFERENCE = '{0}' ")
            SQL.Append("AND ")
            SQL.Append("    A.AUTONUMVIAGEM = {1} ")
            SQL.Append("AND ")
            SQL.Append("    A.TAMANHO = {2} ")
            SQL.Append("GROUP BY ")
            SQL.Append("    C.CODIGO, ")
            SQL.Append("    C.DESCRICAO ")
        Else
            SQL.Append("SELECT ")
            SQL.Append("    C.CODIGO, ")
            SQL.Append("    C.DESCRICAO ")
            SQL.Append("FROM ")
            SQL.Append("    OPERADOR.DBO.VW_BOOKING_WEB A ")
            SQL.Append("LEFT JOIN ")
            SQL.Append("    OPERADOR.DBO.TB_CAD_TIPO_CONTEINER C ON A.TIPOBASICO = C.CODIGO ")
            SQL.Append("WHERE ")
            SQL.Append("    A.REFERENCE = '{0}' ")
            SQL.Append("AND ")
            SQL.Append("    A.AUTONUMVIAGEM =  {1} ")
            SQL.Append("AND ")
            SQL.Append("    A.TAMANHO = {2} ")
            SQL.Append("GROUP BY ")
            SQL.Append("    C.CODIGO, ")
            SQL.Append("    C.DESCRICAO ")
        End If

        Rst.Open(String.Format(SQL.ToString(), Reserva, Viagem, Tamanho), Banco.Conexao, 3, 3)

        While Not Rst.EOF
            Lista.Add(New Conteiner(Rst.Fields("CODIGO").Value.ToString(), Rst.Fields("DESCRICAO").Value.ToString(), ""))
            Rst.MoveNext()
        End While

        Return Lista

    End Function

    Public Function ConsultarTamanhos(ByVal Reserva As String, ByVal Viagem As String) As List(Of Conteiner)

        Dim Rst As New ADODB.Recordset
        Dim SQL As New StringBuilder
        Dim Lista As New List(Of Conteiner)

        If Banco.BancoEmUso = "ORACLE" Then
            SQL.Append("SELECT ")
            SQL.Append("    DISTINCT ")
            SQL.Append("        TAMANHO ")
            SQL.Append("FROM ")
            SQL.Append("    OPERADOR.VW_BOOKING_WEB ")
            SQL.Append("WHERE ")
            SQL.Append("    REFERENCE='{0}' ")
            SQL.Append("AND ")
            SQL.Append("    AUTONUMVIAGEM={1} ")
        Else
            SQL.Append("SELECT ")
            SQL.Append("    DISTINCT ")
            SQL.Append("        TAMANHO ")
            SQL.Append("FROM ")
            SQL.Append("    OPERADOR.DBO.VW_BOOKING_WEB ")
            SQL.Append("WHERE ")
            SQL.Append("    REFERENCE='{0}' ")
            SQL.Append("AND ")
            SQL.Append("    AUTONUMVIAGEM={1} ")
        End If

        Rst.Open(String.Format(SQL.ToString(), Reserva, Viagem), Banco.Conexao, 3, 3)

        While Not Rst.EOF
            Lista.Add(New Conteiner("", "", Rst.Fields("TAMANHO").Value.ToString()))
            Rst.MoveNext()
        End While

        Return Lista

    End Function

    Public Function Consultar(ByVal ID As String, Optional ByVal Filtro As String = "") As String

        Dim Rst As New ADODB.Recordset
        Dim SQL As New StringBuilder

        If Banco.BancoEmUso = "ORACLE" Then
            SQL.Append("SELECT ")
            SQL.Append("    AUTONUM_GD_CNTR, ")
            SQL.Append("    REFERENCE, ")
            SQL.Append("    ID_CONTEINER, ")
            SQL.Append("    TIPO_TRANSPORTE ")
            SQL.Append("FROM ")
            SQL.Append("    OPERADOR.TB_GD_CONTEINER ")
            SQL.Append("WHERE ")
            SQL.Append("    AUTONUM_TRANSPORTADORA={0} ")
            SQL.Append("AND ")
            SQL.Append("   AUTONUMVIAGEM IN (SELECT AUTONUM FROM OPERADOR.TB_VIAGENS WHERE OPERANDO=1)  ")
            SQL.Append("AND ")
            SQL.Append("    NUM_PROTOCOLO=0 ")
            SQL.Append(Filtro)
            SQL.Append(" ORDER BY ")
            SQL.Append("    ID_CONTEINER")
        Else
            SQL.Append("SELECT ")
            SQL.Append("    AUTONUM_GD_CNTR, ")
            SQL.Append("    TAMANHO, ")
            SQL.Append("    ID_CONTEINER, ")
            SQL.Append("    TIPO_TRANSPORTE ")
            SQL.Append("FROM ")
            SQL.Append("    OPERADOR.DBO.TB_GD_CONTEINER ")
            SQL.Append("WHERE ")
            SQL.Append("    AUTONUM_TRANSPORTADORA={0} ")
            SQL.Append("AND ")
            SQL.Append("   AUTONUMVIAGEM IN (SELECT AUTONUM FROM OPERADOR.DBO.TB_VIAGENS WHERE OPERANDO=1)  ")
            SQL.Append("AND ")
            SQL.Append("    NUM_PROTOCOLO=0 ")
            SQL.Append(Filtro)
            SQL.Append(" ORDER BY ")
            SQL.Append("    ID_CONTEINER")
        End If

        Return String.Format(SQL.ToString(), ID)

    End Function

    Public Function ConsultarConteineres(ByVal ID As String) As DataTable

        Dim Rst As New ADODB.Recordset
        Dim SQL As New StringBuilder

        If Banco.BancoEmUso = "ORACLE" Then
            SQL.Append("SELECT ")
            SQL.Append("    AUTONUM_GD_CNTR, ")
            SQL.Append("    REFERENCE, ")
            SQL.Append("    ID_CONTEINER ")
            SQL.Append("FROM ")
            SQL.Append("    OPERADOR.TB_GD_CONTEINER ")
            SQL.Append("WHERE ")
            SQL.Append("    AUTONUM_TRANSPORTADORA={0} ")
            SQL.Append("AND ")
            SQL.Append("   AUTONUMVIAGEM IN (SELECT AUTONUM FROM OPERADOR.TB_VIAGENS WHERE OPERANDO=1)  ")
            SQL.Append("AND ")
            SQL.Append("    NUM_PROTOCOLO=0 ")
            SQL.Append(" ORDER BY ")
            SQL.Append("    ID_CONTEINER")
        Else
            SQL.Append("SELECT ")
            SQL.Append("    AUTONUM_GD_CNTR, ")
            SQL.Append("    TAMANHO, ")
            SQL.Append("    ID_CONTEINER ")
            SQL.Append("FROM ")
            SQL.Append("    OPERADOR.DBO.TB_GD_CONTEINER ")
            SQL.Append("WHERE ")
            SQL.Append("    AUTONUM_TRANSPORTADORA={0} ")
            SQL.Append("AND ")
            SQL.Append("   AUTONUMVIAGEM IN (SELECT AUTONUM FROM OPERADOR.DBO.TB_VIAGENS WHERE OPERANDO=1)  ")
            SQL.Append("AND ")
            SQL.Append("    NUM_PROTOCOLO=0 ")
            SQL.Append(" ORDER BY ")
            SQL.Append("    ID_CONTEINER")
        End If

        Rst.Open(String.Format(SQL.ToString(), ID), Banco.Conexao, 3, 3)

        Using Adapter As New OleDbDataAdapter
            Dim Ds As New DataSet
            Adapter.Fill(Ds, Rst, "TB_GD_CONTEINER")
            Return Ds.Tables(0)
        End Using

    End Function

    Public Function HabilitarEdicaoExclusao(ByVal ID As String) As Boolean

        Dim Rst As New ADODB.Recordset
        Dim SQL As New StringBuilder

        If Banco.BancoEmUso = "ORACLE" Then
            SQL.Append("SELECT ")
            SQL.Append("    NVL(NUM_PROTOCOLO,0) NUM_PROTOCOLO ")
            SQL.Append("FROM ")
            SQL.Append("    OPERADOR.TB_GD_CONTEINER ")
            SQL.Append("WHERE ")
            SQL.Append("    AUTONUM_GD_CNTR={0} ")
        Else
            SQL.Append("SELECT ")
            SQL.Append("    NVL(NUM_PROTOCOLO,0) NUM_PROTOCOLO ")
            SQL.Append("FROM ")
            SQL.Append("    OPERADOR.DBO.TB_GD_CONTEINER ")
            SQL.Append("WHERE ")
            SQL.Append("    AUTONUM_GD_CNTR={0} ")
        End If

        Rst.Open(String.Format(SQL.ToString(), ID), Banco.Conexao, 3, 3)

        If Not Rst.EOF Then
            If Convert.ToInt32(Rst.Fields("NUM_PROTOCOLO").Value.ToString()) = 0 Then
                Return True
            End If
        End If

        Return False

    End Function

    Public Function ValidaIMO(ByVal IMO As String) As Boolean

        Dim Rst As New ADODB.Recordset

        If Banco.BancoEmUso = "ORACLE" Then
            Rst.Open(String.Format("SELECT CODE FROM OPERADOR.TB_CAD_CARGA_PERIGOSA WHERE CODE = '{0}'", IMO), Banco.Conexao, 3, 3)
        Else
            Rst.Open(String.Format("SELECT CODE FROM OPERADOR.DBO.TB_CAD_CARGA_PERIGOSA WHERE CODE = '{0}'", IMO), Banco.Conexao, 3, 3)
        End If

        If Not Rst.EOF Then
            Return True
        End If

        Return False

    End Function

    Public Function ValidaONU(ByVal ONU As String) As Boolean

        Dim Rst As New ADODB.Recordset

        If Banco.BancoEmUso = "ORACLE" Then
            Rst.Open(String.Format("SELECT CODE FROM OPERADOR.TB_CAD_ONU WHERE CODE = '{0}'", ONU), Banco.Conexao, 3, 3)
        Else
            Rst.Open(String.Format("SELECT CODE FROM OPERADOR.DBO.TB_CAD_ONU WHERE CODE = '{0}'", ONU), Banco.Conexao, 3, 3)
        End If

        If Not Rst.EOF Then
            Return True
        End If

        Return False

    End Function

    Public Function ValidarTipoBooking(ByVal Reserva As String) As List(Of String)

        Dim Rst As New ADODB.Recordset
        Dim Lista As New List(Of String)

        If Banco.BancoEmUso = "ORACLE" Then
            Rst.Open(String.Format("SELECT TIPOBASICO FROM OPERADOR.TB_BOOKING WHERE REFERENCE = '{0}'", Reserva), Banco.Conexao, 3, 3)
        Else
            Rst.Open(String.Format("SELECT TIPOBASICO FROM OPERADOR.DBO.TB_BOOKING WHERE REFERENCE = '{0}'", Reserva), Banco.Conexao, 3, 3)
        End If

        While Not Rst.EOF
            Lista.Add(Rst.Fields("TIPOBASICO").Value.ToString())
            Rst.MoveNext()
        End While

        Return Lista

    End Function

    Public Function ConsultarTiposConteiner() As DataTable

        Dim Rst As New ADODB.Recordset
        Dim Lista As New List(Of String)

        If Banco.BancoEmUso = "ORACLE" Then
            Rst.Open("SELECT DISTINCT(TIPOBASICO) FROM OPERADOR.TB_GD_CONTEINER", Banco.Conexao, 3, 3)
        Else
            Rst.Open("SELECT DISTINCT(TIPOBASICO) FROM OPERADOR.DBO.TB_GD_CONTEINER", Banco.Conexao, 3, 3)
        End If

        Using Adapter As New OleDbDataAdapter
            Dim Ds As New DataSet
            Adapter.Fill(Ds, Rst, "TB_GD_CONTEINER")
            Return Ds.Tables(0)
        End Using

    End Function

    Public Function ValidarTamanho20Booking(ByVal Reserva As String, ByVal Viagem As String, ByVal Tipo As String, ByVal IDBooking As Integer) As Integer

        Dim Rst As New ADODB.Recordset
        Dim SQL As New StringBuilder

        If Banco.BancoEmUso = "ORACLE" Then

            SQL.Append("SELECT ")
            SQL.Append("    NVL(QUANT20,0) QUANT20 ")
            SQL.Append("FROM ")
            SQL.Append("    OPERADOR.VW_PRACA_ABERTA ")
            SQL.Append("WHERE ")
            SQL.Append("    DT_INICIO_PRACA IS NOT NULL AND (DT_FIM_PRACA>SYSDATE OR DT_FIM_PRACA IS NULL) ")
            SQL.Append("        AND RESERVA = '" & Reserva & "' AND AUTONUM_VIAGEM = " & Viagem & " AND TIPOBASICO='" & Tipo & "' ")

            If IDBooking <> 0 Then
                SQL.Append(" AND AUTONUM_BOOKING=" & IDBooking & " ")
            End If

            SQL.Append("ORDER BY NVL(DT_FIM_PRACA,TO_DATE('01/01/2100')) DESC")

        Else

            SQL.Append("SELECT ")
            SQL.Append("    ISNULL(QUANT20,0) QUANT20 ")
            SQL.Append("FROM ")
            SQL.Append("    OPERADOR.DBO.VW_PRACA_ABERTA ")
            SQL.Append("WHERE ")
            SQL.Append("    DT_INICIO_PRACA IS NOT NULL AND (DT_FIM_PRACA>GETDATE() OR DT_FIM_PRACA IS NULL) ")
            SQL.Append("        AND RESERVA = '" & Reserva & "' AND AUTONUM_VIAGEM = " & Viagem & " AND TIPOBASICO='" & Tipo & "' ")

            If Not String.IsNullOrEmpty(IDBooking) Then
                SQL.Append(" AND AUTONUM_BOOKING=" & IDBooking & " ")
            End If

            SQL.Append("ORDER BY ISNULL(DT_FIM_PRACA,OPERADOR.TO_DATE('01/01/2100')) DESC")

        End If

        Rst.Open(SQL.ToString(), Banco.Conexao, 3, 3)

        If Not Rst.EOF Then
            Return Rst.Fields("QUANT20").Value.ToString()
        End If

        Return False

    End Function

    Public Function ValidarTamanho40Booking(ByVal Reserva As String, ByVal Viagem As String, ByVal Tipo As String, ByVal IDBooking As String) As Integer

        Dim Rst As New ADODB.Recordset
        Dim SQL As New StringBuilder

        If Banco.BancoEmUso = "ORACLE" Then

            SQL.Append("SELECT ")
            SQL.Append("    NVL(QUANT40,0) QUANT40 ")
            SQL.Append("FROM ")
            SQL.Append("    OPERADOR.VW_PRACA_ABERTA ")
            SQL.Append("WHERE ")
            SQL.Append("    DT_INICIO_PRACA IS NOT NULL AND (DT_FIM_PRACA>SYSDATE OR DT_FIM_PRACA IS NULL) ")
            SQL.Append("        AND RESERVA = '" & Reserva & "' AND AUTONUM_VIAGEM = " & Viagem & " AND TIPOBASICO='" & Tipo & "' ")

            If IDBooking <> 0 Then
                SQL.Append(" AND AUTONUM_BOOKING=" & IDBooking & " ")
            End If

            SQL.Append("ORDER BY NVL(DT_FIM_PRACA,TO_DATE('01/01/2100')) DESC")

        Else

            SQL.Append("SELECT ")
            SQL.Append("    ISNULL(QUANT40,0) QUANT40 ")
            SQL.Append("FROM ")
            SQL.Append("    OPERADOR.DBO.VW_PRACA_ABERTA ")
            SQL.Append("WHERE ")
            SQL.Append("    DT_INICIO_PRACA IS NOT NULL AND (DT_FIM_PRACA>GETDATE() OR DT_FIM_PRACA IS NULL) ")
            SQL.Append("        AND RESERVA = '" & Reserva & "' AND AUTONUM_VIAGEM = " & Viagem & " AND TIPOBASICO='" & Tipo & "' ")

            If Not String.IsNullOrEmpty(IDBooking) Then
                SQL.Append(" AND AUTONUM_BOOKING=" & IDBooking & " ")
            End If

            SQL.Append("ORDER BY ISNULL(DT_FIM_PRACA,OPERADOR.TO_DATE('01/01/2100')) DESC")

        End If

        Rst.Open(SQL.ToString(), Banco.Conexao, 3, 3)

        If Not Rst.EOF Then
            Return Rst.Fields("QUANT40").Value.ToString()
        End If

        Return False

    End Function
    
    Public Function ConsultarQuantidadeAgendado(ByVal Reserva As String, ByVal Viagem As String, ByVal ID_Conteiner As String, ByVal Tipo As String, ByVal Tamanho As String, ByVal IDBooking As Integer) As Integer

        Dim Rst As New ADODB.Recordset
        Dim SQL As New StringBuilder

        If Banco.BancoEmUso = "ORACLE" Then
            SQL.Append("SELECT ")
            SQL.Append("    COUNT(*) AS QTD ")
            SQL.Append("FROM ")
            SQL.Append("    OPERADOR.TB_GD_CONTEINER A ")
            SQL.Append("WHERE ")
            SQL.Append("    A.REFERENCE='{0}' ")
            SQL.Append("AND ")
            SQL.Append("    A.AUTONUMVIAGEM={1} ")
            SQL.Append("AND ")
            SQL.Append("    A.ID_CONTEINER <> '{2}' ")
            SQL.Append("AND ")
            SQL.Append("    A.TIPOBASICO='{3}' ")
            SQL.Append("AND ")
            SQL.Append("    A.TAMANHO = {4} ")
            SQL.Append("AND ")
            SQL.Append("    A.AUTONUM_RESERVA = {5} ")
        Else
            SQL.Append("SELECT ")
            SQL.Append("    COUNT(*) AS QTD ")
            SQL.Append("FROM ")
            SQL.Append("    OPERADOR.TB_GD_CONTEINER A ")
            SQL.Append("WHERE ")
            SQL.Append("    A.REFERENCE='{0}' ")
            SQL.Append("AND ")
            SQL.Append("    A.AUTONUMVIAGEM={1} ")
            SQL.Append("AND ")
            SQL.Append("    A.ID_CONTEINER <> '{2}' ")
            SQL.Append("AND ")
            SQL.Append("    A.TIPOBASICO='{3}' ")
            SQL.Append("AND ")
            SQL.Append("    A.TAMANHO = {4} ")
        End If

        Rst.Open(String.Format(SQL.ToString(), Reserva, Viagem, ID_Conteiner, Tipo, Tamanho, IDBooking), Banco.Conexao, 3, 3)

        If Not Rst.EOF Then
            Return Rst.Fields("QTD").Value.ToString()
        End If

        Return False

    End Function

    Public Function Inserir(ByVal Reserva As Reserva, ByVal UserId As String) As Boolean

        Dim Rst As New ADODB.Recordset
        Dim SQL As New StringBuilder
        Dim Ferr As Boolean

        If Banco.BancoEmUso = "ORACLE" Then
            SQL.Append("INSERT INTO OPERADOR.TB_GD_CONTEINER ")
            SQL.Append("    ( ")
            SQL.Append("        AUTONUM_GD_CNTR, ")
            SQL.Append("        AUTONUM_TRANSPORTADORA, ")
            SQL.Append("        AUTONUMVIAGEM, ")
            SQL.Append("        REFERENCE, ")
            SQL.Append("        ID_CONTEINER, ")
            SQL.Append("        TARA, ")
            SQL.Append("        BRUTO, ")
            SQL.Append("        LACRE1, ")
            SQL.Append("        LACRE2, ")
            SQL.Append("        LACRE3, ")
            SQL.Append("        LACRE4, ")
            SQL.Append("        LACRE5, ")
            SQL.Append("        LACRE6, ")
            SQL.Append("        LACRE7, ")
            SQL.Append("        LACRE_SIF, ")
            SQL.Append("        VENTILACAO, ")
            SQL.Append("        UMIDADE, ")
            SQL.Append("        VOLUMES, ")
            SQL.Append("        TEMPERATURA, ")
            SQL.Append("        ESCALA, ")
            SQL.Append("        OH, ")
            SQL.Append("        OW, ")
            SQL.Append("        OWL, ")
            SQL.Append("        OL, ")
            SQL.Append("        EF, ")
            SQL.Append("        TAMANHO, ")
            SQL.Append("        TIPOBASICO, ")
            SQL.Append("        FLAG_ATIVO, ")
            SQL.Append("        VAGAO, ")
            SQL.Append("        TIPO_TRANSPORTE, ")
            SQL.Append("        AUTONUM_RESERVA, ")
            SQL.Append("        IMO1, ")
            SQL.Append("        IMO2, ")
            SQL.Append("        IMO3, ")
            SQL.Append("        IMO4, ")
            SQL.Append("        ONU1, ")
            SQL.Append("        ONU2, ")
            SQL.Append("        ONU3, ")
            SQL.Append("        ONU4, ")
            SQL.Append("        POD, ")
            SQL.Append("        FLAG_LATE, ")
            SQL.Append("        OBS ")
            If Reserva.Transporte = "F' THEN" Then
                SQL.Append("        NUM_PROTOCOLO ")
                SQL.Append("        ANO_PROTOCOLO ")
                SQL.Append("        USRID ")
            End If
            SQL.Append("    ) ")
            SQL.Append("VALUES ")
            SQL.Append("    ( ")
            SQL.Append("     OPERADOR.SEQ_GD_CONTEINER.NEXTVAL,  ")
            SQL.Append("     " & Reserva.Transportadora.ID & ",  ")
            SQL.Append("     " & Reserva.Autonum_viagem & ",  ")
            SQL.Append("     '" & Reserva.Reserva & "',  ")
            SQL.Append("     '" & Reserva.Sigla & "',  ")
            SQL.Append("     " & Reserva.Tara & ",  ")
            SQL.Append("     " & Reserva.PesoBruto & ",  ")
            SQL.Append("     '" & Reserva.Lacre1 & "',  ")
            SQL.Append("     '" & Reserva.Lacre2 & "',  ")
            SQL.Append("     '" & Reserva.Lacre3 & "',  ")
            SQL.Append("     '" & Reserva.Lacre4 & "',  ")
            SQL.Append("     '" & Reserva.Lacre5 & "',  ")
            SQL.Append("     '" & Reserva.Lacre6 & "',  ")
            SQL.Append("     '" & Reserva.Lacre7 & "',  ")
            SQL.Append("     '" & Reserva.LacreSIF & "',  ")
            SQL.Append("     '" & Reserva.Ventilacao & "',  ")
            SQL.Append("     '" & Reserva.Umidade & "',  ")
            SQL.Append("     " & Reserva.Volumes & ",  ")
            SQL.Append("     '" & Reserva.Temperatura & "',  ")
            SQL.Append("     '" & Reserva.Escala & "',  ")
            SQL.Append("     " & Reserva.Overheight & ",  ")
            SQL.Append("     " & Reserva.Overwidth & ",  ")
            SQL.Append("     " & Reserva.Overwidthl & ",  ")
            SQL.Append("     " & Reserva.Overlength & ",  ")
            SQL.Append("     '" & Reserva.EF & "',  ")
            SQL.Append("     " & Reserva.Tamanho & ",  ")
            SQL.Append("     '" & Reserva.Tipo & "',  ")
            SQL.Append("     1,  ")
            SQL.Append("     '" & Reserva.Vagao & "',  ")
            SQL.Append("     '" & Reserva.Transporte & "',  ")
            SQL.Append("     " & Reserva.Autonum_reserva & ",  ")
            SQL.Append("     '" & Reserva.Imo1 & "',  ")
            SQL.Append("     '" & Reserva.Imo2 & "',  ")
            SQL.Append("     '" & Reserva.Imo3 & "',  ")
            SQL.Append("     '" & Reserva.Imo4 & "',  ")
            SQL.Append("     '" & Reserva.Un1 & "',  ")
            SQL.Append("     '" & Reserva.Un2 & "',  ")
            SQL.Append("     '" & Reserva.Un3 & "',  ")
            SQL.Append("     '" & Reserva.Un4 & "',  ")
            SQL.Append("     '" & Reserva.POD & "',  ")
            SQL.Append("     '" & Reserva.Late & "',  ")
            SQL.Append("     '" & Reserva.Obs & "'  ")
            If Reserva.Transporte = "F' THEN" Then
                SQL.Append(" SEQ_GD_PROT_" & Now.Year & ".NEXTVAL, ")
                SQL.Append("  " & Now.Year & " ,")
                SQL.Append("  " & USERID & "")
            End If
            SQL.Append("    ) ")
        Else
            SQL.Append("INSERT INTO OPERADOR.DBO.TB_GD_CONTEINER ")
            SQL.Append("    ( ")
            SQL.Append("        AUTONUM_TRANSPORTADORA, ")
            SQL.Append("        AUTONUMVIAGEM, ")
            SQL.Append("        REFERENCE, ")
            SQL.Append("        ID_CONTEINER, ")
            SQL.Append("        TARA, ")
            SQL.Append("        BRUTO, ")
            SQL.Append("        LACRE1, ")
            SQL.Append("        LACRE2, ")
            SQL.Append("        LACRE3, ")
            SQL.Append("        LACRE4, ")
            SQL.Append("        LACRE5, ")
            SQL.Append("        LACRE6, ")
            SQL.Append("        LACRE7, ")
            SQL.Append("        LACRE_SIF, ")
            SQL.Append("        VENTILACAO, ")
            SQL.Append("        UMIDADE, ")
            SQL.Append("        VOLUMES, ")
            SQL.Append("        TEMPERATURA, ")
            SQL.Append("        ESCALA, ")
            SQL.Append("        OH, ")
            SQL.Append("        OW, ")
            SQL.Append("        OWL, ")
            SQL.Append("        OL, ")
            SQL.Append("        EF, ")
            SQL.Append("        TAMANHO, ")
            SQL.Append("        TIPOBASICO, ")
            SQL.Append("        FLAG_ATIVO, ")
            SQL.Append("        VAGAO, ")
            SQL.Append("        TIPO_TRANSPORTE, ")
            SQL.Append("        AUTONUM_RESERVA, ")
            SQL.Append("        IMO1, ")
            SQL.Append("        IMO2, ")
            SQL.Append("        IMO3, ")
            SQL.Append("        IMO4, ")
            SQL.Append("        ONU1, ")
            SQL.Append("        ONU2, ")
            SQL.Append("        ONU3, ")
            SQL.Append("        ONU4, ")
            SQL.Append("        POD, ")
            SQL.Append("        FLAG_LATE, ")
            SQL.Append("        OBS ")
            SQL.Append("    ) ")
            SQL.Append("VALUES ")
            SQL.Append("    ( ")
            SQL.Append("     " & Reserva.Transportadora.ID & ",  ")
            SQL.Append("     " & Reserva.Autonum_viagem & ",  ")
            SQL.Append("     '" & Reserva.Reserva & "',  ")
            SQL.Append("     '" & Reserva.Sigla & "',  ")
            SQL.Append("     " & Reserva.Tara & ",  ")
            SQL.Append("     " & Reserva.PesoBruto & ",  ")
            SQL.Append("     '" & Reserva.Lacre1 & "',  ")
            SQL.Append("     '" & Reserva.Lacre2 & "',  ")
            SQL.Append("     '" & Reserva.Lacre3 & "',  ")
            SQL.Append("     '" & Reserva.Lacre4 & "',  ")
            SQL.Append("     '" & Reserva.Lacre5 & "',  ")
            SQL.Append("     '" & Reserva.Lacre6 & "',  ")
            SQL.Append("     '" & Reserva.Lacre7 & "',  ")
            SQL.Append("     '" & Reserva.LacreSIF & "',  ")
            SQL.Append("     '" & Reserva.Ventilacao & "',  ")
            SQL.Append("     '" & Reserva.Umidade & "',  ")
            SQL.Append("     " & Reserva.Volumes & ",  ")
            SQL.Append("     '" & Reserva.Temperatura & "',  ")
            SQL.Append("     '" & Reserva.Escala & "',  ")
            SQL.Append("     " & Reserva.Overheight & ",  ")
            SQL.Append("     " & Reserva.Overwidth & ",  ")
            SQL.Append("     " & Reserva.Overwidthl & ",  ")
            SQL.Append("     " & Reserva.Overlength & ",  ")
            SQL.Append("     '" & Reserva.EF & "',  ")
            SQL.Append("     " & Reserva.Tamanho & ",  ")
            SQL.Append("     '" & Reserva.Tipo & "',  ")
            SQL.Append("     1,  ")
            SQL.Append("     '" & Reserva.Vagao & "',  ")
            SQL.Append("     '" & Reserva.Transporte & "',  ")
            SQL.Append("     " & Reserva.Autonum_reserva & ",  ")
            SQL.Append("     '" & Reserva.Imo1 & "',  ")
            SQL.Append("     '" & Reserva.Imo2 & "',  ")
            SQL.Append("     '" & Reserva.Imo3 & "',  ")
            SQL.Append("     '" & Reserva.Imo4 & "',  ")
            SQL.Append("     '" & Reserva.Un1 & "',  ")
            SQL.Append("     '" & Reserva.Un2 & "',  ")
            SQL.Append("     '" & Reserva.Un3 & "',  ")
            SQL.Append("     '" & Reserva.Un4 & "',  ")
            SQL.Append("     '" & Reserva.POD & "',  ")
            SQL.Append("     '" & Reserva.Late & "',  ")
            SQL.Append("     '" & Reserva.Obs & "'  ")
            SQL.Append("    ) ")
        End If

        Try
            Rst.Open(SQL.ToString(), Banco.Conexao, 3, 3)
            Return True
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try

  

        Return False

    End Function

    Public Function VerificarConteinerExistente(ByVal Autonum_Viagem As String, ByVal ID_Conteiner As String) As Boolean

        Dim Rst As New ADODB.Recordset

        If Banco.BancoEmUso = "ORACLE" Then
            Rst.Open(String.Format("SELECT AUTONUM_GD_CNTR FROM OPERADOR.TB_GD_CONTEINER WHERE AUTONUMVIAGEM={0} AND ID_CONTEINER='{1}' AND FLAG_ATIVO=1 AND AUTONUMVIAGEM<>4286", Autonum_Viagem, ID_Conteiner), Banco.Conexao, 3, 3)
        Else
            Rst.Open(String.Format("SELECT AUTONUM_GD_CNTR FROM OPERADOR.DBO.TB_GD_CONTEINER WHERE AUTONUMVIAGEM={0} AND ID_CONTEINER='{1}' AND FLAG_ATIVO=1 AND AUTONUMVIAGEM<>4286", Autonum_Viagem, ID_Conteiner), Banco.Conexao, 3, 3)
        End If

        If Not Rst.EOF Then
            Return True
        End If

        Return False

    End Function

    Public Function VerificarConteinerDepositado(ByVal Autonum_Viagem As String, ByVal ID_Conteiner As String) As Boolean

        Dim Rst As New ADODB.Recordset

        If Banco.BancoEmUso = "ORACLE" Then
            Rst.Open(String.Format("SELECT AUTONUM FROM OPERADOR.TB_PATIO WHERE AUTONUMVIAGEM={0} AND ID_CONTEINER='{1}' AND IMPEXP='E' AND DATA_ENT_TEMP IS NOT NULL", Autonum_Viagem, ID_Conteiner), Banco.Conexao, 3, 3)
        Else
            Rst.Open(String.Format("SELECT AUTONUM FROM OPERADOR.DBO.TB_PATIO WHERE AUTONUMVIAGEM={0} AND ID_CONTEINER='{1}' AND IMPEXP='E' AND DATA_ENT_TEMP IS NOT NULL", Autonum_Viagem, ID_Conteiner), Banco.Conexao, 3, 3)
        End If

        If Not Rst.EOF Then
            Return True
        End If

        Return False

    End Function

    Public Function Alterar(ByVal Reserva As Reserva) As Boolean

        Dim Rst As New ADODB.Recordset
        Dim SQL As New StringBuilder

        If Banco.BancoEmUso = "ORACLE" Then
            SQL.Append("UPDATE OPERADOR.TB_GD_CONTEINER ")
            SQL.Append("    SET ")
            SQL.Append("        AUTONUMVIAGEM=" & Reserva.Autonum_viagem & ", ")
            SQL.Append("        REFERENCE='" & Reserva.Reserva & "', ")
            SQL.Append("        ID_CONTEINER='" & Reserva.Sigla & "', ")
            SQL.Append("        TARA=" & Reserva.Tara & ", ")
            SQL.Append("        BRUTO=" & Reserva.PesoBruto & ", ")
            SQL.Append("        LACRE1='" & Reserva.Lacre1 & "', ")
            SQL.Append("        LACRE2='" & Reserva.Lacre2 & "', ")
            SQL.Append("        LACRE3='" & Reserva.Lacre3 & "', ")
            SQL.Append("        LACRE4='" & Reserva.Lacre4 & "', ")
            SQL.Append("        LACRE5='" & Reserva.Lacre5 & "', ")
            SQL.Append("        LACRE6='" & Reserva.Lacre6 & "', ")
            SQL.Append("        LACRE7='" & Reserva.Lacre7 & "', ")
            SQL.Append("        LACRE_SIF='" & Reserva.LacreSIF & "', ")
            SQL.Append("        VENTILACAO='" & Reserva.Ventilacao & "', ")
            SQL.Append("        UMIDADE='" & Reserva.Umidade & "', ")
            SQL.Append("        VOLUMES=" & Reserva.Volumes & ", ")
            SQL.Append("        ESCALA='" & Reserva.Escala & "', ")
            SQL.Append("        OH=" & Reserva.Overheight & ", ")
            SQL.Append("        OW=" & Reserva.Overwidth & ", ")
            SQL.Append("        OWL=" & Reserva.Overwidthl & ", ")
            SQL.Append("        OL=" & Reserva.Overlength & ", ")
            SQL.Append("        EF='" & Reserva.EF & "', ")
            SQL.Append("        TAMANHO=" & Reserva.Tamanho & ", ")
            SQL.Append("        TIPOBASICO='" & Reserva.Tipo & "', ")
            SQL.Append("        VAGAO='" & Reserva.Vagao & "', ")
            SQL.Append("        TIPO_TRANSPORTE='" & Reserva.Transporte & "', ")
            SQL.Append("        AUTONUM_RESERVA= " & Reserva.Autonum_reserva & ", ")
            SQL.Append("        IMO1='" & Reserva.Imo1 & "', ")
            SQL.Append("        IMO2='" & Reserva.Imo2 & "', ")
            SQL.Append("        IMO3='" & Reserva.Imo3 & "', ")
            SQL.Append("        IMO4='" & Reserva.Imo4 & "', ")
            SQL.Append("        ONU1='" & Reserva.Un1 & "', ")
            SQL.Append("        ONU2='" & Reserva.Un2 & "', ")
            SQL.Append("        ONU3='" & Reserva.Un3 & "', ")
            SQL.Append("        ONU4='" & Reserva.Un4 & "', ")
            SQL.Append("        POD='" & Reserva.POD & "', ")
            SQL.Append("        OBS='" & Reserva.Obs & "' ")

            Dim Protocolo As Integer = ObterNumeroProtocolo(Reserva.Codigo)

            If Protocolo <> 0 Then               
                SQL.Append("  ,NUM_PROTOCOLO=SEQ_GD_PROT_" & Now.Year & ".NEXTVAL,")
                SQL.Append("   ANO_PROTOCOLO=" & Now.Year & " ")
            End If

            SQL.Append("    WHERE ")
            SQL.Append("        AUTONUM_GD_CNTR=" & Reserva.Codigo & " ")

        Else
            SQL.Append("UPDATE OPERADOR.DBO.TB_GD_CONTEINER ")
            SQL.Append("    SET ")
            SQL.Append("        AUTONUMVIAGEM=" & Reserva.Autonum_viagem & ", ")
            SQL.Append("        REFERENCE='" & Reserva.Reserva & "', ")
            SQL.Append("        ID_CONTEINER='" & Reserva.Sigla & "', ")
            SQL.Append("        TARA=" & Reserva.Tara & ", ")
            SQL.Append("        BRUTO=" & Reserva.PesoBruto & ", ")
            SQL.Append("        LACRE1='" & Reserva.Lacre1 & "', ")
            SQL.Append("        LACRE2='" & Reserva.Lacre2 & "', ")
            SQL.Append("        LACRE3='" & Reserva.Lacre3 & "', ")
            SQL.Append("        LACRE4='" & Reserva.Lacre4 & "', ")
            SQL.Append("        LACRE5='" & Reserva.Lacre5 & "', ")
            SQL.Append("        LACRE6='" & Reserva.Lacre6 & "', ")
            SQL.Append("        LACRE7='" & Reserva.Lacre7 & "', ")
            SQL.Append("        LACRE_SIF='" & Reserva.LacreSIF & "', ")
            SQL.Append("        VENTILACAO='" & Reserva.Ventilacao & "', ")
            SQL.Append("        UMIDADE='" & Reserva.Umidade & "', ")
            SQL.Append("        VOLUMES=" & Reserva.Volumes & ", ")
            SQL.Append("        ESCALA='" & Reserva.Escala & "', ")
            SQL.Append("        OH=" & Reserva.Overheight & ", ")
            SQL.Append("        OW=" & Reserva.Overwidth & ", ")
            SQL.Append("        OWL=" & Reserva.Overwidthl & ", ")
            SQL.Append("        OL=" & Reserva.Overlength & ", ")
            SQL.Append("        EF='" & Reserva.EF & "', ")
            SQL.Append("        TAMANHO=" & Reserva.Tamanho & ", ")
            SQL.Append("        TIPOBASICO='" & Reserva.Tipo & "', ")
            SQL.Append("        VAGAO='" & Reserva.Vagao & "', ")
            SQL.Append("        TIPO_TRANSPORTE='" & Reserva.Transporte & "', ")
            SQL.Append("        AUTONUM_RESERVA= " & Reserva.Autonum_reserva & ", ")
            SQL.Append("        IMO1='" & Reserva.Imo1 & "', ")
            SQL.Append("        IMO2='" & Reserva.Imo2 & "', ")
            SQL.Append("        IMO3='" & Reserva.Imo3 & "', ")
            SQL.Append("        IMO4='" & Reserva.Imo4 & "', ")
            SQL.Append("        ONU1='" & Reserva.Un1 & "', ")
            SQL.Append("        ONU2='" & Reserva.Un2 & "', ")
            SQL.Append("        ONU3='" & Reserva.Un3 & "', ")
            SQL.Append("        ONU4='" & Reserva.Un4 & "', ")
            SQL.Append("        POD='" & Reserva.POD & "', ")
            SQL.Append("        OBS='" & Reserva.Obs & "' ")

            Dim Protocolo As Integer = ObterNumeroProtocolo(Reserva.Codigo)

            If Protocolo <> 0 Then
                SQL.Append("  ,NUM_PROTOCOLO=SEQ_GD_PROT_" & Now.Year & ".NEXTVAL,")
                SQL.Append("   ANO_PROTOCOLO=" & Now.Year & " ")
            End If

            SQL.Append("    WHERE ")
            SQL.Append("        AUTONUM_GD_CNTR=" & Reserva.Codigo & " ")

        End If

        Try
            Rst.Open(SQL.ToString(), Banco.Conexao, 3, 3)
            Return True
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try

        Return False

    End Function

    Public Function Excluir(ByVal ID As String) As Boolean

        Dim Rst As New ADODB.Recordset
        Dim SQL As New StringBuilder

        If Banco.BancoEmUso = "ORACLE" Then
            SQL.Append("DELETE FROM OPERADOR.TB_GD_CONTEINER ")
            SQL.Append("    WHERE ")
            SQL.Append("        AUTONUM_GD_CNTR={0}")
        Else
            SQL.Append("DELETE FROM OPERADOR.DBO.TB_GD_CONTEINER ")
            SQL.Append("    WHERE ")
            SQL.Append("        AUTONUM_GD_CNTR={0}")
        End If

        Try
            Rst.Open(String.Format(SQL.ToString(), ID), Banco.Conexao, 3, 3)
            Return True
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try

        Return False

    End Function

    Public Function ObterNumeroProtocolo(ByVal ID As String) As Integer

        Dim Rst As New ADODB.Recordset

        If Banco.BancoEmUso = "ORACLE" Then
            Rst.Open(String.Format("SELECT NVL(NUM_PROTOCOLO,0) NUM_PROTOCOLO FROM OPERADOR.TB_GD_CONTEINER WHERE AUTONUM_GD_CNTR={0}", ID), Banco.Conexao, 3, 3)
        Else
            Rst.Open(String.Format("SELECT ISNULL(NUM_PROTOCOLO,0) NUM_PROTOCOLO FROM OPERADOR.DBO.TB_GD_CONTEINER WHERE AUTONUM_GD_CNTR={0}", ID), Banco.Conexao, 3, 3)
        End If

        If Not Rst.EOF Then
            Return Convert.ToInt32(Rst.Fields("NUM_PROTOCOLO").Value.ToString())
        End If

        Return False

    End Function

End Class
