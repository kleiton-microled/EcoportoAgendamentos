Public Class Acesso
    Inherits System.Web.UI.Page

    Dim Login As New Login

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not Page.IsPostBack Then
            If Request.QueryString("ID") IsNot Nothing Then

                If Login.EfetuarLogin(Request.QueryString("ID")) IsNot Nothing Then

                    Dim Usuario As Usuario
                    Usuario = Login.EfetuarLogin(Request.QueryString("ID"))
                    'Login.DeletarAcesso(Request.QueryString("ID"))

                    Session("SIS_ID") = Usuario.Codigo
                    Session("SIS_RAZAO") = Usuario.Nome
                    Session("SIS_CNPJ") = Usuario.CNPJ
                    Session("USRID") = Usuario.USRID
                    Session("SIS_TIPO") = Usuario.Tipo
                    Session("SIS_EMPRESA") = Usuario.Empresa
                    Session("SIS_FLAG_TRANSPORTADOR") = Usuario.FlagTransportador

                    Session("SIS_TITULO") = "ECOPORTO - Gate por Demanda"
                    Session("SIS_COD_EMPRESA") = "1"
                    Session("SIS_CSS_ICONE") = "css/tecondi/images/favicon.ico"
                    Session("SIS_COR_PADRAO") = "#B3C63C"

                    If Usuario.Empresa = 1 Then
                        Session("SIS_CSS") = "css/tecondi/tecondi.css"
                        Session("SIS_CSS_UI") = "css/temas/tecondi/tecondi.css"
                    Else
                        Session("SIS_CSS") = "css/termares/termares.css"
                        Session("SIS_CSS_UI") = "css/temas/termares/termares.css"
                    End If

                    Response.Redirect("Principal.aspx")

                Else
                    Response.Redirect("/ICC/")
                End If
            Else
                Response.Redirect("/ICC/")
            End If

        End If

    End Sub

End Class