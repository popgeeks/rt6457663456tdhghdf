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

                Dim oEncryption As New LogicLayer.Utility.Encryption
                sChatText = oEncryption.Encode(String.Concat(sBlock, " ", sChatText).Trim)
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
            'Call ErrorSub(ex.ToString, "ChatFunctions.unisend")
        End Try
    End Sub

    Public Sub uniwarn(ByVal sChatText As String)
        Try
            Dim oEncryption As New LogicLayer.Utility.Encryption
            sChatText = oEncryption.Encode(String.Concat("ADMINWARNING ", sChatText.Replace(" ", "%20")))

            For x As Integer = 0 To frmserver.lstadmin.Items.Count - 1
                Dim sDivide() As String = frmserver.lstadmin.Items(x).ToString.Split(" ")
                Send(sDivide(0), sChatText, , True)
            Next x
        Catch ex As Exception
            Call errorsub(ex.ToString, "ChatFunctions.unisend")
        End Try
    End Sub
End Class
