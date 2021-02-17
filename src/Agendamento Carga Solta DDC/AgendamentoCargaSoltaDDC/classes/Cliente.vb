Public Class Cliente

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

    Public Function ConsultarClientesPorLote(ByVal Lote As String) As List(Of Cliente)

        Dim Dt As New DataTable
        Dim SQL As New StringBuilder
        Dim ListaClientes As New List(Of Cliente)

        SQL.Append("SELECT ")
            SQL.Append("    E.AUTONUM, ")
            SQL.Append("    E.RAZAO ")
            SQL.Append("FROM ")
            SQL.Append("    SGIPA.TB_BL A ")
            SQL.Append("LEFT JOIN ")
            SQL.Append("    SGIPA.TB_AV_ONLINE B ON A.AUTONUM = B.LOTE ")
            SQL.Append("LEFT JOIN ")
            SQL.Append("    SGIPA.TB_AV_ONLINE_TRANSP C ON B.AUTONUM = C.TB_AV_ONLINE ")
            SQL.Append("LEFT JOIN ")
            SQL.Append("    SGIPA.TB_TIPOS_DOCUMENTOS D ON A.TIPO_DOCUMENTO = D.CODE ")
            SQL.Append("LEFT JOIN ")
            SQL.Append("    SGIPA.TB_CAD_PARCEIROS E ON A.IMPORTADOR = E.AUTONUM ")
            SQL.Append("WHERE ")
            SQL.Append("    A.FLAG_ATIVO = 1 ")
            SQL.Append("AND ")
            SQL.Append("    A.ULTIMA_SAIDA IS NULL ")
            SQL.Append("AND ")
            SQL.Append("    A.AUTONUM = {0} ")
            SQL.Append("ORDER BY ")
            SQL.Append("    A.AUTONUM ")

        Dt = Banco.Consultar(String.Format(SQL.ToString(), Lote))

        For Each Item As DataRow In Dt.Rows
            Dim ClienteOBJ As New Cliente
            ClienteOBJ.ID = Item("AUTONUM").ToString()
            ClienteOBJ.Razao = Item("RAZAO").ToString()
            ListaClientes.Add(ClienteOBJ)
        Next

        Return ListaClientes

    End Function

End Class
