VERSION 5.00
Begin VB.Form frmimport 
   BorderStyle     =   1  'Fixed Single
   Caption         =   "Card Import"
   ClientHeight    =   10905
   ClientLeft      =   45
   ClientTop       =   330
   ClientWidth     =   8340
   LinkTopic       =   "Form1"
   MaxButton       =   0   'False
   MinButton       =   0   'False
   ScaleHeight     =   10905
   ScaleWidth      =   8340
   StartUpPosition =   3  'Windows Default
   Begin VB.Frame Frame1 
      Caption         =   "Import List"
      Height          =   2655
      Left            =   4800
      TabIndex        =   4
      Top             =   6240
      Width           =   3255
      Begin VB.CommandButton cmdSQL 
         Caption         =   "Draw SQL"
         Height          =   375
         Left            =   1080
         TabIndex        =   8
         Top             =   2040
         Width           =   1935
      End
      Begin VB.OptionButton optReferal 
         Caption         =   "Referal"
         Height          =   255
         Left            =   240
         TabIndex        =   7
         Top             =   1080
         Width           =   2775
      End
      Begin VB.OptionButton optUnverified 
         Caption         =   "Unverified People"
         Height          =   255
         Left            =   240
         TabIndex        =   6
         Top             =   720
         Width           =   2775
      End
      Begin VB.OptionButton optSignupDate 
         Caption         =   "Signup Date"
         Height          =   255
         Left            =   240
         TabIndex        =   5
         Top             =   360
         Width           =   2775
      End
   End
   Begin VB.CommandButton CmdOne 
      Caption         =   "Import One"
      Height          =   405
      Left            =   5940
      TabIndex        =   3
      Top             =   5145
      Width           =   2055
   End
   Begin VB.FileListBox filelist 
      Height          =   5160
      Left            =   330
      TabIndex        =   2
      Top             =   5115
      Width           =   4185
   End
   Begin VB.Data Data1 
      Caption         =   "Data1"
      Connect         =   "Access 2000;"
      DatabaseName    =   ""
      DefaultCursorType=   0  'DefaultCursor
      DefaultType     =   2  'UseODBC
      Exclusive       =   0   'False
      Height          =   390
      Left            =   5160
      Options         =   0
      ReadOnly        =   0   'False
      RecordsetType   =   1  'Dynaset
      RecordSource    =   ""
      Top             =   10320
      Visible         =   0   'False
      Width           =   2925
   End
   Begin VB.CommandButton cmdAll 
      Caption         =   "Import All"
      Height          =   405
      Left            =   5925
      TabIndex        =   1
      Top             =   5625
      Width           =   2055
   End
   Begin VB.TextBox txtimport 
      Height          =   4830
      Left            =   330
      Locked          =   -1  'True
      MultiLine       =   -1  'True
      ScrollBars      =   2  'Vertical
      TabIndex        =   0
      Top             =   195
      Width           =   7620
   End
End
Attribute VB_Name = "frmimport"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
Private Sub cmdAll_Click()
Dim strNick As String, x As Integer, rdata As String, cardnumber As String

txtimport.Text = ""

filelist.Path = App.Path & "\accounts\"
filelist.Pattern = "*.ttd"

Data1.DatabaseName = App.Path & "\darkwind_cards.mdb"

For x = 0 To filelist.ListCount - 1
    Divide filelist.List(x), ".", strNick
    
    Open App.Path & "\accounts\" & filelist.List(x) For Input As #300
        Do Until rdata = "[cards]" Or EOF(300)
            Line Input #300, rdata
        Loop
        
        If EOF(300) = True Then GoTo skip
        
        Line Input #300, rdata
        
        Do Until InStr(1, rdata, "[") > 0 Or EOF(300)
            
            Divide rdata, "=", rdata, cardnumber
            
            If cardnumber <> "0" Then
                Data1.RecordSource = "SELECT * FROM playercards"
                Data1.Refresh
                Data1.Recordset.AddNew
                Data1.Recordset.Fields("player") = strNick
                Data1.Recordset.Fields("cardname") = rdata
                Data1.Recordset.Fields("value") = Val(cardnumber)
                Data1.Recordset.Update
                Data1.UpdateRecord
            End If
            
            Line Input #300, rdata
        Loop
         
skip:
        txtimport.Text = txtimport.Text & strNick & " imported..." & vbCrLf
    Close #300
Next x

Data1.Recordset.Close

End Sub

Private Sub CmdOne_Click()
Dim strNick As String, x As Integer, rdata As String, cardnumber As String

txtimport.Text = ""

Data1.DatabaseName = App.Path & "\darkwind_cards.mdb"

Divide filelist.List(filelist.ListIndex), ".", strNick
    
    Open App.Path & "\accounts\" & filelist.List(filelist.ListIndex) For Input As #300
        Do Until rdata = "[cards]" Or EOF(300)
            Line Input #300, rdata
        Loop
        
        If EOF(300) = True Then GoTo skip
        
        Line Input #300, rdata
        
        Do Until InStr(1, rdata, "[") > 0 Or EOF(300)
            
            Divide rdata, "=", rdata, cardnumber
            
            If cardnumber <> "0" Then
                Data1.RecordSource = "SELECT * FROM playercards"
                Data1.Refresh
                Data1.Recordset.AddNew
                Data1.Recordset.Fields("player") = strNick
                Data1.Recordset.Fields("cardname") = rdata
                Data1.Recordset.Fields("value") = Val(cardnumber)
                Data1.Recordset.Update
                Data1.UpdateRecord
            End If
            
            Line Input #300, rdata
        Loop
         
skip:
        txtimport.Text = txtimport.Text & strNick & " imported..." & vbCrLf
    Close #300

Data1.Recordset.Close
End Sub

Private Sub cmdSQL_Click()
Dim x As Integer, strNick As String, sValue As String, freefile As Integer

txtimport.Text = ""

If optSignupDate.Value = True Then
    
    filelist.Path = App.Path & "\accounts\"
    filelist.Pattern = "*.ttd"
    
    For x = 0 To filelist.ListCount - 1
        Divide filelist.List(x), ".", strNick
        
        strNick = Trim$(strNick)
        
        If Readini("player", "signup_date", filelist.Path & "\" & filelist.List(x)) <> "" Then
            sValue = Format(CDate(Readini("player", "signup_date", filelist.Path & "\" & filelist.List(x))), "mm-dd-yyyy")
        Else
            sValue = "01-01-2000"
        End If
        
        If strNick <> "" Then
        
            Open "c:\signupdate.sql" For Append As #1
                Print #1, "UPDATE accounts SET signupdate = '" & sValue & "' WHERE player = '" & strNick & "';" & vbCrLf
            Close #1
        End If
    Next x
ElseIf optReferal.Value = True Then

    filelist.Path = App.Path & "\accounts\"
    filelist.Pattern = "*.ttd"
    
    For x = 0 To filelist.ListCount - 1
        Divide filelist.List(x), ".", strNick
        
        strNick = Trim$(strNick)
        
        If Readini("player", "referal", filelist.Path & "\" & filelist.List(x)) <> "" Then
            sValue = Readini("player", "referal", filelist.Path & "\" & filelist.List(x))
        
            If strNick <> "" Then
                Open "c:\referal.sql" For Append As #1
                    Print #1, "UPDATE accounts SET referal = '" & sValue & "' WHERE player = '" & strNick & "';" & vbCrLf
                Close #1
            End If
        ElseIf Readini("player", "referal", filelist.Path & "\" & filelist.List(x)) = strNick Then
            sValue = ""
        Else
            sValue = ""
        End If
    Next x
ElseIf optUnverified.Value = True Then
    Dim sEmail As String, sPass As String
    
    filelist.Path = App.Path & "\accounts\"
    filelist.Pattern = "*.ttd"
    
    For x = 0 To filelist.ListCount - 1
        Divide filelist.List(x), ".", strNick
        
        strNick = Trim$(strNick)
        sEmail = Readini("player", "email", filelist.Path & "\" & filelist.List(x))
        sPass = Readini("player", "password", filelist.Path & "\" & filelist.List(x))
        
        If Readini("player", "Verify", filelist.Path & "\" & filelist.List(x)) = "0" Then
            If execCountQuery("SELECT player FROM accounts WHERE player = '" & strNick & "'") = 0 Then
                Open "c:\unverified.sql" For Append As #1
                    Print #1, "INSERT INTO accounts VALUES ('" & strNick & "', '000', '013', '002', 'male', '0', 1500, 1500, 1500, 1500, 1500, 0, 0, '', '" & sEmail & "', '" & sPass & "', '', 0, '', '', 0, '', 0, '', '', 'off');" & vbCrLf
                Close #1
            End If
        End If
    Next x
End If

txtimport.Text = "Done"
'MsgBox "Done", vbOKOnly + vbInformation, "Import Completed"
End Sub

Private Sub Form_Load()
filelist.Path = App.Path & "\accounts\"
filelist.Pattern = "*.ttd"
End Sub
