Imports MySql.Data.MySqlClient
Imports MySQLFactory.MySQLDBHelper

Public Class AlternateAdvancementDAL
    Private iID As Integer
    Private sKeyword As String
    Private sName As String
    Private sDescription As String
    Private iMinLevel As Integer
    Private iLevels As Integer
    Private iPoints As Integer
    Private sMod1 As String
    Private sMod2 As String
    Private sMod3 As String
    Private sMod4 As String
    Private sMod5 As String

    Public Property Points() As Integer
        Get
            Return iPoints
        End Get
        Set(ByVal value As Integer)
            iPoints = value
        End Set
    End Property

    Public Property Levels() As Integer
        Get
            Return iLevels
        End Get
        Set(ByVal value As Integer)
            iLevels = value
        End Set
    End Property

    Public Property MinimumLevels() As Integer
        Get
            Return iMinLevel
        End Get
        Set(ByVal value As Integer)
            iMinLevel = value
        End Set
    End Property

    Public Property Level5Mod() As String
        Get
            Return sMod5
        End Get
        Set(ByVal value As String)
            sMod5 = value
        End Set
    End Property

    Public Property Level4Mod() As String
        Get
            Return sMod4
        End Get
        Set(ByVal value As String)
            sMod4 = value
        End Set
    End Property

    Public Property Level3Mod() As String
        Get
            Return sMod3
        End Get
        Set(ByVal value As String)
            sMod3 = value
        End Set
    End Property

    Public Property Level2Mod() As String
        Get
            Return sMod2
        End Get
        Set(ByVal value As String)
            sMod2 = value
        End Set
    End Property

    Public Property Level1Mod() As String
        Get
            Return sMod1
        End Get
        Set(ByVal value As String)
            sMod1 = value
        End Set
    End Property

    Public Property Description() As String
        Get
            Return sDescription
        End Get
        Set(ByVal value As String)
            sDescription = value
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

    Public Property Keyword() As String
        Get
            Return sKeyword
        End Get
        Set(ByVal value As String)
            sKeyword = value
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

    Public Function UpdateAAPoints(ByVal sNick As String, ByVal sKey As String, ByVal iPoints As Integer) As Boolean
        Try
            Dim oMySqlHelper As New MySQLFactory.MySQLDBHelper
            Dim arParms() As MySqlParameter = New MySqlParameter(2) {}

            arParms(0) = New MySqlParameter("?sNick", sNick)
            arParms(1) = New MySqlParameter("?iPoints", iPoints)
            arParms(2) = New MySqlParameter("?sKey", sKey)
            oMySqlHelper.DBExecuteScalar(ConnectionString, CommandType.StoredProcedure, "spu_Account_AlternateAdvancement", arParms)
            Return True
        Catch ex As Exception
            Call errorsub(ex, "UpdateAAPoints")
            Return False
        Finally
            oServerConfig.MySQLCalls += 1
        End Try
    End Function

    Protected Sub GetAlternateAdvancement(ByVal sKey As String)
        Try
            Dim oMySqlHelper As New MySQLFactory.MySQLDBHelper
            Dim arParms() As MySqlParameter = New MySqlParameter(0) {}
            Dim oDataRow As DataRow = Nothing

            arParms(0) = New MySqlParameter("?sKey", sKey)
            oMySqlHelper.FillDataRow(ConnectionString, CommandType.StoredProcedure, "sps_AlternateAdvancement", oDataRow, arParms)

            If Not (oDataRow Is Nothing) Then
                With oDataRow
                    ID = .Item("id")
                    Keyword = .Item("keyword").ToString
                    Name = .Item("name").ToString
                    Description = .Item("description").ToString
                    MinimumLevels = .Item("minlvl")
                    Levels = .Item("levels")
                    Points = .Item("points")
                    Level1Mod = .Item("lvl1_mod").ToString
                    Level2Mod = .Item("lvl2_mod").ToString
                    Level3Mod = .Item("lvl3_mod").ToString
                    Level4Mod = .Item("lvl4_mod").ToString
                    Level5Mod = .Item("lvl5_mod").ToString
                End With
            Else
                ClearAll()
            End If
        Catch ex As Exception
            Call errorsub(ex, "GetAlternateAdvancement")
        Finally
            oServerConfig.MySQLCalls += 1
        End Try
    End Sub

    Private Sub ClearAll()
        ID = -1
        Keyword = String.Empty
        Name = String.Empty
        Description = String.Empty
        MinimumLevels = 0
        Levels = 0
        Points = 0
        Level1Mod = String.Empty
        Level2Mod = String.Empty
        Level3Mod = String.Empty
        Level4Mod = String.Empty
        Level5Mod = String.Empty
    End Sub
End Class
