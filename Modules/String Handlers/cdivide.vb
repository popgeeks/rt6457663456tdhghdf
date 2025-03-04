Option Strict Off
Option Explicit On
Module cdivided
	
	Public Sub SplitDivide(ByRef pstrStingToDivide As String, ByRef pstrSeperator As String, ByRef pstr1 As String, ByRef pstr2 As String, ByRef pstr3 As String, ByRef pstr4 As String, ByRef pstr5 As String, ByRef pstr6 As String, ByRef pstr7 As String, ByRef pstr8 As String, ByRef pstr9 As String, ByRef pstr10 As String, ByRef pstr11 As String, ByRef pstr12 As String, ByRef pstr13 As String, ByRef pstr14 As String, ByRef pstr15 As String, ByRef pstr16 As String, ByRef pstr17 As String, ByRef pstr18 As String, ByRef pstr19 As String, ByRef pstr20 As String, ByRef pstr21 As String, ByRef pstr22 As String, ByRef pstr23 As String, ByRef pstr24 As String, ByRef pstr25 As String, ByRef pstr26 As String, ByRef pstr27 As String, ByRef pstr28 As String, ByRef pstr29 As String, ByRef pstr30 As String, ByRef pstr31 As String, ByRef pstr32 As String, ByRef pstr33 As String, ByRef pstr34 As String, ByRef pstr35 As String, ByRef pstr36 As String, ByRef pstr37 As String, ByRef pstr38 As String, ByRef pstr39 As String, ByRef pstr40 As String, ByRef pstr41 As String, ByRef pstr42 As String, ByRef pstr43 As String)
		
        Dim varData As String()
        Dim intIndex As Integer
        Dim intCount As Integer

		varData = Split(pstrStingToDivide, pstrSeperator)
		
		intIndex = 0
		For intCount = LBound(varData) To UBound(varData)
			intIndex = intIndex + 1
			Select Case intIndex
				Case 1
                    pstr1 = varData(intCount)
				Case 2
                    pstr2 = varData(intCount)
				Case 3
                    pstr3 = varData(intCount)
				Case 4
                    pstr4 = varData(intCount)
				Case 5
                    pstr5 = varData(intCount)
				Case 6
                    pstr6 = varData(intCount)
				Case 7
                    pstr7 = varData(intCount)
				Case 8
                    pstr8 = varData(intCount)
                Case 9
                    pstr9 = varData(intCount)
				Case 10
                    pstr10 = varData(intCount)
                Case 11
                    pstr11 = varData(intCount)
				Case 12
                    pstr12 = varData(intCount)
                Case 13
                    pstr13 = varData(intCount)
				Case 14
                    pstr14 = varData(intCount)
                Case 15
                    pstr15 = varData(intCount)
				Case 16
                    pstr16 = varData(intCount)
                Case 17
                    pstr17 = varData(intCount)
				Case 18
                    pstr18 = varData(intCount)
                Case 19
                    pstr19 = varData(intCount)
				Case 20
                    pstr20 = varData(intCount)
                Case 21
                    pstr21 = varData(intCount)
				Case 22
                    pstr22 = varData(intCount)
                Case 23
                    pstr23 = varData(intCount)
				Case 24
                    pstr24 = varData(intCount)
                Case 25
                    pstr25 = varData(intCount)
				Case 26
                    pstr26 = varData(intCount)
                Case 27
                    pstr27 = varData(intCount)
				Case 28
                    pstr28 = varData(intCount)
                Case 29
                    pstr29 = varData(intCount)
				Case 30
                    pstr30 = varData(intCount)
                Case 31
                    pstr31 = varData(intCount)
				Case 32
                    pstr32 = varData(intCount)
                Case 33
                    pstr33 = varData(intCount)
				Case 34
                    pstr34 = varData(intCount)
                Case 35
                    pstr35 = varData(intCount)
				Case 36
                    pstr36 = varData(intCount)
                Case 37
                    pstr37 = varData(intCount)
				Case 38
                    pstr38 = varData(intCount)
                Case 39
                    pstr39 = varData(intCount)
				Case 40
                    pstr40 = varData(intCount)
                Case 41
                    pstr41 = varData(intCount)
				Case 42
                    pstr42 = varData(intCount)
                Case 43
                    pstr43 = varData(intCount)
            End Select
		Next intCount
		
	End Sub ' =)
End Module