Option Strict Off
Option Explicit On

Module shrmodStrings

    Public Function StringReplaceAll(ByRef pstrString As String, ByVal ParamArray ThingsToReplace As String()) As String
        'Replaces every occurance of pstrReplaceMe in pstrString with pstrWithMe
        'pstrWithMe CAN contain pstrReplaceMe.  For example, the following line is ok:
        'StringReplaceAll strText, "aa", "aaaa"
        '
        '10/1/98 - modified paramters to allow multiple strings to find and replace with one call
        '          new syntaxes: StringReplaceAll String, ReplaceMe, WithMe [, ReplaceMe2, WithMe2 [, ReplaceMe3, WithMe3 ... ] ]
        '                        NewString = StringReplaceAll (String, ReplaceMe, WithMe [, ReplaceMe2, WithMe2 [, ReplaceMe3, WithMe3 ... ] ] )
        '
        '11/11/98 - This function is no longer case sensative when searching for replacements
        '           (never was because this module was previously Option Compare Text, but
        '           I changed it to Option Compary Binary so that I could determine cases)
        '11/11/98 - This function is now case sensative when making replacements
        '           (this is not possible with Option Compare Text)
        '

        Dim intLocation As Integer
        Dim intLenWithMe As Integer
        Dim intLenReplaceMe As Integer
        Dim pstrReplaceMe, pstrWithMe As String
        Dim intParameterCount As Integer

        For intParameterCount = LBound(ThingsToReplace) To (UBound(ThingsToReplace) - 1) Step 2

            pstrReplaceMe = ThingsToReplace(intParameterCount)
            pstrWithMe = ThingsToReplace(intParameterCount + 1)

            intLenWithMe = Len(pstrWithMe)
            intLenReplaceMe = Len(pstrReplaceMe)

            'Find the first occurance
            intLocation = InStr(UCase(pstrString), UCase(pstrReplaceMe))
            Do Until intLocation = 0
                If IsAllCaps(Mid(pstrString, intLocation, intLenReplaceMe)) Then
                    pstrString = Left(pstrString, intLocation - 1) & UCase(pstrWithMe) & Mid(pstrString, intLocation + intLenReplaceMe)
                ElseIf IsAllLowerCase(Mid(pstrString, intLocation, intLenReplaceMe)) Then
                    pstrString = Left(pstrString, intLocation - 1) & LCase(pstrWithMe) & Mid(pstrString, intLocation + intLenReplaceMe)
                ElseIf IsInitialCapped(Mid(pstrString, intLocation, intLenReplaceMe)) Then
                    pstrString = Left(pstrString, intLocation - 1) & ICase(pstrWithMe) & Mid(pstrString, intLocation + intLenReplaceMe)
                Else
                    pstrString = Left(pstrString, intLocation - 1) & pstrWithMe & Mid(pstrString, intLocation + intLenReplaceMe)
                End If
                'Find the next occurance, starting with the character following the
                'text that was inserted as a replacement.
                intLocation = InStr(intLocation + intLenWithMe, pstrString, pstrReplaceMe)
            Loop

        Next intParameterCount

        StringReplaceAll = pstrString

    End Function
End Module