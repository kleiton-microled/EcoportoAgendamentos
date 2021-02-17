Public Class AgendamentoRecusado
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Request.QueryString("protocolo") IsNot Nothing Then
            Me.lblMensagem.Text = Banco.Conexao.Execute("SELECT MOTIVO_AGENDAMENTO_RECUSADO FROM TB_CNTR_BL WHERE AUTONUM_TRANSPORTE_AGENDA = " & Val(Session("SIS_ID").ToString()) & " AND (NUM_PROTOCOLO || ANO_PROTOCOLO) = " & Request.QueryString("protocolo").ToString()).Fields(0).Value.ToString()
        End If
    End Sub

End Class