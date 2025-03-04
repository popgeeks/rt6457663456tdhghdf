Namespace BusinessObjects
    Public Class PlayingCardList
        Inherits List(Of PlayingCard)

        Public Overloads Sub Add(ByVal iLeft As Integer, ByVal iRight As Integer, ByVal iDown As Integer, ByVal iUp As Integer, ByVal sElement As String, ByVal iLevel As Integer, _
        ByVal sCardName As String, ByVal bSpecial As Boolean, ByVal sDeck As String, ByVal iCardID As Integer)
            Dim oPlayingCard As New PlayingCard()

            With oPlayingCard
                .CardID = iCardID
                .CardName = sCardName
                .Level = iLevel
                .Right = iRight
                .Deck = sDeck
                .Down = iDown
                .Left = iLeft
                .Up = iUp
                .Element = sElement
                .SpecialCard = bSpecial
            End With

            MyBase.Add(oPlayingCard)
        End Sub
    End Class

    Public Class PlayingCard
        Private iLeft As Integer
        Private iRight As Integer
        Private iDown As Integer
        Private iUp As Integer
        Private sElement As String
        Private iLevel As Integer
        Private sCardName As String
        Private bSpecial As Boolean
        Private sDeck As String
        Private iCardID As Integer

        Public Sub New()

        End Sub

        Protected Sub ClearAll()
            iDown = 0
            iUp = 0
            sElement = String.Empty
            iLevel = 0
            iLeft = 0
            iRight = 0
            sCardName = String.Empty
            bSpecial = False
            iCardID = 0
            sDeck = String.Empty
        End Sub

        Public Property CardName() As String
            Get
                Return sCardName
            End Get
            Set(ByVal value As String)
                sCardName = value
            End Set
        End Property

        Public Property Deck() As String
            Get
                Return sDeck
            End Get
            Set(ByVal value As String)
                sDeck = value
            End Set
        End Property
        Public Property CardID() As Integer
            Get
                Return iCardID
            End Get
            Set(ByVal value As Integer)
                iCardID = value
            End Set
        End Property

        Public Property SpecialCard() As Boolean
            Get
                Return bSpecial
            End Get

            Set(ByVal value As Boolean)
                bSpecial = value
            End Set
        End Property
        Public Property Element() As String
            Get
                Return sElement
            End Get
            Set(ByVal value As String)
                sElement = value
            End Set
        End Property

        Public ReadOnly Property DirectionValue(ByVal sDirection As String) As Integer
            Get
                Select Case sDirection
                    Case "Up"
                        Return Up
                    Case "Down"
                        Return Down
                    Case "Left"
                        Return Left
                    Case "Right"
                        Return Right
                End Select
            End Get
        End Property

        Public Property Down() As Integer
            Get
                Return iDown
            End Get
            Set(ByVal value As Integer)
                iDown = value
            End Set
        End Property

        Public Property Up() As Integer
            Get
                Return iUp
            End Get
            Set(ByVal value As Integer)
                iUp = value
            End Set
        End Property

        Public Property Right() As Integer
            Get
                Return iRight
            End Get
            Set(ByVal value As Integer)
                iRight = value
            End Set
        End Property

        Public Property Left() As Integer
            Get
                Return iLeft
            End Get
            Set(ByVal value As Integer)
                iLeft = value
            End Set
        End Property

        Public Property Level() As Integer
            Get
                Return iLevel
            End Get
            Set(ByVal value As Integer)
                iLevel = value
            End Set
        End Property
    End Class
End Namespace