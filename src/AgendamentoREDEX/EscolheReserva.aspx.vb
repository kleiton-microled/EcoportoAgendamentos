Public Class EscolheReserva
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not Page.IsPostBack Then
            If Request.QueryString("reserva") IsNot Nothing Then
                Me.dgBooking.DataSource = Banco.List("SELECT AUTONUM AS AUTONUM_BOO,RESERVA AS REFERENCE,NAVIO || '/' || NUM_VIAGEM AS NAVIO_VIAGEM,NVOCC,EXPORTADOR FROM REDEX.VW_AGENDAMENTO_WEB_DADOS_BOO WHERE UPPER(RESERVA) = '" & Request.QueryString("reserva").ToUpper() & "'")
                Me.dgBooking.DataBind()
            End If
        End If

    End Sub

    Protected Sub dgBooking_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles dgBooking.RowCommand

        If Me.dgBooking.Rows.Count > 0 Then

            Dim Index As Integer = e.CommandArgument
            Dim ID As String = Me.dgBooking.DataKeys(Index)("AUTONUM_BOO").ToString()

            If e.CommandName = "SEL" Then
                If Not String.IsNullOrEmpty(ID) And Not String.IsNullOrWhiteSpace(ID) Then                    
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert2", "parent.document.location.href='AgendarCSCarregamento.aspx?booking=" & ID & "';", True)
                End If
            End If

        End If


    End Sub
End Class