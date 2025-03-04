Imports MySql.Data.MySqlClient
Imports MySQLFactory.MySQLDBHelper

Public Class AuctionsDAL
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

    Protected Function PlayerAuctionsTopBid(ByVal iKey As Integer) As DataRow
        Try
            Dim oMySqlHelper As New MySQLFactory.MySQLDBHelper
            Dim arParms() As MySqlParameter = New MySqlParameter(0) {}
            Dim oDataRow As DataRow = Nothing

            arParms(0) = New MySqlParameter("?iKey", iKey)
            oMySqlHelper.FillDataRow(ConnectionString, CommandType.StoredProcedure, "sps_PlayerAuctionsTopBid", oDataRow, arParms)
            Return oDataRow
        Catch ex As Exception
            'Call errorsub(ex.ToString, "PlayerAuctionsTopBid")
            Return Nothing
        Finally
            oServerConfig.MySQLCalls += 1
        End Try
    End Function

    Protected Function PlayerAuctionsList(ByVal iKey As Integer) As DataRow
        Try
            Dim oMySqlHelper As New MySQLFactory.MySQLDBHelper
            Dim arParms() As MySqlParameter = New MySqlParameter(0) {}
            Dim oDataRow As DataRow = Nothing

            arParms(0) = New MySqlParameter("?iKey", iKey)
            oMySqlHelper.FillDataRow(ConnectionString, CommandType.StoredProcedure, "sps_PlayerAuctionsListing", oDataRow, arParms)
            Return oDataRow
        Catch ex As Exception
            Call errorsub(ex.ToString, "PlayerAuctionsList")
            Return Nothing
        Finally
            oServerConfig.MySQLCalls += 1
        End Try
    End Function

    Protected Function InsertPlayerAuction(ByVal sNick As String, ByVal sCard As String, ByVal iCost As Integer, ByVal iDays As Integer) As Boolean
        Try
            Dim oMySqlHelper As New MySQLFactory.MySQLDBHelper
            Dim arParms() As MySqlParameter = New MySqlParameter(4) {}

            arParms(0) = New MySqlParameter("?sNick", sNick)
            arParms(1) = New MySqlParameter("?sCard", sCard)
            arParms(2) = New MySqlParameter("?iCost", iCost)
            arParms(3) = New MySqlParameter("?iBuyNow", 0)
            arParms(4) = New MySqlParameter("?iDays", iDays)
            oMySqlHelper.DBExecuteScalar(ConnectionString, CommandType.StoredProcedure, "spi_PlayerAuction", arParms)
            Return True
        Catch ex As Exception
            Call errorsub(ex.ToString, "InsertPlayerAuction")
            Return False
        Finally
            oServerConfig.MySQLCalls += 1
        End Try
    End Function

    Protected Function InsertPlayerAuctionBid(ByVal iKey As Integer, ByVal sBuyer As String, ByVal iBid As Integer) As Boolean
        Try
            Dim oMySqlHelper As New MySQLFactory.MySQLDBHelper
            Dim arParms() As MySqlParameter = New MySqlParameter(2) {}

            arParms(0) = New MySqlParameter("?iKey", iKey)
            arParms(1) = New MySqlParameter("?sBuyer", sBuyer)
            arParms(2) = New MySqlParameter("?iBid", iBid)
            oMySqlHelper.DBExecuteScalar(ConnectionString, CommandType.StoredProcedure, "spi_PlayerAuctionsBid", arParms)
            Return True
        Catch ex As Exception
            Call errorsub(ex.ToString, "InsertPlayerAuctionBid")
            Return False
        Finally
            oServerConfig.MySQLCalls += 1
        End Try
    End Function

    Protected Function UpdateAuctionCost(ByVal iKey As Integer, ByVal iCost As Integer) As Boolean
        Try
            Dim oMySqlHelper As New MySQLFactory.MySQLDBHelper
            Dim arParms() As MySqlParameter = New MySqlParameter(1) {}

            arParms(0) = New MySqlParameter("?iKey", iKey)
            arParms(1) = New MySqlParameter("?iCost", iCost)
            oMySqlHelper.DBExecuteScalar(ConnectionString, CommandType.StoredProcedure, "spu_AuctionCost", arParms)
            Return True
        Catch ex As Exception
            Call errorsub(ex.ToString, "UpdateAuctionCost")
            Return False
        Finally
            oServerConfig.MySQLCalls += 1
        End Try
    End Function
End Class
