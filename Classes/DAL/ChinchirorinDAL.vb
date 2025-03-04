Imports MySql.Data.MySqlClient
Imports MySQLFactory.MySQLDBHelper

Public Class ChinchirorinDAL
    Protected Function UpdateRecord(ByVal sKey As String, ByVal iValue As Integer, ByVal iGameID As Integer) As Boolean
        Try
            Dim oMySqlHelper As New MySQLFactory.MySQLDBHelper
            Dim arParms() As MySqlParameter = New MySqlParameter(2) {}

            arParms(0) = New MySqlParameter("?sKey", sKey)
            arParms(1) = New MySqlParameter("?iGameID", iGameID)
            arParms(2) = New MySqlParameter("?iValue", iValue)
            oMySqlHelper.DBExecuteScalar(ConnectionString, CommandType.StoredProcedure, "spu_ChinchirorinGame", arParms)

            Return True
        Catch ex As Exception
            Call errorsub(ex, "Chinchin.UpdateRecord")
            Return False
        Finally
            oServerConfig.MySQLCalls += 1
        End Try
    End Function

    Protected Function InsertGame(ByVal iGameID As Integer, ByVal sPlayer1 As String, ByVal sPlayer2 As String, ByVal iWager As Integer) As Boolean
        Try
            Dim oMySqlHelper As New MySQLFactory.MySQLDBHelper
            Dim arParms() As MySqlParameter = New MySqlParameter(3) {}

            arParms(0) = New MySqlParameter("?iGameID", iGameID)
            arParms(1) = New MySqlParameter("?sPlayer1", sPlayer1)
            arParms(2) = New MySqlParameter("?sPlayer2", sPlayer2)
            arParms(3) = New MySqlParameter("?iWager", iWager)
            oMySqlHelper.DBExecuteScalar(ConnectionString, CommandType.StoredProcedure, "spi_ChinchirorinGame", arParms)
            Return True
        Catch ex As Exception
            Call errorsub(ex, "Chinchin.InsertGame")
            Return False
        Finally
            oServerConfig.MySQLCalls += 1
        End Try
    End Function

    Protected Function SelectGame(ByVal iGameID As Integer) As DataRow
        Try
            Dim oMySqlHelper As New MySQLFactory.MySQLDBHelper
            Dim arParms() As MySqlParameter = New MySqlParameter(0) {}
            Dim oDataRow As DataRow = Nothing

            arParms(0) = New MySqlParameter("?iGameID", iGameID)
            oMySqlHelper.FillDataRow(ConnectionString, CommandType.StoredProcedure, "sps_ChinchirorinGame", oDataRow, arParms)
            Return oDataRow
        Catch ex As Exception
            Call errorsub(ex, "SelectGame")
            Return Nothing
        Finally
            oServerConfig.MySQLCalls += 1
        End Try
    End Function

    Protected Function ChinHistory(ByVal iGameID As Integer, ByVal sPlayer As String, ByVal sDice1 As String, ByVal sDice2 As String, ByVal sDice3 As String, ByVal sResult As String) As Boolean
        Try
            Dim oMySqlHelper As New MySQLFactory.MySQLDBHelper
            Dim arParms() As MySqlParameter = New MySqlParameter(5) {}

            arParms(0) = New MySqlParameter("?iGameID", iGameID)
            arParms(1) = New MySqlParameter("?sPlayer", sPlayer)
            arParms(2) = New MySqlParameter("?sDice1", sDice1)
            arParms(3) = New MySqlParameter("?sDice2", sDice2)
            arParms(4) = New MySqlParameter("?sDice3", sDice3)
            arParms(5) = New MySqlParameter("?sResult", sResult)
            oMySqlHelper.DBExecuteScalar(ConnectionString, CommandType.StoredProcedure, "spi_chinchirorin_history", arParms)
            Return True
        Catch ex As Exception
            Call errorsub(ex, "Chinchin.ChinHistory")
            Return False
        Finally
            oServerConfig.MySQLCalls += 1
        End Try
    End Function
End Class
