Public Class Acesso
    Inherits System.Web.UI.Page

    Dim Login As New Login

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not Page.IsPostBack Then
            If Request.QueryString("ID") IsNot Nothing Then

                If Login.Acesso(Request.QueryString("ID")) IsNot Nothing Then

                    Dim Transportadora As Transportadora
                    Dim Usuario As New Usuario

                   Transportadora = Login.Acesso(Request.QueryString("ID"))
                    'Login.DeletarAcesso(Request.QueryString("ID"))

                    Session("SIS_TIAID") = Request.QueryString("ID")
                    Session("SIS_USRID") = Usuario.ObterIdUsuario(Request.QueryString("ID"))

                    Session("SIS_ID") = Transportadora.ID
                    Session("SIS_RAZAO") = Transportadora.Razao
                    Session("SIS_CNPJ") = Transportadora.CNPJ
                    Session("SIS_TRANSPEMPRESA") = Transportadora.Empresa

                    If Transportadora.Empresa = "1" Then
                        Session("SIS_TITULO") = "ECOPORTO - Agendamento de Carga Solta"
                        Session("SIS_CSS") = "css/ecoporto/ecoporto.css"
                        Session("SIS_CSS_UI") = "css/temas/ecoporto/ecoporto.css"
                        Session("SIS_CSS_ICONE") = "css/termares/images/favicon.ico"
                        Session("SIS_COR_PADRAO") = "#B3C63C"
                    Else
                        Session("SIS_TITULO") = "ECOPORTO - Recinto Alfandegado"
                        Session("SIS_CSS") = "css/termares/termares.css"
                        Session("SIS_CSS_UI") = "css/temas/ecoporto/ecoporto.css"
                        Session("SIS_CSS_ICONE") = "css/termares/images/favicon.ico"
                        Session("SIS_COR_PADRAO") = "#B3C63C"
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