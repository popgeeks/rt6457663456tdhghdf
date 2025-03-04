Imports System.Configuration

Module quickfunctions
    Public Sub WagerRule(ByVal sWinner As String, ByVal sLoser As String, ByVal iGameID As Integer)
        Try
            Dim iTableID As Integer = 0
            Dim oTables As New BusinessLayer.Tables
            Dim oTableWagers As New DataLayer.GameFunctions.Tables(ConnectionString)

            iTableID = oTables.FindPlayersTable(Tables, sWinner)

            If Tables(iTableID).GoldWager > 0 Then
                Dim iWager As Integer = oTableWagers.TableWagers(sWinner, sLoser, Tables(iTableID).GoldWager, iGameID)

                If iWager <> 0 Then
                    Call blockchat(sWinner, "Wager", String.Format("You won {0} GP in the table wager. Congratulations!", iWager))
                    Call blockchat(sLoser, "Wager", String.Format("You lost {0} GP in the table wager.", Tables(iTableID).GoldWager))
                End If
            End If
        Catch ex As Exception
            Call errorsub(ex.ToString, "wagerrule")
        End Try
    End Sub

    Public Function gpadd(ByVal sNick As String, ByVal iAmount As Integer, Optional ByVal blnSend As Boolean = False) As Boolean
        Try
            Dim oPlayerAccount As New PlayerAccountDAL(sNick)
            Dim oChatFunctions As New ChatFunctions

            oPlayerAccount.Gold += iAmount
            oPlayerAccount.UpdateField("gold", oPlayerAccount.Gold.ToString, True)
            Call blockchat(sNick, "Gold", String.Concat("You have received ", iAmount, "GP."))

            If blnSend = True Then oPlayerAccount.SendGold()

            If oPlayerAccount.Gold < 0 Then
                'oPlayerAccount.UpdateField("gold", "0", True)

                oChatFunctions.uniwarn(String.Concat(sNick, " had less than 0 gold and bypassed all filters.  Player is auto banned."))
                Send(sNick, "EXIT You have been kicked for gold duping.  You had less than zero gold.  Send a ticket immediately and explain what you did prior to this message and the ban will be lifted.  This is a security precaution.")
                oPlayerAccount.UpdateField("banned", "1", True)
            End If
        Catch ioorex As System.IndexOutOfRangeException
            ' do nothing
        Catch ex As Exception
            Call errorsub(ex.ToString, "gpadd")
        End Try
    End Function

    Public Function EndGameGP(ByVal sPlayer As String, ByVal iOutcome As Integer) As Double
        Dim dGP As Double = 0
        Dim oPlayerAccount As New PlayerAccountDAL(sPlayer)

        Select Case iOutcome
            Case -1 ' Loss
                dGP = oServerConfig.LossRate
            Case 1 ' Win
                dGP = oServerConfig.WinRate
            Case 0 ' Draw
                dGP = oServerConfig.DrawRate
        End Select

        dGP *= oPlayerAccount.GPBonus
        Return dGP
    End Function

    Public Sub sendall(ByVal strText As String)
        For x As Integer = 0 To frmSystray.Winsock.Count - 1
            If frmSystray.Winsock(x).CtlState = MSWinsockLib.StateConstants.sckConnected And frmSystray.Winsock(x).Tag <> "" Then
                Send(String.Empty, strText, x)
                System.Windows.Forms.Application.DoEvents()
            End If
        Next x
    End Sub

    Public Sub CardPenalty(ByVal tableID As Integer, ByVal iPlayerID As Integer, ByVal sLoser As String)
        Try
            If Tables(tableID).GameID = 0 Then Exit Sub

            Dim oTT As New TTFunctions(Tables(tableID).GameID)

            If oTT.CardsTaken = False Then
                oTT.CardstoTake = oTT.FindCardsWon
                oTT.Update()
                oTT.CardPenalty(sLoser, False)
            Else
                Exit Sub
            End If
        Catch ex As Exception
            Call errorsub(ex.ToString, "CardPenalty")
        End Try
    End Sub

    Public Function totalcardsnum(ByVal strPlayer As String) As Integer
        Try
            totalcardsnum = Val(getDBField("call usp_TotalPlayerCards('" & strPlayer & "')", "totalcards"))
        Catch ex As Exception
            Call errorsub(ex.ToString, "totalcardsnum")
        End Try
    End Function

    Public Function isOnline(ByVal sPlayer As String) As Boolean
        If sPlayer = String.Empty Then Return True

        Dim oPlayerAccount As New PlayerAccountDAL(sPlayer)

        Return oPlayerAccount.Online
    End Function

    Public Function isZombie(ByVal sPlayer As String) As Boolean
        If sPlayer = String.Empty Then Return True
        Dim oZombies As DataTable = oServerConfig.ChatStuffers

        For x As Integer = 0 To oZombies.Rows.Count - 1
            If oZombies.Rows(x).Item("Player") = sPlayer Then Return True
        Next

        Return False
    End Function

    Public Sub historyadd(ByVal sNick As String, ByVal iType As Integer, ByVal sText As String)
        Dim oFunctions As New DatabaseFunctions

        oFunctions.HistoryInsert(sNick, iType.ToString, sText.Replace("'", String.Empty))
    End Sub
End Module