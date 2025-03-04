Imports System.Configuration

Public Class clsSlots
    Private socket As Integer
    Private sNick As String
    Private sType As String

    Public Sub New(ByVal iSocket As Integer, ByVal strType As String)
        socket = iSocket
        sNick = frmSystray.Winsock(iSocket).Tag
        sType = strType
    End Sub
#Region "Slots"
    Public Sub slots()
        Dim x As Integer, iRoll As Integer, slot(2) As Integer, iPrize As Integer, iGameID As Integer
        Dim dRow As DataRow, dwin As DataTable
        Dim sCombo As String = String.Empty
        Dim oFunctions As New DatabaseFunctions
        Dim oTables As New TableFunctions
        Dim oChatFunctions As New ChatFunctions

        Try
            Dim blnTrip As Boolean

            If oTables.findtableplayer(sNick) = True Then
                Call blockchat(sNick, "Problem", "You cannot play slots when you are already in a game.")
                Exit Sub
            End If

            dRow = GetDataRow("call sps_slots('" & sType & "', '" & sNick & "')")

            If dRow.Item("switch") = "0" Then
                Call blockchat(sNick, "Problem", dRow.Item("problem").ToString)
                Exit Sub
            End If

            If Val(dRow.Item("ap")) < Val(dRow.Item("cost").ToString) Then
                Call blockchat(sNick, "Problem", "You do not have enough AP to play")

                oChatFunctions.uniwarn(sNick & " tried to play slots but did not have enough AP")
                Call historyadd(sNick, 2, sNick & " tried to play slots but did not have enough AP")
                Exit Sub
            End If

            If Val(dRow.Item("cost").ToString) <= 0 Then
                Call blockchat(sNick, "Problem", "Server could not process your slot play request.  Try again later")
                Exit Sub
            End If

            Dim oGameFunctions As New GameFunctions
            iGameID = oGameFunctions.NewGameID

            Send(String.Empty, "UPDATEAP " & giveap(sNick, -1 * Val(dRow.Item("cost").ToString), False), socket)

            blnTrip = False

            For x = 1 To 3
                Randomize()

                iRoll = Int(Rnd() * Val(dRow.Item("total").ToString)) + 1

                If iRoll <= Val(dRow.Item("limit1").ToString) Then
                    slot(x - 1) = 1
                ElseIf iRoll <= Val(dRow.Item("limit2").ToString) Then
                    slot(x - 1) = 2
                    blnTrip = True
                ElseIf iRoll <= Val(dRow.Item("limit3").ToString) Then
                    slot(x - 1) = 3
                ElseIf iRoll <= Val(dRow.Item("limit4").ToString) Then
                    slot(x - 1) = 4
                ElseIf iRoll <= Val(dRow.Item("limit5").ToString) Then
                    slot(x - 1) = 5
                ElseIf iRoll <= Val(dRow.Item("limit6").ToString) Then
                    slot(x - 1) = 6
                ElseIf iRoll <= Val(dRow.Item("limit7").ToString) Then
                    slot(x - 1) = 7
                ElseIf iRoll <= Val(dRow.Item("limit8").ToString) Then
                    slot(x - 1) = 8
                ElseIf iRoll <= Val(dRow.Item("limit9").ToString) Then
                    slot(x - 1) = 9
                End If
            Next

            ' Now let's find if they are a winner
            sCombo = slot(0) & slot(1) & slot(2)

            If sCombo <> "222" And blnTrip = True Then
                Dim y As Integer

                If Left(sCombo, 1) = "2" Then y += 1
                If Mid(sCombo, 2, 1) = "2" Then y += 1
                If Right(sCombo, 1) = "2" Then y += 1

                If y = 1 Then sCombo = "2**"
                If y = 2 Then sCombo = "22*"
            End If

            dwin = GetDataTable("call sps_slotswin('" & sType & "', '" & sCombo & "')", String.Empty)

            If dwin.Rows.Count = 0 Then
                ' you didnt win anything
                Call blockchat(sNick, "Slots", getDBField("call sps_RandomSlotLine('" & sType & "')", "message"))
                Send(String.Empty, "SLOTS " & sType & " " & slot(0) & " " & slot(1) & " " & slot(2), socket)
            Else
                iPrize = Int(Val(dRow.Item("cost").ToString) * Val(dwin.Rows(0).Item("prize").ToString))

                Send(String.Empty, "SLOTS " & sType & " " & slot(0) & " " & slot(1) & " " & slot(2) & " 1", socket)
                Call blockchat(sNick, "Slots", dwin.Rows(0).Item("winmsg").ToString & " You earned " & iPrize & " AP!")
                Send(String.Empty, "UPDATEAP " & giveap(sNick, iPrize, False), socket)

                If sCombo = "999" Then
                    oChatFunctions.unisend("ADDBLOCK", "Slots" & " " & frmSystray.Winsock(socket).Tag & "%20hit%20the%20" & sType & "%20slot%20jackpot!" & " " & CStr(System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Yellow)))
                    oFunctions.SendEmail(ConnectionString, "support@tripletriadextreme.com", "Jackpot Winner", frmSystray.Winsock(socket).Tag & " won the " & sType & " slot jackpot!")
                End If
            End If
        Catch ex As Exception
            Call errorsub(ex.ToString & " (" & sNick & " - " & sType & " - SLOTS)", "slots")
        Finally
            Call dbEXECQuery("call spi_SlotGames(" & iGameID & ", '" & sNick & "', '" & sCombo & "', '" & iPrize & "', '" & sType & "')")
            Send(String.Empty, "UNLOCKSLOT " & sType, socket)
        End Try
    End Sub
#End Region
End Class

