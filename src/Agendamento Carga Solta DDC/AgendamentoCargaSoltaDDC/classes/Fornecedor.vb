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

        Dim Dt As New DataTable
        Dim ListaFornecedores As New List(Of Fornecedor)

        Dt = Banco.Consultar("SELECT AUTONUM,RAZAO FROM SGIPA.TB_CAD_PARCEIROS WHERE FLAG_FORNECEDOR=1")

        If Dt.Rows.Count > 0 Then

            For Each Item As DataRow In Dt.Rows
                Dim FornecedorOBJ As New Fornecedor
                FornecedorOBJ.ID = Item("AUTONUM").ToString()
                FornecedorOBJ.Razao = Item("RAZAO").ToString()
                ListaFornecedores.Add(FornecedorOBJ)
            Next

            Return ListaFornecedores

        End If

        Return Nothing

    End Function

End Class
