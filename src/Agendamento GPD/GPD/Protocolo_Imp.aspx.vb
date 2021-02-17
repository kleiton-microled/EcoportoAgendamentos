Public Class Protocolo_Imp
    Inherits System.Web.UI.Page

    Dim ImportacaoBLL As New Importacao

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not Page.IsPostBack Then
            If Not Request.QueryString("protocolo") Is Nothing Then

                If Documento.AgendamentoRecusado(Request.QueryString("protocolo").ToString(), Val(Session("SIS_ID").ToString())) Then
                    Response.Redirect("AgendamentoRecusado.aspx?protocolo=" & Request.QueryString("protocolo").ToString())
                End If

                If Not Documento.AgendamentoLiberado(Request.QueryString("protocolo").ToString(), Val(Session("SIS_ID").ToString())) Then
                    Response.Redirect("ProtocoloNaoLiberado.aspx?protocolo=" & Request.QueryString("protocolo").ToString())
                    Exit Sub
                End If

                Dim Sb As StringBuilder = ImportacaoBLL.GerarProtocolo(Request.QueryString("protocolo").ToString(), Session("SIS_COR_PADRAO").ToString(), Session("SIS_EMPRESA").ToString())
                Response.Write("<center>" & Sb.ToString() & "</center>")

            End If
        End If

    End Sub

End Class