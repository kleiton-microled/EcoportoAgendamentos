Public Class CadastrarConteineresExportacao
    Inherits System.Web.UI.Page

    Dim Exportacao As New Exportacao
    Dim Conteiner As New ConteinerExportacao
    Dim Transportadora As New Transportadora

    Protected Sub cbTipoTransporte_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cbTipoTransporte.SelectedIndexChanged

        If cbTipoTransporte.SelectedValue = "F" Then
            PanelVagao1.Visible = True
            PanelVagao2.Visible = True
            txtVagao.Focus()
        Else
            PanelVagao1.Visible = False
            PanelVagao2.Visible = False
        End If

    End Sub

    Private Sub CarregarDadosDaReserva(ByVal Reserva As String)

        If Exportacao.ReservaInexistente(Reserva) Then
            If Exportacao.NavioIndisponivelParaAgendamento(Reserva) Then
                ScriptManager.RegisterClientScriptBlock(Me, [GetType](), "script", "<script>alert('Reserva Cadastrada, mas o Navio não está aberto para agendamento. Verificar informações no site.');</script>", False)
                btSalvar.Enabled = False
                Exit Sub
            End If
            ScriptManager.RegisterClientScriptBlock(Me, [GetType](), "script", "<script>alert('Reserva Inexistente.');</script>", False)
            Exit Sub
        End If

        If Exportacao.ClienteBloqueado(Reserva) Then
            ScriptManager.RegisterClientScriptBlock(Me, [GetType](), "script", "<script>alert('Prezado cliente, para continuidade deste processo é necessário atualizar seus dados cadastrais, por favor contate nossa central de relacionamento com o cliente de segunda a sexta-feira das 08h00 as 18h00 através do telefone (13) 32130010 ou no e-mail atendimento@tecondi.com.br');location.href='CadastrarConteineresExportacao.aspx';</script>", False)
            Exit Sub
        End If

        If Exportacao.IsLate(Reserva) Then
            ScriptManager.RegisterClientScriptBlock(Me, [GetType](), "script", "<script>alert('Contêiner em situação Late Arrival. Clique em OK para continuar...');</script>", False)
            lblFlagLate.Text = "1"
        End If

        btSalvar.Enabled = True
        cbTamanho.Items.Clear()

        ViewState("AUTONUM_VIAGEM") = Exportacao.ObterCodigoViagem(Reserva)

        If Exportacao.EntradaDireta(Reserva, ViewState("AUTONUM_VIAGEM").ToString()) Then
            ScriptManager.RegisterClientScriptBlock(Me, [GetType](), "script", "<script>alert('Reserva com entrada direta. Operação não permitida.');</script>", False)
            Exit Sub
        End If

        CarregarTamanhosConteiner(Reserva, ViewState("AUTONUM_VIAGEM").ToString())

        If cbTamanho.Items.Count > 0 Then
            CarregarTiposConteiner(Reserva, ViewState("AUTONUM_VIAGEM").ToString(), cbTamanho.Text)
        End If

        If cbTipo.Items.Count > 0 Then
            CarregarRemarks(Reserva, ViewState("AUTONUM_VIAGEM").ToString(), cbTipo.SelectedValue, cbTamanho.Text)
        End If

        Dim ID_Booking As Integer = 0

        If cbRemarks.Items.Count > 0 Then
            ID_Booking = cbRemarks.SelectedValue
        End If

        CarregarCampos(Reserva, ViewState("AUTONUM_VIAGEM").ToString(), ID_Booking)

    End Sub

    Private Sub CarregarTamanhosConteiner(ByVal Reserva As String, ByVal Viagem As String)

        Dim Lista As List(Of Conteiner) = Conteiner.ConsultarTamanhos(Reserva, Viagem)

        If cbTamanho.Items.Count = 0 Then
            For Each Item As Conteiner In Lista
                Me.cbTamanho.Items.Add(New ListItem(Item.Tamanho, Item.Tamanho))
            Next
        End If

    End Sub

    Private Sub CarregarTiposConteiner(ByVal Reserva As String, ByVal Viagem As String, ByVal Tamanho As String)

        Dim Lista As List(Of Conteiner) = Conteiner.ConsultarTipos(Reserva, Viagem, Tamanho)

        cbTipo.Items.Clear()

        For Each Item In Lista
            Me.cbTipo.Items.Add(New ListItem(Item.Tipo, Item.Codigo))
        Next

        If cbTipo.SelectedValue <> "RE" And cbTipo.SelectedValue <> "HR" Then
            txtTemperatura.Text = String.Empty
            txtEscala.Text = String.Empty
            txtUmidade.Text = String.Empty
            txtVentilacao.Text = String.Empty
        Else
            'txtTemperatura.Enabled = True
            'txtEscala.Enabled = True
            'txtUmidade.Enabled = True
            'txtVentilacao.Enabled = True
        End If

    End Sub

    Private Sub CarregarRemarks(ByVal Reserva As String, ByVal AutonumViagem As String, ByVal Tipo As String, ByVal Tamanho As String)

        cbRemarks.Items.Clear()

        Dim Lista As List(Of Remark) = Exportacao.ConsultarRemarks(Reserva, AutonumViagem, Tipo, Tamanho)

        For Each Item In Lista
            Me.cbRemarks.Items.Add(New ListItem(Item.Descricao, Item.Codigo))
        Next

        If cbRemarks.Items.Count > 0 Then
            cbRemarks.SelectedIndex = 0
        End If

    End Sub

    Protected Sub txtReserva_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles txtReserva.TextChanged

        If Not txtReserva.Text = String.Empty Then
            CarregarDadosDaReserva(txtReserva.Text.ToUpper())
        End If

    End Sub

    Private Sub CarregarCampos(ByVal Reserva As String, ByVal Viagem As String, ByVal ID_Booking As Integer)

        Dim ReservaOBJ As Reserva = Exportacao.ConsultarDadosReservaPorID(Reserva, Viagem, ID_Booking)

        If Not ReservaOBJ Is Nothing Then

            txtExportador.Text = ReservaOBJ.Exportador
            lblNavioViagem.Text = ReservaOBJ.Navioviagem
            lblPortoDescarga.Text = ReservaOBJ.POD
            lblPortoDestino.Text = ReservaOBJ.FDES
            lblDataDeadLine.Text = ReservaOBJ.Data_dead_line
            lblEF.Text = ReservaOBJ.EF
            txtIMO1.Text = ReservaOBJ.Imo1
            txtIMO2.Text = ReservaOBJ.Imo2
            txtIMO3.Text = ReservaOBJ.Imo3
            txtIMO4.Text = ReservaOBJ.Imo4
            txtOnu1.Text = ReservaOBJ.Un1
            txtOnu2.Text = ReservaOBJ.Un2
            txtOnu3.Text = ReservaOBJ.Un3
            txtOnu4.Text = ReservaOBJ.Un4
            txtTemperatura.Text = ReservaOBJ.Temperatura
            txtEscala.Text = ReservaOBJ.Escala
            txtAltura.Text = ReservaOBJ.Overheight
            txtComp.Text = ReservaOBJ.Overlength
            txtLatDir.Text = ReservaOBJ.Overwidth
            txtLatEsq.Text = ReservaOBJ.Overwidthl
            txtUmidade.Text = ReservaOBJ.Umidade
            txtVentilacao.Text = ReservaOBJ.Ventilacao

        End If

    End Sub

    Protected Sub cbRemarks_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cbRemarks.SelectedIndexChanged

        If Not cbRemarks.SelectedValue = String.Empty Then
            CarregarCampos(txtReserva.Text.ToUpper(), ViewState("AUTONUM_VIAGEM").ToString(), cbRemarks.SelectedValue)
        End If

    End Sub

    Private Function Validar() As Boolean

        If txtSiglaConteiner.Text = String.Empty Then
            ScriptManager.RegisterClientScriptBlock(Me, [GetType](), "script", "<script>alert('Informe a Sigla do Contêiner.');</script>", False)
            Return False
        End If

        If cbTipo.Text = String.Empty Then
            ScriptManager.RegisterClientScriptBlock(Me, [GetType](), "script", "<script>alert('Selecione o Tipo do Contêiner.');</script>", False)
            Return False
        End If

        If cbTamanho.Text = String.Empty Then
            ScriptManager.RegisterClientScriptBlock(Me, [GetType](), "script", "<script>alert('Selecione o Tamanho do Contêiner.');</script>", False)
            Return False
        End If

        If cbTipoTransporte.Text = String.Empty Then
            ScriptManager.RegisterClientScriptBlock(Me, [GetType](), "script", "<script>alert('Selecione o Tipo de Transporte.');</script>", False)
            Return False
        End If

        If cbTipoTransporte.SelectedValue = "F" Then
            If txtVagao.Text = String.Empty Then
                ScriptManager.RegisterClientScriptBlock(Me, [GetType](), "script", "<script>alert('Informe o Vagão.');</script>", False)
                Return False
            End If
        End If

        If txtPesoBruto.Text = String.Empty Then
            ScriptManager.RegisterClientScriptBlock(Me, [GetType](), "script", "<script>alert('Informe o Peso Bruto.');</script>", False)
            Return False
        End If

        If txtTara.Text = String.Empty Then
            ScriptManager.RegisterClientScriptBlock(Me, [GetType](), "script", "<script>alert('Informe a Tara.');</script>", False)
            Return False
        End If

        If cbTipo.SelectedValue = "RE" Or cbTipo.SelectedValue = "HR" Then
            If txtTemperatura.Text = String.Empty Or txtTemperatura.Text = "0" Then
                ScriptManager.RegisterClientScriptBlock(Me, [GetType](), "script", "<script>alert('Informe a Temperatura.');</script>", False)
                Return False
            End If
            If Not txtEscala.Text = String.Empty Then
                If txtEscala.Text <> "C" And txtEscala.Text <> "F" Then
                    ScriptManager.RegisterClientScriptBlock(Me, [GetType](), "script", "<script>alert('Escala Inválida. (Válida: C ou F)');</script>", False)
                    Return False
                End If
            Else
                ScriptManager.RegisterClientScriptBlock(Me, [GetType](), "script", "<script>alert('Informe a Escala. Celsius (C) ou Fahrenheit (F).');</script>", False)
                Return False
            End If
        End If

        If Not txtLacreArmador2.Text = String.Empty Then
            If txtLacreArmador1.Text = String.Empty Then
                ScriptManager.RegisterClientScriptBlock(Me, [GetType](), "script", "<script>alert('Informe o Lacre Armador 1.');</script>", False)
                txtLacreArmador1.Focus()
                Return False
            End If
        End If

        If Not txtOutrosLacres2.Text = String.Empty Then
            If txtOutrosLacres1.Text = String.Empty Then
                ScriptManager.RegisterClientScriptBlock(Me, [GetType](), "script", "<script>alert(Informe o lacre 1.');</script>", False)
                txtOutrosLacres1.Focus()
                Return False
            End If
        End If

        If Not txtOutrosLacres3.Text = String.Empty Then
            If txtOutrosLacres1.Text = String.Empty Then
                ScriptManager.RegisterClientScriptBlock(Me, [GetType](), "script", "<script>alert(Informe o lacre 1.');</script>", False)
                txtOutrosLacres1.Focus()
                Return False
            End If
            If txtOutrosLacres2.Text = String.Empty Then
                ScriptManager.RegisterClientScriptBlock(Me, [GetType](), "script", "<script>alert(Informe o lacre 2.');</script>", False)
                txtOutrosLacres2.Focus()
                Return False
            End If
        End If

        If Not txtOutrosLacres4.Text = String.Empty Then
            If txtOutrosLacres1.Text = String.Empty Then
                ScriptManager.RegisterClientScriptBlock(Me, [GetType](), "script", "<script>alert(Informe o lacre 1.');</script>", False)
                txtOutrosLacres1.Focus()
                Return False
            End If
            If txtOutrosLacres2.Text = String.Empty Then
                ScriptManager.RegisterClientScriptBlock(Me, [GetType](), "script", "<script>alert(Informe o lacre 2.');</script>", False)
                txtOutrosLacres2.Focus()
                Return False
            End If
            If txtOutrosLacres3.Text = String.Empty Then
                ScriptManager.RegisterClientScriptBlock(Me, [GetType](), "script", "<script>alert(Informe o lacre 3.');</script>", False)
                txtOutrosLacres3.Focus()
                Return False
            End If
        End If

        If cbTipo.SelectedValue = "OT" Then
            If txtLacreExportador.Text = String.Empty Then
                ScriptManager.RegisterClientScriptBlock(Me, [GetType](), "script", "<script>alert('Contêineres do tipo Open Top (OT) necessitam de 2 lacres.');</script>", False)
                Return False
            End If
        End If

        If cbTipo.SelectedValue <> "PL" And cbTipo.SelectedValue <> "FR" And cbTipo.SelectedValue <> "TK" Then
            If txtLacreArmador1.Text = String.Empty And txtLacreArmador2.Text = String.Empty Then
                ScriptManager.RegisterClientScriptBlock(Me, [GetType](), "script", "<script>alert('É necessário informar ao menos 1 Lacre.');</script>", False)
                Return False
            End If
        End If

        If Not txtLacreArmador1.Text = String.Empty Then
            If Not Exportacao.ValidarLacres(txtReserva.Text.ToUpper(), txtLacreArmador1.Text.ToUpper()) > 0 Then
                ScriptManager.RegisterClientScriptBlock(Me, [GetType](), "script", "<script>alert('O Lacre Armador difere dos Formatos validos do Lacre.');</script>", False)
                txtLacreArmador1.Focus()
                Return False
            End If
        End If

        If Not txtLacreArmador2.Text = String.Empty Then
            If Not Exportacao.ValidarLacres(txtReserva.Text.ToUpper(), txtLacreArmador2.Text.ToUpper()) > 0 Then
                ScriptManager.RegisterClientScriptBlock(Me, [GetType](), "script", "<script>alert('O Lacre Armador difere dos Formatos validos do Lacre.');</script>", False)
                txtLacreArmador2.Focus()
                Return False
            End If
        End If

        If Convert.ToDouble(txtTara.Text) > 0 Then
            If Convert.ToDouble(txtTara.Text) < 1800.0 Or Convert.ToDouble(txtTara.Text) > 60000.0 Then
                ScriptManager.RegisterClientScriptBlock(Me, [GetType](), "script", "<script>alert('Tara Inválida. (Válida: 1800 a 60000)');</script>", False)
                Return False
            End If
        End If

        If Convert.ToDouble(txtPesoBruto.Text) > 0 Then
            If Convert.ToDouble(txtPesoBruto.Text) < 90000.0 Then
                If Convert.ToDouble(txtPesoBruto.Text) < Convert.ToDouble(txtTara.Text) Then
                    ScriptManager.RegisterClientScriptBlock(Me, [GetType](), "script", "<script>alert('O Peso Bruto deve ser superior a Tara.');</script>", False)
                    Return False
                End If
            Else
                ScriptManager.RegisterClientScriptBlock(Me, [GetType](), "script", "<script>alert('Peso Bruto Inválido. (Válido: Até 90000)');</script>", False)
                Return False
            End If
        End If

        If Not txtIMO1.Text = String.Empty Then
            If Not Conteiner.ValidaIMO(txtIMO1.Text) Then
                ScriptManager.RegisterClientScriptBlock(Me, [GetType](), "script", "<script>alert('Código IMO1 inexistente.');</script>", False)
                Return False
            End If
        End If

        If Not txtIMO2.Text = String.Empty Then
            If Not Conteiner.ValidaIMO(txtIMO2.Text) Then
                ScriptManager.RegisterClientScriptBlock(Me, [GetType](), "script", "<script>alert('Código IMO2 inexistente.');</script>", False)
                Return False
            End If
        End If

        If Not txtIMO4.Text = String.Empty Then
            If Not Conteiner.ValidaIMO(txtIMO3.Text) Then
                ScriptManager.RegisterClientScriptBlock(Me, [GetType](), "script", "<script>alert('Código IMO3 inexistente.');</script>", False)
                Return False
            End If
        End If

        If Not txtIMO4.Text = String.Empty Then
            If Not Conteiner.ValidaIMO(txtIMO4.Text) Then
                ScriptManager.RegisterClientScriptBlock(Me, [GetType](), "script", "<script>alert('Código IMO4 inexistente.');</script>", False)
                Return False
            End If
        End If


        If Not txtOnu1.Text = String.Empty Then
            If Not Conteiner.ValidaONU(txtOnu1.Text) Then
                ScriptManager.RegisterClientScriptBlock(Me, [GetType](), "script", "<script>alert('Código ONU1 inexistente.');</script>", False)
                Return False
            End If
        End If

        If Not txtOnu2.Text = String.Empty Then
            If Not Conteiner.ValidaONU(txtOnu2.Text) Then
                ScriptManager.RegisterClientScriptBlock(Me, [GetType](), "script", "<script>alert('Código ONU2 inexistente.');</script>", False)
                Return False
            End If
        End If

        If Not txtOnu3.Text = String.Empty Then
            If Not Conteiner.ValidaONU(txtOnu3.Text) Then
                ScriptManager.RegisterClientScriptBlock(Me, [GetType](), "script", "<script>alert('Código IMO3 inexistente.');</script>", False)
                Return False
            End If
        End If

        If Not txtOnu4.Text = String.Empty Then
            If Not Conteiner.ValidaONU(txtOnu4.Text) Then
                ScriptManager.RegisterClientScriptBlock(Me, [GetType](), "script", "<script>alert('Código IMO4 inexistente.');</script>", False)
                Return False
            End If
        End If

        Dim Tipos As List(Of String) = Conteiner.ValidarTipoBooking(txtReserva.Text.ToUpper())

        For Each Tipo In Tipos
            If Not Tipos.Contains(cbTipo.SelectedValue) Then
                ScriptManager.RegisterClientScriptBlock(Me, [GetType](), "script", "<script>alert('Tipo Básico incorreto para a Referência.');</script>", False)
                Return False
            End If
        Next

        Dim ID_Booking As Integer = 0

        If cbRemarks.Items.Count > 0 Then
            ID_Booking = cbRemarks.SelectedValue
        End If

        If cbTamanho.Text = "20" Then

            Dim Tamanho20 As Integer = Conteiner.ValidarTamanho20Booking(txtReserva.Text.ToUpper(), ViewState("AUTONUM_VIAGEM").ToString(), cbTipo.Text, ID_Booking)

            If cbTamanho.Text = "20" And Tamanho20 = 0 Then
                ScriptManager.RegisterClientScriptBlock(Me, [GetType](), "script", "<script>alert('Tamanho de 20 incorreto para a Referência.');</script>", False)
                Return False
            End If

            If Not Request.QueryString("id") IsNot Nothing Then

                Dim Quant20 As Integer = Tamanho20
                Dim Agendados As Integer = Conteiner.ConsultarQuantidadeAgendado(txtReserva.Text.ToUpper(), ViewState("AUTONUM_VIAGEM").ToString(), txtSiglaConteiner.Text, cbTipo.Text, cbTamanho.Text, ID_Booking)

                If Agendados >= Quant20 Then
                    ScriptManager.RegisterClientScriptBlock(Me, [GetType](), "script", "<script>alert('Quantidade de Contêineres de 20 excedida para a Referência.');</script>", False)
                    Return False
                End If

            End If

        End If

        If cbTamanho.Text = "40" Then

            Dim Tamanho40 As Integer = Conteiner.ValidarTamanho40Booking(txtReserva.Text.ToUpper(), ViewState("AUTONUM_VIAGEM").ToString(), cbTipo.Text, ID_Booking)

            If cbTamanho.Text = "40" And Tamanho40 = 0 Then
                ScriptManager.RegisterClientScriptBlock(Me, [GetType](), "script", "<script>alert('Tamanho de 40 incorreto para a Referência.');</script>", False)
                Return False
            End If

            If Not Request.QueryString("id") IsNot Nothing Then

                Dim Quant40 As Integer = Tamanho40
                Dim Agendados As Integer = Conteiner.ConsultarQuantidadeAgendado(txtReserva.Text.ToUpper(), ViewState("AUTONUM_VIAGEM").ToString(), txtSiglaConteiner.Text, cbTipo.Text, cbTamanho.Text, ID_Booking)

                If Agendados >= Quant40 Then
                    ScriptManager.RegisterClientScriptBlock(Me, [GetType](), "script", "<script>alert('Quantidade de Contêineres de 40 excedida para a Referência.');</script>", False)
                    Return False
                End If

            End If

        End If

        If cbTipoTransporte.SelectedValue = "F" Then
            If Not Transportadora.VerificarTransporteFerroviario(Session("SIS_ID").ToString()) Then
                ScriptManager.RegisterClientScriptBlock(Me, [GetType](), "script", "<script>alert('Transportadora não Cadastrada para Transporte Ferroviário.');</script>", False)
                Return False
            End If
        End If

        Return True

    End Function

    Protected Sub btSalvar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btSalvar.Click

        If Validar() Then

            Dim TransportadoraOBJ As New Transportadora
            TransportadoraOBJ.ID = Session("SIS_ID").ToString()

            Dim ConteinerOBJ As New Reserva

            ConteinerOBJ.Reserva = txtReserva.Text.ToUpper()
            ConteinerOBJ.Tamanho = cbTamanho.Text.ToUpper()
            ConteinerOBJ.Tipo = cbTipo.Text.ToUpper()
            ConteinerOBJ.EF = lblEF.Text.ToUpper()
            ConteinerOBJ.Sigla = txtSiglaConteiner.Text.ToUpper()
            ConteinerOBJ.Transporte = cbTipoTransporte.SelectedValue.ToUpper()
            ConteinerOBJ.Vagao = txtVagao.Text.ToUpper()
            ConteinerOBJ.Tara = Replace(Replace(txtTara.Text, ".", ""), ",", ".")
            ConteinerOBJ.PesoBruto = Replace(Replace(txtPesoBruto.Text, ".", ""), ",", ".")
            ConteinerOBJ.Un1 = txtOnu1.Text.ToUpper()
            ConteinerOBJ.Un2 = txtOnu2.Text.ToUpper()
            ConteinerOBJ.Un3 = txtOnu3.Text.ToUpper()
            ConteinerOBJ.Un4 = txtOnu4.Text.ToUpper()
            ConteinerOBJ.Un1 = txtOnu1.Text.ToUpper()
            ConteinerOBJ.Imo1 = txtIMO1.Text.ToUpper()
            ConteinerOBJ.Imo2 = txtIMO2.Text.ToUpper()
            ConteinerOBJ.Imo3 = txtIMO3.Text.ToUpper()
            ConteinerOBJ.Imo4 = txtIMO4.Text.ToUpper()
            ConteinerOBJ.Temperatura = txtTemperatura.Text.ToUpper()
            ConteinerOBJ.Escala = txtEscala.Text.ToUpper()
            ConteinerOBJ.Umidade = txtUmidade.Text.ToUpper()
            ConteinerOBJ.Ventilacao = txtVentilacao.Text.ToUpper()
            ConteinerOBJ.Overwidth = txtLatDir.Text.ToUpper()
            ConteinerOBJ.Overwidthl = txtLatEsq.Text.ToUpper()
            ConteinerOBJ.Overlength = txtComp.Text.ToUpper()
            ConteinerOBJ.Overheight = txtAltura.Text.ToUpper()
            ConteinerOBJ.Lacre1 = txtLacreArmador1.Text.ToUpper()
            ConteinerOBJ.Lacre2 = txtOutrosLacres1.Text.ToUpper()
            ConteinerOBJ.Lacre3 = txtOutrosLacres2.Text.ToUpper()
            ConteinerOBJ.Lacre4 = txtOutrosLacres3.Text.ToUpper()
            ConteinerOBJ.Lacre5 = txtLacreArmador2.Text.ToUpper()
            ConteinerOBJ.Lacre6 = txtLacreExportador.Text.ToUpper()
            ConteinerOBJ.Lacre7 = txtOutrosLacres4.Text.ToUpper()
            ConteinerOBJ.LacreSIF = txtLacreSIF.Text.ToUpper()
            ConteinerOBJ.Obs = txtObs.Text.ToUpper()

            Dim Porto_Descarga As String()
            Porto_Descarga = lblPortoDescarga.Text.Split("-")
            ConteinerOBJ.POD = Porto_Descarga(0).Trim.ToUpper()

            ConteinerOBJ.Transportadora = TransportadoraOBJ

            If Not txtVolumes.Text = String.Empty Then
                ConteinerOBJ.Volumes = txtVolumes.Text
            Else
                ConteinerOBJ.Volumes = "0"
            End If

            ConteinerOBJ.Autonum_viagem = ViewState("AUTONUM_VIAGEM").ToString()

            If Not cbRemarks.SelectedValue = String.Empty Then
                ConteinerOBJ.Autonum_reserva = cbRemarks.SelectedValue
            Else
                ConteinerOBJ.Autonum_reserva = "0"
            End If

            If lblFlagLate.Text = "1" Then
                ConteinerOBJ.Late = "1"
            Else
                ConteinerOBJ.Late = "0"
            End If

            If Exportacao.ConteinerCadastrado(txtSiglaConteiner.Text.ToUpper(), ViewState("AUTONUM_VIAGEM").ToString(), lblCodigo.Text) Then
                ScriptManager.RegisterClientScriptBlock(Me, [GetType](), "script", "<script>alert('Esse Contêiner já foi Cadastrado. Escolha outro ID.');</script>", False)
                Exit Sub
            End If

            If lblCodigo.Text = String.Empty Then

                If Conteiner.Inserir(ConteinerOBJ, Session("USRID").ToString()) Then
                    ScriptManager.RegisterClientScriptBlock(Me, [GetType](), "script", "<script>alert('Contêiner Cadastrado com Sucesso!');location.href='ConsultarConteineresExportacao.aspx';</script>", False)
                End If

            Else

                ConteinerOBJ.Codigo = lblCodigo.Text

                If Conteiner.Alterar(ConteinerOBJ) Then
                    ScriptManager.RegisterClientScriptBlock(Me, [GetType](), "script", "<script>alert('Contêiner Alterado com Sucesso!');location.href='ConsultarConteineresExportacao.aspx';</script>", False)
                End If

            End If

        End If

    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load

        If Not Page.IsPostBack Then

            If Not Request.QueryString("id") Is Nothing Then
                If Not Request.QueryString("action") Is Nothing Then
                    If Request.QueryString("action").ToString() = "edit" Then
                        CarregarTelaParaEdicao(Request.QueryString("id").ToString())
                    End If
                End If
            End If

            ViewState("URL") = Request.UrlReferrer

        End If

    End Sub

    Private Sub CarregarTelaParaEdicao(ByVal ID As String)

        Dim ReservaOBJ As Reserva = Exportacao.ConsultarDadosParaEdicao(ID)

        If Not ReservaOBJ Is Nothing Then

            lblCodigo.Text = ID
            txtReserva.Text = ReservaOBJ.Reserva
            lblEF.Text = ReservaOBJ.EF
            txtIMO1.Text = ReservaOBJ.Imo1
            txtIMO2.Text = ReservaOBJ.Imo2
            txtIMO3.Text = ReservaOBJ.Imo3
            txtIMO4.Text = ReservaOBJ.Imo4
            txtOnu1.Text = ReservaOBJ.Un1
            txtOnu2.Text = ReservaOBJ.Un2
            txtOnu3.Text = ReservaOBJ.Un3
            txtOnu4.Text = ReservaOBJ.Un4
            txtTemperatura.Text = ReservaOBJ.Temperatura
            txtEscala.Text = ReservaOBJ.Escala
            txtPesoBruto.Text = FormatNumber(ReservaOBJ.PesoBruto, 3)
            txtAltura.Text = ReservaOBJ.Overheight
            txtLatDir.Text = ReservaOBJ.Overwidth
            txtComp.Text = ReservaOBJ.Overlength
            txtLatEsq.Text = ReservaOBJ.Overwidthl
            txtObs.Text = ReservaOBJ.Obs
            txtSiglaConteiner.Text = ReservaOBJ.Sigla
            txtTara.Text = FormatNumber(ReservaOBJ.Tara, 3)
            txtVolumes.Text = ReservaOBJ.Volumes
            cbTipoTransporte.SelectedValue = ReservaOBJ.Transporte
            txtVagao.Text = ReservaOBJ.Vagao
            txtUmidade.Text = ReservaOBJ.Umidade
            txtVentilacao.Text = ReservaOBJ.Ventilacao
            txtLacreArmador1.Text = ReservaOBJ.Lacre1
            txtOutrosLacres1.Text = ReservaOBJ.Lacre2
            txtOutrosLacres2.Text = ReservaOBJ.Lacre3
            txtOutrosLacres3.Text = ReservaOBJ.Lacre4
            txtLacreArmador2.Text = ReservaOBJ.Lacre5
            txtLacreExportador.Text = ReservaOBJ.Lacre6
            txtOutrosLacres4.Text = ReservaOBJ.Lacre7
            txtLacreSIF.Text = ReservaOBJ.LacreSIF

            ViewState("AUTONUM_VIAGEM") = ReservaOBJ.Autonum_viagem

            CarregarTamanhosConteiner(ReservaOBJ.Reserva, ViewState("AUTONUM_VIAGEM").ToString())

            cbTamanho.SelectedValue = ReservaOBJ.Tamanho

            If cbTamanho.Items.Count > 0 Then
                CarregarTiposConteiner(ReservaOBJ.Reserva, ViewState("AUTONUM_VIAGEM").ToString(), cbTamanho.Text)
            End If

            If cbTipo.Items.Count > 0 Then
                CarregarRemarks(txtReserva.Text.ToUpper(), ViewState("AUTONUM_VIAGEM").ToString(), cbTipo.SelectedValue, cbTamanho.Text)
            End If

            If cbRemarks.Items.Count > 0 Then
                cbRemarks.SelectedValue = ReservaOBJ.Autonum_reserva
            End If

            Dim ID_Booking As Integer = 0

            If cbRemarks.Items.Count > 0 Then
                ID_Booking = cbRemarks.SelectedValue
            End If

            CarregarCampos(txtReserva.Text.ToUpper(), ViewState("AUTONUM_VIAGEM").ToString(), ID_Booking)

            cbTipo.SelectedValue = ReservaOBJ.Tipo

            If cbTipo.SelectedValue <> "RE" And cbTipo.SelectedValue <> "HR" Then
                txtTemperatura.Text = String.Empty
                txtEscala.Text = String.Empty
                txtUmidade.Text = String.Empty
                txtVentilacao.Text = String.Empty
            Else
                'txtTemperatura.Enabled = True
                'txtEscala.Enabled = True
                'txtUmidade.Enabled = True
                'txtVentilacao.Enabled = True
            End If

        End If

    End Sub

    Protected Sub btNovo_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btNovo.Click
        Response.Redirect("CadastrarConteineresExportacao.aspx")
    End Sub

    Protected Sub btExcluir_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btExcluir.Click

        If Not lblCodigo.Text = String.Empty Then
            If Conteiner.Excluir(lblCodigo.Text) Then
                ScriptManager.RegisterClientScriptBlock(Me, [GetType](), "script", "<script>alert('Contêiner Excluído com Sucesso!');location.href='ConsultarConteineresExportacao.aspx';</script>", False)
            End If
        End If

    End Sub

    Protected Sub btRetornar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btRetornar.Click
        Response.Redirect(ViewState("URL").ToString())
    End Sub

    Protected Sub txtTara_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles txtTara.TextChanged

        If Not txtTara.Text = String.Empty Then
            txtTara.Text = FormatNumber(txtTara.Text, 3)
            txtPesoBruto.Focus()
        End If

    End Sub

    Protected Sub txtPesoBruto_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles txtPesoBruto.TextChanged

        If Not txtPesoBruto.Text = String.Empty Then
            txtPesoBruto.Text = FormatNumber(txtPesoBruto.Text, 3)
            txtVolumes.Focus()
        End If

    End Sub

    Protected Sub cbTipo_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cbTipo.SelectedIndexChanged

        If cbTipo.Items.Count > 0 Then

            CarregarTamanhosConteiner(txtReserva.Text.ToUpper(), ViewState("AUTONUM_VIAGEM").ToString())

            txtUmidade.Text = Exportacao.ObterUmidade(txtReserva.Text.ToUpper(), ViewState("AUTONUM_VIAGEM").ToString())
            txtVentilacao.Text = Exportacao.ObterVentilacao(txtReserva.Text.ToUpper(), ViewState("AUTONUM_VIAGEM").ToString())
            txtTemperatura.Text = Exportacao.ObterTemperatura(txtReserva.Text.ToUpper(), ViewState("AUTONUM_VIAGEM").ToString())

            CarregarRemarks(txtReserva.Text.ToUpper(), ViewState("AUTONUM_VIAGEM").ToString(), cbTipo.SelectedValue, cbTamanho.Text)

            If cbTipo.SelectedValue <> "RE" And cbTipo.SelectedValue <> "HR" Then
                txtTemperatura.Text = String.Empty
                txtEscala.Text = String.Empty
                txtUmidade.Text = String.Empty
                txtVentilacao.Text = String.Empty
            Else
                'txtTemperatura.Enabled = True
                'txtEscala.Enabled = True
                'txtUmidade.Enabled = True
                'txtVentilacao.Enabled = True
            End If

        End If

    End Sub

    Protected Sub cbTamanho_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cbTamanho.SelectedIndexChanged

        If cbTamanho.Items.Count > 0 Then

            cbTipo.Items.Clear()
            CarregarTiposConteiner(txtReserva.Text.ToUpper(), ViewState("AUTONUM_VIAGEM").ToString(), cbTamanho.Text)
            CarregarRemarks(txtReserva.Text.ToUpper(), ViewState("AUTONUM_VIAGEM").ToString(), cbTipo.SelectedValue, cbTamanho.Text)

            If cbRemarks.Items.Count > 0 Then               
                CarregarCampos(txtReserva.Text.ToUpper(), ViewState("AUTONUM_VIAGEM").ToString(), cbRemarks.SelectedValue)
            End If

        End If

    End Sub
End Class