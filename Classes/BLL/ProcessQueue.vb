Public Class ProcessQueue
    Inherits ProcessQueueDAL

    Public Sub IncomingModule(ByVal sCommand As String, ByVal sIncoming As String)
        Try
            Select Case sCommand
                Case "PQNOTICE"
                    Call PQNotice(sIncoming)
                Case "PQNOTIFY"
                    Call pqnotify(sIncoming)
                Case "PQNOTIFYALL"
                    Call PQNotifyAll(sIncoming)
                Case "PQADMINCHAT"
                    Call PQAdminChat(sIncoming)
                Case "PQENGINECHAT"
                    Call PQEngineChat(sIncoming)
                Case "PQADMINWARN"
                    Call PQAdminWarning(sIncoming)
                Case "PQUPDATETOKENS"
                    Call PQUpdateTokens(sIncoming)
                Case "PQADDZOMBIE"
                    Call PQAddZombie(sIncoming)
                Case "PQREMOVEZOMBIE"
                    Call PQRemoveZombie(sIncoming)
                Case "PQBAN"
                    Call PQBan(sIncoming)
                Case "PQGUILDTAG"
                    Call PQGuildTag(sIncoming)
                Case "PQUPDATEPLAYER"
                    Call PQGuildTag(sIncoming)
                Case "PQMATCH"
                    Call PQMatch(sIncoming)
            End Select
        Catch ex As Exception
            Call errorsub(ex, "ProcessQueue.IncomingModule")
        End Try
    End Sub

    Private Sub PQGuildTag(ByVal incoming As String)
        Try
            Dim sItems() As String = incoming.Split(" ")
            Dim oChatFunctions As New ChatFunctions

            If sItems(1) <> String.Empty Then
                Dim oPlayerAccount As New PlayerAccountDAL(sItems(1))
                Call sendall(String.Concat("UPDATEPLAYER ", oPlayerAccount.Player, " ", oPlayerAccount.Surname.Replace(" ", "%20"), " ", oPlayerAccount.Guild.Replace(" ", "%20")))
            End If
        Catch ex As Exception
            Call errorsub(ex, "PQGuildTag")
        End Try
    End Sub

    ''' <summary>
    ''' sItems(1) = Text
    ''' 
    ''' </summary>
    ''' <param name="incoming"></param>
    ''' <remarks></remarks>
    Private Sub PQNotice(ByVal incoming As String)
        Try
            Dim sItems() As String = incoming.Split(" ")
            Dim oChatFunctions As New ChatFunctions

            If sItems(1) <> String.Empty Then
                oChatFunctions.unisend("NOTICE", sItems(1))
            End If
        Catch ex As Exception
            Call errorsub(ex, "PQNotice")
        End Try
    End Sub

    ''' <summary>
    ''' sItems(1) = Player
    ''' sItems(2) = Block
    ''' sItems(3) = Text
    ''' </summary>
    ''' <param name="incoming"></param>
    ''' <remarks></remarks>
    ''' 
    Private Sub PQNotify(ByVal incoming As String)
        Try
            Dim sItems() As String = incoming.Split(" ")

            If sItems(1) <> String.Empty And sItems(2) <> String.Empty And sItems(3) <> String.Empty Then
                Call blockchat(sItems(1), sItems(2), sItems(3))
            End If
        Catch ex As Exception
            Call errorsub(ex, "PQNotify")
        End Try
    End Sub

    ''' <summary>
    ''' sItems(1) = Winner
    ''' sItems(2) = Winner Rank
    ''' sItems(3) = Loser
    ''' sItems(4) = Loser Rank
    ''' sItems(5) = Winner Score
    ''' sItems(6) = Loser Score
    ''' sItems(7) = Game ID (6 for Chinchirorin)
    ''' sItems(8) = Game Replay ID
    ''' </summary>
    ''' <param name="incoming"></param>
    ''' <remarks></remarks>
    ''' 
    Private Sub PQMatch(ByVal incoming As String)
        Try
            Dim sItems() As String = incoming.Split(" ")

            If sItems(7) <> "7" Then
                Call sendall(String.Format("MATCH {0} {1} {2} {3} {4} {5} {6} {7}", sItems(1), sItems(2), sItems(3), sItems(4), sItems(5), sItems(6), sItems(7), sItems(8)))
            Else
                Call sendall(String.Format("CARDWARSMATCH {0} {1} {2} {3} {4} {5} {6}", sItems(1), sItems(2), sItems(3), sItems(4), sItems(5), sItems(6), sItems(7)))
            End If
        Catch ex As Exception
            Call errorsub(ex, "PQMatch")
        End Try
    End Sub

    ''' <summary>
    ''' sItems(1) = Block
    ''' sitems(2) = Text
    ''' </summary>
    ''' <param name="incoming"></param>
    ''' <remarks></remarks>
    Private Sub PQNotifyAll(ByVal incoming As String)
        Try
            Dim sItems() As String = incoming.Split(" ")
            Dim oChatFunctions As New ChatFunctions
            Dim oChatLog As New ChatLog

            If sItems(1) = "Login" Then
                Dim oPlayerAccount As New PlayerAccountDAL(sItems(3))
                If isOnline(sItems(3)) = False Then
                    oChatFunctions.unisend("NEWPLAYER", oPlayerAccount.AdminLevel & " " & oPlayerAccount.Country.Replace(" ", "%20") & " " & oPlayerAccount.AchievementScore & " " & oPlayerAccount.Membership.Replace(" ", "%20") & " " & String.Concat(oPlayerAccount.Player, " ", oPlayerAccount.Surname.Replace(" ", "%20"), " ", (IIf(oPlayerAccount.Guild <> String.Empty, "<" & oPlayerAccount.Guild & ">", String.Empty)).Replace(" ", "%20")).Replace(" ", "%20"))

                    If sItems(1) <> String.Empty And sItems(2) <> String.Empty Then
                        oChatFunctions.unisend("CHATBLOCK", String.Concat(sItems(1), " ", sItems(2)))
                    End If
                End If

                oChatLog.InsertChat("System", String.Format("Player Logged In: {0}", sItems(3)))
            ElseIf sItems(1) = "Logoff" Then
                If isOnline(sItems(3)) = False Then
                    oChatFunctions.unisend("REMPLAYER", sItems(3))

                    If sItems(1) <> String.Empty And sItems(2) <> String.Empty Then
                        oChatFunctions.unisend("CHATBLOCK", String.Concat(sItems(1), " ", sItems(2)))
                    End If
                End If

                oChatLog.InsertChat("System", String.Format("Player Logged Off: {0}", sItems(3)))
            Else
                If sItems(1) <> String.Empty And sItems(2) <> String.Empty Then
                    oChatFunctions.unisend("CHATBLOCK", String.Concat(sItems(1), " ", sItems(2)))
                    oChatLog.InsertChat("System", String.Format("<{0}> {1}", sItems(1), sItems(2)))
                End If
            End If
        Catch ex As Exception
            Call errorsub(ex, "PQNotifyAll")
        End Try
    End Sub

    ''' <summary>
    ''' sItems(1) = Text
    ''' </summary>
    ''' <param name="incoming"></param>
    ''' <remarks></remarks>
    Private Sub PQAdminChat(ByVal incoming As String)
        Try
            Dim sItems() As String = incoming.Split(" ")
            Dim oChatFunctions As New ChatFunctions

            If sItems(1) <> String.Empty Then
                Dim adminname As String = String.Empty
                Dim oDataLayer As New DataLayer.AdminFunctions(ConnectionString)
                Dim oDataTable As DataTable = oDataLayer.OnlineAdmins

                oServerConfig.MySQLCalls += 1

                If Not (oDataTable Is Nothing) Then
                    For x As Integer = 0 To oDataTable.Rows.Count - 1
                        Send(String.Empty, String.Concat("ADMINCHAT Server ", sItems(1).Replace(" ", "%20")), oDataTable.Rows(x).Item("id"))
                    Next x
                End If
            End If
        Catch ex As Exception
            Call errorsub(ex, "PQAdminChat")
        End Try
    End Sub

    Private Sub PQEngineChat(ByVal incoming As String)
        Try
            Dim sItems() As String = incoming.Split(" ")
            Dim oChatFunctions As New ChatFunctions

            Dim playerName As String = sItems(1).Replace(" ", "%20")
            Dim chatText As String = sItems(2).Replace(" ", "%20")

            If sItems(1) <> String.Empty Then
                oChatFunctions.unisend(String.Format("CHAT {0} ", playerName), chatText)
            End If

            oServerConfig.MySQLCalls += 1
        Catch ex As Exception
            Call errorsub(ex, "PQAdminChat")
        End Try
    End Sub

    Private Sub PQAdminWarning(ByVal incoming As String)
        Try
            Dim sItems() As String = incoming.Split(" ")
            Dim oChatFunctions As New ChatFunctions

            If sItems(1) <> String.Empty Then oChatFunctions.uniwarn(sItems(1))
        Catch ex As Exception
            Call errorsub(ex, "PQAdminWarning")
        End Try
    End Sub

    Private Sub PQBan(ByVal incoming As String)
        Try
            Dim sItems() As String = incoming.Split(" ")

            If sItems(1) <> String.Empty Then
                Send(sItems(1), "EXIT You have been kicked for: Infractions Exceeded Limit")
            End If
        Catch ex As Exception
            Call errorsub(ex, "PQAdminWarning")
        End Try
    End Sub

    ''' <summary>
    ''' sItems(1) = Name
    ''' 
    ''' </summary>
    ''' <param name="incoming"></param>
    ''' <remarks></remarks>
    Private Sub PQUpdateTokens(ByVal incoming As String)
        Try
            Dim sItems() As String = incoming.Split(" ")
            Dim oChatFunctions As New ChatFunctions
            Dim oPlayerAccount As New PlayerAccountDAL(sItems(1))

            If oPlayerAccount.Online = True Then Send(String.Empty, String.Concat("UPDATETOKENS ", oPlayerAccount.Tokens), oPlayerAccount.PlayerSocket)
        Catch ex As Exception
            Call errorsub(ex, "PQUpdateTokens")
        End Try
    End Sub

    ''' <summary>
    ''' sItems(1) = Name
    ''' 
    ''' </summary>
    ''' <param name="incoming"></param>
    ''' <remarks></remarks>
    Private Sub PQAddZombie(ByVal incoming As String)
        Try
            Dim sItems() As String = incoming.Split(" ")
            Dim oChatFunctions As New ChatFunctions
            Dim oPlayerAccount As New PlayerAccountDAL(sItems(1))

            'Send(String.Empty, "NEWPLAYER " & oPlayerAccount.AdminLevel & " " & oPlayerAccount.Country.Replace(" ", "%20") & " " & oPlayerAccount.Membership & " " & String.Concat(oPlayerAccount.Player, " ", oPlayerAccount.Surname.Replace(" ", "%20"), " ", (IIf(oPlayerAccount.Guild <> String.Empty, "<" & oPlayerAccount.Guild & ">", String.Empty)).Replace(" ", "%20")).Replace(" ", "%20"), iID)
            oChatFunctions.unisend("NEWPLAYER", oPlayerAccount.AdminLevel & " " & oPlayerAccount.Country.Replace(" ", "%20") & " " & oPlayerAccount.Membership & " " & String.Concat(oPlayerAccount.Player, " ", oPlayerAccount.Surname.Replace(" ", "%20"), " ", (IIf(oPlayerAccount.Guild <> String.Empty, "<" & oPlayerAccount.Guild & ">", String.Empty)).Replace(" ", "%20")).Replace(" ", "%20"))
            'If oPlayerAccount.Online = True Then Send(String.Empty, String.Concat("UPDATETOKENS ", oPlayerAccount.Tokens), oPlayerAccount.PlayerSocket)
        Catch ex As Exception
            Call errorsub(ex, "PQAddZombie")
        End Try
    End Sub

    ''' <summary>
    ''' sItems(1) = Name
    ''' 
    ''' </summary>
    ''' <param name="incoming"></param>
    ''' <remarks></remarks>
    Private Sub PQRemoveZombie(ByVal incoming As String)
        Try
            Dim sItems() As String = incoming.Split(" ")
            Dim oChatFunctions As New ChatFunctions
            '            Dim oPlayerAccount As New PlayerAccountDAL(sItems(1))

            'Send(String.Empty, "NEWPLAYER " & oPlayerAccount.AdminLevel & " " & oPlayerAccount.Country.Replace(" ", "%20") & " " & oPlayerAccount.Membership & " " & String.Concat(oPlayerAccount.Player, " ", oPlayerAccount.Surname.Replace(" ", "%20"), " ", (IIf(oPlayerAccount.Guild <> String.Empty, "<" & oPlayerAccount.Guild & ">", String.Empty)).Replace(" ", "%20")).Replace(" ", "%20"), iID)
            oChatFunctions.unisend("REMPLAYER", sItems(1))
            'If oPlayerAccount.Online = True Then Send(String.Empty, String.Concat("UPDATETOKENS ", oPlayerAccount.Tokens), oPlayerAccount.PlayerSocket)
        Catch ex As Exception
            Call errorsub(ex, "PQRemoveZombie")
        End Try
    End Sub
End Class
