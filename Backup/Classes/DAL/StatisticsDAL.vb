Imports MySql.Data.MySqlClient
Imports MySQLFactory.MySQLDBHelper

Public Class StatisticsDAL
    Protected Function UpdateStats(ByVal sNick As String, ByVal sGame As String, ByVal iStatus As Integer) As Boolean
        Try
            Dim oMySqlHelper As New MySQLFactory.MySQLDBHelper
            Dim arParms() As MySqlParameter = New MySqlParameter(2) {}

            arParms(0) = New MySqlParameter("?sNick", sNick)
            arParms(1) = New MySqlParameter("?sGame", sGame)
            arParms(2) = New MySqlParameter("?iStatus", iStatus)
            oMySqlHelper.DBExecuteScalar(ConnectionString, CommandType.StoredProcedure, "spu_AccountStats", arParms)
            Return True
        Catch ex As Exception
            Call errorsub(ex.ToString, "CompileStats")
            Return False
        Finally
            oServerConfig.MySQLCalls += 1
        End Try
    End Function
End Class
