Public Class Banco

    Private Shared _Connect As OleDb.OleDbConnection
    Private Shared _Command As OleDb.OleDbCommand

    Private Shared _StringConexao As String

    Private Shared Function StringConexaoOracle() As String

        Dim Servidor As String = My.Settings.Servidor
        Dim Usuario As String = My.Settings.Usuario
        Dim Senha As String = My.Settings.Senha

        Dim BaseHomologacao As Boolean =
            Convert.ToBoolean(Convert.ToInt16(My.Settings.BaseHomologacao))

        Try
            If Val(My.Settings.SenhaEncriptada) = 1 Then
                Dim ws As New WsControleSenha.CriptografiaSoapClient
                Senha = ws.RetornaSenhaBD(Usuario, BaseHomologacao)
            End If
        Catch ex As Exception
            Senha = My.Settings.Senha
        End Try

        Return String.Format("Provider=OraOLEDB.Oracle;Data Source={0};User ID={1};Password={2};Unicode=True", Servidor, Usuario, Senha)

    End Function

    Shared Sub New()

        _Connect = New OleDb.OleDbConnection(StringConexaoOracle())
        _Command = New OleDb.OleDbCommand()

    End Sub

    Private Shared Sub Conectar()

        Try
            If _Connect.State = ConnectionState.Closed Then
                _Connect.Open()
            End If
        Catch ex As Exception
            Throw New Exception("Erro ao abrir a conexão.")
        End Try

    End Sub

    Private Shared Sub Desconectar()

        Try
            If _Connect.State = ConnectionState.Open Then
                _Connect.Close()
            End If
        Catch ex As Exception
            Throw New Exception("Erro ao fechar a conexão.")
        End Try

    End Sub

    Public Shared Sub Executar(ByVal SQL As String)

        Conectar()

        _Command.CommandType = CommandType.Text
        _Command.CommandText = SQL
        _Command.Connection = _Connect

        Try
            _Command.ExecuteNonQuery()
        Catch ex As Exception
            Throw New Exception("Erro ao executar o comando SQL.")
        Finally
            Desconectar()
        End Try

    End Sub

    Public Shared Function ExecutaRetorna(ByVal SQL As String) As Object

        Conectar()

        _Command.CommandType = CommandType.Text
        _Command.CommandText = SQL
        _Command.Connection = _Connect

        Try
            Return _Command.ExecuteScalar()
        Catch ex As Exception
            Throw New Exception("Erro ao executar o comando SQL.")
        Finally
            Desconectar()
        End Try

        Return 0

    End Function

    Public Shared Function Consultar(ByVal SQL As String) As DataTable

        Conectar()

        _Command.CommandType = CommandType.Text
        _Command.CommandText = SQL
        _Command.Connection = _Connect

        Dim Ds As New DataSet

        Using _Adapter As New OleDb.OleDbDataAdapter(_Command)
            Try
                _Adapter.Fill(Ds)
            Catch ex As Exception
                Throw New Exception("Erro ao realziar a consulta SQL.")
            Finally
                Desconectar()
            End Try
        End Using

        Return Ds.Tables(0)

    End Function

End Class
