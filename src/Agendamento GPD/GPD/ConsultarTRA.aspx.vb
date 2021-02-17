Public Class ConsultarTRA
    Inherits System.Web.UI.Page

    Dim AgRetirada As New AgRetiradaTRA

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("SIS_COD_EMPRESA").ToString <> "1" Then
            ScriptManager.RegisterClientScriptBlock(Me, [GetType](), "scriptTransp", "alert('Acesso da página negado a este pátio');location.href='Principal.aspx';", True)
            Exit Sub
        End If

        If Not Page.IsPostBack Then
            CarregarAgendamentos()
        End If

        dgConsulta.HeaderStyle.BackColor = System.Drawing.ColorTranslator.FromHtml(Session("SIS_COR_PADRAO").ToString())
        dgConsulta.PagerStyle.BackColor = System.Drawing.ColorTranslator.FromHtml(Session("SIS_COR_PADRAO").ToString())

    End Sub

    Protected Sub btAbrirPesquisar_Click(sender As Object, e As EventArgs) Handles btAbrirPesquisar.Click
        pnlPesquisa.Visible = True
        pnlBarra.Visible = False
        dgConsulta.Visible = False
    End Sub

    Protected Sub btLimpar_Click(sender As Object, e As EventArgs) Handles btLimpar.Click
        cbFiltroNavio.SelectedIndex = -1
        cbFiltroCNH.SelectedIndex = -1
        cbFiltroMotorista.SelectedIndex = -1

        txtNavio.Text = String.Empty
        txtMotorista.Text = String.Empty
        txtCNH.Text = String.Empty
        txtPlacaCV.Text = String.Empty
        txtPlacaCR.Text = String.Empty

        Session("FILTRO_AG_RETIRADA") = String.Empty

    End Sub

    Protected Sub btPesquisar_Click(sender As Object, e As EventArgs) Handles btPesquisar.Click
        Dim SQL As New StringBuilder

        If Not txtNavio.Text = String.Empty Then
            If cbFiltroNavio.SelectedIndex = 0 Then
                SQL.Append("    AND UPPER(RG.NAVIO_VIAGEM) = '" & txtNavio.Text.ToUpper() & "' ")
            ElseIf cbFiltroNavio.SelectedIndex = 1 Then
                SQL.Append("    AND UPPER(RG.NAVIO_VIAGEM) LIKE '%" & txtNavio.Text.ToUpper() & "%' ")
            End If
        End If

        If Not txtMotorista.Text = String.Empty Then
            If cbFiltroMotorista.SelectedIndex = 0 Then
                SQL.Append("    AND UPPER(M.NOME) = '" & txtMotorista.Text.ToUpper() & "' ")
            ElseIf cbFiltroMotorista.SelectedIndex = 1 Then
                SQL.Append("    AND UPPER(M.NOME) LIKE '%" & txtMotorista.Text.ToUpper() & "%' ")
            End If
        End If

        If Not txtCNH.Text = String.Empty Then
            If cbFiltroCNH.SelectedIndex = 0 Then
                SQL.Append("    AND UPPER(M.CNH) = '" & txtCNH.Text & "' ")
            ElseIf cbFiltroCNH.SelectedIndex = 1 Then
                SQL.Append("    AND UPPER(M.CNH) LIKE '%" & txtCNH.Text & "%' ")
            End If
        End If

        If Not txtPlacaCV.Text = "___-____" Then
            SQL.Append("    AND UPPER(V.PLACA_CAVALO) = '" & txtPlacaCV.Text.ToUpper() & "' ")
        End If

        If Not txtPlacaCR.Text = "___-____" Then
            SQL.Append("    AND UPPER(V.PLACA_CARRETA) = '" & txtPlacaCR.Text.ToUpper() & "' ")
        End If

        Session("FILTRO_AG_RETIRADA") = SQL.ToString()

        CarregarAgendamentos()
        pnlBarra.Visible = True
        dgConsulta.Visible = True
        pnlPesquisa.Visible = False

    End Sub

    Public Sub CarregarAgendamentos()

        If Session("FILTRO_AG_RETIRADA") Is Nothing Then
            dgConsulta.DataSource = AgRetirada.ConsAgendRetirada(Session("SIS_ID").ToString())
        Else
            dgConsulta.DataSource = AgRetirada.ConsAgendRetirada(Session("SIS_ID").ToString(), Session("FILTRO_AG_RETIRADA").ToString())
        End If
        dgConsulta.DataBind()

    End Sub

    Protected Sub dgConsulta_RowCommand(sender As Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles dgConsulta.RowCommand

        Dim ID As String = String.Empty

        Select Case e.CommandName
            Case "EDITAR"
                ID = dgConsulta.DataKeys(e.CommandArgument)("AUTONUM").ToString()
                If AgRetirada.ObterStatusImpressao(ID) = 1 Then
                    ScriptManager.RegisterClientScriptBlock(Me, [GetType](), "script", "<script>alert('Não há como Editar Agendamento, pois este já foi Impresso.');</script>", False)
                Else
                    Response.Redirect("RetiradaTRA.aspx?action=edit&id=" & ID)
                End If

            Case "DEL"
                Dim LinhAfet As Integer
                ID = dgConsulta.DataKeys((CType(CType(e.CommandSource, Control).NamingContainer, GridViewRow)).RowIndex)("AUTONUM").ToString()

                If AgRetirada.ObterStatusImpressao(ID) = 1 Then
                    ScriptManager.RegisterClientScriptBlock(Me, [GetType](), "scriptExcl", "<script>alert('Não há como Excluir Agendamento, pois este já foi Impresso');</script>", False)
                    Exit Sub
                End If

                LinhAfet = AgRetirada.ExcluirAgendamento(ID)
                If LinhAfet >= 1 Then
                    ScriptManager.RegisterClientScriptBlock(Me, [GetType](), "script", "<script>alert('Registro removido com sucesso.');location.href='ConsultarTRA.aspx';</script>", False)
                ElseIf LinhAfet = 0 Then
                    ScriptManager.RegisterClientScriptBlock(Me, [GetType](), "script", "<script>alert('Houve um erro ao tentar remover registro,\npode ser que este já foi excluído.');location.href='ConsultarTRA.aspx';</script>", False)
                Else
                    ScriptManager.RegisterClientScriptBlock(Me, [GetType](), "script", "<script>alert('Houve um erro ao tentar remover registro.');location.href='ConsultarTRA.aspx';</script>", False)
                End If
        End Select
    End Sub
    Protected Sub btRetornar_Click(sender As Object, e As EventArgs) Handles btRetornar.Click
        pnlBarra.Visible = True
        pnlPesquisa.Visible = False
        dgConsulta.Visible = True
    End Sub

    Protected Sub dgConsulta_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles dgConsulta.RowDataBound

        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim ToolTipEditar As String = "Editar"

            Dim Excluir As ImageButton = DirectCast(e.Row.FindControl("cmdExcluir"), ImageButton)
            Excluir.Attributes.Add("onclick", "javascript:return " & "confirm('Deseja realmente excluir o agendamento de protocolo " & DataBinder.Eval(e.Row.DataItem, "NUM_PROTOCOLO") & "/" & DataBinder.Eval(e.Row.DataItem, "ANO_PROTOCOLO") & "?');")

            e.Row.Cells(0).Attributes.Add("title", ToolTipEditar)
        End If
    End Sub

    Protected Sub dgConsulta_PageIndexChanging(sender As Object, e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles dgConsulta.PageIndexChanging
        dgConsulta.PageIndex = e.NewPageIndex
        CarregarAgendamentos()
    End Sub

    Protected Sub btImprimir_Click(sender As Object, e As EventArgs) Handles btImprimir.Click

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

            For Each Agendamento As GridViewRow In Me.dgConsulta.Rows
                Check = DirectCast(Agendamento.FindControl("CheckProtocolo"), CheckBox)
                If Check.Checked Then
                    Protocolo = dgConsulta.DataKeys(Agendamento.RowIndex)("NUM_PROTOCOLO").ToString() & dgConsulta.DataKeys(Agendamento.RowIndex)("ANO_PROTOCOLO").ToString()
                    ListaProtocolos = ListaProtocolos & Protocolo & ","
                End If
            Next

            If ListaProtocolos = String.Empty Then
                ScriptManager.RegisterClientScriptBlock(Me, [GetType](), "script", "<script>alert('Selecione o(s) protocolo(s) que deseja imprimir.');</script>", False)
            Else
                'If Mid(ListaProtocolos, ListaProtocolos.Length, ListaProtocolos.Length - 1) = "," Then
                ListaProtocolos = ListaProtocolos.Remove(ListaProtocolos.Length - 1) 'Se vier aqui é porque o último caracter é ','
                'End If
                ScriptManager.RegisterClientScriptBlock(Me, [GetType](), "script", "<script>window.open('Protocolo_RetiradaTRA.aspx?protocolo=" & ListaProtocolos.ToString() & "', '_blank');</script>", False)
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
                ScriptManager.RegisterClientScriptBlock(Me, [GetType](), "script", "<script>window.open('Protocolo_RetiradaTRA.aspx?protocolo=" & ListaProtocolos.ToString() & "', '_blank');</script>", False)
            End If

        End If

    End Sub
End Class