Public Class NigerianInit
    Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        For Each foundFile As String In My.Computer.FileSystem.GetFiles("levels", FileIO.SearchOption.SearchAllSubDirectories, "*.lvl")
            cobLevel.Items.Add(foundFile.Remove(0, foundFile.LastIndexOf("\") + 1))
        Next
        For Each foundobject As String In My.Computer.FileSystem.GetDirectories("graphics/characters", FileIO.SearchOption.SearchTopLevelOnly)
            cobPlayer.Items.Add(foundobject.Remove(0, foundobject.LastIndexOf("\") + 1))
        Next
        For Each foundDir As String In My.Computer.FileSystem.GetDirectories("graphics/terrain", FileIO.SearchOption.SearchTopLevelOnly)
            cobDiff.Items.Add(foundDir.Remove(0, foundDir.LastIndexOf("\") + 1))
        Next
    End Sub
    Dim player As String
    Dim level As String

    Private Sub Mw_Disposed(sender As Object, e As EventArgs) Handles Me.Disposed
        frmMainMenu.wakeMenu()
    End Sub

    Private Sub cobPlayer_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cobPlayer.SelectedIndexChanged
        player = "graphics/characters/" & cobPlayer.SelectedItem
        Dim playerAvatar As Image = Image.FromFile(player & "/" & cobPlayer.SelectedItem & "_south.png")
        If cobLevel.SelectedItem IsNot Nothing Then
            btnGO.Enabled = True
        End If
        Me.picPlayerPreview.Image = playerAvatar
    End Sub

    Private Sub cobLevel_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cobLevel.SelectedIndexChanged
        level = "levels/" & cobLevel.SelectedItem
        If cobPlayer.SelectedItem IsNot Nothing Then
            btnGO.Enabled = True
        End If
    End Sub

    Private Sub btnGO_Click(sender As Object, e As EventArgs) Handles btnGO.Click
        Dim lvlSize As String = My.Computer.FileSystem.ReadAllText(level)
        lvlSize = lvlSize.Substring(0, lvlSize.IndexOf(","))
        Dim newgame As New Nigerian(level, cobDiff.SelectedItem, lvlSize, cobPlayer.SelectedItem)
        newgame.Enabled = True
        newgame.Visible = True
        newgame.Activate()
        Me.Visible = False
    End Sub

End Class