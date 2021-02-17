Imports System.Drawing

Public Class BarCode

    Public Function CriarCodigoBarras(ByVal Codigo As String) As Bitmap

        Dim Code As String = Codigo
        Dim w As Integer = (Code.Length * 40)
        Dim oBitmap As Bitmap = New Bitmap(w, 100)
        Dim oGraphics As Graphics = Graphics.FromImage(oBitmap)
        Dim oFont As Font = New Font("IDAutomationHC39M_FREE", 18)
        Dim oPoint As PointF = New PointF(2.0!, 2.0!)
        Dim oBrushWrite As SolidBrush = New SolidBrush(Color.Black)
        Dim oBrush As SolidBrush = New SolidBrush(Color.White)
        oGraphics.FillRectangle(oBrush, 0, 0, w, 100)
        oGraphics.DrawString(("*" + (Code + "*")), oFont, oBrushWrite, oPoint)

        'Response.ContentType = "image/jpeg"
        'oBitmap.Save(Response.OutputStream, ImageFormat.Jpeg)

        Return oBitmap

    End Function

End Class
