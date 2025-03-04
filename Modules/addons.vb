Module addons
    Public Sub blockchat(ByVal strPlayer As String, ByVal strBlock As String, ByVal strText As String, Optional ByVal iSocket As Integer = 0)
        If iSocket = 0 Then
            Send(strPlayer, String.Concat("CHATBLOCK ", strBlock.Replace(" ", "%20"), " ", strText.Replace(" ", "%20")))
        Else
            Send(String.Empty, String.Concat("CHATBLOCK ", strBlock.Replace(" ", "%20"), " ", strText.Replace(" ", "%20")), iSocket)
        End If
    End Sub
End Module