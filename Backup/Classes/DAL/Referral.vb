Imports MySql.Data.MySqlClient

Public Class Referral
    Public Sub AddPendingReferral(ByVal sReferral As String, ByVal sPlayer As String)
        Dim oMySQLHelper As New MySQLFactory.MySQLDBHelper

        Try
            Dim arParms() As MySqlParameter = New MySqlParameter(1) {}

            arParms(0) = New MySqlParameter("?sReferal", sReferral)
            arParms(1) = New MySqlParameter("?sPlayer", sPlayer)

            oMySQLHelper.DBExecuteScalar(ConnectionString, CommandType.StoredProcedure, "spi_PendingReferal", arParms)
        Catch ex As Exception

        Finally
            oMySQLHelper = Nothing
            oServerConfig.MySQLCalls += 1
        End Try
    End Sub
End Class
