Public Class TableFunctions
    Public Sub IncomingModule(ByVal sCommand As String, ByVal incoming As String, ByVal socket As Integer)
        Dim objTableFunctions As New BusinessLayer.Tables

        Select Case sCommand
            Case "TABLEKILL"
                Call EndMatch(incoming, socket, False, True)
            Case "TABLECLOSE"
                Call objTableFunctions.PreGameTableClose(Tables, socket)
            Case "TABLEENDMATCH"
                Call EndMatch(incoming, socket)
            Case "TABLECREATE"
                Call tablecreate(incoming, socket)
            Case "TABLECREATE2"
                Call tablecreate2(incoming, socket)
            Case "TABLECREATE3"
                Call tablecreate3(incoming, socket)
            Case "TABLECREATE4"
                Call tablecreate4(incoming, socket)
            Case "TABLEJOIN"
                Call tablejoin(incoming, socket)
            Case "TABLEQUERY"
                Call tablequery(incoming, socket)
            Case "TABLEINFO"
                Call tableinfo(incoming, socket)
        End Select
    End Sub

    Public Sub EndMatch(ByVal incoming As String, ByVal socket As Integer, Optional ByVal bAdmin As Boolean = False, Optional ByVal blnEnd As Boolean = False)
        Try
            Dim opponent As String = String.Empty, lostvar As String = String.Empty, enick As String = String.Empty, gameidfile As String = String.Empty
            Dim strStatus As String

            Dim oTableFunctions As New BusinessLayer.Tables

            Divide(incoming, " ", lostvar, enick)

            If bAdmin = True Then
                strStatus = getAdministrator_Field(frmSystray.Winsock(socket).Tag, "status")
            Else
                strStatus = String.Empty
            End If

            If (strStatus <> "0" Or blnEnd = True) Or (bAdmin = False And strStatus = String.Empty) Then
                Dim iTableID As Integer = oTableFunctions.FindPlayersTable(Tables, enick)

                If iTableID = 0 Then
                    Call blockchat(String.Empty, "Error", "Server could not end match.  Table not found in memory.", socket)
                    Exit Sub
                End If

                Call oTableFunctions.SendKillMatch(Tables(iTableID))
                Call oTableFunctions.ClosePlayerTable(Tables, iTableID)
            End If
        Catch ex As Exception
            Call errorsub(ex.ToString, "endmatch")
        End Try
    End Sub

    Private Sub tablecreate(ByVal incoming As String, ByVal socket As Integer)
        Try
            Dim strComment As String = String.Empty, lostvar As String = String.Empty
            Dim strDecks As String = String.Empty

            Dim oTables As New BusinessLayer.Tables
            Dim oPlayerAccount As New PlayerAccountDAL(frmSystray.Winsock(socket).Tag)

            If oTables.FindTablePlayer(Tables, oPlayerAccount.Player) = True Then
                Send(String.Empty, "CANCELBOX", socket)
                Exit Sub
            End If

            Dim strRules As String = String.Empty

            ' CREATETABLE
            Divide(incoming, "@@", strRules, strComment, strDecks)

            'CREATETABLE CHINCHIN
            Dim iCutOff As Integer = InStr(1, incoming, "@@") - 13
            strRules = Trim(Mid$(incoming, 13, incoming.Length - (incoming.Length - InStr(1, incoming, "@@"))))

            Dim tablenum As Integer = oTables.FindEmptyTable(Tables)

            If InStr(1, incoming, "CHINCHIN") > 0 Then
                If oPlayerAccount.AP < 3 Then
                    Call blockchat(String.Empty, "Failed", "You cannot play this game with less than 3AP", socket)
                    Send(String.Empty, "TRADEEXIT", socket)
                    Exit Sub
                End If
            End If

            If tablenum = oServerConfig.MaxTables Then
                Send(String.Empty, "NOTABLE", socket)
                Exit Sub
            End If

            If InStr(1, strRules, "TRADE") > 0 Then strRules = "Trade"

            If oServerConfig.AllowGames = False Then
                Call blockchat(String.Empty, "Lock", "The server operator has games suspended.  You may not create a game at this time.", socket)
                Send(String.Empty, "TRADEEXIT", socket)
                Exit Sub
            End If

            If strRules <> "Trade" Then
                If totalcardsnum(frmSystray.Winsock(socket).Tag) < 5 Then
                    Call blockchat(String.Empty, "Lock", "You cannot create a table because you have less than 5 cards.", socket)
                    Send(String.Empty, "TRADEEXIT", socket)
                    Exit Sub
                End If
            End If

            'If strRules = "Trade" Then
            '    If frmserver.chkTrades.Checked = False Then
            '        Call blockchat(String.Empty, "Lock", "Trades have been disabled by the administrator", socket)
            '        Send(String.Empty, "TRADEEXIT", socket)
            '        Exit Sub
            '    End If

            '    If CDate(DateString) < oPlayerAccount.LastNewDeck.AddDays(3) Then
            '        Call blockchat(String.Empty, "Lock", "You have gotten a newdeck or created an account within the last 3 days.  You may not trade or sell cards until this 3 day lock has expired", socket)
            '        Send(String.Empty, "TRADEEXIT", socket)
            '        Exit Sub
            '    End If

            '    Tables(tablenum).Player1 = frmSystray.Winsock(socket).Tag
            '    Tables(tablenum).Player2 = String.Empty
            '    Tables(tablenum).Player3 = String.Empty
            '    Tables(tablenum).Player4 = String.Empty
            '    Tables(tablenum).Player_Ready(1) = False
            '    Tables(tablenum).Player_Ready(2) = False
            '    Tables(tablenum).Player_Ready(3) = False
            '    Tables(tablenum).Player_Ready(4) = False
            '    Tables(tablenum).RuleList = "Trade"
            '    Tables(tablenum).Code_X = "Trade"
            '    Tables(tablenum).Comment = "Trade"
            '    Tables(tablenum).Stalemate1 = False
            '    Tables(tablenum).Stalemate2 = False
            '    Tables(tablenum).Decks = String.Empty
            '    Tables(tablenum).GameID = 0

            '    strRules = Tables(tablenum).RuleList.Replace(" ", "%20")

            '    oTables.SendTable(Tables, tablenum)
            '    'Call sendall(String.Concat("ADDTABLE ", tablenum, " ", Tables(tablenum).Player1, " ", Tables(tablenum).Player2, " ", Tables(tablenum).RuleList, " @@", Tables(tablenum).Comment, "@@", Tables(tablenum).Decks))

            '    Tables(tablenum).Lock = True
            '    Exit Sub
            'End If

            If InStr(1, incoming, "MEMORY") > 0 Then
                Tables(tablenum).Player1 = frmSystray.Winsock(socket).Tag
                Tables(tablenum).Player2 = String.Empty
                Tables(tablenum).Player3 = String.Empty
                Tables(tablenum).Player4 = String.Empty
                Tables(tablenum).Player_Ready(1) = False
                Tables(tablenum).Player_Ready(2) = False
                Tables(tablenum).Player_Ready(3) = False
                Tables(tablenum).Player_Ready(4) = False
                Tables(tablenum).RuleList = "MEMORY"
                Tables(tablenum).Code_X = Tables(tablenum).RuleList
                Tables(tablenum).Comment = "Triple Triad Memory"
                Tables(tablenum).Stalemate1 = False
                Tables(tablenum).Stalemate2 = False
                Tables(tablenum).Decks = "Standard"
                Tables(tablenum).GameID = 0
            ElseIf InStr(1, incoming, "CARDWARS") > 0 Then
                Tables(tablenum).Player1 = frmSystray.Winsock(socket).Tag
                Tables(tablenum).Player2 = String.Empty
                Tables(tablenum).Player3 = String.Empty
                Tables(tablenum).Player4 = String.Empty
                Tables(tablenum).Player_Ready(1) = False
                Tables(tablenum).Player_Ready(2) = False
                Tables(tablenum).Player_Ready(3) = False
                Tables(tablenum).Player_Ready(4) = False
                Tables(tablenum).RuleList = "CARDWARS"
                Tables(tablenum).Code_X = Tables(tablenum).RuleList
                Tables(tablenum).Comment = "Triple Triad War"
                Tables(tablenum).Stalemate1 = False
                Tables(tablenum).Stalemate2 = False
                Tables(tablenum).Decks = "Standard"
                Tables(tablenum).GameID = 0
            ElseIf InStr(1, incoming, "CHINCHIN") > 0 Then
                Tables(tablenum).Player1 = frmSystray.Winsock(socket).Tag
                Tables(tablenum).Player2 = String.Empty
                Tables(tablenum).Player3 = String.Empty
                Tables(tablenum).Player4 = String.Empty
                Tables(tablenum).Player_Ready(1) = False
                Tables(tablenum).Player_Ready(2) = False
                Tables(tablenum).Player_Ready(3) = False
                Tables(tablenum).Player_Ready(4) = False
                Tables(tablenum).RuleList = "CHINCHIN 1 0"
                Tables(tablenum).Code_X = Tables(tablenum).RuleList
                Tables(tablenum).Comment = "Chinchirorin"
                Tables(tablenum).Stalemate1 = False
                Tables(tablenum).Stalemate2 = False
                Tables(tablenum).Decks = String.Empty
                Tables(tablenum).GameID = 0
            Else
                Tables(tablenum).Player1 = frmSystray.Winsock(socket).Tag
                Tables(tablenum).Player2 = String.Empty
                Tables(tablenum).Player3 = String.Empty
                Tables(tablenum).Player4 = String.Empty
                Tables(tablenum).Player_Ready(1) = False
                Tables(tablenum).Player_Ready(2) = False
                Tables(tablenum).Player_Ready(3) = False
                Tables(tablenum).Player_Ready(4) = False
                Tables(tablenum).RuleList = "Basic Random 1 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0"
                Tables(tablenum).Code_X = Tables(tablenum).RuleList
                Tables(tablenum).Comment = "Triple Triad"
                Tables(tablenum).Stalemate1 = False
                Tables(tablenum).Stalemate2 = False
                Tables(tablenum).Decks = "Standard"
                Tables(tablenum).GameID = 0
            End If

            strRules = Tables(tablenum).RuleList.Replace(" ", "%20")

            oTables.SendTable(Tables, tablenum)
            'Call sendall(String.Concat("ADDTABLE ", tablenum, " ", Tables(tablenum).Player1, " ", Tables(tablenum).Player2, " ", Tables(tablenum).RuleList, " @@", Tables(tablenum).Comment, "@@", Tables(tablenum).Decks))
            Send(String.Empty, String.Concat("CHALLENGEBOXS ", tablenum, " ", Tables(tablenum).Player1, " ", Tables(tablenum).Player2, " ", Tables(tablenum).GoldWager, " ", strRules, "@@", Tables(tablenum).Decks), socket)

            Send(String.Empty, String.Concat("CHALLENGEAVATARS 1 ", oPlayerAccount.Background, " ", oPlayerAccount.Head, " ", oPlayerAccount.Body, " ", oPlayerAccount.Gender), socket)
        Catch ex As Exception
            Call errorsub(ex.ToString, "createtable")
        End Try
    End Sub

    Private Sub tablecreate2(ByVal incoming As String, ByVal socket As Integer)
        Dim strRules As String = String.Empty, lostvar As String = String.Empty
        Dim strExtraRules As String = String.Empty
        Dim oTables As New BusinessLayer.Tables

        If oTables.FindTablePlayer(Tables, frmSystray.Winsock(socket).Tag) = True Then
            Send(String.Empty, "CANCELBOX", socket)
            Exit Sub
        End If

        ' CREATETABLE
        Divide(incoming, " ", lostvar, strRules, strExtraRules)

        Dim strComment As String = Mid(incoming, InStr(1, incoming, "@@") + 2, Len(incoming))

        Dim tablenum As Byte = oTables.FindEmptyTable(Tables)

        If tablenum = oServerConfig.MaxTables Then
            Send(String.Empty, "NOTABLE", socket)
            Exit Sub
        End If

        Select Case strRules
            Case "OMNITT_RANDOM"

            Case "OMNITT_PLAYER"
                If totalcardsnum(frmSystray.Winsock(socket).Tag) < 15 Then
                    Call blockchat(String.Empty, "Lock", "You have less than 15 cards.  You cannot play an Omni-TT game with player deck enforced with less than 25 cards.", socket)
                    Send(String.Empty, "TRADEEXIT", socket)
                    Exit Sub
                End If
        End Select

        If oServerConfig.AllowGames = False Then
            Call blockchat(String.Empty, "Lock", "The server operator has games suspended.  You may not create a game at this time.", socket)
            Send(String.Empty, "TRADEEXIT", socket)
            Exit Sub
        End If

        If Val(strExtraRules) > 7 Then
            Call blockchat(String.Empty, "Lock", "Cheating is bad, mmkay?", socket)
            Exit Sub
        End If

        With Tables(tablenum)
            .Player1 = frmSystray.Winsock(socket).Tag
            .Player2 = String.Empty
            .RuleList = strRules
            .Code_X = strRules
            .Comment = "Omni Triple Triad"
            .Stalemate1 = False
            .Stalemate2 = False
            .Decks = String.Empty
            .Blocks = strExtraRules
            .GameID = 0
        End With

        strRules = Tables(tablenum).RuleList.Replace(" ", "%20")

        oTables.SendTable(Tables, tablenum)
        'Call sendall(String.Concat("ADDTABLE ", tablenum, " ", Tables(tablenum).Player1, " " & Tables(tablenum).Player2, " " & Tables(tablenum).RuleList, " @@", Tables(tablenum).Comment, "@@" & Tables(tablenum).Decks, " ", Tables(tablenum).Blocks))
        Send(String.Empty, String.Concat("CHALLENGEBOXS ", tablenum, " ", Tables(tablenum).Player1, " " & Tables(tablenum).Player2, " ", Tables(tablenum).GoldWager, " ", strRules.Trim, "%20", Tables(tablenum).Blocks), socket)

        Dim oPlayerAccount As New PlayerAccountDAL(frmSystray.Winsock(socket).Tag)

        If oPlayerAccount.ErrorFlag = False Then Send(String.Empty, String.Concat("CHALLENGEAVATARS 1 ", oPlayerAccount.Background, " ", oPlayerAccount.Head, " ", oPlayerAccount.Body, " ", oPlayerAccount.Gender), socket)
    End Sub

    Private Sub tablecreate3(ByVal incoming As String, ByVal socket As Integer)
        Try
            Dim oTables As New BusinessLayer.Tables
            Dim lostvar As String = String.Empty, strScore As String = String.Empty, strTime As String = String.Empty, strTurns As String = String.Empty

            If oTables.FindTablePlayer(Tables, frmSystray.Winsock(socket).Tag) = True Then
                Send(String.Empty, "CANCELBOX", socket)
                Exit Sub
            End If

            Divide(incoming, " ", lostvar, strTime, strScore, strTurns)

            Dim strComment As String = "Sphere Break"
            Dim tablenum As Integer = oTables.FindEmptyTable(Tables)

            If tablenum = oServerConfig.MaxTables Then
                Send(String.Empty, "NOTABLE", socket)
                Exit Sub
            End If

            If oServerConfig.AllowGames = False Then
                Call blockchat(String.Empty, "Lock", "The server operator has games suspended.  You may not create a game at this time.", socket)
                Send(String.Empty, "TRADEEXIT", socket)
                Exit Sub
            End If

            If strTime = String.Empty Then strTime = "60"
            If strScore = String.Empty Then strScore = "50"
            If strTurns = String.Empty Then strTurns = "20"

            Tables(tablenum).Player1 = frmSystray.Winsock(socket).Tag
            Tables(tablenum).Player2 = String.Empty
            Tables(tablenum).RuleList = String.Concat("SPHEREBREAK ", strTime, " ", strScore, " ", strTurns)
            Tables(tablenum).Code_X = String.Empty
            Tables(tablenum).Comment = "Sphere Break"
            Tables(tablenum).Stalemate1 = False
            Tables(tablenum).Stalemate2 = False
            Tables(tablenum).Decks = String.Empty
            Tables(tablenum).Blocks = String.Empty
            Tables(tablenum).GameID = 0

            Dim strRules As String = Tables(tablenum).RuleList.Replace(" ", "%20")

            oTables.SendTable(Tables, tablenum)
            'Call sendall(String.Concat("ADDTABLE ", tablenum, " ", Tables(tablenum).Player1, " ", Tables(tablenum).Player2, " ", Tables(tablenum).RuleList, " @@", Tables(tablenum).Comment, "@@", Tables(tablenum).Decks))
            Send(String.Empty, String.Concat("CHALLENGEBOXS ", tablenum, " ", Tables(tablenum).Player1, " ", Tables(tablenum).Player2, " ", Tables(tablenum).GoldWager, " ", strRules, "@@", Tables(tablenum).Decks), socket)

            Dim oPlayerAccount As New PlayerAccountDAL(frmSystray.Winsock(socket).Tag)

            If oPlayerAccount.ErrorFlag = False Then Send(String.Empty, String.Concat("CHALLENGEAVATARS 1 ", oPlayerAccount.Background, " ", oPlayerAccount.Head, " ", oPlayerAccount.Body, " ", oPlayerAccount.Gender), socket)
        Catch ex As Exception
            Call errorsub(ex.ToString, "createtable3")
        End Try
    End Sub

    Private Sub tablecreate4(ByVal incoming As String, ByVal socket As Integer)
        Dim strComment As String = String.Empty, lostvar As String = String.Empty
        Dim oTables As New BusinessLayer.Tables

        If oTables.FindTablePlayer(Tables, frmSystray.Winsock(socket).Tag) = True Then
            Send(String.Empty, "CANCELBOX", socket)
            Exit Sub
        End If

        Dim tablenum As Integer = oTables.FindEmptyTable(Tables)

        If tablenum = oServerConfig.MaxTables Then
            Send(String.Empty, "NOTABLE", socket)
            Exit Sub
        End If

        If oServerConfig.AllowGames = False Then
            Call blockchat(String.Empty, "Lock", "The server operator has games suspended.  You may not create a game at this time.", socket)
            Send(String.Empty, "TRADEEXIT", socket)
            Exit Sub
        End If

        Tables(tablenum).Player1 = frmSystray.Winsock(socket).Tag
        Tables(tablenum).Player2 = String.Empty
        Tables(tablenum).Player3 = String.Empty
        Tables(tablenum).Player4 = String.Empty
        Tables(tablenum).RuleList = "OMNITT_RANDOM"
        Tables(tablenum).Code_X = "OMNITT_PLAYER"
        Tables(tablenum).Comment = "Team Omni Triple Triad"

        For x As Integer = 1 To 4
            Tables(tablenum).Player_Ready(x) = False
        Next x

        Tables(tablenum).Stalemate1 = False
        Tables(tablenum).Stalemate2 = False
        Tables(tablenum).Decks = String.Empty
        Tables(tablenum).Blocks = "5"
        Tables(tablenum).GameID = 0

        oTables.SendTable(Tables, tablenum)
        'Call sendall(String.Concat("ADDTABLE ", tablenum, " ", Tables(tablenum).Player1, " ", Tables(tablenum).Player2, " ", Tables(tablenum).RuleList, " @@", Tables(tablenum).Comment, "@@", Tables(tablenum).Decks, "@@", Tables(tablenum).Blocks))
        Send(String.Empty, String.Concat("CHALLENGEBOX ", tablenum, " ", Tables(tablenum).Player1, " ", Tables(tablenum).Player2, " ", Tables(tablenum).Player3, " ", Tables(tablenum).Player4, " ", Tables(tablenum).RuleList, " ", Tables(tablenum).Blocks), socket)

        Dim oPlayerAccount As New PlayerAccountDAL(frmSystray.Winsock(socket).Tag)

        If oPlayerAccount.ErrorFlag = False Then Send(String.Empty, String.Concat("CHALLENGEAVATAR 1 ", oPlayerAccount.Background, " ", oPlayerAccount.Head, " ", oPlayerAccount.Body, " ", oPlayerAccount.Gender), socket)
    End Sub

    Private Sub tablejoin(ByVal incoming As String, ByVal socket As Integer)
        Dim lostvar As String = String.Empty, tableID As String = String.Empty
        Dim oTables As New BusinessLayer.Tables

        Divide(incoming, " ", lostvar, tableID)

        If frmSystray.Winsock(socket).Tag = String.Empty Then Exit Sub

        If oTables.FindTablePlayer(Tables, frmSystray.Winsock(socket).Tag) = True Then Exit Sub

        Dim oPlayerAccount As New PlayerAccountDAL(frmSystray.Winsock(socket).Tag)
        Dim oOpponentAccount As New PlayerAccountDAL(Tables(Val(tableID)).Player1)

        'If frmserver.chkdebug.CheckState = 1 Then
        '    If oPlayerAccount.LastIP = oOpponentAccount.LastIP Then
        '        Call blockchat(String.Empty, "Lock", "The server op has games locked for those wanting to play with people with the same IP as you.", socket)
        '        Send(String.Empty, "TRADEEXIT", socket)
        '        Exit Sub
        '    End If
        'End If

        If Tables(Val(tableID)).Player1 <> String.Empty And Tables(Val(tableID)).Player2 <> String.Empty And Left(Tables(Val(tableID)).Comment, 4) <> "Team" Then
            Call blockchat(String.Empty, "Table", "This table is full", socket)
            Exit Sub
        End If

        If Tables(Val(tableID)).Lock = True Then
            If Date.Today < oPlayerAccount.SignupDate.AddDays(3) Then
                Call blockchat(String.Empty, "Lock", "You have gotten a newdeck or created an account within the last 3 days.  You may not play trade all or trade four games until this has expired.", socket)
                Send(String.Empty, "TRADEEXIT", socket)
                Exit Sub
            End If
        End If

        If (Tables(Val(tableID)).RuleList <> "Trade") And lostvar <> "SPHEREBREAK" Then
            If totalcardsnum(frmSystray.Winsock(socket).Tag) < 5 Then
                Call blockchat(String.Empty, "Lock", "You have less than 4 cards.  You cannot play a 5 card game with 4 cards!", socket)
                Send(String.Empty, "TRADEEXIT", socket)
                Exit Sub
            End If
        End If

        If (Tables(Val(tableID)).RuleList = "Trade") Then
            If oPlayerAccount.LastIP = oOpponentAccount.LastIP Then
                Call blockchat(String.Empty, "Lock", "Users with the same IP may not trade with each other.", socket)
                Send(String.Empty, "TRADEEXIT", socket)
                Exit Sub
            End If
        End If

        If Tables(Val(tableID)).Wager > 0 Then
            If oPlayerAccount.Gold < Tables(Val(tableID)).Wager Then
                Call blockchat(String.Empty, "Lock", "You cannot join a game in which you have less GP than what is wagered", socket)
                Send(String.Empty, "TRADEEXIT", socket)
                Exit Sub
            End If
        End If

        If Tables(Val(tableID)).Comment = "Team Omni Triple Triad" Then
            Call jointeamtable(incoming, socket)
            Exit Sub
        End If

        ' Random Deck - Omni Triple Triad
        If Tables(Val(tableID)).RuleList = "OMNITT_RANDOM" Or Tables(Val(tableID)).RuleList = "OMNITT_PLAYER" Then
            If Tables(Val(tableID)).RuleList = "OMNITT_PLAYER" Then
                If totalottcards(frmSystray.Winsock(socket).Tag) < 15 Then
                    Call blockchat(String.Empty, "Lock", "You do not have enough cards to play with the player deck version of OTT", socket)
                    Exit Sub
                End If
            End If

            If Tables(Val(tableID)).Player1 <> String.Empty And Tables(Val(tableID)).Player2 = String.Empty Then
                Call joinstable(incoming, socket)
                Exit Sub
            End If
        End If

        If InStr(1, Tables(Val(tableID)).RuleList, "SPHEREBREAK") > 0 Then
            Call joinstable(incoming, socket) ' send to single 1v1 table function
            Exit Sub
        End If

        If Tables(Val(tableID)).RuleList <> "Trade" Then
            Call joinstable(incoming, socket) ' send to single 1v1 table function
        End If
    End Sub

    Private Sub jointeamtable(ByVal incoming As String, ByVal socket As Integer)
        Try
            Dim lostvar As String = String.Empty, tableID As String = String.Empty, strPlayer(4) As String

            Dim intPlayer As Integer = 0
            Dim oPlayerAccount As New PlayerAccountDAL(frmSystray.Winsock(socket).Tag)

            If oPlayerAccount.ErrorFlag = True Then
                Call blockchat(String.Empty, "Error", "An error occured.  You cannot join this table.  Please try again later.", socket)
                Exit Sub
            End If

            Divide(incoming, " ", lostvar, tableID)

            Dim tablenum As Integer = Val(tableID)

            If Tables(tablenum).Player2 = String.Empty Then
                Tables(tablenum).Player2 = oPlayerAccount.Player
                Tables(tablenum).Player_Ready(2) = False
                intPlayer = 2
            ElseIf Tables(tablenum).Player3 = String.Empty Then
                Tables(tablenum).Player3 = oPlayerAccount.Player
                Tables(tablenum).Player_Ready(3) = False
                intPlayer = 3
            ElseIf Tables(tablenum).Player4 = String.Empty Then
                Tables(tablenum).Player4 = oPlayerAccount.Player
                Tables(tablenum).Player_Ready(4) = False
                intPlayer = 4
            Else
                Call blockchat(String.Empty, "Full", "The table is full.  Try again later", socket)
                Exit Sub
            End If

            ' Update the challenge boxes
            If Tables(tablenum).Player1 <> String.Empty Then Send(Tables(tablenum).Player1, String.Concat("CHALLENGEBOX ", tablenum, " ", Tables(tablenum).Player1, " ", Tables(tablenum).Player2, " ", Tables(tablenum).Player3, " ", Tables(tablenum).Player4, " ", Tables(tablenum).RuleList, " ", Tables(tablenum).Blocks))
            If Tables(tablenum).Player2 <> String.Empty Then Send(Tables(tablenum).Player2, String.Concat("CHALLENGEBOX ", tablenum, " ", Tables(tablenum).Player1, " ", Tables(tablenum).Player2, " ", Tables(tablenum).Player3, " ", Tables(tablenum).Player4, " ", Tables(tablenum).RuleList, " ", Tables(tablenum).Blocks))
            If Tables(tablenum).Player3 <> String.Empty Then Send(Tables(tablenum).Player3, String.Concat("CHALLENGEBOX ", tablenum, " ", Tables(tablenum).Player1, " ", Tables(tablenum).Player2, " ", Tables(tablenum).Player3, " ", Tables(tablenum).Player4, " ", Tables(tablenum).RuleList, " ", Tables(tablenum).Blocks))
            If Tables(tablenum).Player4 <> String.Empty Then Send(Tables(tablenum).Player4, String.Concat("CHALLENGEBOX ", tablenum, " ", Tables(tablenum).Player1, " ", Tables(tablenum).Player2, " ", Tables(tablenum).Player3, " ", Tables(tablenum).Player4, " ", Tables(tablenum).RuleList, " ", Tables(tablenum).Blocks))

            strPlayer(1) = Tables(tablenum).Player1
            strPlayer(2) = Tables(tablenum).Player2
            strPlayer(3) = Tables(tablenum).Player3
            strPlayer(4) = Tables(tablenum).Player4

            Dim oPlayer1Account As New PlayerAccountDAL(Tables(tablenum).Player1)
            Dim oPlayer2Account As New PlayerAccountDAL(Tables(tablenum).Player2)
            Dim oPlayer3Account As New PlayerAccountDAL(Tables(tablenum).Player3)
            Dim oPlayer4Account As New PlayerAccountDAL(Tables(tablenum).Player4)

            For x As Integer = 1 To 4
                If strPlayer(x) <> String.Empty Then
                    If strPlayer(1) <> String.Empty Then Send(strPlayer(x), String.Concat("CHALLENGEAVATAR 1 ", oPlayer1Account.Background, " ", oPlayer1Account.Head, " ", oPlayer1Account.Body, " ", oPlayer1Account.Gender))
                    If strPlayer(2) <> String.Empty Then Send(strPlayer(x), String.Concat("CHALLENGEAVATAR 2 ", oPlayer2Account.Background, " ", oPlayer2Account.Head, " ", oPlayer2Account.Body, " ", oPlayer2Account.Gender))
                    If strPlayer(3) <> String.Empty Then Send(strPlayer(x), String.Concat("CHALLENGEAVATAR 3 ", oPlayer3Account.Background, " ", oPlayer3Account.Head, " ", oPlayer3Account.Body, " ", oPlayer3Account.Gender))
                    If strPlayer(4) <> String.Empty Then Send(strPlayer(x), String.Concat("CHALLENGEAVATAR 4 ", oPlayer4Account.Background, " ", oPlayer4Account.Head, " ", oPlayer4Account.Body, " ", oPlayer4Account.Gender))
                Else
                    For y As Integer = 1 To 4
                        If strPlayer(y) <> String.Empty Then Send(strPlayer(y), String.Concat("CHALLENGEAVATAR ", x))
                    Next y
                End If
            Next x

            Send(String.Empty, String.Concat("CHALLENGEREADY 1 ", System.Math.Abs(CInt(Tables(tablenum).Player_Ready(1)))), socket)
            Send(String.Empty, String.Concat("CHALLENGEREADY 2 ", System.Math.Abs(CInt(Tables(tablenum).Player_Ready(2)))), socket)
            Send(String.Empty, String.Concat("CHALLENGEREADY 3 ", System.Math.Abs(CInt(Tables(tablenum).Player_Ready(3)))), socket)
            Send(String.Empty, String.Concat("CHALLENGEREADY 4 ", System.Math.Abs(CInt(Tables(tablenum).Player_Ready(4)))), socket)
        Catch ex As Exception
            Call errorsub(ex.ToString, "jointeamtable")
            Call blockchat(String.Empty, "Error", "An error occured.  You cannot join this table.  Please try again later.", socket)
        End Try
    End Sub

    Private Sub joinstable(ByVal incoming As String, ByVal socket As Integer)
        Try
            Dim lostvar As String = String.Empty, tableID As String = String.Empty
            Dim intPlayer As Integer
            Dim strPlayer(2) As String

            Dim oTables As New BusinessLayer.Tables
            Dim oPlayerAccount As New PlayerAccountDAL(frmSystray.Winsock(socket).Tag)

            If oPlayerAccount.ErrorFlag = True Then
                Call blockchat(String.Empty, "Error", "An error occured.  You cannot join this table.  Please try again later.", socket)
                Exit Sub
            End If

            Divide(incoming, " ", lostvar, tableID)

            Dim tablenum As Integer = CInt(tableID)

            If Tables(tablenum).Player2 = String.Empty Then
                Tables(tablenum).Player2 = oPlayerAccount.Player
                Tables(tablenum).Player_Ready(2) = False
                intPlayer = 2
            Else
                Call blockchat(String.Empty, "Full", "The table is full.  Try again later", socket)
                Exit Sub
            End If

            Dim strRules As String = Tables(tablenum).RuleList
            Dim strDecks As String = Tables(tablenum).Decks

            If InStr(1, strRules, "OMNITT") > 0 Then strDecks = Tables(tablenum).Blocks

            strRules = strRules.Replace(" ", "%20")
            strDecks = strDecks.Replace(" ", "%20")

            ' Update the challenge boxes
            If InStr(1, strRules, "OMNITT") = 0 Then
                If Tables(tablenum).Player1 <> String.Empty Then Send(Tables(tablenum).Player1, String.Concat("CHALLENGEBOXS ", tablenum, " ", Tables(tablenum).Player1, " ", Tables(tablenum).Player2, " ", Tables(tablenum).GoldWager, " ", strRules, "@@", strDecks))
                If Tables(tablenum).Player2 <> String.Empty Then Send(Tables(tablenum).Player2, String.Concat("CHALLENGEBOXS ", tablenum, " ", Tables(tablenum).Player1, " ", Tables(tablenum).Player2, " ", Tables(tablenum).GoldWager, " ", strRules, "@@", strDecks))
            Else
                If Tables(tablenum).Player1 <> String.Empty Then Send(Tables(tablenum).Player1, String.Concat("CHALLENGEBOXS ", tablenum, " ", Tables(tablenum).Player1, " ", Tables(tablenum).Player2, " ", Tables(tablenum).GoldWager, " ", strRules, "@@", strDecks))
                If Tables(tablenum).Player2 <> String.Empty Then Send(Tables(tablenum).Player2, String.Concat("CHALLENGEBOXS ", tablenum, " ", Tables(tablenum).Player1, " ", Tables(tablenum).Player2, " ", Tables(tablenum).GoldWager, " ", strRules, "@@", strDecks))
            End If

            strPlayer(1) = Tables(tablenum).Player1
            strPlayer(2) = Tables(tablenum).Player2

            Dim oAccount As New PlayerAccountDAL(Tables(tablenum).Player1)
            Dim oAccount2 As New PlayerAccountDAL(Tables(tablenum).Player2)

            'Send info to player 1
            Send(strPlayer(1), String.Concat("CHALLENGEAVATARS ", 1, " ", oAccount.Background, " ", oAccount.Head, " ", oAccount.Body, " ", oAccount.Gender))
            Send(strPlayer(1), String.Concat("CHALLENGEAVATARS ", 2, " ", oAccount2.Background, " ", oAccount2.Head, " ", oAccount2.Body, " ", oAccount2.Gender))

            'Send info to player 2
            Send(strPlayer(2), String.Concat("CHALLENGEAVATARS ", 1, " ", oAccount.Background, " ", oAccount.Head, " ", oAccount.Body, " ", oAccount.Gender))
            Send(strPlayer(2), String.Concat("CHALLENGEAVATARS ", 2, " ", oAccount2.Background, " ", oAccount2.Head, " ", oAccount2.Body, " ", oAccount2.Gender))

            Send(String.Empty, String.Concat("CHALLENGEREADYS 1 ", System.Math.Abs(CInt(Tables(tablenum).Player_Ready(1)))), socket)
            Send(String.Empty, String.Concat("CHALLENGEREADYS 2 ", System.Math.Abs(CInt(Tables(tablenum).Player_Ready(2)))), socket)

            oTables.SendTable(Tables, tablenum)
            'Call sendall(String.Concat("ADDTABLE ", tablenum, " ", Tables(tablenum).Player1, " ", Tables(tablenum).Player2, " ", Tables(tablenum).RuleList, " @@", Tables(tablenum).Comment, "@@", Tables(tablenum).Decks))
        Catch ex As Exception
            Call errorsub(ex.ToString, "joinstable")
            Call blockchat(String.Empty, "Error", "An error occured.  You cannot join this table.  Please try again later.", socket)
        End Try
    End Sub

    Private Sub tablequery(ByVal incoming As String, ByVal socket As Integer)
        For q As Integer = 0 To oServerConfig.MaxTables - 1
            If Not (Tables(q).Player1 = String.Empty And Tables(q).Player2 = String.Empty And Tables(q).Player3 = String.Empty And Tables(q).Player4 = String.Empty) Then
                If Tables(q).Comment = "Team Omni Triple Triad" Then
                    If Tables(q).Player1 = String.Empty Or Tables(q).Player2 = String.Empty Or Tables(q).Player3 = String.Empty Or Tables(q).Player4 = String.Empty Then
                        Send(String.Empty, String.Concat("ADDTABLE ", q, " ", Tables(q).Player1, " {JOIN}", " ", Tables(q).RuleList, "@@", Tables(q).Comment, "@@", Tables(q).Decks), socket)
                    ElseIf Tables(q).Player1 <> String.Empty Or Tables(q).Player2 <> String.Empty Or Tables(q).Player3 <> String.Empty Or Tables(q).Player4 <> String.Empty Then
                        Send(String.Empty, String.Concat("ADDTABLE ", q, " ", Tables(q).Player1, " |FULL| ", Tables(q).RuleList, "@@", Tables(q).Comment, "@@", Tables(q).Decks), socket)
                    End If
                Else
                    Send(String.Empty, String.Concat("ADDTABLE ", q, " ", Tables(q).Player1, " ", Tables(q).Player2, " ", Tables(q).RuleList, "@@", Tables(q).Comment, "@@", Tables(q).Decks), socket)
                End If
            End If
        Next q
    End Sub

    Private Sub tableinfo(ByVal incoming As String, ByVal socket As Integer)
        Dim vars As String() = incoming.Split(" ")
        Dim iTableID As Integer = Integer.Parse(vars(1))

        Dim oTables As New BusinessLayer.Tables
        oTables.SendtoPlayer(Tables, iTableID, socket)
    End Sub
End Class
