Module modTables
    Public Sub tableclose(ByVal socket As Integer)
        Dim tableID As String
        Dim x, intPlayer As Integer
        Dim strPlayers(2) As String
        Dim oTables As New TableFunctions

        tableID = oTables.findplayerstable(frmSystray.Winsock(socket).Tag).ToString

        If tableID = "0" Then Exit Sub

        strPlayers(1) = frmSystray.Winsock(socket).Tag
        strPlayers(2) = tables(CInt(tableID)).player2

        If tables(CInt(tableID)).player1 = frmSystray.Winsock(socket).Tag Then
            tables(CInt(tableID)).player_ready(1) = False

            oTables.ClosePlayerTable(frmSystray.Winsock(socket).Tag)

            tables(CInt(tableID)).player1 = ""

            intPlayer = 1
        ElseIf tables(CInt(tableID)).player2 = frmSystray.Winsock(socket).Tag Then
            tables(CInt(tableID)).player_ready(2) = False
            tables(CInt(tableID)).player2 = ""

            intPlayer = 2
        End If

        ' Update the challenge boxes
        If strPlayers(1) <> "" And intPlayer <> 1 Then
            Send(tables(CInt(tableID)).player1, "CHALLENGEREMOVES " & intPlayer)
        End If

        If strPlayers(2) <> "" And intPlayer <> 2 Then
            Send(tables(CInt(tableID)).player2, "CHALLENGEREMOVES " & intPlayer)
        End If

        If intPlayer = 1 Then
            For x = 1 To 2
                Send(strPlayers(x), "CHALLENGEREMOVES 5")
            Next x
        Else
            Send("", "CHALLENGEREMOVES 5", socket)
        End If

        Call sendall("ADDTABLE " & tableID & " " & tables(CInt(tableID)).player1 & " " & tables(CInt(tableID)).player2 & " " & tables(CInt(tableID)).rulelist & " @@" & tables(CInt(tableID)).comment & "@@" & tables(CInt(tableID)).strDecks)
    End Sub
End Module
