Imports MySqlDBHelper.MySQLDBHelper
Imports MySql.Data.MySqlClient

Module replenishdeck
    Public Function gpadd(ByVal sNick As String, ByVal iAmount As Integer, Optional ByVal blnSend As Boolean = False) As Boolean
        Try
            Dim oPlayerAccount As New PlayerAccountDAL(sNick)
            Dim oChatFunctions As New ChatFunctions

            oPlayerAccount.Gold += iAmount

            Call setaccountdata(sNick, "gold", oPlayerAccount.Gold.ToString, True)

            If blnSend = True Then Send(sNick, String.Concat("GPADD ", iAmount))

            If oPlayerAccount.Gold < 0 Then
                Call setaccountdata(sNick, "gold", 0, True)

                oChatFunctions.uniwarn(String.Concat(sNick, " had less than 0 gold and bypassed all filters"))
                Call ban(String.Concat("BAN ", sNick, " ", "Card%20Cheating"), 0)
            End If
        Catch ioorex As System.IndexOutOfRangeException
            ' do nothing
        Catch ex As Exception
            Call errorsub(ex.ToString, "gpadd")
        End Try
    End Function
End Module