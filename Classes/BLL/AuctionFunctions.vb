Public Class AuctionFunctions
    Inherits AuctionsDAL

    Public Sub AuctionModule(ByVal sCommand As String, ByVal incoming As String, ByVal socket As Integer)
        Select Case sCommand
            Case "AUCTIONADD"
                Call AuctionAdd(incoming, socket)
            Case "AUCTIONBID"
                Call AuctionBid(incoming, socket)
        End Select
    End Sub

    Private Sub AuctionAdd(ByVal incoming As String, ByVal socket As Integer)
        Try
            Dim lostvar As String = String.Empty
            Dim sBasePrice As String = String.Empty, sDays As String = String.Empty
            Dim sCard As String = String.Empty, sGP As String = String.Empty

            Dim oTableFunctions As New BusinessLayer.Tables
            Dim oPlayerAccount As New PlayerAccountDAL(frmSystray.Winsock(socket).Tag)

            If oTableFunctions.FindTablePlayer(Tables, oPlayerAccount.Player) = True Then
                Call blockchat(oPlayerAccount.Player, "Problem", "You cannot make an auction while in a game.")
                Exit Sub
            ElseIf oPlayerAccount.Player = String.Empty Then
                Call blockchat(frmSystray.Winsock(socket).Tag, "Problem", "Server could not process your request")
            End If

            Divide(incoming, " ", lostvar, sCard, sBasePrice, sDays)

            sCard = sCard.Replace("%20", " ")

            If Val(sBasePrice) < 0 Then
                Call blockchat(oPlayerAccount.Player, "Problem", "The minimum price cannot be negative")
                Exit Sub
            End If

            Dim oFees As New ListingFees

            If oFees.ListingFee = 9999 Then
                Call blockchat(oPlayerAccount.Player, "Problem", "Server could not process your listing request.  Try again later.")
                Exit Sub
            End If

            Dim oAuctions As New AuctionsDAL

            Dim oDataRow As DataRow = PlayerCardDetail(oPlayerAccount.Player, sCard)

            If oDataRow Is Nothing Then
                Call blockchat(oPlayerAccount.Player, "Problem", "Server could not process your listing request.  Try again later.")
                Exit Sub
            End If

            If Val(oDataRow.Item("value").ToString) > 0 Then
                If InsertPlayerAuction(oPlayerAccount.Player, sCard, Val(sBasePrice), Val(sDays)) = True Then
                    Dim oChatFunctions As New ChatFunctions

                    Call cardchg(oPlayerAccount.Player, sCard, -1, False)
                    Call gpadd(oPlayerAccount.Player, -1 * oFees.ListingFee, True)

                    Call blockchat(oPlayerAccount.Player, "Listing Successful", "Your listing was successfully added")
                    oChatFunctions.unisend("CHATBLOCK", "Auction " & (String.Concat(oPlayerAccount.Player, " has put up a(n) ", sCard, " up for sale.  Bids start at ", sBasePrice, " and expires in ", sDays, " days.")).Replace(" ", "%20"))
                Else
                    Call blockchat(oPlayerAccount.Player, "Problem", "Server could not process your listing request.  Try again later.")
                    Exit Sub
                End If
            Else
                Call blockchat(oPlayerAccount.Player, "Problem", "You do not have enough of this card.")
                Exit Sub
            End If
        Catch ex As Exception
            Call errorsub(ex.ToString, "AuctionAdd")
        End Try
    End Sub

    Private Sub AuctionBid(ByVal incoming As String, ByVal socket As Integer)
        Try
            Dim lostvar As String = String.Empty, sKey As String = String.Empty, sBid As String = String.Empty

            Dim oTableFunctions As New BusinessLayer.Tables
            Dim oPlayerAccount As New PlayerAccountDAL(frmSystray.Winsock(socket).Tag)

            If oTableFunctions.FindTablePlayer(Tables, oPlayerAccount.Player) = True Then
                Call blockchat(String.Empty, "Problem", "You cannot make an auction while in a game.", socket)
                Exit Sub
            ElseIf oPlayerAccount.Player = String.Empty Then
                Call blockchat(String.Empty, "Problem", "Server could not process your request", socket)
                Exit Sub
            End If

            Divide(incoming, " ", lostvar, sKey, sBid)

            Dim oDataRow As DataRow = PlayerAuctionsList(Val(sKey))

            If Not (oDataRow Is Nothing) Then
                Dim oDataBidRow As DataRow = PlayerAuctionsTopBid(Val(sKey))

                Dim iTopBid As Integer = 0
                Dim sTopBidder As String = String.Empty

                If oDataRow.Item("Seller").ToString = oPlayerAccount.Player Then
                    Dim oChatFunctions As New ChatFunctions
                    Call blockchat(String.Empty, "Error", "You cannot bid on your own auction", socket)
                    oChatFunctions.uniwarn(String.Concat(oPlayerAccount.Player, " tried to bid on his own auction.  This means he got passed the client filter"))
                    Call historyadd(oPlayerAccount.Player, 2, String.Concat(oPlayerAccount.Player, " tried to bid on his own auction.  This means he got passed the client filter"))
                    oChatFunctions = Nothing
                    Exit Sub
                End If

                If Not (oDataBidRow Is Nothing) Then
                    iTopBid = CInt(Val(oDataBidRow.Item("TopBid").ToString))
                    sTopBidder = oDataBidRow.Item("TopBidder").ToString
                Else
                    iTopBid = Val(oDataRow.Item("CurrentBid").ToString)
                    sTopBidder = String.Empty
                End If

                If oDataRow.Item("Expired").ToString = "0" Then
                    If iTopBid >= Val(sBid) Then
                        ' Someone had a higher bid
                        Call UpdateAuctionCost(Val(sKey), Val(sBid))
                        Call InsertPlayerAuctionBid(Val(sKey), oPlayerAccount.Player, Val(sBid))

                        Call blockchat(String.Empty, "Outbid", "You have been outbid! Try a higher bid!", socket)
                        Send(String.Empty, String.Concat("AUCTIONREFRESH ", sKey), socket)
                    ElseIf Val(sBid) < Int(iTopBid * 1.05) Then
                        Call blockchat(String.Empty, "Error", "You must bid at least 5% higher than the current bid!", socket)
                        Send(String.Empty, String.Concat("AUCTIONREFRESH ", sKey), socket)
                        Exit Sub
                    Else
                        If sTopBidder <> String.Empty Then
                            Call gpadd(sTopBidder, iTopBid, True)
                            Call historyadd(sTopBidder, 24, String.Concat(sTopBidder, " received ", iTopBid, " for being outbid on Auction #", oDataRow.Item("AuctionNumber").ToString))
                        End If

                        Call gpadd(oPlayerAccount.Player, -1 * Val(sBid), True)
                        Call historyadd(oPlayerAccount.Player, 26, String.Concat(oPlayerAccount.Player, " had ", sBid, " removed because of bidding on Auction #", oDataRow.Item("AuctionNumber").ToString))

                        Call UpdateAuctionCost(Val(sKey), Val(sBid))
                        Call InsertPlayerAuctionBid(Val(sKey), oPlayerAccount.Player, Val(sBid))

                        Call blockchat(String.Empty, "Successful", "You now have the winning bid so far!", socket)

                        If isOnline(oDataBidRow.Item("TopBidder").ToString) = True Then Call blockchat(oDataBidRow.Item("TopBidder").ToString, "Outbid", String.Concat("You have been outbid on ", oDataRow.Item("Card").ToString))
                        Send(String.Empty, String.Concat("AUCTIONREFRESH ", sKey), socket)
                    End If
                Else
                    Call blockchat(oPlayerAccount.Player, "Problem", "This auction has expired")
                    Exit Sub
                End If
            Else
                Call blockchat(oPlayerAccount.Player, "Problem", "The server could not process your request.  Please try again later")
            End If
        Catch ex As Exception
            'Call errorsub(ex.ToString, "AuctionBid")
        End Try
    End Sub
End Class
