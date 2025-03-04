<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class frmSystray
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
                '				Form_Terminate_renamed()
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
    Public WithEvents imlimages As System.Windows.Forms.ImageList
    Public WithEvents _Winsock_0 As AxMSWinsockLib.AxWinsock
	Public WithEvents Winsock As AxWinsockArray.AxWinsockArray
    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.
	<System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmSystray))
        Me.imlimages = New System.Windows.Forms.ImageList(Me.components)
        Me._Winsock_0 = New AxMSWinsockLib.AxWinsock()
        Me.Winsock = New AxWinsockArray.AxWinsockArray(Me.components)
        Me.niSystray = New System.Windows.Forms.NotifyIcon(Me.components)
        Me.cmsMenu = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.RestoreToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ShutdownServerToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.WithoutReasonToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.WithReasonToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        CType(Me._Winsock_0, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Winsock, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.cmsMenu.SuspendLayout()
        Me.SuspendLayout()
        '
        'imlimages
        '
        Me.imlimages.ImageStream = CType(resources.GetObject("imlimages.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.imlimages.TransparentColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.imlimages.Images.SetKeyName(0, "hand")
        '
        '_Winsock_0
        '
        Me._Winsock_0.Enabled = True
        Me.Winsock.SetIndex(Me._Winsock_0, CType(0, Short))
        Me._Winsock_0.Location = New System.Drawing.Point(1, 0)
        Me._Winsock_0.Name = "_Winsock_0"
        Me._Winsock_0.OcxState = CType(resources.GetObject("_Winsock_0.OcxState"), System.Windows.Forms.AxHost.State)
        Me._Winsock_0.Size = New System.Drawing.Size(28, 28)
        Me._Winsock_0.TabIndex = 1
        '
        'Winsock
        '
        '
        'niSystray
        '
        Me.niSystray.BalloonTipIcon = System.Windows.Forms.ToolTipIcon.Info
        Me.niSystray.BalloonTipText = "Thanks for Playing Dark Wind: Triple Triad Extreme!"
        Me.niSystray.BalloonTipTitle = "Dark Wind: Triple Triad Extreme"
        Me.niSystray.Text = "Dark Wind: Triple Triad Extreme"
        Me.niSystray.Visible = True
        '
        'cmsMenu
        '
        Me.cmsMenu.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.RestoreToolStripMenuItem, Me.ShutdownServerToolStripMenuItem})
        Me.cmsMenu.Name = "cmsMenu"
        Me.cmsMenu.Size = New System.Drawing.Size(164, 48)
        '
        'RestoreToolStripMenuItem
        '
        Me.RestoreToolStripMenuItem.Name = "RestoreToolStripMenuItem"
        Me.RestoreToolStripMenuItem.Size = New System.Drawing.Size(163, 22)
        Me.RestoreToolStripMenuItem.Text = "Restore"
        '
        'ShutdownServerToolStripMenuItem
        '
        Me.ShutdownServerToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.WithoutReasonToolStripMenuItem, Me.WithReasonToolStripMenuItem})
        Me.ShutdownServerToolStripMenuItem.Name = "ShutdownServerToolStripMenuItem"
        Me.ShutdownServerToolStripMenuItem.Size = New System.Drawing.Size(163, 22)
        Me.ShutdownServerToolStripMenuItem.Text = "Shutdown Server"
        '
        'WithoutReasonToolStripMenuItem
        '
        Me.WithoutReasonToolStripMenuItem.Name = "WithoutReasonToolStripMenuItem"
        Me.WithoutReasonToolStripMenuItem.Size = New System.Drawing.Size(158, 22)
        Me.WithoutReasonToolStripMenuItem.Text = "Without Reason"
        '
        'WithReasonToolStripMenuItem
        '
        Me.WithReasonToolStripMenuItem.Name = "WithReasonToolStripMenuItem"
        Me.WithReasonToolStripMenuItem.Size = New System.Drawing.Size(158, 22)
        Me.WithReasonToolStripMenuItem.Text = "With Reason"
        '
        'frmSystray
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Window
        Me.ClientSize = New System.Drawing.Size(116, 34)
        Me.ControlBox = False
        Me.Controls.Add(Me._Winsock_0)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Location = New System.Drawing.Point(1, 1)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmSystray"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.WindowState = System.Windows.Forms.FormWindowState.Minimized
        CType(Me._Winsock_0, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Winsock, System.ComponentModel.ISupportInitialize).EndInit()
        Me.cmsMenu.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents niSystray As System.Windows.Forms.NotifyIcon
    Friend WithEvents cmsMenu As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents RestoreToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ShutdownServerToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents WithoutReasonToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents WithReasonToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Public WithEvents ToolTip1 As System.Windows.Forms.ToolTip
#End Region 
End Class