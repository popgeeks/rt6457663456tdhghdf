Public Class Statistics
    Inherits StatisticsDAL

    Public Sub CompileStats(ByVal sWinner As String, ByVal sLoser As String, ByVal sGame As String, ByVal blnDraw As Boolean)
        If blnDraw = False Then
            Call UpdateStats(sWinner, sGame, 1)
            Call UpdateStats(sLoser, sGame, 2)
        Else
            Call UpdateStats(sWinner, sGame, 3)
            Call UpdateStats(sLoser, sGame, 3)
        End If
    End Sub
End Class
