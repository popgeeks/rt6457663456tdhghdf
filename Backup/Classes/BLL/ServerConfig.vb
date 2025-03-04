Imports mySQL.Data.MySqlClient
Imports MySQLFactory
Imports System.ComponentModel
Imports System.Configuration
Imports System.text

Public Class ServerConfig
    Private bError As Boolean
    Private oError As Exception

    Private oBadWords As DataTable
    Private oChatStuffers As DataTable

    Private iBytesReceived As Double
    Private iBytesSent As Double
    Private sVersion As String
    Private iPlayers As Integer
    Private iMaxPlayers As Integer
    Private sMOTD As String
    Private bLockChat As Boolean
    Private sServerName As String
    Private iRankMod As Integer

    Private iWinRate As Integer
    Private iLossRate As Integer
    Private iDrawRate As Integer
    Private iTurboMinimum As Integer

    Private iMySQLCalls As Integer
    Private iPacketReceiveCount As Integer
    Private iPacketSentCount As Integer

    'Experience Multipliers
    Private iDraw As Integer
    Private iTTExp As Integer
    Private iLoser As Integer
    Private iSphereWin As Integer
    Private iSphereLoss As Integer
    Private iMemoryWin As Integer
    Private iMemoryLoss As Integer

    Private iZombies As Integer
    Private iKickOff As Integer

    Public MaxTables As Integer
    Public TTJackPotEnabled As Boolean
    Public TTJackpotMax As Integer
    Public Ping As Integer
    Public EvasionPenalty As Boolean
    Public EnableAP As Boolean
    Public AllowGames As Boolean
    Public EnableAvatarShop As Boolean

    Public THChest As Integer
    Public WarChest As Integer
    Public TTChest As Integer

    Public CardWarsWinExperience As Integer
    Public CardWarsLossExperience As Integer


    Public Property PacketsReceived() As Integer
        Get
            Return iPacketReceiveCount
        End Get

        Set(ByVal value As Integer)
            iPacketReceiveCount = value
        End Set
    End Property

    Public Property PacketsSent() As Integer
        Get
            Return iPacketSentCount
        End Get

        Set(ByVal value As Integer)
            iPacketSentCount = value
        End Set
    End Property

    Public Property MySQLCalls() As Integer
        Get
            Return iMySQLCalls
        End Get

        Set(ByVal value As Integer)
            iMySQLCalls = value
        End Set
    End Property

    Public Property KickOff() As Integer
        Get
            Return iKickOff
        End Get
        Set(ByVal value As Integer)
            iKickOff = value
        End Set
    End Property

    Public Property TripleTriadExperience() As Integer
        Get
            Return iTTExp
        End Get
        Set(ByVal value As Integer)
            iTTExp = value
        End Set
    End Property

    Public Property RankModifier() As Integer
        Get
            Return iRankMod
        End Get
        Set(ByVal value As Integer)
            iRankMod = value
        End Set
    End Property

    Public Property Zombies() As Integer
        Get
            Return iZombies
        End Get
        Set(ByVal value As Integer)
            iZombies = value
        End Set
    End Property

    Public Property TurboMinimum() As Integer
        Get
            Return iTurboMinimum
        End Get
        Set(ByVal value As Integer)
            iTurboMinimum = value
        End Set
    End Property
    Public Property LoserExp() As Integer
        Get
            Return iLoser
        End Get
        Set(ByVal value As Integer)
            iLoser = value
        End Set
    End Property

    Public Property SphereWinExp() As Integer
        Get
            Return iSphereWin
        End Get
        Set(ByVal value As Integer)
            iSphereWin = value
        End Set
    End Property

    Public Property SphereLossExp() As Integer
        Get
            Return iSphereLoss
        End Get
        Set(ByVal value As Integer)
            iSphereLoss = value
        End Set
    End Property

    Public Property MemoryWinExp() As Integer
        Get
            Return iMemoryWin
        End Get
        Set(ByVal value As Integer)
            iMemoryWin = value
        End Set
    End Property

    Public Property MemoryLossExp() As Integer
        Get
            Return iMemoryLoss
        End Get
        Set(ByVal value As Integer)
            iMemoryLoss = value
        End Set
    End Property

    Public Property DrawExp() As Integer
        Get
            Return iDraw
        End Get
        Set(ByVal value As Integer)
            iDraw = value
        End Set
    End Property

    Public Property ModeratedChat() As Boolean
        Get
            Return bLockChat
        End Get
        Set(ByVal value As Boolean)
            bLockChat = value
        End Set
    End Property

    Public Property MessageOfTheDay() As String
        Get
            Return sMOTD
        End Get
        Set(ByVal value As String)
            sMOTD = value
        End Set
    End Property

    Public ReadOnly Property PlayerBonus() As Decimal
        Get
            Select Case oServerConfig.PlayersOnline
                Case 200
                    Return 0.5
                Case 100
                    Return 0.3
                Case 75
                    Return 0.2
                Case 65
                    Return 0.15
                Case 50
                    Return 0.1
                Case 40
                    Return 0.05
                Case Else
                    Return 0
            End Select
        End Get
    End Property

    Public Property ServerName() As String
        Get
            Return sServerName
        End Get
        Set(ByVal value As String)
            sServerName = value
        End Set
    End Property

    Public Property MaxPlayers() As Integer
        Get
            Return iMaxPlayers
        End Get
        Set(ByVal value As Integer)
            iMaxPlayers = value
        End Set
    End Property

    Public Property WinRate() As Integer
        Get
            Return iWinRate
        End Get
        Set(ByVal value As Integer)
            iWinRate = value
        End Set
    End Property

    Public Property DrawRate() As Integer
        Get
            Return iDrawRate
        End Get
        Set(ByVal value As Integer)
            iDrawRate = value
        End Set
    End Property

    Public Property LossRate() As Integer
        Get
            Return iLossRate
        End Get
        Set(ByVal value As Integer)
            iLossRate = value
        End Set
    End Property

    Public Property PlayersOnline() As Integer
        Get
            Return iPlayers
        End Get
        Set(ByVal value As Integer)
            iPlayers = value
        End Set
    End Property

    Public Property BytesReceived() As Double
        Get
            Return iBytesReceived
        End Get
        Set(ByVal value As Double)
            iBytesReceived = value
        End Set
    End Property

    Public Property BytesSent() As Double
        Get
            Return iBytesSent
        End Get
        Set(ByVal value As Double)
            iBytesSent = value
        End Set
    End Property

    Public Property CurrentVersion() As String
        Get
            Return sVersion
        End Get

        Set(ByVal value As String)
            sVersion = value
        End Set
    End Property

    Public Sub Refresh()
        LoadRecord()
    End Sub

    Public ReadOnly Property BadWords() As DataTable
        Get
            Return oBadWords
        End Get
    End Property

    Public ReadOnly Property ChatStuffers() As DataTable
        Get
            Return oChatStuffers
        End Get
    End Property

    Public ReadOnly Property FilterNeeded(ByVal sText As String) As Boolean
        Get
            If Not (oBadWords Is Nothing) Then
                If oBadWords.Rows.Count > 0 Then
                    For x As Integer = 0 To oBadWords.Rows.Count - 1
                        If InStr(1, LCase(sText), LCase(oBadWords.Rows(x).Item("word").ToString)) > 0 Then
                            Return True
                        End If
                    Next x
                End If

                Return False
            Else
                Return False
            End If
        End Get
    End Property

    Public Sub LoadFilter()
        Try
            Dim oMySqlHelper As New MySQLFactory.MySQLDBHelper

            oMySqlHelper.FillDataTable(ConnectionString, CommandType.StoredProcedure, "usp_BadWords", oBadWords, "Filter")
        Catch ex As Exception
            ErrorDescription = ex
            ErrorFlag = True
        End Try
    End Sub

    Public Sub LoadChatStuffers()
        Try
            Dim oMySqlHelper As New MySQLFactory.MySQLDBHelper

            oMySqlHelper.FillDataTable(ConnectionString, CommandType.StoredProcedure, "sps_ChatStuffers", oChatStuffers, "Zombies")
        Catch ex As Exception
            ErrorDescription = ex
            ErrorFlag = True
        End Try
    End Sub

    Protected Sub LoadRecord()
        Try
            Dim oMySqlHelper As New MySQLFactory.MySQLDBHelper
            Dim oDataRow As DataRow = Nothing

            oMySqlHelper.FillDataRow(ConnectionString, CommandType.StoredProcedure, "sps_ServerConfig", oDataRow)

            If oDataRow Is Nothing Then
                Throw New Exception
            Else
                With oDataRow
                    CurrentVersion = String.Concat(.Item("major").ToString, ".", .Item("minor").ToString, ".", .Item("revision").ToString)
                    iRankMod = .Item("rankmod")
                    sMOTD = .Item("motd").ToString
                    sServerName = .Item("servername").ToString

                    TTChest = Val(.Item("jackpot"))
                    THChest = Val(.Item("thchest"))
                    WarChest = Val(.Item("warchest"))

                    iWinRate = .Item("winrate").ToString
                    iLossRate = .Item("loserate").ToString
                    iDrawRate = .Item("drawrate").ToString

                    iTTExp = .Item("ttexp")
                    iDraw = .Item("draw")
                    iLoser = .Item("loser")

                    iSphereWin = .Item("spherewinexp")
                    iSphereLoss = .Item("spherelossexp")
                    iMemoryWin = .Item("memorywinexp")
                    iMemoryLoss = .Item("memorylossexp")

                    CardWarsWinExperience = .Item("cardwarswinexp")
                    CardWarsLossExperience = .Item("cardwarslossexp")

                    iTurboMinimum = .Item("turbominimum")

                    MaxTables = .Item("maxtables")
                    TTJackPotEnabled = IIf(.Item("ttjackpotenabled") = 1, True, False)
                    TTJackpotMax = .Item("ttjackpotmax")
                    Ping = .Item("pingtimeout")
                    EnableAP = IIf(.Item("enableap") = 1, True, False)
                    EnableAvatarShop = IIf(.Item("enableavatarshop") = 1, True, False)
                    AllowGames = IIf(.Item("allowgames") = 1, True, False)
                    EvasionPenalty = IIf(.Item("evasionpenalty") = 1, True, False)
                End With
            End If
        Catch ex As Exception
            Call errorsub(ex.ToString, "Server Config Loader")
            ErrorDescription = ex
            ErrorFlag = True
        End Try
    End Sub

    Public Function GetConfigurationValue(ByVal sKeyword As String) As String
        Dim oDataRow As DataRow = Nothing
        Dim oMySQLHelper As New MySQLFactory.MySQLDBHelper

        Try
            Dim arParms() As MySqlParameter = New MySqlParameter(0) {}
            arParms(0) = New MySqlParameter("?sKeyword", sKeyword)

            oMySQLHelper.FillDataRow(ConnectionString, CommandType.StoredProcedure, "sps_GetConfigurationValue", oDataRow, arParms)

            Dim sValue As String = oDataRow.Item(0).ToString

            If Not (oDataRow Is Nothing) Then
                ErrorDescription = Nothing
                ErrorFlag = False
                Return sValue
            Else
                Return String.Empty
            End If
        Catch ex As Exception
            ErrorDescription = ex
            ErrorFlag = True
            Return String.Empty
        Finally
            oMySQLHelper = Nothing
            oDataRow = Nothing
        End Try
    End Function

    Public Sub InsertServerHealthRecord()
        Dim oMySQLHelper As New MySQLFactory.MySQLDBHelper

        Try
            Dim arParms() As MySqlParameter = New MySqlParameter(4) {}
            arParms(0) = New MySqlParameter("?iMySQLCalls", MySQLCalls)
            arParms(1) = New MySqlParameter("?iPacketsSent", PacketsSent)
            arParms(2) = New MySqlParameter("?iPacketsReceived", PacketsReceived)
            arParms(3) = New MySqlParameter("?iBytesSent", BytesSent)
            arParms(4) = New MySqlParameter("?iBytesReceived", BytesReceived)

            oMySQLHelper.DBExecuteScalar(ConnectionString, CommandType.StoredProcedure, "spi_ServerHealthRecord", arParms)

        Catch ex As Exception
            ErrorDescription = ex
            ErrorFlag = True
        Finally
            oMySQLHelper = Nothing

            MySQLCalls = 0
            PacketsSent = 0
            PacketsReceived = 0
            BytesReceived = 0
            BytesSent = 0
        End Try
    End Sub

    Public Sub InsertUser(ByVal iID As Integer, ByVal sNick As String)
        Try
            Dim oMySqlHelper As New MySQLFactory.MySQLDBHelper
            Dim arParms() As MySqlParameter = New MySqlParameter(1) {}

            arParms(0) = New MySqlParameter("?iID", iID)
            arParms(1) = New MySqlParameter("?sNick", sNick)
            oMySqlHelper.DBExecuteScalar(ConnectionString, CommandType.StoredProcedure, "spi_OnlineUsers", arParms)
        Catch ex As Exception
            Call errorsub(ex.ToString, "InsertUser")
            ErrorDescription = ex
            ErrorFlag = True
        End Try
    End Sub

    Public Sub DeleteUser(ByVal iID As Integer, ByVal sNick As String)
        Try
            Dim oMySqlHelper As New MySQLFactory.MySQLDBHelper
            Dim arParms() As MySqlParameter = New MySqlParameter(0) {}

            arParms(0) = New MySqlParameter("?iID", iID)
            oMySqlHelper.DBExecuteScalar(ConnectionString, CommandType.StoredProcedure, "spd_OnlineUsers", arParms)
        Catch ex As Exception
            Call errorsub(ex.ToString, "DeleteUser")
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

    Public Sub ReInitialize()
        Call LoadRecord()
        Call LoadFilter()
        Call LoadChatStuffers()
    End Sub

    Public Sub New()
        Call LoadRecord()
        Call LoadFilter()
        Call LoadChatStuffers()
    End Sub
End Class
