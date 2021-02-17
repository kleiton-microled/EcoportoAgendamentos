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
        Dim Rst As New ADODB.Recordset

        SQL.Append("SELECT ")
        SQL.Append("    USRID ")
        SQL.Append("FROM ")
        SQL.Append("    INTERNET.")
        If Banco.BancoEmUso <> "ORACLE" Then
            SQL.Append("DBO.")
        End If
        SQL.Append("TB_INT_ACESSO ")
        SQL.Append("    WHERE TIAID = " & Id)

        Rst.Open(String.Format(SQL.ToString()), Banco.Conexao, 3, 3)

        If Not Rst.EOF Then
            UserId = Convert.ToInt32(Rst.Fields("USRID").Value.ToString())
        Else
            UserId = 0
        End If

        Return UserId

    End Function
End Class
