Imports MySql.Data.MySqlClient
Imports MySQLFactory.MySQLDBHelper

Public Class GuildsDAL
    Protected Function GuildRightsInfo(ByVal sNick As String, ByVal sGuildName As String) As DataRow
        Try
            Dim oMySqlHelper As New MySQLFactory.MySQLDBHelper
            Dim arParms() As MySqlParameter = New MySqlParameter(1) {}
            Dim oDataRow As DataRow = Nothing

            arParms(0) = New MySqlParameter("?sNick", sNick)
            arParms(1) = New MySqlParameter("?sGuildName", sGuildName)
            oMySqlHelper.FillDataRow(ConnectionString, CommandType.StoredProcedure, "sps_GuildInfoRights", oDataRow, arParms)
            Return oDataRow
        Catch ex As Exception
            Call errorsub(ex.ToString, "GuildRightsInfo")
            Return Nothing
        Finally
            oServerConfig.MySQLCalls += 1
        End Try
    End Function

    Protected Function GetGuildCount(ByVal sGuildName As String) As Integer
        Try
            Dim oMySqlHelper As New MySQLFactory.MySQLDBHelper
            Dim arParms() As MySqlParameter = New MySqlParameter(0) {}
            Dim oDataRow As DataRow = Nothing

            arParms(0) = New MySqlParameter("?sGuild", sGuildName)
            oMySqlHelper.FillDataRow(ConnectionString, CommandType.StoredProcedure, "sps_GuildCount", oDataRow, arParms)

            If Not (oDataRow Is Nothing) Then
                Return Val(oDataRow.Item("Count").ToString)
            Else
                Return 0
            End If
        Catch ex As Exception
            Call errorsub(ex.ToString, "GuildRightsInfo")
            Return Nothing
        Finally
            oServerConfig.MySQLCalls += 1
        End Try
    End Function

    Protected Function GuildOnlineUsers(ByVal sGuildName As String) As DataSet
        Try
            Dim oMySqlHelper As New MySQLFactory.MySQLDBHelper
            Dim arParms() As MySqlParameter = New MySqlParameter(0) {}
            Dim oDataset As New DataSet

            arParms(0) = New MySqlParameter("?sGuild", sGuildName)
            oMySqlHelper.FillDataSet(ConnectionString, CommandType.StoredProcedure, "sps_GuildOnlineUsers", oDataset, New String() {"Results"}, arParms)
            Return oDataset
        Catch ex As Exception
            Call errorsub(ex.ToString, "GuildOnlineUsers")
            Return Nothing
        Finally
            oServerConfig.MySQLCalls += 1
        End Try
    End Function

    Protected Function GuildBankGold(ByVal sGuildName As String) As DataRow
        Try
            Dim oMySqlHelper As New MySQLFactory.MySQLDBHelper
            Dim arParms() As MySqlParameter = New MySqlParameter(0) {}
            Dim oDataRow As DataRow = Nothing

            arParms(0) = New MySqlParameter("?sGuild", sGuildName)
            oMySqlHelper.FillDataRow(ConnectionString, CommandType.StoredProcedure, "sps_GuildBankGold", oDataRow, arParms)
            Return oDataRow
        Catch ex As Exception
            Call errorsub(ex.ToString, "GuildBankGold")
            Return Nothing
        Finally
            oServerConfig.MySQLCalls += 1
        End Try
    End Function

    Protected Function DeleteBankCard(ByVal sGuild As String, ByVal sCard As String) As Boolean
        Try
            Dim oMySqlHelper As New MySQLFactory.MySQLDBHelper
            Dim arParms() As MySqlParameter = New MySqlParameter(1) {}
            Dim oDataRow As DataRow = Nothing

            arParms(0) = New MySqlParameter("?sGuild", sGuild)
            arParms(1) = New MySqlParameter("?sCardName", sCard)
            oMySqlHelper.FillDataRow(ConnectionString, CommandType.StoredProcedure, "spd_GuildBankCards", oDataRow, arParms)
            Return IIf(oDataRow.Item("Succeed") = "1", True, False)
        Catch ex As Exception
            Call errorsub(ex.ToString, "DeleteBankCard")
            Return False
        Finally
            oServerConfig.MySQLCalls += 1
        End Try
    End Function

    Protected Function PlayerCardDetail(ByVal sNick As String, ByVal sCard As String) As DataRow
        Try
            Dim oMySqlHelper As New MySQLFactory.MySQLDBHelper
            Dim arParms() As MySqlParameter = New MySqlParameter(1) {}
            Dim oDataRow As DataRow = Nothing

            arParms(0) = New MySqlParameter("?sNick", sNick)
            arParms(1) = New MySqlParameter("?sCard", sCard)
            oMySqlHelper.FillDataRow(ConnectionString, CommandType.StoredProcedure, "sps_PlayerCardDetail", oDataRow, arParms)
            Return oDataRow
        Catch ex As Exception
            Call errorsub(ex.ToString, "PlayerCardDetail")
            Return Nothing
        Finally
            oServerConfig.MySQLCalls += 1
        End Try
    End Function

    Protected Function InsertGuildBankCards(ByVal sPlayer As String, ByVal sGuild As String, ByVal sCard As String) As String
        Dim oMySqlHelper As New MySQLFactory.MySQLDBHelper
        Dim oDataRow As DataRow = Nothing

        Try
            Dim arParms() As MySqlParameter = New MySqlParameter(2) {}

            arParms(0) = New MySqlParameter("?sPlayer", sPlayer)
            arParms(1) = New MySqlParameter("?sGuild", sGuild)
            arParms(2) = New MySqlParameter("?sCardName", sCard)

            oMySqlHelper.FillDataRow(ConnectionString, CommandType.StoredProcedure, "spi_GuildBankCards", oDataRow, arParms)

            If oDataRow Is Nothing Then
                Return "Operation Failed"
            Else
                Return oDataRow.Item(0).ToString
            End If

            Return True
        Catch ex As Exception
            Call errorsub(ex.ToString, "InsertGuildBankCards")
            Return "An Error Occured.  Please try again later."
        Finally
            oMySqlHelper = Nothing
            oDataRow = Nothing
            oServerConfig.MySQLCalls += 1
        End Try
    End Function

    Protected Function InsertGuildDescription(ByVal sGuild As String, ByVal sDescription As String) As Boolean
        Try
            Dim oMySqlHelper As New MySQLFactory.MySQLDBHelper
            Dim arParms() As MySqlParameter = New MySqlParameter(1) {}

            arParms(0) = New MySqlParameter("?sGuild", sGuild)
            arParms(1) = New MySqlParameter("?sDescription", sDescription)
            oMySqlHelper.DBExecuteScalar(ConnectionString, CommandType.StoredProcedure, "spi_GuildDescription", arParms)
            Return True
        Catch ex As Exception
            Call errorsub(ex.ToString, "InsertGuildDescription")
            Return False
        Finally
            oServerConfig.MySQLCalls += 1
        End Try
    End Function

    Protected Function InsertGuildHistory(ByVal sGuild As String, ByVal sType As String, ByVal sDescription As String) As Boolean
        Try
            Dim oMySqlHelper As New MySQLFactory.MySQLDBHelper
            Dim arParms() As MySqlParameter = New MySqlParameter(2) {}

            arParms(0) = New MySqlParameter("?sGuild", sGuild)
            arParms(1) = New MySqlParameter("?sType", sType)
            arParms(2) = New MySqlParameter("?sDescription", sDescription)
            oMySqlHelper.DBExecuteScalar(ConnectionString, CommandType.StoredProcedure, "spi_GuildHistory", arParms)
            Return True
        Catch ex As Exception
            Call errorsub(ex.ToString, "InsertGuildHistory")
            Return False
        Finally
            oServerConfig.MySQLCalls += 1
        End Try
    End Function

    Protected Function InsertGuildApplication(ByVal sGuild As String, ByVal sNick As String) As Boolean
        Try
            Dim oMySqlHelper As New MySQLFactory.MySQLDBHelper
            Dim arParms() As MySqlParameter = New MySqlParameter(1) {}

            arParms(0) = New MySqlParameter("?sGuild", sGuild)
            arParms(1) = New MySqlParameter("?sNick", sNick)
            oMySqlHelper.DBExecuteScalar(ConnectionString, CommandType.StoredProcedure, "spi_GuildApplications", arParms)
            Return True
        Catch ex As Exception
            Call errorsub(ex.ToString, "InsertGuildApplication")
            Return False
        Finally
            oServerConfig.MySQLCalls += 1
        End Try
    End Function

    Protected Function UpdateGuildInfo(ByVal sGuild As String, ByVal sDescription As String, ByVal sMOTD As String) As Boolean
        Try
            Dim oMySqlHelper As New MySQLFactory.MySQLDBHelper
            Dim arParms() As MySqlParameter = New MySqlParameter(2) {}

            arParms(0) = New MySqlParameter("?sGuild", sGuild)
            arParms(1) = New MySqlParameter("?sDescription", sDescription)
            arParms(2) = New MySqlParameter("?sMOTD", sMOTD)
            oMySqlHelper.DBExecuteScalar(ConnectionString, CommandType.StoredProcedure, "spu_GuildDescription", arParms)
            Return True
        Catch ex As Exception
            Call errorsub(ex.ToString, "InsertGuildApplication")
            Return False
        Finally
            oServerConfig.MySQLCalls += 1
        End Try
    End Function

    Protected Function UpdateBankGold(ByVal sGuild As String, ByVal iGold As Integer) As Boolean
        Try
            Dim oMySqlHelper As New MySQLFactory.MySQLDBHelper
            Dim arParms() As MySqlParameter = New MySqlParameter(1) {}

            arParms(0) = New MySqlParameter("?sGuild", sGuild)
            arParms(1) = New MySqlParameter("?iGold", iGold)
            oMySqlHelper.DBExecuteScalar(ConnectionString, CommandType.StoredProcedure, "spu_GuildBankGold", arParms)
            Return True
        Catch ex As Exception
            Call errorsub(ex.ToString, "UpdateBankGold")
            Return False
        Finally
            oServerConfig.MySQLCalls += 1
        End Try
    End Function
    Protected Function DeleteGuildApplication(ByVal sNick As String) As Boolean
        Try
            Dim oMySqlHelper As New MySQLFactory.MySQLDBHelper
            Dim arParms() As MySqlParameter = New MySqlParameter(0) {}

            arParms(0) = New MySqlParameter("?sNick", sNick)
            oMySqlHelper.DBExecuteScalar(ConnectionString, CommandType.StoredProcedure, "spd_GuildApplications", arParms)
            Return True
        Catch ex As Exception
            Call errorsub(ex.ToString, "DeleteGuildApplication")
            Return False
        Finally
            oServerConfig.MySQLCalls += 1
        End Try
    End Function
End Class
