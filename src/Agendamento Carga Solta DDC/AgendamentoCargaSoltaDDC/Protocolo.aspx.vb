Public Class Protocolo
    Inherits System.Web.UI.Page

    Dim Agendamento As New Agendamento

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not Page.IsPostBack Then
            If Not Request.QueryString("ID") Is Nothing Then
                Dim Sb As StringBuilder = Agendamento.GerarProtocolo(Request.QueryString("ID").ToString(), Session("SIS_TRANSPEMPRESA"))
                If Sb.ToString.Length > 10 Then
                    Response.Write("<center>" & Sb.ToString() & "</center>")
                End If
            End If
        End If

    End Sub

End Class