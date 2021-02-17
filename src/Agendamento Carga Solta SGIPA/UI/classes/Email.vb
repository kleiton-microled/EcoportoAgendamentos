Imports System.Data.OleDb

Public Class Email

    Public Shared Function EnviarEmail(ByVal fFrom As String, ByVal tTo As String, ByVal Subject As String, ByVal Cc As String, ByVal Bcc As String, ByVal Message As String) As String

        Dim EnderecoWebService = Banco.Conexao.Execute("SELECT WS_EMAIL FROM SGIPA.TB_PARAMETROS_SISTEMA").Fields(0).Value.ToString()

        Using email As New WsEmail.Email()

            email.Url = EnderecoWebService

            Dim emailPara() As String = {tTo}
            Dim emailCopia() As String = {Cc}
            Dim emailCopiaOculta() As String = {Bcc}
            Dim anexos() As String = {}
            Dim emailRetornoErro() As String = {"sistemati@ecoportosantos.com.br"}

            Dim msg = email.EnviarEmail(
                "Microled",
                "microl&d@123",
                fFrom,
                emailPara,
                emailCopia,
                emailCopiaOculta,
                Subject,
                Message,
                True,
                True,
                anexos,
                String.Empty,
                emailRetornoErro)

            Return msg

        End Using

    End Function

    Public Shared Function ValidarEmail(ByVal email As String) As Boolean
        Return Regex.IsMatch(email, "^[a-zA-Z][\w\.-]*[a-zA-Z0-9]@[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z]$")
    End Function

End Class
