<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class frmConnect
#Region "Windows Form Designer generated code "
	<System.Diagnostics.DebuggerNonUserCode()> Public Sub New()
		MyBase.New()
		'This call is required by the Windows Form Designer.
		InitializeComponent()
	End Sub
	'Form overrides dispose to clean up the component list.
	<System.Diagnostics.DebuggerNonUserCode()> Protected Overloads Overrides Sub Dispose(ByVal Disposing As Boolean)
		If Disposing Then
			Static fTerminateCalled As Boolean
			If Not fTerminateCalled Then
                'RaiseEvent me.FormClosed()
				fTerminateCalled = True
			End If
			If Not components Is Nothing Then
				components.Dispose()
			End If
		End If
		MyBase.Dispose(Disposing)
	End Sub
	'Required by the Windows Form Designer
	Private components As System.ComponentModel.IContainer
	Public ToolTip1 As System.Windows.Forms.ToolTip
    Public WithEvents port As System.Windows.Forms.TextBox
	Public WithEvents limit As System.Windows.Forms.TextBox
	Public WithEvents Timer1 As System.Windows.Forms.Timer
	Public WithEvents imlimages As System.Windows.Forms.ImageList
	Public WithEvents Label2 As System.Windows.Forms.Label
	Public WithEvents Label1 As System.Windows.Forms.Label
	Public WithEvents lbltitle As System.Windows.Forms.Label
    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.
	<System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmConnect))
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.port = New System.Windows.Forms.TextBox
        Me.limit = New System.Windows.Forms.TextBox
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.imlimages = New System.Windows.Forms.ImageList(Me.components)
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.lbltitle = New System.Windows.Forms.Label
        Me.command1 = New System.Windows.Forms.Button
        Me.command2 = New System.Windows.Forms.Button
        Me.SuspendLayout()
        '
        'port
        '
        Me.port.AcceptsReturn = True
        Me.port.BackColor = System.Drawing.Color.Black
        Me.port.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.port.Font = New System.Drawing.Font("Verdana", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.port.ForeColor = System.Drawing.Color.White
        Me.port.Location = New System.Drawing.Point(310, 215)
        Me.port.MaxLength = 5
        Me.port.Name = "port"
        Me.port.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.port.Size = New System.Drawing.Size(57, 18)
        Me.port.TabIndex = 0
        Me.port.Text = "2000"
        '
        'limit
        '
        Me.limit.AcceptsReturn = True
        Me.limit.BackColor = System.Drawing.Color.Black
        Me.limit.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.limit.Font = New System.Drawing.Font("Verdana", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.limit.ForeColor = System.Drawing.Color.White
        Me.limit.Location = New System.Drawing.Point(310, 240)
        Me.limit.MaxLength = 5
        Me.limit.Name = "limit"
        Me.limit.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.limit.Size = New System.Drawing.Size(57, 18)
        Me.limit.TabIndex = 1
        Me.limit.Text = "0"
        '
        'Timer1
        '
        Me.Timer1.Enabled = True
        Me.Timer1.Interval = 20000
        '
        'imlimages
        '
        Me.imlimages.ImageStream = CType(resources.GetObject("imlimages.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.imlimages.TransparentColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.imlimages.Images.SetKeyName(0, "hand")
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.Color.Black
        Me.Label2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Label2.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label2.Font = New System.Drawing.Font("Verdana", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.White
        Me.Label2.Location = New System.Drawing.Point(151, 236)
        Me.Label2.Name = "Label2"
        Me.Label2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label2.Size = New System.Drawing.Size(145, 17)
        Me.Label2.TabIndex = 5
        Me.Label2.Text = "Server Cap (0 as None):"
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.Black
        Me.Label1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Label1.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label1.Font = New System.Drawing.Font("Verdana", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.White
        Me.Label1.Location = New System.Drawing.Point(151, 212)
        Me.Label1.Name = "Label1"
        Me.Label1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label1.Size = New System.Drawing.Size(145, 17)
        Me.Label1.TabIndex = 4
        Me.Label1.Text = "Server Port:"
        '
        'lbltitle
        '
        Me.lbltitle.BackColor = System.Drawing.Color.Black
        Me.lbltitle.Cursor = System.Windows.Forms.Cursors.Default
        Me.lbltitle.Font = New System.Drawing.Font("Verdana", 14.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbltitle.ForeColor = System.Drawing.Color.White
        Me.lbltitle.Location = New System.Drawing.Point(16, 32)
        Me.lbltitle.Name = "lbltitle"
        Me.lbltitle.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lbltitle.Size = New System.Drawing.Size(465, 25)
        Me.lbltitle.TabIndex = 3
        Me.lbltitle.Text = "Triple Triad Extreme Server v2"
        Me.lbltitle.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'command1
        '
        Me.command1.Location = New System.Drawing.Point(186, 272)
        Me.command1.Name = "command1"
        Me.command1.Size = New System.Drawing.Size(75, 23)
        Me.command1.TabIndex = 2
        Me.command1.Text = "Start"
        Me.command1.UseVisualStyleBackColor = True
        '
        'command2
        '
        Me.command2.Location = New System.Drawing.Point(276, 272)
        Me.command2.Name = "command2"
        Me.command2.Size = New System.Drawing.Size(75, 23)
        Me.command2.TabIndex = 3
        Me.command2.Text = "Exit"
        Me.command2.UseVisualStyleBackColor = True
        '
        'frmConnect
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.Black
        Me.ClientSize = New System.Drawing.Size(501, 311)
        Me.Controls.Add(Me.command2)
        Me.Controls.Add(Me.command1)
        Me.Controls.Add(Me.port)
        Me.Controls.Add(Me.limit)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.lbltitle)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Location = New System.Drawing.Point(0, -4)
        Me.Name = "frmConnect"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Dark Wind: Return of the Card Masters"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents command1 As System.Windows.Forms.Button
    Friend WithEvents command2 As System.Windows.Forms.Button
#End Region 
End Class