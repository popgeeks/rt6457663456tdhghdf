Imports mySQL.Data.MySqlClient
Imports MySQLFactory
Imports System.ComponentModel
Imports System.Configuration

Public Class PlayerAccountDAL
    Private sPlayer As String
    Private dLastPlayed As Date
    Private dLastLogin As Date
    Private dSignupDate As Date
    Private iGold As Integer
    Private iAP As Integer
    Private sEmail As String
    Private sGuild As String
    Private iGuildStatus As Integer
    Private sSurname As String
    Private sReferral As String
    Private iFailed As Integer
    Private bVerified As Boolean
    Private sLastIP As String
    Private bBanned As Boolean
    Private bStaffBonus As Boolean
    Private iLevel As Integer
    Private iExp As Integer
    Private iTotalCards As Integer
    Private iTotalShopCards As Integer
    Private sPassword As String
    Private iNextLevel As Integer
    Private sGender As String
    Private sHead As String
    Private sBody As String
    Private sBackground As String
    Private sCash As String
    Private iMinutes As Integer
    Private bAdminStatus As Boolean
    Private sPasskey As String
    Private iAdminLevel As Integer
    Private bOnline As Boolean
    Private iAAPoints As Integer
    Private iAATrained As Integer
    Private iVAPoints As Integer
    Private iTokens As Integer
    Private iSocket As Integer
    Private sMembership As String
    Private sCountry As String
    Private sAppliedGuild As String
    Private bChanged As Boolean
    Private dtExpDate As DateTime
    Private dGPBonus As Double
    Private dEXPBonus As Double
    Private iMailCount As Integer

    Private iTTRank As Integer
    Private iSBrank As Integer
    Private iMemoryRank As Integer
    Private iOTTRank As Integer
    Private iChinRank As Integer

    Private iAchievementScore As Integer

    Private iAAExp As Integer
    Private dAAPct As Double

    Private iQuestCount As Integer

    Private bError As Boolean
    Private oError As Exception

    Public TotalNewDecks As Integer
    Public TodayNewDecks As Integer
    Public LastNewDeck As DateTime
    Public HasShop As Boolean
    Public RushGauge As Decimal
    Public CWRank As Integer

    Public Function IsBanned(ByVal sPlayer As String, ByVal sIP As String) As Boolean
        Dim oMySQLHelper As New MySQLFactory.MySQLDBHelper

        Try
            Dim arParms() As MySqlParameter = New MySqlParameter(1) {}
            Dim oDataRow As DataRow = Nothing

            arParms(0) = New MySqlParameter("?sPlayer", sPlayer)
            arParms(1) = New MySqlParameter("?sIP", sIP)

            oMySQLHelper.FillDataRow(ConnectionString, CommandType.StoredProcedure, "sps_IsPlayerBanned", oDataRow, arParms)

            Dim bMessage As Boolean = False

            If IsDBNull(oDataRow.Item(0)) = False Then bMessage = oDataRow.Item(0)

            Return bMessage
        Catch ex As Exception
            Return False
        Finally
            oMySQLHelper = Nothing
            oServerConfig.MySQLCalls += 1
        End Try
    End Function

    Public Sub New()
    End Sub

    Public Sub New(ByVal sNick As String)
        If sNick <> String.Empty Then LoadRecord(sNick)
    End Sub

    Public Function InsertRecord(ByVal sNick As String, ByVal sEmail As String, ByVal sPass As String, ByVal sReferal As String, ByVal sCountry As String, ByVal iNewsletter As Integer, ByVal iIncentive As Integer, ByVal iCOPPA As Integer) As Boolean
        Try
            Dim oMySqlHelper As New MySQLFactory.MySQLDBHelper
            Dim arParms() As MySqlParameter = New MySqlParameter(8) {}

            arParms(0) = New MySqlParameter("?sNick", sNick)
            arParms(1) = New MySqlParameter("?sEmail", sEmail)
            arParms(2) = New MySqlParameter("?sPass", sPass)
            arParms(3) = New MySqlParameter("?sReferal", sReferal)
            arParms(4) = New MySqlParameter("?sCountry", sCountry)
            arParms(5) = New MySqlParameter("?iNewsletter", iNewsletter)
            arParms(6) = New MySqlParameter("?iIncentive", iIncentive)
            arParms(7) = New MySqlParameter("?iCOPPA", iCOPPA)
            arParms(8) = New MySqlParameter("?sToday", DateString)
            oMySqlHelper.DBExecuteScalar(ConnectionString, CommandType.StoredProcedure, "spi_Account", arParms)
            Return True
        Catch ex As Exception
            Call errorsub(ex, "InsertRecord")
            Return False
        Finally
            oServerConfig.MySQLCalls += 1
        End Try
    End Function

    Protected Sub LoadRecord(ByVal sNick As String)
        Dim oDataRow As DataRow = Nothing
        Dim oMySQLHelper As New MySQLFactory.MySQLDBHelper

        Try
            Dim arParms() As MySqlParameter = New MySqlParameter(0) {}
            arParms(0) = New MySqlParameter("?sNick", sNick)

            oMySQLHelper.FillDataRow(ConnectionString, CommandType.StoredProcedure, "sps_playerstats", oDataRow, arParms)

            If Not (oDataRow Is Nothing) Then
                With oDataRow
                    Player = sNick
                    If IsDBNull(.Item("lastplayed")) = False And .Item("lastplayed").ToString <> String.Empty Then LastPlayed = Format(CDate(.Item("lastplayed").ToString), "yyyy-MM-dd")
                    If IsDBNull(.Item("lastlogin")) = False And .Item("lastlogin").ToString <> String.Empty Then LastLogin = Format(CDate(.Item("lastlogin").ToString), "yyyy-MM-dd")
                    If IsDBNull(.Item("signupdate")) = False And .Item("signupdate").ToString <> String.Empty Then SignupDate = Format(CDate(.Item("signupdate").ToString), "yyyy-MM-dd")
                    Gold = Val(.Item("gold").ToString)
                    AP = Val(.Item("ap").ToString)
                    Email = .Item("email").ToString
                    Guild = .Item("guild").ToString
                    GuildStatus = Val(.Item("guildstatus").ToString)
                    Surname = .Item("surname").ToString
                    Referral = .Item("referal").ToString
                    FailedLogins = Val(.Item("failed").ToString)
                    Verified = IIf(Val(.Item("verified")) = 1, True, False)
                    LastIP = .Item("lastip").ToString
                    Banned = IIf(Val(.Item("banned")) = 1, True, False)
                    StaffBonus = False
                    Level = Val(.Item("level").ToString)
                    Experience = Val(.Item("exp").ToString)
                    TotalCards = Val(.Item("totalcards").ToString)
                    TotalShopCards = 0
                    Password = .Item("password").ToString
                    Gender = .Item("gender").ToString
                    Head = .Item("avatar_head").ToString
                    Body = .Item("avatar_body").ToString
                    Background = .Item("avatar_bg").ToString
                    Cash = .Item("cash").ToString
                    NextLevel = .Item("nextexp").ToString
                    PassKey = .Item("Passkey").ToString
                    AdminLevel = .Item("AdminLevel")
                    Country = ""
                    Silenced_Minutes = .Item("SilencedMinutes")
                    AdminStatus = IIf(.Item("adminstatus").ToString = "on", True, False)
                    Online = IIf(.Item("Online").ToString = "Yes", True, False)
                    AAPoints = .Item("aapoints")
                    AATrained = .Item("trained")
                    Tokens = .Item("Tokens")
                    TTRank = .Item("tt_rank")
                    SBRank = .Item("sb_rank")
                    MemoryRank = .Item("memory_rank")
                    ChinRank = .Item("chinchin_rank")
                    OTTRank = .Item("ott_rank")
                    CWRank = .Item("cw_rank")
                    AAPct = .Item("aapct")
                    AAExp = .Item("aaexp")
                    PlayerSocket = IIf(.Item("OnlineID") <> 0, .Item("OnlineID"), 0)
                    Membership = .Item("MembershipType").ToString
                    AppliedGuild = .Item("AppliedGuild").ToString
                    Changed = IIf(.Item("changed").ToString = "1", True, False)
                    VAPoints = .Item("VAPoints")
                    AchievementScore = IIf(IsDBNull(.Item("AchievementScore")) = True, 0, .Item("AchievementScore"))
                    QuestCount = 0
                    MailCount = .Item("MailCount")

                    MembershipExpDate = IIf(IsDBNull(.Item("ExpDate")) = True, DateTime.Parse("1900-01-01"), DateTime.Parse(.Item("ExpDate").ToString))

                    GPBonus = .Item("GPBonus")
                    EXPBonus = .Item("EXPBonus")

                    LastNewDeck = IIf(IsDBNull(.Item("LastNewDeck")) = True, DateTime.Parse("1900-01-01"), DateTime.Parse(.Item("LastNewDeck").ToString))
                    TotalNewDecks = .Item("TotalNewDecks")
                    TodayNewDecks = .Item("TodayNewDecks")

                    RushGauge = .Item("Rush")
                    HasShop = False

                    ErrorDescription = Nothing
                    ErrorFlag = False
                End With
            Else
                ClearAll()
            End If
        Catch ex As Exception
            ErrorDescription = ex
            ErrorFlag = True
            errorsub(ex, "LoadRecord")
        Finally
            oMySQLHelper = Nothing
            oDataRow = Nothing
            oServerConfig.MySQLCalls += 1
        End Try
    End Sub

    Public Sub UpdateExp(ByVal iExp As Integer)
        Try
            Dim iAAEXP As Integer = iExp * (AAPct / 100)
            Dim iRegEXP As Integer = iExp - iAAEXP

            GiveExp(iRegEXP, iAAEXP)

            If AAExp > 2000 Then
                GiveExp(0, -2000)
                AAPoints += 1

                UpdateField("aapoints", AAPoints.ToString, True)
                Send(Player, String.Concat("AALEVELUP ", AAPoints))
            End If
        Catch ex As Exception
            Call errorsub(ex, "UpdateExp")
        End Try
    End Sub

    Public Function UpdateField(ByVal strField As String, ByVal strData As String, Optional ByVal intBit As Integer = 0) As Boolean
        Try
            Dim oFunctions As New DatabaseFunctions

            If intBit = 0 Then
                oFunctions.AccountUpdate(ConnectionString, Player, strField, strData)
            Else
                oFunctions.AccountUpdate(ConnectionString, Player, strField, CInt(Val(strData)))
            End If
        Catch ex As Exception
            Call errorsub(ex, "setaccountdata")
        End Try
    End Function

    Public Property EXPBonus() As Double
        Get
            Return dEXPBonus + 1
        End Get
        Set(ByVal value As Double)
            dEXPBonus = value
        End Set
    End Property

    Public Property MailCount() As Integer
        Get
            Return iMailCount
        End Get

        Set(ByVal value As Integer)
            iMailCount = value
        End Set
    End Property

    Public Property GPBonus() As Double
        Get
            Return dGPBonus + 1
        End Get
        Set(ByVal value As Double)
            dGPBonus = value
        End Set
    End Property

    Public Property QuestCount() As Integer
        Get
            Return iQuestCount
        End Get

        Set(ByVal value As Integer)
            iQuestCount = value
        End Set
    End Property

    Protected Sub GiveExp(ByVal iExp As Integer, ByVal iAAExp As Integer)
        Dim oDataRow As DataRow = Nothing
        Dim oMySQLHelper As New MySQLFactory.MySQLDBHelper

        Try
            Dim arParms() As MySqlParameter = New MySqlParameter(2) {}
            arParms(0) = New MySqlParameter("?sNick", Player)
            arParms(1) = New MySqlParameter("?iEXP", iExp)
            arParms(2) = New MySqlParameter("?iAAEXP", iAAExp)

            oMySQLHelper.DBExecuteScalar(ConnectionString, CommandType.StoredProcedure, "usp_GiveExp", arParms)

            Experience += iExp
            AAExp += iAAExp
        Catch ex As Exception
            Call errorsub(ex, "GiveExp")
        Finally
            oServerConfig.MySQLCalls += 1
        End Try
    End Sub

    Public Sub Reload()
        LoadRecord(Player)
    End Sub

    Private Sub ClearAll()
        Player = String.Empty
        LastPlayed = Nothing
        SignupDate = Nothing
        LastLogin = Nothing
        Gold = 0
        AP = 0
        Email = String.Empty
        Guild = String.Empty
        GuildStatus = 0
        Surname = String.Empty
        Referral = String.Empty
        FailedLogins = 0
        Verified = False
        LastIP = String.Empty
        Banned = False
        StaffBonus = False
        Level = 0
        Experience = 0
        TotalCards = 0
        TotalShopCards = 0
        Password = String.Empty
        Silenced_Minutes = 0
        AdminStatus = False
        PassKey = String.Empty
        adminlevel = 0
        bError = False
        oError = Nothing
        bOnline = False
        iTokens = 0
        iTTRank = 0
        iSBrank = 0
        iMemoryRank = 0
        iOTTRank = 0
        iChinRank = 0
        iAAExp = 0
        dAAPct = 0
        sCountry = String.Empty
        sAppliedGuild = String.Empty
        bChanged = False
        iAchievementScore = 0
        iquestcount = 0
        dtExpDate = Nothing
        dGPBonus = 0
        dEXPBonus = 0

        LastNewDeck = Nothing
        TodayNewDecks = 0
        TotalNewDecks = 0
    End Sub

    Public Property MembershipExpDate() As DateTime
        Get
            Return dtExpDate
        End Get

        Set(ByVal value As DateTime)
            dtExpDate = value
        End Set
    End Property

    Public Property AchievementScore() As Integer
        Get
            Return iAchievementScore
        End Get

        Set(ByVal value As Integer)
            iAchievementScore = value
        End Set
    End Property

    Public Property PassKey() As String
        Get
            Return sPasskey
        End Get
        Set(ByVal value As String)
            sPasskey = value
        End Set
    End Property

    Public Property Country() As String
        Get
            Return sCountry
        End Get
        Set(ByVal value As String)
            sCountry = value
        End Set
    End Property

    Public Property PlayerSocket() As Integer
        Get
            Return iSocket
        End Get
        Set(ByVal value As Integer)
            iSocket = value
        End Set
    End Property

    Public Property AAExp() As Integer
        Get
            Return iAAExp
        End Get
        Set(ByVal value As Integer)
            iAAExp = value
        End Set
    End Property

    Public Property AAPct() As Double
        Get
            Return dAAPct
        End Get
        Set(ByVal value As Double)
            dAAPct = value
        End Set
    End Property

    Public Property AdminLevel() As Integer
        Get
            Return iAdminLevel
        End Get
        Set(ByVal value As Integer)
            iAdminLevel = value
        End Set
    End Property

    Public Property TTRank() As Integer
        Get
            Return iTTRank
        End Get
        Set(ByVal value As Integer)
            iTTRank = value
        End Set
    End Property

    Public Property SBRank() As Integer
        Get
            Return iSBrank
        End Get
        Set(ByVal value As Integer)
            iSBrank = value
        End Set
    End Property

    Public Property MemoryRank() As Integer
        Get
            Return iMemoryRank
        End Get
        Set(ByVal value As Integer)
            iMemoryRank = value
        End Set
    End Property

    Public Property OTTRank() As Integer
        Get
            Return iOTTRank
        End Get
        Set(ByVal value As Integer)
            iOTTRank = value
        End Set
    End Property

    Public Property ChinRank() As Integer
        Get
            Return iChinRank
        End Get
        Set(ByVal value As Integer)
            iChinRank = value
        End Set
    End Property

    Public Property Tokens() As Integer
        Get
            Return iTokens
        End Get
        Set(ByVal value As Integer)
            iTokens = value
        End Set
    End Property

    Public Property AAPoints() As Integer
        Get
            Return iAAPoints
        End Get
        Set(ByVal value As Integer)
            iAAPoints = value
        End Set
    End Property

    Public Property Membership() As String
        Get
            Return sMembership
        End Get
        Set(ByVal value As String)
            sMembership = value
        End Set
    End Property

    Public Property AATrained() As Integer
        Get
            Return iAATrained
        End Get
        Set(ByVal value As Integer)
            iAATrained = value
        End Set
    End Property

    Public Property AdminStatus() As Boolean
        Get
            Return bAdminStatus
        End Get
        Set(ByVal value As Boolean)
            bAdminStatus = value
        End Set
    End Property

    Public Property Changed() As Boolean
        Get
            Return bChanged
        End Get
        Set(ByVal value As Boolean)
            bChanged = value
        End Set
    End Property

    Public Property Online() As Boolean
        Get
            Return bOnline
        End Get
        Set(ByVal value As Boolean)
            bOnline = value
        End Set
    End Property

    Public ReadOnly Property Silenced() As Boolean
        Get
            Return IIf(Silenced_Minutes > 0, True, False)
        End Get
    End Property

    Public Property Silenced_Minutes() As Integer
        Get
            Return iMinutes
        End Get

        Set(ByVal value As Integer)
            iMinutes = value
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

    <Description("Gets or Sets Player's Total Cards")> _
    Public Property TotalCards() As Integer
        Get
            Return iTotalCards
        End Get
        Set(ByVal Value As Integer)
            iTotalCards = Value
        End Set
    End Property

    <Description("Gets or Sets Player's Total Cards in Shop")> _
    Public Property TotalShopCards() As Integer
        Get
            Return iTotalShopCards
        End Get
        Set(ByVal Value As Integer)
            iTotalShopCards = Value
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

    <Description("Gets or Sets Player's Experience Value")> _
    Public Property Experience() As Integer
        Get
            Return iExp
        End Get
        Set(ByVal Value As Integer)
            iExp = Value
        End Set
    End Property

    <Description("Gets or Sets Player's Experience to Next Level")> _
    Public Property NextLevel() As Integer
        Get
            Return iNextLevel
        End Get
        Set(ByVal Value As Integer)
            iNextLevel = Value
        End Set
    End Property

    <Description("Gets or Sets Player's Experience Level")> _
    Public Property Level() As Integer
        Get
            Return iLevel
        End Get
        Set(ByVal Value As Integer)
            iLevel = Value
        End Set
    End Property

    <Description("Gets or Sets Player Staff Bonus")> _
    Public Property StaffBonus() As Boolean
        Get
            Return bStaffBonus
        End Get
        Set(ByVal Value As Boolean)
            bStaffBonus = Value
        End Set
    End Property

    <Description("Gets or Sets if Player Has Been Banned")> _
    Public Property Banned() As Boolean
        Get
            Return bBanned
        End Get
        Set(ByVal Value As Boolean)
            bBanned = Value
        End Set
    End Property

    <Description("Gets or Sets Player's IP from the Last Login")> _
    Public Property LastIP() As String
        Get
            Return sLastIP
        End Get
        Set(ByVal Value As String)
            sLastIP = Value
        End Set
    End Property

    Public Property AppliedGuild() As String
        Get
            Return sAppliedGuild
        End Get
        Set(ByVal value As String)
            sAppliedGuild = value
        End Set
    End Property

    <Description("Gets or Sets if Player Has Validated Their Email Address")> _
    Public Property Verified() As Boolean
        Get
            Return bVerified
        End Get
        Set(ByVal Value As Boolean)
            bVerified = Value
        End Set
    End Property

    <Description("Gets or Sets Number of Consecutive Failed Login Attempts")> _
    Public Property FailedLogins() As Integer
        Get
            Return iFailed
        End Get
        Set(ByVal Value As Integer)
            iFailed = Value
        End Set
    End Property

    <Description("Gets or Sets Player's Referral")> _
    Public Property Referral() As String
        Get
            Return sReferral
        End Get
        Set(ByVal Value As String)
            sReferral = Value
        End Set
    End Property

    <Description("Gets or Sets Player's Surname")> _
    Public Property Surname() As String
        Get
            Return sSurname
        End Get
        Set(ByVal Value As String)
            sSurname = Value
        End Set
    End Property

    <Description("Gets or Sets Player's Guild Status, 0 = None, 1 = Officer, 2 = GM, 3 = Member")> _
    Public Property GuildStatus() As Integer
        Get
            Return iGuildStatus
        End Get
        Set(ByVal Value As Integer)
            iGuildStatus = Value
        End Set
    End Property

    <Description("Gets or Sets Player's Guild Name")> _
    Public Property Guild() As String
        Get
            Return sGuild
        End Get
        Set(ByVal Value As String)
            sGuild = Value
        End Set
    End Property

    <Description("Gets or Sets Player's Email Address")> _
    Public Property Email() As String
        Get
            Return sEmail
        End Get
        Set(ByVal Value As String)
            sEmail = Value
        End Set
    End Property

    <Description("Gets or Sets Player's Active Points")> _
    Public Property AP() As Integer
        Get
            Return iAP
        End Get
        Set(ByVal Value As Integer)
            iAP = Value
        End Set
    End Property

    <Description("Gets or Sets Player's Gold")> _
    Public Property Gold() As Integer
        Get
            Return iGold
        End Get
        Set(ByVal Value As Integer)
            iGold = Value
        End Set
    End Property

    <Description("Gets or Sets Player's Signup Date")> _
    Public Property SignupDate() As Date
        Get
            Return dSignupDate
        End Get
        Set(ByVal Value As Date)
            dSignupDate = Value
        End Set
    End Property

    <Description("Gets or Sets Player's Date of Last Login")> _
    Public Property LastLogin() As Date
        Get
            Return dLastLogin
        End Get
        Set(ByVal Value As Date)
            dLastLogin = Value
        End Set
    End Property

    <Description("Gets or Sets Player's Name")> _
    Public Property Player() As String
        Get
            Return sPlayer
        End Get
        Set(ByVal Value As String)
            sPlayer = Value
        End Set
    End Property

    <Description("Gets or Sets Player's Last Played Date")> _
    Public Property LastPlayed() As Date
        Get
            Return dLastPlayed
        End Get
        Set(ByVal Value As Date)
            dLastPlayed = Value
        End Set
    End Property

    <Description("Gets or Sets Player's Password")> _
    Public Property Password() As String
        Get
            Return sPassword
        End Get
        Set(ByVal Value As String)
            sPassword = Value
        End Set
    End Property

    <Description("Gets or Sets Player's Cash Value")> _
    Public Property Cash() As String
        Get
            Return sCash
        End Get
        Set(ByVal Value As String)
            sCash = Value
        End Set
    End Property

    Public Property Gender() As String
        Get
            Return sGender
        End Get
        Set(ByVal Value As String)
            sGender = Value
        End Set
    End Property

    Public Property Body() As String
        Get
            Return sBody
        End Get
        Set(ByVal Value As String)
            sBody = Value
        End Set
    End Property

    Public Property VAPoints() As Integer
        Get
            Return iVAPoints
        End Get
        Set(ByVal value As Integer)
            iVAPoints = value
        End Set
    End Property

    Public Property Head() As String
        Get
            Return sHead
        End Get
        Set(ByVal Value As String)
            sHead = Value
        End Set
    End Property

    Public Property Background() As String
        Get
            Return sBackground
        End Get
        Set(ByVal Value As String)
            sBackground = Value
        End Set
    End Property

    Public Sub InvalidLogin()
        Try
            Dim oMySqlHelper As New MySQLFactory.MySQLDBHelper

            Dim arParms() As MySqlParameter = New MySqlParameter(0) {}
            arParms(0) = New MySqlParameter("?sNick", Player)

            oMySqlHelper.DBExecuteScalar(ConnectionString, CommandType.StoredProcedure, "spu_InvalidLogin", arParms)
        Catch ex As Exception
            ErrorDescription = ex
            ErrorFlag = True
        Finally
            oServerConfig.MySQLCalls += 1
        End Try
    End Sub

    Public Sub Login_UpdateAccount()
        Try
            Dim oMySqlHelper As New MySQLFactory.MySQLDBHelper

            Dim arParms() As MySqlParameter = New MySqlParameter(2) {}
            arParms(0) = New MySqlParameter("?sNick", Player)
            arParms(1) = New MySqlParameter("?sLastIP", LastIP)
            arParms(2) = New MySqlParameter("?sLastLogin", LastLogin)

            oMySqlHelper.DBExecuteScalar(ConnectionString, CommandType.StoredProcedure, "spu_Account_LoadGame", arParms)
        Catch ex As Exception
            ErrorDescription = ex
            ErrorFlag = True
        Finally
            oServerConfig.MySQLCalls += 1
        End Try
    End Sub

    Public Sub VerifyUser()
        Try
            Dim oMySqlHelper As New MySQLFactory.MySQLDBHelper

            Dim arParms() As MySqlParameter = New MySqlParameter(0) {}
            arParms(0) = New MySqlParameter("?sNick", Player)

            oMySqlHelper.DBExecuteScalar(ConnectionString, CommandType.StoredProcedure, "spu_VerifyUser", arParms)
        Catch ex As Exception
            ErrorDescription = ex
            ErrorFlag = True
        Finally
            oServerConfig.MySQLCalls += 1
        End Try
    End Sub

    Public Function UpdateAvatar() As Boolean
        Try
            Dim oMySqlHelper As New MySQLFactory.MySQLDBHelper

            Dim arParms() As MySqlParameter = New MySqlParameter(5) {}
            arParms(0) = New MySqlParameter("?sGender", Gender)
            arParms(1) = New MySqlParameter("?sBackground", Background)
            arParms(2) = New MySqlParameter("?sHead", Head)
            arParms(3) = New MySqlParameter("?sBody", Body)
            arParms(4) = New MySqlParameter("?iChanged", IIf(Changed = True, 1, 0))
            arParms(5) = New MySqlParameter("?sNick", Player)

            oMySqlHelper.DBExecuteScalar(ConnectionString, CommandType.StoredProcedure, "spu_PlayerAvatar", arParms)
            Return True
        Catch ex As Exception
            ErrorDescription = ex
            ErrorFlag = True
            Return False
        Finally
            oServerConfig.MySQLCalls += 1
        End Try
    End Function

    Protected Function UpdateUserInfo(ByVal sNick As String, ByVal sPassword As String, ByVal iNewsletter As Integer, ByVal iIncentive As Integer, ByVal sCountry As String) As Boolean
        Try
            Dim oMySqlHelper As New MySQLFactory.MySQLDBHelper

            Dim arParms() As MySqlParameter = New MySqlParameter(4) {}
            arParms(0) = New MySqlParameter("?sNick", sNick)
            arParms(1) = New MySqlParameter("?sPassword", sPassword)
            arParms(2) = New MySqlParameter("?iNewsletter", iNewsletter)
            arParms(3) = New MySqlParameter("?iIncentive", iIncentive)
            arParms(4) = New MySqlParameter("?sCountry", sCountry)

            oMySqlHelper.DBExecuteScalar(ConnectionString, CommandType.StoredProcedure, "spu_AccountInfo", arParms)
            Return True
        Catch ex As Exception
            ErrorDescription = ex
            ErrorFlag = True
            Return False
        Finally
            oServerConfig.MySQLCalls += 1
        End Try
    End Function

    Public Sub SendGold()
        Try
            Send(String.Empty, String.Concat("UPDATEGP ", Gold), PlayerSocket)
        Catch ex As Exception
            Call errorsub(ex, "SendGold")
        End Try
    End Sub
End Class
