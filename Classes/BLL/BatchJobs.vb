Imports MySql.Data.MySqlClient
Imports MySQLFactory.MySQLDBHelper

Public Class BatchJobs
    Public Sub PlayerSilence()
        Dim oMySqlHelper As New MySQLFactory.MySQLDBHelper
        oMySqlHelper.DBExecuteScalar(ConnectionString, CommandType.StoredProcedure, "spu_PlayerSilence")
    End Sub

    Public Sub MassAP()
        Dim oMySQLHelper As New MySQLFactory.MySQLDBHelper

        oMySQLHelper.DBExecuteScalar(ConnectionString, CommandType.StoredProcedure, "usp_givemassap")
    End Sub

    Public Sub RankPenalty()
        Dim oMySQLHelper As New MySQLFactory.MySQLDBHelper

        oMySQLHelper.DBExecuteScalar(ConnectionString, CommandType.StoredProcedure, "usp_RankPenalty")
    End Sub
End Class
