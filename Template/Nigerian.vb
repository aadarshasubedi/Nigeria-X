'John Riemers
'P5 Programming
'
'
Public Class Nigerian
    Sub New(passedterrainfile As String, passedEnv As String, size As String, uplayer As String)

        ' This call is required by the designer.
        InitializeComponent()
        Me.Text = Me.Name
        ' Add any initialization after the InitializeComponent() call.
        ' DoubleBuffered = True
        'SetStyle(ControlStyles.UserPaint, True)
        'SetStyle(ControlStyles.AllPaintingInWmPaint, True)
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

        'the outer bounds of canvas movement, so dimmed as the SCROLLRATE const + 1 for avoid clipping
        canvasX = (SCROLLRATE * -1) - 1
        canvasY = (SCROLLRATE * -1) - 1

        ground = New landscape(GFX, passedterrainfile, passedEnv, levelSize)
        jPlayer = New player(GFX, uplayer, lblCanvas.Width / 2, lblCanvas.Height / 2)
        jPlayer.shouldCollide = True
        players(0) = jPlayer
        SCROLLRATE = jPlayer.MoveSpeed
        globalTime.Enabled = True
    End Sub
    Structure levelBnd
        Dim vertical As Point
        Dim horizontal As Point
    End Structure
    Dim jPlayer As player
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
    Dim players(0) As player
    Dim ground As landscape
    Dim BackBuffer As Bitmap 'collector bitmap as described above
    Dim SCROLLRATE As Integer
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
        enemy.shouldCollide = True
        enemy.moveDirection = "E"
        enemy.MoveSpeed = 3
        enemy.move = True
        ReDim Preserve enemies(enemies.Length)
        enemies(enemies.Length - 1) = enemy
        Call enemy.entityPlace()

    End Sub

    Private Sub global_ticking(sender As Object, e As System.EventArgs) Handles globalTime.Tick
        'Me.DoubleBuffered = True
        Dim ourCanvas As Graphics = lblCanvas.CreateGraphics
        ticks += 1
        'set gfx to plain current background for clean refresh
        GFX.DrawImage(level, 0, 0)
        'call the Place method of all objects intended to be visible to ensure that they show up
        'on this refresh
        ground.DrawMe()
        For Each block As terrain In ground.groundObjects
            block.calcCollision(enemies)
            block.calcCollision(players)
        Next
        jPlayer.calcCollision(ground.groundObjects)
        jPlayer.entityPlace()
        jPlayer.tryMove()
        For Each thing As entity In enemies
            'thing.calcCollision(ground.groundObjects)
            thing.entityPlace()
            thing.tryMove()
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

        Dim canvasCenter As New Point(((lblCanvas.Width / 2) - jPlayer.staticSprite.Width / 2) - canvasX, _
                                      ((lblCanvas.Height / 2) - jPlayer.staticSprite.Height) - canvasY)

        'this is called every tick/refresh to check if the scroll is moving and also constrain it
        'via canvasX/canvasY variables and the points contained in lblBND structure levelbounds

        'NEW: moveView actually keeps track of the location of the center of the label and compares it to the moving jPlayer entity
        'if it matches or comes close to it in traversing the level, we've converted the existing function to move the buffer bitmap
        'on occurence to the desired location

        'in this way the screen keeps with the player
        If canvasRight = True And jPlayer.locationX >= canvasCenter.X Then
            If levelBounds.horizontal.X - 32 > (lblCanvas.Location.X + lblCanvas.Width) Then
                canvasX -= SCROLLRATE
            End If
        End If
        If canvasLeft = True And jPlayer.locationX <= canvasCenter.X Then
            If canvasX + SCROLLRATE < 0 Then
                canvasX += SCROLLRATE
            End If
        End If
        If canvasUp = True And jPlayer.location.Y <= canvasCenter.Y Then
            If canvasY + SCROLLRATE < 0 Then
                canvasY += SCROLLRATE
            End If
        End If
        If canvasDown = True And jPlayer.location.Y >= canvasCenter.Y Then
            If levelBounds.vertical.Y - SCROLLRATE > (lblCanvas.Location.Y + lblCanvas.Height) Then
                canvasY -= SCROLLRATE
            End If
        End If
    End Sub

    Private Sub isViewMove(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        'check if keypress; if so, set movement to true
        jPlayer.move = True
        Select Case e.KeyCode
            Case Keys.A
                jPlayer.moveDirection = "W"
                canvasLeft = True
            Case Keys.D
                jPlayer.moveDirection = "E"
                canvasRight = True
            Case Keys.W
                jPlayer.moveDirection = "N"
                canvasUp = True
            Case Keys.S
                jPlayer.moveDirection = "S"
                canvasDown = True
        End Select
    End Sub
    Private Sub endViewMove(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyUp
        'on key release set movement to false
        Select Case e.KeyCode
            Case Keys.A
                If jPlayer.moveDirection = "W" Then
                    jPlayer.move = False
                End If
                'Call isViewMove(sender, e)
                canvasLeft = False
            Case Keys.D
                If jPlayer.moveDirection = "E" Then
                    jPlayer.move = False
                End If
                'Call isViewMove(sender, e)
                canvasRight = False
            Case Keys.W
                If jPlayer.moveDirection = "N" Then
                    jPlayer.move = False
                End If
                'Call isViewMove(sender, e)
                canvasUp = False
            Case Keys.S
                If jPlayer.moveDirection = "S" Then
                    jPlayer.move = False
                End If
                'Call isViewMove(sender, e)
                canvasDown = False
                'Case Else
                'MsgBox("WTF happened?")
        End Select
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        For Each enemy As entity In enemies
            enemy.moveDirection = "N"
        Next
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        For Each enemy As entity In enemies
            enemy.moveDirection = "S"
        Next
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        For Each enemy As entity In enemies
            enemy.moveDirection = "E"
        Next
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        For Each enemy As entity In enemies
            enemy.moveDirection = "W"
        Next
    End Sub
End Class