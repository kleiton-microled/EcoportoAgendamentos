Imports System.Data.OleDb

Public Class Viagem

    Private _Autonumviagem As Integer
    Private _Navio As String
    Private _CodRecinto As Integer
    Private _dtInicio As String
    Private _dtFim As String

    Public Property Autonumviagem() As Integer
        Get
            Return _Autonumviagem
        End Get
        Set(value As Integer)
            _Autonumviagem = value
        End Set
    End Property

    Public Property Navio() As String
        Get
            Return _Navio
        End Get
        Set(value As String)
            _Navio = value
        End Set
    End Property

    Public Property CodRecinto() As Integer
        Get
            Return _CodRecinto
        End Get
        Set(value As Integer)
            _CodRecinto = value
        End Set
    End Property

    Public Property dtInicio() As String
        Get
            Return _dtInicio
        End Get
        Set(value As String)
            _dtInicio = value
        End Set
    End Property

    Public Property dtFim() As String
        Get
            Return _dtFim
        End Get
        Set(value As String)
            _dtFim = value
        End Set
    End Property

    Public Function CarregarViagens(ByVal Transportadora As String) As DataTable
        Dim SQL As New StringBuilder
        Dim Conexao As New OleDbConnection
        Dim Adaptador As OleDbDataAdapter
        Dim DsV As New DataSet
        'Dim DtV As New DataTable
        'Dim DtVRow As DataRow

        SQL.Append("SELECT ")
        SQL.Append("    DISTINCT ")
        SQL.Append("   AUTONUMVIAGEM, NAVIO_VIAGEM ")
        SQL.Append("FROM ")
        If Banco.BancoEmUso = "ORACLE" Then
            SQL.Append("    OPERADOR.VW_AG_RETIRADA_GMCI ")
        Else
            SQL.Append("    OPERADOR.DBO.VW_AG_RETIRADA_GMCI ")
        End If
        SQL.Append("WHERE ")
        SQL.Append("    TRANSPORTADORA = " & Transportadora & " ")
        SQL.Append("ORDER BY NAVIO_VIAGEM ")

        Conexao.ConnectionString = Banco.StringConexao(True)
        Conexao.Open()

        Adaptador = New OleDbDataAdapter(SQL.ToString(), Conexao)
        Adaptador.Fill(DsV)
        Conexao.Close()

        'DtV.Columns.Add("AUTONUMVIAGEM")
        'DtV.Columns.Add("NAVIO_VIAGEM")
        'For Linha = 0 To DsV.Tables(0).Rows.Count - 1
        'If Linha = 0 OrElse DsV.Tables(0).Rows(Linha)("AUTONUMVIAGEM").ToString() <> DsV.Tables(0).Rows(Linha - 1)("AUTONUMVIAGEM").ToString() Then
        'DtVRow = DtV.NewRow()
        'DtVRow("AUTONUMVIAGEM") = DsV.Tables(0).Rows(Linha)("AUTONUMVIAGEM").ToString()
        'DtVRow("NAVIO_VIAGEM") = DsV.Tables(0).Rows(Linha)("NAVIO_VIAGEM").ToString()
        'DtV.Rows.Add(DtVRow)
        'End If
        'Next

        Return DsV.Tables(0)

    End Function

End Class
