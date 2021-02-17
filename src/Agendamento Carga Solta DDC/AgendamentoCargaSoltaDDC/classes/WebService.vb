Public Class WebService

    Private _CodigoRetorno As String
    Private _DescricaoRetorno As String
    Private _TipoBDCC As String

    Public Property CodigoRetorno() As String
        Get
            Return _CodigoRetorno
        End Get
        Set(ByVal value As String)
            _CodigoRetorno = value
        End Set
    End Property

    Public Property DescricaoRetorno() As String
        Get
            Return _DescricaoRetorno
        End Get
        Set(ByVal value As String)
            _DescricaoRetorno = value
        End Set
    End Property

    Public Property TipoBDCC() As String
        Get
            Return _TipoBDCC
        End Get
        Set(ByVal value As String)
            _TipoBDCC = value
        End Set
    End Property

    Public Function ValidarMotorista(ByVal CPF As String, ByVal Cnpj As String, ByVal Autonomo As String) As Boolean

        Try
            Using Consulta As New WsBDCC.WsSincronoSoapClient
                CodigoRetorno = Consulta.ConsultaCpf(CPF, False, Cnpj, Autonomo).CodigoRetorno
                DescricaoRetorno = Consulta.ConsultaCpf(CPF, False, Cnpj, Autonomo).DescricaoRetorno
                Return True
            End Using
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try

        Return False

    End Function

    Public Function ValidarVeiculo(ByVal Renavam As String) As Boolean

        Try
            Using Consulta As New WsBDCC.WsSincronoSoapClient
                CodigoRetorno = Consulta.ConsultaRenavam(Renavam, False).CodigoRetorno
                DescricaoRetorno = Consulta.ConsultaRenavam(Renavam, False).DescricaoRetorno
                Return True
            End Using
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try

        Return False

    End Function

    Public Function ValidarBDCC() As String
        Return Banco.ExecutaRetorna(String.Format("SELECT TIPO_RETORNO FROM SGIPA.TB_BDCC_RETORNO WHERE COD_RETORNO='{0}'", CodigoRetorno))
    End Function

End Class
