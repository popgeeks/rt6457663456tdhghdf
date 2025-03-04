Public Class SphereFunctions
    Inherits SphereBreakDAL

    Public Sub GameModule(ByVal sCommand As String, ByVal incoming As String, ByVal socket As Integer)
        Select Case sCommand
            Case "SBREADY"
                Call sbready(socket)
            Case "SBCOMPLETE"
                Call sbcomplete(incoming, socket)
            Case "SBFROZEN"
                Call sbfrozen(socket)
        End Select
    End Sub

    Private Sub fetch_coregrid(ByVal gameid As Integer)
        Try
            Dim gamefile As String = My.Application.Info.DirectoryPath & "\gamefiles\" & gameid & ".tgf"

            For x As Integer = 1 To 4
                Send(Readini("Gameinfo", "Player1", gamefile), "SBCORE " & x & " " & Readini("Core", x.ToString, gamefile))
                Send(Readini("Gameinfo", "Player2", gamefile), "SBCORE " & x & " " & Readini("Core", x.ToString, gamefile))
            Next x
        Catch ex As Exception
            Call errorsub(ex, "fetch_coregrid")
        End Try
    End Sub

    Private Sub fetch_spheregrid(ByVal gameid As Integer, ByVal player As String)
        Try
            Dim gamefile As String = My.Application.Info.DirectoryPath & "\gamefiles\" & gameid & ".tgf"

            For x As Integer = 1 To 12
                If Val(Readini("Grid" & player, x.ToString, gamefile)) > 0 And Val(Readini("Hide" & player, x.ToString, gamefile)) = 0 Then
                    Send(Readini("Gameinfo", "Player" & player, gamefile), "SBBORDER " & x & " " & Readini("Grid" & player, x.ToString, gamefile))
                Else
                    Send(Readini("Gameinfo", "Player" & player, gamefile), "SBBORDER " & x & " 0")
                End If
            Next x
        Catch ex As Exception
            Call errorsub(ex, "fetch_spheregrid")
        End Try
    End Sub

    Private Sub increase_spheregrid(ByVal gameid As Integer, ByVal player As String)
        Try
            Dim spherevalue, hidevalue As Integer

            Dim gamefile As String = My.Application.Info.DirectoryPath & "\gamefiles\" & gameid & ".tgf"

            For x As Integer = 1 To 12
                spherevalue = Val(Readini("Grid" & player, x.ToString, gamefile)) + 1
                If spherevalue = 10 Then spherevalue = -2
                writeini("Grid" & player, x.ToString, CStr(spherevalue), gamefile)

                If Val(Readini("Hide" & player, x.ToString, gamefile)) < 0 Then
                    hidevalue = Val(Readini("Hide" & player, x.ToString, gamefile)) + 1
                    writeini("Hide" & player, x.ToString, CStr(hidevalue), gamefile)
                End If
            Next x
        Catch ex As Exception
            Call errorsub(ex, "increase_spheregrid")
        End Try
    End Sub

    Private Function fetch_gridvalue(ByVal gameid As String, ByVal strSpot As String, ByVal playerid As Integer) As Integer
        Try
            Return Val(Readini("Grid" & playerid, strSpot, My.Application.Info.DirectoryPath & "\gamefiles\" & gameid & ".tgf"))
        Catch ex As Exception
            Call errorsub(ex, "fetch_gridvalue")
        End Try
    End Function

    Private Function fetch_multivalue(ByVal gameid As String, ByVal playerid As Integer) As Integer
        Try
            Return Val(Readini("Player" & playerid, "Multiple", My.Application.Info.DirectoryPath & "\gamefiles\" & gameid & ".tgf"))
        Catch ex As Exception
            Call errorsub(ex, "fetch_multivalue")
        End Try
    End Function

    Private Sub increase_spherescore(ByVal gameid As String, ByVal playerid As Integer, ByVal intAmt As Integer)
        Try
            intAmt += Val(Readini("Score", "Player" & playerid, My.Application.Info.DirectoryPath & "\gamefiles\" & gameid & ".tgf"))
            writeini("Score", "Player" & playerid, intAmt.ToString, My.Application.Info.DirectoryPath & "\gamefiles\" & gameid & ".tgf")
        Catch ex As Exception
            Call errorsub(ex, "increase_spherescore")
        End Try
    End Sub

    Private Function fetch_turnvalue(ByVal gameid As String, ByVal playerid As Integer) As Integer
        Try
            Dim intTurns As Integer = Val(Readini("Player" & playerid, "Turns", My.Application.Info.DirectoryPath & "\gamefiles\" & gameid & ".tgf"))
            fetch_turnvalue = intTurns
        Catch ex As Exception
            Call errorsub(ex, "fetch_turnvalue")
        End Try
    End Function

    Private Function increase_turnvalue(ByVal gameid As String, ByVal playerid As Integer) As Integer
        Try
            Dim intTurns As Integer = 1 + Val(Readini("Player" & playerid, "Turns", My.Application.Info.DirectoryPath & "\gamefiles\" & gameid & ".tgf"))
            writeini("Player" & playerid, "Turns", CStr(intTurns), My.Application.Info.DirectoryPath & "\gamefiles\" & gameid & ".tgf")
            Return intTurns
        Catch ex As Exception
            Call errorsub(ex, "increase_turnvalue")
        End Try
    End Function

    Private Sub fetch_playerscore(ByVal gameid As String, ByVal playerid As Integer)
        Try
            Send(Readini("Gameinfo", "Player" & playerid, My.Application.Info.DirectoryPath & "\gamefiles\" & gameid & ".tgf"), "SBSCORE " & Readini("Score", "Player" & playerid, My.Application.Info.DirectoryPath & "\gamefiles\" & gameid & ".tgf"))
        Catch ex As Exception
            Call errorsub(ex, "fetch_playerscore")
        End Try
    End Sub

    Private Sub spheregameover(ByVal gameid As String, ByVal winnerID As Integer)
        Try
            Dim gprate As Integer
            Dim oStats As New Statistics
            Dim oGameFunctions As New GameFunctions
            Dim oGameDAL As New GameFunctionsDAL

            If Readini("Gameinfo", "Over", My.Application.Info.DirectoryPath & "\gamefiles\" & gameid & ".tgf") = "1" Then Exit Sub

            Dim plr1score As Integer = Val(Readini("Score", "Player1", My.Application.Info.DirectoryPath & "\gamefiles\" & gameid & ".tgf"))
            Dim plr2score As Integer = Val(Readini("Score", "Player2", My.Application.Info.DirectoryPath & "\gamefiles\" & gameid & ".tgf"))

            Dim plr1nick As String = Readini("Gameinfo", "Player1", My.Application.Info.DirectoryPath & "\gamefiles\" & gameid & ".tgf")
            Dim plr2nick As String = Readini("Gameinfo", "Player2", My.Application.Info.DirectoryPath & "\gamefiles\" & gameid & ".tgf")

            writeini("player", "match", DateString, My.Application.Info.DirectoryPath & "\accounts\" & plr1nick & ".ttd")
            writeini("player", "match", DateString, My.Application.Info.DirectoryPath & "\accounts\" & plr2nick & ".ttd")

            writeini("Gameinfo", "Over", "1", My.Application.Info.DirectoryPath & "\gamefiles\" & gameid & ".tgf")

            If plr1score > plr2score Then
                'Player 1 won
                writeini("Gameinfo", "Winner", Readini("Gameinfo", "Player1", My.Application.Info.DirectoryPath & "\gamefiles\" & gameid & ".tgf"), My.Application.Info.DirectoryPath & "\gamefiles\" & gameid & ".tgf")

                oGameDAL.RankChange(plr1nick, plr2nick, "SB")
                Call laddersub(plr1nick, plr2nick, plr1score.ToString, plr2score.ToString, CInt(gameid), False, False, "sb")

                If oServerConfig.LossRate > 0 Then
                    gprate = EndGameGP(plr2nick, -1)

                    gprate = Int(gprate / 2)
                    Call gpadd(plr2nick, gprate, True)
                End If

                gprate = EndGameGP(plr1nick, 1)
                gprate = Int(gprate / 2)

                Call gpadd(plr1nick, gprate, True)
                Call oGameFunctions.GiveExp(plr1nick, plr1score, plr2nick, plr2score, CInt(gameid), Nothing, False, "sb")

                Call WagerRule(plr1nick, plr2nick, gameid)
                oStats.CompileStats(plr1nick, plr2nick, "sb", False)
            ElseIf plr1score < plr2score Then
                'Player 2 won

                writeini("Gameinfo", "Winner", Readini("Gameinfo", "Player2", My.Application.Info.DirectoryPath & "\gamefiles\" & gameid & ".tgf"), My.Application.Info.DirectoryPath & "\gamefiles\" & gameid & ".tgf")

                If oServerConfig.LossRate > 0 Then
                    gprate = EndGameGP(plr1nick, -1)
                    gprate = Int(gprate / 2)

                    Call gpadd(plr1nick, gprate, True)
                End If

                gprate = EndGameGP(plr2nick, 1)
                gprate = Int(gprate / 2)

                Call gpadd(plr2nick, gprate, True)
                oGameDAL.RankChange(plr2nick, plr1nick, "SB")
                Call laddersub(plr2nick, plr1nick, plr2score.ToString, plr1score.ToString, CInt(gameid), False, False, "sb")
                Call oGameFunctions.GiveExp(plr2nick, plr2score, plr1nick, plr1score, CInt(gameid), Nothing, False, "sb")

                Call WagerRule(plr2nick, plr1nick, gameid)
                oStats.CompileStats(plr2nick, plr1nick, "sb", False)
            ElseIf plr1score = plr2score Then
                Call oGameFunctions.drawevent(plr1nick, plr2nick, CInt(gameid), False, "sb")
                oStats.CompileStats(plr1nick, plr2nick, "sb", True)
            End If

            Dim oTables As New BusinessLayer.Tables
            Dim iTableID As Integer = oTables.FindPlayersTable(Tables, plr1nick)
            If plr1score + plr2score >= 20 Then oGameFunctions.EndGameEvent(Tables(iTableID))

            otables.ClosePlayerTable(Tables, plr1nick, False)

            writeini("Ready", "1", "false", My.Application.Info.DirectoryPath & "\gamefiles\" & gameid & ".tgf")
            writeini("Ready", "2", "false", My.Application.Info.DirectoryPath & "\gamefiles\" & gameid & ".tgf")

            Send(plr1nick, "SBCLOSE")
            Send(plr2nick, "SBCLOSE")
            InsertGameFile(Val(gameid))
        Catch ex As Exception
            Call errorsub(ex, "spheregameover")
        End Try
    End Sub

    Public Function spherechallenge(ByVal incoming As String, ByVal socket As Integer) As String
        Dim challengeyesnick As String = String.Empty, lostvar As String = String.Empty
        Dim gameid, oppsocket As Integer
        Dim strScore As String = String.Empty, strTime As String = String.Empty, strTurns As String = String.Empty

        Divide(incoming, " ", challengeyesnick, lostvar, strTime, strScore, strTurns)

        spherechallenge = String.Empty

        If challengeyesnick = String.Empty Then
            Call blockchat(frmSystray.Winsock(socket).Tag, "Cancelled", "Your challenge was cancelled by the server.  Please try again later.")
            Exit Function
        End If

        'opp = frmSystray.Winsock(socket).Tag
        oppsocket = GetSocket(challengeyesnick)

        'User accepted the challenge... damn it,
        'now i have to code the game core.... :)

        'Send challengeyesnick, "CHALLENGEYES " & frmSystray.Winsock(socket).Tag
        'DoEvents

        Send(challengeyesnick, "GAMEOPPONENT " & frmSystray.Winsock(socket).Tag)
        Send(frmSystray.Winsock(socket).Tag, "GAMEOPPONENT " & challengeyesnick)

        'Tell both players to pick they cards

        writeini("Play", "Playing", "true", My.Application.Info.DirectoryPath & "\accounts\" & frmSystray.Winsock(socket).Tag & ".ttd")
        writeini("Play", "Playing", "true", My.Application.Info.DirectoryPath & "\accounts\" & challengeyesnick & ".ttd")

        Dim oGameFunctions As New GameFunctions
        gameid = oGameFunctions.NewGameID

        'Write GameID in both player file
        writeini("Play", "GameID", gameid.ToString, My.Application.Info.DirectoryPath & "\accounts\" & frmSystray.Winsock(socket).Tag & ".ttd")
        writeini("Play", "GameID", gameid.ToString, My.Application.Info.DirectoryPath & "\accounts\" & challengeyesnick & ".ttd")

        writeini("Gameinfo", "TT", "1", My.Application.Info.DirectoryPath & "\gamefiles\" & gameid & ".tgf")

        writeini("Games", "Playing", "SB", My.Application.Info.DirectoryPath & "\accounts\" & frmSystray.Winsock(socket).Tag & ".ttd")
        writeini("Games", "Playing", "SB", My.Application.Info.DirectoryPath & "\accounts\" & challengeyesnick & ".ttd")

        'Write the opponent nick in both player file
        writeini("Play", "Versus", challengeyesnick, My.Application.Info.DirectoryPath & "\accounts\" & frmSystray.Winsock(socket).Tag & ".ttd")
        writeini("Play", "Versus", frmSystray.Winsock(socket).Tag, My.Application.Info.DirectoryPath & "\accounts\" & challengeyesnick & ".ttd")

        'Write the date in the Game file
        writeini("Gameinfo", "Date", CStr(Today), My.Application.Info.DirectoryPath & "\gamefiles\" & gameid & ".tgf")
        'Define in both player and game file who is
        'player 1 and who is player 2

        writeini("Gameinfo", "Player1", challengeyesnick, My.Application.Info.DirectoryPath & "\gamefiles\" & gameid & ".tgf")
        writeini("Play", "Iam", "1", My.Application.Info.DirectoryPath & "\accounts\" & challengeyesnick & ".ttd")
        writeini("Gameinfo", "Player2", frmSystray.Winsock(socket).Tag, My.Application.Info.DirectoryPath & "\gamefiles\" & gameid & ".tgf")
        writeini("Play", "Iam", "2", My.Application.Info.DirectoryPath & "\accounts\" & frmSystray.Winsock(socket).Tag & ".ttd")

        'writeini "Stats", "Games", CStr(Val(Readini("Stats", "Games", App.Path & "\stats.ini")) + 1), App.Path & "\stats.ini"

        'Write Core Grid
        Dim x, y As Byte

        For x = 1 To 4
            Randomize()
            writeini("Core", x.ToString, CStr(Int(Rnd() * 6) + 1), My.Application.Info.DirectoryPath & "\gamefiles\" & gameid & ".tgf")
        Next x

        'Write Border Grid
        For y = 1 To 2
            For x = 1 To 16
                Randomize()
                writeini("Grid" & y, x.ToString, CStr(Int(Rnd() * 9) + 1), My.Application.Info.DirectoryPath & "\gamefiles\" & gameid & ".tgf")
            Next x
        Next y

        'Write players score (5-5) in player file
        writeini("Score", "Player1", "0", My.Application.Info.DirectoryPath & "\gamefiles\" & gameid & ".tgf")
        writeini("Score", "Player2", "0", My.Application.Info.DirectoryPath & "\gamefiles\" & gameid & ".tgf")

        writeini("Data", "Time", strTime, My.Application.Info.DirectoryPath & "\gamefiles\" & gameid & ".tgf")
        writeini("Data", "Turns", strTurns, My.Application.Info.DirectoryPath & "\gamefiles\" & gameid & ".tgf")
        writeini("Data", "Score", strScore, My.Application.Info.DirectoryPath & "\gamefiles\" & gameid & ".tgf")

        spherechallenge = gameid.ToString

        'Dim oChatFunctions As New ChatFunctions
        'oChatFunctions.unisend("CHALLENGESTART", challengeyesnick & " " & frmSystray.Winsock(socket).Tag & " 3")

        Send(String.Empty, "SBSTART", socket)
        Send(String.Empty, "SBSTART", oppsocket)
    End Function

    Private Sub sbready(ByVal socket As Integer)
        Dim readyplayer As String = String.Empty
        Dim z, gameid As Integer

        readyplayer = frmSystray.Winsock(socket).Tag
        gameid = Val(Readini("Play", "GameID", My.Application.Info.DirectoryPath & "\accounts\" & readyplayer & ".ttd"))

        If Readini("Gameinfo", "Player1", My.Application.Info.DirectoryPath & "\gamefiles\" & gameid & ".tgf") = readyplayer Then
            writeini("Ready", "1", "true", My.Application.Info.DirectoryPath & "\gamefiles\" & gameid & ".tgf")
            'If both players are ready, define who starts first and notify both players
            If Readini("Ready", "2", My.Application.Info.DirectoryPath & "\gamefiles\" & gameid & ".tgf") = "true" Then GoTo start
        Else
            writeini("Ready", "2", "true", My.Application.Info.DirectoryPath & "\gamefiles\" & gameid & ".tgf")
            'If both players are ready, define who starts first and notify both players
            If Readini("Ready", "1", My.Application.Info.DirectoryPath & "\gamefiles\" & gameid & ".tgf") = "true" Then GoTo start
        End If

        Exit Sub

start:

        Call fetch_coregrid(gameid)
        Call fetch_spheregrid(gameid, "1")
        Call fetch_spheregrid(gameid, "2")

        Randomize()

        z = Int(Rnd() * 9) + 1

        Send(Readini("Gameinfo", "Player1", My.Application.Info.DirectoryPath & "\gamefiles\" & gameid & ".tgf"), "SBMULTI " & z)
        Send(Readini("Gameinfo", "Player2", My.Application.Info.DirectoryPath & "\gamefiles\" & gameid & ".tgf"), "SBMULTI " & z)

        writeini("Player1", "Multiple", CStr(z), My.Application.Info.DirectoryPath & "\gamefiles\" & gameid & ".tgf")
        writeini("Player2", "Multiple", CStr(z), My.Application.Info.DirectoryPath & "\gamefiles\" & gameid & ".tgf")

        writeini("Player1", "Turns", "1", My.Application.Info.DirectoryPath & "\gamefiles\" & gameid & ".tgf")
        writeini("Player2", "Turns", "1", My.Application.Info.DirectoryPath & "\gamefiles\" & gameid & ".tgf")

        Send(Readini("Gameinfo", "Player1", My.Application.Info.DirectoryPath & "\gamefiles\" & gameid & ".tgf"), "SBTURNS " & fetch_turnvalue(CStr(gameid), 1))
        Send(Readini("Gameinfo", "Player2", My.Application.Info.DirectoryPath & "\gamefiles\" & gameid & ".tgf"), "SBTURNS " & fetch_turnvalue(CStr(gameid), 2))

        Send(Readini("Gameinfo", "Player1", My.Application.Info.DirectoryPath & "\gamefiles\" & gameid & ".tgf"), "SBGO " & Readini("Data", "Time", My.Application.Info.DirectoryPath & "\gamefiles\" & gameid & ".tgf"))
        Send(Readini("Gameinfo", "Player2", My.Application.Info.DirectoryPath & "\gamefiles\" & gameid & ".tgf"), "SBGO " & Readini("Data", "Time", My.Application.Info.DirectoryPath & "\gamefiles\" & gameid & ".tgf"))
    End Sub

    Private Sub sbcomplete(ByVal incoming As String, ByVal socket As Integer)
        Try

            Dim var(20) As String, gameid As String = String.Empty, gamefile As String = String.Empty
            Dim intSum, intMulti, z, intBonus, playerID As Integer
            Dim lostvar As String = String.Empty, strOpponent As String = String.Empty
            Dim dblMultiple As Double

            Divide(incoming, " ", lostvar, var(1), var(2), var(3), var(4), var(5), var(6), var(7), var(8), var(9), var(10), var(11), var(12), var(13), var(14), var(15), var(16), var(17), var(18), var(19), var(20))

            gameid = Readini("Play", "GameID", My.Application.Info.DirectoryPath & "\accounts\" & frmSystray.Winsock(socket).Tag & ".ttd")
            gamefile = My.Application.Info.DirectoryPath & "\gamefiles\" & gameid & ".tgf"

            If Readini("Gameinfo", "Player1", gamefile) = frmSystray.Winsock(socket).Tag Then
                playerID = 1
                strOpponent = Readini("Gameinfo", "Player2", gamefile)
            ElseIf Readini("Gameinfo", "Player2", gamefile) = frmSystray.Winsock(socket).Tag Then
                playerID = 2
                strOpponent = Readini("Gameinfo", "Player1", gamefile)
            Else
                Exit Sub
            End If

            Dim x As Integer

            intSum = 0

            For x = 1 To 20
                If var(x) = String.Empty Then Exit For

                If var(x) <> "99" Then
                    If x < 5 Then
                        intSum = intSum + Val(Readini("Core", var(x), My.Application.Info.DirectoryPath & "\gamefiles\" & gameid & ".tgf"))
                    End If

                    If x >= 5 Then
                        intSum = intSum + fetch_gridvalue(gameid, var(x), Val(playerID))
                        writeini("Hide" & playerID, var(x), "-3", My.Application.Info.DirectoryPath & "\gamefiles\" & gameid & ".tgf")
                    End If
                End If
            Next x

            intMulti = Int(fetch_multivalue(gameid, Val(playerID)))

            If intSum Mod intMulti <> 0 Then
                Dim oChatFunctions As New ChatFunctions
                oChatFunctions.uniwarn(frmSystray.Winsock(socket).Tag & " tried an invalid combonation in sphere break")
                Send(String.Empty, "EXIT You were terminated from the server for completing an invalid combonation in sphere break.", socket)
                Exit Sub
            End If

            Call increase_spherescore(gameid, Val(playerID), x - 5)

            ' Check for multiple echo bonus
            dblMultiple = intSum / intMulti

            If (x - 5) <> 0 Then
                If CStr(dblMultiple) = Readini("Bonus" & playerID, "Multiple", My.Application.Info.DirectoryPath & "\gamefiles\" & gameid & ".tgf") Then
                    intBonus = Val(Readini("Bonus" & playerID, "MultiBonus", My.Application.Info.DirectoryPath & "\gamefiles\" & gameid & ".tgf")) + 1
                    writeini("Bonus" & playerID, "MultiBonus", CStr(intBonus), My.Application.Info.DirectoryPath & "\gamefiles\" & gameid & ".tgf")
                    Send(String.Empty, "SBECHO " & intBonus, socket)
                    Call increase_spherescore(gameid, Val(playerID), intBonus)
                Else
                    writeini("Bonus" & playerID, "MultiBonus", "0", My.Application.Info.DirectoryPath & "\gamefiles\" & gameid & ".tgf")
                End If
            Else
                writeini("Bonus" & playerID, "MultiBonus", "0", My.Application.Info.DirectoryPath & "\gamefiles\" & gameid & ".tgf")
            End If
            ' Check for sum echo bonus - Core coin only echos do not apply

            If (x - 5) <> 0 Then
                If CStr(x - 5) = Readini("Bonus" & playerID, "Sum", My.Application.Info.DirectoryPath & "\gamefiles\" & gameid & ".tgf") Then
                    intBonus = Val(Readini("Bonus" & playerID, "SumBonus", My.Application.Info.DirectoryPath & "\gamefiles\" & gameid & ".tgf")) + 1
                    writeini("Bonus" & playerID, "SumBonus", CStr(intBonus), My.Application.Info.DirectoryPath & "\gamefiles\" & gameid & ".tgf")
                    Send(String.Empty, "SBECHO " & intBonus, socket)
                    Call increase_spherescore(gameid, Val(playerID), intBonus)
                Else
                    writeini("Bonus" & playerID, "SumBonus", "0", My.Application.Info.DirectoryPath & "\gamefiles\" & gameid & ".tgf")
                End If
            Else
                writeini("Bonus" & playerID, "SumBonus", "0", My.Application.Info.DirectoryPath & "\gamefiles\" & gameid & ".tgf")
                writeini("Bonus" & playerID, "Sum", "0", My.Application.Info.DirectoryPath & "\gamefiles\" & gameid & ".tgf")
            End If

            writeini("Bonus" & playerID, "Multiple", CStr(dblMultiple), My.Application.Info.DirectoryPath & "\gamefiles\" & gameid & ".tgf")
            writeini("Bonus" & playerID, "Sum", CStr(x - 5), My.Application.Info.DirectoryPath & "\gamefiles\" & gameid & ".tgf")

            If Val(Readini("Score", "Player" & playerID, My.Application.Info.DirectoryPath & "\gamefiles\" & gameid & ".tgf")) >= CDbl(Readini("Data", "Score", My.Application.Info.DirectoryPath & "\gamefiles\" & gameid & ".tgf")) Then
                Call spheregameover(gameid, Val(playerID))
                Exit Sub
            End If

            If increase_turnvalue(gameid, Val(playerID)) >= CDbl(Readini("Data", "Turns", My.Application.Info.DirectoryPath & "\gamefiles\" & gameid & ".tgf")) Then
                ' Player is frozen
                Send(String.Empty, "SBTURNS " & fetch_turnvalue(CStr(gameid), Val(playerID)), socket)
                Send(String.Empty, "SBFREEZE", socket)
                Call sbfrozen(socket)
                Exit Sub
            End If

            Call increase_spheregrid(CInt(CStr(gameid)), CStr(playerID))
            Call fetch_spheregrid(CInt(CStr(gameid)), CStr(playerID))
            Call fetch_playerscore(CStr(gameid), Val(playerID))
            Send(String.Empty, "SBTURNS " & fetch_turnvalue(CStr(gameid), Val(playerID)), socket)

            ' Change the multiple
            Randomize()

            z = Int(Rnd() * 9) + 1
            Send(Readini("Gameinfo", "Player" & playerID, My.Application.Info.DirectoryPath & "\gamefiles\" & gameid & ".tgf"), "SBMULTI " & z)
            writeini("Player" & playerID, "Multiple", CStr(z), My.Application.Info.DirectoryPath & "\gamefiles\" & gameid & ".tgf")

            Select Case playerID
                Case 1
                    Send(strOpponent, "SBOPPSCORE " & Readini("Score", "Player1", gamefile))
                Case 2
                    Send(strOpponent, "SBOPPSCORE " & Readini("Score", "Player2", gamefile))
            End Select

            ' Next Round
            Send(Readini("Gameinfo", "Player" & playerID, My.Application.Info.DirectoryPath & "\gamefiles\" & gameid & ".tgf"), "SBGO " & Readini("Data", "Time", My.Application.Info.DirectoryPath & "\gamefiles\" & gameid & ".tgf"))
        Catch ex As Exception
            Call errorsub(ex, "sbcomplete")
        End Try
    End Sub

    Public Function GamePlrNumber(ByRef strNick As String, ByRef gameid As Integer) As Integer

        If Readini("Gameinfo", "Player1", My.Application.Info.DirectoryPath & "\gamefiles\" & gameid & ".tgf") = strNick Then
            GamePlrNumber = 1
        ElseIf Readini("Gameinfo", "Player2", My.Application.Info.DirectoryPath & "\gamefiles\" & gameid & ".tgf") = strNick Then
            GamePlrNumber = 2
        ElseIf Readini("Gameinfo", "Player3", My.Application.Info.DirectoryPath & "\gamefiles\" & gameid & ".tgf") = strNick Then
            GamePlrNumber = 3
        ElseIf Readini("Gameinfo", "Player4", My.Application.Info.DirectoryPath & "\gamefiles\" & gameid & ".tgf") = strNick Then
            GamePlrNumber = 4
        Else
            GamePlrNumber = 0
        End If
    End Function

    Private Sub sbfrozen(ByVal socket As Integer)
        Dim gamefile As String = String.Empty, strOpponent As String = String.Empty
        Dim playerid, ThisPlrId, tableID As Integer
        Dim oTables As New BusinessLayer.Tables

        tableID = oTables.findplayerstable(tables, frmSystray.Winsock(socket).Tag)

        If tableID = 0 Then Exit Sub

        ThisPlrId = GamePlrNumber(frmSystray.Winsock(socket).Tag, tables(tableID).gameid)

        If ThisPlrId = 0 Then Exit Sub

        gamefile = My.Application.Info.DirectoryPath & "\gamefiles\" & tables(tableID).gameid & ".tgf"

        If Readini("Gameinfo", "Player1", gamefile) = frmSystray.Winsock(socket).Tag Then
            playerid = 1
            Send(tables(tableID).player2, "SBOPPFROZEN")
            'strOpponent = Readini("Gameinfo", "Player2", gamefile)
        ElseIf Readini("Gameinfo", "Player2", gamefile) = frmSystray.Winsock(socket).Tag Then
            playerid = 2
            Send(tables(tableID).player1, "SBOPPFROZEN")
            'strOpponent = Readini("Gameinfo", "Player1", gamefile)
        Else
            Exit Sub
        End If

        writeini("Frozen", "Player" & playerid, "1", gamefile)

        Select Case playerid
            Case 1

            Case 2
                'Send strOpponent, "SBOPPFROZEN"
        End Select

        If Readini("Frozen", "Player1", gamefile) = "1" And Readini("Frozen", "Player2", gamefile) = "1" Then
            Call spheregameover(CStr(tables(tableID).gameid), 1)
        End If

    End Sub
End Class
