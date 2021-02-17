Public Class Empresa
    Private _RazaoSocial As String
    Private _Fantasia As String

    Public Property RazaoSocial As String
        Get
            Return _RazaoSocial
        End Get
        Set(ByVal value As String)
            _RazaoSocial = value
        End Set
    End Property

    Public Property Fantasia As String
        Get
            Return _Fantasia
        End Get
        Set(ByVal value As String)
            _Fantasia = value
        End Set
    End Property

    Public Function ObterNomeFantasia(ByVal TiaEmpresa As Integer) As String

        Dim SQL As New StringBuilder

        SQL.Append("SELECT ")
        SQL.Append("    NOME_FANTASIA ")
        SQL.Append("FROM ")
        SQL.Append("    SGIPA.TB_EMPRESAS E ")
        SQL.Append("INNER JOIN ")
        SQL.Append("    INTERNET.TB_INT_ACESSO A ")
        SQL.Append("ON E.AUTONUM = A.TIAEMPRESA ")
        SQL.Append("    AND ")
        SQL.Append("    A.TIAID = {0}")

        Return Banco.ExecutaRetorna(String.Format(SQL.ToString(), TiaEmpresa))

    End Function
    ''' <summary>
    ''' Obtém o nome do diretório com CSS relacionado ao nome fantasia da empresa a partir do código desta
    ''' </summary>
    ''' <param name="TiaEmpresa">Autonum (TIAID) da Empresa</param>
    ''' <returns>Nome do diretório com CSS</returns>
    ''' <remarks></remarks>
    Public Function ObterNomeFantasiaDiret(ByVal TiaEmpresa As Integer) As String

        Dim SQL As New StringBuilder

        SQL.Append("SELECT ")
        SQL.Append("    NOME_FANTASIA ")
        SQL.Append("FROM ")
        SQL.Append("    SGIPA.TB_EMPRESAS E ")
        SQL.Append("WHERE ")
        SQL.Append("    AUTONUM = {0}")

        Return Banco.ExecutaRetorna(String.Format(SQL.ToString(), TiaEmpresa))

    End Function

End Class
