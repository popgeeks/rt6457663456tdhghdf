Imports MySql.Data.MySqlClient
Imports MySQLFactory.MySQLDBHelper

Public Class CardPacksDAL
    Public Function PurchasePack(ByVal sPlayer As String, ByVal sCardPack As String) As DataRow
        Try
            Dim oMySqlHelper As New MySQLFactory.MySQLDBHelper
            Dim arParms() As MySqlParameter = New MySqlParameter(1) {}
            Dim oDataRow As DataRow = Nothing

            arParms(0) = New MySqlParameter("?sPlayer", sPlayer)
            arParms(1) = New MySqlParameter("?sCardPack", sCardPack)
            oMySqlHelper.FillDataRow(ConnectionString, CommandType.StoredProcedure, "spi_CardPackPurchase", oDataRow, arParms)
            Return oDataRow
        Catch ex As Exception
            Call errorsub(ex.ToString, "PurchasePack")
            Return Nothing
        End Try
    End Function
End Class
