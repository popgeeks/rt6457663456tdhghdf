Module modEmail
	Public Sub EmailHandler(ByVal sSubject As String, ByVal sBody As String, ByVal sTo As String)
		Dim oEmail As New SendEmail

		oEmail.SendMail(sTo, "darklumina13@darkwindonline.com", sSubject, sBody)
	End Sub
End Module
