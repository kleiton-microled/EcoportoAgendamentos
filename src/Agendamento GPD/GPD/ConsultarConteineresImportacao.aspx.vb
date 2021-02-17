Public Class ConsultarConteineresImportacao
    Inherits System.Web.UI.Page

    Const PENDENCIA = "SIM"
    Dim ConteinerBLL As New ConteinerImportacao

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not Page.IsPostBack Then
            Consultar()
        End If

        dgConsulta.HeaderStyle.BackColor = System.Drawing.ColorTranslator.FromHtml(Session("SIS_COR_PADRAO").ToString())
        dgConsulta.PagerStyle.BackColor = System.Drawing.ColorTranslator.FromHtml(Session("SIS_COR_PADRAO").ToString())

    End Sub

    Private Sub Consultar(Optional ByVal Filtro As String = "")

        DsConsulta.ConnectionString = Banco.StringConexao(False)
        DsConsulta.ProviderName = "System.Data.OracleClient"

        DsConsulta.SelectCommand = ConteinerBLL.ConsultarConteineresDisponiveis(Session("SIS_ID").ToString(), Session("SIS_EMPRESA").ToString())

        DsConsulta.DataBind()

    End Sub

    Protected Sub dgConsulta_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles dgConsulta.PageIndexChanging

        dgConsulta.PageIndex = e.NewPageIndex
        Consultar()

    End Sub

    Protected Sub dgConsulta_SelectedIndexChanged(sender As Object, e As EventArgs) Handles dgConsulta.SelectedIndexChanged

    End Sub

    Protected Sub dgConsulta_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles dgConsulta.RowDataBound

        If e.Row.RowType = DataControlRowType.DataRow Then

            For index = 0 To e.Row.Cells.Count - 1

                Dim HeaderText As String = dgConsulta.Columns(index).HeaderText

                If HeaderText = "Pendências" Then
                    If e.Row.Cells(17).Text.ToUpper() = PENDENCIA Then
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
                    '   lblPendencias.BackColor = System.Drawing.Color.LightGreen
                    If lblPendencias.Text = "SIM" Then
                        'lnkAgendar.Visible = False
                        lnkAgendar.Text = "."
                        lnkAgendar.Enabled = False

                        lnkAgendar.ToolTip = "Visualizar Pendências"
                        'lblPendencias.BackColor = System.Drawing.Color.Red

                        '                    lblPendencias.ForeColor = System.Drawing.Color.White
                    End If

                End If
            End If
        End If
    End Sub
End Class