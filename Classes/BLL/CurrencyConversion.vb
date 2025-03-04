'Public Class CurrencyConversion
'    Public Sub IncomingModule(ByVal sCommand As String, ByVal incoming As String, ByVal socket As Integer)
'        Select Case sCommand
'            Case "CONVGPAP"
'                Call gpapconv(incoming, socket)
'            Case "CONVAPGP"
'                Call apgpconv(incoming, socket)
'            Case "CONVCASHGP"
'                Call cashgpconv(incoming, socket)
'        End Select
'    End Sub

'    Private Sub gpapconv(ByVal incoming As String, ByVal socket As Integer)
'        Dim lostvar As String = String.empty, sGP As String = String.Empty
'        Dim iGP As Integer, iAP As Integer, iRatio As Integer
'        Dim oTables As New TableFunctions
'        Dim oChatFunctions As New ChatFunctions

'        Try
'            Dim oPlayerAccount As New PlayerAccountDAL(frmSystray.Winsock(socket).Tag)

'            If oTables.findtableplayer(frmSystray.Winsock(socket).Tag) = True Then
'                Call blockchat(String.Empty, "Problem", "You cannot convert GP while you are in a game", socket)
'                Exit Sub
'            End If

'            If getServerStats("gpapconversion") = "0" Then
'                Call blockchat(String.Empty, "Problem", "The server operator has chosen for the GP to AP conversion be disabled", socket)
'                Exit Sub
'            End If

'            Divide(incoming, " ", lostvar, sGP)

'            iGP = Val(sGP)

'            If iGP <= 0 Then Exit Sub

'            If iGP > oPlayerAccount.Gold Then
'                Call blockchat(String.Empty, "Problem", "You cannot convert more GP than you have.", socket)
'                oChatFunctions.uniwarn(String.Concat(frmSystray.Winsock(socket).Tag, " tried to convert more GP than he has"))
'                Call historyadd(frmSystray.Winsock(socket).Tag, 2, String.Concat(frmSystray.Winsock(socket).Tag, " tried to convert more GP than he has"))
'                Exit Sub
'            End If

'            iRatio = Val(getServerStats("gpapratio"))

'            If iRatio > 50 Then
'                Call blockchat(String.Empty, "Problem", "Server could not recognize this request.  An email was dispatched to the server operator", socket)
'                Call errorsub(String.Concat(incoming, " - CRITICAL - Ratio: ", iRatio), "gpapconv")
'                Exit Sub
'            End If

'            iAP = Int(iGP * iRatio)

'            Call gpadd(frmSystray.Winsock(socket).Tag, -1 * iGP, True)
'            Send(String.Empty, String.Concat("UPDATEAP ", giveap(frmSystray.Winsock(socket).Tag, iAP, False)), socket)

'            Call historyadd(frmSystray.Winsock(socket).Tag, 16, String.Concat(frmSystray.Winsock(socket).Tag, " converted ", iGP, "GP into ", iAP, "AP at a ratio of ", iRatio))
'            Call blockchat(frmSystray.Winsock(socket).Tag, "Conversion", String.Concat("You converted ", iGP, "GP into ", iAP, "AP."))
'        Catch ex As Exception
'            Call errorsub(String.Concat(ex.ToString, " (", incoming, ")"), "gpapconv")
'        End Try
'    End Sub

'    Private Sub apgpconv(ByVal incoming As String, ByVal socket As Integer)
'        Try
'            Dim oPlayerAccount As New PlayerAccountDAL(frmSystray.Winsock(socket).Tag)
'            Dim oTables As New TableFunctions
'            Dim oChatFunctions As New ChatFunctions

'            If oTables.findtableplayer(frmSystray.Winsock(socket).Tag) = True Then
'                Call blockchat(String.Empty, "Problem", "You cannot convert AP while you are in a game", socket)
'                Exit Sub
'            End If

'            If getServerStats("apgpconversion") = "0" Then
'                Call blockchat(String.Empty, "Problem", "The server operator has chosen for the AP to GP conversion be disabled", socket)
'                Exit Sub
'            End If

'            Dim sString() As String = incoming.Split(" ")
'            Dim iAP As Integer = Val(sString(1))

'            If iAP <= 0 Then Exit Sub

'            If iAP > oPlayerAccount.AP Then
'                Call blockchat(String.Empty, "Problem", "You cannot convert more AP than you have.", socket)
'                oChatFunctions.uniwarn(String.Concat(frmSystray.Winsock(socket).Tag, " tried to convert more AP than he has"))
'                Call historyadd(frmSystray.Winsock(socket).Tag, 2, String.Concat(frmSystray.Winsock(socket).Tag, " tried to convert more AP than he has"))
'                Exit Sub
'            End If

'            Dim iRatio As Integer = Val(getServerStats("apgpratio"))

'            If iRatio < 250 Then
'                Call blockchat(String.Empty, "Problem", "Server could not recognize this request.  An email was dispatched to the server operator", socket)
'                Call errorsub(String.Concat(incoming, " - CRITICAL - Ratio: ", iRatio), "apgpconv")
'                Exit Sub
'            End If

'            Dim iGP As Integer = Int(iAP / iRatio)

'            Call gpadd(frmSystray.Winsock(socket).Tag, iGP, True)
'            Send(String.Empty, String.Concat("UPDATEAP ", giveap(frmSystray.Winsock(socket).Tag, -1 * iAP, False)), socket)

'            Call historyadd(frmSystray.Winsock(socket).Tag, 16, String.Concat(frmSystray.Winsock(socket).Tag, " converted ", iAP, "AP into ", iGP, "GP at a ratio of ", iRatio))
'            Call blockchat(frmSystray.Winsock(socket).Tag, "Conversion", String.Concat("You converted ", iAP, "AP into ", iGP, "GP."))
'        Catch ex As Exception
'            Call errorsub(String.Concat(ex.ToString, " (", incoming, ")"), "apgpconv")
'        End Try
'    End Sub

'    Private Sub cashgpconv(ByVal incoming As String, ByVal socket As Integer)
'        Try
'            Dim oPlayerAccount As New PlayerAccountDAL(frmSystray.Winsock(socket).Tag)
'            Dim oTables As New TableFunctions
'            Dim oChatFunctions As New ChatFunctions

'            If oTables.findtableplayer(frmSystray.Winsock(socket).Tag) = True Then
'                Call blockchat(String.Empty, "Problem", "You cannot convert Cash while you are in a game", socket)
'                Exit Sub
'            End If

'            If getServerStats("cashtogpconversion") = "0" Then
'                Call blockchat(String.Empty, "Problem", "The server operator has chosen for the Cash to GP conversion be disabled", socket)
'                Exit Sub
'            End If

'            Dim sString() As String = incoming.Split(" ")
'            Dim dblCash As Double = Val(sString(1))

'            If dblCash <= 0 Then Exit Sub

'            If dblCash > oPlayerAccount.Cash Then
'                Call blockchat(String.Empty, "Problem", "You cannot convert more Cash than you have.", socket)
'                oChatFunctions.uniwarn(String.Concat(frmSystray.Winsock(socket).Tag, " tried to convert more Cash than he has"))
'                Call historyadd(frmSystray.Winsock(socket).Tag, 2, String.Concat(frmSystray.Winsock(socket).Tag, " tried to convert more Cash than he has"))
'                Exit Sub
'            End If

'            Dim iRatio As Integer = Val(getServerStats("cashtogpratio"))

'            If iRatio > 500 Then
'                Call blockchat(String.Empty, "Problem", "Server could not recognize this request.  An email was dispatched to the server operator", socket)
'                Call errorsub(String.Concat(incoming, " - CRITICAL CASH - Ratio: ", iRatio), "cashgpconv")
'                Exit Sub
'            End If

'            Dim iGP As Integer = Int(dblCash * iRatio)
'            oPlayerAccount.Cash -= dblCash

'            Call gpadd(frmSystray.Winsock(socket).Tag, iGP, True)
'            Call setaccountdata(frmSystray.Winsock(socket).Tag, "cash", oPlayerAccount.Cash, 0)

'            Send(String.Empty, String.Concat("UPDATECASH ", oPlayerAccount.Cash), socket)

'            Call historyadd(frmSystray.Winsock(socket).Tag, 16, String.Concat(frmSystray.Winsock(socket).Tag, " converted $", dblCash, " into ", iGP & "GP at a ratio of ", iRatio))
'            Call blockchat(frmSystray.Winsock(socket).Tag, "Conversion", String.Concat("You converted $", dblCash, " into ", iGP, "GP."))
'        Catch ex As Exception
'            Call errorsub(String.Concat(ex.ToString, " (", incoming, ")"), "cashgpconv")
'        End Try
'    End Sub
'End Class
