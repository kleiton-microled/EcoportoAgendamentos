Public Class Fornecedor

    Private _ID As Integer
    Private _Razao As String

    Public Property ID() As Integer
        Get
            Return _ID
        End Get
        Set(ByVal value As Integer)
            _ID = value
        End Set
    End Property

    Public Property Razao() As String
        Get
            Return _Razao
        End Get
        Set(ByVal value As String)
            _Razao = value
        End Set
    End Property


    Public Function ConsultarFornecedores() As List(Of Fornecedor)

        Dim Rst As New ADODB.Recordset
        Dim ListaFornecedores As New List(Of Fornecedor)

        If Banco.BancoEmUso = "ORACLE" Then
            Rst.Open("SELECT AUTONUM,RAZAO FROM SGIPA.TB_CAD_PARCEIROS WHERE FLAG_FORNECEDOR=1", Banco.Conexao, 3, 3)
        Else
            Rst.Open("SELECT AUTONUM,RAZAO FROM SGIPA.DBO.TB_CAD_PARCEIROS WHERE FLAG_FORNECEDOR=1", Banco.Conexao, 3, 3)
        End If

        If Not Rst.EOF Then

            While Not Rst.EOF
                Dim FornecedorOBJ As New Fornecedor
                FornecedorOBJ.ID = Rst.Fields("AUTONUM").Value.ToString()
                FornecedorOBJ.Razao = Rst.Fields("RAZAO").Value.ToString()
                ListaFornecedores.Add(FornecedorOBJ)
                Rst.MoveNext()
            End While

            Return ListaFornecedores

        End If

        Return Nothing

    End Function

End Class
