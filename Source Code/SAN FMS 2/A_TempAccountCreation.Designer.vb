<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class A_TempAccountCreation
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(A_TempAccountCreation))
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel
        Me.OK_Button = New System.Windows.Forms.Button
        Me.Cancel_Button = New System.Windows.Forms.Button
        Me.Label1 = New System.Windows.Forms.Label
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.a_tempaccountformat = New System.Windows.Forms.TextBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.a_tempaccountstart = New System.Windows.Forms.NumericUpDown
        Me.a_tempaccountend = New System.Windows.Forms.NumericUpDown
        Me.Label5 = New System.Windows.Forms.Label
        Me.a_tempaccountpassword = New System.Windows.Forms.TextBox
        Me.Label6 = New System.Windows.Forms.Label
        Me.TableLayoutPanel1.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        CType(Me.a_tempaccountstart, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.a_tempaccountend, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TableLayoutPanel1.ColumnCount = 2
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.OK_Button, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.Cancel_Button, 1, 0)
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(261, 297)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 1
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(146, 29)
        Me.TableLayoutPanel1.TabIndex = 0
        '
        'OK_Button
        '
        Me.OK_Button.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.OK_Button.Location = New System.Drawing.Point(3, 3)
        Me.OK_Button.Name = "OK_Button"
        Me.OK_Button.Size = New System.Drawing.Size(67, 23)
        Me.OK_Button.TabIndex = 0
        Me.OK_Button.Text = "OK"
        '
        'Cancel_Button
        '
        Me.Cancel_Button.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Cancel_Button.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Cancel_Button.Location = New System.Drawing.Point(76, 3)
        Me.Cancel_Button.Name = "Cancel_Button"
        Me.Cancel_Button.Size = New System.Drawing.Size(67, 23)
        Me.Cancel_Button.TabIndex = 1
        Me.Cancel_Button.Text = "Cancel"
        '
        'Label1
        '
        Me.Label1.Location = New System.Drawing.Point(21, 18)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(386, 33)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "To successfully generate your A-TEMPXXX acounts, please ensure that the following" & _
            " three pieces of information are filled in:"
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.a_tempaccountpassword)
        Me.GroupBox1.Controls.Add(Me.Label6)
        Me.GroupBox1.Controls.Add(Me.a_tempaccountend)
        Me.GroupBox1.Controls.Add(Me.a_tempaccountstart)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.a_tempaccountformat)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.ForeColor = System.Drawing.Color.DimGray
        Me.GroupBox1.Location = New System.Drawing.Point(24, 64)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(383, 156)
        Me.GroupBox1.TabIndex = 2
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Settings"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label2.Location = New System.Drawing.Point(20, 26)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(116, 13)
        Me.Label2.TabIndex = 0
        Me.Label2.Text = "Account Name Format:"
        '
        'a_tempaccountformat
        '
        Me.a_tempaccountformat.Location = New System.Drawing.Point(142, 23)
        Me.a_tempaccountformat.Name = "a_tempaccountformat"
        Me.a_tempaccountformat.Size = New System.Drawing.Size(210, 20)
        Me.a_tempaccountformat.TabIndex = 1
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label3.Location = New System.Drawing.Point(20, 55)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(106, 13)
        Me.Label3.TabIndex = 2
        Me.Label3.Text = "Generate Starting At:"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label4.Location = New System.Drawing.Point(20, 86)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(103, 13)
        Me.Label4.TabIndex = 3
        Me.Label4.Text = "Generate Ending At:"
        '
        'a_tempaccountstart
        '
        Me.a_tempaccountstart.Location = New System.Drawing.Point(142, 53)
        Me.a_tempaccountstart.Maximum = New Decimal(New Integer() {1000, 0, 0, 0})
        Me.a_tempaccountstart.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.a_tempaccountstart.Name = "a_tempaccountstart"
        Me.a_tempaccountstart.Size = New System.Drawing.Size(80, 20)
        Me.a_tempaccountstart.TabIndex = 4
        Me.a_tempaccountstart.Value = New Decimal(New Integer() {1, 0, 0, 0})
        '
        'a_tempaccountend
        '
        Me.a_tempaccountend.Location = New System.Drawing.Point(142, 84)
        Me.a_tempaccountend.Maximum = New Decimal(New Integer() {1000, 0, 0, 0})
        Me.a_tempaccountend.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.a_tempaccountend.Name = "a_tempaccountend"
        Me.a_tempaccountend.Size = New System.Drawing.Size(80, 20)
        Me.a_tempaccountend.TabIndex = 5
        Me.a_tempaccountend.Value = New Decimal(New Integer() {1, 0, 0, 0})
        '
        'Label5
        '
        Me.Label5.Location = New System.Drawing.Point(21, 235)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(386, 46)
        Me.Label5.TabIndex = 3
        Me.Label5.Text = "Note that the generated accounts will be named like this: FormatNumber. So for ex" & _
            "ample, if your format is A-TEMP and your starting number is 5, then the first ac" & _
            "count generated will be A-TEMP005" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10)
        '
        'a_tempaccountpassword
        '
        Me.a_tempaccountpassword.Location = New System.Drawing.Point(142, 115)
        Me.a_tempaccountpassword.Name = "a_tempaccountpassword"
        Me.a_tempaccountpassword.Size = New System.Drawing.Size(210, 20)
        Me.a_tempaccountpassword.TabIndex = 7
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label6.Location = New System.Drawing.Point(20, 118)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(99, 13)
        Me.Label6.TabIndex = 6
        Me.Label6.Text = "Account Password:"
        '
        'A_TempAccountCreation
        '
        Me.AcceptButton = Me.OK_Button
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.Cancel_Button
        Me.ClientSize = New System.Drawing.Size(431, 338)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "A_TempAccountCreation"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "A-TEMPXXX Account Creation"
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.a_tempaccountstart, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.a_tempaccountend, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents OK_Button As System.Windows.Forms.Button
    Friend WithEvents Cancel_Button As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Public WithEvents a_tempaccountformat As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Public WithEvents a_tempaccountstart As System.Windows.Forms.NumericUpDown
    Public WithEvents a_tempaccountend As System.Windows.Forms.NumericUpDown
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Public WithEvents a_tempaccountpassword As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label

End Class
