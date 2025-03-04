Module modEndofGames
    Public Sub endmatch(ByRef incoming As String, ByRef socket As Integer, Optional ByRef blnEnd As Boolean = False)
        On Error GoTo errhandler
        Dim opponent As String = "", lostvar As String = "", enick As String = "", gameidfile As String = ""
        Dim strStatus As String
        Dim blnFound As Boolean
        Dim gameid, q As Integer

        Divide(incoming, " ", lostvar, enick)

        'If Readini("Play", "Playing", App.Path & "\accounts\" & enick & ".ttd") = "true" Or Readini("Play", "Playing", App.Path & "\accounts\" & enick & ".ttd") = "True" Then

        strStatus = getAdministrator_Field(frmSystray.Winsock(socket).Tag, "status")

        If strStatus <> "0" Or blnEnd = True Then

            blnFound = False
            For q = 0 To 254
                If (tables(q).player1) = (enick) Or tables(q).player2 = enick Or tables(q).player3 = enick Or tables(q).player4 = enick Then
                    'If (tables(q).player1) = (enick) Then
                    Call killtable("KILLTABLE " & q, socket)
                    blnFound = True
                    Exit For
                End If
            Next q

            'Call unichat(socket, "End Match by: " & frmSystray.Winsock(socket).Tag & " -> to end a game played by " & opponent & " and " & enick)
            matchesend = matchesend + 1
            'Call writestats("Kills", CStr(1))
        End If

        'Call killgamestat(gameid)

        Exit Sub
errhandler:
        Call ErrorSub(Err.Description, "endmatch")
    End Sub

    Public Sub write_teamottgamefile(ByRef gameid As Integer)
        Dim MyQuery, gamefile As String
        Dim x As Integer

        On Error GoTo errhandler

        gamefile = My.Application.Info.DirectoryPath & "\gamefiles\" & gameid & ".tgf"

        MyQuery = "call usp_WriteTeamOmniTripleTriadGame('" & gameid & "'"
        compilequery(MyQuery, Format(Date.Now, "MM-dd-yyyy"), False)
        compilequery(MyQuery, Readini("Gameinfo", "Player1", gamefile), False)
        compilequery(MyQuery, Readini("Gameinfo", "Player2", gamefile), False)
        compilequery(MyQuery, Readini("Gameinfo", "Player3", gamefile), False)
        compilequery(MyQuery, Readini("Gameinfo", "Player4", gamefile), False)

        If Readini("Rules", "Random", gamefile) = "True" Then
            compilequery(MyQuery, "1", False)
        Else
            compilequery(MyQuery, "0", False)
        End If

        For x = 0 To 24
            compilequery(MyQuery, Readini("Board", x & "-Name", gamefile), False)
        Next

        For x = 0 To 24
            compilequery(MyQuery, Readini("Board", x & "-Color", gamefile), False)
        Next

        compilequery(MyQuery, Readini("Score", "player1", gamefile), False)
        compilequery(MyQuery, Readini("Score", "player2", gamefile), False)

        MyQuery = MyQuery & ")"

        Console.WriteLine(MyQuery)
        Call dbEXECQuery(MyQuery)

        Exit Sub

errhandler:
        Call errorsub(Err.Description, "write_teamottgamefile")
    End Sub

    Public Sub compilequery(ByRef MyQuery As String, ByVal sItem As String, ByVal blnInt As Boolean)
        If blnInt = True Then
            MyQuery = MyQuery & ", " & sItem
        Else
            MyQuery = MyQuery & ", '" & sItem & "'"
        End If
    End Sub

    Public Sub write_gamefile(ByRef gameid As Integer)
        Dim MyQuery, gamefile As String
        Dim x As Integer, y As Integer

        On Error GoTo errhandler

        gamefile = My.Application.Info.DirectoryPath & "\gamefiles\" & gameid & ".tgf"

        MyQuery = "call usp_WriteTripleTriadGame('" & gameid & "'"

        compilequery(MyQuery, Readini("Gameinfo", "TT", gamefile), False)
        compilequery(MyQuery, Format(Date.Now, "MM-dd-yyyy"), False)
        compilequery(MyQuery, Readini("Gameinfo", "Player1", gamefile), False)
        compilequery(MyQuery, Readini("Gameinfo", "Player2", gamefile), False)
        compilequery(MyQuery, Readini("Gameinfo", "Winner", gamefile), False)
        compilequery(MyQuery, Readini("Rules", "Open", gamefile), False)
        compilequery(MyQuery, Readini("Rules", "Random", gamefile), False)
        compilequery(MyQuery, Readini("Rules", "Proom", gamefile), False)
        compilequery(MyQuery, Readini("Rules", "TradeAll", gamefile), False)
        compilequery(MyQuery, Readini("Rules", "TradeDiff", gamefile), False)
        compilequery(MyQuery, Readini("Rules", "TradeTwo", gamefile), False)
        compilequery(MyQuery, Readini("Rules", "TradeThree", gamefile), False)
        compilequery(MyQuery, Readini("Rules", "TradeFour", gamefile), False)
        compilequery(MyQuery, Readini("Rules", "Cardstotake", gamefile), False)
        compilequery(MyQuery, Readini("Rules", "Capture", gamefile), False)
        compilequery(MyQuery, Readini("Rules", "Direct", gamefile), False)
        compilequery(MyQuery, Readini("Rules", "Maxrule", gamefile), False)
        compilequery(MyQuery, Readini("Rules", "Maxlevel", gamefile), False)
        compilequery(MyQuery, Readini("Rules", "CCRule", gamefile), False)
        compilequery(MyQuery, Readini("Rules", "Same", gamefile), False)
        compilequery(MyQuery, Readini("Rules", "Plus", gamefile), False)
        compilequery(MyQuery, Readini("Rules", "Traditional", gamefile), False)
        compilequery(MyQuery, Readini("Rules", "Minrule", gamefile), False)
        compilequery(MyQuery, Readini("Rules", "Minlevel", gamefile), False)
        compilequery(MyQuery, Readini("Rules", "Wager", gamefile), False)
        compilequery(MyQuery, Readini("Rules", "Wageramt", gamefile), False)
        compilequery(MyQuery, Readini("Rules", "Wall", gamefile), False)
        compilequery(MyQuery, Readini("Rules", "Combo", gamefile), False)
        compilequery(MyQuery, Readini("Rules", "Mirror", gamefile), False)
        compilequery(MyQuery, Readini("Rules", "Elemental", gamefile), False)
        compilequery(MyQuery, Readini("Rules", "Neutral", gamefile), False)
        compilequery(MyQuery, Readini("Rules", "Order", gamefile), False)
        compilequery(MyQuery, Readini("Rules", "Cross", gamefile), False)
        compilequery(MyQuery, Readini("Rules", "Reverse", gamefile), False)
        compilequery(MyQuery, Readini("Rules", "Minus", gamefile), False)
        compilequery(MyQuery, Readini("Rules", "Set Wall", gamefile), False)
        compilequery(MyQuery, Readini("Rules", "Wall Value", gamefile), False)
        compilequery(MyQuery, Readini("Rules", "Immune", gamefile), False)
        compilequery(MyQuery, Readini("Rules", "Starter", gamefile), False)
        compilequery(MyQuery, Readini("Rules", "Startee", gamefile), False)
        compilequery(MyQuery, Readini("Rules", "Death", gamefile), False)
        compilequery(MyQuery, Readini("Rules", "Skip", gamefile), False)
        compilequery(MyQuery, Readini("Games", "Times", gamefile), False)
        compilequery(MyQuery, Readini("Rules", "Rank Freeze", gamefile), False)

        For x = 0 To 8
            compilequery(MyQuery, Readini("Board", x & "-Name", gamefile).Replace("%20", " "), False)
        Next

        For x = 0 To 8
            compilequery(MyQuery, Readini("Board", x & "-Color", gamefile).Replace("%20", " "), False)
        Next

        compilequery(MyQuery, Readini("Score", "player1", gamefile), False)
        compilequery(MyQuery, Readini("Score", "player2", gamefile), False)

        For y = 1 To 2
            For x = 0 To 4
                compilequery(MyQuery, Readini("Cards-Plr" & y, x.ToString, gamefile), False)
            Next

            For x = 0 To 4
                compilequery(MyQuery, Readini("Original-Plr" & y, x.ToString, gamefile), False)
            Next
        Next y

        For x = 0 To 8
            compilequery(MyQuery, Readini("PlacedBy", x.ToString, gamefile), False)
        Next

        MyQuery = MyQuery & ")"

        Console.WriteLine(MyQuery)
        Call dbEXECQuery(MyQuery)

        Exit Sub

errhandler:
        Call errorsub(Err.Description, "write_gamefile")
        'If FileExists(gamefile) = True Then Kill gamefile
    End Sub
End Module
