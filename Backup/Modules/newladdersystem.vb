Module modLaddermodule
    Public Sub laddersub(ByVal winner As String, ByVal loser As String, ByVal winnerscore As String, ByVal loserscore As String, ByVal gameid As Integer, Optional ByVal bRankFreeze As Boolean = False, Optional ByVal blnOTT As Boolean = False, Optional ByVal strGame As String = "")
        Try
            Dim oChatFunctions As New ChatFunctions

            If winnerscore <> loserscore Then
                If bRankFreeze = False Then
                    Call playerrankings(winner, loser, strGame)
                End If
            End If

            Dim oPlayerAccount1 As New PlayerAccountDAL(winner)
            Dim oPlayerAccount2 As New PlayerAccountDAL(loser)

            If blnOTT = False Then
                If strGame = "sb" Then
                    oChatFunctions.unisend("MATCH", String.Concat(winner, " ", oPlayerAccount1.SBRank, " ", loser, " ", oPlayerAccount2.SBRank, " ", winnerscore, " ", loserscore, " 3 ", gameid))
                ElseIf strGame = "cw" Then
                    oChatFunctions.unisend("CARDWARSMATCH", String.Concat(winner, " ", oPlayerAccount1.CWRank, " ", loser, " ", oPlayerAccount2.CWRank, " ", winnerscore, " ", loserscore, " ", gameid))
                Else
                    oChatFunctions.unisend("MATCH", String.Concat(winner, " ", oPlayerAccount1.TTRank, " ", loser, " ", oPlayerAccount2.TTRank, " ", winnerscore, " ", loserscore, " 1 ", gameid))
                End If
            Else
                oChatFunctions.unisend("MATCH", String.Concat(winner, " ", oPlayerAccount1.OTTRank, " ", loser, " ", oPlayerAccount2.OTTRank, " ", winnerscore, " ", loserscore, " 2 ", gameid))
            End If

            Call setaccountdata(winner, "lastplayed", DateString)
            Call setaccountdata(loser, "lastplayed", DateString)
        Catch ex As Exception
            Call errorsub(ex.ToString, "laddersub")
        End Try
    End Sub

    Public Sub playerrankings(ByRef winner As String, ByRef loser As String, ByRef strGame As String)
        Try
            Dim modifier, loserrank, winnerrank, rankavg, intDiff As Integer

            winnerrank = CInt(getDBField("SELECT " & strGame & "_rank FROM accounts WHERE player = '" & winner & "'", strGame & "_rank"))
            loserrank = CInt(getDBField("SELECT " & strGame & "_rank FROM accounts WHERE player = '" & loser & "'", strGame & "_rank"))

            rankavg = Int((winnerrank + loserrank) / 2)
            modifier = rankavg / 10
            intDiff = (winnerrank - loserrank) / oServerConfig.RankModifier
            Dim iPointChange As Integer = Int((modifier - intDiff) / 2)

            If iPointChange < 0 Then
                'No rank change
                'winnerrank = winnerrank
                'loserrank = loserrank
            Else
                winnerrank = winnerrank + iPointChange
                loserrank = loserrank - iPointChange

                If winnerrank < 0 Then winnerrank = 0
                If loserrank < 0 Then loserrank = 0

                Call dbEXECQuery("UPDATE accounts SET " & strGame & "_rank = " & winnerrank & " WHERE player = '" & winner & "'")
                Call dbEXECQuery("UPDATE accounts SET " & strGame & "_rank = " & loserrank & " WHERE player = '" & loser & "'")
            End If
        Catch ex As Exception
            Call errorsub(ex.ToString, "playerrankings")
        End Try
    End Sub
    '    Public Function getrank(ByRef strNick As String, ByRef strGame As String) As String
    '        On Error GoTo errhandler

    '        getrank = getDBField("SELECT " & strGame & "_rank FROM accounts WHERE player = '" & strNick & "'", strGame & "_rank")
    '        Exit Function

    'errhandler:
    '        Call errorsub(Err.Description, "getrank")
    '    End Function

    Public Function giveap(ByVal strNick As String, ByRef intAmt As Integer, Optional ByVal bAA As Boolean = True) As String
        Try
            Dim oPlayerAccount As New PlayerAccountDAL(strNick)
            Dim dblAP As Double = oPlayerAccount.AP

            If intAmt > 0 And oServerConfig.EnableAP = False Then Return String.Empty

            If getAAField(strNick, "busy", "level") = "1" And bAA = True Then
                If intAmt = 1 Then
                    dblAP += 1 ' Busy bee get 2AP per minute
                Else
                    dblAP += Int(intAmt * 1.5)
                    intAmt *= 1.5
                End If
            Else
                dblAP += intAmt
            End If

            oPlayerAccount.UpdateField("ap", dblAP)
            Return dblAP.ToString
        Catch ex As Exception
            Call errorsub(ex.ToString, "giveap")
            Return String.Empty
        End Try
    End Function
End Module