Imports MySql.Data.MySqlClient
Imports MySQLFactory.MySQLDBHelper

Public Class AdminFunctionsDAL
    Protected Function WarnInsert(ByVal sPlayer As String, ByVal sAdmin As String, ByVal sReason As String) As Boolean
        Try
            Dim oMySqlHelper As New MySQLFactory.MySQLDBHelper
            Dim arParms() As MySqlParameter = New MySqlParameter(2) {}

            arParms(0) = New MySqlParameter("?sPlayer", sPlayer)
            arParms(1) = New MySqlParameter("?sAdmin", sAdmin)
            arParms(2) = New MySqlParameter("?sReason", sReason)

            oMySqlHelper.DBExecuteScalar(ConnectionString, CommandType.StoredProcedure, "spi_Warn", arParms)
            Return True
        Catch ex As Exception
            Call errorsub(ex.ToString, "WarnInsert")
            Return False
        Finally
            oServerConfig.MySQLCalls += 1
        End Try
    End Function

    Protected Function BanInsert(ByVal sPlayer As String, ByVal sAdmin As String, ByVal sReason As String, ByVal sIP As String) As Boolean
        Try
            Dim oMySqlHelper As New MySQLFactory.MySQLDBHelper
            Dim arParms() As MySqlParameter = New MySqlParameter(3) {}

            arParms(0) = New MySqlParameter("?sPlayer", sPlayer)
            arParms(1) = New MySqlParameter("?sAdmin", sAdmin)
            arParms(2) = New MySqlParameter("?sReason", sReason)
            arParms(3) = New MySqlParameter("?sIP", sIP)

            oMySqlHelper.DBExecuteScalar(ConnectionString, CommandType.StoredProcedure, "spi_Ban", arParms)
            Return True
        Catch ex As Exception
            Call errorsub(ex.ToString, "BanInsert")
            Return False
        Finally
            oServerConfig.MySQLCalls += 1
        End Try
    End Function

    Protected Function KickInsert(ByVal sPlayer As String, ByVal sAdmin As String, ByVal sReason As String) As Boolean
        Try
            Dim oMySqlHelper As New MySQLFactory.MySQLDBHelper
            Dim arParms() As MySqlParameter = New MySqlParameter(2) {}

            arParms(0) = New MySqlParameter("?sPlayer", sPlayer)
            arParms(1) = New MySqlParameter("?sAdmin", sAdmin)
            arParms(2) = New MySqlParameter("?sReason", sReason)

            oMySqlHelper.DBExecuteScalar(ConnectionString, CommandType.StoredProcedure, "spi_Kick", arParms)
            Return True
        Catch ex As Exception
            Call errorsub(ex.ToString, "KickInsert")
            Return False
        Finally
            oServerConfig.MySQLCalls += 1
        End Try
    End Function

    Protected Function SilenceInsert(ByVal sNick As String) As Boolean
        Try
            Dim oMySqlHelper As New MySQLFactory.MySQLDBHelper
            Dim arParms() As MySqlParameter = New MySqlParameter(0) {}

            arParms(0) = New MySqlParameter("?sNick", sNick)
            oMySqlHelper.DBExecuteScalar(ConnectionString, CommandType.StoredProcedure, "spi_PlayerSilence", arParms)
            Return True
        Catch ex As Exception
            Call errorsub(ex.ToString, "SilenceInsert")
            Return False
        Finally
            oServerConfig.MySQLCalls += 1
        End Try
    End Function

    Protected Function FindKeywords(ByVal sKey As String) As String
        Try
            Dim oMySqlHelper As New MySQLFactory.MySQLDBHelper
            Dim oDataRow As DataRow = Nothing
            Dim arParms() As MySqlParameter = New MySqlParameter(0) {}

            arParms(0) = New MySqlParameter("?sKeyword", sKey)
            oMySqlHelper.FillDataRow(ConnectionString, CommandType.StoredProcedure, "sps_Keyword", oDataRow, arParms)

            Return oDataRow.Item("description").ToString
        Catch ex As Exception
            Return String.Empty
        Finally
            oServerConfig.MySQLCalls += 1
        End Try
    End Function

    Public Function OnlineAdmins() As DataTable
        Try
            Dim oMySqlHelper As New MySQLFactory.MySQLDBHelper
            Dim oDataTable As DataTable = Nothing

            oMySqlHelper.FillDataTable(ConnectionString, CommandType.StoredProcedure, "sps_OnlineAdmins", oDataTable, "Results")
            Return oDataTable
        Catch ex As Exception
            Return Nothing
        Finally
            oServerConfig.MySQLCalls += 1
        End Try
    End Function
End Class
