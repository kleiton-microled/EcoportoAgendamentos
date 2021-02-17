Module Banco

    Public LinhasAfetadas As Integer
    Private objConexao As ADODB.Connection
    Private objConexaoBDCC As ADODB.Connection
    Private _StringConexao As String

    Public Function Conexao() As ADODB.Connection

        Try

            If objConexao Is Nothing Then
                objConexao = New ADODB.Connection
            End If

            If objConexao.State = 0 Then
                objConexao.Open(StringConexao(True))
            End If

        Catch ex As Exception
            Throw New Exception("Erro ao abrir a conexão.")
        End Try

        Return objConexao

    End Function

    Public Sub Desconectar()

        Try
            If objConexao IsNot Nothing Then
                If objConexao.State = 1 Then
                    objConexao.Close()
                End If
            End If
        Catch ex As Exception
            Throw New Exception("Erro ao fechar a conexão.")
        End Try

    End Sub

    Public Function ConexaoBDCC() As ADODB.Connection

        If objConexaoBDCC Is Nothing Then
            objConexaoBDCC = New ADODB.Connection
            objConexaoBDCC.Open(StringConexao(True))
        End If

        Return objConexaoBDCC

    End Function

    Public Function StringConexao(ByVal Provider As Boolean) As String

        Dim Servidor As String = My.Settings.Servidor
        Dim Usuario As String = My.Settings.Usuario
        Dim Senha As String = My.Settings.Senha

        If Provider Then
            Return String.Format("Provider=OraOLEDB.Oracle;Data Source={0};User ID={1};Password={2};Unicode=True", Servidor, Usuario, Senha)
        End If

        Return String.Format("Data Source={0};User ID={1};Password={2}", Servidor, Usuario, Senha)

    End Function

    Public Function BancoEmUso() As String
        Return My.Settings.Banco
    End Function

End Module
