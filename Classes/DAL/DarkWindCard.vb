'Imports mySQL.Data.MySqlClient
'Imports MySQLFactory
'Imports System.ComponentModel
'Imports System.Configuration

'Public Class DarkWindCard
'    Private bError As Boolean
'    Private oError As Exception

'    Private iLeft As Integer
'    Private iRight As Integer
'    Private iDown As Integer
'    Private iUp As Integer
'    Private sElement As String
'    Private iLevel As Integer
'    Private sCardName As String
'    Private bSpecial As Boolean
'    Private sDeck As String
'    Private iCardID As Integer
'    Private iTokenCost As Integer

'    Public Sub New(ByVal sCard As String)
'        LoadRecord(sCard)
'    End Sub

'    Public Sub New()

'    End Sub
'    Protected Sub ClearAll()
'        iDown = 0
'        iUp = 0
'        sElement = String.Empty
'        iLevel = 0
'        iLeft = 0
'        iRight = 0
'        sCardName = String.Empty
'        bSpecial = False
'        iCardID = 0
'        sDeck = String.Empty
'    End Sub

'    Public Property CardName() As String
'        Get
'            Return sCardName
'        End Get
'        Set(ByVal value As String)
'            sCardName = value
'        End Set
'    End Property

'    Public ReadOnly Property TokenCost() As Integer
'        Get
'            If CardName = "Atomsplitter" Then
'                Return 8
'            ElseIf Level = 8 Or Level = 7 Then
'                Return 1
'            ElseIf Level = 9 Then
'                Return 2
'            ElseIf Level = 10 And SpecialCard = False Then
'                Return 3
'            ElseIf Level = 10 And SpecialCard = True Then
'                Return 4
'            Else
'                Return 9999
'            End If
'        End Get
'    End Property
'    Public Property Deck() As String
'        Get
'            Return sDeck
'        End Get
'        Set(ByVal value As String)
'            sDeck = value
'        End Set
'    End Property
'    Public Property CardID() As Integer
'        Get
'            Return iCardID
'        End Get
'        Set(ByVal value As Integer)
'            iCardID = value
'        End Set
'    End Property

'    Public Property SpecialCard() As Boolean
'        Get
'            Return bSpecial
'        End Get

'        Set(ByVal value As Boolean)
'            bSpecial = value
'        End Set
'    End Property
'    Public Property Element() As String
'        Get
'            Return sElement
'        End Get
'        Set(ByVal value As String)
'            sElement = value
'        End Set
'    End Property
'    Public ReadOnly Property DirectionValue(ByVal sDirection As String) As Integer
'        Get
'            Select Case sDirection
'                Case "Up"
'                    Return Up
'                Case "Down"
'                    Return Down
'                Case "Left"
'                    Return Left
'                Case "Right"
'                    Return Right
'            End Select
'        End Get
'    End Property

'    Public Property Down() As Integer
'        Get
'            Return iDown
'        End Get
'        Set(ByVal value As Integer)
'            iDown = value
'        End Set
'    End Property

'    Public Property Up() As Integer
'        Get
'            Return iUp
'        End Get
'        Set(ByVal value As Integer)
'            iUp = value
'        End Set
'    End Property

'    Public Property Right() As Integer
'        Get
'            Return iRight
'        End Get
'        Set(ByVal value As Integer)
'            iRight = value
'        End Set
'    End Property

'    Public Property Left() As Integer
'        Get
'            Return iLeft
'        End Get
'        Set(ByVal value As Integer)
'            iLeft = value
'        End Set
'    End Property

'    Public Property Level() As Integer
'        Get
'            Return iLevel
'        End Get
'        Set(ByVal value As Integer)
'            iLevel = value
'        End Set
'    End Property

'    Public Sub LoadRecord(ByVal sCard As String)
'        Try
'            If sCard = String.Empty Or sCard = "Block" Or sCard = "Empty" Then
'                ClearAll()
'                Exit Sub
'            End If

'            Dim oMySqlHelper As New MySQLFactory.MySQLDBHelper
'            Dim oDataSet As New DataSet

'            Dim arParms() As MySqlParameter = New MySqlParameter(0) {}
'            arParms(0) = New MySqlParameter("?sCard", sCard)

'            oMySqlHelper.FillDataSet(ConnectionString, CommandType.StoredProcedure, "sps_Card", oDataSet, New String() {"Results"}, arParms)

'            If oDataSet Is Nothing Then
'                Throw New Exception
'            Else
'                With oDataSet.Tables(0).Rows(0)
'                    sCardName = .Item("cardname").ToString
'                    iLevel = .Item("level")
'                    iLeft = .Item("left")
'                    iRight = .Item("right")
'                    iUp = .Item("up")
'                    iDown = .Item("down")
'                    sElement = .Item("element").ToString
'                    bSpecial = IIf(.Item("flggold") = 1, True, False)
'                    sDeck = .Item("deck").ToString
'                    iCardID = .Item("id")
'                End With
'            End If
'        Catch ex As Exception
'            ErrorDescription = ex
'            ErrorFlag = True
'            Call errorsub(ex.ToString, "DarkWindCard.LoadRecord")
'        Finally
'            oServerConfig.MySQLCalls += 1
'        End Try
'    End Sub

'    <Description("Gets or Sets Exception Information from Retrieving Data")> _
'Public Property ErrorDescription() As Exception
'        Get
'            Return oError
'        End Get
'        Set(ByVal Value As Exception)
'            oError = Value
'        End Set
'    End Property

'    <Description("Gets or Sets the Error Flag if Exception Information Exists")> _
'    Public Property ErrorFlag() As Boolean
'        Get
'            Return bError
'        End Get
'        Set(ByVal Value As Boolean)
'            bError = Value
'        End Set
'    End Property
'End Class
