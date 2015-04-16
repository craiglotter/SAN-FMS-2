<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Main_Screen
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
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
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Main_Screen))
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip
        Me.HelpToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem
        Me.AboutToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.AboutToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem
        Me.AutoUpdateToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.Button1 = New System.Windows.Forms.Button
        Me.BackgroundWorker1 = New System.ComponentModel.BackgroundWorker
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.ActivityLogLink = New System.Windows.Forms.LinkLabel
        Me.ErrorLogLink = New System.Windows.Forms.LinkLabel
        Me.TimerLabel = New System.Windows.Forms.Label
        Me.Label5 = New System.Windows.Forms.Label
        Me.Label6 = New System.Windows.Forms.Label
        Me.StatusLabel = New System.Windows.Forms.Label
        Me.NotifyIcon1 = New System.Windows.Forms.NotifyIcon(Me.components)
        Me.Label4 = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label7 = New System.Windows.Forms.Label
        Me.SAN_Folder = New System.Windows.Forms.Label
        Me.Button2 = New System.Windows.Forms.Button
        Me.SAN_Database = New System.Windows.Forms.Label
        Me.SAN_FileZilla = New System.Windows.Forms.Label
        Me.SAN_XML = New System.Windows.Forms.Label
        Me.Button3 = New System.Windows.Forms.Button
        Me.Button4 = New System.Windows.Forms.Button
        Me.Button5 = New System.Windows.Forms.Button
        Me.ProgressBar1 = New System.Windows.Forms.ProgressBar
        Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog
        Me.OpenFileDialog2 = New System.Windows.Forms.OpenFileDialog
        Me.OpenFileDialog3 = New System.Windows.Forms.OpenFileDialog
        Me.FolderBrowserDialog1 = New System.Windows.Forms.FolderBrowserDialog
        Me.Button_GenerateATempXXXAccounts = New System.Windows.Forms.Button
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.GroupBox2 = New System.Windows.Forms.GroupBox
        Me.Button_RemoveATempXXXAccounts = New System.Windows.Forms.Button
        Me.GroupBox3 = New System.Windows.Forms.GroupBox
        Me.BackgroundWorker2 = New System.ComponentModel.BackgroundWorker
        Me.BackgroundWorker3 = New System.ComponentModel.BackgroundWorker
        Me.Label8 = New System.Windows.Forms.Label
        Me.Label9 = New System.Windows.Forms.Label
        Me.TempTimerLabel = New System.Windows.Forms.Label
        Me.MenuStrip1.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.SuspendLayout()
        '
        'MenuStrip1
        '
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.HelpToolStripMenuItem1, Me.AboutToolStripMenuItem})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(448, 24)
        Me.MenuStrip1.TabIndex = 4
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'HelpToolStripMenuItem1
        '
        Me.HelpToolStripMenuItem1.Name = "HelpToolStripMenuItem1"
        Me.HelpToolStripMenuItem1.Size = New System.Drawing.Size(40, 20)
        Me.HelpToolStripMenuItem1.Text = "Help"
        '
        'AboutToolStripMenuItem
        '
        Me.AboutToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.AboutToolStripMenuItem1, Me.AutoUpdateToolStripMenuItem})
        Me.AboutToolStripMenuItem.Name = "AboutToolStripMenuItem"
        Me.AboutToolStripMenuItem.Size = New System.Drawing.Size(48, 20)
        Me.AboutToolStripMenuItem.Text = "About"
        '
        'AboutToolStripMenuItem1
        '
        Me.AboutToolStripMenuItem1.Name = "AboutToolStripMenuItem1"
        Me.AboutToolStripMenuItem1.Size = New System.Drawing.Size(143, 22)
        Me.AboutToolStripMenuItem1.Text = "About"
        '
        'AutoUpdateToolStripMenuItem
        '
        Me.AutoUpdateToolStripMenuItem.Name = "AutoUpdateToolStripMenuItem"
        Me.AutoUpdateToolStripMenuItem.Size = New System.Drawing.Size(143, 22)
        Me.AutoUpdateToolStripMenuItem.Text = "AutoUpdate"
        '
        'Button1
        '
        Me.Button1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Button1.Location = New System.Drawing.Point(13, 77)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(108, 23)
        Me.Button1.TabIndex = 5
        Me.Button1.Text = "Stop Timer"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'BackgroundWorker1
        '
        '
        'Timer1
        '
        Me.Timer1.Enabled = True
        Me.Timer1.Interval = 1000
        '
        'Panel1
        '
        Me.Panel1.BackgroundImage = Global.SAN_FMS_2.My.Resources.Resources.Form_Banner
        Me.Panel1.Location = New System.Drawing.Point(0, 24)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(449, 69)
        Me.Panel1.TabIndex = 11
        '
        'ActivityLogLink
        '
        Me.ActivityLogLink.AutoSize = True
        Me.ActivityLogLink.ForeColor = System.Drawing.SystemColors.ControlText
        Me.ActivityLogLink.Location = New System.Drawing.Point(127, 82)
        Me.ActivityLogLink.Name = "ActivityLogLink"
        Me.ActivityLogLink.Size = New System.Drawing.Size(67, 13)
        Me.ActivityLogLink.TabIndex = 16
        Me.ActivityLogLink.TabStop = True
        Me.ActivityLogLink.Text = "Activity Logs"
        '
        'ErrorLogLink
        '
        Me.ErrorLogLink.AutoSize = True
        Me.ErrorLogLink.ForeColor = System.Drawing.SystemColors.ControlText
        Me.ErrorLogLink.Location = New System.Drawing.Point(200, 82)
        Me.ErrorLogLink.Name = "ErrorLogLink"
        Me.ErrorLogLink.Size = New System.Drawing.Size(55, 13)
        Me.ErrorLogLink.TabIndex = 17
        Me.ErrorLogLink.TabStop = True
        Me.ErrorLogLink.Text = "Error Logs"
        '
        'TimerLabel
        '
        Me.TimerLabel.AutoSize = True
        Me.TimerLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TimerLabel.ForeColor = System.Drawing.SystemColors.ControlText
        Me.TimerLabel.Location = New System.Drawing.Point(99, 16)
        Me.TimerLabel.Margin = New System.Windows.Forms.Padding(0)
        Me.TimerLabel.Name = "TimerLabel"
        Me.TimerLabel.Size = New System.Drawing.Size(64, 25)
        Me.TimerLabel.TabIndex = 18
        Me.TimerLabel.Text = "0900"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label5.Location = New System.Drawing.Point(16, 25)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(68, 13)
        Me.Label5.TabIndex = 19
        Me.Label5.Text = "Next Sweep:"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label6.Location = New System.Drawing.Point(188, 25)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(70, 13)
        Me.Label6.TabIndex = 20
        Me.Label6.Text = "Force Sweep"
        '
        'StatusLabel
        '
        Me.StatusLabel.AutoEllipsis = True
        Me.StatusLabel.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.StatusLabel.ForeColor = System.Drawing.SystemColors.ControlDark
        Me.StatusLabel.Location = New System.Drawing.Point(0, 455)
        Me.StatusLabel.Name = "StatusLabel"
        Me.StatusLabel.Padding = New System.Windows.Forms.Padding(0, 0, 10, 0)
        Me.StatusLabel.Size = New System.Drawing.Size(448, 23)
        Me.StatusLabel.TabIndex = 21
        Me.StatusLabel.Text = "Application Loaded"
        Me.StatusLabel.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'NotifyIcon1
        '
        Me.NotifyIcon1.BalloonTipIcon = System.Windows.Forms.ToolTipIcon.Info
        Me.NotifyIcon1.Icon = CType(resources.GetObject("NotifyIcon1.Icon"), System.Drawing.Icon)
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label4.Location = New System.Drawing.Point(156, 25)
        Me.Label4.Margin = New System.Windows.Forms.Padding(0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(29, 13)
        Me.Label4.TabIndex = 22
        Me.Label4.Text = "secs"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label1.Location = New System.Drawing.Point(16, 25)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(91, 13)
        Me.Label1.TabIndex = 23
        Me.Label1.Text = "SAN Base Folder:"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label2.Location = New System.Drawing.Point(16, 48)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(81, 13)
        Me.Label2.TabIndex = 24
        Me.Label2.Text = "SAN Database:"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label3.Location = New System.Drawing.Point(16, 72)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(101, 13)
        Me.Label3.TabIndex = 25
        Me.Label3.Text = "FileZilla Executable:"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label7.Location = New System.Drawing.Point(16, 96)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(95, 13)
        Me.Label7.TabIndex = 26
        Me.Label7.Text = "FileZilla User XML:"
        '
        'SAN_Folder
        '
        Me.SAN_Folder.AutoEllipsis = True
        Me.SAN_Folder.ForeColor = System.Drawing.Color.SaddleBrown
        Me.SAN_Folder.Location = New System.Drawing.Point(113, 25)
        Me.SAN_Folder.Name = "SAN_Folder"
        Me.SAN_Folder.Size = New System.Drawing.Size(232, 13)
        Me.SAN_Folder.TabIndex = 27
        '
        'Button2
        '
        Me.Button2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Button2.Location = New System.Drawing.Point(351, 21)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(55, 20)
        Me.Button2.TabIndex = 28
        Me.Button2.Text = "Browse"
        Me.Button2.UseVisualStyleBackColor = True
        '
        'SAN_Database
        '
        Me.SAN_Database.AutoEllipsis = True
        Me.SAN_Database.ForeColor = System.Drawing.Color.SaddleBrown
        Me.SAN_Database.Location = New System.Drawing.Point(113, 48)
        Me.SAN_Database.Name = "SAN_Database"
        Me.SAN_Database.Size = New System.Drawing.Size(232, 13)
        Me.SAN_Database.TabIndex = 29
        '
        'SAN_FileZilla
        '
        Me.SAN_FileZilla.AutoEllipsis = True
        Me.SAN_FileZilla.ForeColor = System.Drawing.Color.SaddleBrown
        Me.SAN_FileZilla.Location = New System.Drawing.Point(113, 72)
        Me.SAN_FileZilla.Name = "SAN_FileZilla"
        Me.SAN_FileZilla.Size = New System.Drawing.Size(232, 13)
        Me.SAN_FileZilla.TabIndex = 30
        '
        'SAN_XML
        '
        Me.SAN_XML.AutoEllipsis = True
        Me.SAN_XML.ForeColor = System.Drawing.Color.SaddleBrown
        Me.SAN_XML.Location = New System.Drawing.Point(113, 96)
        Me.SAN_XML.Name = "SAN_XML"
        Me.SAN_XML.Size = New System.Drawing.Size(232, 13)
        Me.SAN_XML.TabIndex = 31
        '
        'Button3
        '
        Me.Button3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Button3.Location = New System.Drawing.Point(351, 44)
        Me.Button3.Name = "Button3"
        Me.Button3.Size = New System.Drawing.Size(55, 20)
        Me.Button3.TabIndex = 32
        Me.Button3.Text = "Browse"
        Me.Button3.UseVisualStyleBackColor = True
        '
        'Button4
        '
        Me.Button4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Button4.Location = New System.Drawing.Point(351, 68)
        Me.Button4.Name = "Button4"
        Me.Button4.Size = New System.Drawing.Size(55, 20)
        Me.Button4.TabIndex = 33
        Me.Button4.Text = "Browse"
        Me.Button4.UseVisualStyleBackColor = True
        '
        'Button5
        '
        Me.Button5.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Button5.Location = New System.Drawing.Point(351, 92)
        Me.Button5.Name = "Button5"
        Me.Button5.Size = New System.Drawing.Size(55, 20)
        Me.Button5.TabIndex = 34
        Me.Button5.Text = "Browse"
        Me.Button5.UseVisualStyleBackColor = True
        '
        'ProgressBar1
        '
        Me.ProgressBar1.Enabled = False
        Me.ProgressBar1.Location = New System.Drawing.Point(261, 77)
        Me.ProgressBar1.Name = "ProgressBar1"
        Me.ProgressBar1.Size = New System.Drawing.Size(143, 23)
        Me.ProgressBar1.TabIndex = 35
        '
        'OpenFileDialog1
        '
        Me.OpenFileDialog1.DefaultExt = "mdb"
        Me.OpenFileDialog1.Filter = "MSAccess Database|*.mdb|All files|*.*"
        Me.OpenFileDialog1.Title = "Please select the SAN Database to interrogate"
        '
        'OpenFileDialog2
        '
        Me.OpenFileDialog2.DefaultExt = "exe"
        Me.OpenFileDialog2.Filter = "FileZilla executable|*.exe|All files|*.*"
        Me.OpenFileDialog2.Title = "Please select the relevant FileZilla executable to affect"
        '
        'OpenFileDialog3
        '
        Me.OpenFileDialog3.DefaultExt = "xml"
        Me.OpenFileDialog3.Filter = "Config XML file|*.xml|All files|*.xml"
        Me.OpenFileDialog3.Title = "Please select the FileZilla Config XML to replace"
        '
        'FolderBrowserDialog1
        '
        Me.FolderBrowserDialog1.Description = "Please select the SAN base folder to parse"
        Me.FolderBrowserDialog1.ShowNewFolderButton = False
        '
        'Button_GenerateATempXXXAccounts
        '
        Me.Button_GenerateATempXXXAccounts.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Button_GenerateATempXXXAccounts.Location = New System.Drawing.Point(38, 19)
        Me.Button_GenerateATempXXXAccounts.Name = "Button_GenerateATempXXXAccounts"
        Me.Button_GenerateATempXXXAccounts.Size = New System.Drawing.Size(172, 23)
        Me.Button_GenerateATempXXXAccounts.TabIndex = 36
        Me.Button_GenerateATempXXXAccounts.Text = "Generate A-TEMPXXX Accounts"
        Me.Button_GenerateATempXXXAccounts.UseVisualStyleBackColor = True
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.Label9)
        Me.GroupBox1.Controls.Add(Me.TempTimerLabel)
        Me.GroupBox1.Controls.Add(Me.Label8)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.Button1)
        Me.GroupBox1.Controls.Add(Me.ActivityLogLink)
        Me.GroupBox1.Controls.Add(Me.ProgressBar1)
        Me.GroupBox1.Controls.Add(Me.ErrorLogLink)
        Me.GroupBox1.Controls.Add(Me.Label5)
        Me.GroupBox1.Controls.Add(Me.Label6)
        Me.GroupBox1.Controls.Add(Me.TimerLabel)
        Me.GroupBox1.ForeColor = System.Drawing.Color.DimGray
        Me.GroupBox1.Location = New System.Drawing.Point(12, 324)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(424, 117)
        Me.GroupBox1.TabIndex = 37
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Controllers"
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.Button_RemoveATempXXXAccounts)
        Me.GroupBox2.Controls.Add(Me.Button_GenerateATempXXXAccounts)
        Me.GroupBox2.ForeColor = System.Drawing.Color.DimGray
        Me.GroupBox2.Location = New System.Drawing.Point(12, 251)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(424, 56)
        Me.GroupBox2.TabIndex = 38
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Extra Functionality"
        '
        'Button_RemoveATempXXXAccounts
        '
        Me.Button_RemoveATempXXXAccounts.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Button_RemoveATempXXXAccounts.Location = New System.Drawing.Point(216, 19)
        Me.Button_RemoveATempXXXAccounts.Name = "Button_RemoveATempXXXAccounts"
        Me.Button_RemoveATempXXXAccounts.Size = New System.Drawing.Size(172, 23)
        Me.Button_RemoveATempXXXAccounts.TabIndex = 37
        Me.Button_RemoveATempXXXAccounts.Text = "Remove A-TEMPXXX Accounts"
        Me.Button_RemoveATempXXXAccounts.UseVisualStyleBackColor = True
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.Label1)
        Me.GroupBox3.Controls.Add(Me.Label2)
        Me.GroupBox3.Controls.Add(Me.Label3)
        Me.GroupBox3.Controls.Add(Me.Button5)
        Me.GroupBox3.Controls.Add(Me.Label7)
        Me.GroupBox3.Controls.Add(Me.Button4)
        Me.GroupBox3.Controls.Add(Me.SAN_Folder)
        Me.GroupBox3.Controls.Add(Me.Button3)
        Me.GroupBox3.Controls.Add(Me.Button2)
        Me.GroupBox3.Controls.Add(Me.SAN_XML)
        Me.GroupBox3.Controls.Add(Me.SAN_Database)
        Me.GroupBox3.Controls.Add(Me.SAN_FileZilla)
        Me.GroupBox3.ForeColor = System.Drawing.Color.DimGray
        Me.GroupBox3.Location = New System.Drawing.Point(12, 109)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(424, 127)
        Me.GroupBox3.TabIndex = 39
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "Environmental Control Settings"
        '
        'BackgroundWorker2
        '
        '
        'BackgroundWorker3
        '
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label8.Location = New System.Drawing.Point(16, 51)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(141, 13)
        Me.Label8.TabIndex = 36
        Me.Label8.Text = "Next Temp Account Sweep:"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label9.Location = New System.Drawing.Point(217, 51)
        Me.Label9.Margin = New System.Windows.Forms.Padding(0)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(29, 13)
        Me.Label9.TabIndex = 38
        Me.Label9.Text = "secs"
        '
        'TempTimerLabel
        '
        Me.TempTimerLabel.AutoSize = True
        Me.TempTimerLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TempTimerLabel.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.TempTimerLabel.Location = New System.Drawing.Point(160, 42)
        Me.TempTimerLabel.Margin = New System.Windows.Forms.Padding(0)
        Me.TempTimerLabel.Name = "TempTimerLabel"
        Me.TempTimerLabel.Size = New System.Drawing.Size(64, 25)
        Me.TempTimerLabel.TabIndex = 37
        Me.TempTimerLabel.Text = "0180"
        '
        'Main_Screen
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(448, 478)
        Me.Controls.Add(Me.GroupBox3)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.StatusLabel)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.MenuStrip1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.Name = "Main_Screen"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Form1"
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents MenuStrip1 As System.Windows.Forms.MenuStrip
    Friend WithEvents HelpToolStripMenuItem1 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents AboutToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents AboutToolStripMenuItem1 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents AutoUpdateToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents BackgroundWorker1 As System.ComponentModel.BackgroundWorker
    Friend WithEvents Timer1 As System.Windows.Forms.Timer
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents ActivityLogLink As System.Windows.Forms.LinkLabel
    Friend WithEvents ErrorLogLink As System.Windows.Forms.LinkLabel
    Friend WithEvents TimerLabel As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents StatusLabel As System.Windows.Forms.Label
    Friend WithEvents NotifyIcon1 As System.Windows.Forms.NotifyIcon
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents SAN_Folder As System.Windows.Forms.Label
    Friend WithEvents Button2 As System.Windows.Forms.Button
    Friend WithEvents SAN_Database As System.Windows.Forms.Label
    Friend WithEvents SAN_FileZilla As System.Windows.Forms.Label
    Friend WithEvents SAN_XML As System.Windows.Forms.Label
    Friend WithEvents Button3 As System.Windows.Forms.Button
    Friend WithEvents Button4 As System.Windows.Forms.Button
    Friend WithEvents Button5 As System.Windows.Forms.Button
    Friend WithEvents ProgressBar1 As System.Windows.Forms.ProgressBar
    Friend WithEvents OpenFileDialog1 As System.Windows.Forms.OpenFileDialog
    Friend WithEvents OpenFileDialog2 As System.Windows.Forms.OpenFileDialog
    Friend WithEvents OpenFileDialog3 As System.Windows.Forms.OpenFileDialog
    Friend WithEvents FolderBrowserDialog1 As System.Windows.Forms.FolderBrowserDialog
    Friend WithEvents Button_GenerateATempXXXAccounts As System.Windows.Forms.Button
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents BackgroundWorker2 As System.ComponentModel.BackgroundWorker
    Friend WithEvents Button_RemoveATempXXXAccounts As System.Windows.Forms.Button
    Friend WithEvents BackgroundWorker3 As System.ComponentModel.BackgroundWorker
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents TempTimerLabel As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label

End Class
