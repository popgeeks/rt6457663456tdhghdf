Imports System.Configuration
Imports MySql.Data.MySqlClient
Imports MySQLFactory.MySQLDBHelper
Imports System.Collections.generic

Friend Class frmserver
    Inherits System.Windows.Forms.Form

    Public ping As Integer

    Private Sub cmdremove_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdremove.Click
        On Error Resume Next

        Dim x As Integer

        If lstadmin.Text <> String.Empty Then
            For x = 0 To lstadmin.Items.Count - 1
                If lstadmin.Text = VB6.GetItemString(lstadmin, x) Then
                    lstadmin.Items.RemoveAt((x))
                End If
            Next x
        End If

    End Sub

    Private Sub frmserver_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load
        Try
            Dim ininame As String = My.Application.Info.DirectoryPath & "\config\ttserver.ini"
            Dim version As String = My.Application.Info.Version.Major & "." & My.Application.Info.Version.Minor & "." & My.Application.Info.Version.Revision & "a" & "-Regis"

            ReDim players(0)
            writeini("Configuration", "Port", "4000", ininame)

            oServerConfig.MaxPlayers = 499
            Me.Tag = "499"

            Server_Load()

            Call dbEXECQuery("TRUNCATE TABLE onlineusers")

            'lstaway.Items.Clear()
            'numbconn.Text = "0 (Max: 499)"

            'allowconn.CheckState = System.Windows.Forms.CheckState.Checked

            If oServerConfig.ServerName = String.Empty Then
                MsgBox("WARNING! You must configure the server under serveraids -> Server Configuration.  In order for users to be able to enter this server you must enter a server name.", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "Server Name Not Configured")
                ' allowconn.CheckState = System.Windows.Forms.CheckState.Unchecked
            End If

            'Server initialization
            frmSystray.Show()
            tmrkilltable.Enabled = True

            'Set the form with all the configuration and stuff
            Me.Text = "Triple Triad Extreme - Version " & version & " - " & oServerConfig.GetConfigurationValue("ServerPort")

            For x As Integer = 1 To oServerConfig.MaxPlayers
                lstplayers.Items.Add(x - 1 & ":")
                'lstping.Items.Add("0")
                lstpacketcontrol.Items.Add("0")
            Next x

            '        Call maindirs()
            tmrping.Enabled = True
            ping = 0

            Timer1.Enabled = True
            timedout.Enabled = False
            tmrSendPackets.Enabled = True

            'lblstatus.Text = checkwinsock()
            Me.Show()
        Catch ex As Exception
            Call errorsub(Err.Description, "frmServer - Form_Load")
        End Try
    End Sub

    Private Sub frmserver_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        Me.Hide()
    End Sub

    Private Function checkwinsock() As String
        Select Case frmSystray.Winsock(0).CtlState
            Case 0
                checkwinsock = "Closed"
            Case 2
                checkwinsock = "Connected"
            Case 7
                checkwinsock = "Connected"
            Case 8
                checkwinsock = "Closing"
            Case Else
                Return String.Empty
        End Select
    End Function


    Private Sub Timer1_Tick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles Timer1.Tick
        Call ProcessTimer()
    End Sub

    Private Sub ProcessTimer()
        Try
            Dim dTable As DataTable
            Dim oFunctions As New DatabaseFunctions
            Dim oTables As New BusinessLayer.Tables

            dTable = oFunctions.PlayerStats

            If dTable.Rows.Count > 0 Then
                For x As Integer = 0 To dTable.Rows.Count - 1
                    Send(String.Empty, String.Format("UPDATESTATS {0} {1} {2} {3} {4} {5}", dTable.Rows.Item(x).Item("ap").ToString, dTable.Rows.Item(x).Item("gold").ToString, dTable.Rows.Item(x).Item("rush").ToString, oServerConfig.TTChest, oServerConfig.THChest, oServerConfig.WarChest), dTable.Rows.Item(x).Item("id"))

                    If Integer.Parse(dTable.Rows.Item(x).Item("gold").ToString) < 0 Then
                        Try
                            Dim oChatFunctions As New ChatFunctions()
                            oChatFunctions.uniwarn(String.Format("WARNING!!! {0} has less than zero gold.  Investigate immediate.  If a lot less than zero, ban immediately.  Player has {1} gold.", frmSystray.Winsock(dTable.Rows.Item(x).Item("id")).Tag, dTable.Rows.Item(x).Item("gold").ToString))
                            oChatFunctions = Nothing
                        Catch ex As Exception
                        End Try
                    End If
                Next
            End If

            If Date.Now.Minute Mod 10 = 0 Then
                oServerConfig.Refresh()
                oTables.RefreshDatabaseTables(Tables)
            End If

            If Date.Now.Hour Mod 2 = 0 And Date.Now.Minute = 0 Then oServerConfig.LoadFilter()

            If Date.Now.Minute Mod 2 = 0 Then
                If oServerConfig.KickOff >= 10 Then
                    'Reset Detected
                    oFunctions.SendEmail(ConnectionString, "gamewar@yahoo.com", "Reset Detected!", String.Concat("Reset was detected on ", Date.Now))
                    oServerConfig.KickOff = 0
                Else
                    oServerConfig.KickOff = 0
                End If
            End If

            Dim oProcessQueue As New ProcessQueue

            Dim oDataTable As DataTable = oProcessQueue.LoadTable

            If Not (oDataTable Is Nothing) Then
                For x As Integer = 0 To oDataTable.Rows.Count - 1
                    Dim oWinsock As New clsWinsock(oDataTable.Rows(x).Item("sKeyword").ToString, 0)
                    oWinsock.HandleQueue()

                    Debug.Print("Process Queue -- Seconds: " & oWinsock.ProcessingSeconds)
                    'Me.AddProcessTimer("Process Queue", oWinsock.ProcessingSeconds)

                    oWinsock = Nothing

                    oProcessQueue.UpdateRecord(oDataTable.Rows(x).Item("ID"))
                Next
            End If

            Timer1.Enabled = True
            oFunctions = Nothing
        Catch ex As Exception
            'Call errorsub(ex.ToString, "ProcessTimer")
        Finally
            oServerConfig.InsertServerHealthRecord()
        End Try
    End Sub

    Private Sub tmrkilltable_Tick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles tmrkilltable.Tick
        Try
            Dim oTables As New BusinessLayer.Tables
            Dim oTime As New ProcessingTime

            If Date.Now.Minute Mod 2 = 0 Then Exit Sub

            oTime.BeginProcessing = Date.Now

            For x As Integer = 1 To oServerConfig.MaxTables - 1
                If Tables(x).Player1 <> String.Empty Or Tables(x).Player2 <> String.Empty Or Tables(x).Player3 <> String.Empty Or Tables(x).Player4 <> String.Empty Then
                    If isOnline(Tables(x).Player1) = False Or isOnline(Tables(x).Player2) = False Or isOnline(Tables(x).Player3) = False Or isOnline(Tables(x).Player4) = False Then
                        If Tables(x).Comment = "Triple Triad" Then
                            If isOnline(Tables(x).Player1) = False Then
                                Call CardPenalty(x, 2, Tables(x).Player1)
                            ElseIf isOnline(Tables(x).Player2) = False Then
                                Call CardPenalty(x, 1, Tables(x).Player2)
                            End If
                        End If

                        Call oTables.SendKillMatch(Tables(x))
                        'Call oTables.EndMatch(String.Concat("KILLTABLE " & x), 0, False, True)
                        Call oTables.ClosePlayerTable(Tables, x)
                    End If
                End If
            Next x

            tmrkilltable.Enabled = True
            oTime.EndProcessing = Date.Now
            'Debug.Print(String.Concat("Timer - Table Kill: ", oTime.ProcessingSeconds, " seconds"))
            'Me.AddProcessTimer("Table Kill Process", oTime.ProcessingSeconds)
        Catch ex As Exception
        End Try
    End Sub

    Private Sub tmrpacket_Tick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles tmrpacket.Tick
        For x As Integer = 0 To Me.lstpacketcontrol.Items.Count - 1
            VB6.SetItemString(Me.lstpacketcontrol, x, "0")
        Next x
    End Sub

    Private Sub tmrping_Tick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles tmrping.Tick
        Try
            Dim oChatFunctions As New ChatFunctions

            ping += 1

            If ping Mod oServerConfig.Ping = 0 Then
                For x As Integer = 1 To (frmSystray.Winsock.Count - 1)
                    If Not (players(x) Is Nothing) Then
                        If players(x).Ping = False Then
                            If frmSystray.Winsock(x).CtlState <> MSWinsockLib.StateConstants.sckClosed Then
                                Send(String.Empty, "EXIT Server disconnected you from the game network because you timed out.", x)

                                If x <> 0 Then
                                    oChatFunctions.uniwarn(String.Concat(players(x).nickname, " has timed out."))
                                    RemPlayer(players(x).nickname, x, False)
                                    frmSystray.Winsock(x).Close()
                                End If
                            End If
                        End If
                    End If
                Next x

                For x As Integer = 1 To (frmSystray.Winsock.Count - 1)
                    If Not (players(x) Is Nothing) Then players(x).Ping = False
                Next x

                'oChatFunctions.unisend("PING", String.Empty)
                ping = 0
            Else
                'oChatFunctions.unisend("PING", String.Empty)
                tmrping.Enabled = True
                'ping = 0
                Exit Sub
            End If

            tmrping.Enabled = True
        Catch ex As Exception
            Call errorsub(ex.ToString, "tmrping.tick")
        End Try
    End Sub

    Private Sub maindirs()

        If (Dir(My.Application.Info.DirectoryPath & "\accounts", FileAttribute.Directory) = "") Then
            MkDir(My.Application.Info.DirectoryPath & "\accounts")
        End If


        'If (Dir(My.Application.Info.DirectoryPath & "\chatrooms", FileAttribute.Directory) = "") Then
        '    MkDir(My.Application.Info.DirectoryPath & "\chatrooms")
        'End If


        If (Dir(My.Application.Info.DirectoryPath & "\logs", FileAttribute.Directory) = "") Then
            MkDir(My.Application.Info.DirectoryPath & "\logs")
        End If


        'If (Dir(My.Application.Info.DirectoryPath & "\backups", FileAttribute.Directory) = "") Then
        '    MkDir(My.Application.Info.DirectoryPath & "\backups")
        'End If


        If (Dir(My.Application.Info.DirectoryPath & "\gamefiles", FileAttribute.Directory) = "") Then
            MkDir(My.Application.Info.DirectoryPath & "\gamefiles")
        End If


        If (Dir(My.Application.Info.DirectoryPath & "\stats", FileAttribute.Directory) = "") Then
            MkDir(My.Application.Info.DirectoryPath & "\stats")
        End If


        'If (Dir(My.Application.Info.DirectoryPath & "\guilds", FileAttribute.Directory) = "") Then
        '    MkDir(My.Application.Info.DirectoryPath & "\guilds")
        'End If

        If (Dir(My.Application.Info.DirectoryPath & "\logs\accounts\", FileAttribute.Directory) = "") Then
            MkDir(My.Application.Info.DirectoryPath & "\logs\accounts\")
        End If


        If (Dir(My.Application.Info.DirectoryPath & "\logs\accounts\packetlogs\", FileAttribute.Directory) = "") Then
            MkDir(My.Application.Info.DirectoryPath & "\logs\accounts\packetlogs\")
        End If
    End Sub

    Private Sub cmsPlayer_Disconnect_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmsPlayer_Disconnect.Click
        Dim plrNick As String = String.Empty, lostvar As String = String.Empty
        Dim plrSocket As Integer

        If lstplayers.Text = String.Empty Then Exit Sub

        Divide((lstplayers.Text), " ", lostvar, plrNick)

        plrSocket = GetSocket(plrNick)


        If frmSystray.Winsock(plrSocket).CtlState = MSWinsockLib.StateConstants.sckConnected Then
            'frmSystray.Winsock(plrSocket).SendData "EXIT You were disconnected from the game server by an administrator.*****"
            Send(String.Empty, "EXIT You were disconnected from the game server by an administrator.*****", plrSocket)
            System.Windows.Forms.Application.DoEvents()
        End If

        Call quit("QUIT " & plrNick, GetSocket(plrNick), False)
    End Sub

    Private Sub btnRefreshCards_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefreshCards.Click
        Dim oCards As New Entities.BusinessObjects.PlayingCardList
        Dim oList As New List(Of String)

        LoadCards(oCards, oList)

        Cards = oCards
        CardNameList = oList
    End Sub

    Private Sub tmrSendPackets_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tmrSendPackets.Tick
        Call SendPackets()
        tmrSendPackets.Enabled = True
    End Sub
End Class