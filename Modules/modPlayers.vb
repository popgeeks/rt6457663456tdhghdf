Imports MySql.Data.MySqlClient
Imports MySQLFactory
Imports System.Configuration

Module modPlayers
    Public players() As clsPlayer

    Public Function NewPlayerClass(ByVal pintIndex As Integer) As clsPlayer
        Try
            Dim intUBound As Integer = UBound(players)

            If pintIndex > intUBound Then
                ReDim Preserve players(pintIndex)
            End If

            players(pintIndex) = New clsPlayer
            players(pintIndex).DataWaitingToSend = String.Empty
            players(pintIndex).SendingData = False
            players(pintIndex).DataWaitingToBeProcessed = String.Empty
            players(pintIndex).ProcessingData = False
            players(pintIndex).PacketsSent = 0
            players(pintIndex).IP = String.Empty
            players(pintIndex).Away = False

            Return players(pintIndex)
        Catch ex As Exception
            Call ErrorSub(ex.ToString, "modPlayers.NewPlayerClass")
            Return Nothing
        End Try
    End Function

    Public Sub RemovePlayerClass(ByVal pintIndex As Integer)
        Try
            players(pintIndex) = Nothing
        Catch ex As Exception
            Call errorsub(ex, "RemovePlayerClass")
        End Try
    End Sub

    Public Function GetSocket(ByVal sPlayer As String) As Integer
        Try
            For x As Integer = 1 To frmSystray.Winsock.Count
                If frmSystray.Winsock(x).Tag.ToLower = sPlayer.ToLower Then
                    Return x
                End If
            Next

            Return 0
        Catch ex As Exception
            Return 0
        End Try
    End Function

    'Public Function GetSocket(ByVal sNick As String) As Integer
    '    Try
    '        If sNick = String.Empty Then Exit Function

    '        Dim oMySqlHelper As New MySQLFactory.MySQLDBHelper
    '        Dim oDataRow As DataRow = Nothing

    '        Dim arParms() As MySqlParameter = New MySqlParameter(0) {}
    '        arParms(0) = New MySqlParameter("?sNick", sNick)

    '        oMySqlHelper.FillDataRow(ConnectionString, CommandType.StoredProcedure, "sps_PlayerSocket", oDataRow, arParms)

    '        If oDataRow Is Nothing Then
    '            Throw New Exception
    '        Else
    '            Return oDataRow.Item("id")
    '        End If
    '    Catch ex As Exception
    '        'Call errorsub(ex, "GetSocket")
    '    End Try
    'End Function
End Module