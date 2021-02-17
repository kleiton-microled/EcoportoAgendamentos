Imports System.Globalization

Public Class CadastrarMotoristas
    Inherits System.Web.UI.Page

    Dim Motorista As New Motorista
    Dim Veiculo As New Veiculo
    
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not Page.IsPostBack Then
            txtIDTransportadora.Text = Session("SIS_AUTONUM_TRANSPORTADORA").ToString()
            txtTransportadora.Text = Session("SIS_RAZAO").ToString()
        End If

    End Sub

    Protected Sub btSalvar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btSalvar.Click

        Dim TransportadoraOBJ As New Transportadora
        TransportadoraOBJ.ID = txtIDTransportadora.Text

        Motorista.Validade = txtValidadeCNH.Text

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
                PanelMsg.Visible = True
                lblMsgErro.Text = String.Empty
                lblMsgOK.Text = "Registro salvo com sucesso!"
                btSalvar.Enabled = False
            Else
                PanelMsg.Visible = True
                lblMsgOK.Text = String.Empty
                lblMsgErro.Text = "Erro: Falha durante o cadastro do Motorista. Tente novamente."
            End If

        End If

    End Sub

    Private Function Validar() As Boolean

        If Not txtValidadeCNH.Text = String.Empty Then
            If IsDate(txtValidadeCNH.Text) Then
                If Convert.ToDateTime(txtValidadeCNH.Text, New CultureInfo("pt-BR")) < Convert.ToDateTime(Now, New CultureInfo("pt-BR")) Then
                    PanelMsg.Visible = True
                    lblMsgOK.Text = String.Empty
                    lblMsgErro.Text = "Erro: Data Inválida. A Validade da CNH deverá ser maior que a data atual."
                    Return False
                End If
            End If       
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