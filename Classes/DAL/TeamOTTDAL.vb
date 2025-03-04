Imports mySQL.Data.MySqlClient
Imports MySQLFactory
Imports System.ComponentModel
Imports System.Configuration

Public Class TeamOTTDAL
    Private bError As Boolean
    Private oError As Exception

    Private iGameID As Integer
    Private sPlayer(3) As String
    Private iTeam1_Score As Integer
    Private iTeam2_Score As Integer
    Private iPlayer1_Discards As Integer
    Private iPlayer2_Discards As Integer
    Private iPlayer3_Discards As Integer
    Private iPlayer4_Discards As Integer
    Private iPlayer1_Index As Integer
    Private iPlayer2_Index As Integer
    Private iPlayer3_Index As Integer
    Private iPlayer4_Index As Integer
    'Private iPlayer1_Socket As Integer
    'Private iPlayer2_Socket As Integer
    'Private iPlayer3_Socket As Integer
    'Private iPlayer4_Socket As Integer
    Private sBoardCards(24) As String
    Private sBoardColors(24) As String
    Private sPlayer1_Cards(7) As String
    Private sPlayer2_Cards(7) As String
    Private sPlayer3_Cards(7) As String
    Private sPlayer4_Cards(7) As String
    Private dStartDate As DateTime
    Private dLastActivity As DateTime
    Private dGameEnds As DateTime
    Private iSurrender As Integer
    Private sType As String
    Private iBlocks As Integer
    Private iMaxLevel As Integer

    Protected Function Omni_NextCard(ByVal iPlayerID As Integer, ByVal blnAdvance As Boolean) As String
        Dim sCard As String = String.Empty

        Select Case iPlayerID
            Case 1
                If blnAdvance = True Then
                    Player_Index(1) += 1
                    UpdateIndex()
                End If

                Return Player_Hand(1, Player_Index(1)).Replace(" ", "%20")
            Case 2
                If blnAdvance = True Then
                    Player_Index(2) += 1
                    UpdateIndex()
                End If

                Return Player_Hand(2, Player_Index(2)).Replace(" ", "%20")
            Case 3
                If blnAdvance = True Then
                    Player_Index(3) += 1
                    UpdateIndex()
                End If

                Return Player_Hand(3, Player_Index(3)).Replace(" ", "%20")
            Case 4
                If blnAdvance = True Then
                    Player_Index(4) += 1
                    UpdateIndex()
                End If

                Return Player_Hand(4, Player_Index(4)).Replace(" ", "%20")
            Case Else
                Return String.Empty
        End Select
    End Function

    Public Sub UpdateIndex()
        Try
            Dim oMySqlHelper As New MySQLFactory.MySQLDBHelper

            Dim arParms() As MySqlParameter = New MySqlParameter(4) {}
            arParms(0) = New MySqlParameter("?iGameID", GameID)
            arParms(1) = New MySqlParameter("?iPlayer1_Index", Player_Index(1))
            arParms(2) = New MySqlParameter("?iPlayer2_Index", Player_Index(2))
            arParms(3) = New MySqlParameter("?iPlayer3_Index", Player_Index(3))
            arParms(4) = New MySqlParameter("?iPlayer4_Index", Player_Index(4))

            oMySqlHelper.DBExecuteScalar(ConnectionString, CommandType.StoredProcedure, "spu_TeamOTTGame_CardIndex", arParms)
        Catch ex As Exception
            ErrorDescription = ex
            ErrorFlag = True
        Finally
            oServerConfig.MySQLCalls += 1
        End Try
    End Sub

    <Description("Adds a Record to Game History/Table")> _
    Public Sub GameInsert()
        Try
            Dim oMySqlHelper As New MySQLFactory.MySQLDBHelper

            Dim arParms() As MySqlParameter = New MySqlParameter(64) {}
            arParms(0) = New MySqlParameter("?iGameID", GameID)
            arParms(1) = New MySqlParameter("?sPlayer1", Player1)
            arParms(2) = New MySqlParameter("?sPlayer2", Player2)
            arParms(3) = New MySqlParameter("?sPlayer3", Player3)
            arParms(4) = New MySqlParameter("?sPlayer4", Player4)
            arParms(5) = New MySqlParameter("?sType", Type)
            arParms(6) = New MySqlParameter("?iBlocks", Blocks)
            arParms(7) = New MySqlParameter("?iMaxLevel", MaxLevel)

            For x As Integer = 0 To 24
                arParms(x + 8) = New MySqlParameter(String.Concat("?sBoard", x), Board(x))
            Next

            For x As Integer = 0 To 7
                arParms(x + 33) = New MySqlParameter(String.Concat("?sPlr1_Hand_", x), Player_Hand(1, x))
            Next

            For x As Integer = 0 To 7
                arParms(x + 41) = New MySqlParameter(String.Concat("?sPlr2_Hand_", x), Player_Hand(2, x))
            Next

            For x As Integer = 0 To 7
                arParms(x + 49) = New MySqlParameter(String.Concat("?sPlr3_Hand_", x), Player_Hand(3, x))
            Next

            For x As Integer = 0 To 7
                arParms(x + 57) = New MySqlParameter(String.Concat("?sPlr4_Hand_", x), Player_Hand(4, x))
            Next

            oMySqlHelper.DBExecuteScalar(ConnectionString, CommandType.StoredProcedure, "spi_TeamOTTGame", arParms)
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

            oMySqlHelper.FillDataRow(ConnectionString, CommandType.StoredProcedure, "sps_teamottgame", oDataRow, arParms)

            If oDataRow Is Nothing Then
                Throw New Exception
            Else
                With oDataRow
                    sPlayer(0) = .Item("Player1").ToString
                    sPlayer(1) = .Item("Player2").ToString
                    sPlayer(2) = .Item("Player3").ToString
                    sPlayer(3) = .Item("Player4").ToString

                    iTeam1_Score = .Item("Team1_Score")
                    iTeam2_Score = .Item("Team2_Score")

                    iPlayer1_Index = .Item("Player1_Index")
                    iPlayer2_Index = .Item("Player2_Index")
                    iPlayer3_Index = .Item("Player3_Index")
                    iPlayer4_Index = .Item("Player4_Index")

                    iPlayer1_Discards = .Item("Player1_Discards")
                    iPlayer2_Discards = .Item("Player2_Discards")
                    iPlayer3_Discards = .Item("Player3_Discards")
                    iPlayer4_Discards = .Item("Player4_Discards")

                    sType = .Item("Type").ToString
                    iBlocks = .Item("Blocks")
                    iMaxLevel = .Item("MaxLevel")

                    For x As Integer = 0 To 24
                        sBoardCards(x) = .Item(String.Concat("Board_", x)).ToString
                    Next

                    For x As Integer = 0 To 24
                        sBoardColors(x) = .Item(String.Concat("Color_", x)).ToString
                    Next

                    For x As Integer = 0 To 7
                        sPlayer1_Cards(x) = .Item(String.Concat("Player1_Hand_", x)).ToString
                    Next

                    For x As Integer = 0 To 7
                        sPlayer2_Cards(x) = .Item(String.Concat("Player2_Hand_", x)).ToString
                    Next

                    For x As Integer = 0 To 7
                        sPlayer3_Cards(x) = .Item(String.Concat("Player3_Hand_", x)).ToString
                    Next

                    For x As Integer = 0 To 7
                        sPlayer4_Cards(x) = .Item(String.Concat("Player4_Hand_", x)).ToString
                    Next

                    If IsDBNull(.Item("StartDate")) = False Then dStartDate = .Item("StartDate")
                    If IsDBNull(.Item("LastActivity")) = False Then dLastActivity = .Item("LastActivity")
                    If IsDBNull(.Item("GameEnds")) = False Then dGameEnds = .Item("GameEnds")

                    iSurrender = .Item("Surrender")

                    'If IsDBNull(.Item("P1Socket")) = False Then iPlayer1_Socket = .Item("P1Socket")
                    'If IsDBNull(.Item("P2Socket")) = False Then iPlayer2_Socket = .Item("P2Socket")
                    'If IsDBNull(.Item("P3Socket")) = False Then iPlayer3_Socket = .Item("P3Socket")
                    'If IsDBNull(.Item("P4Socket")) = False Then iPlayer4_Socket = .Item("P4Socket")
                End With
            End If
        Catch ex As Exception
            ErrorDescription = ex
            ErrorFlag = True
        Finally
            oServerConfig.MySQLCalls += 1
        End Try
    End Sub

    'Public ReadOnly Property PlayerSocket(ByVal iPlayerID As Integer) As Integer
    '    Get
    '        Select Case iPlayerID
    '            Case 1
    '                Return iPlayer1_Socket
    '            Case 2
    '                Return iPlayer2_Socket
    '            Case 3
    '                Return iPlayer3_Socket
    '            Case 4
    '                Return iPlayer4_Socket
    '            Case Else
    '                Return 0
    '        End Select
    '    End Get
    'End Property

    Public Property Discards(ByVal iPlayerID As Integer) As Integer
        Get
            Select Case iPlayerID
                Case 1
                    Return iPlayer1_Discards
                Case 2
                    Return iPlayer2_Discards
                Case 3
                    Return iPlayer3_Discards
                Case 4
                    Return iPlayer4_Discards
                Case Else
                    Return 0
            End Select
        End Get

        Set(ByVal value As Integer)
            Select Case iPlayerID
                Case 1
                    iPlayer1_Discards = value
                Case 2
                    iPlayer2_Discards = value
                Case 3
                    iPlayer3_Discards = value
                Case 4
                    iPlayer4_Discards = value
            End Select
        End Set
    End Property

    Public Property Player_Index(ByVal iPlayerID As Integer) As Integer
        Get
            Select Case iPlayerID
                Case 1
                    Return iPlayer1_Index
                Case 2
                    Return iPlayer2_Index
                Case 3
                    Return iPlayer3_Index
                Case 4
                    Return iPlayer4_Index
            End Select
        End Get

        Set(ByVal value As Integer)
            Select Case iPlayerID
                Case 1
                    iPlayer1_Index = value
                Case 2
                    iPlayer2_Index = value
                Case 3
                    iPlayer3_Index = value
                Case 4
                    iPlayer4_Index = value
            End Select
        End Set
    End Property

    Public Property Type() As String
        Get
            Return sType
        End Get
        Set(ByVal value As String)
            sType = value
        End Set
    End Property

    Public Property Blocks() As Integer
        Get
            Return iBlocks
        End Get
        Set(ByVal value As Integer)
            If value = 0 Then value = 5

            iBlocks = value
        End Set
    End Property

    Public Property MaxLevel() As Integer
        Get
            Return iMaxLevel
        End Get

        Set(ByVal value As Integer)
            iMaxLevel = value
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

    Public Property Player2() As String
        Get
            Return sPlayer(1)
        End Get

        Set(ByVal value As String)
            sPlayer(1) = value
        End Set
    End Property


    Public Property Player3() As String
        Get
            Return sPlayer(2)
        End Get

        Set(ByVal value As String)
            sPlayer(2) = value
        End Set
    End Property

    Public Property Player4() As String
        Get
            Return sPlayer(3)
        End Get

        Set(ByVal value As String)
            sPlayer(3) = value
        End Set
    End Property
    Public Property Team1_Score() As Integer
        Get
            Return iTeam1_Score
        End Get

        Set(ByVal value As Integer)
            iTeam1_Score = value
        End Set
    End Property

    Public Property Team2_Score() As Integer
        Get
            Return iTeam2_Score
        End Get

        Set(ByVal value As Integer)
            iTeam2_Score = value
        End Set
    End Property

    Public Property Board(ByVal iSpot As Integer) As String
        Get
            Return sBoardCards(iSpot)
        End Get

        Set(ByVal value As String)
            sBoardCards(iSpot) = value
        End Set
    End Property

    Public Property CardColor(ByVal iSpot As Integer) As String
        Get
            Return sBoardColors(iSpot)
        End Get

        Set(ByVal value As String)
            sBoardColors(iSpot) = value
        End Set
    End Property

    Public Property Player_Hand(ByVal iPlayerID As Integer, ByVal iSpot As Integer) As String
        Get
            If iPlayerID = 1 Then
                Return sPlayer1_Cards(iSpot)
            ElseIf iPlayerID = 2 Then
                Return sPlayer2_Cards(iSpot)
            ElseIf iPlayerID = 3 Then
                Return sPlayer3_Cards(iSpot)
            ElseIf iPlayerID = 4 Then
                Return sPlayer4_Cards(iSpot)
            Else
                Return Nothing
            End If
        End Get

        Set(ByVal value As String)
            If iPlayerID = 1 Then
                sPlayer1_Cards(iSpot) = value
            ElseIf iPlayerID = 2 Then
                sPlayer2_Cards(iSpot) = value
            ElseIf iPlayerID = 3 Then
                sPlayer3_Cards(iSpot) = value
            ElseIf iPlayerID = 4 Then
                sPlayer4_Cards(iSpot) = value
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

    Public ReadOnly Property GameEnds() As Date
        Get
            Return dGameEnds
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
            ElseIf sNick = sPlayer(2) Then
                Return 3
            ElseIf sNick = sPlayer(3) Then
                Return 4
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

    Public Property GameID() As Integer
        Get
            Return iGameID
        End Get

        Set(ByVal value As Integer)
            iGameID = value
        End Set
    End Property

    Public Sub Update()
        Try
            Dim oMySqlHelper As New MySQLFactory.MySQLDBHelper
            Dim oDataRow As DataRow = Nothing

            Dim arParms() As MySqlParameter = New MySqlParameter(61) {}
            arParms(0) = New MySqlParameter("?iGameID", GameID)
            arParms(1) = New MySqlParameter("?iTeam1_Score", Team1_Score)
            arParms(2) = New MySqlParameter("?iTeam2_Score", Team2_Score)
            arParms(3) = New MySqlParameter("?iPlayer1_Discards", Discards(1))
            arParms(4) = New MySqlParameter("?iPlayer2_Discards", Discards(2))
            arParms(5) = New MySqlParameter("?iPlayer3_Discards", Discards(3))
            arParms(6) = New MySqlParameter("?iPlayer4_Discards", Discards(4))
            arParms(7) = New MySqlParameter("?iPlayer1_Index", Player_Index(1))
            arParms(8) = New MySqlParameter("?iPlayer2_Index", Player_Index(2))
            arParms(9) = New MySqlParameter("?iPlayer3_Index", Player_Index(3))
            arParms(10) = New MySqlParameter("?iPlayer4_Index", Player_Index(4))
            arParms(11) = New MySqlParameter("?iSurrender", Surrender)

            For x As Integer = 0 To 24
                arParms(x + 12) = New MySqlParameter(String.Concat("?sBoard", x), Board(x))
            Next

            For x As Integer = 0 To 24
                arParms(x + 37) = New MySqlParameter(String.Concat("?sColor", x), CardColor(x))
            Next

            oMySqlHelper.DBExecuteScalar(ConnectionString, CommandType.StoredProcedure, "spu_TeamOTTGame", arParms)
        Catch ex As Exception
            ErrorDescription = ex
            ErrorFlag = True
        Finally
            oServerConfig.MySQLCalls += 1
        End Try
    End Sub

    Public Sub HistoryInsert(ByVal iSpot As Integer, ByVal sCard As String, ByVal sResult As String)
        Dim oMySqlHelper As New MySQLFactory.MySQLDBHelper

        Dim arParms() As MySqlParameter = New MySqlParameter(3) {}
        arParms(0) = New MySqlParameter("?iGameID", GameID)
        arParms(1) = New MySqlParameter("?iBoard", iSpot)
        arParms(2) = New MySqlParameter("?sCard", sCard)
        arParms(3) = New MySqlParameter("?sResult", sResult)

        oMySqlHelper.DBExecuteScalar(ConnectionString, CommandType.StoredProcedure, "spi_TeamOTTGame_History", arParms)


        oServerConfig.MySQLCalls += 1
    End Sub

    Public Sub EndGame()
        Dim oMySqlHelper As New MySQLFactory.MySQLDBHelper

        Dim arParms() As MySqlParameter = New MySqlParameter(0) {}
        arParms(0) = New MySqlParameter("?iGameID", GameID)

        oMySqlHelper.DBExecuteScalar(ConnectionString, CommandType.StoredProcedure, "spu_TeamOTTGame_GameEnds", arParms)


        oServerConfig.MySQLCalls += 1
    End Sub

    Public Function IsGameOver() As Boolean
        For x As Integer = 0 To 24
            If Board(x) = String.Empty Then
                Return False
            End If
        Next

        Return True
    End Function

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

    Private Sub Initialize()
        iGameID = 0
        Team1_Score = 0
        Team2_Score = 0

        iPlayer1_Discards = 0
        iPlayer2_Discards = 0
        iPlayer3_Discards = 0
        iPlayer4_Discards = 0

        iPlayer1_Index = 0
        iPlayer2_Index = 0
        iPlayer3_Index = 0
        iPlayer4_Index = 0

        'iPlayer1_Socket = 0
        'iPlayer2_Socket = 0
        'iPlayer3_Socket = 0
        'iPlayer4_Socket = 0

        iSurrender = 0
        sType = String.Empty
        iBlocks = 1
        iMaxLevel = 0

        For x As Integer = 0 To 3
            sPlayer(x) = String.Empty
        Next x

        For x As Integer = 0 To 24
            sBoardCards(x) = String.Empty
            sBoardColors(x) = String.Empty
        Next

        For x As Integer = 0 To 7
            sPlayer1_Cards(x) = String.Empty
            sPlayer2_Cards(x) = String.Empty
            sPlayer3_Cards(x) = String.Empty
            sPlayer4_Cards(x) = String.Empty
        Next
    End Sub

    Public Sub New()
        Initialize()
    End Sub

    Public Sub DalNew(ByVal iGamePK As Integer)
        Initialize()
        iGameID = iGamePK
        LoadRecord(iGamePK)
    End Sub

    Public Sub GetRandomCards(ByVal iPlayerID As Integer)
        Try
            Dim oCardFunctions As New CardFunctions

            Dim oDataSet As DataSet = oCardFunctions.GetRandomCards(8)

            For x As Integer = 0 To oDataSet.Tables(0).Rows.Count - 1
                Select Case iPlayerID
                    Case 1
                        sPlayer1_Cards(x) = oDataSet.Tables(0).Rows(x).Item("cardname").ToString
                    Case 2
                        sPlayer2_Cards(x) = oDataSet.Tables(0).Rows(x).Item("cardname").ToString
                    Case 3
                        sPlayer3_Cards(x) = oDataSet.Tables(0).Rows(x).Item("cardname").ToString
                    Case 4
                        sPlayer4_Cards(x) = oDataSet.Tables(0).Rows(x).Item("cardname").ToString
                End Select
            Next
        Catch ex As Exception
            Call ErrorSub(ex.ToString, "GetRandomCards")
        End Try
    End Sub

    Public Sub GetRandomPlayerCards(ByVal iPlayerID As Integer)
        Try
            Dim oCardFunctions As New CardFunctions
            Dim oDataSet As New DataSet

            Select Case iPlayerID
                Case 1
                    oDataSet = oCardFunctions.GetRandomPlayerCards(Player1, 8)
                Case 2
                    oDataSet = oCardFunctions.GetRandomPlayerCards(Player2, 8)
                Case 3
                    oDataSet = oCardFunctions.GetRandomPlayerCards(Player3, 8)
                Case 4
                    oDataSet = oCardFunctions.GetRandomPlayerCards(Player4, 8)
            End Select

            For x As Integer = 0 To oDataSet.Tables(0).Rows.Count - 1
                Select Case iPlayerID
                    Case 1
                        sPlayer1_Cards(x) = oDataSet.Tables(0).Rows(x).Item("cardname").ToString
                    Case 2
                        sPlayer2_Cards(x) = oDataSet.Tables(0).Rows(x).Item("cardname").ToString
                    Case 3
                        sPlayer3_Cards(x) = oDataSet.Tables(0).Rows(x).Item("cardname").ToString
                    Case 4
                        sPlayer4_Cards(x) = oDataSet.Tables(0).Rows(x).Item("cardname").ToString
                End Select
            Next
        Catch ex As Exception
            Call ErrorSub(ex.ToString, "GetRandomCards")
        End Try
    End Sub

    Public ReadOnly Property IsBlock(ByVal iSpot As Integer) As Boolean
        Get
            If sBoardCards(iSpot) = "Block" Then
                Return True
            Else
                Return False
            End If
        End Get
    End Property

    Public ReadOnly Property WinningTeam() As Integer
        Get
            If Team1_Score = Team2_Score Then Return 0
            If Team1_Score > Team2_Score Then Return 1
            If Team1_Score < Team2_Score Then Return 2
        End Get
    End Property

    Public ReadOnly Property WinningScore() As Integer
        Get
            If Team1_Score = Team2_Score Then Return 10
            If Team1_Score > Team2_Score Then Return Team1_Score
            If Team1_Score < Team2_Score Then Return Team2_Score
        End Get
    End Property

    Public ReadOnly Property LosingScore() As Integer
        Get
            If Team1_Score = Team2_Score Then Return 10
            If Team1_Score > Team2_Score Then Return Team2_Score
            If Team1_Score < Team2_Score Then Return Team1_Score
        End Get
    End Property

    Public ReadOnly Property IsEmpty(ByVal iSpot As Integer) As Boolean
        Get
            If sBoardCards(iSpot) = String.Empty Then
                Return True
            Else
                Return False
            End If
        End Get
    End Property

    Public ReadOnly Property IsPlayable(ByVal iSpot As Integer) As Boolean
        Get
            If IsBlock(iSpot) = False And IsEmpty(iSpot) = True Then
                Return True
            Else
                Return False
            End If
        End Get
    End Property

    Public Sub GetRandomBlocks()
        Try
            Dim oMySqlHelper As New MySQLFactory.MySQLDBHelper
            Dim oDataSet As New DataSet

            Dim arParms() As MySqlParameter = New MySqlParameter(0) {}
            arParms(0) = New MySqlParameter("?iBlocks", Blocks)

            oMySqlHelper.FillDataSet(ConnectionString, CommandType.StoredProcedure, "sps_GetRandomBlocks", oDataSet, New String() {"Results"}, arParms)

            For x As Integer = 0 To oDataSet.Tables(0).Rows.Count - 1
                sBoardCards(oDataSet.Tables(0).Rows(x).Item("blockid")) = "Block"

                Send(String.Empty, String.Concat("TEAM_OMNIBLOCK ", oDataSet.Tables(0).Rows(x).Item("blockid")), GetSocket(Player1))
                Send(String.Empty, String.Concat("TEAM_OMNIBLOCK ", oDataSet.Tables(0).Rows(x).Item("blockid")), GetSocket(Player2))
                Send(String.Empty, String.Concat("TEAM_OMNIBLOCK ", oDataSet.Tables(0).Rows(x).Item("blockid")), GetSocket(Player3))
                Send(String.Empty, String.Concat("TEAM_OMNIBLOCK ", oDataSet.Tables(0).Rows(x).Item("blockid")), GetSocket(Player4))
            Next
        Catch ex As Exception
            ErrorDescription = ex
            ErrorFlag = True
        Finally
            oServerConfig.MySQLCalls += 1
        End Try
    End Sub
End Class
