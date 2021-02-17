Imports System.Linq
Public Class ConsultarVeiculos
    Inherits System.Web.UI.Page

    Dim VeiculoBLL As New Veiculo
    Dim VeiculoOBJ As New Veiculo

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not Page.IsPostBack Then           
            CarregarTiposCarreta()
            Consultar()
        End If

        DgVeiculos.HeaderStyle.BackColor = System.Drawing.ColorTranslator.FromHtml(Session("SIS_COR_PADRAO").ToString())
        DgVeiculos.PagerStyle.BackColor = System.Drawing.ColorTranslator.FromHtml(Session("SIS_COR_PADRAO").ToString())

    End Sub

    Private Sub Consultar()

        DsConsulta.ConnectionString = Banco.StringConexaoDataSource
        DsConsulta.ProviderName = Banco.Provedor

        If Session("FILTRO_CONSULTA_VEICULOS") IsNot Nothing Then
            DsConsulta.SelectCommand = VeiculoBLL.Consultar(Session("SIS_ID").ToString(), Session("FILTRO_CONSULTA_VEICULOS").ToString())
        Else
            DsConsulta.SelectCommand = VeiculoBLL.Consultar(Session("SIS_ID").ToString(), "")
        End If

        DsConsulta.DataBind()
        DgVeiculos.DataBind()

    End Sub

    Private Sub CarregarTiposCarreta()

        Me.cbTipo.DataTextField = "DESCR"
        Me.cbTipo.DataValueField = "AUTONUM"

        Me.cbTipo.DataSource = VeiculoBLL.ConsultarTipos()
        Me.cbTipo.DataBind()

        Me.cbTipo.Items.Insert(0, String.Empty)

    End Sub

    Protected Sub btNovo_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btNovo.Click
        Response.Redirect("CadastrarVeiculos.aspx")
    End Sub

    Protected Sub DgVeiculos_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles DgVeiculos.RowCommand

        Dim ID As String = String.Empty
        Dim Cavalo As String = String.Empty
        Dim Carreta As String = String.Empty
        Dim Transportadora As String = String.Empty

        Select Case e.CommandName

            Case "EDIT"
                ID = DgVeiculos.DataKeys(e.CommandArgument)("AUTONUM").ToString()
                Response.Redirect(String.Format("AlterarVeiculos.aspx?ID={0}", ID))
            Case "DEL"

                ID = DgVeiculos.DataKeys((CType(CType(e.CommandSource, Control).NamingContainer, GridViewRow)).RowIndex)("AUTONUM").ToString()
                Cavalo = DgVeiculos.DataKeys((CType(CType(e.CommandSource, Control).NamingContainer, GridViewRow)).RowIndex)("PLACA_CAVALO").ToString()
                Carreta = DgVeiculos.DataKeys((CType(CType(e.CommandSource, Control).NamingContainer, GridViewRow)).RowIndex)("PLACA_CARRETA").ToString()
                Transportadora = DgVeiculos.DataKeys((CType(CType(e.CommandSource, Control).NamingContainer, GridViewRow)).RowIndex)("ID_TRANSPORTADORA").ToString()

                Dim TransportadoraOBJ As New Transportadora
                TransportadoraOBJ.ID = Transportadora

                VeiculoOBJ.ID = ID
                VeiculoOBJ.Cavalo = Cavalo
                VeiculoOBJ.Carreta = Carreta
                VeiculoOBJ.Transportadora = TransportadoraOBJ

                If VeiculoBLL.ValidarExclusao(VeiculoOBJ) Then
                    If VeiculoBLL.Excluir(VeiculoOBJ) Then
                        ScriptManager.RegisterClientScriptBlock(Me, [GetType](), "script", "<script>alert('Veículo excluído com sucesso!');</script>", False)
                    Else
                        ScriptManager.RegisterClientScriptBlock(Me, [GetType](), "script", "<script>alert('Falha durante a exclusão.');</script>", False)
                    End If
                Else
                    ScriptManager.RegisterClientScriptBlock(Me, [GetType](), "script", "<script>alert('Operação não permitida. Este veículo está vinclulado a outros agendamentos.');</script>", False)
                End If

                Consultar()

        End Select

    End Sub

    Protected Sub DgVeiculos_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles DgVeiculos.RowDataBound

        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim Excluir As ImageButton = DirectCast(e.Row.FindControl("cmdExcluir"), ImageButton)
            Excluir.Attributes.Add("onclick", "javascript:return " & "confirm('Confirma a exclusão do Veículo?');")
        End If

    End Sub

    Protected Sub btFiltrar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btFiltrar.Click

        Dim iSQL As New StringBuilder

        If Not txtPlacaCavalo.Text = "___-____" Then
            Select Case cbFiltro1.SelectedValue
                Case 0
                    iSQL.Append(String.Format(" AND A.PLACA_CAVALO = '{0}' ", txtPlacaCavalo.Text.ToUpper()))
                Case 1
                    iSQL.Append(String.Format(" AND A.PLACA_CAVALO LIKE '%{0}%' ", txtPlacaCavalo.Text.ToUpper()))
            End Select
        End If

        If Not txtPlacaCarreta.Text = "___-____" Then
            Select Case cbFiltro2.SelectedValue
                Case 0
                    iSQL.Append(String.Format(" AND A.PLACA_CARRETA = '{0}' ", txtPlacaCarreta.Text.ToUpper()))
                Case 1
                    iSQL.Append(String.Format(" AND A.PLACA_CARRETA LIKE '%{0}%' ", txtPlacaCarreta.Text.ToUpper()))
            End Select
        End If

        If Not txtModelo.Text = String.Empty Then
            Select Case cbFiltro3.SelectedValue
                Case 0
                    iSQL.Append(String.Format(" AND A.MODELO = '{0}' ", txtModelo.Text.ToUpper()))
                Case 1
                    iSQL.Append(String.Format(" AND A.MODELO LIKE '%{0}%' ", txtModelo.Text.ToUpper()))
            End Select
        End If

        If Not txtCor.Text = String.Empty Then
            Select Case cbFiltro4.SelectedValue
                Case 0
                    iSQL.Append(String.Format(" AND A.COR = '{0}' ", txtCor.Text.ToUpper()))
                Case 1
                    iSQL.Append(String.Format(" AND A.COR LIKE '%{0}%' ", txtCor.Text.ToUpper()))
            End Select
        End If

        If Not txtChassi.Text = String.Empty Then
            Select Case cbFiltro5.SelectedValue
                Case 0
                    iSQL.Append(String.Format(" AND A.CHASSI = '{0}' ", txtChassi.Text.ToUpper()))
                Case 1
                    iSQL.Append(String.Format(" AND A.CHASSI LIKE '%{0}%' ", txtChassi.Text.ToUpper()))
            End Select
        End If

        If Not txtTara.Text = String.Empty Then
            Select Case cbFiltro6.SelectedValue
                Case 0
                    iSQL.Append(String.Format(" AND A.TARA = '{0}' ", txtTara.Text.ToUpper()))
                Case 1
                    iSQL.Append(String.Format(" AND A.TARA LIKE '%{0}%' ", txtTara.Text.ToUpper()))
            End Select
        End If

        If Not cbTipo.Text = String.Empty Then
            Select Case cbFiltro7.SelectedValue
                Case 0
                    iSQL.Append(String.Format(" AND A.ID_TIPO_CAMINHAO = {0} ", cbTipo.SelectedValue))
                Case 1
                    iSQL.Append(String.Format(" AND A.ID_TIPO_CAMINHAO LIKE '%{0}%' ", cbTipo.SelectedValue))
            End Select
        End If

        Session("FILTRO_CONSULTA_VEICULOS") = iSQL.ToString()

        PanelPesquisa.Visible = False
        Consultar()
        PanelBarra.Visible = True
        Me.DgVeiculos.Visible = True

    End Sub

    Protected Sub btLimpar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btLimpar.Click

        cbFiltro1.SelectedIndex = -1
        cbFiltro2.SelectedIndex = -1
        cbFiltro3.SelectedIndex = -1
        cbFiltro4.SelectedIndex = -1
        cbFiltro5.SelectedIndex = -1
        cbFiltro6.SelectedIndex = -1
        cbFiltro7.SelectedIndex = -1

        txtPlacaCarreta.Text = String.Empty
        txtPlacaCavalo.Text = String.Empty
        txtModelo.Text = String.Empty
        txtCor.Text = String.Empty
        txtChassi.Text = String.Empty
        txtTara.Text = String.Empty
        cbTipo.SelectedIndex = -1

    End Sub

    Protected Sub btPesquisar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btPesquisar.Click
        Me.DgVeiculos.Visible = False
        PanelBarra.Visible = False
        PanelPesquisa.Visible = True
    End Sub

    Protected Sub DgVeiculos_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles DgVeiculos.PageIndexChanging

        DgVeiculos.PageIndex = e.NewPageIndex
        Consultar()

    End Sub
End Class