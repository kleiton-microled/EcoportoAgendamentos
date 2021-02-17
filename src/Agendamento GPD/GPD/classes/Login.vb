Public Class Login

    Dim Usuario As New Usuario

    Public Function EfetuarLogin(ByVal ID As String) As Usuario

        Dim Rst As New ADODB.Recordset

        If Banco.BancoEmUso = "ORACLE" Then
            Rst.Open(String.Format("SELECT * FROM INTERNET.TB_INT_ACESSO WHERE TIAID={0}", ID), Banco.Conexao, 3, 3)
        Else
            Rst.Open(String.Format("SELECT * FROM INTERNET.DBO.TB_INT_ACESSO WHERE TIAID={0}", ID), Banco.Conexao, 3, 3)
        End If

        If Not Rst.EOF Then

            Usuario.CNPJ = Rst.Fields("TIACNPJ").Value.ToString()
            Usuario.USRID = Rst.Fields("USRID").Value.ToString()
            Usuario.Empresa = Rst.Fields("TIAEMPRESA").Value.ToString()

            Rst.Close()

            If Banco.BancoEmUso = "ORACLE" Then 'Ecoporto - Pode acessar tudo
                Rst.Open(String.Format("SELECT AUTONUM, RAZAO_SOCIAL, CNPJ FROM SGIPA.TB_EMPRESAS WHERE CNPJ = '{0}'", Usuario.CNPJ), Banco.Conexao, 3, 3)
            Else
                Rst.Open(String.Format("SELECT AUTONUM, RAZAO_SOCIAL, CNPJ FROM SGIPA.DBO.TB_EMPRESAS WHERE CNPJ = '{0}'", Usuario.CNPJ), Banco.Conexao, 3, 3)
            End If

            If Not Rst.EOF Then
                'Dim Rec As New Recinto
                Usuario.Codigo = Rst.Fields("AUTONUM").Value.ToString()
                Usuario.Nome = Rst.Fields("RAZAO_SOCIAL").Value.ToString()
                Usuario.CNPJ = Rst.Fields("CNPJ").Value.ToString()
                Usuario.Tipo = "E" 'Empresa ou Ecoporto
                Rst.Close()
                'Usuario.CodRecinto = Rec.ObterCodRecinto(Usuario.CNPJ)
                Return Usuario

            Else
                Rst.Close()
                Usuario.Tipo = String.Empty

                If Banco.BancoEmUso = "ORACLE" Then 'Transportadora
                    Rst.Open(String.Format("SELECT AUTONUM,RAZAO,CGC FROM OPERADOR.TB_CAD_TRANSPORTADORAS WHERE CGC='{0}'", Usuario.CNPJ), Banco.Conexao, 3, 3)
                Else
                    Rst.Open(String.Format("SELECT AUTONUM,RAZAO,CGC FROM OPERADOR.DBO.TB_CAD_TRANSPORTADORAS WHERE CGC='{0}'", Usuario.CNPJ), Banco.Conexao, 3, 3)
                End If

                If Not Rst.EOF Then
                    Usuario.Codigo = Rst.Fields("AUTONUM").Value.ToString()
                    Usuario.Nome = Rst.Fields("RAZAO").Value.ToString()
                    Usuario.CNPJ = Rst.Fields("CGC").Value.ToString()
                    Usuario.Tipo = "T" 'Transportadora
                End If

                Rst.Close()

                Return Usuario

            End If

        End If

        Return Nothing

    End Function

    Public Sub DeletarAcesso(ByVal ID As String)

        Dim Rst As New ADODB.Recordset
        Rst.Open(String.Format("DELETE FROM INTERNET.TB_INT_ACESSO WHERE TIAID={0}", ID), Banco.Conexao, 3, 3)

    End Sub


End Class
