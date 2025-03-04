Public Class Avatars
    Inherits AvatarsDAL

    Public Sub IncomingModule(ByVal sCommand As String, ByVal incoming As String, ByVal socket As Integer)
        Select Case sCommand
            Case "AVATARBUY"
                Call buyavatars(incoming, socket)
            Case "AVATARSELL"
                Call sellavatars(incoming, socket)
            Case "AVATAREQUIP"
                Call equipavatars(incoming, socket)
            Case "AVATARCHANGE"
                Call avatarchange(socket)
        End Select
    End Sub

    Private Sub avatarchange(ByVal socket As Integer)
        Try
            If frmSystray.Winsock(socket).Tag = String.Empty Then Exit Sub

            ' Is the shop on?
            If oServerConfig.EnableAvatarShop = False Then
                Call blockchat(String.Empty, "Shop", "The avatar shop is disabled at this time", socket)
                Exit Sub
            End If

            Dim oPlayerAccount As New PlayerAccountDAL(frmSystray.Winsock(socket).Tag)

            Dim defVal(2) As String

            If oPlayerAccount.ErrorFlag = False Then
                ' has he been neutered already?

                If oPlayerAccount.Changed = True Then
                    Call blockchat(String.Empty, "Change", "You cannot receive another avatar gender change", socket)
                    Exit Sub
                End If

                Select Case oPlayerAccount.Gender
                    Case "male"
                        With oPlayerAccount
                            .Background = "000"
                            .Head = "001"
                            .Body = "002"
                            .Gender = "female"
                            .Changed = True

                            If .UpdateAvatar = False Then
                                Call blockchat(String.Empty, "Fail", "Server could not process your request at this time.  Please try again later.", socket)
                                Exit Sub
                            End If
                        End With
                    Case "female"
                        With oPlayerAccount
                            .Background = "000"
                            .Head = "013"
                            .Body = "002"
                            .Gender = "male"
                            .Changed = True

                            If .UpdateAvatar = False Then
                                Call blockchat(String.Empty, "Fail", "Server could not process your request at this time.  Please try again later.", socket)
                                Exit Sub
                            End If
                        End With
                End Select
            Else
                Call blockchat(String.Empty, "Shop", "The avatar you tried to equip is not found.  Try again later", socket)
            End If
        Catch ex As Exception
            Call errorsub(ex.ToString, "avatarchange")
        End Try
    End Sub

    Private Sub equipavatars(ByVal incoming As String, ByVal socket As Integer)
        Try
            Dim lostvar As String = String.Empty, strKey As String = String.Empty

            If frmSystray.Winsock(socket).Tag = String.Empty Then Exit Sub

            Divide(incoming, " ", lostvar, strKey)

            If strKey = String.Empty Or strKey = "keyname" Then
                Call blockchat(String.Empty, "Problem", "The server could not process your request", socket)
                Exit Sub
            End If

            ' Is the shop on?
            If oServerConfig.EnableAvatarShop = False Then
                Call blockchat(String.Empty, "Shop", "The avatar shop is disabled at this time", socket)
                Exit Sub
            End If

            Dim strColumn As String = String.Empty
            Dim rInventory As DataRow

            Dim oPlayerAccount As New PlayerAccountDAL(frmSystray.Winsock(socket).Tag)

            rInventory = GetDataRow("SELECT inventory.gender, player_inventory.keyname, player_inventory.type, player, inventory.keynum FROM player_inventory INNER JOIN inventory ON player_inventory.keyname = inventory.keyname WHERE player_inventory.keyname = '" & strKey & "' AND player = '" & frmSystray.Winsock(socket).Tag & "'")

            If rInventory.IsNull("type") = False Then
                ' ok let's equip?

                If rInventory.Item("gender").ToString <> oPlayerAccount.Gender And rInventory.Item("type").ToString <> "background" Then
                    Call blockchat(String.Empty, "Avatars", "You cannot equip an item that is not of your gender type!", socket)
                    Exit Sub
                End If

                Select Case rInventory.Item("type").ToString
                    Case "background"
                        oPlayerAccount.Background = rInventory.Item("keynum").ToString
                    Case "head"
                        oPlayerAccount.Head = rInventory.Item("keynum").ToString
                    Case "body"
                        oPlayerAccount.Body = rInventory.Item("keynum").ToString
                End Select

                If oPlayerAccount.UpdateAvatar() = True Then
                    Send(String.Empty, String.Concat("UPDATEAVATAR ", oPlayerAccount.Gender, _
                    " ", oPlayerAccount.Background, " ", oPlayerAccount.Body, _
                    " ", oPlayerAccount.Head), socket)
                Else
                    Call blockchat(String.Empty, "Fail", "Server could not process your request at this time.  Please try again later.", socket)
                End If
            Else
                Call blockchat(String.Empty, "Shop", "The avatar you tried to equip is not found.  Try again later", socket)
            End If
        Catch ex As Exception
            Call errorsub(ex.ToString, "equipavatars")
        End Try
    End Sub

    Private Sub buyavatars(ByVal incoming As String, ByVal socket As Integer)
        Try
            Dim lostvar As String = String.Empty, strKey As String = String.Empty

            If frmSystray.Winsock(socket).Tag = String.Empty Then Exit Sub

            Divide(incoming, " ", lostvar, strKey)

            If strKey = String.Empty Or strKey = "keyname" Then
                Call blockchat(String.Empty, "Problem", "The server could not process your request", socket)
                Exit Sub
            ElseIf oServerConfig.EnableAvatarShop = False Then
                Call blockchat(String.Empty, "Shop", "The avatar shop is disabled at this time", socket)
                Exit Sub
            End If

            Dim oTables As New BusinessLayer.Tables
            Dim oPlayerAccount As New PlayerAccountDAL(frmSystray.Winsock(socket).Tag)

            If oTables.FindTablePlayer(Tables, frmSystray.Winsock(socket).Tag) = True Then
                Call blockchat(String.Empty, "Error", "You cannot engage in shop transactions while playing a game.  Try again later.", socket)
                Exit Sub
            End If

            Dim rInventory As DataRow, intlock As Integer

            rInventory = GetDataRow("SELECT * FROM inventory WHERE keyname = '" & strKey & "'")
            intlock = Val(rInventory.Item("intLock"))

            If intlock = 1 Then
                Call blockchat(String.Empty, "Avatars", "You may not buy this avatar", socket)
                Exit Sub
            End If

            If rInventory.IsNull("type") = False Then
                If rInventory.Item("gender").ToString <> oPlayerAccount.Gender And rInventory.Item("type").ToString <> "background" Then
                    Call blockchat(String.Empty, "Avatars", "You cannot cross-dress!", socket)
                    Exit Sub
                End If

                If oPlayerAccount.Gold > Val(rInventory.Item("gold")) Then
                    Call gpadd(oPlayerAccount.Player, -1 * Val(rInventory.Item("gold")), True)
                    InsertPlayerAvatarInventory(rInventory.Item("keyname").ToString, rInventory.Item("type").ToString, oPlayerAccount.Player)
                    Send(String.Empty, "AVATARINVENTORYREFRESH", socket)

                    Call blockchat(String.Empty, "Shop", String.Concat("You purchased a ", rInventory.Item("description").ToString), socket)
                Else
                    Call blockchat(String.Empty, "Shop", "You don't have enough gold for this transaction.", socket)
                End If
            Else
                Call blockchat(String.Empty, "Shop", "The avatar you tried to purchase is not found.  Try again later", socket)
            End If
        Catch ex As Exception
            Call errorsub(ex.ToString, "buyavatars")
        End Try
    End Sub

    Private Sub sellavatars(ByVal incoming As String, ByVal socket As Integer)
        Try
            Dim lostvar As String = String.Empty, strKey As String = String.Empty

            If frmSystray.Winsock(socket).Tag = String.Empty Then Exit Sub

            Divide(incoming, " ", lostvar, strKey)

            If strKey = String.Empty Then Exit Sub

            ' Is the shop on?
            If strKey = String.Empty Or strKey = "keyname" Then
                Call blockchat(String.Empty, "Problem", "The server could not process your request", socket)
                Exit Sub
            ElseIf oServerConfig.EnableAvatarShop = False Then
                Call blockchat(String.Empty, "Shop", "The avatar shop is disabled at this time", socket)
                Exit Sub
            End If

            Dim oTables As New BusinessLayer.Tables
            Dim oPlayerAccount As New PlayerAccountDAL(frmSystray.Winsock(socket).Tag)

            If oTables.FindTablePlayer(Tables, frmSystray.Winsock(socket).Tag) = True Then
                Call blockchat(String.Empty, "Error", "You cannot engage in shop transactions while playing a game.  Try again later.", socket)
                Exit Sub
            End If

            Dim rInventory As DataRow
            rInventory = GetDataRow("SELECT DISTINCT inventory.intLock, inventory.gender, inventory.description, inventory.keynum, player_inventory.keyname, player_inventory.type, player, inventory.gold FROM player_inventory INNER JOIN inventory ON player_inventory.keyname = inventory.keyname WHERE player_inventory.keyname = '" & strKey & "' AND player = '" & frmSystray.Winsock(socket).Tag & "'")

            If oPlayerAccount.Gender = rInventory.Item("gender").ToString Or rInventory.Item("type").ToString = "background" Then
                Select Case rInventory.Item("type").ToString
                    Case "background"
                        If oPlayerAccount.Background = rInventory.Item("keynum").ToString Then
                            Call blockchat(String.Empty, "Avatars", "You must unequip first before selling!", socket)
                            Exit Sub
                        End If
                    Case "head"
                        If oPlayerAccount.Head = rInventory.Item("keynum").ToString Then
                            Call blockchat(String.Empty, "Avatars", "You must unequip first before selling!", socket)
                            Exit Sub
                        End If
                    Case "body"
                        If oPlayerAccount.Body = rInventory.Item("keynum").ToString Then
                            Call blockchat(String.Empty, "Avatars", "You must unequip first before selling!", socket)
                            Exit Sub
                        End If
                End Select
            End If

            Dim intlock As Integer
            intlock = Val(rInventory.Item("intLock"))

            If intlock = 1 Then
                Call blockchat(String.Empty, "Avatars", "You may not sell this avatar", socket)
                Exit Sub
            End If

            Dim idvalue As String, gpamt As Integer

            If rInventory.IsNull("keynum") = False Then
                ' ok let's sell?
                idvalue = Val(getDBField("SELECT DISTINCT player_inventory.id, player_inventory.keyname, player_inventory.type, player, inventory.gold FROM player_inventory INNER JOIN inventory ON player_inventory.keyname = inventory.keyname WHERE player_inventory.keyname = '" & strKey & "' AND player = '" & frmSystray.Winsock(socket).Tag & "'", "id"))

                gpamt = Val(rInventory.Item("gold"))
                gpamt = Val(Int(gpamt / 4))

                If DeletePlayerAvatarInventory(idvalue) = True Then
                    Call gpadd(frmSystray.Winsock(socket).Tag, gpamt, True)
                    Call blockchat(String.Empty, "Avatar Shop", String.Concat("You sold a ", rInventory.Item("description").ToString, " for ", gpamt, "GP"), socket)
                    Send(String.Empty, "AVATARINVENTORYREFRESH", socket)
                Else
                    Call blockchat(String.Empty, "Problem", "The server could not process your request", socket)
                End If
            Else
                Call blockchat(String.Empty, "Shop", "The avatar you tried to sell is not found.  Try again later", socket)
            End If
        Catch ex As Exception
            Call errorsub(ex.ToString, "sellavatars")
        End Try
    End Sub
End Class
