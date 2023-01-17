<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form2
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.CSVFFormatToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.AuthorCsvToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.EditorCSVToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.MenuStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'MenuStrip1
        '
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.CSVFFormatToolStripMenuItem})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(415, 24)
        Me.MenuStrip1.TabIndex = 0
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'CSVFFormatToolStripMenuItem
        '
        Me.CSVFFormatToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.AuthorCsvToolStripMenuItem, Me.EditorCSVToolStripMenuItem})
        Me.CSVFFormatToolStripMenuItem.Name = "CSVFFormatToolStripMenuItem"
        Me.CSVFFormatToolStripMenuItem.Size = New System.Drawing.Size(81, 20)
        Me.CSVFFormatToolStripMenuItem.Text = "CSV Format"
        '
        'AuthorCsvToolStripMenuItem
        '
        Me.AuthorCsvToolStripMenuItem.Name = "AuthorCsvToolStripMenuItem"
        Me.AuthorCsvToolStripMenuItem.Size = New System.Drawing.Size(152, 22)
        Me.AuthorCsvToolStripMenuItem.Text = "Author CSV"
        '
        'EditorCSVToolStripMenuItem
        '
        Me.EditorCSVToolStripMenuItem.Name = "EditorCSVToolStripMenuItem"
        Me.EditorCSVToolStripMenuItem.Size = New System.Drawing.Size(152, 22)
        Me.EditorCSVToolStripMenuItem.Text = "Editor CSV"
        '
        'Form2
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.ActiveCaption
        Me.ClientSize = New System.Drawing.Size(415, 214)
        Me.Controls.Add(Me.MenuStrip1)
        Me.MainMenuStrip = Me.MenuStrip1
        Me.MaximizeBox = False
        Me.Name = "Form2"
        Me.Text = "CSV Creation Tool"
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents MenuStrip1 As System.Windows.Forms.MenuStrip
    Friend WithEvents CSVFFormatToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents AuthorCsvToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents EditorCSVToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
End Class
