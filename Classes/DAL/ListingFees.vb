Imports MySql.Data.MySqlClient
Imports MySQLFactory.MySQLDBHelper

Public Class ListingFees
    Private iListingFee As Integer
    Private sFinalFee As String

    Public Property ListingFee() As Integer
        Get
            Return iListingFee
        End Get
        Set(ByVal value As Integer)
            iListingFee = value
        End Set
    End Property

    Public Property FinalFee() As String
        Get
            Return sFinalFee
        End Get
        Set(ByVal value As String)
            sFinalFee = value
        End Set
    End Property

    Public Sub New()
        Try
            Dim oMySqlHelper As New MySQLFactory.MySQLDBHelper
            Dim oDataRow As DataRow = Nothing

            oMySqlHelper.FillDataRow(ConnectionString, CommandType.StoredProcedure, "sps_ListingFees", oDataRow)

            If Not (oDataRow Is Nothing) Then
                ListingFee = oDataRow.Item("AuctionListingFee")
                FinalFee = oDataRow.Item("FinalListingFee")
            End If
        Catch ex As Exception
            Call errorsub(ex.ToString, "ListingFees")
            ListingFee = 9999
            FinalFee = 9999
        Finally
            oServerConfig.MySQLCalls += 1
        End Try
    End Sub
End Class
