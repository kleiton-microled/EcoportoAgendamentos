Public Class ConsultarAgendamentosImportacao
    Inherits System.Web.UI.Page

    Dim ImportacaoBLL As New Importacao
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

        DsConsulta.ConnectionString = Banco.StringConexao(False)
        DsConsulta.ProviderName = "System.Data.OracleClient"

        If Session("FILTRO_AG_IMPORTACAO") IsNot Nothing Then
            DsConsulta.SelectCommand = ImportacaoBLL.Consultar(Session("SIS_ID").ToString(), Session("SIS_EMPRESA").ToString(), Session("FILTRO_AG_IMPORTACAO").ToString())
        Else
            DsConsulta.SelectCommand = ImportacaoBLL.Consultar(Session("SIS_ID").ToString(), Session("SIS_EMPRESA").ToString(), "")
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

            Dim ListaProtocolos As String = ""
            Dim Linhas As Integer = dgConsulta.Rows.Count - 1
            Dim Protocolo As String

            For I As Integer = 0 To Linhas
                If DirectCast(dgConsulta.Rows(I).FindControl("CheckProtocolo"), CheckBox).Checked = True Then
                    Protocolo = dgConsulta.DataKeys(I)("NUM_PROTOCOLO").ToString() & dgConsulta.DataKeys(I)("ANO_PROTOCOLO").ToString()
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

                If Documento.AgendamentoRecusado(ListaProtocolos, Val(Session("SIS_ID").ToString())) Then
                    Response.Redirect("AgendamentoRecusado.aspx?protocolo=" & ListaProtocolos)
                End If

                If Not Documento.AgendamentoLiberado(ListaProtocolos, Val(Session("SIS_ID").ToString())) Then
                    Response.Redirect("ProtocoloNaoLiberado.aspx?protocolo=" & ListaProtocolos)
                    Exit Sub
                End If

                ScriptManager.RegisterClientScriptBlock(Me, [GetType](), "script", "<script>window.open('Protocolo_Imp.aspx?protocolo=" & ListaProtocolos.ToString() & "', '_blank');</script>", False)

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

            If Not Documento.AgendamentoLiberado(ListaProtocolos.ToString(), Val(Session("SIS_EMPRESA").ToString())) Then
                Response.Redirect("ProtocoloNaoLiberado.aspx")
                Exit Sub
            End If

            If ListaProtocolos = String.Empty Then
                ScriptManager.RegisterClientScriptBlock(Me, [GetType](), "script", "<script>alert('Não existem protocolos a serem impressos.');</script>", False)
            Else
                ScriptManager.RegisterClientScriptBlock(Me, [GetType](), "script", "<script>window.open('Protocolo_Imp.aspx?protocolo=" & ListaProtocolos.ToString() & "', '_blank');</script>", False)
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
            SQL.Append(" AND A.ID_CONTEINER = '" & txtReserva.Text & "' ")

        End If
        If Not cbTipo.Text = String.Empty Then
            SQL.Append(" AND TC.DESCR = '" & cbTipo.Text & "' ")
        End If

        If Not cbTamanho.Text = String.Empty Then
            SQL.Append(" AND TAMANHO = '" & cbTamanho.Text & "' ")
        End If

        If Not txtPlacaCV.Text = "___-____" Then
            SQL.Append(" AND V.PLACA_CAVALO = '" & txtPlacaCV.Text & "' ")
        End If

        If Not txtPlacaCR.Text = "___-____" Then
            SQL.Append(" AND V.PLACA_CARRETA = '" & txtPlacaCR.Text & "' ")
        End If

        If Not txtMotorista.Text = String.Empty Then
            If cbFiltro1.SelectedValue = 0 Then
                SQL.Append(" AND UPPER(M.NOME) = '" & txtMotorista.Text.ToUpper().Replace("'", "") & "' ")
            ElseIf cbFiltro1.SelectedValue = 1 Then
                SQL.Append(" AND UPPER(M.NOME) LIKE '%" & txtMotorista.Text.ToUpper().Replace("'", "") & "%' ")
            End If
        End If

        Session("FILTRO_AG_IMPORTACAO") = SQL.ToString()

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
        cbTamanho.SelectedIndex = -1
        cbTipo.SelectedIndex = -1

        txtReserva.Text = String.Empty
        txtPlacaCV.Text = String.Empty
        txtPlacaCR.Text = String.Empty
        txtMotorista.Text = String.Empty

    End Sub

    Protected Sub dgConsulta_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles dgConsulta.RowCommand

        Dim ID As String = String.Empty
        Dim Chegada As String = String.Empty

        Select Case e.CommandName
            Case "EDIT"

                ID = dgConsulta.DataKeys(e.CommandArgument)("AUTONUM").ToString()
                Chegada = dgConsulta.DataKeys(e.CommandArgument)("DT_CHEGADA_VEI").ToString()

                If IsDate(Chegada) Then
                    ScriptManager.RegisterClientScriptBlock(Me, [GetType](), "script", "<script>alert('O agendamento não pode ser editado pois já consta o registro da carga!');location.href='ConsultarAgendamentosimportacao.aspx';</script>", False)
                    Exit Sub
                Else
                    If Conteiner.AgendamentoComRegistro(ID) > 0 Then
                        ScriptManager.RegisterClientScriptBlock(Me, [GetType](), "script", "<script>alert('O agendamento não pode ser editado pois já consta o registro da carga!');location.href='ConsultarAgendamentosimportacao.aspx';</script>", False)
                        Exit Sub
                    End If
                End If

                Response.Redirect(String.Format("AgendamentoImportacao.aspx?id={0}", ID))

            Case "DEL"

                ID = dgConsulta.DataKeys((CType(CType(e.CommandSource, Control).NamingContainer, GridViewRow)).RowIndex)("AUTONUM").ToString()
                Chegada = dgConsulta.DataKeys(e.CommandArgument)("DT_CHEGADA_VEI").ToString()

                If IsDate(Chegada) Then
                    ScriptManager.RegisterClientScriptBlock(Me, [GetType](), "script", "<script>alert('O agendamento não pode ser excluído pois já consta o registro da carga!');location.href='ConsultarAgendamentosimportacao.aspx';</script>", False)
                    Exit Sub
                Else
                    If Conteiner.AgendamentoComRegistro(ID) > 0 Then
                        ScriptManager.RegisterClientScriptBlock(Me, [GetType](), "script", "<script>alert('O agendamento não pode ser excluído pois já consta o registro da carga!');location.href='ConsultarAgendamentosimportacao.aspx';</script>", False)
                        Exit Sub
                    End If
                End If

                If ImportacaoBLL.DesassociarConteiner(ID) Then
                    ScriptManager.RegisterClientScriptBlock(Me, [GetType](), "script", "<script>alert('Desassociação realizada com sucesso.');location.href='ConsultarAgendamentosimportacao.aspx';</script>", False)
                End If
        End Select

    End Sub

    Protected Sub dgConsulta_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles dgConsulta.RowDataBound

        If e.Row.RowType = DataControlRowType.DataRow Then

            Dim Excluir As ImageButton = DirectCast(e.Row.FindControl("cmdExcluir"), ImageButton)
            Excluir.Attributes.Add("onclick", "javascript:return " & "confirm('Deseja realmente excluir o agendamento?');")

            Dim ToolTipString1 As String = "Editar"
            e.Row.Cells(0).Attributes.Add("title", ToolTipString1)

            Dim ToolTipString2 As String = "Excluir"
            e.Row.Cells(1).Attributes.Add("title", ToolTipString2)

            Dim ToolTipString3 As String = "Imprimir"
            e.Row.Cells(7).Attributes.Add("title", ToolTipString3)

        End If

    End Sub

    Protected Sub dgConsulta_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles dgConsulta.PageIndexChanging
        dgConsulta.PageIndex = e.NewPageIndex
        CarregarGrid()
    End Sub

    Protected Sub dgConsulta_RowUpdating(sender As Object, e As GridViewUpdateEventArgs) Handles dgConsulta.RowUpdating

    End Sub

    Protected Sub dgConsulta_RowEditing(sender As Object, e As GridViewEditEventArgs) Handles dgConsulta.RowEditing

    End Sub

    Protected Sub txtReserva_TextChanged(sender As Object, e As EventArgs) Handles txtReserva.TextChanged

    End Sub

    Protected Sub dgConsulta_SelectedIndexChanged(sender As Object, e As EventArgs)

    End Sub
End Class