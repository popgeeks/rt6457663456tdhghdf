Imports mySQL.Data.MySqlClient
Imports MySQLFactory
Imports System.ComponentModel
Imports System.Configuration

Public Class TTFunctionsDAL
    Private bError As Boolean
    Private oError As Exception

    Private iGameID As Integer
    Private sPlayer(1) As String

    Private iCardstoTake As Integer

    Private bCross As Boolean
    Private bOrder As Boolean
    Private bNeutral As Boolean
    Private bElemental As Boolean
    Private bMirror As Boolean
    Private bCombo As Boolean
    Private bImmune As Boolean
    Private bWall As Boolean
    Private bWager As Boolean
    Private iWager As Double
    Private bStarter As Boolean
    Private iStartee As Integer
    Private bRankFreeze As Boolean
    Private bSame As Boolean
    Private bPlus As Boolean
    Private bCCRule As Boolean
    Private bMinLevel As Boolean
    Private iMinLevel As Integer
    Private bMaxLevel As Boolean
    Private iMaxLevel As Integer
    Private bSetWall As Boolean
    Private bCardsTaken As Boolean
    Private iSetWall As Integer
    Private bNoDupes As Boolean
    Private bOpen As Boolean
    Private bRandom As Boolean
    Private bReverse As Boolean
    Private bMinus As Boolean
    Private bSuddenDeath As Boolean
    Private bTraditional As Boolean
    Private bSkip As Boolean
    Private sDecks As String
    Private bCapture As Boolean
    Private bTradeAll As Boolean
    Private bTradeDirect As Boolean
    Private bTradeDiff As Boolean
    Private bTradeTwo As Boolean
    Private bTradeThree As Boolean
    Private bTradeFour As Boolean
    Private bTradeNone As Boolean
    Private bTradeOne As Boolean
    Private iTimes As Integer

    Private bMultiply As Boolean
    Private bRandomOne As Boolean
    Private bFullDeck As Boolean

    Private sCards(8) As String
    Private sColors(8) As String
    Private sElement(8) As String

    Private Player_Score(1) As Integer
    Private sPlayer1_Cards(4) As String
    Private sPlayer2_Cards(4) As String
    Private sPlayer1_OrgCards(4) As String
    Private sPlayer2_OrgCards(4) As String

    Private iPlacedBy(8) As Integer
    Private iEvalue(8) As Integer
    Private iPlayer1_Socket As Integer
    Private iPlayer2_Socket As Integer

    Private dTimeOut As DateTime

    Private bPlayerReady(1) As Boolean

    Private bSurrender As Boolean
    Private dGameEnds As DateTime
    Private dLastActivity As DateTime
    Private dStartDate As DateTime

    Sub Initialize()
        iGameID = 0

        sPlayer(0) = String.Empty
        sPlayer(1) = String.Empty

        iCardstoTake = 0

        bCross = False
        bOrder = False
        bNeutral = False
        bElemental = False
        bMirror = False
        bCombo = False
        bImmune = False
        bWall = False
        bWager = False
        iWager = 0
        bStarter = False
        iStartee = 0
        bRankFreeze = False
        bSame = False
        bPlus = False
        bCCRule = False
        bMinLevel = False
        iMinLevel = 0
        bMaxLevel = False
        iMaxLevel = 0
        bSetWall = False
        iSetWall = 0
        bNoDupes = False
        bOpen = False
        dTimeOut = Nothing
        bRandom = False
        bReverse = False
        bMinus = False
        bSuddenDeath = False
        bTraditional = False
        bCardsTaken = False
        bSkip = False
        sDecks = String.empty
        bCapture = False
        bTradeAll = False
        bTradeDirect = False
        bTradeDiff = False
        bTradeTwo = False
        bTradeThree = False
        bTradeFour = False
        bTradeNone = False
        bTradeOne = False
        iTimes = 0

        For x As Integer = 0 To 8
            sCards(x) = String.Empty
            sColors(x) = String.Empty
            sElement(x) = String.Empty
            iPlacedBy(x) = 0
        Next x

        Player_Score(0) = 0
        Player_Score(1) = 0

        For x As Integer = 0 To 4
            sPlayer1_Cards(x) = String.Empty
            sPlayer2_Cards(x) = String.Empty
            sPlayer1_OrgCards(x) = String.Empty
            sPlayer2_OrgCards(x) = String.Empty
        Next x

        iPlayer1_Socket = 0
        iPlayer2_Socket = 0

        bPlayerReady(0) = False
        bPlayerReady(1) = False

        bSurrender = False
        dGameEnds = Nothing
        dLastActivity = Nothing
        dStartDate = Nothing
    End Sub
    Public ReadOnly Property GameLength() As Integer
        Get
            Return DateDiff(DateInterval.Second, dStartDate, dLastActivity)
        End Get
    End Property
    Public Property PlayerReady(ByVal iPlayerID As Integer) As Boolean
        Get
            Select Case iPlayerID
                Case 1
                    Return bPlayerReady(0)
                Case 2
                    Return bPlayerReady(1)
                Case Else
                    Return False
            End Select
        End Get

        Set(ByVal value As Boolean)
            Select Case iPlayerID
                Case 1
                    bPlayerReady(0) = value
                Case 2
                    bPlayerReady(1) = value
            End Select
        End Set
    End Property

    'Public Property PlayerSocket(ByVal iPlayerID As Integer) As Integer
    '    Get
    '        Select Case iPlayerID
    '            Case 1
    '                Return iPlayer1_Socket
    '            Case 2
    '                Return iPlayer2_Socket
    '            Case Else
    '                Return 0
    '        End Select
    '    End Get

    '    Set(ByVal value As Integer)
    '        Select Case iPlayerID
    '            Case 1
    '                iPlayer1_Socket = value
    '            Case 2
    '                iPlayer2_Socket = value
    '        End Select
    '    End Set
    'End Property

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

    Public Property CardstoTake() As Integer
        Get
            Return iCardstoTake
        End Get

        Set(ByVal value As Integer)
            iCardstoTake = value
        End Set
    End Property

    Public ReadOnly Property ScoreDifference() As Integer
        Get
            Return Math.Abs(Player1_Score - Player2_Score)
        End Get
    End Property

    Public Function FindCardsWon() As Integer
        If TradeNone = True Then Return 0
        If TradeOne = True Then Return 1
        If TradeTwo = True Then Return 2
        If TradeThree = True Then Return 3
        If TradeFour = True Then Return 4
        If TradeAll = True Then Return 5
        If TradeRandomOne = True Then Return 1
        If FullDeck = True Then Return 0

        If TradeDiff = True Then
            Select Case ScoreDifference
                Case 2 ' Score was 6-4
                    Return 1
                Case 4 ' Score was 7-3
                    Return 2
                Case 6 ' Score was 8-2
                    Return 3
                Case 8 ' Score was 9-1
                    Return 4
                Case Else
                    Return 5
            End Select
        End If

        Return 1

    End Function

    Public Property Player1_Score() As Integer
        Get
            Return Player_Score(0)
        End Get

        Set(ByVal value As Integer)
            Player_Score(0) = value
        End Set
    End Property

    Public Property Player2_Score() As Integer
        Get
            Return Player_Score(1)
        End Get

        Set(ByVal value As Integer)
            Player_Score(1) = value
        End Set
    End Property

    Public Property Board(ByVal iSpot As Integer) As String
        Get
            Return sCards(iSpot)
        End Get

        Set(ByVal value As String)
            sCards(iSpot) = value
        End Set
    End Property

    Public Property Element(ByVal iSpot As Integer) As String
        Get
            Return sElement(iSpot)
        End Get

        Set(ByVal value As String)
            sElement(iSpot) = value
        End Set
    End Property

    Public Function CheckCards() As DataRow
        Try
            Dim oMySqlHelper As New MySQLFactory.MySQLDBHelper
            Dim oDataRow As DataRow = Nothing

            Dim arParms() As MySqlParameter = New MySqlParameter(0) {}
            arParms(0) = New MySqlParameter("?iGameID", GameID)

            oMySqlHelper.FillDataRow(ConnectionString, CommandType.StoredProcedure, "sps_TTCheckCards", oDataRow, arParms)

            Return oDataRow
        Catch ex As Exception
            Call errorsub(ex, "CheckCards")
            Return Nothing
        Finally
            oServerConfig.MySQLCalls += 1
        End Try
    End Function

    Public Property CardColor(ByVal iSpot As Integer) As String
        Get
            Return sColors(iSpot)
        End Get

        Set(ByVal value As String)
            sColors(iSpot) = value
        End Set
    End Property

    Public Property PlacedBy(ByVal iSpot As Integer) As Integer
        Get
            Return iPlacedBy(iSpot)
        End Get

        Set(ByVal value As Integer)
            iPlacedBy(iSpot) = value
        End Set
    End Property

    Public Property EValue(ByVal iSpot As Integer) As Integer
        Get
            Return iEvalue(iSpot)
        End Get

        Set(ByVal value As Integer)
            iEvalue(iSpot) = value
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

    Public Property Player_OriginalHand(ByVal iPlayerID As Integer, ByVal iSpot As Integer) As String
        Get
            If iPlayerID = 1 Then
                Return sPlayer1_OrgCards(iSpot)
            ElseIf iPlayerID = 2 Then
                Return sPlayer2_OrgCards(iSpot)
            Else
                Return Nothing
            End If
        End Get

        Set(ByVal value As String)
            If iPlayerID = 1 Then
                sPlayer1_OrgCards(iSpot) = value
            ElseIf iPlayerID = 2 Then
                sPlayer2_OrgCards(iSpot) = value
            End If
        End Set
    End Property

    Public ReadOnly Property StartDate() As Date
        Get
            Return dStartDate
        End Get
    End Property

    Public ReadOnly Property TimeoutDate() As Date
        Get
            Return dTimeOut
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

    Public Property Surrender() As Boolean
        Get
            Return bSurrender
        End Get

        Set(ByVal value As Boolean)
            bSurrender = value
        End Set
    End Property

    Public Property CardsTaken() As Boolean
        Get
            Return bCardsTaken
        End Get

        Set(ByVal value As Boolean)
            bCardsTaken = value
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

    Public Property GameID() As Integer
        Get
            Return iGameID
        End Get

        Set(ByVal value As Integer)
            iGameID = value
        End Set
    End Property


    Public Function IsGameOver() As Boolean
        For x As Integer = 0 To 8
            If Board(x) = String.Empty Then Return False
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

    Public ReadOnly Property IsEmpty(ByVal iSpot As Integer) As Boolean
        Get
            If sCards(iSpot) = String.Empty Then
                Return True
            Else
                Return False
            End If
        End Get
    End Property

    Public ReadOnly Property IsPlayable(ByVal iSpot As Integer) As Boolean
        Get
            If IsEmpty(iSpot) = True Then
                Return True
            Else
                Return False
            End If
        End Get
    End Property

    Public Property Cross() As Boolean
        Get
            Return bCross
        End Get

        Set(ByVal value As Boolean)
            bCross = value
        End Set
    End Property

    Public Property Traditional() As Boolean
        Get
            Return bTraditional
        End Get

        Set(ByVal value As Boolean)
            bTraditional = value
        End Set
    End Property

    Public Property Elemental() As Boolean
        Get
            Return bElemental
        End Get

        Set(ByVal value As Boolean)
            bElemental = value
        End Set
    End Property

    Public Property Order() As Boolean
        Get
            Return bOrder
        End Get

        Set(ByVal value As Boolean)
            bOrder = value
        End Set
    End Property

    Public Property Neutral() As Boolean
        Get
            Return bNeutral
        End Get

        Set(ByVal value As Boolean)
            bNeutral = value
        End Set
    End Property

    Public Property Mirror() As Boolean
        Get
            Return bMirror
        End Get

        Set(ByVal value As Boolean)
            bMirror = value
        End Set
    End Property

    Public Property Combo() As Boolean
        Get
            Return bCombo
        End Get

        Set(ByVal value As Boolean)
            bCombo = value
        End Set
    End Property

    Public Property Immune() As Boolean
        Get
            Return bImmune
        End Get

        Set(ByVal value As Boolean)
            bImmune = value
        End Set
    End Property

    Public Property Wall() As Boolean
        Get
            Return bWall
        End Get

        Set(ByVal value As Boolean)
            bWall = value
        End Set
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

    Public Property Wager() As Boolean
        Get
            Return bWager
        End Get

        Set(ByVal value As Boolean)
            bWager = value
        End Set
    End Property

    Public Property WagerAmt() As Double
        Get
            Return iWager
        End Get

        Set(ByVal value As Double)
            iWager = value
        End Set
    End Property

    Public Property Starter() As Boolean
        Get
            Return bStarter
        End Get

        Set(ByVal value As Boolean)
            bStarter = value
        End Set
    End Property

    Public Property Startee() As Integer
        Get
            Return iStartee
        End Get

        Set(ByVal value As Integer)
            iStartee = value
        End Set
    End Property

    Public Property RankFreeze() As Boolean
        Get
            Return bRankFreeze
        End Get

        Set(ByVal value As Boolean)
            bRankFreeze = value
        End Set
    End Property

    Public Property Same() As Boolean
        Get
            Return bSame
        End Get

        Set(ByVal value As Boolean)
            bSame = value
        End Set
    End Property

    Public Property Plus() As Boolean
        Get
            Return bPlus
        End Get

        Set(ByVal value As Boolean)
            bPlus = value
        End Set
    End Property

    Public Property CCRule() As Boolean
        Get
            Return bCCRule
        End Get

        Set(ByVal value As Boolean)
            bCCRule = value
        End Set
    End Property

    Public Property MinLevel() As Boolean
        Get
            Return bMinLevel
        End Get

        Set(ByVal value As Boolean)
            bMinLevel = value
        End Set
    End Property

    Public Property MinLevelValue() As Integer
        Get
            Return iMinLevel
        End Get

        Set(ByVal value As Integer)
            iMinLevel = value
        End Set
    End Property

    Public Property MaxLevel() As Boolean
        Get
            Return bMaxLevel
        End Get

        Set(ByVal value As Boolean)
            bMaxLevel = value
        End Set
    End Property

    Public Property MaxLevelValue() As Integer
        Get
            Return iMaxLevel
        End Get

        Set(ByVal value As Integer)
            iMaxLevel = value
        End Set
    End Property

    Public Property SetWall() As Boolean
        Get
            Return bSetWall
        End Get

        Set(ByVal value As Boolean)
            bSetWall = value
        End Set
    End Property

    Public Property SetWallValue() As Integer
        Get
            Return iSetWall
        End Get

        Set(ByVal value As Integer)
            iSetWall = value
        End Set
    End Property

    Public Property NoDupes() As Boolean
        Get
            Return bNoDupes
        End Get

        Set(ByVal value As Boolean)
            bNoDupes = value
        End Set
    End Property

    Public Property Open() As Boolean
        Get
            Return bOpen
        End Get

        Set(ByVal value As Boolean)
            bOpen = value
        End Set
    End Property

    Public Property Random() As Boolean
        Get
            Return bRandom
        End Get

        Set(ByVal value As Boolean)
            bRandom = value
        End Set
    End Property

    Public Property Reverse() As Boolean
        Get
            Return bReverse
        End Get

        Set(ByVal value As Boolean)
            bReverse = value
        End Set
    End Property

    Public Property FullDeck() As Boolean
        Get
            Return bFullDeck
        End Get

        Set(ByVal value As Boolean)
            bFullDeck = value
        End Set
    End Property

    Public Property Multiply() As Boolean
        Get
            Return bMultiply
        End Get

        Set(ByVal value As Boolean)
            bMultiply = value
        End Set
    End Property

    Public Property Minus() As Boolean
        Get
            Return bMinus
        End Get

        Set(ByVal value As Boolean)
            bMinus = value
        End Set
    End Property

    Public Property SuddenDeath() As Boolean
        Get
            Return bSuddenDeath
        End Get

        Set(ByVal value As Boolean)
            bSuddenDeath = value
        End Set
    End Property

    Public Property Skip() As Boolean
        Get
            Return bSkip
        End Get

        Set(ByVal value As Boolean)
            bSkip = value
        End Set
    End Property

    Public Property Decks() As String
        Get
            Return sDecks
        End Get

        Set(ByVal value As String)
            sDecks = value
        End Set
    End Property

    Public ReadOnly Property DecksArray() As String
        Get
            Dim sItems As String() = sDecks.Split(" ")
            Dim sReturn As String = String.Empty

            For x As Integer = 0 To sItems.GetUpperBound(0)
                If x <> sItems.GetUpperBound(0) Then
                    sReturn += String.Concat("'", sItems(x), "', ")
                Else
                    sReturn += String.Concat("'", sItems(x), "'")
                End If
            Next

            Return sReturn
        End Get
    End Property

    Public Property Capture() As Boolean
        Get
            Return bCapture
        End Get

        Set(ByVal value As Boolean)
            bCapture = value
        End Set
    End Property

    Public Property TradeAll() As Boolean
        Get
            Return bTradeAll
        End Get

        Set(ByVal value As Boolean)
            bTradeAll = value
        End Set
    End Property

    Public Property TradeDirect() As Boolean
        Get
            Return bTradeDirect
        End Get

        Set(ByVal value As Boolean)
            bTradeDirect = value
        End Set
    End Property

    Public Property TradeDiff() As Boolean
        Get
            Return bTradeDiff
        End Get

        Set(ByVal value As Boolean)
            bTradeDiff = value
        End Set
    End Property

    Public Property TradeTwo() As Boolean
        Get
            Return bTradeTwo
        End Get

        Set(ByVal value As Boolean)
            bTradeTwo = value
        End Set
    End Property

    Public Property TradeRandomOne() As Boolean
        Get
            Return bRandomOne
        End Get

        Set(ByVal value As Boolean)
            bRandomOne = value
        End Set
    End Property

    Public Property TradeThree() As Boolean
        Get
            Return bTradeThree
        End Get

        Set(ByVal value As Boolean)
            bTradeThree = value
        End Set
    End Property

    Public Property TradeFour() As Boolean
        Get
            Return bTradeFour
        End Get

        Set(ByVal value As Boolean)
            bTradeFour = value
        End Set
    End Property

    Public ReadOnly Property IsLoaded() As Boolean
        Get
            If Player1 = String.Empty And Player2 = String.Empty Then
                Return False
            Else
                Return True
            End If
        End Get
    End Property

    Public Property TradeNone() As Boolean
        Get
            Return bTradeNone
        End Get

        Set(ByVal value As Boolean)
            bTradeNone = value
        End Set
    End Property

    Public Property TradeOne() As Boolean
        Get
            Return bTradeOne
        End Get

        Set(ByVal value As Boolean)
            bTradeOne = value
        End Set
    End Property

    Public Property Times() As Integer
        Get
            Return iTimes
        End Get

        Set(ByVal value As Integer)
            iTimes = value
        End Set
    End Property

    Public Sub HistoryInsert(ByVal iSpot As Integer, ByVal sCard As String, ByVal sResult As String)
        Dim oMySqlHelper As New MySQLFactory.MySQLDBHelper

        Dim arParms() As MySqlParameter = New MySqlParameter(3) {}
        arParms(0) = New MySqlParameter("?iGameID", GameID)
        arParms(1) = New MySqlParameter("?iBoard", iSpot)
        arParms(2) = New MySqlParameter("?sCard", sCard)
        arParms(3) = New MySqlParameter("?sResult", sResult)

        oMySqlHelper.DBExecuteScalar(ConnectionString, CommandType.StoredProcedure, "spi_TTGame_History", arParms)

        oServerConfig.MySQLCalls += 1
    End Sub

    Public Function RandomCards(ByVal sPlayer As String, ByVal iPlayerID As Integer) As Boolean
        Try
            Dim oDataTable As New DataTable

            Dim oMySqlHelper As New MySQLFactory.MySQLDBHelper
            Dim arParms() As MySqlParameter = New MySqlParameter(4) {}
            arParms(0) = New MySqlParameter("?iMinLvl", MinLevelValue)
            arParms(1) = New MySqlParameter("?iMaxLvl", MaxLevelValue)
            arParms(2) = New MySqlParameter("?sPlayer", sPlayer)
            arParms(3) = New MySqlParameter("?sDecks", DecksArray)
            arParms(4) = New MySqlParameter("?iTrade", IIf(TradeNone = True, 1, 0))

            oMySqlHelper.FillDataTable(ConnectionString, CommandType.StoredProcedure, "sps_TTRandomCards", oDataTable, "Results", arParms)

            If Not (oDataTable Is Nothing) Then
                For x As Integer = 0 To 4
                    Player_Hand(iPlayerID, x) = oDataTable.Rows(x).Item(0).ToString
                Next

                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            'Call errorsub(ex, "RandomCards")
            Return False
        Finally
            oServerConfig.MySQLCalls += 1
        End Try
    End Function

    Public Function FullRandomCards(ByVal iPlayerID As Integer) As Boolean
        Try
            Dim oDataTable As New DataTable

            Dim oMySqlHelper As New MySQLFactory.MySQLDBHelper
            Dim arParms() As MySqlParameter = New MySqlParameter(2) {}
            arParms(0) = New MySqlParameter("?iMinLvl", MinLevelValue)
            arParms(1) = New MySqlParameter("?iMaxLvl", MaxLevelValue)
            arParms(2) = New MySqlParameter("?sDecks", DecksArray)

            oMySqlHelper.FillDataTable(ConnectionString, CommandType.StoredProcedure, "sps_TTFullRandomCards", oDataTable, "Results", arParms)

            If Not (oDataTable Is Nothing) Then
                For x As Integer = 0 To 4
                    Player_Hand(iPlayerID, x) = oDataTable.Rows(x).Item(0).ToString
                Next

                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Call errorsub(ex, "FullRandomCards")
            Return False
        Finally
            oServerConfig.MySQLCalls += 1
        End Try
    End Function

    Public Sub EndGame()
        Dim oMySqlHelper As New MySQLFactory.MySQLDBHelper

        Dim arParms() As MySqlParameter = New MySqlParameter(0) {}
        arParms(0) = New MySqlParameter("?iGameID", GameID)

        oMySqlHelper.DBExecuteScalar(ConnectionString, CommandType.StoredProcedure, "spu_TTGame_GameEnds", arParms)
        oServerConfig.MySQLCalls += 1
    End Sub

    Public Sub Update()
        Try
            Dim oMySqlHelper As New MySQLFactory.MySQLDBHelper
            Dim oDataRow As DataRow = Nothing

            Dim arParms() As MySqlParameter = New MySqlParameter(52) {}
            arParms(0) = New MySqlParameter("?iGameID", GameID)
            arParms(1) = New MySqlParameter("?iPlayer1_Score", Player1_Score)
            arParms(2) = New MySqlParameter("?iPlayer2_Score", Player2_Score)
            arParms(3) = New MySqlParameter("?iSurrender", Surrender)

            For x As Integer = 0 To 8
                arParms(x + 4) = New MySqlParameter(String.Concat("?sBoard", x), Board(x))
            Next

            For x As Integer = 0 To 8
                arParms(x + 13) = New MySqlParameter(String.Concat("?sColor", x), CardColor(x))
            Next

            For x As Integer = 0 To 8
                arParms(x + 22) = New MySqlParameter(String.Concat("?sElement", x), Element(x))
            Next

            For x As Integer = 0 To 8
                arParms(x + 31) = New MySqlParameter(String.Concat("?iPlacedBy", x), PlacedBy(x))
            Next

            arParms(40) = New MySqlParameter("?iReady1", PlayerReady(1))
            arParms(41) = New MySqlParameter("?iReady2", PlayerReady(2))

            For x As Integer = 0 To 8
                arParms(x + 42) = New MySqlParameter(String.Concat("?iEvalue", x), EValue(x))
            Next

            arParms(51) = New MySqlParameter("?iCardsTaken", IIf(CardsTaken = True, 1, 0))
            arParms(52) = New MySqlParameter("?iCardstoTake", CardstoTake)

            oMySqlHelper.DBExecuteScalar(ConnectionString, CommandType.StoredProcedure, "spu_TTGame", arParms)
        Catch ex As Exception
            ErrorDescription = ex
            ErrorFlag = True
        Finally
            oServerConfig.MySQLCalls += 1
        End Try
    End Sub

    Public Sub UpdateHand()
        Try
            Dim oMySqlHelper As New MySQLFactory.MySQLDBHelper
            Dim oDataRow As DataRow = Nothing

            Dim arParms() As MySqlParameter = New MySqlParameter(10) {}
            arParms(0) = New MySqlParameter("?iGameID", GameID)

            For x As Integer = 0 To 4
                arParms(x + 1) = New MySqlParameter(String.Concat("?sPlr1_Hand_", x), Player_Hand(1, x))
            Next

            For x As Integer = 0 To 4
                arParms(x + 6) = New MySqlParameter(String.Concat("?sPlr2_Hand_", x), Player_Hand(2, x))
            Next

            oMySqlHelper.DBExecuteScalar(ConnectionString, CommandType.StoredProcedure, "spu_TTGame_Hand", arParms)
        Catch ex As Exception
            ErrorDescription = ex
            ErrorFlag = True
        Finally
            oServerConfig.MySQLCalls += 1
        End Try
    End Sub

    Public Sub UpdateOrgHand()
        Try
            Dim oMySqlHelper As New MySQLFactory.MySQLDBHelper
            Dim oDataRow As DataRow = Nothing

            Dim arParms() As MySqlParameter = New MySqlParameter(10) {}
            arParms(0) = New MySqlParameter("?iGameID", GameID)

            For x As Integer = 0 To 4
                arParms(x + 1) = New MySqlParameter(String.Concat("?sPlr1_Hand_", x), Player_Hand(1, x))
            Next

            For x As Integer = 0 To 4
                arParms(x + 6) = New MySqlParameter(String.Concat("?sPlr2_Hand_", x), Player_Hand(2, x))
            Next

            oMySqlHelper.DBExecuteScalar(ConnectionString, CommandType.StoredProcedure, "spu_TTGame_OrgHand", arParms)
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

            Dim arParms() As MySqlParameter = New MySqlParameter(63) {}
            arParms(0) = New MySqlParameter("?iGameID", GameID)
            arParms(1) = New MySqlParameter("?sPlayer1", Player1)
            arParms(2) = New MySqlParameter("?sPlayer2", Player2)

            For x As Integer = 0 To 8
                arParms(x + 3) = New MySqlParameter(String.Concat("?sElement", x), Element(x))
            Next

            For x As Integer = 0 To 4
                arParms(x + 12) = New MySqlParameter(String.Concat("?sPlr1_Hand_", x), Player_Hand(1, x))
            Next

            For x As Integer = 0 To 4
                arParms(x + 17) = New MySqlParameter(String.Concat("?sPlr2_Hand_", x), Player_Hand(2, x))
            Next

            arParms(22) = New MySqlParameter("?iOpen", Math.Abs(CInt(Open)))
            arParms(23) = New MySqlParameter("?iRandom", Math.Abs(CInt(Random)))
            arParms(24) = New MySqlParameter("?iTradeNone", Math.Abs(CInt(TradeNone)))
            arParms(25) = New MySqlParameter("?iTradeAll", Math.Abs(CInt(TradeAll)))
            arParms(26) = New MySqlParameter("?iTradeDiff", Math.Abs(CInt(TradeDiff)))
            arParms(27) = New MySqlParameter("?iTradeTwo", Math.Abs(CInt(TradeTwo)))
            arParms(28) = New MySqlParameter("?iTradeThree", Math.Abs(CInt(TradeThree)))
            arParms(29) = New MySqlParameter("?iTradeFour", Math.Abs(CInt(TradeFour)))
            arParms(30) = New MySqlParameter("?iCapture", Math.Abs(CInt(Capture)))
            arParms(31) = New MySqlParameter("?iTradeDirect", Math.Abs(CInt(TradeDirect)))
            arParms(32) = New MySqlParameter("?iMaxRule", Math.Abs(CInt(MaxLevel)))
            arParms(33) = New MySqlParameter("?iMaxLevel", MaxLevelValue)
            arParms(34) = New MySqlParameter("?iCCrule", Math.Abs(CInt(CCRule)))
            arParms(35) = New MySqlParameter("?iSame", Math.Abs(CInt(Same)))
            arParms(36) = New MySqlParameter("?iPlus", Math.Abs(CInt(Plus)))
            arParms(37) = New MySqlParameter("?iTraditional", Math.Abs(CInt(Traditional)))
            arParms(38) = New MySqlParameter("?iMinRule", Math.Abs(CInt(MinLevel)))
            arParms(39) = New MySqlParameter("?iMinLevel", MinLevelValue)
            arParms(40) = New MySqlParameter("?iWager", Math.Abs(CInt(Wager)))
            arParms(41) = New MySqlParameter("?iWagerAmt", WagerAmt)
            arParms(42) = New MySqlParameter("?iWall", Math.Abs(CInt(Wall)))
            arParms(43) = New MySqlParameter("?iCombo", Math.Abs(CInt(Combo)))
            arParms(44) = New MySqlParameter("?iMirror", Math.Abs(CInt(Mirror)))
            arParms(45) = New MySqlParameter("?iElemental", Math.Abs(CInt(Elemental)))
            arParms(46) = New MySqlParameter("?iNeutral", Math.Abs(CInt(Neutral)))
            arParms(47) = New MySqlParameter("?iOrder", Math.Abs(CInt(Order)))
            arParms(48) = New MySqlParameter("?iCross", Math.Abs(CInt(Cross)))
            arParms(49) = New MySqlParameter("?iReverse", Math.Abs(CInt(Reverse)))
            arParms(50) = New MySqlParameter("?iMinus", Math.Abs(CInt(Minus)))
            arParms(51) = New MySqlParameter("?iSetWall", Math.Abs(CInt(SetWall)))
            arParms(52) = New MySqlParameter("?iWallValue", SetWallValue)
            arParms(53) = New MySqlParameter("?iImmune", Math.Abs(CInt(Immune)))
            arParms(54) = New MySqlParameter("?iStarter", Math.Abs(CInt(Starter)))
            arParms(55) = New MySqlParameter("?iStartee", Startee)
            arParms(56) = New MySqlParameter("?iDeath", Math.Abs(CInt(SuddenDeath)))
            arParms(57) = New MySqlParameter("?iSkip", Math.Abs(CInt(Skip)))
            arParms(58) = New MySqlParameter("?iRankFreeze", Math.Abs(CInt(RankFreeze)))
            arParms(59) = New MySqlParameter("?iTimes", Times)
            arParms(60) = New MySqlParameter("?sDecks", Decks)
            arParms(61) = New MySqlParameter("?iMultiply", Math.Abs(CInt(Multiply)))
            arParms(62) = New MySqlParameter("?iRandomOne", Math.Abs(CInt(TradeRandomOne)))
            arParms(63) = New MySqlParameter("?iFullDeck", Math.Abs(CInt(FullDeck)))



            oMySqlHelper.DBExecuteScalar(ConnectionString, CommandType.StoredProcedure, "spi_TTGame", arParms)
        Catch ex As Exception
            ErrorDescription = ex
            ErrorFlag = True
        Finally
            oServerConfig.MySQLCalls += 1
        End Try
    End Sub

    <Description("Loads the Game Record")> _
    Protected Sub LoadRecord()
        Try
            Dim oMySqlHelper As New MySQLFactory.MySQLDBHelper
            Dim oDataRow As DataRow = Nothing

            Dim arParms() As MySqlParameter = New MySqlParameter(0) {}
            arParms(0) = New MySqlParameter("?iGameID", GameID)

            oMySqlHelper.FillDataRow(ConnectionString, CommandType.StoredProcedure, "sps_ttgame", oDataRow, arParms)

            If oDataRow Is Nothing Then
                Throw New Exception
            Else
                With oDataRow
                    sPlayer(0) = .Item("player1").ToString
                    sPlayer(1) = .Item("player2").ToString

                    Player_Score(0) = .Item("player1_Score")
                    Player_Score(1) = .Item("player2_Score")

                    Open = CBool(.Item("open"))
                    Random = CBool(.Item("random"))
                    TradeNone = CBool(.Item("proom"))
                    TradeAll = CBool(.Item("tradeall"))
                    TradeDiff = CBool(.Item("tradediff"))
                    TradeTwo = CBool(.Item("tradetwo"))
                    TradeThree = CBool(.Item("tradethree"))
                    TradeFour = CBool(.Item("tradefour"))
                    TradeRandomOne = CBool(.Item("randomone"))
                    CardstoTake = .Item("cardstotake")
                    Capture = CBool(.Item("capture"))
                    TradeDirect = CBool(.Item("direct"))
                    MaxLevel = CBool(.Item("maxrule"))
                    MaxLevelValue = .Item("maxlevel")
                    CCRule = CBool(.Item("ccrule"))
                    Same = CBool(.Item("same"))
                    Plus = CBool(.Item("plus"))
                    Traditional = CBool(.Item("traditional"))
                    MinLevel = CBool(.Item("minrule"))
                    MinLevelValue = .Item("minlevel")
                    Wager = CBool(.Item("wager"))
                    Immune = CBool(.Item("immune"))
                    WagerAmt = .Item("wageramt")
                    Wall = CBool(.Item("wall"))
                    Combo = CBool(.Item("combo"))
                    Mirror = CBool(.Item("mirror"))
                    Elemental = CBool(.Item("elemental"))
                    Neutral = CBool(.Item("neutral"))
                    Order = CBool(.Item("order"))
                    Cross = CBool(.Item("cross"))
                    Reverse = CBool(.Item("reverse"))
                    Minus = CBool(.Item("minus"))
                    SetWall = CBool(.Item("set wall"))
                    SetWallValue = .Item("wall value")
                    Immune = CBool(.Item("immune"))
                    Starter = CBool(.Item("starter"))
                    Startee = .Item("startee")
                    SuddenDeath = CBool(.Item("death"))
                    Skip = CBool(.Item("skip"))
                    RankFreeze = CBool(.Item("rankfreeze"))
                    CardsTaken = CBool(.Item("cardstaken"))
                    Multiply = CBool(.Item("multiply"))
                    FullDeck = CBool(.Item("fulldeck"))

                    Times = .Item("times")

                    For x As Integer = 0 To 8
                        sCards(x) = .Item(String.Concat(x, "-name")).ToString
                    Next

                    For x As Integer = 0 To 8
                        sColors(x) = .Item(String.Concat(x, "-color")).ToString
                    Next

                    For x As Integer = 0 To 8
                        sElement(x) = .Item(String.Concat(x, "-element")).ToString
                    Next

                    For x As Integer = 0 To 4
                        sPlayer1_Cards(x) = .Item(String.Concat("cards-plr1_", x)).ToString
                    Next

                    For x As Integer = 0 To 4
                        sPlayer2_Cards(x) = .Item(String.Concat("cards-plr2_", x)).ToString
                    Next

                    For x As Integer = 0 To 4
                        sPlayer1_OrgCards(x) = .Item(String.Concat("original-player1_", x)).ToString
                    Next

                    For x As Integer = 0 To 4
                        sPlayer2_OrgCards(x) = .Item(String.Concat("original-player2_", x)).ToString
                    Next

                    For x As Integer = 0 To 8
                        iPlacedBy(x) = Val(.Item(String.Concat("placedby_", x)).ToString)
                        EValue(x) = Val(.Item(String.Concat(x, "_evalue")).ToString)
                    Next

                    bPlayerReady(0) = CBool(.Item("p1-ready"))
                    bPlayerReady(1) = CBool(.Item("p2-ready"))

                    Decks = .Item("decks").ToString

                    If IsDBNull(.Item("StartDate")) = False Then dStartDate = .Item("StartDate")
                    If IsDBNull(.Item("Timeout")) = False Then dTimeOut = .Item("Timeout")
                    If IsDBNull(.Item("LastActivity")) = False Then dLastActivity = .Item("LastActivity")
                    If IsDBNull(.Item("GameEnds")) = False Then dGameEnds = .Item("GameEnds")

                    Surrender = CBool(.Item("Surrender"))

                    If IsDBNull(.Item("P1Socket")) = False Then iPlayer1_Socket = .Item("P1Socket")
                    If IsDBNull(.Item("P2Socket")) = False Then iPlayer2_Socket = .Item("P2Socket")
                End With
            End If
        Catch ex As Exception
            ErrorDescription = ex
            ErrorFlag = True
        Finally
            oServerConfig.MySQLCalls += 1
        End Try
    End Sub

    Protected Sub resetjackpot(ByVal sPlayer As String)
        Try
            Dim oMySqlHelper As New MySQLFactory.MySQLDBHelper
            Dim oDataRow As DataRow = Nothing

            Dim arParms() As MySqlParameter = New MySqlParameter(0) {}
            arParms(0) = New MySqlParameter("?sPlayer", sPlayer)

            oMySqlHelper.DBExecuteScalar(ConnectionString, CommandType.StoredProcedure, "spu_ResetJackpot", arParms)
        Catch ex As Exception
            Call errorsub(ex, "resetjackpot")
        Finally
            oServerConfig.TTChest = 0
            oServerConfig.MySQLCalls += 1
        End Try
    End Sub

    Public Sub New()
        Initialize()
    End Sub
End Class
