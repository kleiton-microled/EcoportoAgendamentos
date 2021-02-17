<Serializable()> _
Public Class NotaFiscal

    Private _ID As String
    Private _ID_Conteiner As String
    Private _Lote As String
    Private _NotaFiscal As String
    Private _Serie As String
    Private _Emissao As String
    Private _Conteiner As String

    Public Property ID() As String
        Get
            Return _ID
        End Get
        Set(ByVal value As String)
            _ID = value
        End Set
    End Property

    Public Property ID_Conteiner() As String
        Get
            Return _ID_Conteiner
        End Get
        Set(ByVal value As String)
            _ID_Conteiner = value
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
            Return FormatDateTime(_Emissao, DateFormat.ShortDate)
        End Get
        Set(ByVal value As String)
            _Emissao = value
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

    Public Property Conteiner() As String
        Get
            Return _Conteiner
        End Get
        Set(ByVal value As String)
            _Conteiner = value
        End Set
    End Property

End Class
