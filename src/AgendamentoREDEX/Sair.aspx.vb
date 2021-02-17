Public Class Sair
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Session.RemoveAll()
        Session.Abandon()

        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgAlerta", "document.location.href='http://op.ecoportosantos.com.br/icc/';", True)

    End Sub

End Class