Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data
Imports System.Drawing
Imports System.Text
Imports System.Windows.Forms
Imports DevExpress.Data.Filtering
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraGrid
Imports System.Collections

Namespace WarnNewRowDisappears
	Partial Public Class Form1
		Inherits Form
		Public Sub New()
			InitializeComponent()
		End Sub

		Private Sub Form1_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
			Dim TempXViewsPrinting As DevExpress.XtraGrid.Design.XViewsPrinting = New DevExpress.XtraGrid.Design.XViewsPrinting(gridControl1)
			gridView1.OptionsView.ShowGroupPanel = False
			gridView1.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Bottom
			gridView1.ActiveFilterCriteria = New OperandProperty("Discontinued") = New OperandValue(False)
		End Sub

		Private Function IsRowFilteredOut(ByVal view As GridView, ByVal dataRow As Object) As Boolean
			If view Is Nothing OrElse view.DataSource Is Nothing OrElse dataRow Is Nothing Then
				Throw New ArgumentNullException()
			End If
			Dim dataSourceIndex As Integer = (CType(view.DataSource, IList)).IndexOf(dataRow)
			If dataSourceIndex < 0 Then
				Throw New Exception("Row doesn't belong to the data source")
			End If
			Return view.GetRowHandle(dataSourceIndex) < 0
		End Function

		Private Sub gridView1_RowUpdated(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Base.RowObjectEventArgs) Handles gridView1.RowUpdated
			Dim view As GridView = CType(sender, GridView)
			If view.ActiveFilterEnabled AndAlso view.FocusedRowHandle = GridControl.NewItemRowHandle Then
				If IsRowFilteredOut(view, e.Row) Then
					MessageBox.Show("The new row you've just inserted is filtered out of the view due to the active filter.")
				End If
			End If
		End Sub
	End Class
End Namespace