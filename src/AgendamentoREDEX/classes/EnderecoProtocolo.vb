Public Class EnderecoProtocolo

    Public Shared Function ObterEnderecoProtocolo(ByVal Patio As Integer) As String

        Return Banco.ExecuteScalar("select 'PÁTIO ' || PATIO || ' - ' || NOME_FANTASIA || ' - ' || LOGRADOURO || ' - ' || BAIRRO || ' - ' || CIDADE  || '/' || ESTADO AS ENDERECO FROM REDEX.TB_EMPRESAS WHERE PATIO = " & Patio)

    End Function
End Class
