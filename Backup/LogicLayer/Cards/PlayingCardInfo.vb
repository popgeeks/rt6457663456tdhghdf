Namespace Cards
    Public Class PlayingCardInfo
        Public Function GetPlayingCard(ByVal sCard As String, ByVal CardList As Entities.BusinessObjects.PlayingCardList, ByVal CardnameList As List(Of String)) As Entities.BusinessObjects.PlayingCard
            Try
                Dim pcCard As New Entities.BusinessObjects.PlayingCard

                Dim iIndex As Integer = CardnameList.IndexOf(sCard)

                Return CardList(iIndex)
            Catch ex As Exception
                Return Nothing
            End Try
        End Function
    End Class
End Namespace
