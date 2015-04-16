Public Class frmMainMenu
    Public menuActive As Boolean
    Private Sub item_select(sender As Object, e As System.EventArgs) Handles btnEditor.Click, btnExit.Click, btnNewGame.Click, btnOptions.Click
        Dim selectedObject As Button = sender
        Select Case selectedObject.Tag
            Case 1
                Dim newWindow As New NigerianInit
                'newWindow.Name = "Game"
                phoenixifyMenu(newWindow)
            Case 2
                Dim newWindow As New LiberianInit
                phoenixifyMenu(newWindow)
            Case 3
            Case 4
                Application.Exit()
        End Select
    End Sub
    Function phoenixifyMenu(window As Form)
        window.Visible = True
        window.Enabled = True
        window.Focus()
        window.Activate()
        window.Text = window.Name
        menuActive = False
        Me.Visible = False
        Me.Enabled = False
        Return 0
    End Function
    Function wakeMenu()
        Me.Visible = True
        Me.Enabled = True
        Return menuActive = True
    End Function
End Class