Public Class TeamOTTFunctions
    Inherits TeamOTTDAL

    Public Sub GameModule(ByVal sCommand As String, ByVal incoming As String, ByVal socket As Integer)
        Select Case sCommand
            Case "TEAM_OTTPLACEAT"
                Call team_ottplaceat(incoming, socket)
            Case "TEAM_OTTCARDDISCARD"
                Call team_ottdiscard(socket)
            Case "TEAM_CHANGERULE"
                Call team_changerule(incoming, socket)
            Case "TEAM_CHANGE"
                Call team_change(socket)
            Case "TEAM_GAMECHAT"
                Call team_gamechat(incoming, socket)
            Case "TEAM_READY"
                Call teamready(socket)
            Case "TEAM_CLOSE"
                Call teamclose(socket)
        End Select
    End Sub

    Public Sub challengechat(ByVal incoming As String, ByVal socket As Integer)
        Dim strHandle As String = String.empty, tableID As String = String.empty, strChat As String = String.empty, strPlayer As String = String.empty
        Dim oTables As New BusinessLayer.Tables

        tableID = oTables.findplayerstable(tables, frmSystray.Winsock(socket).Tag).ToString

        If CDbl(tableID) = 0 Then Exit Sub

        'Divide incoming, " ", lostvar, opponent
        strChat = Mid(incoming, 15, Len(incoming))
        Divide(strChat, " ", strHandle)

        If frmSystray.Winsock(socket).Tag = tables(CInt(tableID)).player1 Then
            Select Case strHandle
                Case "/kick"
                    Divide(strChat, " ", strHandle, strPlayer)

                    Select Case strPlayer
                        Case "2"
                            If tables(CInt(tableID)).player2 = String.empty Then Exit Sub
                            Call blockchat(tables(CInt(tableID)).player2, "Kicked", "You have been kicked by the table owner")

                            Send(tables(CInt(tableID)).player1, "CHALLENGECHAT *** " & tables(CInt(tableID)).player2 & " has been kicked")
                            Send(tables(CInt(tableID)).player3, "CHALLENGECHAT *** " & tables(CInt(tableID)).player2 & " has been kicked")
                            Send(tables(CInt(tableID)).player4, "CHALLENGECHAT *** " & tables(CInt(tableID)).player2 & " has been kicked")
                            Call teamclose(GetSocket(tables(CInt(tableID)).player2))
                        Case "3"
                            If tables(CInt(tableID)).player3 = String.empty Then Exit Sub
                            Call blockchat(tables(CInt(tableID)).player3, "Kicked", "You have been kicked by the table owner")

                            Send(tables(CInt(tableID)).player1, "CHALLENGECHAT *** " & tables(CInt(tableID)).player3 & " has been kicked")
                            Send(tables(CInt(tableID)).player2, "CHALLENGECHAT *** " & tables(CInt(tableID)).player3 & " has been kicked")
                            Send(tables(CInt(tableID)).player4, "CHALLENGECHAT *** " & tables(CInt(tableID)).player3 & " has been kicked")
                            Call teamclose(GetSocket(tables(CInt(tableID)).player3))
                        Case "4"
                            If tables(CInt(tableID)).player4 = String.empty Then Exit Sub
                            Call blockchat(tables(CInt(tableID)).player4, "Kicked", "You have been kicked by the table owner")

                            Send(tables(CInt(tableID)).player1, "CHALLENGECHAT *** " & tables(CInt(tableID)).player4 & " has been kicked")
                            Send(tables(CInt(tableID)).player2, "CHALLENGECHAT *** " & tables(CInt(tableID)).player4 & " has been kicked")
                            Send(tables(CInt(tableID)).player3, "CHALLENGECHAT *** " & tables(CInt(tableID)).player4 & " has been kicked")
                            Call teamclose(GetSocket(tables(CInt(tableID)).player4))
                    End Select

                    Exit Sub
            End Select
        End If

        If tables(CInt(tableID)).player1 <> String.Empty Then Send(tables(CInt(tableID)).player1, "CHALLENGECHAT " & frmSystray.Winsock(socket).Tag & " " & strChat)
        If tables(CInt(tableID)).player2 <> String.Empty Then Send(tables(CInt(tableID)).player2, "CHALLENGECHAT " & frmSystray.Winsock(socket).Tag & " " & strChat)
        If tables(CInt(tableID)).player3 <> String.Empty Then Send(tables(CInt(tableID)).player3, "CHALLENGECHAT " & frmSystray.Winsock(socket).Tag & " " & strChat)
        If tables(CInt(tableID)).player4 <> String.Empty Then Send(tables(CInt(tableID)).player4, "CHALLENGECHAT " & frmSystray.Winsock(socket).Tag & " " & strChat)
    End Sub

    Public Sub teamclose(ByVal socket As Integer)
        Dim tableID As String
        Dim x, intPlayer As Integer
        Dim strPlayers(4) As String
        Dim oTables As New BusinessLayer.Tables

        tableID = oTables.findplayerstable(tables, frmSystray.Winsock(socket).Tag).ToString

        If tableID = "0" Then Exit Sub

        strPlayers(1) = frmSystray.Winsock(socket).Tag
        strPlayers(2) = tables(CInt(tableID)).player2
        strPlayers(3) = tables(CInt(tableID)).player3
        strPlayers(4) = tables(CInt(tableID)).player4

        If tables(CInt(tableID)).player1 = frmSystray.Winsock(socket).Tag Then
            tables(CInt(tableID)).player_ready(1) = False

            oTables.ClosePlayerTable(Tables, frmSystray.Winsock(socket).Tag, False)
            oTables = Nothing

            tables(CInt(tableID)).player1 = String.empty

            intPlayer = 1
        ElseIf tables(CInt(tableID)).player2 = frmSystray.Winsock(socket).Tag Then
            tables(CInt(tableID)).player_ready(2) = False
            tables(CInt(tableID)).player2 = String.empty

            intPlayer = 2
        ElseIf tables(CInt(tableID)).player3 = frmSystray.Winsock(socket).Tag Then
            tables(CInt(tableID)).player_ready(3) = False
            tables(CInt(tableID)).player3 = String.empty

            intPlayer = 3
        ElseIf tables(CInt(tableID)).player4 = frmSystray.Winsock(socket).Tag Then
            tables(CInt(tableID)).player_ready(4) = False
            tables(CInt(tableID)).player4 = String.empty

            intPlayer = 4
        End If

        ' Update the challenge boxes
        If strPlayers(1) <> String.empty And intPlayer <> 1 Then
            Send(tables(CInt(tableID)).player1, String.concat("CHALLENGEREMOVE ", intPlayer))
        End If

        If strPlayers(2) <> String.empty And intPlayer <> 2 Then
            Send(tables(CInt(tableID)).player2, String.concat("CHALLENGEREMOVE ", intPlayer))
        End If

        If strPlayers(3) <> String.empty And intPlayer <> 3 Then
            Send(tables(CInt(tableID)).player3, String.Concat("CHALLENGEREMOVE ", intPlayer))
        End If

        If strPlayers(4) <> String.Empty And intPlayer <> 4 Then
            Send(tables(CInt(tableID)).player4, String.Concat("CHALLENGEREMOVE ", intPlayer))
        End If

        If intPlayer = 1 Then
            For x = 1 To 4
                Send(strPlayers(x), "CHALLENGEREMOVE 5")
            Next x
        Else
            Send(String.empty, "CHALLENGEREMOVE 5", socket)
        End If
    End Sub

    Private Sub teamready(ByVal socket As Integer)
        Dim x, intStatus, intPlayer, intCards As Integer
        Dim blnStart As Boolean

        Dim oTables As New BusinessLayer.Tables

        Dim tableID As Integer = oTables.findplayerstable(tables, frmSystray.Winsock(socket).Tag)

        If tableID = 0 Then Exit Sub
        If tables(tableID).gameid <> 0 Then Exit Sub

        If tables(tableID).rulelist = "OMNITT_PLAYER" Then
            intCards = totalcardsnum(frmSystray.Winsock(socket).Tag)
        End If

        If tables(tableID).player1 = frmSystray.Winsock(socket).Tag Then
            If tables(tableID).player_ready(1) = True Then
                tables(tableID).player_ready(1) = False
                intStatus = 0
            Else
                If intCards >= 15 Or tables(tableID).rulelist = "OMNITT_RANDOM" Then
                    tables(tableID).player_ready(1) = True
                    intStatus = 1
                Else
                    GoTo skip
                End If
            End If

            intPlayer = 1
        ElseIf tables(tableID).player2 = frmSystray.Winsock(socket).Tag Then
            If tables(tableID).player_ready(2) = True Then
                tables(tableID).player_ready(2) = False
                intStatus = 0
            Else
                If intCards >= 15 Or tables(tableID).rulelist = "OMNITT_RANDOM" Then
                    tables(tableID).player_ready(2) = True
                    intStatus = 1
                Else
                    GoTo skip
                End If
            End If

            intPlayer = 2
        ElseIf tables(tableID).player3 = frmSystray.Winsock(socket).Tag Then
            If tables(tableID).player_ready(3) = True Then
                tables(tableID).player_ready(3) = False
                intStatus = 0
            Else
                If intCards >= 15 Or tables(tableID).rulelist = "OMNITT_RANDOM" Then
                    tables(tableID).player_ready(3) = True
                    intStatus = 1
                Else
                    GoTo skip
                End If
            End If

            intPlayer = 3
        ElseIf tables(tableID).player4 = frmSystray.Winsock(socket).Tag Then
            If tables(tableID).player_ready(4) = True Then
                tables(tableID).player_ready(4) = False
                intStatus = 0
            Else
                If intCards >= 15 Or tables(tableID).rulelist = "OMNITT_RANDOM" Then
                    tables(tableID).player_ready(4) = True
                    intStatus = 1
                Else
                    GoTo skip
                End If
            End If

            intPlayer = 4
        End If


        ' Update the challenge boxes
        If tables(tableID).player1 <> "" Then
            Send(tables(tableID).player1, "CHALLENGEREADY " & intPlayer & " " & intStatus)
        End If

        If tables(tableID).player2 <> "" Then
            Send(tables(tableID).player2, "CHALLENGEREADY " & intPlayer & " " & intStatus)
        End If

        If tables(tableID).player3 <> "" Then
            Send(tables(tableID).player3, "CHALLENGEREADY " & intPlayer & " " & intStatus)
        End If

        If tables(tableID).player4 <> "" Then
            Send(tables(tableID).player4, "CHALLENGEREADY " & intPlayer & " " & intStatus)
        End If

        blnStart = True

        For x = 1 To 4
            If tables(tableID).player_ready(x) = False Then
                blnStart = False
            End If
        Next x

        If blnStart = True Then
            Call teamomnichallengeyes(tableID)
        End If

        oTables.UpdateTable(Tables, tableID)

        Exit Sub
skip:
        Call sendchallenge_chat("Table Warning: " & frmSystray.Winsock(socket).Tag & " does not have enough cards to play playerdeck rules.  /kick [playernumber] or change game rules in order to play from this table.", Val(CStr(tableID)))
    End Sub

    Private Sub team_gamechat(ByVal incoming As String, ByVal socket As Integer)
        Try
            Dim oTables As New BusinessLayer.Tables
            Dim strChat As String
            Dim tableID, ThisPlrId As Integer

            strChat = Mid(incoming, 15, Len(incoming))
            tableID = oTables.findplayerstable(tables, frmSystray.Winsock(socket).Tag)

            If tableID = 0 Then Exit Sub
            Call DalNew(tables(tableID).gameid)

            ThisPlrId = PlayerID(frmSystray.Winsock(socket).Tag)

            If ThisPlrId = 0 Then Exit Sub

            If Left(strChat, 5) <> "/team" Then
                If ThisPlrId <> 1 Then Send(String.Empty, "TEAM_GAMECHAT " & frmSystray.Winsock(socket).Tag & " " & strChat, PlayerSocket(1))
                If ThisPlrId <> 2 Then Send(String.Empty, "TEAM_GAMECHAT " & frmSystray.Winsock(socket).Tag & " " & strChat, PlayerSocket(2))
                If ThisPlrId <> 3 Then Send(String.Empty, "TEAM_GAMECHAT " & frmSystray.Winsock(socket).Tag & " " & strChat, PlayerSocket(3))
                If ThisPlrId <> 4 Then Send(String.Empty, "TEAM_GAMECHAT " & frmSystray.Winsock(socket).Tag & " " & strChat, PlayerSocket(4))
            Else
                Select Case ThisPlrId
                    Case 1
                        Send(String.Empty, "TEAM_GAMECHAT " & frmSystray.Winsock(socket).Tag & " " & strChat, PlayerSocket(2))
                    Case 2
                        Send(String.Empty, "TEAM_GAMECHAT " & frmSystray.Winsock(socket).Tag & " " & strChat, PlayerSocket(1))
                    Case 3
                        Send(String.Empty, "TEAM_GAMECHAT " & frmSystray.Winsock(socket).Tag & " " & strChat, PlayerSocket(4))
                    Case 4
                        Send(String.Empty, "TEAM_GAMECHAT " & frmSystray.Winsock(socket).Tag & " " & strChat, PlayerSocket(3))
                End Select
            End If
        Catch ioorex As System.IndexOutOfRangeException
            ' Do nothing
        Catch ex As Exception
            Call errorsub(ex.Message, "team_gamechat")
        End Try
    End Sub

    Private Function teamomnichallengeyes(ByVal tableID As Integer) As String
        Try
            'omnichallengeyes("OMNICHALLENGEYES " & tables(Val(tableID)).player1 & " " & tables(Val(tableID)).rulelist, socket, tables(Val(tableID)).rulelist))
            Blocks = Val(Tables(tableID).Blocks)

            Select Case Tables(tableID).RuleList
                Case "OMNITT_RANDOM"
                    Type = "Random"
                Case "OMNITT_PLAYER"
                    Type = "Player"
            End Select

            'Tell both players to pick they cards

            Dim oGameFunctions As New GameFunctions
            GameID = oGameFunctions.NewGameID().ToString

            Tables(tableID).GameID = GameID

            'Write GameID in both player file
            Player1 = Tables(tableID).Player1
            Player2 = Tables(tableID).Player2
            Player3 = Tables(tableID).Player3
            Player4 = Tables(tableID).Player4

            If Type = "Random" Then
                GetRandomCards(1)
                GetRandomCards(2)
                GetRandomCards(3)
                GetRandomCards(4)
            Else
                GetRandomPlayerCards(1)
                GetRandomPlayerCards(2)
                GetRandomPlayerCards(3)
                GetRandomPlayerCards(4)
            End If

            'Find card blocks
            If Blocks = 0 Then Blocks = 5

            'Dim oChatFunctions As New ChatFunctions
            'Call oChatFunctions.unisend("CHALLENGESTART", String.Concat(Player1, "/", Player2, " ", Player3, "/", Player4, " 2"))

            Call sendall(String.Concat("ADDTABLE ", tableID, " ", Tables(tableID).Player1, " |FULL| ", Tables(tableID).RuleList, " @@", Tables(tableID).Comment, "@@", Tables(tableID).Decks))

            'If both players are ready, define who starts first and notify both players
            Dim iGameStarter As Integer = oGameFunctions.PickStarter(True)

            Dim strPlayer As String = String.Empty

            Select Case iGameStarter
                Case 1
                    strPlayer = Player1
                Case 2
                    strPlayer = Player2
                Case 3
                    strPlayer = Player3
                Case 4
                    strPlayer = Player4
            End Select

            Send(Player1, String.Concat("TEAM_OTTSTARTER ", iGameStarter, " ", strPlayer))
            Send(Player2, String.Concat("TEAM_OTTSTARTER ", iGameStarter, " ", strPlayer))
            Send(Player3, String.Concat("TEAM_OTTSTARTER ", iGameStarter, " ", strPlayer))
            Send(Player4, String.Concat("TEAM_OTTSTARTER ", iGameStarter, " ", strPlayer))

            GetRandomBlocks()
            GameInsert()

            Send(Player1, "TEAM_OTTCOLOR blue")
            Send(Player2, "TEAM_OTTCOLOR blue")
            Send(Player3, "TEAM_OTTCOLOR red")
            Send(Player4, "TEAM_OTTCOLOR red")

            Select Case iGameStarter
                Case 1
                    Send(Player1, "TEAM_OTTCARDDRAW " & Omni_NextCard(1, False) & " " & Omni_NextCard(1, True))
                    Call teamqueue(1, 3, 2, 4, True)
                Case 2
                    Send(Player2, "TEAM_OTTCARDDRAW " & Omni_NextCard(2, False) & " " & Omni_NextCard(2, True))
                    Call teamqueue(2, 4, 1, 3, True)
                Case 3
                    Send(Player3, "TEAM_OTTCARDDRAW " & Omni_NextCard(3, False) & " " & Omni_NextCard(3, True))
                    Call teamqueue(3, 2, 4, 1, True)
                Case 4
                    Send(Player4, "TEAM_OTTCARDDRAW " & Omni_NextCard(4, False) & " " & Omni_NextCard(4, True))
                    Call teamqueue(4, 1, 3, 2, True)
            End Select

            Send(strPlayer, "TEAM_OTTYOURTURN")
            Return GameID
        Catch ex As Exception
            Call errorsub(ex.ToString, "teamomnichallengeyes")
            Return String.Empty
        End Try
    End Function

    Private Sub team_changerule(ByVal incoming As String, ByVal socket As Integer)
        Dim lostvar As String = String.Empty, strIndex As String = String.Empty
        Dim tableID As Integer
        Dim oTables As New BusinessLayer.Tables

        Divide(incoming, " ", lostvar, strIndex)

        tableID = oTables.findplayerstable(tables, frmSystray.Winsock(socket).Tag)
        If Tables(tableID).Player1 <> frmSystray.Winsock(socket).Tag Then Exit Sub


        Select Case strIndex
            Case "1"
                Select Case Tables(tableID).Blocks
                    Case "1"
                        Tables(tableID).Blocks = "3"
                    Case "3"
                        Tables(tableID).Blocks = "5"
                    Case "5"
                        Tables(tableID).Blocks = "7"
                    Case "7"
                        Tables(tableID).Blocks = "1"
                    Case Else
                        Tables(tableID).Blocks = "5"
                End Select
            Case "2"
                Select Case Tables(tableID).RuleList
                    Case "OMNITT_RANDOM"
                        Tables(tableID).RuleList = "OMNITT_PLAYER"
                    Case "OMNITT_PLAYER"
                        Tables(tableID).RuleList = "OMNITT_RANDOM"
                End Select
        End Select

        If Tables(tableID).Player1 <> String.Empty Then
            Send(Tables(tableID).Player1, "CHALLENGEBOX " & tableID & " " & Tables(tableID).Player1 & " " & Tables(tableID).Player2 & " " & Tables(tableID).Player3 & " " & Tables(tableID).Player4 & " " & Tables(tableID).RuleList & " " & Tables(tableID).Blocks)
        End If

        If Tables(tableID).Player2 <> String.Empty Then
            Send(Tables(tableID).Player2, "CHALLENGEBOX " & tableID & " " & Tables(tableID).Player1 & " " & Tables(tableID).Player2 & " " & Tables(tableID).Player3 & " " & Tables(tableID).Player4 & " " & Tables(tableID).RuleList & " " & Tables(tableID).Blocks)
        End If

        If Tables(tableID).Player3 <> String.Empty Then
            Send(Tables(tableID).Player3, "CHALLENGEBOX " & tableID & " " & Tables(tableID).Player1 & " " & Tables(tableID).Player2 & " " & Tables(tableID).Player3 & " " & Tables(tableID).Player4 & " " & Tables(tableID).RuleList & " " & Tables(tableID).Blocks)
        End If

        If Tables(tableID).Player4 <> String.Empty Then
            Send(Tables(tableID).Player4, "CHALLENGEBOX " & tableID & " " & Tables(tableID).Player1 & " " & Tables(tableID).Player2 & " " & Tables(tableID).Player3 & " " & Tables(tableID).Player4 & " " & Tables(tableID).RuleList & " " & Tables(tableID).Blocks)
        End If
    End Sub

    Private Sub team_change(ByVal socket As Integer)
        Dim tableID, ThisPlrId As Integer
        Dim oTables As New BusinessLayer.Tables

        tableID = oTables.findplayerstable(tables, frmSystray.Winsock(socket).Tag)
        If Tables(tableID).Player1 = frmSystray.Winsock(socket).Tag Then Exit Sub

        If frmSystray.Winsock(socket).Tag = Tables(tableID).Player1 Then ThisPlrId = 1
        If frmSystray.Winsock(socket).Tag = Tables(tableID).Player2 Then ThisPlrId = 2
        If frmSystray.Winsock(socket).Tag = Tables(tableID).Player3 Then ThisPlrId = 3
        If frmSystray.Winsock(socket).Tag = Tables(tableID).Player4 Then ThisPlrId = 4

        Select Case ThisPlrId
            Case 2
                If Tables(tableID).Player3 = String.Empty Then
                    Tables(tableID).Player3 = frmSystray.Winsock(socket).Tag
                    Tables(tableID).Player2 = String.Empty
                    Tables(tableID).Player_Ready(2) = False
                ElseIf Tables(tableID).Player4 = String.Empty Then
                    Tables(tableID).Player4 = frmSystray.Winsock(socket).Tag
                    Tables(tableID).Player2 = String.Empty
                    Tables(tableID).Player_Ready(2) = False
                Else
                    Exit Sub
                End If
            Case 3
                If Tables(tableID).Player2 = String.Empty Then
                    Tables(tableID).Player2 = frmSystray.Winsock(socket).Tag
                    Tables(tableID).Player3 = String.Empty
                    Tables(tableID).Player_Ready(3) = False
                ElseIf Tables(tableID).Player4 = String.Empty Then
                    Tables(tableID).Player4 = frmSystray.Winsock(socket).Tag
                    Tables(tableID).Player3 = String.Empty
                    Tables(tableID).Player_Ready(3) = False
                Else
                    Exit Sub
                End If
            Case 4
                If Tables(tableID).Player2 = String.Empty Then
                    Tables(tableID).Player2 = frmSystray.Winsock(socket).Tag
                    Tables(tableID).Player4 = String.Empty
                    Tables(tableID).Player_Ready(4) = False
                ElseIf Tables(tableID).Player3 = String.Empty Then
                    Tables(tableID).Player3 = frmSystray.Winsock(socket).Tag
                    Tables(tableID).Player4 = String.Empty
                    Tables(tableID).Player_Ready(4) = False
                Else
                    Exit Sub
                End If
        End Select

        If Tables(tableID).Player1 <> String.Empty Then
            Send(Tables(tableID).Player1, "CHALLENGEBOX " & tableID & " " & Tables(tableID).Player1 & " " & Tables(tableID).Player2 & " " & Tables(tableID).Player3 & " " & Tables(tableID).Player4 & " " & Tables(tableID).RuleList & " " & Tables(tableID).Blocks)
            Send(Tables(tableID).Player1, "CHALLENGEREADY 1 " & System.Math.Abs(CShort(Tables(tableID).Player_Ready(1))))
            Send(Tables(tableID).Player1, "CHALLENGEREADY 2 " & System.Math.Abs(CShort(Tables(tableID).Player_Ready(2))))
            Send(Tables(tableID).Player1, "CHALLENGEREADY 3 " & System.Math.Abs(CShort(Tables(tableID).Player_Ready(3))))
            Send(Tables(tableID).Player1, "CHALLENGEREADY 4 " & System.Math.Abs(CShort(Tables(tableID).Player_Ready(4))))
        End If

        If Tables(tableID).Player2 <> String.Empty Then
            Send(Tables(tableID).Player2, "CHALLENGEBOX " & tableID & " " & Tables(tableID).Player1 & " " & Tables(tableID).Player2 & " " & Tables(tableID).Player3 & " " & Tables(tableID).Player4 & " " & Tables(tableID).RuleList & " " & Tables(tableID).Blocks)
            Send(Tables(tableID).Player2, "CHALLENGEREADY 1 " & System.Math.Abs(CShort(Tables(tableID).Player_Ready(1))))
            Send(Tables(tableID).Player2, "CHALLENGEREADY 2 " & System.Math.Abs(CShort(Tables(tableID).Player_Ready(2))))
            Send(Tables(tableID).Player2, "CHALLENGEREADY 3 " & System.Math.Abs(CShort(Tables(tableID).Player_Ready(3))))
            Send(Tables(tableID).Player2, "CHALLENGEREADY 4 " & System.Math.Abs(CShort(Tables(tableID).Player_Ready(4))))
        End If

        If Tables(tableID).Player3 <> String.Empty Then
            Send(Tables(tableID).Player3, "CHALLENGEBOX " & tableID & " " & Tables(tableID).Player1 & " " & Tables(tableID).Player2 & " " & Tables(tableID).Player3 & " " & Tables(tableID).Player4 & " " & Tables(tableID).RuleList & " " & Tables(tableID).Blocks)
            Send(Tables(tableID).Player3, "CHALLENGEREADY 1 " & System.Math.Abs(CShort(Tables(tableID).Player_Ready(1))))
            Send(Tables(tableID).Player3, "CHALLENGEREADY 2 " & System.Math.Abs(CShort(Tables(tableID).Player_Ready(2))))
            Send(Tables(tableID).Player3, "CHALLENGEREADY 3 " & System.Math.Abs(CShort(Tables(tableID).Player_Ready(3))))
            Send(Tables(tableID).Player3, "CHALLENGEREADY 4 " & System.Math.Abs(CShort(Tables(tableID).Player_Ready(4))))
        End If

        If Tables(tableID).Player4 <> String.Empty Then
            Send(Tables(tableID).Player4, "CHALLENGEBOX " & tableID & " " & Tables(tableID).Player1 & " " & Tables(tableID).Player2 & " " & Tables(tableID).Player3 & " " & Tables(tableID).Player4 & " " & Tables(tableID).RuleList & " " & Tables(tableID).Blocks)
            Send(Tables(tableID).Player4, "CHALLENGEREADY 1 " & System.Math.Abs(CShort(Tables(tableID).Player_Ready(1))))
            Send(Tables(tableID).Player4, "CHALLENGEREADY 2 " & System.Math.Abs(CShort(Tables(tableID).Player_Ready(2))))
            Send(Tables(tableID).Player4, "CHALLENGEREADY 3 " & System.Math.Abs(CShort(Tables(tableID).Player_Ready(3))))
            Send(Tables(tableID).Player4, "CHALLENGEREADY 4 " & System.Math.Abs(CShort(Tables(tableID).Player_Ready(4))))
        End If

        Dim strBody As String = String.Empty, strBackground As String = String.Empty, strHead As String = String.Empty, strGender As String = String.Empty
        Dim strPlayer(4) As String

        strPlayer(1) = Tables(tableID).Player1
        strPlayer(2) = Tables(tableID).Player2
        strPlayer(3) = Tables(tableID).Player3
        strPlayer(4) = Tables(tableID).Player4

        For x As Integer = 1 To 4
            If strPlayer(x) <> String.Empty Then
                Dim oPlayerAccount As New PlayerAccountDAL(strPlayer(x))

                For y As Integer = 1 To 4
                    If strPlayer(y) <> String.Empty Then
                        Send(strPlayer(y), String.Concat("CHALLENGEAVATAR ", x, " ", oPlayerAccount.Background, " ", oPlayerAccount.Head, " ", oPlayerAccount.Body, " ", oPlayerAccount.Gender))
                    End If
                Next y

                oPlayerAccount = Nothing
            Else
                For y As Integer = 1 To 4
                    If strPlayer(y) <> String.Empty Then
                        strBackground = String.Empty
                        strHead = String.Empty
                        strBody = String.Empty
                        strGender = String.Empty
                        Send(strPlayer(y), "CHALLENGEAVATAR " & x & " " & strBackground & " " & strHead & " " & strBody & " " & strGender)
                    End If
                Next y
            End If
        Next x
    End Sub

    Private Sub team_OTTAnalyze(ByVal iSquareID As Integer, ByVal sCardPlayed As String, ByVal iThisPlrID As Integer)
        Try
            Call team_ottsamesub(iSquareID, sCardPlayed, iThisPlrID)
            Call team_ottplussub(iSquareID, sCardPlayed, iThisPlrID)
            Call team_ottbasicsub(iSquareID, sCardPlayed, iThisPlrID)

            team_FindOttSendPoints()
            Update()

            If IsGameOver() = False Then
                Select Case iThisPlrID
                    Case 1
                        ' This means player 3 is up
                        Call teamqueue(3, 2, 4, 1)

                        Send(String.Empty, "TEAM_OTTTURNOVER " & Player3, PlayerSocket(1))
                        Send(String.Empty, "TEAM_OTTTURNOVER " & Player3, PlayerSocket(2))
                        Send(String.Empty, "TEAM_OTTYOURTURN", PlayerSocket(3))
                        Send(String.Empty, "TEAM_OTTTURNOVER " & Player3, PlayerSocket(4))
                        Send(Player3, "TEAM_OTTCARDDRAW " & Omni_NextCard(3, False) & " " & Omni_NextCard(3, True))
                    Case 2
                        ' This means player 4 is up
                        Call teamqueue(4, 1, 3, 2)

                        Send(String.Empty, "TEAM_OTTTURNOVER " & Player3, PlayerSocket(1))
                        Send(String.Empty, "TEAM_OTTTURNOVER " & Player3, PlayerSocket(2))
                        Send(String.Empty, "TEAM_OTTYOURTURN", PlayerSocket(4))
                        Send(String.Empty, "TEAM_OTTTURNOVER " & Player3, PlayerSocket(3))
                        Send(Player4, "TEAM_OTTCARDDRAW " & Omni_NextCard(4, False) & " " & Omni_NextCard(4, True))
                    Case 3
                        ' This means player 2 is up
                        Call teamqueue(2, 4, 1, 3)

                        Send(String.Empty, "TEAM_OTTTURNOVER " & Player3, PlayerSocket(1))
                        Send(String.Empty, "TEAM_OTTTURNOVER " & Player3, PlayerSocket(3))
                        Send(String.Empty, "TEAM_OTTYOURTURN", PlayerSocket(2))
                        Send(String.Empty, "TEAM_OTTTURNOVER " & Player3, PlayerSocket(4))
                        Send(Player2, "TEAM_OTTCARDDRAW " & Omni_NextCard(2, False) & " " & Omni_NextCard(2, True))
                    Case 4
                        ' This means player 1 is up
                        Call teamqueue(1, 3, 2, 4)

                        Send(String.Empty, "TEAM_OTTTURNOVER " & Player3, PlayerSocket(2))
                        Send(String.Empty, "TEAM_OTTTURNOVER " & Player3, PlayerSocket(3))
                        Send(String.Empty, "TEAM_OTTYOURTURN", PlayerSocket(1))
                        Send(String.Empty, "TEAM_OTTTURNOVER " & Player3, PlayerSocket(4))
                        Send(Player1, "TEAM_OTTCARDDRAW " & Omni_NextCard(1, False) & " " & Omni_NextCard(1, True))
                End Select
            Else
                TeamGameOver()
            End If
        Catch ex As Exception
            Call errorsub(Err.Description, "team_OTTAnalyze")
        End Try
    End Sub

    Private Sub teamqueue(ByVal seq1 As Integer, ByVal seq2 As Integer, ByVal seq3 As Integer, ByVal seq4 As Integer, Optional ByVal blnStart As Boolean = False)
        Dim strString(4) As String

        For x As Integer = 1 To 4
            Dim y As Integer = 0

            Select Case x
                Case 1
                    y = seq1
                Case 2
                    y = seq2
                Case 3
                    y = seq3
                Case 4
                    y = seq4
            End Select

            Dim oPlayer As New PlayerAccountDAL(PlayerNick(y))
            strString(x) = String.Concat(oPlayer.Player, "::", oPlayer.Gender, "::", oPlayer.Background, "::", oPlayer.Body, "::", oPlayer.Head)
        Next x

        For x As Integer = 1 To 4
            Send(PlayerNick(x), "TEAM_QUEUE " & strString(1) & " " & strString(2) & " " & strString(3) & " " & strString(4) & " " & System.Math.Abs(CShort(blnStart)))
        Next x
    End Sub

    Private Sub TeamGameOver()
        Try
            Dim gprate As Integer = 0
            Dim oStats As New Statistics

            If Team1_Score > Team2_Score Then
                'Team1 1 won
                Call teamladdersub()

                If oServerConfig.LossRate > 0 Then
                    gprate = endgamegp(player3, -1)
                    gpadd(Player3, gprate, True)

                    gprate = EndGameGP(Player4, -1)
                    gpadd(Player4, gprate, True)
                End If

                gprate = EndGameGP(Player1, 1)
                Call gpadd(Player1, gprate, True)

                gprate = EndGameGP(Player2, 1)
                Call gpadd(Player2, gprate, True)

                Call teamgiveexp()
                oStats.CompileStats(Player1, Player3, "tott", False)
                oStats.CompileStats(Player2, Player4, "tott", False)
            ElseIf Team1_Score < Team2_Score Then
                'Team 2 won
                Call teamladdersub()

                If oServerConfig.LossRate > 0 Then
                    gprate = EndGameGP(Player1, -1)
                    gpadd(Player1, gprate, True)

                    gprate = EndGameGP(Player2, -1)
                    gpadd(Player2, gprate, True)
                End If

                gprate = EndGameGP(Player3, 1)
                Call gpadd(Player3, gprate, True)

                gprate = EndGameGP(Player4, 1)
                Call gpadd(Player4, gprate, True)

                Call teamgiveexp()
                oStats.CompileStats(Player3, Player1, "tott", False)
                oStats.CompileStats(Player4, Player2, "tott", False)
            ElseIf Team1_Score = Team2_Score Then
                'Draw game
                Call teamdrawevent(Player1, Player2, Player3, Player4)
                oStats.CompileStats(Player1, Player2, "tott", True)
                oStats.CompileStats(Player3, Player4, "tott", True)
            End If

            Send(String.Empty, "TEAM_OTTTURNOVER", PlayerSocket(1))
            Send(String.Empty, "TEAM_OTTTURNOVER", PlayerSocket(2))
            Send(String.Empty, "TEAM_OTTTURNOVER", PlayerSocket(3))
            Send(String.Empty, "TEAM_OTTTURNOVER", PlayerSocket(4))

            Send(String.Empty, "TEAM_OTTCLOSE", PlayerSocket(1))
            Send(String.Empty, "TEAM_OTTCLOSE", PlayerSocket(2))
            Send(String.Empty, "TEAM_OTTCLOSE", PlayerSocket(3))
            Send(String.Empty, "TEAM_OTTCLOSE", PlayerSocket(4))

            EndGame()
        Catch ex As Exception
            Call errorsub(ex.ToString, "TeamGameOver")
        Finally
            Dim oTables As New BusinessLayer.Tables
            Call oTables.ClosePlayerTable(Tables, Player1, False)
            oTables = Nothing
        End Try
    End Sub

    Private Sub team_ottbasicsub(ByVal iSquareID As Integer, ByVal sCardPlayed As String, ByVal iThisPlrId As Integer)
        Select Case iSquareID
            Case 0
                'Basic Checking
                team_ottMorePower(iSquareID, iThisPlrId, sCardPlayed, "Right", 1, "Left")
                team_ottMorePower(iSquareID, iThisPlrId, sCardPlayed, "Down", 5, "Up")
            Case 1
                'Basic Checking
                team_ottMorePower(iSquareID, iThisPlrId, sCardPlayed, "Left", 0, "Right")
                team_ottMorePower(iSquareID, iThisPlrId, sCardPlayed, "Down", 6, "Up")
                team_ottMorePower(iSquareID, iThisPlrId, sCardPlayed, "Right", 2, "Left")
            Case 2
                'Basic Checking
                team_ottMorePower(iSquareID, iThisPlrId, sCardPlayed, "Left", 1, "Right")
                team_ottMorePower(iSquareID, iThisPlrId, sCardPlayed, "Right", 3, "Left")
                team_ottMorePower(iSquareID, iThisPlrId, sCardPlayed, "Down", 7, "Up")
            Case 3
                'Basic Checking
                team_ottMorePower(iSquareID, iThisPlrId, sCardPlayed, "Left", 2, "Right")
                team_ottMorePower(iSquareID, iThisPlrId, sCardPlayed, "Right", 4, "Left")
                team_ottMorePower(iSquareID, iThisPlrId, sCardPlayed, "Down", 8, "Up")
            Case 4
                'Basic Checking
                team_ottMorePower(iSquareID, iThisPlrId, sCardPlayed, "Left", 3, "Right")
                team_ottMorePower(iSquareID, iThisPlrId, sCardPlayed, "Down", 9, "Up")
            Case 5
                'Basic Checking
                team_ottMorePower(iSquareID, iThisPlrId, sCardPlayed, "Up", 0, "Down")
                team_ottMorePower(iSquareID, iThisPlrId, sCardPlayed, "Right", 6, "Left")
                team_ottMorePower(iSquareID, iThisPlrId, sCardPlayed, "Down", 10, "Up")
            Case 6
                'Basic Checking
                team_ottMorePower(iSquareID, iThisPlrId, sCardPlayed, "Up", 1, "Down")
                team_ottMorePower(iSquareID, iThisPlrId, sCardPlayed, "Left", 5, "Right")
                team_ottMorePower(iSquareID, iThisPlrId, sCardPlayed, "Right", 7, "Left")
                team_ottMorePower(iSquareID, iThisPlrId, sCardPlayed, "Down", 11, "Up")
            Case 7
                'Basic Checking
                team_ottMorePower(iSquareID, iThisPlrId, sCardPlayed, "Up", 2, "Down")
                team_ottMorePower(iSquareID, iThisPlrId, sCardPlayed, "Left", 6, "Right")
                team_ottMorePower(iSquareID, iThisPlrId, sCardPlayed, "Right", 8, "Left")
                team_ottMorePower(iSquareID, iThisPlrId, sCardPlayed, "Down", 12, "Up")
            Case 8
                'Basic Checking
                team_ottMorePower(iSquareID, iThisPlrId, sCardPlayed, "Up", 3, "Down")
                team_ottMorePower(iSquareID, iThisPlrId, sCardPlayed, "Left", 7, "Right")
                team_ottMorePower(iSquareID, iThisPlrId, sCardPlayed, "Right", 9, "Left")
                team_ottMorePower(iSquareID, iThisPlrId, sCardPlayed, "Down", 13, "Up")
            Case 9
                team_ottMorePower(iSquareID, iThisPlrId, sCardPlayed, "Up", 4, "Down")
                team_ottMorePower(iSquareID, iThisPlrId, sCardPlayed, "Left", 8, "Right")
                team_ottMorePower(iSquareID, iThisPlrId, sCardPlayed, "Down", 14, "Up")
            Case 10
                team_ottMorePower(iSquareID, iThisPlrId, sCardPlayed, "Up", 5, "Down")
                team_ottMorePower(iSquareID, iThisPlrId, sCardPlayed, "Right", 11, "Left")
                team_ottMorePower(iSquareID, iThisPlrId, sCardPlayed, "Down", 15, "Up")
            Case 11
                team_ottMorePower(iSquareID, iThisPlrId, sCardPlayed, "Up", 6, "Down")
                team_ottMorePower(iSquareID, iThisPlrId, sCardPlayed, "Left", 10, "Right")
                team_ottMorePower(iSquareID, iThisPlrId, sCardPlayed, "Right", 12, "Left")
                team_ottMorePower(iSquareID, iThisPlrId, sCardPlayed, "Down", 16, "Up")
            Case 12
                team_ottMorePower(iSquareID, iThisPlrId, sCardPlayed, "Up", 7, "Down")
                team_ottMorePower(iSquareID, iThisPlrId, sCardPlayed, "Left", 11, "Right")
                team_ottMorePower(iSquareID, iThisPlrId, sCardPlayed, "Right", 13, "Left")
                team_ottMorePower(iSquareID, iThisPlrId, sCardPlayed, "Down", 17, "Up")
            Case 13
                team_ottMorePower(iSquareID, iThisPlrId, sCardPlayed, "Up", 8, "Down")
                team_ottMorePower(iSquareID, iThisPlrId, sCardPlayed, "Left", 12, "Right")
                team_ottMorePower(iSquareID, iThisPlrId, sCardPlayed, "Right", 14, "Left")
                team_ottMorePower(iSquareID, iThisPlrId, sCardPlayed, "Down", 18, "Up")
            Case 14
                team_ottMorePower(iSquareID, iThisPlrId, sCardPlayed, "Up", 9, "Down")
                team_ottMorePower(iSquareID, iThisPlrId, sCardPlayed, "Left", 13, "Right")
                team_ottMorePower(iSquareID, iThisPlrId, sCardPlayed, "Down", 19, "Up")
            Case 15
                team_ottMorePower(iSquareID, iThisPlrId, sCardPlayed, "Up", 10, "Down")
                team_ottMorePower(iSquareID, iThisPlrId, sCardPlayed, "Right", 16, "Left")
                team_ottMorePower(iSquareID, iThisPlrId, sCardPlayed, "Down", 20, "Up")
            Case 16
                team_ottMorePower(iSquareID, iThisPlrId, sCardPlayed, "Up", 11, "Down")
                team_ottMorePower(iSquareID, iThisPlrId, sCardPlayed, "Left", 15, "Right")
                team_ottMorePower(iSquareID, iThisPlrId, sCardPlayed, "Right", 17, "Left")
                team_ottMorePower(iSquareID, iThisPlrId, sCardPlayed, "Down", 21, "Up")
            Case 17
                team_ottMorePower(iSquareID, iThisPlrId, sCardPlayed, "Up", 12, "Down")
                team_ottMorePower(iSquareID, iThisPlrId, sCardPlayed, "Left", 16, "Right")
                team_ottMorePower(iSquareID, iThisPlrId, sCardPlayed, "Right", 18, "Left")
                team_ottMorePower(iSquareID, iThisPlrId, sCardPlayed, "Down", 22, "Up")
            Case 18
                team_ottMorePower(iSquareID, iThisPlrId, sCardPlayed, "Up", 13, "Down")
                team_ottMorePower(iSquareID, iThisPlrId, sCardPlayed, "Left", 17, "Right")
                team_ottMorePower(iSquareID, iThisPlrId, sCardPlayed, "Right", 19, "Left")
                team_ottMorePower(iSquareID, iThisPlrId, sCardPlayed, "Down", 23, "Up")
            Case 19
                team_ottMorePower(iSquareID, iThisPlrId, sCardPlayed, "Up", 14, "Down")
                team_ottMorePower(iSquareID, iThisPlrId, sCardPlayed, "Left", 18, "Right")
                team_ottMorePower(iSquareID, iThisPlrId, sCardPlayed, "Down", 24, "Up")
            Case 20
                team_ottMorePower(iSquareID, iThisPlrId, sCardPlayed, "Up", 15, "Down")
                team_ottMorePower(iSquareID, iThisPlrId, sCardPlayed, "Right", 21, "Left")
            Case 21
                team_ottMorePower(iSquareID, iThisPlrId, sCardPlayed, "Up", 16, "Down")
                team_ottMorePower(iSquareID, iThisPlrId, sCardPlayed, "Left", 20, "Right")
                team_ottMorePower(iSquareID, iThisPlrId, sCardPlayed, "Right", 22, "Left")
            Case 22
                team_ottMorePower(iSquareID, iThisPlrId, sCardPlayed, "Up", 17, "Down")
                team_ottMorePower(iSquareID, iThisPlrId, sCardPlayed, "Left", 21, "Right")
                team_ottMorePower(iSquareID, iThisPlrId, sCardPlayed, "Right", 23, "Left")
            Case 23
                team_ottMorePower(iSquareID, iThisPlrId, sCardPlayed, "Up", 18, "Down")
                team_ottMorePower(iSquareID, iThisPlrId, sCardPlayed, "Left", 22, "Right")
                team_ottMorePower(iSquareID, iThisPlrId, sCardPlayed, "Right", 24, "Left")
            Case 24
                team_ottMorePower(iSquareID, iThisPlrId, sCardPlayed, "Up", 19, "Down")
                team_ottMorePower(iSquareID, iThisPlrId, sCardPlayed, "Left", 23, "Right")
        End Select
    End Sub

    Private Sub team_ottMorePower(ByVal iSquareID As Integer, ByVal iThisPlrID As Integer, ByVal sCardPlayed As String, ByVal sPlayedSpot_Direction As String, ByVal iNeighborID As Integer, ByVal sNeighbor_Direction As String)
        Try
            Dim ignoremove As Boolean = False
            Dim oCardInfo As New LogicLayer.Cards.PlayingCardInfo

            Dim oPlacedCard As Entities.BusinessObjects.PlayingCard = oCardInfo.GetPlayingCard(sCardPlayed, Cards, CardNameList)

            If oPlacedCard.DirectionValue(sPlayedSpot_Direction) = 0 Then ignoremove = True

            If CardColor(iNeighborID) = "blue" And (iThisPlrID = 1 Or iThisPlrID = 2) Then ignoremove = True
            If CardColor(iNeighborID) = "red" And (iThisPlrID = 3 Or iThisPlrID = 4) Then ignoremove = True
            If IsBlock(iSquareID) = True Or IsEmpty(iSquareID) = True Then ignoremove = True
            If IsBlock(iNeighborID) = True Or IsEmpty(iNeighborID) = True Then ignoremove = True

            If ignoremove = False Then
                Dim oNeighborCard As Entities.BusinessObjects.PlayingCard = oCardInfo.GetPlayingCard(Board(iNeighborID), Cards, CardNameList)

                If oPlacedCard.DirectionValue(sPlayedSpot_Direction) > oNeighborCard.DirectionValue(sNeighbor_Direction) Then
                    'Card is stronger, replace the color

                    If iThisPlrID = 1 Or iThisPlrID = 2 Then
                        CardColor(iNeighborID) = "blue"
                    Else
                        CardColor(iNeighborID) = "red"
                    End If

                    Call sendteam_ottcardflip(iSquareID, iNeighborID)
                    Call sendteam_ottcarddata(iNeighborID)
                End If
            End If
        Catch ex As Exception
            Call errorsub(ex.ToString, "team_ottMorePower")
        End Try
    End Sub

    Private Sub team_FindOttSendPoints()
        Try
            'Re-Calculate the player points and send them to the players

            Team1_Score = 0
            Team2_Score = 0

            For x As Integer = 0 To 24
                Select Case CardColor(x)
                    Case "blue"
                        Team1_Score += 1
                    Case "red"
                        Team2_Score += 1
                End Select
            Next x

            Send(String.Empty, String.Concat("TEAM_OTTPOINTS ", Team1_Score, " ", Team2_Score), PlayerSocket(1))
            Send(String.Empty, String.Concat("TEAM_OTTPOINTS ", Team1_Score, " ", Team2_Score), PlayerSocket(2))
            Send(String.Empty, String.Concat("TEAM_OTTPOINTS ", Team1_Score, " ", Team2_Score), PlayerSocket(3))
            Send(String.Empty, String.Concat("TEAM_OTTPOINTS ", Team1_Score, " ", Team2_Score), PlayerSocket(4))
        Catch ex As Exception
            Call errorsub(ex.Message, "team_FindOttSendPoints")
        End Try
    End Sub

    Private Sub sendteam_ottcardflip(ByVal iBoardID As Integer, ByVal iNeighborID As Integer)
        Dim status As Integer = 1

        Select Case iNeighborID
            Case 0
                If iBoardID = 5 Then status = 2
            Case 1
                If iBoardID = 6 Then status = 2
            Case 2
                If iBoardID = 7 Then status = 2
            Case 3
                If iBoardID = 8 Then status = 2
            Case 4
                If iBoardID = 9 Then status = 2
            Case 5
                If iBoardID = 0 Or iBoardID = 10 Then status = 2
            Case 6
                If iBoardID = 1 Or iBoardID = 11 Then status = 2
            Case 7
                If iBoardID = 2 Or iBoardID = 12 Then status = 2
            Case 8
                If iBoardID = 3 Or iBoardID = 13 Then status = 2
            Case 9
                If iBoardID = 4 Or iBoardID = 14 Then status = 2
            Case 10
                If iBoardID = 5 Or iBoardID = 15 Then status = 2
            Case 11
                If iBoardID = 6 Or iBoardID = 16 Then status = 2
            Case 12
                If iBoardID = 7 Or iBoardID = 17 Then status = 2
            Case 13
                If iBoardID = 8 Or iBoardID = 18 Then status = 2
            Case 14
                If iBoardID = 9 Or iBoardID = 19 Then status = 2
            Case 15
                If iBoardID = 10 Or iBoardID = 20 Then status = 2
            Case 16
                If iBoardID = 11 Or iBoardID = 21 Then status = 2
            Case 17
                If iBoardID = 12 Or iBoardID = 22 Then status = 2
            Case 18
                If iBoardID = 13 Or iBoardID = 23 Then status = 2
            Case 19
                If iBoardID = 14 Or iBoardID = 14 Then status = 2
            Case 20
                If iBoardID = 15 Then status = 2
            Case 21
                If iBoardID = 16 Then status = 2
            Case 22
                If iBoardID = 17 Then status = 2
            Case 23
                If iBoardID = 18 Then status = 2
            Case 24
                If iBoardID = 19 Then status = 2
        End Select

        Send(String.Empty, String.Concat("TEAM_OTTCARDFLIP ", iNeighborID, " ", status, " ", CardColor(iNeighborID)), PlayerSocket(1))
        Send(String.Empty, String.Concat("TEAM_OTTCARDFLIP ", iNeighborID, " ", status, " ", CardColor(iNeighborID)), PlayerSocket(2))
        Send(String.Empty, String.Concat("TEAM_OTTCARDFLIP ", iNeighborID, " ", status, " ", CardColor(iNeighborID)), PlayerSocket(3))
        Send(String.Empty, String.Concat("TEAM_OTTCARDFLIP ", iNeighborID, " ", status, " ", CardColor(iNeighborID)), PlayerSocket(4))
    End Sub

    Private Sub team_ottsamesub(ByVal iSquareID As Integer, ByVal sCardPlayed As String, ByVal iThisPlrId As Integer)
        Call team_ottsame(iSquareID, sCardPlayed, iThisPlrId)
    End Sub

    Private Sub team_ottplussub(ByVal iSquareID As Integer, ByVal sCardPlayed As String, ByVal iThisPlrId As Integer)
        Call team_ottplus(iSquareID, sCardPlayed, iThisPlrId)
    End Sub

    Public Sub team_ottsame(ByVal iSquareID As Integer, ByVal sCardPlayed As String, ByVal iThisPlrId As Integer)
        Try
            'Check Right(0), Left(1) and Down(0), Up(3)
            Select Case iSquareID
                Case 0
                    'Check Right(0), Left(1) and Down(0), Up(3)
                    Call team_ottchecksame(sCardPlayed, 0, "Right", "Down", 1, "Left", 5, "Up", iThisPlrId)
                Case 1
                    Call team_ottchecksame(sCardPlayed, 1, "Left", "Down", 0, "Right", 6, "Up", iThisPlrId)
                    Call team_ottchecksame(sCardPlayed, 1, "Left", "Right", 0, "Right", 2, "Left", iThisPlrId)
                    Call team_ottchecksame(sCardPlayed, 1, "Right", "Down", 2, "Left", 6, "Up", iThisPlrId)
                Case 2
                    'Check Left(2), Right(1) and Down(2), Up(5)
                    Call team_ottchecksame(sCardPlayed, 2, "Left", "Down", 1, "Right", 7, "Up", iThisPlrId)
                    Call team_ottchecksame(sCardPlayed, 2, "Left", "Right", 1, "Right", 3, "Left", iThisPlrId)
                    Call team_ottchecksame(sCardPlayed, 2, "Right", "Down", 3, "Left", 7, "Up", iThisPlrId)
                Case 3
                    Call team_ottchecksame(sCardPlayed, 3, "Left", "Down", 2, "Right", 8, "Up", iThisPlrId)
                    Call team_ottchecksame(sCardPlayed, 3, "Left", "Right", 2, "Right", 4, "Left", iThisPlrId)
                    Call team_ottchecksame(sCardPlayed, 3, "Right", "Down", 4, "Left", 8, "Up", iThisPlrId)
                Case 4
                    Call team_ottchecksame(sCardPlayed, 4, "Left", "Down", 3, "Right", 9, "Up", iThisPlrId)
                Case 5
                    Call team_ottchecksame(sCardPlayed, 5, "Up", "Right", 0, "Down", 6, "Left", iThisPlrId)
                    Call team_ottchecksame(sCardPlayed, 5, "Right", "Down", 6, "Left", 10, "Up", iThisPlrId)
                    Call team_ottchecksame(sCardPlayed, 5, "Up", "Down", 0, "Down", 10, "Up", iThisPlrId)
                Case 6
                    Call team_ottchecksame(sCardPlayed, 6, "Up", "Down", 1, "Down", 11, "Up", iThisPlrId)
                    Call team_ottchecksame(sCardPlayed, 6, "Left", "Down", 5, "Right", 11, "Up", iThisPlrId)
                    Call team_ottchecksame(sCardPlayed, 6, "Left", "Right", 5, "Right", 7, "Left", iThisPlrId)
                    Call team_ottchecksame(sCardPlayed, 6, "Right", "Down", 7, "Left", 11, "Up", iThisPlrId)
                    Call team_ottchecksame(sCardPlayed, 6, "Up", "Right", 1, "Down", 7, "Left", iThisPlrId)
                    Call team_ottchecksame(sCardPlayed, 6, "Up", "Left", 1, "Down", 5, "Right", iThisPlrId)
                Case 7
                    Call team_ottchecksame(sCardPlayed, 7, "Up", "Down", 2, "Down", 12, "Up", iThisPlrId)
                    Call team_ottchecksame(sCardPlayed, 7, "Left", "Down", 6, "Right", 12, "Up", iThisPlrId)
                    Call team_ottchecksame(sCardPlayed, 7, "Left", "Right", 6, "Right", 8, "Left", iThisPlrId)
                    Call team_ottchecksame(sCardPlayed, 7, "Right", "Down", 8, "Left", 12, "Up", iThisPlrId)
                    Call team_ottchecksame(sCardPlayed, 7, "Up", "Right", 2, "Down", 8, "Left", iThisPlrId)
                    Call team_ottchecksame(sCardPlayed, 7, "Up", "Left", 2, "Down", 6, "Right", iThisPlrId)
                Case 8
                    Call team_ottchecksame(sCardPlayed, 8, "Up", "Down", 3, "Down", 13, "Up", iThisPlrId)
                    Call team_ottchecksame(sCardPlayed, 8, "Left", "Down", 7, "Right", 13, "Up", iThisPlrId)
                    Call team_ottchecksame(sCardPlayed, 8, "Left", "Right", 7, "Right", 9, "Left", iThisPlrId)
                    Call team_ottchecksame(sCardPlayed, 8, "Right", "Down", 9, "Left", 13, "Up", iThisPlrId)
                    Call team_ottchecksame(sCardPlayed, 8, "Up", "Right", 3, "Down", 9, "Left", iThisPlrId)
                    Call team_ottchecksame(sCardPlayed, 8, "Up", "Left", 3, "Down", 7, "Right", iThisPlrId)
                Case 9
                    Call team_ottchecksame(sCardPlayed, 9, "Up", "Down", 4, "Down", 14, "Up", iThisPlrId)
                    Call team_ottchecksame(sCardPlayed, 9, "Left", "Down", 8, "Right", 14, "Up", iThisPlrId)
                    Call team_ottchecksame(sCardPlayed, 9, "Up", "Left", 4, "Down", 8, "Right", iThisPlrId)
                Case 10
                    Call team_ottchecksame(sCardPlayed, 10, "Up", "Down", 5, "Down", 15, "Up", iThisPlrId)
                    Call team_ottchecksame(sCardPlayed, 10, "Right", "Down", 11, "Left", 15, "Up", iThisPlrId)
                    Call team_ottchecksame(sCardPlayed, 10, "Right", "Up", 11, "Left", 5, "Down", iThisPlrId)
                Case 11
                    Call team_ottchecksame(sCardPlayed, 11, "Up", "Down", 6, "Down", 16, "Up", iThisPlrId)
                    Call team_ottchecksame(sCardPlayed, 11, "Left", "Down", 10, "Right", 16, "Up", iThisPlrId)
                    Call team_ottchecksame(sCardPlayed, 11, "Left", "Right", 10, "Right", 12, "Left", iThisPlrId)
                    Call team_ottchecksame(sCardPlayed, 11, "Right", "Down", 12, "Left", 16, "Up", iThisPlrId)
                    Call team_ottchecksame(sCardPlayed, 11, "Up", "Right", 6, "Down", 12, "Left", iThisPlrId)
                    Call team_ottchecksame(sCardPlayed, 11, "Up", "Left", 6, "Down", 10, "Right", iThisPlrId)
                Case 12
                    Call team_ottchecksame(sCardPlayed, 12, "Up", "Down", 7, "Down", 17, "Up", iThisPlrId)
                    Call team_ottchecksame(sCardPlayed, 12, "Left", "Down", 11, "Right", 17, "Up", iThisPlrId)
                    Call team_ottchecksame(sCardPlayed, 12, "Left", "Right", 11, "Right", 13, "Left", iThisPlrId)
                    Call team_ottchecksame(sCardPlayed, 12, "Right", "Down", 13, "Left", 17, "Up", iThisPlrId)
                    Call team_ottchecksame(sCardPlayed, 12, "Up", "Right", 7, "Down", 13, "Left", iThisPlrId)
                    Call team_ottchecksame(sCardPlayed, 12, "Up", "Left", 7, "Down", 11, "Right", iThisPlrId)
                Case 13
                    Call team_ottchecksame(sCardPlayed, 13, "Up", "Down", 8, "Down", 18, "Up", iThisPlrId)
                    Call team_ottchecksame(sCardPlayed, 13, "Left", "Down", 12, "Right", 18, "Up", iThisPlrId)
                    Call team_ottchecksame(sCardPlayed, 13, "Left", "Right", 12, "Right", 14, "Left", iThisPlrId)
                    Call team_ottchecksame(sCardPlayed, 13, "Right", "Down", 14, "Left", 18, "Up", iThisPlrId)
                    Call team_ottchecksame(sCardPlayed, 13, "Up", "Right", 8, "Down", 14, "Left", iThisPlrId)
                    Call team_ottchecksame(sCardPlayed, 13, "Up", "Left", 8, "Down", 12, "Right", iThisPlrId)
                Case 14
                    Call team_ottchecksame(sCardPlayed, 14, "Up", "Down", 9, "Down", 19, "Up", iThisPlrId)
                    Call team_ottchecksame(sCardPlayed, 14, "Left", "Down", 13, "Right", 19, "Up", iThisPlrId)
                    Call team_ottchecksame(sCardPlayed, 14, "Up", "Left", 9, "Down", 13, "Right", iThisPlrId)
                Case 15
                    Call team_ottchecksame(sCardPlayed, 15, "Up", "Down", 10, "Down", 20, "Up", iThisPlrId)
                    Call team_ottchecksame(sCardPlayed, 15, "Right", "Down", 16, "Left", 20, "Up", iThisPlrId)
                    Call team_ottchecksame(sCardPlayed, 15, "Up", "Right", 10, "Down", 16, "Left", iThisPlrId)
                Case 16
                    Call team_ottchecksame(sCardPlayed, 16, "Up", "Down", 11, "Down", 21, "Up", iThisPlrId)
                    Call team_ottchecksame(sCardPlayed, 16, "Left", "Down", 15, "Right", 21, "Up", iThisPlrId)
                    Call team_ottchecksame(sCardPlayed, 16, "Left", "Right", 15, "Right", 17, "Left", iThisPlrId)
                    Call team_ottchecksame(sCardPlayed, 16, "Right", "Down", 17, "Left", 21, "Up", iThisPlrId)
                    Call team_ottchecksame(sCardPlayed, 16, "Up", "Right", 11, "Down", 17, "Left", iThisPlrId)
                    Call team_ottchecksame(sCardPlayed, 16, "Up", "Left", 11, "Down", 15, "Right", iThisPlrId)
                Case 17
                    Call team_ottchecksame(sCardPlayed, 17, "Up", "Down", 12, "Down", 22, "Up", iThisPlrId)
                    Call team_ottchecksame(sCardPlayed, 17, "Left", "Down", 16, "Right", 22, "Up", iThisPlrId)
                    Call team_ottchecksame(sCardPlayed, 17, "Left", "Right", 16, "Right", 18, "Left", iThisPlrId)
                    Call team_ottchecksame(sCardPlayed, 17, "Right", "Down", 18, "Left", 22, "Up", iThisPlrId)
                    Call team_ottchecksame(sCardPlayed, 17, "Up", "Right", 12, "Down", 18, "Left", iThisPlrId)
                    Call team_ottchecksame(sCardPlayed, 17, "Up", "Left", 12, "Down", 16, "Right", iThisPlrId)
                Case 18
                    Call team_ottchecksame(sCardPlayed, 18, "Up", "Down", 13, "Down", 23, "Up", iThisPlrId)
                    Call team_ottchecksame(sCardPlayed, 18, "Left", "Down", 17, "Right", 23, "Up", iThisPlrId)
                    Call team_ottchecksame(sCardPlayed, 18, "Left", "Right", 17, "Right", 19, "Left", iThisPlrId)
                    Call team_ottchecksame(sCardPlayed, 18, "Right", "Down", 19, "Left", 23, "Up", iThisPlrId)
                    Call team_ottchecksame(sCardPlayed, 18, "Up", "Right", 13, "Down", 19, "Left", iThisPlrId)
                    Call team_ottchecksame(sCardPlayed, 18, "Up", "Left", 13, "Down", 17, "Right", iThisPlrId)
                Case 19
                    Call team_ottchecksame(sCardPlayed, 19, "Up", "Down", 14, "Down", 24, "Up", iThisPlrId)
                    Call team_ottchecksame(sCardPlayed, 19, "Left", "Down", 18, "Right", 24, "Up", iThisPlrId)
                    Call team_ottchecksame(sCardPlayed, 19, "Up", "Left", 14, "Down", 18, "Right", iThisPlrId)
                Case 20
                    Call team_ottchecksame(sCardPlayed, 20, "Up", "Right", 15, "Down", 21, "Left", iThisPlrId)
                Case 21
                    Call team_ottchecksame(sCardPlayed, 21, "Left", "Right", 20, "Right", 22, "Left", iThisPlrId)
                    Call team_ottchecksame(sCardPlayed, 21, "Up", "Right", 16, "Down", 22, "Left", iThisPlrId)
                    Call team_ottchecksame(sCardPlayed, 21, "Up", "Left", 16, "Down", 20, "Right", iThisPlrId)
                Case 22
                    Call team_ottchecksame(sCardPlayed, 22, "Left", "Right", 21, "Right", 23, "Left", iThisPlrId)
                    Call team_ottchecksame(sCardPlayed, 22, "Up", "Right", 17, "Down", 23, "Left", iThisPlrId)
                    Call team_ottchecksame(sCardPlayed, 22, "Up", "Left", 17, "Down", 21, "Right", iThisPlrId)
                Case 23
                    Call team_ottchecksame(sCardPlayed, 23, "Left", "Right", 22, "Right", 24, "Left", iThisPlrId)
                    Call team_ottchecksame(sCardPlayed, 23, "Up", "Right", 18, "Down", 24, "Left", iThisPlrId)
                    Call team_ottchecksame(sCardPlayed, 23, "Up", "Left", 18, "Down", 22, "Right", iThisPlrId)
                Case 24
                    Call team_ottchecksame(sCardPlayed, 24, "Up", "Left", 19, "Down", 23, "Right", iThisPlrId)
            End Select
        Catch ex As Exception
            Call errorsub(ex.Message, "team_ottsame")
        End Try
    End Sub

    Private Sub team_ottchecksame(ByVal sCardPlayed As String, ByVal iBoardID As Integer, ByVal sPlayedSpot_Direction1 As String, ByVal sPlayedSpot_Direction2 As String, ByVal iNeighborID As Integer, ByVal sNeighbor_Direction1 As String, ByVal iNeighbor2ID As Integer, ByVal sNeighbor_Direction2 As String, ByVal iThisPlrID As Integer)
        Try
            Dim pointchange As Integer = 0
            Dim part1 As Boolean = False, part2 As Boolean = False, ignoremove As Boolean = False

            Dim oCardInfo As New LogicLayer.Cards.PlayingCardInfo

            If IsEmpty(iNeighborID) = True Or IsBlock(iNeighborID) = True Then ignoremove = True
            If IsEmpty(iNeighbor2ID) = True Or IsBlock(iNeighbor2ID) = True Then ignoremove = True

            Dim oPlacedCard As Entities.BusinessObjects.PlayingCard = oCardInfo.GetPlayingCard(sCardPlayed, Cards, CardNameList)

            If oPlacedCard.DirectionValue(sPlayedSpot_Direction1) = 0 Then ignoremove = True
            If oPlacedCard.DirectionValue(sPlayedSpot_Direction2) = 0 Then ignoremove = True

            If ignoremove = False Then
                Dim oNeighbor1Card As Entities.BusinessObjects.PlayingCard = oCardInfo.GetPlayingCard(Board(iNeighborID), Cards, CardNameList)
                Dim oNeighbor2Card As Entities.BusinessObjects.PlayingCard = oCardInfo.GetPlayingCard(Board(iNeighbor2ID), Cards, CardNameList)

                If (oPlacedCard.DirectionValue(sPlayedSpot_Direction1) = oNeighbor1Card.DirectionValue(sNeighbor_Direction1)) And (oPlacedCard.DirectionValue(sPlayedSpot_Direction2) = oNeighbor2Card.DirectionValue(sNeighbor_Direction2)) Then
                    If iThisPlrID = 1 Or iThisPlrID = 2 Then
                        If CardColor(iNeighborID) = "red" Then
                            pointchange += 1
                            part1 = True
                        End If

                        If CardColor(iNeighbor2ID) = "red" Then
                            pointchange += 1
                            part2 = True
                        End If

                        CardColor(iBoardID) = "blue"
                        CardColor(iNeighborID) = "blue"
                        CardColor(iNeighbor2ID) = "blue"
                    Else
                        If CardColor(iNeighborID) = "blue" Then
                            pointchange += 1
                            part1 = True
                        End If

                        If CardColor(iNeighbor2ID) = "blue" Then
                            pointchange += 1
                            part2 = True
                        End If

                        CardColor(iBoardID) = "red"
                        CardColor(iNeighborID) = "red"
                        CardColor(iNeighbor2ID) = "red"
                    End If

                    Call sendteam_ottcardflip(iBoardID, iNeighborID)
                    Call sendteam_ottcarddata(iBoardID)
                    Call sendteam_ottcarddata(iNeighborID)

                    Call sendteam_ottcardflip(iBoardID, iNeighbor2ID)
                    Call sendteam_ottcarddata(iNeighbor2ID)

                    Call team_ottsamedata(pointchange)

                    If part1 = True Then
                        Call team_ottcombosub(oNeighbor1Card.CardName, iNeighborID, iThisPlrID)
                    End If

                    If part2 = True Then
                        Call team_ottcombosub(oNeighbor2Card.CardName, iNeighbor2ID, iThisPlrID)
                    End If
                End If
            End If
        Catch ex As Exception
            Call errorsub(ex.Message, "team_ottchecksame")
        End Try
    End Sub

    Public Sub team_ottsamedata(ByVal pointchange As Integer)
        Try
            If pointchange > 0 Then
                Send(String.Empty, "TEAM_OTTSAME", PlayerSocket(1))
                Send(String.Empty, "TEAM_OTTSAME", PlayerSocket(2))
                Send(String.Empty, "TEAM_OTTSAME", PlayerSocket(3))
                Send(String.Empty, "TEAM_OTTSAME", PlayerSocket(4))

                HistoryInsert(-1, String.Empty, "SAME")
            End If
        Catch ex As Exception
            Call errorsub(ex.Message, "team_ottsamedata")
        End Try
    End Sub

    Public Sub team_ottplus(ByVal iSquareID As Integer, ByVal sCardPlayed As String, ByVal iThisPlrId As Integer)
        Try
            'Check Right(0), Left(1) and Down(0), Up(3)
            Select Case iSquareID
                Case 0
                    'Check Right(0), Left(1) and Down(0), Up(3)
                    Call team_ottcheckplus(sCardPlayed, 0, "Right", "Down", 1, "Left", 5, "Up", iThisPlrId)
                Case 1
                    Call team_ottcheckplus(sCardPlayed, 1, "Left", "Down", 0, "Right", 6, "Up", iThisPlrId)
                    Call team_ottcheckplus(sCardPlayed, 1, "Left", "Right", 0, "Right", 2, "Left", iThisPlrId)
                    Call team_ottcheckplus(sCardPlayed, 1, "Right", "Down", 2, "Left", 6, "Up", iThisPlrId)
                Case 2
                    'Check Left(2), Right(1) and Down(2), Up(5)
                    Call team_ottcheckplus(sCardPlayed, 2, "Left", "Down", 1, "Right", 7, "Up", iThisPlrId)
                    Call team_ottcheckplus(sCardPlayed, 2, "Left", "Right", 1, "Right", 3, "Left", iThisPlrId)
                    Call team_ottcheckplus(sCardPlayed, 2, "Right", "Down", 3, "Left", 7, "Up", iThisPlrId)
                Case 3
                    Call team_ottcheckplus(sCardPlayed, 3, "Left", "Down", 2, "Right", 8, "Up", iThisPlrId)
                    Call team_ottcheckplus(sCardPlayed, 3, "Left", "Right", 2, "Right", 4, "Left", iThisPlrId)
                    Call team_ottcheckplus(sCardPlayed, 3, "Right", "Down", 4, "Left", 8, "Up", iThisPlrId)
                Case 4
                    Call team_ottcheckplus(sCardPlayed, 4, "Left", "Down", 3, "Right", 9, "Up", iThisPlrId)
                Case 5
                    Call team_ottcheckplus(sCardPlayed, 5, "Up", "Right", 0, "Down", 6, "Left", iThisPlrId)
                    Call team_ottcheckplus(sCardPlayed, 5, "Right", "Down", 6, "Left", 10, "Up", iThisPlrId)
                    Call team_ottcheckplus(sCardPlayed, 5, "Up", "Down", 0, "Down", 10, "Up", iThisPlrId)
                Case 6
                    Call team_ottcheckplus(sCardPlayed, 6, "Up", "Down", 1, "Down", 11, "Up", iThisPlrId)
                    Call team_ottcheckplus(sCardPlayed, 6, "Left", "Down", 5, "Right", 11, "Up", iThisPlrId)
                    Call team_ottcheckplus(sCardPlayed, 6, "Left", "Right", 5, "Right", 7, "Left", iThisPlrId)
                    Call team_ottcheckplus(sCardPlayed, 6, "Right", "Down", 7, "Left", 11, "Up", iThisPlrId)
                    Call team_ottcheckplus(sCardPlayed, 6, "Up", "Right", 1, "Down", 7, "Left", iThisPlrId)
                    Call team_ottcheckplus(sCardPlayed, 6, "Up", "Left", 1, "Down", 5, "Right", iThisPlrId)
                Case 7
                    Call team_ottcheckplus(sCardPlayed, 7, "Up", "Down", 2, "Down", 12, "Up", iThisPlrId)
                    Call team_ottcheckplus(sCardPlayed, 7, "Left", "Down", 6, "Right", 12, "Up", iThisPlrId)
                    Call team_ottcheckplus(sCardPlayed, 7, "Left", "Right", 6, "Right", 8, "Left", iThisPlrId)
                    Call team_ottcheckplus(sCardPlayed, 7, "Right", "Down", 8, "Left", 12, "Up", iThisPlrId)
                    Call team_ottcheckplus(sCardPlayed, 7, "Up", "Right", 2, "Down", 8, "Left", iThisPlrId)
                    Call team_ottcheckplus(sCardPlayed, 7, "Up", "Left", 2, "Down", 6, "Right", iThisPlrId)
                Case 8
                    Call team_ottcheckplus(sCardPlayed, 8, "Up", "Down", 3, "Down", 13, "Up", iThisPlrId)
                    Call team_ottcheckplus(sCardPlayed, 8, "Left", "Down", 7, "Right", 13, "Up", iThisPlrId)
                    Call team_ottcheckplus(sCardPlayed, 8, "Left", "Right", 7, "Right", 9, "Left", iThisPlrId)
                    Call team_ottcheckplus(sCardPlayed, 8, "Right", "Down", 9, "Left", 13, "Up", iThisPlrId)
                    Call team_ottcheckplus(sCardPlayed, 8, "Up", "Right", 3, "Down", 9, "Left", iThisPlrId)
                    Call team_ottcheckplus(sCardPlayed, 8, "Up", "Left", 3, "Down", 7, "Right", iThisPlrId)
                Case 9
                    Call team_ottcheckplus(sCardPlayed, 9, "Up", "Down", 4, "Down", 14, "Up", iThisPlrId)
                    Call team_ottcheckplus(sCardPlayed, 9, "Left", "Down", 8, "Right", 14, "Up", iThisPlrId)
                    Call team_ottcheckplus(sCardPlayed, 9, "Up", "Left", 4, "Down", 8, "Right", iThisPlrId)
                Case 10
                    Call team_ottcheckplus(sCardPlayed, 10, "Up", "Down", 5, "Down", 15, "Up", iThisPlrId)
                    Call team_ottcheckplus(sCardPlayed, 10, "Right", "Down", 11, "Left", 15, "Up", iThisPlrId)
                    Call team_ottcheckplus(sCardPlayed, 10, "Right", "Up", 11, "Left", 5, "Down", iThisPlrId)
                Case 11
                    Call team_ottcheckplus(sCardPlayed, 11, "Up", "Down", 6, "Down", 16, "Up", iThisPlrId)
                    Call team_ottcheckplus(sCardPlayed, 11, "Left", "Down", 10, "Right", 16, "Up", iThisPlrId)
                    Call team_ottcheckplus(sCardPlayed, 11, "Left", "Right", 10, "Right", 12, "Left", iThisPlrId)
                    Call team_ottcheckplus(sCardPlayed, 11, "Right", "Down", 12, "Left", 16, "Up", iThisPlrId)
                    Call team_ottcheckplus(sCardPlayed, 11, "Up", "Right", 6, "Down", 12, "Left", iThisPlrId)
                    Call team_ottcheckplus(sCardPlayed, 11, "Up", "Left", 6, "Down", 10, "Right", iThisPlrId)
                Case 12
                    Call team_ottcheckplus(sCardPlayed, 12, "Up", "Down", 7, "Down", 17, "Up", iThisPlrId)
                    Call team_ottcheckplus(sCardPlayed, 12, "Left", "Down", 11, "Right", 17, "Up", iThisPlrId)
                    Call team_ottcheckplus(sCardPlayed, 12, "Left", "Right", 11, "Right", 13, "Left", iThisPlrId)
                    Call team_ottcheckplus(sCardPlayed, 12, "Right", "Down", 13, "Left", 17, "Up", iThisPlrId)
                    Call team_ottcheckplus(sCardPlayed, 12, "Up", "Right", 7, "Down", 13, "Left", iThisPlrId)
                    Call team_ottcheckplus(sCardPlayed, 12, "Up", "Left", 7, "Down", 11, "Right", iThisPlrId)
                Case 13
                    Call team_ottcheckplus(sCardPlayed, 13, "Up", "Down", 8, "Down", 18, "Up", iThisPlrId)
                    Call team_ottcheckplus(sCardPlayed, 13, "Left", "Down", 12, "Right", 18, "Up", iThisPlrId)
                    Call team_ottcheckplus(sCardPlayed, 13, "Left", "Right", 12, "Right", 14, "Left", iThisPlrId)
                    Call team_ottcheckplus(sCardPlayed, 13, "Right", "Down", 14, "Left", 18, "Up", iThisPlrId)
                    Call team_ottcheckplus(sCardPlayed, 13, "Up", "Right", 8, "Down", 14, "Left", iThisPlrId)
                    Call team_ottcheckplus(sCardPlayed, 13, "Up", "Left", 8, "Down", 12, "Right", iThisPlrId)
                Case 14
                    Call team_ottcheckplus(sCardPlayed, 14, "Up", "Down", 9, "Down", 19, "Up", iThisPlrId)
                    Call team_ottcheckplus(sCardPlayed, 14, "Left", "Down", 13, "Right", 19, "Up", iThisPlrId)
                    Call team_ottcheckplus(sCardPlayed, 14, "Up", "Left", 9, "Down", 13, "Right", iThisPlrId)
                Case 15
                    Call team_ottcheckplus(sCardPlayed, 15, "Up", "Down", 10, "Down", 20, "Up", iThisPlrId)
                    Call team_ottcheckplus(sCardPlayed, 15, "Right", "Down", 16, "Left", 20, "Up", iThisPlrId)
                    Call team_ottcheckplus(sCardPlayed, 15, "Up", "Right", 10, "Down", 16, "Left", iThisPlrId)
                Case 16
                    Call team_ottcheckplus(sCardPlayed, 16, "Up", "Down", 11, "Down", 21, "Up", iThisPlrId)
                    Call team_ottcheckplus(sCardPlayed, 16, "Left", "Down", 15, "Right", 21, "Up", iThisPlrId)
                    Call team_ottcheckplus(sCardPlayed, 16, "Left", "Right", 15, "Right", 17, "Left", iThisPlrId)
                    Call team_ottcheckplus(sCardPlayed, 16, "Right", "Down", 17, "Left", 21, "Up", iThisPlrId)
                    Call team_ottcheckplus(sCardPlayed, 16, "Up", "Right", 11, "Down", 17, "Left", iThisPlrId)
                    Call team_ottcheckplus(sCardPlayed, 16, "Up", "Left", 11, "Down", 15, "Right", iThisPlrId)
                Case 17
                    Call team_ottcheckplus(sCardPlayed, 17, "Up", "Down", 12, "Down", 22, "Up", iThisPlrId)
                    Call team_ottcheckplus(sCardPlayed, 17, "Left", "Down", 16, "Right", 22, "Up", iThisPlrId)
                    Call team_ottcheckplus(sCardPlayed, 17, "Left", "Right", 16, "Right", 18, "Left", iThisPlrId)
                    Call team_ottcheckplus(sCardPlayed, 17, "Right", "Down", 18, "Left", 22, "Up", iThisPlrId)
                    Call team_ottcheckplus(sCardPlayed, 17, "Up", "Right", 12, "Down", 18, "Left", iThisPlrId)
                    Call team_ottcheckplus(sCardPlayed, 17, "Up", "Left", 12, "Down", 16, "Right", iThisPlrId)
                Case 18
                    Call team_ottcheckplus(sCardPlayed, 18, "Up", "Down", 13, "Down", 23, "Up", iThisPlrId)
                    Call team_ottcheckplus(sCardPlayed, 18, "Left", "Down", 17, "Right", 23, "Up", iThisPlrId)
                    Call team_ottcheckplus(sCardPlayed, 18, "Left", "Right", 17, "Right", 19, "Left", iThisPlrId)
                    Call team_ottcheckplus(sCardPlayed, 18, "Right", "Down", 19, "Left", 23, "Up", iThisPlrId)
                    Call team_ottcheckplus(sCardPlayed, 18, "Up", "Right", 13, "Down", 19, "Left", iThisPlrId)
                    Call team_ottcheckplus(sCardPlayed, 18, "Up", "Left", 13, "Down", 17, "Right", iThisPlrId)
                Case 19
                    Call team_ottcheckplus(sCardPlayed, 19, "Up", "Down", 14, "Down", 24, "Up", iThisPlrId)
                    Call team_ottcheckplus(sCardPlayed, 19, "Left", "Down", 18, "Right", 24, "Up", iThisPlrId)
                    Call team_ottcheckplus(sCardPlayed, 19, "Up", "Left", 14, "Down", 18, "Right", iThisPlrId)
                Case 20
                    Call team_ottcheckplus(sCardPlayed, 20, "Up", "Right", 15, "Down", 21, "Left", iThisPlrId)
                Case 21
                    Call team_ottcheckplus(sCardPlayed, 21, "Left", "Right", 20, "Right", 22, "Left", iThisPlrId)
                    Call team_ottcheckplus(sCardPlayed, 21, "Up", "Right", 16, "Down", 22, "Left", iThisPlrId)
                    Call team_ottcheckplus(sCardPlayed, 21, "Up", "Left", 16, "Down", 20, "Right", iThisPlrId)
                Case 22
                    Call team_ottcheckplus(sCardPlayed, 22, "Left", "Right", 21, "Right", 23, "Left", iThisPlrId)
                    Call team_ottcheckplus(sCardPlayed, 22, "Up", "Right", 17, "Down", 23, "Left", iThisPlrId)
                    Call team_ottcheckplus(sCardPlayed, 22, "Up", "Left", 17, "Down", 21, "Right", iThisPlrId)
                Case 23
                    Call team_ottcheckplus(sCardPlayed, 23, "Left", "Right", 22, "Right", 24, "Left", iThisPlrId)
                    Call team_ottcheckplus(sCardPlayed, 23, "Up", "Right", 18, "Down", 24, "Left", iThisPlrId)
                    Call team_ottcheckplus(sCardPlayed, 23, "Up", "Left", 18, "Down", 22, "Right", iThisPlrId)
                Case 24
                    Call team_ottcheckplus(sCardPlayed, 24, "Up", "Left", 19, "Down", 23, "Right", iThisPlrId)
            End Select
        Catch ex As Exception
            Call errorsub(ex.Message, "Team_OTTPlus")
        End Try
    End Sub

    Public Sub team_ottplusdata(ByVal pointchange As Integer)
        Try
            If pointchange > 0 Then
                Send(String.Empty, "TEAM_OTTPLUS", PlayerSocket(1))
                Send(String.Empty, "TEAM_OTTPLUS", PlayerSocket(2))
                Send(String.Empty, "TEAM_OTTPLUS", PlayerSocket(3))
                Send(String.Empty, "TEAM_OTTPLUS", PlayerSocket(4))

                HistoryInsert(-1, String.Empty, "PLUS")
            End If
        Catch ex As Exception
            Call errorsub(ex.ToString, "team_ottplusdata")
        End Try
    End Sub

    Private Sub team_ottcheckplus(ByVal sCardPlayed As String, ByVal iBoardID As Integer, ByVal sPlayedSpot_Direction1 As String, ByVal sPlayedSpot_Direction2 As String, ByVal iNeighborID As Integer, ByVal sNeighbor_Direction1 As String, ByVal iNeighbor2ID As Integer, ByVal sNeighbor_Direction2 As String, ByVal iThisPlrID As Integer)
        Try
            Dim pointchange As Integer = 0
            Dim part1 As Boolean = False, part2 As Boolean = False, ignoremove As Boolean = False

            If IsEmpty(iNeighborID) = True Or IsBlock(iNeighborID) = True Then ignoremove = True
            If IsEmpty(iNeighbor2ID) = True Or IsBlock(iNeighbor2ID) = True Then ignoremove = True

            Dim oCardInfo As New LogicLayer.Cards.PlayingCardInfo

            Dim oPlacedCard As Entities.BusinessObjects.PlayingCard = oCardInfo.GetPlayingCard(sCardPlayed, Cards, CardNameList)

            If oPlacedCard.DirectionValue(sPlayedSpot_Direction1) = 0 Then ignoremove = True
            If oPlacedCard.DirectionValue(sPlayedSpot_Direction2) = 0 Then ignoremove = True

            If ignoremove = False Then
                Dim oNeighbor1Card As Entities.BusinessObjects.PlayingCard = oCardInfo.GetPlayingCard(Board(iNeighborID), Cards, CardNameList)
                Dim oNeighbor2Card As Entities.BusinessObjects.PlayingCard = oCardInfo.GetPlayingCard(Board(iNeighbor2ID), Cards, CardNameList)

                If (oPlacedCard.DirectionValue(sPlayedSpot_Direction1) + oNeighbor1Card.DirectionValue(sNeighbor_Direction1)) = (oPlacedCard.DirectionValue(sPlayedSpot_Direction2) + oNeighbor2Card.DirectionValue(sNeighbor_Direction2)) Then

                    If iThisPlrID = 1 Or iThisPlrID = 2 Then
                        If CardColor(iNeighborID) = "red" Then
                            pointchange += 1
                            part1 = True
                        End If

                        If CardColor(iNeighbor2ID) = "red" Then
                            pointchange += 1
                            part2 = True
                        End If

                        CardColor(iBoardID) = "blue"
                        CardColor(iNeighborID) = "blue"
                        CardColor(iNeighbor2ID) = "blue"
                    Else
                        If CardColor(iNeighborID) = "blue" Then
                            pointchange += 1
                            part1 = True
                        End If

                        If CardColor(iNeighbor2ID) = "blue" Then
                            pointchange += 1
                            part2 = True
                        End If

                        CardColor(iBoardID) = "red"
                        CardColor(iNeighborID) = "red"
                        CardColor(iNeighbor2ID) = "red"
                    End If

                    Call sendteam_ottcardflip(iBoardID, iNeighborID)
                    Call sendteam_ottcarddata(iBoardID)
                    Call sendteam_ottcarddata(iNeighborID)

                    Call sendteam_ottcardflip(iBoardID, iNeighbor2ID)
                    Call sendteam_ottcarddata(iNeighbor2ID)

                    Call team_ottplusdata(pointchange)

                    If part1 = True Then
                        Call team_ottcombosub(oNeighbor1Card.CardName, iNeighborID, iThisPlrID)
                    End If

                    If part2 = True Then
                        Call team_ottcombosub(oNeighbor2Card.CardName, iNeighbor2ID, iThisPlrID)
                    End If
                End If
            End If
        Catch ex As Exception
            Call errorsub(ex.Message, "Team_OTTCheckPlus")
        End Try
    End Sub

    Public Sub team_ottcombosub(ByVal sPlayedCard As String, ByVal iBoardID As Integer, ByVal iThisPlrID As Integer)
        Try
            Select Case iBoardID
                Case 0
                    'Check Right(0), Left(1) and Down(0), Up(3)
                    Call team_ottcheckcombo(sPlayedCard, iThisPlrID, 0, "Right", 1, "Left")
                    Call team_ottcheckcombo(sPlayedCard, iThisPlrID, 0, "Down", 5, "Up")
                Case 1
                    Call team_ottcheckcombo(sPlayedCard, iThisPlrID, 1, "Left", 0, "Right")
                    Call team_ottcheckcombo(sPlayedCard, iThisPlrID, 1, "Down", 6, "Up")
                    Call team_ottcheckcombo(sPlayedCard, iThisPlrID, 1, "Right", 2, "Left")
                Case 2
                    Call team_ottcheckcombo(sPlayedCard, iThisPlrID, 2, "Left", 1, "Right")
                    Call team_ottcheckcombo(sPlayedCard, iThisPlrID, 2, "Down", 7, "Up")
                    Call team_ottcheckcombo(sPlayedCard, iThisPlrID, 2, "Right", 3, "Left")
                Case 3
                    Call team_ottcheckcombo(sPlayedCard, iThisPlrID, 3, "Left", 2, "Right")
                    Call team_ottcheckcombo(sPlayedCard, iThisPlrID, 3, "Down", 8, "Up")
                    Call team_ottcheckcombo(sPlayedCard, iThisPlrID, 3, "Right", 4, "Left")
                Case 4
                    Call team_ottcheckcombo(sPlayedCard, iThisPlrID, 4, "Left", 3, "Right")
                    Call team_ottcheckcombo(sPlayedCard, iThisPlrID, 4, "Down", 9, "Up")
                Case 5
                    Call team_ottcheckcombo(sPlayedCard, iThisPlrID, 5, "Up", 0, "Down")
                    Call team_ottcheckcombo(sPlayedCard, iThisPlrID, 5, "Down", 10, "Up")
                    Call team_ottcheckcombo(sPlayedCard, iThisPlrID, 5, "Right", 6, "Left")
                Case 6
                    Call team_ottcheckcombo(sPlayedCard, iThisPlrID, 6, "Up", 1, "Down")
                    Call team_ottcheckcombo(sPlayedCard, iThisPlrID, 6, "Down", 11, "Up")
                    Call team_ottcheckcombo(sPlayedCard, iThisPlrID, 6, "Right", 7, "Left")
                    Call team_ottcheckcombo(sPlayedCard, iThisPlrID, 6, "Left", 5, "Right")
                Case 7
                    Call team_ottcheckcombo(sPlayedCard, iThisPlrID, 7, "Up", 2, "Down")
                    Call team_ottcheckcombo(sPlayedCard, iThisPlrID, 7, "Down", 12, "Up")
                    Call team_ottcheckcombo(sPlayedCard, iThisPlrID, 7, "Right", 8, "Left")
                    Call team_ottcheckcombo(sPlayedCard, iThisPlrID, 7, "Left", 6, "Right")
                Case 8
                    Call team_ottcheckcombo(sPlayedCard, iThisPlrID, 8, "Up", 3, "Down")
                    Call team_ottcheckcombo(sPlayedCard, iThisPlrID, 8, "Down", 13, "Up")
                    Call team_ottcheckcombo(sPlayedCard, iThisPlrID, 8, "Right", 9, "Left")
                    Call team_ottcheckcombo(sPlayedCard, iThisPlrID, 8, "Left", 7, "Right")
                Case 9
                    Call team_ottcheckcombo(sPlayedCard, iThisPlrID, 9, "Up", 4, "Down")
                    Call team_ottcheckcombo(sPlayedCard, iThisPlrID, 9, "Down", 14, "Up")
                    Call team_ottcheckcombo(sPlayedCard, iThisPlrID, 9, "Left", 8, "Right")
                Case 10
                    Call team_ottcheckcombo(sPlayedCard, iThisPlrID, 10, "Up", 5, "Down")
                    Call team_ottcheckcombo(sPlayedCard, iThisPlrID, 10, "Down", 15, "Up")
                    Call team_ottcheckcombo(sPlayedCard, iThisPlrID, 10, "Right", 11, "Left")
                Case 11
                    Call team_ottcheckcombo(sPlayedCard, iThisPlrID, 11, "Up", 6, "Down")
                    Call team_ottcheckcombo(sPlayedCard, iThisPlrID, 11, "Down", 16, "Up")
                    Call team_ottcheckcombo(sPlayedCard, iThisPlrID, 11, "Right", 12, "Left")
                    Call team_ottcheckcombo(sPlayedCard, iThisPlrID, 11, "Left", 10, "Right")
                Case 12
                    Call team_ottcheckcombo(sPlayedCard, iThisPlrID, 12, "Up", 7, "Down")
                    Call team_ottcheckcombo(sPlayedCard, iThisPlrID, 12, "Down", 17, "Up")
                    Call team_ottcheckcombo(sPlayedCard, iThisPlrID, 12, "Right", 13, "Left")
                    Call team_ottcheckcombo(sPlayedCard, iThisPlrID, 12, "Left", 11, "Right")
                Case 13
                    Call team_ottcheckcombo(sPlayedCard, iThisPlrID, 13, "Up", 8, "Down")
                    Call team_ottcheckcombo(sPlayedCard, iThisPlrID, 13, "Down", 18, "Up")
                    Call team_ottcheckcombo(sPlayedCard, iThisPlrID, 13, "Right", 14, "Left")
                    Call team_ottcheckcombo(sPlayedCard, iThisPlrID, 13, "Left", 12, "Right")
                Case 14
                    Call team_ottcheckcombo(sPlayedCard, iThisPlrID, 14, "Up", 9, "Down")
                    Call team_ottcheckcombo(sPlayedCard, iThisPlrID, 14, "Down", 19, "Up")
                    Call team_ottcheckcombo(sPlayedCard, iThisPlrID, 14, "Left", 13, "Right")
                Case 15
                    Call team_ottcheckcombo(sPlayedCard, iThisPlrID, 15, "Up", 10, "Down")
                    Call team_ottcheckcombo(sPlayedCard, iThisPlrID, 15, "Down", 20, "Up")
                    Call team_ottcheckcombo(sPlayedCard, iThisPlrID, 15, "Right", 16, "Left")
                Case 16
                    Call team_ottcheckcombo(sPlayedCard, iThisPlrID, 16, "Up", 11, "Down")
                    Call team_ottcheckcombo(sPlayedCard, iThisPlrID, 16, "Down", 21, "Up")
                    Call team_ottcheckcombo(sPlayedCard, iThisPlrID, 16, "Right", 17, "Left")
                    Call team_ottcheckcombo(sPlayedCard, iThisPlrID, 16, "Left", 15, "Right")
                Case 17
                    Call team_ottcheckcombo(sPlayedCard, iThisPlrID, 17, "Up", 12, "Down")
                    Call team_ottcheckcombo(sPlayedCard, iThisPlrID, 17, "Down", 22, "Up")
                    Call team_ottcheckcombo(sPlayedCard, iThisPlrID, 17, "Right", 18, "Left")
                    Call team_ottcheckcombo(sPlayedCard, iThisPlrID, 17, "Left", 16, "Right")
                Case 18
                    Call team_ottcheckcombo(sPlayedCard, iThisPlrID, 18, "Up", 13, "Down")
                    Call team_ottcheckcombo(sPlayedCard, iThisPlrID, 18, "Down", 23, "Up")
                    Call team_ottcheckcombo(sPlayedCard, iThisPlrID, 18, "Right", 19, "Left")
                    Call team_ottcheckcombo(sPlayedCard, iThisPlrID, 18, "Left", 17, "Right")
                Case 19
                    Call team_ottcheckcombo(sPlayedCard, iThisPlrID, 19, "Up", 14, "Down")
                    Call team_ottcheckcombo(sPlayedCard, iThisPlrID, 19, "Down", 24, "Up")
                    Call team_ottcheckcombo(sPlayedCard, iThisPlrID, 19, "Left", 18, "Right")
                Case 20
                    Call team_ottcheckcombo(sPlayedCard, iThisPlrID, 20, "Up", 15, "Down")
                    Call team_ottcheckcombo(sPlayedCard, iThisPlrID, 20, "Right", 21, "Left")
                Case 21
                    Call team_ottcheckcombo(sPlayedCard, iThisPlrID, 21, "Up", 16, "Down")
                    Call team_ottcheckcombo(sPlayedCard, iThisPlrID, 21, "Right", 22, "Left")
                    Call team_ottcheckcombo(sPlayedCard, iThisPlrID, 21, "Left", 20, "Right")
                Case 22
                    Call team_ottcheckcombo(sPlayedCard, iThisPlrID, 22, "Up", 17, "Down")
                    Call team_ottcheckcombo(sPlayedCard, iThisPlrID, 22, "Right", 23, "Left")
                    Call team_ottcheckcombo(sPlayedCard, iThisPlrID, 22, "Left", 21, "Right")
                Case 23
                    Call team_ottcheckcombo(sPlayedCard, iThisPlrID, 23, "Up", 18, "Down")
                    Call team_ottcheckcombo(sPlayedCard, iThisPlrID, 23, "Right", 24, "Left")
                    Call team_ottcheckcombo(sPlayedCard, iThisPlrID, 23, "Left", 22, "Right")
                Case 24
                    Call team_ottcheckcombo(sPlayedCard, iThisPlrID, 24, "Up", 19, "Down")
                    Call team_ottcheckcombo(sPlayedCard, iThisPlrID, 24, "Left", 23, "Right")
            End Select
        Catch ex As Exception
            Call errorsub(ex.ToString, "team_OTTComboSub")
        End Try
    End Sub

    Public Sub team_ottcheckcombo(ByVal sCardPlayed As String, ByVal iThisPlrID As Integer, ByVal iBoardID As Integer, ByVal sPlayedSpot_Direction1 As String, ByVal iNeighborID As Integer, ByVal sNeighbor_Direction1 As String)
        Try
            Dim ignoremove As Boolean = False, pointchange As Integer = 0, part1 As Boolean = False

            Dim oCardInfo As New LogicLayer.Cards.PlayingCardInfo

            Dim oPlacedCard As Entities.BusinessObjects.PlayingCard = oCardInfo.GetPlayingCard(sCardPlayed, Cards, CardNameList)

            If IsEmpty(iBoardID) = True Or IsBlock(iBoardID) = True Then ignoremove = True
            If IsEmpty(iNeighborID) = True Or IsBlock(iNeighborID) = True Then ignoremove = True
            If oPlacedCard.DirectionValue(sPlayedSpot_Direction1) = 0 Then ignoremove = True

            If ignoremove = False Then
                Dim oNeighborCard As Entities.BusinessObjects.PlayingCard = oCardInfo.GetPlayingCard(Board(iNeighborID), Cards, CardNameList)

                If oPlacedCard.DirectionValue(sPlayedSpot_Direction1) > oNeighborCard.DirectionValue(sNeighbor_Direction1) Then
                    If iThisPlrID = 1 Or iThisPlrID = 2 Then
                        If CardColor(iNeighborID) = "red" Then
                            pointchange += 1
                            part1 = True
                        End If

                        CardColor(iBoardID) = "blue"
                        CardColor(iNeighborID) = "blue"
                    Else
                        If CardColor(iNeighborID) = "blue" Then
                            pointchange += 1
                            part1 = True
                        End If

                        CardColor(iBoardID) = "red"
                        CardColor(iNeighborID) = "red"
                    End If

                    Call sendteam_ottcardflip(iBoardID, iNeighborID)
                    Call sendteam_ottcarddata(iBoardID)
                    Call sendteam_ottcarddata(iNeighborID)

                    Call team_ottcombodata(pointchange)
                    'FindOttSendPoints(oTeamOTTFunctions)

                    If part1 = True Then
                        Call team_ottcombosub(oNeighborCard.CardName, iNeighborID, iThisPlrID)
                    End If
                End If
            End If
        Catch ex As Exception
            Call errorsub(ex.ToString, "OTTCheckCombo")
        End Try
    End Sub

    Public Sub team_ottcombodata(ByVal pointchange As Integer)
        Try
            If pointchange > 0 Then
                Send(String.Empty, "TEAM_OTTCOMBO", PlayerSocket(1))
                Send(String.Empty, "TEAM_OTTCOMBO", PlayerSocket(2))
                Send(String.Empty, "TEAM_OTTCOMBO", PlayerSocket(3))
                Send(String.Empty, "TEAM_OTTCOMBO", PlayerSocket(4))

                HistoryInsert(-1, String.Empty, "COMBO")
            End If
        Catch ex As Exception
            Call errorsub(ex.ToString, "team_OTTComboData")
        End Try
    End Sub

    Public Sub team_ottplaceat(ByVal incoming As String, ByVal socket As Integer)
        Dim sDivide() As String = incoming.Split(" ")
        Dim iSquareID As Integer = Val(sDivide(1))

        Dim oTables As New BusinessLayer.Tables
        Dim iGameID As Integer = tables(oTables.findplayerstable(tables, frmSystray.Winsock(socket).Tag)).gameid
        Call dalnew(iGameID)

        Dim iThisPlrID As Integer = PlayerID(frmSystray.Winsock(socket).Tag)
        Dim sCardPlayed As String = Player_Hand(iThisPlrID, Player_Index(iThisPlrID) - 1)

        If IsPlayable(iSquareID) = False Then
            Dim oDatabaseFunctions As New DatabaseFunctions
            Dim oChatFunctions As New ChatFunctions

            Call quit("QUIT", socket, True)

            oChatFunctions.uniwarn(String.Concat(frmSystray.Winsock(socket).Tag, " tried to place a card on the OTT board that already had a card on it.  GameID: ", GameID))
            oDatabaseFunctions.HistoryInsert(frmSystray.Winsock(socket).Tag, "2", String.Concat(frmSystray.Winsock(socket).Tag, " tried to place a card on the OTT board that already had a card on it.  GameID: ", GameID))
            Exit Sub
        End If

        Board(iSquareID) = sCardPlayed
        HistoryInsert(iSquareID, sCardPlayed, String.Concat(frmSystray.Winsock(socket).Tag, " placed ", sCardPlayed, " on the board."))

        Select Case iThisPlrID
            Case 1
                CardColor(iSquareID) = "blue"
                Send(String.Empty, String.Concat("TEAM_OTTPLACEDCARD ", iSquareID, " ", sCardPlayed.Replace(" ", "%20"), " blue"), GetSocket(Player2))
                Send(String.Empty, String.Concat("TEAM_OTTPLACEDCARD ", iSquareID, " ", sCardPlayed.Replace(" ", "%20"), " blue"), GetSocket(Player3))
                Send(String.Empty, String.Concat("TEAM_OTTPLACEDCARD ", iSquareID, " ", sCardPlayed.Replace(" ", "%20"), " blue"), GetSocket(Player4))
            Case 2
                CardColor(iSquareID) = "blue"
                Send(String.Empty, String.Concat("TEAM_OTTPLACEDCARD ", iSquareID, " ", sCardPlayed.Replace(" ", "%20"), " blue"), GetSocket(Player1))
                Send(String.Empty, String.Concat("TEAM_OTTPLACEDCARD ", iSquareID, " ", sCardPlayed.Replace(" ", "%20"), " blue"), GetSocket(Player3))
                Send(String.Empty, String.Concat("TEAM_OTTPLACEDCARD ", iSquareID, " ", sCardPlayed.Replace(" ", "%20"), " blue"), GetSocket(Player4))
            Case 3
                CardColor(iSquareID) = "red"
                Send(String.Empty, String.Concat("TEAM_OTTPLACEDCARD ", iSquareID, " ", sCardPlayed.Replace(" ", "%20"), " red"), GetSocket(Player1))
                Send(String.Empty, String.Concat("TEAM_OTTPLACEDCARD ", iSquareID, " ", sCardPlayed.Replace(" ", "%20"), " red"), GetSocket(Player2))
                Send(String.Empty, String.Concat("TEAM_OTTPLACEDCARD ", iSquareID, " ", sCardPlayed.Replace(" ", "%20"), " red"), GetSocket(Player4))
            Case 4
                CardColor(iSquareID) = "red"
                Send(String.Empty, String.Concat("TEAM_OTTPLACEDCARD ", iSquareID, " ", sCardPlayed.Replace(" ", "%20"), " red"), GetSocket(Player1))
                Send(String.Empty, String.Concat("TEAM_OTTPLACEDCARD ", iSquareID, " ", sCardPlayed.Replace(" ", "%20"), " red"), GetSocket(Player2))
                Send(String.Empty, String.Concat("TEAM_OTTPLACEDCARD ", iSquareID, " ", sCardPlayed.Replace(" ", "%20"), " red"), GetSocket(Player3))
        End Select

        team_OTTAnalyze(iSquareID, sCardPlayed, iThisPlrID)
        System.Windows.Forms.Application.DoEvents()
    End Sub

    Public Sub team_ottdiscard(ByVal socket As Integer)
        Dim oTables As New BusinessLayer.Tables
        Dim iGameID As Integer = tables(oTables.findplayerstable(tables, frmSystray.Winsock(socket).Tag)).gameid

        Call DalNew(iGameID)

        Dim iThisPlrId As Integer = PlayerID(frmSystray.Winsock(socket).Tag)

        If Discards(iThisPlrId) = 0 Then
            'Kill match and give warning
            Dim oChatFunctions As New ChatFunctions
            Dim objTableFunctions As New TableFunctions

            Call objTableFunctions.EndMatch("ENDMATCH " & frmSystray.Winsock(socket).Tag, 0, False, True)
            oChatFunctions.uniwarn(String.Concat("WARNING ADMINS! ", frmSystray.Winsock(socket).Tag, " tried to discard one too many cards!"))
            Call historyadd(frmSystray.Winsock(socket).Tag, 2, String.Concat(frmSystray.Winsock(socket).Tag, " tried to discard one too many cards! (OTT)"))
            Exit Sub
        End If

        Discards(iThisPlrId) -= 1
        HistoryInsert(-1, String.Empty, String.Concat(frmSystray.Winsock(socket).Tag, " discarded"))
        Update()

        Send(String.Empty, String.Concat("TEAM_OTTCARDDRAW ", Omni_NextCard(iThisPlrId, False), " ", Omni_NextCard(iThisPlrId, True)), socket)
    End Sub

    Public Sub sendteam_ottcarddata(ByVal iBoardID As Integer)
        Send(String.Empty, String.Concat("TEAM_OTT", UCase(CardColor(iBoardID)), "CARD ", iBoardID, " ", Board(iBoardID).Replace(" ", "%20")), PlayerSocket(1))
        Send(String.Empty, String.Concat("TEAM_OTT", UCase(CardColor(iBoardID)), "CARD ", iBoardID, " ", Board(iBoardID).Replace(" ", "%20")), PlayerSocket(2))
        Send(String.Empty, String.Concat("TEAM_OTT", UCase(CardColor(iBoardID)), "CARD ", iBoardID, " ", Board(iBoardID).Replace(" ", "%20")), PlayerSocket(3))
        Send(String.Empty, String.Concat("TEAM_OTT", UCase(CardColor(iBoardID)), "CARD ", iBoardID, " ", Board(iBoardID).Replace(" ", "%20")), PlayerSocket(4))
    End Sub

    Public Sub sendchallenge_chat(ByRef strText As String, ByRef tableID As Short)
        If tables(tableID).player1 <> "" Then Send(tables(tableID).player1, "CHALLENGECHAT *** " & strText)
        If tables(tableID).player2 <> "" Then Send(tables(tableID).player2, "CHALLENGECHAT *** " & strText)
        If tables(tableID).player3 <> "" Then Send(tables(tableID).player3, "CHALLENGECHAT *** " & strText)
        If tables(tableID).player4 <> "" Then Send(tables(tableID).player4, "CHALLENGECHAT *** " & strText)
    End Sub

    Private Sub teamgiveexp()
        Dim winners(2) As String
        Dim losers(2) As String
        Dim winners_exp(2) As Integer
        Dim losers_exp(2) As Integer

        Dim oPlayer As New PlayerAccountDAL
        Dim oGameFunctions As New GameFunctions

        With Me
            Dim exp As Integer = 30 + ((.WinningScore - .LosingScore) * 2)
            Dim exploser As Integer = 21 + (.LosingScore - .WinningScore)

            If exp <= 0 Then exp = 1
            If exploser <= 0 Then exploser = 1

            exp = exp * (1 + oServerConfig.PlayerBonus)
            exploser = exploser * (1 + oServerConfig.PlayerBonus)

            If .WinningTeam = 1 Then
                winners(1) = .PlayerNick(1)
                winners(2) = .PlayerNick(2)
                losers(1) = .PlayerNick(3)
                losers(2) = .PlayerNick(4)
            Else
                winners(1) = .PlayerNick(3)
                winners(2) = .PlayerNick(4)
                losers(1) = .PlayerNick(1)
                losers(2) = .PlayerNick(2)
            End If

            For x As Integer = 1 To 2
                Dim oWinner As New PlayerAccountDAL(winners(x))
                Dim oLoser As New PlayerAccountDAL(losers(x))

                winners_exp(x) = exp * oWinner.EXPBonus
                losers_exp(x) = exploser * oLoser.EXPBonus

                Call oWinner.UpdateExp(winners_exp(x))
                Call oLoser.UpdateExp(losers_exp(x))

                If oWinner.Experience >= oWinner.NextLevel Then
                    oWinner.UpdateField("level", (oWinner.Level + 1).ToString, True)
                    oWinner.Reload()

                    Send(String.Empty, String.Concat("LEVELUP ", oWinner.Level, " ", oWinner.Experience, " ", oWinner.NextLevel), oWinner.PlayerSocket)
                    Call oGameFunctions.LevelUp(winners(x), oWinner.Level)
                End If

                If oLoser.Experience >= oLoser.NextLevel Then
                    oLoser.UpdateField("level", (oLoser.Level + 1).ToString, True)
                    oLoser.Reload()

                    Send(String.Empty, String.Concat("LEVELUP ", oLoser.Level, " ", oLoser.Experience, " ", oLoser.NextLevel), oLoser.PlayerSocket)
                    Call oGameFunctions.LevelUp(losers(x), oLoser.Level)
                End If

                Send(String.Empty, String.Concat("UPDATEEXP ", exp, " ", oWinner.Experience, " ", oWinner.Level, " ", oWinner.NextLevel), oWinner.PlayerSocket)
                Send(String.Empty, String.Concat("UPDATEEXP ", exp, " ", oLoser.Experience, " ", oLoser.Level, " ", oLoser.NextLevel), oLoser.PlayerSocket)

                Dim y As Integer = oGameFunctions.APBonus(winners(x))
                Send(String.Empty, "UPDATEAP " & giveap(winners(x), y), oWinner.PlayerSocket)
                Call blockchat(String.Empty, "AP", String.Concat("You received ", y, " Active Points!"), oWinner.PlayerSocket)

                y = oGameFunctions.APBonus(losers(x))
                Send(String.Empty, "UPDATEAP " & giveap(losers(x), CInt(y)), oLoser.PlayerSocket)
                Call blockchat(String.Empty, "AP", String.Concat("You received ", y, " Active Points!"), oLoser.PlayerSocket)
            Next x
        End With
    End Sub

    Public Sub teamladdersub()
        Try
            Dim winners(2) As String, losers(2) As String

            With Me
                If .WinningTeam = 1 Then
                    winners(1) = .PlayerNick(1)
                    winners(2) = .PlayerNick(2)

                    losers(1) = .PlayerNick(3)
                    losers(2) = .PlayerNick(4)
                Else
                    winners(1) = .PlayerNick(3)
                    winners(2) = .PlayerNick(4)

                    losers(1) = .PlayerNick(1)
                    losers(2) = .PlayerNick(2)
                End If

                Dim oPlayerAccount As New PlayerAccountDAL(winners(1))
                Dim oPlayerAccount2 As New PlayerAccountDAL(winners(2))
                Dim oPlayerAccount3 As New PlayerAccountDAL(losers(1))
                Dim oPlayerAccount4 As New PlayerAccountDAL(losers(2))

                Dim oChatFunctions As New ChatFunctions

                oChatFunctions.unisend("TEAM_MATCH", String.Concat(winners(1), " ", oPlayerAccount.OTTRank, " ", winners(2), " ", oPlayerAccount2.OTTRank, " ", losers(1), " ", oPlayerAccount3.OTTRank, " ", losers(2) & " " & oPlayerAccount4.OTTRank, " ", .WinningScore, " ", .LosingScore, " 1 ", GameID))

                oPlayerAccount.UpdateField("lastplayed", DateString)
                oPlayerAccount2.UpdateField("lastplayed", DateString)
                oPlayerAccount3.UpdateField("lastplayed", DateString)
                oPlayerAccount4.UpdateField("lastplayed", DateString)
            End With
        Catch ex As Exception
            Call errorsub(ex.ToString, "teamladdersub")
        End Try
    End Sub

    Public Sub teamdrawevent(ByVal sPlayer1 As String, ByVal sPlayer2 As String, ByVal sPlayer3 As String, ByVal sPlayer4 As String)
        Call teamladdersub()

        Call gpadd(sPlayer1, EndGameGP(Player1, 0), True)
        Call gpadd(sPlayer2, EndGameGP(Player2, 0), True)
        Call gpadd(sPlayer3, EndGameGP(Player3, 0), True)
        Call gpadd(sPlayer4, EndGameGP(Player4, 0), True)

        Call teamgiveexp()
    End Sub
End Class
