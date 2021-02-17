Public Class ConsultarAgendamentosExportacao
    Inherits System.Web.UI.Page

    Dim ExportacaoBLL As New Exportacao
    Dim Conteiner As New Conteiner

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not Page.IsPostBack Then
            ConsultarTiposConteiner()
            CarregarGrid()
        End If

        dgConsulta.HeaderStyle.BackColor = System.Drawing.ColorTranslator.FromHtml(Session("SIS_COR_PADRAO").ToString())
        dgConsulta.PagerStyle.BackColor = System.Drawing.ColorTranslator.FromHtml(Session("SIS_COR_PADRAO").ToString())

    End Sub

    Private Sub CarregarGrid()

        DsConsulta.ConnectionString = Banco.StringConexaoDataSource
        DsConsulta.ProviderName = Banco.Provedor

        If Session("FILTRO_AG_EXPORTACAO") IsNot Nothing Then
            DsConsulta.SelectCommand = ExportacaoBLL.Consultar(Session("SIS_ID").ToString(), Session("FILTRO_AG_EXPORTACAO").ToString())
        Else
            DsConsulta.SelectCommand = ExportacaoBLL.Consultar(Session("SIS_ID").ToString(), "")
        End If

        DsConsulta.DataBind()
        dgConsulta.DataBind()

    End Sub

    Private Sub ConsultarTiposConteiner()

        Dim Lista As List(Of String) = Conteiner.ConsultarTipos()

        cbTipo.Items.Add("")

        For Each Item In Lista
            cbTipo.Items.Add(Item.ToString())
        Next

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

        If dgConsulta.Rows.Count > 0 Then

            Dim Check As CheckBox
            Dim ListaProtocolos As String = ""
            Dim Protocolo As String

            For Each Linha As GridViewRow In Me.dgConsulta.Rows
                Check = DirectCast(Linha.FindControl("CheckProtocolo"), CheckBox)
                If Check.Checked Then
                    Protocolo = dgConsulta.DataKeys(Linha.RowIndex)("NUM_PROTOCOLO").ToString() & dgConsulta.DataKeys(Linha.RowIndex)("ANO_PROTOCOLO").ToString()
                    ListaProtocolos = ListaProtocolos & Protocolo & ","
                End If             
            Next
            
            If ListaProtocolos = String.Empty Then
                ScriptManager.RegisterClientScriptBlock(Me, [GetType](), "script", "<script>alert('Selecione o(s) protocolo(s) que deseja imprimir.');</script>", False)
            Else

                If Mid(ListaProtocolos, ListaProtocolos.Length, ListaProtocolos.Length - 1) = "," Then
                    ListaProtocolos = ListaProtocolos.Remove(ListaProtocolos.Length - 1)
                End If

                ScriptManager.RegisterClientScriptBlock(Me, [GetType](), "script", "<script>window.open('Protocolo_Exp.aspx?protocolo=" & ListaProtocolos.ToString() & "', '_blank');</script>", False)

            End If

        End If

    End Sub

    Private Sub ImprimirTodosProtocolos()

        If dgConsulta.Rows.Count > 0 Then

            Dim ListaProtocolos As String = ""
            Dim Linhas As Integer = dgConsulta.Rows.Count - 1
            Dim Protocolo As String

            For I As Integer = 0 To Linhas
                Protocolo = dgConsulta.DataKeys(I)("NUM_PROTOCOLO").ToString() & dgConsulta.DataKeys(I)("ANO_PROTOCOLO").ToString()
                If I < Linhas Then
                    ListaProtocolos = ListaProtocolos & Protocolo & ","
                Else
                    ListaProtocolos = ListaProtocolos & Protocolo
                End If

            Next

            If ListaProtocolos = String.Empty Then
                ScriptManager.RegisterClientScriptBlock(Me, [GetType](), "script", "<script>alert('Não existem protocolos a serem impressos.');</script>", False)
            Else
                ScriptManager.RegisterClientScriptBlock(Me, [GetType](), "script", "<script>window.open('Protocolo_Exp.aspx?protocolo=" & ListaProtocolos.ToString() & "', '_blank');</script>", False)
            End If

        End If

    End Sub

    Protected Sub btPesquisar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btPesquisar.Click

        pnBarra.Visible = False
        pnPesquisa.Visible = True
        dgConsulta.Visible = False

    End Sub

    Protected Sub btPesquisar2_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btPesquisar2.Click

        Dim SQL As New StringBuilder()

        If Not txtReserva.Text = String.Empty Then
            SQL.Append(" AND A.REFERENCE = '" & txtReserva.Text.Trim.ToUpper().Replace("'", "") & "' ")
        End If

        If Not txtConteiner.Text = "__________-_" Then
            SQL.Append(" AND A.ID_CONTEINER = '" & txtConteiner.Text.Trim.ToUpper() & "' ")
        End If

        If Not cbTipo.Text = String.Empty Then
            SQL.Append(" AND A.TIPOBASICO = '" & cbTipo.Text.Trim.ToUpper() & "' ")
        End If

        If Not cbTamanho.Text = String.Empty Then
            SQL.Append(" AND A.TAMANHO = " & cbTamanho.Text & " ")
        End If

        If Not txtPlacaCV.Text = "___-____" Then
            SQL.Append(" AND G.PLACA_CAVALO = '" & txtPlacaCV.Text.Trim.ToUpper() & "' ")
        End If

        If Not txtPlacaCR.Text = "___-____" Then
            SQL.Append(" AND G.PLACA_CARRETA = '" & txtPlacaCR.Text.Trim.ToUpper() & "' ")
        End If

        If Not txtNavio.Text = String.Empty Then
            If cbFiltro1.SelectedValue = 0 Then
                SQL.Append(" AND UPPER(E.NOME) = '" & txtNavio.Text.Trim.ToUpper().Replace("'", "") & "' ")
            ElseIf cbFiltro1.SelectedValue = 1 Then
                SQL.Append(" AND UPPER(E.NOME) LIKE '%" & txtNavio.Text.Trim.ToUpper().Replace("'", "") & "%' ")
            End If
        End If

        If Not txtMotorista.Text = String.Empty Then
            If cbFiltro2.SelectedValue = 0 Then
                SQL.Append(" AND UPPER(C.NOME) = '" & txtMotorista.Text.Trim.ToUpper().Replace("'", "") & "' ")
            ElseIf cbFiltro2.SelectedValue = 1 Then
                SQL.Append(" AND UPPER(C.NOME) LIKE '%" & txtMotorista.Text.Trim.ToUpper().Replace("'", "") & "%' ")
            End If
        End If

        Session("FILTRO_AG_EXPORTACAO") = SQL.ToString()

        CarregarGrid()
        pnBarra.Visible = True
        pnPesquisa.Visible = False
        dgConsulta.Visible = True

    End Sub

    Protected Sub btRetornar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btRetornar.Click

        pnBarra.Visible = True
        pnPesquisa.Visible = False
        dgConsulta.Visible = True

    End Sub

    Protected Sub btLimpar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btLimpar.Click

        cbFiltro1.SelectedIndex = -1
        cbFiltro2.SelectedIndex = -1
        cbTamanho.SelectedIndex = -1
        cbTipo.SelectedIndex = -1

        txtReserva.Text = String.Empty
        txtPlacaCV.Text = String.Empty
        txtPlacaCR.Text = String.Empty
        txtNavio.Text = String.Empty
        txtMotorista.Text = String.Empty
        txtConteiner.Text = String.Empty

    End Sub

    Protected Sub dgConsulta_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles dgConsulta.RowCommand

        Dim ID As String = String.Empty
        Dim Motorista As String = String.Empty
        Dim ID_Periodo As String = String.Empty
        Dim Periodo_Inicial As String = String.Empty
        Dim Periodo_Final As String = String.Empty
        Dim Conteiner As String = String.Empty
        Dim Reserva As String = String.Empty
        Dim Periodo As String = String.Empty
        Dim Veiculo As String = String.Empty

        Select Case e.CommandName
            Case "EDITAR"

                ID = dgConsulta.DataKeys(e.CommandArgument)("AUTONUM_GD_CNTR").ToString()
                Motorista = dgConsulta.DataKeys(e.CommandArgument)("AUTONUM_GD_MOTORISTA").ToString()
                ID_Periodo = dgConsulta.DataKeys(e.CommandArgument)("AUTONUM_GD_RESERVA").ToString()
                Periodo_Inicial = dgConsulta.DataKeys(e.CommandArgument)("PERIODO_INICIAL").ToString()
                Periodo_Final = dgConsulta.DataKeys(e.CommandArgument)("PERIODO_FINAL").ToString()
                Conteiner = dgConsulta.DataKeys(e.CommandArgument)("ID_CONTEINER").ToString()
                Reserva = dgConsulta.DataKeys(e.CommandArgument)("REFERENCE").ToString()
                Periodo = Periodo_Inicial & " - " & Periodo_Final
                Veiculo = dgConsulta.DataKeys(e.CommandArgument)("AUTONUM_VEICULO").ToString()

                If Me.dgConsulta.Rows(e.CommandArgument).Cells(18).Text = "-" Then
                    Response.Redirect(String.Format("AgendamentoExportacao.aspx?id={0}&motorista={1}&id_periodo={2}&periodo={3}&conteiner={4}&reserva={5}&action=edit&agendado=1&veiculo={6}", ID, Server.UrlEncode(Motorista), ID_Periodo, Server.UrlEncode(Periodo), Server.UrlEncode(Conteiner), Reserva, Server.UrlEncode(Veiculo)))
                Else
                    ScriptManager.RegisterClientScriptBlock(Me, [GetType](), "script", "<script>alert('Edição não permitida. Este contêiner já entrou no Pátio.');</script>", False)
                End If

            Case "DEL"
                ID = dgConsulta.DataKeys((CType(CType(e.CommandSource, Control).NamingContainer, GridViewRow)).RowIndex)("AUTONUM_GD_CNTR").ToString()
                If Me.dgConsulta.Rows((CType(CType(e.CommandSource, Control).NamingContainer, GridViewRow)).RowIndex).Cells(18).Text = "-" Then
                    If ExportacaoBLL.DesassociarConteiner(ID) Then
                        ScriptManager.RegisterClientScriptBlock(Me, [GetType](), "script", "<script>alert('Desassociação realizada com sucesso.');location.href='ConsultarAgendamentosExportacao.aspx';</script>", False)
                    End If
                Else
                    ScriptManager.RegisterClientScriptBlock(Me, [GetType](), "script", "<script>alert('Edição não permitida. Este contêiner já entrou no Pátio.');</script>", False)
                End If
        End Select

    End Sub

    Protected Sub dgConsulta_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles dgConsulta.RowDataBound

        If e.Row.RowType = DataControlRowType.DataRow Then

            Dim Excluir As ImageButton = DirectCast(e.Row.FindControl("cmdExcluir"), ImageButton)
            Excluir.Attributes.Add("onclick", "javascript:return " & "confirm('Deseja realmente excluir o agendamento?');")

            Dim Link As TableCell = DirectCast(e.Row.Cells(4), TableCell)

            Dim ToolTipString1 As String = "Editar"
            e.Row.Cells(0).Attributes.Add("title", ToolTipString1)

            Dim ToolTipString2 As String = "Excluir"
            e.Row.Cells(1).Attributes.Add("title", ToolTipString2)

            Dim ToolTipString3 As String = "Imprimir"
            e.Row.Cells(13).Attributes.Add("title", ToolTipString3)

            If Not e.Row.Cells(18).Text = "-" Then
                Link.Enabled = False
            End If

        End If

    End Sub

    Protected Sub dgConsulta_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles dgConsulta.PageIndexChanging
        dgConsulta.PageIndex = e.NewPageIndex
        CarregarGrid()
    End Sub

    Protected Sub dgConsulta_SelectedIndexChanged(sender As Object, e As EventArgs) Handles dgConsulta.SelectedIndexChanged

    End Sub
End Class