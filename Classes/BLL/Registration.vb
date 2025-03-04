Public Class Registration
    Inherits RegistrationDAL

    Public Sub Register(ByVal zNick As String, ByVal zEmail As String, ByVal zReferal As String, ByVal zCountry As String, ByVal sNewsletter As String, ByVal sIncentive As String, ByVal sCOPPA As String, ByVal zSocket As Integer)
        Dim oStringValidation As New StringValidation
        Dim oFunctions As New DatabaseFunctions
        Dim oPlayerAccount As New PlayerAccountDAL


        Try
            If zReferal = zNick Then zReferal = String.Empty

            Call CheckNick(zNick, zEmail)

            If Me.ErrorFlag = True Then Throw New Exception

            If ValidPlayer = False Then
                If ValidEmail = False Then
                    Send(String.Empty, "NEWUSER-EMAILEXIST", zSocket)
                Else
                    Send(String.Empty, "NEWUSER-NICKEXIST", zSocket)
                End If

                'If zSocket <> 0 Then frmSystray.Winsock(zSocket).Close()
                Exit Sub
            End If

            If oStringValidation.IsAlphaNumeric(zNick) = False Then
                Send(String.Empty, "NEWUSERFAILED", zSocket)
                'If zSocket <> 0 Then frmSystray.Winsock(zSocket).Close()
                Exit Sub
            End If

            Randomize()
            Dim zPass As Integer = Int(Rnd() * 999999).ToString

            Dim oEmail As SendEmail = New SendEmail
            Dim zMsg As String = oEmail.GetMail("register")

            zMsg = zMsg & "Username: " & zNick & vbCrLf
            zMsg = zMsg & "Password: " & zPass & vbCrLf
            zMsg = zMsg & "Registration Time: " & DateString & " - " & TimeString & vbCrLf

            oFunctions.SendEmail(ConnectionString, zEmail, "Triple Triad Extreme Account Information", zMsg)

            If oPlayerAccount.InsertRecord(zNick, zEmail, zPass, zReferal, zCountry.Replace("%20", " "), Val(sNewsletter), Val(sIncentive), Val(sCOPPA)) = False Then
                Send(String.Empty, "NEWUSERFAILED", zSocket)
                Exit Sub
            End If

            Send(String.Empty, "NEWUSEROK", zSocket)

            Call historyadd(zNick, 2, String.Concat(zNick, " (", frmSystray.Winsock(zSocket).RemoteHostIP, ") Registered"))

            'Send info to all admins
            Dim oAdminFunctions As New AdminFunctions
            Dim oDataTable As DataTable = oAdminFunctions.OnlineAdmins

            If Not (oDataTable Is Nothing) Then
                For x As Integer = 0 To oDataTable.Rows.Count - 1
                    Call blockchat(String.Empty, "Register", String.Concat(zNick, " (", frmSystray.Winsock(zSocket).RemoteHostIP, ") registered at ", Date.Now), oDataTable.Rows(x).Item("id"))
                Next x
            End If
        Catch ex As Exception
            Send(String.Empty, "NEWUSERFAILED", zSocket)
            Call errorsub(ex.ToString, "Register")
        Finally
            oPlayerAccount = Nothing
            oFunctions = Nothing
            oStringValidation = Nothing
            If zSocket <> 0 Then frmSystray.Winsock(zSocket).Close()
        End Try
    End Sub
End Class
