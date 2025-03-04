Public Class MemoryFunctions
    Inherits MemoryFunctionsDAL

    Public Sub MemoryModule(ByVal sCommand As String, ByVal incoming As String, ByVal socket As Integer)
        Try
            Select Case sCommand
                Case "MEMORYSLOT"
                    Call memoryslot(incoming, socket)
                Case "MEMORYFORFEIT"
                    Call memoryforfeit(incoming, socket)
                Case "MEMORYSURRENDER"
                    Call MemorySurrender(socket)
                Case "MEMORYSTALEMATE"
                    Call MemoryStalemate(socket)
                Case "MEMORYENDGAME"
                    Call MemoryEndGame(socket)
            End Select
        Catch ex As Exception
            Call errorsub(ex, "MemoryModule")
        End Try
    End Sub

    Private Sub MemoryEndGame(ByVal socket As Integer)
        Dim oChatFunctions As New ChatFunctions

        Try
            Dim oTables As New BusinessLayer.Tables
            Dim iTableID As Integer = oTables.FindPlayersTable(Tables, frmSystray.Winsock(socket).Tag)
            Dim iGameID As Integer = tables(iTableID).gameid

            If iGameID = 0 Then Throw New Exception

            LoadRecord(iGameID)

            If TimeoutDate < Date.Now Then
                'The game has expired
                Call MemoryIsOver(iGameID, frmSystray.Winsock(socket).Tag, IIf(PlayerID(frmSystray.Winsock(socket).Tag) = 1, Player2, Player1), True, IIf(PlayerID(frmSystray.Winsock(socket).Tag) = 1, 1, 2))
            Else
                oChatFunctions.unisend("CHAT", String.Concat(frmSystray.Winsock(socket).Tag, " !endgame [End Game]"))
                Call blockchat(String.Empty, "Request", "An endgame request has been sent to the administrators that are online as 4 minutes have not yet elapsed.", socket)
            End If
        Catch ex As Exception
            oChatFunctions.uniwarn(String.Concat(frmSystray.Winsock(socket).Tag, " sent an invalid stalemate request."))
        End Try
    End Sub

    Private Sub MemorySurrender(ByVal socket As Integer)
        Try
            Dim oPlayerAccount As New PlayerAccountDAL(frmSystray.Winsock(socket).Tag)

            If oPlayerAccount.Gold < 100 Then
                Call blockchat(frmSystray.Winsock(socket).Tag, "Surrender", "You do not have enough gold to surrender. Surrenders cost 100GP.")
                Exit Sub
            End If

            Dim oTables As New BusinessLayer.Tables
            Dim iGameID As Integer = Tables(oTables.FindPlayersTable(Tables, frmSystray.Winsock(socket).Tag)).GameID

            If iGameID = 0 Then Throw New Exception

            Call gpadd(frmSystray.Winsock(socket).Tag, -100, True)
            Call historyadd(frmSystray.Winsock(socket).Tag, 6, frmSystray.Winsock(socket).Tag & " surrendered on " & DateString & " " & TimeString & " - GameID: " & iGameID)

            Call dalNew(iGameID)
            MemoryIsOver(iGameID, Player1, Player2, False, IIf(Player1 = frmSystray.Winsock(socket).Tag, 2, 1))
        Catch ex As Exception
            Dim oChatFunctions As New ChatFunctions
            oChatFunctions.uniwarn(frmSystray.Winsock(socket).Tag & " sent an invalid game surrender packet.")
        End Try
    End Sub

    Private Sub MemoryStalemate(ByVal socket As Integer)
        Dim oTables As New BusinessLayer.Tables
        Dim iTableID As Integer = oTables.findplayerstable(tables, frmSystray.Winsock(socket).Tag)
        Dim iGameID As Integer = tables(iTableID).gameid

        If iGameID = 0 Then Throw New Exception

        LoadRecord(iGameID)

        Dim iPlayerID As Integer = PlayerID(frmSystray.Winsock(socket).Tag)

        If iPlayerID = 1 Then
            tables(iTableID).stalemate1 = True
        ElseIf iPlayerID = 2 Then
            tables(iTableID).stalemate2 = True
        Else
            Throw New Exception
        End If

        If tables(iTableID).stalemate1 = True And tables(iTableID).stalemate2 = True Then
            MemoryIsOver(GameID, Player1, Player2, False, 3)
        Else
            Send(IIf(iPlayerID = 1, Player2, Player1), "GAMESTALEMATE")
        End If
    End Sub

    Public Function memorychallengeyes(ByRef incoming As String, ByRef socket As Integer) As String
        Dim iGameID As Integer

        Dim lstmemory As New ListBox

        Try
            Dim sDivide() As String = incoming.Split(" ")

            Dim sChallengeYesNick As String = sDivide(1)

            If sChallengeYesNick = String.Empty Then
                Send(String.Empty, "WORDCOMMAND 4", socket)
                memorychallengeyes = String.Empty
                Exit Function
            End If

            Dim sOpponent As String = frmSystray.Winsock(socket).Tag
            Dim iOppSocket As Integer = GetSocket(sChallengeYesNick)

            'User accepted the challenge... damn it,
            'now i have to code the game core.... :)
            Send(String.Empty, "MEMORYYES", iOppSocket)

            Dim oGameFunctions As New GameFunctions
            iGameID = oGameFunctions.NewGameID()

            memorychallengeyes = iGameID.ToString
            GameInsert(iGameID, sChallengeYesNick, sOpponent)

            lstmemory.Items.Clear()
            Dim z As Integer = 0

            For x As Integer = 0 To 23
                lstmemory.Items.Add(x.ToString)
            Next x

            Dim oDataTable As DataTable = Nothing
            oDataTable = RandomMemoryCards()

            Dim iCards(23) As Integer

            For x As Integer = 0 To oDataTable.Rows.Count - 1
                For intA As Integer = 1 To 2
                    Randomize()
                    Dim y As Integer = Int(Rnd() * lstmemory.Items.Count)

                    iCards(Val(lstmemory.Items(y))) = oDataTable.Rows(x).Item(0)
                    lstmemory.Items.RemoveAt(y)
                Next intA
            Next

            UpdateCardSlots(iGameID, iCards)

            'Dim oChatFunctions As New ChatFunctions
            'oChatFunctions.unisend("CHALLENGESTART", String.Concat(sChallengeYesNick, " ", frmSystray.Winsock(socket).Tag, "5"))
            'oChatFunctions = Nothing

            Send(String.Empty, "MEMORYTURN 1", GetSocket(sChallengeYesNick))
            Send(String.Empty, "MEMORYTURN 1", socket)
        Catch ex As Exception
            Call errorsub(ex, "memorychallengeyes")
            memorychallengeyes = String.Empty
        Finally
            lstmemory.Dispose()
        End Try
    End Function

    Private Sub memoryforfeit(ByVal incoming As String, ByVal socket As Integer)
        Dim oTableFunctions As New BusinessLayer.Tables
        Dim iTableNumber As Integer = oTableFunctions.FindPlayersTable(Tables, frmSystray.Winsock(socket).Tag)
        Dim sOpponent As String = String.Empty

        If tables(iTableNumber).player1 = frmSystray.Winsock(socket).Tag Then
            sOpponent = tables(iTableNumber).player2
        Else
            sOpponent = tables(iTableNumber).player1
        End If

        Call dalNew(tables(iTableNumber).gameid)

        If PlayerID(frmSystray.Winsock(socket).Tag) = 1 Then
            Send(String.Empty, "MEMORYTURN 2", socket)
            Send(String.Empty, "MEMORYTURN 2", GetSocket(sOpponent))
        Else
            Send(String.Empty, "MEMORYTURN 1", socket)
            Send(String.Empty, "MEMORYTURN 1", GetSocket(sOpponent))
        End If
    End Sub

    Private Sub memoryslot(ByVal incoming As String, ByVal socket As Integer)
        Dim sOpponent As String = String.Empty
        Dim strCard As String = String.Empty
        Dim oTableFunctions As New BusinessLayer.Tables

        Try
            Dim sDivide() As String = incoming.Split(" ")
            Dim iSpot As Integer = Val(sDivide(1))
            Dim iSpotChecked As Integer = Val(sDivide(2))

            Dim iTableNumber As Integer = oTableFunctions.FindPlayersTable(Tables, frmSystray.Winsock(socket).Tag)

            If tables(iTableNumber).player1 = frmSystray.Winsock(socket).Tag Then
                sOpponent = tables(iTableNumber).player2
            Else
                sOpponent = tables(iTableNumber).player1
            End If

            Dim iGameID As Integer = tables(iTableNumber).gameid

            Call dalNew(iGameID)

            Dim iPlayerID As Integer = PlayerID(frmSystray.Winsock(socket).Tag)

            strCard = MemoryCard(iSpot)

            If iSpotChecked = 1 Then
                Player_Spot(iPlayerID, 1) = 0
                Player_Spot(iPlayerID, 2) = 0
            End If

            Player_Spot(iPlayerID, iSpotChecked) = iSpot

            strCard = strCard.Replace(" ", "%20")

            Send(String.Empty, String.Concat("MEMORYSLOT ", strCard, " ", iSpot, " 0 ", iSpotChecked), socket)
            Send(String.Empty, String.Concat("MEMORYSLOT ", strCard, " ", iSpot, " 1 ", iSpotChecked), GetSocket(sOpponent))

            Update()

            If iSpotChecked = 2 Then Call memoryanalyze(iGameID, frmSystray.Winsock(socket).Tag, sOpponent)
        Catch ex As Exception
            Call errorsub(ex, "memoryslot")
        End Try
    End Sub

    Private Sub memoryanalyze(ByVal gameid As Integer, ByVal player As String, ByVal opponent As String)
        Try
            Call dalNew(gameid)
            Dim iPlayerID As Integer = PlayerID(player)

            ' If the card in both spots they picked are equal then they got 1 right!
            If MemoryCard(Player_Spot(iPlayerID, 1)) = MemoryCard(Player_Spot(iPlayerID, 2)) Then
                HistoryInsert(Player_Spot(iPlayerID, 1), Player_Spot(iPlayerID, 2), String.Concat(player, " made a match (capture)"))

                Call MemoryPoints(iPlayerID)
                Send(String.Empty, String.Concat("MEMORYMATCH ", Player_Spot(iPlayerID, 1), " ", Player_Spot(iPlayerID, 2)), GetSocket(player))
                Send(String.Empty, String.Concat("MEMORYMATCH ", Player_Spot(iPlayerID, 1), " ", Player_Spot(iPlayerID, 2)), GetSocket(opponent))

                Player_Spot(iPlayerID, 1) = 0
                Player_Spot(iPlayerID, 2) = 0
                Update()

                If IsGameOver() = False Then
                    If iPlayerID = 1 Then
                        Send(String.Empty, "MEMORYTURN 1", GetSocket(player))
                        Send(String.Empty, "MEMORYTURN 1", GetSocket(opponent))
                    Else
                        Send(String.Empty, "MEMORYTURN 2", GetSocket(player))
                        Send(String.Empty, "MEMORYTURN 2", GetSocket(opponent))
                    End If
                Else
                    Call MemoryIsOver(gameid, player, opponent, False)
                End If
            Else
                HistoryInsert(Player_Spot(iPlayerID, 1), Player_Spot(iPlayerID, 2), String.Concat(player, " missed (miss)"))

                If iPlayerID = 1 Then
                    Send(String.Empty, "MEMORYTURN 2", GetSocket(player))
                    Send(String.Empty, "MEMORYTURN 2", GetSocket(opponent))
                Else
                    Send(String.Empty, "MEMORYTURN 1", GetSocket(player))
                    Send(String.Empty, "MEMORYTURN 1", GetSocket(opponent))
                End If
            End If
        Catch ex As Exception
            Call errorsub(ex, "memoryanalyze")
        End Try
    End Sub

    Public Sub MemoryIsOver(ByVal gameid As Integer, ByVal player As String, ByVal opponent As String, ByVal blnTimeout As Boolean, Optional ByVal skipWinner As Integer = 0)
        Dim blnSkip As Boolean, FoundEmpty As Boolean
        Dim intdraw As Integer
        Dim oStats As New Statistics
        Dim oTableFunctions As New BusinessLayer.Tables
        Dim iTableID As Integer = oTableFunctions.FindPlayersTable(Tables, player)
        Dim oGameDAL As New GameFunctionsDAL

        Call dalNew(gameid)
        Dim iWinner_Gold As Integer, iLoser_Gold As Integer, sWinner As String = String.Empty, sLoser As String = String.Empty

        Try
            Dim iPlayerID As Integer = PlayerID(player)

            Select Case skipWinner
                Case 3
                    FoundEmpty = False

                    Player1_Score = 6
                    Player2_Score = 6
                    Surrender = 1
                    Update()

                    blnSkip = True
                Case 1
                    FoundEmpty = False

                    Player1_Score = 12
                    Player2_Score = 0

                    If blnTimeout = False Then
                        Surrender = 1
                        Call blockchat(Player1, "Surrender", Player2 & " surrendered and lost.")
                        Call blockchat(Player2, "Surrender", Player2 & " surrendered and lost.")
                    End If

                    Update()
                    blnSkip = True
                Case 2
                    FoundEmpty = False

                    Player1_Score = 0
                    Player2_Score = 12

                    If blnTimeout = False Then
                        Call blockchat(Player1, "Surrender", Player1 & " surrendered and lost.")
                        Call blockchat(Player2, "Surrender", Player1 & " surrendered and lost.")
                        Surrender = 1
                    End If

                    Update()
                    blnSkip = True
            End Select

            If Player1_Score = Player2_Score Then intdraw = 1

            Dim oGameFunctions As New GameFunctions
            Dim oPlayerAccount As New PlayerAccountDAL(Player1)
            Dim oPlayerAccount2 As New PlayerAccountDAL(Player2)

            If intdraw = 1 Then
                If blnSkip = False Then
                    Call oGameFunctions.drawevent(Player1, Player2, gameid, False, "ttm", Me.GameLength, Nothing)
                End If

                Call sendall(String.Concat("MATCH ", Player1, " ", oPlayerAccount.MemoryRank, " ", Player2, " ", oPlayerAccount2.MemoryRank, " ", Player1_Score, " ", Player2_Score, " 4"))
                oStats.CompileStats(Player1, Player2, "ttm", True)
            ElseIf Player1_Score > Player2_Score Then
                If blnSkip = False Then
                    iLoser_Gold = EndGameGP(Player2, -1)
                    sLoser = Player2

                    iWinner_Gold = endgamegp(player1, 1)
                    sWinner = Player1
                End If

                oGameDAL.RankChange(Player1, Player2, "TTM")
                Call sendall(String.Concat("MATCH ", Player1, " ", oPlayerAccount.MemoryRank, " ", Player2, " ", oPlayerAccount2.MemoryRank, " ", Player1_Score, " ", Player2_Score, " 4"))

                Call WagerRule(Player1, Player2, gameid)
                oStats.CompileStats(Player1, Player2, "ttm", False)
            Else
                If blnSkip = False Then
                    sLoser = Player1
                    sWinner = Player2

                    iLoser_Gold = EndGameGP(Player1, -1)
                    iWinner_Gold = EndGameGP(Player2, 1)
                End If

                oGameDAL.RankChange(Player2, Player1, "TTM")
                Call sendall(String.Concat("MATCH ", Player2, " ", oPlayerAccount2.MemoryRank, " ", Player1, " ", oPlayerAccount.MemoryRank, " ", Player2_Score, " ", Player1_Score, " 4"))

                Call WagerRule(Player2, Player1, gameid)
                oStats.CompileStats(Player2, Player1, "ttm", False)
            End If

            Send(Player1, "MEMORYCLOSE")
            Send(Player2, "MEMORYCLOSE")

            If Player1_Score <> Player2_Score Then
                Call gpadd(sWinner, iWinner_Gold, True)
                Call gpadd(sLoser, iLoser_Gold, True)

                Call oGameFunctions.GiveExp(sWinner, IIf(Me.Winner = 1, Player1_Score, Player2_Score), sLoser, IIf(Me.Winner = 1, Player1_Score, Player2_Score), gameid, Nothing, False, "ttm")
                'ElseIf Player1_Score <> 6 Then
                '    Call blockchat(Player1, "Warning", "You did not earn gold, active points, or experience because you and your opponent played too fast.  Turbo play is no longer rewarded.")
                '    Call blockchat(Player2, "Warning", "You did not earn gold, active points, or experience because you and your opponent played too fast.  Turbo play is no longer rewarded.")
            End If

            If blnSkip = False Then oGameFunctions.EndGameEvent(Tables(iTableID))
        Catch ex As Exception
            Call errorsub(ex, "memoryisover")
        Finally
            oTableFunctions.ClosePlayerTable(Tables, Player1, False)
            oTableFunctions = Nothing
        End Try
    End Sub

    Private Sub MemoryPoints(ByVal iPlayerID As Integer)
        Try
            Select Case iPlayerID
                Case 1
                    Player1_Score += 1
                Case 2
                    Player2_Score += 1
            End Select

            Send(String.Empty, String.Concat("MEMORYSCORE ", Player1_Score, " ", Player2_Score), GetSocket(Player1))
            Send(String.Empty, String.Concat("MEMORYSCORE ", Player1_Score, " ", Player2_Score), GetSocket(Player2))
        Catch ex As Exception
            Call errorsub(ex, "memorypoints")
        End Try
    End Sub
End Class
