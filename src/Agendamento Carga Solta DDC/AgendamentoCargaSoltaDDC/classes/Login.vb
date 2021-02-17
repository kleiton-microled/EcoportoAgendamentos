Public Class Login

    Public Function Acesso(ByVal ID As String) As Object

        Dim Dt As New DataTable
        Dim TransportadoraOBJ As New Transportadora
        Dim UsuarioOBJ As New Usuario

        Dt = Banco.Consultar(String.Format("SELECT * FROM INTERNET.TB_INT_ACESSO WHERE TIAID={0}", ID))

        If dt.rows.count > 0 Then

            TransportadoraOBJ.Empresa = Dt.Rows(0)("TIAEMPRESA").ToString()
            TransportadoraOBJ.CNPJ = Dt.Rows(0)("TIACNPJ").ToString()
            UsuarioOBJ.UserId = Dt.Rows(0)("USRID").ToString()

            Dt = Banco.Consultar(String.Format("SELECT AUTONUM,RAZAO,CGC FROM OPERADOR.TB_CAD_TRANSPORTADORAS WHERE CGC='{0}'", TransportadoraOBJ.CNPJ))

            If Dt.Rows.Count > 0 Then

                TransportadoraOBJ.ID = Dt.Rows(0)("AUTONUM").ToString()
                TransportadoraOBJ.Razao = Dt.Rows(0)("RAZAO").ToString()
                TransportadoraOBJ.CNPJ = Dt.Rows(0)("CGC").ToString()
                TransportadoraOBJ.Usuario = UsuarioOBJ

                Return TransportadoraOBJ

            End If

        End If

        Return Nothing

    End Function

    Public Sub DeletarAcesso(ByVal ID As String)
        Banco.Executar(String.Format("DELETE FROM INTERNET.TB_INT_ACESSO WHERE TIAID={0}", ID))
    End Sub

End Class
