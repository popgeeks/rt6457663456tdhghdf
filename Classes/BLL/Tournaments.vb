Public Class Tournaments
    Inherits TournamentsDAL
    Public Sub IncomingModule(ByVal sCommand As String, ByVal incoming As String, ByVal socket As Integer)
        Select Case sCommand
            Case "TOURNAMENTCREATE"
                Call tournamentcreate(incoming, socket)
            Case "TOURNAMENTJOIN"
                Call tournamentjoin(incoming, socket)
        End Select
    End Sub

    Private Sub tournamentjoin(ByVal incoming As String, ByVal socket As Integer)
        Try
            Dim lostvar As String = String.Empty, sID As String = String.Empty

            If frmSystray.Winsock(socket).Tag = String.Empty Then Exit Sub

            Divide(incoming, " ", lostvar, sID)

            If sID = String.Empty Then
                Call blockchat(String.Empty, "Problem", "The server could not process your request.  Some information was not supplied or lost during transmission.", socket)
                Exit Sub
            End If

            Dim oPlayerAccount As New PlayerAccountDAL(frmSystray.Winsock(socket).Tag)
            Dim oTournament As New IndividualTournamentDAL(Val(sID))

            If oTournament.expired = True Then
                Call blockchat(String.Empty, "Failed", "Registration for this tournament has expired.", socket)
                Exit Sub
            End If

            If GetRowCount(String.Concat("SELECT ID FROM tournamentplayers where TID = ", sID)) > 0 Then
                Call blockchat(String.Empty, "Failed", "You are already in this tournament", socket)
                Exit Sub
            End If

            If oPlayerAccount.AP < oTournament.APCost Then
                Call blockchat(String.Empty, "Failed", String.Concat("You do not have enough AP to join this tournament.", socket))
                Exit Sub
            ElseIf oPlayerAccount.AP < oTournament.GoldCost Then
                Call blockchat(String.Empty, "Failed", String.Concat("You do not have enough gold to join this tournament.", socket))
                Exit Sub
            End If

            If InsertPlayer(oPlayerAccount.Player, Val(sID)) = True Then
                Call giveap(oPlayerAccount.Player, 0, False)
                Call gpadd(oPlayerAccount.Player, 0, False)
                Call blockchat(String.Empty, "Tournament", "You have received entry into the tournament.", socket)
                Exit Sub
            End If
        Catch ex As Exception
            Call blockchat(String.Empty, "Failed", "The server could not process your request.  Try again later.", socket)
        End Try
    End Sub

    Private Sub tournamentcreate(ByVal incoming As String, ByVal socket As Integer)
        Dim lostvar As String = String.Empty, sName As String = String.Empty, sType As String = String.Empty
        Dim sAmount As String = String.Empty, sGame As String = String.Empty, sRuleset As String = String.Empty
        Dim sEntry As String = String.Empty

        If frmSystray.Winsock(socket).Tag = String.Empty Then Exit Sub

        Divide(incoming, " ", lostvar, sName, sType, sEntry, sAmount, sGame, sRuleset)

        If sName = String.Empty Or sType = String.Empty Or sAmount = String.Empty Or sGame = String.Empty Then
            Call blockchat(String.Empty, "Problem", "The server could not process your request.  Some information was not supplied or lost during transmission.", socket)
            Exit Sub
        End If

        Dim oPlayerAccount As New PlayerAccountDAL(frmSystray.Winsock(socket).Tag)

        If oPlayerAccount.AP < oServerConfig.TournamentCost Then
            Call blockchat(String.Empty, "Failed", String.Concat("You do not have enough AP to create this tournament. You have ", oPlayerAccount.AP, "AP and ", oServerConfig.TournamentCost, "AP is required."), socket)
            Exit Sub
        End If

        If Insert(sName.Replace("%20", " "), sType.Replace("%20", " "), sGame.Replace("%20", " "), IIf(sEntry = "Gold", Val(sAmount), 0), IIf(sEntry = "AP", Val(sAmount), 0), sRuleset.Replace("%20", " ")) = True Then
            Call giveap(oPlayerAccount.Player, -1 * oServerConfig.TournamentCost, False)
            Call blockchat(String.Empty, "Tournament", "Your tournament was created.  It may take up to 5 minutes for your tournament to be broadcasted; however, it is available for signups through the tournament list.", socket)
            Exit Sub
        End If
    End Sub
End Class
