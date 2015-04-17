<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class tilesettings
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
        Me.panSettings = New System.Windows.Forms.Panel()
        Me.btnApply = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'panSettings
        '
        Me.panSettings.AutoScroll = True
        Me.panSettings.Location = New System.Drawing.Point(12, 16)
        Me.panSettings.Name = "panSettings"
        Me.panSettings.Size = New System.Drawing.Size(203, 104)
        Me.panSettings.TabIndex = 0
        '
        'btnApply
        '
        Me.btnApply.Location = New System.Drawing.Point(221, 42)
        Me.btnApply.Name = "btnApply"
        Me.btnApply.Size = New System.Drawing.Size(42, 54)
        Me.btnApply.TabIndex = 1
        Me.btnApply.Text = "Apply"
        Me.btnApply.UseVisualStyleBackColor = True
        '
        'tilesettings
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(275, 132)
        Me.Controls.Add(Me.btnApply)
        Me.Controls.Add(Me.panSettings)
        Me.Name = "tilesettings"
        Me.Text = "tilesettings"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents panSettings As System.Windows.Forms.Panel
    Friend WithEvents btnApply As System.Windows.Forms.Button
End Class
