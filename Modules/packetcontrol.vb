Option Strict Off
Option Explicit On
Module packetcontrol
    Public Function checkpacket(ByRef socket As Integer) As Boolean
        If socket = 0 Then Exit Function

        frmserver.lstpacketcontrol.Items.Item(socket) = CStr(Val(frmserver.lstpacketcontrol.Items.Item(socket)) + 1)

		If Val(frmserver.lstpacketcontrol.Items.Item(socket).ToString) >= 30 Then
			Call quit("QUIT", socket)
			checkpacket = True
			Exit Function
		End If

    End Function
End Module