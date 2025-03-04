Public Class PacketContainer
    Inherits List(Of Packets)

    Public Overloads Sub Add(ByVal sPlayer As String, ByVal sPacket As String, ByVal iSocket As Integer, ByVal dStamp As DateTime)
        Dim oPacket As New Entities.Packets

        With oPacket
            .Player = sPlayer
            .Packet = sPacket
            .Socket = iSocket
            .TimeStamp = dStamp
        End With

        MyBase.Add(oPacket)
    End Sub
End Class

Public Class Packets
    Public Player As String
    Public Packet As String
    Public Socket As Integer
    Public TimeStamp As DateTime
End Class