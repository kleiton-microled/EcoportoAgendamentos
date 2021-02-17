Public Class AgendamentoRecusado
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Request.QueryString("ID") IsNot Nothing Then
            Me.lblMensagem.Text = Banco.Conexao.Execute("SELECT MOTIVO_AGENDAMENTO_RECUSADO FROM TB_AG_CS WHERE AUTONUM = " & Request.QueryString("ID").ToString()).Fields(0).Value.ToString()
        End If
    End Sub

End Class