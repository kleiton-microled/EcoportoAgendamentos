Public Class Principal
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Dim SQL As String = String.Empty

        SQL = " DELETE FROM TB_AG_CS_NF WHERE AUTONUM_AGENDAMENTO IN ( "
        SQL = SQL & " SELECT AUTONUM FROM TB_AG_CS WHERE NVL(NUM_PROTOCOLO,0) = 0 AND NVL(ANO_PROTOCOLO,0) = 0 "
        SQL = SQL & " AND NVL(IMPRESSO,0) = 0 AND NVL(STATUS,0) = 0 AND NVL(AUTONUM_VEICULO,0) = 0 "
        SQL = SQL & " AND NVL(AUTONUM_MOTORISTA,0) = 0 AND NVL(AUTONUM_GD_RESERVA,0) =0 AND AUTONUM_USUARIO = " & Val(Session("SIS_USUARIO_LOGADO")) & ")"

        Banco.Executar(SQL)

        SQL = " DELETE FROM TB_AG_CS WHERE NVL(NUM_PROTOCOLO,0) = 0 AND NVL(ANO_PROTOCOLO,0) = 0 "
        SQL = SQL & " AND NVL(IMPRESSO,0) = 0 AND NVL(STATUS,0) = 0 AND NVL(AUTONUM_VEICULO,0) = 0 "
        SQL = SQL & " AND NVL(AUTONUM_MOTORISTA,0) = 0 AND NVL(AUTONUM_GD_RESERVA,0) =0 AND AUTONUM_USUARIO = " & Val(Session("SIS_USUARIO_LOGADO"))

        Banco.Executar(SQL)

    End Sub

End Class