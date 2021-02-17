Public Class ConsultarMotoristas
    Inherits System.Web.UI.Page

    Dim Motorista As New Motorista

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not Page.IsPostBack Then
            Consultar()
        End If

    End Sub

    Private Sub Consultar(Optional ByVal Filtro As String = "")

        Me.DgMotoristas.DataSource = Motorista.Consultar(Session("SIS_ID").ToString(), Filtro)
        Me.DgMotoristas.DataBind()

    End Sub

    Protected Sub DgMotoristas_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles DgMotoristas.RowCommand

        Dim ID As String
        Dim Transportadora As String = Session("SIS_ID").ToString()

        Select Case e.CommandName
            Case "EDIT"
                ID = DgMotoristas.DataKeys(e.CommandArgument)("AUTONUM").ToString()
                Response.Redirect(String.Format("AlterarMotoristas.aspx?ID={0}", ID))
            Case "DEL"
                ID = DgMotoristas.DataKeys((CType(CType(e.CommandSource, Control).NamingContainer, GridViewRow)).RowIndex)("AUTONUM").ToString()

                Dim TransportadoraOBJ As New Transportadora
                TransportadoraOBJ.ID = Transportadora

                Dim MotoristaOBJ As New Motorista
                MotoristaOBJ.ID = ID
                MotoristaOBJ.Transportadora = TransportadoraOBJ

                If Motorista.ValidarExclusao(MotoristaOBJ) Then
                    Motorista.Excluir(MotoristaOBJ)
                    Consultar()
                Else
                    ScriptManager.RegisterClientScriptBlock(Me, [GetType](), "script", "<script>alert('Operação não permitida. Existem Agendamentos vinculados a esse Motorista.');</script>", False)
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

        If Not txtCavalo.Text = String.Empty Then
            Select Case cbFiltro1.SelectedValue
                Case 0
                    iSQL.Append(String.Format(" AND A.PLACA_CAVALO = '{0}' ", txtCavalo.Text.ToUpper()))
                Case 1
                    iSQL.Append(String.Format(" AND A.PLACA_CAVALO LIKE '%{0}%' ", txtCavalo.Text.ToUpper()))
            End Select
        End If

        If Not txtCarreta.Text = String.Empty Then
            Select Case cbFiltro2.SelectedValue
                Case 0
                    iSQL.Append(String.Format(" AND A.PLACA_CARRETA = '{0}' ", txtCarreta.Text.ToUpper()))
                Case 1
                    iSQL.Append(String.Format(" AND A.PLACA_CARRETA LIKE '%{0}%' ", txtCarreta.Text.ToUpper()))
            End Select
        End If

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

        PanelPesquisa.Visible = False
        Consultar(iSQL.ToString())
        PanelBarra.Visible = True
        Me.DgMotoristas.Visible = True

    End Sub

    Protected Sub btLimpar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btLimpar.Click

        cbFiltro1.SelectedIndex = -1
        cbFiltro2.SelectedIndex = -1
        cbFiltro3.SelectedIndex = -1
        cbFiltro4.SelectedIndex = -1

        txtCarreta.Text = String.Empty
        txtCavalo.Text = String.Empty
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

    Protected Sub DgMotoristas_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles DgMotoristas.PageIndexChanging
        Me.DgMotoristas.PageIndex = e.NewPageIndex
        Consultar()
    End Sub

End Class