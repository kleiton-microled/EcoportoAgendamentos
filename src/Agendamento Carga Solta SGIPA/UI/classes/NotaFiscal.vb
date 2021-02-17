Public Class NotaFiscal

    Private _Autonum As String
    Private _Lote As String
    Private _NotaFiscal As String
    Private _Serie As String
    Private _Emissao As String
    Private _Tipo As String
    Private _Patio As String
    Private _AutonumAgendamento As String

    Public Property Autonum() As String
        Get
            Return _Autonum
        End Get
        Set(ByVal value As String)
            _Autonum = value
        End Set
    End Property

    Public Property NotaFiscal() As String
        Get
            Return _NotaFiscal
        End Get
        Set(ByVal value As String)
            _NotaFiscal = value
        End Set
    End Property

    Public Property Serie() As String
        Get
            Return _Serie
        End Get
        Set(ByVal value As String)
            _Serie = value
        End Set
    End Property

    Public Property Emissao() As String
        Get
            Return _Emissao
        End Get
        Set(ByVal value As String)
            _Emissao = value
        End Set
    End Property

    Public Property Tipo() As String
        Get
            Return _Tipo
        End Get
        Set(ByVal value As String)
            _Tipo = value
        End Set
    End Property

    Public Property Lote() As String
        Get
            Return _Lote
        End Get
        Set(ByVal value As String)
            _Lote = value
        End Set
    End Property

    Public Property Patio() As String
        Get
            Return _Patio
        End Get
        Set(ByVal value As String)
            _Patio = value
        End Set
    End Property

    Public Property AutonumAgendamento() As String
        Get
            Return _AutonumAgendamento
        End Get
        Set(ByVal value As String)
            _AutonumAgendamento = value
        End Set
    End Property

    Public Sub New()

    End Sub

    Public Sub New(ByVal Autonum As String, ByVal Lote As String, ByVal NotaFiscal As String, ByVal Serie As String, ByVal Emissao As String, ByVal Tipo As String, ByVal Patio As String, ByVal AutonumAgendamento As String)
        Me.Autonum = Autonum
        Me.Lote = Lote
        Me.NotaFiscal = NotaFiscal
        Me.Serie = Serie
        Me.Emissao = Emissao
        Me.Tipo = Tipo
        Me.Patio = Patio
        Me.AutonumAgendamento = AutonumAgendamento
    End Sub

    Public Function ConsultarCFOP() As List(Of String)

        Dim Rst As New ADODB.Recordset
        Dim ListaCFOP As New List(Of String)

        If Banco.BancoEmUso = "ORACLE" Then
            Rst.Open("SELECT CODIGO FROM SGIPA.TB_CAD_CFOP WHERE FLAG_REDEX=1 ORDER BY CODIGO ASC", Banco.Conexao, 3, 3)
        Else
            Rst.Open("SELECT CODIGO FROM SGIPA.DBO.TB_CAD_CFOP WHERE FLAG_REDEX=1 ORDER BY CODIGO ASC", Banco.Conexao, 3, 3)
        End If

        If Not Rst.EOF Then

            While Not Rst.EOF
                ListaCFOP.Add(Rst.Fields("CODIGO").Value.ToString())
                Rst.MoveNext()
            End While

            Return ListaCFOP

        End If

        Return Nothing

    End Function

End Class
