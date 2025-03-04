Imports MySql.Data.MySqlClient

Public Class Jackpots
    Public Function JackpotTicket(ByVal sPlayer As String) As String
        Dim oMySQLHelper As New MySQLFactory.MySQLDBHelper

        Try
            Dim arParms() As MySqlParameter = New MySqlParameter(0) {}
            Dim oDataRow As DataRow = Nothing

            arParms(0) = New MySqlParameter("?sPlayer", sPlayer)

            oMySQLHelper.FillDataRow(ConnectionString, CommandType.StoredProcedure, "spt_JackpotTicket", oDataRow, arParms)

            Dim sMessage As String = String.Empty

            If IsDBNull(oDataRow.Item(0)) = False Then sMessage = oDataRow.Item(0)

            Return sMessage
        Catch ex As Exception
            Return String.Empty
        Finally
            oMySQLHelper = Nothing
            oServerConfig.MySQLCalls += 1
        End Try
    End Function
End Class
