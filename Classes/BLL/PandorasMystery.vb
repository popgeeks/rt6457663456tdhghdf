Public Class PandorasMystery
    Inherits PandorasMysteryDAL

    Public Sub GameModule(ByVal sCommand As String, ByVal incoming As String, ByVal socket As Integer)
        Select Case sCommand
            Case "PANDORAGUESS"
                Call PandoraGuess(incoming, socket)
        End Select
    End Sub

    Private Sub PandoraGuess(ByVal incoming As String, ByVal socket As Integer)
        Dim oPlayerAccount As New PlayerAccountDAL(frmSystray.Winsock(socket).Tag)
        Dim oCard As New DarkWindCard("Pandoras Box")
        Dim oTableFunctions As New TableFunctions
        Dim iCorrect As Integer = 0

        ' 0 = lostvar, 1 = up, 2 = down, 3 = left, 4 = right
        Dim sItems() As String = incoming.Split(" ")

        If oPlayerAccount.Player = String.Empty Then
            Call blockchat(frmSystray.Winsock(socket).Tag, "Error", "Server could not process your request at this time.  Try again later")
            Exit Sub
        ElseIf oTableFunctions.findtableplayer(oPlayerAccount.Player) = True Then
            Call blockchat(frmSystray.Winsock(socket).Tag, "Error", "You cannot play Pandoras Mystery while you are in a game.")
            Exit Sub
        End If

        Load()

        Call giveap(oPlayerAccount.Player, -1 * Fee, False)

        If Val(sItems(1)) = oCard.Up Then iCorrect += 1
        If Val(sItems(2)) = oCard.Down Then iCorrect += 1
        If Val(sItems(3)) = oCard.Left Then iCorrect += 1
        If Val(sItems(4)) = oCard.Right Then iCorrect += 1

        Dim oGameFunctions As New GameFunctions
        Dim iGameID As Integer = oGameFunctions.NewGameID()

        InsertPandoraGame(iGameID, oPlayerAccount.Player, Val(sItems(1)), Val(sItems(2)), Val(sItems(3)), Val(sItems(4)), String.Concat(iCorrect, " numbers correct", IIf(iCorrect = 4, " - won jackpot", String.Empty)))

        If iCorrect = 4 Then
            ' You won
            ResetPandorasBox()
            ResetPot()
            Send(String.Empty, String.Concat("PANDORAGUESS ", "You%20opened%20pandora's%20box%20and%20won%20the%20pot!"), socket)
            Call giveap(oPlayerAccount.Player, Pot, False)

            Dim oChatFunctions As New ChatFunctions
            oChatFunctions.unisend("CHATBLOCK", String.Concat("Mystery ", (String.Concat(oPlayerAccount.Player, " opened Pandora's Box and won the pot of ", Pot, " AP!")).Replace(" ", "%20")))
            Call historyadd(oPlayerAccount.Player, 2, String.Concat("Hit the Pandora's Mystery Jackpot of ", Pot, " AP!"))
        Else
            UpdatePMPot()
            Send(String.Empty, String.Concat("PANDORAGUESS ", "You%20got%20", iCorrect, "%20numbers%20correct."), socket)
        End If
    End Sub
End Class
