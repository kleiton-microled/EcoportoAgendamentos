Public Class Usuario

    Private _UserId As Integer

    Public Property UserId() As Integer
        Get
            Return _UserId
        End Get
        Set(value As Integer)
            _UserId = value
        End Set
    End Property

    Public Function ObterIdUsuario(ByVal Id As String) As Integer

        Dim SQL As New StringBuilder

        SQL.Append("SELECT ")
        SQL.Append("    USRID ")
        SQL.Append("FROM ")
        SQL.Append("    INTERNET.")
        SQL.Append("TB_INT_ACESSO ")
        SQL.Append("    WHERE TIAID = " & Id)

        Return Banco.ExecutaRetorna(String.Format(SQL.ToString()))

    End Function

End Class
