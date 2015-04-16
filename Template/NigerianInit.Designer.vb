<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class NigerianInit
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
        Me.cobPlayer = New System.Windows.Forms.ComboBox()
        Me.cobLevel = New System.Windows.Forms.ComboBox()
        Me.lblLevelPrompt = New System.Windows.Forms.Label()
        Me.lblPlayerPrompt = New System.Windows.Forms.Label()
        Me.cobDiff = New System.Windows.Forms.ComboBox()
        Me.lblDiffPrompt = New System.Windows.Forms.Label()
        Me.btnGO = New System.Windows.Forms.Button()
        Me.picPlayerPreview = New System.Windows.Forms.PictureBox()
        Me.CheckBox1 = New System.Windows.Forms.CheckBox()
        CType(Me.picPlayerPreview, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'cobPlayer
        '
        Me.cobPlayer.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cobPlayer.FormattingEnabled = True
        Me.cobPlayer.Location = New System.Drawing.Point(62, 30)
        Me.cobPlayer.Name = "cobPlayer"
        Me.cobPlayer.Size = New System.Drawing.Size(57, 21)
        Me.cobPlayer.TabIndex = 0
        '
        'cobLevel
        '
        Me.cobLevel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cobLevel.FormattingEnabled = True
        Me.cobLevel.Location = New System.Drawing.Point(48, 3)
        Me.cobLevel.Name = "cobLevel"
        Me.cobLevel.Size = New System.Drawing.Size(71, 21)
        Me.cobLevel.TabIndex = 1
        '
        'lblLevelPrompt
        '
        Me.lblLevelPrompt.AutoSize = True
        Me.lblLevelPrompt.Location = New System.Drawing.Point(6, 6)
        Me.lblLevelPrompt.Name = "lblLevelPrompt"
        Me.lblLevelPrompt.Size = New System.Drawing.Size(36, 13)
        Me.lblLevelPrompt.TabIndex = 2
        Me.lblLevelPrompt.Text = "Level:"
        '
        'lblPlayerPrompt
        '
        Me.lblPlayerPrompt.AutoSize = True
        Me.lblPlayerPrompt.Location = New System.Drawing.Point(6, 33)
        Me.lblPlayerPrompt.Name = "lblPlayerPrompt"
        Me.lblPlayerPrompt.Size = New System.Drawing.Size(39, 13)
        Me.lblPlayerPrompt.TabIndex = 3
        Me.lblPlayerPrompt.Text = "Player:"
        '
        'cobDiff
        '
        Me.cobDiff.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cobDiff.FormattingEnabled = True
        Me.cobDiff.Location = New System.Drawing.Point(62, 57)
        Me.cobDiff.Name = "cobDiff"
        Me.cobDiff.Size = New System.Drawing.Size(57, 21)
        Me.cobDiff.TabIndex = 4
        '
        'lblDiffPrompt
        '
        Me.lblDiffPrompt.AutoSize = True
        Me.lblDiffPrompt.Location = New System.Drawing.Point(6, 60)
        Me.lblDiffPrompt.Name = "lblDiffPrompt"
        Me.lblDiffPrompt.Size = New System.Drawing.Size(50, 13)
        Me.lblDiffPrompt.TabIndex = 5
        Me.lblDiffPrompt.Text = "Difficulty:"
        '
        'btnGO
        '
        Me.btnGO.Enabled = False
        Me.btnGO.Location = New System.Drawing.Point(12, 87)
        Me.btnGO.Name = "btnGO"
        Me.btnGO.Size = New System.Drawing.Size(75, 23)
        Me.btnGO.TabIndex = 6
        Me.btnGO.Text = "Go!"
        Me.btnGO.UseVisualStyleBackColor = True
        '
        'picPlayerPreview
        '
        Me.picPlayerPreview.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.picPlayerPreview.Location = New System.Drawing.Point(140, 12)
        Me.picPlayerPreview.Name = "picPlayerPreview"
        Me.picPlayerPreview.Size = New System.Drawing.Size(61, 53)
        Me.picPlayerPreview.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.picPlayerPreview.TabIndex = 7
        Me.picPlayerPreview.TabStop = False
        '
        'CheckBox1
        '
        Me.CheckBox1.AutoSize = True
        Me.CheckBox1.Location = New System.Drawing.Point(102, 91)
        Me.CheckBox1.Name = "CheckBox1"
        Me.CheckBox1.Size = New System.Drawing.Size(108, 17)
        Me.CheckBox1.TabIndex = 8
        Me.CheckBox1.Text = "Gunderson Mode"
        Me.CheckBox1.UseVisualStyleBackColor = True
        '
        'NigerianInit
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(213, 122)
        Me.Controls.Add(Me.CheckBox1)
        Me.Controls.Add(Me.picPlayerPreview)
        Me.Controls.Add(Me.btnGO)
        Me.Controls.Add(Me.lblDiffPrompt)
        Me.Controls.Add(Me.cobDiff)
        Me.Controls.Add(Me.lblPlayerPrompt)
        Me.Controls.Add(Me.lblLevelPrompt)
        Me.Controls.Add(Me.cobLevel)
        Me.Controls.Add(Me.cobPlayer)
        Me.Name = "NigerianInit"
        Me.Text = "NigerianInit"
        CType(Me.picPlayerPreview, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents cobPlayer As System.Windows.Forms.ComboBox
    Friend WithEvents cobLevel As System.Windows.Forms.ComboBox
    Friend WithEvents lblLevelPrompt As System.Windows.Forms.Label
    Friend WithEvents lblPlayerPrompt As System.Windows.Forms.Label
    Friend WithEvents cobDiff As System.Windows.Forms.ComboBox
    Friend WithEvents lblDiffPrompt As System.Windows.Forms.Label
    Friend WithEvents btnGO As System.Windows.Forms.Button
    Friend WithEvents picPlayerPreview As System.Windows.Forms.PictureBox
    Friend WithEvents CheckBox1 As System.Windows.Forms.CheckBox
End Class
