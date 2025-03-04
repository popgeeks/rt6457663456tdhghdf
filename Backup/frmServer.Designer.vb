<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class frmserver
#Region "Windows Form Designer generated code "
	<System.Diagnostics.DebuggerNonUserCode()> Public Sub New()
		MyBase.New()
		'This call is required by the Windows Form Designer.
		InitializeComponent()
	End Sub
	'Form overrides dispose to clean up the component list.
	<System.Diagnostics.DebuggerNonUserCode()> Protected Overloads Overrides Sub Dispose(ByVal Disposing As Boolean)
		If Disposing Then
			If Not components Is Nothing Then
				components.Dispose()
			End If
		End If
		MyBase.Dispose(Disposing)
	End Sub
	'Required by the Windows Form Designer
	Private components As System.ComponentModel.IContainer
	Public ToolTip1 As System.Windows.Forms.ToolTip
    Public WithEvents filelist As Microsoft.VisualBasic.Compatibility.VB6.FileListBox
    Public WithEvents tmrkilltable As System.Windows.Forms.Timer
    Public WithEvents tmrpacket As System.Windows.Forms.Timer
    Public WithEvents lstpacketcontrol As System.Windows.Forms.ListBox
    Public WithEvents cmdremove As System.Windows.Forms.Button
    Public WithEvents Winsock1 As AxMSWinsockLib.AxWinsock
    Public WithEvents tmrping As System.Windows.Forms.Timer
    Public WithEvents lstadmin As System.Windows.Forms.ListBox
    Public WithEvents timedout As System.Windows.Forms.Timer
    Public WithEvents Timer1 As System.Windows.Forms.Timer
    Public WithEvents Inet_ladder As AxInetCtlsObjects.AxInet
    Public WithEvents inet_ladderreport As AxInetCtlsObjects.AxInet
    Public WithEvents Label19 As System.Windows.Forms.Label
    Public WithEvents Label16 As System.Windows.Forms.Label
    Public WithEvents Label14 As System.Windows.Forms.Label
    Public WithEvents MainMenu1 As System.Windows.Forms.MenuStrip
    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmserver))
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.filelist = New Microsoft.VisualBasic.Compatibility.VB6.FileListBox
        Me.tmrkilltable = New System.Windows.Forms.Timer(Me.components)
        Me.tmrpacket = New System.Windows.Forms.Timer(Me.components)
        Me.lstpacketcontrol = New System.Windows.Forms.ListBox
        Me.cmdremove = New System.Windows.Forms.Button
        Me.Winsock1 = New AxMSWinsockLib.AxWinsock
        Me.tmrping = New System.Windows.Forms.Timer(Me.components)
        Me.lstadmin = New System.Windows.Forms.ListBox
        Me.cmsPlayer = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.cmsPlayer_Disconnect = New System.Windows.Forms.ToolStripMenuItem
        Me.timedout = New System.Windows.Forms.Timer(Me.components)
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.Inet_ladder = New AxInetCtlsObjects.AxInet
        Me.inet_ladderreport = New AxInetCtlsObjects.AxInet
        Me.Label19 = New System.Windows.Forms.Label
        Me.Label16 = New System.Windows.Forms.Label
        Me.Label14 = New System.Windows.Forms.Label
        Me.MainMenu1 = New System.Windows.Forms.MenuStrip
        Me.lstplayers = New System.Windows.Forms.ListBox
        Me.btnRefreshCards = New System.Windows.Forms.Button
        Me.tmrSendPackets = New System.Windows.Forms.Timer(Me.components)
        CType(Me.Winsock1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.cmsPlayer.SuspendLayout()
        CType(Me.Inet_ladder, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.inet_ladderreport, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'filelist
        '
        Me.filelist.BackColor = System.Drawing.SystemColors.Window
        Me.filelist.Cursor = System.Windows.Forms.Cursors.Default
        Me.filelist.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.filelist.ForeColor = System.Drawing.SystemColors.WindowText
        Me.filelist.FormattingEnabled = True
        Me.filelist.Location = New System.Drawing.Point(677, 464)
        Me.filelist.Name = "filelist"
        Me.filelist.Pattern = "*.*"
        Me.filelist.Size = New System.Drawing.Size(101, 46)
        Me.filelist.TabIndex = 91
        Me.filelist.Visible = False
        '
        'tmrkilltable
        '
        Me.tmrkilltable.Enabled = True
        Me.tmrkilltable.Interval = 30000
        '
        'tmrpacket
        '
        Me.tmrpacket.Enabled = True
        Me.tmrpacket.Interval = 30000
        '
        'lstpacketcontrol
        '
        Me.lstpacketcontrol.BackColor = System.Drawing.Color.Black
        Me.lstpacketcontrol.Cursor = System.Windows.Forms.Cursors.Default
        Me.lstpacketcontrol.Font = New System.Drawing.Font("Arial", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lstpacketcontrol.ForeColor = System.Drawing.Color.White
        Me.lstpacketcontrol.ItemHeight = 12
        Me.lstpacketcontrol.Location = New System.Drawing.Point(852, 803)
        Me.lstpacketcontrol.Name = "lstpacketcontrol"
        Me.lstpacketcontrol.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lstpacketcontrol.Size = New System.Drawing.Size(147, 412)
        Me.lstpacketcontrol.TabIndex = 47
        Me.lstpacketcontrol.Visible = False
        '
        'cmdremove
        '
        Me.cmdremove.BackColor = System.Drawing.SystemColors.Control
        Me.cmdremove.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdremove.Font = New System.Drawing.Font("Arial", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdremove.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdremove.Location = New System.Drawing.Point(25, 188)
        Me.cmdremove.Name = "cmdremove"
        Me.cmdremove.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdremove.Size = New System.Drawing.Size(113, 17)
        Me.cmdremove.TabIndex = 37
        Me.cmdremove.Text = "Remove"
        Me.cmdremove.UseVisualStyleBackColor = False
        '
        'Winsock1
        '
        Me.Winsock1.Enabled = True
        Me.Winsock1.Location = New System.Drawing.Point(392, 0)
        Me.Winsock1.Name = "Winsock1"
        Me.Winsock1.OcxState = CType(resources.GetObject("Winsock1.OcxState"), System.Windows.Forms.AxHost.State)
        Me.Winsock1.Size = New System.Drawing.Size(28, 28)
        Me.Winsock1.TabIndex = 95
        '
        'tmrping
        '
        Me.tmrping.Enabled = True
        Me.tmrping.Interval = 60000
        '
        'lstadmin
        '
        Me.lstadmin.BackColor = System.Drawing.Color.Black
        Me.lstadmin.Cursor = System.Windows.Forms.Cursors.Default
        Me.lstadmin.Font = New System.Drawing.Font("Arial", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lstadmin.ForeColor = System.Drawing.Color.White
        Me.lstadmin.ItemHeight = 12
        Me.lstadmin.Location = New System.Drawing.Point(25, 70)
        Me.lstadmin.Name = "lstadmin"
        Me.lstadmin.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lstadmin.Size = New System.Drawing.Size(113, 112)
        Me.lstadmin.TabIndex = 12
        '
        'cmsPlayer
        '
        Me.cmsPlayer.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.cmsPlayer_Disconnect})
        Me.cmsPlayer.Name = "cmsPlayer"
        Me.cmsPlayer.Size = New System.Drawing.Size(169, 26)
        '
        'cmsPlayer_Disconnect
        '
        Me.cmsPlayer_Disconnect.Name = "cmsPlayer_Disconnect"
        Me.cmsPlayer_Disconnect.Size = New System.Drawing.Size(168, 22)
        Me.cmsPlayer_Disconnect.Text = "Disconnect Player"
        '
        'timedout
        '
        Me.timedout.Enabled = True
        Me.timedout.Interval = 60000
        '
        'Timer1
        '
        Me.Timer1.Enabled = True
        Me.Timer1.Interval = 60000
        '
        'Inet_ladder
        '
        Me.Inet_ladder.Enabled = True
        Me.Inet_ladder.Location = New System.Drawing.Point(226, 564)
        Me.Inet_ladder.Name = "Inet_ladder"
        Me.Inet_ladder.OcxState = CType(resources.GetObject("Inet_ladder.OcxState"), System.Windows.Forms.AxHost.State)
        Me.Inet_ladder.Size = New System.Drawing.Size(38, 38)
        Me.Inet_ladder.TabIndex = 96
        '
        'inet_ladderreport
        '
        Me.inet_ladderreport.Enabled = True
        Me.inet_ladderreport.Location = New System.Drawing.Point(226, 604)
        Me.inet_ladderreport.Name = "inet_ladderreport"
        Me.inet_ladderreport.OcxState = CType(resources.GetObject("inet_ladderreport.OcxState"), System.Windows.Forms.AxHost.State)
        Me.inet_ladderreport.Size = New System.Drawing.Size(38, 38)
        Me.inet_ladderreport.TabIndex = 97
        '
        'Label19
        '
        Me.Label19.BackColor = System.Drawing.Color.Black
        Me.Label19.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label19.Font = New System.Drawing.Font("Arial", 6.75!, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label19.ForeColor = System.Drawing.Color.White
        Me.Label19.Location = New System.Drawing.Point(853, 788)
        Me.Label19.Name = "Label19"
        Me.Label19.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label19.Size = New System.Drawing.Size(106, 11)
        Me.Label19.TabIndex = 48
        Me.Label19.Text = "Packet Control:"
        Me.Label19.Visible = False
        '
        'Label16
        '
        Me.Label16.BackColor = System.Drawing.Color.Black
        Me.Label16.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label16.Font = New System.Drawing.Font("Arial", 6.75!, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label16.ForeColor = System.Drawing.Color.White
        Me.Label16.Location = New System.Drawing.Point(156, 42)
        Me.Label16.Name = "Label16"
        Me.Label16.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label16.Size = New System.Drawing.Size(106, 11)
        Me.Label16.TabIndex = 34
        Me.Label16.Text = "Player List:"
        '
        'Label14
        '
        Me.Label14.BackColor = System.Drawing.Color.Black
        Me.Label14.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label14.Font = New System.Drawing.Font("Arial", 6.75!, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label14.ForeColor = System.Drawing.Color.White
        Me.Label14.Location = New System.Drawing.Point(23, 44)
        Me.Label14.Name = "Label14"
        Me.Label14.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label14.Size = New System.Drawing.Size(106, 11)
        Me.Label14.TabIndex = 32
        Me.Label14.Text = "Admins Logged In:"
        '
        'MainMenu1
        '
        Me.MainMenu1.Location = New System.Drawing.Point(0, 0)
        Me.MainMenu1.Name = "MainMenu1"
        Me.MainMenu1.Size = New System.Drawing.Size(391, 24)
        Me.MainMenu1.TabIndex = 98
        '
        'lstplayers
        '
        Me.lstplayers.BackColor = System.Drawing.Color.Black
        Me.lstplayers.ContextMenuStrip = Me.cmsPlayer
        Me.lstplayers.Cursor = System.Windows.Forms.Cursors.Default
        Me.lstplayers.Font = New System.Drawing.Font("Arial", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lstplayers.ForeColor = System.Drawing.Color.White
        Me.lstplayers.ItemHeight = 12
        Me.lstplayers.Location = New System.Drawing.Point(158, 56)
        Me.lstplayers.Name = "lstplayers"
        Me.lstplayers.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lstplayers.Size = New System.Drawing.Size(162, 196)
        Me.lstplayers.TabIndex = 9
        '
        'btnRefreshCards
        '
        Me.btnRefreshCards.BackColor = System.Drawing.SystemColors.Control
        Me.btnRefreshCards.Cursor = System.Windows.Forms.Cursors.Default
        Me.btnRefreshCards.Font = New System.Drawing.Font("Arial", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnRefreshCards.ForeColor = System.Drawing.SystemColors.ControlText
        Me.btnRefreshCards.Location = New System.Drawing.Point(25, 244)
        Me.btnRefreshCards.Name = "btnRefreshCards"
        Me.btnRefreshCards.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.btnRefreshCards.Size = New System.Drawing.Size(113, 17)
        Me.btnRefreshCards.TabIndex = 99
        Me.btnRefreshCards.Text = "Refresh Cards"
        Me.btnRefreshCards.UseVisualStyleBackColor = False
        '
        'tmrSendPackets
        '
        Me.tmrSendPackets.Interval = 200
        '
        'frmserver
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.Black
        Me.ClientSize = New System.Drawing.Size(391, 325)
        Me.ControlBox = False
        Me.Controls.Add(Me.btnRefreshCards)
        Me.Controls.Add(Me.filelist)
        Me.Controls.Add(Me.lstpacketcontrol)
        Me.Controls.Add(Me.cmdremove)
        Me.Controls.Add(Me.Winsock1)
        Me.Controls.Add(Me.lstadmin)
        Me.Controls.Add(Me.Inet_ladder)
        Me.Controls.Add(Me.lstplayers)
        Me.Controls.Add(Me.inet_ladderreport)
        Me.Controls.Add(Me.Label19)
        Me.Controls.Add(Me.MainMenu1)
        Me.Controls.Add(Me.Label14)
        Me.Controls.Add(Me.Label16)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Font = New System.Drawing.Font("Times New Roman", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Location = New System.Drawing.Point(3, 41)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmserver"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Tag = "0"
        Me.Text = "Triple Triad Extreme Server"
        CType(Me.Winsock1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.cmsPlayer.ResumeLayout(False)
        CType(Me.Inet_ladder, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.inet_ladderreport, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents cmsPlayer As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents cmsPlayer_Disconnect As System.Windows.Forms.ToolStripMenuItem
    Public WithEvents lstplayers As System.Windows.Forms.ListBox
    Public WithEvents btnRefreshCards As System.Windows.Forms.Button
    Friend WithEvents tmrSendPackets As System.Windows.Forms.Timer
#End Region
End Class