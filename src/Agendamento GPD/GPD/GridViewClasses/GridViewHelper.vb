'------------------------------------------------------------------------------------------
' Copyright © 2006 Agrinei Sousa [www.agrinei.com]
'
' Esse código fonte é fornecido sem garantia de qualquer tipo.
' Sinta-se livre para utilizá-lo, modificá-lo e distribuí-lo,
' inclusive em aplicações comerciais.
' É altamente desejável que essa mensagem não seja removida.
'------------------------------------------------------------------------------------------

Imports System.Collections
Imports System.Collections.Generic
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls


Public Delegate Sub FooterEvent(ByVal row As GridViewRow)

''' <summary>
''' A class to allow you to add summaries and groups to a GridView, easily!
''' </summary>
Public Class GridViewHelper

#Region "Fields"

    Private mGrid As GridView
    Private mGeneralSummaries As List(Of GridViewSummary)
    Private mGroups As List(Of GridViewGroup)
    Private useFooter As Boolean
    Private groupSortDir As SortDirection

#End Region


#Region "Messages"

    Private Const USE_ADEQUATE_METHOD_TO_REGISTER_THE_SUMMARY As String = "Use adequate method to register a summary with custom operation."
    Private Const GROUP_NOT_FOUND As String = "Group {0} not found. Please register the group before the summary."
    Private Const INVALID_SUMMARY As String = "Invalid summary."
    Private Const SUPRESS_GROUP_ALREADY_DEFINED As String = "A supress group is already defined. You can't define supress AND summary groups simultaneously"
    Private Const ONE_GROUP_ALREADY_REGISTERED As String = "At least a group is already defined. A supress group can't coexist with other groups"

#End Region


#Region "Events"

    ''' <summary>
    ''' Event triggered when a new group starts
    ''' </summary>
    Public Event GroupStart As GroupEvent

    ''' <summary>
    ''' Event triggered when a group ends
    ''' </summary>
    Public Event GroupEnd As GroupEvent

    ''' <summary>
    ''' Event triggered after a row for group header be inserted
    ''' </summary>
    Public Event GroupHeader As GroupEvent

    ''' <summary>
    ''' Event triggered after a row for group summary be inserted
    ''' </summary>
    Public Event GroupSummary As GroupEvent

    ''' <summary>
    ''' Event triggered when the footer is databound
    ''' </summary>
    Public Event FooterDataBound As FooterEvent

#End Region


#Region "Constructors"

    Public Sub New(ByVal grd As GridView)
        Me.New(grd, False, SortDirection.Ascending)
    End Sub

    Public Sub New(ByVal grd As GridView, ByVal useFooterForGeneralSummaries As Boolean)
        Me.New(grd, useFooterForGeneralSummaries, SortDirection.Ascending)
    End Sub

    Public Sub New(ByVal grd As GridView, ByVal useFooterForGeneralSummaries As Boolean, ByVal groupSortDirection As SortDirection)
        Me.mGrid = grd
        Me.useFooter = useFooterForGeneralSummaries
        Me.groupSortDir = groupSortDirection
        Me.mGeneralSummaries = New List(Of GridViewSummary)()
        Me.mGroups = New List(Of GridViewGroup)()
        AddHandler Me.mGrid.RowDataBound, New GridViewRowEventHandler(AddressOf RowDataBoundHandler)
    End Sub

#End Region


#Region "RegisterSummary overloads"

    Public Function RegisterSummary(ByVal column As String, ByVal operation As SummaryOperation) As GridViewSummary
        If operation = SummaryOperation.[Custom] Then
            Throw New Exception(USE_ADEQUATE_METHOD_TO_REGISTER_THE_SUMMARY)
        End If

        ' TO DO: Perform column validation...
        Dim s As New GridViewSummary(column, operation, Nothing)
        mGeneralSummaries.Add(s)

        ' if general summaries are displayed in the footer, it must be set to visible
        If useFooter Then
            mGrid.ShowFooter = True
        End If

        Return s
    End Function

    Public Function RegisterSummary(ByVal column As String, ByVal operation As SummaryOperation, ByVal groupName As String) As GridViewSummary
        If operation = SummaryOperation.[Custom] Then
            Throw New Exception(USE_ADEQUATE_METHOD_TO_REGISTER_THE_SUMMARY)
        End If

        Dim group As GridViewGroup = FindGroupByName(groupName)
        If group Is Nothing Then
            Throw New Exception([String].Format(GROUP_NOT_FOUND, groupName))
        End If

        ' TO DO: Perform column validation...
        Dim s As New GridViewSummary(column, operation, group)
        group.AddSummary(s)

        Return s
    End Function

    Public Function RegisterSummary(ByVal column As String, ByVal operation As CustomSummaryOperation, ByVal getResult As SummaryResultMethod) As GridViewSummary
        ' TO DO: Perform column validation...
        Dim s As New GridViewSummary(column, operation, getResult, Nothing)
        mGeneralSummaries.Add(s)

        ' if general summaries are displayed in the footer, it must be set to visible
        If useFooter Then
            mGrid.ShowFooter = True
        End If

        Return s
    End Function

    Public Function RegisterSummary(ByVal column As String, ByVal operation As CustomSummaryOperation, ByVal getResult As SummaryResultMethod, ByVal groupName As String) As GridViewSummary
        Dim group As GridViewGroup = FindGroupByName(groupName)
        If group Is Nothing Then
            Throw New Exception([String].Format(GROUP_NOT_FOUND, groupName))
        End If

        ' TO DO: Perform column validation...
        Dim s As New GridViewSummary(column, operation, getResult, group)
        group.AddSummary(s)

        Return s
    End Function

    Public Function RegisterSummary(ByVal s As GridViewSummary) As GridViewSummary
        If Not s.Validate() Then
            Throw New Exception(INVALID_SUMMARY)
        End If

        If s.Group Is Nothing Then
            ' if general summaries are displayed in the footer, it must be set to visible
            If useFooter Then
                mGrid.ShowFooter = True
            End If

            mGeneralSummaries.Add(s)
        ElseIf Not s.Group.ContainsSummary(s) Then
            s.Group.AddSummary(s)
        End If

        Return s
    End Function

#End Region


#Region "RegisterGroup overloads"

    Public Function RegisterGroup(ByVal column As String, ByVal auto As Boolean, ByVal hideGroupColumns As Boolean) As GridViewGroup
        Dim cols As String() = New String(0) {column}
        Return RegisterGroup(cols, auto, hideGroupColumns)
    End Function

    Public Function RegisterGroup(ByVal columns As String(), ByVal auto As Boolean, ByVal hideGroupColumns As Boolean) As GridViewGroup
        If HasSupressGroup() Then
            Throw New Exception(SUPRESS_GROUP_ALREADY_DEFINED)
        End If

        ' TO DO: Perform column validation...
        Dim g As New GridViewGroup(columns, auto, hideGroupColumns)
        mGroups.Add(g)

        If hideGroupColumns Then
            For i As Integer = 0 To mGrid.Columns.Count - 1
                For j As Integer = 0 To columns.Length - 1
                    If GetDataFieldName(mGrid.Columns(i)).ToLower() = columns(j).ToLower() Then
                        mGrid.Columns(i).Visible = False
                    End If
                Next
            Next
        End If

        Return g
    End Function

#End Region


#Region "SetSupressGroup overloads"

    Public Function SetSupressGroup(ByVal column As String) As GridViewGroup
        Dim cols As String() = New String(0) {column}
        Return SetSupressGroup(cols)
    End Function

    Public Function SetSupressGroup(ByVal columns As String()) As GridViewGroup
        If mGroups.Count > 0 Then
            Throw New Exception(ONE_GROUP_ALREADY_REGISTERED)
        End If

        ' TO DO: Perform column validation...
        Dim g As New GridViewGroup(columns, True)
        mGroups.Add(g)

        Return g
    End Function

#End Region


#Region "Private Helper functions"

    Private Function GetSequentialGroupColumns() As String
        Dim ret As String = [String].Empty

        For Each g As GridViewGroup In mGroups
            ret += g.Name.Replace("+"c, ","c) & ","
        Next
        Return ret.Substring(0, ret.Length - 1)
    End Function

    ''' <summary>
    ''' Compares the actual group values with the values of the current dataitem
    ''' </summary>
    ''' <param name="g"></param>
    ''' <param name="dataitem"></param>
    ''' <returns></returns>
    Private Function EvaluateEquals(ByVal g As GridViewGroup, ByVal dataitem As Object) As Boolean
        ' The values wasn't initialized
        If g.ActualValues Is Nothing Then
            Return False
        End If

        For i As Integer = 0 To g.Columns.Length - 1
            If g.ActualValues(i) Is Nothing AndAlso DataBinder.Eval(dataitem, g.Columns(i)) IsNot Nothing Then
                Return False
            End If
            If g.ActualValues(i) IsNot Nothing AndAlso DataBinder.Eval(dataitem, g.Columns(i)) Is Nothing Then
                Return False
            End If
            If Not g.ActualValues(i).Equals(DataBinder.Eval(dataitem, g.Columns(i))) Then
                Return False
            End If
        Next

        Return True
    End Function

    Private Function HasSupressGroup() As Boolean
        For Each g As GridViewGroup In mGroups
            If g.IsSupressGroup Then
                Return True
            End If
        Next
        Return False
    End Function

    Private Function HasAutoSummary(ByVal list As List(Of GridViewSummary)) As Boolean
        For Each s As GridViewSummary In list
            If s.Automatic Then
                Return True
            End If
        Next
        Return False
    End Function

    Private Function GetGroupRowValues(ByVal g As GridViewGroup, ByVal dataitem As Object) As Object()
        Dim values As Object() = New Object(g.Columns.Length - 1) {}

        For i As Integer = 0 To g.Columns.Length - 1
            values(i) = DataBinder.Eval(dataitem, g.Columns(i))
        Next

        Return values
    End Function

    Private Function FindGroupByName(ByVal name As String) As GridViewGroup
        For Each g As GridViewGroup In mGroups
            If g.Name.ToLower() = name.ToLower() Then
                Return g
            End If
        Next

        Return Nothing
    End Function

    ''' <summary>
    ''' Inserts a grid row. Only cells required for the summary results
    ''' will be created (except if GenerateAllCellsOnSummaryRow is true).
    ''' The group will be checked for columns with summary
    ''' </summary>
    ''' <param name="beforeRow"></param>
    ''' <param name="g"></param>
    ''' <returns></returns>
    Private Function InsertGridRow(ByVal beforeRow As GridViewRow, ByVal g As GridViewGroup) As GridViewRow
        Dim colspan As Integer
        Dim cell As TableCell
        Dim tcArray As TableCell()
        Dim visibleColumns As Integer = Me.GetVisibleColumnCount()

        Dim tbl As Table = DirectCast(mGrid.Controls(0), Table)
        Dim newRowIndex As Integer = tbl.Rows.GetRowIndex(beforeRow)
        Dim newRow As New GridViewRow(newRowIndex, newRowIndex, DataControlRowType.DataRow, DataControlRowState.Normal)

        If g IsNot Nothing AndAlso (g.IsSupressGroup OrElse g.GenerateAllCellsOnSummaryRow) Then
            ' Create all the table cells
            tcArray = New TableCell(visibleColumns - 1) {}
            For i As Integer = 0 To visibleColumns - 1
                cell = New TableCell()
                cell.ApplyStyle(mGrid.Columns(GetRealIndexFromVisibleColumnIndex(i)).ItemStyle)
                cell.Text = " "
                tcArray(i) = cell
            Next
        Else
            ' Create only the required table cells
            colspan = 0
            Dim tcc As New List(Of TableCell)()
            For i As Integer = 0 To mGrid.Columns.Count - 1
                If ColumnHasSummary(i, g) Then
                    If colspan > 0 Then
                        cell = New TableCell()
                        cell.Text = " "
                        cell.ColumnSpan = colspan
                        tcc.Add(cell)
                        colspan = 0
                    End If

                    ' insert table cell and copy the style
                    cell = New TableCell()
                    cell.ApplyStyle(mGrid.Columns(i).ItemStyle)
                    tcc.Add(cell)
                ElseIf mGrid.Columns(i).Visible Then
                    ' A visible column that will have no cell because has
                    ' no summary. So we increase the colspan...
                    colspan += 1
                End If
            Next

            If colspan > 0 Then
                cell = New TableCell()
                cell.Text = " "
                cell.ColumnSpan = colspan
                tcc.Add(cell)
                colspan = 0
            End If

            tcArray = New TableCell(tcc.Count - 1) {}
            tcc.CopyTo(tcArray)
        End If

        newRow.Cells.AddRange(tcArray)
        tbl.Controls.AddAt(newRowIndex, newRow)

        Return newRow
    End Function

    ''' <summary>
    '''  Inserts a grid row with one cell only
    ''' </summary>
    ''' <param name="beforeRow"></param>
    ''' <returns></returns>
    Private Function InsertGridRow(ByVal beforeRow As GridViewRow) As GridViewRow
        Dim visibleColumns As Integer = Me.GetVisibleColumnCount()

        Dim tbl As Table = DirectCast(mGrid.Controls(0), Table)
        Dim newRowIndex As Integer = tbl.Rows.GetRowIndex(beforeRow)
        Dim newRow As New GridViewRow(newRowIndex, newRowIndex, DataControlRowType.DataRow, DataControlRowState.Normal)

        newRow.Cells.Add(New TableCell())
        If visibleColumns > 1 Then
            newRow.Cells(0).ColumnSpan = visibleColumns
        End If

        tbl.Controls.AddAt(newRowIndex, newRow)

        Return newRow
    End Function

#End Region


#Region "Core"

    Private Sub RowDataBoundHandler(ByVal sender As Object, ByVal e As GridViewRowEventArgs)
        For Each g As GridViewGroup In mGroups
            ' The last group values are caught here
            If e.Row.RowType = DataControlRowType.Footer Then
                g.CalculateSummaries()
                GenerateGroupSummary(g, e.Row)
                RaiseEvent GroupEnd(g.Name, g.ActualValues, e.Row)
            ElseIf e.Row.RowType = DataControlRowType.DataRow Then
                ProcessGroup(g, e)
                If g.IsSupressGroup Then
                    e.Row.Visible = False
                End If
            End If
        Next

        ' This will deal only with general summaries
        For Each s As GridViewSummary In mGeneralSummaries
            If e.Row.RowType = DataControlRowType.Header Then
                ' Essentially this isn't required, but it prevents wrong calc
                ' in case of RowDataBound event be called twice (for each row)
                s.Reset()
            ElseIf e.Row.RowType = DataControlRowType.DataRow Then
                s.AddValue(DataBinder.Eval(e.Row.DataItem, s.Column))
            ElseIf e.Row.RowType = DataControlRowType.Footer Then
                s.Calculate()
            End If
        Next

        If e.Row.RowType = DataControlRowType.Footer Then
            ' Automatic generation of summary
            GenerateGeneralSummaries(e)

            ' Triggers event footerdatabound
            RaiseEvent FooterDataBound(e.Row)
        End If
    End Sub

    Private Sub ProcessGroup(ByVal g As GridViewGroup, ByVal e As GridViewRowEventArgs)
        Dim groupHeaderText As String = [String].Empty

        ' Check if it's still in the same group values
        If Not EvaluateEquals(g, e.Row.DataItem) Then
            ' Check if a group ends or if it is the first group values starting...
            If g.ActualValues IsNot Nothing Then
                g.CalculateSummaries()
                GenerateGroupSummary(g, e.Row)

                ' Triggers event GroupEnd
                RaiseEvent GroupEnd(g.Name, g.ActualValues, e.Row)
            End If

            ' Another group values starts now
            g.Reset()
            g.ActualValues = GetGroupRowValues(g, e.Row.DataItem)

            ' If group is automatic inserts a group header
            If g.Automatic Then
                For v As Integer = 0 To g.ActualValues.Length - 1
                    If g.ActualValues(v) Is Nothing Then
                        Continue For
                    End If
                    groupHeaderText += g.ActualValues(v).ToString()
                    If g.ActualValues.Length - v > 1 Then
                        groupHeaderText += " - "
                    End If
                Next

                Dim newRow As GridViewRow = InsertGridRow(e.Row)
                newRow.Cells(0).Text = groupHeaderText

                ' Triggers event GroupHeader
                RaiseEvent GroupHeader(g.Name, g.ActualValues, newRow)
            End If

            ' Triggers event GroupStart
            RaiseEvent GroupStart(g.Name, g.ActualValues, e.Row)
        End If

        g.AddValueToSummaries(e.Row.DataItem)
    End Sub

    Private Function GetFormatedString(ByVal preferredFormat As String, ByVal secondFormat As String, ByVal value As Object) As String
        Dim format As [String] = preferredFormat
        If format.Length = 0 Then
            format = secondFormat
        End If

        If format.Length > 0 Then
            Return [String].Format(format, value)
        Else
            Return value.ToString()
        End If
    End Function

    Private Sub GenerateGroupSummary(ByVal g As GridViewGroup, ByVal row As GridViewRow)
        Dim colIndex As Integer
        Dim colValue As Object

        If Not HasAutoSummary(g.Summaries) AndAlso Not HasSupressGroup() Then
            Return
        End If

        ' Inserts a new row 
        Dim newRow As GridViewRow = InsertGridRow(row, g)

        For Each s As GridViewSummary In g.Summaries
            If s.Automatic Then
                colIndex = GetVisibleColumnIndex(s.Column)
                colIndex = ResolveCellIndex(newRow, colIndex)
                newRow.Cells(colIndex).Text = Me.GetFormatedString(s.FormatString, Me.GetColumnFormat(GetColumnIndex(s.Column)), s.Value)
            End If
        Next

        ' If it is a supress group must set the grouped values in the cells
        ' of the inserted row
        If g.IsSupressGroup Then
            For i As Integer = 0 To g.Columns.Length - 1
                colValue = g.ActualValues(i)
                If colValue IsNot Nothing Then
                    colIndex = GetVisibleColumnIndex(g.Columns(i))
                    colIndex = ResolveCellIndex(newRow, colIndex)
                    newRow.Cells(colIndex).Text = colValue.ToString()
                End If
            Next
        End If

        ' Triggers event GroupSummary
        RaiseEvent GroupSummary(g.Name, g.ActualValues, newRow)

    End Sub

    ''' <summary>
    ''' Generates the general summaries in the grid. 
    ''' </summary>
    ''' <param name="e">GridViewRowEventArgs</param>
    Private Sub GenerateGeneralSummaries(ByVal e As GridViewRowEventArgs)
        Dim colIndex As Integer
        Dim row As GridViewRow

        If Not HasAutoSummary(Me.mGeneralSummaries) Then
            Return
        End If

        If useFooter Then
            row = e.Row
        Else
            row = InsertGridRow(e.Row, Nothing)
        End If

        For Each s As GridViewSummary In mGeneralSummaries
            If Not s.Automatic Then
                Continue For
            End If

            If useFooter Then
                colIndex = GetColumnIndex(s.Column)
            Else
                colIndex = GetVisibleColumnIndex(s.Column)
            End If

            colIndex = ResolveCellIndex(row, colIndex)
            row.Cells(colIndex).Text = Me.GetFormatedString(s.FormatString, Me.GetColumnFormat(GetColumnIndex(s.Column)), s.Value)
        Next

        ' Triggers event GroupSummary
        RaiseEvent GroupSummary(Nothing, Nothing, row)

    End Sub

    ''' <summary>
    ''' Identifies the equivalent index on a row that contains cells with colspan
    ''' </summary>
    ''' <param name="row"></param>
    ''' <param name="colIndex"></param>
    ''' <returns></returns>
    Private Function ResolveCellIndex(ByVal row As GridViewRow, ByVal colIndex As Integer) As Integer
        Dim colspansum As Integer = 0
        Dim realIndex As Integer

        For i As Integer = 0 To row.Cells.Count - 1
            realIndex = i + colspansum
            If realIndex = colIndex Then
                Return i
            End If

            If row.Cells(i).ColumnSpan > 1 Then
                colspansum = colspansum + row.Cells(i).ColumnSpan - 1
            End If
        Next

        Return -1
    End Function

    Private Function ColumnHasSummary(ByVal colindex As Integer, ByVal g As GridViewGroup) As Boolean
        Dim list As List(Of GridViewSummary)
        Dim column As String = Me.GetDataFieldName(mGrid.Columns(colindex))

        If g Is Nothing Then
            list = Me.mGeneralSummaries
        Else
            list = g.Summaries
        End If

        For Each s As GridViewSummary In list
            If column.ToLower() = s.Column.ToLower() Then
                Return True
            End If
        Next
        Return False
    End Function

    Private Function ColumnHasSummary(ByVal column As String, ByVal g As GridViewGroup) As Boolean
        Dim list As List(Of GridViewSummary)

        If g Is Nothing Then
            list = Me.mGeneralSummaries
        Else
            list = g.Summaries
        End If

        For Each s As GridViewSummary In list
            If column.ToLower() = s.Column.ToLower() Then
                Return True
            End If
        Next
        Return False
    End Function

#End Region


#Region "Public Helper functions"

    Public Function GetRealIndexFromVisibleColumnIndex(ByVal visibleIndex As Integer) As Integer
        Dim visibles As Integer = 0
        For i As Integer = 0 To mGrid.Columns.Count - 1
            If mGrid.Columns(i).Visible Then
                If visibleIndex = visibles Then
                    Return i
                End If
                visibles += 1
            End If
        Next

        ' Not found....
        Return -1
    End Function

    Public Function GetVisibleColumnIndex(ByVal columnName As String) As Integer
        Dim visibles As Integer = 0
        For i As Integer = 0 To mGrid.Columns.Count - 1
            If GetDataFieldName(mGrid.Columns(i)).ToLower() = columnName.ToLower() Then
                Return visibles
            End If

            If mGrid.Columns(i).Visible Then
                visibles += 1
            End If
        Next

        ' Not found....
        Return -1
    End Function

    Public Function GetColumnIndex(ByVal columnName As String) As Integer
        For i As Integer = 0 To mGrid.Columns.Count - 1
            If GetDataFieldName(mGrid.Columns(i)).ToLower() = columnName.ToLower() Then
                Return i
            End If
        Next

        ' Not found....
        Return -1
    End Function

    Public Function GetDataFieldName(ByVal field As DataControlField) As String
        ' TO DO: Enable search in HyperLinkField, ButtonField...

        If TypeOf field Is BoundField Then
            Return TryCast(field, BoundField).DataField
        Else
            ' It hopes that SortExpression is set (and it's equal to column name)
            Return field.SortExpression
        End If
    End Function

    Public Function GetColumnFormat(ByVal colIndex As Integer) As String
        ' TO DO: Enable search in HyperLinkField, ButtonField...

        If TypeOf mGrid.Columns(colIndex) Is BoundField Then
            Return TryCast(mGrid.Columns(colIndex), BoundField).DataFormatString
        Else
            Return [String].Empty
        End If
    End Function

    Public Function GetVisibleColumnCount() As Integer
        Dim ret As Integer = 0

        For i As Integer = 0 To mGrid.Columns.Count - 1
            If mGrid.Columns(i).Visible Then
                ret += 1
            End If
        Next

        Return ret
    End Function


    ''' <summary>
    ''' This method must be called to hide columns that doesn't 
    ''' have any summary operation when we are using a supress group
    ''' </summary>
    Public Sub SetInvisibleColumnsWithoutGroupSummary()
        Dim colname As String
        Dim colChecked As Boolean

        For Each dcf As DataControlField In mGrid.Columns
            colChecked = False
            colname = GetDataFieldName(dcf).ToLower()

            For Each g As GridViewGroup In mGroups
                ' Check if it's part of the group columns
                For j As Integer = 0 To g.Columns.Length - 1
                    If colname = g.Columns(j).ToLower() Then
                        colChecked = True
                        Exit For
                    End If
                Next

                If colChecked Then
                    Exit For
                End If

                ' Check if it's part of a group summary
                colChecked = ColumnHasSummary(colname, g)

                If colChecked Then
                    Exit For
                End If
            Next

            If colChecked Then
                Continue For
            End If


            dcf.Visible = False
        Next
    End Sub

    Public Sub ApplyGroupSort()
        mGrid.Sort(Me.GetSequentialGroupColumns(), groupSortDir)
    End Sub

#End Region
End Class
