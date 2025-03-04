Public Class PlayerAccount
    Inherits PlayerAccountDAL

    Public Sub AccountModule(ByVal sCommand As String, ByVal incoming As String, ByVal socket As Integer)
        Select Case sCommand
            Case "CHPASS"
                Call chpass(incoming, socket)
        End Select
    End Sub

    ''' <summary>
    ''' Change password
    ''' 
    ''' 0 = lostvar, 1 = new password, 2 = newsletter, 3 = incentive
    ''' </summary>
    ''' <param name="incoming"></param>
    ''' <param name="socket"></param>
    ''' <remarks></remarks>
    Private Sub chpass(ByVal incoming As String, ByVal socket As Integer)
        Try
            Dim sItems() As String = incoming.Split(" ")

            Dim oPlayerAccount As New PlayerAccount
            Dim oStringValidation As New LogicLayer.Utility.StringValidation

            If (sItems(1).Length <= 12 And oStringValidation.IsAlphaNumeric(sItems(1)) = True) Or sItems(1) = "" Then
                If oPlayerAccount.UpdateUserInfo(frmSystray.Winsock(socket).Tag, sItems(1), Val(sItems(2)), Val(sItems(3)), sItems(4).Replace("%20", " ")) = True Then
                    Send(String.Empty, "CHPASS-OK", socket)
                Else
                    Throw New Exception
                End If
            Else
                Send(String.Empty, "CHPASS-WRONGPASS", socket)
            End If
        Catch ex As Exception
            Call errorsub(ex, "chpass")
            Send(String.Empty, "CHPASS-WRONGPASS", socket)
        End Try
    End Sub
End Class
