Imports MySql.Data.MySqlClient
Imports MySQLFactory.MySQLDBHelper
Imports System.Data

Namespace ServerObjects
    Public Class ServerLoad
        Inherits FMWK.BaseDAL

        Private _ConnectionString As String

        Public Sub New(ByVal sConnectionString As String)
            _ConnectionString = sConnectionString
        End Sub

        Public Function LoadCards() As DataTable
            Dim oDataTable As New DataTable

            Try
                Dim oMySqlHelper As New MySQLFactory.MySQLDBHelper

                oMySqlHelper.FillDataTable(_ConnectionString, CommandType.StoredProcedure, "sps_GetAllCards", oDataTable, "Cards")

                Return oDataTable
            Catch ex As Exception
                Return Nothing
            Finally
                oDataTable = Nothing
            End Try
        End Function
    End Class
End Namespace