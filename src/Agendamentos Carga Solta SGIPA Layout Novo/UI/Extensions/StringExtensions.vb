Imports System.Data.SqlTypes
Imports System.Globalization
Imports System.Runtime.CompilerServices

Module StringExtensions
    <Extension()>
    Function ToInt(ByVal valor As String) As Integer
        Dim resultado As Integer = 0
        If Int32.TryParse(valor, resultado) Then Return resultado
        Return 0
    End Function

    <Extension()>
    Function ToIntOrNull(ByVal valor As String) As Integer?
        Dim resultado As Integer = 0
        If Int32.TryParse(valor, resultado) Then Return resultado
        Return Nothing
    End Function

    <Extension()>
    Function ToDecimal(ByVal valor As String) As Decimal
        Dim resultado As Decimal = 0
        If Decimal.TryParse(valor, resultado) Then Return resultado
        Return 0
    End Function
    <Extension()>
    Function ToDecimalEnUs(ByVal valor As String) As String
        Return valor.ToDecimal().ToString(CultureInfo.CreateSpecificCulture("en-Us"))
    End Function

    <Extension()>
    Function ToDouble(ByVal valor As String) As Double
        Dim resultado As Double = 0
        If Double.TryParse(valor, resultado) Then Return resultado
        Return 0
    End Function

    <Extension()>
    Function ToDateTime(ByVal valor As String) As DateTime
        Dim resultado As DateTime
        If DateTime.TryParse(valor, resultado) Then Return resultado
        Return SqlDateTime.MinValue.Value
    End Function

    <Extension()>
    Function ToNullDateTime(ByVal valor As String) As DateTime?
        Dim resultado As DateTime
        If DateTime.TryParse(valor, resultado) Then Return resultado
        Return Nothing
    End Function

    <Extension()>
    Function PPonto(ByVal valor As String) As String
        Dim valorFormatado = String.Format("{0:N4}", Convert.ToDecimal(valor))
        Return valorFormatado.Replace(".", "").Replace(",", ".")
    End Function

    <Extension()>
    Function RemoverCaracteresEspeciais(ByVal valor As String) As String
        If String.IsNullOrEmpty(valor) Then Return String.Empty
        valor = valor.Replace("&", "&amp;")
        valor = valor.Replace("<", "&lt;")
        valor = valor.Replace(">", "&gt;")
        Return valor
    End Function

    <Extension()>
    Function RemoverCaracteresEspeciaisAlert(ByVal valor As String) As String
        If String.IsNullOrEmpty(valor) Then Return String.Empty
        valor = valor.Replace("'", "")
        Return valor
    End Function

    <Extension()>
    Function QuebraDeLinhaXML(ByVal valor As String) As String
        Return Regex.Replace(valor, "\r\n?|\n", " ")
    End Function
End Module


