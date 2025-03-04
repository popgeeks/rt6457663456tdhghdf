Option Strict Off
Option Explicit On
Module modSoldCards
	
	Public Function totalsoldcards(ByRef sNick As String) As String
        totalsoldcards = getDBField("call usp_SellCardCount('" & sNick & "')", "CardsSold")
	End Function
	
	Public Function lastsoldcards(ByRef sNick As String) As String
        lastsoldcards = getDBField("call usp_SellCardCount('" & sNick & "')", "LastSold")

        If lastsoldcards = "" Then lastsoldcards = "01-01-1900"
    End Function

    Public Function soldcardstoday(ByRef sNick As String) As String
        soldcardstoday = getDBField("call usp_SellCardCount('" & sNick & "')", "CardsSoldToday")
    End Function
End Module