Public Class CadastrarUsuarios
    Inherits System.Web.UI.Page

    Dim UsuarioOBJ As New Usuario
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

    Protected Sub btSalvar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btSalvar.Click

        UsuarioOBJ.CNPJ = txtCPF.Text

        If rbAdmin.Items(0).Selected = True Then
            UsuarioOBJ.Admin = True
        Else
            UsuarioOBJ.Admin = False
        End If

        If rbAtivo.Items(0).Selected = True Then
            UsuarioOBJ.Ativo = True
        Else
            UsuarioOBJ.Ativo = False
        End If

        UsuarioOBJ.Nome = txtNome.Text
        UsuarioOBJ.Email = txtEmail.Text

        If Not UsuarioBLL.ValidarUsuario(UsuarioOBJ) Then
            If UsuarioBLL.InserirUsuario(UsuarioOBJ) Then
                PanelMsg.Visible = True
                lblMsgErro.Text = String.Empty
                lblMsgOK.Text = "Registro salvo com sucesso! A senha de acesso será o CPF sem traço e pontos."
            Else
                PanelMsg.Visible = True
                lblMsgOK.Text = String.Empty
                lblMsgErro.Text = "Erro: Falha durante o cadastro do Usuário. Tente novamente."
            End If
        Else
            PanelMsg.Visible = True
            lblMsgOK.Text = String.Empty
            lblMsgErro.Text = "Erro: Já existe um Usuário cadastrado com os dados informados."
        End If        

    End Sub

    Protected Sub btRetornar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btRetornar.Click
        Response.Redirect("ConsultarUsuarios.aspx")
    End Sub

    Protected Sub btLimpar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btLimpar.Click
        txtCPF.Text = String.Empty
        txtEmail.Text = String.Empty
        txtNome.Text = String.Empty
        rbAdmin.Items.Clear()
        rbAtivo.Items.Clear()
    End Sub
End Class