Imports System.Drawing
Imports System.Drawing.Imaging

Public Class Protocolo_RetiradaTRA
    Inherits System.Web.UI.Page

    Dim AgendRetirada As New AgRetiradaTRA

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            If Not Request.QueryString("protocolo") Is Nothing Then
                Dim Layout As StringBuilder = AgendRetirada.GerarProtocolo(Request.QueryString("protocolo").ToString(), Session("SIS_COD_EMPRESA").ToString(), Session("SIS_COR_PADRAO").ToString(), Session("SIS_ID").ToString())
                conteudo.InnerHtml = "<center>" & Layout.ToString() & "</center>"
            End If
        End If
    End Sub

End Class