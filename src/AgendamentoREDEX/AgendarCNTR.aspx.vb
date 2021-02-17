Imports System.Data.OleDb
Imports System.IO
Imports System.Xml

Public Class AgendarCNTR
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not Page.IsPostBack Then

            ConsultarCavalos()
            ConsultarTransportadora()
            ConsultarTiposConteiner()

            If Request.QueryString("id") IsNot Nothing Then

                Dim SQL As New StringBuilder

                SQL.Append("SELECT ")
                SQL.Append("    AUTONUM_GD_CNTR, ")
                SQL.Append("    REFERENCE, ")
                SQL.Append("    AUTONUM_GD_MOTORISTA, ")
                SQL.Append("    AUTONUM_VEICULO, ")
                SQL.Append("    AUTONUMVIAGEM, ")
                SQL.Append("    ID_CONTEINER, ")
                SQL.Append("    TARA, ")
                SQL.Append("    BRUTO, ")
                SQL.Append("    LACRE1, ")
                SQL.Append("    LACRE2, ")
                SQL.Append("    LACRE3, ")
                SQL.Append("    LACRE4, ")
                SQL.Append("    LACRE5, ")
                SQL.Append("    LACRE6, ")
                SQL.Append("    LACRE7, ")
                SQL.Append("    LACRE_SIF, ")
                SQL.Append("    VENTILACAO, ")
                SQL.Append("    UMIDADE, ")
                SQL.Append("    VOLUMES, ")
                SQL.Append("    TEMPERATURA, ")
                SQL.Append("    ESCALA, ")
                SQL.Append("    ALTURA, ")
                SQL.Append("    LATERAL_DIREITA, ")
                SQL.Append("    LATERAL_ESQUERDA, ")
                SQL.Append("    COMPRIMENTO, ")
                SQL.Append("    TAMANHO, ")
                SQL.Append("    TIPOBASICO, ")
                SQL.Append("    AUTONUM_GD_RESERVA, ")
                SQL.Append("    IMO1, ")
                SQL.Append("    IMO2, ")
                SQL.Append("    IMO3, ")
                SQL.Append("    IMO4, ")
                SQL.Append("    ONU1, ")
                SQL.Append("    ONU2, ")
                SQL.Append("    ONU3, ")
                SQL.Append("    ONU4, ")
                SQL.Append("    LINE, ")
                SQL.Append("    NOME, ")
                SQL.Append("    PLACA_CAVALO, ")
                SQL.Append("    PLACA_CARRETA, ")
                SQL.Append("    NUM_PROTOCOLO, ")
                SQL.Append("    ANO_PROTOCOLO, ")
                SQL.Append("    ID_TRANSPORTADORA, ")
                SQL.Append("    CNH, ")
                SQL.Append("    VEICULO, ")
                SQL.Append("    NAVIO_VIAGEM, ")
                SQL.Append("    DEAD_LINE, ")
                SQL.Append("    CONTEINER, ")
                SQL.Append("    PROTOCOLO, ")
                SQL.Append("    STATUS, ")
                SQL.Append("    PERIODO, ")
                SQL.Append("    TRANSPORTADORA, ")
                SQL.Append("    MODELO, ")
                SQL.Append("    NEXTEL, ")
                SQL.Append("    RG, ")
                SQL.Append("    POD, ")
                SQL.Append("    FDES, ")
                SQL.Append("    EXPORTADOR, ")
                SQL.Append("    COD_ISO, ")
                SQL.Append("    PATIO ")
                SQL.Append("FROM ")
                SQL.Append("   REDEX.VW_AGENDAMENTO_WEB_CN_PROT ")
                SQL.Append("WHERE AUTONUM_GD_CNTR = " & Request.QueryString("id").ToString())

                Dim Ds As New DataTable
                Ds = Banco.List(SQL.ToString())

                Dim Periodo As String

                If Ds IsNot Nothing Then
                    If Ds.Rows.Count > 0 Then

                        Me.txtReserva.Text = Ds.Rows(0)("REFERENCE").ToString()
                        txtReserva_TextChanged(sender, e)

                        Me.lblCodigoAgendamento.Text = Ds.Rows(0)("AUTONUM_GD_CNTR").ToString()
                        Me.lblCodigoMotorista.Text = Ds.Rows(0)("AUTONUM_GD_MOTORISTA").ToString()
                        Me.lblCodigoVeiculo.Text = Ds.Rows(0)("AUTONUM_VEICULO").ToString()
                        Me.lblCodigoPeriodo.Text = Ds.Rows(0)("AUTONUM_GD_RESERVA").ToString()
                        Me.lblPatio.Text = Ds.Rows(0)("PATIO").ToString()
                        Me.txtConteiner.Text = Ds.Rows(0)("ID_CONTEINER").ToString()
                        Me.txtPesoBrutoCntr.Text = Ds.Rows(0)("BRUTO").ToString()
                        Me.txtTara.Text = Ds.Rows(0)("TARA").ToString()
                        Me.txtAltura.Text = Ds.Rows(0)("ALTURA").ToString()
                        Me.txtLatDir.Text = Ds.Rows(0)("LATERAL_DIREITA").ToString()
                        Me.txtLatEsq.Text = Ds.Rows(0)("LATERAL_ESQUERDA").ToString()
                        Me.txtComprimento.Text = Ds.Rows(0)("COMPRIMENTO").ToString()
                        Me.cbTipo.SelectedValue = Ds.Rows(0)("TIPOBASICO").ToString()
                        Me.cbTamanho.Text = Ds.Rows(0)("TAMANHO").ToString()
                        Me.txtIMO1.Text = Ds.Rows(0)("IMO1").ToString()
                        Me.txtIMO2.Text = Ds.Rows(0)("IMO2").ToString()
                        Me.txtIMO3.Text = Ds.Rows(0)("IMO3").ToString()
                        Me.txtIMO4.Text = Ds.Rows(0)("IMO4").ToString()
                        Me.txtUn1.Text = Ds.Rows(0)("ONU1").ToString()
                        Me.txtUn2.Text = Ds.Rows(0)("ONU2").ToString()
                        Me.txtUn3.Text = Ds.Rows(0)("ONU3").ToString()
                        Me.txtUn4.Text = Ds.Rows(0)("ONU4").ToString()
                        Me.txtVent.Text = Ds.Rows(0)("VENTILACAO").ToString()
                        Me.txtUmidade.Text = Ds.Rows(0)("UMIDADE").ToString()
                        Me.txtTemp.Text = Ds.Rows(0)("TEMPERATURA").ToString()
                        Me.txtEscala.Text = Ds.Rows(0)("ESCALA").ToString()
                        Me.txtLacre1.Text = Ds.Rows(0)("LACRE1").ToString()
                        Me.txtLacre2.Text = Ds.Rows(0)("LACRE2").ToString()
                        Me.txtLacre3.Text = Ds.Rows(0)("LACRE3").ToString()
                        Me.txtLacre4.Text = Ds.Rows(0)("LACRE4").ToString()
                        Me.txtLacre5.Text = Ds.Rows(0)("LACRE5").ToString()
                        Me.txtLacre6.Text = Ds.Rows(0)("LACRE6").ToString()
                        Me.txtLacre7.Text = Ds.Rows(0)("LACRE7").ToString()
                        Me.txtLacre8.Text = Ds.Rows(0)("LACRE_SIF").ToString()
                        Me.txtMotorista.Text = Ds.Rows(0)("NOME").ToString()
                        Me.cbCavalo.Text = Ds.Rows(0)("PLACA_CAVALO").ToString()

                        ConsultarCarretas(Me.cbCavalo.Text)
                        Me.cbCarreta.Text = Ds.Rows(0)("PLACA_CARRETA").ToString()

                        HabilitaCamposCNTR(True)

                        ConsultarNF(Me.lblCodigoAgendamento.Text)
                        HabilitaCamposNF(True)

                        ConsultarPeriodos(Val(Me.lblPatio.Text))

                        Me.lblCodigoPeriodo.Text = Ds.Rows(0)("AUTONUM_GD_RESERVA").ToString()
                        Periodo = Banco.ExecuteScalar("SELECT TO_CHAR(PERIODO_INICIAL,'DD/MM/YYYY HH24:MI') || ' - ' || TO_CHAR(PERIODO_FINAL,'DD/MM/YYYY HH24:MI') PERIODO FROM REDEX.TB_GD_RESERVA WHERE AUTONUM_GD_RESERVA = " & Me.lblCodigoPeriodo.Text)

                        For Each Linha As GridViewRow In Me.dgConsultaPeriodos.Rows
                            Linha.BackColor = Drawing.Color.Transparent
                        Next

                        For Each Linha As GridViewRow In Me.dgConsultaPeriodos.Rows
                            If Linha.Cells(1).Text & " - " & Linha.Cells(2).Text = Periodo Then
                                Linha.BackColor = Drawing.Color.MistyRose
                            End If
                        Next

                    Else
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgAlerta", "exibeMensagem('Registro inexistente!');", True)
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgAlerta", "document.location.href='ConsultarAgendamentosCNTR.aspx';", True)
                    End If
                End If

            End If

        End If

        If Not String.IsNullOrEmpty(Me.lblCodigoBooking.Text) Then
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "exibeAccordion", "document.getElementById('accordion').style.display = 'block';", True)
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "exibePanelCTE", "document.getElementById('pnlAlertaCTE').style.display = 'block';", True)

        End If

    End Sub

    Public Sub ConsultarCavalos()

        Me.cbCavalo.DataSource = Banco.List("SELECT PLACA_CAVALO FROM OPERADOR.TB_AG_VEICULOS WHERE ID_TRANSPORTADORA = " & Session("SIS_AUTONUM_TRANSPORTADORA").ToString())
        Me.cbCavalo.DataBind()

        Me.cbCavalo.Items.Insert(0, New ListItem("", ""))

    End Sub

    Public Sub ConsultarTiposConteiner()

        Me.cbTipo.DataSource = Banco.List("SELECT DISTINCT CODIGO FROM OPERADOR.TB_CAD_TIPO_CONTEINER ORDER BY CODIGO")
        Me.cbTipo.DataBind()

        Me.cbTipo.Items.Insert(0, New ListItem("", ""))

    End Sub

    Public Sub ConsultarCarretas(ByVal Cavalo As String)

        Me.cbCarreta.DataSource = Banco.List("SELECT PLACA_CARRETA FROM OPERADOR.TB_AG_VEICULOS WHERE ID_TRANSPORTADORA = " & Session("SIS_AUTONUM_TRANSPORTADORA").ToString() & " AND PLACA_CAVALO = '" & Me.cbCavalo.Text & "'")
        Me.cbCarreta.DataBind()

        Me.cbCarreta.Items.Insert(0, New ListItem("", ""))

    End Sub

    Public Sub ConsultarTransportadora()
        Me.lblTransportadora.Text = Banco.ExecuteScalar("SELECT RAZAO FROM OPERADOR.TB_CAD_TRANSPORTADORAS WHERE AUTONUM = " & Session("SIS_AUTONUM_TRANSPORTADORA").ToString())
    End Sub

    Protected Sub txtConteiner_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles txtConteiner.TextChanged
        txtConteiner.Text = txtConteiner.Text.ToUpper()
        Me.AccordionIndex.Value = 2
    End Sub

    Protected Sub txtReserva_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles txtReserva.TextChanged

        Dim SQL As New StringBuilder

        If Not String.IsNullOrEmpty(Me.txtReserva.Text.Trim()) And
            Not String.IsNullOrWhiteSpace(Me.txtReserva.Text.Trim()) Then

            Me.AccordionIndex.Value = 0

            If Me.lblCodigoAgendamento.Text <> String.Empty Then
                'ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgAlerta", "alert('Existe um Agendamento sendo criado, para escolher outra Reserva, primeiro cancele o agendamento.');", True)
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgAlerta", "exibeMensagem('Existe um Agendamento sendo criado, para escolher outra Reserva, primeiro cancele o agendamento.');", True)
                Exit Sub
            End If

            Dim ret As String = String.Empty

            If Convert.ToInt32(Banco.ExecuteScalar("SELECT COUNT(1) FROM REDEX.TB_BOOKING WHERE REFERENCE = '" & Me.txtReserva.Text.ToUpper() & "'")) = 0 Then
                If My.Settings.LerXmlCraft = 1 Then
                    Try
                        ret = Craft.ImportarXMLCraft(Me.txtReserva.Text.ToUpper())
                        If ret <> String.Empty Then
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgAlerta", "exibeMensagem('" & ret & "');", True)
                        End If
                    Catch ex As Exception
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgAlerta", "exibeMensagem('" & ret & "');", True)
                    End Try
                End If
            End If

            Dim Ds As New DataTable

            SQL.Clear()
            SQL.Append("SELECT")
            SQL.Append("    AUTONUM, ")
            SQL.Append("    RESERVA, ")
            SQL.Append("    QUANTIDADE, ")
            SQL.Append("    NAVIO, ")
            SQL.Append("    NVOCC, ")
            SQL.Append("    EXPORTADOR, ")
            SQL.Append("    NVOCC, ")
            SQL.Append("    AUTONUM_VIA, ")
            SQL.Append("    NUM_VIAGEM, ")
            SQL.Append("    DT_DEAD_LINE, ")
            SQL.Append("    FLAG_CS, ")
            SQL.Append("    FLAG_CNTR, ")
            SQL.Append("    PATIO ")
            SQL.Append("FROM ")
            SQL.Append("    REDEX.VW_AGENDAMENTO_WEB_DADOS_BOO ")
            SQL.Append(" WHERE UPPER(RESERVA) = '" & Me.txtReserva.Text.ToUpper() & "' AND NVL(FLAG_CNTR,0) = 1")

            Ds = Banco.List(SQL.ToString())

            If Ds IsNot Nothing Then
                If Ds.Rows.Count > 0 Then
                    For Each Linha As DataRow In Ds.Rows

                        Me.lblDeadLine.Text = Linha("DT_DEAD_LINE").ToString()
                        Me.lblExportador.Text = Linha("EXPORTADOR").ToString()
                        Me.lblNavio.Text = Linha("NAVIO").ToString()
                        Me.lblStatus.Text = "Liberado"
                        Me.lblTipo.Text = "Contêiner Cheio"
                        Me.lblCodigoViagem.Text = Linha("AUTONUM_VIA").ToString()
                        Me.lblViagem.Text = Linha("NUM_VIAGEM").ToString()
                        Me.lblTotal.Text = Linha("QUANTIDADE").ToString()
                        Me.lblCodigoBooking.Text = Linha("AUTONUM").ToString()
                        Me.lblPatio.Text = Linha("PATIO").ToString()

                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "exibeAccordion", "document.getElementById('accordion').style.display = 'block';", True)
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "exibepnlAlertaCTE", "document.getElementById('pnlAlertaCTE').style.display = 'block';", True)
                        Me.txtMotorista.Focus()

                        Me.btnSalvar.Visible = True
                        Me.btnCancelar.Visible = True

                        ConsultarQuantidades()
                        ConsultarPeriodos(Val(Me.lblPatio.Text))
                        Me.txtMotorista.Focus()

                    Next

                Else
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgAlerta", "exibeMensagem('Reserva inválida!');", True)
                End If
            End If

        End If


        If Me.lblCodigoBooking.Text = String.Empty Then

            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgAlerta", "exibeMensagem('Reserva não encontrada!');", True)

            Me.lblDeadLine.Text = String.Empty
            Me.lblExportador.Text = String.Empty
            Me.lblM3.Text = String.Empty
            Me.lblNavio.Text = String.Empty
            Me.lblNVOCC.Text = String.Empty
            Me.lblPesoBruto.Text = String.Empty
            Me.lblStatus.Text = String.Empty
            Me.lblTipo.Text = String.Empty
            Me.lblTotal.Text = String.Empty
            Me.lblViagem.Text = String.Empty
            Me.lblVolumes.Text = String.Empty

        End If

    End Sub

    Private Sub ConsultarQuantidades()

        Dim SQL As New StringBuilder

        SQL.Append("SELECT COUNT(1) UTILIZADOS FROM REDEX.TB_GD_CONTEINER WHERE UPPER(REFERENCE) = '" & Me.txtReserva.Text.ToUpper() & "' ")

        Dim Ds As New DataTable
        Ds = Banco.List(SQL.ToString())

        If Ds IsNot Nothing Then
            If Ds.Rows.Count > 0 Then
                Me.lblUtilizados.Text = Ds.Rows(0)("UTILIZADOS").ToString()
                Me.lblDisponiveis.Text = (Nnull(Me.lblTotal.Text, 0) - Nnull(Me.lblUtilizados.Text, 0))
            End If
        End If

    End Sub

    Protected Sub txtMotorista_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles txtMotorista.TextChanged

        If Not String.IsNullOrEmpty(Me.txtMotorista.Text.Trim()) And
                Not String.IsNullOrWhiteSpace(Me.txtMotorista.Text.Trim()) Then
            Me.AccordionIndex.Value = 1
            Me.txtMotorista.Text = Banco.ExecuteScalar("SELECT DISTINCT NOME || ' - ' || CNH FROM OPERADOR.TB_AG_MOTORISTAS WHERE (UPPER(NOME) LIKE '%" & Me.txtMotorista.Text.ToUpper() & "%' OR CNH = '" & Me.txtMotorista.Text & "') AND ID_TRANSPORTADORA = " & Session("SIS_AUTONUM_TRANSPORTADORA").ToString())
            HabilitaCamposCNTR(True)
        End If

    End Sub

    Protected Sub btnSalvar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSalvar.Click

        Dim SQL As New StringBuilder

        If Me.btnSalvar.Text = "Concluir" Then

            SQL.Append("UPDATE REDEX.TB_GD_CONTEINER ")
            SQL.Append("    SET ")
            SQL.Append("        AUTONUM_GD_RESERVA = " & Nnull(Me.lblCodigoPeriodo.Text, 0) & " ")
            SQL.Append("  WHERE AUTONUM_GD_CNTR = " & Me.lblCodigoAgendamento.Text)

            If Banco.BeginTransaction(SQL.ToString()) Then
                'If Agendamento.InsereAgendamentoNaFila(Val(Me.lblCodigoAgendamento.Text), Val(Me.lblCodigoPeriodo.Text), Val(Me.lblCodigoBooking.Text), Val(Me.lblCodigoAgendamento.Text), TipoAgendamento.CONTEINER_DESCARGA) Then
                Response.Redirect("ConsultarAgendamentosCNTR.aspx")
                'End If
            End If

        End If

        If ValidarCampos() Then

            If Request.QueryString("id") Is Nothing Then

                Me.lblCodigoAgendamento.Text = Banco.ExecuteScalar("SELECT REDEX.SEQ_GD_CONTEINER.NEXTVAL FROM DUAL")
                Me.lblCodigoBooking.Text = Banco.ExecuteScalar("SELECT AUTONUM_BOO FROM REDEX.TB_BOOKING WHERE UPPER(REFERENCE) = '" & Me.txtReserva.Text.ToUpper() & "'")
                Me.lblCodigoMotorista.Text = Banco.ExecuteScalar("SELECT AUTONUM FROM OPERADOR.TB_AG_MOTORISTAS WHERE UPPER(TRIM(NOME)) = '" & ObterNomeMotorista(Me.txtMotorista.Text.ToUpper()) & "' AND ID_TRANSPORTADORA = " & Session("SIS_AUTONUM_TRANSPORTADORA").ToString())
                Me.lblCodigoVeiculo.Text = Banco.ExecuteScalar("SELECT AUTONUM FROM OPERADOR.TB_AG_VEICULOS WHERE UPPER(PLACA_CAVALO) = '" & Me.cbCavalo.Text & "' AND UPPER(PLACA_CARRETA) = '" & Me.cbCarreta.Text & "' AND ID_TRANSPORTADORA = " & Session("SIS_AUTONUM_TRANSPORTADORA").ToString())

                SQL.Append("INSERT INTO REDEX.TB_GD_CONTEINER ")
                SQL.Append("    ( ")
                SQL.Append("        AUTONUM_GD_CNTR, ")
                SQL.Append("        AUTONUM_TRANSPORTADORA, ")
                SQL.Append("        AUTONUM_GD_MOTORISTA, ")
                SQL.Append("        AUTONUM_VEICULO, ")
                SQL.Append("        AUTONUMVIAGEM, ")
                SQL.Append("        REFERENCE, ")
                SQL.Append("        ID_CONTEINER, ")
                SQL.Append("        TARA, ")
                SQL.Append("        BRUTO, ")
                SQL.Append("        LACRE1, ")
                SQL.Append("        LACRE2, ")
                SQL.Append("        LACRE3, ")
                SQL.Append("        LACRE4, ")
                SQL.Append("        LACRE5, ")
                SQL.Append("        LACRE6, ")
                SQL.Append("        LACRE7, ")
                SQL.Append("        LACRE_SIF, ")
                SQL.Append("        VENTILACAO, ")
                SQL.Append("        UMIDADE, ")
                SQL.Append("        VOLUMES, ")
                SQL.Append("        TEMPERATURA, ")
                SQL.Append("        ESCALA, ")
                SQL.Append("        OH, ")
                SQL.Append("        OW, ")
                SQL.Append("        OWL, ")
                SQL.Append("        OL, ")
                SQL.Append("        EF, ")
                SQL.Append("        TAMANHO, ")
                SQL.Append("        TIPOBASICO, ")
                SQL.Append("        FLAG_ATIVO, ")
                SQL.Append("        VAGAO, ")
                SQL.Append("        TIPO_TRANSPORTE, ")
                SQL.Append("        AUTONUM_GD_RESERVA, ")
                SQL.Append("        IMO1, ")
                SQL.Append("        IMO2, ")
                SQL.Append("        IMO3, ")
                SQL.Append("        IMO4, ")
                SQL.Append("        ONU1, ")
                SQL.Append("        ONU2, ")
                SQL.Append("        ONU3, ")
                SQL.Append("        ONU4, ")
                SQL.Append("        POD, ")
                SQL.Append("        FLAG_LATE, ")
                SQL.Append("        OBS, ")
                SQL.Append("        NUM_PROTOCOLO, ")
                SQL.Append("        ANO_PROTOCOLO, ")
                SQL.Append("        USRID ")
                SQL.Append("    ) ")
                SQL.Append("VALUES ")
                SQL.Append("    ( ")
                SQL.Append("     " & Me.lblCodigoAgendamento.Text & ",  ")
                SQL.Append("     " & Session("SIS_AUTONUM_TRANSPORTADORA").ToString() & ",  ")
                SQL.Append("     " & Me.lblCodigoMotorista.Text & ",  ")
                SQL.Append("     " & Me.lblCodigoVeiculo.Text & ",  ")
                SQL.Append("     " & Me.lblCodigoViagem.Text & ",  ")
                SQL.Append("     '" & Me.txtReserva.Text.ToUpper() & "',  ")
                SQL.Append("     '" & Me.txtConteiner.Text.ToUpper() & "',  ")
                SQL.Append("     '" & Me.txtTara.Text & "',  ")
                SQL.Append("     '" & Me.txtPesoBrutoCntr.Text & "',  ")
                SQL.Append("     '" & Me.txtLacre1.Text & "',  ")
                SQL.Append("     '" & Me.txtLacre2.Text & "',  ")
                SQL.Append("     '" & Me.txtLacre3.Text & "',  ")
                SQL.Append("     '" & Me.txtLacre4.Text & "',  ")
                SQL.Append("     '" & Me.txtLacre5.Text & "',  ")
                SQL.Append("     '" & Me.txtLacre6.Text & "',  ")
                SQL.Append("     '" & Me.txtLacre7.Text & "',  ")
                SQL.Append("     '" & Me.txtLacre8.Text & "',  ")
                SQL.Append("     '" & Me.txtVent.Text & "',  ")
                SQL.Append("     '" & Me.txtUmidade.Text & "',  ")
                SQL.Append("     '" & Me.txtVolumesCntr.Text & "',  ")
                SQL.Append("     '" & Me.txtTemp.Text & "',  ")
                SQL.Append("     '" & Me.txtEscala.Text & "',  ")
                SQL.Append("     '" & Me.txtAltura.Text & "',  ")
                SQL.Append("     '" & Me.txtLatDir.Text & "',  ")
                SQL.Append("     '" & Me.txtLatEsq.Text & "',  ")
                SQL.Append("     '" & Me.txtComprimento.Text & "',  ")
                SQL.Append("     'F',  ")
                SQL.Append("     " & Me.cbTamanho.Text & ",  ")
                SQL.Append("     '" & Me.cbTipo.Text & "',  ")
                SQL.Append("     1,  ")
                SQL.Append("     '',  ")
                SQL.Append("     'R',  ")
                SQL.Append("     " & Nnull(Me.lblCodigoPeriodo.Text, 0) & ",  ")
                SQL.Append("     '" & Me.txtIMO1.Text & "',  ")
                SQL.Append("     '" & Me.txtIMO2.Text & "',  ")
                SQL.Append("     '" & Me.txtIMO3.Text & "',  ")
                SQL.Append("     '" & Me.txtIMO4.Text & "',  ")
                SQL.Append("     '" & Me.txtUn1.Text & "',  ")
                SQL.Append("     '" & Me.txtUn2.Text & "',  ")
                SQL.Append("     '" & Me.txtUn3.Text & "',  ")
                SQL.Append("     '" & Me.txtUn4.Text & "',  ")
                SQL.Append("     '" & Mid(Me.lblPortoDescarga.Text, 1, 3) & "',  ")
                SQL.Append("     '',  ")
                SQL.Append("     '',  ")
                SQL.Append("    REDEX.SEQ_GD_PROT_" & Now.Year & ".NEXTVAL, ")
                SQL.Append("    " & Now.Year & " ,")
                SQL.Append("    0")
                SQL.Append("    ) ")

                If Convert.ToInt32(Banco.BeginTransaction(SQL.ToString())) > 0 Then
                    'ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgAlerta", "alert('Agendamento criado com sucesso! Vincule as Notas Fiscais utlizando os campos abaixo.');", True)
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgAlerta", "exibeMensagem('Agendamento criado com sucesso! Vincule as Notas Fiscais utlizando os campos abaixo.');", True)
                    Me.btnSalvar.Text = "Concluir"
                    Me.lblMsgNF.Visible = False
                    Me.AccordionIndex.Value = 3
                    HabilitaCamposNF(True)
                    ConsultarNF(Me.lblCodigoAgendamento.Text)
                Else
                    'ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgAlerta", "alert('Erro ao criar um novo agendamento. Tente Novamente.');", True)
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgAlerta", "exibeMensagem('Erro ao criar um novo agendamento. Tente Novamente.');", True)
                End If

            Else

                Me.lblCodigoMotorista.Text = Banco.ExecuteScalar("SELECT AUTONUM FROM OPERADOR.TB_AG_MOTORISTAS WHERE UPPER(TRIM(NOME)) = '" & ObterNomeMotorista(Me.txtMotorista.Text.ToUpper()) & "' AND ID_TRANSPORTADORA = " & Session("SIS_AUTONUM_TRANSPORTADORA").ToString())
                Me.lblCodigoVeiculo.Text = Banco.ExecuteScalar("SELECT AUTONUM FROM OPERADOR.TB_AG_VEICULOS WHERE UPPER(PLACA_CAVALO) = '" & Me.cbCavalo.Text & "' AND UPPER(PLACA_CARRETA) = '" & Me.cbCarreta.Text & "' AND ID_TRANSPORTADORA = " & Session("SIS_AUTONUM_TRANSPORTADORA").ToString())
                Me.lblCodigoProtocolo.Text = Banco.ExecuteScalar("SELECT REDEX.SEQ_AGENDAMENTO_WEB_PROT_" & Now.Year & ".NEXTVAL FROM DUAL")

                SQL.Append("UPDATE REDEX.TB_GD_CONTEINER ")
                SQL.Append("    SET ")
                SQL.Append("        AUTONUM_GD_MOTORISTA = " & Nnull(Me.lblCodigoMotorista.Text, 0) & ", ")
                SQL.Append("        AUTONUM_VEICULO = " & Nnull(Me.lblCodigoVeiculo.Text, 0) & ", ")
                SQL.Append("        ID_CONTEINER = '" & Me.txtConteiner.Text.ToUpper() & "', ")
                SQL.Append("        TARA = " & Nnull(PPonto(Me.txtTara.Text), 0) & ", ")
                SQL.Append("        BRUTO = " & Nnull(PPonto(Me.txtPesoBrutoCntr.Text), 0) & ", ")
                SQL.Append("        LACRE1 = '" & Me.txtLacre1.Text & "', ")
                SQL.Append("        LACRE2 = '" & Me.txtLacre2.Text & "', ")
                SQL.Append("        LACRE3 = '" & Me.txtLacre3.Text & "', ")
                SQL.Append("        LACRE4 = '" & Me.txtLacre4.Text & "', ")
                SQL.Append("        LACRE5 = '" & Me.txtLacre5.Text & "', ")
                SQL.Append("        LACRE6 = '" & Me.txtLacre6.Text & "', ")
                SQL.Append("        LACRE7 = '" & Me.txtLacre7.Text & "', ")
                SQL.Append("        LACRE_SIF = '" & Me.txtLacre8.Text & "', ")
                SQL.Append("        VENTILACAO = '" & Me.txtVent.Text & "', ")
                SQL.Append("        UMIDADE = '" & Me.txtUmidade.Text & "', ")
                SQL.Append("        VOLUMES = " & Nnull(PPonto(Me.txtVolumesCntr.Text), 0) & ", ")
                SQL.Append("        TEMPERATURA = '" & Me.txtTemp.Text & "', ")
                SQL.Append("        ESCALA = '" & Me.txtEscala.Text & "', ")
                SQL.Append("        OH = " & Nnull(PPonto(Me.txtAltura.Text), 0) & ", ")
                SQL.Append("        OW = " & Nnull(PPonto(Me.txtLatDir.Text), 0) & ", ")
                SQL.Append("        OWL = " & Nnull(PPonto(Me.txtLatEsq.Text), 0) & ", ")
                SQL.Append("        OL = " & Nnull(PPonto(Me.txtComprimento.Text), 0) & ", ")
                SQL.Append("        EF = 'F', ")
                SQL.Append("        TAMANHO = " & Nnull(PPonto(Me.cbTamanho.Text), 0) & ", ")
                SQL.Append("        TIPOBASICO = '" & Me.cbTipo.Text & "', ")
                SQL.Append("        FLAG_ATIVO = 1, ")
                SQL.Append("        VAGAO = '', ")
                SQL.Append("        TIPO_TRANSPORTE = 'R', ")
                SQL.Append("        AUTONUM_GD_RESERVA = " & Nnull(Me.lblCodigoPeriodo.Text, 0) & ", ")
                SQL.Append("        IMO1 = '" & Me.txtIMO1.Text & "', ")
                SQL.Append("        IMO2 = '" & Me.txtIMO2.Text & "', ")
                SQL.Append("        IMO3 = '" & Me.txtIMO3.Text & "', ")
                SQL.Append("        IMO4 = '" & Me.txtIMO4.Text & "', ")
                SQL.Append("        ONU1 = '" & Me.txtUn1.Text & "', ")
                SQL.Append("        ONU2 = '" & Me.txtUn2.Text & "', ")
                SQL.Append("        ONU3 = '" & Me.txtUn3.Text & "', ")
                SQL.Append("        ONU4 = '" & Me.txtUn4.Text & "', ")
                SQL.Append("        POD = '" & Mid(Me.lblPortoDescarga.Text, 1, 3) & "', ")
                SQL.Append("        FLAG_LATE = '', ")
                SQL.Append("        OBS = '', ")
                SQL.Append("        STATUS = 'GE', ")
                SQL.Append("        NUM_PROTOCOLO = REDEX.SEQ_GD_PROT_" & Now.Year & ".NEXTVAL, ")
                SQL.Append("        ANO_PROTOCOLO = " & Now.Year & ", ")
                SQL.Append("        USRID = 0 ")
                SQL.Append("  WHERE AUTONUM_GD_CNTR = " & Me.lblCodigoAgendamento.Text)

                If Banco.BeginTransaction(SQL.ToString()) Then
                    'If Agendamento.InsereAgendamentoNaFila(Val(Me.lblCodigoAgendamento.Text), Val(Me.lblCodigoPeriodo.Text), Val(Me.lblCodigoBooking.Text), Val(Me.lblCodigoAgendamento.Text), TipoAgendamento.CONTEINER_DESCARGA) Then
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgAlerta", "exibeMensagem('Agendamento alterado com sucesso!','ConsultarAgendamentosCNTR.aspx');", True)
                    'End If
                Else
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgAlerta", "exibeMensagem('Erro ao alterar o agendamento. Tente Novamente.');", True)
                End If

            End If

        End If

    End Sub

    Private Function ObterNomeMotorista(ByVal Nome As String) As String

        Dim Cadeia() As String = Nome.Split("-")

        If Cadeia.Count > 0 Then
            Return Cadeia(0).ToString().Trim()
        End If

        Return String.Empty

    End Function

    Private Function ValidarCampos() As Boolean

        If Request.QueryString("id") Is Nothing Then
            If Convert.ToInt32(Me.lblDisponiveis.Text) = 0 Then
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgAlerta", "exibeMensagem('Todas as unidades da Reserva " & Me.txtReserva.Text & " já foram agendadas! Quantidade máxima de contêineres sem agendamento foi atingida.');", True)
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgAlerta", "document.location.href='AgendarCNTR.aspx';", True)
                Return False
            End If
        End If

        If String.IsNullOrEmpty(Me.txtReserva.Text.Trim()) Or
            String.IsNullOrWhiteSpace(Me.txtReserva.Text.Trim()) Then
            'ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgAlerta", "alert('Nenhuma Reserva Informada.');", True)
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgAlerta", "exibeMensagem('Nenhuma Reserva Informada.');", True)
            Me.AccordionIndex.Value = 0
            Me.txtReserva.Focus()
            Return False
        End If

        If String.IsNullOrEmpty(Me.txtMotorista.Text.Trim()) Or
            String.IsNullOrWhiteSpace(Me.txtMotorista.Text.Trim()) Then
            'ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgAlerta", "alert('O campo Nome do Motorista é obrigatório!');", True)
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgAlerta", "exibeMensagem('O campo Nome do Motorista é obrigatório!');", True)
            Me.AccordionIndex.Value = 1
            Me.txtMotorista.Focus()
            Return False
        End If

        If String.IsNullOrEmpty(Me.cbCavalo.Text.Trim()) Or
            String.IsNullOrWhiteSpace(Me.cbCavalo.Text.Trim()) Then
            'ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgAlerta", "alert('Selecione a Placa do Cavalo.');", True)
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgAlerta", "exibeMensagem('Selecione a Placa do Cavalo.');", True)
            Me.AccordionIndex.Value = 1
            Me.cbCavalo.Focus()
            Return False
        End If

        If String.IsNullOrEmpty(Me.cbCarreta.Text.Trim()) Or
            String.IsNullOrWhiteSpace(Me.cbCarreta.Text.Trim()) Then
            'ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgAlerta", "alert('Selecione a Placa da Carreta.');", True)
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgAlerta", "exibeMensagem('Selecione a Placa da Carreta.');", True)
            Me.AccordionIndex.Value = 1
            Me.cbCavalo.Focus()
            Return False
        End If

        If Me.txtConteiner.Text = String.Empty Then
            'ScriptManager.RegisterClientScriptBlock(Me, [GetType](), "script", "<script>alert('Informe a Sigla do Contêiner.');</script>", False)
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgAlerta", "exibeMensagem('Informe a Sigla do Contêiner.');", True)
            Me.AccordionIndex.Value = 2
            Me.txtConteiner.Focus()
            Return False
        End If

        If Me.cbTipo.Text = String.Empty Then
            'ScriptManager.RegisterClientScriptBlock(Me, [GetType](), "script", "<script>alert('Selecione o Tipo do Contêiner.');</script>", False)
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgAlerta", "exibeMensagem('Selecione o Tipo do Contêiner.');", True)
            Me.AccordionIndex.Value = 2
            Me.cbTipo.Focus()
            Return False
        End If

        If Me.cbTamanho.Text = String.Empty Then
            'ScriptManager.RegisterClientScriptBlock(Me, [GetType](), "script", "<script>alert('Selecione o Tamanho do Contêiner.');</script>", False)
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgAlerta", "exibeMensagem('Selecione o Tamanho do Contêiner.');", True)
            Me.AccordionIndex.Value = 2
            Me.cbTamanho.Focus()
            Return False
        End If

        If txtPesoBrutoCntr.Text = String.Empty Then
            'ScriptManager.RegisterClientScriptBlock(Me, [GetType](), "script", "<script>alert('Informe o Peso Bruto.');</script>", False)
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgAlerta", "exibeMensagem('Informe o Peso Bruto.');", True)
            Me.AccordionIndex.Value = 2
            Me.txtPesoBrutoCntr.Focus()
            Return False
        End If

        If txtTara.Text = String.Empty Then
            'ScriptManager.RegisterClientScriptBlock(Me, [GetType](), "script", "<script>alert('Informe a Tara.');</script>", False)
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgAlerta", "exibeMensagem('Informe a Tara.');", True)
            Me.AccordionIndex.Value = 2
            Return False
        End If

        If cbTipo.Text = "RE" Or cbTipo.Text = "HR" Then

            If txtTemp.Text = String.Empty Or txtTemp.Text = "0" Then
                'ScriptManager.RegisterClientScriptBlock(Me, [GetType](), "script", "<script>alert('Informe a Temperatura.');</script>", False)
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgAlerta", "exibeMensagem('Informe a Temperatura.');", True)
                Me.AccordionIndex.Value = 2
                Me.txtTemp.Focus()
                Return False
            End If

            If Not txtEscala.Text = String.Empty Then
                If txtEscala.Text <> "C" And txtEscala.Text <> "F" Then
                    'ScriptManager.RegisterClientScriptBlock(Me, [GetType](), "script", "<script>alert('Escala Inválida. (Válida: C ou F)');</script>", False)
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgAlerta", "exibeMensagem('Escala Inválida. (Válida: C ou F)');", True)
                    Me.AccordionIndex.Value = 2
                    Me.txtEscala.Focus()
                    Return False
                End If
            Else
                'ScriptManager.RegisterClientScriptBlock(Me, [GetType](), "script", "<script>alert('Informe a Escala. Celsius (C) ou Fahrenheit (F).');</script>", False)
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgAlerta", "exibeMensagem('Informe a Escala. Celsius (C) ou Fahrenheit (F).');", True)
                Me.AccordionIndex.Value = 2
                Return False
            End If
        End If

        If Not Me.txtLacre1.Text = String.Empty Then
            If Me.txtLacre1.Text = String.Empty Then
                'ScriptManager.RegisterClientScriptBlock(Me, [GetType](), "script", "<script>alert('Informe o Lacre Armador 1.');</script>", False)
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgAlerta", "exibeMensagem('Informe o Lacre Armador 1.');", True)
                Me.AccordionIndex.Value = 2
                txtLacre1.Focus()
                Return False
            End If
        End If

        If Not Me.txtLacre4.Text = String.Empty Then
            If Me.txtLacre3.Text = String.Empty Then
                ScriptManager.RegisterClientScriptBlock(Me, [GetType](), "script", "<script>alert(Informe o lacre 1.');</script>", False)
                Me.AccordionIndex.Value = 2
                Me.txtLacre3.Focus()
                Return False
            End If
        End If

        If Not Me.txtLacre5.Text = String.Empty Then
            If Me.txtLacre4.Text = String.Empty Then
                ScriptManager.RegisterClientScriptBlock(Me, [GetType](), "script", "<script>alert(Informe o lacre 1.');</script>", False)
                Me.AccordionIndex.Value = 2
                Me.txtLacre4.Focus()
                Return False
            End If
            If Me.txtLacre3.Text = String.Empty Then
                ScriptManager.RegisterClientScriptBlock(Me, [GetType](), "script", "<script>alert(Informe o lacre 2.');</script>", False)
                Me.AccordionIndex.Value = 2
                Me.txtLacre3.Focus()
                Return False
            End If
        End If

        If Not txtLacre6.Text = String.Empty Then
            If txtLacre5.Text = String.Empty Then
                ScriptManager.RegisterClientScriptBlock(Me, [GetType](), "script", "<script>alert(Informe o lacre 1.');</script>", False)
                Me.AccordionIndex.Value = 2
                Me.txtLacre5.Focus()
                Return False
            End If
            If Me.txtLacre4.Text = String.Empty Then
                ScriptManager.RegisterClientScriptBlock(Me, [GetType](), "script", "<script>alert(Informe o lacre 2.');</script>", False)
                Me.AccordionIndex.Value = 2
                Me.txtLacre4.Focus()
                Return False
            End If
            If Me.txtLacre3.Text = String.Empty Then
                ScriptManager.RegisterClientScriptBlock(Me, [GetType](), "script", "<script>alert(Informe o lacre 3.');</script>", False)
                Me.AccordionIndex.Value = 2
                Me.txtLacre3.Focus()
                Return False
            End If
        End If

        If Me.cbTipo.SelectedValue = "OT" Then
            If Me.txtLacre7.Text = String.Empty Then
                'ScriptManager.RegisterClientScriptBlock(Me, [GetType](), "script", "<script>alert('Contêineres do tipo Open Top (OT) necessitam de 2 lacres.');</script>", False)
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgAlerta", "exibeMensagem('Contêineres do tipo Open Top (OT) necessitam de 2 lacres.');", True)
                Me.AccordionIndex.Value = 2
                Return False
            End If
        End If

        If Me.cbTipo.SelectedValue <> "PL" And cbTipo.SelectedValue <> "FR" And cbTipo.SelectedValue <> "TK" Then
            If Me.txtLacre1.Text = String.Empty And Me.txtLacre2.Text = String.Empty Then
                'ScriptManager.RegisterClientScriptBlock(Me, [GetType](), "script", "<script>alert('É necessário informar ao menos 1 Lacre.');</script>", False)
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgAlerta", "exibeMensagem('É necessário informar ao menos 1 Lacre.');", True)
                Me.AccordionIndex.Value = 2
                Return False
            End If
        End If

        If Convert.ToDouble(txtTara.Text) > 0 Then
            If Convert.ToDouble(txtTara.Text) < 1800.0 Or Convert.ToDouble(txtTara.Text) > 60000.0 Then
                'ScriptManager.RegisterClientScriptBlock(Me, [GetType](), "script", "<script>alert('Tara Inválida. (Válida: 1800 a 60000)');</script>", False)
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgAlerta", "exibeMensagem('Tara Inválida. (Válida: 1800 a 60000)');", True)
                Me.AccordionIndex.Value = 2
                Return False
            End If
        End If

        If IsNumeric(txtPesoBruto.Text) Then
            If Convert.ToDouble(txtPesoBruto.Text) < 90000.0 Then
                If Convert.ToDouble(txtPesoBrutoCntr.Text) < Convert.ToDouble(txtTara.Text) Then
                    'ScriptManager.RegisterClientScriptBlock(Me, [GetType](), "script", "<script>alert('O Peso Bruto deve ser superior a Tara.');</script>", False)
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgAlerta", "exibeMensagem('O Peso Bruto deve ser superior a Tara.');", True)
                    Me.AccordionIndex.Value = 2
                    Return False
                End If
            Else
                'ScriptManager.RegisterClientScriptBlock(Me, [GetType](), "script", "<script>alert('Peso Bruto Inválido. (Válido: Até 90000)');</script>", False)
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgAlerta", "exibeMensagem('Peso Bruto Inválido. (Válido: Até 90000)');", True)
                Me.AccordionIndex.Value = 2
                Return False
            End If
        End If

        'If Me.lblCodigoPeriodo.Text = String.Empty Then
        '    'ScriptManager.RegisterClientScriptBlock(Me, [GetType](), "script", "<script>alert('Nenhum período foi selecionado.');</script>", False)
        '    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgAlerta", "exibeMensagem('Nenhum período foi selecionado.');", True)
        '    Me.AccordionIndex.Value = 3
        '    Return False
        'End If

        Return True

    End Function

    Public Function ConsultarQuantidadeAgendado(ByVal Reserva As String, ByVal Viagem As String, ByVal ID_Conteiner As String, ByVal Tipo As String, ByVal Tamanho As String, ByVal IDBooking As Integer) As Integer

        Dim SQL As New StringBuilder

        SQL.Append("SELECT ")
        SQL.Append("    COUNT(*) AS QTD ")
        SQL.Append("FROM ")
        SQL.Append("    OPERADOR.TB_GD_CONTEINER A ")
        SQL.Append("WHERE ")
        SQL.Append("    A.REFERENCE='{0}' ")
        SQL.Append("AND ")
        SQL.Append("    A.AUTONUMVIAGEM={1} ")
        SQL.Append("AND ")
        SQL.Append("    A.ID_CONTEINER <> '{2}' ")
        SQL.Append("AND ")
        SQL.Append("    A.TIPOBASICO='{3}' ")
        SQL.Append("AND ")
        SQL.Append("    A.TAMANHO = {4} ")
        SQL.Append("AND ")
        SQL.Append("    A.AUTONUM_RESERVA = {5} ")

        Return Banco.ExecuteScalar(SQL.ToString())

    End Function

    Private Function ValidarCamposNF() As Boolean

        Me.AccordionIndex.Value = 3

        If String.IsNullOrEmpty(Me.lblCodigoAgendamento.Text) Or
            String.IsNullOrWhiteSpace(Me.lblCodigoAgendamento.Text) Then
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgAlerta", "exibeMensagem('As Notas Fiscais só poderão ser vinculadas após a conclusão do Agendamento. Clique no botão Salvar para continuar.');", True)
            Return False
        End If

        If String.IsNullOrEmpty(Me.txtDANFE.Text.Trim()) Or String.IsNullOrWhiteSpace(Me.txtDANFE.Text.Trim()) Then

            If String.IsNullOrEmpty(Me.txtNumero.Text.Trim()) Or
            String.IsNullOrWhiteSpace(Me.txtNumero.Text.Trim()) Then
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgAlerta", "exibeMensagem('O campo Número é obrigatório.');", True)
                Me.txtNumero.Focus()
                Return False
            Else
                If Not IsNumeric(Me.txtNumero.Text) Then
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgAlerta", "exibeMensagem('O campo Número não é um valor Numérico.');", True)
                    Me.txtNumero.Focus()
                    Return False
                End If
            End If

            If String.IsNullOrEmpty(Me.txtSerie.Text.Trim()) Or
                String.IsNullOrWhiteSpace(Me.txtSerie.Text.Trim()) Then
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgAlerta", "exibeMensagem('O campo Série é obrigatório.');", True)
                Me.txtSerie.Focus()
                Return False
            End If

            If String.IsNullOrEmpty(Me.txtEmissor.Text.Trim()) Or
                String.IsNullOrWhiteSpace(Me.txtEmissor.Text.Trim()) Then
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgAlerta", "exibeMensagem('O campo Emissor é obrigatório.');", True)
                Me.txtEmissor.Focus()
                Return False
            End If

            If String.IsNullOrEmpty(Me.txtEmissao.Text.Trim()) Or
                String.IsNullOrWhiteSpace(Me.txtEmissao.Text.Trim()) Then
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgAlerta", "exibeMensagem('O campo Emissão é obrigatório.');", True)
                Me.txtEmissao.Focus()
                Return False
            Else
                If Not IsDate(Me.txtEmissao.Text) Then
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgAlerta", "exibeMensagem('O campo Emissão não é uma Data Válida.');", True)
                    Me.txtEmissao.Focus()
                    Return False
                End If
            End If

            If String.IsNullOrEmpty(Me.txtValor.Text.Trim()) Or
                String.IsNullOrWhiteSpace(Me.txtValor.Text.Trim()) Then
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgAlerta", "exibeMensagem('O campo Valor é obrigatório.');", True)
                Me.txtValor.Focus()
                Return False
            Else
                If Not IsNumeric(Me.txtValor.Text) Then
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgAlerta", "exibeMensagem('O campo Valor não é um valor Numérico.');", True)
                    Me.txtValor.Focus()
                    Return False
                End If
            End If

            If Nnull(Me.lblPesoBruto.Text, 0) <> 0 Then

                If String.IsNullOrEmpty(Me.txtPesoBruto.Text.Trim()) Or
                String.IsNullOrWhiteSpace(Me.txtPesoBruto.Text.Trim()) Then
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgAlerta", "exibeMensagem('O campo Peso Bruto é obrigatório.');", True)
                    Me.txtPesoBruto.Focus()
                    Return False
                Else
                    If Not IsNumeric(Me.txtPesoBruto.Text) Then
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgAlerta", "exibeMensagem('O campo Peso Bruto não é um valor Numérico.');", True)
                        Me.txtPesoBruto.Focus()
                        Return False
                    End If
                End If

            End If

        Else
            If Not IsNumeric(Me.txtDANFE.Text) Then
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgAlerta", "exibeMensagem('O campo DANFE não é um valor Numérico.');", True)
                Me.txtDANFE.Focus()
                Return False
            End If
        End If

        Return True

    End Function

    Private Function ExisteDuplicidadeNF(ByVal CodigoAgendamento As String, ByVal CodigoNF As String) As Boolean

        Dim SQL As New StringBuilder

        SQL.Append("SELECT COUNT(1) FROM ")
        SQL.Append("  REDEX.TB_AGENDAMENTO_WEB_CNTR_NF ")
        SQL.Append("  WHERE AUTONUM > 0 ")

        If String.IsNullOrEmpty(Me.txtDANFE.Text.Trim()) Or String.IsNullOrWhiteSpace(Me.txtDANFE.Text.Trim()) Then
            SQL.Append("    AND NUMERO = " & Me.txtNumero.Text & " ")
            SQL.Append("    AND SERIE = '" & Me.txtSerie.Text & "' ")
            SQL.Append("    AND EMISSOR = '" & Me.txtEmissor.Text & "' ")
            SQL.Append("    AND EMISSAO = TO_DATE('" & Convert.ToDateTime(Me.txtEmissao.Text).ToString("dd/MM/yyyy") & "','DD/MM/YYYY') ")
            'SQL.Append("    AND QTDE = " & Me.txtQtde.Text & " ")
            SQL.Append("    AND VALOR = " & Replace(Replace(Me.txtValor.Text, ".", ""), ",", ".") & " ")
            SQL.Append("    AND PESO_BRUTO = " & Replace(Replace(Me.txtPesoBruto.Text, ".", ""), ",", ".") & " ")
            'SQL.Append("    AND M3 = " & Replace(Replace(Me.txtM3.Text, ".", ""), ",", ".") & " ")
        Else
            SQL.Append("   AND DANFE = '" & Me.txtDANFE.Text & "' ")
        End If

        SQL.Append("    AND AUTONUM_AGENDAMENTO = " & CodigoAgendamento)

        If Not String.IsNullOrEmpty(CodigoNF) And
            Not String.IsNullOrWhiteSpace(CodigoNF) Then
            SQL.Append("  AND AUTONUM <> " & CodigoNF)
        End If

        If Convert.ToInt32(Banco.ExecuteScalar(SQL.ToString())) > 0 Then
            Return True
        End If

        Return False

    End Function

    Protected Sub btnSalvarNF_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSalvarNF.Click

        Me.AccordionIndex.Value = 3

        If ValidarCamposNF() Then

        End If
        'Dim ArquivoDanfe = UploadDanfe()

        'If ArquivoDanfe = "ERRO" Then
        '    Exit Sub
        'End If

        Dim SQL As New StringBuilder



        If Me.lblCodigoNF.Text = String.Empty Then

            If String.IsNullOrEmpty(Me.txtXml.Text) Then
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgAlerta", "exibeMensagem('Arquivo XML de DANFE não informado. Selecione o Arquivo e clique em UPLOAD!');", True)
                Exit Sub
            End If

            If ExisteDuplicidadeNF(Me.lblCodigoAgendamento.Text, String.Empty) Then
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgAlerta", "exibeMensagem('Já existe uma outra NF cadastrada com os mesmos dados.');", True)
                Exit Sub
            End If

            SQL.Append("INSERT INTO ")
            SQL.Append("  REDEX.TB_AGENDAMENTO_WEB_CNTR_NF ")
            SQL.Append("  ( ")
            SQL.Append("    AUTONUM, ")
            SQL.Append("    AUTONUM_AGENDAMENTO, ")
            SQL.Append("    DANFE, ")
            SQL.Append("    RESERVA, ")
            SQL.Append("    NUMERO, ")
            SQL.Append("    SERIE, ")
            SQL.Append("    EMISSOR, ")
            SQL.Append("    EMISSAO, ")
            SQL.Append("    VALOR, ")
            SQL.Append("    PESO_BRUTO, ")
            SQL.Append("    ARQUIVO_DANFE")
            SQL.Append("  ) ")
            SQL.Append("  VALUES ")
            SQL.Append("  ( ")
            SQL.Append("    REDEX.SEQ_AGENDAMENTO_WEB_CNTR_NF.NEXTVAL, ")
            SQL.Append("    :CodigoAgendamento, ")
            SQL.Append("    :Danfe, ")
            SQL.Append("    :Reserva, ")
            SQL.Append("    :Numero, ")
            SQL.Append("    :Serie, ")
            SQL.Append("    :Emissor, ")
            SQL.Append("    :Emissao, ")
            SQL.Append("    :Valor, ")
            SQL.Append("    :PesoBruto, ")
            SQL.Append("    :Xml")
            SQL.Append("  ) ")

            Using Con As New OleDbConnection(Banco.ConnectionString())
                Using Cmd As New OleDbCommand(SQL.ToString(), Con)

                    Cmd.Parameters.Add(New OleDbParameter("CodigoAgendamento", Me.lblCodigoAgendamento.Text))
                    Cmd.Parameters.Add(New OleDbParameter("Danfe", Me.txtDANFE.Text))
                    Cmd.Parameters.Add(New OleDbParameter("Reserva", Me.txtReserva.Text))
                    Cmd.Parameters.Add(New OleDbParameter("Numero", Me.txtNumero.Text))
                    Cmd.Parameters.Add(New OleDbParameter("Serie", Me.txtSerie.Text))
                    Cmd.Parameters.Add(New OleDbParameter("Emissor", Me.txtEmissor.Text))
                    Cmd.Parameters.Add(New OleDbParameter("Emissao", Convert.ToDateTime(Me.txtEmissao.Text)))
                    Cmd.Parameters.Add(New OleDbParameter("Valor", PPonto(Me.txtValor.Text)))
                    Cmd.Parameters.Add(New OleDbParameter("PesoBruto", PPonto(Me.txtPesoBruto.Text)))
                    Cmd.Parameters.Add(New OleDbParameter("Xml", Me.txtXml.Text))

                    Con.Open()

                    Try
                        Cmd.ExecuteNonQuery()
                    Catch ex As Exception
                        Me.msgErroNF.Text = ex.Message
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgAlerta", "exibeMensagem('Erro ao Cadastrar a NF. Tente Novamente.');", True)
                    End Try

                End Using
            End Using

        Else

            If ExisteDuplicidadeNF(Me.lblCodigoAgendamento.Text, Me.lblCodigoNF.Text) Then
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgAlerta", "exibeMensagem('Já existe uma outra NF cadastrada com os mesmos dados.');", True)
                Exit Sub
            End If

            SQL.Append("UPDATE ")
            SQL.Append("  REDEX.TB_AGENDAMENTO_WEB_CNTR_NF ")
            SQL.Append("  SET ")
            SQL.Append("    NUMERO = :Numero, ")
            SQL.Append("    SERIE = :Serie, ")
            SQL.Append("    EMISSOR = :Emissor, ")
            SQL.Append("    EMISSAO = :Emissao, ")
            SQL.Append("    VALOR = :Valor, ")
            SQL.Append("    PESO_BRUTO = :PesoBruto, ")
            SQL.Append("    Danfe = :Danfe, ")
            SQL.Append("    Arquivo_Danfe = :Xml ")
            SQL.Append("  WHERE AUTONUM = :CodigoNF")

            Using Con As New OleDbConnection(Banco.ConnectionString())
                Using Cmd As New OleDbCommand(SQL.ToString(), Con)

                    Cmd.Parameters.Add(New OleDbParameter("Numero", Me.txtNumero.Text))
                    Cmd.Parameters.Add(New OleDbParameter("Serie", Me.txtSerie.Text))
                    Cmd.Parameters.Add(New OleDbParameter("Emissor", Me.txtEmissor.Text))
                    Cmd.Parameters.Add(New OleDbParameter("Emissao", Convert.ToDateTime(Me.txtEmissao.Text)))
                    Cmd.Parameters.Add(New OleDbParameter("Valor", PPonto(Me.txtValor.Text)))
                    Cmd.Parameters.Add(New OleDbParameter("PesoBruto", PPonto(Me.txtPesoBruto.Text)))
                    Cmd.Parameters.Add(New OleDbParameter("Danfe", Me.txtDANFE.Text))
                    Cmd.Parameters.Add(New OleDbParameter("Xml", Me.txtXml.Text))
                    Cmd.Parameters.Add(New OleDbParameter("CodigoNF", Me.lblCodigoNF.Text))

                    Con.Open()

                    Try
                        Cmd.ExecuteNonQuery()
                    Catch ex As Exception
                        Me.msgErroNF.Text = ex.Message
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgAlerta", "exibeMensagem('Erro ao atualizar a NF. Tente Novamente.');", True)
                    End Try

                End Using
            End Using

            Me.lblCodigoNF.Text = String.Empty

        End If

        ConsultarNF(Me.lblCodigoAgendamento.Text)

        If Me.dgConsultaNF.Rows.Count > 0 Then
            Me.btnCancelarNF.Enabled = True
        Else
            Me.btnCancelarNF.Enabled = False
        End If

    End Sub

    Private Sub ConsultarNF(ByVal CodigoAgendamento As String)

        If Not String.IsNullOrEmpty(CodigoAgendamento) Or Not String.IsNullOrWhiteSpace(CodigoAgendamento) Then

            Me.dgConsultaNF.DataSource = Nothing

            Me.dgConsultaNF.DataSource = Banco.List("SELECT AUTONUM,DANFE,NUMERO,SERIE,EMISSOR,TO_CHAR(EMISSAO,'DD/MM/YYYY') EMISSAO,VALOR,PESO_BRUTO, NVL(ARQUIVO_DANFE, 'X') ARQUIVO_DANFE FROM REDEX.TB_AGENDAMENTO_WEB_CNTR_NF WHERE AUTONUM_AGENDAMENTO = " & CodigoAgendamento)
            Me.dgConsultaNF.DataBind()
        End If

    End Sub

    Protected Sub dgConsultaNF_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles dgConsultaNF.RowCommand

        If Me.dgConsultaNF.Rows.Count > 0 Then

            Me.AccordionIndex.Value = 3

            Dim Index As Integer = e.CommandArgument
            Dim ID As String = Me.dgConsultaNF.DataKeys(Index)("AUTONUM").ToString()

            If e.CommandName = "DEL" Then
                If Not String.IsNullOrEmpty(ID) And
                    Not String.IsNullOrWhiteSpace(ID) Then
                    If Banco.BeginTransaction("DELETE FROM REDEX.TB_AGENDAMENTO_WEB_CNTR_NF WHERE AUTONUM = " & ID) Then
                        ConsultarNF(ID)
                    Else
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgAlerta", "exibeMensagem('Erro ao Excluir a NF. Tente Novamente.');", True)
                    End If
                End If
            End If

            If e.CommandName = "EDITAR" Then

                If Not String.IsNullOrEmpty(Me.lblCodigoAgendamento.Text.Trim()) And
                    Not String.IsNullOrWhiteSpace(Me.lblCodigoAgendamento.Text.Trim()) Then

                    Dim Ds As New DataTable
                    Ds = Banco.List("SELECT AUTONUM,DANFE,NUMERO,SERIE,EMISSOR,TO_CHAR(EMISSAO,'DD/MM/YYYY') EMISSAO,VALOR,PESO_BRUTO,ARQUIVO_DANFE FROM REDEX.TB_AGENDAMENTO_WEB_CNTR_NF WHERE AUTONUM = " & ID)

                    If Ds IsNot Nothing Then
                        If Ds.Rows.Count > 0 Then

                            Me.lblCodigoNF.Text = Ds.Rows(0)("AUTONUM").ToString()
                            Me.txtDANFE.Text = Ds.Rows(0)("DANFE").ToString()
                            Me.txtNumero.Text = Ds.Rows(0)("NUMERO").ToString()
                            Me.txtSerie.Text = Ds.Rows(0)("SERIE").ToString()
                            Me.txtEmissor.Text = Ds.Rows(0)("EMISSOR").ToString()
                            Me.txtEmissao.Text = Ds.Rows(0)("EMISSAO").ToString()
                            'Me.txtQtde.Text = Ds.Rows(0)("QTDE").ToString()
                            Me.txtValor.Text = Ds.Rows(0)("VALOR").ToString()
                            Me.txtPesoBruto.Text = Ds.Rows(0)("PESO_BRUTO").ToString()
                            Me.txtXml.Text = Ds.Rows(0)("ARQUIVO_DANFE").ToString()
                            'Me.txtM3.Text = Ds.Rows(0)("M3").ToString()

                        End If
                    End If

                End If
            End If

        End If

    End Sub

    Protected Sub btnCancelarNF_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnCancelarNF.Click

        Me.lblCodigoNF.Text = String.Empty
        Me.txtDANFE.Text = String.Empty
        Me.txtNumero.Text = String.Empty
        Me.txtSerie.Text = String.Empty
        Me.txtEmissor.Text = String.Empty
        Me.txtEmissao.Text = String.Empty
        'Me.txtQtde.Text = String.Empty
        Me.txtValor.Text = String.Empty
        Me.txtPesoBruto.Text = String.Empty
        'Me.txtM3.Text = String.Empty

    End Sub

    Private Sub HabilitaCamposCNTR(ByVal Habilita As Boolean)

        If Habilita Then
            Me.txtConteiner.Enabled = True
            Me.txtVolumesCntr.Enabled = True
            Me.cbTamanho.Enabled = True
            Me.cbTipo.Enabled = True
            Me.txtTara.Enabled = True
            Me.txtPesoBrutoCntr.Enabled = True
            Me.txtUn1.Enabled = True
            Me.txtUn2.Enabled = True
            Me.txtUn3.Enabled = True
            Me.txtUn4.Enabled = True
            Me.txtIMO1.Enabled = True
            Me.txtIMO2.Enabled = True
            Me.txtIMO3.Enabled = True
            Me.txtIMO4.Enabled = True
            Me.txtTemp.Enabled = True
            Me.txtEscala.Enabled = True
            Me.txtUmidade.Enabled = True
            Me.txtVent.Enabled = True
            Me.txtComprimento.Enabled = True
            Me.txtLatEsq.Enabled = True
            Me.txtAltura.Enabled = True
            Me.txtLatDir.Enabled = True
            Me.txtLacre1.Enabled = True
            Me.txtLacre2.Enabled = True
            Me.txtLacre3.Enabled = True
            Me.txtLacre4.Enabled = True
            Me.txtLacre5.Enabled = True
            Me.txtLacre6.Enabled = True
            Me.txtLacre7.Enabled = True
            Me.txtLacre8.Enabled = True
        Else
            Me.txtConteiner.Enabled = False
            Me.txtVolumesCntr.Enabled = False
            Me.cbTamanho.Enabled = False
            Me.cbTipo.Enabled = False
            Me.txtTara.Enabled = False
            Me.txtPesoBrutoCntr.Enabled = False
            Me.txtUn1.Enabled = False
            Me.txtUn2.Enabled = False
            Me.txtUn3.Enabled = False
            Me.txtUn4.Enabled = False
            Me.txtIMO1.Enabled = False
            Me.txtIMO2.Enabled = False
            Me.txtIMO3.Enabled = False
            Me.txtIMO4.Enabled = False
            Me.txtTemp.Enabled = False
            Me.txtEscala.Enabled = False
            Me.txtUmidade.Enabled = False
            Me.txtVent.Enabled = False
            Me.txtComprimento.Enabled = False
            Me.txtLatEsq.Enabled = False
            Me.txtAltura.Enabled = False
            Me.txtLatDir.Enabled = False
            Me.txtLacre1.Enabled = False
            Me.txtLacre2.Enabled = False
            Me.txtLacre3.Enabled = False
            Me.txtLacre4.Enabled = False
            Me.txtLacre5.Enabled = False
            Me.txtLacre6.Enabled = False
            Me.txtLacre7.Enabled = False
            Me.txtLacre8.Enabled = False
        End If

    End Sub

    Private Sub HabilitaCamposNF(ByVal Habilita As Boolean)

        If Habilita Then
            'Me.txtDANFE.Enabled = True
            'Me.txtNumero.Enabled = True
            'Me.txtSerie.Enabled = True
            'Me.txtEmissor.Enabled = True
            'Me.txtEmissao.Enabled = True
            'Me.txtQtde.Enabled = True
            'Me.txtValor.Enabled = True
            Me.txtPesoBruto.Enabled = True
            'Me.txtM3.Enabled = True
            Me.btnSalvarNF.Enabled = True
            Me.txtDANFE.Focus()
        Else
            Me.txtDANFE.Enabled = False
            Me.txtNumero.Enabled = False
            Me.txtSerie.Enabled = False
            Me.txtEmissor.Enabled = False
            Me.txtEmissao.Enabled = False
            'Me.txtQtde.Enabled = False
            Me.txtValor.Enabled = False
            Me.txtPesoBruto.Enabled = False
            'Me.txtM3.Enabled = False
        End If

    End Sub

    Protected Sub btnCancelar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnCancelar.Click

        If Me.lblCodigoAgendamento.Text <> String.Empty Then
            If Banco.BeginTransaction("DELETE FROM REDEX.TB_GD_CONTEINER WHERE AUTONUM_GD_CNTR = " & Me.lblCodigoAgendamento.Text) Then
                Banco.BeginTransaction("DELETE FROM REDEX.TB_AGENDAMENTO_WEB_CNTR_NF WHERE AUTONUM_AGENDAMENTO = " & Me.lblCodigoAgendamento.Text)
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgAlerta", "document.location.href='AgendarCS.aspx';", True)
            End If
        Else
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgAlerta", "document.location.href='AgendarCS.aspx';", True)
        End If

    End Sub

    Protected Sub btnSair_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSair.Click

        If Request.QueryString("id") IsNot Nothing Then
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgAlerta", "document.location.href='Default.aspx';", True)
        Else
            If Me.lblCodigoAgendamento.Text <> String.Empty Then
                If Banco.BeginTransaction("DELETE FROM REDEX.TB_GD_CONTEINER WHERE AUTONUM_GD_CNTR = " & Me.lblCodigoAgendamento.Text) Then
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgAlerta", "document.location.href='Default.aspx';", True)
                End If
            Else
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgAlerta", "document.location.href='Default.aspx';", True)
            End If
        End If

    End Sub

    Protected Sub cbCavalo_OnSelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cbCavalo.SelectedIndexChanged

        If Me.cbCavalo.Text <> String.Empty Then
            Me.AccordionIndex.Value = 1
            Me.ConsultarCarretas(Me.cbCavalo.Text)
        End If

    End Sub

    Public Sub ConsultarPeriodos(ByVal Patio As Integer)

        Dim SQL As New StringBuilder

        SQL.Append("SELECT ")
        SQL.Append("  AUTONUM_GD_RESERVA, ")
        SQL.Append("  PERIODO_INICIAL, ")
        SQL.Append("  PERIODO_FINAL, ")
        SQL.Append("  NVL(LIMITE_CAMINHOES,0) LIMITE_CAMINHOES ")
        SQL.Append("FROM ")
        SQL.Append("  ( ")
        SQL.Append("    SELECT ")
        SQL.Append("      A.AUTONUM_GD_RESERVA, ")
        SQL.Append("      TO_CHAR(A.PERIODO_INICIAL, 'DD/MM/YYYY HH24:MI') PERIODO_INICIAL, ")
        SQL.Append("      TO_CHAR(A.PERIODO_FINAL, 'DD/MM/YYYY HH24:MI') PERIODO_FINAL, ")
        SQL.Append("      A.LIMITE_CAMINHOES - ")
        SQL.Append("        ( ")
        SQL.Append("            SELECT NVL(COUNT(1),0) FROM REDEX.TB_GD_CONTEINER WHERE TB_GD_CONTEINER.AUTONUM_GD_RESERVA = A.AUTONUM_GD_RESERVA ")
        SQL.Append("        ) LIMITE_CAMINHOES ")
        SQL.Append("  FROM ")
        SQL.Append("      REDEX.TB_GD_RESERVA A ")
        SQL.Append("  WHERE ")
        SQL.Append("      A.SERVICO_GATE = 'C' AND A.TIPO = 'CN' AND TO_CHAR(A.PERIODO_INICIAL,'YYYYMMDDHH24MI') >= TO_CHAR(SYSDATE,'YYYYMMDDHH24MI') ")

        If Val(Me.lblPatio.Text) <> 0 Then
            SQL.Append("  AND A.PATIO = " & Val(Me.lblPatio.Text))
        Else
            SQL.Append(" AND A.PATIO = 9999 ")
        End If

        SQL.Append("  ) Q ")
        SQL.Append("  WHERE AUTONUM_GD_RESERVA > 0 ")

        Dim DsDes As New DataTable
        DsDes = Banco.List("SELECT FLAG_CS_M3, FLAG_CS_PESO, FLAG_CS_VOLUME, FLAG_CS_CAMINHAO, FLAG_CN_CAMINHAO FROM REDEX.TB_AGENDAMENTO_WEB_PERIODO_DES")

        If DsDes IsNot Nothing Then
            If Convert.ToInt32(DsDes.Rows(0)("FLAG_CS_CAMINHAO").ToString()) > 0 Then
                SQL.Append(" AND LIMITE_CAMINHOES > 0 ")
            End If
        End If

        SQL.Append("ORDER BY TO_DATE(PERIODO_INICIAL,'DD/MM/YYYY HH24:MI') ")

        Me.dgConsultaPeriodos.DataSource = Banco.List(SQL.ToString())
        Me.dgConsultaPeriodos.DataBind()

    End Sub

    Public Function ObterViagem(ByVal ID_Conteiner As String) As String
        Return Banco.ExecuteScalar(String.Format("SELECT NVL(AUTONUMVIAGEM,0) AUTONUMVIAGEM FROM OPERADOR.TB_GD_CONTEINER WHERE ID_CONTEINER='{0}'", ID_Conteiner))
    End Function

    Public Function ObterLine(ByVal Viagem As String, ByVal Reserva As String) As String
        Return Banco.ExecuteScalar(String.Format("select carrier from tb_booking where reference= '{0}' and autonumviagem = '{1}' and atual = 1", Reserva, Viagem))
    End Function

    Protected Sub dgConsultaPeriodos_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles dgConsultaPeriodos.RowCommand

        If Me.dgConsultaPeriodos.Rows.Count > 0 Then

            Me.AccordionIndex.Value = 4

            Dim Index As Integer = e.CommandArgument
            Dim ID As String = Me.dgConsultaPeriodos.DataKeys(Index)("AUTONUM_GD_RESERVA").ToString()
            Dim Periodo As String = String.Empty

            If e.CommandName = "SEL" Then
                If Not String.IsNullOrEmpty(ID) And
                    Not String.IsNullOrWhiteSpace(ID) Then

                    Me.lblCodigoPeriodo.Text = ID
                    Periodo = Banco.ExecuteScalar("SELECT TO_CHAR(PERIODO_INICIAL,'DD/MM/YYYY HH24:MI') || ' - ' || TO_CHAR(PERIODO_FINAL,'DD/MM/YYYY HH24:MI') PERIODO FROM REDEX.TB_GD_RESERVA WHERE AUTONUM_GD_RESERVA = " & ID)

                    For Each Linha As GridViewRow In Me.dgConsultaPeriodos.Rows
                        Linha.BackColor = Drawing.Color.Transparent
                    Next

                    For Each Linha As GridViewRow In Me.dgConsultaPeriodos.Rows
                        If Linha.Cells(1).Text & " - " & Linha.Cells(2).Text = Periodo Then
                            Linha.BackColor = Drawing.Color.MistyRose
                        End If
                    Next

                End If
            End If

        End If

    End Sub


    Protected Sub btnUploadNota_Click(sender As Object, e As EventArgs) Handles btnUploadNota.Click

        Me.AccordionIndex.Value = 3

        msgSucessoDanfe.Text = ""
        msgErroNF.Text = ""

        msgSucessoDanfe.Visible = False
        msgErroNF.Visible = False

        Me.msgErroNF.Visible = False
        Me.msgSucessoDanfe.Visible = False

        If txtArquivoDanfe.HasFile Then

            If Not txtArquivoDanfe.FileName.ToUpper().EndsWith("XML") Then

                Me.msgErroNF.Text = "Tipo de arquivo não permitido"
                Me.msgErroNF.Visible = True

                Exit Sub

            End If

            If txtArquivoDanfe.PostedFile.ContentType <> "text/xml" Then
                Me.msgErroNF.Text = "O arquivo informado não é um XML válido"
                Me.msgErroNF.Visible = True

                Exit Sub

            End If

            Dim NomeArquivo As String = String.Empty
            Dim TamanhoArquivo As Integer = 0

            If txtArquivoDanfe.HasFile Then

                NomeArquivo = Path.GetFileName(Me.txtArquivoDanfe.PostedFile.FileName)
                TamanhoArquivo = Me.txtArquivoDanfe.PostedFile.ContentLength

                If TamanhoArquivo > 5242880 Then

                    Me.msgErroNF.Text = "O tamanho do arquivo ultrapassa 5MB"
                    Me.msgErroNF.Visible = True

                    Exit Sub

                End If

                Try

                    Dim doc As New XmlDocument()
                    doc.Load(Me.txtArquivoDanfe.PostedFile.InputStream)

                    Dim ns As New XmlNamespaceManager(doc.NameTable)
                    ns.AddNamespace("nfe", "http://www.portalfiscal.inf.br/nfe")

                    txtXml.Text = doc.InnerXml

                    Dim dadosDanfe = doc.SelectNodes("//nfe:infProt", ns)

                    For Each dadoDanfe As XmlNode In dadosDanfe
                        For Each childNode As XmlNode In dadoDanfe.ChildNodes
                            If childNode.Name = "chNFe" Then
                                Me.txtDANFE.Text = childNode.InnerText
                            End If
                        Next
                    Next

                    If Me.txtDANFE.Text.Length = 0 Then
                        Dim chaveDanfe = doc.SelectSingleNode("//nfe:infNFe", ns)
                        If chaveDanfe.Attributes.Count = 2 Then
                            Me.txtDANFE.Text = chaveDanfe.Attributes.GetNamedItem("Id").Value

                            If Me.txtDANFE.Text.Contains("NFe") Then
                                Me.txtDANFE.Text = Me.txtDANFE.Text.Replace("NFe", "")
                            End If
                        End If
                    End If

                    If Me.txtDANFE.Text.Length <> 44 Then

                        Me.msgErroNF.Text = "Danfe inválida"
                        Me.msgErroNF.Visible = True

                        Exit Sub

                    End If

                    Dim dadosEmit = doc.SelectNodes("//nfe:emit", ns)

                    For Each dadoEmit As XmlNode In dadosEmit
                        For Each childNode As XmlNode In dadoEmit.ChildNodes
                            If childNode.Name = "xNome" Then
                                Me.txtEmissor.Text = childNode.InnerText
                            End If
                        Next
                    Next

                    Dim dadosNota = doc.SelectNodes("//nfe:ide", ns)

                    For Each dadoNfe As XmlNode In dadosNota
                        For Each childNode As XmlNode In dadoNfe.ChildNodes
                            If childNode.Name = "nNF" Then
                                Me.txtNumero.Text = childNode.InnerText
                            End If
                            If childNode.Name = "serie" Then
                                Me.txtSerie.Text = childNode.InnerText
                            End If
                            If childNode.Name = "dhEmi" Then
                                Me.txtEmissao.Text = Convert.ToDateTime(childNode.InnerText).AddHours(3).ToString("dd/MM/yyyy")
                            End If
                        Next
                    Next

                    Dim dadosProduto = doc.SelectNodes("//nfe:prod", ns)

                    Dim SomaQuantidade As Decimal
                    Dim SomaValor As Decimal

                    For Each dadoProd As XmlNode In dadosProduto
                        For Each childNode As XmlNode In dadoProd.ChildNodes
                            If childNode.Name = "vProd" Then
                                SomaValor = SomaValor + Convert.ToDecimal(childNode.InnerText.Replace(".", ","))
                            End If
                            If childNode.Name = "qTrib" Then
                                SomaQuantidade = SomaQuantidade + Convert.ToDecimal(childNode.InnerText.Replace(".", ","))
                            End If
                        Next
                    Next

                    Me.txtValor.Text = SomaValor
                    'Me.txtQtde.Text = SomaQuantidade

                    Dim dadosVolumes = doc.SelectNodes("//nfe:transp", ns)

                    For Each dadoVolume As XmlNode In dadosVolumes
                        For Each childNode As XmlNode In dadoVolume.ChildNodes
                            If childNode.Name = "vol" Then

                                For Each tag As XmlNode In childNode.ChildNodes
                                    If tag.Name = "qVol" Then
                                        ' Me.txtQtde.Text = tag.InnerText
                                    End If
                                    If tag.Name = "pesoB" Then
                                        Me.txtPesoBruto.Text = tag.InnerText
                                    End If
                                Next

                            End If
                        Next
                    Next

                Catch ex As Exception
                    Me.msgErroNF.Text = ex.Message
                    Me.msgErroNF.Visible = True
                End Try

            End If

        End If

    End Sub

End Class