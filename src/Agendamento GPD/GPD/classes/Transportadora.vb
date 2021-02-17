Imports System.Data.OleDb

Public Class Transportadora

    Private _ID As Integer
    Private _Razao As String
    Private _CNPJ As String
    Private _Empresa As String

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

    Public Function VerificarTransporteFerroviario(ByVal ID As String) As Boolean

        Dim Rst As New ADODB.Recordset

        If Banco.BancoEmUso = "ORACLE" Then
            Rst.Open(String.Format("SELECT FLAG_FERROVIA FROM OPERADOR.TB_CAD_TRANSPORTADORAS WHERE AUTONUM = {0}", ID), Banco.Conexao, 3, 3)
        Else
            Rst.Open(String.Format("SELECT FLAG_FERROVIA FROM OPERADOR.DBO.TB_CAD_TRANSPORTADORAS WHERE AUTONUM = {0}", ID), Banco.Conexao, 3, 3)
        End If

        If Not Rst.EOF Then
            If Rst.Fields("FLAG_FERROVIA").Value.ToString() = "0" Then
                Return False
            End If
        End If

        Return True

    End Function

    Public Function CarregarTransportadoras() As DataTable

        Dim Rst As New ADODB.Recordset
        Dim SQL As New StringBuilder

        SQL.Append("SELECT ")
        SQL.Append("    AUTONUM, RAZAO, CGC ")
        SQL.Append("FROM ")
        If Banco.BancoEmUso = "ORACLE" Then
            SQL.Append("    OPERADOR.TB_CAD_TRANSPORTADORAS ")
        Else
            SQL.Append("    OPERADOR.DBO.TB_CAD_TRANSPORTADORAS ")
        End If
        SQL.Append("ORDER BY RAZAO ")

        Rst.Open(SQL.ToString(), Banco.Conexao, 3, 3)

        Using Adaptador As New OleDbDataAdapter
            Dim Ds As New DataSet
            Adaptador.Fill(Ds, Rst, "TB_CAD_TRANPORTADORAS ")
            Return Ds.Tables(0)
        End Using

    End Function

    Public Shared Function ObterEmailAgendamento(ByVal ID As Integer) As String

        Dim Rst As New ADODB.Recordset
        Rst.Open("SELECT EMAIL_AGENDAMENTO FROM OPERADOR.TB_CAD_TRANSPORTADORAS WHERE AUTONUM = " & ID, Banco.Conexao, 3, 3)

        If Not Rst.EOF Then
            If Not Rst.EOF Then
                Return Rst.Fields("EMAIL_AGENDAMENTO").Value.ToString()
            End If
        End If

    End Function

End Class
