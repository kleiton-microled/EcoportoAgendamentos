Public Class ConsultarAgendamentosCargaSoltaCarregamento
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not Page.IsPostBack Then
            Consultar()
        End If

        If Request.QueryString("erro") IsNot Nothing Then
            If Request.QueryString("erro").ToString().Equals("1") Then
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgAlerta", "exibeMensagem('Nenhum período foi selelecionado para o Agendamento.');", True)
            End If
        End If

    End Sub

    Private Sub Consultar(Optional ByVal Filtro As String = "")

        Dim SQL As New StringBuilder

        SQL.Append("SELECT ")
        SQL.Append("    AUTONUM, ")
        SQL.Append("    RESERVA, ")
        SQL.Append("    NAVIO_VIAGEM, ")
        SQL.Append("    PROTOCOLO, ")
        SQL.Append("    PERIODO, ")
        SQL.Append("    NOME, ")
        SQL.Append("    CNH, ")
        SQL.Append("    VEICULO, ")
        SQL.Append("    NAVIO_VIAGEM, ")
        SQL.Append("    DT_DEAD_LINE, ")
        SQL.Append("    STATUS ")
        SQL.Append("FROM ")
        SQL.Append("    REDEX.VW_AGENDAMENTO_WEB_CONS_CS_CA ")
        SQL.Append("WHERE AUTONUM_TRANSPORTADORA = " & Session("SIS_AUTONUM_TRANSPORTADORA").ToString() & " ")
        SQL.Append(Filtro)
        SQL.Append(" ORDER BY ")
        SQL.Append("    PROTOCOLO DESC ")

        Me.dgConsulta.DataSource = Banco.List(SQL.ToString())
        Me.dgConsulta.DataBind()

    End Sub

    Protected Sub dgConsulta_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles dgConsulta.RowCommand

        Dim Index As Integer = e.CommandArgument
        Dim ID As String = Me.dgConsulta.DataKeys(Index)("AUTONUM").ToString()

        If e.CommandName = "DEL" Then
            If Not String.IsNullOrEmpty(ID) Or
                Not String.IsNullOrWhiteSpace(ID) Then

                If Banco.ExecuteScalar("SELECT STATUS FROM REDEX.TB_AGENDAMENTO_WEB_CS_CA WHERE AUTONUM = " & ID) = "GE" Then
                    If Banco.BeginTransaction("DELETE FROM REDEX.TB_AGENDAMENTO_WEB_CS_CA WHERE AUTONUM = " & ID) Then
                        Banco.BeginTransaction("DELETE FROM REDEX.TB_AGENDAMENTO_WEB_CS_ITENS WHERE AUTONUM_AGENDAMENTO = " & ID)
                        Consultar()
                    Else
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgAlerta", "alert('Erro ao Excluir o Agendamento. Tente Novamente.');", True)
                    End If
                Else
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgAlerta", "alert('O Agendamento não pode ser excluído pois já foi impresso.');", True)
                End If

            End If
        End If

        If e.CommandName = "EDITAR" Then
            If Not String.IsNullOrEmpty(ID) Or
                Not String.IsNullOrWhiteSpace(ID) Then
                If Banco.ExecuteScalar("SELECT STATUS FROM REDEX.TB_AGENDAMENTO_WEB_CS_CA WHERE AUTONUM = " & ID) = "GE" Then
                    Response.Redirect("AgendarCSCarregamento.aspx?id=" & ID)
                Else
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgAlerta", "alert('O Agendamento não pode ser editado pois já foi impresso.');", True)
                End If
            End If
        End If

    End Sub

    Protected Sub btnAgendar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnAgendar.Click
        Response.Redirect("AgendarCSCarregamento.aspx")
    End Sub

    Protected Sub btPesquisar2_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btPesquisar2.Click

        Dim SQL As New StringBuilder()

        If Not txtReserva.Text = String.Empty Then
            SQL.Append(" AND RESERVA = '" & txtReserva.Text.Trim.ToUpper().Replace("'", "") & "' ")
        End If

        If Not txtPlacaCV.Text = "___-____" Then
            SQL.Append(" AND VEICULO LIKE '%" & txtPlacaCV.Text.Trim.ToUpper() & "%' ")
        End If

        If Not txtPlacaCR.Text = "___-____" Then
            SQL.Append(" AND VEICULO LIKE '%" & txtPlacaCR.Text.Trim.ToUpper() & "%' ")
        End If

        If Not txtNavio.Text = String.Empty Then
            If cbFiltro1.SelectedValue = 0 Then
                SQL.Append(" AND UPPER(NAVIO_VIAGEM) = '" & txtNavio.Text.Trim.ToUpper().Replace("'", "") & "' ")
            ElseIf cbFiltro1.SelectedValue = 1 Then
                SQL.Append(" AND UPPER(NAVIO_VIAGEM) LIKE '%" & txtNavio.Text.Trim.ToUpper().Replace("'", "") & "%' ")
            End If
        End If

        If Not txtMotorista.Text = String.Empty Then
            If cbFiltro2.SelectedValue = 0 Then
                SQL.Append(" AND UPPER(NOME) = '" & txtMotorista.Text.Trim.ToUpper().Replace("'", "") & "' ")
            ElseIf cbFiltro2.SelectedValue = 1 Then
                SQL.Append(" AND UPPER(NOME) LIKE '%" & txtMotorista.Text.Trim.ToUpper().Replace("'", "") & "%' ")
            End If
        End If

        Consultar(SQL.ToString())

    End Sub

    Protected Sub btnPesquisar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnPesquisar.Click
        Me.pnFiltro.Visible = True
    End Sub

    Protected Sub btRetornar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btRetornar.Click
        Me.pnFiltro.Visible = False
    End Sub

    Protected Sub btLimpar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btLimpar.Click

        cbFiltro1.SelectedIndex = -1
        cbFiltro2.SelectedIndex = -1

        txtReserva.Text = String.Empty
        txtPlacaCV.Text = String.Empty
        txtPlacaCR.Text = String.Empty
        txtNavio.Text = String.Empty
        txtMotorista.Text = String.Empty

        Consultar()

    End Sub

End Class