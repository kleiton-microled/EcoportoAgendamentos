Module Geral

    Public Function Nnull(ByVal Par As Object, ByVal Tipo As Integer, Optional ByVal RemoveEspaços As Boolean = True) As Object

        On Error Resume Next
        Dim sVAR As Object

        If IsNull(Par) = True Then
            Return IIf(Tipo = 0, 0, String.Empty)
        Else
            Return IIf(Trim(Par) <> String.Empty, IIf(Tipo = 0, Par, IIf(RemoveEspaços = True, Trim(Par), Par)), IIf(Tipo = 0, 0, String.Empty))
        End If

        If Nnull = "00:00:00" Then
            Return String.Empty
        End If

        sVAR = String.Empty

    End Function

    Public Function IsNull(ByVal Valor As String) As Boolean

        If String.IsNullOrEmpty(Valor.Trim) Then
            Return True
        End If

        Return False

    End Function

    Public Function PPonto(ByVal Valor As String) As String
        Return Replace(Replace(Nnull(Valor, 0), ".", ""), ",", ".")
    End Function

End Module
