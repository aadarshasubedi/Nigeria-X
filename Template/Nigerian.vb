'John Riemers
'P5 Programming
'
'
Public Class Nigerian
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
        globalTime.Enabled = True
    End Sub
    Structure levelBnd
        Dim vertical As Point
        Dim horizontal As Point
    End Structure
    Const LEVELSCROLL As Integer = 4
    Dim currentTerrain() As String 'array to contain terrain objects
    Dim terrainFile As String = Nothing   'pending string for lvl file reference on initialization
    Dim levelSize As String = Nothing
    Dim canvasbounds As Rectangle
    Dim levelBounds As levelBnd
    Dim ticks As Integer 'ticks passed; used to keep track of useful stuff
    Dim canvasX, canvasY As Integer
    Dim canvasRight, canvasLeft, canvasUp, canvasDown As Boolean
    Dim level As Image = My.Resources.back2 'overall level background behind terrain objects
    Dim enemies(-1) As entity
    Dim ground As landscape
    Dim BackBuffer As Bitmap 'collector bitmap as described above
    Public GFX As Graphics  'described in new constructor

    Private Sub ExitToolStripMenuItem_Click(sender As Object, e As System.EventArgs) Handles ExitToolStripMenuItem.Click
        Me.Dispose()
    End Sub
    Private Sub updatelevelBounds()
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

    Private Sub global_ticking(sender As Object, e As System.EventArgs) Handles globalTime.Tick
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
            thing.calcCollision(enemies)
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