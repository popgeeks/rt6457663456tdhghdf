Namespace GameObjects
    Public Class CardWars
        Public GameID As Integer
        Public Player1 As String
        Public Player2 As String
        Public Winner As Integer
        Public Player1_HP As Integer
        Public Player2_HP As Integer
        Public Offense As Integer
        Public Defense As Integer
        Public ElementField As Integer
        Public P1Ready As Integer
        Public P2Ready As Integer
        Public ChainPlayer As Integer
        Public ChainCount As Integer
        Public PlayerTurn As Integer
        Public Rounds As Integer
        Public Surrender As Integer
        Public StartDate As DateTime
        Public LastActivity As DateTime
        Public GameEnds As DateTime
        Public TimeOut As DateTime

        Public Player1_Card As String
        Public Player2_Card As String

        Public Player1Hand As New List(Of String)
        Public Player2Hand As New List(Of String)
    End Class
End Namespace
