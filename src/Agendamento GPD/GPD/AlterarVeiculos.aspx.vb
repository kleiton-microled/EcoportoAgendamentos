Public Class AlterarVeiculos
    Inherits System.Web.UI.Page

    Dim Veiculo As New Veiculo
    Dim WebService As New WebService

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not Page.IsPostBack Then
            If Request.QueryString("ID") IsNot Nothing Then

                Veiculo.ID = Request.QueryString("ID").ToString()
                txtID.Text = Request.QueryString("ID").ToString()

                CarregarDadosTransportadora()
                CarregarTiposCarreta()
                CarregarDados()

                Dim TransportadoraOBJ As New Transportadora
                TransportadoraOBJ.ID = txtIDTransportadora.Text
                Veiculo.Transportadora = TransportadoraOBJ

                If Veiculo.ValidarExclusao(Veiculo) Then
                    txtPlacaCavalo.Enabled = True
                    txtPlacaCarreta.Enabled = True
                End If

            End If
        End If

    End Sub

    Private Sub CarregarDadosTransportadora()
        txtIDTransportadora.Text = Session("SIS_ID").ToString()
        txtTransportadora.Text = Session("SIS_RAZAO").ToString()
    End Sub

    Private Sub CarregarDados()

        Dim Ds As New DataTable
        Ds = Veiculo.ConsultarPorID(Veiculo.ID)

        If Ds.Rows.Count > 0 Then
            txtPlacaCavalo.Text = Ds.Rows(0)("PLACA_CAVALO").ToString()
            txtPlacaCarreta.Text = Ds.Rows(0)("PLACA_CARRETA").ToString()
            txtTaraVeiculo.Text = Ds.Rows(0)("TARA").ToString()
            txtChassi.Text = Ds.Rows(0)("CHASSI").ToString()
            txtRenavam.Text = Ds.Rows(0)("RENAVAM").ToString()
            cbTipoCarreta.SelectedValue = Ds.Rows(0)("ID_TIPO_CAMINHAO").ToString()
            txtModelo.Text = Ds.Rows(0)("MODELO").ToString()
            txtCor.Text = Ds.Rows(0)("COR").ToString()
        End If

    End Sub

    Private Sub CarregarTiposCarreta()

        Me.cbTipoCarreta.DataTextField = "DESCR"
        Me.cbTipoCarreta.DataValueField = "AUTONUM"

        Me.cbTipoCarreta.DataSource = Veiculo.ConsultarTipos()
        Me.cbTipoCarreta.DataBind()

    End Sub

    Private Function ValidarBDCC(ByVal Renavam As String) As Boolean

        If Config.ValidaBDCC() Then

            If WebService.ValidarVeiculo(Renavam) Then
                If WebService.ValidarBDCC() Then

                    If WebService.TipoBDCC = 1 Then
                        PanelMsg.Visible = True
                        lblMsgOK.Text = "BDCC: " & WebService.DescricaoRetorno
                        lblMsgErro.Text = String.Empty
                    End If

                    If WebService.TipoBDCC = 2 Then
                        PanelMsg.Visible = True
                        lblMsgOK.Text = String.Empty
                        lblMsgErro.Text = "BDCC: " & WebService.DescricaoRetorno
                        Return False
                    End If

                End If
            End If

            Return True

        End If

        Return True

    End Function

    Protected Sub btSalvar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btSalvar.Click

        Dim TransportadoraOBJ As New Transportadora
        TransportadoraOBJ.ID = txtIDTransportadora.Text

        Veiculo.ID = txtID.Text
        Veiculo.Transportadora = TransportadoraOBJ
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

        If Not txtRenavam.Text = String.Empty Then
            If Not ValidarBDCC(txtRenavam.Text) Then
                Exit Sub
            End If
        End If

        If Veiculo.ValidarVeiculosIguais(Veiculo, True) Then
            If Veiculo.Alterar(Veiculo) Then
                PanelMsg.Visible = True
                lblMsgErro.Text = String.Empty
                lblMsgOK.Text = "Registro salvo com sucesso!"
            Else
                PanelMsg.Visible = True
                lblMsgOK.Text = String.Empty
                lblMsgErro.Text = "Erro: Falha durante a alteração do veículo. Tente novamente."
            End If
        Else
            PanelMsg.Visible = True
            lblMsgOK.Text = String.Empty
            lblMsgErro.Text = "Erro: Já existe um veículo cadastrado com as placas informadas."
        End If

    End Sub

    Protected Sub btRetornar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btRetornar.Click
        Response.Redirect("ConsultarVeiculos.aspx")
    End Sub

    Protected Sub btExcluir_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btExcluir.Click

        If Not txtID.Text = String.Empty Then

            Dim TransportadoraOBJ As New Transportadora
            TransportadoraOBJ.ID = Session("SIS_ID").ToString()

            Veiculo.ID = txtID.Text
            Veiculo.Transportadora = TransportadoraOBJ

            If Veiculo.ValidarExclusao(Veiculo) Then
                If Veiculo.Excluir(Veiculo) Then

                    PanelMsg.Visible = True
                    lblMsgErro.Text = String.Empty
                    lblMsgOK.Text = "Registro excluído com sucesso!"

                    cbTipoCarreta.Items.Insert(0, New ListItem("", ""))
                    cbTipoCarreta.SelectedIndex = 0

                    txtPlacaCavalo.Text = String.Empty
                    txtPlacaCarreta.Text = String.Empty
                    txtTaraVeiculo.Text = String.Empty
                    txtChassi.Text = String.Empty
                    txtRenavam.Text = String.Empty
                    txtModelo.Text = String.Empty
                    txtCor.Text = String.Empty

                    txtPlacaCavalo.Enabled = False
                    txtPlacaCarreta.Enabled = False
                    txtTaraVeiculo.Enabled = False
                    txtChassi.Enabled = False
                    txtRenavam.Enabled = False
                    cbTipoCarreta.Enabled = False

                    btExcluir.Enabled = False
                    btSalvar.Enabled = False

                Else
                    PanelMsg.Visible = True
                    lblMsgOK.Text = String.Empty
                    lblMsgErro.Text = "Erro: Falha durante a exclusão do Veículo. Tente novamente."
                End If
            Else
                PanelMsg.Visible = True
                lblMsgOK.Text = String.Empty
                lblMsgErro.Text = "Erro: Operação não permitida. Este veículo está vinclulado a um motorista cadastrado."
            End If

        End If

    End Sub
End Class