<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Liberian
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
        Me.components = New System.ComponentModel.Container()
        Me.lblCanvas = New System.Windows.Forms.Label()
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.FileToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SaveToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ExitToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.globalTime = New System.Windows.Forms.Timer(Me.components)
        Me.grpBrushes = New System.Windows.Forms.GroupBox()
        Me.panBrushes = New System.Windows.Forms.Panel()
        Me.MenuStrip1.SuspendLayout()
        Me.grpBrushes.SuspendLayout()
        Me.SuspendLayout()
        '
        'lblCanvas
        '
        Me.lblCanvas.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblCanvas.Location = New System.Drawing.Point(106, 24)
        Me.lblCanvas.Name = "lblCanvas"
        Me.lblCanvas.Size = New System.Drawing.Size(278, 273)
        Me.lblCanvas.TabIndex = 0
        '
        'MenuStrip1
        '
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.FileToolStripMenuItem})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(384, 24)
        Me.MenuStrip1.TabIndex = 1
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'FileToolStripMenuItem
        '
        Me.FileToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.SaveToolStripMenuItem, Me.ExitToolStripMenuItem})
        Me.FileToolStripMenuItem.Name = "FileToolStripMenuItem"
        Me.FileToolStripMenuItem.Size = New System.Drawing.Size(37, 20)
        Me.FileToolStripMenuItem.Text = "File"
        '
        'SaveToolStripMenuItem
        '
        Me.SaveToolStripMenuItem.Name = "SaveToolStripMenuItem"
        Me.SaveToolStripMenuItem.Size = New System.Drawing.Size(98, 22)
        Me.SaveToolStripMenuItem.Text = "Save"
        '
        'ExitToolStripMenuItem
        '
        Me.ExitToolStripMenuItem.Name = "ExitToolStripMenuItem"
        Me.ExitToolStripMenuItem.Size = New System.Drawing.Size(98, 22)
        Me.ExitToolStripMenuItem.Text = "Exit"
        '
        'globalTime
        '
        Me.globalTime.Interval = 45
        '
        'grpBrushes
        '
        Me.grpBrushes.Controls.Add(Me.panBrushes)
        Me.grpBrushes.Location = New System.Drawing.Point(2, 27)
        Me.grpBrushes.Name = "grpBrushes"
        Me.grpBrushes.Size = New System.Drawing.Size(100, 224)
        Me.grpBrushes.TabIndex = 2
        Me.grpBrushes.TabStop = False
        Me.grpBrushes.Text = "Brushes"
        '
        'panBrushes
        '
        Me.panBrushes.AutoScroll = True
        Me.panBrushes.Dock = System.Windows.Forms.DockStyle.Top
        Me.panBrushes.Location = New System.Drawing.Point(3, 16)
        Me.panBrushes.Name = "panBrushes"
        Me.panBrushes.Size = New System.Drawing.Size(94, 207)
        Me.panBrushes.TabIndex = 4
        '
        'Liberian
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(384, 297)
        Me.Controls.Add(Me.grpBrushes)
        Me.Controls.Add(Me.lblCanvas)
        Me.Controls.Add(Me.MenuStrip1)
        Me.MainMenuStrip = Me.MenuStrip1
        Me.Name = "Liberian"
        Me.Text = "Liberian"
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.grpBrushes.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents lblCanvas As System.Windows.Forms.Label
    Friend WithEvents MenuStrip1 As System.Windows.Forms.MenuStrip
    Friend WithEvents FileToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ExitToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents globalTime As System.Windows.Forms.Timer
    Friend WithEvents grpBrushes As System.Windows.Forms.GroupBox
    Friend WithEvents SaveToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents panBrushes As System.Windows.Forms.Panel
End Class
