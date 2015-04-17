Public Class player
    Inherits entity
    Dim weapon As equipment
    Public Sub New(ByVal ovrcanvas As Graphics, ByVal sprite As String, ByVal locx As Integer, locy As Integer)
        MyBase.New(ovrcanvas, "characters/" & sprite, locx, locy)
    End Sub
    Sub doAttack()

    End Sub
End Class
