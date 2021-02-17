Public Class EmailTransportadora
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not Page.IsPostBack Then
            If Request.QueryString("id") IsNot Nothing Then
                Me.txtEmail.Text = Transportadora.ObterEmailAgendamento(Val(Request.QueryString("id").ToString()))
            End If
        End If

    End Sub

    Protected Sub btnAnexar_Click(sender As Object, e As EventArgs) Handles btnAnexar.Click

        Me.lblErro.Visible = False
        Me.lblSucesso.Visible = False

        Dim Emails() As String = Me.txtEmail.Text.Split(";")
        Dim Invalidos As New List(Of String)

        Dim Invalido As Boolean = False

        For Each emailStr In Emails
            If Not Email.ValidarEmail(emailStr) Then
                Invalido = True
                Invalidos.Add(emailStr)
            End If
        Next

        If Invalido Then
            Me.lblErro.Text = "O e-email informado é inválido: <br/>" & String.Join("<br/>", Invalidos)
            Me.lblErro.Visible = True
            Exit Sub
        End If

        Banco.Conexao.Execute("UPDATE OPERADOR.TB_CAD_TRANSPORTADORAS SET EMAIL_AGENDAMENTO = '" & Me.txtEmail.Text & "' WHERE AUTONUM = " & Val(Session("SIS_ID").ToString()))

        Me.lblSucesso.Visible = True

    End Sub

End Class
