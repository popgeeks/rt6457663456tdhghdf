Imports mySQL.Data.MySqlClient
Imports MySQLFactory
Imports System.ComponentModel
Imports System.Configuration

Public Class ProcessQueueDAL
    Public Function LoadTable() As DataTable
        Try
            Dim oMySqlHelper As New MySQLFactory.MySQLDBHelper
            Dim oDataTable As New DataTable

            oMySqlHelper.FillDataTable(ConnectionString, CommandType.StoredProcedure, "sps_ProcessQueue", oDataTable, "Results")
            Return oDataTable
        Catch ex As Exception
            Call errorsub(ex.ToString, "LoadTable")
            Return Nothing
        Finally
            oServerConfig.MySQLCalls += 1
        End Try
    End Function

    Public Function UpdateRecord(ByVal iID As Integer) As Boolean
        Try
            Dim oMySqlHelper As New MySQLFactory.MySQLDBHelper
            Dim arParms() As MySqlParameter = New MySqlParameter(0) {}

            arParms(0) = New MySqlParameter("?iID", iID)
            oMySqlHelper.DBExecuteScalar(ConnectionString, CommandType.StoredProcedure, "spu_ProcessQueue", arParms)
            Return True
        Catch ex As Exception
            Call errorsub(ex.ToString, "UpdateRecord")
            Return False
        Finally
            oServerConfig.MySQLCalls += 1
        End Try
    End Function
End Class
