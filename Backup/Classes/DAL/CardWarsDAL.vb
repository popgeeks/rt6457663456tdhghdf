Imports mySQL.Data.MySqlClient
Imports MySQLFactory
Imports System.ComponentModel
Imports System.Configuration

Public Class CardWarsDAL
    Protected Function InsertGame(ByVal iGameID As Integer, ByVal sPlayer1 As String, ByVal sPlayer2 As String, ByVal iStarter As Integer) As Boolean
        Try
            Dim oMySqlHelper As New MySQLFactory.MySQLDBHelper
            Dim arParms() As MySqlParameter = New MySqlParameter(3) {}

            arParms(0) = New MySqlParameter("?iGameID", iGameID)
            arParms(1) = New MySqlParameter("?sPlayer1", sPlayer1)
            arParms(2) = New MySqlParameter("?sPlayer2", sPlayer2)
            arParms(3) = New MySqlParameter("?iStarter", iStarter)
            oMySqlHelper.DBExecuteScalar(ConnectionString, CommandType.StoredProcedure, "spi_CardWars", arParms)
            Return True
        Catch ex As Exception
            Call errorsub(ex.ToString, "Chinchin.InsertGame")
            Return False
        Finally
            oServerConfig.MySQLCalls += 1
        End Try
    End Function
End Class
