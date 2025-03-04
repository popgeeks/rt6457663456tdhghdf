Public Class AlternateAdvancement
    Inherits AlternateAdvancementDAL

    Public Sub AAModule(ByVal sCommand As String, ByVal incoming As String, ByVal socket As Integer)
        Select Case sCommand
            Case "ALTERNATESET"
                Call aaset(incoming, socket)
            Case "ALTERNATETRAIN"
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

        Dim oAA As New AlternateAdvancement
        oAA.GetAlternateAdvancement(sItems(1))

        If oAA.Name = String.Empty Or oAA.Points <= 0 Then
            Call blockchat(oPlayerAccount.Player, "Error", "Server could not process your request")
            Exit Sub
        ElseIf oPlayerAccount.AAPoints < oAA.Points Then
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
        Call UpdateAAPoints(oPlayerAccount.Player, oAA.Keyword, oAA.Points)
        Call blockchat(oPlayerAccount.Player, "Success", "You have successfully trained this ability")
        Call historyadd(oPlayerAccount.Player, 2, String.Concat("Trained (AA) a level in ", oAA.Keyword))

        Send(String.Empty, "AAREFRESH", socket)
        Call sendall(String.Concat("CHATBLOCK Server ", String.Concat(oPlayerAccount.Player, " feels more powerful.").Replace(" ", "%20")))
    End Sub

    ''' <summary>
    ''' sItems: 0 = lostvar, 1 = sValue
    ''' </summary>
    ''' <param name="incoming"></param>
    ''' <param name="socket"></param>
    ''' <remarks></remarks>
    Private Sub aaset(ByVal incoming As String, ByVal socket As Integer)
        Try
            Dim oPlayerAccount As New PlayerAccountDAL(frmSystray.Winsock(socket).Tag)

            Dim sItems() As String = incoming.Split(" ")

            If oPlayerAccount.Level < 10 Then
                Call blockchat(oPlayerAccount.Player, "Error", "You have not reached an adequate level to begin training advancement points")
                Exit Sub
            End If

            Call setaccountdata(oPlayerAccount.Player, "aapct", sItems(1))
        Catch ex As Exception
            Call errorsub(ex, "aaset")
        End Try
    End Sub
End Class
