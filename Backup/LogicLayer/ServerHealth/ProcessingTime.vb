Namespace ServerHealth
    Public Class ProcessingTime
        Private dBegin As DateTime
        Private dEnd As DateTime

        Public Property BeginProcessing() As DateTime
            Get
                Return dBegin
            End Get
            Set(ByVal value As DateTime)
                dBegin = value
            End Set
        End Property

        Public Property EndProcessing() As DateTime
            Get
                Return dEnd
            End Get
            Set(ByVal value As DateTime)
                dEnd = value
            End Set
        End Property

        Public ReadOnly Property ProcessingTime() As TimeSpan
            Get
                Dim dTimeSpan As TimeSpan = EndProcessing.Subtract(BeginProcessing)
            End Get
        End Property

        Public ReadOnly Property ProcessingSeconds() As Decimal
            Get
                Try
                    Dim dTimeSpan As TimeSpan = EndProcessing.Subtract(BeginProcessing)
                    Return Convert.ToDecimal(dTimeSpan.TotalSeconds)
                Catch ex As Exception
                    Return 0.0
                End Try
            End Get
        End Property
    End Class
End Namespace