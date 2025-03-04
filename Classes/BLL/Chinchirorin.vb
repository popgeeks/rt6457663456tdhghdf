Imports System.Configuration

Public Class Chinchirorin
    Inherits ChinchirorinDAL

    Public Sub Chinchinmodule(ByVal sCommand As String, ByVal incoming As String, ByVal socket As Integer)
        Select Case sCommand
            Case "CHINCHINROLL"
                Call chinchinroll(socket)
            Case "CHINCHINENDGAME"
                Call chinchinendgame(socket)
        End Select
    End Sub

    Private Sub chinchinendgame(ByVal socket As Integer)
        Dim oChatFunctions As New ChatFunctions

        Try
            Dim oTables As New BusinessLayer.Tables
            Dim iTableID As Integer = oTables.FindPlayersTable(Tables, frmSystray.Winsock(socket).Tag)

            If Tables(iTableID).GameID = 0 Then Throw New Exception
            Dim oGameData As DataRow = SelectGame(Tables(iTableID).GameID)

            If oGameData.Item("Timeout") < Date.Now Then
                'The game has expired
                Call chinchinisover(iTableID, 0, 0)
            Else
                oChatFunctions.unisend("CHAT", String.Concat(frmSystray.Winsock(socket).Tag, " !endgame [End Game]"))
                Call blockchat(String.Empty, "Request", "An endgame request has been sent to the administrators that are online as 4 minutes have not yet elapsed.", socket)
            End If
        Catch ex As Exception
            oChatFunctions.uniwarn(String.Concat(frmSystray.Winsock(socket).Tag, " sent an invalid stalemate request."))
        End Try
    End Sub

    Public Sub chinchinroll(ByVal socket As Integer)
        Dim intDice(3) As Integer
        Dim intPlayerID, intOppID As Integer
        Dim iOut As Integer
        Dim oPlayerAccount As New PlayerAccountDAL(frmSystray.Winsock(socket).Tag)
        Dim oTables As New BusinessLayer.Tables

        Try
            If oPlayerAccount.Player = String.Empty Then Exit Sub

            Dim tableID As Integer = oTables.FindPlayersTable(Tables, oPlayerAccount.Player)

            For x As Integer = 1 To 3
                Randomize()
                intDice(x) = Int(Rnd() * 6) + 1
            Next x

            ' Let's check if they lose automatically with 1, 2, 3

            Send(Tables(tableID).Player1, String.Concat("CHINCHINTEXT ", oPlayerAccount.Player, " rolled a ", intDice(1), ", ", intDice(2), ", ", intDice(3)))
            Send(Tables(tableID).Player2, String.Concat("CHINCHINTEXT ", oPlayerAccount.Player, " rolled a ", intDice(1), ", ", intDice(2), ", ", intDice(3)))

            Send(Tables(tableID).Player1, String.Concat("CHINCHINDICE ", intDice(1), " ", intDice(2), " ", intDice(3)))
            Send(Tables(tableID).Player2, String.Concat("CHINCHINDICE ", intDice(1), " ", intDice(2), " " & intDice(3)))

            Call ChinHistory(Tables(tableID).GameID, oPlayerAccount.Player, intDice(1), intDice(2), intDice(3), String.Empty)
            Dim oGameData As DataRow = SelectGame(Tables(tableID).GameID)

            If oPlayerAccount.Player = Tables(tableID).Player1 Then
                intPlayerID = 1
                intOppID = 2

                If oGameData.Item("chin_p1rolls").ToString = "3" Then
                    Call ChangeTurn(tableID, String.Empty, String.Empty, intPlayerID)
                    Exit Sub
                End If

                Call UpdateRecord("p1rolls", CInt(oGameData.Item("chin_p1rolls").ToString) + 1, Tables(tableID).GameID)
                oGameData.Item("chin_p1rolls") += 1
            ElseIf oPlayerAccount.Player = Tables(tableID).Player2 Then
                intPlayerID = 2
                intOppID = 1

                If oGameData.Item("chin_p2rolls").ToString = "3" Then
                    Call ChangeTurn(tableID, String.Empty, String.Empty, intPlayerID)
                    Exit Sub
                End If

                Call UpdateRecord("p2rolls", CInt(oGameData.Item("chin_p2rolls").ToString) + 1, Tables(tableID).GameID)
                oGameData.Item("chin_p2rolls") += 1
            End If

            Randomize()
            iOut = Int(Rnd() * 100) + 1

            If iOut >= 97 Then
                'Oops you threw it out of the bowl

                Send(Tables(tableID).Player1, String.Concat("CHINCHINTEXT ", oPlayerAccount.Player, " threw the dice out of the bowl!"))
                Send(Tables(tableID).Player2, String.Concat("CHINCHINTEXT ", oPlayerAccount.Player, " threw the dice out of the bowl!"))

                Call ChinHistory(Tables(tableID).GameID, oPlayerAccount.Player, String.Empty, String.Empty, String.Empty, "Threw Dice Out of the Bowl - Automatic Loss")

                If intPlayerID = 1 Then
                    Call chinchinisover(tableID, 2, 1)
                Else
                    Call chinchinisover(tableID, 1, 1)
                End If
            End If

            If intDice(1) = 1 Then
                If intDice(2) = 2 Or intDice(3) = 2 Then
                    If intDice(2) = 3 Or intDice(3) = 3 Then
                        Send(Tables(tableID).Player1, String.Concat("CHINCHINTEXT ", oPlayerAccount.Player, " threw a low run and lost twice his bet!"))
                        Send(Tables(tableID).Player2, String.Concat("CHINCHINTEXT ", oPlayerAccount.Player, " threw a low run and lost twice his bet!"))

                        Call ChinHistory(Tables(tableID).GameID, oPlayerAccount.Player, String.Empty, String.Empty, String.Empty, "Low Run - Lost 2x Bet")
                        Call chinchinisover(tableID, CByte(intOppID), 2)
                        Exit Sub
                    End If
                End If
            End If

            If intDice(2) = 1 Then
                If intDice(1) = 2 Or intDice(3) = 2 Then
                    If intDice(1) = 3 Or intDice(3) = 3 Then
                        Send(Tables(tableID).Player1, String.Concat("CHINCHINTEXT ", oPlayerAccount.Player, " threw a low run and lost twice his bet!"))
                        Send(Tables(tableID).Player2, String.Concat("CHINCHINTEXT ", oPlayerAccount.Player, " threw a low run and lost twice his bet!"))
                        Call ChinHistory(Tables(tableID).GameID, oPlayerAccount.Player, String.Empty, String.Empty, String.Empty, "Low Run - Lost 2x Bet")
                        Call chinchinisover(tableID, CByte(intOppID), 2)
                        Exit Sub
                    End If
                End If
            End If

            If intDice(3) = 1 Then
                If intDice(1) = 2 Or intDice(2) = 2 Then
                    If intDice(1) = 3 Or intDice(2) = 3 Then
                        Send(Tables(tableID).Player1, String.Concat("CHINCHINTEXT ", oPlayerAccount.Player, " threw a low run and lost twice his bet!"))
                        Send(Tables(tableID).Player2, String.Concat("CHINCHINTEXT ", oPlayerAccount.Player, " threw a low run and lost twice his bet!"))
                        Call ChinHistory(Tables(tableID).GameID, oPlayerAccount.Player, String.Empty, String.Empty, String.Empty, "Low Run - Lost 2x Bet")
                        Call chinchinisover(tableID, CByte(intOppID), 2)
                        Exit Sub
                    End If
                End If
            End If

            ' Let's check if they lose 3x bet with 1, 1, 1

            If intDice(1) = 1 And intDice(2) = 1 And intDice(3) = 1 Then
                Send(Tables(tableID).Player1, String.Concat("CHINCHINTEXT ", oPlayerAccount.Player, " threw a storm and lost triple his bet!"))
                Send(Tables(tableID).Player2, String.Concat("CHINCHINTEXT ", oPlayerAccount.Player, " threw a storm and lost triple his bet!"))
                Call ChinHistory(Tables(tableID).GameID, oPlayerAccount.Player, String.Empty, String.Empty, String.Empty, "Storm - Lost 3x Bet")
                Call chinchinisover(tableID, CByte(intOppID), 3)
                Exit Sub
            End If

            ' Let's check if they won automatically 2x with 4, 5, 6

            If intDice(1) = 4 Then
                If intDice(2) = 5 Or intDice(3) = 5 Then
                    If intDice(2) = 6 Or intDice(3) = 6 Then
                        Send(Tables(tableID).Player1, String.Concat("CHINCHINTEXT ", oPlayerAccount.Player, " threw a high run and won twice his bet!"))
                        Send(Tables(tableID).Player2, String.Concat("CHINCHINTEXT ", oPlayerAccount.Player, " threw a high run and won twice his bet!"))
                        Call ChinHistory(Tables(tableID).GameID, oPlayerAccount.Player, String.Empty, String.Empty, String.Empty, "High Run - Won 2x Bet")
                        Call chinchinisover(tableID, CByte(intPlayerID), 2)
                        Exit Sub
                    End If
                End If
            End If

            If intDice(2) = 4 Then
                If intDice(1) = 5 Or intDice(3) = 5 Then
                    If intDice(1) = 6 Or intDice(3) = 6 Then
                        Send(Tables(tableID).Player1, String.Concat("CHINCHINTEXT ", oPlayerAccount.Player, " threw a high run and won twice his bet!"))
                        Send(Tables(tableID).Player2, String.Concat("CHINCHINTEXT ", oPlayerAccount.Player, " threw a high run and won twice his bet!"))
                        Call ChinHistory(Tables(tableID).GameID, oPlayerAccount.Player, String.Empty, String.Empty, String.Empty, "High Run - Won 2x Bet")
                        Call chinchinisover(tableID, CByte(intPlayerID), 2)
                        Exit Sub
                    End If
                End If
            End If

            If intDice(3) = 4 Then
                If intDice(1) = 5 Or intDice(2) = 5 Then
                    If intDice(1) = 6 Or intDice(2) = 6 Then
                        Send(Tables(tableID).Player1, String.Concat("CHINCHINTEXT ", oPlayerAccount.Player, " threw a high run and won twice his bet!"))
                        Send(Tables(tableID).Player2, String.Concat("CHINCHINTEXT ", oPlayerAccount.Player, " threw a high run and won twice his bet!"))
                        Call ChinHistory(Tables(tableID).GameID, oPlayerAccount.Player, String.Empty, String.Empty, String.Empty, "High Run - Won 2x Bet")
                        Call chinchinisover(tableID, CByte(intPlayerID), 2)
                        Exit Sub
                    End If
                End If
            End If

            'Let's see if they have 3 of a kind for 3x win

            If intDice(1) = intDice(2) And intDice(1) = intDice(3) Then
                Send(Tables(tableID).Player1, String.Concat("CHINCHINTEXT ", oPlayerAccount.Player, " threw a triple load and won triple his bet!"))
                Send(Tables(tableID).Player2, String.Concat("CHINCHINTEXT ", oPlayerAccount.Player, " threw a triple load and won triple his bet!"))
                Call ChinHistory(Tables(tableID).GameID, oPlayerAccount.Player, String.Empty, String.Empty, String.Empty, "Storm/Triple Load - Won 3x Bet")
                Call chinchinisover(tableID, CByte(intPlayerID), 3)
                Exit Sub
            End If

            ' Well, no instant wins/losses, let's do scores now

            If (intDice(1) = intDice(2)) Then
                Call UpdateRecord("p" & intPlayerID & "score", intDice(3).ToString, Tables(tableID).GameID)
                Call SendPoints(tableID, intPlayerID, intDice(3))
                oGameData.Item("chin_p" & intPlayerID & "score") = intDice(3)

                Send(Tables(tableID).Player1, String.Concat("CHINCHINTEXT ", oPlayerAccount.Player, " threw a pair and scored ", intDice(3), " points!"))
                Send(Tables(tableID).Player2, String.Concat("CHINCHINTEXT ", oPlayerAccount.Player, " threw a pair and scored ", intDice(3), " points!"))
                Call ChinHistory(Tables(tableID).GameID, oPlayerAccount.Player, String.Empty, String.Empty, String.Empty, "Scored " & intDice(3) & " Points!")

                Call ChangeTurn(tableID, String.Empty, String.Empty, intPlayerID)
                Exit Sub
            ElseIf (intDice(1) = intDice(3)) Then
                Call UpdateRecord("p" & intPlayerID & "score", intDice(2).ToString, Tables(tableID).GameID)
                Call SendPoints(tableID, intPlayerID, intDice(2))
                oGameData.Item("chin_p" & intPlayerID & "score") = intDice(2)

                Send(Tables(tableID).Player1, String.Concat("CHINCHINTEXT ", oPlayerAccount.Player, " threw a pair and scored ", intDice(2), " points!"))
                Send(Tables(tableID).Player2, String.Concat("CHINCHINTEXT ", oPlayerAccount.Player, " threw a pair and scored ", intDice(2), " points!"))
                Call ChinHistory(Tables(tableID).GameID, oPlayerAccount.Player, String.Empty, String.Empty, String.Empty, "Scored " & intDice(2) & " Points!")

                Call ChangeTurn(tableID, String.Empty, String.Empty, intPlayerID)
                Exit Sub
            ElseIf (intDice(2) = intDice(3)) Then
                Call SendPoints(tableID, intPlayerID, intDice(1))
                Call UpdateRecord("p" & intPlayerID & "score", intDice(1).ToString, Tables(tableID).GameID)
                oGameData.Item("chin_p" & intPlayerID & "score") = intDice(1)

                Send(Tables(tableID).Player1, String.Concat("CHINCHINTEXT ", oPlayerAccount.Player, " threw a pair and scored ", intDice(1), " points!"))
                Send(Tables(tableID).Player2, String.Concat("CHINCHINTEXT ", oPlayerAccount.Player, " threw a pair and scored ", intDice(1), " points!"))
                Call ChinHistory(Tables(tableID).GameID, oPlayerAccount.Player, String.Empty, String.Empty, String.Empty, "Scored " & intDice(1) & " Points!")

                Call ChangeTurn(tableID, String.Empty, String.Empty, intPlayerID)
                Exit Sub
            End If

            ' Screw it, nothing.. check to see if they have more turns and if so, kick back another shot.

            Dim strScore1 As String = String.Empty, strScore2 As String = String.Empty
            If (oGameData.Item("chin_p1rolls").ToString = "3" Or oGameData.Item("chin_p1score").ToString <> "0") And (oGameData.Item("chin_p2rolls").ToString = "3" Or oGameData.Item("chin_p2score").ToString <> "0") Then
                ' the game is over

                strScore1 = oGameData.Item("chin_p1score").ToString
                strScore2 = oGameData.Item("chin_p2score").ToString

                If Val(strScore1) > Val(strScore2) Then
                    Send(Tables(tableID).Player1, String.Concat("CHINCHINTEXT ", oPlayerAccount.Player, " threw nothing!"))
                    Send(Tables(tableID).Player2, String.Concat("CHINCHINTEXT ", oPlayerAccount.Player, " threw nothing!"))

                    Call chinchinisover(tableID, 1, 1)
                ElseIf Val(strScore2) > Val(strScore1) Then
                    Send(Tables(tableID).Player1, String.Concat("CHINCHINTEXT ", oPlayerAccount.Player, " threw nothing!"))
                    Send(Tables(tableID).Player2, String.Concat("CHINCHINTEXT ", oPlayerAccount.Player, " threw nothing!"))
                    Call chinchinisover(tableID, 2, 1)
                Else
                    Send(Tables(tableID).Player1, String.Concat("CHINCHINTEXT ", oPlayerAccount.Player, " threw nothing!"))
                    Send(Tables(tableID).Player2, String.Concat("CHINCHINTEXT ", oPlayerAccount.Player, " threw nothing!"))

                    Call chinchinisover(tableID, 0, 1)
                End If
                Exit Sub
            End If

            If oPlayerAccount.Player = Tables(tableID).Player1 Then
                If oGameData.Item("chin_p1rolls").ToString <> "3" Then
                    Send(Tables(tableID).Player1, String.Concat("CHINCHINTEXT ", oPlayerAccount.Player, " rolled nothing."))
                    Send(Tables(tableID).Player2, String.Concat("CHINCHINTEXT ", oPlayerAccount.Player, " rolled nothing."))

                    Send(Tables(tableID).Player1, "CHINCHINTURN 1")
                    Send(Tables(tableID).Player2, "CHINCHINTURN 1")
                Else
                    If oGameData.Item("chin_p2rolls").ToString = "0" Then
                        Send(Tables(tableID).Player1, "CHINCHINTURN 2")
                        Send(Tables(tableID).Player2, "CHINCHINTURN 2")

                        Call ChinHistory(Tables(tableID).GameID, oPlayerAccount.Player, String.Empty, String.Empty, String.Empty, "Dead Roll")
                    Else
                        Call ChangeTurn(tableID, String.Empty, String.Empty, intPlayerID)
                        Exit Sub
                    End If
                End If
            ElseIf oPlayerAccount.Player = Tables(tableID).Player2 Then
                If oGameData.Item("chin_p2rolls").ToString <> "3" Then
                    Send(Tables(tableID).Player1, String.Concat("CHINCHINTEXT ", oPlayerAccount.Player, " rolled nothing."))
                    Send(Tables(tableID).Player2, String.Concat("CHINCHINTEXT ", oPlayerAccount.Player, " rolled nothing."))

                    Send(Tables(tableID).Player1, "CHINCHINTURN 2")
                    Send(Tables(tableID).Player2, "CHINCHINTURN 2")
                Else
                    If oGameData.Item("chin_p1rolls").ToString = "0" Then
                        ' oops out of turns, next player's turn
                        Send(Tables(tableID).Player1, "CHINCHINTURN 1")
                        Send(Tables(tableID).Player2, "CHINCHINTURN 1")

                        Call ChinHistory(Tables(tableID).GameID, oPlayerAccount.Player, String.Empty, String.Empty, String.Empty, "Dead Roll")
                    Else
                        Call ChangeTurn(tableID, String.Empty, String.Empty, intPlayerID)
                        Exit Sub
                    End If
                End If
            End If
        Catch ex As Exception
            Call errorsub(ex, "chinchinroll")
        End Try
    End Sub
    Private Sub SendPoints(ByVal tableID As Integer, ByVal iPlayerID As Integer, ByVal iValue As Integer)
        Select Case iPlayerID
            Case 1
                Send(tables(tableID).player1, String.Concat("CHINCHINPOINTS ", iPlayerID, " ", iValue))
                Send(tables(tableID).player2, String.Concat("CHINCHINPOINTS ", iPlayerID, " ", iValue))
            Case 2
                Send(tables(tableID).player1, String.Concat("CHINCHINPOINTS ", iPlayerID, " ", iValue))
                Send(tables(tableID).player2, String.Concat("CHINCHINPOINTS ", iPlayerID, " ", iValue))
        End Select
    End Sub
    Private Sub ChangeTurn(ByVal tableID As Integer, ByVal strScore1 As String, ByVal strScore2 As String, ByVal intPlayerID As Integer)
        Dim oGameData As DataRow = SelectGame(tables(tableID).gameid)

        If (oGameData.Item("chin_p1score").ToString <> "0" Or oGameData.Item("chin_p1rolls").ToString = "3") And (oGameData.Item("chin_p2score").ToString <> "0" Or oGameData.Item("chin_p2rolls").ToString = "3") Then
            strScore1 = oGameData.Item("chin_p1score").ToString
            strScore2 = oGameData.Item("chin_p2score").ToString

            If Val(strScore1) > Val(strScore2) Then
                Call chinchinisover(tableID, 1, 1)
            ElseIf Val(strScore2) > Val(strScore1) Then
                Call chinchinisover(tableID, 2, 1)
            Else
                Call chinchinisover(tableID, 0, 1)
            End If
        Else
            Select Case intPlayerID
                Case 1
                    Send(tables(tableID).player1, "CHINCHINTURN 2")
                    Send(tables(tableID).player2, "CHINCHINTURN 2")
                Case 2
                    Send(tables(tableID).player1, "CHINCHINTURN 1")
                    Send(tables(tableID).player2, "CHINCHINTURN 1")
            End Select
        End If
    End Sub

    Private Sub chinchinisover(ByVal tableID As Integer, ByVal intWinner As Byte, ByVal intWagerMultiple As Byte)
        Dim plr1nick, plr1score, plr2score, plr2nick As String
        Dim gameid As Integer
        Dim strWager As String = String.Empty, lostvar As String = String.Empty
        Dim oGameData As DataRow = SelectGame(tables(tableID).gameid)
        Dim oStats As New Statistics

        Try
            gameid = tables(tableID).gameid

            Divide(tables(tableID).rulelist, " ", lostvar, strWager)

            strWager = CStr(Val(strWager) * intWagerMultiple)

            plr1score = oGameData.Item("chin_p1score").ToString
            plr2score = oGameData.Item("chin_p2score").ToString

            plr1nick = oGameData.Item("chin_player1").ToString
            plr2nick = oGameData.Item("chin_player2").ToString

            Dim oPlayerAccount As New PlayerAccountDAL(plr1nick)
            Dim oPlayerAccount2 As New PlayerAccountDAL(plr2nick)

            Call UpdateRecord("wager", strWager, gameid)

            If intWinner = 0 Then
                plr1score = "0"
                plr2score = "0"

                Call UpdateRecord("p1score", 0, tables(tableID).gameid)
                Call UpdateRecord("p2score", 0, tables(tableID).gameid)

                Call sendall(String.Concat("MATCH ", plr1nick, " ", oPlayerAccount.ChinRank, " " & plr2nick, " ", oPlayerAccount2.ChinRank, " ", plr1score, " ", plr2score, " 6 ", gameid))

                oStats.CompileStats(plr1nick, plr2nick, "chinchin", True)
            ElseIf intWinner = 1 Then
                plr1score = "1"
                plr2score = "0"

                Call UpdateRecord("p1score", 1, tables(tableID).gameid)
                Call UpdateRecord("p2score", 0, tables(tableID).gameid)

                Call WagerRule(plr1nick, plr2nick, gameid)
                oStats.CompileStats(plr1nick, plr2nick, "chinchin", False)

                Call giveap(tables(tableID).player1, CInt(Int(Val(strWager) * 0.95)))
                Call blockchat(tables(tableID).player1, "Gamble", String.Concat("You won ", CInt(Int(Val(strWager) * 0.95)), "AP!"))
                Call giveap(tables(tableID).player2, CInt(strWager) * -1)
                Call blockchat(tables(tableID).player2, "Gamble", String.Concat("You lost ", CInt(strWager), "AP!"))

                Call playerrankings(plr1nick, plr2nick, "chinchin")
                Call sendall(String.Concat("MATCH ", plr1nick, " ", oPlayerAccount.ChinRank, " ", plr2nick, " ", oPlayerAccount2.ChinRank, " ", plr1score, " ", plr2score, " 6 ", gameid))
                'Call setServerStats("aptax", CStr(Int(Val(strWager) * 0.05)))
            ElseIf intWinner = 2 Then
                plr1score = "0"
                plr2score = "1"

                Call UpdateRecord("p1score", 0, tables(tableID).gameid)
                Call UpdateRecord("p2score", 1, tables(tableID).gameid)

                Call WagerRule(plr2nick, plr1nick, gameid)
                oStats.CompileStats(plr2nick, plr1nick, "chinchin", False)

                Call giveap(tables(tableID).player2, CInt(Int(Val(strWager) * 0.95)))
                Call blockchat(tables(tableID).player2, "Gamble", String.Concat("You won ", CInt(Int(Val(strWager) * 0.95)), "AP!"))
                Call giveap(tables(tableID).player1, CInt(strWager) * -1)
                Call blockchat(tables(tableID).player1, "Gamble", String.Concat("You lost ", CInt(strWager) & "AP!"))

                Call playerrankings(plr2nick, plr1nick, "chinchin")
                Call sendall(String.Concat("MATCH ", plr2nick, " ", oPlayerAccount2.ChinRank, " ", plr1nick, " ", oPlayerAccount.ChinRank, " ", plr2score, " ", plr1score, " 6 ", gameid))
                'Call setServerStats("aptax", CStr(Int(Val(strWager) * 0.05)))
            End If

            Dim oTables As New BusinessLayer.Tables
            oTables.ClosePlayerTable(Tables, plr1nick, False)

            Send(plr1nick, String.Concat("UPDATEAP ", oPlayerAccount.AP))
            Send(plr2nick, String.Concat("UPDATEAP ", oPlayerAccount2.AP))

            Send(plr1nick, "CHINCHINCLOSE")
            Send(plr2nick, "CHINCHINCLOSE")

            If Val(strWager) >= 5000 Then
                Dim oGameFunctions As New GameFunctions

                oGameFunctions.RushBoost(plr1nick)
                oGameFunctions.RushBoost(plr2nick)
            End If
        Catch ex As Exception
            Call errorsub(ex, "chinchinisover")
        End Try
    End Sub

    Public Function chinchinyes(ByVal incoming As String, ByVal socket As Integer) As String

        Dim opp As String = String.Empty, challengeyesnick As String = String.Empty, lostvar As String = String.Empty
        Dim gameid As Integer, oppsocket As Integer

        Try
            Divide(incoming, " ", lostvar, challengeyesnick)
            chinchinyes = String.Empty

            If challengeyesnick = String.Empty Then
                Call blockchat(frmSystray.Winsock(socket).Tag, "Error", "Server could not process your challenge")
                Exit Function
            End If

            opp = frmSystray.Winsock(socket).Tag
            oppsocket = GetSocket(challengeyesnick)

            'User accepted the challenge... damn it,
            'now i have to code the game core.... :)
            Send(String.Empty, "CHINCHINYES", oppsocket)

            Dim oGameFunctions As New GameFunctions
            gameid = oGameFunctions.NewGameID()

            Call InsertGame(gameid, challengeyesnick, opp, 0)

            'Dim oChatFunctions As New ChatFunctions
            'oChatFunctions.unisend("CHALLENGESTART", String.Concat(challengeyesnick, " ", frmSystray.Winsock(socket).Tag, " 6"))

            Dim gamestarter As Integer = oGameFunctions.PickStarter(False)

            Send(String.Empty, String.Concat("CHINCHINTURN ", gamestarter), oppsocket)
            Send(String.Empty, String.Concat("CHINCHINTURN ", gamestarter), socket)

            Return gameid.ToString
        Catch ex As Exception
            Call errorsub(ex, "chinchinyes")
            Return String.Empty
        End Try
    End Function
End Class
