'------------------------------------------------------------------------------------------
' Copyright © 2006 Agrinei Sousa [www.agrinei.com]
'
' Esse código fonte é fornecido sem garantia de qualquer tipo.
' Sinta-se livre para utilizá-lo, modificá-lo e distribuí-lo,
' inclusive em aplicações comerciais.
' É altamente desejável que essa mensagem não seja removida.
'------------------------------------------------------------------------------------------

Imports System.Collections.Generic
Imports System.Web.UI
Imports System.Web.UI.WebControls


Public Delegate Sub GroupEvent(ByVal groupName As String, ByVal values As Object(), ByVal row As GridViewRow)

''' <summary>
''' A class that represents a group consisting of a set of columns
''' </summary>
Public Class GridViewGroup
    Public Columns As String()
    Public ActualValues As Object()
    Public Quantity As Integer
    Public Automatic As Boolean
    Public HideGroupColumns As Boolean
    Public IsSupressGroup As Boolean
    Public GenerateAllCellsOnSummaryRow As Boolean

    Private mSummaries As List(Of GridViewSummary)

    Public ReadOnly Property Summaries() As List(Of GridViewSummary)
        Get
            Return mSummaries
        End Get
    End Property

    Public ReadOnly Property Name() As String
        Get
            Return [String].Join("+", Me.Columns)
        End Get
    End Property

    Private Sub New()
        Me.ActualValues = Nothing
        Me.Quantity = 0
        Me.IsSupressGroup = False
        Me.mSummaries = New List(Of GridViewSummary)()
    End Sub

    Public Sub New(ByVal cols As String(), ByVal auto As Boolean, ByVal hideGroupColumns As Boolean)
        Me.New()
        Me.Columns = cols
        Me.Automatic = auto
        Me.HideGroupColumns = hideGroupColumns
    End Sub

    Public Sub New(ByVal cols As String(), ByVal isSupress As Boolean)
        Me.New(cols, False, False)
        Me.IsSupressGroup = isSupress
    End Sub

    Public Function ContainsSummary(ByVal s As GridViewSummary) As Boolean
        Return mSummaries.Contains(s)
    End Function

    Public Sub AddSummary(ByVal s As GridViewSummary)
        If Me.ContainsSummary(s) Then
            Throw New Exception("Summary already exists in this group.")
        End If

        If Not s.Validate() Then
            Throw New Exception("Invalid summary.")
        End If

        s.Group = Me
        Me.mSummaries.Add(s)
    End Sub

    Public Sub Reset()
        Me.Quantity = 0

        For Each s As GridViewSummary In mSummaries
            s.Reset()
        Next
    End Sub

    Public Sub AddValueToSummaries(ByVal dataitem As Object)
        Me.Quantity += 1

        For Each s As GridViewSummary In mSummaries
            s.AddValue(DataBinder.Eval(dataitem, s.Column))
        Next
    End Sub

    Public Sub CalculateSummaries()
        For Each s As GridViewSummary In mSummaries
            s.Calculate()
        Next
    End Sub
End Class
