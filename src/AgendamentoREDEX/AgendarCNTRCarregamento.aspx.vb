Imports System.Data.OleDb

Public Class AgendarCNTRCarregamento
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not Page.IsPostBack Then

            ConsultarCavalos()
            ConsultarTransportadora()
            'ConsultarPeriodos()

            If Request.QueryString("id") IsNot Nothing Then

                Dim SQL As New StringBuilder

                SQL.Append("SELECT ")
                SQL.Append("    A.AUTONUM, ")
                SQL.Append("    A.RESERVA, ")
                SQL.Append("    A.AUTONUM_MOTORISTA, ")
                SQL.Append("    A.AUTONUM_VEICULO, ")
                SQL.Append("    A.AUTONUM_GD_RESERVA, ")
                SQL.Append("    A.ID_CONTEINER, ")
                SQL.Append("    B.NOME, ")
                SQL.Append("    C.PLACA_CAVALO, ")
                SQL.Append("    C.PLACA_CARRETA ")
                SQL.Append("FROM ")
                SQL.Append("    REDEX.TB_AGENDAMENTO_WEB_CNTR_CA A ")
                SQL.Append("INNER JOIN ")
                SQL.Append("    OPERADOR.TB_AG_MOTORISTAS B ON A.AUTONUM_MOTORISTA = B.AUTONUM ")
                SQL.Append("INNER JOIN ")
                SQL.Append("    OPERADOR.TB_AG_VEICULOS C ON A.AUTONUM_VEICULO = C.AUTONUM ")
                SQL.Append("INNER JOIN ")
                SQL.Append("    OPERADOR.TB_GD_RESERVA E ON A.AUTONUM_GD_RESERVA = E.AUTONUM_GD_RESERVA ")
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

                        Me.txtMotorista.Text = Ds.Rows(0)("NOME").ToString()
                        Me.cbCavalo.Text = Ds.Rows(0)("PLACA_CAVALO").ToString()

                        ConsultarCarretas(Me.cbCavalo.Text)
                        Me.cbCarreta.Text = Ds.Rows(0)("PLACA_CARRETA").ToString()

                        'ConsultarPeriodos()
                        ConsultarQuantidades()
                        ConsultarConteineres()

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

                        Me.cbConteiner.Text = Ds.Rows(0)("ID_CONTEINER").ToString()

                    Else
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgAlerta", "exibeMensagem('Registro inexistente!');", True)
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgAlerta", "document.location.href='ConsultarAgendamentosCNTRCarregamento.aspx';", True)
                    End If
                End If

            End If

        End If

        If Not String.IsNullOrEmpty(Me.lblCodigoBooking.Text) Then
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "exibeAccordion", "document.getElementById('accordion').style.display = 'block';", True)
        End If

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

            Me.AccordionIndex.Value = 0

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
            SQL.Append("    AUTONUM_VIAGEM, ")
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
                        Me.lblViagem.Text = Linha("NUM_VIAGEM").ToString()
                        Me.lblTotal.Text = Linha("QUANTIDADE").ToString()
                        Me.lblCodigoBooking.Text = Linha("AUTONUM").ToString()
                        Me.lblCodigoPatio.Text = Linha("PATIO").ToString()

                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "exibeAccordion", "document.getElementById('accordion').style.display = 'block';", True)
                        Me.txtMotorista.Focus()

                        Me.btnSalvar.Visible = True
                        Me.btnCancelar.Visible = True

                        Me.txtMotorista.Focus()

                        ConsultarConteineres()
                        ConsultarQuantidades()

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

        SQL.Append("SELECT COUNT(1) UTILIZADOS FROM REDEX.TB_AGENDAMENTO_WEB_CNTR_CA WHERE UPPER(RESERVA) = '" & Me.txtReserva.Text.ToUpper() & "' ")

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
        End If

    End Sub

    Protected Sub btnSalvar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSalvar.Click

        Dim SQL As New StringBuilder

        If Me.btnSalvar.Text = "Concluir" Then
            Response.Redirect("ConsultarAgendamentosCNTRCarregamento.aspx")
        End If

        If ValidarCampos() Then

            If Request.QueryString("id") Is Nothing Then

                Me.lblCodigoAgendamento.Text = Banco.ExecuteScalar("SELECT REDEX.SEQ_GD_CONTEINER.NEXTVAL FROM DUAL")
                Me.lblCodigoBooking.Text = Banco.ExecuteScalar("SELECT AUTONUM_BOO FROM REDEX.TB_BOOKING WHERE UPPER(REFERENCE) = '" & Me.txtReserva.Text.ToUpper() & "'")
                Me.lblCodigoMotorista.Text = Banco.ExecuteScalar("SELECT AUTONUM FROM OPERADOR.TB_AG_MOTORISTAS WHERE UPPER(TRIM(NOME)) = '" & ObterNomeMotorista(Me.txtMotorista.Text.ToUpper()) & "' AND ID_TRANSPORTADORA = " & Session("SIS_AUTONUM_TRANSPORTADORA").ToString())
                Me.lblCodigoVeiculo.Text = Banco.ExecuteScalar("SELECT AUTONUM FROM OPERADOR.TB_AG_VEICULOS WHERE UPPER(PLACA_CAVALO) = '" & Me.cbCavalo.Text & "' AND UPPER(PLACA_CARRETA) = '" & Me.cbCarreta.Text & "' AND ID_TRANSPORTADORA = " & Session("SIS_AUTONUM_TRANSPORTADORA").ToString())
                Me.lblCodigoViagem.Text = Banco.ExecuteScalar("SELECT AUTONUM_VIA FROM REDEX.TB_VIAGENS WHERE NUM_VIAGEM = '" & Me.lblViagem.Text & "'")

                SQL.Append("INSERT INTO ")
                SQL.Append("  REDEX.TB_AGENDAMENTO_WEB_CNTR_CA ")
                SQL.Append("( ")
                SQL.Append("    AUTONUM, ")
                SQL.Append("    RESERVA, ")
                SQL.Append("    AUTONUM_MOTORISTA, ")
                SQL.Append("    AUTONUM_VEICULO, ")
                SQL.Append("    AUTONUM_TRANSPORTADORA, ")
                SQL.Append("    AUTONUM_GD_RESERVA, ")
                SQL.Append("    AUTONUM_BOOKING, ")
                SQL.Append("    DATA_AGENDAMENTO, ")
                SQL.Append("    STATUS, ")
                SQL.Append("    NUM_PROTOCOLO, ")
                SQL.Append("    ANO_PROTOCOLO, ")
                SQL.Append("    AUTONUM_CONTEINER ")
                SQL.Append(")VALUES( ")
                SQL.Append("    SEQ_AGENDAMENTO_WEB_CNTR_CA.NEXTVAL, ")
                SQL.Append("    '" & Me.txtReserva.Text.ToUpper() & "', ")
                SQL.Append("    " & Me.lblCodigoMotorista.Text & ", ")
                SQL.Append("    " & Me.lblCodigoVeiculo.Text & ", ")
                SQL.Append("    " & Session("SIS_AUTONUM_TRANSPORTADORA").ToString() & ", ")
                SQL.Append("    " & Me.lblCodigoPeriodo.Text & ", ")
                SQL.Append("    " & Me.lblCodigoBooking.Text & ", ")
                SQL.Append("    SYSDATE, ")
                SQL.Append("    'GE', ")
                SQL.Append("    SEQ_AGENDAMENTO_WEB_PROT_" & Now.Year & ".NEXTVAL, ")
                SQL.Append("    " & Now.Year & ", ")
                SQL.Append("    " & Me.cbConteiner.SelectedValue & " ")
                SQL.Append(") ")

                If Banco.BeginTransaction(SQL.ToString()) Then
                    'If Agendamento.InsereAgendamentoNaFila(Val(Me.cbConteiner.SelectedValue), Val(Me.lblCodigoPeriodo.Text), Val(Me.lblCodigoBooking.Text), Val(Me.lblCodigoAgendamento.Text), TipoAgendamento.CONTEINER_CARREGAMENTO) Then
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgAlerta", "exibeMensagem('Agendamento criado com sucesso!','ConsultarAgendamentosCNTRCarregamento.aspx');", True)
                        Me.btnSalvar.Text = "Concluir"
                        Me.AccordionIndex.Value = 0
                    'End If
                Else
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgAlerta", "exibeMensagem('Erro ao criar um novo agendamento. Tente Novamente.');", True)
                End If

            Else

                Me.lblCodigoMotorista.Text = Banco.ExecuteScalar("SELECT AUTONUM FROM OPERADOR.TB_AG_MOTORISTAS WHERE UPPER(TRIM(NOME)) = '" & ObterNomeMotorista(Me.txtMotorista.Text.ToUpper()) & "' AND ID_TRANSPORTADORA = " & Session("SIS_AUTONUM_TRANSPORTADORA").ToString())
                Me.lblCodigoVeiculo.Text = Banco.ExecuteScalar("SELECT AUTONUM FROM OPERADOR.TB_AG_VEICULOS WHERE UPPER(PLACA_CAVALO) = '" & Me.cbCavalo.Text & "' AND UPPER(PLACA_CARRETA) = '" & Me.cbCarreta.Text & "' AND ID_TRANSPORTADORA = " & Session("SIS_AUTONUM_TRANSPORTADORA").ToString())
                Me.lblCodigoProtocolo.Text = Banco.ExecuteScalar("SELECT REDEX.SEQ_AGENDAMENTO_WEB_PROT_" & Now.Year & ".NEXTVAL FROM DUAL")

                SQL.Append("UPDATE ")
                SQL.Append("    REDEX.TB_AGENDAMENTO_WEB_CNTR_CA ")
                SQL.Append("SET ")
                SQL.Append("    AUTONUM_MOTORISTA = " & Me.lblCodigoMotorista.Text & ", ")
                SQL.Append("    AUTONUM_VEICULO = " & Me.lblCodigoVeiculo.Text & ", ")
                SQL.Append("    AUTONUM_TRANSPORTADORA = " & Session("SIS_AUTONUM_TRANSPORTADORA").ToString() & ", ")
                SQL.Append("    AUTONUM_GD_RESERVA = " & Me.lblCodigoPeriodo.Text & ", ")
                SQL.Append("    STATUS = 'GE', ")
                SQL.Append("    NUM_PROTOCOLO = SEQ_AGENDAMENTO_WEB_PROT_" & Now.Year & ".NEXTVAL, ")
                SQL.Append("    ANO_PROTOCOLO = " & Now.Year & ", ")
                SQL.Append("    AUTONUM_CONTEINER = " & Me.cbConteiner.SelectedValue & " ")
                SQL.Append("WHERE ")
                SQL.Append("    AUTONUM = " & Me.lblCodigoAgendamento.Text)

                If Convert.ToInt32(Banco.BeginTransaction(SQL.ToString())) > 0 Then
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgAlerta", "exibeMensagem('Agendamento alterado com sucesso!','ConsultarAgendamentosCNTRCarregamento.aspx');", True)
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
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgAlerta", "document.location.href='AgendarCNTRCarregamento.aspx';", True)
                Return False
            End If
        End If        

        If String.IsNullOrEmpty(Me.txtReserva.Text.Trim()) Or
            String.IsNullOrWhiteSpace(Me.txtReserva.Text.Trim()) Then
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgAlerta", "exibeMensagem('Nenhuma Reserva Informada.');", True)
            Me.AccordionIndex.Value = 0
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
            Me.cbCarreta.Focus()
            Return False
        End If

        If Me.cbConteiner.SelectedIndex = 0 Then
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgAlerta", "exibeMensagem('Nenhum Contêiner foi selecionado.');", True)
            Me.cbConteiner.Focus()
            Me.AccordionIndex.Value = 2
            Return False
        End If

        If Me.lblCodigoPeriodo.Text = String.Empty Then
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgAlerta", "exibeMensagem('Nenhum período foi selecionado.');", True)
            Me.AccordionIndex.Value = 2
            Return False
        End If

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

    Protected Sub btnCancelar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnCancelar.Click

        If Me.lblCodigoAgendamento.Text <> String.Empty Then
            If Banco.BeginTransaction("DELETE FROM REDEX.TB_AGENDAMENTO_WEB_CNTR_CA WHERE AUTONUM = " & Me.lblCodigoAgendamento.Text) Then
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgAlerta", "exibeMensagem('Agendamento Excluído com Sucesso!');", True)
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
                If Banco.BeginTransaction("DELETE FROM REDEX.TB_AGENDAMENTO_WEB_CNTR_CA WHERE AUTONUM = " & Me.lblCodigoAgendamento.Text) Then
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

    Private Sub ConsultarConteineres()

        Me.cbConteiner.DataSource = Banco.List("SELECT AUTONUM_PATIO, ID_CONTEINER CONTEINER FROM REDEX.VW_CONSULTA_CNTR WHERE UPPER(REFERENCE) = '" & Me.txtReserva.Text.ToUpper() & "'")
        Me.cbConteiner.DataBind()

        Me.cbConteiner.Items.Insert(0, New ListItem("-- SELECIONE O CONTÊINER --", ""))

    End Sub

    Public Sub ConsultarPeriodos()

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
        SQL.Append("            SELECT NVL(COUNT(1),0) FROM TB_AGENDAMENTO_WEB_CNTR_CA WHERE TB_AGENDAMENTO_WEB_CNTR_CA.AUTONUM_GD_RESERVA = A.AUTONUM_GD_RESERVA ")
        SQL.Append("        ) LIMITE_CAMINHOES ")
        SQL.Append("  FROM ")
        SQL.Append("      REDEX.TB_GD_RESERVA A ")
        SQL.Append("  WHERE ")
        SQL.Append("      A.SERVICO_GATE = 'D' AND A.TIPO = 'CN' AND TO_CHAR(A.PERIODO_INICIAL,'YYYYMMDDHH24MI') >= TO_CHAR(SYSDATE,'YYYYMMDDHH24MI') ")

        If Val(Me.lblCodigoPatio.Text) <> 0 Then
            SQL.Append("  AND A.PATIO = " & Val(Me.lblCodigoPatio.Text))
        Else
            SQL.Append(" AND A.PATIO = 9999 ")
        End If

        SQL.Append("  ) Q ")
        SQL.Append("  WHERE AUTONUM_GD_RESERVA > 0 ")

        Dim DsDes As New DataTable
        DsDes = Banco.List("SELECT FLAG_CS_M3, FLAG_CS_PESO, FLAG_CS_VOLUME, FLAG_CS_CAMINHAO, FLAG_CN_CAMINHAO FROM REDEX.TB_AGENDAMENTO_WEB_PERIODO_CA ")

        If DsDes IsNot Nothing Then
            If Convert.ToInt32(DsDes.Rows(0)("FLAG_CS_CAMINHAO").ToString()) > 0 Then
                SQL.Append(" AND LIMITE_CAMINHOES > 0 ")
            End If
        End If

        SQL.Append("ORDER BY TO_DATE(PERIODO_INICIAL,'DD/MM/YYYY HH24:MI') ")

        Me.dgConsultaPeriodos.DataSource = Banco.List(SQL.ToString())
        Me.dgConsultaPeriodos.DataBind()

        If Me.dgConsultaPeriodos.Rows.Count > 0 Then
            'pnPeriodos.Visible = True
        End If

    End Sub

    Public Function ObterViagem(ByVal ID_Conteiner As String) As String
        Return Banco.ExecuteScalar(String.Format("SELECT NVL(AUTONUMVIAGEM,0) AUTONUMVIAGEM FROM OPERADOR.TB_GD_CONTEINER WHERE ID_CONTEINER='{0}'", ID_Conteiner))
    End Function

    Public Function ObterLine(ByVal Viagem As String, ByVal Reserva As String) As String
        Return Banco.ExecuteScalar(String.Format("select carrier from tb_booking where reference= '{0}' and autonumviagem = '{1}' and atual = 1", Reserva, Viagem))
    End Function

    Protected Sub dgConsultaPeriodos_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles dgConsultaPeriodos.RowCommand

        If Me.dgConsultaPeriodos.Rows.Count > 0 Then

            Me.AccordionIndex.Value = 3

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

    Protected Sub cbConteiner_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cbConteiner.SelectedIndexChanged

        ConsultarPeriodos()
        Me.AccordionIndex.Value = 2

    End Sub

    Protected Sub btnSelecionarConteiner_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSelecionarConteiner.Click
        'ConsultarPeriodos()
        Me.AccordionIndex.Value = 3
    End Sub

End Class