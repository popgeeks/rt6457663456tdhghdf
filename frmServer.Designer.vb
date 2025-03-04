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
    Public WithEvents filelist As Microsoft.VisualBasic.Compatibility.VB6.FileListBox
    Public WithEvents tmrkilltable As System.Windows.Forms.Timer
    Public WithEvents tmrpacket As System.Windows.Forms.Timer
    Public WithEvents tmrping As System.Windows.Forms.Timer
    Public WithEvents Timer1 As System.Windows.Forms.Timer
    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmserver))
        Me.filelist = New Microsoft.VisualBasic.Compatibility.VB6.FileListBox()
        Me.tmrkilltable = New System.Windows.Forms.Timer(Me.components)
        Me.tmrpacket = New System.Windows.Forms.Timer(Me.components)
        Me.tmrping = New System.Windows.Forms.Timer(Me.components)
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.tmrSendPackets = New System.Windows.Forms.Timer(Me.components)
        Me.tmrReceivePackets = New System.Windows.Forms.Timer(Me.components)
        Me.tmrSocketKill = New System.Windows.Forms.Timer(Me.components)
        Me.tmrShutDown = New System.Windows.Forms.Timer(Me.components)
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        Me.SuspendLayout()
        '
        'filelist
        '
        Me.filelist.BackColor = System.Drawing.SystemColors.Window
        Me.filelist.Cursor = System.Windows.Forms.Cursors.Default
        Me.filelist.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.filelist.ForeColor = System.Drawing.SystemColors.WindowText
        Me.filelist.FormattingEnabled = True
        Me.filelist.Location = New System.Drawing.Point(533, 426)
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
        'tmrping
        '
        Me.tmrping.Enabled = True
        Me.tmrping.Interval = 60000
        '
        'Timer1
        '
        Me.Timer1.Enabled = True
        Me.Timer1.Interval = 60000
        '
        'tmrSendPackets
        '
        Me.tmrSendPackets.Interval = 200
        '
        'tmrReceivePackets
        '
        Me.tmrReceivePackets.Interval = 200
        '
        'tmrSocketKill
        '
        Me.tmrSocketKill.Enabled = True
        Me.tmrSocketKill.Interval = 15000
        '
        'tmrShutDown
        '
        Me.tmrShutDown.Interval = 60000
        '
        'TextBox1
        '
        Me.TextBox1.BackColor = System.Drawing.Color.Black
        Me.TextBox1.ForeColor = System.Drawing.Color.White
        Me.TextBox1.Location = New System.Drawing.Point(22, 28)
        Me.TextBox1.Multiline = True
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.TextBox1.Size = New System.Drawing.Size(579, 360)
        Me.TextBox1.TabIndex = 92
        '
        'frmserver
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.Black
        Me.ClientSize = New System.Drawing.Size(637, 412)
        Me.ControlBox = False
        Me.Controls.Add(Me.TextBox1)
        Me.Controls.Add(Me.filelist)
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
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents tmrSendPackets As System.Windows.Forms.Timer
    Friend WithEvents tmrReceivePackets As System.Windows.Forms.Timer
    Public WithEvents tmrSocketKill As System.Windows.Forms.Timer
    Friend WithEvents tmrShutDown As System.Windows.Forms.Timer
    Friend WithEvents TextBox1 As System.Windows.Forms.TextBox
#End Region
End Class