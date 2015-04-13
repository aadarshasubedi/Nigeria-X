Public Class LiberianInit
    Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        For Each foundFile As String In My.Computer.FileSystem.GetFiles("levels", FileIO.SearchOption.SearchAllSubDirectories, "*.lvl")
            Dim tempString As String = foundFile
            cobLoadName.Items.Add(foundFile.Remove(0, foundFile.LastIndexOf("\") + 1))
        Next
    End Sub
    Dim creationType As String = Nothing
    Dim loadedLevels(-1) As String

    Private Sub levelOptSelect(sender As System.Object, e As System.EventArgs) Handles radNewLevel.Click, radLoadLevel.Click
        If radLoadLevel.Checked = False Then
            makeUseable(grpNewCanvas, grpLoadCanvas)
            creationType = "load"
        ElseIf radNewLevel.Checked = False Then
            makeUseable(grpLoadCanvas, grpNewCanvas)
            creationType = "new"
        End If
    End Sub
    Function makeUseable(ByRef formFriend As Control, ByRef formEnemy As Control)
        formEnemy.Enabled = False
        formEnemy.Visible = False
        formEnemy.SendToBack()
        formFriend.Enabled = True
        formFriend.Visible = True
        formFriend.BringToFront()
        Return 0
    End Function

    Private Sub cobLoadName_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles cobLoadName.SelectedIndexChanged
        btnGo.Enabled = True
    End Sub

    Private Sub btnGo_Click(sender As Object, e As System.EventArgs) Handles btnGo.Click
        If creationType = "load" Then
            Dim editorWindow As New Liberian(cobLoadName.SelectedItem)
            editorWindow.Enabled = True
            editorWindow.Visible = True
            editorWindow.Activate()
        End If
    End Sub
End Class