'------------------------------------------------------------------------------------------
' Copyright © 2006 Agrinei Sousa [www.agrinei.com]
'
' Esse código fonte é fornecido sem garantia de qualquer tipo.
' Sinta-se livre para utilizá-lo, modificá-lo e distribuí-lo,
' inclusive em aplicações comerciais.
' É altamente desejável que essa mensagem não seja removida.
'------------------------------------------------------------------------------------------

Imports System.Web.UI
Imports System.Web.UI.WebControls


Public Enum SummaryOperation
    Sum
    Avg
    Count
    [Custom]
End Enum
Public Delegate Sub CustomSummaryOperation(ByVal column As String, ByVal groupName As String, ByVal value As Object)
Public Delegate Function SummaryResultMethod(ByVal column As String, ByVal groupName As String) As Object

''' <summary>
''' A class that represents a summary operation defined to a column
''' </summary>
Public Class GridViewSummary
    Public Column As String
    Public Operation As SummaryOperation
    Public CustomOperation As CustomSummaryOperation
    Public GetSummaryMethod As SummaryResultMethod
    Public Group As GridViewGroup
    Public Value As Object
    Public FormatString As String
    Public Quantity As Integer
    Public Automatic As Boolean
    Public TreatNullAsZero As Boolean


    Private Sub New(ByVal col As String, ByVal grp As GridViewGroup)
        Me.Column = col
        Me.Group = grp
        Me.Value = Nothing
        Me.FormatString = [String].Empty
        Me.Quantity = 0
        Me.Automatic = True
        Me.TreatNullAsZero = False
    End Sub

    Public Sub New(ByVal col As String, ByVal op As SummaryOperation, ByVal grp As GridViewGroup)
        Me.New(col, grp)
        Me.Operation = op
        Me.CustomOperation = Nothing
        Me.GetSummaryMethod = Nothing
    End Sub

    Public Sub New(ByVal col As String, ByVal op As CustomSummaryOperation, ByVal getResult As SummaryResultMethod, ByVal grp As GridViewGroup)
        Me.New(col, grp)
        Me.Operation = SummaryOperation.[Custom]
        Me.CustomOperation = op
        Me.GetSummaryMethod = getResult
    End Sub

    Public Function Validate() As Boolean
        If Me.Operation = SummaryOperation.[Custom] Then
            Return (Me.CustomOperation IsNot Nothing AndAlso Me.GetSummaryMethod IsNot Nothing)
        Else
            Return (Me.CustomOperation Is Nothing AndAlso Me.GetSummaryMethod Is Nothing)
        End If
    End Function

    Public Sub Reset()
        Me.Quantity = 0
        Me.Value = Nothing
    End Sub

    Public Sub AddValue(ByVal newValue As Object)
        ' Increment to (later) calc the Avg or for other calcs
        Me.Quantity += 1

        ' Built-in operations
        If Me.Operation = SummaryOperation.Sum OrElse Me.Operation = SummaryOperation.Avg Then
            If Me.Value Is Nothing Then
                Me.Value = newValue
            Else
                Me.Value = PerformSum(Me.Value, newValue)
            End If
        Else
            ' Custom operation
            If Me.CustomOperation IsNot Nothing Then
                ' Call the custom operation
                If Me.Group IsNot Nothing Then
                    Me.CustomOperation(Me.Column, Me.Group.Name, newValue)
                Else
                    Me.CustomOperation(Me.Column, Nothing, newValue)
                End If
            End If
        End If
    End Sub

    Public Sub Calculate()
        If Me.Operation = SummaryOperation.Avg Then
            Me.Value = PerformDiv(Me.Value, Me.Quantity)
        End If
        If Me.Operation = SummaryOperation.Count Then
            Me.Value = Me.Quantity
        ElseIf Me.Operation = SummaryOperation.[Custom] Then
            If Me.GetSummaryMethod IsNot Nothing Then
                Me.Value = Me.GetSummaryMethod(Me.Column, Nothing)
            End If
        End If
        ' if this.Operation == SummaryOperation.Avg
        ' this.Value already contains the correct value
    End Sub

#Region "Built-in Summary Operations"

    Private Function PerformSum(ByVal a As Object, ByVal b As Object) As Object
        Dim zero As Object = 0

        If a Is Nothing Then
            If TreatNullAsZero Then
                a = 0
            Else
                Return Nothing
            End If
        End If

        If b Is Nothing Then
            If TreatNullAsZero Then
                b = 0
            Else
                Return Nothing
            End If
        End If

        ' Convert to proper type before add
        Select Case a.[GetType]().FullName
            Case "System.Int16"
                Return Convert.ToInt16(a) + Convert.ToInt16(b)
            Case "System.Int32"
                Return Convert.ToInt32(a) + Convert.ToInt32(b)
            Case "System.Int64"
                Return Convert.ToInt64(a) + Convert.ToInt64(b)
            Case "System.UInt16"
                Return Convert.ToUInt16(a) + Convert.ToUInt16(b)
            Case "System.UInt32"
                Return Convert.ToUInt32(a) + Convert.ToUInt32(b)
            Case "System.UInt64"
                Return Convert.ToUInt64(a) + Convert.ToUInt64(b)
            Case "System.Single"
                Return Convert.ToSingle(a) + Convert.ToSingle(b)
            Case "System.Double"
                Return Convert.ToDouble(a) + Convert.ToDouble(b)
            Case "System.Decimal"
                Return Convert.ToDecimal(a) + Convert.ToDecimal(b)
            Case "System.Byte"
                Return Convert.ToByte(a) + Convert.ToByte(b)
            Case "System.String"
                Return a.ToString() & b.ToString()
        End Select

        Return Nothing
    End Function

    Private Function PerformDiv(ByVal a As Object, ByVal b As Integer) As Object
        Dim zero As Object = 0

        If a Is Nothing Then
            Return (If(TreatNullAsZero, zero, Nothing))
        End If

        ' Don't raise an exception, just return null
        If b = 0 Then
            Return Nothing
        End If

        ' Convert to proper type before div
        Select Case a.[GetType]().FullName
            Case "System.Int16"
                Return Convert.ToInt16(a) \ b
            Case "System.Int32"
                Return Convert.ToInt32(a) \ b
            Case "System.Int64"
                Return Convert.ToInt64(a) \ b
            Case "System.UInt16"
                Return Convert.ToUInt16(a) \ b
            Case "System.UInt32"
                Return Convert.ToUInt32(a) \ b
            Case "System.Single"
                Return Convert.ToSingle(a) / b
            Case "System.Double"
                Return Convert.ToDouble(a) / b
            Case "System.Decimal"
                Return Convert.ToDecimal(a) / b
            Case "System.Byte"
                Return Convert.ToByte(a) \ b
                ' Operator '/' cannot be applied to operands of type 'ulong' and 'int'
                'case "System.UInt64": return Convert.ToUInt64(a) / b;
        End Select

        Return Nothing
    End Function

#End Region

End Class
