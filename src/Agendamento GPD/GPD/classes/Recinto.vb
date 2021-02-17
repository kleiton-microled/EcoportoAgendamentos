Imports System.Data.OleDb

Public Class Recinto

    Private _Autonum As Integer
    Private _IdTransp As Integer
    Private _IdRecinto As Integer
    Private _AutonumAtivo As Integer
    Private _AutonumRecinto As Integer
    Private _Descricao As String
    Private _DescrResumida As String

    ''' <summary>
    ''' Autonum da tabela de ligação entre transportadora e recinto
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Autonum() As Integer
        Get
            Return _Autonum
        End Get
        Set(value As Integer)
            _Autonum = value
        End Set
    End Property

    Public Property IdTransp() As Integer
        Get
            Return _IdTransp
        End Get
        Set(value As Integer)
            _IdTransp = value
        End Set
    End Property

    Public Property IdRecinto() As Integer
        Get
            Return _IdRecinto
        End Get
        Set(value As Integer)
            _IdRecinto = value
        End Set
    End Property

    Public Property AutonumAtivo As Integer
        Get
            Return _AutonumAtivo
        End Get
        Set(value As Integer)
            _AutonumAtivo = value
        End Set
    End Property

    ''' <summary>
    ''' Autonum da tabela de recintos (DTE_TB_RECINTOS)
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AutonumRecinto As Integer
        Get
            Return _AutonumRecinto
        End Get
        Set(value As Integer)
            _AutonumRecinto = value
        End Set
    End Property

    ''' <summary>
    ''' Descricao detalhada da tabela de recintos
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Descricao As String
        Get
            Return _Descricao.Trim()
        End Get
        Set(value As String)
            _Descricao = value.Trim()
        End Set
    End Property

    Public Property DescrResumida As String
        Get
            Return _DescrResumida.Trim()
        End Get
        Set(value As String)
            _DescrResumida = value.Trim()
        End Set
    End Property

    Public Function CadastrarTranspRecinto(ByVal Recinto As String, ByVal IdTransp As String) As Integer
        Dim SQL As New StringBuilder
        Dim Conex As New OleDbConnection
        Dim Comando As New OleDbCommand
        Dim IdTranspRec As Integer = 0
        Dim LinhasAfetadas As Integer = 0

        If Banco.BancoEmUso = "SQLSERVER" Then
            SQL.Append("SELECT MAX(AUTONUM) + 1 FROM OPERADOR.DBO.TB_TRANSPORTE_RECINTO")
            Conex.ConnectionString = Banco.StringConexao(False)
            Conex.Open()
            Comando = New OleDbCommand(SQL.ToString(), Conex)
            IdTranspRec = Comando.ExecuteScalar()

            Conex.Close()
            Comando = Nothing
            SQL.Clear()
        End If

        Try
            SQL.Append("INSERT ")
            SQL.Append("    INTO ")
            If Banco.BancoEmUso = "ORACLE" Then
                SQL.Append("    OPERADOR.TB_TRANSPORTE_RECINTO ")
            Else
                SQL.Append("    OPERADOR.DBO.TB_TRANSPORTE_RECINTO ")
            End If
            SQL.Append("(AUTONUM, TRANSPORTADORA, RECINTO, FLAG_ATIVO) ")
            SQL.Append("VALUES (")
            If Banco.BancoEmUso = "ORACLE" Then
                SQL.Append("OPERADOR.SEQ_TRANSPORTE_RECINTO.NEXTVAL")
            Else
                SQL.Append(IdTranspRec)
            End If
            SQL.Append(", " & IdTransp & ", " & Recinto & ", 1)")

            Conex.ConnectionString = Banco.StringConexao(False)
            Conex.Open()
            Comando = New OleDbCommand(SQL.ToString(), Conex)
            LinhasAfetadas = Comando.ExecuteNonQuery()
            Conex.Close()
            Return LinhasAfetadas

        Catch ex As Exception
            Return -1
        End Try

    End Function

    Public Function AlterarRecintoTransp(ByVal Codigo As String, ByVal Recinto As String, ByVal Transp As String, ByVal Ativo As Integer) As Integer
        Dim SQL As New StringBuilder
        Dim Conex As New OleDbConnection
        Dim Comando As New OleDbCommand

        Try
            Dim Retornar As String

            SQL.Append("UPDATE ")
            If Banco.BancoEmUso = "ORACLE" Then
                SQL.Append("    OPERADOR.TB_TRANSPORTE_RECINTO ")
            Else
                SQL.Append("    OPERADOR.TB_TRANSPORTE_RECINTO ")
            End If
            SQL.Append("SET ")
            SQL.Append("    RECINTO = " & Recinto & ", ")
            SQL.Append("    TRANSPORTADORA = " & Transp & ", ")
            SQL.Append("    FLAG_ATIVO = " & Ativo.ToString() & " ")
            SQL.Append("WHERE ")
            SQL.Append("    AUTONUM = " & Codigo & "")

            Conex.ConnectionString = Banco.StringConexao(False)
            Conex.Open()
            Comando = New OleDbCommand(SQL.ToString(), Conex)
            Retornar = Comando.ExecuteNonQuery()
            Conex.Close()
            Return Retornar

        Catch ex As Exception
            Return -1
        End Try

    End Function

    Public Function ExcluirRecintoTransp(ByVal Identificacao As String) As Integer
        Dim SQL As New StringBuilder
        Dim Conex As New OleDbConnection
        Dim Comando As New OleDbCommand

        Try
            SQL.Append("DELETE FROM ")
            If Banco.BancoEmUso = "ORACLE" Then
                SQL.Append("OPERADOR.TB_TRANSPORTE_RECINTO ")
            Else
                SQL.Append("OPERADOR.DBO.TB_TRANSPORTE_RECINTO ")
            End If
            SQL.Append("WHERE AUTONUM = " & Identificacao)

            Conex.ConnectionString = Banco.StringConexao(False)
            Conex.Open()
            Comando = New OleDbCommand(SQL.ToString, Conex)
            LinhasAfetadas = Comando.ExecuteNonQuery()
            Conex.Close()
            Return LinhasAfetadas

        Catch ex As Exception
            Return -1
        End Try

    End Function

    Public Function CarregarRecintos() As DataTable
        Dim SQL As New StringBuilder
        Dim Con As New OleDbConnection

        SQL.Append("SELECT ")
        SQL.Append("    CODE, DESCR, DESCR_RESUMIDA ")
        SQL.Append("FROM ")
        If Banco.BancoEmUso = "ORACLE" Then
            SQL.Append("    OPERADOR.DTE_TB_RECINTOS ")
        Else
            SQL.Append("    OPERADOR.DBO.DTE_TB_RECINTOS ")
        End If
        SQL.Append("WHERE ")
        SQL.Append("    DESCR_RESUMIDA IS NOT NULL ")
        SQL.Append("ORDER BY DESCR")

        Con.ConnectionString = Banco.StringConexao(False)
        Con.Open()

        Using Adaptador As New OleDbDataAdapter(SQL.ToString(), Con)
            Dim Ds As New DataSet
            Adaptador.Fill(Ds, "RECINTOS")
            Con.Close()
            Return Ds.Tables(0)
        End Using

    End Function

    ''' <summary>
    ''' Obtém código do recinto através de seu CNPJ
    ''' </summary>
    ''' <param name="CNPJ">CNPJ do recinto</param>
    ''' <returns>Código (CODE) do Recinto; -1 caso não exista</returns>
    ''' <remarks></remarks>
    Public Function ObterCodRecinto(ByVal CNPJ As String) As Integer
        Dim SQL As New StringBuilder
        Dim Con As New OleDbConnection
        Dim Comando As OleDbCommand
        Dim Cod As Integer

        SQL.Append("SELECT ")
        SQL.Append("    CODE FROM ")
        If Banco.BancoEmUso = "ORACLE" Then
            SQL.Append("OPERADOR.DTE_TB_RECINTOS ")
        Else
            SQL.Append("OPERADOR.DBO.DTE_TB_RECINTOS ")
        End If
        SQL.Append("WHERE ")
        SQL.Append("    REPLACE(REPLACE(REPLACE(CGC, '.', ''), '/', ''), '-', '') = '" & CNPJ.Replace(".", "").Replace("-", "").Replace("/", "").Trim & "' ")

        Try
            Con.ConnectionString = Banco.StringConexao(False)
            Con.Open()
            Comando = New OleDbCommand(SQL.ToString(), Con)
            Cod = Convert.ToInt32(Comando.ExecuteScalar())
        Catch ex As Exception
            Cod = -1
        End Try

        Return Cod

    End Function

    ''' <summary>
    ''' Obtém o código do recinto de uma determinada vinculação entre Transportadora e Recinto
    ''' </summary>
    ''' <param name="CodVinc">Id (Autonum) da Vinculação entre Transportadora e Recinto</param>
    ''' <returns>Código do Recinto; -1 caso não exista</returns>
    ''' <remarks></remarks>
    Public Function ObterCodRecVinc(ByVal CodVinc As String) As Integer
        Dim SQL As New StringBuilder
        Dim Con As New OleDbConnection
        Dim Comando As OleDbCommand
        Dim Cod As Integer

        SQL.Append("SELECT ")
        SQL.Append("    RECINTO ")
        SQL.Append("FROM ")
        If Banco.BancoEmUso = "ORACLE" Then
            SQL.Append("OPERADOR.TB_TRANSPORTE_RECINTO ")
        Else
            SQL.Append("OPERADOR.DBO.TB_TRANSPORTE_RECINTO ")
        End If
        SQL.Append("WHERE ")
        SQL.Append("    AUTONUM = " & CodVinc.ToString() & "")

        Try
            Con.ConnectionString = Banco.StringConexao(False)
            Con.Open()
            Comando = New OleDbCommand(SQL.ToString(), Con)
            Cod = Convert.ToInt32(Comando.ExecuteScalar())
        Catch ex As Exception
            Cod = -1
        End Try

        Return Cod

    End Function

    ''' <summary>
    ''' Obtém recintos vinculados à transportadora que sejam ativos
    ''' </summary>
    ''' <param name="IdTransp">Transportadora fornecida</param>
    ''' <returns>Lista dos recintos com o código da vinculação entre recintos e transportadora, descrição e descrição resumida do recinto</returns>
    ''' <remarks></remarks>
    Public Function ConsultarRecintosAtivosTransp(ByVal IdTransp) As List(Of Recinto)
        Dim SQL As New StringBuilder
        Dim Con As New OleDbConnection
        Dim Comando As New OleDbCommand
        Dim LeitorRec As OleDbDataReader

        Dim lstRecAg As New List(Of Recinto)

        SQL.Append("SELECT ")
        SQL.Append("    TR.AUTONUM AS AUTONUM, R.DESCR AS DESCRICAO, R.DESCR_RESUMIDA AS CODIGO ")
        SQL.Append("FROM ")
        If Banco.BancoEmUso = "ORACLE" Then
            SQL.Append("    OPERADOR.TB_TRANSPORTE_RECINTO TR ")
        Else
            SQL.Append("    OPERADOR.DBO.TB_TRANSPORTE_RECINTO TR ")
        End If
        SQL.Append("INNER JOIN ")
        If Banco.BancoEmUso = "ORACLE" Then
            SQL.Append("    OPERADOR.DTE_TB_RECINTOS R ")
        Else
            SQL.Append("    OPERADOR.DBO.DTE_TB_RECINTOS R ")
        End If
        SQL.Append("    ON TR.RECINTO = R.CODE ")
        SQL.Append("WHERE ")
        If Banco.BancoEmUso = "ORACLE" Then
            SQL.Append("NVL")
        Else
            SQL.Append("ISNULL")
        End If
        SQL.Append("(TR.FLAG_ATIVO, 0) = 1 ")
        SQL.Append("AND ")
        SQL.Append("    TR.TRANSPORTADORA = " & IdTransp & " ")
        SQL.Append("ORDER BY DESCR")


        Con.ConnectionString = Banco.StringConexao(False)
        Con.Open()
        Comando = New OleDbCommand(SQL.ToString(), Con)
        LeitorRec = Comando.ExecuteReader()

        While LeitorRec.Read()
            Dim RecAgend As New Recinto
            RecAgend.Autonum = LeitorRec("AUTONUM").ToString()
            RecAgend.Descricao = LeitorRec("DESCRICAO").ToString()
            RecAgend.DescrResumida = LeitorRec("CODIGO").ToString()
            lstRecAg.Add(RecAgend)
            RecAgend = Nothing
        End While
        LeitorRec.Close()
        Con.Close()

        Return lstRecAg

    End Function

    ''' <summary>
    ''' Verifica se existe vinculação entre um determinado recinto e uma determinada transportadora quando cadastra uma vinculação
    ''' </summary>
    ''' <param name="Recinto">Recinto</param>
    ''' <param name="IdTransp">Código da Transportadora</param>
    ''' <returns>False: Se não existe vinculação; True: se existe vinculação</returns>
    ''' <remarks></remarks>
    Public Function ExisteVinculacao(ByVal Recinto As String, ByVal IdTransp As String) As Boolean
        Dim SQL As New StringBuilder
        Dim Con As New OleDbConnection
        Dim Comando As New OleDbCommand
        Dim Qtde As Integer

        SQL.Append("SELECT ")
        SQL.Append("    COUNT(*) ")
        SQL.Append("FROM ")
        If Banco.BancoEmUso = "ORACLE" Then
            SQL.Append("    OPERADOR.TB_TRANSPORTE_RECINTO ")
        Else
            SQL.Append("    OPERADOR.DBO.TB_TRANSPORTE_RECINTO ")
        End If
        SQL.Append("WHERE ")
        SQL.Append("    TRANSPORTADORA = '" & IdTransp & "' AND ")
        SQL.Append("    RECINTO = " & Recinto & "")

        Con.ConnectionString = Banco.StringConexao(False)
        Con.Open()
        Comando = New OleDbCommand(SQL.ToString(), Con)
        Qtde = Convert.ToInt16(Comando.ExecuteScalar())
        Con.Close()

        If Qtde = 0 Then
            Return False
        Else
            Return True
        End If

    End Function

    ''' <summary>
    ''' Verifica se existe vinculação entre um determinado recinto e uma determinada transportadora quando altera uma vinculação
    ''' </summary>
    ''' <param name="Recinto">Recinto</param>
    ''' <param name="IdTransp">Código da Transportadora</param>
    ''' <param name="Autonum">Autonum do registro</param>
    ''' <returns>False: Se não existe vinculação; True: se existe vinculação</returns>
    ''' <remarks></remarks>
    Public Function ExisteVinculacao(ByVal Recinto As String, ByVal IdTransp As String, ByVal Autonum As String) As Boolean
        Dim SQL As New StringBuilder
        Dim Con As New OleDbConnection
        Dim Comando As New OleDbCommand
        Dim Qtde As Integer

        SQL.Append("SELECT ")
        SQL.Append("    COUNT(*) ")
        SQL.Append("FROM ")
        If Banco.BancoEmUso = "ORACLE" Then
            SQL.Append("    OPERADOR.TB_TRANSPORTE_RECINTO ")
        Else
            SQL.Append("    OPERADOR.DBO.TB_TRANSPORTE_RECINTO ")
        End If
        SQL.Append("WHERE ")
        SQL.Append("    (TRANSPORTADORA = '" & IdTransp & "' AND ")
        SQL.Append("    RECINTO = " & Recinto & ") ")
        SQL.Append("    AND ")
        SQL.Append("    AUTONUM <> " & Autonum & "") 'Despreza se for o autonum do que está sendo editado

        Con.ConnectionString = Banco.StringConexao(False)
        Con.Open()
        Comando = New OleDbCommand(SQL.ToString(), Con)
        Qtde = Convert.ToInt16(Comando.ExecuteScalar())
        Con.Close()

        If Qtde = 0 Then
            Return False
        Else
            Return True
        End If

    End Function

    ''' <summary>
    ''' Carrega os recintos vinculados à transportadora logada
    ''' </summary>
    ''' <param name="CodTransportadora">Autonum da transportadora</param>
    ''' <returns>Dados dos recintos vinculados à transportadora</returns>
    ''' <remarks></remarks>

    Public Function ConsultarRecintosTransp(ByVal CodTransportadora As String) As DataTable
        Dim SQL As New StringBuilder
        Dim Conex As New OleDbConnection

        SQL.Append("SELECT ")
        SQL.Append("    TR.AUTONUM AS AUTONUM, R.DESCR AS DESCR, R.DESCR_RESUMIDA AS DESCR_RESUMIDA, TR.FLAG_ATIVO AS FLAG_ATIVO, ")
        SQL.Append("    CASE ")
        SQL.Append("        WHEN TR.FLAG_ATIVO = 1 THEN 'SIM' ")
        SQL.Append("    ELSE CASE ")
        SQL.Append("        WHEN TR.FLAG_ATIVO = 0 THEN 'NÃO' END ")
        SQL.Append("    END AS ATIVO, ")
        SQL.Append("    '' AS DESCR_RECINTO ") 'Descrição do Recinto será DESCR p:/ este caso
        SQL.Append("FROM ")
        SQL.Append("    OPERADOR.TB_TRANSPORTE_RECINTO TR ")
        SQL.Append("INNER JOIN ")
        SQL.Append("    OPERADOR.DTE_TB_RECINTOS R ")
        SQL.Append("ON R.CODE = TR.RECINTO ")
        SQL.Append("WHERE ")
        SQL.Append("    TRANSPORTADORA = " & CodTransportadora & " ")
        SQL.Append("    ORDER BY R.DESCR ")

        Conex.ConnectionString = Banco.StringConexao(False)
        Conex.Open()

        Using Adaptador As New OleDbDataAdapter(SQL.ToString(), Conex)
            Dim Ds As New DataSet
            Adaptador.Fill(Ds, "RECINTOS_TRANSPORTADORA")
            Conex.Close()
            Return Ds.Tables(0)
        End Using

    End Function

    ''' <summary>
    ''' Consulta Dados da tabela OPERADOR.TB_TRANSPORTE_RECINTO de acordo com o Autonum desta
    ''' </summary>
    ''' <param name="Codigo">Autonum do registro</param>
    ''' <returns>Objeto Recinto, retorna -1 pra Autonum caso registro pra esta vinculação não exite mais</returns>
    ''' <remarks></remarks>
    Public Function ConsultarDadosRecTransp(ByVal Codigo As String) As Recinto
        Dim SQL As New StringBuilder
        Dim Conex As New OleDbConnection
        Dim TrRec As New Recinto

        SQL.Append("SELECT ")
        SQL.Append("    TRANSPORTADORA, RECINTO, FLAG_ATIVO ")
        SQL.Append("FROM ")
        If Banco.BancoEmUso = "ORACLE" Then
            SQL.Append("OPERADOR.TB_TRANSPORTE_RECINTO ")
        Else
            SQL.Append("OPERADOR.DBO.TB_TRANSPORTE_RECINTO ")
        End If
        SQL.Append("WHERE ")
        SQL.Append("    AUTONUM = " & Codigo)

        Try
            Conex.ConnectionString = Banco.StringConexao(False)
            Conex.Open()

            Using Adaptador As New OleDbDataAdapter(SQL.ToString(), Conex)
                Dim Ds As New DataSet
                Dim Dt As New DataTable
                Adaptador.Fill(Ds, "TRANSPORTE_RECINTO")
                Conex.Close()

                Dt = Ds.Tables(0)
                TrRec.Autonum = Convert.ToInt64(Codigo)
                TrRec.IdTransp = Convert.ToInt32(Dt.Rows(0)("TRANSPORTADORA").ToString())
                TrRec.IdRecinto = Convert.ToInt32(Dt.Rows(0)("RECINTO").ToString())
                TrRec.AutonumAtivo = Convert.ToInt16(Dt.Rows(0)("FLAG_ATIVO").ToString())
                Return TrRec
            End Using
        Catch ex As Exception
            TrRec.Autonum = -1
            Return TrRec
        End Try

    End Function

    ''' <summary>
    ''' Carrega as transportadoras vinculadas a recintos
    ''' </summary>
    ''' <param name="CodRecinto">Code do Recinto</param>
    ''' <returns>Dados das transportadoras vinculadas ao recinto </returns>
    ''' <remarks>Usado também para EcoPorto, para consultar Recintos, já que está consulta todas as vinculações; se estiver sem parâmetros retorna todos as vinculações entre recintos e transportadoras</remarks>
    Public Function ConsultarTranspRecintos(Optional ByVal CodRecinto As String = "") As DataTable
        Dim SQL As New StringBuilder
        Dim Conex As New OleDbConnection

        SQL.Append("SELECT ")
        SQL.Append("    TR.AUTONUM AS AUTONUM, T.RAZAO AS DESCR, T.FANTASIA AS DESCR_RESUMIDA, TR.FLAG_ATIVO AS FLAG_ATIVO, ")
        SQL.Append("    CASE ")
        SQL.Append("        WHEN TR.FLAG_ATIVO = 1 THEN 'SIM' ")
        SQL.Append("    ELSE CASE ")
        SQL.Append("        WHEN TR.FLAG_ATIVO = 0 THEN 'NÃO' END ")
        SQL.Append("    END AS ATIVO, ")
        SQL.Append("    R.DESCR AS DESCR_RECINTO ")
        SQL.Append("FROM ")
        SQL.Append("    OPERADOR.TB_TRANSPORTE_RECINTO TR ")
        SQL.Append("LEFT JOIN ")
        SQL.Append("    OPERADOR.TB_CAD_TRANSPORTADORAS T ")
        SQL.Append("ON T.AUTONUM = TR.TRANSPORTADORA ")
        SQL.Append("LEFT JOIN ")
        SQL.Append("    OPERADOR.DTE_TB_RECINTOS R ")
        SQL.Append("ON R.CODE = TR.RECINTO ")
        If CodRecinto <> String.Empty Then
            SQL.Append("WHERE ")
            SQL.Append("    RECINTO = " & CodRecinto & " ")
        End If
        SQL.Append("    ORDER BY T.RAZAO, R.DESCR ")

        Conex.ConnectionString = Banco.StringConexao(False)
        Conex.Open()

        Using Adaptador As New OleDbDataAdapter(SQL.ToString(), Conex)
            Dim Ds As New DataSet
            Adaptador.Fill(Ds, "RECINTOS_TRANSPORTADORA")
            Conex.Close()
            Return Ds.Tables(0)
        End Using

    End Function

    ''' <summary>
    ''' Verifica por questões de segurança se id da Vinculação é do Recinto logado
    ''' </summary>
    ''' <param name="idRec">Código do Recinto que está logado</param>
    ''' <param name="idVinc">Autonum da Vinculação da Transportadora em um Recinto</param>
    ''' <returns>true: se id do recinto é o do recinto logado, false: se id do recinto não é o do recinto logado</returns>
    ''' <remarks></remarks>
    Public Function VerificarVincRec(ByVal idRec As String, ByVal idVinc As String) As Boolean
        Dim SQL As New StringBuilder
        Dim Conex As New OleDbConnection
        Dim Comando As OleDbCommand
        Dim RecintVinc As Integer

        SQL.Append("SELECT ")
        SQL.Append("    RECINTO FROM ")
        If Banco.BancoEmUso = "ORACLE" Then
            SQL.Append("OPERADOR.TB_TRANSPORTE_RECINTO ")
        Else
            SQL.Append("OPERADOR.DBO.TB_TRANSPORTE_RECINTO ")
        End If
        SQL.Append("WHERE ")
        SQL.Append("    AUTONUM = " & Convert.ToInt64(idVinc) & "")

        Conex.ConnectionString = Banco.StringConexao(False)
        Conex.Open()
        Comando = New OleDbCommand(SQL.ToString(), Conex)
        RecintVinc = Convert.ToInt64(Comando.ExecuteScalar())
        Conex.Close()

        If RecintVinc = Convert.ToInt32(idRec) Then
            Return True
        Else
            Return False
        End If

    End Function
End Class
