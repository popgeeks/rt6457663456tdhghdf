Imports mySQL.Data.MySqlClient
Imports MySQLFactory

Public Class ChatLog
    Public Function InsertChat(ByVal sPlayer As String, ByVal sChatText As String) As Boolean
        Dim oMySqlHelper As New MySQLFactory.MySQLDBHelper

        Try
            Dim arParms() As MySqlParameter = New MySqlParameter(1) {}
            arParms(0) = New MySqlParameter("?sPlayer", sPlayer)
            arParms(1) = New MySqlParameter("?sChatText", sChatText)

            oMySqlHelper.DBExecuteScalar(TTEConnectionString, CommandType.StoredProcedure, "spi_chatlog", arParms)
            Return True
        Catch ex As Exception
            Return False
        Finally
            oMySqlHelper = Nothing
            oServerConfig.MySQLCalls += 1
        End Try
    End Function

    Public Function InsertPM(ByVal sFrom As String, ByVal sTo As String, ByVal sMessage As String) As Boolean
        Dim oMySqlHelper As New MySQLFactory.MySQLDBHelper

        Try
            Dim arParms() As MySqlParameter = New MySqlParameter(2) {}
            arParms(0) = New MySqlParameter("?sFrom", sFrom)
            arParms(1) = New MySqlParameter("?sTo", sTo)
            arParms(2) = New MySqlParameter("?sMessage", sMessage)

            oMySqlHelper.DBExecuteScalar(TTEConnectionString, CommandType.StoredProcedure, "spi_PM", arParms)
            Return True
        Catch ex As Exception
            Return False
        Finally
            oMySqlHelper = Nothing
            oServerConfig.MySQLCalls += 1
        End Try
    End Function
End Class
