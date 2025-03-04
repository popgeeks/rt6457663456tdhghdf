Imports System.Configuration

Module dbfunctions
    Public Function totalottcards(ByRef strPlayer As String) As Integer
        totalottcards = Val(getDBField("SELECT count(*) as totalcards FROM playercards WHERE player = '" & strPlayer & "' AND value <> 0", "totalcards"))
    End Function

    'Public Function getServerStats(ByRef sField As String) As String
    '    getServerStats = getDBField("SELECT * FROM config", sField)
    'End Function

    'Public Sub setServerStats(ByRef sField As String, ByVal sValue As String)
    '    Call dbEXECQuery("UPDATE config SET " & sField & " = " & sField & " + " & sValue)
    'End Sub

    Public Function getAdministrator_Field(ByRef strNick As String, ByRef strField As String) As String
        getAdministrator_Field = getDBField("SELECT * FROM Administrators WHERE player = '" & strNick & "'", strField)

        If getAdministrator_Field = "" Then getAdministrator_Field = "0"
    End Function

    Public Function getAccount_Field(ByRef strNick As String, ByRef strField As String) As String
        On Error GoTo ErrorHandler

        getAccount_Field = getDBField("SELECT * FROM accounts WHERE player = '" & strNick & "'", strField)

        Exit Function
ErrorHandler:
        Call errorsub(Err.Description, "getaccount_field")
    End Function

    Public Function getAAField(ByRef strNick As String, ByRef strKey As String, ByRef strField As String) As String
        On Error GoTo ErrorHandler

        getAAField = getDBField("SELECT * FROM player_advancements WHERE player = '" & strNick & "' and keyword = '" & strKey & "'", strField)

        Exit Function
ErrorHandler:
        Call errorsub(Err.Description, "getAAField")
    End Function

    Public Function getAAMod(ByVal strNick As String, ByVal strKey As String) As String
        Try
            Return getDBField(String.Format("SELECT fx_GetPlayerAdvancementLevelValue('{0}', '{1}') as AAMod", strNick, strKey), "AAMod")
        Catch ex As Exception
            Call errorsub(ex.ToString, "getAAMod")
            Return 0
        End Try
    End Function

    Public Function getVAMod(ByVal strNick As String, ByVal strKey As String) As Double
        Try
            Return getDBField(String.Format("SELECT fx_GetVeteranAdvancementLevelValue('{0}', '{1}') as AAMod", strNick, strKey), "AAMod")
        Catch ex As Exception
            Call errorsub(ex.ToString, "getVAMod")
            Return 0
        End Try
    End Function

    Public Function setaccountdata(ByVal strNick As String, ByVal strField As String, ByVal strData As String, Optional ByVal intBit As Integer = 0) As Boolean
        Try
            Dim oFunctions As New DatabaseFunctions

            If intBit = 0 Then
                oFunctions.AccountUpdate(ConnectionString, strNick, strField, strData)
                '            setaccountdata = dbEXECQuery("UPDATE accounts SET " & strField & "='" & strData & "' WHERE player = '" & strNick & "'")
            Else
                oFunctions.AccountUpdate(ConnectionString, strNick, strField, CInt(Val(strData)))
                'setaccountdata = dbEXECQuery("UPDATE accounts SET " & strField & "=" & strData & " WHERE player = '" & strNick & "'")
            End If
        Catch ex As Exception
            Call errorsub(ex.Message, "setaccountdata")
        End Try
    End Function

    Public Function expnextlevel(ByVal sNick As String) As String
        expnextlevel = getDBField("SELECT levels.exp as nextlvl FROM accounts INNER JOIN levels ON accounts.level = levels.level WHERE accounts.player = '" & sNick & "'", "nextlvl")
    End Function
End Module
