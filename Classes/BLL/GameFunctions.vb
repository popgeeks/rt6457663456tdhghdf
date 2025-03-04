Imports mySQL.Data.MySqlClient
Imports MySQLFactory
Imports System.ComponentModel
Imports System.Configuration

Public Class GameFunctions
    Private bError As Boolean
    Private oError As Exception

    Public Sub KillGame(ByVal iP1Socket As Integer, ByVal iP2Socket As Integer, ByVal sNick As String, ByVal sReason As String)
        Try
            Dim oTableFunctions As New TableFunctions
            Dim oChatFunctions As New ChatFunctions
            Dim oDatabasefunctions As New DatabaseFunctions

            Call blockchat(String.Empty, "Game Ended", sReason, iP1Socket)
            Call blockchat(String.Empty, "Game Ended", sReason, iP2Socket)
            oTableFunctions.EndMatch("ENDMATCH " & sNick, 0, False, True)
        Catch ex As Exception
            Call errorsub(ex, "killgame")
        End Try
    End Sub

    Public Sub EndGameEvent(ByVal oTable As Entities.BusinessObjects.Tables)
        Call Jackpots(oTable)
    End Sub

    Private Sub Jackpots(ByVal oTable As Entities.BusinessObjects.Tables)
        If oTable.Player1 <> String.Empty And oTable.Player1_Jackpot = True Then
            Call JackpotParticipation(oTable.Player1)
        End If

        If oTable.Player2 <> String.Empty And oTable.Player2_Jackpot = True Then
            Call JackpotParticipation(oTable.Player2)
        End If

        If oTable.Player3 <> String.Empty And oTable.Player3_Jackpot = True Then
            Call JackpotParticipation(oTable.Player3)
        End If

        If oTable.Player4 <> String.Empty And oTable.Player4_Jackpot = True Then
            Call JackpotParticipation(oTable.Player4)
        End If
    End Sub

    Private Sub JackpotParticipation(ByVal sPlayer As String)
        Dim oDAL As New Jackpots
        Dim sCSM As String = oDAL.JackpotTicket(sPlayer)

        If sCSM <> String.Empty Then
            Call blockchat(sPlayer, "Ticket", sCSM)
        End If
    End Sub

    Public Sub GiveExp(ByVal winner As String, ByVal winnerscore As Integer, ByVal loser As String, ByVal loserscore As Integer, ByVal gameid As Integer, Optional ByVal oTT As TTFunctions = Nothing, Optional ByVal blnOTT As Boolean = False, Optional ByVal strGame As String = "")
        Try
            Dim exp, exploser As Integer
            Dim oGameFunctions As New GameFunctions

            If blnOTT = False Then
                If strGame = "sb" Then
                    exp = oServerConfig.SphereWinExp
                    exploser = oServerConfig.SphereLossExp
                ElseIf strGame = "ttm" Then
                    exp = oServerConfig.MemoryWinExp
                    exploser = oServerConfig.MemoryLossExp
                ElseIf strGame = "cw" Then
                    exp = oServerConfig.CardWarsWinExperience
                    exploser = oServerConfig.CardWarsLossExperience
                ElseIf strGame = "6" Then
                    exp = 3
                    exploser = 3
                Else
                    exp = (winnerscore - loserscore) * oServerConfig.TripleTriadExperience
                    exploser = (winnerscore - loserscore) * oServerConfig.LoserExp
                End If
            Else
                If blnOTT = True Then
                    exp = 30 + ((winnerscore - loserscore) * 2)
                    exploser = 21 + (loserscore - winnerscore)
                End If
            End If

            If exp = 0 Then exp = 10
            If exploser = 0 Then exploser = 10

            If winnerscore = 0 And loserscore = 0 And strGame = "sb" Then
                exp = 0
                exploser = 0
            ElseIf winnerscore <= 5 And loserscore <= 5 And strGame = "sb" Then
                exp = 5
                exploser = 1
            End If

            Dim oWinner As New PlayerAccountDAL(winner)
            Dim oLoser As New PlayerAccountDAL(loser)

            If oWinner.Level - oLoser.Level <= -10 Then exp = Int(exp * 1.25)

            Dim intplayers As Integer = findtotalplayers()

            If intplayers >= 200 Then
                exp = Int(exp * 1.5)
                exploser = Int(exploser * 1.5)
            ElseIf intplayers >= 100 Then
                exp = Int(exp * 1.3)
                exploser = Int(exploser * 1.3)
            ElseIf intplayers >= 75 Then
                exp = Int(exp * 1.2)
                exploser = Int(exploser * 1.2)
            ElseIf intplayers >= 65 Then
                exp = Int(exp * 1.15)
                exploser = Int(exploser * 1.15)
            ElseIf intplayers >= 50 Then
                exp = Int(exp * 1.1)
                exploser = Int(exploser * 1.1)
            ElseIf intplayers >= 40 Then
                exp = Int(exp * 1.05)
                exploser = Int(exploser * 1.05)
            End If

            exp = exp * oWinner.EXPBonus
            exploser = exploser * oLoser.EXPBonus

            'If oWinner.StaffBonus = True Then exp = Int(exp * 1.05)
            'If oLoser.StaffBonus = True Then exploser = Int(exploser * 1.05)

            oWinner.UpdateExp(exp)
            oLoser.UpdateExp(exploser)

            If oWinner.Experience >= oWinner.NextLevel Then
                oWinner.UpdateField("level", (oWinner.Level + 1).ToString, True)
                oWinner.Reload()

                Send(String.Empty, String.Concat("LEVELUP ", oWinner.Level, " ", oWinner.Experience, " ", oWinner.NextLevel), oWinner.PlayerSocket)
                Call oGameFunctions.LevelUp(winner, oWinner.Level)
            End If

            If oLoser.Experience >= oLoser.NextLevel Then
                oLoser.UpdateField("level", (oLoser.Level + 1).ToString, True)
                oLoser.Reload()

                Send(String.Empty, String.Concat("LEVELUP ", oLoser.Level, " ", oLoser.Experience, " ", oLoser.NextLevel), oLoser.PlayerSocket)
                Call oGameFunctions.LevelUp(loser, oLoser.Level)
            End If

            Send(String.Empty, String.Concat("UPDATEEXP ", exp, " ", oWinner.Experience, " ", oWinner.Level, " ", oWinner.NextLevel), oWinner.PlayerSocket)
            Send(String.Empty, String.Concat("UPDATEEXP ", exploser, " ", oLoser.Experience, " ", oLoser.Level, " ", oLoser.NextLevel), oLoser.PlayerSocket)

            Dim y As Integer = oGameFunctions.APBonus(winner)
            Send(String.Empty, "UPDATEAP " & giveap(winner, y), oWinner.PlayerSocket)
            Call blockchat(String.Empty, "AP", String.Concat("You received ", y, " Active Points!"), oWinner.PlayerSocket)

            y = oGameFunctions.APBonus(loser)
            Send(String.Empty, "UPDATEAP " & giveap(loser, CInt(y)), oLoser.PlayerSocket)
            Call blockchat(String.Empty, "AP", String.Concat("You received ", y, " Active Points!"), oLoser.PlayerSocket)
        Catch oEx_Null As System.NullReferenceException
            'do nothing
        Catch ex As Exception
            Call errorsub(ex, "giveexp")
        Finally
            RushBoost(winner)
            RushBoost(loser)
        End Try
    End Sub

    Public Sub RushBoost(ByVal sPlayer As String)
        'Dim oRush As New Rush
        'Dim sRush As String = oRush.RushBoost(sPlayer)

        'If sRush = "-1" Then
        '    Exit Sub
        'Else
        '    Send(sPlayer, String.Format("UPDATERUSH {0}", sRush))
        'End If
    End Sub

    Public Sub drawevent(ByVal Player1 As String, ByVal Player2 As String, Optional ByVal gameid As Integer = 0, Optional ByVal blnOTT As Boolean = False, Optional ByVal strGame As String = "", Optional ByVal iGamelength As Integer = 0, Optional ByRef oTTFunctions As TTFunctions = Nothing)
        Try
            Dim gprate(1) As Integer
            Dim plr1score As Integer, plr2score As Integer

            If blnOTT = True Then
                plr1score = 10
                plr2score = 10
            Else
                If strGame = "sb" Then
                    plr1score = Val(Readini("Score", "Player1", My.Application.Info.DirectoryPath & "\gamefiles\" & gameid & ".tgf"))
                    plr2score = Val(Readini("Score", "Player2", My.Application.Info.DirectoryPath & "\gamefiles\" & gameid & ".tgf"))
                Else
                    plr1score = 5
                    plr2score = 5
                End If
            End If

            Call laddersub(Player1, Player2, plr1score.ToString, plr2score.ToString, gameid, False, blnOTT, strGame)

            If plr1score = 0 And plr2score = 0 Then
                gprate(0) = 0
                gprate(1) = 0
            ElseIf plr1score <= 5 And plr2score <= 5 Then
                gprate(0) = 5
                gprate(1) = 5
            End If

            If strGame = "sb" Then
                gprate(0) = Int(gprate(0) / 2)
                gprate(1) = Int(gprate(1) / 2)
            Else
                gprate(0) = EndGameGP(Player1, 0)
                gprate(1) = EndGameGP(Player2, 0)
            End If

            'Dim iTurbo As Integer = oServerConfig.TurboMinimum

            'Select Case strGame
            '    Case "tt"
            '        iTurbo /= 2
            '    Case "ttm"
            '        iTurbo = 0
            'End Select

            'If iGamelength >= iTurbo Then
            '    Call GiveExp(Player1, 5, Player2, 5, gameid, oTTFunctions, blnOTT, strGame)

            '    Call gpadd(Player1, gprate(0), True)
            '    Call gpadd(Player2, gprate(1), True)
            'Else
            '    Call blockchat(Player1, "Warning", "You did not earn gold, active points, or experience because you and your opponent played too fast.  Turbo play is no longer rewarded.")
            '    Call blockchat(Player2, "Warning", "You did not earn gold, active points, or experience because you and your opponent played too fast.  Turbo play is no longer rewarded.")
            'End If
        Catch ex As Exception
            Call errorsub(ex, "drawevent")
        End Try
    End Sub

    Public Sub LevelUp(ByVal sName As String, ByVal iLevel As Integer)
        Try
            If iLevel = 20 Then
                Dim strReferal As String = getAccount_Field(sName, "referal")
                Dim oAA As New AlternateAdvancement
                oAA.UpdateAAPoints(sName, "honor", 0)

                If strReferal <> String.Empty Then
                    Dim oReferral As New Referral
                    oReferral.AddPendingReferral(sName, strReferal)
                End If
            End If

            Dim levelupgp As Integer = Int(iLevel * (50 + Int(Rnd() * iLevel)))

            If levelupgp > 1000 Then levelupgp = 1000

            Dim iBonus As Double = Val(getAAMod(sName, "true")) + Val(getVAMod(sName, "extended"))
            levelupgp = Int(levelupgp * (1 + iBonus))

            Call gpadd(sName, levelupgp, True)
            Send(sName, String.Concat("CHATBLOCK ", "Reward You%20receive%20", levelupgp, "GP%20for%20a%20level%20bonus!"))
        Catch ex As Exception
            Call errorsub(ex, "LevelUp")
        End Try
    End Sub

    Public Function APBonus(ByVal sNick As String) As Integer
        Try
            Randomize()
            Dim y As Integer = (Int(Rnd() * 50) + 1) * ((1 + (Val(getAAMod(sNick, "busy")))))

            If y < 10 Then y = 10

            Return y
        Catch ex As Exception
            Call errorsub(ex, "APBonus")
        End Try
    End Function

    Public Function PickStarter(ByVal blnTeam As Boolean) As Integer
        Try
            Static RandomNumGen As New System.Random
            Dim intnumber As Integer = RandomNumGen.Next(1, 11)

            Select Case blnTeam
                Case False
                    If intnumber <= 5 Then
                        Return 1
                    Else
                        Return 2
                    End If
                Case True
                    If intnumber <= 2 Then
                        Return 1
                    ElseIf intnumber <= 5 Then
                        Return 2
                    ElseIf intnumber <= 8 Then
                        Return 3
                    Else
                        Return 4
                    End If
            End Select
        Catch ex As Exception
            Call errorsub(ex, "PickStarter")
            Return 1
        End Try
    End Function

    <Description("Gets New GameID")> _
    Public Function NewGameID() As Integer
        Try
            Dim oMySqlHelper As New MySQLFactory.MySQLDBHelper
            Dim oDataRow As DataRow = Nothing

            oMySqlHelper.FillDataRow(ConnectionString, CommandType.StoredProcedure, "usp_getnextid", oDataRow)

            If oDataRow Is Nothing Then
                Throw New Exception
            Else
                Return Val(oDataRow.Item("nextid").ToString)
            End If
        Catch ex As Exception
            ErrorDescription = ex
            ErrorFlag = True
            Return Nothing
        End Try
    End Function

    <Description("Gets or Sets Exception Information from Retrieving Data")> _
Public Property ErrorDescription() As Exception
        Get
            Return oError
        End Get
        Set(ByVal Value As Exception)
            oError = Value
        End Set
    End Property

    <Description("Gets or Sets the Error Flag if Exception Information Exists")> _
    Public Property ErrorFlag() As Boolean
        Get
            Return bError
        End Get
        Set(ByVal Value As Boolean)
            bError = Value
        End Set
    End Property
End Class
