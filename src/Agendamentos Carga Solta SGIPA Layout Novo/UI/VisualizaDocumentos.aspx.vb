Imports System.Data.OleDb
Imports System.IO

Public Class VisualizaDocumentos
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Request.QueryString("id") IsNot Nothing Then
            If Request.QueryString("lote") IsNot Nothing Then
                ReceberArquivo(Request.QueryString("lote").ToString(), Request.QueryString("id").ToString())
            End If
        End If

    End Sub

    Private Sub ReceberArquivo(ByVal Lote As String, ByVal ID As String)

        Dim Ws As New WsSharepoint.WsIccSharepoint

        Try
            Dim imagemBytes As [Byte]()

            imagemBytes = DirectCast(Ws.ObterImagemDocAverbacaoPorLoteEautonum(Lote, Val(ID)), [Byte]())

            Dim ds As New DataTable

            ds = Consultar(ID)

            If ds IsNot Nothing Then
                If ds.Rows.Count > 0 Then

                    If imagemBytes IsNot Nothing Then

                        Using imagemStream As Stream = New MemoryStream(imagemBytes)

                            Select Case FileValidation.getMimeFromFile(imagemBytes)
                                Case "application/pdf"
                                    Response.ContentType = "application/pdf"
                                    Response.AddHeader("Content-Disposition", String.Format("inline; filename={0}.pdf", ds.Rows(0)("NOME_IMG").ToString()))
                                    Response.BinaryWrite(imagemBytes)
                                Case "image/jpeg"
                                    Response.ContentType = "image/jpeg"
                                    Response.BinaryWrite(imagemBytes)
                                Case "image/pjpeg"
                                    Response.ContentType = "image/pjpeg"
                                    Response.BinaryWrite(imagemBytes)
                                Case "image/png"
                                    Response.ContentType = "image/png"
                                    Response.BinaryWrite(imagemBytes)
                                Case "image/x-png"
                                    Response.ContentType = "image/x-png"
                                    Response.BinaryWrite(imagemBytes)
                                Case "image/gif"
                                    Response.ContentType = "image/gif"
                                    Response.BinaryWrite(imagemBytes)
                            End Select

                        End Using

                    End If
                Else
                    Response.Write("Ops! Esse recurso não foi encontrado! :(")
                End If
            End If

        Catch ex As Exception
            Response.Write("Ops! Erro! :(")
        End Try

    End Sub

    Public Function Consultar(ByVal ID As String) As DataTable

        Dim Rst As New ADODB.Recordset
        Dim SQL As New StringBuilder

        SQL.Append("SELECT NOME_IMG FROM SGIPA.TB_AV_IMAGEM WHERE AUTONUM = " & ID)

        Rst.Open(SQL.ToString(), Banco.Conexao, 3, 3)

        Using Adapter As New OleDbDataAdapter()
            Dim Ds As New DataSet
            Adapter.Fill(Ds, Rst, "TB_AV_IMAGEM")
            Return Ds.Tables(0)
        End Using

    End Function

End Class