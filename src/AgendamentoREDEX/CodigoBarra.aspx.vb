Imports System.Drawing
Imports System.Drawing.Imaging

Public Class CodigoBarra
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Request.QueryString("protocolo") IsNot Nothing Then

            Dim BarCode As String
            Dim Code As String = Request.QueryString("protocolo").ToString()
            Dim w As Integer = (Code.Length * 40)
            Dim oBitmap As Bitmap = New Bitmap(w, 100)
            Dim oGraphics As Graphics = Graphics.FromImage(oBitmap)
            Dim oFont As Font = New Font("IDAutomationHC39M", 14)
            Dim oPoint As PointF = New PointF(2.0!, 2.0!)
            Dim oBrushWrite As SolidBrush = New SolidBrush(Color.Black)
            Dim oBrush As SolidBrush = New SolidBrush(Color.White)
            oGraphics.FillRectangle(oBrush, 0, 0, w, 100)

            If Request.QueryString("modo").ToString() = "E" Then
                BarCode = "*" + (Code + "*")
            ElseIf Request.QueryString("modo").ToString() = "R" Then
                BarCode = "T" + Code
            End If
            oGraphics.DrawString(BarCode, oFont, oBrushWrite, oPoint)

            Response.ContentType = "image/jpeg"
            oBitmap.Save(Response.OutputStream, ImageFormat.Jpeg)

        End If

    End Sub

End Class