Option Strict Off
Option Explicit On
Imports System.Configuration
Friend Class frmConnect
	Inherits System.Windows.Forms.Form
	Dim ininame As String
	Dim version As String
	Dim inbuildlimit As String
	
    Private Sub frmConnect_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load
        ReDim players(0)

        ininame = My.Application.Info.DirectoryPath & "\config\ttserver.ini"
        Timer1.Enabled = True
        version = My.Application.Info.Version.Major & "." & My.Application.Info.Version.Minor & "." & My.Application.Info.Version.Revision & "a" & "-Regis"
        '************************
        ' Looking if this is a registred or free server
        ' 0 = Unlimitted # of players
        ' 8 =  T.T. Players maximum (free server)
        ' Default for free server is 8 players max (8 connections)
        ' NOTE : There is no account limit on a free server and
        ' all cards are available
        '************************
        inbuildlimit = "0"
        ' This is a *Registred* Server
        'inbuildlimit = "8"
        ' This is a *free* server

        If (Dir(ininame) <> "") Then
            port.Text = getServerStats("port")
            If (CDbl(inbuildlimit) = 0) Then
                limit.Text = getServerStats("servercap")
            Else
                limit.Text = inbuildlimit
                limit.Enabled = False
            End If
        End If
    End Sub

    Private Sub Timer1_Tick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles Timer1.Tick
        Call Command1_Click(command1, New System.EventArgs())
        Timer1.Enabled = False
    End Sub

    Private Sub command1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles command1.Click
        ininame = My.Application.Info.DirectoryPath & "\config\ttserver.ini"
        version = My.Application.Info.Version.Major & "." & My.Application.Info.Version.Minor & "." & My.Application.Info.Version.Revision & "a" & "-Regis"
        '************************
        ' Looking if this is a registred or free server
        ' 0 = Unlimitted # of players
        ' 8 =  T.T. Players maximum (free server)
        ' Default for free server is 8 players max (8 connections)
        ' NOTE : There is no account limit on a free server and
        ' all cards are available
        '************************
        inbuildlimit = CStr(0)
        ' This is a *Registred* Server
        'inbuildlimit = "8"
        ' This is a *free* server

        writeini("Configuration", "Port", (Me.port.Text), ininame)

        If (CDbl(inbuildlimit) = 0) Then
            writeini("Configuration", "Limit", (limit.Text), ininame)
        End If

        oServerConfig.MaxPlayers = Val(limit.Text)

        If oServerConfig.MaxPlayers <> 0 Then
            oServerConfig.MaxPlayers = Val(limit.Text)
            Me.Tag = Val(limit.Text)
        Else
            oServerConfig.MaxPlayers = 499
            Me.Tag = 499
        End If

        Timer1.Enabled = False
        'Server initialization
        frmSystray.Show()

        'Me.Hide()
    End Sub

    Private Sub command2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles command2.Click
        '    Unload frmDebug
        Me.Close()
    End Sub
End Class