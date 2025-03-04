Public Class clsPlayer
    Private iNickname As String
    Private sDataWaitingToSend As String
    Private bSendingData As Boolean
    Private sDataWaitingToBeProcessed As String
    Private bProcessingData As Boolean
    Private iPackets As Integer
    Private sIP As String
    Private oConnectionState As Integer
    Private bPing As Boolean
    Private bAway As Boolean

    Public Property ConnectionState() As Integer
        Get
            Return oConnectionState
        End Get

        Set(ByVal value As Integer)
            oConnectionState = value
        End Set
    End Property

    Public Property Away() As Boolean
        Get
            Return bAway
        End Get

        Set(ByVal value As Boolean)
            bAway = value
        End Set
    End Property

    Public Property Ping() As Boolean
        Get
            Return bPing
        End Get

        Set(ByVal value As Boolean)
            bPing = value
        End Set
    End Property

    Public Property IP() As String
        Get
            Return sIP
        End Get
        Set(ByVal value As String)
            sIP = value
        End Set
    End Property
    Public Property PacketsSent() As Integer
        Get
            Return iPackets
        End Get
        Set(ByVal value As Integer)
            iPackets = value
        End Set
    End Property

    Public Property ProcessingData() As Boolean
        Get
            Return bProcessingData
        End Get
        Set(ByVal value As Boolean)
            bProcessingData = value
        End Set
    End Property

    Public Property DataWaitingToBeProcessed() As String
        Get
            Return sDataWaitingToBeProcessed
        End Get

        Set(ByVal value As String)
            sDataWaitingToBeProcessed = value
        End Set
    End Property

    Public Property SendingData() As Boolean
        Get
            Return bSendingData
        End Get

        Set(ByVal value As Boolean)
            bSendingData = value
        End Set
    End Property
    Public Property DataWaitingToSend() As String
        Get
            Return sDataWaitingToSend
        End Get
        Set(ByVal value As String)
            sDataWaitingToSend = value
        End Set
    End Property

    Public Property nickname() As String
        Get
            Return iNickname
        End Get

        Set(ByVal value As String)
            iNickname = value
        End Set
    End Property
End Class