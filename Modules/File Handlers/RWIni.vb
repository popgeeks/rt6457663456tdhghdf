Option Strict Off
Option Explicit On
Module RWIni
	'***************************************************************
	'Windows API/Global Declarations for :16 and 32 bit functions to
	'     create
	'***************************************************************
	'****************************************************
	'* INI_sm.BAS *
	'****************************************************
	
#If Win16 Then
	'UPGRADE_NOTE: #If #EndIf block was not upgraded because the expression Win16 did not evaluate to True or was not evaluated. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="27EE2C3C-05AF-4C04-B2AF-657B4FB6B5FC"'
	Declare Function WritePrivateProfileString Lib "Kernel" (ByVal AppName As String, ByVal KeyName As String, ByVal NewString As String, ByVal filename As String) As Integer
	Declare Function GetPrivateProfileString Lib "Kernel" Alias "GetPrivateProfilestring" (ByVal AppName As String, ByVal KeyName As Any, ByVal default As String, ByVal ReturnedString As String, ByVal MAXSIZE As Integer, ByVal filename As String) As Integer
#Else
	' NOTE: The lpKeyName argument for GetProfileString, WriteProfile
	'     String,
	'GetPrivateProfileString, and WritePrivateProfileString can be ei
	'     ther
	'a string or NULL. This is why the argument is defined as "As Any
	'     ".
	' For example, to pass a string specifyByVal "wallpaper"
	' To pass NULL specifyByVal 0&
	'You can also pass NULL for the lpString argument for WriteProfil
	'     eString
	'and WritePrivateProfileString
	' Below it has been changed to a string due to the ability to use
	'     vbNullString
    'Declare Function GetPrivateProfileString Lib "kernel32"  Alias "GetPrivateProfileStringA"(ByVal lpApplicationName As String, ByVal lpKeyName As String, ByVal lpDefault As String, ByVal lpReturnedString As String, ByVal nSize As Integer, ByVal lpFileName As String) As Integer
    Private Declare Ansi Function GetPrivateProfileString _
      Lib "kernel32.dll" Alias "GetPrivateProfileStringA" _
      (ByVal lpApplicationName As String, _
      ByVal lpKeyName As String, ByVal lpDefault As String, _
      ByVal lpReturnedString As System.Text.StringBuilder, _
      ByVal nSize As Integer, ByVal lpFileName As String) _
      As Integer

    Declare Function WritePrivateProfileString Lib "kernel32" Alias "WritePrivateProfileStringA" (ByVal lpApplicationName As String, ByVal lpKeyName As String, ByVal lpString As String, ByVal lpFileName As String) As Integer
#End If
	
	'***************************************************************
	' Name: 16 and 32 bit functions to create
	' Description:16 AND 32 bit functions to read/write ini files--ve
	'     ry useful!
	' By: VB Qaid
	'
	'
	' Inputs:None
	'
	' Returns:None
	'
	'Assumes:None
	'
	'Side Effects:None
	'
	'Code provided by Planet Source Code(tm) (http://www.Planet-Sourc
	'     e-Code.com) 'as is', without warranties as to performance, fitnes
	'     s, merchantability,and any other warranty (whether expressed or i
	'     mplied).
	'This source code is copyrighted by Planet Source Code who has ex
	'     clusive rights to distribute it.
	'It is freely redistributable for personal use in source code for
	'     m, or for personal or business use in a non-source code binary ex
	'     ecutable.
	'All other redistributions are prohibited without express written
	'     consent from Exhedra Solutions, Inc.
	'***************************************************************
	'*******************************************************
	'* Procedure Name: sReadINI*
	'*=====================================================*
	'*Returns a string from an INI file. To use, call the *
	'*functions and pass it the Section, KeyName and INI*
	'*File Name, [sRet=sReadINI(Section,Key1,INIFile)].*
	'*val command. *
	'*******************************************************
	
	
    Function Readini(ByRef Section As String, ByRef KeyName As String, ByRef filename As String) As String
        'Dim sRet As String = New String(Chr(0), 255)
        'Readini = Left(sRet, GetPrivateProfileString(Section, KeyName, "", sRet, Len(sRet), filename))
        Dim intCharCount As Integer
        Dim objResult As New System.Text.StringBuilder(256)
        Readini = ""

        intCharCount = GetPrivateProfileString(Section, KeyName, _
           "", objResult, objResult.Capacity, filename)
        If intCharCount > 0 Then Readini = _
           Left(objResult.ToString, intCharCount)
    End Function
	
	'*******************************************************
	'* Procedure Name: WriteINI*
	'*=====================================================*
	'*Writes a string to an INI file. To use, call the *
	'*function and pass it the sSection, sKeyName, the New *
	'*String and the INI File Name,*
	'*[Ret=WriteINI(Section,Key,String,INIFile)]. *
	'*Returns a 1 if there were no errors and *
	'*a 0 if there were errors.*
	'*******************************************************
	
	
    Function writeini(ByRef sSection As String, ByRef sKeyName As String, ByRef sNewString As String, ByRef sFileName As String) As Integer
        writeini = WritePrivateProfileString(sSection, sKeyName, sNewString, sFileName)
    End Function
End Module