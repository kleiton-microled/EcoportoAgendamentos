Imports System.Drawing
Imports System.Drawing.Imaging

Public Class Protocolo_Exp
    Inherits System.Web.UI.Page

    Dim ExportacaoBLL As New Exportacao
    Dim Bar As New BarCode

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not Page.IsPostBack Then
            If Not Request.QueryString("protocolo") Is Nothing Then
                Dim Sb As StringBuilder = ExportacaoBLL.GerarProtocolo(Request.QueryString("protocolo").ToString(), Session("SIS_COR_PADRAO").ToString(), Session("SIS_COD_EMPRESA").ToString())
                conteudo.InnerHtml = "<center>" & Sb.ToString() & "</center>"
            End If
        End If

    End Sub

End Class