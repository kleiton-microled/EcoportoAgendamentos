Public Class ConsultarLotesImportacao
    Inherits System.Web.UI.Page

    Const PENDENCIA = "SIM"

    Dim Agendamento As New Agendamento

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not Page.IsPostBack Then
            Consultar()
        End If

        dgConsulta.HeaderStyle.BackColor = System.Drawing.ColorTranslator.FromHtml(Session("SIS_COR_PADRAO").ToString())
        dgConsulta.PagerStyle.BackColor = System.Drawing.ColorTranslator.FromHtml(Session("SIS_COR_PADRAO").ToString())

    End Sub

    Private Sub Consultar(Optional ByVal Filtro As String = "")

        DsConsulta.ConnectionString = Banco.StringConexaoOracle()
        DsConsulta.ProviderName = "System.Data.OleDb"

        Dim SQL As New StringBuilder

        SQL.Append("SELECT ")
        SQL.Append(" LOTE, ")
        SQL.Append(" PATIO,")
        SQL.Append(" TIPO_DOCUMENTO,")
        SQL.Append(" NUM_DOCUMENTO,")
        SQL.Append(" NUMERO,")
        SQL.Append(" DESCR,   SALDO,   QUANTIDADE_LOTE,   QUANTIDADE_AGENDA,")
        SQL.Append("  CASE WHEN AGENDAR ='SIM' THEN 'NAO' ELSE 'SIM' END AGENDAR  ")
        SQL.Append(" FROM VW_CS_DISPONIVEL  ")
        SQL.Append(" WHERE ")
        SQL.Append(" TRANSPORTADORA =  " & Session("SIS_ID").ToString())
        SQL.Append("  AND ")
        SQL.Append("  (")
        SQL.Append("  SALDO  > 0")
        SQL.Append("  )")

        If Val(Session("SIS_TRANSPEMPRESA").ToString()) = 1 Then 'ECOPORTO 
            SQL.Append("    AND ")
            SQL.Append("    PATIO <> 5 ")
        Else ' ECOPORTO RA
            SQL.Append("    AND ")
            SQL.Append("    PATIO =5 ")
        End If
        SQL.Append("    ORDER  BY LOTE  ")

        DsConsulta.SelectCommand = SQL.ToString()

        DsConsulta.DataBind()

    End Sub

    Protected Sub DgEtapasCs_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles dgConsulta.PageIndexChanging

        dgConsulta.PageIndex = e.NewPageIndex
        Consultar()

    End Sub

    Protected Sub DgEtapasCs_SelectedIndexChanged(sender As Object, e As EventArgs) Handles dgConsulta.SelectedIndexChanged

    End Sub

    Protected Sub btnVoltar_Click(sender As Object, e As EventArgs) Handles btnVoltar.Click
        Response.Redirect("Principal.aspx")
    End Sub

    Protected Sub dgConsulta_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles dgConsulta.RowDataBound

        If e.Row.RowType = DataControlRowType.DataRow Then



            For index = 0 To e.Row.Cells.Count - 1

                    Dim HeaderText As String = dgConsulta.Columns(index).HeaderText

                    If HeaderText = "Pendências" Then
                    If e.Row.Cells(10).Text.ToUpper() = PENDENCIA Then
                        e.Row.Cells(index).BackColor = System.Drawing.Color.Red
                        e.Row.Cells(index).ForeColor = System.Drawing.Color.White
                        e.Row.Cells(index).HorizontalAlign = HorizontalAlign.Center

                    Else
                        e.Row.Cells(index).BackColor = System.Drawing.Color.LightGreen
                            e.Row.Cells(index).HorizontalAlign = HorizontalAlign.Center

                        End If
                    End If

                Next



            Dim lnkAgendar As LinkButton = TryCast(e.Row.FindControl("lnkAgendar"), LinkButton)

            If lnkAgendar IsNot Nothing Then

                Dim lblPendencias As Label = TryCast(e.Row.FindControl("lblPendencias"), Label)

                If lblPendencias IsNot Nothing Then
                    '    lblPendencias.BackColor = System.Drawing.Color.LightGreen
                    If lblPendencias.Text = "SIM" Then
                        'lnkAgendar.Visible = False
                        lnkAgendar.Text = "."
                        lnkAgendar.Enabled = False

                        lnkAgendar.ToolTip = "Visualizar Pendências"
                        '       lblPendencias.BackColor = System.Drawing.Color.Red
                        '      lblPendencias.ForeColor = System.Drawing.Color.White
                    End If

                End If

            End If

        End If

    End Sub
End Class