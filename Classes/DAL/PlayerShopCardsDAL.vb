Imports MySql.Data.MySqlClient
Imports MySQLFactory.MySQLDBHelper

Public Class PlayerShopCardsDAL
    Protected Function GetPlayerShopCards(ByVal sCard As String, ByVal sNick As String, ByVal sPrice As String) As DataRow
        Try
            Dim oMySqlHelper As New MySQLFactory.MySQLDBHelper
            Dim arParms() As MySqlParameter = New MySqlParameter(2) {}
            Dim oDataRow As DataRow = Nothing

            arParms(0) = New MySqlParameter("?sCard", sCard)
            arParms(1) = New MySqlParameter("?sNick", sNick)
            arParms(2) = New MySqlParameter("?sPrice", sPrice)
            oMySqlHelper.FillDataRow(ConnectionString, CommandType.StoredProcedure, "usp_PlayerShopCards", oDataRow, arParms)
            Return oDataRow
        Catch ex As Exception
            'Call errorsub(ex.ToString, "GetPackInfo")
            Return Nothing
        End Try
    End Function

    Protected Function DeletePlayerShopCards(ByVal iID As Integer) As Boolean
        Try
            Dim oMySqlHelper As New MySQLFactory.MySQLDBHelper
            Dim arParms() As MySqlParameter = New MySqlParameter(0) {}

            arParms(0) = New MySqlParameter("?iID", iID)
            oMySqlHelper.DBExecuteScalar(ConnectionString, CommandType.StoredProcedure, "spd_PlayerShopCard", arParms)
            Return True
        Catch ex As Exception
            Call errorsub(ex.ToString, "DeletePlayerShopCards")
            Return False
        End Try
    End Function

    Protected Function InsertPlayerShopTransactions(ByVal sNick As String, ByVal sBuyer As String, ByVal sCard As String, ByVal sPrice As String) As Boolean
        Try
            Dim oMySqlHelper As New MySQLFactory.MySQLDBHelper
            Dim arParms() As MySqlParameter = New MySqlParameter(3) {}

            arParms(0) = New MySqlParameter("?sShopOwner", sNick)
            arParms(1) = New MySqlParameter("?sBuyer", sBuyer)
            arParms(2) = New MySqlParameter("?sCardName", sCard)
            arParms(3) = New MySqlParameter("?sPrice", sPrice)

            oMySqlHelper.DBExecuteScalar(ConnectionString, CommandType.StoredProcedure, "spi_PlayerShopTransactions", arParms)
            Return True
        Catch ex As Exception
            Call errorsub(ex.ToString, "DeletePlayerShopCards")
            Return False
        End Try
    End Function
End Class
