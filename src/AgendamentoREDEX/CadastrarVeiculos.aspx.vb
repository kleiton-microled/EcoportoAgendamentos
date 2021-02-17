Public Class CadastrarVeiculos
    Inherits System.Web.UI.Page

    Dim Veiculo As New Veiculo
    Dim Transportadora As New Transportadora

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not Page.IsPostBack Then
            CarregarDadosTransportadora()
            CarregarTiposCarreta()
        End If

    End Sub

    Private Sub CarregarDadosTransportadora()
        txtIDTransportadora.Text = Session("SIS_AUTONUM_TRANSPORTADORA").ToString()
        txtTransportadora.Text = Session("SIS_RAZAO").ToString()
    End Sub

    Protected Sub btSalvar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btSalvar.Click

        Transportadora.ID = txtIDTransportadora.Text
        Veiculo.Transportadora = Transportadora
        Veiculo.Cavalo = txtPlacaCavalo.Text.Trim.ToUpper()
        Veiculo.Carreta = txtPlacaCarreta.Text.Trim.ToUpper()
        Veiculo.Chassi = txtChassi.Text.Trim.ToUpper()
        Veiculo.Renavam = txtRenavam.Text.Trim.ToUpper()
        Veiculo.Tipo = cbTipoCarreta.SelectedValue
        Veiculo.Modelo = txtModelo.Text.Trim.ToUpper()
        Veiculo.Cor = txtCor.Text.Trim.ToUpper()

        If Not txtTaraVeiculo.Text = String.Empty Then
            Veiculo.Tara = txtTaraVeiculo.Text.Trim.ToUpper()
        Else
            Veiculo.Tara = "0"
        End If

        If Not IsNumeric(Me.txtRenavam.Text) Then
            PanelMsg.Visible = True
            lblMsgOK.Text = String.Empty
            lblMsgErro.Text = "Erro: O campo Renavam deve ser um valor numérico."
            Exit Sub
        End If

        If txtRenavam.Text = String.Empty Then
            ScriptManager.RegisterClientScriptBlock(Me, [GetType](), "script", "<script>alert('Informe o Renavam do Veículo.');</script>", False)
            Exit Sub
        End If

        If Veiculo.ValidarVeiculosIguais(Veiculo, False) Then
            If Veiculo.Inserir(Veiculo) Then
                PanelMsg.Visible = True
                lblMsgErro.Text = String.Empty
                lblMsgOK.Text = "Registro salvo com sucesso!"
            Else
                PanelMsg.Visible = True
                lblMsgOK.Text = String.Empty
                lblMsgErro.Text = "Erro: Falha durante o cadastro do veículo. Tente novamente."
            End If
        Else
            PanelMsg.Visible = True
            lblMsgOK.Text = String.Empty
            lblMsgErro.Text = "Erro: Já existe um veículo cadastrado com as placas informadas."
        End If

    End Sub

    Private Sub CarregarTiposCarreta()

        Me.cbTipoCarreta.DataTextField = "DESCR"
        Me.cbTipoCarreta.DataValueField = "AUTONUM"

        Me.cbTipoCarreta.DataSource = Veiculo.ConsultarTipos()
        Me.cbTipoCarreta.DataBind()

    End Sub

    Protected Sub btRetornar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btRetornar.Click
        Response.Redirect("ConsultarVeiculos.aspx")
    End Sub

End Class