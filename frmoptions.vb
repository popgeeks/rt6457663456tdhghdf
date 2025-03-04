Option Strict Off
Option Explicit On
Friend Class frmoptions
	Inherits System.Windows.Forms.Form
	Public Sub mnuban_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles mnuban.Click
		If mnuban.Tag = "" Then Exit Sub
		
		Call ban("BAN " & mnuban.Tag & " Server%20Operator%20Ban", 0)
	End Sub
End Class