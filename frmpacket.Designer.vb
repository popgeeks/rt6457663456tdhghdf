<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class frmpacket
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
	Public WithEvents txtsent As System.Windows.Forms.TextBox
	Public WithEvents txtpacket As System.Windows.Forms.TextBox
	'NOTE: The following procedure is required by the Windows Form Designer
	'It can be modified using the Windows Form Designer.
	'Do not modify it using the code editor.
	<System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
		Me.components = New System.ComponentModel.Container
		Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmpacket))
		Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
		Me.txtsent = New System.Windows.Forms.TextBox
		Me.txtpacket = New System.Windows.Forms.TextBox
		Me.SuspendLayout()
		'
		'txtsent
		'
		Me.txtsent.AcceptsReturn = True
		Me.txtsent.BackColor = System.Drawing.Color.Black
		Me.txtsent.Cursor = System.Windows.Forms.Cursors.IBeam
		Me.txtsent.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.txtsent.ForeColor = System.Drawing.Color.White
		Me.txtsent.Location = New System.Drawing.Point(13, 306)
		Me.txtsent.MaxLength = 0
		Me.txtsent.Multiline = True
		Me.txtsent.Name = "txtsent"
		Me.txtsent.ReadOnly = True
		Me.txtsent.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.txtsent.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
		Me.txtsent.Size = New System.Drawing.Size(664, 273)
		Me.txtsent.TabIndex = 1
		'
		'txtpacket
		'
		Me.txtpacket.AcceptsReturn = True
		Me.txtpacket.BackColor = System.Drawing.Color.Black
		Me.txtpacket.Cursor = System.Windows.Forms.Cursors.IBeam
		Me.txtpacket.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.txtpacket.ForeColor = System.Drawing.Color.White
		Me.txtpacket.Location = New System.Drawing.Point(11, 11)
		Me.txtpacket.MaxLength = 0
		Me.txtpacket.Multiline = True
		Me.txtpacket.Name = "txtpacket"
		Me.txtpacket.ReadOnly = True
		Me.txtpacket.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.txtpacket.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
		Me.txtpacket.Size = New System.Drawing.Size(666, 289)
		Me.txtpacket.TabIndex = 0
		'
		'frmpacket
		'
		Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.BackColor = System.Drawing.Color.Black
		Me.ClientSize = New System.Drawing.Size(689, 591)
		Me.Controls.Add(Me.txtsent)
		Me.Controls.Add(Me.txtpacket)
		Me.Cursor = System.Windows.Forms.Cursors.Default
		Me.Font = New System.Drawing.Font("Verdana", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
		Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
		Me.Location = New System.Drawing.Point(3, 22)
		Me.MaximizeBox = False
		Me.MinimizeBox = False
		Me.Name = "frmpacket"
		Me.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.Text = "Packet Log"
		Me.ResumeLayout(False)

	End Sub
#End Region 
End Class