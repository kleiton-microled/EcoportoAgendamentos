Imports System.Data.OleDb

Public Class AgRetiradaTRA

    Private _Autonum As Integer
    Private _Transportadora As Transportadora
    Private _Motorista As Motorista
    Private _Veiculo As Veiculo
    Private _Viagem As Viagem
    Private _IdPeriodo As Integer

    Public Property Autonum() As Integer
        Get
            Return _Autonum
        End Get
        Set(value As Integer)
            _Autonum = value
        End Set
    End Property

    Public Property Transportadora() As Transportadora
        Get
            Return Me._Transportadora
        End Get
        Set(value As Transportadora)
            Me._Transportadora = value
        End Set
    End Property

    Public Property Motorista() As Motorista
        Get
            Return Me._Motorista
        End Get
        Set(value As Motorista)
            Me._Motorista = value
        End Set
    End Property

    Public Property Veiculo() As Veiculo
        Get
            Return Me._Veiculo
        End Get
        Set(value As Veiculo)
            Me._Veiculo = value
        End Set
    End Property

    Public Property Viagem() As Viagem
        Get
            Return Me._Viagem
        End Get
        Set(value As Viagem)
            Me._Viagem = value
        End Set
    End Property

    Public Property IdPeriodo() As Integer
        Get
            Return _IdPeriodo
        End Get
        Set(value As Integer)
            _IdPeriodo = value
        End Set
    End Property

    Dim Empresa As New Empresa
    Dim RecintoVinc As New Recinto

    ''' <summary>
    ''' Verifica se o agendamento pertence realmente à transportadora logada, por questões de segurança
    ''' </summary>
    ''' <param name="IdAgendamento">Autonum do Agendamento de Retirada</param>
    ''' <param name="IdTransp">Autonum da Transportadora Logada</param>
    ''' <returns>True se agendamento é da transportadora logada, false se não é</returns>
    ''' <remarks>IdTransp que não é número inteiro deve ser verificado antes de acessar o método</remarks>
    Public Function VerificarAgendTransp(ByVal IdAgendamento As String, ByVal IdTransp As String) As Boolean
        Dim SQL As New StringBuilder
        Dim Conex As New OleDbConnection
        Dim Comando As OleDbCommand
        Dim IdTranspRetir As Integer

        SQL.Append("SELECT ")
        SQL.Append("    AUTONUM_TRANSPORTADORA ")
        SQL.Append("FROM ")
        SQL.Append("    TB_AG_RETIRADA ")
        SQL.Append("WHERE ")
        SQL.Append("    AUTONUM = '" & IdAgendamento & "' ")

        Conex.ConnectionString = Banco.StringConexao(True)
        Conex.Open()
        Comando = New OleDbCommand(SQL.ToString(), Conex)
        IdTranspRetir = Convert.ToInt32(Comando.ExecuteScalar())

        If IdTranspRetir = Convert.ToInt32(IdTransp) Then
            Return True
        Else
            Return False
        End If

    End Function

    Public Function Agendar(ByVal IdTransp As String, ByVal IdMotorista As String, ByVal IdVeiculo As String, ByVal IdViagem As String, ByVal IdPeriodo As String) As Integer
        Dim SQL As New StringBuilder
        Dim Conex As New OleDbConnection
        Dim Comando As OleDbCommand
        Dim LinhasAfetadas As Integer

        Dim IdAgRetirada As Integer = 0
        Dim IdNumProt As Integer = 0

        If Banco.BancoEmUso = "SQLSERVER" Then
            SQL.Append("SELECT MAX(AUTONUM) + 1 FROM OPERADOR.DBO.TB_AG_RETIRADA ")
            Conex.ConnectionString = Banco.StringConexao(True)
            Conex.Open()
            Comando = New OleDbCommand(SQL.ToString(), Conex)
            IdAgRetirada = Comando.ExecuteScalar()

            Conex.Close()
            Comando = Nothing
            SQL.Clear()

            SQL.Append("SELECT MAX(NUM_PROTOCOLO) + 1 FROM OPERADOR.DBO.TB_AG_RETIRADA ")
            Conex.ConnectionString = Banco.StringConexao(True)
            Conex.Open()
            Comando = New OleDbCommand(SQL.ToString(), Conex)
            IdNumProt = Comando.ExecuteScalar()

            Conex.Close()
            Comando = Nothing
            SQL.Clear()
        End If

        Try
            SQL.Append("INSERT INTO ")
            If Banco.BancoEmUso = "ORACLE" Then
                SQL.Append("OPERADOR.TB_AG_RETIRADA ")
            Else
                SQL.Append("OPERADOR.DBO.TB_AG_RETIRADA ")
            End If
            SQL.Append("(AUTONUM, AUTONUM_TRANSPORTADORA, AUTONUM_MOTORISTA, AUTONUM_VEICULO, AUTONUM_VIAGEM, AUTONUM_GD_RESERVA, NUM_PROTOCOLO, ANO_PROTOCOLO, DATA_CADASTRO, IMPRESSO) ")
            SQL.Append("VALUES (")
            If Banco.BancoEmUso = "ORACLE" Then
                SQL.Append("OPERADOR.SEQ_AG_RETIRADA.NEXTVAL")
            Else
                SQL.Append(IdAgRetirada)
            End If
            SQL.Append(", " & IdTransp & ", " & IdMotorista & ", " & IdVeiculo & ", " & IdViagem & ", " & IdPeriodo & ", ")
            If Banco.BancoEmUso = "ORACLE" Then
                SQL.Append("OPERADOR.SEQ_AG_RETIR_PROT_" & Now.Year().ToString() & ".NEXTVAL")
            Else
                SQL.Append(IdNumProt)
            End If
            SQL.Append(", " & Now.Year().ToString() & ", SYSDATE, 0)")

            Conex.ConnectionString = Banco.StringConexao(True)
            Conex.Open()
            Comando = New OleDbCommand(SQL.ToString(), Conex)
            LinhasAfetadas = Comando.ExecuteNonQuery()
            Conex.Close()
            Return LinhasAfetadas

        Catch ex As Exception
            Return -1
        End Try

    End Function

    Public Function AlterarAgendamento(ByVal Autonum As String, ByVal IdTransp As String, ByVal IdMotorista As String, ByVal IdVeiculo As String, ByVal IdViagem As String, ByVal IdPeriodo As String) As Integer
        Dim SQL As New StringBuilder
        Dim Conex As New OleDbConnection
        Dim Comando As OleDbCommand
        Dim LinhasAfetadas As Integer = 0

        Dim IdNumProt As Integer = 0

        If Banco.BancoEmUso = "SQLSERVER" Then
            SQL.Append("SELECT MAX(NUM_PROTOCOLO) + 1 FROM OPERADOR.DBO.TB_AG_RETIRADA ")
            Conex.ConnectionString = Banco.StringConexao(True)
            Conex.Open()
            Comando = New OleDbCommand(SQL.ToString(), Conex)
            IdNumProt = Comando.ExecuteScalar()

            Conex.Close()
            Comando = Nothing
            SQL.Clear()
        End If

        Try
            SQL.Append("UPDATE ")
            If Banco.BancoEmUso = "ORACLE" Then
                SQL.Append("OPERADOR.TB_AG_RETIRADA ")
            Else
                SQL.Append("OPERADOR.DBO.TB_AG_RETIRADA ")
            End If
            SQL.Append(" SET ")
            SQL.Append("    AUTONUM_TRANSPORTADORA = " & IdTransp & ", ")
            SQL.Append("    AUTONUM_MOTORISTA = " & IdMotorista & ", ")
            SQL.Append("    AUTONUM_VEICULO = " & IdVeiculo & ", ")
            SQL.Append("    AUTONUM_VIAGEM = " & IdViagem & ", ")
            SQL.Append("    AUTONUM_GD_RESERVA = " & IdPeriodo & ", ")
            SQL.Append("    NUM_PROTOCOLO = ")
            If Banco.BancoEmUso = "ORACLE" Then
                SQL.Append("OPERADOR.SEQ_AG_RETIR_PROT_" & Now.Year().ToString & ".NEXTVAL, ")
            Else
                SQL.Append(IdNumProt & ", ")
            End If
            SQL.Append("    ANO_PROTOCOLO = " & Now.Year().ToString() & " ")
            SQL.Append("WHERE ")
            SQL.Append("    AUTONUM = " & Autonum & " ")

            Conex.ConnectionString = Banco.StringConexao(True)
            Conex.Open()
            Comando = New OleDbCommand(SQL.ToString(), Conex)
            LinhasAfetadas = Comando.ExecuteNonQuery()
            Conex.Close()
            Return LinhasAfetadas

        Catch ex As Exception
            Return -1
        End Try

    End Function

    Public Function AlterarStatusImpressao(ByVal Protocolo As String) As Integer
        Dim SQL As New StringBuilder
        Dim Conex As New OleDbConnection
        Dim Comando As New OleDbCommand
        Dim LinAfet As Integer

        SQL.Append("UPDATE ")
        If Banco.BancoEmUso = "ORACLE" Then
            SQL.Append("OPERADOR.TB_AG_RETIRADA ")
        Else
            SQL.Append("OPERADOR.DBO.TB_AG_RETIRADA ")
        End If
        SQL.Append("SET ")
        SQL.Append("    IMPRESSO = 1 ")
        SQL.Append("WHERE ")
        SQL.Append("    CONCAT(NUM_PROTOCOLO, ANO_PROTOCOLO) = '" & Protocolo & "'")

        Try
            Conex.ConnectionString = Banco.StringConexao(True)
            Conex.Open()
            Comando = New OleDbCommand(SQL.ToString(), Conex)
            LinAfet = Comando.ExecuteNonQuery()
            Conex.Close()
        Catch ex As Exception
            LinAfet = -1
        End Try

        Return LinAfet

    End Function


    Public Function ExcluirAgendamento(ByVal idAgend As String) As Integer
        Dim SQL As New StringBuilder
        Dim Con As New OleDbConnection
        Dim Comando As OleDbCommand
        Dim Afetadas As Integer

        SQL.Append("DELETE FROM ")
        If Banco.BancoEmUso = "ORACLE" Then
            SQL.Append("OPERADOR.TB_AG_RETIRADA ")
        Else
            SQL.Append("OPERADOR.DBO.TB_AG_RETIRADA ")
        End If
        SQL.Append("WHERE ")
        SQL.Append("    AUTONUM = " & idAgend)
        Try
            Con.ConnectionString = Banco.StringConexao(True)
            Con.Open()
            Comando = New OleDbCommand(SQL.ToString(), Con)
            Afetadas = Comando.ExecuteNonQuery()
            Con.Close()

            Return Afetadas
        Catch ex As Exception
            Return -1
        End Try

    End Function

    Public Function ConsultarDadosAgendamento(ByVal idAgend As String) As AgRetiradaTRA
        Dim SQL As New StringBuilder
        Dim Con As New OleDbConnection
        Dim Comando As OleDbCommand
        Dim DadosAgend As OleDbDataReader

        Dim AgendCarregar As New AgRetiradaTRA
        Transportadora = New Transportadora
        AgendCarregar.Transportadora = Transportadora
        Motorista = New Motorista
        AgendCarregar.Motorista = Motorista
        Veiculo = New Veiculo
        AgendCarregar.Veiculo = Veiculo
        Viagem = New Viagem
        AgendCarregar.Viagem = Viagem

        SQL.Append("SELECT ")
        SQL.Append("    AUTONUM_TRANSPORTADORA, AUTONUM_MOTORISTA, AUTONUM_VEICULO, AUTONUM_VIAGEM, AUTONUM_GD_RESERVA ")
        SQL.Append("FROM ")
        If Banco.BancoEmUso = "ORACLE" Then
            SQL.Append("OPERADOR.TB_AG_RETIRADA ")
        Else
            SQL.Append("OPERADOR.DBO.TB_AG_RETIRADA ")
        End If
        SQL.Append(" WHERE ")
        SQL.Append("AUTONUM = " & idAgend & " ")

        Con.ConnectionString = Banco.StringConexao(True)
        Con.Open()
        Comando = New OleDbCommand(SQL.ToString(), Con)
        DadosAgend = Comando.ExecuteReader()

        While DadosAgend.Read
            AgendCarregar.Transportadora.ID = Convert.ToInt32(DadosAgend("AUTONUM_TRANSPORTADORA").ToString())
            AgendCarregar.Motorista.ID = Convert.ToInt32(DadosAgend("AUTONUM_MOTORISTA").ToString())
            AgendCarregar.Veiculo.ID = Convert.ToInt32(DadosAgend("AUTONUM_VEICULO").ToString())
            AgendCarregar.Viagem.Autonumviagem = Convert.ToInt64(DadosAgend("AUTONUM_VIAGEM").ToString())
            AgendCarregar.IdPeriodo = Convert.ToInt32(DadosAgend("AUTONUM_GD_RESERVA").ToString())
        End While
        DadosAgend.Close()
        Con.Close()

        Return AgendCarregar

    End Function

    Public Function ConsAgendRetirada(ByVal IdTransp As String, Optional ByVal Filtro As String = "") As DataTable
        Dim Conexao As New OleDbConnection
        Dim SQL As New StringBuilder

        SQL.Append("SELECT ")
        SQL.Append("    DISTINCT ")
        SQL.Append("    RET.AUTONUM AS AUTONUM, ")
        SQL.Append("    RET.NUM_PROTOCOLO, RET.ANO_PROTOCOLO, ")
        SQL.Append("    RET.NUM_PROTOCOLO || '/' || RET.ANO_PROTOCOLO AS PROTOCOLO, ")
        SQL.Append("    M.NOME AS NOME, M.CNH AS CNH, ")
        SQL.Append("    V.PLACA_CAVALO || ' - ' || V.PLACA_CARRETA AS VEICULO, ")
        SQL.Append("    TO_CHAR(RES.PERIODO_INICIAL,'DD/MM/YYYY HH24:MI') || '<BR/>' || TO_CHAR(RES.PERIODO_FINAL,'DD/MM/YYYY HH24:MI') AS PERIODO, ")
        SQL.Append("    RG.NAVIO_VIAGEM AS NAVIO_VIAGEM, ")
        SQL.Append("    '1' AS PATIO, ")
        SQL.Append("    CASE WHEN RET.IMPRESSO = 0 THEN 'NÃO' ")
        SQL.Append("    ELSE CASE WHEN RET.IMPRESSO = 1 THEN 'SIM' END END AS IMP, ")
        SQL.Append("    RET.IMPRESSO AS IMPRESSO ")
        SQL.Append("FROM ")
        If Banco.BancoEmUso = "ORACLE" Then
            SQL.Append("    OPERADOR.TB_AG_RETIRADA RET ")
        Else
            SQL.Append("    OPERADOR.DBO.TB_AG_RETIRADA RET ")
        End If
        SQL.Append("LEFT JOIN ")
        If Banco.BancoEmUso = "ORACLE" Then
            SQL.Append("    OPERADOR.TB_AG_MOTORISTAS M ")
        Else
            SQL.Append("    OPERADOR.DBO.TB_AG_MOTORISTAS M ")
        End If
        SQL.Append("    ON RET.AUTONUM_MOTORISTA = M.AUTONUM ")
        SQL.Append("LEFT JOIN ")
        If Banco.BancoEmUso = "ORACLE" Then
            SQL.Append("    OPERADOR.TB_CAD_TRANSPORTADORAS T ")
        Else
            SQL.Append("    OPERADOR.DBO.TB_CAD_TRANSPORTADORAS T ")
        End If
        SQL.Append("    ON RET.AUTONUM_TRANSPORTADORA = T.AUTONUM ")
        SQL.Append("LEFT JOIN ")
        If Banco.BancoEmUso = "ORACLE" Then
            SQL.Append("    OPERADOR.TB_AG_VEICULOS V ")
        Else
            SQL.Append("    OPERADOR.DBO.TB_AG_VEICULOS V ")
        End If
        SQL.Append("    ON RET.AUTONUM_VEICULO = V.AUTONUM ")
        SQL.Append("LEFT JOIN ")
        If Banco.BancoEmUso = "ORACLE" Then
            SQL.Append("    OPERADOR.TB_GD_RESERVA RES ")
        Else
            SQL.Append("    OPERADOR.DBO.TB_GD_RESERVA RES ")
        End If
        SQL.Append("    ON RET.AUTONUM_GD_RESERVA = RES.AUTONUM_GD_RESERVA ")
        SQL.Append("LEFT JOIN ")
        If Banco.BancoEmUso = "ORACLE" Then
            SQL.Append("    OPERADOR.VW_AG_RETIRADA_GMCI RG ")
        Else
            SQL.Append("    OPERADOR.DBO.VW_AG_RETIRADA_GMCI RG ")
        End If
        SQL.Append("    ON RG.AUTONUMVIAGEM = RET.AUTONUM_VIAGEM ")
        SQL.Append("WHERE ")
        SQL.Append("    RET.AUTONUM_TRANSPORTADORA = 9447 ")
        SQL.Append("ORDER BY RG.NAVIO_VIAGEM, M.NOME ")

        Conexao.ConnectionString = Banco.StringConexao(True)
        Conexao.Open()

        Using Adaptador As New OleDbDataAdapter(SQL.ToString(), Conexao)
            Dim Ds As New DataSet
            Adaptador.Fill(Ds, "TB_AG_RETIRADA")
            Conexao.Close()
            Return Ds.Tables(0)
        End Using

    End Function

    ''' <summary>
    ''' Consulta Períodos disponíveis
    ''' </summary>
    ''' <param name="CodViagem">Autonum da Viagem</param>
    ''' <param name="CodEmpresa">Autonum da Transportadora</param>
    ''' <param name="CodPeriodo">Autonum do Período (opcional para Agendamentos a Editar)</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ConsultarPeriodos(ByVal CodViagem As String, ByVal CodEmpresa As String, Optional ByVal CodPeriodo As String = "0") As DataTable
        Dim SQL As New StringBuilder
        Dim PeriodoViagem As New Viagem
        Dim Conexao As New OleDbConnection

        PeriodoViagem = ObterPeriodosViagem(CodViagem)

        SQL.Append("SELECT ")
        SQL.Append("    GR.AUTONUM_GD_RESERVA, ")
        SQL.Append("    TO_CHAR(GR.PERIODO_INICIAL, 'DD/MM/YYYY HH24:MI') AS PERIODO_INICIAL, ")
        SQL.Append("    TO_CHAR(GR.PERIODO_FINAL, 'DD/MM/YYYY HH24:MI') AS PERIODO_FINAL, ")
        SQL.Append("    TO_CHAR(GR.LIMITE_MOVIMENTOS - (SELECT COUNT(AUTONUM) FROM OPERADOR.TB_AG_RETIRADA R ")
        SQL.Append("        WHERE R.AUTONUM_GD_RESERVA = GR.AUTONUM_GD_RESERVA), '000') AS SALDO ")
        SQL.Append("FROM ")
        If Banco.BancoEmUso = "ORACLE" Then
            SQL.Append("    OPERADOR.TB_GD_RESERVA GR ")
        Else
            SQL.Append("    OPERADOR.DBO.TB_GD_RESERVA GR ")
        End If
        SQL.Append("LEFT JOIN ")
        If Banco.BancoEmUso = "ORACLE" Then
            SQL.Append("    OPERADOR.TB_PATIOS P ")
        Else
            SQL.Append("    OPERADOR.DBO.TB_PATIOS P ")
        End If
        SQL.Append("ON P.AUTONUM = GR.PATIO ")
        SQL.Append("WHERE ")
        SQL.Append("( ")
        SQL.Append("    GR.SERVICO_GATE = 'T' ")
        SQL.Append("    AND ")
        SQL.Append("    P.AUTONUM_EMPRESA = " & CodEmpresa & " ")
        SQL.Append("    AND ")
        SQL.Append("    GR.LIMITE_MOVIMENTOS - (SELECT COUNT(AUTONUM) FROM OPERADOR.TB_AG_RETIRADA R ")
        SQL.Append("        WHERE R.AUTONUM_GD_RESERVA = GR.AUTONUM_GD_RESERVA) > 0 ")
        SQL.Append("    AND ")
        SQL.Append("    TO_CHAR(GR.PERIODO_INICIAL, 'YYYYMMDDHH24MI') >= ")
        SQL.Append("'" & Format(Convert.ToDateTime(PeriodoViagem.dtInicio), "yyyyMMddHHmm") & "' ")
        SQL.Append("    AND ")
        SQL.Append("    TO_CHAR(GR.PERIODO_FINAL, 'YYYYMMDDHH24MI') <= ")
        SQL.Append("'" & Format(Convert.ToDateTime(PeriodoViagem.dtFim), "yyyyMMddHHmm") & "' ")
        SQL.Append("     AND GR.PERIODO_INICIAL > SYSDATE")
        SQL.Append(") ")
        SQL.Append("    OR ")
        SQL.Append("    (GR.AUTONUM_GD_RESERVA = " & CodPeriodo & ")")
        SQL.Append("    ORDER BY ")
        SQL.Append("    GR.PERIODO_INICIAL ")

        Conexao.ConnectionString = Banco.StringConexao(True)
        Conexao.Open()

        Using Adaptador As New OleDbDataAdapter(SQL.ToString(), Conexao)
            Dim Ds As New DataSet
            Adaptador.Fill(Ds, "PERIODOS")
            Conexao.Close()
            Return Ds.Tables(0)
        End Using

    End Function

    Public Function ObterPeriodosViagem(ByVal CodViagem As String) As Viagem
        Dim SQL As New StringBuilder
        Dim PeriodoViagem As New Viagem
        Dim Conexao As New OleDbConnection
        Dim Comando As OleDbCommand
        Dim Periodos As OleDbDataReader

        SQL.Append("SELECT ")
        SQL.Append("    DISTINCT DT_INICIO, DT_FIM ")
        SQL.Append("FROM ")
        If Banco.BancoEmUso = "ORACLE" Then
            SQL.Append("    OPERADOR.VW_AG_RETIRADA_GMCI ")
        Else
            SQL.Append("    OPERADOR.DBO.VW_AG_RETIRADA_GMCI ")
        End If
        SQL.Append("WHERE ")
        SQL.Append("    AUTONUMVIAGEM = " & CodViagem & " ")

        Conexao.ConnectionString = Banco.StringConexao(True)
        Conexao.Open()
        Comando = New OleDbCommand(SQL.ToString(), Conexao)
        Periodos = Comando.ExecuteReader()

        While Periodos.Read
            PeriodoViagem.dtInicio = Periodos("DT_INICIO").ToString()
            PeriodoViagem.dtFim = Periodos("DT_FIM").ToString()
        End While
        Periodos.Close()
        Conexao.Close()

        Return PeriodoViagem

    End Function

    Public Function GerarProtocolo(ByVal ID As String, ByVal codEmpresa As String, ByVal CorPadrao As String, ByVal CodTransp As String) As StringBuilder
        Dim SQL As New StringBuilder
        Dim Cabecalho As New StringBuilder
        Dim DadosPrincipal As New StringBuilder
        Dim Recinto As New StringBuilder
        Dim Rodape As New StringBuilder
        Dim Estrutura As New StringBuilder
        Dim ListaRec As List(Of Recinto)

        Dim Protocolos As String() = ID.Split(",")

        'Já tem método que carrega recintos da transportadora:
        ListaRec = RecintoVinc.ConsultarRecintosAtivosTransp(CodTransp)

        For Each Item In Protocolos
            Dim CnxAgRet As New OleDbConnection
            Dim CmdAgRet As OleDbCommand
            Dim LeitorAgRet As OleDbDataReader

            If CnxAgRet.State = ConnectionState.Open Then
                CnxAgRet.Close()
                CnxAgRet = Nothing
            End If

            SQL.Append("SELECT ")
            SQL.Append("    DISTINCT ")
            If Banco.BancoEmUso = "ORACLE" Then
                SQL.Append("    RET.NUM_PROTOCOLO || '/' || RET.ANO_PROTOCOLO AS PROTOCOLO, ")
            Else
                SQL.Append("    RET.NUM_PROTOCOLO + '/' + RET.ANO_PROTOCOLO AS PROTOCOLO, ")
            End If
            SQL.Append("    RET.NUM_PROTOCOLO, RET.ANO_PROTOCOLO, ")
            SQL.Append("    M.NOME, M.CNH, ")
            SQL.Append("    M.RG, M.NEXTEL, ")
            SQL.Append("    V.PLACA_CAVALO, V.PLACA_CARRETA, ")
            SQL.Append("    V.MODELO, V.COR, ")
            SQL.Append("    V.TARA, V.CHASSI, ")
            If Banco.BancoEmUso = "ORACLE" Then
                SQL.Append("    TO_CHAR(RES.PERIODO_INICIAL, 'DD/MM/YYYY HH24:MI') || ' - ' || TO_CHAR(RES.PERIODO_FINAL, 'DD/MM/YYYY HH24:MI') AS PERIODO, ")
            Else
                SQL.Append("    TO_CHAR(RES.PERIODO_INICIAL, 'DD/MM/YYYY HH24:MI') + ' - ' + TO_CHAR(RES.PERIODO_FINAL, 'DD/MM/YYYY HH24:MI') AS PERIODO, ")
            End If
            SQL.Append("    T.RAZAO AS TRANSPORTADORA, ")
            SQL.Append("    RG.NAVIO_VIAGEM ")
            SQL.Append("FROM ")
            If Banco.BancoEmUso = "ORACLE" Then
                SQL.Append("    OPERADOR.TB_AG_RETIRADA RET ")
            Else
                SQL.Append("    OPERADOR.DBO.TB_AG_RETIRADA RET ")
            End If
            SQL.Append("LEFT JOIN ")
            If Banco.BancoEmUso = "ORACLE" Then
                SQL.Append("    OPERADOR.TB_AG_MOTORISTAS M ")
            Else
                SQL.Append("    OPERADOR.DBO.TB_AG_MOTORISTAS M ")
            End If
            SQL.Append("    ON M.AUTONUM = RET.AUTONUM_MOTORISTA ")
            SQL.Append("LEFT JOIN ")
            If Banco.BancoEmUso = "ORACLE" Then
                SQL.Append("    OPERADOR.TB_AG_VEICULOS V ")
            Else
                SQL.Append("    OPERADOR.DBO.TB_AG_VEICULOS V ")
            End If
            SQL.Append("    ON V.AUTONUM = RET.AUTONUM_VEICULO ")
            SQL.Append("LEFT JOIN ")
            If Banco.BancoEmUso = "ORACLE" Then
                SQL.Append("    OPERADOR.TB_GD_RESERVA RES ")
            Else
                SQL.Append("    OPERADOR.DBO.TB_GD_RESERVA RES ")
            End If
            SQL.Append("    ON RES.AUTONUM_GD_RESERVA = RET.AUTONUM_GD_RESERVA ")
            SQL.Append("LEFT JOIN ")
            If Banco.BancoEmUso = "ORACLE" Then
                SQL.Append("    OPERADOR.TB_CAD_TRANSPORTADORAS T ")
            Else
                SQL.Append("    OPERADOR.DBO.TB_CAD_TRANSPORTADORAS T ")
            End If
            SQL.Append("    ON T.AUTONUM = RET.AUTONUM_TRANSPORTADORA ")
            SQL.Append("LEFT JOIN ")
            If Banco.BancoEmUso = "ORACLE" Then
                SQL.Append("    OPERADOR.VW_AG_RETIRADA_GMCI RG ")
            Else
                SQL.Append("    OPERADOR.DBO.VW_AG_RETIRADA_GMCI RG ")
            End If
            SQL.Append("    ON RG.AUTONUMVIAGEM = RET.AUTONUM_VIAGEM ")
            SQL.Append("WHERE ")
            SQL.Append("    CONCAT(RET.NUM_PROTOCOLO, RET.ANO_PROTOCOLO) = '" & Item & "'")

            CnxAgRet.ConnectionString = Banco.StringConexao(True)
            CnxAgRet.Open()
            CmdAgRet = New OleDbCommand(SQL.ToString(), CnxAgRet)
            LeitorAgRet = CmdAgRet.ExecuteReader()
            SQL.Clear()

            While LeitorAgRet.Read
                'Começando a preencher o cabeçalho:
                Cabecalho.Append("<table id=cabecalho>")
                Cabecalho.Append("  <tr>")
                Cabecalho.Append("      <td align=left width=180px>")
                Cabecalho.Append("          <img src=css/" & Empresa.ObterNomeFantasiaDiret(codEmpresa) & "/images/LogoTop.png />")
                Cabecalho.Append("      </td>")
                Cabecalho.Append("      <td>")
                Cabecalho.Append("      <font face=Arial size=3>PROTOCOLO DE AGENDAMENTO DE RETIRADA DE CONTÊINER</font>")
                Cabecalho.Append("      <br/>")
                Cabecalho.Append("      <font face=Arial size=5>Nº: " & LeitorAgRet("NUM_PROTOCOLO").ToString() & "/" & LeitorAgRet("ANO_PROTOCOLO").ToString() & "</font> ")
                Cabecalho.Append("      <br/><br/>")
                Cabecalho.Append("      " & LeitorAgRet("PERIODO").ToString())
                Cabecalho.Append("      </td>")
                Cabecalho.Append("  </tr>")
                Cabecalho.Append("</table>")
                Cabecalho.Append("<br/>")

                'Preenchendo com os dados principais:
                DadosPrincipal.Append("<table>")
                DadosPrincipal.Append("<caption>Dados do Transporte</caption>")
                DadosPrincipal.Append(" <thead bgcolor=" & CorPadrao & ">")
                DadosPrincipal.Append("     <td colspan=6>TRANSPORTADORA</td>")
                DadosPrincipal.Append(" </thead>")
                DadosPrincipal.Append(" <tbody>")
                DadosPrincipal.Append("     <td colspan=6>" & LeitorAgRet("TRANSPORTADORA").ToString() & "</td>")
                DadosPrincipal.Append(" </tbody>")

                DadosPrincipal.Append(" <thead bgcolor=" & CorPadrao & ">")
                DadosPrincipal.Append("     <td>PLACA CAVALO</td>")
                DadosPrincipal.Append("     <td>PLACA CARRETA</td>")
                DadosPrincipal.Append("     <td>MODELO</td>")
                DadosPrincipal.Append("     <td>COR</td>")
                DadosPrincipal.Append("     <td>TARA</td>")
                DadosPrincipal.Append("     <td>CHASSI</td>")
                DadosPrincipal.Append(" </thead>")
                DadosPrincipal.Append(" <tbody>")
                DadosPrincipal.Append("     <td>" & LeitorAgRet("PLACA_CAVALO").ToString() & "</td>")
                DadosPrincipal.Append("     <td>" & LeitorAgRet("PLACA_CARRETA").ToString() & "</td>")
                DadosPrincipal.Append("     <td>" & LeitorAgRet("MODELO").ToString() & "</td>")
                DadosPrincipal.Append("     <td>" & LeitorAgRet("COR").ToString() & "</td>")
                DadosPrincipal.Append("     <td>" & LeitorAgRet("TARA").ToString() & "</td>")
                DadosPrincipal.Append("     <td>" & LeitorAgRet("CHASSI").ToString() & "</td>")
                DadosPrincipal.Append(" </tbody>")

                DadosPrincipal.Append(" <thead bgcolor=" & CorPadrao & ">")
                DadosPrincipal.Append("     <td colspan=3>MOTORISTA</td>")
                DadosPrincipal.Append("     <td>CNH</td>")
                DadosPrincipal.Append("     <td>RG</td>")
                DadosPrincipal.Append("     <td>NEXTEL</td>")
                DadosPrincipal.Append(" </thead>")
                DadosPrincipal.Append(" <tbody>")
                DadosPrincipal.Append("     <td colspan=3>" & LeitorAgRet("NOME").ToString() & "</td>")
                DadosPrincipal.Append("     <td>" & LeitorAgRet("CNH").ToString() & "</td>")
                DadosPrincipal.Append("     <td>" & LeitorAgRet("RG").ToString() & "</td>")
                DadosPrincipal.Append("     <td>" & LeitorAgRet("NEXTEL").ToString() & "</td>")
                DadosPrincipal.Append(" </tbody>")

                DadosPrincipal.Append(" <thead bgcolor=" & CorPadrao & ">")
                DadosPrincipal.Append("     <td colspan=6>NAVIO/VIAGEM</td>")
                DadosPrincipal.Append(" </thead>")
                DadosPrincipal.Append(" <tbody>")
                DadosPrincipal.Append("     <td colspan=6>" & LeitorAgRet("NAVIO_VIAGEM").ToString() & "</td>")
                DadosPrincipal.Append(" </tbody>")
                DadosPrincipal.Append("<table>")
                DadosPrincipal.Append("<br/><br/>")

                'Começando a parte dos recintos:
                Recinto.Append("<table>")
                Recinto.Append("<caption>Recintos Vinculados à Transportadora</caption>")
                Recinto.Append("    <thead bgcolor=" & CorPadrao & ">")
                Recinto.Append("        <td>DESCRIÇÃO</td>")
                Recinto.Append("        <td>CÓDIGO</td>")
                Recinto.Append("    </thead>")

                For Each DadoTR In ListaRec
                    Recinto.Append("<tbody>")
                    Recinto.Append("    <td>" & DadoTR.Descricao & "</td>")
                    Recinto.Append("    <td>" & DadoTR.DescrResumida & "</td>")
                    Recinto.Append("</tbody>")
                Next
                Recinto.Append("</table>")

                'Parte mais de baixo do protocolo:
                Rodape.Append("<br />")
                Rodape.Append("<br/>")
                Rodape.Append("<table>")
                Rodape.Append("    <thead bgcolor=" & CorPadrao & ">")
                Rodape.Append("        <td></td>")
                Rodape.Append("        <td>CHEGADA NO TERMINAL</td>")
                Rodape.Append("        <td>SAÍDA DO TERMINAL</td>")
                Rodape.Append("    </thead>")
                Rodape.Append("    <tbody>")
                Rodape.Append("    <td align=""center""><img src=""CodigoBarra.aspx?protocolo=" & LeitorAgRet("NUM_PROTOCOLO").ToString() & "/" & LeitorAgRet("ANO_PROTOCOLO").ToString() & "&modo=R"" /></td> ")
                Rodape.Append("        <td class=assinatura><br/> ")
                Rodape.Append("            ___/___/___ ___:___:___ ")
                Rodape.Append("    <br/> <br/> <br/> <br/> ")
                Rodape.Append("            _______________________ ")
                Rodape.Append("        </td>")
                Rodape.Append("        <td class=assinatura><br/> ")
                Rodape.Append("            ___/___/___ ___:___:___ ")
                Rodape.Append("    <br/> <br/> <br/> <br/> ")
                Rodape.Append("            _______________________ ")
                Rodape.Append("        </td>")
                Rodape.Append("    </tbody>")
                Rodape.Append("</table>")
                Rodape.Append("<br />")

                Estrutura.Append(Cabecalho.ToString())
                Estrutura.Append(DadosPrincipal.ToString())
                Estrutura.Append(Recinto.ToString())
                Estrutura.Append(Rodape.ToString())
                Estrutura.Append("<div class=folha></div>")

            End While

            Cabecalho.Clear()
            DadosPrincipal.Clear()
            Recinto.Clear()
            Rodape.Clear()
            SQL.Clear()

            AlterarStatusImpressao(Item)

        Next

        Return Estrutura

    End Function

    Public Function CarregarDatasPeriodo(ByVal idPeriodo As Integer) As String
        Dim SQL As New StringBuilder
        Dim Conex As New OleDbConnection
        Dim Comando As OleDbCommand
        Dim TextoPeriodo As String

        SQL.Append("SELECT ")
        SQL.Append("    TO_CHAR(PERIODO_INICIAL, 'DD/MM/YYYY HH24:MI') || ' - ' || TO_CHAR(PERIODO_FINAL, 'DD/MM/YYYY HH24:MI') AS HORARIO ")
        SQL.Append("FROM ")
        If Banco.BancoEmUso = "ORACLE" Then
            SQL.Append("    OPERADOR.TB_GD_RESERVA ")
        Else
            SQL.Append("    OPERADOR.DBO.TB_GD_RESERVA ")
        End If
        SQL.Append("WHERE ")
        SQL.Append("    AUTONUM_GD_RESERVA = " & idPeriodo)

        Conex.ConnectionString = Banco.StringConexao(True)
        Conex.Open()
        Comando = New OleDbCommand(SQL.ToString(), Conex)
        TextoPeriodo = Comando.ExecuteScalar()
        Conexao.Close()

        Return TextoPeriodo
    End Function

    ''' <summary>
    ''' Obtém o Status da Impressão de um determinado Agendamento de Retirada de Contêiner
    ''' </summary>
    ''' <param name="Id">Id do Agendamento de Retirada</param>
    ''' <returns>Cód Impresso</returns>
    ''' <remarks></remarks>
    Public Function ObterStatusImpressao(ByVal Id As Integer) As Integer
        Dim SQL As New StringBuilder
        Dim Conex As New OleDbConnection
        Dim Comando As OleDbCommand
        Dim Status As Integer

        SQL.Append("SELECT ")
        If Banco.BancoEmUso = "ORACLE" Then
            SQL.Append("    NVL")
        Else
            SQL.Append("    ISNULL")
        End If
        SQL.Append("(IMPRESSO, 0) AS IMPRESSO ")
        SQL.Append("FROM ")
        If Banco.BancoEmUso = "ORACLE" Then
            SQL.Append("    OPERADOR.TB_AG_RETIRADA ")
        Else
            SQL.Append("    OPERADOR.DBO.TB_AG_RETIRADA ")
        End If
        SQL.Append("WHERE ")
        SQL.Append("    AUTONUM = {0} ")

        Conex.ConnectionString = Banco.StringConexao(True)
        Conex.Open()
        Comando = New OleDbCommand(String.Format(SQL.ToString(), Id), Conex)
        Status = Convert.ToInt16(Comando.ExecuteScalar())
        Conexao.Close()

        Return Status

    End Function

End Class
