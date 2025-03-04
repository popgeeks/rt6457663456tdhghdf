Imports System.Configuration

Public Class AdminFunctions
    Public Sub AdminModule(ByVal sCommand As String, ByVal incoming As String, ByVal socket As Integer)
        Select Case sCommand
            Case "ADMINSILENCEPLAYER"
                Call adminsilenceplayer(incoming, socket)
            Case "ADMINMOD"
                Call adminmod(incoming, socket)
            Case "ADMINCHAT"
                Call adminchat(incoming, socket)
            Case "ADMINLOGIN"
                Call adminlogin(incoming, socket)
            Case "ADMINKICK"
                Call kick(incoming, socket)
            Case "ADMINWARN"
                Call warn(incoming, socket)
            Case "ADMINBAN"
                Call ban(incoming, socket)
            Case "ADMINKEYWORD"
                Call adminkeyword(incoming, socket)
            Case "ADMINREMOVETRIGGER"
                Call adminremovetrigger(incoming, socket)
            Case "ADMINCHATCLEAR"
                Call chatclear(incoming, socket)
        End Select
    End Sub

    Public Sub ban(ByVal incoming As String, ByVal socket As Integer)
        Try
            Dim player As String = String.Empty
            Dim reason As String = String.Empty, lostvar As String = String.Empty

            Divide(incoming, " ", lostvar, player, reason)

            Dim oAdminAccount As New PlayerAccountDAL(frmSystray.Winsock(socket).Tag)
            Dim oPlayerAccount As New PlayerAccountDAL(player)
            Dim oChatFunctions As New ChatFunctions

            If (oAdminAccount.AdminLevel <> 1) And (oPlayerAccount.AdminLevel <> 4) And (oAdminAccount.AdminLevel <> 0) And socket <> 0 Or lostvar = "Server" Then
                Dim oDataLayer As New DataLayer.AdminFunctions(ConnectionString)
                oDataLayer.BanInsert(oPlayerAccount.Player, oAdminAccount.Player, reason.Replace("%20", " "), oPlayerAccount.LastIP)
                oServerConfig.MySQLCalls += 1

                Send(String.Empty, String.Concat("EXIT You have been banned for: ", reason), oPlayerAccount.PlayerSocket)
                oChatFunctions.uniwarn(String.Concat(player, " was banned by ", oAdminAccount.Player, " - Reason: ", reason))

                Call quit("QUIT " & player, oPlayerAccount.PlayerSocket, False)
                'VB6.SetItemString(frmserver.lstplayers, oPlayerAccount.PlayerSocket, oPlayerAccount.PlayerSocket & ":")
            End If
        Catch ex As Exception
            Call errorsub(ex, "ban")
        End Try
    End Sub

    Private Sub kick(ByVal incoming As String, ByVal socket As Integer)
        Try
            Dim player As String = String.Empty
            Dim reason As String = String.Empty, lostvar As String = String.Empty

            Divide(incoming, " ", lostvar, player, reason)

            Dim oAdminAccount As New PlayerAccountDAL(frmSystray.Winsock(socket).Tag)
            Dim oPlayerAccount As New PlayerAccountDAL(player)
            Dim oChatFunctions As New ChatFunctions

            If (oAdminAccount.AdminLevel <> 1) And (oPlayerAccount.AdminLevel <> 4) And (oAdminAccount.AdminLevel <> 0) And socket <> 0 Then
                Dim oDataLayer As New DataLayer.AdminFunctions(ConnectionString)
                oDataLayer.KickInsert(oPlayerAccount.Player, oAdminAccount.Player, reason.Replace("%20", " "))
                oServerConfig.MySQLCalls += 1

                Send(String.Empty, String.Concat("EXIT You have been kicked for: ", reason), oPlayerAccount.PlayerSocket)
                oChatFunctions.uniwarn(String.Concat(player, " was kicked by ", oAdminAccount.Player, " - Reason: ", reason))

                Call quit("QUIT " & player, oPlayerAccount.PlayerSocket, False)
            End If
        Catch ex As Exception
            Call errorsub(ex, "kick")
        End Try
    End Sub

    Private Sub warn(ByVal incoming As String, ByVal socket As Integer)
        Try
            Dim player As String = String.Empty
            Dim reason As String = String.Empty, lostvar As String = String.Empty

            Divide(incoming, " ", lostvar, player, reason)

            Dim oAdminAccount As New PlayerAccountDAL(frmSystray.Winsock(socket).Tag)
            Dim oPlayerAccount As New PlayerAccountDAL(player)
            Dim oChatFunctions As New ChatFunctions

            If (oAdminAccount.AdminLevel <> 1) And (oPlayerAccount.AdminLevel <> 4) And (oAdminAccount.AdminLevel <> 0) And socket <> 0 Then
                Dim oDataLayer As New DataLayer.AdminFunctions(ConnectionString)
                oDataLayer.WarnInsert(oPlayerAccount.Player, oAdminAccount.Player, reason.Replace("%20", " "))
                oServerConfig.MySQLCalls += 1

                oChatFunctions.uniwarn(String.Concat(player, " was warned by ", oAdminAccount.Player, " - Reason: ", reason))
                Send(String.Empty, String.Concat("ADMINWARN ", reason), oPlayerAccount.PlayerSocket)

                'oChatFunctions.unisend("ADMINOPCOMMAND", String.Concat("warn ", player, " ", oAdminAccount.Player, " ", reason.Replace(" ", "%20")))
            End If
        Catch ex As Exception
            Call errorsub(ex, "warn")
        End Try
    End Sub

    Private Sub adminmod(ByVal incoming As String, ByVal socket As Integer)
        Try
            Dim oChatFunctions As New ChatFunctions

            Dim lostvar As String = String.Empty, status As String = String.Empty
            Dim strReason As String = String.Empty

            Divide(incoming, " ", lostvar, status)

            Dim oPlayerAccount As New PlayerAccountDAL(frmSystray.Winsock(socket).Tag)

            If oPlayerAccount.AdminStatus = True And oPlayerAccount.AdminLevel > 0 Then
                If status = "1" Then
                    strReason = String.Concat(frmSystray.Winsock(socket).Tag, "%20sets%20mode:%20+m")
                    oServerConfig.ModeratedChat = True
                ElseIf status = "0" Then
                    strReason = String.Concat(frmSystray.Winsock(socket).Tag, "%20sets%20mode:%20-m")
                    oServerConfig.ModeratedChat = False
                Else
                    Exit Sub
                End If

                oChatFunctions.unisend("CHATBLOCK", "Moderation " & strReason.Replace(" ", "%20"))
            Else
                Call blockchat(String.Empty, "Error", "You must be logged in to use this command.", oPlayerAccount.PlayerSocket)
            End If
        Catch ex As Exception
            Call errorsub(ex, "adminmod")
        End Try
    End Sub

    Private Sub adminchat(ByVal incoming As String, ByVal socket As Integer)
        Dim chattxt As String = Mid(incoming, 10, incoming.Length)

        Dim oPlayerAccount As New PlayerAccountDAL(frmSystray.Winsock(socket).Tag)

        If (oPlayerAccount.AdminLevel <> 0) And socket <> 0 Then
            If AdminList.AdminOnline(frmSystray.Winsock(socket).Tag) = False Then
                Call blockchat(String.Empty, "Error", "You must be logged in to talk in admin chat.", socket)
                Exit Sub
            End If

            If AdminList.Count > 0 Then
                For x As Integer = 0 To AdminList.Count - 1
                    Dim strAdmin As String = AdminList(x).Player
                    Send(String.Empty, String.Concat("ADMINCHAT ", frmSystray.Winsock(socket).Tag, " ", chattxt.Replace(" ", "%20")), GetSocket(strAdmin))
                Next x
            End If
        Else
            Call blockchat(String.Empty, "Error", "You must be logged in to talk in admin chat.", socket)
            Exit Sub
        End If
    End Sub

    Private Sub chatclear(ByVal incoming As String, ByVal socket As Integer)
        Dim oPlayerAccount As New PlayerAccountDAL(frmSystray.Winsock(socket).Tag)

        If (oPlayerAccount.AdminLevel = 4) And socket <> 0 Then
            Dim oChatFunctions As New ChatFunctions
            oChatFunctions.unisend("ADMINCHATCLEAR", String.Empty)
        End If
    End Sub

    Private Sub adminlogin(ByVal incoming As String, ByVal socket As Integer)
        Try
            Dim passcode As String = String.Empty, lostvar As String = String.Empty
            Divide(incoming, " ", lostvar, passcode)

            If frmSystray.Winsock(socket).Tag = String.Empty Then Exit Sub

            Dim oPlayerAccount As New PlayerAccountDAL(frmSystray.Winsock(socket).Tag)

            If oPlayerAccount.PassKey <> passcode Then
                Send(String.Empty, "ADMINPASSERROR", socket)
                Exit Sub
            End If

            If (oPlayerAccount.AdminLevel = 4) And LCase(frmSystray.Winsock(socket).Tag) <> "darklumina" And LCase(frmSystray.Winsock(socket).Tag) <> "atomicstorm" Then
                Send(String.Empty, "ADMINSTATUS 4", socket)

                'RemAdmin(frmSystray.Winsock(socket).Tag)
                AdminList.Add(frmSystray.Winsock(socket).Tag, 4)
            ElseIf (oPlayerAccount.AdminLevel = 3) Then
                Send(String.Empty, "ADMINSTATUS 3", socket)
                AdminList.Add(frmSystray.Winsock(socket).Tag, 3)
            ElseIf (oPlayerAccount.AdminLevel = 2) Then
                Send(String.Empty, "ADMINSTATUS 2", socket)
                'RemAdmin(frmSystray.Winsock(socket).Tag)
                AdminList.Add(frmSystray.Winsock(socket).Tag, 2)
            ElseIf (oPlayerAccount.AdminLevel = 1) Then
                Send(String.Empty, "ADMINSTATUS 1", socket)
                'RemAdmin(frmSystray.Winsock(socket).Tag)
                AdminList.Add(frmSystray.Winsock(socket).Tag, 1)
            ElseIf (oPlayerAccount.AdminLevel = 4) And (LCase(frmSystray.Winsock(socket).Tag) = "darklumina") Or (LCase(frmSystray.Winsock(socket).Tag) = "atomicstorm") Then
                Send(String.Empty, "ADMINSTATUS 5", socket)
                AdminList.Add(frmSystray.Winsock(socket).Tag, 5)
            Else
                Send(frmSystray.Winsock(socket).Tag, "ADMINNO")
                Exit Sub
            End If

            oPlayerAccount.UpdateField("adminstatus", "on", False)
            'frmserver.lbladmin.Text = frmserver.lstadmin.Items.Count.ToString
        Catch ex As Exception
            Call errorsub(ex, "adminlogin")
        End Try
    End Sub

    Private Sub adminsilenceplayer(ByVal incoming As String, ByVal socket As Integer)
        Try
            Dim sNick As String = String.Empty, lostvar As String = String.Empty

            Divide(incoming, " ", lostvar, sNick)

            If sNick = String.Empty Then Exit Sub

            If sNick.ToLower = "atomicstorm" Then
                Call blockchat(String.Empty, "Failed", "You cannot silence atomicstorm because he is a server operator.", socket)
                Exit Sub
            End If

            Dim oPlayerAccount As New PlayerAccountDAL(frmSystray.Winsock(socket).Tag)

            If oPlayerAccount.AdminLevel <> 0 Then
                Dim oChatFunctions As New ChatFunctions
                Dim oDataLayer As New DataLayer.AdminFunctions(ConnectionString)

                oDataLayer.SilenceInsert(sNick)
                oServerConfig.MySQLCalls += 1

                Call blockchat(String.Empty, "Silence", String.Concat("You have successfully silenced ", sNick, " for 5 minutes."), socket)
                Call blockchat(sNick, "Silence", "You have been silenced by " & frmSystray.Winsock(socket).Tag & ".  You may not talk for approximately 5 minutes.")
                Call blockchat(sNick, "Silence", "Should you log off, the silence effect will remain until you expire the length of your sanction.")
            End If
        Catch ex As Exception
            Call errorsub(ex, "adminsilenceplayer")
        End Try
    End Sub

    Private Sub adminkeyword(ByVal incoming As String, ByVal socket As Integer)
        Try
            Dim key As String = String.Empty, lostvar As String = String.Empty
            Dim oAdminAccount As New PlayerAccountDAL(frmSystray.Winsock(socket).Tag)
            Dim oChatFunctions As New ChatFunctions

            Divide(incoming, " ", lostvar, key)

            Dim oDataLayer As New DataLayer.AdminFunctions(ConnectionString)
            Dim sKeyword As String = oDataLayer.FindKeywords(key)
            oServerConfig.MySQLCalls += 1

            If sKeyword = String.Empty Then Exit Sub

            If oAdminAccount.AdminStatus = True Then oChatFunctions.unisend("ADMINKEYWORD", String.Concat(key, " ", sKeyword.Replace(" ", "%20")))
        Catch ex As Exception
            Call errorsub(ex, "adminkeyword")
        End Try
    End Sub

    Private Sub adminremovetrigger(ByVal incoming As String, ByVal socket As Integer)
        Try
            If AdminList.Count > 0 Then
                For x As Integer = 0 To AdminList.Count - 1
                    Dim strAdmin As String = AdminList(x).Player
                    Send(strAdmin, incoming)
                Next x
            End If
        Catch ex As Exception
            Call errorsub(ex, "adminremovetrigger")
        End Try
    End Sub
End Class
