Public Class ConsultarRecTransp
    Inherits System.Web.UI.Page

    Dim Recinto As New Recinto
    Dim gdv As GridViewHelper

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("SIS_COD_EMPRESA").ToString <> "1" Then
            ScriptManager.RegisterClientScriptBlock(Me, [GetType](), "scriptTransp", "alert('Acesso da página negado a este pátio');location.href='Principal.aspx';", True)
            Exit Sub
        End If

        If Not Page.IsPostBack Then
            CarregarComboOpcao()
            'cbOpcao.SelectedIndex = -1
            If Not Convert.ToBoolean(Session("SIS_FLAG_RECINTO")) Then
                btNovo.Visible = False
            End If
            Consultar()
        End If
    End Sub

    ''' <summary>
    ''' Carrega as consultas disponíveis que a empresa logada pode realizar
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CarregarComboOpcao()
        If Session("SIS_TIPO").ToString() = "E" Then
            cbOpcao.Items.Add(New ListItem("CONSULTAR TODAS AS TRANSPORTADORAS VINCULADAS", "E"))
        End If

        If Convert.ToBoolean(Session("SIS_FLAG_TRANSPORTADOR")) Then
            cbOpcao.Items.Add(New ListItem("CONSULTAR OS RECINTOS VINCULADOS", "T"))
        End If

        If Convert.ToBoolean(Session("SIS_FLAG_RECINTO")) Then
            cbOpcao.Items.Add(New ListItem("CONSULTAR AS TRANSPORTADORAS VINCULADAS", "R"))
        End If

    End Sub

    'Private Sub Consultar()
    '    'If Session("SIS_TIPO").ToString() = "T" Then
    '    'dgTranspRec.DataSource = Recinto.ConsultarRecintosTransp(Session("SIS_ID").ToString())
    '    If Session("SIS_TIPO").ToString() = "E" Then
    '        dgTranspRec.DataSource = Recinto.ConsultarTranspRecintos()
    '    ElseIf Convert.ToBoolean(Session("SIS_FLAG_RECINTO")) Then 'R
    '        dgTranspRec.DataSource = Recinto.ConsultarTranspRecintos(Session("SIS_ID").ToString())
    '    Else 'T
    '        dgTranspRec.DataSource = Recinto.ConsultarRecintosTransp(Session("SIS_ID").ToString())
    '    End If
    '    dgTranspRec.DataBind()
    '    gdv = New GridViewHelper(dgTranspRec)

    '    If Not Convert.ToBoolean(Session("SIS_FLAG_RECINTO")) Then 'Session("SIS_TIPO").ToString() = "T" Then
    '        dgTranspRec.Columns(5).Visible = False 'Edição
    '        dgTranspRec.Columns(6).Visible = False 'Exclusão
    '        dgTranspRec.Columns(gdv.GetColumnIndex("DESCR_RECINTO")).Visible = False
    '    Else
    '        If Session("SIS_TIPO").ToString() = "R" Then
    '            dgTranspRec.Columns(gdv.GetColumnIndex("DESCR_RECINTO")).Visible = False
    '        End If
    '        dgTranspRec.Columns(gdv.GetColumnIndex("DESCR")).HeaderText = "DESCRIÇÃO TRANSPORTADORA"
    '        dgTranspRec.Columns(gdv.GetColumnIndex("DESCR_RESUMIDA")).HeaderText = "NOME FANTASIA"
    '    End If
    'End Sub

    ''' <summary>
    ''' Consulta de acordo com o que está solicitado em cbOpcao
    ''' </summary>
    ''' <remarks>Com este método dá para alterar a visualização quando a empresa for EcoPorto ou for transportadora e recinto simultaneamente</remarks>
    Private Sub Consultar()
        If cbOpcao.SelectedValue = "E" Then
            dgTranspRec.DataSource = Recinto.ConsultarTranspRecintos()
        ElseIf cbOpcao.SelectedValue = "R" Then 'R
            dgTranspRec.DataSource = Recinto.ConsultarTranspRecintos(Session("SIS_ID_REC").ToString())
        ElseIf cbOpcao.SelectedValue = "T" Then 'T
            dgTranspRec.DataSource = Recinto.ConsultarRecintosTransp(Session("SIS_ID").ToString())
        End If


        'gdv = New GridViewHelper(dgTranspRec)

        If cbOpcao.SelectedValue = "T" Then
            dgTranspRec.Columns(5).Visible = False 'Edição
            dgTranspRec.Columns(6).Visible = False 'Exclusão
            dgTranspRec.Columns(3).Visible = False
            dgTranspRec.Columns(1).HeaderText = "DESCRIÇÃO"
            dgTranspRec.Columns(2).HeaderText = "DESCRIÇÃO RESUMIDA"
        Else
           
            dgTranspRec.Columns(5).Visible = True 'Edição
            dgTranspRec.Columns(6).Visible = True 'Exclusão
            dgTranspRec.Columns(3).Visible = True
            dgTranspRec.Columns(1).HeaderText = "DESCRIÇÃO TRANSPORTADORA"
            If cbOpcao.SelectedValue = "R" Then
                dgTranspRec.Columns(3).Visible = False
            Else
                dgTranspRec.Columns(3).Visible = True
            End If
            dgTranspRec.Columns(2).HeaderText = "NOME FANTASIA"
        End If

        dgTranspRec.DataBind()


    End Sub

    Protected Sub btNovo_Click(sender As Object, e As EventArgs) Handles btNovo.Click
        Response.Redirect("VincularTransportadora.aspx")
    End Sub

    Protected Sub dgTranspRec_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles dgTranspRec.RowDataBound

        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim Excluir As ImageButton = DirectCast(e.Row.FindControl("cmdExcluir"), ImageButton)
            Excluir.Attributes.Add("onclick", "javascript:return " & "confirm('Tem certeza que deseja desvincular este recinto (" & DataBinder.Eval(e.Row.DataItem, "DESCR").ToString().Trim() & ") desta transportadora?');")
        End If

    End Sub

    Protected Sub dgTranspRec_RowCommand(sender As Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles dgTranspRec.RowCommand

        Dim Id As String

        Select Case e.CommandName
            Case "EDIT"
                Id = dgTranspRec.DataKeys(e.CommandArgument)("AUTONUM").ToString()
                Response.Redirect("AlterarVincTransp.aspx?Id=" & Id)
            Case "DEL"
                Id = dgTranspRec.DataKeys((CType(CType(e.CommandSource, Control).NamingContainer, GridViewRow)).RowIndex)("AUTONUM").ToString()
                'Deve-se fazer algo para validar exclusão???
                If Recinto.ExcluirRecintoTransp(Id) > 0 Then
                    ScriptManager.RegisterClientScriptBlock(Me, [GetType](), "script", "<script>alert('Vinculação desfeita!');</script>", False)
                    Consultar()
                Else
                    ScriptManager.RegisterClientScriptBlock(Me, [GetType](), "script", "<script>alert('Erro ao desfazer vinculação!\nPode ser que um outro usuário já a desfez');</script>", False)
                    Consultar()
                End If

        End Select

    End Sub

    Protected Sub dgTranspRec_PageIndexChanging(sender As Object, e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles dgTranspRec.PageIndexChanging
        dgTranspRec.PageIndex = e.NewPageIndex
        Consultar()
    End Sub

    Protected Sub cbOpcao_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbOpcao.SelectedIndexChanged
        Consultar()
    End Sub

End Class