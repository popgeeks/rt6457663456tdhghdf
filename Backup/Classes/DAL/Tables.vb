Imports System.Collections.Generic
Imports mySQL.Data.MySqlClient
Imports MySQLFactory

Namespace BusinessLayer
    Public Class Tables
        Public Sub ClosePlayerTable(ByRef Tables() As Entities.BusinessObjects.Tables, ByVal sPlayer As String, ByVal bSendKill As Boolean)
            Dim blnFound As Boolean

            For q As Integer = 0 To oServerConfig.MaxTables - 1
                If (Tables(q).Player1) = (sPlayer) Then
                    Tables(q).Code_X = String.Empty
                    Tables(q).Player1 = String.Empty
                    Tables(q).Player2 = String.Empty
                    Tables(q).Player3 = String.Empty
                    Tables(q).Player4 = String.Empty
                    Tables(q).Player_Ready(1) = False
                    Tables(q).Player_Ready(2) = False
                    Tables(q).Player_Ready(3) = False
                    Tables(q).Player_Ready(4) = False

                    Tables(q).RuleList = String.Empty
                    Tables(q).Comment = String.Empty
                    Tables(q).Wager = 0
                    Tables(q).Stalemate1 = False
                    Tables(q).Stalemate2 = False
                    Tables(q).Decks = String.Empty
                    Tables(q).Blocks = String.Empty
                    Tables(q).GoldWager = 0

                    blnFound = True
                End If

                If bSendKill = True And blnFound = True Then
                    Dim iTable As Integer = FindPlayersTable(Tables, sPlayer)
                    If iTable <> 0 Then SendKillMatch(Tables(iTable))
                End If

                If blnFound = True Then
                    Tables(q).GameID = 0
                    Call SendTable(Tables, q)
                    'Call sendall("ADDTABLE " & q & " " & Tables(q).Player1 & " " & Tables(q).Player2 & " " & " " & Tables(q).Player3 & " " & Tables(q).Player4 & " " & Tables(q).RuleList & "@@" & Tables(q).Comment)
                    blnFound = False
                    Exit For
                End If
            Next q
        End Sub

        Public Sub ClosePlayerTable(ByRef Tables() As Entities.BusinessObjects.Tables, ByVal iTableID As Integer)
            If Tables(iTableID).player1 = String.Empty And Tables(iTableID).player2 = String.Empty And Tables(iTableID).gameid = 0 Then Exit Sub

            Tables(iTableID).Clear()
            SendRemoveTable(iTableID)
            UpdateTable(Tables, iTableID)
        End Sub

        Protected Sub SendRemoveTable(ByVal iTableID As Integer)
            Call sendall(String.Format("REMOVETABLE {0}", iTableID))
        End Sub

        Public Sub SendKillMatch(ByRef Table As Entities.BusinessObjects.Tables)
            If Table.Player1 <> String.Empty Then Send(Table.Player1, "KILLMATCH")
            If Table.Player2 <> String.Empty Then Send(Table.Player2, "KILLMATCH")
            If Table.Player3 <> String.Empty Then Send(Table.Player3, "KILLMATCH")
            If Table.Player4 <> String.Empty Then Send(Table.Player4, "KILLMATCH")
        End Sub

        Public Function FindTablePlayer(ByRef Tables() As Entities.BusinessObjects.Tables, ByVal sPlayer As String) As Boolean
            For Each objTable As Entities.BusinessObjects.Tables In Tables
                If objTable.PlayerInTable(sPlayer) = True Then
                    Return True
                End If
            Next
        End Function

        Public Function FindPlayersTable(ByRef Tables() As Entities.BusinessObjects.Tables, ByVal sPlayer As String) As Integer
            For Each objTable As Entities.BusinessObjects.Tables In Tables
                If objTable.PlayerInTable(sPlayer) = True Then
                    Return Array.IndexOf(Tables, objTable)
                End If
            Next

            Return 0
        End Function

        Public Function FindEmptyTable(ByRef Tables() As Entities.BusinessObjects.Tables) As Integer
            For Each objTable As Entities.BusinessObjects.Tables In Tables
                If objTable.IsEmpty And Array.IndexOf(Tables, objTable) <> 0 Then
                    Return Array.IndexOf(Tables, objTable)
                End If
            Next

            Return oServerConfig.MaxTables
        End Function

        Public Sub PreGameTableClose(ByRef Tables() As Entities.BusinessObjects.Tables, ByVal socket As Integer)
            Dim tableID As String
            Dim x, intPlayer As Integer
            Dim strPlayers(2) As String

            'Dim oTables As New BusinessLayer.Tables

            tableID = FindPlayersTable(Tables, frmSystray.Winsock(socket).Tag).ToString

            If tableID = "0" Then Exit Sub

            strPlayers(1) = frmSystray.Winsock(socket).Tag
            strPlayers(2) = Tables(CInt(tableID)).Player2

            If Tables(CInt(tableID)).Player1 = frmSystray.Winsock(socket).Tag Then
                Tables(CInt(tableID)).Player_Ready(1) = False

                ClosePlayerTable(Tables, frmSystray.Winsock(socket).Tag, False)

                Tables(CInt(tableID)).Player1 = String.Empty

                intPlayer = 1
            ElseIf Tables(CInt(tableID)).Player2 = frmSystray.Winsock(socket).Tag Then
                Tables(CInt(tableID)).Player_Ready(2) = False
                Tables(CInt(tableID)).Player2 = String.Empty

                intPlayer = 2
            End If

            ' Update the challenge boxes
            If strPlayers(1) <> "" And intPlayer <> 1 Then
                Send(Tables(CInt(tableID)).Player1, "CHALLENGEREMOVES " & intPlayer)
            End If

            If strPlayers(2) <> String.Empty And intPlayer <> 2 Then
                Send(Tables(CInt(tableID)).Player2, "CHALLENGEREMOVES " & intPlayer)
            End If

            If intPlayer = 1 Then
                For x = 1 To 2
                    Send(strPlayers(x), "CHALLENGEREMOVES 5")
                Next x
            Else
                Send(String.Empty, "CHALLENGEREMOVES 5", socket)
            End If

            SendTable(Tables, CInt(tableID))
            'Call sendall("ADDTABLE " & tableID & " " & Tables(CInt(tableID)).Player1 & " " & Tables(CInt(tableID)).Player2 & " " & Tables(CInt(tableID)).RuleList & " @@" & Tables(CInt(tableID)).Comment & "@@" & Tables(CInt(tableID)).Decks)
        End Sub

        Public Sub SendtoPlayer(ByRef Tables() As Entities.BusinessObjects.Tables, ByVal iTableID As Integer, ByVal socket As Integer)
            Send(String.Empty, String.Format("TABLEINFO {0} {1} {2} {3} @@{4}@@{5}@@{6}", iTableID, Tables(iTableID).Player1, Tables(iTableID).Player2, Tables(iTableID).RuleList, Tables(iTableID).Comment, Tables(iTableID).Decks, Tables(iTableID).GoldWager), socket)
        End Sub

        Public Sub SendTable(ByRef Tables() As Entities.BusinessObjects.Tables, ByVal iTableID As Integer)
            Call sendall(String.Format("ADDTABLE {0} {1} {2} {3} @@{4}@@{5}@@{6}", iTableID, Tables(iTableID).Player1, Tables(iTableID).Player2, Tables(iTableID).RuleList, Tables(iTableID).Comment, Tables(iTableID).Decks, Tables(iTableID).GoldWager))
            UpdateTable(Tables, iTableID)
        End Sub

        Public Sub RefreshDatabaseTables(ByRef Tables() As Entities.BusinessObjects.Tables)
            For x As Integer = 0 To oServerConfig.MaxTables - 1
                UpdateTable(Tables, x)
            Next
        End Sub

        Public Function ClearAllTables() As Boolean
            Try
                Dim oMySqlHelper As New MySQLFactory.MySQLDBHelper
                oMySqlHelper.DBExecuteScalar(ConnectionString, CommandType.StoredProcedure, "spd_AllTables")
                Return True
            Catch ex As Exception
                Call errorsub(ex.ToString, "ClearAllTables")
                Return False
            Finally
                oServerConfig.MySQLCalls += 1
            End Try
        End Function

        Public Function InsertTable(ByVal iTableID As Integer) As Boolean
            Try
                Dim oMySqlHelper As New MySQLFactory.MySQLDBHelper
                Dim arParms() As MySqlParameter = New MySqlParameter(0) {}

                arParms(0) = New MySqlParameter("?iTableID", iTableID)

                oMySqlHelper.DBExecuteScalar(ConnectionString, CommandType.StoredProcedure, "spi_BlankTable", arParms)
                Return True
            Catch ex As Exception
                Call errorsub(ex.ToString, "InsertTable")
                Return False
            Finally
                oServerConfig.MySQLCalls += 1
            End Try
        End Function

        Public Function UpdateTable(ByRef Tables() As Entities.BusinessObjects.Tables, ByVal iTableID As Integer) As Boolean
            Dim oMySqlHelper As New MySQLFactory.MySQLDBHelper

            Try
                Dim arParms() As MySqlParameter = New MySqlParameter(19) {}

                arParms(0) = New MySqlParameter("?iTableID", iTableID)
                arParms(1) = New MySqlParameter("?iGameID", Tables(iTableID).GameID)
                arParms(2) = New MySqlParameter("?sPlayer1", Tables(iTableID).Player1)
                arParms(3) = New MySqlParameter("?sPlayer2", Tables(iTableID).Player2)
                arParms(4) = New MySqlParameter("?sPlayer3", Tables(iTableID).Player3)
                arParms(5) = New MySqlParameter("?sPlayer4", Tables(iTableID).Player4)
                arParms(6) = New MySqlParameter("?iPR1", IIf(Tables(iTableID).Player_Ready(1) = True, 1, 0))
                arParms(7) = New MySqlParameter("?iPR2", IIf(Tables(iTableID).Player_Ready(2) = True, 1, 0))
                arParms(8) = New MySqlParameter("?iPR3", IIf(Tables(iTableID).Player_Ready(3) = True, 1, 0))
                arParms(9) = New MySqlParameter("?iPR4", IIf(Tables(iTableID).Player_Ready(4) = True, 1, 0))
                arParms(10) = New MySqlParameter("?sRuleList", Tables(iTableID).RuleList)
                arParms(11) = New MySqlParameter("?sCode_X", Tables(iTableID).Code_X)
                arParms(12) = New MySqlParameter("?sComment", Tables(iTableID).Comment)
                arParms(13) = New MySqlParameter("?iLock", Tables(iTableID).Lock)
                arParms(14) = New MySqlParameter("?iWager", Tables(iTableID).Wager)
                arParms(15) = New MySqlParameter("?iS1", IIf(Tables(iTableID).Stalemate1 = True, 1, 0))
                arParms(16) = New MySqlParameter("?iS2", IIf(Tables(iTableID).Stalemate2 = True, 1, 0))
                arParms(17) = New MySqlParameter("?sDecks", Tables(iTableID).Decks)
                arParms(18) = New MySqlParameter("?sBlocks", Tables(iTableID).Blocks)
                arParms(19) = New MySqlParameter("?iGoldWager", Tables(iTableID).GoldWager)

                oMySqlHelper.DBExecuteScalar(ConnectionString, CommandType.StoredProcedure, "spu_Table", arParms)
                Return True
            Catch ex As Exception
                Call errorsub(ex.ToString, "InsertRecord")
                Return False
            Finally
                oMySqlHelper = Nothing
                oServerConfig.MySQLCalls += 1
            End Try
        End Function

        Protected Overrides Sub Finalize()
            MyBase.Finalize()
        End Sub
    End Class
End Namespace