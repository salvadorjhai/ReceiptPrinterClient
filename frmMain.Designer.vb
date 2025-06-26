<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmMain
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.txtIPAddress = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.chkAutoStart = New System.Windows.Forms.CheckBox()
        Me.btnFindFreePort = New System.Windows.Forms.Button()
        Me.cboPrinters = New System.Windows.Forms.ComboBox()
        Me.txtPort = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.btnStartStop = New System.Windows.Forms.Button()
        Me.lblHost = New System.Windows.Forms.LinkLabel()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.NotifyIcon1 = New System.Windows.Forms.NotifyIcon(Me.components)
        Me.TabControl1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.SuspendLayout()
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Location = New System.Drawing.Point(8, 8)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(332, 172)
        Me.TabControl1.TabIndex = 0
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.txtIPAddress)
        Me.TabPage1.Controls.Add(Me.Label1)
        Me.TabPage1.Controls.Add(Me.chkAutoStart)
        Me.TabPage1.Controls.Add(Me.btnFindFreePort)
        Me.TabPage1.Controls.Add(Me.cboPrinters)
        Me.TabPage1.Controls.Add(Me.txtPort)
        Me.TabPage1.Controls.Add(Me.Label4)
        Me.TabPage1.Controls.Add(Me.Label3)
        Me.TabPage1.Location = New System.Drawing.Point(4, 22)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(324, 146)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "General"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'txtIPAddress
        '
        Me.txtIPAddress.Location = New System.Drawing.Point(96, 40)
        Me.txtIPAddress.Name = "txtIPAddress"
        Me.txtIPAddress.Size = New System.Drawing.Size(212, 21)
        Me.txtIPAddress.TabIndex = 8
        Me.txtIPAddress.Text = "192.168.3.246"
        Me.ToolTip1.SetToolTip(Me.txtIPAddress, "Replace with your local ip address")
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(16, 44)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(63, 13)
        Me.Label1.TabIndex = 7
        Me.Label1.Text = "IP Address:"
        '
        'chkAutoStart
        '
        Me.chkAutoStart.AutoSize = True
        Me.chkAutoStart.Location = New System.Drawing.Point(96, 96)
        Me.chkAutoStart.Name = "chkAutoStart"
        Me.chkAutoStart.Size = New System.Drawing.Size(75, 17)
        Me.chkAutoStart.TabIndex = 6
        Me.chkAutoStart.Text = "Auto start"
        Me.ToolTip1.SetToolTip(Me.chkAutoStart, "Auto start when application is started.")
        Me.chkAutoStart.UseVisualStyleBackColor = True
        '
        'btnFindFreePort
        '
        Me.btnFindFreePort.Location = New System.Drawing.Point(200, 68)
        Me.btnFindFreePort.Name = "btnFindFreePort"
        Me.btnFindFreePort.Size = New System.Drawing.Size(108, 23)
        Me.btnFindFreePort.TabIndex = 5
        Me.btnFindFreePort.Text = "Find Free Port"
        Me.ToolTip1.SetToolTip(Me.btnFindFreePort, "find open/free port")
        Me.btnFindFreePort.UseVisualStyleBackColor = True
        '
        'cboPrinters
        '
        Me.cboPrinters.FormattingEnabled = True
        Me.cboPrinters.Location = New System.Drawing.Point(96, 12)
        Me.cboPrinters.Name = "cboPrinters"
        Me.cboPrinters.Size = New System.Drawing.Size(212, 21)
        Me.cboPrinters.TabIndex = 4
        Me.ToolTip1.SetToolTip(Me.cboPrinters, "Select receipt printer (thermal printer)")
        '
        'txtPort
        '
        Me.txtPort.Location = New System.Drawing.Point(96, 68)
        Me.txtPort.Name = "txtPort"
        Me.txtPort.Size = New System.Drawing.Size(100, 21)
        Me.txtPort.TabIndex = 3
        Me.ToolTip1.SetToolTip(Me.txtPort, "Set listening port")
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(16, 72)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(76, 13)
        Me.Label4.TabIndex = 2
        Me.Label4.Text = "Listening Port:"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(16, 16)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(43, 13)
        Me.Label3.TabIndex = 1
        Me.Label3.Text = "Printer:"
        '
        'btnStartStop
        '
        Me.btnStartStop.Location = New System.Drawing.Point(224, 184)
        Me.btnStartStop.Name = "btnStartStop"
        Me.btnStartStop.Size = New System.Drawing.Size(108, 23)
        Me.btnStartStop.TabIndex = 6
        Me.btnStartStop.Text = "Start"
        Me.ToolTip1.SetToolTip(Me.btnStartStop, "Start/Stop receipt printer client")
        Me.btnStartStop.UseVisualStyleBackColor = True
        '
        'lblHost
        '
        Me.lblHost.AutoSize = True
        Me.lblHost.Location = New System.Drawing.Point(12, 188)
        Me.lblHost.Name = "lblHost"
        Me.lblHost.Size = New System.Drawing.Size(25, 13)
        Me.lblHost.TabIndex = 7
        Me.lblHost.TabStop = True
        Me.lblHost.Text = "Idle"
        Me.ToolTip1.SetToolTip(Me.lblHost, "Click to copy")
        '
        'NotifyIcon1
        '
        Me.NotifyIcon1.Text = "NotifyIcon1"
        Me.NotifyIcon1.Visible = True
        '
        'frmMain
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(350, 221)
        Me.Controls.Add(Me.lblHost)
        Me.Controls.Add(Me.btnStartStop)
        Me.Controls.Add(Me.TabControl1)
        Me.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Name = "frmMain"
        Me.Text = "***"
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.TabPage1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents TabControl1 As TabControl
    Friend WithEvents TabPage1 As TabPage
    Friend WithEvents cboPrinters As ComboBox
    Friend WithEvents txtPort As TextBox
    Friend WithEvents Label4 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents btnFindFreePort As Button
    Friend WithEvents btnStartStop As Button
    Friend WithEvents lblHost As LinkLabel
    Friend WithEvents ToolTip1 As ToolTip
    Friend WithEvents chkAutoStart As CheckBox
    Friend WithEvents txtIPAddress As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents NotifyIcon1 As NotifyIcon
End Class
