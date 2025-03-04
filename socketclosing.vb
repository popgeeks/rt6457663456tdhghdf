Option Strict Off
Option Explicit On
Module socketclosing
	
    Public Sub closesocket(ByRef socket As Integer)

        Call quit("QUIT " & frmSystray.Winsock(socket).Tag, socket)

        Exit Sub
errhandler:
        Call errorsub(Err.Description, "quit")

    End Sub
End Module