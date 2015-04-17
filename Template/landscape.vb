Public Class landscape
    Public groundObjects(-1) As terrain
    Dim GFX As Graphics
    Dim terrainFile As String
    Dim ourSize As String
    Dim collideArray As String
    Dim terrainBrush(-1) As Image 'containing array for terrain types in /graphics/terrain directory
    Dim envDir As String    'environment is a tileset, sort of. this string stores the folder location as specified in caller
    Dim currentTerrain(-1) As String   'array to contain terrain objects
    Dim levelCollisiondata As String
    Sub New(canvas As Graphics, groundFile As String, environment As String, lvlsize As String)
        GFX = canvas
        If environment = Nothing Then
            envDir = "normal"
        Else
            envDir = environment
        End If

        ourSize = lvlsize
        terrainFile = groundFile
        populateterrainBrushes()
        buildTerrain(currentTerrain)
        If My.Computer.FileSystem.FileExists("graphics/terrain/" & envDir & "/passable.txt") Then
            levelCollisiondata = My.Computer.FileSystem.ReadAllText("graphics/terrain/" & envDir & "/passable.txt")
            makeCollidables()
        Else
            My.Computer.FileSystem.WriteAllText("graphics/terrain/" & envDir & "/passable.txt", "", False)
        End If
    End Sub

    Function populateterrainBrushes()

        'Dim cleanfoundFile As String
        Try
            'initialize terraintype array to contain any images found inside the /terrain/*environment* directory
            For Each foundFile As String In My.Computer.FileSystem.GetFiles("graphics/terrain/" & envDir, FileIO.SearchOption.SearchTopLevelOnly, "*.png")
                'MsgBox(foundFile)
                'cleanfoundFile = foundFile.Remove(0, foundFile.LastIndexOf("/"))
                'If levelCollisiondata.Contains(cleanfoundFile) Then
                'collideArray &= foundFile
                'End If
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
        Next
    End Function
    Function makeCollidables()
        Dim uncleantag As String
        For Each tile As terrain In groundObjects
            uncleantag = tile.staticSprite.Tag
            uncleantag = uncleantag.Remove(0, uncleantag.LastIndexOf("\") + 1)
            uncleantag = uncleantag.Remove(uncleantag.IndexOf("."), 4)
            'MsgBox(uncleantag)
            If levelCollisiondata.Contains(uncleantag) Then
                tile.shouldCollide = True
            End If
        Next
    End Function
    Private Function unDuckFile(ByRef duckedArray() As String)
        Dim factor As Integer
        Select Case ourSize
            Case "Tiny"
                factor = 8
            Case "Large"
                factor = 16
            Case "Huge"
                factor = 24
            Case "XXL"
                factor = 64
        End Select
        If duckedArray.Length - 1 > factor Then
            ReDim Preserve duckedArray(factor)
        End If
    End Function
    Sub buildTerrain(ByVal terrainArray() As String)
        Dim terrainString As String = My.Computer.FileSystem.ReadAllText(terrainFile)
        MsgBox("the total terrain file is " & terrainString)
        terrainArray = terrainString.Split(",") 'replace with terrainarray when possible
        unDuckFile(terrainArray)
        For line As Integer = 1 To (terrainArray.Length - 1)
            terrainArray(line) = Val(terrainArray(line)) 'the I/O method we use includes enters; must use Val to make pure num strings
            'MsgBox(newS)
            'terrainInteger = trimmer(terrainArray(line))
            'MsgBox("We are currently on line " & line & " which is: " & terrainArray(line))
            For tile As Integer = 0 To terrainArray(line).Length - 1
                Static goingAmt As Integer = 0
                Static keepgoing As Boolean = False
                Static offset As Boolean = False
                Static notation As Integer = Val(terrainArray(line).Substring(terrainArray(line).IndexOf("+") + 1, 2)) _
                                             - Val(terrainArray(line).IndexOf("E") - 1)
                'MsgBox(notation)
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
                    Dim statictile As String = terrainArray(line).Substring(tile - 1, 1)
                    keepgoing = True
                    Do While goingAmt < notation
                        consideredData = statictile
                        For consideredBrush As Integer = 0 To terrainBrush.Length - 1
                            If consideredData = consideredBrush Then

                                Dim locx As Integer = tile * 32
                                Dim locy As Integer = line * 32
                                If offset Then
                                    locx -= 32
                                End If
                                Using tblock As New terrain(GFX, terrainBrush(consideredBrush), locx, locy - 32)
                                    'MsgBox("tile " & tile & " which is " & consideredData & " of line " & line & "(" & terrainArray(line) & ")" & " has been recognized as " & terrainBrush(consideredBrush).Tag)
                                    tblock.staticSprite.Tag = terrainBrush(consideredBrush).Tag
                                    ReDim Preserve groundObjects(groundObjects.Length)
                                    groundObjects(groundObjects.Length - 1) = tblock
                                End Using
                            End If
                        Next
                        goingAmt += 1
                        tile += 1
                    Loop
                    keepgoing = False
                    Exit For
                Else
                    If keepgoing = False Then
                        For consideredBrush As Integer = 0 To terrainBrush.Length - 1
                            If consideredData = consideredBrush Then

                                Dim locx As Integer = tile * 32
                                Dim locy As Integer = line * 32
                                If offset Then
                                    locx -= 32
                                End If
                                Using tblock As New terrain(GFX, terrainBrush(consideredBrush), locx, locy - 32)
                                    'MsgBox("tile " & tile & " which is " & consideredData & " of line " & line & "(" & terrainArray(line) & ")" & " has been recognized as " & terrainBrush(consideredBrush).Tag)
                                    tblock.staticSprite.Tag = terrainBrush(consideredBrush).Tag
                                    ReDim Preserve groundObjects(groundObjects.Length)
                                    groundObjects(groundObjects.Length - 1) = tblock
                                End Using
                            End If
                        Next
                        goingAmt = 0
                    End If
                End If
            Next
        Next
    End Sub
    ReadOnly Property brushes As Array
        Get
            Return terrainBrush
        End Get
    End Property
    Function trimmer(ByRef refobject As String)
        Dim test As String = refobject
        Dim localbsremover As Integer
        'MsgBox(refobject)
        'MsgBox(test)
        ' Try
        For indice As Integer = 0 To refobject.Length - 1
            'MsgBox(refobject)
            'If refobject.Substring(indice, 1) = "." Then
            'MsgBox("skipping")
            'Continue For
            'ElseIf refobject.Chars(indice) = "E" Then
            'Dim locstring As Integer = refobject.Substring(indice + 2, 2)
            'For index As Integer = 0 To locstring
            'localbsremover &= refobject.Chars(indice - 1)
            'Next
            'MsgBox(localbsremover)
            'Else
            'localbsremover &= refobject.Chars(indice)
            'End If
            'MsgBox(localbsremover)
        Next
        'Catch ex As Exception

        'End Try

        Return test
    End Function
End Class
