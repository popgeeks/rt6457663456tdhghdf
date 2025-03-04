Imports mySQL.Data.MySqlClient
Imports MySQLFactory
Imports System.ComponentModel
Imports System.Configuration

Public Class DatabaseFunctions
    Private bError As Boolean
    Private oError As Exception

    Public Sub NewDeck(ByVal sNick As String, ByVal sDeck As String)
        Try
            Dim oMySqlHelper As New MySQLFactory.MySQLDBHelper
            Dim arParms() As MySqlParameter = New MySqlParameter(1) {}

            arParms(0) = New MySqlParameter("?nick", sNick)
            arParms(1) = New MySqlParameter("?sDeck", sDeck)
            oMySqlHelper.DBExecuteScalar(ConnectionString, CommandType.StoredProcedure, "usp_NewDeck", arParms)
        Catch ex As Exception
            Call errorsub(ex.ToString, "Newdeck")
        Finally
            Dim oChatFunctions As New ChatFunctions
            oChatFunctions.uniwarn(String.Concat(sNick, " got a new deck >> ", sDeck))
        End Try
    End Sub

    Public Sub NewDeckClearStats(ByVal sNick As String)
        Try
            Dim oMySqlHelper As New MySQLFactory.MySQLDBHelper
            Dim arParms() As MySqlParameter = New MySqlParameter(0) {}

            arParms(0) = New MySqlParameter("?sNick", sNick)
            oMySqlHelper.DBExecuteScalar(ConnectionString, CommandType.StoredProcedure, "spt_NewDeckClearStats", arParms)
        Catch ex As Exception
            Call errorsub(ex.ToString, "NewDeckClearStats")
        End Try
    End Sub

    'Public Sub InsertTotalPlayers()
    '    Dim oMySQLHelper As New MySQLFactory.MySQLDBHelper

    '    Dim arParms() As MySqlParameter = New MySqlParameter(1) {}
    '    arParms(0) = New MySqlParameter("?sStamp", Date.Now.ToString)
    '    arParms(1) = New MySqlParameter("?iUsers", oServerConfig.PlayersOnline)

    '    oMySQLHelper.DBExecuteScalar(ConnectionString, CommandType.StoredProcedure, "usp_totalplayers", arParms)
    'End Sub

    <Description("Adds a Record to a Player's History")> _
    Public Sub HistoryInsert(ByVal sNick As String, ByVal sType As String, ByVal sDescription As String)
        Try
            Dim oMySqlHelper As New MySQLFactory.MySQLDBHelper

            Dim arParms() As MySqlParameter = New MySqlParameter(2) {}
            arParms(0) = New MySqlParameter("?player", sNick)
            arParms(1) = New MySqlParameter("?type", sType)
            arParms(2) = New MySqlParameter("?description", sDescription)

            oMySqlHelper.DBExecuteScalar(ConnectionString, CommandType.StoredProcedure, "usp_HistoryAdd", arParms)
        Catch ex As Exception
            ErrorDescription = ex
            ErrorFlag = True
        End Try
    End Sub

    Public Sub SendEmail(ByVal sConnectionString As String, ByVal sEmail As String, ByVal sSubject As String, ByVal sBody As String)
        Try
            Dim oMySqlHelper As New MySQLFactory.MySQLDBHelper

            Dim arParms() As MySqlParameter = New MySqlParameter(2) {}
            arParms(0) = New MySqlParameter("?sEmail", sEmail)
            arParms(1) = New MySqlParameter("?sSubject", sSubject)
            arParms(2) = New MySqlParameter("?sBody", sBody)

            oMySqlHelper.DBExecuteScalar(ConnectionString, CommandType.StoredProcedure, "spi_emails", arParms)
        Catch ex As Exception
            ErrorDescription = ex
            ErrorFlag = True
        End Try
    End Sub

    Public Sub AccountUpdate(ByVal sConnectionString As String, ByVal sNick As String, ByVal sOption As String, ByVal sValue As String)
        Try
            Dim oMySqlHelper As New MySQLFactory.MySQLDBHelper

            oMySqlHelper.DBExecuteScalar(ConnectionString, CommandType.Text, String.Concat("UPDATE accounts SET ", sOption, " = '", sValue, "' WHERE player = '", sNick & "'"))
        Catch ex As Exception
            ErrorDescription = ex
            ErrorFlag = True
        End Try
    End Sub

    'Public Function PlayerListGold() As DataTable
    '    Try
    '        Dim oMySqlHelper As New MySQLFactory.MySQLDBHelper
    '        Dim oDataTable As DataTable = Nothing

    '        oMySqlHelper.FillDataTable(ConnectionString, CommandType.StoredProcedure, "sps_AccountsGold", oDataTable, "Results")

    '        If oDataTable Is Nothing Then
    '            Throw New Exception
    '        Else
    '            Return oDataTable
    '        End If
    '    Catch ex As Exception
    '        ErrorDescription = ex
    '        ErrorFlag = True
    '        Return Nothing
    '    End Try
    'End Function

    Public Function PlayerStats() As DataTable
        Try
            Dim oMySqlHelper As New MySQLFactory.MySQLDBHelper
            Dim oDataTable As DataTable = Nothing

            oMySqlHelper.FillDataTable(ConnectionString, CommandType.StoredProcedure, "sps_AccountStats", oDataTable, "Results")

            If oDataTable Is Nothing Then
                Throw New Exception
            Else
                Return oDataTable
            End If
        Catch ex As Exception
            ErrorDescription = ex
            ErrorFlag = True
            Return Nothing
        End Try
    End Function

    Public Function OnlinePlayers() As DataTable
        Try
            Dim oMySqlHelper As New MySQLFactory.MySQLDBHelper
            Dim oDataTable As DataTable = Nothing

            oMySqlHelper.FillDataTable(ConnectionString, CommandType.StoredProcedure, "sps_onlineplayers", oDataTable, "OnlinePlayers")

            If oDataTable Is Nothing Then
                Throw New Exception
            Else
                Return oDataTable
            End If
        Catch ex As Exception
            ErrorDescription = ex
            ErrorFlag = True
            Return Nothing
        End Try
    End Function

    Public Sub AccountUpdate(ByVal sConnectionString As String, ByVal sNick As String, ByVal sOption As String, ByVal iValue As Integer)
        Try
            Dim oMySqlHelper As New MySQLFactory.MySQLDBHelper

            oMySqlHelper.DBExecuteScalar(ConnectionString, CommandType.Text, String.Concat("UPDATE accounts SET ", sOption, " = ", iValue, " WHERE player = '", sNick & "'"))
        Catch ex As Exception
            ErrorDescription = ex
            ErrorFlag = True
        End Try
    End Sub

    Public Sub ConfigUpdate(ByVal sConnectionString As String, ByVal sOption As String, ByVal sValue As String)
        Try
            Dim oMySqlHelper As New MySQLFactory.MySQLDBHelper

            oMySqlHelper.DBExecuteScalar(ConnectionString, CommandType.Text, String.Concat("UPDATE config SET ", sOption, " = '", sValue, "'"))
        Catch ex As Exception
            ErrorDescription = ex
            ErrorFlag = True
        End Try
    End Sub

    Public Sub ConfigUpdate(ByVal sConnectionString As String, ByVal sOption As String, ByVal sValue As Integer)
        Try
            Dim oMySqlHelper As New MySQLFactory.MySQLDBHelper

            oMySqlHelper.DBExecuteScalar(ConnectionString, CommandType.Text, String.Concat("UPDATE config SET ", sOption, " = ", sValue))
        Catch ex As Exception
            ErrorDescription = ex
            ErrorFlag = True
        End Try
    End Sub

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
End Class
