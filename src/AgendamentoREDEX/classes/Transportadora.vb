Imports System.Data.OleDb

Public Class Transportadora

    Private _ID As Integer
    Private _Razao As String
    Private _CNPJ As String
    Private _Empresa As String
    Private _Bitrem As Integer

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

    Public Property CNPJ() As String
        Get
            Return _CNPJ
        End Get
        Set(ByVal value As String)
            _CNPJ = value
        End Set
    End Property

    Public Property Empresa() As String
        Get
            Return _Empresa
        End Get
        Set(ByVal value As String)
            _Empresa = value
        End Set
    End Property

    Public Property Bitrem() As Integer
        Get
            Return _Bitrem
        End Get
        Set(value As Integer)
            _Bitrem = value
        End Set
    End Property

    Public Function VerificarTransporteFerroviario(ByVal ID As String) As Boolean

        Dim retorno As String = ""
        retorno = Banco.ExecuteScalar(String.Format("SELECT FLAG_FERROVIA FROM OPERADOR.TB_CAD_TRANSPORTADORAS WHERE AUTONUM = {0}", ID))

        If retorno = "0" Then
            Return False
        End If

        Return True

    End Function

    Public Function CarregarTransportadoras() As DataTable

        Dim SQL As New StringBuilder

        SQL.Append("SELECT ")
        SQL.Append("    AUTONUM, RAZAO, CGC ")
        SQL.Append("FROM ")
        SQL.Append("    OPERADOR.TB_CAD_TRANSPORTADORAS ")
        SQL.Append("ORDER BY RAZAO ")

        Return Banco.List(SQL.ToString())

    End Function

End Class
