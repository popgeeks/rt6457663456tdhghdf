Imports System.Configuration

Public Class ChatFunctions
    Public Sub unisend(ByVal sBlock As String, ByVal sChatText As String)
        Try
            Dim lostvar As String = String.Empty
            Dim oDatabaseFunctions As New DatabaseFunctions
            Dim oDataTable As DataTable = oDatabaseFunctions.OnlinePlayers()

            If sBlock = "CHAT" Or sBlock = "GAMECHAT" Then
                sChatText = String.Concat(sBlock, " ", sChatText).Trim
            Else
                sChatText = String.Concat(sBlock, " ", sChatText).Trim
            End If

            If Not (oDataTable Is Nothing) Then
                For x As Integer = 0 To oDataTable.Rows.Count - 1
                    Dim iID As Integer = Val(oDataTable.Rows(x).Item("ID").ToString)
                    If frmSystray.Winsock(iID).Tag <> String.Empty Then
                        Send(String.Empty, sChatText, iID, True)
                    End If
                Next x
            End If
        Catch ex As Exception
            Call errorsub(ex, "ChatFunctions.unisend")
        End Try
    End Sub

    Public Sub uniwarn(ByVal sChatText As String)
        Try
            sChatText = String.Concat("ADMINWARNING ", sChatText.Replace(" ", "%20"))

            If AdminList.Count > 0 Then
                For x As Integer = 0 To AdminList.Count - 1
                    Send(AdminList(x).Player, sChatText, , True)
                Next x
            End If
        Catch ex As Exception
            Call errorsub(ex, "ChatFunctions.unisend")
        End Try
    End Sub
End Class
