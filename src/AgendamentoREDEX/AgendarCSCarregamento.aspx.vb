Public Class AgendarCSCarregamento
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not Page.IsPostBack Then

            ConsultarCavalos()
            ConsultarTransportadora()
            ConsultarPeriodos()

            If Request.QueryString("id") IsNot Nothing Then

                Dim SQL As New StringBuilder

                SQL.Append("SELECT ")
                SQL.Append("  A.AUTONUM, ")
                SQL.Append("  B.NOME, ")
                SQL.Append("  B.CNH, ")
                SQL.Append("  A.AUTONUM_MOTORISTA, ")
                SQL.Append("  A.AUTONUM_VEICULO, ")
                SQL.Append("  TO_CHAR(A.DATA_AGENDAMENTO,'DD/MM/YYYY') DATA_AGENDAMENTO, ")
                SQL.Append("  C.PLACA_CAVALO, ")
                SQL.Append("  C.PLACA_CARRETA, ")
                SQL.Append("  A.NUM_PROTOCOLO || '/' || A.ANO_PROTOCOLO AS PROTOCOLO, ")
                SQL.Append(" case when A.STATUS='GE' then 'Gerado' ELSE  'Impresso'  END AS STATUS, ")
                SQL.Append("  A.RESERVA, ")
                SQL.Append("  A.AUTONUM_GD_RESERVA ")                
                SQL.Append("FROM ")
                SQL.Append("  REDEX.TB_AGENDAMENTO_WEB_CS_CA A ")
                SQL.Append("INNER JOIN ")
                SQL.Append("  OPERADOR.TB_AG_MOTORISTAS B ON A.AUTONUM_MOTORISTA = B.AUTONUM ")
                SQL.Append("INNER JOIN ")
                SQL.Append("  OPERADOR.TB_AG_VEICULOS C ON A.AUTONUM_VEICULO = C.AUTONUM ")
                SQL.Append("WHERE A.AUTONUM = " & Request.QueryString("id").ToString())

                Dim Ds As New DataTable
                Ds = Banco.List(SQL.ToString())

                Dim Periodo As String

                If Ds IsNot Nothing Then
                    If Ds.Rows.Count > 0 Then

                        Me.txtReserva.Text = Ds.Rows(0)("RESERVA").ToString()
                        txtReserva_TextChanged(sender, e)

                        Me.lblCodigoAgendamento.Text = Ds.Rows(0)("AUTONUM").ToString()
                        Me.lblCodigoMotorista.Text = Ds.Rows(0)("AUTONUM_MOTORISTA").ToString()
                        Me.lblCodigoVeiculo.Text = Ds.Rows(0)("AUTONUM_VEICULO").ToString()
                        Me.lblCodigoPeriodo.Text = Ds.Rows(0)("AUTONUM_GD_RESERVA").ToString()

                        Me.txtMotorista.Text = Ds.Rows(0)("NOME").ToString()
                        Me.cbCavalo.Text = Ds.Rows(0)("PLACA_CAVALO").ToString()

                        ConsultarCarretas(Me.cbCavalo.Text)

                        Me.cbCarreta.Text = Ds.Rows(0)("PLACA_CARRETA").ToString()

                        Me.btnSalvarMercadoria.Enabled = True
                        ConsultarQuantidades()
                        ConsultarPeriodos()
                        ConsultarItensReserva()

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
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgAlerta", "exibeMensagem('Registro inexistente!','ConsultarAgendamentosCargaSoltaCarregamento.aspx');", True)
                    End If
                End If

            Else

                If Request.QueryString("booking") IsNot Nothing Then

                    Dim SQL As New StringBuilder

                    SQL.Append("SELECT")
                    SQL.Append("    AUTONUM, ")
                    SQL.Append("    RESERVA, ")
                    SQL.Append("    QUANTIDADE, ")
                    SQL.Append("    PESO, ")
                    SQL.Append("    VOLUMES, ")
                    SQL.Append("    METRAGEM_CUBICA, ")
                    SQL.Append("    ARMADOR, ")
                    SQL.Append("    NAVIO, ")
                    SQL.Append("    EXPORTADOR, ")
                    SQL.Append("    NVOCC, ")
                    SQL.Append("    NUM_VIAGEM, ")
                    SQL.Append("    PORTO_ORIGEM, ")
                    SQL.Append("    PORTO_DESTINO, ")
                    SQL.Append("    DT_DEAD_LINE, ")
                    SQL.Append("    AUTONUM_VIAGEM ")
                    SQL.Append("FROM ")
                    SQL.Append("    REDEX.VW_AGENDAMENTO_WEB_DADOS_BOO ")
                    SQL.Append(" WHERE AUTONUM = " & Request.QueryString("booking").ToString())

                    Dim Ds As New DataTable
                    Ds = Banco.List(SQL.ToString())

                    If Ds IsNot Nothing Then
                        If Ds.Rows.Count > 0 Then
                            For Each Linha As DataRow In Ds.Rows

                                Me.lblDeadLine.Text = Linha("DT_DEAD_LINE").ToString()
                                Me.lblExportador.Text = Linha("EXPORTADOR").ToString()
                                Me.lblM3.Text = Linha("METRAGEM_CUBICA").ToString()
                                Me.lblDisponiveis.Text = Linha("VOLUMES").ToString()
                                Me.lblNavio.Text = Linha("NAVIO").ToString()
                                Me.lblPesoBruto.Text = Linha("PESO").ToString()
                                Me.lblStatus.Text = "Liberado"
                                Me.lblTipo.Text = "Carga Solta"
                                Me.lblTotal.Text = Linha("VOLUMES").ToString()
                                Me.lblViagem.Text = Linha("NUM_VIAGEM").ToString()
                                Me.lblVolumes.Text = Linha("VOLUMES").ToString()
                                Me.lblCodigoBooking.Text = Linha("AUTONUM").ToString()
                                Me.txtReserva.Text = Linha("RESERVA").ToString()
                                Me.lblCodigoViagem.Text = Linha("AUTONUM_VIAGEM").ToString()

                                Me.txtReserva.Enabled = False

                                If Convert.ToInt32(Banco.ExecuteScalar("SELECT COUNT(1) FROM REDEX.TB_BOOKING_CARGA WHERE FLAG_CS = 1 AND AUTONUM_BOO = " & Me.lblCodigoBooking.Text)) = 0 Then
                                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgAlerta", "exibeMensagem('A Reserva informada (" & Me.txtReserva.Text.ToUpper() & ") não é de Carga Solta.','AgendarCS.aspx');", True)
                                    Exit Sub
                                End If

                                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "exibeAccordion", "document.getElementById('accordion').style.display = 'block';", True)
                                Me.txtMotorista.Focus()

                                ConsultarQuantidades()
                                ConsultarCarga()

                                Me.btnSalvar.Visible = True
                                Me.btnCancelar.Visible = True

                                pnCadastro.Visible = False
                                modalReserva.Hide()

                            Next
                        Else

                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgAlerta", "exibeMensagem('Reserva não encontrada!');", True)

                            Me.lblDeadLine.Text = String.Empty
                            Me.lblExportador.Text = String.Empty
                            Me.lblM3.Text = String.Empty
                            Me.lblNavio.Text = String.Empty
                            Me.lblPesoBruto.Text = String.Empty
                            Me.lblStatus.Text = String.Empty
                            Me.lblTipo.Text = String.Empty
                            Me.lblTotal.Text = String.Empty
                            Me.lblViagem.Text = String.Empty
                            Me.lblVolumes.Text = String.Empty

                        End If
                    End If

                End If

            End If

        End If


        If Not String.IsNullOrEmpty(Me.lblCodigoBooking.Text) Then
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "exibeAccordion", "document.getElementById('accordion').style.display = 'block';", True)
        End If

    End Sub

    Private Sub ConsultarQuantidades()

        Dim SQL As New StringBuilder

        SQL.Append("SELECT RESERVA, ")
        SQL.Append("       UTILIZADOS ")
        SQL.Append("FROM ")
        SQL.Append("  (SELECT A.RESERVA, ")
        SQL.Append("    (SELECT NVL(SUM(QTDE),0) ")
        SQL.Append("    FROM REDEX.TB_AGENDAMENTO_WEB_CS_ITENS ")
        SQL.Append("    WHERE REDEX.TB_AGENDAMENTO_WEB_CS_ITENS.AUTONUM_AGENDAMENTO = A.AUTONUM ")
        SQL.Append("    ) UTILIZADOS ")
        SQL.Append("  FROM REDEX.TB_AGENDAMENTO_WEB_CS_CA A ")
        SQL.Append("  WHERE UPPER(A.RESERVA) = '" & Me.txtReserva.Text.ToUpper() & "' ")
        SQL.Append("  )Q ")
        SQL.Append("GROUP BY RESERVA,UTILIZADOS ")

        Dim Ds As New DataTable
        Ds = Banco.List(SQL.ToString())

        If Ds IsNot Nothing Then
            If Ds.Rows.Count > 0 Then
                Me.lblUtilizados.Text = Ds.Rows(0)("UTILIZADOS").ToString()
                Me.lblDisponiveis.Text = (Nnull(Me.lblTotal.Text, 0) - Nnull(Me.lblUtilizados.Text, 0))
            End If
        End If

    End Sub

    Public Sub ConsultarCarga()

        Dim SQL As New StringBuilder

        SQL.Append("SELECT A.AUTONUM_BCG, ")
        SQL.Append("  A.DESC_PRODUTO ")
        SQL.Append("  || ' - ' ")
        SQL.Append("  || A.DESCRICAO_EMB ")
        SQL.Append("  || ' - Saldo: (' ")
        SQL.Append("  || (A.QTDE - ")
        SQL.Append("  TO_NUMBER((SELECT NVL(SUM(QTDE),0) QTDE ")
        SQL.Append("    FROM REDEX.TB_AGENDAMENTO_WEB_CS_ITENS WHERE AUTONUM_MERCADORIA = A.AUTONUM_BCG),'999')) ")
        SQL.Append("  || ')' AS PRODUTO ")
        SQL.Append("FROM REDEX.VW_BOOKING_CS A ")
        SQL.Append("WHERE A.AUTONUM_BOO = " & Me.lblCodigoBooking.Text)

        Me.cbMercadoria.DataSource = Banco.List(SQL.ToString())
        Me.cbMercadoria.DataBind()

        Me.cbMercadoria.Items.Insert(0, New ListItem("", ""))

    End Sub

    Public Sub ConsultarCavalos()

        Me.cbCavalo.DataSource = Banco.List("SELECT PLACA_CAVALO FROM OPERADOR.TB_AG_VEICULOS WHERE ID_TRANSPORTADORA = " & Session("SIS_AUTONUM_TRANSPORTADORA").ToString())
        Me.cbCavalo.DataBind()

        Me.cbCavalo.Items.Insert(0, New ListItem("", ""))

    End Sub

    Public Sub ConsultarCarretas(ByVal Cavalo As String)

        Me.cbCarreta.DataSource = Banco.List("SELECT PLACA_CARRETA FROM OPERADOR.TB_AG_VEICULOS WHERE ID_TRANSPORTADORA = " & Session("SIS_AUTONUM_TRANSPORTADORA").ToString() & " AND PLACA_CAVALO = '" & Me.cbCavalo.Text & "'")
        Me.cbCarreta.DataBind()

        Me.cbCarreta.Items.Insert(0, New ListItem("", ""))

    End Sub

    Public Sub ConsultarTransportadora()
        Me.lblTransportadora.Text = Banco.ExecuteScalar("SELECT RAZAO FROM OPERADOR.TB_CAD_TRANSPORTADORAS WHERE AUTONUM = " & Session("SIS_AUTONUM_TRANSPORTADORA").ToString())
    End Sub

    Protected Sub txtReserva_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles txtReserva.TextChanged

        Dim SQL As New StringBuilder

        If Not String.IsNullOrEmpty(Me.txtReserva.Text.Trim()) And
            Not String.IsNullOrWhiteSpace(Me.txtReserva.Text.Trim()) Then

            If Me.lblCodigoAgendamento.Text <> String.Empty Then                
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

            If Convert.ToInt32(Banco.ExecuteScalar("SELECT COUNT(1) FROM REDEX.TB_BOOKING A INNER JOIN REDEX.TB_BOOKING_CARGA B ON A.AUTONUM_BOO = B.AUTONUM_BOO WHERE A.REFERENCE = '" & Me.txtReserva.Text & "' AND B.FLAG_CS = 1")) > 1 Then
                frameCadastro.Attributes("src") = "EscolheReserva.aspx?reserva=" & Me.txtReserva.Text
                pnCadastro.Visible = True
                modalReserva.Show()
                Exit Sub
            End If

            Dim Ds As New DataTable
            
            SQL.Clear()
            SQL.Append("SELECT")
            SQL.Append("    AUTONUM, ")
            SQL.Append("    RESERVA, ")
            SQL.Append("    QUANTIDADE, ")
            SQL.Append("    VOLUMES, ")
            SQL.Append("    PESO, ")
            SQL.Append("    METRAGEM_CUBICA, ")
            SQL.Append("    NAVIO, ")
            SQL.Append("    NVOCC, ")
            SQL.Append("    EXPORTADOR, ")
            SQL.Append("    NVOCC, ")
            SQL.Append("    AUTONUM_VIAGEM, ")
            SQL.Append("    NUM_VIAGEM, ")
            SQL.Append("    DT_DEAD_LINE, ")
            SQL.Append("    FLAG_CS, ")
            SQL.Append("    FLAG_CNTR, ")
            SQL.Append("    PATIO ")
            SQL.Append("FROM ")
            SQL.Append("    REDEX.VW_AGENDAMENTO_WEB_DADOS_BOO ")
            SQL.Append(" WHERE UPPER(RESERVA) = '" & Me.txtReserva.Text.ToUpper() & "' AND NVL(FLAG_CS,0) = 1")

            Ds = Banco.List(SQL.ToString())

            If Ds IsNot Nothing Then
                If Ds.Rows.Count > 0 Then
                    For Each Linha As DataRow In Ds.Rows

                        Me.lblDeadLine.Text = Linha("DT_DEAD_LINE").ToString()
                        Me.lblExportador.Text = Linha("EXPORTADOR").ToString()
                        Me.lblM3.Text = Linha("METRAGEM_CUBICA").ToString()
                        Me.lblNavio.Text = Linha("NAVIO").ToString()
                        Me.lblPesoBruto.Text = Linha("PESO").ToString()
                        Me.lblStatus.Text = "Liberado"
                        Me.lblPesoBruto.Text = Linha("PESO").ToString()
                        Me.lblTipo.Text = "Carga Solta"
                        Me.lblTotal.Text = Linha("VOLUMES").ToString()
                        Me.lblDisponiveis.Text = Linha("VOLUMES").ToString()
                        Me.lblViagem.Text = Linha("NUM_VIAGEM").ToString()
                        Me.lblVolumes.Text = Linha("VOLUMES").ToString()
                        Me.lblCodigoBooking.Text = Linha("AUTONUM").ToString()
                        Me.lblCodigoViagem.Text = Linha("AUTONUM_VIAGEM").ToString()
                        Me.lblCodigoPatio.Text = Linha("PATIO").ToString()

                        Me.lblUtilizados.Text = "0"

                        If Convert.ToInt32(Banco.ExecuteScalar("SELECT COUNT(1) FROM REDEX.TB_BOOKING_CARGA WHERE FLAG_CS = 1 AND AUTONUM_BOO = " & Me.lblCodigoBooking.Text)) = 0 Then
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgAlerta", "exibeMensagem('A Reserva informada (" & Me.txtReserva.Text.ToUpper() & ") não é de Carga Solta.','AgendarCS.aspx');", True)
                            Exit Sub
                        End If

                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "exibeAccordion", "document.getElementById('accordion').style.display = 'block';", True)
                        Me.txtMotorista.Focus()

                        ConsultarQuantidades()
                        ConsultarCarga()
                        ConsultarPeriodos()

                        Me.btnSalvar.Visible = True
                        Me.btnCancelar.Visible = True

                    Next

                Else                    
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgAlerta", "exibeMensagem('Reserva inválida!');", True)
                End If

                If Me.lblCodigoBooking.Text = String.Empty Then

                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgAlerta", "exibeMensagem('Reserva não encontrada!');", True)

                    Me.lblDeadLine.Text = String.Empty
                    Me.lblExportador.Text = String.Empty
                    Me.lblM3.Text = String.Empty
                    Me.lblNavio.Text = String.Empty
                    Me.lblPesoBruto.Text = String.Empty
                    Me.lblStatus.Text = String.Empty
                    Me.lblTipo.Text = String.Empty
                    Me.lblTotal.Text = String.Empty
                    Me.lblViagem.Text = String.Empty
                    Me.lblVolumes.Text = String.Empty

                End If

            End If
        End If


    End Sub

    Protected Sub txtMotorista_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles txtMotorista.TextChanged

        If Not String.IsNullOrEmpty(Me.txtMotorista.Text.Trim()) And
                Not String.IsNullOrWhiteSpace(Me.txtMotorista.Text.Trim()) Then
            Me.txtMotorista.Text = Banco.ExecuteScalar("SELECT DISTINCT NOME || ' - ' || CNH FROM OPERADOR.TB_AG_MOTORISTAS WHERE (UPPER(NOME) LIKE '%" & Me.txtMotorista.Text.ToUpper() & "%' OR CNH = '" & Me.txtMotorista.Text & "') AND ID_TRANSPORTADORA = " & Session("SIS_AUTONUM_TRANSPORTADORA").ToString())
            Me.AccordionIndex.Value = 1
        End If

    End Sub

    Protected Sub btnSalvar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSalvar.Click

        Dim SQL As New StringBuilder

        If Me.btnSalvar.Text = "Concluir" Then

            If Me.lblCodigoPeriodo.Text = String.Empty Then
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgAlerta", "exibeMensagem('Nenhum período foi selecionado.','');", True)
                Me.AccordionIndex.Value = 3
                Exit Sub
            End If

            SQL.Append("UPDATE ")
            SQL.Append("  REDEX.TB_AGENDAMENTO_WEB_CS_CA ")
            SQL.Append("  SET ")
            SQL.Append("    AUTONUM_GD_RESERVA = " & Me.lblCodigoPeriodo.Text & " ")
            SQL.Append("  WHERE AUTONUM = " & Me.lblCodigoAgendamento.Text)

            If Banco.BeginTransaction(SQL.ToString()) Then
                'If Agendamento.InsereAgendamentoNaFila(0, Val(Me.lblCodigoPeriodo.Text), Val(Me.lblCodigoBooking.Text), Val(Me.lblCodigoAgendamento.Text), TipoAgendamento.CARGA_SOLTA_CARREGAMENTO) Then
                Response.Redirect("ConsultarAgendamentosCargaSoltaCarregamento.aspx")
                'End If
            End If

        End If

        If ValidarCampos() Then

            If Request.QueryString("id") Is Nothing Then

                Me.lblCodigoAgendamento.Text = Banco.ExecuteScalar("SELECT REDEX.SEQ_AGENDAMENTO_WEB_CS_CA.NEXTVAL FROM DUAL")

                If Not String.IsNullOrEmpty(Me.lblCodigoAgendamento.Text.Trim()) And Not String.IsNullOrWhiteSpace(Me.lblCodigoAgendamento.Text.Trim()) Then

                    Me.lblCodigoMotorista.Text = Banco.ExecuteScalar("SELECT AUTONUM FROM OPERADOR.TB_AG_MOTORISTAS WHERE UPPER(TRIM(NOME)) = '" & ObterNomeMotorista(Me.txtMotorista.Text.ToUpper()) & "' AND ID_TRANSPORTADORA = " & Session("SIS_AUTONUM_TRANSPORTADORA").ToString())
                    Me.lblCodigoVeiculo.Text = Banco.ExecuteScalar("SELECT AUTONUM FROM OPERADOR.TB_AG_VEICULOS WHERE UPPER(PLACA_CAVALO) = '" & Me.cbCavalo.Text & "' AND UPPER(PLACA_CARRETA) = '" & Me.cbCarreta.Text & "' AND ID_TRANSPORTADORA = " & Session("SIS_AUTONUM_TRANSPORTADORA").ToString())
                    Me.lblCodigoProtocolo.Text = Banco.ExecuteScalar("SELECT REDEX.SEQ_AGENDAMENTO_WEB_PROT_" & Now.Year & ".NEXTVAL FROM DUAL")

                    SQL.Append("INSERT INTO ")
                    SQL.Append("  REDEX.TB_AGENDAMENTO_WEB_CS_CA ")
                    SQL.Append("  ( ")
                    SQL.Append("    AUTONUM, ")
                    SQL.Append("    RESERVA, ")
                    SQL.Append("    AUTONUM_VIAGEM, ")
                    SQL.Append("    AUTONUM_MOTORISTA, ")
                    SQL.Append("    AUTONUM_VEICULO, ")
                    SQL.Append("    AUTONUM_TRANSPORTADORA, ")
                    SQL.Append("    AUTONUM_GD_RESERVA, ")
                    SQL.Append("    AUTONUM_BOOKING, ")
                    SQL.Append("    DATA_AGENDAMENTO, ")
                    SQL.Append("    STATUS, ")
                    SQL.Append("    NUM_PROTOCOLO, ")
                    SQL.Append("    ANO_PROTOCOLO ")
                    SQL.Append("  ) ")
                    SQL.Append("  VALUES ")
                    SQL.Append("  ( ")
                    SQL.Append("  " & Me.lblCodigoAgendamento.Text & ", ")
                    SQL.Append("  '" & Me.txtReserva.Text.ToUpper() & "', ")
                    SQL.Append("  " & Me.lblCodigoViagem.Text & ", ")
                    SQL.Append("  " & Me.lblCodigoMotorista.Text & ", ")
                    SQL.Append("  " & Me.lblCodigoVeiculo.Text & ", ")
                    SQL.Append("  " & Session("SIS_AUTONUM_TRANSPORTADORA").ToString() & ", ")
                    SQL.Append("  " & Nnull(Me.lblCodigoPeriodo.Text, 0) & ", ")
                    SQL.Append("  " & Nnull(Me.lblCodigoBooking.Text, 0) & ", ")
                    SQL.Append("  SYSDATE, ")
                    SQL.Append("  'GE', ")
                    SQL.Append("  " & Me.lblCodigoProtocolo.Text & ", ")
                    SQL.Append("  " & Now.Year & " ")
                    SQL.Append("  ) ")

                    If Convert.ToInt32(Banco.BeginTransaction(SQL.ToString())) > 0 Then                        
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgAlerta", "exibeMensagem('Agendamento criado com sucesso! Vincule as Mercadorias utlizando os campos abaixo.');", True)
                        Me.AccordionIndex.Value = 2
                        Me.btnSalvar.Text = "Concluir"
                        Me.lblMsgSalvar.BackColor = System.Drawing.Color.FromName("#C1FFC1")
                        Me.lblMsgSalvar.Text = "Digite os Dados das Mercadorias e vincule-os ao agendamento clicando no botão Salvar."
                        Me.btnSalvarMercadoria.Enabled = True
                        ConsultarItensReserva()
                    Else                        
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgAlerta", "exibeMensagem('Erro ao criar um novo agendamento. Tente Novamente.');", True)
                    End If

                End If

            Else

                Me.lblCodigoMotorista.Text = Banco.ExecuteScalar("SELECT AUTONUM FROM OPERADOR.TB_AG_MOTORISTAS WHERE UPPER(TRIM(NOME)) = '" & ObterNomeMotorista(Me.txtMotorista.Text.ToUpper()) & "' AND ID_TRANSPORTADORA = " & Session("SIS_AUTONUM_TRANSPORTADORA").ToString())
                Me.lblCodigoVeiculo.Text = Banco.ExecuteScalar("SELECT AUTONUM FROM OPERADOR.TB_AG_VEICULOS WHERE UPPER(PLACA_CAVALO) = '" & Me.cbCavalo.Text & "' AND UPPER(PLACA_CARRETA) = '" & Me.cbCarreta.Text & "' AND ID_TRANSPORTADORA = " & Session("SIS_AUTONUM_TRANSPORTADORA").ToString())
                Me.lblCodigoProtocolo.Text = Banco.ExecuteScalar("SELECT REDEX.SEQ_AGENDAMENTO_WEB_PROT_" & Now.Year & ".NEXTVAL FROM DUAL")

                SQL.Append("UPDATE ")
                SQL.Append("  REDEX.TB_AGENDAMENTO_WEB_CS_CA ")
                SQL.Append("  SET ")
                SQL.Append("    AUTONUM_MOTORISTA = " & Me.lblCodigoMotorista.Text & ", ")
                SQL.Append("    AUTONUM_VEICULO = " & Me.lblCodigoVeiculo.Text & ", ")
                SQL.Append("    NUM_PROTOCOLO = " & Me.lblCodigoProtocolo.Text & ", ")
                SQL.Append("    ANO_PROTOCOLO = " & Now.Year & ", ")
                SQL.Append("    STATUS = 'GE', ")
                SQL.Append("    AUTONUM_GD_RESERVA = " & Me.lblCodigoPeriodo.Text & " ")
                SQL.Append("  WHERE AUTONUM = " & Me.lblCodigoAgendamento.Text)

                If Convert.ToInt32(Banco.BeginTransaction(SQL.ToString())) > 0 Then                    
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgAlerta", "exibeMensagem('Agendamento alterado com sucesso!','ConsultarAgendamentosCargaSoltaCarregamento.aspx');", True)
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

        If String.IsNullOrEmpty(Me.txtReserva.Text.Trim()) Or
            String.IsNullOrWhiteSpace(Me.txtReserva.Text.Trim()) Then            
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgAlerta", "exibeMensagem('Nenhuma Reserva Informada.');", True)
            Me.txtReserva.Focus()
            Return False
        End If

        If String.IsNullOrEmpty(Me.txtMotorista.Text.Trim()) Or
            String.IsNullOrWhiteSpace(Me.txtMotorista.Text.Trim()) Then            
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgAlerta", "exibeMensagem('O campo Nome do Motorista é obrigatório!');", True)
            Me.AccordionIndex.Value = 1
            Me.txtMotorista.Focus()
            Return False
        End If

        If String.IsNullOrEmpty(Me.cbCavalo.Text.Trim()) Or
            String.IsNullOrWhiteSpace(Me.cbCavalo.Text.Trim()) Then            
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgAlerta", "exibeMensagem('Selecione a Placa do Cavalo.');", True)
            Me.AccordionIndex.Value = 1
            Me.cbCavalo.Focus()
            Return False
        End If

        If String.IsNullOrEmpty(Me.cbCarreta.Text.Trim()) Or
            String.IsNullOrWhiteSpace(Me.cbCarreta.Text.Trim()) Then        
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgAlerta", "exibeMensagem('Selecione a Placa da Carreta.');", True)
            Me.AccordionIndex.Value = 1
            Me.cbCavalo.Focus()
            Return False
        End If

        Return True

    End Function

    Protected Sub btnCancelar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnCancelar.Click

        If Me.lblCodigoAgendamento.Text <> String.Empty Then
            If Banco.BeginTransaction("DELETE FROM REDEX.TB_AGENDAMENTO_WEB_CS_CA WHERE AUTONUM = " & Me.lblCodigoAgendamento.Text) Then
                Banco.BeginTransaction("DELETE FROM REDEX.TB_AGENDAMENTO_WEB_CS_ITENS WHERE AUTONUM_AGENDAMENTO = " & Me.lblCodigoAgendamento.Text)                
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgAlerta", "exibeMensagem('Agendamento Excluído com Sucesso!','AgendarCSCarregamento.aspx');", True)
            End If
        Else
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgAlerta", "document.location.href='AgendarCSCarregamento.aspx';", True)
        End If

    End Sub

    Protected Sub cbCavalo_OnSelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cbCavalo.SelectedIndexChanged

        If Me.cbCavalo.Text <> String.Empty Then
            Me.ConsultarCarretas(Me.cbCavalo.Text)
            Me.AccordionIndex.Value = 1
        End If

    End Sub

    Public Sub ConsultarPeriodos()

        Dim SQL As New StringBuilder


        SQL.Append("SELECT AUTONUM_GD_RESERVA, ")
        SQL.Append("       PERIODO_INICIAL, ")
        SQL.Append("       PERIODO_FINAL, ")
        SQL.Append("       NVL(LIMITE_PESO,0) LIMITE_PESO, ")
        SQL.Append("       NVL(LIMITE_VOLUMES,0) LIMITE_VOLUMES, ")
        SQL.Append("       NVL(LIMITE_M3,0) LIMITE_M3, ")
        SQL.Append("       NVL(LIMITE_CAMINHOES,0) LIMITE_CAMINHOES ")
        SQL.Append("FROM ")
        SQL.Append("  (SELECT A.AUTONUM_GD_RESERVA, ")
        SQL.Append("          TO_CHAR(A.PERIODO_INICIAL, 'DD/MM/YYYY HH24:MI') PERIODO_INICIAL, ")
        SQL.Append("          TO_CHAR(A.PERIODO_FINAL, 'DD/MM/YYYY HH24:MI') PERIODO_FINAL, ")
        SQL.Append("          A.LIMITE_PESO - ")
        SQL.Append("     (SELECT NVL(SUM(B.PESO_BRUTO),0) AS VOLUME ")
        SQL.Append("      FROM REDEX.VW_BOOKING_CS B ")
        SQL.Append("      INNER JOIN REDEX.TB_AGENDAMENTO_WEB_CS_ITENS A ON A.AUTONUM_MERCADORIA = B.AUTONUM_BCG ")
        SQL.Append("      INNER JOIN REDEX.TB_AGENDAMENTO_WEB_CS_CA C ON A.AUTONUM_AGENDAMENTO = C.AUTONUM ")
        SQL.Append("      WHERE C.AUTONUM_GD_RESERVA = A.AUTONUM_GD_RESERVA ) LIMITE_PESO, ")
        SQL.Append("          A.LIMITE_VOLUMES - ")
        SQL.Append("     (SELECT NVL(SUM(A.QTDE),0) AS VOLUME ")
        SQL.Append("      FROM REDEX.VW_BOOKING_CS B ")
        SQL.Append("      INNER JOIN REDEX.TB_AGENDAMENTO_WEB_CS_ITENS A ON A.AUTONUM_MERCADORIA = B.AUTONUM_BCG ")
        SQL.Append("      INNER JOIN REDEX.TB_AGENDAMENTO_WEB_CS_CA C ON A.AUTONUM_AGENDAMENTO = C.AUTONUM ")
        SQL.Append("      WHERE C.AUTONUM_GD_RESERVA = A.AUTONUM_GD_RESERVA ) LIMITE_VOLUMES, ")
        SQL.Append("                             A.LIMITE_M3 - ")
        SQL.Append("     (SELECT NVL(SUM(B.VOLUME),0) AS VOLUME ")
        SQL.Append("      FROM REDEX.VW_BOOKING_CS B ")
        SQL.Append("      INNER JOIN REDEX.TB_AGENDAMENTO_WEB_CS_ITENS A ON A.AUTONUM_MERCADORIA = B.AUTONUM_BCG ")
        SQL.Append("      INNER JOIN REDEX.TB_AGENDAMENTO_WEB_CS_CA C ON A.AUTONUM_AGENDAMENTO = C.AUTONUM ")
        SQL.Append("      WHERE C.AUTONUM_GD_RESERVA = A.AUTONUM_GD_RESERVA ) LIMITE_M3, ")
        SQL.Append("                             A.LIMITE_CAMINHOES - ")
        SQL.Append("     (SELECT NVL(COUNT(1),0) ")
        SQL.Append("      FROM TB_AGENDAMENTO_WEB_CS_CA ")
        SQL.Append("      WHERE TB_AGENDAMENTO_WEB_CS_CA.AUTONUM_GD_RESERVA = A.AUTONUM_GD_RESERVA ) LIMITE_CAMINHOES ")
        SQL.Append("   FROM REDEX.TB_GD_RESERVA A ")
        SQL.Append("   WHERE A.SERVICO_GATE = 'B' ")
        SQL.Append("     AND A.TIPO = 'CS' ")

        If Val(Me.lblCodigoPatio.Text) <> 0 Then
            SQL.Append("  AND A.PATIO = " & Val(Me.lblCodigoPatio.Text))
        Else
            SQL.Append(" AND A.PATIO = 9999 ")
        End If

        SQL.Append("     AND TO_CHAR(A.PERIODO_INICIAL,'YYYYMMDDHH24MI') >= TO_CHAR(SYSDATE,'YYYYMMDDHH24MI') ) Q ")
        SQL.Append("WHERE AUTONUM_GD_RESERVA > 0 ")

        Dim DsCa As New DataTable
        DsCa = Banco.List("SELECT FLAG_CS_M3, FLAG_CS_PESO, FLAG_CS_VOLUME, FLAG_CS_CAMINHAO, FLAG_CN_CAMINHAO FROM REDEX.TB_AGENDAMENTO_WEB_PERIODO_CA ")

        If DsCa IsNot Nothing Then
            If Convert.ToInt32(DsCa.Rows(0)("FLAG_CS_M3").ToString()) > 0 Then
                SQL.Append(" AND LIMITE_M3 > 0 ")
            End If
            If Convert.ToInt32(DsCa.Rows(0)("FLAG_CS_PESO").ToString()) > 0 Then
                SQL.Append(" AND LIMITE_PESO > 0 ")
            End If
            If Convert.ToInt32(DsCa.Rows(0)("FLAG_CS_VOLUME").ToString()) > 0 Then
                SQL.Append(" AND LIMITE_VOLUMES > 0 ")
            End If
            If Convert.ToInt32(DsCa.Rows(0)("FLAG_CS_CAMINHAO").ToString()) > 0 Then
                SQL.Append(" AND LIMITE_CAMINHOES > 0 ")
            End If
        End If

        SQL.Append("ORDER BY TO_DATE(PERIODO_INICIAL,'DD/MM/YYYY HH24:MI') ")
        If Val(Me.lblCodigoPatio.Text) <> 0 Then
            Me.dgConsultaPeriodos.DataSource = Banco.List(SQL.ToString())
            Me.dgConsultaPeriodos.DataBind()
        End If


    End Sub

    Protected Sub dgConsultaPeriodos_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles dgConsultaPeriodos.RowCommand

        Me.AccordionIndex.Value = 3

        If Nnull(Me.lblCodigoAgendamento.Text, 0) = 0 Then
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgAlerta", "exibeMensagem('O agendamento ainda não foi salvo. Clique no Botão Salvar.','');", True)
            Exit Sub
        End If

        If Me.dgConsultaPeriodos.Rows.Count > 0 Then

            Dim ID As String = Me.dgConsultaPeriodos.DataKeys(Convert.ToInt32(e.CommandArgument)).Values("AUTONUM_GD_RESERVA").ToString()

            Dim LimitePesoPeriodo As Decimal = 0.0
            Dim LimiteM3Periodo As Decimal = 0.0
            Dim LimiteVolumesPeriodo As Decimal = 0.0

            Dim QuantidadeVolumeCadastrada As Decimal = 0.0
            Dim QuantidadePesoCadastrada As Decimal = 0.0
            Dim QuantidadeM3Cadastrada As Decimal = 0.0

            Dim ds As New DataTable
            ds = Banco.List("SELECT NVL(LIMITE_PESO,0) LIMITE_PESO,NVL(LIMITE_VOLUMES,0) LIMITE_VOLUMES,NVL(LIMITE_M3,0) LIMITE_M3 FROM REDEX.TB_GD_RESERVA WHERE AUTONUM_GD_RESERVA = " & ID)

            If ds IsNot Nothing Then
                If ds.Rows.Count > 0 Then
                    LimitePesoPeriodo = Math.Round(Convert.ToDecimal(ds.Rows(0)("LIMITE_PESO").ToString()), 2)
                    LimiteM3Periodo = Math.Round(Convert.ToDecimal(ds.Rows(0)("LIMITE_M3").ToString()), 2)
                    LimiteVolumesPeriodo = Math.Round(Convert.ToDecimal(ds.Rows(0)("LIMITE_VOLUMES").ToString()), 2)
                End If
            End If

            Dim Periodo As String = String.Empty

            If e.CommandName = "SEL" Then
                If Not String.IsNullOrEmpty(ID) And Not String.IsNullOrWhiteSpace(ID) Then

                    ds = Banco.List("SELECT NVL(SUM(A.QTDE),0) AS QTDE,NVL(SUM(B.VOLUME),0) AS VOLUME,NVL(SUM(B.PESO_BRUTO),0) AS PESO_BRUTO FROM REDEX.VW_BOOKING_CS B INNER JOIN REDEX.TB_AGENDAMENTO_WEB_CS_ITENS A ON A.AUTONUM_MERCADORIA = B.AUTONUM_BCG AND A.AUTONUM_AGENDAMENTO = " & Me.lblCodigoAgendamento.Text)

                    If ds IsNot Nothing Then
                        If ds.Rows.Count > 0 Then

                            QuantidadeVolumeCadastrada = Math.Round(Convert.ToDouble(ds.Rows(0)("QTDE").ToString()), 2)
                            QuantidadeM3Cadastrada = Math.Round(Convert.ToDouble(ds.Rows(0)("VOLUME").ToString()), 2)
                            QuantidadePesoCadastrada = Math.Round(Convert.ToDouble(ds.Rows(0)("PESO_BRUTO").ToString()), 2)

                            Dim DsCA As New DataTable
                            DsCA = Banco.List("SELECT FLAG_CS_M3, FLAG_CS_PESO, FLAG_CS_VOLUME, FLAG_CS_CAMINHAO, FLAG_CN_CAMINHAO FROM REDEX.TB_AGENDAMENTO_WEB_PERIODO_CA")

                            If DsCA IsNot Nothing Then
                                If Convert.ToInt32(DsCA.Rows(0)("FLAG_CS_M3").ToString()) > 0 Then
                                    If Math.Round(Nnull(QuantidadeM3Cadastrada, 0), 2) > Math.Round(Convert.ToDecimal(Nnull(LimiteM3Periodo, 0)), 2) Then
                                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgAlerta", "exibeMensagem('O Volume informado nas Notas Fiscais (" & QuantidadeM3Cadastrada & "m³) ultrapassa o limite de Peso do Período (" & LimiteM3Periodo & "m³).');", True)
                                        Exit Sub
                                    End If
                                End If
                                If Convert.ToInt32(DsCA.Rows(0)("FLAG_CS_PESO").ToString()) > 0 Then
                                    If Math.Round(Nnull(QuantidadePesoCadastrada, 0), 2) > Math.Round(Convert.ToDecimal(Nnull(LimitePesoPeriodo, 0)), 2) Then
                                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgAlerta", "exibeMensagem('O Peso Bruto informado (" & QuantidadePesoCadastrada & "Kg) ultrapassa o limite de Peso do Período (" & LimitePesoPeriodo & "Kg).');", True)
                                        Exit Sub
                                    End If
                                End If
                                If Convert.ToInt32(DsCA.Rows(0)("FLAG_CS_VOLUME").ToString()) > 0 Then
                                    If Math.Round(Nnull(QuantidadeVolumeCadastrada, 0), 2) > Math.Round(Convert.ToDecimal(Nnull(LimiteVolumesPeriodo, 0)), 2) Then
                                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgAlerta", "exibeMensagem('O Volume informado (" & QuantidadeVolumeCadastrada & ") ultrapassa o limite de Volumes do Período (" & LimiteVolumesPeriodo & ").');", True)
                                        Exit Sub
                                    End If
                                End If

                            End If                           
                        End If
                    End If

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

                    If Request.QueryString("id") IsNot Nothing Then
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgAlerta", "exibeMensagem('Você escolheu o Período: " & Periodo & ". Clique no Botão Salvar para concluir o agendamento.');", True)
                    Else
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgAlerta", "exibeMensagem('Você escolheu o Período: " & Periodo & ". Clique no Botão Concluir para terminar o agendamento.');", True)
                    End If

                End If
            End If

        End If

    End Sub

    Private Sub ConsultarItensReserva()

        Me.dgConsultaMercadoria.DataSource = Banco.List("SELECT A.AUTONUM,B.DESC_PRODUTO || ' - ' || B.DESCRICAO_EMB AS PRODUTO,A.QTDE,B.VOLUME,B.PESO_BRUTO FROM REDEX.VW_BOOKING_CS B INNER JOIN REDEX.TB_AGENDAMENTO_WEB_CS_ITENS A ON A.AUTONUM_MERCADORIA = B.AUTONUM_BCG AND A.AUTONUM_AGENDAMENTO = " & Me.lblCodigoAgendamento.Text)
        Me.dgConsultaMercadoria.DataBind()

    End Sub

    Protected Sub cbMercadoria_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cbMercadoria.SelectedIndexChanged

        Me.AccordionIndex.Value = 2

        If Me.cbMercadoria.Items.Count > 0 Then
            If Me.cbMercadoria.SelectedValue IsNot Nothing Then
                Me.lblSaldoCarga.Text = Banco.ExecuteScalar("SELECT NVL(QTDE,0) QTDE FROM REDEX.VW_BOOKING_CS WHERE AUTONUM_BCG = " & Me.cbMercadoria.SelectedValue)
            End If
        End If

    End Sub

    Protected Sub btnSalvarMercadoria_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSalvarMercadoria.Click

        Me.AccordionIndex.Value = 2

        Dim QuantidadeCadastrada As Double = 0.0
        Dim QuantidadePesoCadastrada As Double = 0.0
        Dim QuantidadeM3Cadastrada As Double = 0.0

        If Me.cbMercadoria.SelectedValue Is Nothing Then
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgAlerta", "exibeMensagem('Selecione a Carga.');", True)
            Exit Sub
        End If

        If Me.txtQuantidade.Text = String.Empty Then            
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgAlerta", "exibeMensagem('Informe a Quantidade da Carga.');", True)
            Exit Sub
        End If

        
        Dim SQL As New StringBuilder

        If Request.QueryString("id") Is Nothing And (Math.Round(Convert.ToDecimal(Nnull(Me.lblQuantidadeSelecionadaCarga.Text, 0))) + Math.Round(Convert.ToDecimal(Nnull(Me.lblVolumesSelecionadaCarga.Text, 0))) + Math.Round(Convert.ToDecimal(Nnull(Me.lblPesoSelecionadaCarga.Text, 0)))) = 0 Then
            SQL.Append("SELECT NVL(SUM(A.QTDE),0) AS QTDE,NVL(SUM(B.VOLUME),0) AS VOLUME,NVL(SUM(B.PESO_BRUTO),0) AS PESO_BRUTO FROM REDEX.VW_BOOKING_CS B INNER JOIN REDEX.TB_AGENDAMENTO_WEB_CS_ITENS A ON A.AUTONUM_MERCADORIA = B.AUTONUM_BCG AND A.AUTONUM_AGENDAMENTO = " & Me.lblCodigoAgendamento.Text)
        Else
            SQL.Append("SELECT NVL(SUM(A.QTDE),0) AS QTDE,NVL(SUM(B.VOLUME),0) AS VOLUME,NVL(SUM(B.PESO_BRUTO),0) AS PESO_BRUTO FROM REDEX.VW_BOOKING_CS B INNER JOIN REDEX.TB_AGENDAMENTO_WEB_CS_ITENS A ON A.AUTONUM_MERCADORIA = B.AUTONUM_BCG AND A.AUTONUM_AGENDAMENTO = " & Me.lblCodigoAgendamento.Text & " AND A.AUTONUM <> " & Me.cbMercadoria.SelectedValue)
        End If


        Dim ds As New DataTable
        ds = Banco.List(SQL.ToString())

        If ds IsNot Nothing Then
            If ds.Rows.Count > 0 Then
                QuantidadeCadastrada = Math.Round(Convert.ToDouble(ds.Rows(0)("QTDE").ToString()), 2)
                QuantidadeM3Cadastrada = Math.Round(Convert.ToDouble(ds.Rows(0)("VOLUME").ToString()), 2)
                QuantidadePesoCadastrada = Math.Round(Convert.ToDouble(ds.Rows(0)("PESO_BRUTO").ToString()), 2)
            End If
        End If

        If String.IsNullOrEmpty(Me.txtQuantidade.Text.Trim()) Or String.IsNullOrWhiteSpace(Me.txtQuantidade.Text.Trim()) Or Nnull(Me.txtQuantidade.Text.Trim(), 0) = 0 Then
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgAlerta", "exibeMensagem('O campo Quantidade (Qtde) da mercadoria é obrigatório.');", True)
            Me.AccordionIndex.Value = 2
            Me.txtQuantidade.Focus()
            Exit Sub
        End If

        If Not IsNumeric(Me.txtQuantidade.Text) Then            
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgAlerta", "exibeMensagem('O campo Quantidade (Qtde) da Mercadoria deve ser um valor Numérico.');", True)
            Me.AccordionIndex.Value = 2            
            Me.txtQuantidade.Focus()
            Exit Sub
        End If

        If Math.Round(Convert.ToDecimal(Me.txtQuantidade.Text) + Convert.ToDecimal(QuantidadeCadastrada), 2) > Math.Round(Convert.ToDecimal(Me.lblDisponiveis.Text), 2) Then            
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgAlerta", "exibeMensagem('Você informou uma Quantidade de Volumes superior ao limite disponível da Reserva (" & Me.lblDisponiveis.Text & ").');", True)
            Me.AccordionIndex.Value = 2
            Me.txtQuantidade.Focus()
            Exit Sub
        End If

        If Me.lblCodigoCarga.Text = String.Empty Then
            SQL.Clear()
            SQL.Append("SELECT   ")
            SQL.Append("   NVL((A.QTDE - ")
            SQL.Append("  TO_NUMBER((SELECT NVL(SUM(QTDE),0) QTDE ")
            SQL.Append("    FROM REDEX.TB_AGENDAMENTO_WEB_CS_ITENS WHERE AUTONUM_MERCADORIA = A.AUTONUM_BCG),'999')),0) ")
            SQL.Append("   AS SALDO ")
            SQL.Append("FROM REDEX.VW_BOOKING_CS A ")
            SQL.Append("WHERE A.AUTONUM_BOO = " & Me.lblCodigoBooking.Text)
        Else
            SQL.Clear()
            SQL.Append("SELECT   ")
            SQL.Append("   NVL((A.QTDE - ")
            SQL.Append("  TO_NUMBER((SELECT NVL(SUM(QTDE),0) QTDE ")
            SQL.Append("    FROM REDEX.TB_AGENDAMENTO_WEB_CS_ITENS WHERE AUTONUM_MERCADORIA = A.AUTONUM_BCG),'999')),0) ")
            SQL.Append("   AS SALDO ")
            SQL.Append("FROM REDEX.VW_BOOKING_CS A ")
            SQL.Append("WHERE A.AUTONUM_BOO = " & Me.lblCodigoBooking.Text & " AND A.AUTONUM_BCG <> " & Me.lblCodigoCarga.Text)
        End If

        If Me.lblCodigoCarga.Text = String.Empty Then

            If Convert.ToInt32(Banco.ExecuteScalar(SQL.ToString())) = 0 Then                
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgAlerta", "exibeMensagem('A Mercadoria não possui saldo suficiente para Carregamento.');", True)
                Exit Sub
            End If

            If Banco.BeginTransaction("INSERT INTO REDEX.TB_AGENDAMENTO_WEB_CS_ITENS (AUTONUM,AUTONUM_AGENDAMENTO,AUTONUM_MERCADORIA,QTDE) VALUES (SEQ_AGENDAMENTO_WEB_CS_ITENS.NEXTVAL," & Me.lblCodigoAgendamento.Text & "," & Me.cbMercadoria.SelectedValue & "," & Me.txtQuantidade.Text & ")") Then
                ConsultarItensReserva()
                ConsultarCarga()
            End If

        Else
            If Banco.BeginTransaction("UPDATE REDEX.TB_AGENDAMENTO_WEB_CS_ITENS SET AUTONUM_MERCADORIA = " & Me.cbMercadoria.SelectedValue & ",QTDE=" & Me.txtQuantidade.Text & " WHERE AUTONUM = " & Me.lblCodigoCarga.Text) Then
                ConsultarItensReserva()
                ConsultarCarga()
            End If
        End If

        Me.cbMercadoria.SelectedIndex = -1
        Me.txtQuantidade.Text = String.Empty
        Me.lblCodigoCarga.Text = String.Empty

        Me.lblQuantidadeSelecionadaCarga.Text = String.Empty
        Me.lblVolumesSelecionadaCarga.Text = String.Empty
        Me.lblPesoSelecionadaCarga.Text = String.Empty

        ConsultarPeriodos()

    End Sub

    Protected Sub dgConsultaMercadoria_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles dgConsultaMercadoria.RowCommand

        Me.AccordionIndex.Value = 2

        If Me.dgConsultaMercadoria.Rows.Count > 0 Then

            Dim Index As Integer = e.CommandArgument
            Dim ID As String = Me.dgConsultaMercadoria.DataKeys(Index)("AUTONUM").ToString()

            If e.CommandName = "DEL" Then
                If Not String.IsNullOrEmpty(ID) And
                    Not String.IsNullOrWhiteSpace(ID) Then
                    If Banco.BeginTransaction("DELETE FROM REDEX.TB_AGENDAMENTO_WEB_CS_ITENS WHERE AUTONUM = " & ID) Then
                        ConsultarItensReserva()
                        ConsultarCarga()
                        lblQuantidadeSelecionadaCarga.Text = String.Empty
                        lblVolumesSelecionadaCarga.Text = String.Empty
                        lblPesoSelecionadaCarga.Text = String.Empty
                    Else                        
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgAlerta", "exibeMensagem('Erro ao Excluir a Carga. Tente Novamente.');", True)
                    End If
                End If
            End If

            If e.CommandName = "EDITAR" Then

                If Not String.IsNullOrEmpty(Me.lblCodigoAgendamento.Text.Trim()) And
                    Not String.IsNullOrWhiteSpace(Me.lblCodigoAgendamento.Text.Trim()) Then

                    Dim Ds As New DataTable
                    Ds = Banco.List("SELECT B.AUTONUM_BCG,A.QTDE FROM REDEX.VW_BOOKING_CS B INNER JOIN REDEX.TB_AGENDAMENTO_WEB_CS_ITENS A ON A.AUTONUM_MERCADORIA = B.AUTONUM_BCG AND A.AUTONUM = " & ID)

                    If Ds IsNot Nothing Then
                        If Ds.Rows.Count > 0 Then

                            Me.lblCodigoCarga.Text = ID
                            Me.cbMercadoria.SelectedValue = Ds.Rows(0)("AUTONUM_BCG").ToString()
                            Me.txtQuantidade.Text = Ds.Rows(0)("QTDE").ToString()

                            Ds = Banco.List("SELECT NVL(SUM(A.QTDE),0) AS QTDE,NVL(SUM(B.VOLUME),0) AS VOLUME,NVL(SUM(B.PESO_BRUTO),0) AS PESO_BRUTO FROM REDEX.VW_BOOKING_CS B INNER JOIN REDEX.TB_AGENDAMENTO_WEB_CS_ITENS A ON A.AUTONUM_MERCADORIA = B.AUTONUM_BCG AND A.AUTONUM_AGENDAMENTO = " & Me.lblCodigoAgendamento.Text)

                            If Ds IsNot Nothing Then
                                If Ds.Rows.Count > 0 Then
                                    lblQuantidadeSelecionadaCarga.Text = Ds.Rows(0)("QTDE").ToString()
                                    lblVolumesSelecionadaCarga.Text = Ds.Rows(0)("VOLUME").ToString()
                                    lblPesoSelecionadaCarga.Text = Ds.Rows(0)("PESO_BRUTO").ToString()
                                End If
                            End If

                        End If

                    End If
                End If

            End If
        End If

    End Sub

    Protected Sub btnSair_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSair.Click

        If Request.QueryString("id") IsNot Nothing Then
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgAlerta", "document.location.href='Default.aspx';", True)
        Else
            If Me.lblCodigoAgendamento.Text <> String.Empty Then
                If Banco.BeginTransaction("DELETE FROM REDEX.TB_AGENDAMENTO_WEB_CS_CA WHERE AUTONUM = " & Me.lblCodigoAgendamento.Text) Then
                    Banco.BeginTransaction("DELETE FROM REDEX.TB_AGENDAMENTO_WEB_CS_ITENS WHERE AUTONUM_AGENDAMENTO = " & Me.lblCodigoAgendamento.Text)
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgAlerta", "document.location.href='Default.aspx';", True)
                End If
            Else
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgAlerta", "document.location.href='Default.aspx';", True)
            End If
        End If

    End Sub

End Class