Imports MySql.Data.MySqlClient
Imports MySQLFactory.MySQLDBHelper
Imports System.ComponentModel

Public Class PandorasMysteryDAL
    Private iPot As Integer
    Private iFee As Integer

    Private bError As Boolean
    Private oError As Exception

    Public ReadOnly Property Pot() As Integer
        Get
            Return iPot
        End Get
    End Property

    Public ReadOnly Property Fee() As Integer
        Get
            Return iFee
        End Get
    End Property

    <Description("Gets or Sets Exception Information from Retrieving Data")> _
    Public Property ErrorDescription() As Exception
        Get
            Return oError
        End Get
        Set(ByVal Value As Exception)
            oError = Value
        End Set
    End Property

    <Description("Gets or Sets the Error Flag if Exception Information Exists")> _
    Public Property ErrorFlag() As Boolean
        Get
            Return bError
        End Get
        Set(ByVal Value As Boolean)
            bError = Value
        End Set
    End Property

    Protected Sub Load()
        Dim oDataRow As DataRow = Nothing
        Dim oMySQLHelper As New MySQLFactory.MySQLDBHelper

        Try
            oMySQLHelper.FillDataRow(ConnectionString, CommandType.StoredProcedure, "sps_PMPot", oDataRow)

            If Not (oDataRow Is Nothing) Then
                With oDataRow
                    iPot = .Item("Pot")
                    iFee = .Item("Fee")
                End With
            Else
                ClearAll()
            End If
        Catch ex As Exception
            ErrorDescription = ex
            ErrorFlag = True
        Finally
            oMySQLHelper = Nothing
            oDataRow = Nothing
        End Try
    End Sub

    Public Sub ResetPandorasBox()
        Try
            Dim oMySQLHelper As New MySQLFactory.MySQLDBHelper

            oMySQLHelper.DBExecuteScalar(ConnectionString, CommandType.StoredProcedure, "usp_PandorasBox")
        Catch ex As Exception
            Call errorsub(ex.ToString, "ResetPandorasBox")
        End Try
    End Sub

    Protected Sub UpdatePMPot()
        Try
            Dim oMySQLHelper As New MySQLFactory.MySQLDBHelper

            oMySQLHelper.DBExecuteScalar(ConnectionString, CommandType.StoredProcedure, "spu_UpdatePMPot")
        Catch ex As Exception
            Call errorsub(ex.ToString, "UpdatePMPot")
        End Try
    End Sub

    Protected Sub InsertPandoraGame(ByVal iID As Integer, ByVal sNick As String, ByVal iUp As Integer, ByVal iDown As Integer, ByVal iLeft As Integer, ByVal iRight As Integer, ByVal sOutcome As String)
        Dim oMySQLHelper As New MySQLFactory.MySQLDBHelper

        Try
            Dim arParms() As MySqlParameter = New MySqlParameter(6) {}

            arParms(0) = New MySqlParameter("?iID", iID)
            arParms(1) = New MySqlParameter("?sNick", sNick)
            arParms(2) = New MySqlParameter("?iUp", iUp)
            arParms(3) = New MySqlParameter("?iDown", iDown)
            arParms(4) = New MySqlParameter("?iLeft", iLeft)
            arParms(5) = New MySqlParameter("?iRight", iRight)
            arParms(6) = New MySqlParameter("?sOutcome", sOutcome)

            oMySQLHelper.DBExecuteScalar(ConnectionString, CommandType.StoredProcedure, "spi_PandoraGames", arParms)
        Catch ex As Exception
            ErrorDescription = ex
            ErrorFlag = True
        Finally
            oMySQLHelper = Nothing
        End Try
    End Sub

    Protected Sub ResetPot()
        Dim oMySQLHelper As New MySQLFactory.MySQLDBHelper

        Try
            oMySQLHelper.DBExecuteScalar(ConnectionString, CommandType.StoredProcedure, "spu_ResetPot")
        Catch ex As Exception
            ErrorDescription = ex
            ErrorFlag = True
        Finally
            oMySQLHelper = Nothing
        End Try
    End Sub

    Private Sub ClearAll()
        iPot = 0
        iFee = 0
    End Sub
End Class
