Module modPlayerControl
    Public Sub quit(ByVal incoming As String, ByVal socket As Integer, ByVal bHardQuit As Boolean)
        Try
            Dim lostvar As String = String.Empty
            Dim strCheck As String = String.Empty

            Dim oTables As New BusinessLayer.Tables
            Dim oChatFunctions As New ChatFunctions

            Divide(incoming, " ", lostvar, lostvar, strCheck)

            If frmSystray.Winsock(socket).Tag = String.Empty Or socket = 0 Then Exit Sub

            Dim sNick As String = frmSystray.Winsock(socket).Tag
            Dim itableID As Integer = oTables.FindPlayersTable(Tables, sNick)

            If strCheck = "1" Then
                If itableID > 0 Then
                    Send(String.Empty, "QUITNOTOKAY", socket)
                    Exit Sub
                Else
                    Send(String.Empty, "QUITOKAY", socket)
                End If
            End If

            'frmserver.lstplayers.Items.Item(socket) = String.Concat(socket, ": ")

            If itableID > 0 Then
                Call historyadd(frmSystray.Winsock(socket).Tag, 1, frmSystray.Winsock(socket).Tag & " quit gameID #" & tables(itableID).gameid & " on " & DateString & " " & TimeString)

                oChatFunctions.uniwarn(String.Concat(frmSystray.Winsock(socket).Tag, " quit gameID #", tables(itableID).gameid, " on ", DateString, " ", TimeString))

                Dim oTT As New TTFunctions(tables(itableID).gameid)

                If oTT.ErrorFlag = False And oTT.IsLoaded = True Then
                    Dim iPlayerID As Integer = oTT.PlayerID(sNick)

                    If bHardQuit = True Then CardPenalty(itableID, iPlayerID, socket)

                    'Call displaybox(oTT.Player1, oTT.Player2, oTT.GameID, IIf(iPlayerID = 1, 2, 1))
                Else
                    Dim objTableFunctions As New TableFunctions
                    objTableFunctions.EndMatch(String.Concat("ENDMATCH ", sNick), 0, False, True)
                End If
            End If

            If itableID <> 0 Then
                If tables(itableID).gameid = 0 Then
                    Select Case tables(itableID).comment
                        Case "Team Omni Triple Triad"
                            Dim oTeamOTT As New TeamOTTFunctions
                            Call oTeamOTT.teamclose(socket)
                            oTeamOTT = Nothing
                        Case Else
                            Call oTables.PreGameTableClose(Tables, socket)
                    End Select
                End If
            End If

            If getAccount_Field(frmSystray.Winsock(socket).Tag, "adminstatus") = "on" Then _
                Call setaccountdata(frmSystray.Winsock(socket).Tag, "adminstatus", "off")

            If frmSystray.Winsock(socket).CtlState <> MSWinsockLib.StateConstants.sckClosed Then frmSystray.Winsock(socket).Close()

            RemovePlayerClass(socket) ' <<-- added by Jonathan 1/18/00

            RemPlayer(frmSystray.Winsock(socket).Tag, socket, False)
            'System.Windows.Forms.Application.DoEvents()

            If frmSystray.Winsock(socket).Tag <> String.Empty Then RemPlayer(frmSystray.Winsock(socket).Tag, socket, False)

            frmSystray.Winsock(socket).Tag = String.Empty
            frmSystray.Winsock(socket).Close()

            Call historyadd(sNick, 2, String.Concat(sNick, " logged off."))
        Catch ex As Exception
            Call errorsub(ex, "quit")
        End Try
    End Sub
End Module
