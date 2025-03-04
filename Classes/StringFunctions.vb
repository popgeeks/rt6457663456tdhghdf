Imports System.IO

Public Class StringFunctions
    Public Function GetFileContents(ByVal FullPath As String, _
       Optional ByRef ErrInfo As String = "") As String

        Dim strContents As String
        Dim objReader As StreamReader
        Try

            objReader = New StreamReader(FullPath)
            strContents = objReader.ReadToEnd()
            objReader.Close()
            Return strContents
        Catch Ex As Exception
            ErrInfo = Ex.Message
            Return String.Empty
        End Try
    End Function

    Public Function SaveTextToFile(ByVal strData As String, _
     ByVal FullPath As String, _
       Optional ByVal ErrInfo As String = "") As Boolean

        Dim bAns As Boolean = False
        Dim objReader As StreamWriter
        Try
            Dim sContents As String = GetFileContents(FullPath)
            objReader = New StreamWriter(FullPath)
            objReader.Write(String.Concat(sContents, vbCrLf, strData))
            objReader.Close()
            bAns = True
        Catch Ex As Exception
            ErrInfo = Ex.Message
        End Try

        Return bAns
    End Function

    Public Sub writepacketlog(ByVal sNick As String, ByVal sPacket As String, ByVal dTime As Decimal)
        Try
            SaveTextToFile(String.Concat(sPacket, ">> ", dTime, "s"), My.Application.Info.DirectoryPath & "\logs\accounts\packetlogs\" & sNick & ".log")
            SaveTextToFile(String.Concat(sNick, ">> ", sPacket, ">> ", dTime, "s"), My.Application.Info.DirectoryPath & "\logs\" & Format(Date.Today, "MM-dd-yyyy").ToString & ".log")
        Catch ioex As System.IO.IOException
            ' do nothing
        Catch ex As Exception
            Call errorsub(ex.Message, "writepacketlog")
        End Try
    End Sub
End Class
