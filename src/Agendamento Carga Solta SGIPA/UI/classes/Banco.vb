Module Banco

    Public LinhasAfetadas As Integer
    Private objConexao As ADODB.Connection

    Private _StringConexao As String

    Public Function StringConexaoOracle() As String

        Dim Servidor As String = My.Settings.Servidor
        Dim Usuario As String = My.Settings.Usuario
        Dim Senha As String = My.Settings.Senha

        Dim BaseHomologacao As Boolean =
            Convert.ToBoolean(Convert.ToInt16(My.Settings.BaseHomologacao))

        Try
            If Val(My.Settings.SenhaEncriptada) = 1 Then
                Dim ws As New WsControleSenha.Criptografia
                Senha = ws.RetornaSenhaBD(Usuario, BaseHomologacao)
            End If
        Catch ex As Exception
            Senha = My.Settings.Senha
        End Try

        Return String.Format("Provider=OraOLEDB.Oracle;Data Source={0};User ID={1};Password={2};Unicode=True", Servidor, Usuario, Senha)

    End Function

    Public Function Conexao() As ADODB.Connection

        If objConexao Is Nothing Then
            objConexao = New ADODB.Connection
            If objConexao.State = 0 Then
                objConexao.Open(StringConexaoOracle())
            End If
        End If

        Return objConexao

    End Function

    Public Function BancoEmUso() As String
        Return System.Configuration.ConfigurationManager.AppSettings("Banco")
    End Function

    Public Function Provedor() As String

        If BancoEmUso() = "ORACLE" Then
            Return "System.Data.OracleClient"
        Else
            Return "System.Data.SqlClient"
        End If

    End Function

End Module
