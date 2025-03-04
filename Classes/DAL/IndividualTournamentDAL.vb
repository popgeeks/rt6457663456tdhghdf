Imports mySQL.Data.MySqlClient
Imports MySQLFactory
Imports System.ComponentModel
Imports System.Configuration

Public Class IndividualTournamentDAL
    Private bError As Boolean
    Private oError As Exception

    Private iID As Integer
    Private iAP As Integer
    Private iGold As Integer
    Private sName As String
    Private sRuleset As String
    Private sGame As String
    Private iCompleted As Integer
    Private dDate As DateTime
    Private bExpired As Boolean

    Public Sub New()

    End Sub

    Protected Sub ClearAll()
        iID = 0
        iAP = 0
        iGold = 0
        sName = String.Empty
        sRuleset = String.Empty
        sGame = String.Empty
        iCompleted = 0
        dDate = Nothing
        bExpired = False
    End Sub

    Public ReadOnly Property Expired() As Boolean
        Get
            Return bExpired
        End Get
    End Property
    Public Property CreateDate() As DateTime
        Get
            Return dDate
        End Get
        Set(ByVal value As DateTime)
            dDate = value
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
    Public Property Ruleset() As String
        Get
            Return sRuleset
        End Get
        Set(ByVal value As String)
            sRuleset = value
        End Set
    End Property
    Public Property Name() As String
        Get
            Return sName
        End Get
        Set(ByVal value As String)
            sName = value
        End Set
    End Property

    Public Property Game() As String
        Get
            Return sGame
        End Get
        Set(ByVal value As String)
            sGame = value
        End Set
    End Property
    Public ReadOnly Property IsFreeRoll() As Boolean
        Get
            Return IIf(iAP = 0 And iGold = 0, True, False)
        End Get
    End Property

    Public Property APCost() As Integer
        Get
            Return iAP
        End Get
        Set(ByVal value As Integer)
            iAP = value
        End Set
    End Property

    Public Property GoldCost() As Integer
        Get
            Return iGold
        End Get
        Set(ByVal value As Integer)
            iGold = value
        End Set
    End Property


    Public Property ID() As Integer
        Get
            Return iID
        End Get
        Set(ByVal value As Integer)
            iID = value
        End Set
    End Property

    Public Sub LoadRecord(ByVal iID As Integer)
        Try
            If iID = 0 Then
                ClearAll()
                Exit Sub
            End If

            Dim oMySqlHelper As New MySQLFactory.MySQLDBHelper
            Dim oDataSet As New DataSet

            Dim arParms() As MySqlParameter = New MySqlParameter(0) {}
            arParms(0) = New MySqlParameter("?iID", iID)

            oMySqlHelper.FillDataSet(ConnectionString, CommandType.StoredProcedure, "sps_PlayerTournament", oDataSet, New String() {"Results"}, arParms)

            If oDataSet Is Nothing Then
                Throw New Exception
            Else
                With oDataSet.Tables(0).Rows(0)
                    ID = .Item("ID")
                    Name = .Item("Name").ToString
                    If IsDBNull(.Item("Date")) = False Then CreateDate = .Item("Date")
                    Game = .Item("Game").ToString
                    GoldCost = .Item("Gold")
                    APCost = .Item("AP")
                    Ruleset = .Item("Ruleset").ToString
                    Completed = .Item("Completed")
                    bExpired = IIf(.Item("Expired") = 1, True, False)
                End With
            End If
        Catch ex As Exception
            ErrorDescription = ex
            ErrorFlag = True
            Call errorsub(ex.ToString, "IndividualTournamentDAL.LoadRecord")
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

    Public Sub New(ByVal ID As Integer)
        ClearAll()
        LoadRecord(ID)
    End Sub
End Class
