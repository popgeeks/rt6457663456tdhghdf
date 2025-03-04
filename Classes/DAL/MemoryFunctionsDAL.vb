Imports mySQL.Data.MySqlClient
Imports MySQLFactory
Imports System.ComponentModel
Imports System.Configuration

Public Class MemoryFunctionsDAL
    Private bError As Boolean
    Private oError As Exception

    Private iGameID As Integer
    Private sPlayer(1) As String
    Private iPlayer1_Score As Integer
    Private iPlayer2_Score As Integer
    Private sCard(23) As String
    Private iPlayer1_Spot(1) As Integer
    Private iPlayer2_Spot(1) As Integer
    Private dStartDate As DateTime
    Private dLastActivity As DateTime
    Private iSurrender As Integer
    Private dTimeOut As DateTime

    Public ReadOnly Property TimeoutDate() As Date
        Get
            Return dTimeOut
        End Get
    End Property

    Public ReadOnly Property GameLength() As Integer
        Get
            Return DateDiff(DateInterval.Second, dStartDate, dLastActivity)
        End Get
    End Property

    Public ReadOnly Property Winner() As Integer
        Get
            If Player1_Score > Player2_Score Then
                Return 1
            ElseIf Player2_Score > Player1_Score Then
                Return 2
            Else
                Return 0
            End If
        End Get
    End Property

    <Description("Adds a Record to Game History/Table")> _
    Public Sub GameInsert(ByVal iGameID As Integer, ByVal sPlayer1 As String, ByVal sPlayer2 As String)
        Try
            Dim oMySqlHelper As New MySQLFactory.MySQLDBHelper

            Dim arParms() As MySqlParameter = New MySqlParameter(2) {}
            arParms(0) = New MySqlParameter("?iGameID", iGameID)
            arParms(1) = New MySqlParameter("?sPlayer1", sPlayer1)
            arParms(2) = New MySqlParameter("?sPlayer2", sPlayer2)

            oMySqlHelper.DBExecuteScalar(ConnectionString, CommandType.StoredProcedure, "spi_memorygame", arParms)
        Catch ex As Exception
            ErrorDescription = ex
            ErrorFlag = True
        Finally
            oServerConfig.MySQLCalls += 1
        End Try
    End Sub

    <Description("Gets the cards for the memory board")> _
    Public Function RandomMemoryCards() As DataTable
        Try
            Dim oMySqlHelper As New MySQLFactory.MySQLDBHelper
            Dim oDataTable As DataTable = Nothing

            oMySqlHelper.FillDataTable(ConnectionString, CommandType.StoredProcedure, "sps_memoryrandomcards", oDataTable, "Results")

            If oDataTable Is Nothing Then
                Throw New Exception
            Else
                Return oDataTable
            End If
        Catch ex As Exception
            ErrorDescription = ex
            ErrorFlag = True
            Return Nothing
        Finally
            oServerConfig.MySQLCalls += 1
        End Try
    End Function
    ''' <summary>
    ''' Updates the Memory board record with new data
    ''' </summary>
    ''' <param name="iGameID">The GameID that is being played</param>
    ''' <param name="iCards">The Board (index) spot integer index 0-23</param>
    ''' <remarks></remarks>
    Public Sub UpdateCardSlots(ByVal iGameID As Integer, ByVal iCards() As Integer)
        Try
            Dim oMySqlHelper As New MySQLFactory.MySQLDBHelper
            Dim arParms() As MySqlParameter = New MySqlParameter(24) {}
            arParms(0) = New MySqlParameter("?iGameID", iGameID)

            For x As Integer = 0 To 23
                arParms(x + 1) = New MySqlParameter(String.Concat("?iBoard", x.ToString), iCards(x))
            Next

            oMySqlHelper.DBExecuteScalar(ConnectionString, CommandType.StoredProcedure, "spu_memoryboard", arParms)
        Catch ex As Exception
            ErrorDescription = ex
            ErrorFlag = True
        Finally
            oServerConfig.MySQLCalls += 1
        End Try
    End Sub

    <Description("Loads the Game Record")> _
    Protected Sub LoadRecord(ByVal iGameID As Integer)
        Try
            Dim oMySqlHelper As New MySQLFactory.MySQLDBHelper
            Dim oDataRow As DataRow = Nothing

            Dim arParms() As MySqlParameter = New MySqlParameter(0) {}
            arParms(0) = New MySqlParameter("?iGameID", iGameID)

            oMySqlHelper.FillDataRow(ConnectionString, CommandType.StoredProcedure, "sps_memorygame", oDataRow, arParms)

            If oDataRow Is Nothing Then
                Throw New Exception
            Else
                With oDataRow
                    sPlayer(0) = .Item("memory_player1").ToString
                    sPlayer(1) = .Item("memory_player2").ToString

                    iPlayer1_Score = .Item("memory_p1score")
                    iPlayer2_Score = .Item("memory_p2score")

                    For x As Integer = 0 To 23
                        sCard(x) = .Item(String.Concat("memory_board", x)).ToString
                    Next

                    iPlayer1_Spot(0) = .Item("memory_p1spot_1")
                    iPlayer1_Spot(1) = .Item("memory_p1spot_2")

                    iPlayer2_Spot(0) = .Item("memory_p2spot_1")
                    iPlayer2_Spot(1) = .Item("memory_p2spot_2")

                    'dStartDate = .Item("memory_date")
                    'dLastActivity = .Item("memory_lastactivity")
                    iSurrender = .Item("memory_surrender")
                    If IsDBNull(.Item("Timeout")) = False Then dTimeOut = .Item("Timeout")
                End With
            End If
        Catch ex As Exception
            ErrorDescription = ex
            ErrorFlag = True
        Finally
            oServerConfig.MySQLCalls += 1
        End Try
    End Sub

    Public Property Player1() As String
        Get
            Return sPlayer(0)
        End Get

        Set(ByVal value As String)
            sPlayer(0) = value
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

    Public Property Player1_Score() As Integer
        Get
            Return iPlayer1_Score
        End Get

        Set(ByVal value As Integer)
            iPlayer1_Score = value
        End Set
    End Property

    Public Property Player2_Score() As Integer
        Get
            Return iPlayer2_Score
        End Get

        Set(ByVal value As Integer)
            iPlayer2_Score = value
        End Set
    End Property

    Public Property MemoryCard(ByVal iSpot As Integer) As String
        Get
            Return sCard(iSpot)
        End Get

        Set(ByVal value As String)
            sCard(iSpot) = value
        End Set
    End Property

    Public Property Player_Spot(ByVal iPlayerID As Integer, ByVal iSpot As Integer) As Integer
        Get
            If iPlayerID = 1 Then
                Return iPlayer1_Spot(iSpot - 1)
            ElseIf iPlayerID = 2 Then
                Return iPlayer2_Spot(iSpot - 1)
            End If
        End Get

        Set(ByVal value As Integer)
            If iPlayerID = 1 Then
                iPlayer1_Spot(iSpot - 1) = value
            ElseIf iPlayerID = 2 Then
                iPlayer2_Spot(iSpot - 1) = value
            End If
        End Set
    End Property

    Public ReadOnly Property StartDate() As Date
        Get
            Return dStartDate
        End Get
    End Property

    Public ReadOnly Property LastActivity() As Date
        Get
            Return dLastActivity
        End Get
    End Property

    Public Property Surrender() As Integer
        Get
            Return iSurrender
        End Get

        Set(ByVal value As Integer)
            iSurrender = value
        End Set
    End Property

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

    Public ReadOnly Property GameID() As Integer
        Get
            Return iGameID
        End Get
    End Property
    '(iGameID INT, sPlayer1 VARCHAR(15), sPlayer2 VARCHAR(15), iScoreP1 INT, iScoreP2 INT, iP1Spot1 INT, iP1Spot2 INT, iP2Spot1 INT, iP2Spot2 INT)
    Public Sub Update()
        Try
            Dim oMySqlHelper As New MySQLFactory.MySQLDBHelper
            Dim oDataRow As DataRow = Nothing

            Dim arParms() As MySqlParameter = New MySqlParameter(9) {}
            arParms(0) = New MySqlParameter("?iGameID", GameID)
            arParms(1) = New MySqlParameter("?sPlayer1", Player1)
            arParms(2) = New MySqlParameter("?sPlayer2", Player2)
            arParms(3) = New MySqlParameter("?iScoreP1", Player1_Score)
            arParms(4) = New MySqlParameter("?iScoreP2", Player2_Score)
            arParms(5) = New MySqlParameter("?iP1Spot1", Player_Spot(1, 1))
            arParms(6) = New MySqlParameter("?iP1Spot2", Player_Spot(1, 2))
            arParms(7) = New MySqlParameter("?iP2Spot1", Player_Spot(2, 1))
            arParms(8) = New MySqlParameter("?iP2Spot2", Player_Spot(2, 2))
            arParms(9) = New MySqlParameter("?iSurrender", Surrender)

            oMySqlHelper.DBExecuteScalar(ConnectionString, CommandType.StoredProcedure, "spu_MemoryRecord", arParms)
        Catch ex As Exception
            ErrorDescription = ex
            ErrorFlag = True
        Finally
            oServerConfig.MySQLCalls += 1
        End Try
    End Sub

    Public Sub HistoryInsert(ByVal iSpot1 As Integer, ByVal iSpot2 As Integer, ByVal sResult As String)
        Dim oMySqlHelper As New MySQLFactory.MySQLDBHelper

        Dim arParms() As MySqlParameter = New MySqlParameter(3) {}
        arParms(0) = New MySqlParameter("?iGameID", GameID)
        arParms(1) = New MySqlParameter("?iSpot1", iSpot1)
        arParms(2) = New MySqlParameter("?iSpot2", iSpot2)
        arParms(3) = New MySqlParameter("?sResult", sResult)

        oMySqlHelper.DBExecuteScalar(ConnectionString, CommandType.StoredProcedure, "spi_MemoryHistory", arParms)

        oServerConfig.MySQLCalls += 1
    End Sub

    Public Function IsGameOver() As Boolean
        Dim oMySqlHelper As New MySQLFactory.MySQLDBHelper
        Dim oDataSet As New DataSet
        Dim iBoard(0 To 23) As Boolean

        Dim arParms() As MySqlParameter = New MySqlParameter(0) {}
        arParms(0) = New MySqlParameter("?iGameID", GameID)

        oMySqlHelper.FillDataSet(ConnectionString, CommandType.StoredProcedure, "sps_Memory_CapturedCards", oDataSet, New String() {"Results"}, arParms)

        If oDataSet Is Nothing Then
            Return False
        Else
            If oDataSet.Tables(0).Rows.Count > 0 Then
                For x As Integer = 0 To oDataSet.Tables(0).Rows.Count - 1
                    CapturedCards(iBoard, oDataSet.Tables(0).Rows(x).Item("S1"))
                    CapturedCards(iBoard, oDataSet.Tables(0).Rows(x).Item("S2"))
                Next

                For x As Integer = 0 To 23
                    If iBoard(x) = False Then Return False
                Next

                Return True
            Else
                Return False
            End If
        End If

        oServerConfig.MySQLCalls += 1
    End Function

    Protected Sub CapturedCards(ByRef iBoard() As Boolean, ByVal iSpot As Integer)
        iBoard(iSpot) = True
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


    Public Sub dalNew(ByVal iGamePK As Integer)
        iGameID = iGamePK
        LoadRecord(iGamePK)
    End Sub
End Class
