Public Class DadosDanfe

    Private _Valor As String
    Public Property Valor() As String
        Get
            Return _Valor
        End Get
        Set(ByVal value As String)
            _Valor = value
        End Set
    End Property

    Private _Quantidade As String
    Public Property Quantidade() As String
        Get
            Return _Quantidade
        End Get
        Set(ByVal value As String)
            _Quantidade = value
        End Set
    End Property

    Private _Emissao As DateTime
    Public Property Emissao() As DateTime
        Get
            Return _Emissao
        End Get
        Set(ByVal value As DateTime)
            _Emissao = value
        End Set
    End Property

    Private _Emissor As String
    Public Property Emissor() As String
        Get
            Return _Emissor
        End Get
        Set(ByVal value As String)
            _Emissor = value
        End Set
    End Property

    Private _Serie As String
    Public Property Serie() As String
        Get
            Return _Serie
        End Get
        Set(ByVal value As String)
            _Serie = value
        End Set
    End Property

    Private _Numero As String
    Public Property Numero() As String
        Get
            Return _Numero
        End Get
        Set(ByVal value As String)
            _Numero = value
        End Set
    End Property

    Private _Danfe As String
    Public Property Danfe() As String
        Get
            Return _Danfe
        End Get
        Set(ByVal value As String)
            _Danfe = value
        End Set
    End Property

    Private _Xml As String
    Public Property Xml() As String
        Get
            Return _Xml
        End Get
        Set(ByVal value As String)
            _Xml = value
        End Set
    End Property

End Class
