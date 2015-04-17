Public Class equipment
    Implements IDisposable
    Dim location As Point
    Dim appliedCanvas As Graphics
    Dim weptype As String
    Sub New(locX As Integer, locY As Integer, canvas As Graphics, equiptype As String)
        location.X = locX
        location.Y = locY
        appliedCanvas = canvas
        weptype = equiptype
    End Sub
    Public Sub Fire(caller As Nigerian, callingEntity As entity, locX As Integer, locY As Integer, dir As Char)
        Select Case dir
            Case "N"
                location.Y -= callingEntity.staticSprite.Height / 2 '+ 'callingEntity.staticSprite.Height / 2
                location.X += 5
            Case "S"
                location.Y += callingEntity.staticSprite.Height
                location.X += 10
            Case "E"
                'location.Y = locY + 20
                location.X += callingEntity.staticSprite.Width
            Case "W"
                'location.Y = locY
                location.X -= callingEntity.staticSprite.Width / 2 '+ callingEntity.staticSprite.Height / 2
        End Select
        Dim bullet As New projectile(appliedCanvas, location.X, location.Y, weptype)
        bullet.MoveSpeed = 5
        bullet.moveDirection = dir
        bullet.move = True
        ReDim Preserve caller.projectiles(caller.projectiles.Length)
        caller.projectiles(caller.projectiles.Length - 1) = bullet

    End Sub
    Sub Dispose()
        Me.Dispose()
    End Sub
    Private Class projectile
        Inherits entity
        Sub New(initcanvas As Graphics, initlocx As Integer, initlocy As Integer, initname As String)
            MyBase.New(initcanvas, "projectiles/" & initname, initlocx, initlocy)
        End Sub
    End Class

    Public Sub Dispose1() Implements IDisposable.Dispose

    End Sub
End Class
