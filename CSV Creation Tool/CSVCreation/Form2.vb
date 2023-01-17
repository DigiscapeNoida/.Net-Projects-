Public Class Form2

    Private Sub AuthorCsvToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AuthorCsvToolStripMenuItem.Click
        Form1.Show()
        Me.Hide()
    End Sub

    Private Sub EditorCSVToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles EditorCSVToolStripMenuItem.Click
        Form3.Show()
        Me.Hide()
    End Sub
End Class