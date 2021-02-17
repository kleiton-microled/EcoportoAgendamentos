Public Class ArquivoUpload

    Private _Id As Integer
    Private _NomeArquivo As String

    Public Property Id() As Integer
        Get
            Return _Id
        End Get
        Set(ByVal value As Integer)
            _Id = value
        End Set
    End Property

    Public Property NomeArquivo() As String
        Get
            Return _NomeArquivo
        End Get
        Set(ByVal value As String)
            _NomeArquivo = value
        End Set
    End Property

End Class
