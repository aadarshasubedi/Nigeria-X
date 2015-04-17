Public Class equipment
    Dim location As Point
    Dim appliedCanvas As Graphics
    Dim weptype As String
    Sub New(locX As Integer, locY As Integer, canvas As Graphics, equiptype As String)
        location.X = locX
        location.Y = locY
        appliedCanvas = canvas
        weptype = "graphics/"
    End Sub
    Private Sub Fire()

    End Sub
    Private Class projectile
        Inherits entity
        Sub New(initcanvas As Graphics, initlocx As Integer, initlocy As Integer, initname As String)
            MyBase.New(initcanvas, initname, initlocx, initlocy)
        End Sub
    End Class
End Class
