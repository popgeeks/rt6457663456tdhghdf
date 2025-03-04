Option Strict Off
Option Explicit On
Module file_exists
	
	Public Const INVALID_HANDLE_VALUE As Short = -1
	Public Const MAX_PATH As Short = 260
	
	Structure FILETIME
		Dim dwLowDateTime As Integer
		Dim dwHighDateTime As Integer
	End Structure
	
	Structure WIN32_FIND_DATA
		Dim dwFileAttributes As Integer
		Dim ftCreationTime As FILETIME
		Dim ftLastAccessTime As FILETIME
		Dim ftLastWriteTime As FILETIME
		Dim nFileSizeHigh As Integer
		Dim nFileSizeLow As Integer
		Dim dwReserved0 As Integer
		Dim dwReserved1 As Integer

		<VBFixedString(MAX_PATH),System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.ByValArray,SizeConst:=MAX_PATH)> Public cFileName() As Char

		<VBFixedString(14),System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.ByValArray,SizeConst:=14)> Public cAlternate() As Char
	End Structure
	

	Declare Function FindFirstFile Lib "kernel32"  Alias "FindFirstFileA"(ByVal lpFileName As String, ByRef lpFindFileData As WIN32_FIND_DATA) As Integer
	
	Declare Function FindClose Lib "kernel32" (ByVal hFindFile As Integer) As Integer
	
	
	Public Function FileExists(ByRef sSource As String) As Boolean
		
		Dim WFD As WIN32_FIND_DATA
		Dim hFile As Integer
		
		hFile = FindFirstFile(sSource, WFD)
		FileExists = hFile <> INVALID_HANDLE_VALUE
		
		Call FindClose(hFile)
		
	End Function
End Module