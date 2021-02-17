Public Class Site
    Inherits System.Web.UI.MasterPage

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not Session("LOGADO") Or Session("LOGADO") Is Nothing Then
            Response.Redirect("http://op.ecoportosantos.com.br/icc/")
        Else
            If Session("SIS_AUTONUM_TRANSPORTADORA") Is Nothing Then
                Response.Redirect("http://op.ecoportosantos.com.br/icc/")
            End If
        End If

        Me.lblVersao.Text = "v.27.03.2018"

    End Sub

End Class