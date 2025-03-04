'Option Strict Off
'Option Explicit On
'Module FileSeeker
'	' Programmed By Vangel (JF)
'	' God Bless that guy!
'	'
'	'  example:
'	'
'	'  strFileLocation = FindFile("mypic.gif")
'	'

'	Function FindFile(ByRef pstrLookingFor As String, Optional ByRef pstrPath As String = "") As String
'		On Error GoTo errhandler

'		Dim strFileName As String
'		Dim strNewPath As String
'		Dim strTemp As String
'		Dim strFolders() As String
'		Dim intCounter As Short

'		If pstrPath = "" Then 'optional parameter not passed
'			pstrPath = My.Application.Info.DirectoryPath
'		End If

'		If Not (Right(pstrPath, 1) = "\") Then
'			pstrPath = pstrPath & "\"
'		End If


'		strFileName = Dir(pstrPath & pstrLookingFor, FileAttribute.Normal)
'		If (strFileName <> "") Then
'			'file exists.  return full path
'			FindFile = pstrPath & strFileName
'		Else
'			'file does not exist...search subdirectories
'			ReDim strFolders(0)
'			'load all subfolers into an array

'            strNewPath = Dir(pstrPath & "*.*", FileAttribute.Directory)
'            Do Until strNewPath = ""
'                If (strNewPath <> ".") And (strNewPath <> "..") Then
'                    ReDim Preserve strFolders(UBound(strFolders) + 1)
'                    strFolders(UBound(strFolders)) = pstrPath & strNewPath
'                End If

'                strNewPath = Dir() 'get the next listing
'            Loop
'			'scan each subfolder for the files
'			strFileName = ""
'			For intCounter = 1 To UBound(strFolders)
'				strFileName = FindFile(pstrLookingFor, strFolders(intCounter))
'				If strFileName <> "" Then
'					'match found!  exit loop
'					Exit For
'				End If
'			Next intCounter
'			FindFile = strFileName

'		End If
'		Exit Function

'errhandler: 
'        Call errorsub(Err.Description, "FindFile")
'	End Function
'End Module