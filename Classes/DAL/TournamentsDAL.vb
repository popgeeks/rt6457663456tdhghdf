Imports mySQL.Data.MySqlClient
Imports MySQLFactory
Imports System.ComponentModel
Imports System.Configuration

'TODO: TOURNAMENT TASK LIST
'2.  Process to start tournament at end of 30 minutes, create bracket
'3.  Process to force round if no game played in 30 minutes, knock off all people who are offline
'4.  Process to capture wins, losses each game.
'5.  Payoff


Public Class TournamentsDAL
    Protected Function Insert(ByVal sName As String, ByVal sType As String, ByVal sGame As String, ByVal iGold As Integer, ByVal iAP As Integer, ByVal sRuleset As String) As Boolean
        Try
            Dim oMySqlHelper As New MySQLFactory.MySQLDBHelper

            Dim arParms() As MySqlParameter = New MySqlParameter(5) {}
            arParms(0) = New MySqlParameter("?sName", sName)
            arParms(1) = New MySqlParameter("?sType", sType)
            arParms(2) = New MySqlParameter("?sGame", sGame)
            arParms(3) = New MySqlParameter("?iGold", iGold)
            arParms(4) = New MySqlParameter("?iAP", iAP)
            arParms(5) = New MySqlParameter("?sRuleset", sRuleset)

            oMySqlHelper.DBExecuteScalar(ConnectionString, CommandType.StoredProcedure, "spi_PlayerTournament", arParms)
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Protected Function InsertPlayer(ByVal sName As String, ByVal iID As Integer) As Boolean
        Try
            Dim oMySqlHelper As New MySQLFactory.MySQLDBHelper

            Dim arParms() As MySqlParameter = New MySqlParameter(1) {}
            arParms(0) = New MySqlParameter("?sNick", sName)
            arParms(1) = New MySqlParameter("?iTID", iID)

            oMySqlHelper.DBExecuteScalar(ConnectionString, CommandType.StoredProcedure, "spi_TournamentPlayer", arParms)
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
End Class
