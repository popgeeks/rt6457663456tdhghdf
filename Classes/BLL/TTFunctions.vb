Public Class TTFunctions
    Inherits TTFunctionsDAL

    Public Sub TTModule(ByVal sCommand As String, ByVal incoming As String, ByVal socket As Integer)
        Select Case sCommand
            Case "CHALLENGEYES"
                Call challengeyes(incoming, socket)
            Case "TT_MYCARD"
                Call mycard(incoming, socket)
            Case "TT_PLACEAT"
                Call tt_placeat(incoming, socket)
            Case "TT_SURRENDER"
                Call TTSurrender(socket)
            Case "TT_STALEMATE"
                Call TTStalemate(socket)
            Case "TT_TIMEOUT"
                Call tt_timeout(socket)
            Case "TT_TAKECARD"
                Call tt_takecard(incoming, socket)
            Case "TT_TAKERANDOMCARD"
                Call tt_takerandomcard(incoming, socket)
            Case "TT_ENDGAME"
                Call TTEndgame(socket)
        End Select
    End Sub

    Public Sub CardPenalty(ByVal sLoser As String, ByVal bLoad As Boolean)
        Try
            Dim oChatFunctions As New ChatFunctions

            Dim oTables As New BusinessLayer.Tables
            Dim iTableID As Integer = oTables.FindPlayersTable(Tables, sLoser)

            Dim iGameID As Integer = Tables(iTableID).GameID

            If bLoad = True Then
                GameID = iGameID
                LoadRecord()
            End If

            tables(iTableID).gameid = 0

            Dim ThisPlrID As Integer = PlayerID(sLoser)

            If TradeNone = True Or CardstoTake = 0 Then
                Exit Sub
            ElseIf CardsTaken = True Then
                Exit Sub
            End If

            CardsTaken = True
            Update()

            'Okay so let's take a card
            Dim sCard As String = String.Empty

            Randomize()
            Dim iStartID As Integer = Int(Rnd() * 5)
            If TradeAll = True Then iStartID = 1

            Select Case ThisPlrID
                Case 2
                    For x As Integer = 1 To CardstoTake
                        sCard = Player_OriginalHand(2, iStartID)
                        Call cardchg(Player1, sCard, 1)
                        Call cardchg(Player2, sCard, -1)

                        Call blockchat(String.Empty, "Cards Won", String.Concat("You gained 1 ", sCard), GetSocket(Tables(iTableID).Player1))

                        If iStartID = 4 Then
                            iStartID = 0
                        Else
                            iStartID += 1
                        End If
                    Next x

                    oChatFunctions.uniwarn(String.Concat(Player1, " gained ", CardstoTake, " cards because ", Player2, " left a trade game."))
                Case 1
                    For x As Integer = 1 To CardstoTake
                        sCard = Player_OriginalHand(1, iStartID)
                        Call cardchg(Player2, sCard, 1)
                        Call cardchg(Player1, sCard, -1)

                        Call blockchat(String.Empty, "Cards Won", String.Concat("You gained 1 ", sCard), GetSocket(Tables(iTableID).Player2))

                        If iStartID = 5 Then
                            iStartID = 0
                        Else
                            iStartID += 1
                        End If
                    Next x

                    oChatFunctions.uniwarn(String.Concat(Player2, " gained ", CardstoTake, " cards because ", Player1, " left a trade game."))
                Case 0
                    ' Do nothing, warn admins
            End Select

            HistoryInsert(9999, String.Empty, String.Concat(sLoser, " loses ", CardstoTake, " card(s) due to trade game evasion"))
        Catch ex As Exception
            Call errorsub(ex, "CardPenalty")
        End Try
    End Sub

    Protected Sub tt_takerandomcard(ByVal incoming As String, ByVal socket As Integer)
        Try
            Dim lostvar As String = String.Empty, sGameID As String = String.Empty

            Divide(incoming, " ", lostvar, sGameID)
            Dim oChatFunctions As New ChatFunctions, oTables As New BusinessLayer.Tables

            Dim iTableID As Integer = oTables.FindPlayersTable(Tables, frmSystray.Winsock(socket).Tag)

            If sGameID = String.Empty Or Val(sGameID) = 0 Then Exit Sub

            GameID = Val(sGameID)
            LoadRecord()

            Dim ThisPlrID As Integer = PlayerID(frmSystray.Winsock(socket).Tag)

            If TradeNone = True Or CardstoTake = 0 Then
                Call blockchat(String.Empty, "Error", "You cannot pick a card because the trade rule was none.", socket)
                oChatFunctions.uniwarn(String.Concat(frmSystray.Winsock(socket).Tag, " tried to pick a card in a TT game and the trade rule was none!"))
                Exit Sub
            ElseIf CardsTaken = True Then
                Call blockchat(String.Empty, "Error", "You cannot pick a card because you have already received cards for this game or you are not awarded cards.", socket)
                oChatFunctions.uniwarn(String.Concat(frmSystray.Winsock(socket).Tag, " tried to pick a card in a TT game for a second time (and failed)."))
                Exit Sub
            End If

            'Okay so let's take a card
            Dim sCard As String = String.Empty

            Select Case Winner
                Case 1
                    Randomize()
                    Dim iRandomNumber As Integer = Int(Rnd() * 5)

                    sCard = Player_OriginalHand(2, iRandomNumber)
                    Call cardchg(Player1, sCard, 1)
                    Call cardchg(Player2, sCard, -1)

                    Call blockchat(String.Empty, "Cards Won", String.Concat("You gained 1 ", sCard), GetSocket(Tables(itableid).player1))
                    Call blockchat(String.Empty, "Cards Lost", String.Concat("You lost 1 ", sCard), GetSocket(Tables(itableid).player2))
                Case 2
                    Randomize()
                    Dim iRandomNumber As Integer = Int(Rnd() * 5)

                    sCard = Player_OriginalHand(1, iRandomNumber)
                    Call cardchg(Player2, sCard, 1)
                    Call cardchg(Player1, sCard, -1)

                    Call blockchat(String.Empty, "Cards Won", String.Concat("You gained 1 ", sCard), GetSocket(Tables(itableid).player2))
                    Call blockchat(String.Empty, "Cards Lost", String.Concat("You lost 1 ", sCard), GetSocket(Tables(itableid).player1))
                Case 0
                    ' Do nothing, warn admins
                    Call blockchat(String.Empty, "Error", "You cannot pick a card because you have already received cards for this game or you are not awarded cards.", socket)
                    oChatFunctions.uniwarn(String.Concat(frmSystray.Winsock(socket).Tag, " tried to pick a card but the game was a draw!"))
                    Exit Sub
            End Select

            CardsTaken = True
            Update()
        Catch ex As Exception
            Call errorsub(ex, "tt_takerandomcard")
        End Try
    End Sub

    Private Sub tt_takecard(ByVal incoming As String, ByVal socket As Integer)
        Try

            Dim sItems() As String = incoming.Split(" ")

            ' 0 = lostvar
            ' 1 = gameid
            ' 2-6 = Cards

            Dim oChatFunctions As New ChatFunctions, oTables As New BusinessLayer.Tables

            Dim iTableID As Integer = oTables.FindPlayersTable(Tables, frmSystray.Winsock(socket).Tag)

            'Dim iGameID As Integer = tables(oTables.findplayerstable(tables, frmSystray.Winsock(socket).Tag)).gameid
            If sItems(1) = String.Empty Or Val(sItems(1)) = 0 Then Exit Sub

            GameID = Val(sItems(1))
            LoadRecord()

            Dim ThisPlrID As Integer = PlayerID(frmSystray.Winsock(socket).Tag)

            If TradeNone = True Or CardstoTake = 0 Then
                Call blockchat(String.Empty, "Error", "You cannot pick a card because the trade rule was none.", socket)
                oChatFunctions.uniwarn(String.Concat(frmSystray.Winsock(socket).Tag, " tried to pick a card in a TT game and the trade rule was none!"))
                Exit Sub
            End If

            Select Case ThisPlrID
                Case 1
                    If Player1_Score <= Player2_Score Then
                        Call blockchat(String.Empty, "Error", "You cannot pick a card because you did not win.", socket)
                        oChatFunctions.uniwarn(String.Concat(frmSystray.Winsock(socket).Tag, " tried to pick a card in a TT game that he did not win!"))
                        Exit Sub
                    End If
                Case 2
                    If Player2_Score <= Player1_Score Then
                        Call blockchat(String.Empty, "Error", "You cannot pick a card because you did not win.", socket)
                        oChatFunctions.uniwarn(String.Concat(frmSystray.Winsock(socket).Tag, " tried to pick a card in a TT game that he did not win!"))
                        Exit Sub
                    End If
            End Select

            If CardsTaken = True Then
                Call blockchat(String.Empty, "Error", "You cannot pick a card because you have already received cards for this game or you are not awarded cards.", socket)
                oChatFunctions.uniwarn(String.Concat(frmSystray.Winsock(socket).Tag, " tried to pick a card in a TT game for a second time (and failed)."))
                Exit Sub
            End If

            If sItems(2) <> "Nothing" And sItems(1) <> String.Empty Then
                For t As Integer = 2 To CardstoTake + 1
                    sItems(t) = sItems(t).Replace("%20", " ")

                    If findcardamt(IIf(ThisPlrID = 1, Player2, Player1), sItems(t)) <= 0 Then
                        Call cardchg(IIf(ThisPlrID = 1, Player2, Player1), sItems(t), 0, True)
                        oChatFunctions.uniwarn("WARNING ADMINS! " & IIf(ThisPlrID = 1, Player2, Player1) & " -- DUPE ALERT")
                        Exit Sub
                    End If

                    Call cardchg(IIf(ThisPlrID = 1, Player1, Player2), sItems(t), 1)

                    If findcardamt(IIf(ThisPlrID = 1, Player2, Player1), sItems(t)) = 0 Then
                        Call cardchg(IIf(ThisPlrID = 1, Player2, Player1), sItems(t), 0, True)
                    Else
                        Call cardchg(IIf(ThisPlrID = 1, Player2, Player1), sItems(t), -1)
                    End If
                Next t
            End If

            For t As Integer = 2 To 6
                sItems(t) = sItems(t).Replace(" ", "%20")
            Next t

            Send(String.Empty, "TT_CARDGAIN " & sItems(2) & " " & sItems(3) & " " & sItems(4) & " " & sItems(5) & " " & sItems(6), IIf(ThisPlrID = 1, GetSocket(Tables(itableid).player1), GetSocket(Tables(itableid).player2)))

            'For t As Integer = 2 To 6
            'If sItems(t) <> String.Empty Then Send(String.Empty, "CHATBLOCK Received You%20received%201%20" & sItems(t), IIf(ThisPlrID = 1, GetSocket(tables(itableid).player1), GetSocket(tables(itableid).player2)))
            'Next t

            Send(String.Empty, "TT_CARDLOST " & sItems(2) & " " & sItems(3) & " " & sItems(4) & " " & sItems(5) & " " & sItems(6), IIf(ThisPlrID = 1, GetSocket(Tables(itableid).player2), GetSocket(Tables(itableid).player1)))

            '            For t As Integer = 2 To 6
            ' If sItems(t) <> String.Empty Then Send(String.Empty, "CHATBLOCK Lost You%20lost%201%20" & sItems(t), IIf(ThisPlrID = 1, GetSocket(tables(itableid).player2), GetSocket(tables(itableid).player1)))
            'Next t

            CardsTaken = True
            Update()
        Catch ex As Exception
            Call errorsub(ex, "tt_takecard")
        End Try
    End Sub

    Private Sub tt_timeout(ByVal socket As Integer)
        Try
            Dim oTables As New BusinessLayer.Tables
            Dim iTableID As Integer = oTables.FindPlayersTable(Tables, frmSystray.Winsock(socket).Tag)
            Dim iGameID As Integer = Tables(iTableID).GameID

            GameID = iGameID
            LoadRecord()

            If GetSocket(Tables(iTableID).Player1) <> 0 Then Call blockchat(String.Empty, "Cancelled", "Your challenge was cancelled by the server because your opponent timed out.  Please try again later.", GetSocket(Tables(iTableID).Player1))
            If GetSocket(Tables(iTableID).Player2) <> 0 Then Call blockchat(String.Empty, "Cancelled", "Your challenge was cancelled by the server because your opponent timed out.  Please try again later.", GetSocket(Tables(iTableID).Player2))

            HistoryInsert(9999, String.Empty, String.Concat(frmSystray.Winsock(socket).Tag, " timed out."))
            oTables.SendKillMatch(Tables(iTableID))
            oTables.ClosePlayerTable(Tables, Player1, False)
            EndGame()
        Catch ex As Exception
            Call errorsub(ex, "tt_timeout")
        End Try
    End Sub

    Private Sub mycard(ByVal incoming As String, ByVal socket As Integer)
        Try
            Dim sCardVars() As String = incoming.Split(" ")

            Dim oTables As New BusinessLayer.Tables
            Dim iGameID As Integer = Tables(oTables.FindPlayersTable(Tables, frmSystray.Winsock(socket).Tag)).GameID
            Dim iTableID As Integer = oTables.FindPlayersTable(Tables, frmSystray.Winsock(socket).Tag)

            GameID = iGameID

            LoadRecord()

            'Record the cards information

            For x As Integer = 0 To 4
                If TradeNone = True Then
                    Player_Hand(PlayerID(frmSystray.Winsock(socket).Tag), x) = sCardVars(x + 1).Replace("%20", " ")
                Else
                    Dim oCardInfo As New LogicLayer.Cards.PlayingCardInfo
                    Dim oCard As Entities.BusinessObjects.PlayingCard = oCardInfo.GetPlayingCard(sCardVars(x + 1).Replace("%20", " "), Cards, CardNameList)

                    If oCard.SpecialCard = False Then
                        Player_Hand(PlayerID(frmSystray.Winsock(socket).Tag), x) = sCardVars(x + 1).Replace("%20", " ")
                    Else
                        Dim oTableFunctions As New TableFunctions
                        Dim oChatFunctions As New ChatFunctions
                        Dim oDatabaseFunctions As New DatabaseFunctions

                        Call blockchat(String.Empty, "Game Ended", String.Concat("Your match was ended because ", frmSystray.Winsock(socket).Tag, " tried to pick a special card in a trade game."), GetSocket(Tables(itableid).player1))
                        Call blockchat(String.Empty, "Game Ended", String.Concat("Your match was ended because ", frmSystray.Winsock(socket).Tag, " tried to pick a special card in a trade game."), GetSocket(Tables(itableid).player2))
                        oTableFunctions.EndMatch("ENDMATCH " & frmSystray.Winsock(socket).Tag, 0, False, True)
                        oChatFunctions.uniwarn(String.Concat(frmSystray.Winsock(socket).Tag, " tried to pick a special card in a trade game. GameID: ", GameID))
                        oDatabaseFunctions.HistoryInsert(frmSystray.Winsock(socket).Tag, "2", String.Concat(frmSystray.Winsock(socket).Tag, " tried to pick a special card in a trade game. GameID: ", GameID))
                        Exit Sub
                    End If
                End If
            Next x

            PlayerReady(PlayerID(frmSystray.Winsock(socket).Tag)) = True

            UpdateOrgHand()
            UpdateHand()
            Update()

            If PlayerReady(1) = True And PlayerReady(2) = True Then
                'Let's start this thing
                Call ready()
            End If
        Catch ex As Exception
            Call errorsub(ex, "mycard")
        End Try
    End Sub

    Private Sub SendOpenCards()
        'Send player 1 player 2's cards

        Dim oTables As New BusinessLayer.Tables
        Dim iTableID As Integer = oTables.FindPlayersTable(Tables, Player1)

        Send(String.Empty, String.Concat("TT_OPENCARD ", _
        Player_Hand(2, 0).Replace(" ", "%20"), " ", _
        Player_Hand(2, 1).Replace(" ", "%20"), " ", _
        Player_Hand(2, 2).Replace(" ", "%20"), " ", _
        Player_Hand(2, 3).Replace(" ", "%20"), " ", _
        Player_Hand(2, 4).Replace(" ", "%20")), GetSocket(Tables(itableid).player1))

        'Send player 2 player 1's cards
        Send(String.Empty, String.Concat("TT_OPENCARD ", _
        Player_Hand(1, 0).Replace(" ", "%20"), " ", _
        Player_Hand(1, 1).Replace(" ", "%20"), " ", _
        Player_Hand(1, 2).Replace(" ", "%20"), " ", _
        Player_Hand(1, 3).Replace(" ", "%20"), " ", _
        Player_Hand(1, 4).Replace(" ", "%20")), GetSocket(Tables(itableid).player2))
    End Sub

    Private Sub ready()
        Try
            Dim oTables As New BusinessLayer.Tables
            Dim iTableID As Integer = oTables.FindPlayersTable(Tables, Player1)

            Dim oGameFunctions As New GameFunctions

            Dim iGameStarter As Integer

            If Starter = True Then
                iGameStarter = Startee
            Else
                iGameStarter = oGameFunctions.PickStarter(False)
            End If

            If iGameStarter = 0 Then iGameStarter = 1
            If Open = True Then SendOpenCards()

            If ValidateCards() = True Then
                Send(String.Empty, String.Concat("TT_STARTER ", iGameStarter), GetSocket(Tables(itableid).player1))
                Send(String.Empty, String.Concat("TT_STARTER ", iGameStarter), GetSocket(Tables(itableid).player2))
            End If
        Catch ex As Exception
            Call errorsub(ex, "ready")
        End Try
    End Sub

    Private Function IsDupe(ByVal iPlayerID As Integer, ByVal sCard As String) As Boolean
        Dim iCount As Integer = 0

        For x As Integer = 0 To 4
            If sCard = Player_Hand(iPlayerID, x) Then iCount += 1
        Next

        If iCount > 1 Then
            Return True
        Else
            Return False
        End If
    End Function

    Private Function ValidateCards() As Boolean
        Try
            Dim oDataRow As DataRow = CheckCards()

            With oDataRow
                For y As Integer = 1 To 2
                    For x As Integer = 0 To 4
                        If (.Item(String.Concat("P", y, "Card", x)) = 0) Or (IsDupe(y, Player_Hand(y, x)) = True And .Item(String.Concat("P", y, "Card", x)) < 2) Then
                            Return False
                        End If
                    Next x
                Next y
            End With

            Return True
        Catch ex As Exception
            Call errorsub(ex, "ValidateCards")
        End Try
    End Function

    Public Function challengeyes(ByVal incoming As String, ByVal socket As Integer, Optional ByVal rulelist As String = "", Optional ByVal decklist As String = "") As String
        Dim sItems(40) As String, lostvar As String = String.Empty
        Dim oGameFunctions As New GameFunctions
        Dim oTables As New BusinessLayer.Tables

        Dim iTableID As Integer = oTables.FindPlayersTable(Tables, frmSystray.Winsock(socket).Tag)

        Player1 = Tables(iTableID).Player1
        Player2 = Tables(iTableID).Player2

        Divide(incoming, " ", lostvar, lostvar, sItems(2), sItems(3), sItems(4), sItems(5), sItems(6), sItems(7), sItems(8), sItems(9), sItems(10), sItems(11), sItems(12), sItems(13), sItems(14), _
        sItems(15), sItems(16), sItems(17), sItems(18), sItems(19), sItems(20), sItems(21), sItems(22), sItems(23), sItems(24), sItems(25), sItems(26), sItems(27), sItems(28), sItems(29), _
        sItems(30), sItems(31), sItems(32), sItems(33), sItems(34), sItems(35), sItems(36), sItems(37), sItems(38), sItems(39), sItems(40))

        Open = IIf(sItems(2) = "1", True, False)
        Random = IIf(sItems(3) = "1", True, False)
        TradeNone = IIf(sItems(4) = "1", True, False)
        TradeAll = IIf(sItems(5) = "1", True, False)
        TradeDiff = IIf(sItems(6) = "1", True, False)
        MaxLevel = IIf(sItems(7) = "1", True, False)
        MaxLevelValue = Val(sItems(8))
        CCRule = IIf(sItems(9) = "1", True, False)
        Same = IIf(sItems(10) = "1", True, False)
        Plus = IIf(sItems(11) = "1", True, False)
        RankFreeze = False 'IIf(sItems(12) = "1", True, False)
        MinLevel = IIf(sItems(13) = "1", True, False)
        MinLevelValue = Val(sItems(14))
        Wager = False ' IIf(sItems(15) = "1", True, False)
        WagerAmt = 0 'Val(sItems(16))

        IIf(WagerAmt < 0, WagerAmt = 0, False)

        Wall = IIf(sItems(17) = "1", True, False)
        TradeDirect = IIf(sItems(18) = "1", True, False)
        Combo = IIf(sItems(19) = "1", True, False)
        Mirror = IIf(sItems(20) = "1", True, False)
        Elemental = IIf(sItems(21) = "1", True, False)
        Neutral = IIf(sItems(22) = "1", True, False)
        Order = IIf(sItems(23) = "1", True, False)
        Cross = IIf(sItems(24) = "1", True, False)
        Reverse = IIf(sItems(25) = "1", True, False)
        Minus = IIf(sItems(26) = "1", True, False)
        SetWall = IIf(sItems(27) = "1", True, False)
        SetWallValue = Val(sItems(28))
        Immune = IIf(sItems(29) = "1", True, False)
        TradeTwo = IIf(sItems(30) = "1", True, False)
        TradeThree = IIf(sItems(31) = "1", True, False)
        TradeFour = IIf(sItems(32) = "1", True, False)
        Starter = IIf(sItems(33) = "1", True, False)
        Startee = Val(sItems(34))
        SuddenDeath = IIf(sItems(35) = "1", True, False)
        Skip = IIf(sItems(36) = "1", True, False)
        Capture = IIf(sItems(37) = "1", True, False)
        Multiply = IIf(sItems(38) = "1", True, False)
        TradeRandomOne = IIf(sItems(39) = "1", True, False)
        FullDeck = IIf(sItems(40) = "1", True, False)

        Times = 1

        'GetSocket(Tables(iTableID).Player1) = GetSocket(Player1)
        'GetSocket(Tables(iTableID).Player2) = socket

        If Player1 = String.Empty Then
            Call blockchat(String.Empty, "Cancelled", "Your challenge was cancelled by the server.  Please try again later.", socket)
            Return Nothing
        End If

        Send(String.Empty, String.Concat("CHALLENGEYES ", frmSystray.Winsock(socket).Tag), GetSocket(Tables(iTableID).Player1))
        GameID = oGameFunctions.NewGameID

        Decks = decklist.Trim

        If Random = True And FullDeck = False Then
            If RandomCards(Player1, 1) = True And RandomCards(Player2, 2) = True Then
                Send(String.Empty, "TT_PICKCARDS 0", GetSocket(Tables(iTableID).Player1))
                Send(String.Empty, "TT_PICKCARDS 0", GetSocket(Tables(iTableID).Player2))

                SendCards()
            Else
                Dim oTableFunctions As New BusinessLayer.Tables
                Call blockchat(String.Empty, "Cancelled", "Your challenge was cancelled by the server. One of the players may not have had enough cards to play with the decks that were chosen. Please try again later.", GetSocket(Tables(iTableID).Player1))
                Call blockchat(String.Empty, "Cancelled", "Your challenge was cancelled by the server.  One of the players may not have had enough cards to play with the decks that were chosen. Please try again later.", GetSocket(Tables(iTableID).Player2))
                'oTableFunctions.SendKillMatch(Tables(oTableFunctions.FindPlayersTable(Tables, Player1)))
                oTableFunctions.ClosePlayerTable(Tables, Player1, True)
                Return Nothing
            End If
        ElseIf FullDeck = True Then
            If FullRandomCards(1) = True And FullRandomCards(2) Then
                Send(String.Empty, "TT_PICKCARDS 0", GetSocket(Tables(iTableID).Player1))
                Send(String.Empty, "TT_PICKCARDS 0", GetSocket(Tables(iTableID).Player2))

                SendCards()
            Else
                Dim oTableFunctions As New BusinessLayer.Tables
                Call blockchat(String.Empty, "Cancelled", "Your challenge was cancelled by the server.  One of the players may not have had enough cards to play with the decks that were chosen. Please try again later.", GetSocket(Tables(iTableID).Player1))
                Call blockchat(String.Empty, "Cancelled", "Your challenge was cancelled by the server.  One of the players may not have had enough cards to play with the decks that were chosen. Please try again later.", GetSocket(Tables(iTableID).Player2))
                oTableFunctions.SendKillMatch(Tables(oTableFunctions.FindPlayersTable(Tables, Player1)))
                Return Nothing
            End If
        Else
            Send(String.Empty, "TT_PICKCARDS", GetSocket(Tables(iTableID).Player1))
            Send(String.Empty, "TT_PICKCARDS", GetSocket(Tables(iTableID).Player2))
        End If

        'Elemental Board

        If Elemental = True Then
            Randomize()

            Dim q As Integer = Int(Rnd() * 3) + 1
            If q < 0 Then q = 0

            If q <> 0 Then
                For r As Integer = 1 To q
                    Dim enumber As Integer = 0

                    Do Until enumber <= 100 And enumber <> 0
                        Randomize()
                        enumber = Int(Rnd() * 100) + 1
                    Loop

                    Randomize()
                    Dim i As Integer = Int(Rnd() * 9)
                    'If i < 0 Then i = 0
                    If i = 9 Then i = 8

                    If enumber <= 10 Then
                        Element(i) = "thunder"
                        Send(String.Empty, String.Concat("TT_ELEMENT ", i, " thunder"), GetSocket(Tables(iTableID).Player1))
                        Send(String.Empty, String.Concat("TT_ELEMENT ", i, " thunder"), GetSocket(Tables(iTableID).Player2))
                    ElseIf enumber <= 20 Then
                        Element(i) = "earth"
                        Send(String.Empty, String.Concat("TT_ELEMENT ", i, " earth"), GetSocket(Tables(iTableID).Player1))
                        Send(String.Empty, String.Concat("TT_ELEMENT ", i, " earth"), GetSocket(Tables(iTableID).Player2))
                    ElseIf enumber <= 30 Then
                        Element(i) = "ice"
                        Send(String.Empty, String.Concat("TT_ELEMENT ", i, " ice"), GetSocket(Tables(iTableID).Player1))
                        Send(String.Empty, String.Concat("TT_ELEMENT ", i, " ice"), GetSocket(Tables(iTableID).Player2))
                    ElseIf enumber <= 40 Then
                        Element(i) = "wind"
                        Send(String.Empty, String.Concat("TT_ELEMENT ", i, " wind"), GetSocket(Tables(iTableID).Player1))
                        Send(String.Empty, String.Concat("TT_ELEMENT ", i, " wind"), GetSocket(Tables(iTableID).Player2))
                    ElseIf enumber <= 50 Then
                        Element(i) = "poison"
                        Send(String.Empty, String.Concat("TT_ELEMENT ", i, " poison"), GetSocket(Tables(iTableID).Player1))
                        Send(String.Empty, String.Concat("TT_ELEMENT ", i, " poison"), GetSocket(Tables(iTableID).Player2))
                    ElseIf enumber <= 60 Then
                        Element(i) = "holy"
                        Send(String.Empty, String.Concat("TT_ELEMENT ", i, " holy"), GetSocket(Tables(iTableID).Player1))
                        Send(String.Empty, String.Concat("TT_ELEMENT ", i, " holy"), GetSocket(Tables(iTableID).Player2))
                    ElseIf enumber <= 70 Then
                        Element(i) = "fire"
                        Send(String.Empty, String.Concat("TT_ELEMENT ", i, " fire"), GetSocket(Tables(iTableID).Player1))
                        Send(String.Empty, String.Concat("TT_ELEMENT ", i, " fire"), GetSocket(Tables(iTableID).Player2))
                    ElseIf enumber <= 80 Then
                        Element(i) = "water"
                        Send(String.Empty, String.Concat("TT_ELEMENT ", i, " water"), GetSocket(Tables(iTableID).Player1))
                        Send(String.Empty, String.Concat("TT_ELEMENT ", i, " water"), GetSocket(Tables(iTableID).Player2))
                    ElseIf enumber <= 90 Then
                        Element(i) = "mechanical"
                        Send(String.Empty, String.Concat("TT_ELEMENT ", i, " mechanical"), GetSocket(Tables(iTableID).Player1))
                        Send(String.Empty, String.Concat("TT_ELEMENT ", i, " mechanical"), GetSocket(Tables(iTableID).Player2))
                    Else
                        Element(i) = "shadow"
                        Send(String.Empty, String.Concat("TT_ELEMENT ", i, " shadow"), GetSocket(Tables(iTableID).Player1))
                        Send(String.Empty, String.Concat("TT_ELEMENT ", i, " shadow"), GetSocket(Tables(iTableID).Player2))
                    End If
                Next r
            End If
        End If

        If oServerConfig.TTJackPotEnabled = True Then
            Static RandomNumGen As New System.Random
            Dim iRandom As Integer = RandomNumGen.Next(0, oServerConfig.TTJackpotMax)

            If iRandom = 10 Then
                For r As Integer = 0 To 8
                    If Element(r) = String.Empty Then
                        Dim p As Integer = RandomNumGen.Next(0, 8)

                        Element(p) = "jackpot"
                        Send(String.Empty, String.Concat("TT_ELEMENT ", p, " jackpot"), GetSocket(Tables(iTableID).Player1))
                        Send(String.Empty, String.Concat("TT_ELEMENT ", p, " jackpot"), GetSocket(Tables(iTableID).Player2))
                        Exit For
                    End If
                Next r
            End If
        End If

        GameInsert()

        'Dim oChatFunctions As New ChatFunctions
        'oChatFunctions.unisend("CHALLENGESTART", String.Concat(Player1, " ", Player2, " 1"))

        Return GameID.ToString
    End Function

    Private Sub tt_placeat(ByVal incoming As String, ByVal socket As Integer)
        Dim SquareId, PlrSquareID, ThisPlrId As Integer
        Dim sCard As String

        Dim sItems() As String = incoming.Split(" ")

        Dim oTables As New BusinessLayer.Tables
        Dim iTableID As Integer = oTables.FindPlayersTable(Tables, frmSystray.Winsock(socket).Tag)

        GameID = Tables(oTables.FindPlayersTable(Tables, frmSystray.Winsock(socket).Tag)).GameID

        LoadRecord()

        SquareId = Val(sItems(1))
        PlrSquareID = Val(sItems(2))
        ThisPlrId = PlayerID(frmSystray.Winsock(socket).Tag)

        sCard = Player_Hand(ThisPlrId, PlrSquareID)

        If IsEmpty(SquareId) = False Then
            Dim oChatFunctions As New ChatFunctions
            Dim objTableFunctions As New TableFunctions

            Call blockchat(String.Empty, "Error", String.Concat(frmSystray.Winsock(socket).Tag, " placed a card on the board that already had a card in it.  Game was terminated."), GetSocket(Tables(itableid).player1))
            Call blockchat(String.Empty, "Error", String.Concat(frmSystray.Winsock(socket).Tag, " placed a card on the board that already had a card in it.  Game was terminated."), GetSocket(Tables(itableid).player2))

            Call objTableFunctions.EndMatch("ENDMATCH " & frmSystray.Winsock(socket).Tag, 0, False, True)
            Exit Sub
        ElseIf sCard = String.Empty Then
            Dim oGameFunctions As New GameFunctions
            oGameFunctions.KillGame(GetSocket(Tables(itableid).player1), GetSocket(Tables(itableid).player2), frmSystray.Winsock(socket).Tag, String.Concat("Your game was ended because ", frmSystray.Winsock(socket).Tag, " tried to place a blank card on the board."))
            Exit Sub
        End If

        Board(SquareId) = sCard
        PlacedBy(SquareId) = ThisPlrId

        If ThisPlrId = 1 Then
            Send(String.Empty, String.Concat("TT_PLACEDCARD ", SquareId, " ", PlrSquareID, " ", Replace(sCard, " ", "%20")), GetSocket(Tables(itableid).player2))
            CardColor(SquareId) = "blue"
            Player_Hand(ThisPlrId, PlrSquareID) = String.Empty
        Else
            Send(String.Empty, String.Concat("TT_PLACEDCARD ", SquareId, " ", PlrSquareID, " ", Replace(sCard, " ", "%20")), GetSocket(Tables(itableid).player1))
            CardColor(SquareId) = "red"
            Player_Hand(ThisPlrId, PlrSquareID) = String.Empty
        End If

        Dim oCardInfo As New LogicLayer.Cards.PlayingCardInfo
        Dim oCard As Entities.BusinessObjects.PlayingCard = oCardInfo.GetPlayingCard(sCard, Cards, CardNameList)

        HistoryInsert(SquareId, oCard.CardName, "Card Played")
        Analyze(SquareId, oCard, ThisPlrId)
        'System.Windows.Forms.Application.DoEvents()
    End Sub

    Private Sub Analyze(ByVal iSquareID As Integer, ByVal oCard As Entities.BusinessObjects.PlayingCard, ByVal ThisPlrId As Integer)
        Try
            If Elemental = True Then Call analyze_elemental(iSquareID, oCard)
            If Same = True Then Call analyze_same(iSquareID, oCard, ThisPlrId)
            If Plus = True Then Call analyze_plus(iSquareID, oCard, ThisPlrId)
            If Minus = True Then Call analyze_minus(iSquareID, oCard, ThisPlrId)
            If Multiply = True Then Call analyze_multiply(iSquareID, oCard, ThisPlrId)
            If Wall = True Then Call analyze_wall(iSquareID, oCard, ThisPlrId)
            If SetWall = True Then Call analyze_setwall(iSquareID, oCard, ThisPlrId)
            If Mirror = True Then Call analyze_mirror(iSquareID, oCard, ThisPlrId)
            If Neutral = True Then Call analyze_neutral(iSquareID, oCard, ThisPlrId)
            If Cross = True Then Call analyze_cross(iSquareID, oCard, ThisPlrId)
            If Skip = True Then Call analyze_skip(iSquareID, oCard, ThisPlrId)

            Dim oTables As New BusinessLayer.Tables
            Dim iTableID As Integer = oTables.FindPlayersTable(Tables, Player1)

            If Reverse = False Then
                If Immune = False Then
                    Call analyze_basic(iSquareID, oCard, ThisPlrId)
                End If
            Else
                If Immune = False Then
                    Call analyze_reverse(iSquareID, oCard, ThisPlrId)
                End If
            End If

            Update()
            UpdateHand()

            If IsGameOver() = False Then
                If ThisPlrId = 1 Then
                    Send(String.Empty, "TT_TURNOVER", GetSocket(Tables(itableid).player1))
                    Send(String.Empty, "TT_YOURTURN", GetSocket(Tables(itableid).player2))
                    'System.Windows.Forms.Application.DoEvents()
                Else
                    Send(String.Empty, "TT_TURNOVER", GetSocket(Tables(itableid).player2))
                    Send(String.Empty, "TT_YOURTURN", GetSocket(Tables(itableid).player1))
                    'System.Windows.Forms.Application.DoEvents()
                End If
            Else
                GameIsOver()
            End If
        Catch ex As Exception
            Call errorsub(ex, "Analyze")
        End Try
    End Sub

    Private Sub analyze_cross(ByVal iSquareID As Integer, ByVal oCard As Entities.BusinessObjects.PlayingCard, ByVal ThisPlrId As Integer)
        ' Cross rule checks around the wall to find out if 2 cards have greater values
        ' and flips it if it is greater

        Select Case iSquareID
            Case 0
                MorePower(iSquareID, ThisPlrId, oCard, "Left", 2, "Right")
                MorePower(iSquareID, ThisPlrId, oCard, "Up", 6, "Down")
            Case 1
                MorePower(iSquareID, ThisPlrId, oCard, "Up", 7, "Down")
            Case 2
                MorePower(iSquareID, ThisPlrId, oCard, "Right", 0, "Left")
                MorePower(iSquareID, ThisPlrId, oCard, "Up", 8, "Down")
            Case 3
                MorePower(iSquareID, ThisPlrId, oCard, "Left", 5, "Right")
            Case 5
                MorePower(iSquareID, ThisPlrId, oCard, "Right", 3, "Left")
            Case 6
                MorePower(iSquareID, ThisPlrId, oCard, "Left", 8, "Right")
                MorePower(iSquareID, ThisPlrId, oCard, "Down", 0, "Up")
            Case 7
                MorePower(iSquareID, ThisPlrId, oCard, "Down", 1, "Up")
            Case 8
                MorePower(iSquareID, ThisPlrId, oCard, "Right", 6, "Left")
                MorePower(iSquareID, ThisPlrId, oCard, "Down", 2, "Up")
        End Select
    End Sub

    Private Sub analyze_wall(ByVal iSquareID As Integer, ByVal oCard As Entities.BusinessObjects.PlayingCard, ByVal ThisPlrId As Integer)
        Call analyze_wallcombos("same", iSquareID, oCard, ThisPlrId)
        Call analyze_wallcombos("plus", iSquareID, oCard, ThisPlrId)
        Call analyze_wallcombos("minus", iSquareID, oCard, ThisPlrId)
    End Sub

    Private Sub analyze_setwall(ByVal iSquareID As Integer, ByVal oCard As Entities.BusinessObjects.PlayingCard, ByVal ThisPlrId As Integer)
        Call analyze_setwallcombos("same", iSquareID, oCard, ThisPlrId)
        Call analyze_setwallcombos("plus", iSquareID, oCard, ThisPlrId)
        Call analyze_setwallcombos("minus", iSquareID, oCard, ThisPlrId)
    End Sub

    Private Sub analyze_mirror(ByVal iSquareID As Integer, ByVal oCard As Entities.BusinessObjects.PlayingCard, ByVal ThisPlrId As Integer)
        Call analyze_mirrorcombos("same", iSquareID, oCard, ThisPlrId)
        Call analyze_mirrorcombos("plus", iSquareID, oCard, ThisPlrId)
    End Sub

    Private Sub analyze_neutral(ByVal iSquareID As Integer, ByVal oCard As Entities.BusinessObjects.PlayingCard, ByVal ThisPlrId As Integer)
        Call analyze_neutralcombos("same", iSquareID, oCard, ThisPlrId)
        Call analyze_neutralcombos("plus", iSquareID, oCard, ThisPlrId)
        Call analyze_neutralcombos("minus", iSquareID, oCard, ThisPlrId)
    End Sub

    Private Sub analyze_elemental(ByVal iSquareID As Integer, ByVal oCard As Entities.BusinessObjects.PlayingCard)
        Try
            Dim oTables As New BusinessLayer.Tables
            Dim iTableID As Integer = oTables.FindPlayersTable(Tables, Player1)

            If Element(iSquareID) = String.Empty Then
                'EValue(iSquareID) = 0
            ElseIf oCard.Element = Element(iSquareID) Then
                EValue(iSquareID) = 1
                Send(String.Empty, String.Concat("TT_PLUSELEMENT ", iSquareID, " 1"), GetSocket(Tables(itableid).player1))
                Send(String.Empty, String.Concat("TT_PLUSELEMENT ", iSquareID, " 1"), GetSocket(Tables(itableid).player2))
            ElseIf oCard.Element <> Element(iSquareID) Then
                EValue(iSquareID) = -1
                Send(String.Empty, String.Concat("TT_MINUSELEMENT ", iSquareID, " -1"), GetSocket(Tables(itableid).player1))
                Send(String.Empty, String.Concat("TT_MINUSELEMENT ", iSquareID, " -1"), GetSocket(Tables(itableid).player2))
            End If
        Catch ex As Exception
            Call errorsub(ex, "analyze_elemental")
        End Try
    End Sub

    Private Sub analyze_same(ByVal iSquareID As Integer, ByVal oCard As Entities.BusinessObjects.PlayingCard, ByVal ThisPlrId As Integer)
        Select Case iSquareID
            Case 0
                ' Checks 0 and 1 square and 0 and 3 square
                Call analyze_checksame(oCard, iSquareID, "Right", "Down", 1, "Left", 3, "Up", ThisPlrId)
            Case 1
                ' Check 0, 1 and 1, 4 square
                'Check Left(1), Right(0) and Down(1), Up(4)
                Call analyze_checksame(oCard, iSquareID, "Left", "Down", 0, "Right", 4, "Up", ThisPlrId)
                'Check Left(1), Right(0) and Right(1), Left(2)
                Call analyze_checksame(oCard, iSquareID, "Left", "Right", 0, "Right", 2, "Left", ThisPlrId)
                'Check Right(1), Left(2) and Down(1), Up(4)
                Call analyze_checksame(oCard, iSquareID, "Right", "Down", 2, "Left", 4, "Up", ThisPlrId)
            Case 2
                Call analyze_checksame(oCard, iSquareID, "Left", "Down", 1, "Right", 5, "Up", ThisPlrId)
            Case 3
                ' Checking 0, 3 and 3, 4
                'Check Up(3), Down(0) and Right(3), Left(4)
                Call analyze_checksame(oCard, iSquareID, "Up", "Right", 0, "Down", 4, "Left", ThisPlrId)
                'Check Up(3), Down(0) and Down(3), Up(6)
                Call analyze_checksame(oCard, iSquareID, "Up", "Down", "0", "Down", 6, "Up", ThisPlrId)
                'Check Right(3), Left(4) and Down(3), Up(6)
                Call analyze_checksame(oCard, iSquareID, "Right", "Down", 4, "Left", 6, "Up", ThisPlrId)
            Case 4
                'Check Up(4), Down(1) and Left(4), Right(3)
                Call analyze_checksame(oCard, iSquareID, "Up", "Left", 1, "Down", 3, "Right", ThisPlrId)
                'Check Up(4), Down(1) and Down(4), Up(7)
                Call analyze_checksame(oCard, iSquareID, "Up", "Down", 1, "Down", 7, "Up", ThisPlrId)
                'Check Up(4), Down(1) and Right(4), Left(5)
                Call analyze_checksame(oCard, iSquareID, "Up", "Right", 1, "Down", 5, "Left", ThisPlrId)
                'Check Left(4), Right(3) and Down(4), Up(7)
                Call analyze_checksame(oCard, iSquareID, "Left", "Down", 3, "Right", 7, "Up", ThisPlrId)
                'Check Left(4), Right(3) and Right(4), Left(5)
                Call analyze_checksame(oCard, iSquareID, "Left", "Right", 3, "Right", 5, "Left", ThisPlrId)
                'Check Down(4), Up(7) and Right(4), Left(5)
                Call analyze_checksame(oCard, iSquareID, "Down", "Right", 7, "Up", 5, "Left", ThisPlrId)
            Case 5
                'Check Up(5), Down(2) and Left(5), Right(4)
                Call analyze_checksame(oCard, iSquareID, "Up", "Left", 2, "Down", 4, "Right", ThisPlrId)
                'Check Up(5), Down(2) and Down(5), Up(8)
                Call analyze_checksame(oCard, iSquareID, "Up", "Down", 2, "Down", 8, "Up", ThisPlrId)
                'Check Left(5), Right(4) and Down(5), Up(8)
                Call analyze_checksame(oCard, iSquareID, "Left", "Down", 4, "Right", 8, "Up", ThisPlrId)
            Case 6
                'Check Up(6), Down(3) and Right(6), Left(7)
                Call analyze_checksame(oCard, iSquareID, "Up", "Right", 3, "Down", 7, "Left", ThisPlrId)
            Case 7
                'Check Up(7), Down(4) and Left(7), Right(6)
                Call analyze_checksame(oCard, iSquareID, "Up", "Left", 4, "Down", 6, "Right", ThisPlrId)
                'Check Up(7), Down(4) and Right(7), Left(8)
                Call analyze_checksame(oCard, iSquareID, "Up", "Right", 4, "Down", 8, "Left", ThisPlrId)
                'Check Left(7), Right(6) and Right(7), Left(8)
                Call analyze_checksame(oCard, iSquareID, "Left", "Right", 6, "Right", 8, "Left", ThisPlrId)
            Case 8
                'Check Up(8), Down(5) and Left(8), Right(7)
                Call analyze_checksame(oCard, iSquareID, "Up", "Left", 5, "Down", 7, "Right", ThisPlrId)

        End Select
    End Sub

    Private Sub analyze_checksame(ByVal oCard As Entities.BusinessObjects.PlayingCard, ByVal iSquareID As Integer, ByVal psdir1 As String, ByVal psdir2 As String, ByVal neigh1 As Integer, ByVal neigh1dir As String, ByVal neigh2 As Integer, ByVal neigh2dir As String, ByVal ThisPlrId As Integer)
        Try
            Dim pointchange As Integer
            Dim part1, ignoremove, part2 As Boolean

            If IsEmpty(neigh1) = True Or IsEmpty(neigh2) = True Then ignoremove = True

            If ignoremove = False Then
                Dim oCardInfo As New LogicLayer.Cards.PlayingCardInfo

                Dim oNeighbor1Card As Entities.BusinessObjects.PlayingCard = oCardInfo.GetPlayingCard(Board(neigh1), Cards, CardNameList)
                Dim oNeighbor2Card As Entities.BusinessObjects.PlayingCard = oCardInfo.GetPlayingCard(Board(neigh2), Cards, CardNameList)

                If (oCard.DirectionValue(psdir1) + EValue(iSquareID) = oNeighbor1Card.DirectionValue(neigh1dir) + EValue(neigh1)) _
                    And (oCard.DirectionValue(psdir2) + EValue(iSquareID) = oNeighbor2Card.DirectionValue(neigh2dir) + EValue(neigh2)) Then

                    Call SendTTCardData(IIf(ThisPlrId = 1, "blue", "red"), iSquareID, oCard)
                    Call SendTTCardData(IIf(ThisPlrId = 1, "blue", "red"), neigh1, oNeighbor1Card)
                    Call SendTTCardFlip(iSquareID, neigh1)

                    Call SendTTCardData(IIf(ThisPlrId = 1, "blue", "red"), neigh2, oNeighbor2Card)
                    Call SendTTCardFlip(iSquareID, neigh2)

                    If CardColor(neigh1) = IIf(ThisPlrId = 1, "red", "blue") Then
                        pointchange += 1
                        part1 = True
                    End If

                    If CardColor(neigh2) = IIf(ThisPlrId = 1, "red", "blue") Then
                        pointchange += 1
                        part2 = True
                    End If

                    CardColor(iSquareID) = IIf(ThisPlrId = 1, "blue", "red")
                    CardColor(neigh1) = IIf(ThisPlrId = 1, "blue", "red")
                    CardColor(neigh2) = IIf(ThisPlrId = 1, "blue", "red")

                    HistoryInsert(iSquareID, oCard.CardName, String.Concat("Same >> ", iSquareID, ", ", neigh1, ", ", neigh2))
                    Call SameAlert(pointchange)
                    Call SendPoints(pointchange, ThisPlrId)

                    If Combo = True Then
                        If part1 = True Then Call analyze_combo(neigh1, oNeighbor1Card, ThisPlrId)
                        If part2 = True Then Call analyze_combo(neigh2, oNeighbor2Card, ThisPlrId)
                    End If
                End If
            End If
        Catch ex As Exception
            Call errorsub(ex, "analyze_checksame")
        End Try
    End Sub

    Private Sub analyze_combo(ByVal iSquareID As Integer, ByVal oCard As Entities.BusinessObjects.PlayingCard, ByVal ThisPlrId As Integer)
        On Error GoTo errhandler

        Select Case iSquareID
            Case 0
                Call analyze_checkcombo(oCard, ThisPlrId, "0", "Right", 1, "Left")
                Call analyze_checkcombo(oCard, ThisPlrId, "0", "Down", 3, "Up")
            Case 1
                Call analyze_checkcombo(oCard, ThisPlrId, 1, "Left", "0", "Right")
                Call analyze_checkcombo(oCard, ThisPlrId, 1, "Down", 4, "Up")
                Call analyze_checkcombo(oCard, ThisPlrId, 1, "Right", 2, "Left")
            Case 2
                Call analyze_checkcombo(oCard, ThisPlrId, 2, "Left", 1, "Right")
                Call analyze_checkcombo(oCard, ThisPlrId, 2, "Down", 5, "Up")
            Case 3
                Call analyze_checkcombo(oCard, ThisPlrId, 3, "Up", "0", "Down")
                Call analyze_checkcombo(oCard, ThisPlrId, 3, "Right", 4, "Left")
                Call analyze_checkcombo(oCard, ThisPlrId, 3, "Down", 6, "Up")
            Case 4
                Call analyze_checkcombo(oCard, ThisPlrId, 4, "Up", 1, "Down")
                Call analyze_checkcombo(oCard, ThisPlrId, 4, "Left", 3, "Right")
                Call analyze_checkcombo(oCard, ThisPlrId, 4, "Down", 7, "Up")
                Call analyze_checkcombo(oCard, ThisPlrId, 4, "Right", 5, "Left")
            Case 5
                Call analyze_checkcombo(oCard, ThisPlrId, 5, "Up", 2, "Down")
                Call analyze_checkcombo(oCard, ThisPlrId, 5, "Left", 4, "Right")
                Call analyze_checkcombo(oCard, ThisPlrId, 5, "Down", 8, "Up")
            Case 6
                Call analyze_checkcombo(oCard, ThisPlrId, 6, "Up", 3, "Down")
                Call analyze_checkcombo(oCard, ThisPlrId, 6, "Right", 7, "Left")
            Case 7
                Call analyze_checkcombo(oCard, ThisPlrId, 7, "Left", 6, "Right")
                Call analyze_checkcombo(oCard, ThisPlrId, 7, "Up", 4, "Down")
                Call analyze_checkcombo(oCard, ThisPlrId, 7, "Right", 8, "Left")
            Case 8
                Call analyze_checkcombo(oCard, ThisPlrId, 8, "Left", 7, "Right")
                Call analyze_checkcombo(oCard, ThisPlrId, 8, "Up", 5, "Down")
        End Select

        Exit Sub
errhandler:
        Call errorsub(Err.Description, "combosub")
    End Sub

    Private Sub analyze_checkcombo(ByVal oCard As Entities.BusinessObjects.PlayingCard, ByVal ThisPlrId As Integer, ByVal iSquareID As Integer, ByVal psdir As String, ByVal neighbor As String, ByVal neighdir As String)
        Try
            Dim pointchange As Integer
            Dim ignoremove, part1 As Boolean

            If IsEmpty(neighbor) = True Then ignoremove = True

            If ignoremove = False Then
                Dim oCardInfo As New LogicLayer.Cards.PlayingCardInfo
                Dim oNeighbor1Card As Entities.BusinessObjects.PlayingCard = oCardInfo.GetPlayingCard(Board(neighbor), Cards, CardNameList)

                'If ocard.DirectionValue(psdir) + evalue(isquareid) = 0 Then ignoremove = True

                If (oCard.DirectionValue(psdir) + EValue(iSquareID)) > (oNeighbor1Card.DirectionValue(neighdir) + EValue(neighbor)) Then
                    Call SendTTCardData(IIf(ThisPlrId = 1, "blue", "red"), iSquareID, oCard)
                    Call SendTTCardData(IIf(ThisPlrId = 1, "blue", "red"), neighbor, oNeighbor1Card)
                    Call SendTTCardFlip(iSquareID, neighbor)

                    If CardColor(neighbor) = IIf(ThisPlrId = 1, "red", "blue") Then
                        pointchange += 1
                        part1 = True
                    End If

                    CardColor(iSquareID) = IIf(ThisPlrId = 1, "blue", "red")
                    CardColor(neighbor) = IIf(ThisPlrId = 1, "blue", "red")

                    Call ComboAlert(pointchange)
                    Call SendPoints(pointchange, ThisPlrId)
                    HistoryInsert(iSquareID, oCard.CardName, String.Concat("Combo >> ", iSquareID, ", ", neighbor))

                    If part1 = True Then Call analyze_combo(neighbor, oNeighbor1Card, ThisPlrId)
                End If
            End If
        Catch ex As Exception
            Call errorsub(ex, "analyze_checkcombo")
        End Try
    End Sub

    Private Sub analyze_minus(ByVal iSquareID As Integer, ByVal oCard As Entities.BusinessObjects.PlayingCard, ByVal ThisPlrId As Integer)
        Select Case iSquareID

            Case 0
                Call analyze_checkminus(oCard, iSquareID, "Right", "Down", 1, "Left", 3, "Up", ThisPlrId)
            Case 1
                'Check Left(1), Right(0) and Down(1), Up(4)
                Call analyze_checkminus(oCard, iSquareID, "Left", "Down", 0, "Right", 4, "Up", ThisPlrId)
                'Check Left(1), Right(0) and Right(1), Left(2)
                Call analyze_checkminus(oCard, iSquareID, "Left", "Right", 0, "Right", 2, "Left", ThisPlrId)
                'Check Right(1), Left(2) and Down(1), Up(4)
                Call analyze_checkminus(oCard, iSquareID, "Right", "Down", 2, "Left", 4, "Up", ThisPlrId)
            Case 2
                'Check Left(2), Right(1) and Down(2), Up(5)
                Call analyze_checkminus(oCard, iSquareID, "Left", "Down", 1, "Right", 5, "Up", ThisPlrId)
            Case 3
                'Check Up(3), Down(0) and Right(3), Left(4)
                Call analyze_checkminus(oCard, iSquareID, "Up", "Right", 0, "Down", 4, "Left", ThisPlrId)
                'Check Up(3), Down(0) and Down(3), Up(6)
                Call analyze_checkminus(oCard, iSquareID, "Up", "Down", 0, "Down", 6, "Up", ThisPlrId)
                'Check Right(3), Left(4) and Down(3), Up(6)
                Call analyze_checkminus(oCard, iSquareID, "Right", "Down", 4, "Left", 6, "Up", ThisPlrId)
            Case 4
                'Check Up(4), Down(1) and Left(4), Right(3)
                Call analyze_checkminus(oCard, iSquareID, "Up", "Left", 1, "Down", 3, "Right", ThisPlrId)
                'Check Up(4), Down(1) and Down(4), Up(7)
                Call analyze_checkminus(oCard, iSquareID, "Up", "Down", 1, "Down", 7, "Up", ThisPlrId)
                'Check Up(4), Down(1) and Right(4), Left(5)
                Call analyze_checkminus(oCard, iSquareID, "Up", "Right", 1, "Down", 5, "Left", ThisPlrId)
                'Check Left(4), Right(3) and Down(4), Up(7)
                Call analyze_checkminus(oCard, iSquareID, "Left", "Down", 3, "Right", 7, "Up", ThisPlrId)
                'Check Left(4), Right(3) and Right(4), Left(5)
                Call analyze_checkminus(oCard, iSquareID, "Left", "Right", 3, "Right", 5, "Left", ThisPlrId)
                'Check Down(4), Up(7) and Right(4), Left(5)
                Call analyze_checkminus(oCard, iSquareID, "Down", "Right", 7, "Up", 5, "Left", ThisPlrId)
            Case 5
                'Check Up(5), Down(2) and Left(5), Right(4)
                Call analyze_checkminus(oCard, iSquareID, "Up", "Left", 2, "Down", 4, "Right", ThisPlrId)
                'Check Up(5), Down(2) and Down(5), Up(8)
                Call analyze_checkminus(oCard, iSquareID, "Up", "Down", 2, "Down", 8, "Up", ThisPlrId)
                'Check Left(5), Right(4) and Down(5), Up(8)
                Call analyze_checkminus(oCard, iSquareID, "Left", "Down", 4, "Right", 8, "Up", ThisPlrId)
            Case 6
                'Check Up(6), Down(3) and Right(6), Left(7)
                Call analyze_checkminus(oCard, iSquareID, "Up", "Right", 3, "Down", 7, "Left", ThisPlrId)
            Case 7
                'Check Up(7), Down(4) and Left(7), Right(6)
                Call analyze_checkminus(oCard, iSquareID, "Up", "Left", 4, "Down", 6, "Right", ThisPlrId)
                'Check Up(7), Down(4) and Right(7), Left(8)
                Call analyze_checkminus(oCard, iSquareID, "Up", "Right", 4, "Down", 8, "Left", ThisPlrId)
                'Check Left(7), Right(6) and Right(7), Left(8)
                Call analyze_checkminus(oCard, iSquareID, "Left", "Right", 6, "Right", 8, "Left", ThisPlrId)
            Case 8
                'Check Up(8), Down(5) and Left(8), Right(7)
                Call analyze_checkminus(oCard, iSquareID, "Up", "Left", 5, "Down", 7, "Right", ThisPlrId)
        End Select
    End Sub

    Private Sub analyze_multiply(ByVal iSquareID As Integer, ByVal oCard As Entities.BusinessObjects.PlayingCard, ByVal ThisPlrId As Integer)
        Select Case iSquareID

            Case 0
                Call analyze_checkmultiply(oCard, iSquareID, "Right", "Down", 1, "Left", 3, "Up", ThisPlrId)
            Case 1
                'Check Left(1), Right(0) and Down(1), Up(4)
                Call analyze_checkmultiply(oCard, iSquareID, "Left", "Down", 0, "Right", 4, "Up", ThisPlrId)
                'Check Left(1), Right(0) and Right(1), Left(2)
                Call analyze_checkmultiply(oCard, iSquareID, "Left", "Right", 0, "Right", 2, "Left", ThisPlrId)
                'Check Right(1), Left(2) and Down(1), Up(4)
                Call analyze_checkmultiply(oCard, iSquareID, "Right", "Down", 2, "Left", 4, "Up", ThisPlrId)
            Case 2
                'Check Left(2), Right(1) and Down(2), Up(5)
                Call analyze_checkmultiply(oCard, iSquareID, "Left", "Down", 1, "Right", 5, "Up", ThisPlrId)
            Case 3
                'Check Up(3), Down(0) and Right(3), Left(4)
                Call analyze_checkmultiply(oCard, iSquareID, "Up", "Right", 0, "Down", 4, "Left", ThisPlrId)
                'Check Up(3), Down(0) and Down(3), Up(6)
                Call analyze_checkmultiply(oCard, iSquareID, "Up", "Down", 0, "Down", 6, "Up", ThisPlrId)
                'Check Right(3), Left(4) and Down(3), Up(6)
                Call analyze_checkmultiply(oCard, iSquareID, "Right", "Down", 4, "Left", 6, "Up", ThisPlrId)
            Case 4
                'Check Up(4), Down(1) and Left(4), Right(3)
                Call analyze_checkmultiply(oCard, iSquareID, "Up", "Left", 1, "Down", 3, "Right", ThisPlrId)
                'Check Up(4), Down(1) and Down(4), Up(7)
                Call analyze_checkmultiply(oCard, iSquareID, "Up", "Down", 1, "Down", 7, "Up", ThisPlrId)
                'Check Up(4), Down(1) and Right(4), Left(5)
                Call analyze_checkmultiply(oCard, iSquareID, "Up", "Right", 1, "Down", 5, "Left", ThisPlrId)
                'Check Left(4), Right(3) and Down(4), Up(7)
                Call analyze_checkmultiply(oCard, iSquareID, "Left", "Down", 3, "Right", 7, "Up", ThisPlrId)
                'Check Left(4), Right(3) and Right(4), Left(5)
                Call analyze_checkmultiply(oCard, iSquareID, "Left", "Right", 3, "Right", 5, "Left", ThisPlrId)
                'Check Down(4), Up(7) and Right(4), Left(5)
                Call analyze_checkmultiply(oCard, iSquareID, "Down", "Right", 7, "Up", 5, "Left", ThisPlrId)
            Case 5
                'Check Up(5), Down(2) and Left(5), Right(4)
                Call analyze_checkmultiply(oCard, iSquareID, "Up", "Left", 2, "Down", 4, "Right", ThisPlrId)
                'Check Up(5), Down(2) and Down(5), Up(8)
                Call analyze_checkmultiply(oCard, iSquareID, "Up", "Down", 2, "Down", 8, "Up", ThisPlrId)
                'Check Left(5), Right(4) and Down(5), Up(8)
                Call analyze_checkmultiply(oCard, iSquareID, "Left", "Down", 4, "Right", 8, "Up", ThisPlrId)
            Case 6
                'Check Up(6), Down(3) and Right(6), Left(7)
                Call analyze_checkmultiply(oCard, iSquareID, "Up", "Right", 3, "Down", 7, "Left", ThisPlrId)
            Case 7
                'Check Up(7), Down(4) and Left(7), Right(6)
                Call analyze_checkmultiply(oCard, iSquareID, "Up", "Left", 4, "Down", 6, "Right", ThisPlrId)
                'Check Up(7), Down(4) and Right(7), Left(8)
                Call analyze_checkmultiply(oCard, iSquareID, "Up", "Right", 4, "Down", 8, "Left", ThisPlrId)
                'Check Left(7), Right(6) and Right(7), Left(8)
                Call analyze_checkmultiply(oCard, iSquareID, "Left", "Right", 6, "Right", 8, "Left", ThisPlrId)
            Case 8
                'Check Up(8), Down(5) and Left(8), Right(7)
                Call analyze_checkmultiply(oCard, iSquareID, "Up", "Left", 5, "Down", 7, "Right", ThisPlrId)
        End Select
    End Sub

    Private Sub analyze_checkminus(ByVal oCard As Entities.BusinessObjects.PlayingCard, ByVal iSquareID As Integer, ByVal psdir1 As String, ByVal psdir2 As String, ByVal neigh1 As Integer, ByVal neigh1dir As String, ByVal neigh2 As Integer, ByVal neigh2dir As String, ByVal ThisPlrId As Integer)
        Try

            Dim pointchange As Integer
            Dim part1, ignoremove, part2 As Boolean

            If IsEmpty(neigh1) = True Or IsEmpty(neigh2) = True Then ignoremove = True

            If ignoremove = False Then
                Dim oCardInfo As New LogicLayer.Cards.PlayingCardInfo

                Dim oNeighbor1Card As Entities.BusinessObjects.PlayingCard = oCardInfo.GetPlayingCard(Board(neigh1), Cards, CardNameList)
                Dim oNeighbor2Card As Entities.BusinessObjects.PlayingCard = oCardInfo.GetPlayingCard(Board(neigh2), Cards, CardNameList)

                If Math.Abs(oCard.DirectionValue(psdir1) + EValue(iSquareID) - oNeighbor1Card.DirectionValue(neigh1dir) + EValue(neigh1)) = _
                    Math.Abs(oCard.DirectionValue(psdir2) + EValue(iSquareID) - oNeighbor2Card.DirectionValue(neigh2dir) + EValue(neigh2)) Then

                    Call SendTTCardData(IIf(ThisPlrId = 1, "blue", "red"), iSquareID, oCard)
                    Call SendTTCardData(IIf(ThisPlrId = 1, "blue", "red"), neigh1, oNeighbor1Card)
                    Call SendTTCardFlip(iSquareID, neigh1)

                    Call SendTTCardData(IIf(ThisPlrId = 1, "blue", "red"), neigh2, oNeighbor2Card)
                    Call SendTTCardFlip(iSquareID, neigh2)

                    If CardColor(neigh1) = IIf(ThisPlrId = 1, "red", "blue") Then
                        pointchange += 1
                        part1 = True
                    End If

                    If CardColor(neigh2) = IIf(ThisPlrId = 1, "red", "blue") Then
                        pointchange += 1
                        part2 = True
                    End If

                    CardColor(iSquareID) = IIf(ThisPlrId = 1, "blue", "red")
                    CardColor(neigh1) = IIf(ThisPlrId = 1, "blue", "red")
                    CardColor(neigh2) = IIf(ThisPlrId = 1, "blue", "red")

                    HistoryInsert(iSquareID, oCard.CardName, String.Concat("Minus >> ", iSquareID, ", ", neigh1, ", ", neigh2))
                    Call MinusAlert(pointchange)
                    Call SendPoints(pointchange, ThisPlrId)

                    If Combo = True Then
                        If part1 = True Then Call analyze_combo(neigh1, oNeighbor1Card, ThisPlrId)
                        If part2 = True Then Call analyze_combo(neigh2, oNeighbor2Card, ThisPlrId)
                    End If
                End If
            End If
        Catch ex As Exception
            Call errorsub(ex, "analyze_checkminus")
        End Try
    End Sub

    Private Sub analyze_checkmultiply(ByVal oCard As Entities.BusinessObjects.PlayingCard, ByVal iSquareID As Integer, ByVal psdir1 As String, ByVal psdir2 As String, ByVal neigh1 As Integer, ByVal neigh1dir As String, ByVal neigh2 As Integer, ByVal neigh2dir As String, ByVal ThisPlrId As Integer)
        Try

            Dim pointchange As Integer
            Dim part1, ignoremove, part2 As Boolean

            If IsEmpty(neigh1) = True Or IsEmpty(neigh2) = True Then ignoremove = True

            If ignoremove = False Then
                Dim oCardInfo As New LogicLayer.Cards.PlayingCardInfo
                Dim oNeighbor1Card As Entities.BusinessObjects.PlayingCard = oCardInfo.GetPlayingCard(Board(neigh1), Cards, CardNameList)
                Dim oNeighbor2Card As Entities.BusinessObjects.PlayingCard = oCardInfo.GetPlayingCard(Board(neigh2), Cards, CardNameList)

                If Math.Abs((oCard.DirectionValue(psdir1) + EValue(iSquareID)) * (oNeighbor1Card.DirectionValue(neigh1dir) + EValue(neigh1))) = _
                    Math.Abs((oCard.DirectionValue(psdir2) + EValue(iSquareID)) * (oNeighbor2Card.DirectionValue(neigh2dir) + EValue(neigh2))) Then

                    Call SendTTCardData(IIf(ThisPlrId = 1, "blue", "red"), iSquareID, oCard)
                    Call SendTTCardData(IIf(ThisPlrId = 1, "blue", "red"), neigh1, oNeighbor1Card)
                    Call SendTTCardFlip(iSquareID, neigh1)

                    Call SendTTCardData(IIf(ThisPlrId = 1, "blue", "red"), neigh2, oNeighbor2Card)
                    Call SendTTCardFlip(iSquareID, neigh2)

                    If CardColor(neigh1) = IIf(ThisPlrId = 1, "red", "blue") Then
                        pointchange += 1
                        part1 = True
                    End If

                    If CardColor(neigh2) = IIf(ThisPlrId = 1, "red", "blue") Then
                        pointchange += 1
                        part2 = True
                    End If

                    CardColor(iSquareID) = IIf(ThisPlrId = 1, "blue", "red")
                    CardColor(neigh1) = IIf(ThisPlrId = 1, "blue", "red")
                    CardColor(neigh2) = IIf(ThisPlrId = 1, "blue", "red")

                    HistoryInsert(iSquareID, oCard.CardName, String.Concat("Multiply >> ", iSquareID, ", ", neigh1, ", ", neigh2))
                    Call MultiplyAlert(pointchange)
                    Call SendPoints(pointchange, ThisPlrId)

                    If Combo = True Then
                        If part1 = True Then Call analyze_combo(neigh1, oNeighbor1Card, ThisPlrId)
                        If part2 = True Then Call analyze_combo(neigh2, oNeighbor2Card, ThisPlrId)
                    End If
                End If
            End If
        Catch ex As Exception
            Call errorsub(ex, "analyze_checkmultiply")
        End Try
    End Sub

    Private Sub analyze_plus(ByVal iSquareID As Integer, ByVal oCard As Entities.BusinessObjects.PlayingCard, ByVal ThisPlrId As Integer)
        Select Case iSquareID

            Case 0
                ' Checks 0 and 1 square and 0 and 3 square
                Call analyze_checkplus(oCard, iSquareID, "Right", "Down", 1, "Left", 3, "Up", ThisPlrId)
            Case 1
                'Check Left(1), Right(0) and Down(1), Up(4)
                Call analyze_checkplus(oCard, iSquareID, "Left", "Down", 0, "Right", 4, "Up", ThisPlrId)
                'Check Left(1), Right(0) and Right(1), Left(2)
                Call analyze_checkplus(oCard, iSquareID, "Left", "Right", 0, "Right", 2, "Left", ThisPlrId)
                'Check Right(1), Left(2) and Down(1), Up(4)
                Call analyze_checkplus(oCard, iSquareID, "Right", "Down", 2, "Left", 4, "Up", ThisPlrId)
            Case 2
                'Check Left(2), Right(1) and Down(2), Up(5)
                Call analyze_checkplus(oCard, iSquareID, "Left", "Down", 1, "Right", 5, "Up", ThisPlrId)
            Case 3
                'Check Up(3), Down(0) and Right(3), Left(4)
                Call analyze_checkplus(oCard, iSquareID, "Up", "Right", 0, "Down", 4, "Left", ThisPlrId)
                'Check Up(3), Down(0) and Down(3), Up(6)
                Call analyze_checkplus(oCard, iSquareID, "Up", "Down", 0, "Down", 6, "Up", ThisPlrId)
                'Check Right(3), Left(4) and Down(3), Up(6)
                Call analyze_checkplus(oCard, iSquareID, "Right", "Down", 4, "Left", 6, "Up", ThisPlrId)
            Case 4
                'Check Up(4), Down(1) and Left(4), Right(3)
                Call analyze_checkplus(oCard, iSquareID, "Up", "Left", 1, "Down", 3, "Right", ThisPlrId)
                'Check Up(4), Down(1) and Down(4), Up(7)
                Call analyze_checkplus(oCard, iSquareID, "Up", "Down", 1, "Down", 7, "Up", ThisPlrId)
                'Check Up(4), Down(1) and Right(4), Left(5)
                Call analyze_checkplus(oCard, iSquareID, "Up", "Right", 1, "Down", 5, "Left", ThisPlrId)
                'Check Left(4), Right(3) and Down(4), Up(7)
                Call analyze_checkplus(oCard, iSquareID, "Left", "Down", 3, "Right", 7, "Up", ThisPlrId)
                'Check Left(4), Right(3) and Right(4), Left(5)
                Call analyze_checkplus(oCard, iSquareID, "Left", "Right", 3, "Right", 5, "Left", ThisPlrId)
                'Check Down(4), Up(7) and Right(4), Left(5)
                Call analyze_checkplus(oCard, iSquareID, "Down", "Right", 7, "Up", 5, "Left", ThisPlrId)
            Case 5
                'Check Up(5), Down(2) and Left(5), Right(4)
                Call analyze_checkplus(oCard, iSquareID, "Up", "Left", 2, "Down", 4, "Right", ThisPlrId)
                'Check Up(5), Down(2) and Down(5), Up(8)
                Call analyze_checkplus(oCard, iSquareID, "Up", "Down", 2, "Down", 8, "Up", ThisPlrId)
                'Check Left(5), Right(4) and Down(5), Up(8)
                Call analyze_checkplus(oCard, iSquareID, "Left", "Down", 4, "Right", 8, "Up", ThisPlrId)
            Case 6
                'Check Up(6), Down(3) and Right(6), Left(7)
                Call analyze_checkplus(oCard, iSquareID, "Up", "Right", 3, "Down", 7, "Left", ThisPlrId)
            Case 7
                'Check Up(7), Down(4) and Left(7), Right(6)
                Call analyze_checkplus(oCard, iSquareID, "Up", "Left", 4, "Down", 6, "Right", ThisPlrId)
                'Check Up(7), Down(4) and Right(7), Left(8)
                Call analyze_checkplus(oCard, iSquareID, "Up", "Right", 4, "Down", 8, "Left", ThisPlrId)
                'Check Left(7), Right(6) and Right(7), Left(8)
                Call analyze_checkplus(oCard, iSquareID, "Left", "Right", 6, "Right", 8, "Left", ThisPlrId)
            Case 8
                'Check Up(8), Down(5) and Left(8), Right(7)
                Call analyze_checkplus(oCard, iSquareID, "Up", "Left", 5, "Down", 7, "Right", ThisPlrId)
        End Select
    End Sub

    Private Sub analyze_checkplus(ByVal oCard As Entities.BusinessObjects.PlayingCard, ByVal iSquareID As Integer, ByVal psdir1 As String, ByVal psdir2 As String, ByVal neigh1 As Integer, ByVal neigh1dir As String, ByVal neigh2 As Integer, ByVal neigh2dir As String, ByVal ThisPlrId As Integer)
        Try
            Dim pointchange As Integer
            Dim part1, ignoremove, part2 As Boolean

            If IsEmpty(neigh1) = True Or IsEmpty(neigh2) = True Then ignoremove = True

            If ignoremove = False Then
                Dim oCardInfo As New LogicLayer.Cards.PlayingCardInfo

                Dim oNeighbor1Card As Entities.BusinessObjects.PlayingCard = oCardInfo.GetPlayingCard(Board(neigh1), Cards, CardNameList)
                Dim oNeighbor2Card As Entities.BusinessObjects.PlayingCard = oCardInfo.GetPlayingCard(Board(neigh2), Cards, CardNameList)

                If (oCard.DirectionValue(psdir1) + EValue(iSquareID) + oNeighbor1Card.DirectionValue(neigh1dir) + EValue(neigh1)) = _
                    (oCard.DirectionValue(psdir2) + EValue(iSquareID) + oNeighbor2Card.DirectionValue(neigh2dir) + EValue(neigh2)) Then

                    Call SendTTCardData(IIf(ThisPlrId = 1, "blue", "red"), iSquareID, oCard)
                    Call SendTTCardData(IIf(ThisPlrId = 1, "blue", "red"), neigh1, oNeighbor1Card)
                    Call SendTTCardFlip(iSquareID, neigh1)

                    Call SendTTCardData(IIf(ThisPlrId = 1, "blue", "red"), neigh2, oNeighbor2Card)
                    Call SendTTCardFlip(iSquareID, neigh2)

                    If CardColor(neigh1) = IIf(ThisPlrId = 1, "red", "blue") Then
                        pointchange += 1
                        part1 = True
                    End If

                    If CardColor(neigh2) = IIf(ThisPlrId = 1, "red", "blue") Then
                        pointchange += 1
                        part2 = True
                    End If

                    CardColor(iSquareID) = IIf(ThisPlrId = 1, "blue", "red")
                    CardColor(neigh1) = IIf(ThisPlrId = 1, "blue", "red")
                    CardColor(neigh2) = IIf(ThisPlrId = 1, "blue", "red")

                    HistoryInsert(iSquareID, oCard.CardName, String.Concat("Plus >> ", iSquareID, ", ", neigh1, ", ", neigh2))
                    Call PlusAlert(pointchange)
                    Call SendPoints(pointchange, ThisPlrId)

                    If Combo = True Then
                        If part1 = True Then Call analyze_combo(neigh1, oNeighbor1Card, ThisPlrId)
                        If part2 = True Then Call analyze_combo(neigh2, oNeighbor2Card, ThisPlrId)
                    End If
                End If
            End If
        Catch ex As Exception
            Call errorsub(ex, "analyze_checkplus")
        End Try
    End Sub

    Private Sub ComboAlert(ByVal iPoints As Integer)
        Try
            Dim oTables As New BusinessLayer.Tables
            Dim iTableID As Integer = oTables.FindPlayersTable(Tables, Player1)

            If (iPoints > 0) Then
                Send(String.Empty, "TT_COMBO", GetSocket(Tables(itableid).player1))
                Send(String.Empty, "TT_COMBO", GetSocket(Tables(itableid).player2))
            End If
        Catch ex As Exception
            Call errorsub(ex, "ComboAlert")
        End Try
    End Sub

    Private Sub MultiplyAlert(ByVal iPoints As Integer)
        Try
            Dim oTables As New BusinessLayer.Tables
            Dim iTableID As Integer = oTables.FindPlayersTable(Tables, Player1)

            If (iPoints > 0) Then
                Send(String.Empty, "TT_MULTIPLY", GetSocket(Tables(itableid).player1))
                Send(String.Empty, "TT_MULTIPLY", GetSocket(Tables(itableid).player2))
            End If
        Catch ex As Exception
            Call errorsub(ex, "MultiplyAlert")
        End Try
    End Sub

    Private Sub MinusAlert(ByVal iPoints As Integer)
        Try
            Dim oTables As New BusinessLayer.Tables
            Dim iTableID As Integer = oTables.FindPlayersTable(Tables, Player1)

            If (iPoints > 0) Then
                Send(String.Empty, "TT_MINUS", GetSocket(Tables(itableid).player1))
                Send(String.Empty, "TT_MINUS", GetSocket(Tables(itableid).player2))
            End If
        Catch ex As Exception
            Call errorsub(ex, "MinusAlert")
        End Try
    End Sub

    Private Sub SameAlert(ByVal iPoints As Integer)
        Try
            Dim oTables As New BusinessLayer.Tables
            Dim iTableID As Integer = oTables.FindPlayersTable(Tables, Player1)

            If (iPoints > 0) Then
                Send(String.Empty, "TT_SAME", GetSocket(Tables(itableid).player1))
                Send(String.Empty, "TT_SAME", GetSocket(Tables(itableid).player2))
            End If
        Catch ex As Exception
            Call errorsub(ex, "SameAlert")
        End Try
    End Sub

    Private Sub PlusAlert(ByVal iPoints As Integer)
        Try
            Dim oTables As New BusinessLayer.Tables
            Dim iTableID As Integer = oTables.FindPlayersTable(Tables, Player1)

            If (iPoints > 0) Then
                Send(String.Empty, "TT_PLUS", GetSocket(Tables(itableid).player1))
                Send(String.Empty, "TT_PLUS", GetSocket(Tables(itableid).player2))
            End If
        Catch ex As Exception
            Call errorsub(ex, "PlusAlert")
        End Try
    End Sub

    Private Sub SendTTCardData(ByVal sColor As String, ByVal x As Integer, ByVal oCard As Entities.BusinessObjects.PlayingCard)
        Dim oTables As New BusinessLayer.Tables
        Dim iTableID As Integer = oTables.FindPlayersTable(Tables, Player1)

        Send(String.Empty, String.Concat("TT_", sColor.ToUpper, "CARD ", x, " ", oCard.CardName.Replace(" ", "%20")), GetSocket(Tables(itableid).player1))
        Send(String.Empty, String.Concat("TT_", sColor.ToUpper, "CARD ", x, " ", oCard.CardName.Replace(" ", "%20")), GetSocket(Tables(itableid).player2))
    End Sub

    Private Sub SendTTCardFlip(ByVal iCompareID As Integer, ByVal iSquareID As Integer)
        Dim iStatus As Integer = 1

        Dim oTables As New BusinessLayer.Tables
        Dim iTableID As Integer = oTables.FindPlayersTable(Tables, Player1)

        Select Case iSquareID
            Case 0
                If iCompareID = 3 Or iCompareID = 6 Then iStatus = 2
            Case 1
                If iCompareID = 4 Or iCompareID = 7 Then iStatus = 2
            Case 2
                If iCompareID = 5 Or iCompareID = 8 Then iStatus = 2
            Case 3
                If iCompareID = 0 Or iCompareID = 6 Then iStatus = 2
            Case 4
                If iCompareID = 1 Or iCompareID = 7 Then iStatus = 2
            Case 5
                If iCompareID = 2 Or iCompareID = 8 Then iStatus = 2
            Case 6
                If iCompareID = 3 Or iCompareID = 0 Then iStatus = 2
            Case 7
                If iCompareID = 4 Or iCompareID = 1 Then iStatus = 2
            Case 8
                If iCompareID = 5 Or iCompareID = 2 Then iStatus = 2
        End Select

        Dim wColor As String = CardColor(iSquareID)

        Send(String.Empty, String.Concat("TT_CARDFLIP ", iSquareID, " ", iStatus, " ", wColor), GetSocket(Tables(itableid).player1))
        Send(String.Empty, String.Concat("TT_CARDFLIP ", iSquareID, " ", iStatus, " " & wColor), GetSocket(Tables(itableid).player2))
    End Sub

    Private Sub SendPoints(ByVal iPoints As Integer, ByVal ThisPlrId As Integer)
        Try
            Dim oTables As New BusinessLayer.Tables
            Dim iTableID As Integer = oTables.FindPlayersTable(Tables, Player1)

            If ThisPlrId = 1 Then
                Player1_Score += iPoints
                Player2_Score -= iPoints
            Else
                Player1_Score -= iPoints
                Player2_Score += iPoints
            End If

            Send(String.Empty, String.Concat("TT_POINTS ", Player1_Score, " ", Player2_Score), GetSocket(Tables(itableid).player1))
            Send(String.Empty, String.Concat("TT_POINTS ", Player1_Score, " ", Player2_Score), GetSocket(Tables(itableid).player2))
        Catch ex As Exception
            Call errorsub(ex, "sendpoints")
        End Try
    End Sub

#Region "Wall"
    Private Sub analyze_wallcombos(ByVal sType As String, ByVal iSquareID As Integer, ByVal oCard As Entities.BusinessObjects.PlayingCard, ByVal ThisPlrId As Integer)
        Try
            Select Case iSquareID
                Case 0
                    'Checking Right(0), Left(1) and Left(0) + Left Wall
                    Call analyze_checkwall(sType, oCard, 0, "Right", 1, "Left", "Left", ThisPlrId)
                    'Checking Right(0), Left(1) and Up(0) + Top Wall
                    Call analyze_checkwall(sType, oCard, 0, "Right", 1, "Left", "Up", ThisPlrId)
                    'Checking Down(0), Up(3) and Left(0) + Left Wall
                    Call analyze_checkwall(sType, oCard, 0, "Down", 3, "Up", "Left", ThisPlrId)
                    'Checking Down(0), Up(3) and Up(0) + Top Wall
                    Call analyze_checkwall(sType, oCard, 0, "Down", 3, "Up", "Up", ThisPlrId)
                Case 1
                    'Checking Left(1), Right(0) and Up(1) + Top Wall
                    Call analyze_checkwall(sType, oCard, 1, "Left", 0, "Right", "Up", ThisPlrId)
                    'Checking Down(1), Up(4) and Up(0) + Top Wall
                    Call analyze_checkwall(sType, oCard, 1, "Down", 4, "Up", "Up", ThisPlrId)
                    'Checking Right(1), Left(2) and Up(0) + Top Wall
                    Call analyze_checkwall(sType, oCard, 1, "Right", 2, "Left", "Up", ThisPlrId)
                Case 2
                    'Checking Left(2), Right(1) and Up(0) + Top Wall
                    Call analyze_checkwall(sType, oCard, 2, "Left", 1, "Right", "Up", ThisPlrId)
                    'Checking Down(2), Up(5) and Up(0) + Top Wall
                    Call analyze_checkwall(sType, oCard, 2, "Down", 5, "Up", "Up", ThisPlrId)
                    'Checking Left(2), Right(1) and Right(2) + Right Wall
                    Call analyze_checkwall(sType, oCard, 2, "Left", 1, "Right", "Right", ThisPlrId)
                    'Checking Down(2), Up(5) and Right(2) + Right Wall
                    Call analyze_checkwall(sType, oCard, 2, "Down", 5, "Up", "Right", ThisPlrId)
                Case 3
                    'Checking Up(3), Down(0) and Left(3) + Left Wall
                    Call analyze_checkwall(sType, oCard, 3, "Up", 0, "Down", "Left", ThisPlrId)
                    'Checking Right(3), Left(4) and Left(3) + Left Wall
                    Call analyze_checkwall(sType, oCard, 3, "Right", 4, "Left", "Left", ThisPlrId)
                    'Checking Down(3), Up(6) and Left(3) + Left Wall
                    Call analyze_checkwall(sType, oCard, 3, "Down", 6, "Up", "Left", ThisPlrId)
                Case 5
                    'Checking Up(5), Down(2) and Right(3) + Right Wall
                    Call analyze_checkwall(sType, oCard, 5, "Up", 2, "Down", "Right", ThisPlrId)
                    'Checking Left(5), Right(4) and Right(3) + Right Wall
                    Call analyze_checkwall(sType, oCard, 5, "Left", 4, "Right", "Right", ThisPlrId)
                    'Checking Down(5), Up(8) and Right(3) + Right Wall
                    Call analyze_checkwall(sType, oCard, 5, "Down", 8, "Up", "Right", ThisPlrId)
                Case 6
                    'Checking Up(6), Down(3) and Left(6) + Left Wall
                    Call analyze_checkwall(sType, oCard, 6, "Up", 3, "Down", "Left", ThisPlrId)
                    'Checking Right(6), Left(7) and Left(6) + Left Wall
                    Call analyze_checkwall(sType, oCard, 6, "Right", 7, "Left", "Left", ThisPlrId)
                    'Checking Up(6), Down(3) and Down(6) + Bottom Wall
                    Call analyze_checkwall(sType, oCard, 6, "Up", 3, "Down", "Down", ThisPlrId)
                    'Checking Right(6), Left(7) and Down(6) + Bottom Wall
                    Call analyze_checkwall(sType, oCard, 6, "Right", 7, "Left", "Down", ThisPlrId)
                Case 7
                    'Checking Up(7), Down(4) and Down(7) + Bottom Wall
                    Call analyze_checkwall(sType, oCard, 7, "Up", 4, "Down", "Down", ThisPlrId)
                    'Checking Left(7), Right(6) and Down(7) + Bottom Wall
                    Call analyze_checkwall(sType, oCard, 7, "Left", 6, "Right", "Down", ThisPlrId)
                    'Checking Right(7), Left(8) and Down(7) + Bottom Wall
                    Call analyze_checkwall(sType, oCard, 7, "Right", 8, "Left", "Down", ThisPlrId)
                Case 8
                    'Checking Up(8), Down(5) and Right(8) + Right Wall
                    Call analyze_checkwall(sType, oCard, 8, "Up", 5, "Down", "Right", ThisPlrId)
                    'Checking Left(8), Right(7) and Right(8) + Right Wall
                    Call analyze_checkwall(sType, oCard, 8, "Left", 7, "Right", "Right", ThisPlrId)
                    'Checking Up(8), Down(5) and Down(8) + Bottom Wall
                    Call analyze_checkwall(sType, oCard, 8, "Up", 5, "Down", "Down", ThisPlrId)
                    'Checking Left(8), Right(7) and Down(8) + Bottom Wall
                    Call analyze_checkwall(sType, oCard, 8, "Left", 7, "Right", "Down", ThisPlrId)
            End Select
        Catch ex As Exception
            Call errorsub(ex, "analyze_minuswall")
        End Try
    End Sub

    Private Sub analyze_checkwall(ByVal sType As String, ByVal oCard As Entities.BusinessObjects.PlayingCard, ByVal iSquareID As Integer, ByVal psdir As String, ByVal neighbor As String, ByVal neighdir As String, ByVal backwalldir As String, ByVal ThisPlrId As Integer)
        Try
            Dim pointchange As Integer
            Dim ignoremove, part1 As Boolean
            Dim bOK As Boolean ' It's okay!

            If IsEmpty(neighbor) = True Then ignoremove = True
            'If (cardvalue(dCardName, psdir) + Val(Readini("EValue", dCardName, My.Application.Info.DirectoryPath & "\gamefiles\" & GameID & ".tgf"))) = 0 Then ignoremove = True

            If ignoremove = False Then
                Dim oCardInfo As New LogicLayer.Cards.PlayingCardInfo
                Dim oNeighbor1Card As Entities.BusinessObjects.PlayingCard = oCardInfo.GetPlayingCard(Board(neighbor), Cards, CardNameList)

                Select Case sType
                    Case "same"
                        If (oCard.DirectionValue(psdir) + EValue(iSquareID)) = (oNeighbor1Card.DirectionValue(neighdir) + EValue(neighbor)) _
                            And (oCard.DirectionValue(backwalldir) + EValue(iSquareID) = 10) Then
                            'Then it passes
                            bOK = True
                        Else
                            bOK = False
                        End If
                    Case "plus"
                        If System.Math.Abs(oCard.DirectionValue(psdir) + EValue(iSquareID) + oNeighbor1Card.DirectionValue(neighdir) + EValue(neighbor)) = _
                            System.Math.Abs(oCard.DirectionValue(backwalldir) + EValue(iSquareID) + 10) Then
                            'Then it passes
                            bOK = True
                        Else
                            bOK = False
                        End If
                    Case "minus"
                        If System.Math.Abs(oCard.DirectionValue(psdir) + EValue(iSquareID) - oNeighbor1Card.DirectionValue(neighdir) + EValue(neighbor)) = _
                            System.Math.Abs(oCard.DirectionValue(backwalldir) + EValue(iSquareID) - 10) Then
                            'Then it passes
                            bOK = True
                        Else
                            bOK = False
                        End If
                End Select

                If bOK = True Then
                    Call SendTTCardData(IIf(ThisPlrId = 1, "blue", "red"), iSquareID, oCard)
                    Call SendTTCardData(IIf(ThisPlrId = 1, "blue", "red"), neighbor, oNeighbor1Card)
                    Call SendTTCardFlip(iSquareID, neighbor)

                    If CardColor(neighbor) = IIf(ThisPlrId = 1, "red", "blue") Then
                        pointchange += 1
                        part1 = True
                    End If

                    CardColor(iSquareID) = IIf(ThisPlrId = 1, "blue", "red")
                    CardColor(neighbor) = IIf(ThisPlrId = 1, "blue", "red")

                    Select Case sType
                        Case "same"
                            Call SameAlert(pointchange)
                        Case "plus"
                            Call PlusAlert(pointchange)
                        Case "minus"
                            Call MinusAlert(pointchange)
                    End Select

                    HistoryInsert(iSquareID, oCard.CardName, String.Concat("Wall (", sType, ") >> ", iSquareID, ", ", neighbor))
                    Call SendPoints(pointchange, ThisPlrId)

                    If Combo = True Then If part1 = True Then Call analyze_combo(neighbor, oNeighbor1Card, ThisPlrId)
                End If
            End If
        Catch ex As Exception
            Call errorsub(ex, "analyze_checkwall")
        End Try
    End Sub
#End Region

#Region "Set Wall"
    Private Sub analyze_setwallcombos(ByVal sType As String, ByVal iSquareID As Integer, ByVal oCard As Entities.BusinessObjects.PlayingCard, ByVal ThisPlrId As Integer)
        Try
            Select Case iSquareID
                Case 0
                    'Checking Right(0), Left(1) and Left(0) + Left Wall
                    Call analyze_checksetwall(sType, oCard, 0, "Right", 1, "Left", "Left", ThisPlrId)
                    'Checking Right(0), Left(1) and Up(0) + Top Wall
                    Call analyze_checksetwall(sType, oCard, 0, "Right", 1, "Left", "Up", ThisPlrId)
                    'Checking Down(0), Up(3) and Left(0) + Left Wall
                    Call analyze_checksetwall(sType, oCard, 0, "Down", 3, "Up", "Left", ThisPlrId)
                    'Checking Down(0), Up(3) and Up(0) + Top Wall
                    Call analyze_checksetwall(sType, oCard, 0, "Down", 3, "Up", "Up", ThisPlrId)
                Case 1
                    'Checking Left(1), Right(0) and Up(1) + Top Wall
                    Call analyze_checksetwall(sType, oCard, 1, "Left", 0, "Right", "Up", ThisPlrId)
                    'Checking Down(1), Up(4) and Up(0) + Top Wall
                    Call analyze_checksetwall(sType, oCard, 1, "Down", 4, "Up", "Up", ThisPlrId)
                    'Checking Right(1), Left(2) and Up(0) + Top Wall
                    Call analyze_checksetwall(sType, oCard, 1, "Right", 2, "Left", "Up", ThisPlrId)
                Case 2
                    'Checking Left(2), Right(1) and Up(0) + Top Wall
                    Call analyze_checksetwall(sType, oCard, 2, "Left", 1, "Right", "Up", ThisPlrId)
                    'Checking Down(2), Up(5) and Up(0) + Top Wall
                    Call analyze_checksetwall(sType, oCard, 2, "Down", 5, "Up", "Up", ThisPlrId)
                    'Checking Left(2), Right(1) and Right(2) + Right Wall
                    Call analyze_checksetwall(sType, oCard, 2, "Left", 1, "Right", "Right", ThisPlrId)
                    'Checking Down(2), Up(5) and Right(2) + Right Wall
                    Call analyze_checksetwall(sType, oCard, 2, "Down", 5, "Up", "Right", ThisPlrId)
                Case 3
                    'Checking Up(3), Down(0) and Left(3) + Left Wall
                    Call analyze_checksetwall(sType, oCard, 3, "Up", 0, "Down", "Left", ThisPlrId)
                    'Checking Right(3), Left(4) and Left(3) + Left Wall
                    Call analyze_checksetwall(sType, oCard, 3, "Right", 4, "Left", "Left", ThisPlrId)
                    'Checking Down(3), Up(6) and Left(3) + Left Wall
                    Call analyze_checksetwall(sType, oCard, 3, "Down", 6, "Up", "Left", ThisPlrId)
                Case 5
                    'Checking Up(5), Down(2) and Right(3) + Right Wall
                    Call analyze_checksetwall(sType, oCard, 5, "Up", 2, "Down", "Right", ThisPlrId)
                    'Checking Left(5), Right(4) and Right(3) + Right Wall
                    Call analyze_checksetwall(sType, oCard, 5, "Left", 4, "Right", "Right", ThisPlrId)
                    'Checking Down(5), Up(8) and Right(3) + Right Wall
                    Call analyze_checksetwall(sType, oCard, 5, "Down", 8, "Up", "Right", ThisPlrId)
                Case 6
                    'Checking Up(6), Down(3) and Left(6) + Left Wall
                    Call analyze_checksetwall(sType, oCard, 6, "Up", 3, "Down", "Left", ThisPlrId)
                    'Checking Right(6), Left(7) and Left(6) + Left Wall
                    Call analyze_checksetwall(sType, oCard, 6, "Right", 7, "Left", "Left", ThisPlrId)
                    'Checking Up(6), Down(3) and Down(6) + Bottom Wall
                    Call analyze_checksetwall(sType, oCard, 6, "Up", 3, "Down", "Down", ThisPlrId)
                    'Checking Right(6), Left(7) and Down(6) + Bottom Wall
                    Call analyze_checksetwall(sType, oCard, 6, "Right", 7, "Left", "Down", ThisPlrId)
                Case 7
                    'Checking Up(7), Down(4) and Down(7) + Bottom Wall
                    Call analyze_checksetwall(sType, oCard, 7, "Up", 4, "Down", "Down", ThisPlrId)
                    'Checking Left(7), Right(6) and Down(7) + Bottom Wall
                    Call analyze_checksetwall(sType, oCard, 7, "Left", 6, "Right", "Down", ThisPlrId)
                    'Checking Right(7), Left(8) and Down(7) + Bottom Wall
                    Call analyze_checksetwall(sType, oCard, 7, "Right", 8, "Left", "Down", ThisPlrId)
                Case 8
                    'Checking Up(8), Down(5) and Right(8) + Right Wall
                    Call analyze_checksetwall(sType, oCard, 8, "Up", 5, "Down", "Right", ThisPlrId)
                    'Checking Left(8), Right(7) and Right(8) + Right Wall
                    Call analyze_checksetwall(sType, oCard, 8, "Left", 7, "Right", "Right", ThisPlrId)
                    'Checking Up(8), Down(5) and Down(8) + Bottom Wall
                    Call analyze_checksetwall(sType, oCard, 8, "Up", 5, "Down", "Down", ThisPlrId)
                    'Checking Left(8), Right(7) and Down(8) + Bottom Wall
                    Call analyze_checksetwall(sType, oCard, 8, "Left", 7, "Right", "Down", ThisPlrId)
            End Select
        Catch ex As Exception
            Call errorsub(ex, "analyze_setwallcombos")
        End Try
    End Sub

    Private Sub analyze_checksetwall(ByVal sType As String, ByVal oCard As Entities.BusinessObjects.PlayingCard, ByVal iSquareID As Integer, ByVal psdir As String, ByVal neighbor As String, ByVal neighdir As String, ByVal backwalldir As String, ByVal ThisPlrId As Integer)
        Try
            Dim pointchange As Integer
            Dim ignoremove, part1 As Boolean
            Dim bOK As Boolean ' It's okay!

            If IsEmpty(neighbor) = True Then ignoremove = True
            'If (cardvalue(dCardName, psdir) + Val(Readini("EValue", dCardName, My.Application.Info.DirectoryPath & "\gamefiles\" & GameID & ".tgf"))) = 0 Then ignoremove = True

            If ignoremove = False Then
                Dim oCardInfo As New LogicLayer.Cards.PlayingCardInfo
                Dim oNeighbor1Card As Entities.BusinessObjects.PlayingCard = oCardInfo.GetPlayingCard(Board(neighbor), Cards, CardNameList)

                Select Case sType
                    Case "same"
                        If (oCard.DirectionValue(psdir) + EValue(iSquareID)) = (oNeighbor1Card.DirectionValue(neighdir) + EValue(neighbor)) _
                                And (oCard.DirectionValue(backwalldir) + EValue(iSquareID) = SetWallValue) Then
                            'Then it passes
                            bOK = True
                        Else
                            bOK = False
                        End If
                    Case "plus"
                        If System.Math.Abs(oCard.DirectionValue(psdir) + EValue(iSquareID) + oNeighbor1Card.DirectionValue(neighdir) + EValue(neighbor)) = _
                            System.Math.Abs(oCard.DirectionValue(backwalldir) + EValue(iSquareID) + SetWallValue) Then
                            'Then it passes
                            bOK = True
                        Else
                            bOK = False
                        End If
                    Case "minus"
                        If System.Math.Abs(oCard.DirectionValue(psdir) + EValue(iSquareID) - oNeighbor1Card.DirectionValue(neighdir) + EValue(neighbor)) = _
                            System.Math.Abs(oCard.DirectionValue(backwalldir) + EValue(iSquareID) - SetWallValue) Then
                            'Then it passes
                            bOK = True
                        Else
                            bOK = False
                        End If
                End Select

                If bOK = True Then
                    Call SendTTCardData(IIf(ThisPlrId = 1, "blue", "red"), iSquareID, oCard)
                    Call SendTTCardData(IIf(ThisPlrId = 1, "blue", "red"), neighbor, oNeighbor1Card)
                    Call SendTTCardFlip(iSquareID, neighbor)

                    If CardColor(neighbor) = IIf(ThisPlrId = 1, "red", "blue") Then
                        pointchange += 1
                        part1 = True
                    End If

                    CardColor(iSquareID) = IIf(ThisPlrId = 1, "blue", "red")
                    CardColor(neighbor) = IIf(ThisPlrId = 1, "blue", "red")

                    Select Case sType
                        Case "same"
                            Call SameAlert(pointchange)
                        Case "plus"
                            Call PlusAlert(pointchange)
                        Case "minus"
                            Call MinusAlert(pointchange)
                    End Select

                    HistoryInsert(iSquareID, oCard.CardName, String.Concat("Set Wall (", sType, ") >> ", iSquareID, ", ", neighbor))
                    Call SendPoints(pointchange, ThisPlrId)

                    If Combo = True Then If part1 = True Then Call analyze_combo(neighbor, oNeighbor1Card, ThisPlrId)
                End If
            End If
        Catch ex As Exception
            Call errorsub(ex, "analyze_checksetwall")
        End Try
    End Sub
#End Region

#Region "Mirror"
    Private Sub analyze_mirrorcombos(ByVal sType As String, ByVal iSquareID As Integer, ByVal oCard As Entities.BusinessObjects.PlayingCard, ByVal ThisPlrId As Integer)
        Try
            Select Case iSquareID
                Case 0
                    'Checking Right(0), Left(1) and Left(0) + Left Wall
                    Call analyze_checkmirror(sType, oCard, 0, "Right", 1, "Left", "Left", ThisPlrId)
                    'Checking Right(0), Left(1) and Up(0) + Top Wall
                    Call analyze_checkmirror(sType, oCard, 0, "Right", 1, "Left", "Up", ThisPlrId)
                    'Checking Down(0), Up(3) and Left(0) + Left Wall
                    Call analyze_checkmirror(sType, oCard, 0, "Down", 3, "Up", "Left", ThisPlrId)
                    'Checking Down(0), Up(3) and Up(0) + Top Wall
                    Call analyze_checkmirror(sType, oCard, 0, "Down", 3, "Up", "Up", ThisPlrId)
                Case 1
                    'Checking Left(1), Right(0) and Up(1) + Top Wall
                    Call analyze_checkmirror(sType, oCard, 1, "Left", 0, "Right", "Up", ThisPlrId)
                    'Checking Down(1), Up(4) and Up(0) + Top Wall
                    Call analyze_checkmirror(sType, oCard, 1, "Down", 4, "Up", "Up", ThisPlrId)
                    'Checking Right(1), Left(2) and Up(0) + Top Wall
                    Call analyze_checkmirror(sType, oCard, 1, "Right", 2, "Left", "Up", ThisPlrId)
                Case 2
                    'Checking Left(2), Right(1) and Up(0) + Top Wall
                    Call analyze_checkmirror(sType, oCard, 2, "Left", 1, "Right", "Up", ThisPlrId)
                    'Checking Down(2), Up(5) and Up(0) + Top Wall
                    Call analyze_checkmirror(sType, oCard, 2, "Down", 5, "Up", "Up", ThisPlrId)
                    'Checking Left(2), Right(1) and Right(2) + Right Wall
                    Call analyze_checkmirror(sType, oCard, 2, "Left", 1, "Right", "Right", ThisPlrId)
                    'Checking Down(2), Up(5) and Right(2) + Right Wall
                    Call analyze_checkmirror(sType, oCard, 2, "Down", 5, "Up", "Right", ThisPlrId)
                Case 3
                    'Checking Up(3), Down(0) and Left(3) + Left Wall
                    Call analyze_checkmirror(sType, oCard, 3, "Up", 0, "Down", "Left", ThisPlrId)
                    'Checking Right(3), Left(4) and Left(3) + Left Wall
                    Call analyze_checkmirror(sType, oCard, 3, "Right", 4, "Left", "Left", ThisPlrId)
                    'Checking Down(3), Up(6) and Left(3) + Left Wall
                    Call analyze_checkmirror(sType, oCard, 3, "Down", 6, "Up", "Left", ThisPlrId)
                Case 5
                    'Checking Up(5), Down(2) and Right(3) + Right Wall
                    Call analyze_checkmirror(sType, oCard, 5, "Up", 2, "Down", "Right", ThisPlrId)
                    'Checking Left(5), Right(4) and Right(3) + Right Wall
                    Call analyze_checkmirror(sType, oCard, 5, "Left", 4, "Right", "Right", ThisPlrId)
                    'Checking Down(5), Up(8) and Right(3) + Right Wall
                    Call analyze_checkmirror(sType, oCard, 5, "Down", 8, "Up", "Right", ThisPlrId)
                Case 6
                    'Checking Up(6), Down(3) and Left(6) + Left Wall
                    Call analyze_checkmirror(sType, oCard, 6, "Up", 3, "Down", "Left", ThisPlrId)
                    'Checking Right(6), Left(7) and Left(6) + Left Wall
                    Call analyze_checkmirror(sType, oCard, 6, "Right", 7, "Left", "Left", ThisPlrId)
                    'Checking Up(6), Down(3) and Down(6) + Bottom Wall
                    Call analyze_checkmirror(sType, oCard, 6, "Up", 3, "Down", "Down", ThisPlrId)
                    'Checking Right(6), Left(7) and Down(6) + Bottom Wall
                    Call analyze_checkmirror(sType, oCard, 6, "Right", 7, "Left", "Down", ThisPlrId)
                Case 7
                    'Checking Up(7), Down(4) and Down(7) + Bottom Wall
                    Call analyze_checkmirror(sType, oCard, 7, "Up", 4, "Down", "Down", ThisPlrId)
                    'Checking Left(7), Right(6) and Down(7) + Bottom Wall
                    Call analyze_checkmirror(sType, oCard, 7, "Left", 6, "Right", "Down", ThisPlrId)
                    'Checking Right(7), Left(8) and Down(7) + Bottom Wall
                    Call analyze_checkmirror(sType, oCard, 7, "Right", 8, "Left", "Down", ThisPlrId)
                Case 8
                    'Checking Up(8), Down(5) and Right(8) + Right Wall
                    Call analyze_checkmirror(sType, oCard, 8, "Up", 5, "Down", "Right", ThisPlrId)
                    'Checking Left(8), Right(7) and Right(8) + Right Wall
                    Call analyze_checkmirror(sType, oCard, 8, "Left", 7, "Right", "Right", ThisPlrId)
                    'Checking Up(8), Down(5) and Down(8) + Bottom Wall
                    Call analyze_checkmirror(sType, oCard, 8, "Up", 5, "Down", "Down", ThisPlrId)
                    'Checking Left(8), Right(7) and Down(8) + Bottom Wall
                    Call analyze_checkmirror(sType, oCard, 8, "Left", 7, "Right", "Down", ThisPlrId)
            End Select
        Catch ex As Exception
            Call errorsub(ex, "analyze_checkmirror")
        End Try
    End Sub

    Private Sub analyze_checkmirror(ByVal sType As String, ByVal oCard As Entities.BusinessObjects.PlayingCard, ByVal iSquareID As Integer, ByVal psdir As String, ByVal neighbor As String, ByVal neighdir As String, ByVal backwalldir As String, ByVal ThisPlrId As Integer)
        Try
            Dim pointchange As Integer
            Dim ignoremove, part1 As Boolean
            Dim bOK As Boolean ' It's okay!

            If IsEmpty(neighbor) = True Then ignoremove = True
            'If (cardvalue(dCardName, psdir) + Val(Readini("EValue", dCardName, My.Application.Info.DirectoryPath & "\gamefiles\" & GameID & ".tgf"))) = 0 Then ignoremove = True

            If ignoremove = False Then
                Dim oCardInfo As New LogicLayer.Cards.PlayingCardInfo
                Dim oNeighbor1Card As Entities.BusinessObjects.PlayingCard = oCardInfo.GetPlayingCard(Board(neighbor), Cards, CardNameList)

                Select Case sType
                    Case "same"
                        If (oCard.DirectionValue(psdir) + EValue(iSquareID)) = (oNeighbor1Card.DirectionValue(neighdir) + EValue(neighbor)) _
                            And (oCard.DirectionValue(backwalldir) + EValue(iSquareID)) = (oCard.DirectionValue(backwalldir) + EValue(iSquareID)) Then
                            'Then it passes
                            bOK = True
                        Else
                            bOK = False
                        End If
                    Case "plus"
                        If (oCard.DirectionValue(psdir) + EValue(iSquareID)) _
                            + (oNeighbor1Card.DirectionValue(neighdir) + EValue(neighbor)) _
                            = ((oCard.DirectionValue(backwalldir) + EValue(iSquareID)) * 2) Then
                            'Then it passes
                            bOK = True
                        Else
                            bOK = False
                        End If
                End Select

                If bOK = True Then
                    Call SendTTCardData(IIf(ThisPlrId = 1, "blue", "red"), iSquareID, oCard)
                    Call SendTTCardData(IIf(ThisPlrId = 1, "blue", "red"), neighbor, oNeighbor1Card)
                    Call SendTTCardFlip(iSquareID, neighbor)

                    If CardColor(neighbor) = IIf(ThisPlrId = 1, "red", "blue") Then
                        pointchange += 1
                        part1 = True
                    End If

                    CardColor(iSquareID) = IIf(ThisPlrId = 1, "blue", "red")
                    CardColor(neighbor) = IIf(ThisPlrId = 1, "blue", "red")

                    Select Case sType
                        Case "same"
                            Call SameAlert(pointchange)
                        Case "plus"
                            Call PlusAlert(pointchange)
                        Case "minus"
                            Call MinusAlert(pointchange)
                    End Select

                    HistoryInsert(iSquareID, oCard.CardName, String.Concat("Mirror (", sType, ") >> ", iSquareID, ", ", neighbor))
                    Call SendPoints(pointchange, ThisPlrId)

                    If Combo = True Then If part1 = True Then Call analyze_combo(neighbor, oNeighbor1Card, ThisPlrId)
                End If
            End If
        Catch ex As Exception
            Call errorsub(ex, "analyze_checkmirror")
        End Try
    End Sub
#End Region

#Region "Neutral"
    Private Sub analyze_neutralcombos(ByVal sType As String, ByVal iSquareID As Integer, ByVal oCard As Entities.BusinessObjects.PlayingCard, ByVal ThisPlrId As Integer)
        Try
            Select Case iSquareID
                Case 0
                    'Checking Right(0), Left(1) and Left(0) + Left Wall
                    Call analyze_checkneutral(sType, oCard, 0, "Right", 1, "Left", "Left", ThisPlrId)
                    'Checking Right(0), Left(1) and Up(0) + Top Wall
                    Call analyze_checkneutral(sType, oCard, 0, "Right", 1, "Left", "Up", ThisPlrId)
                    'Checking Down(0), Up(3) and Left(0) + Left Wall
                    Call analyze_checkneutral(sType, oCard, 0, "Down", 3, "Up", "Left", ThisPlrId)
                    'Checking Down(0), Up(3) and Up(0) + Top Wall
                    Call analyze_checkneutral(sType, oCard, 0, "Down", 3, "Up", "Up", ThisPlrId)
                Case 1
                    'Checking Left(1), Right(0) and Up(1) + Top Wall
                    Call analyze_checkneutral(sType, oCard, 1, "Left", 0, "Right", "Up", ThisPlrId)
                    'Checking Down(1), Up(4) and Up(0) + Top Wall
                    Call analyze_checkneutral(sType, oCard, 1, "Down", 4, "Up", "Up", ThisPlrId)
                    'Checking Right(1), Left(2) and Up(0) + Top Wall
                    Call analyze_checkneutral(sType, oCard, 1, "Right", 2, "Left", "Up", ThisPlrId)
                Case 2
                    'Checking Left(2), Right(1) and Up(0) + Top Wall
                    Call analyze_checkneutral(sType, oCard, 2, "Left", 1, "Right", "Up", ThisPlrId)
                    'Checking Down(2), Up(5) and Up(0) + Top Wall
                    Call analyze_checkneutral(sType, oCard, 2, "Down", 5, "Up", "Up", ThisPlrId)
                    'Checking Left(2), Right(1) and Right(2) + Right Wall
                    Call analyze_checkneutral(sType, oCard, 2, "Left", 1, "Right", "Right", ThisPlrId)
                    'Checking Down(2), Up(5) and Right(2) + Right Wall
                    Call analyze_checkneutral(sType, oCard, 2, "Down", 5, "Up", "Right", ThisPlrId)
                Case 3
                    'Checking Up(3), Down(0) and Left(3) + Left Wall
                    Call analyze_checkneutral(sType, oCard, 3, "Up", 0, "Down", "Left", ThisPlrId)
                    'Checking Right(3), Left(4) and Left(3) + Left Wall
                    Call analyze_checkneutral(sType, oCard, 3, "Right", 4, "Left", "Left", ThisPlrId)
                    'Checking Down(3), Up(6) and Left(3) + Left Wall
                    Call analyze_checkneutral(sType, oCard, 3, "Down", 6, "Up", "Left", ThisPlrId)
                Case 5
                    'Checking Up(5), Down(2) and Right(3) + Right Wall
                    Call analyze_checkneutral(sType, oCard, 5, "Up", 2, "Down", "Right", ThisPlrId)
                    'Checking Left(5), Right(4) and Right(3) + Right Wall
                    Call analyze_checkneutral(sType, oCard, 5, "Left", 4, "Right", "Right", ThisPlrId)
                    'Checking Down(5), Up(8) and Right(3) + Right Wall
                    Call analyze_checkneutral(sType, oCard, 5, "Down", 8, "Up", "Right", ThisPlrId)
                Case 6
                    'Checking Up(6), Down(3) and Left(6) + Left Wall
                    Call analyze_checkneutral(sType, oCard, 6, "Up", 3, "Down", "Left", ThisPlrId)
                    'Checking Right(6), Left(7) and Left(6) + Left Wall
                    Call analyze_checkneutral(sType, oCard, 6, "Right", 7, "Left", "Left", ThisPlrId)
                    'Checking Up(6), Down(3) and Down(6) + Bottom Wall
                    Call analyze_checkneutral(sType, oCard, 6, "Up", 3, "Down", "Down", ThisPlrId)
                    'Checking Right(6), Left(7) and Down(6) + Bottom Wall
                    Call analyze_checkneutral(sType, oCard, 6, "Right", 7, "Left", "Down", ThisPlrId)
                Case 7
                    'Checking Up(7), Down(4) and Down(7) + Bottom Wall
                    Call analyze_checkneutral(sType, oCard, 7, "Up", 4, "Down", "Down", ThisPlrId)
                    'Checking Left(7), Right(6) and Down(7) + Bottom Wall
                    Call analyze_checkneutral(sType, oCard, 7, "Left", 6, "Right", "Down", ThisPlrId)
                    'Checking Right(7), Left(8) and Down(7) + Bottom Wall
                    Call analyze_checkneutral(sType, oCard, 7, "Right", 8, "Left", "Down", ThisPlrId)
                Case 8
                    'Checking Up(8), Down(5) and Right(8) + Right Wall
                    Call analyze_checkneutral(sType, oCard, 8, "Up", 5, "Down", "Right", ThisPlrId)
                    'Checking Left(8), Right(7) and Right(8) + Right Wall
                    Call analyze_checkneutral(sType, oCard, 8, "Left", 7, "Right", "Right", ThisPlrId)
                    'Checking Up(8), Down(5) and Down(8) + Bottom Wall
                    Call analyze_checkneutral(sType, oCard, 8, "Up", 5, "Down", "Down", ThisPlrId)
                    'Checking Left(8), Right(7) and Down(8) + Bottom Wall
                    Call analyze_checkneutral(sType, oCard, 8, "Left", 7, "Right", "Down", ThisPlrId)
            End Select
        Catch ex As Exception
            Call errorsub(ex, "analyze_checkneutral")
        End Try
    End Sub

    Private Sub analyze_checkneutral(ByVal sType As String, ByVal oCard As Entities.BusinessObjects.PlayingCard, ByVal iSquareID As Integer, ByVal psdir As String, ByVal neighbor As String, ByVal neighdir As String, ByVal backwalldir As String, ByVal ThisPlrId As Integer)
        Try
            Dim pointchange As Integer
            Dim ignoremove, part1 As Boolean
            Dim bOK As Boolean ' It's okay!

            If IsEmpty(neighbor) = True Then ignoremove = True
            'If (cardvalue(dCardName, psdir) + Val(Readini("EValue", dCardName, My.Application.Info.DirectoryPath & "\gamefiles\" & GameID & ".tgf"))) = 0 Then ignoremove = True

            If ignoremove = False Then
                Dim oCardInfo As New LogicLayer.Cards.PlayingCardInfo

                Dim oNeighbor1Card As Entities.BusinessObjects.PlayingCard = oCardInfo.GetPlayingCard(Board(neighbor), Cards, CardNameList)

                Select Case sType
                    Case "same"
                        If (oCard.DirectionValue(psdir) + EValue(iSquareID)) = (oNeighbor1Card.DirectionValue(neighdir) + EValue(neighbor)) _
                            And (oCard.DirectionValue(backwalldir) + EValue(iSquareID) = 0) Then
                            'Then it passes
                            bOK = True
                        Else
                            bOK = False
                        End If
                    Case "plus"
                        If (oCard.DirectionValue(psdir) + EValue(iSquareID) + oNeighbor1Card.DirectionValue(neighdir) + EValue(neighbor)) = _
                            (oCard.DirectionValue(backwalldir) + EValue(iSquareID) + 0) Then
                            'Then it passes
                            bOK = True
                        Else
                            bOK = False
                        End If
                    Case "minus"
                        If Math.Abs(oCard.DirectionValue(psdir) + EValue(iSquareID) - oNeighbor1Card.DirectionValue(neighdir) + EValue(neighbor)) = _
    Math.Abs(oCard.DirectionValue(backwalldir) + EValue(iSquareID) - 0) Then
                            bOK = True
                        Else
                            bOK = False
                        End If
                End Select

                If bOK = True Then
                    Call SendTTCardData(IIf(ThisPlrId = 1, "blue", "red"), iSquareID, oCard)
                    Call SendTTCardData(IIf(ThisPlrId = 1, "blue", "red"), neighbor, oNeighbor1Card)
                    Call SendTTCardFlip(iSquareID, neighbor)

                    If CardColor(neighbor) = IIf(ThisPlrId = 1, "red", "blue") Then
                        pointchange += 1
                        part1 = True
                    End If

                    CardColor(iSquareID) = IIf(ThisPlrId = 1, "blue", "red")
                    CardColor(neighbor) = IIf(ThisPlrId = 1, "blue", "red")

                    Select Case sType
                        Case "same"
                            Call SameAlert(pointchange)
                        Case "plus"
                            Call PlusAlert(pointchange)
                        Case "minus"
                            Call MinusAlert(pointchange)
                    End Select

                    HistoryInsert(iSquareID, oCard.CardName, String.Concat("Neutral (", sType, ") >> ", iSquareID, ", ", neighbor))
                    Call SendPoints(pointchange, ThisPlrId)

                    If Combo = True Then If part1 = True Then Call analyze_combo(neighbor, oNeighbor1Card, ThisPlrId)
                End If
            End If
        Catch ex As Exception
            Call errorsub(ex, "analyze_checkneutral")
        End Try
    End Sub
#End Region

    Private Sub MorePower(ByVal iSquareID As Integer, ByVal ThisPlrID As Integer, ByVal oCard As Entities.BusinessObjects.PlayingCard, ByVal sDirect As String, ByVal iNeighborID As Integer, ByVal sNeighborDir As String)
        Try
            Dim ignoremove As Boolean

            If ThisPlrID = 1 And CardColor(iNeighborID) = "blue" Then ignoremove = True
            If ThisPlrID = 2 And CardColor(iNeighborID) = "red" Then ignoremove = True
            If IsEmpty(iNeighborID) = True Then ignoremove = True

            'MorePower dSquare, ThisPlrID, dCardName, "right", 1, "left", GameID
            If Not ignoremove Then
                Dim oCardInfo As New LogicLayer.Cards.PlayingCardInfo
                Dim oNeighborCard As Entities.BusinessObjects.PlayingCard = oCardInfo.GetPlayingCard(Board(iNeighborID), Cards, CardNameList)

                If (oCard.DirectionValue(sDirect) + EValue(iSquareID)) > (oNeighborCard.DirectionValue(sNeighborDir) + EValue(iNeighborID)) Then
                    'Card is stronger, replace the color
                    Call SendTTCardData(IIf(ThisPlrID = 1, "blue", "red"), iNeighborID, oNeighborCard)
                    Call SendTTCardFlip(iSquareID, iNeighborID)

                    HistoryInsert(iSquareID, oCard.CardName, String.Concat("Power >> ", iSquareID, ", ", iNeighborID))
                    CardColor(iNeighborID) = IIf(ThisPlrID = 1, "blue", "red")
                    SendPoints(1, ThisPlrID)
                End If
            End If
        Catch ex As Exception
            Call errorsub(ex, "MorePower")
        End Try
    End Sub

    Private Sub RMorePower(ByVal iSquareID As Integer, ByVal ThisPlrID As Integer, ByVal oCard As Entities.BusinessObjects.PlayingCard, ByVal sDirect As String, ByVal iNeighborID As Integer, ByVal sNeighborDir As String)
        Try
            Dim ignoremove As Boolean

            If ThisPlrID = 1 And CardColor(iNeighborID) = "blue" Then ignoremove = True
            If ThisPlrID = 2 And CardColor(iNeighborID) = "red" Then ignoremove = True
            If IsEmpty(iNeighborID) = True Then ignoremove = True

            'MorePower dSquare, ThisPlrID, dCardName, "right", 1, "left", GameID
            If Not ignoremove Then
                Dim oCardInfo As New LogicLayer.Cards.PlayingCardInfo
                Dim oNeighborCard As Entities.BusinessObjects.PlayingCard = oCardInfo.GetPlayingCard(Board(iNeighborID), Cards, CardNameList)

                If (oCard.DirectionValue(sDirect) + EValue(iSquareID)) < (oNeighborCard.DirectionValue(sNeighborDir) + EValue(iNeighborID)) Then
                    'Card is stronger, replace the color
                    Call SendTTCardData(IIf(ThisPlrID = 1, "blue", "red"), iNeighborID, oNeighborCard)
                    Call SendTTCardFlip(iSquareID, iNeighborID)

                    HistoryInsert(iSquareID, oCard.CardName, String.Concat("Reverse >> ", iSquareID, ", ", iNeighborID))
                    CardColor(iNeighborID) = IIf(ThisPlrID = 1, "blue", "red")
                    SendPoints(1, ThisPlrID)
                End If
            End If
        Catch ex As Exception
            Call errorsub(ex, "MorePower")
        End Try
    End Sub

    Private Sub analyze_basic(ByVal iSquareID As Integer, ByVal oCard As Entities.BusinessObjects.PlayingCard, ByVal ThisPlrId As Integer)
        Select Case iSquareID
            Case 0
                'Basic Checking
                MorePower(iSquareID, ThisPlrId, oCard, "Right", 1, "Left")
                MorePower(iSquareID, ThisPlrId, oCard, "Down", 3, "Up")
            Case 1
                'Basic Checking
                MorePower(iSquareID, ThisPlrId, oCard, "Left", 0, "Right")
                MorePower(iSquareID, ThisPlrId, oCard, "Down", 4, "Up")
                MorePower(iSquareID, ThisPlrId, oCard, "Right", 2, "Left")
            Case 2
                'Basic Checking
                MorePower(iSquareID, ThisPlrId, oCard, "Left", 1, "Right")
                MorePower(iSquareID, ThisPlrId, oCard, "Down", 5, "Up")
            Case 3
                'Basic Checking
                MorePower(iSquareID, ThisPlrId, oCard, "Up", 0, "Down")
                MorePower(iSquareID, ThisPlrId, oCard, "Right", 4, "Left")
                MorePower(iSquareID, ThisPlrId, oCard, "Down", 6, "Up")
            Case 4
                'Basic Checking
                MorePower(iSquareID, ThisPlrId, oCard, "Up", 1, "Down")
                MorePower(iSquareID, ThisPlrId, oCard, "Left", 3, "Right")
                MorePower(iSquareID, ThisPlrId, oCard, "Right", 5, "Left")
                MorePower(iSquareID, ThisPlrId, oCard, "Down", 7, "Up")
            Case 5
                'Basic Checking
                MorePower(iSquareID, ThisPlrId, oCard, "Up", 2, "Down")
                MorePower(iSquareID, ThisPlrId, oCard, "Left", 4, "Right")
                MorePower(iSquareID, ThisPlrId, oCard, "Down", 8, "Up")
            Case 6
                'Basic Checking
                MorePower(iSquareID, ThisPlrId, oCard, "Up", 3, "Down")
                MorePower(iSquareID, ThisPlrId, oCard, "Right", 7, "Left")
            Case 7
                'Basic Checking
                MorePower(iSquareID, ThisPlrId, oCard, "Left", 6, "Right")
                MorePower(iSquareID, ThisPlrId, oCard, "Up", 4, "Down")
                MorePower(iSquareID, ThisPlrId, oCard, "Right", 8, "Left")
            Case 8
                'Basic Checking
                MorePower(iSquareID, ThisPlrId, oCard, "Left", 7, "Right")
                MorePower(iSquareID, ThisPlrId, oCard, "Up", 5, "Down")
        End Select
    End Sub

    Private Sub analyze_reverse(ByVal iSquareID As Integer, ByVal oCard As Entities.BusinessObjects.PlayingCard, ByVal ThisPlrId As Integer)
        Select Case iSquareID
            Case 0
                'Basic Checking
                RMorePower(iSquareID, ThisPlrId, oCard, "Right", 1, "Left")
                RMorePower(iSquareID, ThisPlrId, oCard, "Down", 3, "Up")
            Case 1
                'Basic Checking
                RMorePower(iSquareID, ThisPlrId, oCard, "Left", 0, "Right")
                RMorePower(iSquareID, ThisPlrId, oCard, "Down", 4, "Up")
                RMorePower(iSquareID, ThisPlrId, oCard, "Right", 2, "Left")
            Case 2
                'Basic Checking
                RMorePower(iSquareID, ThisPlrId, oCard, "Left", 1, "Right")
                RMorePower(iSquareID, ThisPlrId, oCard, "Down", 5, "Up")
            Case 3
                'Basic Checking
                RMorePower(iSquareID, ThisPlrId, oCard, "Up", 0, "Down")
                RMorePower(iSquareID, ThisPlrId, oCard, "Right", 4, "Left")
                RMorePower(iSquareID, ThisPlrId, oCard, "Down", 6, "Up")
            Case 4
                'Basic Checking
                RMorePower(iSquareID, ThisPlrId, oCard, "Up", 1, "Down")
                RMorePower(iSquareID, ThisPlrId, oCard, "Left", 3, "Right")
                RMorePower(iSquareID, ThisPlrId, oCard, "Right", 5, "Left")
                RMorePower(iSquareID, ThisPlrId, oCard, "Down", 7, "Up")
            Case 5
                'Basic Checking
                RMorePower(iSquareID, ThisPlrId, oCard, "Up", 2, "Down")
                RMorePower(iSquareID, ThisPlrId, oCard, "Left", 4, "Right")
                RMorePower(iSquareID, ThisPlrId, oCard, "Down", 8, "Up")
            Case 6
                'Basic Checking
                RMorePower(iSquareID, ThisPlrId, oCard, "Up", 3, "Down")
                RMorePower(iSquareID, ThisPlrId, oCard, "Right", 7, "Left")
            Case 7
                'Basic Checking
                RMorePower(iSquareID, ThisPlrId, oCard, "Left", 6, "Right")
                RMorePower(iSquareID, ThisPlrId, oCard, "Up", 4, "Down")
                RMorePower(iSquareID, ThisPlrId, oCard, "Right", 8, "Left")
            Case 8
                'Basic Checking
                RMorePower(iSquareID, ThisPlrId, oCard, "Left", 7, "Right")
                RMorePower(iSquareID, ThisPlrId, oCard, "Up", 5, "Down")
        End Select
    End Sub

    Private Sub analyze_skip(ByVal iSquareID As Integer, ByVal oCard As Entities.BusinessObjects.PlayingCard, ByVal ThisPlrID As Integer)
        Select Case iSquareID
            Case 0
                MorePower(iSquareID, ThisPlrID, oCard, "Right", 2, "Left")
                MorePower(iSquareID, ThisPlrID, oCard, "Down", 6, "Up")
            Case 1
                MorePower(iSquareID, ThisPlrID, oCard, "Down", 7, "Up")
            Case 2
                MorePower(iSquareID, ThisPlrID, oCard, "Left", 0, "Right")
                MorePower(iSquareID, ThisPlrID, oCard, "Down", 8, "Up")
            Case 3
                MorePower(iSquareID, ThisPlrID, oCard, "Right", 5, "Left")
            Case 5
                MorePower(iSquareID, ThisPlrID, oCard, "Left", 3, "Right")
            Case 6
                MorePower(iSquareID, ThisPlrID, oCard, "Right", 8, "Left")
                MorePower(iSquareID, ThisPlrID, oCard, "Up", 0, "Down")
            Case 7
                MorePower(iSquareID, ThisPlrID, oCard, "Up", 1, "Down")
            Case 8
                MorePower(iSquareID, ThisPlrID, oCard, "Left", 6, "Right")
                MorePower(iSquareID, ThisPlrID, oCard, "Up", 2, "Down")
        End Select
    End Sub

    Public Function GameIsOver(Optional ByRef skipWinner As Integer = 0, Optional ByVal blnTimeOut As Boolean = False) As Boolean
        Try
            Dim oGameFunctions As New GameFunctions
            Dim oStats As New Statistics
            Dim iWinner_Gold As Integer, iLoser_Gold As Integer, sWinner As String = String.Empty, sLoser As String = String.Empty

            Dim oTables As New BusinessLayer.Tables
            Dim iTableID As Integer = oTables.FindPlayersTable(Tables, Player1)

            Select Case skipWinner
                Case 3
                    Player1_Score = 5
                    Player2_Score = 5
                    Surrender = False
                Case 1
                    Player1_Score = 9
                    Player2_Score = 1

                    If blnTimeOut = False Then
                        Surrender = True
                        Call blockchat(String.Empty, "Surrender", String.Concat(Player2, " surrendered and lost."), GetSocket(Tables(itableid).player1))
                        Call blockchat(String.Empty, "Surrender", String.Concat(Player2, " surrendered and lost."), GetSocket(Tables(itableid).player2))
                    End If
                Case 2
                    Player2_Score = 9
                    Player1_Score = 1

                    If blnTimeOut = False Then
                        Surrender = True
                        Call blockchat(String.Empty, "Surrender", String.Concat(Player1, " surrendered and lost."), GetSocket(Tables(itableid).player1))
                        Call blockchat(String.Empty, "Surrender", String.Concat(Player1, " surrendered and lost."), GetSocket(Tables(itableid).player2))
                    End If
            End Select

            If Player1_Score > Player2_Score Then
                'Player 1 won
                Call laddersub(Player1, Player2, CStr(Player1_Score), CStr(Player2_Score), GameID, RankFreeze, False, "tt")

                If skipWinner = 0 Then
                    iLoser_Gold = EndGameGP(Player2, -1)
                    sLoser = Player2

                    iWinner_Gold = EndGameGP(Player1, 1)
                    sWinner = Player1

                    Call giftelement(Player1)
                End If

                Call WagerRule(Player1, Player2, GameID)

                If TradeDirect = True Then
                    Call directdisplaybox()
                Else
                    Call displaybox()
                End If

                oStats.CompileStats(Player1, Player2, "tt", False)
            ElseIf Player1_Score < Player2_Score Then
                'Player 2 won

                If skipWinner = 0 Then
                    iLoser_Gold = EndGameGP(Player1, -1)
                    sLoser = Player1

                    iWinner_Gold = EndGameGP(Player2, 1)
                    sWinner = Player2

                    Call giftelement(Player2)
                End If

                Call laddersub(Player2, Player1, CStr(Player2_Score), CStr(Player1_Score), GameID, RankFreeze, False, "tt")

                Call WagerRule(Player2, Player1, GameID)

                If TradeDirect = True Then
                    Call directdisplaybox()
                Else
                    Call displaybox()
                End If

                oStats.CompileStats(Player2, Player1, "tt", False)
            ElseIf Player1_Score = Player2_Score Then
                'Draw game
                If SuddenDeath = False Or skipWinner <> 0 Then
                    If skipWinner = 0 Then
                        Call oGameFunctions.drawevent(Player1, Player2, GameID, False, "tt", Me.GameLength, Me)
                    Else
                        Call laddersub(Player1, Player2, Player1_Score.ToString, Player2_Score.ToString, GameID, False, False, "tt")
                    End If

                    oStats.CompileStats(Player1, Player2, "tt", True)
                    Call displaybox()
                Else
                    Call sDeath()
                    Call clearboard()
                    Call SendCards()
                    Exit Function
                End If
            End If

            Dim blnTurbo As Boolean

            'If Me.GameLength >= 30 And Player1_Score <> Player2_Score Then
            Call gpadd(sWinner, iWinner_Gold, True)
            Call gpadd(sLoser, iLoser_Gold, True)

            Call oGameFunctions.GiveExp(sWinner, IIf(Me.Winner = 1, Player1_Score, Player2_Score), sLoser, IIf(Me.Winner = 1, Player1_Score, Player2_Score), GameID, Me, False, "tt")
            'ElseIf Player1_Score <> Player2_Score Then
            'Call blockchat(String.Empty, "Warning", "You did not earn gold, active points, or experience because you and your opponent played too fast.  Turbo play is no longer rewarded.", GetSocket(Tables(iTableID).Player1))
            'Call blockchat(String.Empty, "Warning", "You did not earn gold, active points, or experience because you and your opponent played too fast.  Turbo play is no longer rewarded.", GetSocket(Tables(iTableID).Player2))

            'blnTurbo = True
            'End If

            HistoryInsert(9999, String.Empty, String.Concat("Game Ends"))

            Send(String.Empty, "TT_CLOSE", GetSocket(Tables(iTableID).Player1))
            Send(String.Empty, "TT_CLOSE", GetSocket(Tables(iTableID).Player2))
            EndGame()

            If skipWinner = 0 And blnTurbo = False Then oGameFunctions.EndGameEvent(Tables(iTableID))

            Dim oTableFunctions As New BusinessLayer.Tables
            Call oTableFunctions.ClosePlayerTable(Tables, Player1, False)
        Catch ex As Exception
            Call errorsub(ex, "GameIsOver")

            Dim oTableFunctions As New BusinessLayer.Tables
            Call oTableFunctions.ClosePlayerTable(Tables, Player1, False)
        End Try
    End Function

    Private Sub displaybox()
        Try
            Dim sResult As String = String.Empty
            Dim oTables As New BusinessLayer.Tables
            Dim iTableID As Integer = oTables.FindPlayersTable(Tables, Player1)

            Call setaccountdata(Player1, "lastplayed", DateString)
            Call setaccountdata(Player2, "lastplayed", DateString)

            If TradeNone = True Or FullDeck = True Then Exit Sub

            If TradeRandomOne = True Then
                If Winner = 0 Then Exit Sub

                CardstoTake = 1
                Update()

                Call tt_takerandomcard(String.Concat("TT_TAKERANDOMCARD ", GameID), IIf(Winner = 1, GetSocket(Tables(itableid).player1), GetSocket(Tables(itableid).player2)))
                Exit Sub
            End If

            ' First variables go to GP

            If Player1_Score > Player2_Score Then
                sResult = "Win"
            ElseIf Player1_Score < Player2_Score Then
                sResult = "Loss"
            ElseIf Player1_Score = Player2_Score Then
                sResult = "Draw"
            End If

            CardstoTake = FindCardsWon()
            Update()

            ' Sending User his/her data

            Dim iWon As Integer = IIf(Winner = 1, 1, 0)

            Send(String.Empty, String.Concat("TT_DISPLAY ", GameID, " ", sResult, " ", Player2, " ", iWon, " ", CardstoTake, _
            " ", Player_OriginalHand(2, 0).Replace(" ", "%20"), _
            " ", Player_OriginalHand(2, 1).Replace(" ", "%20"), _
            " ", Player_OriginalHand(2, 2).Replace(" ", "%20"), _
            " ", Player_OriginalHand(2, 3).Replace(" ", "%20"), _
            " ", Player_OriginalHand(2, 4).Replace(" ", "%20")), GetSocket(Tables(itableid).player1))

            ' First find player 2's statistics

            If Player2_Score > Player1_Score Then
                sResult = "Win"
            ElseIf Player2_Score < Player1_Score Then
                sResult = "Loss"
            ElseIf Player1_Score = Player2_Score Then
                sResult = "Draw"
            End If

            iWon = IIf(Winner = 2, 1, 0)

            Send(String.Empty, String.Concat("TT_DISPLAY ", GameID, " ", sResult, " ", Player2, " ", iWon, " ", CardstoTake, _
            " ", Player_OriginalHand(1, 0).Replace(" ", "%20"), _
            " ", Player_OriginalHand(1, 1).Replace(" ", "%20"), _
            " ", Player_OriginalHand(1, 2).Replace(" ", "%20"), _
            " ", Player_OriginalHand(1, 3).Replace(" ", "%20"), _
            " ", Player_OriginalHand(1, 4).Replace(" ", "%20")), GetSocket(Tables(itableid).player2))
        Catch ex As Exception
            Call errorsub(ex, "displaybox")
        End Try
    End Sub

    Private Sub directdisplaybox()
        Try
            Dim sResult As String = String.Empty
            Dim oTables As New BusinessLayer.Tables
            Dim iTableID As Integer = oTables.FindPlayersTable(Tables, Player1)

            Call setaccountdata(Player1, "lastplayed", DateString)
            Call setaccountdata(Player2, "lastplayed", DateString)

            ' First variables go to GP

            If Player1_Score > Player2_Score Then
                sResult = "Win"
            ElseIf Player1_Score < Player2_Score Then
                sResult = "Loss"
            ElseIf Player1_Score = Player2_Score Then
                sResult = "Draw"
            End If

            CardstoTake = 6

            Dim playercards(4) As String
            Dim x As Integer = 0

            For t As Integer = 0 To 8
                If PlacedBy(t) = 2 And CardColor(t) = "blue" Then
                    Call cardchg(Player1, Board(t), 1)
                    Call cardchg(Player2, Board(t), -1)
                    playercards(x) = Board(t).Replace(" ", "%20")
                    x += 1
                End If
            Next t

            ' Sending User his/her data

            Dim iWon As Integer = IIf(Winner = 1, 1, 0)

            Send(String.Empty, String.Concat("TT_DISPLAY ", sResult, " ", Player2, " ", iWon, " 6 ", playercards(0), " ", playercards(1), " ", playercards(2), " ", playercards(3), " ", playercards(4)), GetSocket(Tables(itableid).player1))
            Send(String.Empty, String.Concat("TT_CARDGAIN ", playercards(0), " ", playercards(1), " ", playercards(2), " ", playercards(3), " ", playercards(4), " 1"), GetSocket(Tables(itableid).player1))
            Send(String.Empty, String.Concat("TT_CARDLOST ", playercards(0), " ", playercards(1), " ", playercards(2), " ", playercards(3), " ", playercards(4), " 1"), GetSocket(Tables(itableid).player2))

            'Dim t As Byte

            'For t = 0 To 4
            '    If playercards(t) <> "" Then
            '        Send(plr1nick, "CHATBLOCK Received You%20received%201%20" & playercards(t))
            '        Send(plr2nick, "CHATBLOCK Lost You%20lost%201%20" & playercards(t))
            '    End If
            'Next t

            ' End Part 1 ----------------------------------------------------------------------------------

            If Player2_Score > Player1_Score Then
                sResult = "Win"
            ElseIf Player2_Score < Player1_Score Then
                sResult = "Loss"
            ElseIf Player1_Score = Player2_Score Then
                sResult = "Draw"
            End If

            For y As Integer = 0 To 4
                playercards(y) = String.Empty
            Next y

            For t As Integer = 0 To 8
                If PlacedBy(t) = 1 And CardColor(t) = "red" Then
                    Call cardchg(Player2, Board(t), 1)
                    Call cardchg(Player1, Board(t), -1)
                    playercards(x) = Board(t).Replace(" ", "%20")
                    x += 1
                End If
            Next t

            iWon = IIf(Winner = 2, 1, 0)

            Send(String.Empty, String.Concat("TT_DISPLAY ", sResult, " ", Player2, " ", iWon, " 6 ", playercards(0), " ", playercards(1), " ", playercards(2), " ", playercards(3), " ", playercards(4)), GetSocket(Tables(itableid).player2))
            Send(String.Empty, String.Concat("TT_CARDGAIN ", playercards(0), " ", playercards(1), " ", playercards(2), " ", playercards(3), " ", playercards(4), " 1"), GetSocket(Tables(itableid).player2))
            Send(String.Empty, String.Concat("TT_CARDLOST ", playercards(0), " ", playercards(1), " ", playercards(2), " ", playercards(3), " ", playercards(4), " 1"), GetSocket(Tables(itableid).player1))
        Catch ex As Exception
            Call errorsub(ex, "directdisplaybox")
        End Try
    End Sub

    Private Function giftelement(ByVal winner As String) As Boolean
        Try
            For x As Integer = 0 To 8
                If Element(x) = "jackpot" Then
                    If (CardColor(x) = "blue" And PlayerID(winner) = 1) Or (CardColor(x) = "red" And PlayerID(winner) = 2) Then
                        Call resetjackpot(winner)
                        Return True
                    End If
                End If
            Next x

            Return False
        Catch ex As Exception
            Call errorsub(ex, "giftelement")
        End Try
    End Function

    Private Sub clearboard()
        Try
            Dim oTables As New BusinessLayer.Tables
            Dim iTableID As Integer = oTables.FindPlayersTable(Tables, Player1)

            ' Clears the Board
            For x As Integer = 0 To 8
                Board(x) = String.Empty
                CardColor(x) = String.Empty
                PlacedBy(x) = 0
                EValue(x) = 0

                If Elemental = True Then
                    Send(String.Empty, String.Concat("TT_ELEMENT ", x, " ", Element(x)), GetSocket(Tables(itableid).player1))
                    Send(String.Empty, String.Concat("TT_ELEMENT ", x, " ", Element(x)), GetSocket(Tables(itableid).player2))
                End If
            Next

            Player1_Score = 5
            Player2_Score = 5
            PlayerReady(1) = False
            PlayerReady(2) = False

            Times += 1

            Update()
            UpdateHand()
        Catch ex As Exception
            Call errorsub(ex, "clearboard")
        End Try
    End Sub

    Private Sub TTEndgame(ByVal socket As Integer)
        Dim oChatFunctions As New ChatFunctions

        Try
            Dim oTables As New BusinessLayer.Tables
            Dim iTableID As Integer = oTables.FindPlayersTable(Tables, frmSystray.Winsock(socket).Tag)
            Dim iGameID As Integer = Tables(iTableID).GameID

            If iGameID = 0 Then Throw New Exception
            GameID = iGameID

            LoadRecord()

            If TimeoutDate < Date.Now Then
                'The game has expired
                Call GameIsOver(IIf(PlayerID(frmSystray.Winsock(socket).Tag) = 1, 1, 2), True)
            Else
                oChatFunctions.unisend("CHAT", String.Concat(frmSystray.Winsock(socket).Tag, " !endgame [End Game]"))
                Call blockchat(String.Empty, "Request", "An endgame request has been sent to the administrators that are online as 4 minutes have not yet elapsed.", socket)
            End If
        Catch ex As Exception
            oChatFunctions.uniwarn(String.Concat(frmSystray.Winsock(socket).Tag, " sent an invalid stalemate request."))
        End Try
    End Sub

    Private Sub TTStalemate(ByVal socket As Integer)
        Try
            Dim oTables As New BusinessLayer.Tables
            Dim iTableID As Integer = oTables.FindPlayersTable(Tables, frmSystray.Winsock(socket).Tag)
            Dim iGameID As Integer = Tables(iTableID).GameID

            If iGameID = 0 Then Throw New Exception
            GameID = iGameID

            LoadRecord()

            Dim iPlayerID As Integer = PlayerID(frmSystray.Winsock(socket).Tag)

            If iPlayerID = 1 Then
                Tables(iTableID).Stalemate1 = True
            ElseIf iPlayerID = 2 Then
                Tables(iTableID).Stalemate2 = True
            Else
                Throw New Exception
            End If

            If Tables(iTableID).Stalemate1 = True And Tables(iTableID).Stalemate2 = True Then
                GameIsOver(3)
            Else
                Send(String.Empty, "GAMESTALEMATE", IIf(iPlayerID = 1, GetSocket(Tables(iTableID).Player2), GetSocket(Tables(iTableID).Player1)))
            End If
        Catch ex As Exception
            Dim oChatFunctions As New ChatFunctions
            oChatFunctions.uniwarn(String.Concat(frmSystray.Winsock(socket).Tag, " sent an invalid stalemate request."))
        End Try
    End Sub

    Private Sub TTSurrender(ByVal socket As Integer)
        Try
            Dim oPlayerAccount As New PlayerAccountDAL(frmSystray.Winsock(socket).Tag)

            If oPlayerAccount.Gold < 100 Then
                Call blockchat(frmSystray.Winsock(socket).Tag, "Surrender", "You do not have enough gold to surrender. Surrenders cost 100GP.")
                Exit Sub
            End If

            Dim oTables As New BusinessLayer.Tables
            Dim iGameID As Integer = Tables(oTables.FindPlayersTable(Tables, frmSystray.Winsock(socket).Tag)).GameID

            If iGameID = 0 Then Throw New Exception
            GameID = iGameID

            Call gpadd(frmSystray.Winsock(socket).Tag, -100, True)
            Call historyadd(frmSystray.Winsock(socket).Tag, 6, frmSystray.Winsock(socket).Tag & " surrendered on " & DateString & " " & TimeString & " - GameID: " & iGameID)

            LoadRecord()
            GameIsOver(IIf(Player1 = frmSystray.Winsock(socket).Tag, 2, 1))
        Catch ex As Exception
            Dim oChatFunctions As New ChatFunctions
            oChatFunctions.uniwarn(String.Concat(frmSystray.Winsock(socket).Tag, " sent an invalid stalemate request."))
        End Try
    End Sub

    Private Sub SendCards()
        Try
            Dim oTables As New BusinessLayer.Tables
            Dim iTableID As Integer = oTables.FindPlayersTable(Tables, Player1)

            For x As Integer = 0 To 4
                Send(String.Empty, String.Concat("TT_HAND ", x, " ", Player_Hand(1, x).Replace(" ", "%20")), GetSocket(Tables(itableid).player1))
                Send(String.Empty, String.Concat("TT_HAND ", x, " ", Player_Hand(2, x).Replace(" ", "%20")), GetSocket(Tables(itableid).player2))
            Next

            Dim oGameFunctions As New GameFunctions
            Dim iStarter As Integer

            If Starter = True Then
                iStarter = Startee
            Else
                iStarter = oGameFunctions.PickStarter(False)
            End If

            If Open = True Then SendOpenCards()

            Send(String.Empty, String.Concat("TT_STARTER ", iStarter), GetSocket(Tables(itableid).player1))
            Send(String.Empty, String.Concat("TT_STARTER ", iStarter), GetSocket(Tables(itableid).player2))
        Catch ex As Exception
            Call errorsub(ex, "SendCards")
        End Try
    End Sub

    Private Sub sDeath()
        ' Clear the player's hand, we are about to fill it.
        Dim oTables As New BusinessLayer.Tables
        Dim iTableID As Integer = oTables.FindPlayersTable(Tables, Player1)
        'Send(String.Empty, "TT_CLEARBOARD", GetSocket(tables(itableid).player1))
        'Send(String.Empty, "TT_CLEARBOARD", GetSocket(tables(itableid).player2))

        'Dim bP1_Skip As Boolean = False, bP2_Skip As Boolean = False

        'For x As Integer = 0 To 4
        '    If Player_Hand(1, x) <> String.Empty Then
        '        Player_Hand(2, x) = String.Empty
        'Next

        Send(String.Empty, "TT_TURNOVER", GetSocket(Tables(itableid).player1))
        Send(String.Empty, "TT_TURNOVER", GetSocket(Tables(itableid).player2))

        Send(String.Empty, String.Concat("TT_PICKCARDS 0"), GetSocket(Tables(itableid).player1))
        Send(String.Empty, String.Concat("TT_PICKCARDS 0"), GetSocket(Tables(itableid).player2))

        For x As Integer = 0 To 8
            Select Case CardColor(x)
                Case "blue"
                    For y As Integer = 0 To 4
                        If Player_Hand(1, y) = String.Empty Then
                            Player_Hand(1, y) = Board(x)
                            Exit For
                        End If
                    Next
                Case "red"
                    For y As Integer = 0 To 4
                        If Player_Hand(2, y) = String.Empty Then
                            Player_Hand(2, y) = Board(x)
                            Exit For
                        End If
                    Next
            End Select
        Next
    End Sub

    Public Sub New()

    End Sub

    Public Sub New(ByVal iGameID As Integer)
        GameID = iGameID
        LoadRecord()
    End Sub
End Class

