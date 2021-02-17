Public Class ConsultarMotoristas
    Inherits System.Web.UI.Page

    Dim MotoristaBLL As New Motorista
    Dim MotoristaOBJ As New Motorista

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not Page.IsPostBack Then
            Consultar()
        End If

        DgMotoristas.HeaderStyle.BackColor = System.Drawing.ColorTranslator.FromHtml(Session("SIS_COR_PADRAO").ToString())
        DgMotoristas.PagerStyle.BackColor = System.Drawing.ColorTranslator.FromHtml(Session("SIS_COR_PADRAO").ToString())

    End Sub

    Private Sub Consultar()

        DsConsulta.ConnectionString = Banco.StringConexao(False)
        DsConsulta.ProviderName = "System.Data.OracleClient"

        If Session("FILTRO_CONSULTA_MOTORISTAS") IsNot Nothing Then
            DsConsulta.SelectCommand = MotoristaBLL.Consultar(Session("SIS_ID").ToString(), Session("FILTRO_CONSULTA_MOTORISTAS").ToString())
        Else
            DsConsulta.SelectCommand = MotoristaBLL.Consultar(Session("SIS_ID").ToString(), "")
        End If

        DsConsulta.DataBind()
        DgMotoristas.DataBind()

    End Sub

    Private Sub DgMotoristas_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles DgMotoristas.PageIndexChanging

        DgMotoristas.PageIndex = e.NewPageIndex
        Consultar()

    End Sub

    Protected Sub DgMotoristas_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles DgMotoristas.RowCommand

        Dim ID As String = String.Empty

        Select Case e.CommandName
            Case "EDIT"
                ID = DgMotoristas.DataKeys(e.CommandArgument)("AUTONUM").ToString()
                Response.Redirect(String.Format("AlterarMotoristas.aspx?ID={0}", ID))
            Case "DEL"

                ID = DgMotoristas.DataKeys((CType(CType(e.CommandSource, Control).NamingContainer, GridViewRow)).RowIndex)("AUTONUM").ToString()

                Dim TransportadoraOBJ As New Transportadora
                TransportadoraOBJ.ID = Session("SIS_ID").ToString()

                MotoristaOBJ.ID = ID
                MotoristaOBJ.Transportadora = TransportadoraOBJ

                If Not MotoristaBLL.ValidarExclusao(MotoristaOBJ) Then
                    MotoristaBLL.Excluir(MotoristaOBJ)
                    Consultar()
                Else
                    ScriptManager.RegisterClientScriptBlock(Me, [GetType](), "script", "<script>alert('Operação não permitida. Este Motorista está vinculado a um Agendamento (Importação/Exportação).');</script>", False)
                End If

        End Select

    End Sub

    Protected Sub DgMotoristas_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles DgMotoristas.RowDataBound

        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim Excluir As ImageButton = DirectCast(e.Row.FindControl("cmdExcluir"), ImageButton)
            Excluir.Attributes.Add("onclick", "javascript:return " & "confirm('Confirma a exclusão do Motorista: " & DataBinder.Eval(e.Row.DataItem, "NOME").ToString() & "?');")
        End If

    End Sub

    Protected Sub btFiltrar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btFiltrar.Click

        Dim iSQL As New StringBuilder

        If Not txtCNH.Text = String.Empty Then
            Select Case cbFiltro3.SelectedValue
                Case 0
                    iSQL.Append(String.Format(" AND A.CNH = '{0}' ", txtCNH.Text.ToUpper()))
                Case 1
                    iSQL.Append(String.Format(" AND A.CNH LIKE '%{0}%' ", txtCNH.Text.ToUpper()))
            End Select
        End If

        If Not txtMotorista.Text = String.Empty Then
            Select Case cbFiltro4.SelectedValue
                Case 0
                    iSQL.Append(String.Format(" AND A.NOME = '{0}' ", txtMotorista.Text.ToUpper()))
                Case 1
                    iSQL.Append(String.Format(" AND A.NOME LIKE '%{0}%' ", txtMotorista.Text.ToUpper()))
            End Select
        End If

        Session("FILTRO_CONSULTA_MOTORISTAS") = iSQL.ToString()

        PanelPesquisa.Visible = False
        Consultar()
        PanelBarra.Visible = True
        Me.DgMotoristas.Visible = True

    End Sub

    Protected Sub btLimpar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btLimpar.Click

        cbFiltro3.SelectedIndex = -1
        cbFiltro4.SelectedIndex = -1

        txtCNH.Text = String.Empty
        txtMotorista.Text = String.Empty

    End Sub

    Protected Sub btNovo_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btNovo.Click
        Response.Redirect("CadastrarMotoristas.aspx")
    End Sub

    Protected Sub btPesquisar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btPesquisar.Click

        Me.DgMotoristas.Visible = False
        PanelBarra.Visible = False
        PanelPesquisa.Visible = True

    End Sub

End Class