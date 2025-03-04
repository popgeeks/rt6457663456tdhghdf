Imports System.Web.Mail

Public Class SendEmail
    'Public Function SendMail(ByVal sTo As String, ByVal sFrom As String, ByVal sSubject As String, ByVal sBody As String) As Boolean
    '	Try
    '		Dim msg As New System.Net.Mail.MailMessage
    '		Dim addressFrom As New System.Net.Mail.MailAddress(sFrom)
    '		Dim addressTo As New System.Net.Mail.MailAddress(sTo)

    '		msg.From = addressFrom
    '		msg.To.Add(addressTo)
    '		msg.Subject = sSubject
    '		msg.Body = sBody

    '		Dim smtp As System.Net.Mail.SmtpClient = New System.Net.Mail.SmtpClient
    '		smtp.Host = "smtp.comcast.net"
    '		smtp.Send(msg)

    '		Return True
    '	Catch ex As Exception
    '		Return False
    '	End Try
    'End Function

    'Public Function GetMail(ByVal sKeyword As String) As String
    '	Dim rRow As DataRow, zMsg As String
    '	rRow = GetDataRow("SELECT * FROM emailmsgs WHERE keyword = 'register'")

    '	zMsg = rRow.Item("message").ToString
    '	zMsg = Replace(zMsg, "\n", vbCrLf)

    '	Return zMsg
    'End Function
End Class
