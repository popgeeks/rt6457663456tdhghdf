Imports System.Configuration
Imports System.Collections.Generic

Module modTransmissions
    Public oSendQueue As New Entities.PacketContainer
    Public oReceiveQueue As New Entities.PacketContainer
    Public oPlayerKickList As New Entities.PlayerKickListContainer

    Public Sub Send(ByVal sendnick As String, ByVal sendtext As String, Optional ByVal sendsocket As Integer = 0, Optional ByVal blnSkipEncrypt As Boolean = False)
        Try
            If sendnick = String.Empty And sendsocket = 0 Then Exit Sub
            If sendsocket.ToString = String.Empty Or sendsocket = 0 Then sendsocket = GetSocket(sendnick)

            If (Not (players(sendsocket) Is Nothing) And sendsocket > 0) Or LCase(sendnick) <> "server" Then
                oSendQueue.Add(sendnick, String.Concat(sendtext & Chr(13)), sendsocket, Date.Now())
            End If
        Catch ex As Exception
        End Try
    End Sub

    Public Sub ReceivePackets()
        Dim dBeginTime As DateTime = Date.Now

        If oReceiveQueue.Count = 0 Then Exit Sub

        Dim iSocket As Integer = 0
        Dim sPacket As String = String.Empty

        Try
            Do Until oReceiveQueue.Count = 0
                iSocket = oReceiveQueue(0).Socket
                sPacket = oReceiveQueue(0).Packet

                If frmSystray.Winsock(iSocket).CtlState = MSWinsockLib.StateConstants.sckConnected Then
                    Dim oWinsock As New clsWinsock(sPacket, iSocket)
                    oWinsock.HandleIncoming(False)

                    oReceiveQueue.RemoveAt(0)

                    oServerConfig.BytesReceived += sPacket.Length
                    oServerConfig.PacketsReceived += 1

                    Dim dEndTime As DateTime = Date.Now
                    Dim tsProcessingTime As TimeSpan = dEndTime.Subtract(dBeginTime)
                    Debug.Print(String.Concat("Packet Received: ", sPacket, " - ", tsProcessingTime.Milliseconds, "ms"))
                    oWinsock = Nothing
                    System.Windows.Forms.Application.DoEvents()
                Else
                    oReceiveQueue.RemoveAt(0)
                End If
            Loop
        Catch eNull As System.NullReferenceException
            oReceiveQueue.RemoveAt(0)

            oServerConfig.BytesReceived += sPacket.Length
            oServerConfig.PacketsReceived += 1
        Catch ex As Exception
        End Try
    End Sub

    Public Sub SendPackets()
        Dim dBeginTime As DateTime = Date.Now

        If oSendQueue.Count = 0 Then Exit Sub
        Dim iSocket As Integer = 0
        Dim sPacket As String = String.Empty

        Try
            Do Until oSendQueue.Count = 0
                iSocket = oSendQueue(0).Socket
                sPacket = oSendQueue(0).Packet

                If Not players(iSocket).SendingData Then
                    If frmSystray.Winsock(iSocket).CtlState = MSWinsockLib.StateConstants.sckConnected Then
                        Dim sItems() As String = sPacket.Split(" ")
                        Dim sKeyword As String = sItems(0)

                        players(iSocket).SendingData = True

                        frmSystray.Winsock(iSocket).SendData(sPacket)
                        oSendQueue.RemoveAt(0)

                        oServerConfig.BytesSent += sPacket.Length
                        oServerConfig.PacketsSent += 1

                        Dim dEndTime As DateTime = Date.Now
                        Dim tsProcessingTime As TimeSpan = dEndTime.Subtract(dBeginTime)
                        Debug.Print(String.Concat("Packet Sent: ", sPacket, " - ", tsProcessingTime.Milliseconds, "ms"))

                        If sKeyword = "EXIT" Or sKeyword = "LOADGAME-WRONGPASS" Or sKeyword = "LOADGAME-NOSUCHNICK" Or sKeyword = "UPDATE" Or sKeyword = "QUIT" Then
                            ' Close the socket
                            'If iSocket <> 0 Then frmSystray.Winsock(iSocket).Close()
                            oPlayerKickList.Add(iSocket)
                        End If

                        System.Windows.Forms.Application.DoEvents()
                    Else
                        oSendQueue.RemoveAt(0)
                    End If

                    If Not players(iSocket) Is Nothing Then players(iSocket).SendingData = False
                End If
            Loop

            If Not (players(iSocket) Is Nothing) Then
                players(iSocket).ProcessingData = False
            End If
        Catch eNull As System.NullReferenceException
            oSendQueue.RemoveAt(0)

            oServerConfig.BytesSent += sPacket.Length
            oServerConfig.PacketsSent += 1
        Catch ex As Exception

        Finally
            If Not (players(iSocket) Is Nothing) Then
                players(iSocket).ProcessingData = False
            End If
        End Try
    End Sub

    ''' <summary>
    ''' Close the socket
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub KickPlayerSocketsInQueue()
        Dim iSocket As Integer = 0

        Try
            Do Until oPlayerKickList.Count = 0
                iSocket = oPlayerKickList(0).Socket
                If iSocket <> 0 Then frmSystray.Winsock(iSocket).Close()
                oPlayerKickList.RemoveAt(0)
                System.Windows.Forms.Application.DoEvents()
                Debug.Print(String.Format("{0} Socket Closed by Kick Player Timer", iSocket))
            Loop
        Catch eNull As System.NullReferenceException
            oPlayerKickList.RemoveAt(0)
        Catch ex As Exception
        End Try
    End Sub

    Public Sub closesocket(ByVal socket As Integer)
        Try
            If socket = 0 Then Exit Sub

            RemPlayer(String.Empty, socket, False)
            Call quit("QUIT " & frmSystray.Winsock(socket).Tag, socket, True)
        Catch ex As Exception
            Call errorsub(ex, "quit")
        End Try
    End Sub

    'Public Sub SendOld(ByRef sendnick As String, ByRef sendtext As String, Optional ByRef sendsocket As Integer = 0, Optional ByVal blnSkipEncrypt As Boolean = False)
    '    Dim encryptedtext As String = String.Empty
    '    Dim blnThisProcedureIsCurrent As Boolean

    '    Try
    '        'If blnlogpackets = True Then frmpacket.txtsent.Text = frmpacket.txtsent.Text & frmSystray.Winsock(sendsocket).Tag & ">> " & sendtext & vbCrLf
    '        If sendnick = String.Empty And sendsocket = 0 Then Exit Sub

    '        Dim sendtemp As String = sendtext

    '        'Dim oEncrypt As New LogicLayer.Utility.Encryption
    '        'Dim oEncrypt As New clsCryptography
    '        Dim oEncrypt As New TTEServerEncrypt64.Class1

    '        If Left(sendtext, 9) <> "CHATBLOCK" And Left(sendtext, 4) <> "CHAT" And Left(sendtext, 8) <> "GAMECHAT" And blnSkipEncrypt = False Then
    '            encryptedtext = oEncrypt.Encode(sendtext)
    '        Else
    '            encryptedtext = sendtext
    '        End If


    '        If sendsocket.ToString = String.Empty Or sendsocket = 0 Then sendsocket = GetSocket(sendnick)
    '        sendtext = sendtemp

    '        If (Not (players(sendsocket) Is Nothing) And sendsocket > 0) Or LCase(sendnick) <> "server" Then
    '            players(sendsocket).DataWaitingToSend = players(sendsocket).DataWaitingToSend & encryptedtext & Chr(13)

    '            If Not players(sendsocket).SendingData Then

    '                If frmSystray.Winsock(sendsocket).CtlState = MSWinsockLib.StateConstants.sckConnected Then
    '                    Do Until players(sendsocket).DataWaitingToSend = String.Empty
    '                        players(sendsocket).SendingData = True
    '                        frmSystray.Winsock(sendsocket).SendData(players(sendsocket).DataWaitingToSend)

    '                        players(sendsocket).DataWaitingToSend = String.Empty
    '                        System.Windows.Forms.Application.DoEvents() ' <-- leave in this doevents, this is the only one on the server that needs to be here.

    '                        If players(sendsocket) Is Nothing Then Exit Do
    '                        If frmSystray.Winsock(sendsocket).CtlState <> MSWinsockLib.StateConstants.sckConnected Then Exit Do
    '                    Loop
    '                End If

    '                If Not players(sendsocket) Is Nothing Then players(sendsocket).SendingData = False
    '            End If
    '        End If

    '        oServerConfig.BytesSent += Len(sendtext)
    '        oServerConfig.PacketsSent += 1

    '        'Added by Jonathan 1/18/99
    '        'make sure that we release the ProcessingData
    '        'flag for this socket.

    '        If Not (players(sendsocket) Is Nothing) Then
    '            If blnThisProcedureIsCurrent Then
    '                players(sendsocket).ProcessingData = False
    '            End If
    '        End If
    '        Exit Sub
    '    Catch ex As Exception
    '        If Not (players(sendsocket) Is Nothing) Then
    '            If blnThisProcedureIsCurrent Then
    '                players(sendsocket).ProcessingData = False
    '            End If
    '        End If
    '    End Try
    'End Sub
End Module

'Imports System.Configuration

'Module modTransmissions
'    Public Sub Send(ByRef sendnick As String, ByRef sendtext As String, Optional ByRef sendsocket As Integer = 0, Optional ByVal blnSkipEncrypt As Boolean = False)
'        Dim encryptedtext As String = String.Empty
'        Dim blnThisProcedureIsCurrent As Boolean

'        Try
'            'If blnlogpackets = True Then frmpacket.txtsent.Text = frmpacket.txtsent.Text & frmSystray.Winsock(sendsocket).Tag & ">> " & sendtext & vbCrLf
'            If sendnick = String.Empty And sendsocket = 0 Then Exit Sub

'            Dim sendtemp As String = sendtext

'            'Dim oEncrypt As New LogicLayer.Utility.Encryption
'            'Dim oEncrypt As New clsCryptography

'            'If Left(sendtext, 9) <> "CHATBLOCK" And Left(sendtext, 4) <> "CHAT" And Left(sendtext, 8) <> "GAMECHAT" And blnSkipEncrypt = False Then
'            '    encryptedtext = oEncrypt.Encode(sendtext)
'            'Else
'            encryptedtext = sendtext
'            'End If


'            If sendsocket.ToString = String.Empty Or sendsocket = 0 Then sendsocket = GetSocket(sendnick)
'            sendtext = sendtemp

'            If (Not (players(sendsocket) Is Nothing) And sendsocket > 0) Or LCase(sendnick) <> "server" Then
'                players(sendsocket).DataWaitingToSend = players(sendsocket).DataWaitingToSend & encryptedtext & Chr(13)

'                If Not players(sendsocket).SendingData Then

'                    If frmSystray.Winsock(sendsocket).CtlState = MSWinsockLib.StateConstants.sckConnected Then
'                        Do Until players(sendsocket).DataWaitingToSend = String.Empty
'                            players(sendsocket).SendingData = True
'                            frmSystray.Winsock(sendsocket).SendData(players(sendsocket).DataWaitingToSend)

'                            players(sendsocket).DataWaitingToSend = String.Empty
'                            System.Windows.Forms.Application.DoEvents() ' <-- leave in this doevents, this is the only one on the server that needs to be here.

'                            If players(sendsocket) Is Nothing Then Exit Do
'                            If frmSystray.Winsock(sendsocket).CtlState <> MSWinsockLib.StateConstants.sckConnected Then Exit Do
'                        Loop
'                    End If

'                    If Not players(sendsocket) Is Nothing Then players(sendsocket).SendingData = False
'                End If
'            End If

'            oServerConfig.BytesSent += Len(sendtext)
'            oServerConfig.PacketsSent += 1

'            'Added by Jonathan 1/18/99
'            'make sure that we release the ProcessingData
'            'flag for this socket.

'            If Not (players(sendsocket) Is Nothing) Then
'                If blnThisProcedureIsCurrent Then
'                    players(sendsocket).ProcessingData = False
'                End If
'            End If
'            Exit Sub
'        Catch ex As Exception
'            If Not (players(sendsocket) Is Nothing) Then
'                If blnThisProcedureIsCurrent Then
'                    players(sendsocket).ProcessingData = False
'                End If
'            End If
'        End Try
'    End Sub

'    Public Sub closesocket(ByVal socket As Integer)
'        Try
'            If socket = 0 Then Exit Sub

'            RemPlayer(String.Empty, socket, False)
'            Call quit("QUIT " & frmSystray.Winsock(socket).Tag, socket, True)
'        Catch ex As Exception
'            Call errorsub(ex, "quit")
'        End Try
'    End Sub

'    Public Sub SendOld(ByRef sendnick As String, ByRef sendtext As String, Optional ByRef sendsocket As Integer = 0, Optional ByVal blnSkipEncrypt As Boolean = False)
'        Dim encryptedtext As String = String.Empty
'        Dim blnThisProcedureIsCurrent As Boolean

'        Try
'            'If blnlogpackets = True Then frmpacket.txtsent.Text = frmpacket.txtsent.Text & frmSystray.Winsock(sendsocket).Tag & ">> " & sendtext & vbCrLf
'            If sendnick = String.Empty And sendsocket = 0 Then Exit Sub

'            Dim sendtemp As String = sendtext

'            'Dim oEncrypt As New LogicLayer.Utility.Encryption
'            'Dim oEncrypt As New clsCryptography
'            Dim oEncrypt As New TTEServerEncrypt64.Class1

'            If Left(sendtext, 9) <> "CHATBLOCK" And Left(sendtext, 4) <> "CHAT" And Left(sendtext, 8) <> "GAMECHAT" And blnSkipEncrypt = False Then
'                encryptedtext = oEncrypt.Encode(sendtext)
'            Else
'                encryptedtext = sendtext
'            End If


'            If sendsocket.ToString = String.Empty Or sendsocket = 0 Then sendsocket = GetSocket(sendnick)
'            sendtext = sendtemp

'            If (Not (players(sendsocket) Is Nothing) And sendsocket > 0) Or LCase(sendnick) <> "server" Then
'                players(sendsocket).DataWaitingToSend = players(sendsocket).DataWaitingToSend & encryptedtext & Chr(13)

'                If Not players(sendsocket).SendingData Then

'                    If frmSystray.Winsock(sendsocket).CtlState = MSWinsockLib.StateConstants.sckConnected Then
'                        Do Until players(sendsocket).DataWaitingToSend = String.Empty
'                            players(sendsocket).SendingData = True
'                            frmSystray.Winsock(sendsocket).SendData(players(sendsocket).DataWaitingToSend)

'                            players(sendsocket).DataWaitingToSend = String.Empty
'                            System.Windows.Forms.Application.DoEvents() ' <-- leave in this doevents, this is the only one on the server that needs to be here.

'                            If players(sendsocket) Is Nothing Then Exit Do
'                            If frmSystray.Winsock(sendsocket).CtlState <> MSWinsockLib.StateConstants.sckConnected Then Exit Do
'                        Loop
'                    End If

'                    If Not players(sendsocket) Is Nothing Then players(sendsocket).SendingData = False
'                End If
'            End If

'            oServerConfig.BytesSent += Len(sendtext)
'            oServerConfig.PacketsSent += 1

'            'Added by Jonathan 1/18/99
'            'make sure that we release the ProcessingData
'            'flag for this socket.

'            If Not (players(sendsocket) Is Nothing) Then
'                If blnThisProcedureIsCurrent Then
'                    players(sendsocket).ProcessingData = False
'                End If
'            End If
'            Exit Sub
'        Catch ex As Exception
'            If Not (players(sendsocket) Is Nothing) Then
'                If blnThisProcedureIsCurrent Then
'                    players(sendsocket).ProcessingData = False
'                End If
'            End If
'        End Try
'    End Sub
'End Module
