Module errorhandler
    Public Sub errorsub(ByVal reason As String, ByVal procedure As String)
        Try
            '<summary>
            ' Write Event to Windows Event Log
            ' <params>
            ' p_strMessage = message body to log
            ' p_logType = type of event to log (Enum: EventLogEntryType)
            ' p_strSource = source of the event (normally the application name)
            ' p_strLog = which log to write to (Application|System|Security|custom)
            ' </params>
            '</summary>

            Dim evtLog As New EventLog("Application", ".", "TTE Server")

            ' Check if source already exists before creating the log.
            If EventLog.SourceExists("Source") Then
                EventLog.DeleteEventSource("Source")
            End If

            evtLog.WriteEntry(String.Concat(procedure, ">>", reason), EventLogEntryType.Error)
        Catch ex As Exception
        End Try
    End Sub
End Module