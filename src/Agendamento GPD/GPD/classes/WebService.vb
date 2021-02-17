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

    Public Function ValidarMotorista(ByVal CPF As String, ByVal CNPJ As String, ByVal Autonomo As Integer) As Boolean

        Try
            Using Consulta As New WsBDCC_Ecoporto.WsSincronoSoapClient
                CodigoRetorno = Consulta.ConsultaCpf(CPF, 0, CNPJ, Autonomo).CodigoRetorno
                DescricaoRetorno = Consulta.ConsultaCpf(CPF, 0, CNPJ, Autonomo).DescricaoRetorno
                Return True
            End Using
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try

        Return False

    End Function

    Public Function ValidarVeiculo(ByVal Renavam As String) As Boolean

        Try
            Using Consulta As New WsBDCC_Ecoporto.WsSincronoSoapClient
                CodigoRetorno = Consulta.ConsultaRenavam(Renavam, 0).CodigoRetorno
                DescricaoRetorno = Consulta.ConsultaRenavam(Renavam, 0).DescricaoRetorno
                Return True
            End Using
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try

        Return False

    End Function

    Public Function ValidarBDCC() As Boolean

        Dim Rst As New ADODB.Recordset

        If Banco.BancoEmUso = "ORACLE" Then
            Rst.Open(String.Format("SELECT TIPO_RETORNO FROM SGIPA.TB_BDCC_RETORNO WHERE COD_RETORNO='{0}'", CodigoRetorno), Banco.ConexaoBDCC, 3, 3)
        Else
            Rst.Open(String.Format("SELECT TIPO_RETORNO FROM SGIPA.DBO.TB_BDCC_RETORNO WHERE COD_RETORNO='{0}'", CodigoRetorno), Banco.ConexaoBDCC, 3, 3)
        End If

        If Not Rst.EOF Then
            TipoBDCC = Rst.Fields("TIPO_RETORNO").Value.ToString()
        End If

        Return True

    End Function

End Class
