Public Class AgendamentoExportacao
    Inherits System.Web.UI.Page

    Dim Exportacao As New Exportacao
    Dim Conteiner As New ConteinerExportacao
    Dim Motorista As New Motorista
    Dim Veiculo As New Veiculo

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not Page.IsPostBack Then

            txtIDTransportadora.Text = Session("SIS_ID").ToString()
            txtTransportadora.Text = Session("SIS_RAZAO").ToString()

            CarregarVeiculos()
            CarregarMotoristas()
            CarregarCNTR()

            dgConsulta.HeaderStyle.BackColor = System.Drawing.ColorTranslator.FromHtml(Session("SIS_COR_PADRAO").ToString())
            dgConsulta.PagerStyle.BackColor = System.Drawing.ColorTranslator.FromHtml(Session("SIS_COR_PADRAO").ToString())

            Me.cbVeiculos.Items.Insert(0, New ListItem("SELECIONE...", 0))
            Me.cbMotoristas.Items.Insert(0, New ListItem("SELECIONE...", 0))
            Me.cbConteiner.Items.Insert(0, New ListItem("SELECIONE...", 0))

            ViewState("UrlReferrer") = Request.UrlReferrer

            If Not Request.QueryString("conteiner") Is Nothing And Not Request.QueryString("action") Is Nothing Then
                Editar()
            End If

        End If

    End Sub

    Private Sub Editar()

        Dim ID As String = String.Empty
        Dim Motorista As String = String.Empty
        Dim ID_Periodo As String = String.Empty
        Dim Periodo As String = String.Empty
        Dim Conteiner As String = String.Empty
        Dim Reserva As String = String.Empty
        Dim Veiculo As String = String.Empty

        If Not Request.QueryString("id") Is Nothing Then
            ID = Request.QueryString("id").ToString()
        End If

        If Not Request.QueryString("motorista") Is Nothing Then
            Motorista = Request.QueryString("motorista").ToString()
        End If

        If Not Request.QueryString("id_periodo") Is Nothing Then
            ID_Periodo = Request.QueryString("id_periodo").ToString()
        End If

        If Not Request.QueryString("periodo") Is Nothing Then
            Periodo = Request.QueryString("periodo").ToString()
        End If

        If Not Request.QueryString("conteiner") Is Nothing Then
            Conteiner = Request.QueryString("conteiner").ToString()
        End If

        If Not Request.QueryString("reserva") Is Nothing Then
            Reserva = Request.QueryString("reserva").ToString()
        End If

        If Not Request.QueryString("veiculo") Is Nothing Then
            Veiculo = Request.QueryString("veiculo").ToString()
        End If

        lblIDAgendamento.Text = ID
        lblIDPeriodo.Text = ID_Periodo
        lblPeriodo.Text = "Período: " & Server.UrlDecode(Periodo)
        cbVeiculos.SelectedValue = Server.UrlDecode(Veiculo)
        cbMotoristas.SelectedValue = Server.UrlDecode(Motorista)

        If Request.QueryString("agendado") Is Nothing Then
            cbConteiner.Text = Conteiner
            cbConteiner.SelectedValue = Conteiner
        Else
            cbConteiner.DataSource = Exportacao.ObterCodigoConteiner(Conteiner.Substring(0, 12))
            cbConteiner.DataBind()
        End If

        cbConteiner.Enabled = False

        CarregarPeriodos(Reserva)
        pnPeriodo.Visible = True

        If Not ID = String.Empty Then
            btAgendar.Enabled = True
        End If

    End Sub

    Private Sub CarregarVeiculos()
        Me.cbVeiculos.DataSource = Exportacao.ConsultarVeiculos(txtIDTransportadora.Text)
        Me.cbVeiculos.DataBind()
    End Sub

    Private Sub CarregarMotoristas()
        Me.cbMotoristas.DataSource = Exportacao.ConsultarMotoristas(txtIDTransportadora.Text)
        Me.cbMotoristas.DataBind()
    End Sub

    Private Sub CarregarCNTR()
        Me.cbConteiner.DataSource = Conteiner.ConsultarConteineres(txtIDTransportadora.Text)
        Me.cbConteiner.DataBind()
    End Sub

    Private Sub CarregarPeriodos(ByVal Reserva As String)

        Dim ID As Integer = 0

        If Request.QueryString("id") IsNot Nothing Then
            ID = Convert.ToInt32(Request.QueryString("id").ToString())
        Else
            ID = cbConteiner.SelectedValue
        End If

        If Exportacao.IsLate(Reserva) Then
            Me.dgConsulta.DataSource = Exportacao.ConsultarPeriodos(ID, Reserva, True)
        Else
            Me.dgConsulta.DataSource = Exportacao.ConsultarPeriodos(ID, Reserva, False)
        End If

        Me.dgConsulta.DataBind()

    End Sub

    Private Function ValidarBDCC(ByVal CPF As String, ByVal ExibeMensBDCC As Boolean) As Boolean

        Dim WebServiceOBJ As New WebService

        If Not My.Settings.GPD_WebServiceTecondi_WsSincrono.ToString() = String.Empty Then

            Dim Autonomo As Integer = 0

            If chAutonomo.Checked Then
                Autonomo = 1
            End If

            If WebServiceOBJ.ValidarMotorista(CPF.Replace(".", "").Replace("-", ""), Session("SIS_CNPJ").ToString(), Autonomo) Then
                If WebServiceOBJ.ValidarBDCC() Then

                    lblMsgOK.Text = String.Empty
                    lblMsgErro.Text = String.Empty

                    If WebServiceOBJ.TipoBDCC = 1 Then
                        PanelMsg.Visible = True
                        If ExibeMensBDCC Then
                            lblMsgOK.Text = "BDCC: " & WebServiceOBJ.DescricaoRetorno
                        End If
                        lblMsgErro.Text = String.Empty
                    End If

                    If WebServiceOBJ.TipoBDCC = 2 Then
                        PanelMsg.Visible = True
                        lblMsgOK.Text = String.Empty
                        If ExibeMensBDCC Then
                            lblMsgErro.Text = "BDCC: " & WebServiceOBJ.DescricaoRetorno
                        End If
                        Return False
                    End If

                    Return True

                End If
            End If

            Return True

        End If

        Return True

    End Function

    Protected Sub btAgendar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btAgendar.Click

        If cbVeiculos.SelectedValue = 0 Then
            PanelMsg.Visible = True
            lblMsgErro.Text = String.Empty
            lblMsgErro.Text = "Selecione um Veículo."
            Exit Sub
        End If

        If cbMotoristas.SelectedValue = 0 Then
            PanelMsg.Visible = True
            lblMsgErro.Text = String.Empty
            lblMsgErro.Text = "Selecione um Motorista."
            Exit Sub
        End If

        If cbConteiner.SelectedValue = 0 Then
            PanelMsg.Visible = True
            lblMsgErro.Text = String.Empty
            lblMsgErro.Text = "Selecione um Contêiner."
            Exit Sub
        End If

        If lblIDPeriodo.Text = String.Empty Then
            PanelMsg.Visible = True
            lblMsgErro.Text = String.Empty
            lblMsgErro.Text = "Selecione um Período."
            Exit Sub
        End If

        If Not cbVeiculos.Text = String.Empty Then

            Dim CPF As String = Motorista.ObterCPFMotorista(cbVeiculos.SelectedItem.Text.Split("-")(1).Trim)

            If Not ValidarBDCC(CPF, False) Then
                Exit Sub
            End If

        End If

        If lblIDAgendamento.Text = String.Empty Then

            If Exportacao.Agendar(cbConteiner.SelectedValue, cbMotoristas.SelectedValue, cbVeiculos.SelectedValue, lblIDPeriodo.Text, Session("USRID").ToString()) Then

                PanelMsg.Visible = True
                lblMsgErro.Text = String.Empty
                lblMsgOK.Text = "Agendamento efetuado com sucesso!"

                cbVeiculos.Enabled = False
                cbMotoristas.Enabled = False
                cbConteiner.Enabled = False
                btAgendar.Enabled = False

                chAutonomo.Enabled = False

            Else
                PanelMsg.Visible = True
                lblMsgOK.Text = String.Empty
                lblMsgErro.Text = "Erro: Falha durante o Agendamento. Tente novamente."
                Exit Sub
            End If

        Else

            If Exportacao.AlterarAgendamento(lblIDAgendamento.Text, cbMotoristas.SelectedValue, cbVeiculos.SelectedValue, lblIDPeriodo.Text, Session("USRID").ToString()) Then

                PanelMsg.Visible = True
                lblMsgErro.Text = String.Empty
                lblMsgOK.Text = "Agendamento alterado com sucesso!"

                cbVeiculos.Enabled = False
                cbMotoristas.Enabled = False
                cbConteiner.Enabled = False
                btAgendar.Enabled = False

                chAutonomo.Enabled = False

            Else
                PanelMsg.Visible = True
                lblMsgOK.Text = String.Empty
                lblMsgErro.Text = "Erro: Falha durante a alteração. Tente novamente."
                Exit Sub
            End If

        End If

    End Sub

    Protected Sub dgConsulta_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles dgConsulta.RowCommand

        Dim Index As Integer = e.CommandArgument
        Dim ID As String = dgConsulta.DataKeys(Index)("AUTONUM_GD_RESERVA").ToString()
        Dim Periodo As String = dgConsulta.DataKeys(Index)("PERIODO_INICIAL").ToString() & " - " & dgConsulta.DataKeys(Index)("PERIODO_FINAL").ToString()
        Dim Saldo As String = dgConsulta.DataKeys(Index)("SALDO").ToString()

        If Not ID = String.Empty Or Not ID = 0 Then
            If e.CommandName = "CHECK" Then

                For Each Linha As GridViewRow In Me.dgConsulta.Rows
                    Linha.BackColor = Drawing.Color.White
                Next

                lblIDPeriodo.Text = ID
                lblPeriodo.Text = "Período Selecionado: " & Periodo
                pnPeriodo.Visible = True

                Me.dgConsulta.Rows(Index).BackColor = System.Drawing.ColorTranslator.FromHtml("#A2CD5A")

            End If
        End If

    End Sub

    Protected Sub btRetornar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btRetornar.Click
        Response.Redirect(ViewState("UrlReferrer").ToString() & "")
    End Sub

    Protected Sub cbConteiner_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cbConteiner.SelectedIndexChanged

        If Not cbConteiner.SelectedValue = 0 Then
            CarregarPeriodos(Exportacao.ObterReservaPorConteiner(cbConteiner.SelectedItem.Text))
        End If

    End Sub

    Protected Sub cbMotoristas_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbMotoristas.SelectedIndexChanged

        If cbMotoristas.SelectedValue <> 0 Then

                Dim CPF As String = Motorista.ObterCPFMotorista(cbMotoristas.SelectedItem.Text.Split("-")(1).Trim)

            If Not ValidarBDCC(CPF, True) Then
                cbConteiner.Enabled = False
                btAgendar.Enabled = False
            Else
                cbConteiner.Enabled = True
                btAgendar.Enabled = True
            End If

        End If

    End Sub

    Protected Sub chAutonomo_CheckedChanged(sender As Object, e As EventArgs) Handles chAutonomo.CheckedChanged

    End Sub
End Class