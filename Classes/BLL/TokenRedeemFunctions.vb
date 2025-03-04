Public Class TokenRedeemFunctions
    Public Sub TokenModule(ByVal sCommand As String, ByVal incoming As String, ByVal socket As Integer)
        Select Case sCommand
            Case "TOKENREDEEM"
                Call TokenRedeem(incoming, socket)
        End Select
    End Sub

    Private Sub TokenRedeem(ByVal incoming As String, ByVal socket As Integer)
        Dim lostvar As String = String.Empty, sCard As String = String.Empty

        Divide(incoming, " ", lostvar, sCard)

        sCard = sCard.Replace("%20", " ")

        Dim oCard As New DarkWindCard(sCard)
        Dim oPlayerAccount As New PlayerAccountDAL(frmSystray.Winsock(socket).Tag)

        If oPlayerAccount.Player = String.Empty Then
            Call blockchat(frmSystray.Winsock(socket).Tag, "Error", "Server could not process your request.")
            Exit Sub
        ElseIf oCard.TokenCost > oPlayerAccount.Tokens Then
            Call blockchat(oPlayerAccount.Player, "Error", "You cannot afford this card.")
            Exit Sub
        End If

        Call setaccountdata(oPlayerAccount.Player, "Tokens", oPlayerAccount.Tokens - oCard.TokenCost, 1)
        Call cardchg(oPlayerAccount.Player, sCard, 1, False)
        Call dbEXECQuery(String.Format("call spi_CardDiscovery('{0}', '{1}')", oPlayerAccount.Player, sCard))

        Call historyadd(oPlayerAccount.Player, 2, String.Concat("Redeemed ", oCard.TokenCost, " tokens on ", sCard))
        Call blockchat(oPlayerAccount.Player, "Success", String.Concat("You received 1 ", sCard))
        Send(String.Empty, String.Concat("UPDATETOKENS ", oPlayerAccount.Tokens - oCard.TokenCost, " 1"), socket)
    End Sub
End Class
