Public Class Trades
    Inherits TradesDAL

    Public Sub IncomingModule(ByVal sCommand As String, ByVal incoming As String, ByVal socket As Integer)
        Select Case sCommand
            Case "TRADEINITIATE"
                Call initiatetrade(incoming, socket)
            Case "TRADEREMOVE"
                Call traderemove(incoming, socket)
            Case "TRADESEND"
                Call tradesend(incoming, socket)
            Case "TRADEGPADD"
                Call tradegpadd(incoming, socket)
            Case "TRADEGPREMOVE"
                Call tradegpremove(incoming, socket)
            Case "TRADEEXIT"
                Call tradeexit(socket)
            Case "TRADEFINISHED"
                Call tradefinished(incoming, socket)
        End Select
    End Sub

    Public Sub initiatetrade(ByVal incoming As String, ByVal socket As Integer)
        ' 1 = challengeyesnick
        ' 2 = opp

        Try
            Dim sItems() As String = incoming.Split(" ")
            Dim oGameFunctions As New GameFunctions
            Dim oTables As New TableFunctions

            Dim iTableID As Integer = oTables.findplayerstable(frmSystray.Winsock(socket).Tag)

            Player1 = tables(iTableID).player1
            Player2 = tables(iTableID).player2

            PlayerSocket(1) = GetSocket(tables(iTableID).player1)
            PlayerSocket(2) = socket

            If Player1 = String.Empty Or PlayerSocket(1) = 0 Or PlayerSocket(2) = 0 Then
                Call blockchat(String.Empty, "Cancelled", "Your challenge was cancelled by the server.  Please try again later.", PlayerSocket(1))
                Call blockchat(String.Empty, "Cancelled", "Your challenge was cancelled by the server.  Please try again later.", PlayerSocket(2))
                oTables.KillMatch(iTableID)
                oTables.ClosePlayerTable(iTableID)
                Exit Sub
            End If

            Send(String.Empty, "TRADEYES", PlayerSocket(1))
            Send(String.Empty, "TRADEYES", PlayerSocket(2))

            GameID = oGameFunctions.NewGameID.ToString
            tables(iTableID).gameid = GameID

            GameInsert()
            HistoryInsert("Trade Started")

            Send(String.Empty, String.Concat("TRADESTART ", Player2), PlayerSocket(1))
            Send(String.Empty, String.Concat("TRADESTART ", Player1), PlayerSocket(2))
        Catch ex As Exception
            Call errorsub(ex.ToString, "initiatetrade")
        End Try
    End Sub

    Private Sub tradesend(ByVal incoming As String, ByVal socket As Integer)
        Try
            Dim sItems() As String = incoming.Split(" ")
            Dim oTables As New TableFunctions

            Dim iTableID As Integer = oTables.findplayerstable(frmSystray.Winsock(socket).Tag)
            Dim iGameID As Integer = tables(iTableID).gameid

            If iGameID = 0 Then
                Call blockchat(String.Empty, "Error", "Server could not process your request", socket)
                Exit Sub
            End If

            GameID = iGameID
            LoadRecord()

            Dim iPlayerID As Integer = PlayerID(frmSystray.Winsock(socket).Tag)

            If iPlayerID = 0 Then
                Call blockchat(String.Empty, "Error", "Server could not process your request.  Please try again later.", socket)
                Dim oGameFunctions As New GameFunctions
                oGameFunctions.KillGame(PlayerSocket(1), PlayerSocket(2), frmSystray.Winsock(socket).Tag, "The game was ended because the server did not recognize a command from one of the players involved in this trade.")
                Exit Sub
            End If

            AddCard(iPlayerID, sItems(1).Replace("%20", " "))
            Update()

            If ErrorFlag = True Then
                Call blockchat(String.Empty, "Error", "Server could not process your request.  Please try again later.", socket)
                Dim oGameFunctions As New GameFunctions
                oGameFunctions.KillGame(PlayerSocket(1), PlayerSocket(2), frmSystray.Winsock(socket).Tag, "The game was ended because the server did not recognize a command from one of the players involved in this trade.")
                Call errorsub(ErrorDescription.ToString, "tradesend_update")
                Exit Sub
            End If

            Send(String.Empty, String.Concat("TRADEADD ", sItems(1).Replace(" ", "%20")), IIf(iPlayerID = 1, PlayerSocket(2), PlayerSocket(1)))
            Send(String.Empty, "TRADEFINISHED 0", PlayerSocket(1))
            Send(String.Empty, "TRADEFINISHED 0", PlayerSocket(2))
            HistoryInsert(String.Concat(PlayerNick(iPlayerID), " added 1 ", sItems(1).Replace("%20", " "), " from his/her offering."))
        Catch ex As Exception
            Call errorsub(ex.ToString, "tradesend")
        End Try
    End Sub

    Private Sub traderemove(ByVal incoming As String, ByVal socket As Integer)
        Try
            Dim oTables As New TableFunctions

            Dim iTableID As Integer = oTables.findplayerstable(frmSystray.Winsock(socket).Tag)
            Dim iGameID As Integer = tables(iTableID).gameid

            If iGameID = 0 Then
                Call blockchat(String.Empty, "Error", "Server could not process your request", socket)
                Exit Sub
            End If

            GameID = iGameID
            LoadRecord()

            Dim iPlayerID As Integer = PlayerID(frmSystray.Winsock(socket).Tag)

            RemoveCard(iPlayerID)
            Send(String.Empty, String.Concat("TRADEREMOVE"), IIf(iPlayerID = 1, PlayerSocket(2), PlayerSocket(1)))
            Update()

            Send(String.Empty, "TRADEFINISHED 0", PlayerSocket(1))
            Send(String.Empty, "TRADEFINISHED 0", PlayerSocket(2))
            HistoryInsert(String.Concat(PlayerNick(iPlayerID), " removed all cards from his/her offering."))
        Catch ex As Exception
            Call errorsub(ex.ToString, "traderemove")
        End Try
    End Sub

    Private Sub tradegpadd(ByVal incoming As String, ByVal socket As Integer)
        Try
            Dim oChatFunctions As New ChatFunctions
            Dim sItems() As String = incoming.Split(" ")
            Dim oTables As New TableFunctions

            Dim iTableID As Integer = oTables.findplayerstable(frmSystray.Winsock(socket).Tag)
            Dim iGameID As Integer = tables(iTableID).gameid
            Dim oPlayerAccount As New PlayerAccountDAL(frmSystray.Winsock(socket).Tag)

            If iGameID = 0 Then
                Call blockchat(String.Empty, "Error", "Server could not process your request", socket)
                Exit Sub
            End If

            GameID = iGameID
            LoadRecord()

            Dim iPlayerID As Integer = PlayerID(frmSystray.Winsock(socket).Tag)

            If oPlayerAccount.Gold < Val(sItems(1)) Then
                oChatFunctions.uniwarn(String.Concat(frmSystray.Winsock(socket).Tag, " tried to trade more gold then they have."))
                oTables.KillMatch(iTableID)
                oTables.ClosePlayerTable(iTableID)
                Exit Sub
            ElseIf Val(sItems(1)) < 0 Then
                oChatFunctions.uniwarn(String.Concat(frmSystray.Winsock(socket).Tag, " tried to add less than 0 gold and bypassed client filter."))
                oTables.KillMatch(iTableID)
                oTables.ClosePlayerTable(iTableID)
                Exit Sub
            End If

            Select Case iPlayerID
                Case 1
                    Player_Gold(1) += Val(sItems(1))
                    Send(String.Empty, String.Concat("TRADEGPADD ", sItems(1)), PlayerSocket(2))
                Case 2
                    Player_Gold(2) += Val(sItems(1))
                    Send(String.Empty, String.Concat("TRADEGPADD ", sItems(1)), PlayerSocket(1))
            End Select

            Update()
            HistoryInsert(String.Concat(PlayerNick(iPlayerID), " added ", sItems(1), " gold to his offering"))
        Catch ex As Exception
            Call errorsub(ex.ToString, "tradegpadd")
        End Try
    End Sub

    Private Sub tradegpremove(ByVal incoming As String, ByVal socket As Integer)
        Try
            Dim oChatFunctions As New ChatFunctions
            Dim oTables As New TableFunctions

            Dim sItems() As String = incoming.Split(" ")

            Dim iTableID As Integer = oTables.findplayerstable(frmSystray.Winsock(socket).Tag)
            Dim iGameID As Integer = tables(iTableID).gameid
            Dim oPlayerAccount As New PlayerAccountDAL(frmSystray.Winsock(socket).Tag)
            Dim iPlayerID As Integer = PlayerID(frmSystray.Winsock(socket).Tag)

            If iGameID = 0 Then
                Call blockchat(String.Empty, "Error", "Server could not process your request", socket)
                Exit Sub
            End If

            GameID = iGameID
            LoadRecord()

            If Val(sItems(1)) < 0 Then
                oChatFunctions.uniwarn(String.Concat(frmSystray.Winsock(socket).Tag, " tried to remove less than 0 gold and bypassed client filter."))
                oTables.KillMatch(iTableID)
                oTables.ClosePlayerTable(frmSystray.Winsock(socket).Tag)
                Exit Sub
            End If

            If Player_Gold(iPlayerID) - Val(sItems(1)) < 0 Then
                oChatFunctions.uniwarn(String.Concat(frmSystray.Winsock(socket).Tag, " tried to remove more gold then is currently in his/her offering."))
                oTables.KillMatch(iTableID)
                oTables.ClosePlayerTable(frmSystray.Winsock(socket).Tag)
                Exit Sub
            End If

            Select Case iPlayerID
                Case 1
                    Player_Gold(1) -= Val(sItems(1))
                    Send(String.Empty, String.Concat("TRADEGPREMOVE ", sItems(1)), PlayerSocket(2))
                Case 2
                    Player_Gold(2) -= Val(sItems(2))
                    Send(String.Empty, String.Concat("TRADEGPREMOVE ", sItems(1)), PlayerSocket(1))
            End Select

            Update()
            HistoryInsert(String.Concat(PlayerNick(iPlayerID), " removed ", sItems(1), " gold to his offering"))
        Catch ex As Exception
            Call errorsub(ex.ToString, "tradegpremove")
        End Try
    End Sub

    Private Sub tradeexit(ByVal socket As Integer)
        Try
            Dim oTables As New TableFunctions

            Dim iTableID As Integer = oTables.findplayerstable(frmSystray.Winsock(socket).Tag)
            Dim iGameID As Integer = tables(iTableID).gameid

            If iGameID = 0 Then
                Call blockchat(String.Empty, "Error", "Server could not process your request", socket)
                Exit Sub
            End If

            GameID = iGameID
            LoadRecord()

            Dim iPlayerID As Integer = PlayerID(frmSystray.Winsock(socket).Tag)

            Send(String.Empty, "TRADEEXIT", PlayerSocket(1))
            Send(String.Empty, "TRADEEXIT", PlayerSocket(2))

            Completed = 1

            Update()
            HistoryInsert(String.Concat("Trade Closed by ", frmSystray.Winsock(socket).Tag))

            oTables.ClosePlayerTable(Player1)
        Catch ex As Exception
            Call errorsub(ex.ToString, "tradeexit")
        End Try
    End Sub

    Private Sub tradefinished(ByVal incoming As String, ByVal socket As Integer)
        'sItems(1) = 1 = yes, 0 = no
        Try
            Dim oChatFunctions As New ChatFunctions
            Dim oTables As New TableFunctions

            Dim sItems() As String = incoming.Split(" ")

            Dim iTableID As Integer = oTables.findplayerstable(frmSystray.Winsock(socket).Tag)
            Dim iGameID As Integer = tables(iTableID).gameid

            If iGameID = 0 Then
                Call blockchat(String.Empty, "Error", "Server could not process your request", socket)
                Exit Sub
            End If

            GameID = iGameID
            LoadRecord()

            Dim iPlayerID As Integer = PlayerID(frmSystray.Winsock(socket).Tag)

            Player_Ready(iPlayerID) = IIf(sItems(1) = "1", True, False)
            Send(String.Empty, String.Concat("TRADEFINISHED ", sItems(1)), IIf(iPlayerID = 1, PlayerSocket(2), PlayerSocket(1)))
            Update()

            HistoryInsert(String.Concat(PlayerNick(iPlayerID), IIf(sItems(1) = "1", " accepts ", " does not accept "), "the trade"))

            If Player_Ready(1) = True And Player_Ready(2) = True Then
                Completed = 1

                If CompleteTrade() = True Then
                    If Player_Gold(2) <> 0 Then
                        Dim oPlayerAccount1 As New PlayerAccountDAL(Player1)
                        oPlayerAccount1.SendGold()
                    End If

                    If Player_Gold(1) <> 0 Then
                        Dim oPlayerAccount2 As New PlayerAccountDAL(Player2)
                        oPlayerAccount2.SendGold()
                    End If

                    Call blockchat(String.Empty, "Successful", "The trade was successful.", PlayerSocket(1))
                    Call blockchat(String.Empty, "Successful", "The trade was successful.", PlayerSocket(2))
                    Send(String.Empty, "TRADEEXIT", PlayerSocket(1))
                    Send(String.Empty, "TRADEEXIT", PlayerSocket(2))
                Else
                    Call blockchat(String.Empty, "Trade", "The trade failed due to one player trying to trade items they do not have.  Please try again.  A warning message/abuse report has been sent to an administrator.", PlayerSocket(1))
                    Call blockchat(String.Empty, "Trade", "The trade failed due to one player trying to trade items they do not have.  Please try again.  A warning message/abuse report has been sent to an administrator.", PlayerSocket(2))
                    oChatFunctions.uniwarn(String.Concat("Trade ID ", GameID, " failed.  This is a result of someone trading cards or gp they do not have."))
                    Call historyadd(Player1, 2, String.Concat("Trade ID ", GameID, " failed.  This is a result of someone trading cards or gp they do not have."))
                    Call historyadd(Player2, 2, String.Concat("Trade ID ", GameID, " failed.  This is a result of someone trading cards or gp they do not have."))
                    Send(String.Empty, "TRADEEXIT", PlayerSocket(1))
                    Send(String.Empty, "TRADEEXIT", PlayerSocket(2))
                End If

                oTables.ClosePlayerTable(Player1)
            End If
        Catch ex As Exception
            Call errorsub(ex.ToString, "tradefinished")
        End Try
    End Sub
End Class
