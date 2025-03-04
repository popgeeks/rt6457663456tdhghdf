Public Class VeteranAdvancement
    Inherits VeteranAdvancementDAL

    Public Sub AAModule(ByVal sCommand As String, ByVal incoming As String, ByVal socket As Integer)
        Select Case sCommand
            Case "VETERANALTERNATETRAIN"
                Call aatrain(incoming, socket)
        End Select
    End Sub

    ''' <summary>
    ''' sItems = 0 = lostvar, 1 = callkey (value)
    ''' </summary>
    ''' <param name="incoming"></param>
    ''' <param name="socket"></param>
    ''' <remarks></remarks>
    ''' 
    Private Sub aatrain(ByVal incoming As String, ByVal socket As Integer)
        Dim oPlayerAccount As New PlayerAccountDAL(frmSystray.Winsock(socket).Tag)

        Dim sItems() As String = incoming.Split(" ")

        If sItems(1) = String.Empty Then
            Call blockchat(oPlayerAccount.Player, "Error", "You have tried to train a blank ability.  Click an advancement ability and press train again.")
            Exit Sub
        End If

        sItems(1) = sItems(1).ToLower

        Dim oAA As New VeteranAdvancement
        oAA.GetAlternateAdvancement(sItems(1))

        If oAA.Name = String.Empty Or oAA.Points <= 0 Then
            Call blockchat(oPlayerAccount.Player, "Error", "Server could not process your request")
            Exit Sub
        ElseIf oPlayerAccount.VAPoints < oAA.Points Then
            Call blockchat(oPlayerAccount.Player, "Error", "You do not have enough points to train this ability")
            Exit Sub
        ElseIf getAAField(oPlayerAccount.Player, sItems(1), "level") = oAA.Levels Then
            Call blockchat(oPlayerAccount.Player, "Error", "You have mastered this ability and cannot train anymore")
            Exit Sub
        ElseIf oPlayerAccount.Level < oAA.MinimumLevels Then
            Call blockchat(oPlayerAccount.Player, "Error", "You have not acquired the proper experience to train this ability")
            Exit Sub
        End If

        ' If got here, then train the ability
        Select Case sItems(1)
            Case "token"
                oPlayerAccount.Tokens += 1
                oPlayerAccount.UpdateField("cash", oPlayerAccount.Tokens, 250)
                oPlayerAccount.UpdateField("VAPoints", oPlayerAccount.VAPoints - oAA.Points, 1)
                Send(String.Empty, String.Concat("UPDATETOKENS ", oPlayerAccount.Tokens), socket)
                Call blockchat(oPlayerAccount.Player, "Success", "You successfully exchanged 1 point for 250 Crystals.")
            Case Else
                Call UpdateAAPoints(oPlayerAccount.Player, oAA.Keyword, oAA.Points)
                oPlayerAccount.UpdateField("VAPoints", oPlayerAccount.VAPoints - oAA.Points, 1)
                Call blockchat(oPlayerAccount.Player, "Success", "You have successfully trained this ability")
        End Select

        Call historyadd(oPlayerAccount.Player, 2, String.Concat("Trained (VA) a level in ", oAA.Keyword))
        Send(String.Empty, "AAVETREFRESH", socket)
        Call sendall(String.Concat("CHATBLOCK Server ", String.Concat(oPlayerAccount.Player, " feels more experienced.").Replace(" ", "%20")))
    End Sub
End Class
