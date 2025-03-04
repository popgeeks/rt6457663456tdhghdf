Imports MySql.Data.MySqlClient
Imports MySQLFactory.MySQLDBHelper

Public Class AdminFunctions
    Private _ConnectionString As String

    Public Sub New(ByVal sConnectionString As String)
        _ConnectionString = sConnectionString
    End Sub

    Public Function WarnInsert(ByVal sPlayer As String, ByVal sAdmin As String, ByVal sReason As String) As Boolean
        Try
            Dim oMySqlHelper As New MySQLFactory.MySQLDBHelper
            Dim arParms() As MySqlParameter = New MySqlParameter(2) {}

            arParms(0) = New MySqlParameter("?sPlayer", sPlayer)
            arParms(1) = New MySqlParameter("?sAdmin", sAdmin)
            arParms(2) = New MySqlParameter("?sReason", sReason)

            oMySqlHelper.DBExecuteScalar(_ConnectionString, CommandType.StoredProcedure, "spi_Warn", arParms)
            Return True
        Catch ex As Exception
            Dim oError As New ErrorLogging.ErrorHandling()
            oError.LogErrorToEvents(ex.ToString, "WarnInsert")
            Return False
        End Try
    End Function

    Public Function BanInsert(ByVal sPlayer As String, ByVal sAdmin As String, ByVal sReason As String, ByVal sIP As String) As Boolean
        Try
            Dim oMySqlHelper As New MySQLFactory.MySQLDBHelper
            Dim arParms() As MySqlParameter = New MySqlParameter(3) {}

            arParms(0) = New MySqlParameter("?sPlayer", sPlayer)
            arParms(1) = New MySqlParameter("?sAdmin", sAdmin)
            arParms(2) = New MySqlParameter("?sReason", sReason)
            arParms(3) = New MySqlParameter("?sIP", sIP)

            oMySqlHelper.DBExecuteScalar(_ConnectionString, CommandType.StoredProcedure, "spi_Ban", arParms)
            Return True
        Catch ex As Exception
            Dim oError As New ErrorLogging.ErrorHandling()
            oError.LogErrorToEvents(ex.ToString, "BanInsert")
            Return False
        End Try
    End Function

    Public Function KickInsert(ByVal sPlayer As String, ByVal sAdmin As String, ByVal sReason As String) As Boolean
        Try
            Dim oMySqlHelper As New MySQLFactory.MySQLDBHelper
            Dim arParms() As MySqlParameter = New MySqlParameter(2) {}

            arParms(0) = New MySqlParameter("?sPlayer", sPlayer)
            arParms(1) = New MySqlParameter("?sAdmin", sAdmin)
            arParms(2) = New MySqlParameter("?sReason", sReason)

            oMySqlHelper.DBExecuteScalar(_ConnectionString, CommandType.StoredProcedure, "spi_Kick", arParms)
            Return True
        Catch ex As Exception
            Dim oError As New ErrorLogging.ErrorHandling()
            oError.LogErrorToEvents(ex.ToString, "KickInsert")
            Return False
        End Try
    End Function

    Public Function SilenceInsert(ByVal sNick As String) As Boolean
        Try
            Dim oMySqlHelper As New MySQLFactory.MySQLDBHelper
            Dim arParms() As MySqlParameter = New MySqlParameter(0) {}

            arParms(0) = New MySqlParameter("?sNick", sNick)
            oMySqlHelper.DBExecuteScalar(_ConnectionString, CommandType.StoredProcedure, "spi_PlayerSilence", arParms)
            Return True
        Catch ex As Exception
            Dim oError As New ErrorLogging.ErrorHandling()
            oError.LogErrorToEvents(ex.ToString, "SilenceInsert")
            Return False
        End Try
    End Function

    Public Function FindKeywords(ByVal sKey As String) As String
        Try
            Dim oMySqlHelper As New MySQLFactory.MySQLDBHelper
            Dim oDataRow As DataRow = Nothing
            Dim arParms() As MySqlParameter = New MySqlParameter(0) {}

            arParms(0) = New MySqlParameter("?sKeyword", sKey)
            oMySqlHelper.FillDataRow(_ConnectionString, CommandType.StoredProcedure, "sps_Keyword", oDataRow, arParms)

            Return oDataRow.Item("description").ToString
        Catch ex As Exception
            Return String.Empty
        End Try
    End Function

    Public Function OnlineAdmins() As DataTable
        Try
            Dim oMySqlHelper As New MySQLFactory.MySQLDBHelper
            Dim oDataTable As DataTable = Nothing

            oMySqlHelper.FillDataTable(_ConnectionString, CommandType.StoredProcedure, "sps_OnlineAdmins", oDataTable, "Results")
            Return oDataTable
        Catch ex As Exception
            Return Nothing
        End Try
    End Function
End Class
