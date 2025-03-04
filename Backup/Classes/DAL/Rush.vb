Imports MySql.Data.MySqlClient

Public Class Rush
    Public Function RushBreak(ByVal sPlayer As String) As String
        Dim oMySQLHelper As New MySQLFactory.MySQLDBHelper

        Try
            Dim arParms() As MySqlParameter = New MySqlParameter(0) {}
            Dim oDataRow As DataRow = Nothing

            arParms(0) = New MySqlParameter("?sPlayer", sPlayer)

            oMySQLHelper.FillDataRow(ConnectionString, CommandType.StoredProcedure, "sps_Rush", oDataRow, arParms)

            Dim sMessage As String = String.Empty

            If IsDBNull(oDataRow.Item(0)) = False Then sMessage = oDataRow.Item(0)

            Return sMessage
        Catch ex As Exception
            Return "Limit Break Failed"
        Finally
            oMySQLHelper = Nothing
            oServerConfig.MySQLCalls += 1
        End Try
    End Function

    Public Function RushBoost(ByVal sPlayer As String) As String
        Dim oMySQLHelper As New MySQLFactory.MySQLDBHelper

        Try
            Dim arParms() As MySqlParameter = New MySqlParameter(0) {}
            Dim oDataRow As DataRow = Nothing

            arParms(0) = New MySqlParameter("?sPlayer", sPlayer)

            oMySQLHelper.FillDataRow(ConnectionString, CommandType.StoredProcedure, "spi_Rush", oDataRow, arParms)

            Return oDataRow.Item(0)
        Catch ex As Exception
            'nothing
            Return "-1"
        Finally
            oMySQLHelper = Nothing
            oServerConfig.MySQLCalls += 1
        End Try
    End Function
End Class
