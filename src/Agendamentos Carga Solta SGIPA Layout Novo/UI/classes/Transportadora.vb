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
