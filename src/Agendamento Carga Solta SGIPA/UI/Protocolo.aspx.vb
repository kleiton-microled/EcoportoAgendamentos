Public Class Protocolo
    Inherits System.Web.UI.Page

    Dim Agendamento As New Agendamento
    Dim Documento As New Documento

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not Page.IsPostBack Then
            If Request.QueryString("ID") IsNot Nothing Then

                If Documento.AgendamentoRecusado(Request.QueryString("ID")) Then
                    Response.Redirect("AgendamentoRecusado.aspx?id=" & Request.QueryString("ID").ToString())
                End If

                If Not Documento.AgendamentoLiberado(Val(Request.QueryString("ID").ToString())) Then
                    Response.Redirect("ProtocoloNaoLiberado.aspx?id=" & Request.QueryString("ID").ToString())
                    Exit Sub
                End If

                Dim Sb As StringBuilder = Agendamento.GerarProtocolo(Request.QueryString("ID").ToString(), Session("SIS_TRANSPEMPRESA"))
                If Sb.ToString.Length > 10 Then
                    Response.Write("<center>" & Sb.ToString() & "</center>")
                End If
            End If
        End If

    End Sub

End Class