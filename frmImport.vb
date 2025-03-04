Imports System
Imports System.IO

Public Class frmImport
    Private Sub EXPLevelsToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles EXPLevelsToolStripMenuItem.Click
        Dim x As Integer

        txtSQL.Text = ""

        For x = 1 To 100
            Call dbEXECQuery("INSERT INTO levels VALUES(" & x & ", " & Readini("Levels", CStr(x), My.Application.Info.DirectoryPath & "\levels.ini") & ")")
            txtSQL.Text = txtSQL.Text & "INSERT INTO levels VALUES(" & x & ", " & Readini("Levels", CStr(x), My.Application.Info.DirectoryPath & "\levels.ini") & ")" & vbCrLf
        Next
    End Sub

    Private Sub PlayerEXPToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PlayerEXPToolStripMenuItem.Click
        Dim dFolder As DirectoryInfo = New DirectoryInfo(My.Application.Info.DirectoryPath & "\accounts\")
        Dim fFileArray() As FileInfo = dFolder.GetFiles
        ' 'FILEARRAY' NOW HOLDS ALL THE FILES IN THE SELECTED FOLDER

		Dim fFile As FileInfo, sNick As String = "", sExp As String = ""

        ' LOOP THROUGH ARRAY, LISTING ALL FILES IN LISTVIEW
        For Each fFile In fFileArray
			Divide(fFile.Name, ".", sNick)

			sExp = Val(Readini("Stats", "Exp", My.Application.Info.DirectoryPath & "\accounts\" & fFile.Name)).ToString
			Call setaccountdata(sNick, "exp", sExp, 1)
        Next
    End Sub

    Private Sub PlayerLevelsToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PlayerLevelsToolStripMenuItem.Click
        Dim dFolder As DirectoryInfo = New DirectoryInfo(My.Application.Info.DirectoryPath & "\accounts\")
        Dim fFileArray() As FileInfo = dFolder.GetFiles
        ' 'FILEARRAY' NOW HOLDS ALL THE FILES IN THE SELECTED FOLDER

        Dim fFile As FileInfo, sNick As String = ""

        ' LOOP THROUGH ARRAY, LISTING ALL FILES IN LISTVIEW
        For Each fFile In fFileArray
            Divide(fFile.Name, ".", sNick)
            Call setaccountdata(sNick, "level", Val(Readini("Stats", "level", My.Application.Info.DirectoryPath & "\accounts\" & fFile.Name)), 1)
        Next
    End Sub

	Private Sub PlayerAAsToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PlayerAAsToolStripMenuItem.Click
		Dim dFolder As DirectoryInfo = New DirectoryInfo(My.Application.Info.DirectoryPath & "\accounts\")
		Dim fFileArray() As FileInfo = dFolder.GetFiles
		' 'FILEARRAY' NOW HOLDS ALL THE FILES IN THE SELECTED FOLDER

		Dim fFile As FileInfo, sNick As String = "", iValue As Integer

		' LOOP THROUGH ARRAY, LISTING ALL FILES IN LISTVIEW
		For Each fFile In fFileArray
			Divide(fFile.Name, ".", sNick)

			If Val(Readini("AP", "golden", My.Application.Info.DirectoryPath & "\accounts\" & fFile.Name)) > 0 Then
				iValue = Val(Readini("AP", "golden", My.Application.Info.DirectoryPath & "\accounts\" & fFile.Name))
				Call dbEXECQuery("INSERT INTO player_advancements(player, keyword, level) VALUES ('" & sNick & "', 'golden', " & iValue & ")")
			End If

			If Val(Readini("AP", "darky's", My.Application.Info.DirectoryPath & "\accounts\" & fFile.Name)) > 0 Then
				iValue = Val(Readini("AP", "darky's", My.Application.Info.DirectoryPath & "\accounts\" & fFile.Name))
				Call dbEXECQuery("INSERT INTO player_advancements(player, keyword, level) VALUES ('" & sNick & "', 'darkys', " & iValue & ")")
			End If

			If Val(Readini("AP", "high", My.Application.Info.DirectoryPath & "\accounts\" & fFile.Name)) > 0 Then
				iValue = Val(Readini("AP", "high", My.Application.Info.DirectoryPath & "\accounts\" & fFile.Name))
				Call dbEXECQuery("INSERT INTO player_advancements(player, keyword, level) VALUES ('" & sNick & "', 'high', " & iValue & ")")
			End If
			If Val(Readini("AP", "busybee", My.Application.Info.DirectoryPath & "\accounts\" & fFile.Name)) > 0 Then
				iValue = Val(Readini("AP", "busybee", My.Application.Info.DirectoryPath & "\accounts\" & fFile.Name))
				Call dbEXECQuery("INSERT INTO player_advancements(player, keyword, level) VALUES ('" & sNick & "', 'busy', " & iValue & ")")
			End If

			If Val(Readini("AP", "true", My.Application.Info.DirectoryPath & "\accounts\" & fFile.Name)) > 0 Then
				iValue = Val(Readini("AP", "true", My.Application.Info.DirectoryPath & "\accounts\" & fFile.Name))
				Call dbEXECQuery("INSERT INTO player_advancements(player, keyword, level) VALUES ('" & sNick & "', 'true', " & iValue & ")")
			End If

			If Val(Readini("AP", "bountiful", My.Application.Info.DirectoryPath & "\accounts\" & fFile.Name)) > 0 Then
				iValue = Val(Readini("AP", "bountiful", My.Application.Info.DirectoryPath & "\accounts\" & fFile.Name))
				Call dbEXECQuery("INSERT INTO player_advancements(player, keyword, level) VALUES ('" & sNick & "', 'bountiful', " & iValue & ")")
			End If

			If Val(Readini("AP", "petty", My.Application.Info.DirectoryPath & "\accounts\" & fFile.Name)) > 0 Then
				iValue = Val(Readini("AP", "petty", My.Application.Info.DirectoryPath & "\accounts\" & fFile.Name))
				Call dbEXECQuery("INSERT INTO player_advancements(player, keyword, level) VALUES ('" & sNick & "', 'petty', " & iValue & ")")
			End If

			If Val(Readini("AP", "full", My.Application.Info.DirectoryPath & "\accounts\" & fFile.Name)) > 0 Then
				iValue = Val(Readini("AP", "full", My.Application.Info.DirectoryPath & "\accounts\" & fFile.Name))
				Call dbEXECQuery("INSERT INTO player_advancements(player, keyword, level) VALUES ('" & sNick & "', 'full', " & iValue & ")")
			End If

			If Val(Readini("AP", "beggar's", My.Application.Info.DirectoryPath & "\accounts\" & fFile.Name)) > 0 Then
				iValue = Val(Readini("AP", "beggar's", My.Application.Info.DirectoryPath & "\accounts\" & fFile.Name))
				Call dbEXECQuery("INSERT INTO player_advancements(player, keyword, level) VALUES ('" & sNick & "', 'beggars', " & iValue & ")")
			End If

			If Val(Readini("AP", "alter", My.Application.Info.DirectoryPath & "\accounts\" & fFile.Name)) > 0 Then
				iValue = Val(Readini("AP", "alter", My.Application.Info.DirectoryPath & "\accounts\" & fFile.Name))
				Call dbEXECQuery("INSERT INTO player_advancements(player, keyword, level) VALUES ('" & sNick & "', 'alter', " & iValue & ")")
			End If
		Next
	End Sub

	Private Sub PlayerAAEXPAndPointsToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PlayerAAEXPAndPointsToolStripMenuItem.Click
		Dim dFolder As DirectoryInfo = New DirectoryInfo(My.Application.Info.DirectoryPath & "\accounts\")
		Dim fFileArray() As FileInfo = dFolder.GetFiles
		' 'FILEARRAY' NOW HOLDS ALL THE FILES IN THE SELECTED FOLDER

		Dim fFile As FileInfo, sNick As String = ""
		Dim sAAEXP As String, sAAPoints As String, sTrained As String, sPCT As String = ""

		' LOOP THROUGH ARRAY, LISTING ALL FILES IN LISTVIEW
		For Each fFile In fFileArray
			Divide(fFile.Name, ".", sNick)

			sAAEXP = Val(Readini("AP", "AAEXP", My.Application.Info.DirectoryPath & "\accounts\" & fFile.Name)).ToString
			sAAPoints = Val(Readini("AP", "AAPOINTS", My.Application.Info.DirectoryPath & "\accounts\" & fFile.Name)).ToString
			sTrained = Val(Readini("AP", "TRAINED", My.Application.Info.DirectoryPath & "\accounts\" & fFile.Name)).ToString
			sPCT = Val(Readini("AP", "PCT", My.Application.Info.DirectoryPath & "\accounts\" & fFile.Name)).ToString

			Call setaccountdata(sNick, "aaexp", sAAEXP, 1)
			Call setaccountdata(sNick, "aapoints", sAAPoints, 1)
			Call setaccountdata(sNick, "trained", sTrained, 1)
			Call setaccountdata(sNick, "aapct", sPCT, 0)
		Next
	End Sub
End Class