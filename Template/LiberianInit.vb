Public Class LiberianInit
    Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        For Each foundFile As String In My.Computer.FileSystem.GetFiles("levels", FileIO.SearchOption.SearchAllSubDirectories, "*.lvl")
            cobLoadName.Items.Add(foundFile.Remove(0, foundFile.LastIndexOf("\") + 1))
        Next
        For Each foundDir As String In My.Computer.FileSystem.GetDirectories("graphics/terrain", FileIO.SearchOption.SearchTopLevelOnly)
            cobEnvironment.Items.Add(foundDir.Remove(0, foundDir.LastIndexOf("\") + 1))
            cobModEnv.Items.Add(foundDir.Remove(0, foundDir.LastIndexOf("\") + 1))
        Next
    End Sub
    Dim creationType As String = Nothing
    Dim desiredSize As String
    'Dim loadedLevels(-1) As String

    Private Sub levelOptSelect(sender As System.Object, e As System.EventArgs) Handles radNewLevel.Click, radLoadLevel.Click
        If radLoadLevel.Checked = False Then
            makeUseable(grpNewCanvas, grpLoadCanvas)
            creationType = "new"
        ElseIf radNewLevel.Checked = False Then
            makeUseable(grpLoadCanvas, grpNewCanvas)
            creationType = "load"
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

    Private Sub SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles cobLoadName.SelectedIndexChanged, cobSize.SelectedIndexChanged, _
        cobEnvironment.SelectedIndexChanged
        'whenever one of the combo boxes are selected and used btngo should enable
        Dim whichCob As ComboBox = sender
        If whichCob.Name = "cobLoadName" Then
            btnGo.Enabled = True
        ElseIf whichCob.Name = "cobSize" And txtName.Text IsNot Nothing And cobEnvironment.SelectedItem IsNot Nothing Then
            btnGo.Enabled = True
            desiredSize = cobSize.SelectedItem
        ElseIf whichCob.Name = "cobEnvironment" And Not txtName.Text = "" And cobSize.SelectedItem IsNot Nothing Then
            btnGo.Enabled = True
            desiredSize = cobSize.SelectedItem
        End If

    End Sub

    Private Sub btnGo_Click(sender As Object, e As System.EventArgs) Handles btnGo.Click
        If creationType = "load" Then
            Dim itemLoc As String = "levels/" & cobLoadName.SelectedItem
            Dim lvlSize As String = My.Computer.FileSystem.ReadAllText(itemLoc)
            lvlSize = lvlSize.Substring(0, lvlSize.IndexOf(","))
            Dim editorWindow As New Liberian(itemLoc, cobModEnv.SelectedItem, lvlSize) 'replace with appropriate method to find size//
            editorWindow.Enabled = True
            editorWindow.Visible = True
            editorWindow.Activate()
            Me.Visible = False
        ElseIf creationType = "new" Then
            Dim newFile As String = "levels/" & txtName.Text & ".lvl"
            Dim starterScape(-1) As String
            Dim tempStr As String = ""
            populateScape(starterScape)
            tempStr &= cobSize.SelectedItem & "," & vbNewLine '& cobEnvironment.SelectedItem & "," & vbNewLine
            For xu As Integer = 0 To starterScape.Length - 1
                tempStr &= starterScape(xu) & vbNewLine
            Next
            My.Computer.FileSystem.WriteAllText(newFile, tempStr, False)
            Dim editorWindow As New Liberian(newFile, cobEnvironment.SelectedItem, cobSize.SelectedItem) 'replace with appropriate method to find size//
            editorWindow.Enabled = True
            editorWindow.Visible = True
            editorWindow.Activate()
            Me.Visible = False
        End If
    End Sub
    Private Sub LiberianInit_Disposed(sender As Object, e As EventArgs) Handles Me.Disposed
        frmMainMenu.wakeMenu()
    End Sub
    Private Sub populateScape(ByRef scape() As String)
        Dim tempString As String = ""
        Dim strLength As Integer = 0
        Dim writeFactor As Integer = 0
        Select Case desiredSize
            Case "Tiny"
                writeFactor = 8
            Case "Large"
                writeFactor = 16
            Case "Huge"
                writeFactor = 24
            Case "XXL"
                writeFactor = 64
        End Select

        For linecount As Integer = 0 To writeFactor - 1
            For tileCount As Integer = 0 To writeFactor - 1
                tempString &= 1
            Next
            ReDim Preserve scape(scape.Length)
            'MsgBox("scape " & scape.Length - 1 & " dimmed to " & tempString)
            scape(scape.Length - 1) = tempString & ","
            tempString = Nothing
        Next
    End Sub
End Class