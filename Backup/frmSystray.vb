Friend Class frmSystray
    Inherits System.Windows.Forms.Form

    Private Sub frmSystray_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load
        Try
            Call errorsub("Before show", "")
            Me.Show()

            Dim ininame As String = My.Application.Info.DirectoryPath & "\config\ttserver.ini"
            Dim version As String = My.Application.Info.Version.Major & "." & My.Application.Info.Version.Minor & "." & My.Application.Info.Version.Revision & "a" & "-Regis"

            'the form must be fully visible before calling Shell_NotifyIcon
            Me.Refresh()

            niSystray.Icon = Me.Icon
            niSystray.ContextMenuStrip = Me.cmsMenu

            Me.Winsock(0).LocalPort = Val(oServerConfig.GetConfigurationValue("ServerPort"))
            Me.Winsock(0).Listen()
            frmserver.Show()
        Catch ex As Exception
            Call errorsub(ex.ToString, "frmSystray - Form_Load")
        End Try
    End Sub

    Private Sub frmSystray_Resize(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Resize
        Try
            'this is necessary to assure that the minimized window is hidden
            If Me.WindowState = System.Windows.Forms.FormWindowState.Minimized Then Me.Hide()
            If Me.WindowState <> System.Windows.Forms.FormWindowState.Minimized Then Me.WindowState = System.Windows.Forms.FormWindowState.Minimized
        Catch ex As Exception
            Call errorsub(ex.ToString, "frmsystray_resize")
        End Try
    End Sub

    Private Sub frmSystray_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        'this removes the icon from the system tray
        Shell_NotifyIcon(NIM_DELETE, nid)
    End Sub

    Public Sub Winsock_CloseEvent(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles Winsock.CloseEvent
        Dim Index As Integer = Winsock.GetIndex(eventSender)

        Dim nick As String = Me.Winsock(Index).Tag
        If nick.Trim = String.Empty Then Exit Sub
        Dim oChatFunctions As New ChatFunctions

        If Me.Winsock(Index).CtlState <> MSWinsockLib.StateConstants.sckClosed Then Me.Winsock(Index).Close()

        RemovePlayerClass(Index) ' <<-- added by Jonathan 1/18/00
        Call quit("QUIT 0 1", Index, True)

        RemPlayer(nick, Index, False)
        RemAdmin(nick)

        System.Windows.Forms.Application.DoEvents()

        VB6.SetItemString(frmserver.lstplayers, Index, Index & ": ")

        If Me.Winsock(Index).Tag <> String.Empty Then oChatFunctions.unisend("REMPLAYER", Me.Winsock(Index).Tag)
        Me.Winsock(Index).Tag = String.Empty
    End Sub

    Private Sub Winsock_ConnectionRequest(ByVal eventSender As System.Object, ByVal eventArgs As AxMSWinsockLib.DMSWinsockControlEvents_ConnectionRequestEvent) Handles Winsock.ConnectionRequest
        Dim Index As Short = Winsock.GetIndex(eventSender)
        Dim tempint As Integer

        Dim found As Boolean = False
        Dim intNewConnection As Integer = 0
        'create another control to handle this connection
        On Error Resume Next

        For x As Integer = 0 To (Winsock.Count - 1)
            If Not found = True Then
                If Winsock(x).CtlState = MSWinsockLib.StateConstants.sckClosing Or Winsock(x).CtlState = MSWinsockLib.StateConstants.sckClosed Then
                    found = True

                    If Winsock(x).CtlState <> MSWinsockLib.StateConstants.sckClosed Then
                        'oServerConfig.DeleteUser(x, Winsock(x).Tag)
                        RemPlayer(Winsock(x).Tag, x, False)
                        Winsock(x).Close()
                    End If

                    intNewConnection = x
                End If
            End If
        Next

        If intNewConnection = 0 Then
            intNewConnection = Me.Winsock.UBound + 1
            Me.Winsock.Load(intNewConnection)
        End If

        'added by Jonathan 1/18/00
        NewPlayerClass(intNewConnection)

        'accept this connection
        Me.Winsock(intNewConnection).Tag = String.empty
        Me.Winsock(intNewConnection).LocalPort = Me.Winsock(0).LocalPort
        Me.Winsock(intNewConnection).Accept(eventArgs.requestID)

        If Winsock(intNewConnection).RemoteHostIP = String.Empty Then
            Send(String.Empty, "EXIT The server is currently not accepting new connections.  Please report this on the forums as the server is probably down.", intNewConnection)
            System.Windows.Forms.Application.DoEvents()
            Winsock(intNewConnection).Close()
        ElseIf GetRowCount("SELECT * FROM bannedips WHERE IP = '" & Winsock(intNewConnection).RemoteHostIP & "'") > 1 Then
            Send(String.Empty, "EXIT Sorry but this IP address has been banned from this server", intNewConnection)
            System.Windows.Forms.Application.DoEvents()
            Winsock(intNewConnection).Close()
        End If

        Dim Section(3) As String

        Divide(Winsock(intNewConnection).RemoteHostIP, ".", Section(0), Section(1), Section(2), Section(3))

        If GetRowCount("SELECT * FROM bannedips WHERE IP ='" & Section(0) & "." & Section(1) & "'") > 1 Then
            Send(String.Empty, "EXIT Sorry, but this subdomain has been banned.  Please contact Atomsplit (gamewar@gamewar.com) if you feel that this should be lifted.", intNewConnection)
            System.Windows.Forms.Application.DoEvents()
            Winsock(intNewConnection).Close()
        End If

        If GetRowCount("SELECT * FROM bannedips WHERE IP ='" & Section(0) & "." & Section(1) & "." & Section(2) & "'") > 1 Then
            Send(String.Empty, "EXIT Sorry, but this ip mask has been banned.  Please contact Atomsplit (gamewar@gamewar.com) if you feel that this should be lifted.", intNewConnection)
            System.Windows.Forms.Application.DoEvents()
            Winsock(intNewConnection).Close()
        End If

        'If frmserver.allowconn.CheckState = 0 And Mid$(Winsock(intNewConnection).RemoteHostIP, 1, 9) <> "192.168.1" Then
        '    Send(String.Empty, "EXIT This server dosen't accept connection (possible cause : testing or debugging)", intNewConnection)
        '    System.Windows.Forms.Application.DoEvents()
        '    Winsock(intNewConnection).Close()
        'End If

        If oServerConfig.PlayersOnline + 1 > oServerConfig.MaxPlayers Then
            Send(String.Empty, "EXIT The number of players in which this server is allowed has reached its maximum.  Please try again or try another server (if applicable).", intNewConnection)
            System.Windows.Forms.Application.DoEvents()
            Winsock(intNewConnection).Close()
        End If
    End Sub

    Private Sub winsock_DataArrival(ByVal eventSender As System.Object, ByVal eventArgs As AxMSWinsockLib.DMSWinsockControlEvents_DataArrivalEvent) Handles Winsock.DataArrival
        Dim oStringFunctions As New LogicLayer.Utility.StringFunctions

        Try
            Dim incoming As String = String.Empty
            Dim Index As Integer = Winsock.GetIndex(eventSender)

            ' Any packet sent to the server should count as a ping.
            ' Change February 15, 2009
            oServerConfig.BytesReceived += eventArgs.bytesTotal
            oServerConfig.PacketsReceived += 1

            Winsock(Index).GetData(incoming, VariantType.String, 50000)

            Dim oWinsock As New clsWinsock(incoming, Index)
            'Dim oThread As New Thread(AddressOf oWinsock.HandleIncoming)
            'oThread.Start()
            oWinsock.HandleIncoming(False)
            'oThread = Nothing
            Debug.Print("-- Seconds: " & oWinsock.ProcessingSeconds)
            'oStringFunctions.writepacketlog(Me.Winsock(Index).Tag, oWinsock.DecryptedIncoming, oWinsock.ProcessingSeconds)
            oWinsock = Nothing
        Catch ex As Exception
            Call errorsub(ex.ToString, "winsock_datarrival")
        Finally
            oStringFunctions = Nothing
        End Try
    End Sub

    Private Sub Winsock_Error(ByVal eventSender As System.Object, ByVal eventArgs As AxMSWinsockLib.DMSWinsockControlEvents_ErrorEvent) Handles Winsock.Error
        Dim Index As Short = Winsock.GetIndex(eventSender)
        If eventArgs.number <> 10054 Then Debug.Print("Winsock error." & vbCrLf & vbCrLf & "Number " & eventArgs.number & vbCrLf & vbCrLf & "Descripton : " & eventArgs.description)
    End Sub

    Private Sub Winsock_SendComplete(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles Winsock.SendComplete
        Dim Index As Short = Winsock.GetIndex(eventSender)
        'added by Jonathan 1/18/00
        If Not (players(Index) Is Nothing) Then
            players(Index).SendingData = False
        End If
    End Sub

    Private Sub RestoreToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RestoreToolStripMenuItem.Click
        frmserver.Show()
    End Sub

    Private Sub WithoutReasonToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles WithoutReasonToolStripMenuItem.Click
        For x As Integer = 0 To Me.Winsock.Count - 1

            If Me.Winsock(x).CtlState = MSWinsockLib.StateConstants.sckConnected Then
                'frmSystray.Winsock(x).SendData "EXIT The server was closed by the administrator."
                Send(String.Empty, "EXIT The server was closed by the administrator.", x)
                System.Windows.Forms.Application.DoEvents()
            End If
        Next
        'called when user clicks the popup menu Exit command
        Me.Close()
        End
    End Sub

    Private Sub WithReasonToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles WithReasonToolStripMenuItem.Click
        Dim tempmsg As String = InputBox("Message for shutdown")
        If tempmsg = String.Empty Then Exit Sub

        For x As Integer = 0 To Me.Winsock.Count - 1
            If Me.Winsock(x).CtlState = MSWinsockLib.StateConstants.sckConnected Then
                Send(String.Empty, String.Concat("EXIT ", tempmsg), x)
                System.Windows.Forms.Application.DoEvents()
            End If
        Next

        Me.Close()
        End
    End Sub
End Class