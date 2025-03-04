Public Class clsCardPacks
    Inherits CardPacksDAL

    Public Sub cardpack(ByVal incoming As String, ByVal socket As Integer)
        Dim oDataRow As DataRow

        Try
            Dim oPlayerAccount As New PlayerAccountDAL(frmSystray.Winsock(socket).Tag)
            Dim sPack() As String = incoming.Split(" ")

            Dim oTables As New BusinessLayer.Tables

            If oTables.FindTablePlayer(Tables, oPlayerAccount.Player) = True Then
                Call blockchat(String.Empty, "Error", "You cannot buy a card pack while playing a game.  Try again later.", oPlayerAccount.PlayerSocket)
                Exit Sub
            ElseIf sPack(1) = String.Empty Then
                Call blockchat(String.Empty, "Error", "The server could not process your request.  You must pick a card pack to buy.", socket)
                Exit Sub
            End If

            sPack(1) = sPack(1).Replace("%20", " ")

            Dim oCardPacks As New clsCardPacks
            oDataRow = oCardPacks.PurchasePack(oPlayerAccount.Player, sPack(1))

            Call blockchat(oPlayerAccount.Player, "Pack", oDataRow.Item(0).ToString, socket)

            Send(String.Empty, String.Format("CARDPACK {0}", Mid(oDataRow.Item(0).ToString, 18, oDataRow.Item(0).ToString.Length)), socket)

            oPlayerAccount.Gold = Integer.Parse(oDataRow.Item(1))
            oPlayerAccount.SendGold()
        Catch ex As Exception
            Call errorsub(ex.ToString, "cardpack")
        Finally
            oDataRow = Nothing
        End Try
    End Sub
End Class