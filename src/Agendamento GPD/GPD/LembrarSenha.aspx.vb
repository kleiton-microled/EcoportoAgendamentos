Public Class LembrarSenha
    Inherits System.Web.UI.Page

    Dim UsuarioBLL As New Usuario

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not Page.IsPostBack Then
            CarregarDadosTransportadora()
        End If

    End Sub

    Private Sub CarregarDadosTransportadora()
        txtIDTransportadora.Text = Session("SIS_ID").ToString()
        txtTransportadora.Text = Session("SIS_RAZAO").ToString()
    End Sub

    Protected Sub btLimpar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btLimpar.Click
        txtCPF.Text = String.Empty
    End Sub

    Protected Sub btEnviar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btEnviar.Click

        If UsuarioBLL.LembrarSenha(txtCPF.Text) Then
            PanelMsg.Visible = True
            lblMsgErro.Text = String.Empty
            lblMsgOK.Text = "Email enviado. Dentro de alguns instantes você receberá seu login e senha no e-mail."
            btEnviar.Enabled = False
        Else
            PanelMsg.Visible = True
            lblMsgOK.Text = String.Empty
            lblMsgErro.Text = "Erro: Falha durante o Envio. Tente novamente."
        End If

    End Sub

End Class