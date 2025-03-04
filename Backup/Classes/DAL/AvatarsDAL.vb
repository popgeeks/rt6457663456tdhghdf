Imports mySQL.Data.MySqlClient
Imports MySQLFactory
Imports System.ComponentModel
Imports System.Configuration

Public Class AvatarsDAL
    Protected Function InsertPlayerAvatarInventory(ByVal sKey As String, ByVal sType As String, ByVal sNick As String) As Boolean
        Try
            Dim oMySqlHelper As New MySQLFactory.MySQLDBHelper

            Dim arParms() As MySqlParameter = New MySqlParameter(2) {}
            arParms(0) = New MySqlParameter("?sKey", sKey)
            arParms(1) = New MySqlParameter("?sType", sType)
            arParms(2) = New MySqlParameter("?sNick", sNick)

            oMySqlHelper.DBExecuteScalar(ConnectionString, CommandType.StoredProcedure, "spi_PlayerAvatarInventory", arParms)
            Return True
        Catch ex As Exception
            Return False
        Finally
            oServerConfig.MySQLCalls += 1
        End Try
    End Function

    Protected Function DeletePlayerAvatarInventory(ByVal iID As Integer) As Boolean
        Try
            Dim oMySqlHelper As New MySQLFactory.MySQLDBHelper

            Dim arParms() As MySqlParameter = New MySqlParameter(0) {}
            arParms(0) = New MySqlParameter("?iID", iID)

            oMySqlHelper.DBExecuteScalar(ConnectionString, CommandType.StoredProcedure, "spd_PlayerAvatarInventory", arParms)
            Return True
        Catch ex As Exception
            Return False
        Finally
            oServerConfig.MySQLCalls += 1
        End Try
    End Function
End Class
