<Serializable()> _
Public Class NotaFiscalItem

    Private _AUTONUM As String
    Private _ITEM As String
    Private _AUTONUM_AGENDAMENTO As String
    Private _AUTONUM_NF As String
    Private _LOTE As String
    Private _QTDE As String
    Private _PRODUTO As String
    Private _EMBALAGEM As String
    Private _AUTONUM_CS As String

    Public Property AUTONUM() As String
        Get
            Return Me._AUTONUM
        End Get
        Set(ByVal value As String)
            Me._AUTONUM = value
        End Set
    End Property

    Public Property ITEM() As String
        Get
            Return Me._ITEM
        End Get
        Set(ByVal value As String)
            Me._ITEM = value
        End Set
    End Property

    Public Property AUTONUM_AGENDAMENTO() As String
        Get
            Return Me._AUTONUM_AGENDAMENTO
        End Get
        Set(ByVal value As String)
            Me._AUTONUM_AGENDAMENTO = value
        End Set
    End Property

    Public Property AUTONUM_NF() As String
        Get
            Return Me._AUTONUM_NF
        End Get
        Set(ByVal value As String)
            Me._AUTONUM_NF = value
        End Set
    End Property

    Public Property LOTE() As String
        Get
            Return Me._LOTE
        End Get
        Set(ByVal value As String)
            Me._LOTE = value
        End Set
    End Property

    Public Property QTDE() As String
        Get
            Return Me._QTDE
        End Get
        Set(ByVal value As String)
            Me._QTDE = value
        End Set
    End Property

    Public Property PRODUTO() As String
        Get
            Return Me._PRODUTO
        End Get
        Set(ByVal value As String)
            Me._PRODUTO = value
        End Set
    End Property

    Public Property EMBALAGEM() As String
        Get
            Return Me._EMBALAGEM
        End Get
        Set(ByVal value As String)
            Me._EMBALAGEM = value
        End Set
    End Property

    Public Property AUTONUM_CS() As String
        Get
            Return Me._AUTONUM_CS
        End Get
        Set(ByVal value As String)
            Me._AUTONUM_CS = value
        End Set
    End Property

    Public Sub New(ByVal Autonum As String, ByVal Item As String, ByVal Autonum_Agendamento As String, ByVal Autonum_NF As String, ByVal Lote As String, ByVal Qtd As String, ByVal Autonum_Cs As String, ByVal Produto As String, ByVal Embalagem As String)

        Me.AUTONUM = Autonum
        Me.ITEM = Item
        Me.AUTONUM_AGENDAMENTO = Autonum_Agendamento
        Me.AUTONUM_NF = Autonum_NF
        Me.LOTE = Lote
        Me.QTDE = Qtd
        Me.AUTONUM_CS = Autonum_Cs
        Me.PRODUTO = Produto
        Me.EMBALAGEM = Embalagem

    End Sub

    Public Sub New()

    End Sub

End Class
