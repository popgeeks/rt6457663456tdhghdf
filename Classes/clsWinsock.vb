Imports System.Configuration
Imports mySQL.Data.MySqlClient
Imports MySQLFactory

Public Class clsWinsock
    Private pstrDatatoProcess As String
    Private sDecrypted As String
    Private socket As Integer
    Private dBegin As DateTime
    Private dEnd As DateTime
    Private sPacket As String
    Private Const CharList As String = " ,/.!@#$%^&*()?><';:+-=_[]{}|"

    Public Sub New(ByVal sPacket As String, ByVal iSocket As Integer)
        pstrDatatoProcess = sPacket
        socket = iSocket
    End Sub

    Public Property BeginProcessing() As DateTime
        Get
            Return dBegin
        End Get
        Set(ByVal value As DateTime)
            dBegin = value
        End Set
    End Property

    Public Property EndProcessing() As DateTime
        Get
            Return dEnd
        End Get
        Set(ByVal value As DateTime)
            dEnd = value
        End Set
    End Property

    Public Property DecryptedIncoming() As String
        Get
            Return sDecrypted
        End Get
        Set(ByVal value As String)
            sDecrypted = value
        End Set
    End Property

    Public ReadOnly Property ProcessingTime() As TimeSpan
        Get
            Dim dTimeSpan As TimeSpan = EndProcessing.Subtract(BeginProcessing)
        End Get
    End Property

    Public ReadOnly Property ProcessingSeconds() As Decimal
        Get
            Try
                Dim dTimeSpan As TimeSpan = EndProcessing.Subtract(BeginProcessing)
                Return Convert.ToDecimal(dTimeSpan.TotalSeconds)
            Catch ex As Exception
                Return 0.0
            End Try
        End Get
    End Property

    Public Sub HandleQueue()
        Try
            Dim sCommand As String = String.Empty
            Dim incoming As String = String.Empty
            Dim oProcessQueue As New ProcessQueue

            BeginProcessing = Date.Now

            Divide(pstrDatatoProcess, " ", sCommand)
            Call oProcessQueue.IncomingModule(sCommand, pstrDatatoProcess)
            oProcessQueue = Nothing

            EndProcessing = Date.Now
        Catch ex As Exception
            Call errorsub(ex, "HandleQueue")
        End Try
    End Sub

    Public Sub HandleIncoming(ByVal bSkip As Boolean)
        'Variables :)

        Dim sCommand As String = String.Empty

        BeginProcessing = Date.Now

        Dim blnThisProcedureIsCurrent As Boolean
        Dim incoming As String = String.Empty

        'Debug.Print frmSystray.Winsock(socket).Tag & ">> " & pstrDataToProcess

        'exit the funciton if this socket does not have a class
        'associeated with it
        If players(socket) Is Nothing Then GoTo ExitFunction 'Jon 1/18/00

        'add the current data to the list of data that's waiting
        'to be processed for this socket
        players(socket).DataWaitingToBeProcessed = players(socket).DataWaitingToBeProcessed & pstrDatatoProcess

        players(socket).Ping = True
        'If this routine is called while data is already being processed
        'for this socket, then we want to just exit the routine
        'without actually processing any of the data.  because we've
        'already added the data to players(Socket).DataWaitingToBeProcessed,
        'the data will get processed as soon as the first instance of this
        'routine finishes processing it's own data.

        If players(socket).ProcessingData = True Then GoTo ExitFunction 'Jon 1/18/00

        'Added by Jonathan 1/18/99
        'if the code gets this far, then this is the first time that
        'this routine has been called for this socket, so we want to
        'go ahead and process the data.  but before we do, we want to
        'set some flags.
        'The first flag we're gonna set is to make sure that no other
        'routines start processing data for this socket:

        players(socket).ProcessingData = True

        'the second flag will be used to tell us later that it's ok
        'to UNSET the first flag, and allow data to be processed
        'for this socket once again.

        blnThisProcedureIsCurrent = True

        Do Until players(socket).DataWaitingToBeProcessed = String.Empty
            'Divide the string so we get ony one line
            '       StringDivide players(socket).DataWaitingToBeProcessed, Chr(13), incoming, Rest
            Dim sItems() As String = players(socket).DataWaitingToBeProcessed.Split(Chr(13))

            incoming = sItems(0)
            sPacket = incoming

            If sItems.GetUpperBound(0) > 0 Then
                players(socket).DataWaitingToBeProcessed = sItems(1)
            Else
                players(socket).DataWaitingToBeProcessed = String.Empty
            End If

            sItems = Nothing

            'Find out which command we are trying to do

            'If (Left(incoming, 4) <> "CHAT" Or Left(incoming, 8) <> "GAMECHAT") Or bSkip = False Then

            'If (Left(incoming, 4) <> "CHAT") Or bSkip = False Then
            '    'Debug.Print incoming
            '    Dim oDecrypt As New LogicLayer.Utility.Encryption
            '    incoming = oDecrypt.Decode(incoming)
            'End If

            DecryptedIncoming = incoming

            Debug.Print(incoming)
            Divide(incoming, " ", sCommand)

            If Left(incoming, 7) = "VERSION" And Right(incoming, 4) = "****" Then
                Send(String.Empty, "UPDATE", socket)
                GoTo ExitFunction
            End If

            If checkpacket(socket, sCommand) = True Then GoTo ExitFunction

            If Mid(sCommand, 1, 5) = "ADMIN" Then
                Dim oAdmin As New AdminFunctions
                Call oAdmin.AdminModule(sCommand, incoming, socket)
                oAdmin = Nothing
            ElseIf Mid$(sCommand, 1, 5) = "GUILD" Then
                Dim oGuilds As New Guilds
                oGuilds.GuildModule(sCommand, incoming, socket)
                oGuilds = Nothing
            ElseIf Mid$(sCommand, 1, 2) = "SB" Then
                Dim oSphere As New SphereFunctions
                oSphere.GameModule(sCommand, incoming, socket)
                oSphere = Nothing
            ElseIf Mid$(sCommand, 1, 7) = "VETERAN" Then
                Dim oAA As New VeteranAdvancement
                oAA.AAModule(sCommand, incoming, socket)
                oAA = Nothing
            ElseIf Mid$(sCommand, 1, 9) = "ALTERNATE" Then
                Dim oAA As New AlternateAdvancement
                oAA.AAModule(sCommand, incoming, socket)
                oAA = Nothing
            ElseIf Mid$(sCommand, 1, 8) = "CHINCHIN" Then
                Dim oChinchin As New Chinchirorin
                oChinchin.Chinchinmodule(sCommand, incoming, socket)
                oChinchin = Nothing
            ElseIf Mid(sCommand, 1, 3) = "OTT" Then
                Dim oOTT As New OTTFunctions
                Call oOTT.OTTModule(sCommand, incoming, socket)
                oOTT = Nothing
            ElseIf Mid(sCommand, 1, 6) = "MEMORY" Then
                Dim oMemory As New MemoryFunctions
                Call oMemory.MemoryModule(sCommand, incoming, socket)
                oMemory = Nothing
            ElseIf Mid(sCommand, 1, 5) = "TEAM_" Then
                Dim oTeamOTT As New TeamOTTFunctions
                Call oTeamOTT.GameModule(sCommand, incoming, socket)
                oTeamOTT = Nothing
            ElseIf Mid(sCommand, 1, 2) = "TT" Then
                Dim oTT As New TTFunctions
                Call oTT.TTModule(sCommand, incoming, socket)
                oTT = Nothing
            ElseIf Mid(sCommand, 1, 5) = "TABLE" Then
                Dim oTables As New TableFunctions
                Call oTables.IncomingModule(sCommand, incoming, socket)
                oTables = Nothing
            ElseIf Mid(sCommand, 1, 2) = "PQ" Then
                Dim oProcessQueue As New ProcessQueue
                Call oProcessQueue.IncomingModule(sCommand, incoming)
                oProcessQueue = Nothing
            ElseIf Mid(sCommand, 1, 6) = "AVATAR" Then
                Dim oAvatar As New Avatars
                Call oAvatar.IncomingModule(sCommand, incoming, socket)
                oAvatar = Nothing
            ElseIf Mid(sCommand, 1, 8) = "CARDWARS" Then
                Dim oCardWars As New CardWars
                Call oCardWars.IncomingModule(sCommand, incoming, socket)
                oCardWars = Nothing
            Else
                Select Case sCommand.Trim
                    Case "QUERYPLAYERS"
                        Call GivePlayers(socket)
                    Case "RECONNECT"
                        Call Reconnect(incoming, socket)
                    Case "CHALLENGECHAT"
                        Dim oTeamOTT As New TeamOTTFunctions
                        Call oTeamOTT.challengechat(incoming, socket)
                        oTeamOTT = Nothing
                    Case "CHALLENGECHATS"
                        Call challengechats(incoming, socket)
                    Case "GAMEREADY"
                        Call gameready(socket)
                    Case "AWAY"
                        Call awaystatus(incoming, socket)
                    Case "ABNEWDECK"
                        Call abnewdeck(incoming, socket)

                    Case "RUSH"
                        Call Rush(incoming, socket)

                    Case "GAMECHAT"
                        Call gamechat(incoming, socket)
                    Case "PM"
                        Call pm(incoming, socket)

                    Case "RANDOM"
                        Call random(incoming, socket)
                    Case "QUIT"
                        Call quit(incoming, socket, True)
                    Case "QUITERROR"
                        Call quit(incoming, socket, False)

                    Case "VERSION"
                        Call version(incoming, socket)

                        'Case "CHPASS"
                        '    Dim oplayeraccount As New PlayerAccount
                        '    oplayeraccount.AccountModule(sCommand, incoming, socket)
                        '    oplayeraccount = Nothing
                    Case "LOADGAME"
                        Call loadgamesub(incoming, socket)
                    Case "CHAT"
                        Call chat(incoming, socket)

                    Case "ERRORHANDLER"
                        Call error_playerhandler(incoming, socket)
                    Case "CHANGERULE"
                        Call changerule(incoming, socket)
                    Case "CHANGEWAGER"
                        Call changewager(incoming, socket)
                    Case String.Empty
                        'Dim oCrypt64 As New TTEServerEncrypt64.Class1

                        'Dim sOldDecrypt As String = oCrypt64.Decode(sPacket)

                        'If Mid(sOldDecrypt, 1, 7) = "VERSION" Then
                        '    SendOld(String.Empty, "UPDATE", socket)
                        '    frmSystray.Winsock(socket).Close()
                        '    GoTo exitfunction
                        'End If
                    Case Else
                        'If oServerConfig.EnableAvatarShop = False And sCommand <> String.Empty Then
                        '    Send(String.Empty, "EXIT You were terminated from the server because you commited a critical fault.  Please log in again as this may be a result in packet loss.")
                        '    Dim oChatFunctions As New ChatFunctions
                        '    oChatFunctions.uniwarn(String.Concat(frmSystray.Winsock(socket).Tag, " sent an invalid packet to the server.  User is logged off."))
                        '    oChatFunctions = Nothing
                        'End If
                End Select
            End If

            If players(socket) Is Nothing Then Exit Do

            If frmSystray.Winsock(socket).CtlState <> MSWinsockLib.StateConstants.sckConnected Then Exit Do
        Loop

        'make sure that we release the ProcessingData
        'flag for this socket.
ExitFunction:
        If Not (players(socket) Is Nothing) Then
            If blnThisProcedureIsCurrent Then
                players(socket).ProcessingData = False
            End If
        End If

        EndProcessing = Date.Now

        Exit Sub

errhandler:
        Call errorsub(Err.Description, "HandleIncoming")
        GoTo ExitFunction
    End Sub

    Private Sub GivePlayers(ByVal xsocket As Integer)
        Try
            Dim oDatabaseFunctions As New DatabaseFunctions

            Dim oTable As DataTable = oDatabaseFunctions.OnlinePlayers
            Dim oZombies As DataTable = oServerConfig.ChatStuffers
            Dim oPlayerAccount As New PlayerAccountDAL(frmSystray.Winsock(xsocket).Tag)

            If oPlayerAccount.Player = String.Empty Then Exit Sub

            'Give the player list to the new person logging in
            For x As Integer = 0 To oTable.Rows.Count - 1
                Send(String.Empty, "NEWPLAYER2     " & String.Concat(oTable.Rows(x).Item("Player").ToString, " ", oTable.Rows(x).Item("Surname").ToString.Replace(" ", "%20"), " ", (IIf(oTable.Rows(x).Item("Guild").ToString <> String.Empty, "<" & oTable.Rows(x).Item("Guild").ToString & ">", String.Empty)).Replace(" ", "%20")).Replace(" ", "%20"), xsocket)
            Next

            'Load Zombies
            For x As Integer = 0 To oZombies.Rows.Count - 1
                Send(String.Empty, "NEWPLAYER2     " & String.Concat(oZombies.Rows(x).Item("Player").ToString, " ", oZombies.Rows(x).Item("Surname").ToString.Replace(" ", "%20"), " ", (IIf(oZombies.Rows(x).Item("Guild").ToString <> String.Empty, "<" & oZombies.Rows(x).Item("Guild").ToString & ">", String.Empty)).Replace(" ", "%20")).Replace(" ", "%20"), xsocket)
            Next

            oServerConfig.Zombies = oZombies.Rows.Count

            Send(String.Empty, "PLAYERSLOADED", socket)
        Catch ex As Exception
            Call errorsub(ex, "GivePlayers")
        End Try
    End Sub

    Private Function checkpacket(ByVal socket As Integer, ByVal sCommand As String) As Boolean
        If socket = 0 Then Exit Function

        PlayerList.AddPackets(frmSystray.Winsock(socket).Tag, 1)
        'frmserver.lstpacketcontrol.Items.Item(socket) = (Val(frmserver.lstpacketcontrol.Items.Item(socket)) + 1).ToString

        Dim iPackets As Integer = PlayerList.GetPackets(frmSystray.Winsock(socket).Tag)

        If iPackets >= 40 Then
            Dim oChatFunctions As New ChatFunctions
            oChatFunctions.uniwarn(String.Concat(frmSystray.Winsock(socket).Tag, " got kicked from the server for packet flooding."))
            Call historyadd(frmSystray.Winsock(socket).Tag, 2, String.Concat(frmSystray.Winsock(socket).Tag, " got kicked from the server for packet flooding."))
            Send(String.Empty, "EXIT You were terminated from the server because the server's flood control mechanism was triggered.  Please log in again.", socket)
            Return True
        ElseIf iPackets >= 35 Then
            Call blockchat(String.Empty, "Warning", "You are becoming very close to the number of packets allowed from this connection per minute.  Please be cautious as this may result in your connection being terminated.", socket)
            Return False
        Else
            Return False
        End If
    End Function

    Private Sub pm(ByVal incoming As String, ByVal socket As Integer)
        Dim sItems() As String = incoming.Split(" ")
        Dim oChatLog As New ChatLog

        Try
            If frmSystray.Winsock(socket).Tag = String.Empty Then Exit Sub

            If isOnline(sItems(1)) = False Or sItems(1) = String.Empty Then Throw New System.IndexOutOfRangeException

            If sItems(1).ToLower = "atomsplit" Or sItems(1).ToLower = "darklumina-eye" Then
                Call blockchat(String.Empty, "Error", "You cannot message any type of Triple Triad Extreme watchman or NPC.  If you have an issue, please contact someone in the Servants of Darkness guild or send a ticket on our customer support website.", socket)
                Exit Sub
            End If

            Dim oPlayerAccount As New PlayerAccountDAL(frmSystray.Winsock(socket).Tag)

            If oPlayerAccount.Silenced = True Then
                Call blockchat(String.Empty, "Silenced", String.Concat("You are silenced.  You may not speak in chat or private messages for ", oPlayerAccount.Silenced_Minutes, " minutes!"), socket)
                Exit Sub
            End If

            If FilterNeeded(sItems(2)) = True Then
                Call blockchat(String.Empty, "Filter", "Please Use Appropriate Language", socket)
                Exit Sub
            End If

            Send(String.Empty, String.Concat("INPM ", frmSystray.Winsock(socket).Tag, " ", socket, " ", sItems(2).Replace(" ", "%20")), GetSocket(sItems(1)))
        Catch ioorex As System.IndexOutOfRangeException
            If isZombie(sItems(1)) = False Then Call blockchat(String.Empty, "Error", "The user you are trying to message is offline, is not accepting private messages at this time, or is logged on through the game portal. The message could not be delivered.", socket)
        Catch ex As Exception
            Call errorsub(ex, "pm")
        Finally
            oChatLog.InsertPM(frmSystray.Winsock(socket).Tag, sItems(1), sItems(2).Replace("%20", " "))
            oChatLog = Nothing
        End Try
    End Sub


    Private Sub abnewdeck(ByVal incoming As String, ByVal socket As Integer)
        Try
            Dim lostvar As String = String.Empty, account As String = String.Empty, strDeck As String = String.Empty
            Dim oTables As New BusinessLayer.Tables

            Divide(incoming, " ", lostvar, strDeck)

            If strDeck = String.Empty Then
                Call blockchat(String.Empty, "New Deck", "Server could not process your request.  Try again later", socket)
                Exit Sub
            End If

            account = frmSystray.Winsock(socket).Tag
            Dim oPlayerAccount As New PlayerAccountDAL(account)

            If oPlayerAccount.TodayNewDecks >= 5 Then
                Call blockchat(String.Empty, "New Deck", "New deck daily limit exceeded (5)", socket)
                Exit Sub
            ElseIf oTables.findtableplayer(Tables, frmSystray.Winsock(socket).Tag) Then
                Call blockchat(String.Empty, "New Deck", "You cannot get a new deck when you are in a game.", socket)
                Exit Sub
            End If

            If GetRowCount("SELECT id FROM playershop_cards WHERE player = '" & frmSystray.Winsock(socket).Tag & "'") > 0 Then
                Call blockchat(String.Empty, "Lock", "You may not redeck when you have cards in your shop.  Remove the cards in your shop before attempting to redeck again", socket)
                Exit Sub
            ElseIf GetRowCount("SELECT id FROM historyusers where typeFK = 28 and player = '" & frmSystray.Winsock(socket).Tag & "'") > 0 Then
                Call blockchat(String.Empty, "Lock", "You may no longer get new decks since you have added cards to a guild bank at one time.", socket)
                Exit Sub
            End If

            Dim oNewDecks As New DatabaseFunctions
            oNewDecks.NewDeck(account, strDeck)
            oNewDecks.NewDeckClearStats(account)

            Call blockchat(String.Empty, "New Deck", "Your newdeck has been received.  Remember you are only allowed 5 per day", socket)
            Send(String.Empty, "STARTERDECKREFRESHGRID", socket)
            oNewDecks = Nothing
        Catch ex As Exception
            Call blockchat(String.Empty, "New Deck", "Server could not process your request.  Try again later", socket)
            Call errorsub(ex, "abnewdeck")
        End Try
    End Sub

    Private Sub version(ByVal incoming As String, ByVal socket As Integer)
        Try
            AddPlayer(socket)

            Send(String.Empty, "SHOWMENU", socket)

            Dim sDivide() As String = incoming.Split(" ")

            If oServerConfig.CurrentVersion <> sDivide(1) Then
                Send(String.Empty, "UPDATE", socket)
                'frmSystray.Winsock(socket).Close()
                'Call closesocket(socket)
                Exit Sub
            End If
        Catch ex As Exception
            Call errorsub(ex, "Version")
        End Try
    End Sub

    Private Sub Reconnect(ByVal incoming As String, ByVal socket As Integer)
        Try
            Dim lostvar As String = String.Empty, loadgamenick As String = String.Empty, pass As String = String.Empty
            Divide(incoming, " ", lostvar, loadgamenick, pass)

            Dim oPlayerAccount As New PlayerAccountDAL(loadgamenick)

            If checkstatus(oPlayerAccount, pass, socket) = True Then
                players(socket).Ping = True
                players(socket).nickname = loadgamenick
                players(socket).Away = False
                players(socket).IP = frmSystray.Winsock(socket).RemoteHostIP

                ReconnectGame(oPlayerAccount, socket)
            End If
        Catch ex As Exception
            Send(String.Empty, "EXIT An error occured while trying to log you in.  Please try again later.  If this problem persists and you have an account registered, please send a ticket to customer support using our website.", socket)
            errorsub(ex.ToString, "reconnect")
            If socket <> 0 Then frmSystray.Winsock(socket).Close()
        End Try
    End Sub

    Private Function checkstatus(ByVal oPlayerAccount As PlayerAccountDAL, ByVal sPass As String, ByVal socket As Integer) As Boolean
        If oPlayerAccount.ErrorFlag = True Then
            Send(String.Empty, "EXIT An error occured while trying to log you in.  Please try again later.  If this problem persists and you have an account registered, please send a ticket to customer support using our website.", socket)
            errorsub(oPlayerAccount.Player & " - error flag true", "checkstatus")
            'If socket <> 0 Then frmSystray.Winsock(socket).Close()
            Return False
        End If

        If oPlayerAccount.Password <> sPass Then
            Send(String.Empty, "LOADGAME-WRONGPASS", socket)

            oPlayerAccount.InvalidLogin()
            'If socket <> 0 Then frmSystray.Winsock(socket).Close()
            Return False
        End If

        If oPlayerAccount.Player = String.Empty Then
            Send(String.Empty, "EXIT The server is down at this time. Please post in the Triple Triad Extreme forums immediately to have this resolved.", socket)
            'If socket <> 0 Then frmSystray.Winsock(socket).Close()
            Return False
        End If

        If frmSystray.Winsock(socket).RemoteHostIP <> String.Empty Then
            Dim sIP() As String = frmSystray.Winsock(socket).RemoteHostIP.Split(".")

            If oPlayerAccount.IsBanned(oPlayerAccount.Player, frmSystray.Winsock(socket).RemoteHostIP) = True Or _
                oPlayerAccount.IsBanned(oPlayerAccount.Player, String.Concat(sIP(0), ".", sIP(1), ".", sIP(2), ".*")) = True Or _
                oPlayerAccount.IsBanned(oPlayerAccount.Player, String.Concat(sIP(0), ".", sIP(1), ".*")) = True Then

                Send(String.Empty, "EXIT The Bouncer Moogle has determined that you are not allowed to enter (Banned).  Your IP could be banned, you have too many infractions, or your account has been locked.  If this is in error, please contact support@tripletriadextreme.com.  If you have too many infractions, log into the portal and pay the fines to have it unlocked.", socket)
                'If socket <> 0 Then frmSystray.Winsock(socket).Close()
                Return False
            End If
        End If

        If oPlayerAccount.Banned = True And oPlayerAccount.Player <> String.Empty Then
            Send(String.Empty, "EXIT This account is locked.  Contact support@tripletriadextreme.com to have it unlocked.", socket)
            'If socket <> 0 Then frmSystray.Winsock(socket).Close()
            Return False
        End If

        Return True
    End Function

    Private Sub loadgamesub(ByVal incoming As String, ByVal socket As Integer)
        Try
            Dim lostvar As String = String.Empty, loadgamenick As String = String.Empty, pass As String = String.Empty

            Divide(incoming, " ", lostvar, loadgamenick, pass)

            If loadgamenick.Trim = String.Empty Then
                Throw New Exception
                'If socket <> 0 Then frmSystray.Winsock(socket).Close()
                Exit Sub
            End If

            Dim oPlayerAccount As New PlayerAccountDAL(loadgamenick)

            For x As Integer = 1 To frmSystray.Winsock.Count - 1
                If frmSystray.Winsock(x).Tag.ToString.ToLower = oPlayerAccount.Player.ToLower Then
                    'Send(String.Empty, "LOADGAME-ONLINE", socket)
                    If socket <> 0 And x <> socket Then frmSystray.Winsock(x).Close()
                    Dim oChatFunctions As New ChatFunctions
                    oChatFunctions.uniwarn(String.Concat(frmSystray.Winsock(socket).Tag, " logged in with self online. Other connection terminated."))
                    oChatFunctions = Nothing
                    Exit For
                    'Exit Sub
                End If
            Next x

            If checkstatus(oPlayerAccount, pass, socket) = True Then
                players(socket).Ping = True
                LoadGame(oPlayerAccount, socket)
            End If
        Catch ex As Exception
            Send(String.Empty, "EXIT An error occured while trying to log you in. Please try again later.  If this problem persists and you have an account registered, please send a ticket to customer support using our website.", socket)
            errorsub(ex.ToString, "loadgamesub")
            'If socket <> 0 Then frmSystray.Winsock(socket).Close()
            'Call errorsub(ex, "loadgamesub")
        End Try
    End Sub

    Private Function CheckFilter(ByVal sChatText As String) As String
        sChatText = sChatText.Replace("%20", " ")

        Dim sItems() As String = sChatText.Split(" ")
        Dim i As Integer = 0

        For Each sWord As String In sItems
            If oServerConfig.BadWords.Rows.Count > 0 Then
                For x As Integer = 0 To oServerConfig.BadWords.Rows.Count - 1
                    If CleanWord(sWord.ToLower) = oServerConfig.BadWords.Rows(x).Item("word").ToString.ToLower Then
                        sItems(i) = oServerConfig.BadWords.Rows(x).Item("replace").ToString.ToLower
                        Exit For
                    End If
                Next x
            End If

            i += 1
        Next

        sChatText = String.Empty

        For Each sWord As String In sItems
            sChatText = String.Concat(sChatText, " ", sWord)
        Next

        Return sChatText.Trim
    End Function

    Private Function FilterNeeded(ByVal sChatText As String) As Boolean
        sChatText = sChatText.Replace("%20", " ")

        Dim sItems() As String = sChatText.Split(" ")
        Dim i As Integer = 0

        For Each sWord As String In sItems
            If oServerConfig.BadWords.Rows.Count > 0 Then
                For x As Integer = 0 To oServerConfig.BadWords.Rows.Count - 1
                    If CleanWord(sWord.ToLower) = oServerConfig.BadWords.Rows(x).Item("word").ToString.ToLower Then
                        Return True
                        Exit For
                    End If
                Next x
            End If

            i += 1
        Next

        'sChatText = String.Empty

        'For Each sWord As String In sItems
        '    sChatText = String.Concat(sChatText, " ", sWord)
        'Next

        Return False
    End Function

    Private Function CleanWord(ByVal vstrWord As String) As String
        'Function removes punctuation values from the word and returns
        'only those values not included in the CharList constant

        Dim j As Integer
        Dim oneChar As String
        Dim sWord As String = String.Empty

        For j = 1 To Len(vstrWord)
            oneChar = Mid(vstrWord, j, 1)

            If (InStr(CharList, oneChar) = 0) Then
                sWord = sWord & oneChar
            End If
        Next j

        Return sWord
    End Function

    Private Sub chat(ByVal incoming As String, ByVal socket As Integer)
        Dim oChatFunctions As New ChatFunctions
        Dim oChatLog As New ChatLog
        Dim oPlayerAccount As New PlayerAccountDAL(frmSystray.Winsock(socket).Tag)

        Try
            If frmSystray.Winsock(socket).Tag = String.Empty Then Exit Sub

            incoming = incoming.Replace(Chr(10), String.Empty)
            incoming = incoming.Replace(Chr(160), String.Empty)

            Dim chattxt As String = Right(incoming, Len(incoming) - InStr(1, incoming, " ")).Replace("  ", String.Empty).Trim

            If chattxt = chattxt.ToUpper And chattxt.Length >= 6 Then
                Call blockchat(String.Empty, "Chat", "Please do not type in all caps as it is perceived as shouting and can be annoying to others.", socket)
                Exit Sub

            End If

            If Mid$(chattxt, 1, 6).ToLower = "/guild" Or Mid$(chattxt, 1, 2).ToLower = "/g" Then
                Dim oGuilds As New Guilds
                oGuilds.guildchat(incoming, socket)
                oGuilds = Nothing
                Exit Sub
            End If

            If Mid$(chattxt, 1, 6).ToLower = ".debug" And oPlayerAccount.AdminLevel = 4 Then
                Dim sItems() As String = chattxt.Split(" ")

                Select Case sItems(1)
                    Case "card"
                        DebugCard(Mid$(chattxt, 13, Len(chattxt)))
                    Case "totalcards"
                        DebugTotalCards()
                    Case "writeplayers"
                        DebugWritePlayers()
                End Select

                Exit Sub
            ElseIf Mid$(chattxt, 1, 7).ToLower = ".server" And oPlayerAccount.AdminLevel = 4 Then
                Dim sItems() As String = chattxt.Split(" ")

                Select Case sItems(1)
                    Case "clockstart"
                        Dim iTimer As Integer

                        If UBound(sItems) = 1 Then
                            iTimer = 30
                        Else
                            iTimer = Integer.Parse(sItems(2))
                        End If

                        If iTimer >= 5 Then
                            frmserver.ShutDownTimer = iTimer
                            frmserver.tmrShutDown.Enabled = True
                            frmserver.ShutDownBroadCast(String.Concat("The server will shut down in ", iTimer, " minutes for maintenance or a patch. See the website for more details to this outage. Please make preparations for this down time and if the downtime is soon, please close or finish your games."))
                        Else
                            frmserver.tmrShutDown.Enabled = False
                            Call blockchat(String.Empty, "Error", "Timer must be greater than 5 minutes, timer stopped.", socket)
                            Exit Sub
                        End If
                    Case "clockstop"
                        frmserver.tmrShutDown.Enabled = False
                        frmserver.ShutDownBroadCast(String.Concat("The shut down sequence was stopped by ", oPlayerAccount.Player))
                        frmserver.tmrShutDown.Enabled = False
                        frmserver.ShutDownTimer = 0
                    Case "refresh"
                        oServerConfig.Refresh()
                        Call blockchat(String.Empty, "Refresh", "Server Information Was Refreshed", socket)
                    Case "disconnect"
                        Dim sPlayer As String = String.Empty

                        If UBound(sItems) = 2 Then
                            sPlayer = sItems(2)
                            Send(String.Empty, "EXIT You were disconnected from the game server by an administrator.", GetSocket(sPlayer))
                            Call quit("QUIT " & sPlayer, GetSocket(sPlayer), False)
                        End If
                End Select

                Exit Sub
            ElseIf Mid$(chattxt, 1, 6).ToLower = ".debug" Then
                Call blockchat(String.Empty, "Permissions", "You do not have the appropriate permissions to do that.", socket)
                Exit Sub
            End If

            If oServerConfig.ModeratedChat = True And oPlayerAccount.AdminStatus = False Then Exit Try

            If oPlayerAccount.Silenced = True Then
                Call blockchat(String.Empty, "Silenced", String.Concat("You are silenced.  You may not speak for ", oPlayerAccount.Silenced_Minutes, " minutes!"), socket)
                Exit Sub
            End If

            If FilterNeeded(chattxt) = True Then
                Call blockchat(String.Empty, "Filter", "Please Use Appropriate Language", socket)
                Exit Sub
            End If

            oChatLog.InsertChat(oPlayerAccount.Player, chattxt)
            oChatFunctions.unisend("CHAT", String.Concat(frmSystray.Winsock(socket).Tag, " ", chattxt.Replace(" ", "%20")))
        Catch ex As Exception
            Call errorsub(ex, "chat")
        Finally
            oChatFunctions = Nothing
            oPlayerAccount = Nothing
            oChatLog = Nothing
        End Try
    End Sub

    Private Sub DebugCard(ByVal sCard As String)
        Dim oCardInfo As New LogicLayer.Cards.PlayingCardInfo
        Dim oCard As Entities.BusinessObjects.PlayingCard = oCardInfo.GetPlayingCard(sCard, Cards, CardNameList)

        Call blockchat(String.Empty, sCard, String.Format("Card: {0}", oCard.CardName), socket)
        Call blockchat(String.Empty, sCard, String.Format("Left: {0}", oCard.Left), socket)
        Call blockchat(String.Empty, sCard, String.Format("Up: {0}", oCard.Up), socket)
        Call blockchat(String.Empty, sCard, String.Format("Right: {0}", oCard.Right), socket)
        Call blockchat(String.Empty, sCard, String.Format("Down: {0}", oCard.Down), socket)
        Call blockchat(String.Empty, sCard, String.Format("Level: {0}", oCard.Level), socket)
        Call blockchat(String.Empty, sCard, String.Format("Element: {0}", oCard.Element), socket)
        Call blockchat(String.Empty, sCard, String.Format("Special: {0}", oCard.SpecialCard), socket)
        Call blockchat(String.Empty, sCard, String.Format("Deck: {0}", oCard.Deck), socket)
    End Sub

    Private Sub DebugTotalCards()
        Call blockchat(String.Empty, "Total Cards", String.Format("Total Cards in Playing Card List: {0}", Cards.Count), socket)
        Call blockchat(String.Empty, "Total Cards", String.Format("Total Cards in Generic List: {0}", CardNameList.Count), socket)
        Call blockchat(String.Empty, "Total Cards", String.Format("Is Ok? {0}", IIf(Cards.Count = CardNameList.Count, "Yes", "No")), socket)
    End Sub

    Private Sub DebugWritePlayers()
        Dim oDatabaseFunctions As New DatabaseFunctions
        Dim oDataTable As DataTable = oDatabaseFunctions.OnlinePlayers()

        If Not (oDataTable Is Nothing) Then
            For x As Integer = 0 To oDataTable.Rows.Count - 1
                Dim iID As Integer = Val(oDataTable.Rows(x).Item("ID").ToString)
                If frmSystray.Winsock(iID).Tag <> String.Empty Then
                    Call blockchat(String.Empty, iID, oDataTable.Rows(x).Item("player").ToString, socket)
                End If
            Next x
        End If
    End Sub

    Private Sub pingsub(ByVal socket As Integer)
        players(socket).Ping = True
    End Sub

    Private Sub gamechat(ByVal incoming As String, ByVal socket As Integer)
        Try
            Dim sChat As String = String.Empty, sOpponent As String = String.Empty
            Dim lostvar As String = String.Empty

            'incoming = Mid$(incoming, 10, Len(incoming))
            Divide(incoming, "~@*~*", lostvar, sChat)

            Dim oTables As New BusinessLayer.Tables

            Dim iTableID As Integer = oTables.findplayerstable(tables, frmSystray.Winsock(socket).Tag)

            If iTableID = 0 Then Exit Sub

            If frmSystray.Winsock(socket).Tag = tables(iTableID).player1 Then
                sOpponent = tables(iTableID).player2
            Else
                sOpponent = tables(iTableID).player1
            End If

            Send(sOpponent, String.Concat("GAMECHAT ", frmSystray.Winsock(socket).Tag, " ", sChat))
        Catch ioorex As System.IndexOutOfRangeException
            ' Do nothing
        Catch ex As Exception
            Call errorsub(ex, "gamechat")
        End Try
    End Sub

    Public Sub random(ByVal incoming As String, ByVal socket As Integer)
        Dim min As String = String.Empty, lostvar As String = String.Empty, max As String = String.Empty
        Static RandomNumGen As New System.Random
        Dim oPlayerAccount As New PlayerAccountDAL(frmSystray.Winsock(socket).Tag)

        Divide(incoming, " ", lostvar, min, max)

        If min = String.Empty Or max = String.Empty Then Exit Sub

        If oPlayerAccount.Silenced = True Then
            Call blockchat(String.Empty, "Silenced", String.Concat("You are silenced.  You may not random for ", oPlayerAccount.Silenced_Minutes, " minutes!"), socket)
            Exit Sub
        End If

        Dim intnumber As Integer = RandomNumGen.Next(Integer.Parse(min), Integer.Parse(max) + 1)

        Call sendall("CHAT " & frmSystray.Winsock(socket).Tag & " /random rolls a number between " & min & " and " & max & " and rolls a " & intnumber & " *****")
    End Sub

    Private Sub awaystatus(ByVal incoming As String, ByVal socket As Integer)
        Dim lostvar As String = String.Empty, strStatus As String = String.Empty

        Divide(incoming, " ", lostvar, strStatus)

        Select Case strStatus
            Case "0"
                players(socket).Away = False
            Case "1"
                players(socket).Away = True
        End Select
    End Sub

    Private Sub Rush(ByVal incoming As String, ByVal socket As Integer)
        Dim sPlayer As String = frmSystray.Winsock(socket).Tag
        Dim oDAL As New Rush

        Dim sCSM As String = oDAL.RushBreak(sPlayer)
        Call blockchat(sPlayer, "Limit Break", sCSM)
    End Sub

    Private Sub error_playerhandler(ByVal incoming As String, ByVal socket As Integer)
        Try
            Dim strReason As String = String.Empty, lostvar As String = String.Empty, strProcedure As String = String.Empty
            Dim oChatFunctions As New ChatFunctions

            Divide(incoming, " ", lostvar, strReason, strProcedure)

            strReason = strReason.Replace(" ", "%20").Replace("'", String.Empty)
            strProcedure = strProcedure.Replace("%20", " ")

            Dim oStringFunctions As New LogicLayer.Utility.StringFunctions
            oStringFunctions.SaveTextToFile(String.Concat("Player Error (", frmSystray.Winsock(socket).Tag, ") ", strReason, vbCrLf, vbCrLf), String.Concat(Application.StartupPath, "\logs\player_errors.log"))

            oChatFunctions.uniwarn(String.Concat("Player Error (", frmSystray.Winsock(socket).Tag, ") ", strReason))
        Catch ex As Exception
            Call errorsub(String.Concat(ex, " (", incoming, ")"), "error_playerhandler")
        End Try
    End Sub

    Private Sub challengechats(ByVal incoming As String, ByVal socket As Integer)
        Try
            Dim strHandle As String = String.Empty, tableID As String = String.Empty, strChat As String = String.Empty, strPlayer As String = String.Empty
            Dim oTables As New BusinessLayer.Tables

            tableID = oTables.findplayerstable(tables, frmSystray.Winsock(socket).Tag).ToString

            If CDbl(tableID) = 0 Then Exit Sub

            'Divide incoming, " ", lostvar, opponent
            strChat = Mid(incoming, 16, Len(incoming))
            Divide(strChat, " ", strHandle)

            If tables(Val(tableID)).gameid <> 0 Then
                Send(tables(CInt(tableID)).player1, "CHALLENGECHATS *** Cannot kick or talk to a player here when the game has started")
                Exit Sub
            End If

            If frmSystray.Winsock(socket).Tag = tables(CInt(tableID)).player1 Then
                Select Case strHandle
                    Case "/kick"
                        Divide(strChat, " ", strHandle, strPlayer)

                        Select Case strPlayer
                            Case "2"
                                If tables(CInt(tableID)).player2 = String.Empty Then Exit Sub
                                Call blockchat(tables(CInt(tableID)).player2, "Kicked", "You have been kicked by the table owner")

                                Send(tables(CInt(tableID)).player1, "CHALLENGECHATS *** " & tables(CInt(tableID)).player2 & " has been kicked")
                                Send(tables(CInt(tableID)).player3, "CHALLENGECHATS *** " & tables(CInt(tableID)).player2 & " has been kicked")
                                Send(tables(CInt(tableID)).player4, "CHALLENGECHATS *** " & tables(CInt(tableID)).player2 & " has been kicked")
                                Call oTables.PreGameTableClose(Tables, GetSocket(Tables(CInt(tableID)).Player2))
                        End Select

                        Exit Sub
                End Select
            End If

            If tables(CInt(tableID)).player1 <> String.Empty Then Send(tables(CInt(tableID)).player1, String.Concat("CHALLENGECHATS ", frmSystray.Winsock(socket).Tag, " ", strChat))
            If tables(CInt(tableID)).player2 <> String.Empty Then Send(tables(CInt(tableID)).player2, String.Concat("CHALLENGECHATS ", frmSystray.Winsock(socket).Tag, " ", strChat))
        Catch ex As Exception
            Call errorsub(ex, "challengechats")
        End Try
    End Sub

    Private Sub changerule(ByVal incoming As String, ByVal socket As Integer)
        Try
            Dim lostvar As String = String.Empty, strRules As String = String.Empty
            Dim tableID As Integer, var(3) As String, blnskip As Boolean
            Dim strGame As String = String.Empty, strAll As String = String.Empty, strDecks As String = String.Empty

            Dim oTables As New BusinessLayer.Tables

            tableID = oTables.findplayerstable(tables, frmSystray.Winsock(socket).Tag)

            If tables(tableID).gameid > 0 Then
                Call blockchat(String.Empty, "Error", "You cannot change rules after the game has started.", socket)
                Exit Sub
            ElseIf tableID = 0 Then
                Call blockchat(String.Empty, "Error", "The table does not exist", socket)
                Exit Sub
            End If

            If (tables(tableID).player1 <> frmSystray.Winsock(socket).Tag) And InStr(1, incoming, "CHINCHIN") = 0 Then Exit Sub

            strAll = Mid(incoming, 12, Len(incoming)).Replace(" ", "%20")

            Divide(strAll, "@@", strRules, strGame, strDecks)

            strRules = strRules.Replace("%20", " ")
            strDecks = strDecks.Replace("%20", " ")

            tables(tableID).player_ready(1) = False
            tables(tableID).player_ready(2) = False
            blnskip = False

            If InStr(1, strRules, "OTT") > 0 Then
                Divide(strRules, " ", lostvar, strRules, strGame)
                tables(tableID).rulelist = strRules
                tables(tableID).Blocks = strGame
            ElseIf InStr(1, strRules, "CHINCHIN") > 0 Then
                Divide(tables(tableID).rulelist, " ", lostvar, var(0), var(1))
                Divide(strRules, " ", lostvar, var(2), var(3))

                Dim oPlayerAccount As New PlayerAccountDAL(frmSystray.Winsock(socket).Tag)

                If tables(tableID).player1 = frmSystray.Winsock(socket).Tag Then
                    If Val(var(2)) <= 0 Then Exit Sub

                    If Int(oPlayerAccount.AP / 3) >= Val(var(2)) Then
                        var(0) = CStr(Val(var(2)))
                    Else
                        Send(String.Empty, "CHALLENGECHATS *** Wager is greater than 1/3 of your total AP.  Wager changed has been cancelled.", socket)
                    End If
                ElseIf tables(tableID).player2 = frmSystray.Winsock(socket).Tag Then
                    If Val(var(2)) <= 0 Then Exit Sub

                    If Int(oPlayerAccount.AP / 3) >= Val(var(2)) Then
                        var(1) = CStr(Val(var(2)))
                    Else
                        Send(String.Empty, "CHALLENGECHATS *** Wager is greater than 1/3 of your total AP.  Wager changed has been cancelled.", socket)
                    End If
                End If

                tables(tableID).rulelist = String.Concat("CHINCHIN ", var(0), " ", var(1))
                strRules = tables(tableID).rulelist
                blnskip = True
            Else
                tables(tableID).rulelist = strRules
                tables(tableID).Decks = strDecks
            End If

            strRules = String.Concat(strRules, " ", strGame)

            strRules = strRules.Replace(" ", "%20")
            strDecks = strDecks.Replace(" ", "%20")

            If tables(tableID).player1 <> String.Empty Then
                Send(Tables(tableID).Player1, String.Concat("CHALLENGEBOXS ", tableID, " ", Tables(tableID).Player1, " ", Tables(tableID).Player2, " ", Tables(tableID).GoldWager, " ", strRules, "@@", strDecks))
                Send(tables(tableID).player1, "CHALLENGEREADYS 1 0")
                Send(tables(tableID).player1, "CHALLENGEREADYS 2 0")
                Send(tables(tableID).player1, "CHALLENGECHATS *** The table owner has changed the game rules. Please review the rules and click ready again. If you are in a Triple Triad game and there is not a trade item listed, then it is Trade: One.")
            End If

            If tables(tableID).player2 <> String.Empty Then
                Send(Tables(tableID).Player2, String.Concat("CHALLENGEBOXS ", tableID, " ", Tables(tableID).Player1, " ", Tables(tableID).Player2, " ", Tables(tableID).GoldWager, " ", strRules, "@@", strDecks))
                Send(tables(tableID).player2, "CHALLENGEREADYS 1 0")
                Send(tables(tableID).player2, "CHALLENGEREADYS 2 0")
                Send(tables(tableID).player2, "CHALLENGECHATS *** The table owner has changed the game rules. Please review the rules and click ready again. If you are in a Triple Triad game and there is not a trade item listed, then it is Trade: One.")
            End If

            If blnskip = False Then
                'oTables.SendTable(Tables, tableID)
            Else
                oTables.UpdateTable(Tables, tableID)
            End If
            'Call sendall(String.Concat("ADDTABLE ", tableID, " ", Tables(tableID).Player1, " ", Tables(tableID).Player2, " ", Tables(tableID).RuleList, " @@", Tables(tableID).Comment, "@@", Tables(tableID).Decks))
        Catch ex As Exception
            Call errorsub(ex, "changerule")
        End Try
    End Sub

    Private Sub changewager(ByVal incoming As String, ByVal socket As Integer)
        Try
            Dim sWager As String = String.Empty
            Dim tableID As Integer

            Dim oTables As New BusinessLayer.Tables

            Dim Rules() As String = incoming.Split(" ")

            sWager = Rules(1)

            tableID = oTables.FindPlayersTable(Tables, frmSystray.Winsock(socket).Tag)
            Dim oPlayer As New PlayerAccountDAL(frmSystray.Winsock(socket).Tag)

            If Tables(tableID).GameID > 0 Then
                Call blockchat(String.Empty, "Error", "You cannot change rules after the game has started.", socket)
                Exit Sub
            ElseIf tableID = 0 Then
                Call blockchat(String.Empty, "Error", "The table does not exist.", socket)
                Exit Sub
            ElseIf oPlayer.Gold < Val(sWager) Then
                Send(String.Empty, "CHALLENGECHATS *** You do not have enough gold for this wager.", socket)
                Exit Sub
            ElseIf Val(sWager) < 0 Then
                Send(String.Empty, "CHALLENGECHATS *** Gold wager cannot be less than 0", socket)
                Exit Sub
            ElseIf Tables(tableID).Player1 <> frmSystray.Winsock(socket).Tag Then
                Call blockchat(String.Empty, "Error", "Only the table creators can make changes to tables.", socket)
                Exit Sub
            Else
                Tables(tableID).GoldWager = Val(sWager)
            End If

            Tables(tableID).Player_Ready(1) = False
            Tables(tableID).Player_Ready(2) = False

            Dim sRuleList As String = String.Empty

            If InStr(1, Tables(tableID).RuleList, "OMNI") > 0 Then
                sRuleList = String.Format("{0} {1}", Tables(tableID).RuleList, Tables(tableID).Blocks)
            Else
                sRuleList = Tables(tableID).RuleList
            End If

            If Tables(tableID).Player1 <> String.Empty Then
                Send(Tables(tableID).Player1, String.Concat("CHALLENGEBOXS ", tableID, " ", Tables(tableID).Player1, " ", Tables(tableID).Player2, " ", Tables(tableID).GoldWager, " ", sRuleList.Replace(" ", "%20"), "@@", Tables(tableID).Decks.Replace(" ", "%20")))
                Send(Tables(tableID).Player1, "CHALLENGEREADYS 1 0")
                Send(Tables(tableID).Player1, "CHALLENGEREADYS 2 0")
                Send(Tables(tableID).Player1, "CHALLENGECHATS *** The table owner has changed the game rules. Please review the rules and click ready again. If you are in a Triple Triad game and there is not a trade item listed, then it is Trade: One.")
            End If

            If Tables(tableID).Player2 <> String.Empty Then
                Send(Tables(tableID).Player2, String.Concat("CHALLENGEBOXS ", tableID, " ", Tables(tableID).Player1, " ", Tables(tableID).Player2, " ", Tables(tableID).GoldWager, " ", sRuleList.Replace(" ", "%20"), "@@", Tables(tableID).Decks.Replace(" ", "%20")))
                Send(Tables(tableID).Player2, "CHALLENGEREADYS 1 0")
                Send(Tables(tableID).Player2, "CHALLENGEREADYS 2 0")
                Send(Tables(tableID).Player2, "CHALLENGECHATS *** The table owner has changed the game rules. Please review the rules and click ready again. If you are in a Triple Triad game and there is not a trade item listed, then it is Trade: One.")
            End If

            oTables.UpdateTable(Tables, tableID)
            'oTables.SendTable(Tables, tableID)
        Catch ex As Exception
            Call errorsub(ex, "changewager")
        End Try
    End Sub

    Private Sub gameready(ByVal socket As Integer)
        Dim tableID, intStatus, intPlayer, intCards As Integer
        Dim blnStart As Boolean
        Dim strWager1 As String = String.Empty, strWager2 As String = String.Empty, lostvar As String = String.Empty
        Dim oTables As New BusinessLayer.Tables

        tableID = oTables.findplayerstable(tables, frmSystray.Winsock(socket).Tag)

        If tableID = 0 Then Exit Sub
        If tables(tableID).gameid <> 0 Then Exit Sub

        If tables(tableID).rulelist = "OMNITT_PLAYER" Then intCards = totalcardsnum(frmSystray.Winsock(socket).Tag)

        If Tables(tableID).GoldWager > 0 Then
            Dim oPlayerAccount_P1 As New PlayerAccountDAL(Tables(tableID).Player1)
            Dim oPlayeraccount_P2 As New PlayerAccountDAL(Tables(tableID).Player2)

            If Tables(tableID).GoldWager > oPlayerAccount_P1.Gold Then
                Send(Tables(tableID).Player1, "CHALLENGECHATS *** Gold wager is too much for Player 1")
                Send(Tables(tableID).Player2, "CHALLENGECHATS *** Gold wager is too much for Player 1")
                Exit Sub
            ElseIf Tables(tableID).GoldWager > oPlayeraccount_P2.Gold Then
                Send(Tables(tableID).Player1, "CHALLENGECHATS *** Gold wager is too much for Player 2")
                Send(Tables(tableID).Player2, "CHALLENGECHATS *** Gold wager is too much for Player 2")
                Exit Sub
            End If
        End If

        If Left(tables(tableID).rulelist, 8) = "CHINCHIN" Then
            Divide(tables(tableID).rulelist, " ", lostvar, strWager1, strWager2)

            If strWager1 <> strWager2 Then
                Send(tables(tableID).player1, "CHALLENGECHATS *** Both wagers must be the same in order to begin")
                Send(tables(tableID).player2, "CHALLENGECHATS *** Both wagers must be the same in order to begin")

                tables(tableID).player_ready(1) = False
                tables(tableID).player_ready(2) = False

                Send(tables(tableID).player1, "CHALLENGEREADYS 1 0")
                Send(tables(tableID).player1, "CHALLENGEREADYS 2 0")
                Send(tables(tableID).player2, "CHALLENGEREADYS 1 0")
                Send(tables(tableID).player1, "CHALLENGEREADYS 2 0")
                Exit Sub
            End If
        End If

        If Tables(tableID).Player1 = frmSystray.Winsock(socket).Tag Then
            If Tables(tableID).Player_Ready(1) = True Then
                Tables(tableID).Player_Ready(1) = False
                intStatus = 0
            Else
                If intCards >= 15 Or Tables(tableID).RuleList <> "OMNITT_PLAYER" Then
                    Tables(tableID).Player_Ready(1) = True
                    intStatus = 1
                Else
                    GoTo skip
                End If
            End If

            intPlayer = 1
        ElseIf Tables(tableID).Player2 = frmSystray.Winsock(socket).Tag Then
            If Tables(tableID).Player_Ready(2) = True Then
                Tables(tableID).Player_Ready(2) = False
                intStatus = 0
            Else
                If intCards >= 15 Or Tables(tableID).RuleList <> "OMNITT_PLAYER" Then
                    Tables(tableID).Player_Ready(2) = True
                    intStatus = 1
                Else
                    GoTo skip
                End If
            End If

            intPlayer = 2
        End If

        ' Update the challenge boxes
        If Tables(tableID).Player1 <> String.Empty Then Send(Tables(tableID).Player1, String.Concat("CHALLENGEREADYS ", intPlayer, " ", intStatus))
        If Tables(tableID).Player2 <> String.Empty Then Send(Tables(tableID).Player2, String.Concat("CHALLENGEREADYS ", intPlayer, " ", intStatus))

        blnStart = True

        For x As Integer = 1 To 2
            If Tables(tableID).Player_Ready(x) = False Then blnStart = False
        Next x

        Dim rlOpen As String = String.Empty, rlRandom As String = String.Empty
        Dim elemental As String = String.Empty, comborule As String = String.Empty, wallrule As String = String.Empty, wager As String = String.Empty
        Dim minlvl As String = String.Empty, maxlvl As String = String.Empty, tradeall As String = String.Empty, tradediff As String = String.Empty, maxrule As String = String.Empty
        Dim ccrule As String = String.Empty, minrule As String = String.Empty, wageramt As String = String.Empty, direct As String = String.Empty, mirror As String = String.Empty, neutral As String = String.Empty
        Dim tradethree As String = String.Empty, immune As String = String.Empty, setwall As String = String.Empty, wallvalue As String = String.Empty, tradetwo As String = String.Empty, tradefour As String = String.Empty
        Dim skiprule As String = String.Empty, startee As String = String.Empty, starter As String = String.Empty, suddendeath As String = String.Empty, capturerule As String = String.Empty
        Dim minus_Renamed As String = String.Empty, same_Renamed As String = String.Empty, order As String = String.Empty, proom As String = String.Empty, reverse As String = String.Empty
        Dim trad As String = String.Empty, cross As String = String.Empty, plus_Renamed As String = String.Empty


        If Tables(tableID).GameID <> 0 Then Exit Sub

        If blnStart = True Then
            If InStr(1, Tables(tableID).RuleList, "SPHEREBREAK") > 0 Then
                Dim oSphere As New SphereFunctions
                Tables(tableID).GameID = Val(oSphere.spherechallenge(Tables(tableID).Player1 & " " & Tables(Val(CStr(tableID))).RuleList, GetSocket(Tables(tableID).Player2)))
                oSphere = Nothing
            ElseIf InStr(1, Tables(tableID).RuleList, "OMNITT") > 0 Then
                Dim oOTTFunctions As New OTTFunctions
                Tables(tableID).GameID = Val(oOTTFunctions.OTTChallengeYes("OMNICHALLENGEYES " & Tables(tableID).Player1 & " " & Tables(tableID).RuleList, GetSocket(Tables(tableID).Player2), Tables(tableID).RuleList & " " & Tables(tableID).Blocks))
            ElseIf Tables(tableID).RuleList = "MEMORY" Then
                Dim oMemoryFunctions As New MemoryFunctions
                Tables(tableID).GameID = Val(oMemoryFunctions.memorychallengeyes("MEMORYCHALLENGEYES " & Tables(tableID).Player1 & " " & Tables(tableID).RuleList, GetSocket(Tables(tableID).Player2)))
            ElseIf InStr(1, Tables(tableID).RuleList, "CHINCHIN") > 0 Then
                DoChinGame(tableID, Integer.Parse(strWager1), Integer.Parse(strWager2))
            ElseIf InStr(1, Tables(tableID).RuleList, "CARDWARS") > 0 Then
                Dim oCardwars As New CardWars
                Tables(tableID).GameID = Val(oCardwars.cardwarsyes("CARDWARSYES " & Tables(tableID).Player1 & " " & Tables(tableID).RuleList, GetSocket(Tables(tableID).Player2)))
                oCardwars = Nothing
            Else
                ' Check rules

                CDivide(Tables(tableID).RuleList, " ", rlOpen, rlRandom, proom, tradeall, tradediff, maxrule, maxlvl, ccrule, same_Renamed, plus_Renamed, trad, minrule, minlvl, wager, wageramt, wallrule, direct, comborule, mirror, elemental, neutral, order, cross, reverse, minus_Renamed, setwall, wallvalue, immune, tradetwo, tradethree, tradefour, starter, startee, suddendeath, skiprule, capturerule)

                Dim oPlayerAccount1 As New PlayerAccountDAL(Tables(tableID).Player1)
                Dim oPlayerAccount2 As New PlayerAccountDAL(Tables(tableID).Player2)

                If tradeall = "1" Or tradefour = "1" Or tradediff = "1" Then
                    If Date.Today < oPlayerAccount1.SignupDate.AddDays(3) Or Date.Today < oPlayerAccount2.SignupDate.AddDays(3) Then
                        ' Cant start the game
                        Send(Tables(tableID).Player1, "CHALLENGECHATS *** One of the table players cannot play with these trade rules due to signing up or getting a new deck in the last 3 days.")
                        Send(Tables(tableID).Player2, "CHALLENGECHATS *** One of the table players cannot play with these trade rules due to signing up or getting a new deck in the last 3 days.")
                        GoTo skip2
                    End If

                    If oPlayerAccount1.LastIP = oPlayerAccount2.LastIP Then
                        ' Cant start the game
                        Send(Tables(tableID).Player1, "CHALLENGECHATS *** You cannot play trade all, 4, or trade difference with same IP.")
                        Send(Tables(tableID).Player2, "CHALLENGECHATS *** You cannot play trade all, 4, or trade difference with same IP.")
                        GoTo skip2
                    End If
                Else
                    Tables(tableID).Lock = False
                End If

                If wager = "1" Then
                    Tables(tableID).Wager = Val(wageramt)
                Else
                    Tables(tableID).Wager = 0
                End If

                If oPlayerAccount1.Gold < Tables(tableID).Wager Or oPlayerAccount2.Gold < Tables(tableID).Wager Then
                    Send(Tables(tableID).Player1, "CHALLENGECHATS *** One of the table players does not have enough GP to play with these rules.")
                    Send(Tables(tableID).Player2, "CHALLENGECHATS *** One of the table players does not have enough GP to play with these rules.")
                    GoTo skip2
                ElseIf Tables(tableID).Wager < 0 Then
                    Send(Tables(tableID).Player1, "CHALLENGECHATS *** Wager values cannot be less than zero")
                    Send(Tables(tableID).Player2, "CHALLENGECHATS *** Wager values cannot be less than zero")
                    GoTo skip2
                End If

                Dim oTT As New TTFunctions
                Tables(tableID).GameID = Val(oTT.challengeyes("CHALLENGEYES " & Tables(tableID).Player1 & " " & Tables(tableID).RuleList, GetSocket(Tables(tableID).Player2), Tables(tableID).RuleList, Tables(tableID).Decks))
            End If

            'Call sendall(String.Concat("ADDTABLE ", tableID, " ", tables(Val(CStr(tableID))).player1, " ", tables(Val(CStr(tableID))).player2, " ", tables(tableID).rulelist, "@@", tables(tableID).comment, "@@", tables(tableID).Decks))
        End If

        'oTables.SendTable(Tables, tableID)

        Exit Sub

skip:

        Dim oTeamOTT As New TeamOTTFunctions
        Call oTeamOTT.sendchallenge_chat(String.Concat("Table Warning: ", frmSystray.Winsock(socket).Tag, " does not have enough cards to play playerdeck rules.  /kick [playernumber] or change game rules in order to play from this table."), Val(CStr(tableID)))
        oTeamOTT = Nothing
        Exit Sub

skip2:
        Tables(tableID).Player_Ready(1) = False
        Tables(tableID).Player_Ready(2) = False

        If Tables(tableID).Player1 <> String.Empty Then
            Send(Tables(tableID).Player1, "CHALLENGEREADYS 1 0")
            Send(Tables(tableID).Player1, "CHALLENGEREADYS 2 0")
        End If

        If Tables(tableID).Player2 <> String.Empty Then
            Send(Tables(tableID).Player2, "CHALLENGEREADYS 1 0")
            Send(Tables(tableID).Player2, "CHALLENGEREADYS 2 0")
        End If

        oTables.UpdateTable(Tables, tableID)
    End Sub

    Private Sub DoChinGame(ByVal TableID As Integer, ByVal iWagerP1 As Integer, ByVal iWagerP2 As Integer)
        Dim oPlayerAccount_P1 As New PlayerAccountDAL(Tables(tableID).Player1)
        Dim oPlayeraccount_P2 As New PlayerAccountDAL(Tables(tableID).Player2)

        If oPlayerAccount_P1.AP < iWagerP1 Then
            Send(Tables(TableID).Player1, "CHALLENGECHATS *** Player 1 does not have enough AP.")
            Send(Tables(TableID).Player2, "CHALLENGECHATS *** Player 1 does not have enough AP.")

            Tables(TableID).Player_Ready(1) = False
            Tables(TableID).Player_Ready(2) = False

            Send(Tables(TableID).Player1, "CHALLENGEREADYS 1 0")
            Send(Tables(TableID).Player1, "CHALLENGEREADYS 2 0")
            Send(Tables(TableID).Player2, "CHALLENGEREADYS 1 0")
            Send(Tables(TableID).Player2, "CHALLENGEREADYS 2 0")
            Exit Sub
        ElseIf oPlayeraccount_P2.AP < iWagerP2 Then
            Send(Tables(TableID).Player1, "CHALLENGECHATS *** Player 2 does not have enough AP.")
            Send(Tables(TableID).Player2, "CHALLENGECHATS *** Player 2 does not have enough AP.")

            Tables(TableID).Player_Ready(1) = False
            Tables(TableID).Player_Ready(2) = False

            Send(Tables(TableID).Player1, "CHALLENGEREADYS 1 0")
            Send(Tables(TableID).Player1, "CHALLENGEREADYS 2 0")
            Send(Tables(TableID).Player2, "CHALLENGEREADYS 1 0")
            Send(Tables(TableID).Player2, "CHALLENGEREADYS 2 0")
            Exit Sub
        End If

        Dim oChinchin As New Chinchirorin
        Tables(tableID).GameID = Val(oChinchin.chinchinyes("CHINCHINYES " & Tables(tableID).Player1 & " " & Tables(tableID).RuleList, GetSocket(Tables(tableID).Player2)))
        oChinchin = Nothing
    End Sub
End Class
