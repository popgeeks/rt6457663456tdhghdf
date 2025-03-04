Imports mySQL.Data.MySqlClient
Imports MySQLFactory
Imports System.ComponentModel
Imports System.Configuration

Public Class TradesDAL
    Private bError As Boolean
    Private oError As Exception

    Private sPlayer(1) As String
    Private sPlayer1_Cards(4) As String
    Private sPlayer2_Cards(4) As String

    Private iPlayer1_Ready As Boolean
    Private iPlayer2_Ready As Boolean

    Private iPlayer1_Socket As Integer
    Private iPlayer2_Socket As Integer

    Private iGameID As Integer
    Private iGP(1) As Integer
    'Private dStartDate As Date
    'Private dLastActivity As Date
    'Private dDateEnded As Date
    Private iCompleted As Integer

    Public Sub Initialize()
        sPlayer(0) = String.Empty
        sPlayer(1) = String.Empty

        For x As Integer = 0 To 4
            sPlayer1_Cards(x) = String.Empty
            sPlayer2_Cards(x) = String.Empty
        Next x

        iPlayer1_Socket = 0
        iPlayer2_Socket = 0

        iGameID = 0
        iGP(0) = 0
        iGP(1) = 0
        iCompleted = 0
    End Sub

    Public ReadOnly Property PlayerID(ByVal sNick As String) As Integer
        Get
            If sNick = sPlayer(0) Then
                Return 1
            ElseIf sNick = sPlayer(1) Then
                Return 2
            Else
                Return -1
            End If
        End Get
    End Property

    Public ReadOnly Property PlayerNick(ByVal iPlayerID As Integer) As String
        Get
            If iPlayerID < 1 Then
                Return Nothing
            Else
                Return sPlayer(iPlayerID - 1)
            End If
        End Get
    End Property

    Public Property PlayerSocket(ByVal iPlayerID As Integer) As Integer
        Get
            Select Case iPlayerID
                Case 1
                    Return iPlayer1_Socket
                Case 2
                    Return iPlayer2_Socket
                Case Else
                    Return 0
            End Select
        End Get

        Set(ByVal value As Integer)
            Select Case iPlayerID
                Case 1
                    iPlayer1_Socket = value
                Case 2
                    iPlayer2_Socket = value
            End Select
        End Set
    End Property

    Public Property GameID() As Integer
        Get
            Return iGameID
        End Get

        Set(ByVal value As Integer)
            iGameID = value
        End Set
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

    Public Property Player1() As String
        Get
            Return sPlayer(0)
        End Get

        Set(ByVal value As String)
            sPlayer(0) = value
        End Set
    End Property

    Public Property Player_Ready(ByVal iPlayerID As Integer) As Boolean
        Get
            If iPlayerID = 1 Then
                Return iPlayer1_Ready
            Else
                Return iPlayer2_Ready
            End If
        End Get

        Set(ByVal value As Boolean)
            If iPlayerID = 1 Then
                iPlayer1_Ready = value
            Else
                iPlayer2_Ready = value
            End If
        End Set
    End Property

    Public Property Player2() As String
        Get
            Return sPlayer(1)
        End Get

        Set(ByVal value As String)
            sPlayer(1) = value
        End Set
    End Property

    Public Property Player_Hand(ByVal iPlayerID As Integer, ByVal iSpot As Integer) As String
        Get
            If iPlayerID = 1 Then
                Return sPlayer1_Cards(iSpot)
            ElseIf iPlayerID = 2 Then
                Return sPlayer2_Cards(iSpot)
            Else
                Return Nothing
            End If
        End Get

        Set(ByVal value As String)
            If iPlayerID = 1 Then
                sPlayer1_Cards(iSpot) = value
            ElseIf iPlayerID = 2 Then
                sPlayer2_Cards(iSpot) = value
            End If
        End Set
    End Property

    Public Property Player_Gold(ByVal iPlayerID As Integer) As Integer
        Get
            If iPlayerID = 1 Then
                Return iGP(iPlayerID - 1)
            ElseIf iPlayerID = 2 Then
                Return iGP(iPlayerID - 1)
            Else
                Return Nothing
            End If
        End Get

        Set(ByVal value As Integer)
            If iPlayerID = 1 Then
                iGP(iPlayerID - 1) = value
            ElseIf iPlayerID = 2 Then
                iGP(iPlayerID - 1) = value
            End If
        End Set
    End Property

    Public Property Completed() As Integer
        Get
            Return iCompleted
        End Get
        Set(ByVal value As Integer)
            iCompleted = value
        End Set
    End Property

    'Public ReadOnly Property StartDate() As Date
    '    Get
    '        Return dStartDate
    '    End Get
    'End Property

    'Public ReadOnly Property LastActivity() As Date
    '    Get
    '        Return dLastActivity
    '    End Get
    'End Property

    'Public ReadOnly Property GameEnds() As Date
    '    Get
    '        Return dDateEnded
    '    End Get
    'End Property

    Public Sub AddCard(ByVal iPlayerID As Integer, ByVal sCard As String)
        For x As Integer = 0 To 4
            If Player_Hand(iPlayerID, x) = String.Empty Then
                Player_Hand(iPlayerID, x) = sCard
                Exit For
            End If
        Next
    End Sub

    Public Sub RemoveCard(ByVal iPlayerID As Integer)
        For x As Integer = 0 To 4
            Player_Hand(iPlayerID, x) = String.Empty
        Next
    End Sub

    <Description("Loads the Game Record")> _
    Protected Sub LoadRecord()
        Try
            Dim oMySqlHelper As New MySQLFactory.MySQLDBHelper
            Dim oDataRow As DataRow = Nothing

            Dim arParms() As MySqlParameter = New MySqlParameter(0) {}
            arParms(0) = New MySqlParameter("?iGameID", GameID)

            oMySqlHelper.FillDataRow(ConnectionString, CommandType.StoredProcedure, "sps_Trades", oDataRow, arParms)

            If oDataRow Is Nothing Then
                Throw New Exception
            Else
                With oDataRow
                    sPlayer(0) = .Item("Player1").ToString
                    sPlayer(1) = .Item("Player2").ToString

                    For x As Integer = 0 To 4
                        sPlayer1_Cards(x) = .Item(String.Concat("plr1_card", x)).ToString
                    Next

                    For x As Integer = 0 To 4
                        sPlayer2_Cards(x) = .Item(String.Concat("plr2_card", x)).ToString
                    Next

                    iGP(0) = .Item("plr1_gp")
                    iGP(1) = .Item("plr2_gp")
                    iPlayer1_Socket = .Item("P1Socket")
                    iPlayer2_Socket = .Item("P2Socket")

                    iPlayer1_Ready = CBool(.Item("player1_ready"))
                    iPlayer2_Ready = CBool(.Item("player2_ready"))

                    'If IsDBNull(.Item("started_date")) = False Then dStartDate = .Item("started_date")
                    'If IsDBNull(.Item("lastactivity")) = False Then dLastActivity = .Item("lastactivity")
                    'If IsDBNull(.Item("date_ended")) = False Then dDateEnded = .Item("date_ended")

                    iCompleted = .Item("Completed")
                End With
            End If
        Catch ex As Exception
            ErrorDescription = ex
            ErrorFlag = True
            Call errorsub(ex.ToString, "LoadRecord")
        End Try
    End Sub

    <Description("Adds a Record to Trade History/Table")> _
   Protected Sub GameInsert()
        Try
            Dim oMySqlHelper As New MySQLFactory.MySQLDBHelper

            Dim arParms() As MySqlParameter = New MySqlParameter(2) {}
            arParms(0) = New MySqlParameter("?iGameID", GameID)
            arParms(1) = New MySqlParameter("?sPlayer1", Player1)
            arParms(2) = New MySqlParameter("?sPlayer2", Player2)

            oMySqlHelper.DBExecuteScalar(ConnectionString, CommandType.StoredProcedure, "spi_Trades", arParms)
        Catch ex As Exception
            ErrorDescription = ex
            ErrorFlag = True
        End Try
    End Sub

    Protected Sub Update()
        Try
            Dim oMySqlHelper As New MySQLFactory.MySQLDBHelper
            Dim oDataRow As DataRow = Nothing

            Dim arParms() As MySqlParameter = New MySqlParameter(15) {}
            arParms(0) = New MySqlParameter("?iGameID", GameID)

            For x As Integer = 0 To 4
                arParms(x + 1) = New MySqlParameter(String.Concat("?sPlr1_Card", x), Player_Hand(1, x))
            Next x

            For x As Integer = 0 To 4
                arParms(x + 6) = New MySqlParameter(String.Concat("?sPlr2_Card", x), Player_Hand(2, x))
            Next x

            arParms(11) = New MySqlParameter("?iPlr1_GP", Player_Gold(1))
            arParms(12) = New MySqlParameter("?iPlr2_GP", Player_Gold(2))
            arParms(13) = New MySqlParameter("?iCompleted", Completed)
            arParms(14) = New MySqlParameter("?iP1Ready", iPlayer1_Ready)
            arParms(15) = New MySqlParameter("?iP2Ready", iPlayer2_Ready)

            oMySqlHelper.DBExecuteScalar(ConnectionString, CommandType.StoredProcedure, "spu_Trades", arParms)
        Catch ex As Exception
            ErrorDescription = ex
            ErrorFlag = True
        End Try
    End Sub

    Protected Function CompleteTrade() As Boolean
        Try
            Dim iSucceed As Integer = 0

            Dim oMySqlHelper As New MySQLFactory.MySQLDBHelper
            Dim oDataRow As DataRow = Nothing

            Dim arParms() As MySqlParameter = New MySqlParameter(0) {}
            arParms(0) = New MySqlParameter("?iGameID", GameID)

            oMySqlHelper.DBExecuteScalar(ConnectionString, CommandType.StoredProcedure, "spt_CompleteTrade", arParms)

            If iSucceed <> -1 Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            ErrorDescription = ex
            ErrorFlag = True
            Return False
        End Try
    End Function

    Public Sub HistoryInsert(ByVal sResult As String)
        Try
            Dim oMySqlHelper As New MySQLFactory.MySQLDBHelper

            Dim arParms() As MySqlParameter = New MySqlParameter(1) {}
            arParms(0) = New MySqlParameter("?iGameID", GameID)
            arParms(1) = New MySqlParameter("?sResult", sResult)

            oMySqlHelper.DBExecuteScalar(ConnectionString, CommandType.StoredProcedure, "spi_TradeHistory", arParms)
        Catch ex As Exception
            Call errorsub(ex.ToString, "HistoryInsert")
        End Try
    End Sub
End Class
