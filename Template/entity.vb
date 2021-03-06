﻿Public Class entity
    'Implements System.IDisposable
    Implements IDisposable
    'the face we show to the world
    Private visObject As sprite
    'the location
    Private locObject As Point
    Private moveRate As Integer = 2
    Private moveDir As Char = "N"
    Private moving As Boolean
    'bounds used for collision etc
    Public graceDir As String = ""
    Public health As Integer = 30
    Public isColliding As Boolean = False
    Public collisions() As entity
    Public storedDir As Char = "Q"
    Public collisionHandled As Boolean = False
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
        'note : when constructing name must include subordinate file structure ie "/characters" & name (where name = bill)
        'this sub is intended to be simpler - called using a name that correlates to a file directory which contains
        'files FOLLOWING NAMING CONVENTION ENTITYNAME_DIRECTION.PNG needed to fill sprite structure
        Dim operDir As String = "graphics/" & name
        Try
            With visObject
                .N = Image.FromFile(operDir & "/" & name.Substring(name.LastIndexOf("/")) & "_north.png")
                .N.SetResolution(32, 32)
                .E = Image.FromFile(operDir & "/" & name.Substring(name.LastIndexOf("/")) & "_east.png")
                .E.SetResolution(32, 32)
                .S = Image.FromFile(operDir & "/" & name.Substring(name.LastIndexOf("/")) & "_south.png")
                .S.SetResolution(32, 32)
                .W = Image.FromFile(operDir & "/" & name.Substring(name.LastIndexOf("/")) & "_west.png")
            End With
        Catch ex As Exception
            MsgBox("Something went wrong! Recheck image files related to " & name.Substring(name.LastIndexOf("/")) & " in /graphics/" & name)
        End Try
        locationX = locx
        locationY = locy
        pcanvas = ovrCanvas
        spriteDir(visObject)

        bounds.Location = locObject
        bounds.Width = visObject.current.Width
        bounds.Height = visObject.current.Height
        storedDir = moveDir
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
            'pcanvas.DrawImage(visObject.current, locObject)
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
    Function calcCollision1(ByVal objects() As entity)
        collisions = objects
        For Each member As entity In objects
            If Me.shouldCollide And member.shouldCollide Then
                Dim memberestColl As Rectangle = member.boundaries
                memberestColl.Inflate(0, 0)
                If member.isColliding Then
                Else
                    If member.collisionHandled And (member.graceDir.Contains(member.moveDirection)) And Not member.moveDirection = member.storedDir Then
                        If Me.boundaries.IntersectsWith(memberestColl) Then
                            'MsgBox("found")
                            member.isColliding = True
                            'member.collisionHandled = False
                            member.storedDir = member.moveDirection
                        End If
                    Else
                        If Me.boundaries.IntersectsWith(memberestColl) Then
                            member.isColliding = True
                            'MsgBox("collision with " & member.locationX)
                            member.storedDir = member.moveDirection
                            'MsgBox("storedDir set to current direction " & Me.storedDir)
                            'If member.collisionHandled = False Then
                            'ReDim Preserve member.collisions(member.collisions.Length)
                            'member.isColliding = True
                            'member.collisions(member.collisions.Length - 1) = Me
                        End If
                    End If
                End If
            End If
        Next
        'Return isColliding
    End Function
    Sub tryMove1()
        Static realMove As Integer = Me.MoveSpeed
        If Me.isColliding Then
            If Me.moveDir = Me.storedDir Then
                'MsgBox("stoppingmovement")
                Me.MoveSpeed = 0
            Else
                Me.collisionHandled = True
                'Me.storedDir = Me.moveDir
                Select Case Me.moveDir
                    Case "N"
                        graceDir = "N"
                        If Me.storedDir = "E" Then
                            Me.locationX -= Me.staticSprite.Width / 6
                        ElseIf Me.storedDir = "W" Then
                            Me.locationX += Me.staticSprite.Width / 6
                        ElseIf Me.storedDir = "S" Then
                            Me.locationY -= Me.staticSprite.Width / 6
                        End If
                    Case "S"
                        graceDir = "S"
                        If Me.storedDir = "E" Then
                            Me.locationX -= Me.staticSprite.Width / 6
                        ElseIf Me.storedDir = "W" Then
                            Me.locationX += Me.staticSprite.Width / 6
                        ElseIf Me.storedDir = "N" Then
                            Me.locationY += Me.staticSprite.Width / 6
                        End If
                    Case "E"
                        graceDir = "E"
                        If Me.storedDir = "S" Then
                            Me.locationY -= Me.staticSprite.Width / 6
                        ElseIf Me.storedDir = "N" Then
                            Me.locationY += Me.staticSprite.Width / 6
                        ElseIf Me.storedDir = "W" Then
                            Me.locationX += Me.staticSprite.Width / 6
                        End If

                    Case "W"
                        graceDir = "W"
                        If Me.storedDir = "S" Then
                            Me.locationY -= Me.staticSprite.Width / 6
                        ElseIf Me.storedDir = "N" Then
                            Me.locationY += Me.staticSprite.Width / 6
                        ElseIf Me.storedDir = "E" Then
                            Me.locationX -= Me.staticSprite.Width / 6
                        End If
                End Select
                Me.MoveSpeed = realMove
                Me.isColliding = False
            End If
            'entityMovement()
        Else
            entityMovement()
        End If

    End Sub
    Function calcCollision(ByVal objects() As entity)
        collisions = objects
        For Each member As entity In objects
            If Me.shouldCollide And member.shouldCollide Then
                Dim meColl As Rectangle = Me.boundaries
                'If Me.moveDir <> Me.storedDir Then
                'Else
                Select Case Me.moveDir
                    Case "N"
                        meColl.Offset(0, Me.MoveSpeed * -1)
                        Me.storedDir = "N"
                    Case "S"
                        meColl.Offset(0, Me.MoveSpeed)
                        Me.storedDir = "S"
                    Case "E"
                        meColl.Offset(Me.MoveSpeed, 0)
                        Me.storedDir = "E"
                    Case "W"
                        meColl.Offset(Me.MoveSpeed * -1, 0)
                        Me.storedDir = "W"

                End Select
                If meColl.IntersectsWith(member.boundaries) Then
                    'MsgBox("collision")
                    Me.isColliding = True
                    Exit For
                Else
                    'MsgBox("no collision")
                    Me.isColliding = False
                End If
            End If
            'End If
        Next
    End Function
    Sub tryMove()
        If Me.isColliding = True Then
        Else
            entityMovement()
        End If

    End Sub
    Sub entityMovement()
        If moving = True Then
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
        End If
        'Call entityPlace()
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
        visObject.current.MakeTransparent(Color.White)
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

    Public Sub Dispose1() Implements IDisposable.Dispose

    End Sub
End Class
