Public Class PlayerShopFunctions
    Inherits PlayerShopCardsDAL

    Public Sub PlayerShopModule(ByVal sCommand As String, ByVal incoming As String, ByVal socket As Integer)
        Select Case sCommand
            Case "SHOPCREATE"
                Call shopcreate(incoming, socket)
            Case "SHOPADD"
                Call shopadd(incoming, socket)
            Case "SHOPMINUS"
                Call shopminus(incoming, socket)
            Case "SHOPCARDBUY"
                Call shopcardbuy(incoming, socket)
        End Select
    End Sub

    Private Sub shopadd(ByVal incoming As String, ByVal socket As Integer)
        Dim strCard As String = String.Empty, lostvar As String = String.Empty, strPrice As String = String.Empty
        Dim intMax, intCap As Integer

        Divide(incoming, " ", lostvar, strCard, strPrice)

        Dim oPlayerAccount As New PlayerAccountDAL(frmSystray.Winsock(socket).Tag)

        Dim oTables As New TableFunctions

        If oTables.findtableplayer(oPlayerAccount.Player) = True Then
            Call blockchat(String.Empty, "Error", "You cannot engage in shop transactions while playing a game.  Try again later.", socket)
            Exit Sub
        ElseIf oPlayerAccount.ErrorFlag = True Then
            Call blockchat(String.Empty, "Error", "Server could not process your request at this time.  Please try again later.", socket)
            Exit Sub
        ElseIf GetRowCount("SELECT * FROM playershop_text WHERE player = '" & oPlayerAccount.Player & "'") = 0 Then
            Call blockchat(String.Empty, "Failed", "You must click update shop before adding or removing cards from your shop.  Your shop has not been turned on.", socket)
            Exit Sub
        ElseIf Date.Today < oPlayerAccount.LastNewDeck.AddDays(7) Then
            Call blockchat(String.Empty, "Lock", "You have gotten a newdeck within the last 7 days.  You may not add cards to your shop until this 7 day lock has expired", socket)
            Exit Sub
        End If

        strPrice = Int(CDbl(strPrice)).ToString

        If Val(strPrice) > 99999 Then
            Call blockchat(String.Empty, "Failed", "Price value exceeds price maximum.  Try a lower number for the price of your card", socket)
            Exit Sub
        End If

        strCard = strCard.Replace("%20", " ")

        If strCard = String.Empty Then
            Call blockchat(String.Empty, "Error", "Server could not process your request at this time.  Please try again later.", socket)
            Exit Sub
        ElseIf strPrice = String.Empty Then
            Call blockchat(String.Empty, "Error", "Server could not process your request at this time.  Please try again later.", socket)
            Exit Sub
        End If

        Select Case getmembership(oPlayerAccount.Player)
            Case "5"
                intMax = 75
            Case "4"
                intMax = 40
            Case "3"
                intMax = 30
            Case "2"
                intMax = 25
            Case Else
                intMax = 15
        End Select

        intCap = Val(getAAField(oPlayerAccount.Player, "bountiful", "level")) * 5
        intCap += Val(getVAMod(oPlayerAccount.Player, "enhance"))

        intMax = intMax + intCap

        If GetRowCount("SELECT * FROM playershop_cards WHERE player ='" & oPlayerAccount.Player & "'") >= intMax Then
            Call blockchat(String.Empty, "Failed", "Your shop is full.  You may not add anymore cards.  Consider buying a membership or train the Bountiful Bazaar AA to increase card shop size.", socket)
            Exit Sub
        End If

        If findcardamt(oPlayerAccount.Player, strCard) > 0 Then
            If cardchg(oPlayerAccount.Player, strCard, -1, False) = False Then
                Call blockchat(String.Empty, "Failed", "Server could not execute this command, try again later", socket)
                Exit Sub
            End If

            If shopcardchg(oPlayerAccount.Player, strCard, 1, strPrice) = True Then
                Call blockchat(String.Empty, "Successful", strCard & " was added to your shop for " & strPrice & "GP", socket)
            Else
                Call blockchat(String.Empty, "Failed", strCard & " could not be added to your shop.  Try again later", socket)
                Call cardchg(oPlayerAccount.Player, strCard, 1, False)
            End If

            Send(String.Empty, "SHOPMYREFRESH", socket)
        Else
            Call blockchat(String.Empty, "Failed", "You do not have enough of this card to place in shop", socket)
        End If
    End Sub

    Private Sub shopminus(ByVal incoming As String, ByVal socket As Integer)
        Dim lostvar As String = String.Empty, player As String = String.Empty, strCard As String = String.Empty

        player = frmSystray.Winsock(socket).Tag

        Divide(incoming, " ", lostvar, strCard)

        strCard = strCard.Replace("%20", " ")
        ' Error 1
        If player = String.Empty Then Exit Sub
        ' Error 2
        If strCard = String.Empty Then Exit Sub

        Dim oTables As New TableFunctions

        If oTables.findtableplayer(frmSystray.Winsock(socket).Tag) = True Then
            Call blockchat(String.Empty, "Error", "You cannot engage in shop transactions while playing a game.  Try again later.", socket)
            Exit Sub
        End If

        If shopcardchg(player, strCard, -1, "0") = True Then
            Call blockchat(String.Empty, String.Concat("Successful", strCard, " was removed from your shop"), socket)

            Send(String.Empty, "SHOPMYREFRESH", socket)
        End If
    End Sub

    Public Sub shopcreate(ByVal incoming As String, ByVal socket As Integer)
        Try

            Dim strDescription As String = String.Empty, lostvar As String = String.Empty, strTitle As String = String.Empty, strReason As String = String.Empty

            If Date.Today < ((CDate(getAccount_Field(frmSystray.Winsock(socket).Tag, "signupdate")).AddDays(3))) Then
                strReason = "You have gotten a newdeck or created an account within the last 3 days.  You may not play trade all or trade four games until this has expired."
                strReason = strReason.Replace(" ", "%20")
                Call blockchat(String.Empty, "Lock", strReason, socket)
                Exit Sub
            End If

            Divide(incoming, " ", lostvar, strTitle, strDescription)

            strTitle = strTitle.Replace("%20", " ").Replace("'", String.Empty)
            strDescription = strDescription.Replace("%20", " ").Replace("'", String.Empty)

            If strTitle.Length > 30 Then
                Dim oChatFunctions As New ChatFunctions
                oChatFunctions.uniwarn(String.Concat("WARNING ADMINS! ", frmSystray.Winsock(socket).Tag, " sent shop packet with title greater than 30 characters.  Player is using hacker software!"))
                Exit Sub
            ElseIf strDescription.Length > 250 Then
                Dim oChatFunctions As New ChatFunctions
                oChatFunctions.uniwarn(String.Concat("WARNING ADMINS! ", frmSystray.Winsock(socket).Tag, " sent shop packet with description greater than 250 characters.  Player is using hacker software!"))
                Exit Sub
            ElseIf oServerConfig.FilterNeeded(strTitle) = True Then
                Call blockchat(String.Empty, "Fail", "The title you have chosen for your shop is inappropriate.  Please try something else.", socket)
                Exit Sub
            ElseIf oServerConfig.FilterNeeded(strDescription) = True Then
                Call blockchat(String.Empty, "Fail", "The description you have chosen for your shop is inappropriate.  Please try something else.", socket)
                Exit Sub
            End If

            Dim iRecordCount As Integer = GetRowCount("SELECT * FROM playershop_text WHERE player = '" & frmSystray.Winsock(socket).Tag & "'")

            If iRecordCount = 0 Then
                Call dbEXECQuery("INSERT INTO playershop_text VALUES ('" & frmSystray.Winsock(socket).Tag & "', '" & strTitle & "', '" & strDescription & "')")
            Else
                Call dbEXECQuery("UPDATE playershop_text SET title = '" & strTitle & "', description = '" & strDescription & "' WHERE player = '" & frmSystray.Winsock(socket).Tag & "'")
            End If

            Call blockchat(String.Empty, "Successful", "Shop Update Successful", socket)
        Catch ex As Exception
            Call errorsub(ex.ToString, "shopcreate")
        End Try
    End Sub

    Private Sub shopcardbuy(ByVal incoming As String, ByVal socket As Integer)
        Dim sShopOwner As String = String.Empty, lostvar As String = String.Empty, sCard As String = String.Empty, sPrice As String = String.Empty
        Dim strCount As String = String.Empty, sCard2 As String = String.Empty
        Dim intID As Double, dblTax As Double
        Dim oTables As New TableFunctions

        Dim oPlayerAccount As New PlayerAccountDAL(frmSystray.Winsock(socket).Tag)

        Try
            Divide(incoming, " ", lostvar, sShopOwner, sCard, sPrice)

            Dim oShopOwner As New PlayerAccountDAL(sShopOwner)

            If oShopOwner.Online = False Then
                Call blockchat(String.Empty, "Failed", "You cannot buy a card from a player who is not online.", socket)
                Exit Sub
            ElseIf oTables.findplayerstable(oPlayerAccount.Player) > 0 Then
                Call blockchat(String.Empty, "Failed", "You cannot buy a card while you are in a game", socket)
                Exit Sub
            ElseIf oPlayerAccount.Player = sShopOwner Then
                Call blockchat(String.Empty, "Failed", "You cannot purchase from your own shop", socket)
                Exit Sub
            ElseIf oServerConfig.PlayerShop = False Then
                Call blockchat(String.Empty, "Failed", "Server Operator has chosen for the player merchants to be disabled", socket)
                Exit Sub
            End If

            sCard = sCard.Replace("%20", " ")

            ' Let's see if its still for sale
            Dim rCard As DataRow

            rCard = GetPlayerShopCards(sCard, sShopOwner, sPrice)

            Try
                If rCard.IsNull(0) = True Then
                    Call blockchat(String.Empty, "Unavailable", "The card you purchased is unavailable.  The owner may have removed it from the shop or it have been already purchased", socket)
                    Exit Sub
                End If
            Catch
                Call blockchat(String.Empty, "Unavailable", "The card you are trying to purchase may not be available or already purchased", socket)
                Exit Sub
            End Try

            intID = Val(rCard.Item("id").ToString)
            sPrice = rCard.Item("price").ToString

            If oPlayerAccount.Gold < Val(sPrice) Then
                Call blockchat(String.Empty, "Unavailable", "You do not have enough GP for this transaction", socket)
                Exit Sub
            End If

            ' It's good, he can have the card now.. so let's delete it quick and take the funds
            If DeletePlayerShopCards(intID) = False Then
                Call blockchat(String.Empty, "Server Problem", "The server could not process your request at this time.  Please try again later.", socket)
                Exit Sub
            End If

            Dim blnCheck As Boolean = False

            Select Case getmembership(sShopOwner)
                Case "5"
                    dblTax = 0
                    blnCheck = True
                Case "4"
                    dblTax = 0.0025
                Case "3"
                    dblTax = 0.005
                Case "2"
                    dblTax = 0.01
                Case Else
                    dblTax = 0.02
            End Select

            If getDBField("call usp_PlayerAALevel('" & sShopOwner & "', 'petty')", "level") = "1" Then dblTax = dblTax / 2

            Dim dblTaxCost As Double = Int(Val(sPrice) * dblTax)

            If dblTaxCost = 0 And blnCheck = False Then dblTaxCost = 1

            Call gpadd(sShopOwner, Val(sPrice) - CShort(Int(dblTaxCost)), True)
            Call gpadd(oPlayerAccount.Player, Val(sPrice) * -1, True)

            Call setServerStats("gptax", dblTaxCost.ToString) ' Keep track of GP Tax
            Call cardchg(oPlayerAccount.Player, sCard, 1, False)

            If Val(sPrice) > 75000 Then
                Dim oChatFunctions As New ChatFunctions
                oChatFunctions.uniwarn(String.Concat(oPlayerAccount.Player, " bought a ", sCard, " for ", sPrice, " from ", sShopOwner))
                Exit Sub
            End If

            Call InsertPlayerShopTransactions(sShopOwner, oPlayerAccount.Player, sCard, sPrice)
            Send(String.Empty, String.Concat("SHOPREFRESH ", sShopOwner), socket)

            ' Let's notify them
            Call blockchat(String.Empty, "Card", String.Concat("You received 1 ", sCard), socket)
            Call historyadd(oPlayerAccount.Player, 4, String.Concat(oPlayerAccount.Player, " purchased 1 ", sCard, " for ", Val(sPrice) - CShort(Int(dblTaxCost)), "GP from ", sShopOwner))
            Call blockchat(sShopOwner, "Shop", String.Concat(oPlayerAccount.Player, " purchased 1 ", sCard, " for ", Val(sPrice) - Int(dblTaxCost), "GP"))
        Catch ex As Exception
            Call errorsub(ex.Message & " -- " & ex.StackTrace, "shopcardbuy")
        End Try
    End Sub
End Class
