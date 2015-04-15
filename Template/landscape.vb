Public Class landscape
    Public groundObjects(-1) As terrain
    Dim GFX As Graphics
    Dim terrainFile As String
    Dim ourSize As String
    Dim terrainBrush(-1) As Image 'containing array for terrain types in /graphics/terrain directory
    Dim envDir As String    'environment is a tileset, sort of. this string stores the folder location as specified in caller
    Dim currentTerrain(-1) As String   'array to contain terrain objects
    Sub New(canvas As Graphics, groundFile As String, environment As String, lvlsize As String)
        GFX = canvas
        ourSize = lvlsize
        terrainFile = groundFile
        populateterrainBrushes()
        buildTerrain(currentTerrain)
    End Sub

    Function populateterrainBrushes()
        Try
            'initialize terraintype array to contain any images found inside the /terrain/*environment* directory
            For Each foundFile As String In My.Computer.FileSystem.GetFiles("graphics/terrain/" & envDir, FileIO.SearchOption.SearchTopLevelOnly, "*.png")
                'MsgBox(foundFile)
                ReDim Preserve terrainBrush(terrainBrush.Length)
                terrainBrush(terrainBrush.Length - 1) = Image.FromFile(foundFile)
                terrainBrush(terrainBrush.Length - 1).Tag = foundFile
            Next
        Catch ex As Exception
            MsgBox("Low on memory sire!")
        End Try
        Return 0
    End Function
    Function DrawMe()
        For Each tile As terrain In groundObjects
            tile.entityPlace()
            If tile.staticSprite.Tag = terrainBrush(2).Tag Then
                tile.shouldCollide = True
            End If
        Next
    End Function
    Sub buildTerrain(ByVal terrainArray() As String)
        Dim terrainString As String = My.Computer.FileSystem.ReadAllText(terrainFile)
        MsgBox("the total terrain file is " & terrainString)
        terrainArray = terrainString.Split(",") 'replace with terrainarray when possible
        For line As Integer = 1 To (terrainArray.Length - 1)
            terrainArray(line) = Val(terrainArray(line)) 'the I/O method we use includes enters; must use Val to make pure num strings
            'terrainInteger = trimmer(terrainArray(line))
            'MsgBox("We are currently on line " & line & " which is: " & terrainArray(line))
            For tile As Integer = 0 To terrainArray(line).Length - 1
                Static offset As Boolean = False
                If tile = 0 Then
                    offset = False
                End If
                'MsgBox("We are currently on tile " & tile & " of line " & line)
                'MsgBox("There are " & terrainArray(line).Length - 1 & "tiles in this line, which is " & terrainArray(line))
                Dim consideredData As String = terrainArray(line).Substring(tile, 1)
                If consideredData.Contains(".") Then
                    offset = True
                    Continue For
                ElseIf consideredData.Contains("E") Then
                    Exit For
                End If
                For consideredBrush As Integer = 0 To terrainBrush.Length - 1
                    If consideredData = consideredBrush Then

                        Dim locx As Integer = tile * 32
                        Dim locy As Integer = line * 32
                        If offset Then
                            locx -= 32
                        End If
                        Dim tblock As New terrain(GFX, terrainBrush(consideredBrush), locx, locy - 32)
                        'MsgBox("tile " & tile & " which is " & consideredData & " of line " & line & "(" & terrainArray(line) & ")" & " has been recognized as " & terrainBrush(consideredBrush).Tag)
                        tblock.staticSprite.Tag = terrainBrush(consideredBrush).Tag
                        ReDim Preserve groundObjects(groundObjects.Length)
                        groundObjects(groundObjects.Length - 1) = tblock
                    End If
                Next
            Next
        Next
    End Sub
    ReadOnly Property brushes As Array
        Get
            Return terrainBrush
        End Get
    End Property
    Function trimmer(ByRef refobject As String)
        Dim localbsremover As Integer
        Try
            refobject.Remove(refobject.IndexOf("."), 1)
            refobject.Remove(refobject.IndexOf("E"), 5)
            localbsremover = refobject
        Catch ex As Exception

        End Try

        Return localbsremover
    End Function
End Class
