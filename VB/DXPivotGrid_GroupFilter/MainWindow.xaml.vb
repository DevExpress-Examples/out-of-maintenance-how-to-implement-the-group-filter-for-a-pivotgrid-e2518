Imports System.Windows
Imports System.Windows.Controls
Imports DevExpress.Xpf.PivotGrid

Namespace DXPivotGrid_GroupFilter

    Public Partial Class MainWindow
        Inherits Window

        Public Sub New()
            Me.InitializeComponent()
        End Sub

        Private Sub Window_Loaded(ByVal sender As Object, ByVal e As RoutedEventArgs)
            Me.pivotGridControl1.DataSource = GetTimeTableView()
            Me.fieldMonth.CollapseAll()
        End Sub

        Private Sub RadioButtonList_SelectionChanged(ByVal sender As Object, ByVal e As SelectionChangedEventArgs)
            If Me.pivotGridControl1 Is Nothing Then Return
            Me.pivotGridControl1.BeginUpdate()
            Dim group As PivotGridGroup = Me.pivotGridControl1.Groups(0)
            group.FilterValues.Reset()
            group.FilterValues.BeginUpdate()
            Select Case Me.radioGroup1.SelectedIndex
                Case 0
                    group.FilterValues.FilterType = FieldFilterType.Excluded
                Case 1
                    group.FilterValues.FilterType = FieldFilterType.Included
                    group.FilterValues.Values.Add(2009).ChildValues.Add(12)
                    group.FilterValues.Values.Add(2010).ChildValues.Add(1)
                Case 2
                    group.FilterValues.FilterType = FieldFilterType.Excluded
                    group.FilterValues.Values.Add(2009)
                Case 3
                    group.FilterValues.FilterType = FieldFilterType.Included
                    SelectFirstDays(group)
            End Select

            group.FilterValues.EndUpdate()
            Me.pivotGridControl1.EndUpdate()
            If Me.radioGroup1.SelectedIndex = 3 Then
                Me.fieldMonth.ExpandAll()
            Else
                Me.fieldMonth.CollapseAll()
            End If
        End Sub

        Private Sub SelectFirstDays(ByVal group As PivotGridGroup)
            For Each year As Object In group.GetUniqueValues(Nothing)
                Dim value As DevExpress.XtraPivotGrid.PivotGroupFilterValue = group.FilterValues.Values.Add(year)
                For Each month As Object In group.GetUniqueValues(New Object() {year})
                    value.ChildValues.Add(month).ChildValues.Add(1)
                Next
            Next
        End Sub
    End Class
End Namespace
