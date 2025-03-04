<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmImport
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
		Me.txtSQL = New System.Windows.Forms.TextBox
		Me.MenuStrip1 = New System.Windows.Forms.MenuStrip
		Me.ImportToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
		Me.EXPLevelsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
		Me.PlayerEXPToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
		Me.PlayerLevelsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
		Me.PlayerAAsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
		Me.PlayerAAEXPAndPointsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
		Me.MenuStrip1.SuspendLayout()
		Me.SuspendLayout()
		'
		'txtSQL
		'
		Me.txtSQL.Location = New System.Drawing.Point(94, 57)
		Me.txtSQL.Multiline = True
		Me.txtSQL.Name = "txtSQL"
		Me.txtSQL.Size = New System.Drawing.Size(518, 227)
		Me.txtSQL.TabIndex = 0
		'
		'MenuStrip1
		'
		Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ImportToolStripMenuItem})
		Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
		Me.MenuStrip1.Name = "MenuStrip1"
		Me.MenuStrip1.Size = New System.Drawing.Size(703, 24)
		Me.MenuStrip1.TabIndex = 1
		Me.MenuStrip1.Text = "MenuStrip1"
		'
		'ImportToolStripMenuItem
		'
		Me.ImportToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.EXPLevelsToolStripMenuItem, Me.PlayerEXPToolStripMenuItem, Me.PlayerLevelsToolStripMenuItem, Me.PlayerAAsToolStripMenuItem, Me.PlayerAAEXPAndPointsToolStripMenuItem})
		Me.ImportToolStripMenuItem.Name = "ImportToolStripMenuItem"
		Me.ImportToolStripMenuItem.Size = New System.Drawing.Size(51, 20)
		Me.ImportToolStripMenuItem.Text = "Import"
		'
		'EXPLevelsToolStripMenuItem
		'
		Me.EXPLevelsToolStripMenuItem.Name = "EXPLevelsToolStripMenuItem"
		Me.EXPLevelsToolStripMenuItem.Size = New System.Drawing.Size(152, 22)
		Me.EXPLevelsToolStripMenuItem.Text = "EXP Levels"
		'
		'PlayerEXPToolStripMenuItem
		'
		Me.PlayerEXPToolStripMenuItem.Name = "PlayerEXPToolStripMenuItem"
		Me.PlayerEXPToolStripMenuItem.Size = New System.Drawing.Size(152, 22)
		Me.PlayerEXPToolStripMenuItem.Text = "Player EXP"
		'
		'PlayerLevelsToolStripMenuItem
		'
		Me.PlayerLevelsToolStripMenuItem.Name = "PlayerLevelsToolStripMenuItem"
		Me.PlayerLevelsToolStripMenuItem.Size = New System.Drawing.Size(152, 22)
		Me.PlayerLevelsToolStripMenuItem.Text = "Player Levels"
		'
		'PlayerAAsToolStripMenuItem
		'
		Me.PlayerAAsToolStripMenuItem.Name = "PlayerAAsToolStripMenuItem"
		Me.PlayerAAsToolStripMenuItem.Size = New System.Drawing.Size(152, 22)
		Me.PlayerAAsToolStripMenuItem.Text = "Player AAs"
		'
		'PlayerAAEXPAndPointsToolStripMenuItem
		'
		Me.PlayerAAEXPAndPointsToolStripMenuItem.Name = "PlayerAAEXPAndPointsToolStripMenuItem"
		Me.PlayerAAEXPAndPointsToolStripMenuItem.Size = New System.Drawing.Size(206, 22)
		Me.PlayerAAEXPAndPointsToolStripMenuItem.Text = "Player AA EXP and Points"
		'
		'frmImport
		'
		Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.ClientSize = New System.Drawing.Size(703, 348)
		Me.Controls.Add(Me.txtSQL)
		Me.Controls.Add(Me.MenuStrip1)
		Me.MainMenuStrip = Me.MenuStrip1
		Me.Name = "frmImport"
		Me.Text = "Import Wizard"
		Me.MenuStrip1.ResumeLayout(False)
		Me.MenuStrip1.PerformLayout()
		Me.ResumeLayout(False)
		Me.PerformLayout()

	End Sub
    Friend WithEvents txtSQL As System.Windows.Forms.TextBox
    Friend WithEvents MenuStrip1 As System.Windows.Forms.MenuStrip
    Friend WithEvents ImportToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents EXPLevelsToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents PlayerEXPToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	Friend WithEvents PlayerLevelsToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	Friend WithEvents PlayerAAsToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	Friend WithEvents PlayerAAEXPAndPointsToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
End Class
