Imports MySql.Data.MySqlClient
Imports MySQLFactory.MySQLDBHelper
Imports System.Data

Namespace GameFunctions
    Public Class Tables
        Inherits FMWK.BaseDAL

        Private _ConnectionString As String

        Public Sub New(ByVal sConnectionString As String)
            _ConnectionString = sConnectionString
        End Sub

        Public Function TableWagers(ByVal sWinner As String, ByVal sLoser As String, ByVal iWager As Integer, ByVal iGameID As Integer) As Integer
            Try
                Dim oMySqlHelper As New MySQLFactory.MySQLDBHelper
                Dim oDataRow As DataRow = Nothing
                Dim arParms() As MySqlParameter = New MySqlParameter(3) {}

                arParms(0) = New MySqlParameter("?sWinner", sWinner)
                arParms(1) = New MySqlParameter("?sLoser", sLoser)
                arParms(2) = New MySqlParameter("?iWager", iWager)
                arParms(3) = New MySqlParameter("?iGameID", iGameID)

                oMySqlHelper.FillDataRow(_ConnectionString, CommandType.StoredProcedure, "spi_TableWager", oDataRow, arParms)

                Return Val(oDataRow.Item(0).ToString)
            Catch ex As Exception
                Dim oError As New ErrorLogging.ErrorHandling()
                oError.LogErrorToEvents(ex.ToString, "TableWagers")
                Return "0"
            End Try
        End Function
    End Class
End Namespace