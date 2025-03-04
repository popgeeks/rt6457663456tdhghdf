Option Strict Off
Option Explicit On
Module Systray
	'user defined type required by Shell_NotifyIcon API call
	Public Structure NOTIFYICONDATA
		Dim cbSize As Integer
		Dim hwnd As Integer
		Dim uId As Integer
		Dim uFlags As Integer
		Dim uCallBackMessage As Integer
		Dim hIcon As Integer
        <VBFixedString(64), System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst:=64)> Public szTip() As Char
	End Structure
	
	
	'constants required by Shell_NotifyIcon API call:
	Public Const NIM_ADD As Short = &H0s
	Public Const NIM_MODIFY As Short = &H1s
	Public Const NIM_DELETE As Short = &H2s
	Public Const NIF_MESSAGE As Short = &H1s
	Public Const NIF_ICON As Short = &H2s
	Public Const NIF_TIP As Short = &H4s
	Public Const WM_MOUSEMOVE As Short = &H200s
	Public Const WM_LBUTTONDOWN As Short = &H201s 'Button down
	Public Const WM_LBUTTONUP As Short = &H202s 'Button up
	Public Const WM_LBUTTONDBLCLK As Short = &H203s 'Double-click
	Public Const WM_RBUTTONDOWN As Short = &H204s 'Button down
	Public Const WM_RBUTTONUP As Short = &H205s 'Button up
	Public Const WM_RBUTTONDBLCLK As Short = &H206s 'Double-click
	
	Public Declare Function SetForegroundWindow Lib "user32" (ByVal hwnd As Integer) As Integer

	Public Declare Function Shell_NotifyIcon Lib "shell32"  Alias "Shell_NotifyIconA"(ByVal dwMessage As Integer, ByRef pnid As NOTIFYICONDATA) As Boolean
	
	Public nid As NOTIFYICONDATA
End Module