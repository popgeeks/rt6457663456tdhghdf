Imports mySQL.Data.MySqlClient
Imports MySQLFactory
Imports System.ComponentModel
Imports System.Configuration

Public Class CardFunctions
    Private bError As Boolean
    Private oError As Exception

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

    Public Function GetRandomCards(ByVal iCards As Integer) As DataSet
        Try
            Dim oMySqlHelper As New MySQLFactory.MySQLDBHelper
            Dim oDataSet As New DataSet

            Dim arParms() As MySqlParameter = New MySqlParameter(0) {}
            arParms(0) = New MySqlParameter("?iCards", iCards)

            oMySqlHelper.FillDataSet(ConnectionString, CommandType.StoredProcedure, "sps_GetRandomCards", oDataSet, New String() {"Results"}, arParms)

            Return oDataSet
        Catch ex As Exception
            ErrorDescription = ex
            ErrorFlag = True
            Return Nothing
        Finally
            oServerConfig.MySQLCalls += 1
        End Try
    End Function

    Public Function GetRandomPlayerCards(ByVal sNick As String, ByVal iCards As Integer) As DataSet
        Try
            Dim oMySqlHelper As New MySQLFactory.MySQLDBHelper
            Dim oDataSet As New DataSet

            Dim arParms() As MySqlParameter = New MySqlParameter(1) {}
            arParms(0) = New MySqlParameter("?sNick", sNick)
            arParms(1) = New MySqlParameter("?iCards", iCards)

            oMySqlHelper.FillDataSet(ConnectionString, CommandType.StoredProcedure, "sps_GetRandomPlayerCards", oDataSet, New String() {"Results"}, arParms)

            Return oDataSet
        Catch ex As Exception
            ErrorDescription = ex
            ErrorFlag = True
            Return Nothing
        Finally
            oServerConfig.MySQLCalls += 1
        End Try
    End Function
End Class
