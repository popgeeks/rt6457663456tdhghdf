Module divider
    Public Sub Divide(ByRef sStingToDivide As String, ByRef sSeperator As String, Optional ByRef s1 As String = "", Optional ByRef s2 As String = "", Optional ByRef s3 As String = "", Optional ByRef s4 As String = "", Optional ByRef s5 As String = "", Optional ByRef s6 As String = "", Optional ByRef s7 As String = "", Optional ByRef s8 As String = "", Optional ByRef s9 As String = "", Optional ByRef s10 As String = "", Optional ByRef s11 As String = "", Optional ByRef s12 As String = "", Optional ByRef s13 As String = "", Optional ByRef s14 As String = "", Optional ByRef s15 As String = "", Optional ByRef s16 As String = "", Optional ByRef s17 As String = "", Optional ByRef s18 As String = "", Optional ByRef s19 As String = "", Optional ByRef s20 As String = "", Optional ByRef s21 As String = "", Optional ByRef s22 As String = "", Optional ByRef s23 As String = "", Optional ByRef s24 As String = "", Optional ByRef s25 As String = "", Optional ByRef s26 As String = "", Optional ByRef s27 As String = "", Optional ByRef s28 As String = "", Optional ByRef s29 As String = "", Optional ByRef s30 As String = "", Optional ByRef s31 As String = "", Optional ByRef s32 As String = "", Optional ByRef s33 As String = "", Optional ByRef s34 As String = "", Optional ByRef s35 As String = "", Optional ByRef s36 As String = "", Optional ByRef s37 As String = "", Optional ByRef s38 As String = "", Optional ByRef s39 As String = "", Optional ByRef s40 As String = "", Optional ByRef s41 As String = "", Optional ByRef s42 As String = "")
        On Error GoTo errhandler

        Dim varData As String()
        Dim intIndex As Integer
        Dim intCount As Integer

        varData = Split(sStingToDivide, sSeperator)

        intIndex = 0
        For intCount = LBound(varData) To UBound(varData)
            intIndex = intIndex + 1

            Select Case intIndex
                Case 1
                    s1 = varData(intCount)
                Case 2
                    s2 = varData(intCount)
                Case 3
                    s3 = varData(intCount)
                Case 4
                    s4 = varData(intCount)
                Case 5
                    s5 = varData(intCount)
                Case 6
                    s6 = varData(intCount)
                Case 7
                    s7 = varData(intCount)
                Case 8
                    s8 = varData(intCount)
                Case 9
                    s9 = varData(intCount)
                Case 10
                    s10 = varData(intCount)
                Case 11
                    s11 = varData(intCount)
                Case 12
                    s12 = varData(intCount)
                Case 13
                    s13 = varData(intCount)
                Case 14
                    s14 = varData(intCount)
                Case 15
                    s15 = varData(intCount)
                Case 16
                    s16 = varData(intCount)
                Case 17
                    s17 = varData(intCount)
                Case 18
                    s18 = varData(intCount)
                Case 19
                    s19 = varData(intCount)
                Case 20
                    s20 = varData(intCount)
                Case 21
                    s21 = varData(intCount)
                Case 22
                    s22 = varData(intCount)
                Case 23
                    s23 = varData(intCount)
                Case 24
                    s24 = varData(intCount)
                Case 25
                    s25 = varData(intCount)
                Case 26
                    s26 = varData(intCount)
                Case 27
                    s27 = varData(intCount)
                Case 28
                    s28 = varData(intCount)
                Case 29
                    s29 = varData(intCount)
                Case 30
                    s30 = varData(intCount)
                Case 31
                    s31 = varData(intCount)
                Case 32
                    s32 = varData(intCount)
                Case 33
                    s33 = varData(intCount)
                Case 34
                    s34 = varData(intCount)
                Case 35
                    s35 = varData(intCount)
                Case 36
                    s36 = varData(intCount)
                Case 37
                    s37 = varData(intCount)
                Case 38
                    s38 = varData(intCount)
                Case 39
                    s39 = varData(intCount)
                Case 40
                    s40 = varData(intCount)
                Case 41
                    s41 = varData(intCount)
                Case 42
                    s42 = varData(intCount)
            End Select
        Next intCount
        Exit Sub

errhandler:
        Call errorsub(Err.Description, "Divide")
    End Sub ' =)

    Public Sub CDivide(ByRef sStingToDivide As String, ByRef sSeperator As String, ByRef s1 As String, ByRef s2 As String, ByRef s3 As String, ByRef s4 As String, ByRef s5 As String, ByRef s6 As String, ByRef s7 As String, ByRef s8 As String, ByRef s9 As String, ByRef s10 As String, ByRef s11 As String, ByRef s12 As String, ByRef s13 As String, ByRef s14 As String, ByRef s15 As String, ByRef s16 As String, ByRef s17 As String, ByRef s18 As String, ByRef s19 As String, ByRef s20 As String, ByRef s21 As String, ByRef s22 As String, ByRef s23 As String, ByRef s24 As String, ByRef s25 As String, ByRef s26 As String, ByRef s27 As String, ByRef s28 As String, ByRef s29 As String, ByRef s30 As String, ByRef s31 As String, ByRef s32 As String, ByRef s33 As String, ByRef s34 As String, ByRef s35 As String, ByRef s36 As String)
        On Error GoTo errhandler

        Dim varData As String()
        Dim intIndex As Integer
        Dim intCount As Integer

        varData = Split(sStingToDivide, sSeperator)

        intIndex = 0
        For intCount = LBound(varData) To UBound(varData)
            intIndex = intIndex + 1
            Select Case intIndex
                Case 1

                    s1 = varData(intCount)
                Case 2

                    s2 = varData(intCount)
                Case 3

                    s3 = varData(intCount)
                Case 4

                    s4 = varData(intCount)
                Case 5

                    s5 = varData(intCount)
                Case 6

                    s6 = varData(intCount)
                Case 7

                    s7 = varData(intCount)
                Case 8

                    s8 = varData(intCount)
                Case 9

                    s9 = varData(intCount)
                Case 10

                    s10 = varData(intCount)
                Case 11

                    s11 = varData(intCount)
                Case 12

                    s12 = varData(intCount)
                Case 13

                    s13 = varData(intCount)
                Case 14

                    s14 = varData(intCount)
                Case 15

                    s15 = varData(intCount)
                Case 16

                    s16 = varData(intCount)
                Case 17

                    s17 = varData(intCount)
                Case 18

                    s18 = varData(intCount)
                Case 19

                    s19 = varData(intCount)
                Case 20

                    s20 = varData(intCount)
                Case 21

                    s21 = varData(intCount)
                Case 22

                    s22 = varData(intCount)
                Case 23

                    s23 = varData(intCount)
                Case 24

                    s24 = varData(intCount)
                Case 25

                    s25 = varData(intCount)
                Case 26

                    s26 = varData(intCount)
                Case 27

                    s27 = varData(intCount)
                Case 28

                    s28 = varData(intCount)
                Case 29

                    s29 = varData(intCount)
                Case 30

                    s30 = varData(intCount)
                Case 31

                    s31 = varData(intCount)
                Case 32

                    s32 = varData(intCount)
                Case 33

                    s33 = varData(intCount)
                Case 34

                    s34 = varData(intCount)
                Case 35

                    s35 = varData(intCount)
                Case 36

                    s36 = varData(intCount)
            End Select
        Next intCount
        Exit Sub

errhandler:
        '    Call errorsub(Error, "Divide")
    End Sub ' =)
End Module