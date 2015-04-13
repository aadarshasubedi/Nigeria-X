'John Riemers
'P5 Programming
'
'
Public Class Nigerian
    Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        DoubleBuffered = True
        SetStyle(ControlStyles.UserPaint, True)
        Me.KeyPreview = True    'enable keypress event handlers
        SetStyle(ControlStyles.AllPaintingInWmPaint, True)
        'set backbuffer bitmap to be the size of containing label
        BackBuffer = New Bitmap(level.Width, level.Height)
        'complicated: GFX is a graphics class originating on the backbuffer bitmap instead of the actual
        'surface of the control. We draw objects in GFX, which correlates to the BackBuffer variable
        'When it comes time to render, BackBuffer is then drawn to the form. This prevents flickering.
        GFX = Graphics.FromImage(BackBuffer)
        terraintype(0) = Nothing
        'initialize terraintype array to contain any images found inside the /terrain directory
        For Each foundFile As String In My.Computer.FileSystem.GetFiles("graphics/terrain", FileIO.SearchOption.SearchAllSubDirectories)
            ReDim Preserve terraintype(terraintype.Length)
            terraintype(terraintype.Length - 1) = Image.FromFile(foundFile)
        Next
        canvasX = (LEVELSCROLL * -1) - 1
        canvasY = (LEVELSCROLL * -1) - 1
        Call buildTerrain(currentTerrain)
    End Sub
    Structure levelBnd
        Dim vertical As Point
        Dim horizontal As Point
    End Structure
    Const LEVELSCROLL As Integer = 4
    Dim currentTerrain() As String 'array to contain terrain objects
    Dim levelTerraFile As String = "terraintest.lvl"    'pending string for lvl file reference on initialization
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

    Private Sub ExitToolStripMenuItem_Click(sender As Object, e As System.EventArgs) Handles ExitToolStripMenuItem.Click
        Me.Dispose()
    End Sub
    Sub updatelevelBounds()
        With levelBounds
            .horizontal = New Point(canvasX + BackBuffer.Width, 0)
            .vertical = New Point(0, canvasY + BackBuffer.Height)
        End With
    End Sub

    Private Sub Button1_Click(sender As Object, e As System.EventArgs) Handles Button1.Click
        Dim mainCanvas As Graphics = lblCanvas.CreateGraphics
        Dim enemy As New entity(GFX, "test", 42, 32)
        enemy.moveDirection = "E"
        enemy.MoveSpeed = 3
        enemy.move = True
        ReDim Preserve enemies(enemies.Length)
        enemies(enemies.Length - 1) = enemy
        Call enemy.entityPlace()

    End Sub

    Public Sub global_ticking(sender As Object, e As System.EventArgs) Handles globalTime.Tick
        Me.DoubleBuffered = True
        Dim ourCanvas As Graphics = lblCanvas.CreateGraphics
        ticks += 1
        'set gfx to plain current background for clean refresh
        GFX.DrawImage(level, 0, 0)
        'call the Place method of all objects intended to be visible to ensure that they show up
        'on this refresh
        For Each tile As entity In ground
            tile.entityPlace()
        Next
        For Each thing As entity In enemies
            thing.entityPlace()
            thing.entityMovement()
        Next
        updatelevelBounds()
        moveView()
        'draw completed scene on ourCanvas
        ourCanvas.DrawImage(BackBuffer, canvasX, canvasY)
    End Sub

    Private Sub Nigerian_Disposed(sender As Object, e As EventArgs) Handles Me.Disposed
        frmMainMenu.wakeMenu()
    End Sub
    Function buildTerrain(ByVal terrainArray() As String)
        Static opened As Boolean = False
        If opened Then
        Else
            Dim terrainString As String = My.Computer.FileSystem.ReadAllText(levelTerraFile)
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
                            'Call tblock.entityPlace()
                            ReDim Preserve ground(ground.Length)
                            ground(ground.Length - 1) = tblock
                        Case Is = 2
                            'MsgBox("terrainblock " & tile & " of line " & line & " is 2. setting to gravel")
                            Dim tblock As New terrain(GFX, terraintype(2), tile * 32, line * 32)
                            'Call tblock.entityPlace()
                            ReDim Preserve ground(ground.Length)
                            ground(ground.Length - 1) = tblock
                        Case Is = 3
                            'MsgBox("terrainblock " & tile & " of line " & line & " is 3. setting to water")
                            Dim tblock As New terrain(GFX, terraintype(3), tile * 32, line * 32)
                            'Call tblock.entityPlace()
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
End Class