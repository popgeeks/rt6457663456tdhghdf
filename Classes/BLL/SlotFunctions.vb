Public Class SlotFunctions
    Public Sub SlotModule(ByVal sCommand As String, ByVal incoming As String, ByVal socket As Integer)
        Select Case sCommand
            Case "SLOTSFF7"
                Dim oSlots As New clsSlots(socket, "ff7")
                oSlots.slots()
            Case "SLOTSANIME"
                Dim oSlots As New clsSlots(socket, "anime")
                oSlots.slots()
        End Select
    End Sub
End Class
