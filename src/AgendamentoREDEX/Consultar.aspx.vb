Public Class Consultar
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not Page.IsPostBack Then
            Consultar()
        End If

    End Sub

    Private Sub Consultar()

        Dim SQL As New StringBuilder

        SQL.Append("SELECT ")
        SQL.Append("  A.AUTONUM, ")
        SQL.Append("  B.NOME, ")
        SQL.Append("  B.CNH, ")
        SQL.Append("  TO_CHAR(A.DATA_AGENDAMENTO,'DD/MM/YYYY') DATA_AGENDAMENTO, ")
        SQL.Append("  C.PLACA_CAVALO || ' / ' || C.PLACA_CARRETA AS VEICULO, ")
        SQL.Append("  A.NUM_PROTOCOLO || '/' || A.ANO_PROTOCOLO AS PROTOCOLO, ")
        SQL.Append("  DECODE(A.STATUS,'GE','Gerado','Impresso') AS STATUS ")
        SQL.Append("FROM ")
        SQL.Append("  REDEX.TB_AGENDAMENTO_WEB_CS A ")
        SQL.Append("INNER JOIN ")
        SQL.Append("  OPERADOR.TB_AG_MOTORISTAS B ON A.AUTONUM_MOTORISTA = B.AUTONUM ")
        SQL.Append("INNER JOIN ")
        SQL.Append("  OPERADOR.TB_AG_VEICULOS C ON A.AUTONUM_VEICULO = C.AUTONUM ")
        SQL.Append("WHERE A.AUTONUM_TRANSPORTADORA = " & CodigoTransportadora)

        Me.dgConsulta.DataSource = Banco.List(SQL.ToString())
        Me.dgConsulta.DataBind()

    End Sub

    Protected Sub dgConsulta_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles dgConsulta.RowCommand

        Dim Index As Integer = e.CommandArgument
        Dim ID As String = Me.dgConsulta.DataKeys(Index)("AUTONUM").ToString()

        If e.CommandName = "DEL" Then
            If Not String.IsNullOrEmpty(ID) Or
                Not String.IsNullOrWhiteSpace(ID) Then

                If Banco.ExecuteScalar("SELECT STATUS FROM REDEX.TB_AGENDAMENTO_WEB_CS WHERE AUTONUM = " & ID) = "GE" Then
                    If Banco.BeginTransaction("DELETE FROM REDEX.TB_AGENDAMENTO_WEB_CS WHERE AUTONUM = " & ID) Then
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
                If Banco.ExecuteScalar("SELECT STATUS FROM REDEX.TB_AGENDAMENTO_WEB_CS WHERE AUTONUM = " & ID) = "GE" Then
                    Response.Redirect("Agendar.aspx?id=" & ID)
                Else
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgAlerta", "alert('O Agendamento não pode ser excluído pois já foi impresso.');", True)
                End If
            End If
        End If

    End Sub

End Class