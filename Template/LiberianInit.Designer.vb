<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class LiberianInit
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
        Me.grpLoadorNew = New System.Windows.Forms.GroupBox()
        Me.radLoadLevel = New System.Windows.Forms.RadioButton()
        Me.radNewLevel = New System.Windows.Forms.RadioButton()
        Me.grpNewCanvas = New System.Windows.Forms.GroupBox()
        Me.cobEnvironment = New System.Windows.Forms.ComboBox()
        Me.lblEnvironmentPrompt = New System.Windows.Forms.Label()
        Me.lblCanvasSizePrompt = New System.Windows.Forms.Label()
        Me.cobSize = New System.Windows.Forms.ComboBox()
        Me.lblCanvasNamePrompt = New System.Windows.Forms.Label()
        Me.txtName = New System.Windows.Forms.TextBox()
        Me.grpLoadCanvas = New System.Windows.Forms.GroupBox()
        Me.cobModEnv = New System.Windows.Forms.ComboBox()
        Me.lblModEnvPrompt = New System.Windows.Forms.Label()
        Me.cobLoadName = New System.Windows.Forms.ComboBox()
        Me.lblLoadNamePrompt = New System.Windows.Forms.Label()
        Me.btnGo = New System.Windows.Forms.Button()
        Me.grpLoadorNew.SuspendLayout()
        Me.grpNewCanvas.SuspendLayout()
        Me.grpLoadCanvas.SuspendLayout()
        Me.SuspendLayout()
        '
        'grpLoadorNew
        '
        Me.grpLoadorNew.Controls.Add(Me.radLoadLevel)
        Me.grpLoadorNew.Controls.Add(Me.radNewLevel)
        Me.grpLoadorNew.Location = New System.Drawing.Point(12, 12)
        Me.grpLoadorNew.Name = "grpLoadorNew"
        Me.grpLoadorNew.Size = New System.Drawing.Size(97, 70)
        Me.grpLoadorNew.TabIndex = 0
        Me.grpLoadorNew.TabStop = False
        Me.grpLoadorNew.Text = "Creation"
        '
        'radLoadLevel
        '
        Me.radLoadLevel.AutoSize = True
        Me.radLoadLevel.Location = New System.Drawing.Point(6, 42)
        Me.radLoadLevel.Name = "radLoadLevel"
        Me.radLoadLevel.Size = New System.Drawing.Size(49, 17)
        Me.radLoadLevel.TabIndex = 1
        Me.radLoadLevel.Text = "Load"
        Me.radLoadLevel.UseVisualStyleBackColor = True
        '
        'radNewLevel
        '
        Me.radNewLevel.AutoSize = True
        Me.radNewLevel.Checked = True
        Me.radNewLevel.Location = New System.Drawing.Point(6, 19)
        Me.radNewLevel.Name = "radNewLevel"
        Me.radNewLevel.Size = New System.Drawing.Size(47, 17)
        Me.radNewLevel.TabIndex = 0
        Me.radNewLevel.TabStop = True
        Me.radNewLevel.Text = "New"
        Me.radNewLevel.UseVisualStyleBackColor = True
        '
        'grpNewCanvas
        '
        Me.grpNewCanvas.Controls.Add(Me.cobEnvironment)
        Me.grpNewCanvas.Controls.Add(Me.lblEnvironmentPrompt)
        Me.grpNewCanvas.Controls.Add(Me.lblCanvasSizePrompt)
        Me.grpNewCanvas.Controls.Add(Me.cobSize)
        Me.grpNewCanvas.Controls.Add(Me.lblCanvasNamePrompt)
        Me.grpNewCanvas.Controls.Add(Me.txtName)
        Me.grpNewCanvas.Enabled = False
        Me.grpNewCanvas.Location = New System.Drawing.Point(115, 12)
        Me.grpNewCanvas.Name = "grpNewCanvas"
        Me.grpNewCanvas.Size = New System.Drawing.Size(147, 112)
        Me.grpNewCanvas.TabIndex = 1
        Me.grpNewCanvas.TabStop = False
        Me.grpNewCanvas.Text = "New Canvas"
        Me.grpNewCanvas.Visible = False
        '
        'cobEnvironment
        '
        Me.cobEnvironment.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cobEnvironment.FormattingEnabled = True
        Me.cobEnvironment.Location = New System.Drawing.Point(53, 81)
        Me.cobEnvironment.Name = "cobEnvironment"
        Me.cobEnvironment.Size = New System.Drawing.Size(88, 21)
        Me.cobEnvironment.TabIndex = 5
        '
        'lblEnvironmentPrompt
        '
        Me.lblEnvironmentPrompt.AutoSize = True
        Me.lblEnvironmentPrompt.Location = New System.Drawing.Point(14, 84)
        Me.lblEnvironmentPrompt.Name = "lblEnvironmentPrompt"
        Me.lblEnvironmentPrompt.Size = New System.Drawing.Size(29, 13)
        Me.lblEnvironmentPrompt.TabIndex = 4
        Me.lblEnvironmentPrompt.Text = "Env:"
        '
        'lblCanvasSizePrompt
        '
        Me.lblCanvasSizePrompt.AutoSize = True
        Me.lblCanvasSizePrompt.Location = New System.Drawing.Point(14, 57)
        Me.lblCanvasSizePrompt.Name = "lblCanvasSizePrompt"
        Me.lblCanvasSizePrompt.Size = New System.Drawing.Size(33, 13)
        Me.lblCanvasSizePrompt.TabIndex = 3
        Me.lblCanvasSizePrompt.Text = "Size: "
        '
        'cobSize
        '
        Me.cobSize.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cobSize.FormattingEnabled = True
        Me.cobSize.Items.AddRange(New Object() {"Tiny", "Large", "Huge", "XXL"})
        Me.cobSize.Location = New System.Drawing.Point(53, 54)
        Me.cobSize.Name = "cobSize"
        Me.cobSize.Size = New System.Drawing.Size(88, 21)
        Me.cobSize.TabIndex = 2
        '
        'lblCanvasNamePrompt
        '
        Me.lblCanvasNamePrompt.AutoSize = True
        Me.lblCanvasNamePrompt.Location = New System.Drawing.Point(6, 30)
        Me.lblCanvasNamePrompt.Name = "lblCanvasNamePrompt"
        Me.lblCanvasNamePrompt.Size = New System.Drawing.Size(41, 13)
        Me.lblCanvasNamePrompt.TabIndex = 1
        Me.lblCanvasNamePrompt.Text = "Name: "
        '
        'txtName
        '
        Me.txtName.Location = New System.Drawing.Point(53, 28)
        Me.txtName.Name = "txtName"
        Me.txtName.Size = New System.Drawing.Size(88, 20)
        Me.txtName.TabIndex = 0
        '
        'grpLoadCanvas
        '
        Me.grpLoadCanvas.Controls.Add(Me.cobModEnv)
        Me.grpLoadCanvas.Controls.Add(Me.lblModEnvPrompt)
        Me.grpLoadCanvas.Controls.Add(Me.cobLoadName)
        Me.grpLoadCanvas.Controls.Add(Me.lblLoadNamePrompt)
        Me.grpLoadCanvas.Location = New System.Drawing.Point(115, 12)
        Me.grpLoadCanvas.Name = "grpLoadCanvas"
        Me.grpLoadCanvas.Size = New System.Drawing.Size(147, 112)
        Me.grpLoadCanvas.TabIndex = 1
        Me.grpLoadCanvas.TabStop = False
        Me.grpLoadCanvas.Text = "Load Canvas"
        '
        'cobModEnv
        '
        Me.cobModEnv.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cobModEnv.FormattingEnabled = True
        Me.cobModEnv.Location = New System.Drawing.Point(53, 54)
        Me.cobModEnv.Name = "cobModEnv"
        Me.cobModEnv.Size = New System.Drawing.Size(88, 21)
        Me.cobModEnv.TabIndex = 5
        '
        'lblModEnvPrompt
        '
        Me.lblModEnvPrompt.AutoSize = True
        Me.lblModEnvPrompt.Location = New System.Drawing.Point(14, 57)
        Me.lblModEnvPrompt.Name = "lblModEnvPrompt"
        Me.lblModEnvPrompt.Size = New System.Drawing.Size(29, 13)
        Me.lblModEnvPrompt.TabIndex = 4
        Me.lblModEnvPrompt.Text = "Env:"
        '
        'cobLoadName
        '
        Me.cobLoadName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cobLoadName.FormattingEnabled = True
        Me.cobLoadName.Location = New System.Drawing.Point(53, 27)
        Me.cobLoadName.Name = "cobLoadName"
        Me.cobLoadName.Size = New System.Drawing.Size(88, 21)
        Me.cobLoadName.TabIndex = 2
        '
        'lblLoadNamePrompt
        '
        Me.lblLoadNamePrompt.AutoSize = True
        Me.lblLoadNamePrompt.Location = New System.Drawing.Point(6, 30)
        Me.lblLoadNamePrompt.Name = "lblLoadNamePrompt"
        Me.lblLoadNamePrompt.Size = New System.Drawing.Size(41, 13)
        Me.lblLoadNamePrompt.TabIndex = 1
        Me.lblLoadNamePrompt.Text = "Name: "
        '
        'btnGo
        '
        Me.btnGo.Enabled = False
        Me.btnGo.Location = New System.Drawing.Point(12, 88)
        Me.btnGo.Name = "btnGo"
        Me.btnGo.Size = New System.Drawing.Size(92, 30)
        Me.btnGo.TabIndex = 2
        Me.btnGo.Text = "Go"
        Me.btnGo.UseVisualStyleBackColor = True
        '
        'LiberianInit
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(274, 129)
        Me.Controls.Add(Me.grpLoadCanvas)
        Me.Controls.Add(Me.btnGo)
        Me.Controls.Add(Me.grpNewCanvas)
        Me.Controls.Add(Me.grpLoadorNew)
        Me.Name = "LiberianInit"
        Me.Text = "LiberianInit"
        Me.grpLoadorNew.ResumeLayout(False)
        Me.grpLoadorNew.PerformLayout()
        Me.grpNewCanvas.ResumeLayout(False)
        Me.grpNewCanvas.PerformLayout()
        Me.grpLoadCanvas.ResumeLayout(False)
        Me.grpLoadCanvas.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents grpLoadorNew As System.Windows.Forms.GroupBox
    Friend WithEvents radLoadLevel As System.Windows.Forms.RadioButton
    Friend WithEvents radNewLevel As System.Windows.Forms.RadioButton
    Friend WithEvents grpNewCanvas As System.Windows.Forms.GroupBox
    Friend WithEvents lblCanvasSizePrompt As System.Windows.Forms.Label
    Friend WithEvents cobSize As System.Windows.Forms.ComboBox
    Friend WithEvents lblCanvasNamePrompt As System.Windows.Forms.Label
    Friend WithEvents txtName As System.Windows.Forms.TextBox
    Friend WithEvents cobEnvironment As System.Windows.Forms.ComboBox
    Friend WithEvents lblEnvironmentPrompt As System.Windows.Forms.Label
    Friend WithEvents grpLoadCanvas As System.Windows.Forms.GroupBox
    Friend WithEvents cobModEnv As System.Windows.Forms.ComboBox
    Friend WithEvents lblModEnvPrompt As System.Windows.Forms.Label
    Friend WithEvents cobLoadName As System.Windows.Forms.ComboBox
    Friend WithEvents lblLoadNamePrompt As System.Windows.Forms.Label
    Friend WithEvents btnGo As System.Windows.Forms.Button
End Class
