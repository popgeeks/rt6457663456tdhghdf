Imports MySql.Data.MySqlClient
Imports MySQLFactory.MySQLDBHelper

Public Class CardWars
    Inherits FMWK.BaseDAL

    Private _ConnectionString As String

    Public Sub New(ByVal sConnectionString As String)
        _ConnectionString = sConnectionString
    End Sub

    Public Function LoadRecord(ByVal iGameID As Integer) As Entities.GameObjects.CardWars
        Try
            Dim oMySqlHelper As New MySQLFactory.MySQLDBHelper
            Dim oDataRow As DataRow = Nothing

            Dim oCardWars As New Entities.GameObjects.CardWars

            Dim arParms() As MySqlParameter = New MySqlParameter(0) {}
            arParms(0) = New MySqlParameter("?iGameID", iGameID)

            oMySqlHelper.FillDataRow(_ConnectionString, CommandType.StoredProcedure, "sps_CardWarsGame", oDataRow, arParms)

            If oDataRow Is Nothing Then
                Throw New Exception
            Else
                With oDataRow
                    oCardWars.GameID = iGameID
                    oCardWars.Player1 = .Item("Player1").ToString
                    oCardWars.Player2 = .Item("Player2").ToString

                    oCardWars.Player1_HP = .Item("Player1_Life")
                    oCardWars.Player2_HP = .Item("Player2_Life")

                    oCardWars.Winner = .Item("Winner")
                    oCardWars.Offense = .Item("Offense")
                    oCardWars.Defense = .Item("Defense")
                    oCardWars.ElementField = .Item("ElementField")
                    oCardWars.P1Ready = .Item("P1_Ready")
                    oCardWars.P2Ready = .Item("P2_Ready")
                    oCardWars.ChainCount = .Item("ChainCount")
                    oCardWars.ChainPlayer = .Item("ChainPlayer")

                    oCardWars.PlayerTurn = .Item("PlayerTurn")
                    oCardWars.Rounds = .Item("Rounds")

                    If IsDBNull(.Item("StartDate")) = False Then oCardWars.StartDate = .Item("StartDate")
                    If IsDBNull(.Item("LastActivity")) = False Then oCardWars.LastActivity = .Item("LastActivity")
                    If IsDBNull(.Item("GameEnds")) = False Then oCardWars.GameEnds = .Item("GameEnds")
                    If IsDBNull(.Item("TimeOut")) = False Then oCardWars.TimeOut = .Item("TimeOut")

                    oCardWars.Surrender = .Item("Surrender")
                End With
            End If

            GetPlayerHand(oCardWars.Player1, oCardWars.GameID, oCardWars, 1)
            GetPlayerHand(oCardWars.Player2, oCardWars.GameID, oCardWars, 2)

            Return oCardWars
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Public Sub GetPlayerHand(ByVal sPlayer As String, ByVal iGameID As Integer, ByRef oCardWars As Entities.GameObjects.CardWars, ByVal iPlayerID As Integer)
        Try
            Dim oMySqlHelper As New MySQLFactory.MySQLDBHelper
            Dim arParms() As MySqlParameter = New MySqlParameter(1) {}
            Dim oDataTable As New DataTable

            arParms(0) = New MySqlParameter("?sPlayer", sPlayer)
            arParms(1) = New MySqlParameter("?iGameID", iGameID)

            oMySqlHelper.FillDataTable(_ConnectionString, CommandType.StoredProcedure, "sps_CardWarsPlayerHand", oDataTable, "Results", arParms)

            For Each oDataRow As DataRow In oDataTable.Rows
                If iPlayerID = 1 Then
                    oCardWars.Player1Hand.Add(oDataRow.Item("CardName").ToString)
                Else
                    oCardWars.Player2Hand.Add(oDataRow.Item("CardName").ToString)
                End If
            Next
        Catch ex As Exception
            Dim oError As New ErrorLogging.ErrorHandling()
            oError.LogErrorToEvents(ex.ToString, "GetPlayerHand")
        End Try
    End Sub

    Public Function GetRoundHistory(ByVal iGameID As Integer, ByVal iRound As Integer) As DataTable
        Try
            Dim oMySqlHelper As New MySQLFactory.MySQLDBHelper
            Dim arParms() As MySqlParameter = New MySqlParameter(1) {}
            Dim oDataTable As New DataTable

            arParms(0) = New MySqlParameter("?iGameID", iGameID)
            arParms(1) = New MySqlParameter("?iRound", iRound)

            oMySqlHelper.FillDataTable(_ConnectionString, CommandType.StoredProcedure, "sps_CardWarsHistory", oDataTable, "Results", arParms)

            Return oDataTable
        Catch ex As Exception
            Dim oError As New ErrorLogging.ErrorHandling()
            oError.LogErrorToEvents(ex.ToString, "GetRoundHistory")
            Return Nothing
        End Try
    End Function

    Public Sub AnalyzeRound(ByVal iGameID As Integer, ByRef oCardWars As Entities.GameObjects.CardWars)
        Try
            Dim oMySqlHelper As New MySQLFactory.MySQLDBHelper
            Dim arParms() As MySqlParameter = New MySqlParameter(2) {}
            Dim oDataTable As New DataTable

            arParms(0) = New MySqlParameter("?iGameID", iGameID)
            arParms(1) = New MySqlParameter("?sCard1", oCardWars.Player1_Card)
            arParms(2) = New MySqlParameter("?sCard2", oCardWars.Player2_Card)

            oMySqlHelper.ExecuteScalar(_ConnectionString, CommandType.StoredProcedure, "spu_CardWarsAnalyze", arParms)
        Catch ex As Exception
            Dim oError As New ErrorLogging.ErrorHandling()
            oError.LogErrorToEvents(ex.ToString, "AnalyzeRound")
        End Try
    End Sub

    Public Sub EndGame(ByVal iGameID As Integer)
        Dim oMySqlHelper As New MySQLFactory.MySQLDBHelper

        Dim arParms() As MySqlParameter = New MySqlParameter(0) {}
        arParms(0) = New MySqlParameter("?iGameID", iGameID)

        oMySqlHelper.DBExecuteScalar(_ConnectionString, CommandType.StoredProcedure, "spu_Cardwars_EndGame", arParms)
    End Sub

    Public Sub GameTimeOut(ByVal iGameID As Integer)
        Dim oMySqlHelper As New MySQLFactory.MySQLDBHelper

        Dim arParms() As MySqlParameter = New MySqlParameter(0) {}
        arParms(0) = New MySqlParameter("?iGameID", iGameID)

        oMySqlHelper.DBExecuteScalar(_ConnectionString, CommandType.StoredProcedure, "spu_Cardwars_Timeout", arParms)
    End Sub

    Public Sub Surrender(ByVal iGameID As Integer, ByVal sPlayer As String)
        Dim oMySqlHelper As New MySQLFactory.MySQLDBHelper

        Dim arParms() As MySqlParameter = New MySqlParameter(1) {}
        arParms(0) = New MySqlParameter("?iGameID", iGameID)
        arParms(1) = New MySqlParameter("?sPlayer", sPlayer)

        oMySqlHelper.DBExecuteScalar(_ConnectionString, CommandType.StoredProcedure, "spu_Cardwars_Surrender", arParms)
    End Sub

    Public Sub Stalemate(ByVal iGameID As Integer)
        Dim oMySqlHelper As New MySQLFactory.MySQLDBHelper

        Dim arParms() As MySqlParameter = New MySqlParameter(0) {}
        arParms(0) = New MySqlParameter("?iGameID", iGameID)

        oMySqlHelper.DBExecuteScalar(_ConnectionString, CommandType.StoredProcedure, "spu_Cardwars_Stalemate", arParms)
    End Sub
End Class
