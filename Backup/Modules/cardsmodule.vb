Module cardsmodule
    Public Function cardchg(ByVal nick As String, ByVal cardname As String, ByVal amount As Short, Optional ByVal blnWipe As Boolean = False) As Boolean
        Try
            cardname = cardname.Replace("%20", " ")

            If blnWipe = True Then amount = 0

            Dim iCount As Integer = GetRowCount("SELECT * FROM playercards WHERE player = '" & nick & "'" & "AND cardname = '" & cardname & "'")
            Dim iNewTotal As Integer = Val(getDBField("call usp_CardChange('" & nick & "', '" & cardname & "', " & amount & ")", "NewTotal"))

            If iNewTotal < 0 Then
                Dim oAdminFunctions As New AdminFunctions
                oAdminFunctions.ban("BAN " & nick & " " & "Card%20Cheating", 0)
                cardchg = False
                Exit Function
            End If

            Return True
        Catch ex As Exception
            Call errorsub(ex.ToString, "cardchg")
            Return False
        End Try
    End Function

    Public Function findcardamt(ByVal nick As String, ByVal cardname As String) As Integer
        Try
            findcardamt = Val(getDBField("SELECT value FROM playercards WHERE player = '" & nick & "'" & "AND cardname = '" & cardname.Replace("%20", " ") & "'", "value"))
        Catch ex As Exception
            Call errorsub(ex.Message, "findcardamt - " & nick & " - " & cardname & " - " & findcardamt)
        End Try
    End Function
End Module