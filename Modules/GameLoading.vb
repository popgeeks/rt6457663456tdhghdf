Imports System.Configuration
Imports mySQL.Data.MySqlClient
Imports MySQLFactory

Module GameLoading
	'Game Loading Module
	'Since I didin't want to make the same mistake I did for Registration
	'I created this module to handle the game loading

    Private Function KickOffPeople(ByVal sPlayer As String, ByVal socket As Integer) As Boolean
        Try
            'Dim bFound As Boolean = False

            For x As Integer = 0 To (frmSystray.Winsock.Count - 1)
                If frmSystray.Winsock(x).Tag = sPlayer And sPlayer <> String.Empty And x <> socket And x <> 0 Then
                    Dim oTables As New BusinessLayer.Tables

                    oServerConfig.KickOff += 1
                    oServerConfig.DeleteUser(x, frmSystray.Winsock(x).Tag)
                    RemPlayer(sPlayer, x, False)
                    frmSystray.Winsock(x).Close()
                    'bFound = True
                End If
            Next

            Return False
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Sub ReconnectGame(ByRef oPlayerAccount As PlayerAccountDAL, ByVal zSocket As Integer)
        frmSystray.Winsock(zSocket).Tag = oPlayerAccount.Player
        'frmserver.lstplayers.Items.Item(zSocket) = String.Concat(zSocket, ": ", oPlayerAccount.Player)
        Call historyadd(oPlayerAccount.Player, 11, String.Concat(oPlayerAccount.Player, " reconnected (", frmSystray.Winsock(zSocket).RemoteHostIP.ToString, ")"))

        For x As Integer = 0 To (frmSystray.Winsock.Count - 1)
            If frmSystray.Winsock(x).Tag = oPlayerAccount.Player And oPlayerAccount.Player <> String.Empty And x <> zSocket And x <> 0 Then
                Dim oTables As New BusinessLayer.Tables

                oServerConfig.DeleteUser(x, frmSystray.Winsock(x).Tag)
                frmSystray.Winsock(x).Close()
            End If
        Next

        oServerConfig.InsertUser(zSocket, oPlayerAccount.Player)

        Send(String.Empty, "QUERYRECONNECTPLAYERS", zSocket)
    End Sub

    Public Sub LoadGame(ByRef oPlayerAccount As PlayerAccountDAL, ByVal zSocket As Integer)
        Try
            'Dim oPlayerAccount As New PlayerAccountDAL(zNick)
            Dim bFound As Boolean = False

            bFound = KickOffPeople(oPlayerAccount.Player, zSocket)

            If oPlayerAccount.Player <> String.Empty Then
                Send(String.Empty, "LOADGAME-OK", zSocket)

                If oPlayerAccount.Verified = False Then oPlayerAccount.VerifyUser()

                frmSystray.Winsock(zSocket).Tag = oPlayerAccount.Player
                PlayerList.Add(oPlayerAccount.Player)

                If oPlayerAccount.Gold < 0 Then
                    Dim oChatFunctions As New ChatFunctions

                    oChatFunctions.uniwarn(String.Concat(oPlayerAccount.Player, " had less than 0 gold and bypassed all filters.  Player is auto banned."))

                    Send(String.Empty, "EXIT You have been kicked for gold duping.  You had less than zero gold.  Send a ticket immediately and explain what you did prior to this message and the ban may be lifted.  This is a security precaution.", zSocket)
                    oPlayerAccount.UpdateField("banned", "1", True)
                    Call quit("QUIT " & oPlayerAccount.Player, zSocket, False)
                End If

                'frmserver.lstplayers.Items.Item(zSocket) = String.Concat(zSocket, ": ", oPlayerAccount.Player)
                'frmserver.lstping.Items.Item(zSocket) = "2"

                players(zSocket).nickname = oPlayerAccount.Player '<<-- added by Jonathan 1/18/00

                oServerConfig.InsertUser(zSocket, oPlayerAccount.Player)

                writeini("Play", "Playing", "false", My.Application.Info.DirectoryPath & "\accounts\" & oPlayerAccount.Player & ".ttd")
                frmserver.Timer1.Enabled = True

                'Call GivePlayers(oPlayerAccount, zSocket, bFound)

                Call findtotalips(oPlayerAccount.Player, frmSystray.Winsock(zSocket).RemoteHostIP)
                Call historyadd(oPlayerAccount.Player, 11, String.Concat(oPlayerAccount.Player, " logged in (", frmSystray.Winsock(zSocket).RemoteHostIP.ToString, ")"))

                oPlayerAccount.AdminStatus = False
                oPlayerAccount.LastIP = frmSystray.Winsock(zSocket).RemoteHostIP
                oPlayerAccount.LastLogin = Date.Today.ToString
                oPlayerAccount.FailedLogins = 0
                oPlayerAccount.Login_UpdateAccount()

                Send(String.Empty, String.Concat("MOTD ", oServerConfig.MessageOfTheDay), zSocket)
                Send(String.Empty, String.Concat("SHOWMAIN ", oPlayerAccount.Gold, " N/A ", oPlayerAccount.Experience, " ", oPlayerAccount.Level, " ", oPlayerAccount.NextLevel, " ", oPlayerAccount.FailedLogins, " ", oPlayerAccount.Gender, " ", oPlayerAccount.Background, " ", oPlayerAccount.Body, " ", oPlayerAccount.Head, " " & oPlayerAccount.Cash, " ", oPlayerAccount.Guild.Replace(" ", "%20"), " ", oPlayerAccount.RushGauge, " ", oPlayerAccount.QuestCount, " ", oPlayerAccount.AP, " ", oServerConfig.TTChest, " ", oServerConfig.THChest, " ", oServerConfig.WarChest, " ", oServerConfig.DailyJackpot, " ", oServerConfig.WeeklyJackpot), zSocket)

                Send(String.Empty, "QUERYPLAYERS", zSocket)

                'frmserver.lstping.Items.Item(zSocket) = "1"
                players(zSocket).Away = False

                'playerlist(zSocket).awaystatus = False
                'playerlist(zSocket).activeminutes = 0
            Else
                Send(String.Empty, "LOADGAME-NOSUCHNICK", zSocket)
                Exit Sub
            End If

            'Dim sReason As String = String.Empty
            'Dim iPlayers As Integer = findtotalplayers() + oServerConfig.Zombies

            'If iPlayers = 200 Then
            '    sReason = "EXP obtained in games increased by 50%, 200 players reached."
            'ElseIf iPlayers = 100 Then
            '    sReason = "EXP obtained in games increased by 30%, 100 players reached."
            'ElseIf iPlayers = 75 Then
            '    sReason = "EXP obtained in games increased by 20%, 75 players reached."
            'ElseIf iPlayers = 65 Then
            '    sReason = "EXP obtained in games increased by 15%, 65 players reached."
            'ElseIf iPlayers = 50 Then
            '    sReason = "EXP obtained in games increased by 10%, 50 players reached."
            'ElseIf iPlayers = 40 Then
            '    sReason = "EXP obtained in games increased by 5%, 40 players reached."
            'End If

            'If sReason <> String.Empty Then
            '    Call historyadd("System", 12, sReason)
            '    Call sendall(String.Concat("CHATBLOCK Trigger ", sReason.Replace(" ", "%20")))
            'End If

            'If oPlayerAccount.Country = String.Empty Then Call blockchat(String.Empty, "FYI", "Did you know that you have not told us what country you live in?  Open up the edit your account screen under menu->player maintenance and set it for us!", zSocket)

            Call SendPlayerInfo(oPlayerAccount, zSocket)
        Catch ex As Exception
            Call errorsub(ex, "LoadGame")
        End Try
    End Sub

    Private Sub SendPlayerInfo(ByVal oPlayerAccount As PlayerAccountDAL, ByVal xSocket As Integer)
        Try
            Dim oDatabaseFunctions As New DatabaseFunctions
            Dim oTable As DataTable = oDatabaseFunctions.OnlinePlayers
            If oPlayerAccount.Player = String.Empty Then Exit Sub

            If Not (oTable Is Nothing) Then
                For x As Integer = 0 To oTable.Rows.Count - 1
                    If oTable.Rows(x).Item("Player") <> oPlayerAccount.Player Then
                        Dim iID As Integer = Val(oTable.Rows(x).Item("ID").ToString)
                        If frmSystray.Winsock(iID).Tag <> String.Empty Then Send(String.Empty, "NEWPLAYER " & oPlayerAccount.AdminLevel & " " & oPlayerAccount.Country.Replace(" ", "%20") & " " & oPlayerAccount.AchievementScore & " " & oPlayerAccount.Membership.Replace(" ", "%20") & " " & String.Concat(oPlayerAccount.Player, " ", oPlayerAccount.Surname.Replace(" ", "%20"), " ", (IIf(oPlayerAccount.Guild <> String.Empty, "<" & oPlayerAccount.Guild & ">", String.Empty)).Replace(" ", "%20")).Replace(" ", "%20"), iID)
                    End If
                Next x
            End If

            Send(String.Empty, "NEWPLAYER2     " & String.Concat(oPlayerAccount.Player, " ", oPlayerAccount.Surname.Replace(" ", "%20"), " ", (IIf(oPlayerAccount.Guild.ToString <> String.Empty, "<" & oPlayerAccount.Guild.ToString & ">", String.Empty)).Replace(" ", "%20")).Replace(" ", "%20"), xSocket)
        Catch ex As Exception
            Call errorsub(ex, "SendPlayerInfo")
        End Try
    End Sub

    Public Function findtotalips(ByVal sNick As String, ByVal sIP As String) As Integer
        Dim oChatFunctions As New ChatFunctions

        findtotalips = 1

        For x As Integer = 0 To (frmSystray.Winsock.Count - 1)
            If frmSystray.Winsock(x).RemoteHostIP = sIP And LCase(frmSystray.Winsock(x).Tag) <> LCase(sNick) And LCase(frmSystray.Winsock(x).Tag) <> "server" Then
                If Trim(frmSystray.Winsock(x).Tag) <> String.Empty Then
                    findtotalips = findtotalips + 1

                    If findtotalips > 1 Then
                        oChatFunctions.uniwarn(sNick & " and " & frmSystray.Winsock(x).Tag & " are logged on with the same IP!")
                        Call historyadd(sNick, 2, sNick & " and " & frmSystray.Winsock(x).Tag & " are logged on with the same IP!")
                    End If
                End If
            End If
        Next x

        'frmserver.lblplayers.Text = findtotalplayers().ToString
    End Function
End Module