Imports System.IO

Public Class GerenciadorDeUploads

    Public Shared Function Gravar(ByVal diretorioPadrao As String, ByVal reserva As String, ByVal arquivo() As Byte, ByVal nomeArquivo As String) As String

        Dim diretorioArquivo As String = GetDiretorioArquivo(diretorioPadrao, reserva, nomeArquivo)

        Try

            Using Stream = New FileStream(diretorioArquivo, FileMode.Create)
                Stream.Write(arquivo, 0, arquivo.Length)
            End Using

            Return diretorioArquivo

        Catch ex As DirectoryNotFoundException
            Throw New Exception($"Ocorreu um erro ao gravar o arquivo. Diretório não encontrado. Erro: {ex.ToString()}")
        Catch __unusedUnauthorizedAccessException2__ As UnauthorizedAccessException
            Dim attr As FileAttributes = (New FileInfo(diretorioArquivo)).Attributes
            Dim mensagem = "Não foi possível acessar o arquivo."
            If (attr And FileAttributes.[ReadOnly]) > 0 Then mensagem += " O arquivo é somente leitura"
            Throw New Exception($"Ocorreu um erro ao gravar o arquivo. {mensagem}")
        Catch ex As Exception
            Throw New Exception($"Ocorreu um erro ao gravar o arquivo. Erro: {ex.ToString()}")
        End Try
    End Function

    Private Shared Function GetDiretorioArquivo(ByVal diretorio As String, ByVal reserva As String, ByVal nomeArquivo As String) As String

        reserva = reserva.Replace("/", "_")

        Dim diretorioAgendamento = Path.Combine(diretorio, reserva)

        Try

            If Not Directory.Exists(diretorioAgendamento) Then
                Directory.CreateDirectory(diretorioAgendamento)
            End If

            Dim diretorioArquivo As String = Path.Combine(diretorioAgendamento, nomeArquivo)

            Return diretorioArquivo

        Catch ex As UnauthorizedAccessException
            Throw New Exception($"Acesso ao diretório negado! {ex.ToString()}")
        Catch ex As Exception
            Throw New Exception($"Ocorreu um erro ao obter o arquivo. Erro: {ex.ToString()}")
        End Try
    End Function

    Public Shared Function GetArquivo(ByVal diretorio As String, ByVal reserva As String, ByVal nomeArquivo As String) As Byte()

        Dim diretorioArquivo As String = GetDiretorioArquivo(diretorio, reserva, nomeArquivo)

        Try
            Return File.ReadAllBytes(diretorioArquivo)
        Catch ex As DirectoryNotFoundException
            Throw New Exception($"Ocorreu um erro ao obter o arquivo. Erro: {ex.ToString()}")
        Catch ex As Exception
            Throw New Exception($"Ocorreu um erro ao obter o arquivo. Erro: {ex.ToString()}")
        End Try
    End Function

End Class
