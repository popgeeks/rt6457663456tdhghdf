Module PlrListControl
    Public Sub AddPlayer(ByVal qSocket As Integer)
        Try
            frmserver.Tag = Val(frmserver.Tag) + 1
            'frmserver.numbconn.Text = frmserver.Tag & " (Max: " & oServerConfig.MaxPlayers & ")"

            'System.Windows.Forms.Application.DoEvents()
        Catch ex As Exception
            Call errorsub(ex, "AddPlayer")
        End Try
    End Sub

    Public Sub RemPlayer(ByVal jNick As String, ByVal jSocket As Integer, ByVal bSkipBroadCast As Boolean)
        Try
            Dim oChatFunctions As New ChatFunctions

            frmserver.Tag = Val(frmserver.Tag) - 1

            If bSkipBroadCast = False Then
                If frmSystray.Winsock(jSocket).Tag.ToString.Trim <> String.Empty Then oChatFunctions.unisend("REMPLAYER", frmSystray.Winsock(jSocket).Tag)
            End If

            oServerConfig.DeleteUser(jSocket, jNick)

            If jNick <> String.Empty Then
                Dim oTables As New BusinessLayer.Tables

                oTables.ClosePlayerTable(Tables, jNick, True)
                AdminList.Remove(jNick)
                PlayerList.Remove(jNick)
                'RemAdmin(jNick)
                'RemPlayerList(jNick)
                oTables = Nothing
            End If

            frmSystray.Winsock(jSocket).Tag = String.Empty

            'System.Windows.Forms.Application.DoEvents()
        Catch ex As Exception
            'Call errorsub(ex, "RemPlayer")
        End Try
    End Sub

    'Public Sub RemPlayerList(ByVal jNick As String)
    '    Try
    '        For x As Integer = 0 To frmserver.lstplayers.Items.Count - 1
    '            If InStr(1, frmserver.lstplayers.Items(x).ToString.ToLower, jNick.ToLower) > 0 Then
    '                frmserver.lstplayers.Items(x) = String.Concat(x, ":")
    '                Exit For
    '            End If
    '        Next

    '        'System.Windows.Forms.Application.DoEvents()
    '    Catch ex As Exception
    '        'Call errorsub(ex, "RemPlayerList")
    '    End Try
    'End Sub

    'Public Sub RemAdmin(ByVal jNick As String)
    '    Try
    '        For x As Integer = 0 To frmserver.lstadmin.Items.Count - 1
    '            If InStr(1, frmserver.lstadmin.Items(x).ToString.ToLower, jNick.ToLower) > 0 Then
    '                frmserver.lstadmin.Items.RemoveAt(x)
    '            End If
    '        Next

    '        'System.Windows.Forms.Application.DoEvents()
    '    Catch ex As Exception
    '        'Call errorsub(ex, "RemAdmin")
    '    End Try
    'End Sub

    Public Function findtotalplayers() As Integer
        For x As Integer = 0 To (frmSystray.Winsock.Count - 1)
            If frmSystray.Winsock(x).Tag <> String.Empty And LCase(frmSystray.Winsock(x).Tag) <> "server" Then findtotalplayers += 1
        Next x

        oServerConfig.PlayersOnline = findtotalplayers
        'frmserver.lblplayers.Text = findtotalplayers.ToString
        Return findtotalplayers
    End Function
End Module