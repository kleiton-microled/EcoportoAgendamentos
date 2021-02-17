Imports System.Globalization

Public Class AlterarMotoristas
    Inherits System.Web.UI.Page

    Dim Motorista As New Motorista
    Dim Veiculo As New Veiculo
    Dim WebService As New WebService

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not Page.IsPostBack Then
            If Request.QueryString("ID") IsNot Nothing Then
                txtID.Text = Request.QueryString("ID").ToString()
                Motorista.ID = txtID.Text
                CarregarDadosTransportadora()
                CarregarDados()
            End If
        End If

    End Sub

    Private Sub CarregarDadosTransportadora()
        txtIDTransportadora.Text = Session("SIS_ID").ToString()
        txtTransportadora.Text = Session("SIS_RAZAO").ToString()
    End Sub

    Private Sub CarregarDados()

        Dim MotoristaOBJ As Motorista = Motorista.ConsultarPorID(Motorista.ID)

        If Not Motorista Is Nothing Then

            txtCNH.Text = MotoristaOBJ.CNH
            txtValidadeCNH.Text = MotoristaOBJ.Validade
            txtNome.Text = MotoristaOBJ.Nome
            txtRG.Text = MotoristaOBJ.RG
            txtCPF.Text = MotoristaOBJ.CPF
            txtCelular.Text = MotoristaOBJ.Celular
            txtNextel.Text = MotoristaOBJ.Nextel
            txtNumeroMOP.Text = MotoristaOBJ.NumeroMOP

        End If

    End Sub

    Protected Sub btSalvar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btSalvar.Click

        Dim TransportadoraOBJ As New Transportadora
        TransportadoraOBJ.ID = txtIDTransportadora.Text

        Motorista.ID = txtID.Text
        Motorista.CNH = txtCNH.Text
        Motorista.Nome = txtNome.Text.Trim.ToUpper()
        Motorista.RG = txtRG.Text
        Motorista.CPF = txtCPF.Text
        Motorista.Celular = txtCelular.Text
        Motorista.Nextel = txtNextel.Text
        Motorista.Transportadora = TransportadoraOBJ

        Motorista.Validade = txtValidadeCNH.Text

        If Not txtNumeroMOP.Text = String.Empty Then
            Motorista.NumeroMOP = txtNumeroMOP.Text
        Else
            Motorista.NumeroMOP = "0"
        End If

        If Validar() Then

            If Motorista.ConsultarDadosIguais(txtCNH.Text, txtIDTransportadora.Text, txtID.Text) Then
                PanelMsg.Visible = True
                lblMsgOK.Text = String.Empty
                lblMsgErro.Text = "Erro: Já existe um motorista cadastrado com os dados informados."
                Exit Sub
            End If

            If Motorista.Alterar(Motorista) Then
                If Motorista.AlterarCPFporCNH(Motorista) > -1 Then
                    PanelMsg.Visible = True
                    lblMsgErro.Text = String.Empty
                    lblMsgOK.Text = "Registro alterado com sucesso!"
                Else
                    PanelMsg.Visible = True
                    lblMsgOK.Text = String.Empty
                    lblMsgErro.Text = "Erro: Falha ao alterar CPF do Motorista para esta CNH. Tente novamente."
                End If
            Else
                PanelMsg.Visible = True
                lblMsgOK.Text = String.Empty
                lblMsgErro.Text = "Erro: Falha durante a alteração do Motorista. Tente novamente."
            End If

            End If

    End Sub

    Private Function Validar() As Boolean

        If Not txtValidadeCNH.Text = String.Empty Then
            If IsDate(txtValidadeCNH.Text) Then
                If Convert.ToDateTime(txtValidadeCNH.Text, New CultureInfo("pt-BR")).Date < Convert.ToDateTime(Now, New CultureInfo("pt-BR")).AddDays(-60).Date Then
                    PanelMsg.Visible = True
                    lblMsgOK.Text = String.Empty
                    lblMsgErro.Text = "Erro: Data Inválida. A Validade da CNH deverá ser no mínimo 60 dias inferior à data atual"
                    Return False
                End If
            End If
        End If

        Return True

    End Function

    Protected Sub btExcluir_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btExcluir.Click

        If Not txtID.Text = String.Empty Then

            Dim TransportadoraOBJ As New Transportadora
            TransportadoraOBJ.ID = txtIDTransportadora.Text

            Motorista.ID = txtID.Text
            Motorista.Transportadora = TransportadoraOBJ

            If Motorista.ValidarExclusao(Motorista) Then
                If Motorista.Excluir(Motorista) Then

                    PanelMsg.Visible = True
                    lblMsgErro.Text = String.Empty
                    lblMsgOK.Text = "Registro excluído com sucesso!"

                    txtCelular.Text = String.Empty
                    txtCNH.Text = String.Empty
                    txtRG.Text = String.Empty
                    txtCPF.Text = String.Empty
                    txtID.Text = String.Empty
                    txtNextel.Text = String.Empty
                    txtNome.Text = String.Empty
                    txtValidadeCNH.Text = String.Empty

                    txtCelular.Enabled = False
                    txtCNH.Enabled = False
                    txtRG.Enabled = False
                    txtCPF.Enabled = False
                    txtID.Enabled = False
                    txtNextel.Enabled = False
                    txtNome.Enabled = False
                    txtValidadeCNH.Enabled = False

                    btExcluir.Enabled = False
                    btSalvar.Enabled = False

                Else
                    PanelMsg.Visible = True
                    lblMsgOK.Text = String.Empty
                    lblMsgErro.Text = "Erro: Falha durante a exclusão do Motorista. Tente novamente."
                End If

            Else
                PanelMsg.Visible = True
                lblMsgOK.Text = String.Empty
                lblMsgErro.Text = "Erro: Operação não permitida. Existem Agendamentos vinculados a esse Motorista."
            End If

        End If

    End Sub

    Protected Sub btRetornar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btRetornar.Click
        Response.Redirect("ConsultarMotoristas.aspx")
    End Sub
End Class