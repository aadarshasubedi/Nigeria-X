Public Class entity
    'the face we show to the world
    Private visObject As sprite
    'the location
    Private locObject As New Point
    Private moveRate As Integer = 2
    Private moveDir As Char = "N"
    Private moving As Boolean
    'bounds used for collision etc
    Private collide As Boolean = False
    Private bounds As New Rectangle
    'the graphics class to draw to
    Public pcanvas As Graphics
    Public Structure sprite
        Dim current As Bitmap
        Dim N As Bitmap
        Dim E As Bitmap
        Dim S As Bitmap
        Dim W As Bitmap
    End Structure
    Sub New(ByVal ovrCanvas As Graphics, name As String, locx As Integer, locy As Integer)
        'this sub is intended to be simpler - called using a name that correlates to a file directory which contains
        'files FOLLOWING NAMING CONVENTION ENTITYNAME_DIRECTION.PNG needed to fill sprite structure
        Try
            Dim operDir As String = "graphics/" & name
            With visObject
                .N = Image.FromFile(operDir & "/" & name & "_north.png")
                .E = Image.FromFile(operDir & "/" & name & "_east.png")
                .S = Image.FromFile(operDir & "/" & name & "_south.png")
                .W = Image.FromFile(operDir & "/" & name & "_west.png")
            End With
        Catch ex As Exception
            MsgBox("Something went wrong! Recheck image files related to " & name & " in /graphics/" & name)
        End Try
        locationX = locx
        locationY = locy
        pcanvas = ovrCanvas
        spriteDir(visObject)

        bounds.Location = locObject
        bounds.Width = visObject.current.Width
        bounds.Height = visObject.current.Height
    End Sub
    Sub New(ByVal ovrcanvas As Graphics, image As System.Drawing.Image, locx As Integer, locy As Integer)
        With visObject
            .N = New Bitmap(image)
            .E = New Bitmap(image)
            .S = New Bitmap(image)
            .W = New Bitmap(image)
        End With
        locationX = locx
        locationY = locy
        pcanvas = ovrcanvas
        spriteDir(visObject)

        bounds.Location = locObject
        bounds.Width = visObject.current.Width
        bounds.Height = visObject.current.Height
    End Sub
    Sub New(ByVal ovrcanvas As Graphics, image As System.Drawing.Image, locx As Integer, locy As Integer, speed As Integer)
        With visObject
            .N = New Bitmap(image)
            .E = New Bitmap(image)
            .S = New Bitmap(image)
            .W = New Bitmap(image)
        End With
        locationX = locx
        locationY = locy
        moveRate = speed
        pcanvas = ovrcanvas
        spriteDir(visObject)

        bounds.Location = locObject
        bounds.Width = visObject.current.Width
        bounds.Height = visObject.current.Height
    End Sub
    Sub New(ByVal ovrcanvas As Graphics, image As System.Drawing.Image, locx As Integer, locy As Integer, speed As Integer, orientation As Char)
        With visObject
            .N = New Bitmap(image)
            .E = New Bitmap(image)
            .S = New Bitmap(image)
            .W = New Bitmap(image)
        End With
        locationX = locx
        locationY = locy
        moveDirection = orientation
        moveRate = speed
        pcanvas = ovrcanvas
        spriteDir(visObject)

        bounds.Location = locObject
        bounds.Width = visObject.current.Width
        bounds.Height = visObject.current.Height
    End Sub
    'these constructors include specification of orientation sprites
    Sub New(ByVal ovrcanvas As Graphics, spriteNorth As Image, spriteEast As Image, spriteSouth As Image, _
            spriteWest As Image, locx As Integer, locy As Integer)
        With visObject
            .N = New Bitmap(spriteNorth)
            .E = New Bitmap(spriteEast)
            .S = New Bitmap(spriteSouth)
            .W = New Bitmap(spriteWest)
        End With
        locationX = locx
        locationY = locy
        pcanvas = ovrcanvas
        spriteDir(visObject)

        bounds.Location = locObject
        bounds.Width = visObject.current.Width
        bounds.Height = visObject.current.Height
    End Sub
    Sub New(ByVal ovrcanvas As Graphics, spriteNorth As Image, spriteEast As Image, spriteSouth As Image, _
            spriteWest As Image, locx As Integer, locy As Integer, speed As Integer)
        With visObject
            .N = New Bitmap(spriteNorth)
            .E = New Bitmap(spriteEast)
            .S = New Bitmap(spriteSouth)
            .W = New Bitmap(spriteWest)
        End With
        locationX = locx
        locationY = locy
        moveRate = speed
        pcanvas = ovrcanvas
        spriteDir(visObject)

        bounds.Location = locObject
        bounds.Width = visObject.current.Width
        bounds.Height = visObject.current.Height
    End Sub
    Sub New(ByVal ovrcanvas As Graphics, spriteNorth As Image, spriteEast As Image, spriteSouth As Image, _
            spriteWest As Image, locx As Integer, locy As Integer, speed As Integer, orientation As Char)
        With visObject
            .N = New Bitmap(spriteNorth)
            .E = New Bitmap(spriteEast)
            .S = New Bitmap(spriteSouth)
            .W = New Bitmap(spriteWest)
        End With
        locationX = locx
        locationY = locy
        moveRate = speed
        pcanvas = ovrcanvas
        spriteDir(visObject)

        bounds.Location = locObject
        bounds.Width = visObject.current.Width
        bounds.Height = visObject.current.Height
    End Sub
    Sub entityPlace()
        visObject.current.MakeTransparent(Color.White)
        spriteDir(visObject)
        If moving Then
            pcanvas.DrawImage(visObject.current, locObject)
            bounds.Location = locObject
            Select Case moveDir
                Case "N"
                    pcanvas.DrawImage(visObject.current, locObject.X, locObject.Y + 1)
                Case "E"
                    pcanvas.DrawImage(visObject.current, locObject.X - 1, locObject.Y)
                Case "S"
                    pcanvas.DrawImage(visObject.current, locObject.X, locObject.Y - 1)
                Case "W"
                    pcanvas.DrawImage(visObject.current, locObject.X + 1, locObject.Y)
            End Select
        Else
            pcanvas.DrawImage(visObject.current, locObject)
        End If
    End Sub
    'Function calcCollision(ByVal objects() As entity)
    'For Each member As entity In objects
    ' If member.shouldCollide = True Then
    ' Dim memberestColl As Rectangle = member.boundaries
    'memberestColl.Inflate(member.MoveSpeed, member.MoveSpeed)
    'If Me.boundaries.IntersectsWith(memberestColl) Then
    ' member.move = False
    'End If
    'End If
    'Next
    'End Function
    Sub entityMovement()
        moving = True
        Select Case moveDir
            Case "N"
                locObject.Y -= moveRate
            Case "E"
                locObject.X += moveRate
            Case "S"
                locObject.Y += moveRate
            Case "W"
                locObject.X -= moveRate
        End Select
        Call entityPlace()
    End Sub
    Sub Dispose()
        Me.Dispose()
    End Sub

    Private Function spriteDir(ByRef target As sprite)
        'this function checks to realign the sprite to the direction we want it
        Select Case Me.moveDir
            Case "N"
                target.current = target.N
            Case "E"
                target.current = target.E
            Case "S"
                target.current = target.S
            Case "W"
                target.current = target.W
        End Select
        Return 0
    End Function


    Property locationX As Integer
        Get
            Return Val(locObject.X)
        End Get
        Set(value As Integer)
            locObject.X = value
        End Set
    End Property
    Property locationY As Integer
        Get
            Return Val(locObject.Y)
        End Get
        Set(value As Integer)
            locObject.Y = value
        End Set
    End Property
    Property location As Point
        Get
            Return locObject
        End Get
        Set(value As Point)
            locObject = value
        End Set
    End Property
    Property MoveSpeed As Integer
        Get
            Return moveRate
        End Get
        Set(value As Integer)
            moveRate = value
        End Set
    End Property
    Property moveDirection As Char
        Get
            Return moveDir
        End Get
        Set(value As Char)
            moveDir = value
        End Set
    End Property
    Property move As Boolean
        Get
            Return moving
        End Get
        Set(value As Boolean)
            moving = value
        End Set
    End Property
    Property boundaries As Rectangle
        Get
            Return bounds
        End Get
        Set(value As Rectangle)
            bounds = value
        End Set
    End Property
    Property staticSprite As Image
        Get
            Return visObject.current
        End Get
        Set(value As Image)
            visObject.current = value
        End Set
    End Property
    Property shouldCollide As Boolean
        Get
            Return collide
        End Get
        Set(value As Boolean)
            collide = value
        End Set
    End Property
End Class
