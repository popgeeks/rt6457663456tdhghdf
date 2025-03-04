Public Class CardWars
    Inherits CardWarsDAL

    Public Sub IncomingModule(ByVal sCommand As String, ByVal incoming As String, ByVal socket As Integer)
        Select Case sCommand
            Case "CARDWARS_PLACEAT"
                Call cardwars_placeat(incoming, socket)
            Case "CARDWARS_SURRENDER"
                Call cardwars_surrender(socket)
            Case "CARDWARS_STALEMATE"
                Call cardwars_stalemate(socket)
            Case "CARDWARS_ENDGAME"
                Call cardwars_endgame(socket)
        End Select
    End Sub

    Private Sub cardwars_endgame(ByVal socket As Integer)
        Dim oChatFunctions As New ChatFunctions

        Try
            Dim oTables As New BusinessLayer.Tables
            Dim iTableID As Integer = oTables.FindPlayersTable(Tables, frmSystray.Winsock(socket).Tag)
            Dim iGameID As Integer = Tables(iTableID).GameID

            If iGameID = 0 Or iTableID = 0 Then Throw New Exception

            Dim oCardWars As New Entities.GameObjects.CardWars
            Dim oDataLayer As New DataLayer.CardWars(ConnectionString)

            oCardWars = oDataLayer.LoadRecord(iGameID)
            oServerConfig.MySQLCalls += 1

            If oCardWars.TimeOut < Date.Now Then
                'The game has expired
                Send(String.Empty, "CARDWARSCLOSE", Tables(iTableID).Player1_Socket)
                Send(String.Empty, "CARDWARSCLOSE", Tables(iTableID).Player2_Socket)

                oDataLayer.GameTimeOut(iGameID)
                oServerConfig.MySQLCalls += 1

                Dim oTableFunctions As New BusinessLayer.Tables
                Call oTableFunctions.ClosePlayerTable(Tables, frmSystray.Winsock(socket).Tag, False)
            Else
                oChatFunctions.unisend("CHAT", String.Concat(frmSystray.Winsock(socket).Tag, " !endgame [End Game]"))
                Call blockchat(String.Empty, "Request", "An endgame request has been sent to the administrators that are online as 4 minutes have not yet elapsed.", socket)
            End If
        Catch ex As Exception
            oChatFunctions.uniwarn(String.Concat(frmSystray.Winsock(socket).Tag, " sent an invalid stalemate request."))
            Send(String.Empty, "CARDWARSCLOSE", socket)
        End Try
    End Sub

    Private Sub cardwars_stalemate(ByVal socket As Integer)
        Try
            Dim oTables As New BusinessLayer.Tables
            Dim oDataLayer As New DataLayer.CardWars(ConnectionString)

            Dim iTableID As Integer = oTables.FindPlayersTable(Tables, frmSystray.Winsock(socket).Tag)
            Dim iGameID As Integer = Tables(iTableID).GameID
            Dim iPlayerID As Integer = 0

            If iGameID = 0 Or iTableID = 0 Then Throw New Exception

            If Tables(iTableID).Player1 = frmSystray.Winsock(socket).Tag Then
                iPlayerID = 1
                Tables(iTableID).Stalemate1 = True
            ElseIf Tables(iTableID).Player2 = frmSystray.Winsock(socket).Tag Then
                iPlayerID = 2
                Tables(iTableID).Stalemate2 = True
            Else
                Throw New Exception
            End If

            If Tables(iTableID).Stalemate1 = True And Tables(iTableID).Stalemate2 = True Then
                oDataLayer.Stalemate(iGameID)
                oServerConfig.MySQLCalls += 1

                Send(String.Empty, "CARDWARSCLOSE", Tables(iTableID).Player1_Socket)
                Send(String.Empty, "CARDWARSCLOSE", Tables(iTableID).Player2_Socket)

                Dim oTableFunctions As New BusinessLayer.Tables
                Call oTableFunctions.ClosePlayerTable(Tables, frmSystray.Winsock(socket).Tag, False)
            Else
                Send(String.Empty, "GAMESTALEMATE", IIf(iPlayerID = 1, Tables(iTableID).Player2_Socket, Tables(iTableID).Player1_Socket))
            End If
        Catch ex As Exception
            Dim oChatFunctions As New ChatFunctions
            oChatFunctions.uniwarn(String.Concat(frmSystray.Winsock(socket).Tag, " sent an invalid stalemate request."))
            Send(String.Empty, "CARDWARSCLOSE", socket)
        End Try
    End Sub

    Private Sub cardwars_surrender(ByVal socket As Integer)
        Try
            Dim oPlayerAccount As New PlayerAccountDAL(frmSystray.Winsock(socket).Tag)

            If oPlayerAccount.Gold < 100 Then
                Call blockchat(frmSystray.Winsock(socket).Tag, "Surrender", "You do not have enough gold to surrender. Surrenders cost 100GP.")
                Exit Sub
            End If

            Dim oTables As New BusinessLayer.Tables
            Dim iTableID As Integer = oTables.FindPlayersTable(Tables, oPlayerAccount.Player)
            Dim iGameID As Integer = Tables(iTableID).GameID

            If iGameID = 0 Or iTableID = 0 Then Throw New Exception

            Dim oCardWars As New Entities.GameObjects.CardWars
            Dim oDataLayer As New DataLayer.CardWars(ConnectionString)

            oDataLayer.Surrender(iGameID, oPlayerAccount.Player)
            oServerConfig.MySQLCalls += 1

            oCardWars = oDataLayer.LoadRecord(iGameID)
            oServerConfig.MySQLCalls += 1

            Send(String.Empty, "CARDWARSCLOSE", Tables(iTableID).Player1_Socket)
            Send(String.Empty, "CARDWARSCLOSE", Tables(iTableID).Player2_Socket)

            Dim oTableFunctions As New BusinessLayer.Tables
            Call oTableFunctions.ClosePlayerTable(Tables, oPlayerAccount.Player, False)
        Catch ex As Exception
            Dim oChatFunctions As New ChatFunctions
            oChatFunctions.uniwarn(String.Concat(frmSystray.Winsock(socket).Tag, " sent an invalid surrender request."))
            Send(String.Empty, "CARDWARSCLOSE", socket)
        End Try
    End Sub

    Public Sub cardwars_placeat(ByVal incoming As String, ByVal socket As Integer)
        Dim lostvar As String = String.Empty, sIndex As String = String.Empty

        Divide(incoming, " ", lostvar, sIndex)

        Dim sPlayer As String = frmSystray.Winsock(socket).Tag
        Dim iIndex As Integer = Integer.Parse(sIndex)
        Dim oTables As New BusinessLayer.Tables
        Dim iTableID As Integer = oTables.FindPlayersTable(Tables, sPlayer)

        Dim oDataLayer As New DataLayer.CardWars(ConnectionString)

        If Tables(iTableID).Player1 = sPlayer Then
            Tables(iTableID).CardWarsObject.Player1_Card = Tables(iTableID).CardWarsObject.Player1Hand(iIndex)
        ElseIf Tables(iTableID).Player2 = sPlayer Then
            Tables(iTableID).CardWarsObject.Player2_Card = Tables(iTableID).CardWarsObject.Player2Hand(iIndex)
        End If

        If Tables(iTableID).CardWarsObject.Player1_Card <> String.Empty And _
            Tables(iTableID).CardWarsObject.Player2_Card <> String.Empty Then

            SendCardWarsPhase(Tables(iTableID), Tables(iTableID).CardWarsObject.Player1_Card, Tables(iTableID).CardWarsObject.Player2_Card)

            oDataLayer.AnalyzeRound(Tables(iTableID).GameID, Tables(iTableID).CardWarsObject)
            oServerConfig.MySQLCalls += 1

            SendHistory(Tables(iTableID), Tables(iTableID).CardWarsObject)

            Dim oCardWars As New Entities.GameObjects.CardWars
            oCardWars = oDataLayer.LoadRecord(Tables(iTableID).GameID)

            Tables(iTableID).CardWarsObject = oCardWars

            If oCardWars.Player1_HP <= 0 Then
                Call GameOver(Tables(iTableID), oCardWars.Player2, oCardWars.Player1, oCardWars.Player2_HP, oCardWars.Player1_HP, oCardWars.GameID, False, False)
            ElseIf oCardWars.Player2_HP <= 0 Then
                Call GameOver(Tables(iTableID), oCardWars.Player1, oCardWars.Player2, oCardWars.Player1_HP, oCardWars.Player2_HP, oCardWars.GameID, False, False)
            Else
                SendPlayerHand(Tables(iTableID), oCardWars, 1)
                SendPlayerHand(Tables(iTableID), oCardWars, 2)

                SendCardWarsBoard(Tables(iTableID), oCardWars)
            End If
        End If
    End Sub

    Private Sub GameOver(ByVal oTable As Entities.BusinessObjects.Tables, ByVal sWinner As String, ByVal sLoser As String, ByVal iWinningScore As Integer, ByVal iLosingScore As Integer, ByVal iGameID As Integer, ByVal bSurrender As Boolean, ByVal bDraw As Boolean)
        Dim oTableFunctions As New BusinessLayer.Tables
        Dim iTableID As Integer = oTableFunctions.FindPlayersTable(Tables, sWinner)

        Try
            Dim oGameFunctions As New GameFunctions
            Dim oStats As New Statistics
            Dim iWinner_Gold As Integer, iLoser_Gold As Integer
            Dim oDataLayer As New DataLayer.CardWars(ConnectionString)

            If bSurrender = True Then
                Call blockchat(String.Empty, "Surrender", String.Concat(sLoser, " surrendered and lost."), oTable.Player1_Socket)
                Call blockchat(String.Empty, "Surrender", String.Concat(sLoser, " surrendered and lost."), oTable.Player2_Socket)
            End If

            If bDraw = False Then
                Call laddersub(sWinner, sLoser, iWinningScore.ToString, iLosingScore.ToString, iGameID, False, False, "cw")

                If bSurrender = False Then
                    iLoser_Gold = EndGameGP(sLoser, -1)
                    iWinner_Gold = EndGameGP(sWinner, 1)
                End If

                Call WagerRule(sWinner, sLoser, iGameID)

                oStats.CompileStats(sWinner, sLoser, "cw", False)
            Else
                Call oGameFunctions.drawevent(sWinner, sLoser, iGameID, False, "cw", 120, Nothing)
                oStats.CompileStats(sWinner, sLoser, "cw", True)
            End If

            If bDraw = False Then
                Call gpadd(sWinner, iWinner_Gold, True)
                Call gpadd(sLoser, iLoser_Gold, True)

                Call oGameFunctions.GiveExp(sWinner, iWinningScore, sLoser, iLosingScore, iGameID, Nothing, False, "cw")
            End If

            Send(String.Empty, "CARDWARSCLOSE", oTable.Player1_Socket)
            Send(String.Empty, "CARDWARSCLOSE", oTable.Player2_Socket)

            oDataLayer.EndGame(iGameID)
            oServerConfig.MySQLCalls += 1
        Catch ex As Exception
            Dim oError As New ErrorLogging.ErrorHandling()
            oError.LogErrorToEvents(ex.ToString, "GameOver")
        Finally
            Call oTableFunctions.ClosePlayerTable(Tables, iTableID)
            oTableFunctions.UpdateTable(Tables, iTableID)
        End Try
    End Sub

    Public Function cardwarsyes(ByVal incoming As String, ByVal socket As Integer) As String

        Dim opp As String = String.Empty, challengeyesnick As String = String.Empty, lostvar As String = String.Empty
        Dim gameid As Integer, oppsocket As Integer

        Try
            Divide(incoming, " ", lostvar, challengeyesnick)
            cardwarsyes = String.Empty

            If challengeyesnick = String.Empty Then
                Call blockchat(frmSystray.Winsock(socket).Tag, "Error", "Server could not process your challenge")
                Exit Function
            End If

            opp = frmSystray.Winsock(socket).Tag
            oppsocket = GetSocket(challengeyesnick)

            Dim oGameFunctions As New GameFunctions
            gameid = oGameFunctions.NewGameID()

            Dim gamestarter As Integer = oGameFunctions.PickStarter(False)

            Call InsertGame(gameid, challengeyesnick, opp, gamestarter)

            Dim oCardWars As New Entities.GameObjects.CardWars
            Dim oDataLayer As New DataLayer.CardWars(ConnectionString)
            Dim oTables As New BusinessLayer.Tables
            Dim iTableID As Integer = oTables.FindPlayersTable(Tables, challengeyesnick)

            oCardWars = oDataLayer.LoadRecord(gameid)
            oServerConfig.MySQLCalls += 1

            Tables(iTableID).Player1_Socket = oppsocket
            Tables(iTableID).Player2_Socket = socket
            Tables(iTableID).CardWarsObject = oCardWars

            Send(String.Empty, String.Concat("CARDWARSLOAD 1"), oppsocket)
            Send(String.Empty, String.Concat("CARDWARSLOAD 2"), socket)

            SendPlayerHand(Tables(iTableID), oCardWars, 1)
            SendPlayerHand(Tables(iTableID), oCardWars, 2)

            SendCardWarsBoard(Tables(iTableID), oCardWars)

            Return gameid.ToString
        Catch ex As Exception
            Dim oError As New ErrorLogging.ErrorHandling()
            oError.LogErrorToEvents(ex.ToString, "cardwarsyes")
            Return String.Empty
        End Try
    End Function

    Private Sub SendCardWarsBoard(ByVal oTable As Entities.BusinessObjects.Tables, ByVal oCardWars As Entities.GameObjects.CardWars)
        Send(String.Empty, String.Format("CARDWARSBOARD {0} {1} {2} {3} {4} {5} {6} {7} {8}", _
             oCardWars.Offense, oCardWars.Defense, oCardWars.ChainPlayer, oCardWars.ChainCount, oCardWars.ElementField, oCardWars.Player1_HP, oCardWars.Player2_HP, oCardWars.PlayerTurn, oCardWars.Rounds), oTable.Player1_Socket)

        Send(String.Empty, String.Format("CARDWARSBOARD {0} {1} {2} {3} {4} {5} {6} {7} {8}", _
            oCardWars.Offense, oCardWars.Defense, oCardWars.ChainPlayer, oCardWars.ChainCount, oCardWars.ElementField, oCardWars.Player1_HP, oCardWars.Player2_HP, oCardWars.PlayerTurn, oCardWars.Rounds), oTable.Player2_Socket)
    End Sub

    Private Sub SendPlayerHand(ByVal oTable As Entities.BusinessObjects.Tables, ByVal oCardWars As Entities.GameObjects.CardWars, ByVal iPlayerID As Integer)
        If iPlayerID = 1 Then
            Send(String.Empty, String.Format("CARDWARSHAND {0} {1} {2} {3} {4} {5}", 1, _
                oCardWars.Player1Hand.Item(0).Replace(" ", "%20"), oCardWars.Player1Hand.Item(1).Replace(" ", "%20"), _
                oCardWars.Player1Hand.Item(2).Replace(" ", "%20"), oCardWars.Player1Hand.Item(3).Replace(" ", "%20"), _
                oCardWars.Player1Hand.Item(4).Replace(" ", "%20")), oTable.Player1_Socket)
        ElseIf iPlayerID = 2 Then
            Send(String.Empty, String.Format("CARDWARSHAND {0} {1} {2} {3} {4} {5}", 2, _
                oCardWars.Player2Hand.Item(0).Replace(" ", "%20"), oCardWars.Player2Hand.Item(1).Replace(" ", "%20"), _
                oCardWars.Player2Hand.Item(2).Replace(" ", "%20"), oCardWars.Player2Hand.Item(3).Replace(" ", "%20"), _
                oCardWars.Player2Hand.Item(4).Replace(" ", "%20")), oTable.Player2_Socket)
        End If
    End Sub

    Private Sub SendHistory(ByVal oTable As Entities.BusinessObjects.Tables, ByVal oCardWars As Entities.GameObjects.CardWars)
        Try
            Dim oDataLayer As New DataLayer.CardWars(ConnectionString)
            Dim oDataTable As DataTable = oDataLayer.GetRoundHistory(oCardWars.GameID, oCardWars.Rounds)

            oServerConfig.MySQLCalls += 1

            For Each oDataRow As DataRow In oDataTable.Rows
                Send(String.Empty, String.Format("CARDWARSHISTORY {0}", oDataRow.Item("Results").ToString.Replace(" ", "%20")), oTable.Player1_Socket)
                Send(String.Empty, String.Format("CARDWARSHISTORY {0}", oDataRow.Item("Results").ToString.Replace(" ", "%20")), oTable.Player2_Socket)
            Next
        Catch ex As Exception

        End Try
    End Sub

    Private Sub SendCardWarsPhase(ByVal oTable As Entities.BusinessObjects.Tables, ByVal sBlueCard As String, ByVal sRedCard As String)
        Try
            Send(String.Empty, String.Format("CARDWARSPHASE {0} {1}", sBlueCard.Replace(" ", "%20"), sRedCard.Replace(" ", "%20")), oTable.Player1_Socket)
            Send(String.Empty, String.Format("CARDWARSPHASE {0} {1}", sBlueCard.Replace(" ", "%20"), sRedCard.Replace(" ", "%20")), oTable.Player2_Socket)
        Catch ex As Exception

        End Try
    End Sub
End Class
