Public Class Liberian
    Sub New(passedterrainfile As String, size As String)

        ' This call is required by the designer.
        InitializeComponent()
        terrainFile = passedterrainfile
        ' Add any initialization after the InitializeComponent() call.
        DoubleBuffered = True
        SetStyle(ControlStyles.UserPaint, True)
        Me.KeyPreview = True    'enable keypress event handlers
        SetStyle(ControlStyles.AllPaintingInWmPaint, True)
        'set backbuffer bitmap to be the size of containing label
        levelSize = size
        Select Case levelSize
            Case "Tiny"
                BackBuffer = New Bitmap(256, 256)
            Case "Large"
                BackBuffer = New Bitmap(512, 512)
            Case "Huge"
                BackBuffer = New Bitmap(768, 768)
            Case "XXL"
                BackBuffer = New Bitmap(2048, 2048)
        End Select

        'complicated: GFX is a graphics class originating on the backbuffer bitmap instead of the actual
        'surface of the control. We draw objects in GFX, which correlates to the BackBuffer variable
        'When it comes time to render, BackBuffer is then drawn to the form. This prevents flickering.
        GFX = Graphics.FromImage(BackBuffer)
        terraintype(0) = Nothing
        'initialize terraintype array to contain any images found inside the /terrain directory
        Try
            For Each foundFile As String In My.Computer.FileSystem.GetFiles("graphics/terrain", FileIO.SearchOption.SearchAllSubDirectories)
                ReDim Preserve terraintype(terraintype.Length)
                terraintype(terraintype.Length - 1) = Image.FromFile(foundFile)
                terraintype(terraintype.Length - 1).Tag = foundFile
            Next
        Catch ex As Exception

        End Try
       
        canvasX = (LEVELSCROLL * -1) - 1
        canvasY = (LEVELSCROLL * -1) - 1
        cursorPainter = Nothing
        populateBrushes()
        Call buildTerrain(currentTerrain)
    End Sub
    Structure levelBnd
        Dim vertical As Point
        Dim horizontal As Point
    End Structure
    Const LEVELSCROLL As Integer = 4
    Private cursorPainter As Image
    Dim terrainFile As String
    Dim levelSize As String
    Dim currentTerrain() As String 'array to contain terrain objects
    Dim canvasbounds As Rectangle
    Dim levelBounds As levelBnd
    Dim terraintype(0) As Image 'containing array for terrain types in /graphics/terrain directory
    Dim ticks As Integer 'ticks passed; used to keep track of useful stuff
    Dim canvasX, canvasY As Integer
    Dim canvasRight, canvasLeft, canvasUp, canvasDown As Boolean
    Dim level As Image = My.Resources.back2 'overall level background behind terrain objects
    Dim enemies(-1) As entity
    Dim ground(-1) As terrain
    Dim BackBuffer As Bitmap 'collector bitmap as described above
    Public GFX As Graphics  'described in new constructor

    Private Sub ExitToolStripMenuItem_Click(sender As Object, e As System.EventArgs)
        Me.Dispose()
    End Sub
    Sub updatelevelBounds()
        With levelBounds
            .horizontal = New Point(canvasX + BackBuffer.Width, 0)
            .vertical = New Point(0, canvasY + BackBuffer.Height)
        End With
    End Sub
    Public Sub global_ticking(sender As Object, e As System.EventArgs) Handles globalTime.Tick
        Me.DoubleBuffered = True
        Dim ourCanvas As Graphics = lblCanvas.CreateGraphics
        ticks += 1
        'set gfx to plain current background for clean refresh
        GFX.DrawImage(level, 0, 0)
        'call the Place method of all objects intended to be visible to ensure that they show up
        'on this refresh
        For Each tile As terrain In ground
            tile.entityPlace()
        Next
        For Each thing As entity In enemies
            thing.entityPlace()
        Next
        updatelevelBounds()
        moveView()
        'draw completed scene on ourCanvas
        ourCanvas.DrawImage(BackBuffer, canvasX, canvasY)
    End Sub

    Private Sub Liberian_Disposed(sender As Object, e As EventArgs) Handles Me.Disposed
        frmMainMenu.wakeMenu()
    End Sub
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
                    Select Case terrainArray(line).Substring(tile, 1)
                        Case Is = 1
                            'MsgBox("terrainblock " & tile & " of line " & line & " is 1. setting to grass")
                            Dim tblock As New terrain(GFX, terraintype(1), tile * 32, line * 32)
                            tblock.staticSprite.Tag = terraintype(1).Tag
                            ReDim Preserve ground(ground.Length)
                            ground(ground.Length - 1) = tblock
                            ground(ground.Length - 1).staticSprite.Tag = terraintype(1).Tag
                        Case Is = 2
                            'MsgBox("terrainblock " & tile & " of line " & line & " is 2. setting to gravel")
                            Dim tblock As New terrain(GFX, terraintype(2), tile * 32, line * 32)
                            tblock.staticSprite.Tag = terraintype(2).Tag
                            ReDim Preserve ground(ground.Length)
                            ground(ground.Length - 1) = tblock
                        Case Is = 3
                            'MsgBox("terrainblock " & tile & " of line " & line & " is 3. setting to water")
                            Dim tblock As New terrain(GFX, terraintype(3), tile * 32, line * 32)
                            tblock.staticSprite.Tag = terraintype(3).Tag
                            ReDim Preserve ground(ground.Length)
                            ground(ground.Length - 1) = tblock
                        Case Else
                            MsgBox("we are missing something")
                    End Select
                Next
            Next
            opened = True
        End If
        Return 0
    End Function
    Private Sub moveView()
        'this is called every tick/refresh to check if the scroll is moving and also constrain it
        'via canvasX/canvasY variables and the points contained in lblBND structure levelbounds
        If canvasRight = True Then
            If levelBounds.horizontal.X - LEVELSCROLL > (lblCanvas.Location.X + lblCanvas.Width) Then
                canvasX -= LEVELSCROLL
            End If
        End If
        If canvasLeft = True Then
            If canvasX + LEVELSCROLL < 0 Then
                canvasX += LEVELSCROLL
            End If
        End If
        If canvasUp = True Then
            If canvasY + LEVELSCROLL < 0 Then
                canvasY += LEVELSCROLL
            End If
        End If
        If canvasDown = True Then
            If levelBounds.vertical.Y - LEVELSCROLL > (lblCanvas.Location.Y + lblCanvas.Height) Then
                canvasY -= LEVELSCROLL
            End If
        End If
    End Sub

    Private Sub isViewMove(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        'check if keypress; if so, set movement to true
        Select Case e.KeyCode
            Case Keys.A
                canvasLeft = True
            Case Keys.D
                canvasRight = True
            Case Keys.W
                canvasUp = True
            Case Keys.S
                canvasDown = True
        End Select
    End Sub
    Private Sub endViewMove(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyUp
        'on key release set movement to false
        Select Case e.KeyCode
            Case Keys.A
                canvasLeft = False
            Case Keys.D
                canvasRight = False
            Case Keys.W
                canvasUp = False
            Case Keys.S
                canvasDown = False
                'Case Else
                'MsgBox("WTF happened?")
        End Select
    End Sub

    'reserve for level editor
    Private Function zoomLevel(ByVal out As Boolean)
        Static zoom As Integer = 3
        If out = True Then
            Select Case zoom
                Case 3
                    zoom = 2
                    BackBuffer.SetResolution(BackBuffer.Width / 2, BackBuffer.Height / 2)
                Case 2
                    zoom = 1
                    BackBuffer.SetResolution(BackBuffer.Width / 2, BackBuffer.Height / 2)
            End Select
        ElseIf out = False Then
            Select Case zoom
                Case 3
                Case 2
            End Select
        End If

    End Function
    
    Private Sub lblCanvas_MouseDown(sender As Object, e As MouseEventArgs) Handles lblCanvas.MouseDown
        'if mouse is clicked on canvas, check each member of ground array for intersections. If so, set affected tile image to that
        'provided by cursorpainter variable
        Dim location As Integer = -1
        For Each tile As terrain In ground
            'location is used to figure out which member of ground array to modify based on the for.. parse
            location += 1
            If tile.boundaries.IntersectsWith(New Rectangle(e.Location, New Size(1, 1))) Then
                Try
                    ground(location) = New terrain(GFX, cursorPainter, tile.locationX, tile.locationY)
                Catch ex As Exception
                    MsgBox("No brush selected!")
                End Try
            End If
        Next tile
    End Sub
    Private Function populateBrushes()
        'this attempts to pull members of the terraintype array, redeclare them as pictureboxes, and plop them into the brushes groupbox
        'additionally an event handler is added for click to each to set cursorPainter to their corresponding texture
        Dim grpBrushLoc As Integer = 25
        For tblock As Integer = 1 To terraintype.Length - 1
            Dim brushGUI As New PictureBox
            With brushGUI
                .Image = terraintype(tblock)
                .SizeMode = PictureBoxSizeMode.StretchImage
                .Size = New Size(64, 64)
                .Parent = grpBrushes
            End With
            AddHandler brushGUI.Click, AddressOf selectBrush
            brushGUI.Location = New Point(10, grpBrushLoc)
            grpBrushLoc += 65
        Next
        Return 0
    End Function
    Private Sub selectBrush(sender, e)
        Dim item As PictureBox = sender
        cursorPainter = item.Image
    End Sub
    Private Sub writeToFile(sender As Object, e As System.EventArgs) Handles SaveToolStripMenuItem.Click
        FileOpen(1, terrainFile, OpenMode.Output)
        Dim writeFactor As Integer = 0
        Dim conjunctString As String = ""
        Select Case levelSize
            Case "Tiny"
                writeFactor = 8
            Case "Large"
                writeFactor = 16
            Case "Huge"
                writeFactor = 24
            Case "XXL"
                writeFactor = 64
        End Select
        Dim lineLength As Integer = 0
        For Each tile As terrain In ground
            lineLength += 1
            For Brush As Integer = 1 To terraintype.Length - 1
                If tile.staticSprite.Tag = terraintype(Brush).Tag Then
                    If lineLength = writeFactor Then
                        Write(1, Brush & "," & vbNewLine)
                        lineLength = 0
                    Else
                        Write(1, Brush)
                    End If
                End If
            Next
        Next
    End Sub
End Class