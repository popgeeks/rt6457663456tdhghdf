Option Strict Off
Option Explicit On
Module levelchecking
    Public Function leveldifference(ByRef plr1nick As String, ByRef plr2nick As String) As Short
        leveldifference = Val(getAccount_Field(plr1nick, "level")) - Val(getAccount_Field(plr2nick, "level"))
    End Function
    Public Function leveldifferencesub(ByRef plr1nick As String, ByRef plr2nick As String) As Boolean
        ' Player 1 is who challenged
        ' Player 2 is the challengee
        leveldifferencesub = False

        Dim plr1lvl As Integer, plr2lvl As Integer

        plr1lvl = Val(getAccount_Field(plr1nick, "level"))
		plr2lvl = Val(getAccount_Field(plr2nick, "level"))

        If plr1lvl <= 10 Then
			If plr1lvl - plr2lvl > 5 Then leveldifferencesub = True
		ElseIf plr1lvl <= 20 Then
			If plr1lvl - plr2lvl > 10 Then leveldifferencesub = True
		ElseIf plr1lvl <= 30 Then
			If plr1lvl - plr2lvl > 15 Then leveldifferencesub = True
		ElseIf plr1lvl <= 40 Then
			If plr1lvl - plr2lvl > 20 Then leveldifferencesub = True
		ElseIf plr1lvl <= 50 Then
			If plr1lvl - plr2lvl > 25 Then leveldifferencesub = True
		ElseIf plr1lvl <= 60 Then
			If plr1lvl - plr2lvl > 30 Then leveldifferencesub = True
		ElseIf plr1lvl <= 70 Then
			If plr1lvl - plr2lvl > 35 Then leveldifferencesub = True
		ElseIf plr1lvl <= 80 Then
			If plr1lvl - plr2lvl > 40 Then leveldifferencesub = True
		ElseIf plr1lvl <= 90 Then
			If plr1lvl - plr2lvl > 45 Then leveldifferencesub = True
		ElseIf plr1lvl <= 99 Then
			If plr1lvl - plr2lvl > 50 Then leveldifferencesub = True
		ElseIf plr1lvl >= 100 Then
			If plr1lvl - plr2lvl > 55 Then leveldifferencesub = True
        End If

    End Function
	
	Public Function levelcheck(ByRef plr1nick As String, ByRef plr2nick As String, ByRef tradeall As String, ByRef tradediff As String, ByRef tradetwo As String, ByRef tradethree As String, ByRef tradefour As String, ByRef capture As String, ByRef ghostchallenge As String) As Boolean
		
		If tradeall = "1" Then If findplrlvl(plr1nick) < 5 Or findplrlvl(plr2nick) < 5 Then levelcheck = True
		If tradediff = "1" Or tradetwo = "1" Then If findplrlvl(plr1nick) < 2 Or findplrlvl(plr2nick) < 2 Then levelcheck = True
		If tradethree = "1" Then If findplrlvl(plr1nick) < 3 Or findplrlvl(plr2nick) < 3 Then levelcheck = True
		If tradefour = "1" Then If findplrlvl(plr1nick) < 4 Or findplrlvl(plr2nick) < 4 Then levelcheck = True
		If capture = "1" Then If findplrlvl(plr1nick) < 2 Or findplrlvl(plr2nick) < 2 Then levelcheck = True
		If ghostchallenge <> "" And ghostchallenge <> "0" Then If findplrlvl(plr1nick) < 5 Or findplrlvl(plr2nick) < 5 Then levelcheck = True
		
	End Function
End Module