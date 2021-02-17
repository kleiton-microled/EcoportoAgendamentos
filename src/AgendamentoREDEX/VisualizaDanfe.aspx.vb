Imports System.IO

Public Class VisualizaDanfe
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Request.QueryString("id") IsNot Nothing And Request.QueryString("tipo") IsNot Nothing Then
            ReceberArquivo(Request.QueryString("id").ToString(), Request.QueryString("tipo").ToString())
        End If

    End Sub

    Private Sub ReceberArquivo(ByVal ID As String, ByVal Tipo As String)

        Dim SQL = ""

        If Tipo = "1" Then
            SQL = "SELECT A.ARQUIVO_DANFE, NVL(A.DANFE, 'X') Danfe, A.RESERVA, C.CGC FROM REDEX.TB_AGENDAMENTO_WEB_CS_NF A INNER JOIN REDEX.TB_AGENDAMENTO_WEB_CS B ON A.AUTONUM_AGENDAMENTO = B.AUTONUM INNER JOIN OPERADOR.TB_CAD_TRANSPORTADORAS C ON B.AUTONUM_TRANSPORTADORA = C.AUTONUM WHERE A.AUTONUM = " & ID
        End If

        If Tipo = "2" Then
            SQL = "SELECT A.ARQUIVO_DANFE, NVL(A.DANFE, 'X') Danfe, A.RESERVA, C.CGC FROM REDEX.TB_AGENDAMENTO_WEB_CNTR_NF A INNER JOIN REDEX.TB_GD_CONTEINER B ON A.AUTONUM_AGENDAMENTO = B.AUTONUM_GD_CNTR INNER JOIN OPERADOR.TB_CAD_TRANSPORTADORAS C ON B.AUTONUM_TRANSPORTADORA = C.AUTONUM WHERE A.AUTONUM = " & ID
        End If

        Dim NotaFiscal = Banco.List(SQL)

        If NotaFiscal IsNot Nothing Then

            If NotaFiscal.Rows.Count = 0 Then
                Response.Redirect("Default.aspx")
            End If

            If NotaFiscal.Rows(0)("CGC") <> Session("SIS_CNPJ").ToString() Then
                Response.Redirect("Default.aspx")
            End If

            Dim Xml = NotaFiscal.Rows(0)("ARQUIVO_DANFE").ToString()

            If Xml <> "X" Then

                HttpContext.Current.Response.Clear()
                HttpContext.Current.Response.ContentType = "text/xml"
                HttpContext.Current.Response.AddHeader("Content-Disposition:",
                    "attachment;filename=" + HttpUtility.UrlEncode(NotaFiscal.Rows(0)("DANFE").ToString() & ".xml"))
                HttpContext.Current.Response.Write(Xml)
                HttpContext.Current.Response.End()

            End If

        End If

    End Sub

End Class