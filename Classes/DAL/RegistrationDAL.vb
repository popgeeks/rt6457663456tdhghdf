Imports mySQL.Data.MySqlClient
Imports MySQLFactory
Imports System.ComponentModel

Public Class RegistrationDAL
    Private iPlayers As Integer
    Private iEmails As Integer
    Private iValid As Integer
    Private bError As Boolean
    Private oError As Exception

    Public ReadOnly Property ValidPlayer() As Boolean
        Get
            Return IIf(iPlayers = 0 And iEmails = 0 And iValid = 0, True, False)
        End Get
    End Property

    Public ReadOnly Property ValidEmail() As Boolean
        Get
            Return IIf(iEmails = 0, True, False)
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

    Protected Sub CheckNick(ByVal sNick As String, ByVal sEmail As String)
        Dim oDataRow As DataRow = Nothing
        Dim oMySQLHelper As New MySQLFactory.MySQLDBHelper

        Try
            Dim arParms() As MySqlParameter = New MySqlParameter(1) {}
            arParms(0) = New MySqlParameter("?sNick", sNick)
            arParms(1) = New MySqlParameter("?sEmail", sEmail)

            oMySQLHelper.FillDataRow(ConnectionString, CommandType.StoredProcedure, "sps_ValidRegistration", oDataRow, arParms)

            If Not (oDataRow Is Nothing) Then
                With oDataRow
                    iPlayers = .Item("Players")
                    iEmails = .Item("Email")
                    iValid = .Item("Reserved")
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

    Private Sub ClearAll()
        iPlayers = 0
        iEmails = 0
        iValid = 0
    End Sub
End Class
