Public Class ConsultarConteineresExportacao
    Inherits System.Web.UI.Page

    Dim ConteinerBLL As New ConteinerExportacao

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not Page.IsPostBack Then
            Consultar()
        End If

        dgConsulta.HeaderStyle.BackColor = System.Drawing.ColorTranslator.FromHtml(Session("SIS_COR_PADRAO").ToString())
        dgConsulta.PagerStyle.BackColor = System.Drawing.ColorTranslator.FromHtml(Session("SIS_COR_PADRAO").ToString())

    End Sub

    Private Sub Consultar()

        dsConsulta.ConnectionString = Banco.StringConexaoDataSource
        dsConsulta.ProviderName = Banco.Provedor

        If Session("FILTRO_CNTR_EXPORTACAO") IsNot Nothing Then
            dsConsulta.SelectCommand = ConteinerBLL.Consultar(Session("SIS_ID").ToString(), Session("FILTRO_CNTR_EXPORTACAO").ToString())
        Else
            dsConsulta.SelectCommand = ConteinerBLL.Consultar(Session("SIS_ID").ToString())
        End If

        dsConsulta.DataBind()
        dgConsulta.DataBind()

    End Sub

    Protected Sub btNovo_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btNovo.Click
        Response.Redirect("CadastrarConteineresExportacao.aspx")
    End Sub

    Protected Sub dgConsulta_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles dgConsulta.RowCommand

        Dim Index As Integer = e.CommandArgument
        Dim ID As String = dgConsulta.DataKeys(Index)("AUTONUM_GD_CNTR").ToString()

        If Not ID = String.Empty Or Not ID = 0 Then
            Select Case e.CommandName
                Case "EDIT"
                    If ConteinerBLL.HabilitarEdicaoExclusao(ID) Then
                        Response.Redirect(String.Format("CadastrarConteineresExportacao.aspx?id={0}&action=edit", ID))
                    Else
                        ScriptManager.RegisterClientScriptBlock(Me, [GetType](), "script", "<script>alert('Edição não permiida. Esse contêiner já foi agendado.');</script>", False)
                    End If
                Case "DEL"
                    If ConteinerBLL.HabilitarEdicaoExclusao(ID) Then
                        If ConteinerBLL.Excluir(ID) Then
                            ScriptManager.RegisterClientScriptBlock(Me, [GetType](), "script", "<script>alert('Contêiner Excluído com Sucesso!');location.href='ConsultarConteineresExportacao.aspx';</script>", False)
                        End If
                    Else
                        ScriptManager.RegisterClientScriptBlock(Me, [GetType](), "script", "<script>alert('Edição não permiida. Esse contêiner já foi agendado.');</script>", False)
                    End If
            End Select
        End If

    End Sub

    Protected Sub dgConsulta_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles dgConsulta.RowDataBound

        If e.Row.RowType = DataControlRowType.DataRow Then

            'Excluir: adiciona atributos
            Dim Excluir As ImageButton = DirectCast(e.Row.FindControl("cmdExcluir"), ImageButton)
            Excluir.Attributes.Add("onclick", "javascript:return " & "confirm('Confirma a exclusão do Contêiner: " & DataBinder.Eval(e.Row.DataItem, "ID_CONTEINER").ToString() & "?');")
        End If

    End Sub

    Protected Sub btPEsquisar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btPEsquisar.Click

        pnBarra.Visible = False
        pnPesquisa.Visible = True
        dgConsulta.Visible = False

    End Sub

    Protected Sub btFiltrar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btFiltrar.Click

        Dim Filtro As New StringBuilder

        If Not txtReserva.Text = String.Empty Then
            If cbFiltro1.SelectedIndex = 0 Then
                Filtro.Append(" AND REFERENCE='" & txtReserva.Text.ToUpper() & "' ")
            Else
                Filtro.Append(" AND REFERENCE LIKE '%" & txtReserva.Text.ToUpper() & "%' ")
            End If
        End If

        If Not txtConteiner.Text = String.Empty Then
            Filtro.Append(" AND ID_CONTEINER='" & txtConteiner.Text & "' ")
        End If

        Session("FILTRO_CNTR_EXPORTACAO") = Filtro.ToString()

        Consultar()
        pnBarra.Visible = True
        PnPesquisa.Visible = False
        dgConsulta.Visible = True

    End Sub

    Protected Sub btLimpar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btLimpar.Click

        cbFiltro1.SelectedIndex = -1
        cbFiltro2.SelectedIndex = -1
        txtReserva.Text = String.Empty
        txtConteiner.Text = String.Empty

    End Sub

    Protected Sub dgConsulta_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles dgConsulta.PageIndexChanging

        dgConsulta.PageIndex = e.NewPageIndex
        Consultar()

    End Sub
End Class