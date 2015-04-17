Public Class tilesettings
    Dim localTileArray(-1)
    Dim trueEnviron As String
    Sub New(consideredItems() As Image, environ As String)
        InitializeComponent()
        Dim consideredString As String = ""

        For Each derp As Image In consideredItems
            consideredString = derp.Tag
            consideredString = consideredString.Substring(consideredString.LastIndexOf("\") + 1)
            ReDim Preserve localTileArray(localTileArray.Length)
            localTileArray(localTileArray.Length - 1) = consideredString
            'MsgBox(localTileArray(localTileArray.Length - 1))
            If environ = Nothing Then trueEnviron = "normal" Else trueEnviron = environ
            Me.Text = "Tileset " & trueEnviron
            trueEnviron = "graphics/terrain/" & trueEnviron & "/passable.txt"
            captainPicard()
        Next
    End Sub

    Private Sub captainPicard()
        Dim makeitSo As String = My.Computer.FileSystem.ReadAllText(trueEnviron)
        For index As Integer = 0 To localTileArray.Length - 1
            Dim checker As New CheckBox()
            With checker
                .Parent = panSettings
                .Location = New Point(5, index * checker.Height)
                .Text = localTileArray(index)
            End With
            If makeitSo.Contains(localTileArray(index)) Then
                checker.Checked = True
            End If
        Next
    End Sub

    Private Sub btnApply_Click(sender As Object, e As EventArgs) Handles btnApply.Click
        Dim passingFile As String = ""
        For Each chesspiece As CheckBox In panSettings.Controls
            If chesspiece.Checked = True Then
                passingFile &= chesspiece.Text & vbNewLine
                MsgBox("writing " & chesspiece.Text & " to file")
            ElseIf chesspiece.Checked = False Then
            End If
            My.Computer.FileSystem.WriteAllText(trueEnviron, passingFile, False)
        Next
    End Sub
End Class