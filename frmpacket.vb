Friend Class frmpacket
    Inherits System.Windows.Forms.Form

    Private Sub frmpacket_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        blnlogpackets = False
    End Sub

    Private Sub txtpacket_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtpacket.TextChanged
        If Len(txtpacket.Text) > 12500 Then txtpacket.Text = String.Empty
        txtpacket.SelectionStart = Len(txtpacket.Text)
    End Sub

    Private Sub txtsent_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtsent.TextChanged
        If Len(txtsent.Text) > 12500 Then txtsent.Text = String.Empty
        txtsent.SelectionStart = Len(txtsent.Text)
    End Sub
End Class