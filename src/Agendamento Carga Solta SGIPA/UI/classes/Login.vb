Public Class Login

    Public Function Acesso(ByVal ID As String) As Object

        Dim Rst As New ADODB.Recordset
        Dim TransportadoraOBJ As New Transportadora
        Dim UsuarioOBJ As New Usuario

        Rst.Open(String.Format("SELECT * FROM INTERNET.TB_INT_ACESSO WHERE TIAID={0}", ID), Banco.Conexao, 3, 3)

        If Not Rst.EOF Then

            TransportadoraOBJ.Empresa = Rst.Fields("TIAEMPRESA").Value.ToString()
            TransportadoraOBJ.CNPJ = Rst.Fields("TIACNPJ").Value.ToString()
            UsuarioOBJ.UserId = Rst.Fields("USRID").Value.ToString()

            Rst.Close()
            Rst.Open(String.Format("SELECT AUTONUM,RAZAO,CGC FROM OPERADOR.TB_CAD_TRANSPORTADORAS WHERE CGC='{0}'", TransportadoraOBJ.CNPJ), Banco.Conexao, 3, 3)

            If Not Rst.EOF Then

                TransportadoraOBJ.ID = Rst.Fields("AUTONUM").Value.ToString()
                TransportadoraOBJ.Razao = Rst.Fields("RAZAO").Value.ToString()
                TransportadoraOBJ.CNPJ = Rst.Fields("CGC").Value.ToString()

                Return TransportadoraOBJ

            End If

        End If

        Return Nothing

    End Function

    Public Sub DeletarAcesso(ByVal ID As String)

        Dim Rst As New ADODB.Recordset

        If Banco.BancoEmUso = "ORACLE" Then
            Rst.Open(String.Format("DELETE FROM INTERNET.TB_INT_ACESSO WHERE TIAID={0}", ID), Banco.Conexao, 3, 3)
        Else
            Rst.Open(String.Format("DELETE FROM INTERNET.TB_INT_ACESSO WHERE TIAID={0}", ID), Banco.Conexao, 3, 3)
        End If

    End Sub

End Class
