Public Class Liberian
    Sub New(passedterrainfile As String, passedEnv As String, size As String)

        ' This call is required by the designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
        DoubleBuffered = True
        SetStyle(ControlStyles.UserPaint, True)
        SetStyle(ControlStyles.AllPaintingInWmPaint, True)
        Me.KeyPreview = True    'enable keypress event handlers

        terrainFile = passedterrainfile
        'set backbuffer bitmap to be the size of containing label
        levelSize = size
        levelEnv = passedEnv
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

        canvasX = (LEVELSCROLL * -1) - 1
        canvasY = (LEVELSCROLL * -1) - 1

        ground = New landscape(GFX, passedterrainfile, passedEnv, levelSize)
        populateBrushes()
        globalTime.Enabled = True
    End Sub
    Structure levelBnd
        Dim vertical As Point
        Dim horizontal As Point
    End Structure
    Const LEVELSCROLL As Integer = 4
    Private cursorPainter As Image = Nothing
    Dim terrainFile As String
    Dim ground As landscape
    Dim levelSize As String
    Dim levelEnv As String
    Dim canvasbounds As Rectangle
    Dim levelBounds As levelBnd
    'Dim terraintype(0) As Image 'containing array for terrain types in /graphics/terrain directory
    Dim ticks As Integer 'ticks passed; used to keep track of useful stuff
    Dim canvasX, canvasY As Integer
    Dim canvasRight, canvasLeft, canvasUp, canvasDown As Boolean
    Dim level As Image = My.Resources.back2 'overall level background behind terrain objects
    Dim enemies(-1) As entity
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
        ground.DrawMe()
        For Each block As terrain In ground.groundObjects
            block.calcCollision(enemies)
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
        If cursorPainter Is Nothing Then
            MsgBox("No brush selected!")
        Else
            For Each tile As terrain In ground.groundObjects
                'location is used to figure out which member of ground array to modify based on the for.. parse
                location += 1
                If tile.boundaries.IntersectsWith(New Rectangle(New Point(e.X - canvasX, e.Y - canvasY), New Size(1, 1))) Then
                    Dim tblock As New terrain(GFX, cursorPainter, tile.locationX, tile.locationY)
                    tblock.staticSprite.Tag = cursorPainter.Tag
                    ground.groundObjects(location) = tblock
                End If
            Next
        End If
    End Sub
    Private Function populateBrushes()
        'this attempts to pull members of the terraintype array, redeclare them as pictureboxes, and plop them into the brushes groupbox
        'additionally an event handler is added for click to each to set cursorPainter to their corresponding texture
        Dim grpBrushLoc As Integer = 25
        For tblock As Integer = 0 To ground.brushes.Length - 1
            Dim brushGUI As New PictureBox
            With brushGUI
                .Image = ground.brushes(tblock)
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
        'FileOpen(1, terrainFile, OpenMode.Output)
        Dim writeFactor As Integer = 0
        Dim conjunctString As String
        conjunctString = levelSize & "," & vbNewLine '& levelEnv & "," & vbNewLine
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
        For Each tile As terrain In ground.groundObjects
            'For currentTile As Integer = 0 To ground.groundObjects.Length - 1
            lineLength += 1
            For Brush As Integer = 0 To ground.brushes.Length - 1
                If tile.staticSprite.Tag = ground.brushes(Brush).Tag Then
                    'If ground.groundObjects(currentTile).staticSprite.Tag = ground.brushes(Brush).Tag Then
                    If lineLength = (writeFactor - 1) Then
                        conjunctString &= (Brush & "," & vbNewLine)
                        lineLength = 0
                    Else
                        conjunctString &= Brush
                    End If

                Else
                    'MsgBox("the tile tag " & tile.staticSprite.Tag & " is not equal to brush tag " & ground.brushes(Brush).tag)
                End If
            Next
        Next
        My.Computer.FileSystem.WriteAllText(terrainFile, conjunctString, False)
    End Sub
End Class