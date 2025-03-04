Namespace BusinessObjects
    Public Class Tables
        Public GameID As Integer
        Public Player1 As String
        Public Player2 As String
        Public Player3 As String
        Public Player4 As String

        Public Player1_Socket As Integer
        Public Player2_Socket As Integer
        Public Player3_Socket As Integer
        Public Player4_Socket As Integer

        Public Player_Ready(5) As Boolean
        Public RuleList As String
        Public Code_X As String
        Public Comment As String
        Public Lock As Boolean
        Public Wager As Integer
        Public Stalemate1 As Boolean
        Public Stalemate2 As Boolean
        Public Decks As String
        Public Blocks As String
        Public GoldWager As Integer

        Public TableID As Integer

        Public CardWarsObject As Entities.GameObjects.CardWars

        Public Function IsEmpty() As Boolean
            If Player1 = String.Empty And Player2 = String.Empty And Player3 = String.Empty And Player4 = String.Empty Then
                Return True
            Else
                Return False
            End If
        End Function

        Public Function IsPlayer(ByVal iPlayerIndex As Integer) As Boolean
            Select Case iPlayerIndex
                Case 1
                    If Player1 <> String.Empty Then Return True
                Case 2
                    If Player2 <> String.Empty Then Return True
                Case 3
                    If Player3 <> String.Empty Then Return True
                Case 4
                    If Player4 <> String.Empty Then Return True
            End Select
        End Function

        Public Function PlayerInTable(ByVal sPlayer As String) As Boolean
            If Player1 = sPlayer Or Player2 = sPlayer Or Player3 = sPlayer Or Player4 = sPlayer Then
                Return True
            Else
                Return False
            End If
        End Function

        Public Sub New()
            Clear()
        End Sub

        Public Sub Clear()
            GameID = 0
            Player1 = String.Empty
            Player2 = String.Empty
            Player3 = String.Empty
            Player4 = String.Empty

            Player1_Socket = 0
            Player2_Socket = 0
            Player3_Socket = 0
            Player4_Socket = 0

            For Each bPlayerReady As Boolean In Player_Ready
                bPlayerReady = False
            Next

            RuleList = String.Empty
            Code_X = String.Empty
            Comment = String.Empty
            Lock = False
            Wager = 0
            Stalemate1 = False
            Stalemate2 = False
            Decks = String.Empty
            Blocks = String.Empty
            GoldWager = 0

            TableID = 0

            CardWarsObject = Nothing
        End Sub

        Protected Overrides Sub Finalize()
            MyBase.Finalize()
        End Sub
    End Class
End Namespace