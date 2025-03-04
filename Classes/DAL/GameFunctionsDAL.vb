Imports MySql.Data.MySqlClient
Imports MySQLFactory.MySQLDBHelper

Public Class GameFunctionsDAL
    Public Function RankChange(ByVal sWinner As String, ByVal sLoser As String, ByVal sGame As String) As Boolean
        Try
            Dim oMySqlHelper As New MySQLFactory.MySQLDBHelper
            Dim arParms() As MySqlParameter = New MySqlParameter(2) {}

            arParms(0) = New MySqlParameter("?sWinner", sWinner)
            arParms(1) = New MySqlParameter("?sLoser", sLoser)
            arParms(2) = New MySqlParameter("?sGame", sGame)

            oMySqlHelper.DBExecuteScalar(ConnectionString, CommandType.StoredProcedure, "spt_Player_RankChange", arParms)
            Return True
        Catch ex As Exception
            Call errorsub(ex, "GameFunctionsDAL.RankChange")
            Return False
        Finally
            oServerConfig.MySQLCalls += 1
        End Try
    End Function
End Class
