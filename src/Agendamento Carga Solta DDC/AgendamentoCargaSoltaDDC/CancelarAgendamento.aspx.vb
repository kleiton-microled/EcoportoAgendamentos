Public Class CancelarAgendamento
    Inherits System.Web.UI.Page

    Dim Agendamento As New Agendamento

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not Page.IsPostBack Then
            If Request.QueryString("ID") IsNot Nothing Then
                Agendamento.CancelarAgendamento(Request.QueryString("ID").ToString())
            End If
        End If

    End Sub

End Class