Imports MySql.Data.MySqlClient
Imports MySQLFactory.MySQLDBHelper

Public Class SphereBreakDAL
    Protected Sub InsertGameFile(ByVal gameid As Integer)
        Try
            Dim gamefile As String = My.Application.Info.DirectoryPath & "\gamefiles\" & gameid & ".tgf"
            Dim oMySqlHelper As New MySQLFactory.MySQLDBHelper

            Dim arParms() As MySqlParameter = New MySqlParameter(8) {}
            arParms(0) = New MySqlParameter("?sGameID", gameid.ToString)
            arParms(1) = New MySqlParameter("?sPlayer1", Readini("Gameinfo", "Player1", gamefile))
            arParms(2) = New MySqlParameter("?sPlayer2", Readini("Gameinfo", "Player2", gamefile))
            arParms(3) = New MySqlParameter("?sPlr1Score", Readini("Score", "Player1", gamefile))
            arParms(4) = New MySqlParameter("?sPlr2Score", Readini("Score", "Player2", gamefile))
            arParms(5) = New MySqlParameter("?sTime", Readini("Data", "Time", gamefile))
            arParms(6) = New MySqlParameter("?sLimit", Readini("Data", "Score", gamefile))
            arParms(7) = New MySqlParameter("?sTurns", Readini("Data", "Turns", gamefile))
            arParms(8) = New MySqlParameter("?sDate", Format(Date.Now, "MM-dd-yyyy"))

            oMySqlHelper.DBExecuteScalar(ConnectionString, CommandType.StoredProcedure, "spi_SphereBreakGame", arParms)
            Kill(gamefile)
        Catch ex As Exception
            Call errorsub(ex, "SphereBreakDAL.GameInsert")
        Finally
            oServerConfig.MySQLCalls += 1
        End Try
    End Sub
End Class
