'Imports MySql.Data.MySqlClient
'Imports MySQLFactory.MySQLDBHelper
'Imports System.Configuration

'Module modNewDecks
'    Public Function newdeckstoday(ByVal sNick As String) As String
'        Dim oMySQLDBHelper As New MySQLFactory.MySQLDBHelper
'        Dim oDatarow As DataRow = Nothing

'        Dim arParms() As MySqlParameter = New MySqlParameter(0) {}
'        arParms(0) = New MySqlParameter("?nick", sNick)

'        oMySQLDBHelper.FillDataRow(ConnectionString, CommandType.StoredProcedure, "usp_NewDeckCount", oDatarow, arParms)

'        If Not (oDatarow Is Nothing) Then
'            newdeckstoday = oDatarow.Item("NewDecksToday").ToString
'        Else
'            newdeckstoday = String.Empty
'        End If
'        'newdeckstoday = getDBField("call usp_NewDeckCount('" & sNick & "')", "NewDecksToday")
'    End Function
'End Module