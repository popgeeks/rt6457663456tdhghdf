Imports MySql.Data.MySqlClient
Imports System.Configuration

Module modMYSQL
    Public ReadOnly Property ConnectionString() As String
        Get
            Return "Database=darkwind;Data Source=127.0.0.1;User Id=root;Password=412^luck"
        End Get
    End Property

    Public ReadOnly Property TTEConnectionString() As String
        Get
            Return "Database=ttechat;Data Source=127.0.0.1;User Id=root;Password=412^luck"
        End Get
    End Property

    Public Function GetDataRow(ByVal sSQL As String) As DataRow
        Dim ds As New DataSet
        Dim dt As New DataTable

        Dim da As New MySqlDataAdapter
        Dim con As MySqlConnection = New MySqlConnection(ConnectionString)

        Dim cmd As MySqlCommand = New MySqlCommand(sSQL, con)

        da.SelectCommand = cmd
        da.Fill(ds)

        Try
            dt = ds.Tables(0)

            If dt.Rows.Count <> 0 Then
                Return dt.Rows(0)
            Else
                Return Nothing
            End If
        Catch ex As Exception
            Dim oText As New LogicLayer.Utility.StringFunctions
            Call oText.SaveTextToFile(String.Concat(ex.Message, ">> ", sSQL), My.Application.Info.DirectoryPath & "\logs\" & Format(Date.Today, "MM-dd-yyyy").ToString & ".log")
            oText = Nothing
            Return Nothing
        Finally
            con.Close()
            dt.Dispose()
            ds.Dispose()
            da.Dispose()
            con.Dispose()
            oServerConfig.MySQLCalls += 1
        End Try
    End Function

    Public Function GetDataRow(ByVal sSQL As String, ByVal iRow As Integer) As DataRow
        Dim ds As New DataSet
        Dim dt As New DataTable

        Dim da As New MySqlDataAdapter
        Dim con As MySqlConnection = New MySqlConnection(ConnectionString)

        Dim cmd As MySqlCommand = New MySqlCommand(sSQL, con)

        da.SelectCommand = cmd

        da.Fill(ds)

        Try
            dt = ds.Tables(iRow)
            Return dt.Rows(iRow)
        Catch ex As Exception
            Dim oText As New LogicLayer.Utility.StringFunctions
            Call oText.SaveTextToFile(String.Concat(ex.Message, ">> ", sSQL), My.Application.Info.DirectoryPath & "\logs\" & Format(Date.Today, "MM-dd-yyyy").ToString & ".log")
            oText = Nothing
            Return Nothing
        Finally
            con.Close()
            dt.Dispose()
            ds.Dispose()
            da.Dispose()
            con.Dispose()
            oServerConfig.MySQLCalls += 1
        End Try
    End Function
    Public Function GetDataTable(ByVal sSql As String, ByVal parameterList As String) As DataTable
        '//parameterList will be in this form: "@currentCountry,varchar/255,USA;@cutOffDate,date,1/1/1963"

        Dim ds As New DataSet
        Dim dt As New DataTable

        Dim da As New MySqlDataAdapter
        Dim con As MySqlConnection = New MySqlConnection(ConnectionString)

        Dim cmd As MySqlCommand = New MySqlCommand(sSql, con)

        'GetParameters(cmd, parameterList)
        da.SelectCommand = cmd
        da.Fill(ds)

        Try
            dt = ds.Tables(0)
            Return dt
        Catch ex As Exception
            Dim oText As New LogicLayer.Utility.StringFunctions
            Call oText.SaveTextToFile(String.Concat(ex.Message, ">> ", sSql), My.Application.Info.DirectoryPath & "\logs\" & Format(Date.Today, "MM-dd-yyyy").ToString & ".log")
            oText = Nothing
            Return Nothing
        Finally
            con.Close()
            dt.Dispose()
            ds.Dispose()
            da.Dispose()
            con.Dispose()
            oServerConfig.MySQLCalls += 1
        End Try
    End Function
    Public Function GetRowCount(ByVal sSql As String) As Int64

        Dim ds As New DataSet
        Dim dt As New DataTable
        Dim iCount As Int64

        Dim da As New MySqlDataAdapter
        Dim con As MySqlConnection = New MySqlConnection(ConnectionString)

        Dim cmd As MySqlCommand = New MySqlCommand(sSql, con)

        'GetParameters(cmd, parameterList)
        da.SelectCommand = cmd
        da.Fill(ds)

        Try
            dt = ds.Tables(0)
            iCount = dt.Rows.Count
            Return iCount
        Catch ex As Exception
            Dim oText As New LogicLayer.Utility.StringFunctions
            Call oText.SaveTextToFile(String.Concat(ex.Message, ">> ", sSql), My.Application.Info.DirectoryPath & "\logs\" & Format(Date.Today, "MM-dd-yyyy").ToString & ".log")
            oText = Nothing
            Return Nothing
        Finally
            con.Close()
            dt.Dispose()
            ds.Dispose()
            da.Dispose()
            con.Dispose()
            oServerConfig.MySQLCalls += 1
        End Try
    End Function
    Public Function GetDataSet(ByVal sSql As String) As DataSet
        Dim ds As New DataSet
        Dim dt As New DataTable

        Dim da As New MySqlDataAdapter
        Dim con As MySqlConnection = New MySqlConnection(ConnectionString)

        Dim cmd As MySqlCommand = New MySqlCommand(sSql, con)

        da.SelectCommand = cmd
        da.Fill(ds)

        Try
            Return ds
        Catch ex As Exception
            Dim oText As New LogicLayer.Utility.StringFunctions
            Call oText.SaveTextToFile(String.Concat(ex.Message, ">> ", sSql), My.Application.Info.DirectoryPath & "\logs\" & Format(Date.Today, "MM-dd-yyyy").ToString & ".log")
            oText = Nothing
            Return Nothing
        Finally
            con.Close()
            dt.Dispose()
            ds.Dispose()
            da.Dispose()
            con.Dispose()
            oServerConfig.MySQLCalls += 1
        End Try
    End Function

    Public Function dbEXECQuery(ByVal sSQL As String) As Integer

        'sParameterList -> parameter#value@parameter#value@parameter#value
        Dim ds As New DataSet
        Dim dt As New DataTable

        Dim da As New MySqlDataAdapter
        Dim con As New MySqlConnection(ConnectionString)

        Try
            Dim cmd As New MySqlCommand(sSQL, con)

            cmd.CommandType = CommandType.Text

            con.Open()

            Dim iAffected As Integer = cmd.ExecuteNonQuery

            If iAffected = 1 Then
                Return 1
            Else
                Return 0
            End If
        Catch ex As Exception
            Dim oText As New LogicLayer.Utility.StringFunctions
            Call oText.SaveTextToFile(String.Concat(ex.Message, ">> ", sSQL), My.Application.Info.DirectoryPath & "\logs\" & Format(Date.Today, "MM-dd-yyyy").ToString & ".log")
            oText = Nothing
            Return False
        Finally
            con.Close()
            dt.Dispose()
            ds.Dispose()
            da.Dispose()
            con.Dispose()
            oServerConfig.MySQLCalls += 1
        End Try
    End Function

    Public Function getDBField(ByVal sSQL As String, ByVal sDataField As String) As String
        Dim ds As New DataSet, dt As New DataTable, da As New MySqlDataAdapter
        Dim con As New MySqlConnection(ConnectionString)
        Dim dr As DataRow

        Dim cmd As New MySqlCommand(sSQL, con), sValue As String

        da.SelectCommand = cmd
        da.Fill(ds)

        Try
            dt = ds.Tables(0)

            If dt.Rows.Count > 0 Then
                dr = dt.Rows(0)
                sValue = dr.Item(sDataField)

                If sValue <> "" Then
                    Return sValue
                Else
                    Return Nothing
                End If
            Else
                Return Nothing
            End If
        Catch ex As Exception
            'Dim oText As New StringFunctions
            'Call oText.SaveTextToFile(String.Concat(ex.Message, ">> ", sSQL), My.Application.Info.DirectoryPath & "\logs\" & Format(Date.Today, "MM-dd-yyyy").ToString & ".log")
            'oText = Nothing
            Return Nothing
        Finally
            con.Close()
            dt.Dispose()
            ds.Dispose()
            da.Dispose()
            con.Dispose()
            oServerConfig.MySQLCalls += 1
        End Try
    End Function
End Module
