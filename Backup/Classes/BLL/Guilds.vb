Public Class Guilds
    Inherits GuildsDAL

    Public Sub GuildModule(ByVal sCommand As String, ByVal incoming As String, ByVal socket As Integer)
        Select Case sCommand
            Case "GUILDDISBAND"
                Call guilddisband(socket)
            Case "GUILDCREATE"
                Call guildcreate(incoming, socket)
            Case "GUILDDISBANDPLAYER"
                Call guilddisbandplayer(incoming, socket)
            Case "GUILDAPPLY"
                Call guildapply(incoming, socket)
            Case "GUILDAPPROVEPLAYER"
                Call guildapproveplayer(incoming, socket)
            Case "GUILDDISAPPROVEPLAYER"
                Call guilddisapproveplayer(incoming, socket)
            Case "GUILDINFOUPDATE"
                Call guildinfoupdate(incoming, socket)
            Case "GUILDBANKCARDADD"
                Call guildbankcardadd(incoming, socket)
            Case "GUILDBANKCARDREMOVE"
                Call guildbankcardremove(incoming, socket)
            Case "GUILDBANKDEPOSITGOLD"
                Call guildbankdepositgold(incoming, socket)
            Case "GUILDBANKWITHDRAWGOLD"
                Call guildbankwithdrawgold(incoming, socket)
        End Select
    End Sub

    Private Sub guildcreate(ByVal incoming As String, ByVal socket As Integer)
        Dim lostvar As String = String.Empty, sName As String = String.Empty, sDescription As String = String.Empty
        Dim dRow As DataRow
        Dim oPlayerAccount As New PlayerAccountDAL(frmSystray.Winsock(socket).Tag)
        Dim oTables As New BusinessLayer.Tables

        Try
            Divide(incoming, " ", lostvar, sName, sDescription)

            sName = sName.Replace("%20", " ")
            sName = sName.Replace("'", String.Empty)

            sDescription = sDescription.Replace("%20", " ")
            sDescription = sDescription.Replace("'", String.Empty)

            If sDescription.Length > 2000 Then
                Send(String.Empty, String.Concat("GUILDSETUPFAIL ", "Guild description is too long"), socket)
                Exit Sub
            ElseIf sName.Length > 15 Then
                Send(String.Empty, String.Concat("GUILDSETUPFAIL ", "Guild name is too long"), socket)
                Exit Sub
            ElseIf sName.ToUpper = sName Then
                Send(String.Empty, String.Concat("GUILDSETUPFAIL ", "Guild name cannot be in all capital letters"), socket)
                Exit Sub
            ElseIf oTables.FindTablePlayer(Tables, oPlayerAccount.Player) = True Then
                Call blockchat(String.Empty, "Error", "You cannot create a guild while in a game.  Try again later.", socket)
                Exit Sub
            ElseIf oServerConfig.FilterNeeded(sDescription) = True Then
                Send(String.Empty, String.Concat("GUILDSETUPFAIL ", "Guild description is inappropriate"), socket)
                Exit Sub
            ElseIf oServerConfig.FilterNeeded(sName) = True Then
                Send(String.Empty, String.Concat("GUILDSETUPFAIL ", "Guild name is inappropriate"), socket)
                Exit Sub
            ElseIf GetGuildCount(sName) > 0 Then
                Send(String.Empty, String.Concat("GUILDSETUPFAIL ", "Guild name already exists"), socket)
                Exit Sub
            End If

            dRow = GuildRightsInfo(oPlayerAccount.Player, sName)

            If Not (dRow Is Nothing) Then
                If oTables.FindTablePlayer(Tables, oPlayerAccount.Player) = True Then
                    Send(String.Empty, "GUILDSETUPFAIL You cannot create a guild while playing a game.  Try again later.", socket)
                    Exit Sub
                ElseIf dRow.Item("CurrentGuild").ToString <> String.Empty Then
                    Send(String.Empty, "GUILDSETUPFAIL You cannot create a guild if you are already in one", socket)
                    Exit Sub
                ElseIf Val(dRow.Item("Cost").ToString) > Val(dRow.Item("Gold").ToString) Then
                    Send(String.Empty, "GUILDSETUPFAIL You do not have enough gold to create a guild", socket)
                    Exit Sub
                Else
                    Call gpadd(oPlayerAccount.Player, -1 * Val(dRow.Item("Cost").ToString), True)
                    Call setaccountdata(oPlayerAccount.Player, "guild", sName)
                    Call setaccountdata(oPlayerAccount.Player, "guildstatus", "2")

                    Call InsertGuildDescription(sName, sDescription)

                    Call sendall(String.Concat("UPDATEPLAYER ", oPlayerAccount.Player, " ", oPlayerAccount.Surname.Replace(" ", "%20"), " ", sName.Replace(" ", "%20")))
                    Send(String.Empty, String.Concat("GUILDSETUPOK ", sName.Replace(" ", "%20")), socket)
                    Send(String.Empty, String.Concat("UPDATEGUILD ", sName.Replace(" ", "%20")), socket)
                    Exit Sub
                End If
            Else
                ' Could not process
                Send(String.Empty, "GUILDSETUPFAIL Server could not process your request", socket)
            End If
        Catch ex As Exception
            Call errorsub(ex.ToString, "guildcreate")
        End Try
    End Sub

    Private Sub guilddisbandplayer(ByVal incoming As String, ByVal socket As Integer)
        Dim lostvar As String = String.Empty, sPlayer As String = String.Empty

        Dim oPlayerAccount As New PlayerAccountDAL(frmSystray.Winsock(socket).Tag)

        Try
            Divide(incoming, " ", lostvar, sPlayer)

            If oPlayerAccount.GuildStatus = 2 Or oPlayerAccount.GuildStatus = 1 Then
                Dim oDisbandAccount As New PlayerAccountDAL(sPlayer)

                If oDisbandAccount.GuildStatus = 2 And oPlayerAccount.GuildStatus <> 2 Then
                    Call blockchat(String.Empty, "Fail", "You cannot disband a guild master.  He can only disband himself.", socket)
                    Exit Sub
                End If

                oDisbandAccount.UpdateField("guild", String.Empty)
                oDisbandAccount.UpdateField("guildstatus", String.Empty)

                Call blockchat(String.Empty, "Removed", String.Concat(sPlayer, " was removed from the guild."), socket)

                Call InsertGuildHistory(oPlayerAccount.Guild, "Disband", String.Concat(sPlayer, " was removed from the guild by ", oPlayerAccount.Player))
                Send(String.Empty, "GUILDMAINTENCEREFRESH", socket)

                If oDisbandAccount.Online = True Then
                    Call sendall(String.Concat("UPDATEPLAYER ", oDisbandAccount.Player, " ", oDisbandAccount.Surname.Replace(" ", "%20")))
                    Send(String.Empty, "UPDATEGUILD", oDisbandAccount.PlayerSocket)
                End If
            End If
        Catch ex As Exception
            Call errorsub(ex.ToString, "guilddisbandplayer")
        End Try
    End Sub

    Private Sub guildapply(ByVal incoming As String, ByVal socket As Integer)
        Try
            Dim sGuild As String = Trim(Mid$(incoming, 12, Len(incoming)))

            If sGuild = "Servants of Darkness" Then
                Call blockchat(String.Empty, "Failed", "You cannot apply to Servants of Darkness because it is an administrative guild.", socket)
                Exit Sub
            End If

            Dim oPlayerAccount As New PlayerAccountDAL(frmSystray.Winsock(socket).Tag)

            If oPlayerAccount.Guild = String.Empty Then
                Call DeleteGuildApplication(frmSystray.Winsock(socket).Tag)
                Call blockchat(oPlayerAccount.Player, "Guild Application", String.Concat("You have applied to ", sGuild, ". Any outstanding applications you had previously were deleted."))

                If InsertGuildApplication(sGuild, oPlayerAccount.Player) = True Then
                    Call blockchat(oPlayerAccount.Player, "Guild Application", String.Concat("Your application to ", sGuild, " was successful"))
                Else
                    Call blockchat(oPlayerAccount.Player, "Guild Application", String.Concat("Your application to ", sGuild, " was unsuccessful"))
                End If
            Else
                Call blockchat(oPlayerAccount.Player, "Guild Application", "You cannot apply to a guild if you are already in one")
            End If
        Catch ex As Exception
            Call errorsub(ex.ToString, "guildapply")
        End Try
    End Sub

    Private Sub guildapproveplayer(ByVal incoming As String, ByVal socket As Integer)
        Try
            Dim sPlayer As String = String.Empty, lostvar As String = String.Empty
            Dim sStatus As String = String.Empty

            Divide(incoming, " ", lostvar, sPlayer, sStatus)

            If sStatus = "2" Then
                Call blockchat(String.Empty, "Fail", "You cannot approve a player as a guild master.", socket)
                Exit Sub
            End If

            Dim oPlayerAccount As New PlayerAccountDAL(frmSystray.Winsock(socket).Tag)
            Dim oApplicant As New PlayerAccountDAL(sPlayer)

            If oPlayerAccount.Guild <> oApplicant.AppliedGuild Then
                Call blockchat(String.Empty, "Fail", "This player has not applied to your guild.", socket)
                Exit Sub
            End If

            If Not (oPlayerAccount.GuildStatus = 2 Or oPlayerAccount.GuildStatus = 1) Then
                Call blockchat(String.Empty, "Guild Approval", "You do not have rights to approve or disapprove applicants for this guild", socket)
            ElseIf oApplicant.Guild = String.Empty Then
                Call setaccountdata(sPlayer, "guild", oPlayerAccount.Guild)
                Call setaccountdata(sPlayer, "guildstatus", sStatus)

                Call blockchat(String.Empty, "Guild Approval", String.Concat(sPlayer, " has been approved."), socket)
                Call DeleteGuildApplication(sPlayer)
                Call InsertGuildHistory(oPlayerAccount.Guild, "Applicant Approval", String.Concat(sPlayer, " was approved as an applicant by ", oPlayerAccount.Player))

                If oApplicant.PlayerSocket <> 0 Then
                    Call blockchat(String.Empty, "Guild Approval", String.Concat(sPlayer, " has been approved."), oApplicant.PlayerSocket)
                    Call sendall(String.Concat("UPDATEPLAYER ", oApplicant.Player, " ", oApplicant.Surname.Replace(" ", "%20"), " ", oPlayerAccount.Guild.Replace(" ", "%20")))
                    Send(String.Empty, String.Concat("UPDATEGUILD ", oPlayerAccount.Guild.Replace(" ", "%20")), oApplicant.PlayerSocket)
                End If

                Send(String.Empty, "GUILDMAINTENANCEAPPLICANTREFRESH", socket)
            Else
                Call blockchat(String.Empty, "Guild Approval", String.Concat(sPlayer, " is already in a guild"), socket)
            End If
        Catch ex As Exception
            Call errorsub(ex.ToString, "guildapproveplayer")
        End Try
    End Sub

    Private Sub guilddisapproveplayer(ByVal incoming As String, ByVal socket As Integer)
        Try
            Dim sPlayer As String = String.Empty, lostvar As String = String.Empty

            Divide(incoming, " ", lostvar, sPlayer)

            Dim oPlayerAccount As New PlayerAccountDAL(frmSystray.Winsock(socket).Tag)
            Dim oApplicant As New PlayerAccountDAL(sPlayer)

            If Not (oPlayerAccount.GuildStatus = 2 Or oPlayerAccount.GuildStatus = 3) Then
                Call blockchat(oPlayerAccount.Player, "Guild Approval", "You do not have rights to approve or disapprove applicants for this guild")
            Else
                Call blockchat(oPlayerAccount.Player, "Guild Disapproval", String.Concat(sPlayer, " has been disapproved."))
                Call DeleteGuildApplication(sPlayer)
                Call InsertGuildHistory(oPlayerAccount.Guild, "Applicant Disapproval", String.Concat(sPlayer, " was disapproved as an applicant by ", oPlayerAccount.Player))
                Call blockchat(oPlayerAccount.Player, "Guild Disapproval", sPlayer & " has been disapproved.")
                Send(String.Empty, "GUILDMAINTENANCEAPPLICANTREFRESH", socket)
            End If
        Catch ex As Exception
            Call errorsub(ex.ToString, "guilddisapproveplayer")
        End Try
    End Sub

    Private Sub guildinfoupdate(ByRef incoming As String, ByRef socket As Integer)
        Try
            Dim lostvar As String = String.Empty, smotd As String = String.Empty
            Dim sGuild As String = String.Empty, sDescription As String = String.Empty
            Dim oPlayerAccount As New PlayerAccountDAL(frmSystray.Winsock(socket).Tag)

            Divide(incoming, " ", lostvar, sDescription, smotd)

            smotd = smotd.Replace("%20", " ")
            smotd = smotd.Replace("'", String.Empty)

            sDescription = sDescription.Replace("%20", " ")
            sDescription = sDescription.Replace("'", String.Empty)

            sGuild = oPlayerAccount.Guild

            If smotd = String.Empty Then
                Call blockchat(String.Empty, "Fail", "Server could not update your information as the MOTD is blank and is not allowed.", socket)
                Exit Sub
            ElseIf sGuild = String.Empty Then
                Call blockchat(String.Empty, "Fail", "Server could not update your information.  Please try again later.", socket)
                Exit Sub
            ElseIf sDescription = String.Empty Then
                Call blockchat(String.Empty, "Fail", "Server could not update your information as the description is blank and is not allowed.", socket)
                Exit Sub
            ElseIf oServerConfig.FilterNeeded(sDescription) = True Then
                Call blockchat(String.Empty, "Fail", "Server could not update your information as the description contains inappropriate language.", socket)
                Exit Sub
            ElseIf oServerConfig.FilterNeeded(smotd) = True Then
                Call blockchat(String.Empty, "Fail", "Server could not update your information as the MOTD contains inappropriate language.", socket)
                Exit Sub
            End If

            If oPlayerAccount.GuildStatus = 2 Or oPlayerAccount.GuildStatus = 1 Then
                Call UpdateGuildInfo(sGuild, sDescription, smotd)
                Call blockchat(String.Empty, "Guild Information", "The guild information has been updated", socket)
                Send(String.Empty, "GUILDINFOREFRESH", socket)
            Else
                Call blockchat(String.Empty, "Guild Information", "You do not have sufficient rights to update guild information", socket)
                Send(String.Empty, "GUILDINFOREFRESH", socket)
            End If
        Catch ex As Exception
            Call errorsub(ex.ToString, "guildinfoupdate")
        End Try
    End Sub

    Private Sub guildbankcardadd(ByVal incoming As String, ByVal socket As Integer)
        Try
            Dim lostvar As String = String.Empty, sCardname As String = String.Empty
            Dim oDataRow As DataRow, sGuild As String = String.Empty
            Dim oPlayerAccount As New PlayerAccountDAL(frmSystray.Winsock(socket).Tag)
            Dim oTables As New BusinessLayer.Tables
            Dim bRefresh As Boolean = False

            Divide(incoming, " ", lostvar, sCardname)

            sCardname = sCardname.Replace("%20", " ")

            oDataRow = PlayerCardDetail(oPlayerAccount.Player, sCardname)

            If Not (oDataRow Is Nothing) Then
                sGuild = oPlayerAccount.Guild

                If sGuild = String.Empty Then
                    Call blockchat(String.Empty, "Bank Error", "You are not in a guild", oPlayerAccount.PlayerSocket)
                    Exit Sub
                ElseIf Val(oDataRow.Item("value").ToString) <= 0 Then
                    Call blockchat(String.Empty, "Bank Error", "You do not have enough cards to add to the vault", oPlayerAccount.PlayerSocket)
                    Exit Sub
                ElseIf oTables.FindTablePlayer(Tables, oPlayerAccount.Player) = True Then
                    Call blockchat(String.Empty, "Bank Error", "You cannot engage in guild bank transactions while playing a game.  Try again later.", oPlayerAccount.PlayerSocket)
                    Exit Sub
                Else
                    Dim sMessage As String = InsertGuildBankCards(oPlayerAccount.Player, sGuild, sCardname)
                    Call blockchat(oPlayerAccount.Player, "Bank", sMessage)
                    bRefresh = True
                End If
            Else
                Call blockchat(String.Empty, "Bank Error", "You do not have enough cards to add to the vault", oPlayerAccount.PlayerSocket)
                Exit Sub
            End If

            If bRefresh = True Then
                Send(String.Empty, "CARDVIEWERREFRESH", socket)
            Else
                Send(String.Empty, "CARDVIEWERREFRESHCONTROLS", socket)
            End If
        Catch ex As Exception
            Call errorsub(ex.ToString, "guildbankcardadd")
        End Try
    End Sub

    Private Sub guildbankcardremove(ByVal incoming As String, ByVal socket As Integer)
        Try
            Dim lostvar As String = String.Empty, sCardname As String = String.Empty
            Dim oDataRow As DataRow
            Dim oPlayerAccount As New PlayerAccountDAL(frmSystray.Winsock(socket).Tag)
            Dim oTables As New BusinessLayer.Tables

            Divide(incoming, " ", lostvar, sCardname)

            sCardname = sCardname.Replace("%20", " ")

            oDataRow = GetDataRow("call sps_GuildBankCardInfo('" & oPlayerAccount.Guild & "', '" & sCardname & "')")

            If Not (oDataRow Is Nothing) Then

                If oPlayerAccount.Guild = String.Empty Then
                    Call blockchat(String.Empty, "Bank Error", "You are not in a guild", oPlayerAccount.PlayerSocket)
                    Exit Sub
                ElseIf Not (oPlayerAccount.GuildStatus = 2 Or oPlayerAccount.GuildStatus = 1) Then
                    Call blockchat(String.Empty, "Bank Error", "You do not have rights to withdrawl a card from this guild", oPlayerAccount.PlayerSocket)
                    Exit Sub
                ElseIf Val(oDataRow.Item("Total").ToString) <= 0 Then
                    Call blockchat(String.Empty, "Bank Error", "The guild bank does not have any of this card to withdraw", oPlayerAccount.PlayerSocket)
                ElseIf oTables.FindTablePlayer(Tables, oPlayerAccount.Player) = True Then
                    Call blockchat(String.Empty, "Bank Error", "You cannot engage in guild bank transactions while playing a game.  Try again later.", oPlayerAccount.PlayerSocket)
                Else
                    If DeleteBankCard(oPlayerAccount.Guild, sCardname) = True Then
                        Call InsertGuildHistory(oPlayerAccount.Guild, "Card Remove", String.Concat(oPlayerAccount.Player, " removed a ", sCardname, " from the bank."))
                        Call historyadd(oPlayerAccount.Player, 21, String.Concat(sCardname, " was removed from ", oPlayerAccount.Guild, " guild bank."))
                        Call cardchg(oPlayerAccount.Player, sCardname, 1)
                        Call blockchat(String.Empty, "Bank", String.Concat(sCardname, " was removed from the guild bank and added to your card inventory."), oPlayerAccount.PlayerSocket)
                    Else
                        Call blockchat(String.Empty, "Bank Error", "Server could not process your request.  Please try again later.", oPlayerAccount.PlayerSocket)
                        Exit Sub
                    End If
                End If
            Else
                Call blockchat(String.Empty, "Bank Error", "You do not have enough cards to add to the vault", oPlayerAccount.PlayerSocket)
            End If

            Send(String.Empty, "GUILDBANKREFRESH", socket)
        Catch ex As Exception
            Call errorsub(ex.ToString, "guildbankcardremove")
        End Try
    End Sub

    Private Sub guildbankdepositgold(ByVal incoming As String, ByVal socket As Integer)
        Try
            Dim lostvar As String = String.Empty, sGold As String = String.Empty
            Dim oDataRow As DataRow
            Dim oPlayerAccount As New PlayerAccountDAL(frmSystray.Winsock(socket).Tag)
            Dim oChatFunctions As New ChatFunctions
            Dim oTables As New BusinessLayer.Tables

            Divide(incoming, " ", lostvar, sGold)

            If Val(sGold) < 0 Then
                oChatFunctions.uniwarn(String.Concat(oPlayerAccount.Player, " tried to deposit less than 0 gold and got around client filter!"))
                Exit Sub
            End If

            oDataRow = GuildBankGold(oPlayerAccount.Guild)

            If Not (oDataRow Is Nothing) Then
                If oPlayerAccount.Guild = String.Empty Then
                    Call blockchat(String.Empty, "Bank Error", "You are not in a guild", oPlayerAccount.PlayerSocket)
                    Exit Sub
                ElseIf oTables.FindTablePlayer(Tables, oPlayerAccount.Player) = True Then
                    Call blockchat(String.Empty, "Bank Error", "You cannot engage in guild bank transactions while playing a game.  Try again later.", oPlayerAccount.PlayerSocket)
                    Exit Sub
                ElseIf Val(sGold) > oPlayerAccount.Gold Then
                    Call blockchat(String.Empty, "Bank Error", "You do not have enough gold to deposit", oPlayerAccount.PlayerSocket)
                    Exit Sub
                Else
                    Call gpadd(oPlayerAccount.Player, -1 * Val(sGold), True)
                    Call UpdateBankGold(oPlayerAccount.Guild, Val(sGold))
                    Call InsertGuildHistory(oPlayerAccount.Guild, "Bank Deposit", String.Concat(oPlayerAccount.Player, " added a ", sGold, " GP to the bank."))
                    Call historyadd(oPlayerAccount.Player, 21, String.Concat(sGold, " GP was added to ", oPlayerAccount.Guild, " guild bank."))
                    Call blockchat(String.Empty, "Bank", String.Concat(sGold, " GP was added to the guild bank."), oPlayerAccount.PlayerSocket)
                End If
            Else
                Call blockchat(String.Empty, "Bank Error", "You do not have enough gold to deposit", oPlayerAccount.PlayerSocket)
                Exit Sub
            End If

            Send(String.Empty, "GUILDBANKREFRESH", socket)
        Catch ex As Exception
            Call errorsub(ex.ToString, "guildbankdepositgold")
        End Try
    End Sub

    Private Sub guildbankwithdrawgold(ByVal incoming As String, ByVal socket As Integer)
        Try
            Dim lostvar As String = String.Empty, sGold As String = String.Empty
            Dim oDataRow As DataRow
            Dim oPlayerAccount As New PlayerAccountDAL(frmSystray.Winsock(socket).Tag)
            Dim oChatFunctions As New ChatFunctions, oTables As New BusinessLayer.Tables

            Divide(incoming, " ", lostvar, sGold)

            If Val(sGold) < 0 Then
                oChatFunctions.uniwarn(String.Concat(oPlayerAccount.Player, " tried to withdrawl less than 0 gold and got around client filter!"))
                Exit Sub
            End If

            oDataRow = GuildBankGold(oPlayerAccount.Guild)

            If Not (oDataRow Is Nothing) Then
                If Not (oPlayerAccount.GuildStatus = 2 Or oPlayerAccount.GuildStatus = 1) Then
                    Call blockchat(String.Empty, "Bank Error", "You do not have rights to withdrawl gold from this guild", socket)
                ElseIf oPlayerAccount.Guild = String.Empty Then
                    Call blockchat(String.Empty, "Bank Error", "You are not in a guild", oPlayerAccount.PlayerSocket)
                    Exit Sub
                ElseIf oTables.FindTablePlayer(Tables, oPlayerAccount.Player) = True Then
                    Call blockchat(String.Empty, "Bank Error", "You cannot engage in guild bank transactions while playing a game.  Try again later.", oPlayerAccount.PlayerSocket)
                ElseIf Val(sGold) > Val(oDataRow.Item("gold").ToString) Then
                    Call blockchat(String.Empty, "Bank Error", "The guild does not have enough gold for your withdrawl", oPlayerAccount.PlayerSocket)
                Else
                    Call gpadd(oPlayerAccount.Player, Val(sGold), True)
                    Call UpdateBankGold(oPlayerAccount.Guild, (Val(sGold) * -1))
                    Call InsertGuildHistory(oPlayerAccount.Guild, "Bank Withdrawl", String.Concat(oPlayerAccount.Player, " removed ", sGold, " GP from the bank."))
                    Call historyadd(oPlayerAccount.Player, 21, String.Concat(sGold, " GP was removed from ", oPlayerAccount.Guild, " guild bank."))
                    Call blockchat(String.Empty, "Bank", String.Concat(sGold, " GP was removed from the guild bank."), oPlayerAccount.PlayerSocket)
                End If
            Else
                Call blockchat(String.Empty, "Bank Error", "The guild does not have enough gold for your withdrawl", oPlayerAccount.PlayerSocket)
            End If

            Send(String.Empty, "GUILDBANKREFRESH", socket)
        Catch ex As Exception
            Call errorsub(ex.ToString, "guildbankwithdrawgold")
        End Try
    End Sub

    Public Sub guildchat(ByVal incoming As String, ByVal socket As Integer)
        Try
            Dim oPlayerAccount As New PlayerAccountDAL(frmSystray.Winsock(socket).Tag)

            Dim oDataSet As New DataSet
            oDataSet = GuildOnlineUsers(oPlayerAccount.Guild)

            If oPlayerAccount.Guild = String.Empty Then
                Call blockchat(String.Empty, "Error", "You are not a part of a guild.", socket)
                Exit Sub
            End If

            Dim sChat As String = Mid(incoming, 5, incoming.Length).Trim

            For x As Integer = 0 To oDataSet.Tables(0).Rows.Count - 1
                If oDataSet.Tables(0).Rows(x).Item("socket").ToString <> "" Then
                    Send(String.Empty, String.Concat("CHAT ", frmSystray.Winsock(socket).Tag, " ", sChat), oDataSet.Tables(0).Rows(x).Item("socket"))
                    System.Windows.Forms.Application.DoEvents()
                End If
            Next

            oDataSet = Nothing
        Catch ex As Exception
            Call errorsub(ex.ToString, "guildchat")
        End Try
    End Sub

    Private Sub guilddisband(ByVal socket As Integer)
        Try
            Dim oPlayerAccount As New PlayerAccountDAL(frmSystray.Winsock(socket).Tag)

            If oPlayerAccount.Guild <> String.Empty Then
                oPlayerAccount.UpdateField("guild", String.Empty)
                oPlayerAccount.UpdateField("guildstatus", String.Empty)

                Call sendall(String.Concat("UPDATEPLAYER ", oPlayerAccount.Player, " ", oPlayerAccount.Surname.Replace(" ", "%20")))
                Send(String.Empty, "UPDATEGUILD", socket)
            End If
        Catch ex As Exception
            Call errorsub(ex.ToString, "guilddisband")
        End Try
    End Sub
End Class
