Public Class Conteiner

    Private _Codigo As String
    Private _Tipo As String
    Private _Tamanho As String

    Public Property Codigo() As String
        Get
            Return _Codigo
        End Get
        Set(ByVal value As String)
            _Codigo = value
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

    Public Property Tamanho() As String
        Get
            Return _Tamanho
        End Get
        Set(ByVal value As String)
            _Tamanho = value
        End Set
    End Property

    Public Sub New()

    End Sub

    Public Sub New(ByVal Codigo As String, ByVal Tipo As String, ByVal Tamanho As String)
        Me.Codigo = Codigo
        Me.Tipo = Tipo
        Me.Tamanho = Tamanho
    End Sub

    Public Function ConsultarTipos() As List(Of String)

        Dim Rst As New ADODB.Recordset
        Dim Lista As New List(Of String)

        If Banco.BancoEmUso = "ORACLE" Then
            Rst.Open("SELECT CODIGO FROM OPERADOR.TB_CAD_TIPO_CONTEINER ORDER BY CODIGO", Banco.Conexao, 3, 3)
        Else
            Rst.Open("SELECT CODIGO FROM OPERADOR.DBO.TB_CAD_TIPO_CONTEINER ORDER BY CODIGO", Banco.Conexao, 3, 3)
        End If

        While Not Rst.EOF
            Lista.Add(Rst.Fields("CODIGO").Value.ToString())
            Rst.MoveNext()
        End While

        Return Lista

    End Function

    Public Function AgendamentoComRegistro(ByVal ID As Integer) As Integer

        Dim Rst As New ADODB.Recordset
        Dim Lista As New List(Of String)

        If Banco.BancoEmUso = "ORACLE" Then
            Rst.Open("SELECT C.DATA_ORDEM FROM SGIPA.TB_REGISTRO_SAIDA_CNTR A INNER JOIN SGIPA.TB_AMR_CNTR_BL B ON A.CNTR = B.CNTR INNER JOIN TB_ORDEM_CARREGAMENTO C ON A.ORDEM_CARREG = C.AUTONUM WHERE B.CNTR = " & ID & " ORDER BY C.DATA_ORDEM NULLS FIRST ", Banco.Conexao, 3, 3)
        End If

        If Not Rst.EOF Then
            If IsDate(Rst.Fields("DATA_ORDEM").Value.ToString()) Then
                Return 1
            End If
        End If

        Return 0

    End Function

End Class
