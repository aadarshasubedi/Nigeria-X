Public Class player
    Inherits entity

    Public Sub New(ByVal ovrcanvas As Graphics, ByVal sprite As String, ByVal locx As Integer, locy As Integer)
        MyBase.New(ovrcanvas, sprite, locx, locy)
    End Sub
    Sub playerMovement(ByVal currentKey As String)
        Me.move = True
        Select Case currentKey
            Case "up"
                Me.moveDirection = "N"
            Case "down"
                Me.moveDirection = "S"
            Case "left"
                Me.moveDirection = "W"
            Case "right"
                Me.moveDirection = "E"
        End Select
        Me.tryMove()
    End Sub
End Class
