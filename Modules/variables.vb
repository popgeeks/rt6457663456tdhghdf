Imports System.Collections.Generic

Module variables
    Public pmnum As Integer
    Public oServerConfig As New ServerConfig()
    Public Tables(50) As Entities.BusinessObjects.Tables
    Public Cards As New Entities.BusinessObjects.PlayingCardList
    Public CardNameList As New List(Of String)
    Public AdminList As New Entities.BusinessObjects.AdminList
    Public PlayerList As New Entities.BusinessObjects.PlayerList

    Public Sub Server_Load()
        Tables_New()
        LoadCards(Cards, CardNameList)
        oServerConfig.MySQLCalls += 1
    End Sub

    Private Sub Tables_New()
        Dim oTables As New BusinessLayer.Tables

        oTables.ClearAllTables()

        For x As Integer = 0 To oServerConfig.MaxTables - 1
            Tables(x) = New Entities.BusinessObjects.Tables
            Tables(x).TableID = x
            oTables.InsertTable(x)
        Next x
    End Sub

    Public Sub LoadCards(ByRef oCards As Entities.BusinessObjects.PlayingCardList, ByRef oCardNameList As List(Of String))
        Dim oServerLoad As New DataLayer.ServerObjects.ServerLoad(ConnectionString)
        Dim oDataTable As DataTable = oServerLoad.LoadCards()

        For Each oRow As DataRow In oDataTable.Rows
            With oRow
                oCards.Add(.Item("left"), .Item("right"), .Item("down"), .Item("up"), .Item("element"), .Item("level"), .Item("cardname"), .Item("flggold"), .Item("deck"), .Item("id"))
                oCardNameList.Add(.Item("cardname").ToString.ToLower)
            End With
        Next
    End Sub
End Module