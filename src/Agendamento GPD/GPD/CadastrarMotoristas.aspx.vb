Imports System.Globalization

Public Class CadastrarMotoristas
    Inherits System.Web.UI.Page

    Dim Motorista As New Motorista
    Dim Veiculo As New Veiculo
    Dim WebService As New WebService

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not Page.IsPostBack Then
            txtIDTransportadora.Text = Session("SIS_ID").ToString()
            txtTransportadora.Text = Session("SIS_RAZAO").ToString()
        End If

    End Sub

    Protected Sub btSalvar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btSalvar.Click

        Dim TransportadoraOBJ As New Transportadora
        TransportadoraOBJ.ID = txtIDTransportadora.Text

        Try
            Motorista.Validade = Convert.ToDateTime(txtValidadeCNH.Text).ToString("dd/MM/yyyy")
        Catch ex As Exception
            PanelMsg.Visible = True
            lblMsgOK.Text = String.Empty
            lblMsgErro.Text = "Data de Validade CNH (" & txtValidadeCNH.Text & ") é inválida"
            Exit Sub
        End Try

        Motorista.CNH = txtCNH.Text
        Motorista.Nome = txtNome.Text.Trim.ToUpper().Replace("'", "")
        Motorista.RG = txtRG.Text
        Motorista.CPF = txtCPF.Text
        Motorista.Celular = txtCelular.Text.Replace("_", "")
        Motorista.Nextel = txtNextel.Text.Replace("'", "")
        Motorista.Transportadora = TransportadoraOBJ

        If Not txtNumeroMOP.Text = String.Empty Then
            Motorista.NumeroMOP = txtNumeroMOP.Text
        Else
            Motorista.NumeroMOP = "0"
        End If

        If Validar() Then

            If Motorista.ConsultarDadosIguais(txtCNH.Text, txtIDTransportadora.Text) Then
                PanelMsg.Visible = True
                lblMsgOK.Text = String.Empty
                lblMsgErro.Text = "Erro: Já existe um motorista cadastrado com os dados informados."
                Exit Sub
            End If

            If Motorista.Inserir(Motorista) Then
                If Motorista.AlterarCPFporCNH(Motorista) > -1 Then
                    PanelMsg.Visible = True
                    lblMsgErro.Text = String.Empty
                    lblMsgOK.Text = "Registro salvo com sucesso!"
                    btSalvar.Enabled = False
                Else
                    PanelMsg.Visible = True
                    lblMsgOK.Text = String.Empty
                    lblMsgErro.Text = "Erro: Falha ao alterar CPF do Motorista para esta CNH. Tente novamente."
                End If
            Else
                PanelMsg.Visible = True
                lblMsgOK.Text = String.Empty
                lblMsgErro.Text = "Erro: Falha durante o cadastro do Motorista. Tente novamente."
            End If

            End If

    End Sub

    Private Function Validar() As Boolean

        PanelMsg.Visible = False
        lblMsgOK.Text = String.Empty

        If Not txtValidadeCNH.Text = String.Empty Then
            If IsDate(txtValidadeCNH.Text) Then
                If Convert.ToDateTime(txtValidadeCNH.Text, New CultureInfo("pt-BR")).Date < Convert.ToDateTime(Now, New CultureInfo("pt-BR")).AddDays(-30).Date Then
                    lblMsgErro.Text = "Erro: Data Inválida. A Validade da CNH deverá ser no mínimo 30 dias inferior à data atual"
                    PanelMsg.Visible = True
                    Return False
                End If
            End If
        End If

        If txtCPF.Text.Replace(".", "").Replace("-", "").Replace("_", "").Length <> 11 Then
            lblMsgErro.Text = "Preencha o campo CPF corretamente"
            PanelMsg.Visible = True
            Return False
        End If

        If Not ValidaCPF.Validar(Me.txtCPF.Text) Then
            lblMsgErro.Text = "CPF inválido"
            PanelMsg.Visible = True
            Return False
        End If

        If txtCelular.Text.Replace("(", "").Replace(")", "").Replace("_", "").Replace(" ", "").Replace("-", "").Length <> 11 Then
            lblMsgErro.Text = "Preencha o campo Celular corretamente"
            PanelMsg.Visible = True
            Return False
        End If

        If txtRG.Text.Length < 5 Then
            lblMsgErro.Text = "O campo RG precisa ter no mínimo 5 caracteres"
            PanelMsg.Visible = True
            Return False
        End If

        If txtNextel.Text.Length < 3 Then
            lblMsgErro.Text = "O campo Nextel precisa ter no mínimo 3 caracteres"
            PanelMsg.Visible = True
            Return False
        End If

        Return True

    End Function

    Private Sub ConsultarCNH(ByVal Motorista As Motorista)

        Dim MotoristaOBJ As New Motorista
        MotoristaOBJ = Motorista.ConsultarCNH(Motorista.CNH)

        If Not MotoristaOBJ Is Nothing Then

            If Not MotoristaOBJ.CNH = String.Empty Then
                txtCNH.Text = MotoristaOBJ.CNH
                txtCNH.Enabled = False
            End If

            If Not MotoristaOBJ.Validade = String.Empty Then
                If IsDate(MotoristaOBJ.Validade) Then
                    txtValidadeCNH.Text = Convert.ToDateTime(MotoristaOBJ.Validade, New CultureInfo("pt-BR"))
                    txtValidadeCNH.Enabled = True
                End If
            End If

            If Not MotoristaOBJ.Nome = String.Empty Then
                txtNome.Text = MotoristaOBJ.Nome
                txtNome.Enabled = False
            End If

            If Not MotoristaOBJ.RG = String.Empty Then
                txtRG.Text = MotoristaOBJ.RG
                txtRG.Enabled = True
            End If

            If Not MotoristaOBJ.CPF = String.Empty Then
                txtCPF.Text = MotoristaOBJ.CPF
                txtCPF.Enabled = False
            End If

        End If

    End Sub

    Protected Sub btConsultarCNH_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btConsultarCNH.Click

        If Not txtCNH.Text = String.Empty Then
            Motorista.CNH = txtCNH.Text
            ConsultarCNH(Motorista)
        End If

    End Sub

    Protected Sub btRetornar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btRetornar.Click
        Response.Redirect("ConsultarMotoristas.aspx")
    End Sub

    Protected Sub btLimpar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btLimpar.Click

        txtCNH.Text = String.Empty
        txtValidadeCNH.Text = String.Empty
        txtNome.Text = String.Empty
        txtRG.Text = String.Empty
        txtCPF.Text = String.Empty
        txtCelular.Text = String.Empty
        txtNextel.Text = String.Empty
        txtNumeroMOP.Text = String.Empty

        txtCNH.Enabled = True
        txtValidadeCNH.Enabled = True
        txtNome.Enabled = True
        txtRG.Enabled = True
        txtCPF.Enabled = True
        txtCelular.Enabled = True
        txtNextel.Enabled = True

        lblMsgOK.Text = String.Empty
        lblMsgErro.Text = String.Empty

        btSalvar.Enabled = True

    End Sub

    Protected Sub txtCNH_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles txtCNH.TextChanged

        If Not txtCNH.Text = String.Empty Then
            Motorista.CNH = txtCNH.Text
            ConsultarCNH(Motorista)
        End If

    End Sub

End Class