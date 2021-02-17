Public Class ConsultarAgendamentos
    Inherits System.Web.UI.Page

    Dim Agendamento As New Agendamento
    Dim Transportadora As New Transportadora

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not Page.IsPostBack Then
            Consultar()
        End If

        Dim SQL As String = String.Empty

        SQL = " DELETE FROM TB_AG_CS_NF WHERE AUTONUM_AGENDAMENTO IN ( "
        SQL = SQL & " SELECT AUTONUM FROM TB_AG_CS WHERE NVL(NUM_PROTOCOLO,0) = 0 AND NVL(ANO_PROTOCOLO,0) = 0 "
        SQL = SQL & " AND NVL(IMPRESSO,0) = 0 AND NVL(STATUS,0) = 0 AND NVL(AUTONUM_VEICULO,0) = 0 "
        SQL = SQL & " AND NVL(AUTONUM_MOTORISTA,0) = 0 AND NVL(AUTONUM_GD_RESERVA,0) =0 AND AUTONUM_USUARIO = " & Val(Session("SIS_USUARIO_LOGADO")) & ")"

        Banco.Executar(SQL)

        SQL = " DELETE FROM TB_AG_CS WHERE NVL(NUM_PROTOCOLO,0) = 0 AND NVL(ANO_PROTOCOLO,0) = 0 "
        SQL = SQL & " AND NVL(IMPRESSO,0) = 0 AND NVL(STATUS,0) = 0 AND NVL(AUTONUM_VEICULO,0) = 0 "
        SQL = SQL & " AND NVL(AUTONUM_MOTORISTA,0) = 0 AND NVL(AUTONUM_GD_RESERVA,0) =0 AND AUTONUM_USUARIO = " & Val(Session("SIS_USUARIO_LOGADO"))

        Banco.Executar(SQL)

    End Sub

    Private Sub Consultar(Optional ByVal Filtro As String = "")
        Me.DgAgendamentos.DataSource = Agendamento.Consultar(Session("SIS_ID").ToString(), Int(Session("SIS_TRANSPEMPRESA")), Filtro)
        Me.DgAgendamentos.DataBind()
    End Sub

    Protected Sub btNovo_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btNovo.Click
        LimparSessaoPeriodo()
        Session("LOTE_DOCUMENTO") = Nothing
        Response.Redirect("CadastrarAgendamentos.aspx")
    End Sub

    Protected Sub DgAgendamentos_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles DgAgendamentos.RowDataBound

        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim Excluir As ImageButton = DirectCast(e.Row.FindControl("cmdExcluir"), ImageButton)
            Excluir.Attributes.Add("onclick", "javascript:return " & "confirm('Confirma a exclusão do Agendamento: " & DataBinder.Eval(e.Row.DataItem, "PROTOCOLO").ToString() & "?');")

            Dim DataRowView As DataRowView = e.Row.DataItem

            'If DataRowView("NOME_MOTORISTA").ToString() = "" Or DataRowView("PLACA_CAVALO" & "PLACA_CARRETA").ToString() = "" Then
            'If e.Row.Cells(3).Text = "" Or e.Row.Cells(5).Text = "" Then '3:nome motorista, 4:placa cav/carr, 7:hyperlink protocolo, 14:Check Impressao
            'Dim ProtocoloHL As HyperLinkField = CType(DataRowView("PROTOCOLO"), HyperLinkField)
            'ProtocoloHL.Visible = False

            'Dim CheckImprimir As CheckBox = CType(DataRowView("CheckProtocolo"), CheckBox)
            'CheckImprimir.Enabled = False
            'End If
        End If

    End Sub

    Protected Sub DgAgendamentos_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles DgAgendamentos.RowCommand

        'Dim ID As String = Me.DgAgendamentos.DataKeys(e.CommandArgument.ToString()).Values("AUTONUM_AG_CS").ToString()
        Dim Index As Integer = e.CommandArgument
        'Método para selecionar o Index da página do GridView:
        While Index >= DgAgendamentos.PageSize
            Index -= DgAgendamentos.PageSize
        End While
        Dim ID As String = Me.DgAgendamentos.DataKeys(Index)("AUTONUM_AG_CS").ToString()
        Dim CNTR As String = Me.DgAgendamentos.DataKeys(Index)("CNTR").ToString()

        Select Case e.CommandName
            Case "DEL"

                If Agendamento.AgendamentoComRegistro(ID) > 0 Then
                    ScriptManager.RegisterClientScriptBlock(Me, [GetType](), "script", "<script>alert('O agendamento não pode ser excluído pois já consta o registro da carga!');location.href='ConsultarAgendamentos.aspx';</script>", False)
                    Exit Sub
                End If

                If Agendamento.CancelarAgendamento(ID) Then
                    ScriptManager.RegisterClientScriptBlock(Me, [GetType](), "script", "<script>alert('Agendamento excluído com sucesso!');location.href='ConsultarAgendamentos.aspx';</script>", False)
                End If

            Case "EDIT"
                Response.Redirect("CadastrarAgendamentos.aspx?id=" & ID & "&action=edit&cntr=" & CNTR)
        End Select

    End Sub

    Protected Sub btImprimir_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btImprimir.Click

        Select Case cbModoImpressao.SelectedValue
            Case 0
                ImprimirProtocolosSelecionados()
            Case 1
                ImprimirTodosProtocolos()
            Case Else
                ScriptManager.RegisterClientScriptBlock(Me, [GetType](), "script", "<script>alert('Selecione um modo de impressão.');</script>", False)
        End Select

    End Sub

    Private Sub ImprimirProtocolosSelecionados()

        If DgAgendamentos.Rows.Count > 0 Then

            Dim ListaProtocolos As String = ""
            Dim Linhas As Integer = DgAgendamentos.Rows.Count
            Dim Protocolo As String

            For I As Integer = 0 To Linhas - 1
                Protocolo = DgAgendamentos.DataKeys(I)("AUTONUM_AG_CS").ToString()
                If DirectCast(DgAgendamentos.Rows(I).FindControl("CheckProtocolo"), CheckBox).Checked = True Then
                    If I < Linhas Then
                        ListaProtocolos = ListaProtocolos & Protocolo & ","
                    Else
                        ListaProtocolos = ListaProtocolos & Protocolo
                    End If
                End If
            Next

            If ListaProtocolos = String.Empty Then
                ScriptManager.RegisterClientScriptBlock(Me, [GetType](), "script", "<script>alert('Selecione o(s) protocolo(s) que deseja imprimir.');</script>", False)
            Else
                If Mid(ListaProtocolos, ListaProtocolos.Length, ListaProtocolos.Length - 1) = "," Then
                    ListaProtocolos = ListaProtocolos.Remove(ListaProtocolos.Length - 1)
                End If

                ScriptManager.RegisterClientScriptBlock(Me, [GetType](), "script", "<script>window.open('Protocolo.aspx?id=" & ListaProtocolos.ToString() & "', '_blank');</script>", False)
            End If

        End If

    End Sub

    Private Sub ImprimirTodosProtocolos()

        If DgAgendamentos.Rows.Count > 0 Then

            Dim ListaProtocolos As String = ""
            Dim Linhas As Integer = DgAgendamentos.Rows.Count - 1
            Dim Protocolo As String

            For I As Integer = 0 To Linhas
                Protocolo = DgAgendamentos.DataKeys(I)("AUTONUM_AG_CS").ToString()
                If I < Linhas Then
                    ListaProtocolos = ListaProtocolos & Protocolo & ","
                Else
                    ListaProtocolos = ListaProtocolos & Protocolo
                End If
            Next

            If ListaProtocolos = String.Empty Then
                ScriptManager.RegisterClientScriptBlock(Me, [GetType](), "script", "<script>alert('Não existem protocolos a serem impressos.');</script>", False)
            Else
                ScriptManager.RegisterClientScriptBlock(Me, [GetType](), "script", "<script>window.open('Protocolo.aspx?id=" & ListaProtocolos.ToString() & "', '_blank');</script>", False)
            End If

        End If

    End Sub

    Protected Sub btFiltrar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btFiltrar.Click

        Dim iSQL As New StringBuilder

        If Not txtProtocolo.Text = String.Empty Then
            Select Case cbFiltro1.SelectedValue
                Case 0
                    iSQL.Append(String.Format(" AND PROTOCOLO = '{0}' ", txtProtocolo.Text.ToUpper()))
                Case 1
                    iSQL.Append(String.Format(" AND PROTOCOLO LIKE '%{0}%' ", txtProtocolo.Text.ToUpper()))
            End Select
        End If

        If Not txtCavalo.Text = String.Empty Then
            Select Case cbFiltro2.SelectedValue
                Case 0
                    iSQL.Append(String.Format(" AND PLACA_CAVALO = '{0}' ", txtCavalo.Text.ToUpper()))
                Case 1
                    iSQL.Append(String.Format(" AND PLACA_CAVALO LIKE '%{0}%' ", txtCavalo.Text.ToUpper()))
            End Select
        End If

        If Not txtCarreta.Text = String.Empty Then
            Select Case cbFiltro3.SelectedValue
                Case 0
                    iSQL.Append(String.Format(" AND PLACA_CARRETA = '{0}' ", txtCarreta.Text.ToUpper()))
                Case 1
                    iSQL.Append(String.Format(" AND PLACA_CARRETA LIKE '%{0}%' ", txtCarreta.Text.ToUpper()))
            End Select
        End If

        If Not txtCNH.Text = String.Empty Then
            Select Case cbFiltro4.SelectedValue
                Case 0
                    iSQL.Append(String.Format(" AND CNH = '{0}' ", txtCNH.Text.ToUpper()))
                Case 1
                    iSQL.Append(String.Format(" AND CNH LIKE '%{0}%' ", txtCNH.Text.ToUpper()))
            End Select
        End If

        If Not txtMotorista.Text = String.Empty Then
            Select Case cbFiltro5.SelectedValue
                Case 0
                    iSQL.Append(String.Format(" AND NOME_MOTORISTA = '{0}' ", txtMotorista.Text.ToUpper()))
                Case 1
                    iSQL.Append(String.Format(" AND NOME_MOTORISTA LIKE '%{0}%' ", txtMotorista.Text.ToUpper()))
            End Select
        End If

        If Not txtDocumento.Text = String.Empty Then
            Select Case cbFiltro6.SelectedValue
                Case 0
                    iSQL.Append(String.Format(" AND NUM_DOC_SAIDA = {0} ", txtDocumento.Text.ToUpper()))
                Case 1
                    iSQL.Append(String.Format(" AND NUM_DOC_SAIDA LIKE '%{0}%' ", txtDocumento.Text.ToUpper()))
            End Select
        End If


        PanelPesquisa.Visible = False
        Consultar(iSQL.ToString())
        PanelBarra.Visible = True
        DgAgendamentos.Visible = True

    End Sub

    Protected Sub btPesquisar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btPesquisar.Click
        DgAgendamentos.Visible = False
        PanelBarra.Visible = False
        PanelPesquisa.Visible = True
    End Sub

    Protected Sub btLimpar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btLimpar.Click

        cbFiltro1.SelectedIndex = -1
        cbFiltro2.SelectedIndex = -1
        cbFiltro3.SelectedIndex = -1
        cbFiltro4.SelectedIndex = -1
        cbFiltro5.SelectedIndex = -1
        cbFiltro6.SelectedIndex = -1

        txtCarreta.Text = String.Empty
        txtCavalo.Text = String.Empty
        txtCNH.Text = String.Empty
        txtDocumento.Text = String.Empty
        txtMotorista.Text = String.Empty
        txtProtocolo.Text = String.Empty

    End Sub

    Protected Sub DgAgendamentos_PageIndexChanging(sender As Object, e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles DgAgendamentos.PageIndexChanging
        Me.DgAgendamentos.PageIndex = e.NewPageIndex
        Consultar()
    End Sub

    Protected Sub DgAgendamentos_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DgAgendamentos.SelectedIndexChanged

    End Sub

    Public Sub LimparSessaoPeriodo()
        Session("PERIODO_ANTERIOR_COD") = ""
        Session("PERIODO_ANTERIOR") = "Nenhum período foi selecionado." 'É o vazio da lblPeriodo de CadastrarAgendamentos.aspx
    End Sub
End Class