Public Class landscape
    Dim ground(-1) As terrain
    Dim GFX As Graphics
    Dim terrainFile As String
    Dim terrainBrush(-1) As Image 'containing array for terrain types in /graphics/terrain directory
    Dim envDir As String    'environment is a tileset, sort of. this string stores the folder location as specified in caller
    Dim currentTerrain() As String 'array to contain terrain objects
    Sub New(canvas As Graphics, groundFile As String, environment As String)
        GFX = canvas
        terrainFile = groundFile
        populateterrainBrushes()
        buildTerrain(currentTerrain)
    End Sub

    Function populateterrainBrushes()
        Try
            'initialize terraintype array to contain any images found inside the /terrain/*environment* directory
            For Each foundFile As String In My.Computer.FileSystem.GetFiles("graphics/terrain/" & envDir & "/", FileIO.SearchOption.SearchAllSubDirectories, "*.png")
                MsgBox(foundFile)
                ReDim Preserve terrainBrush(terrainBrush.Length)
                terrainBrush(terrainBrush.Length - 1) = Image.FromFile(foundFile)
                terrainBrush(terrainBrush.Length - 1).Tag = foundFile
            Next
        Catch ex As Exception

        End Try
        Return 0
    End Function
    Function DrawMe()
        For Each tile As terrain In ground
            tile.entityPlace()
        Next
    End Function
    Function buildTerrain(ByVal terrainArray() As String)
        Static opened As Boolean = False
        If opened Then
        Else
            Dim terrainString As String = My.Computer.FileSystem.ReadAllText(terrainFile)
            MsgBox("the total terrain file is " & terrainString)
            terrainArray = Split(terrainString, ",")  'replace with terrainarray when possible
            For line As Integer = 0 To 2
                terrainArray(line) = Val(terrainArray(line)) 'the I/O method we use includes enters; must use Val to make pure num strings
                'MsgBox("We are currently on line " & line & " which is: " & terrainArray(line))
                For tile As Integer = 0 To terrainArray(line).Length - 1
                    'MsgBox("We are currently on tile " & tile & " of line " & line)
                    Dim consideredData As Integer = terrainArray(line).Substring(tile, 1)
                    For consideredBrush As Integer = 0 To terrainBrush.Length - 1
                        If consideredData = consideredBrush Then
                            Dim tblock As New terrain(GFX, terrainBrush(consideredBrush), tile * 32, line * 32)
                            tblock.staticSprite.Tag = terrainBrush(1).Tag
                            ReDim Preserve ground(ground.Length)
                            ground(ground.Length - 1) = tblock
                            ground(ground.Length - 1).staticSprite.Tag = terrainBrush(1).Tag
                        End If
                    Next
                Next
            Next
            opened = True
        End If
        Return 0
    End Function
    ReadOnly Property brushes As Array
        Get
            Return terrainBrush
        End Get
    End Property
    ReadOnly Property groundObjects As Array
        Get
            Return ground
        End Get
    End Property

End Class
