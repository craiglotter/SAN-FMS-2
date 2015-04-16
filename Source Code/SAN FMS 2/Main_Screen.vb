Imports System.IO
Imports System.Web.Mail
Imports System.Security.Cryptography
Imports System.Text


Public Class Main_Screen

    Private busyworking As Boolean = False
    Private AutoUpdate As Boolean = False
    Dim shownminimizetip As Boolean = False

    Dim emaillist As String = ""
    Dim emaillist2 As String = ""
    Dim emaillist3 As String = ""

    Dim currentuserfolders As Integer = 0
    Dim currentuserfiles As Integer = 0
    Dim currentuserusage As Long = 0

    Private LastReport As Date

    Dim a_tempaccountnumber As Integer = 0
    Dim a_tempaccountstart As Integer = 0
    Dim a_tempaccountend As Integer = 0
    Dim a_tempaccountformat As String = "A-TEMP"
    Dim a_tempaccountpassword As String = "summer"

    Dim SAN_MailServer As String = ""
    Dim SAN_FromAddress As String = ""
    Dim SAN_ReportToAddress As String = ""
    Dim SAN_ReportTime As String = ""

    Private Sub Error_Handler(ByVal ex As Exception, Optional ByVal identifier_msg As String = "")
        Try
            If ex.Message.IndexOf("Thread was being aborted") < 0 Then
                Dim Display_Message1 As New Display_Message()
                Display_Message1.Message_Textbox.Text = "The Application encountered the following problem: " & vbCrLf & identifier_msg & ": " & ex.ToString
                Display_Message1.Timer1.Interval = 1000
                Display_Message1.ShowDialog()
                Dim dir As System.IO.DirectoryInfo = New System.IO.DirectoryInfo((Application.StartupPath & "\").Replace("\\", "\") & "Error Logs")
                If dir.Exists = False Then
                    dir.Create()
                End If
                dir = Nothing
                Dim filewriter As System.IO.StreamWriter = New System.IO.StreamWriter((Application.StartupPath & "\").Replace("\\", "\") & "Error Logs\" & Format(Now(), "yyyyMMdd") & "_Error_Log.txt", True)
                filewriter.WriteLine("#" & Format(Now(), "dd/MM/yyyy hh:mm:ss tt") & " - " & identifier_msg & ": " & ex.ToString)
                filewriter.WriteLine("")
                filewriter.Flush()
                filewriter.Close()
                filewriter = Nothing
            End If
            StatusLabel.Text = "Error Reported"
        Catch exc As Exception
            MsgBox("An error occurred in the application's error handling routine. The application will try to recover from this serious error." & vbCrLf & vbCrLf & exc.ToString, MsgBoxStyle.Critical, "Critical Error Encountered")
        End Try
    End Sub

    Private Sub Activity_Handler(ByVal message As String)
        Try
            Dim dir As System.IO.DirectoryInfo = New System.IO.DirectoryInfo((Application.StartupPath & "\").Replace("\\", "\") & "Activity Logs")
            If dir.Exists = False Then
                dir.Create()
            End If
            dir = Nothing
            Dim filewriter As System.IO.StreamWriter = New System.IO.StreamWriter((Application.StartupPath & "\").Replace("\\", "\") & "Activity Logs\" & Format(Now(), "yyyyMMdd") & "_Activity_Log.txt", True)
            filewriter.WriteLine("#" & Format(Now(), "dd/MM/yyyy hh:mm:ss tt") & " - " & message)
            filewriter.WriteLine("")
            filewriter.Flush()
            filewriter.Close()
            filewriter = Nothing
            StatusLabel.Text = "Activity Logged"
        Catch ex As Exception
            Error_Handler(ex, "Activity Handler")
        End Try
    End Sub

    Private Sub Main_Screen_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Control.CheckForIllegalCrossThreadCalls = False
            Me.Text = My.Application.Info.ProductName & " (" & Format(My.Application.Info.Version.Major, "0000") & Format(My.Application.Info.Version.Minor, "00") & Format(My.Application.Info.Version.Build, "00") & "." & Format(My.Application.Info.Version.Revision, "00") & ")"
            NotifyIcon1.BalloonTipText = "You have chosen to hide " & My.Application.Info.ProductName & ". To bring it back up, simply click here."
            NotifyIcon1.BalloonTipTitle = My.Application.Info.ProductName
            NotifyIcon1.Text = "Click to bring up " & My.Application.Info.ProductName
            loadSettings()
            Dim timearray As String()
            timearray = SAN_ReportTime.Split(":")
            LastReport = New Date(Now.Year, Now.Month, Now.Day, Integer.Parse(timearray(0)), Integer.Parse(timearray(1)), Integer.Parse(timearray(2)), 0, DateTimeKind.Local)
            timearray = Nothing
            StatusLabel.Text = "Application Loaded"
            SendNotificationEmail("Startup")
            Timer1.Start()
        Catch ex As Exception
            Error_Handler(ex, "Application Loading")
        End Try
    End Sub

    Private Sub loadSettings()
        Try

            Dim configfile As String = (Application.StartupPath & "\config.sav").Replace("\\", "\")
            If My.Computer.FileSystem.FileExists(configfile) Then
                Dim reader As StreamReader = New StreamReader(configfile)
                Dim lineread As String
                Dim variablevalue As String
                While reader.Peek <> -1
                    lineread = reader.ReadLine
                    If lineread.IndexOf("=") <> -1 Then
                        variablevalue = lineread.Remove(0, lineread.IndexOf("=") + 1)
                        If lineread.StartsWith("SAN_Folder=") Then
                            SAN_Folder.Text = variablevalue
                            If My.Computer.FileSystem.DirectoryExists(variablevalue) = True Then
                                FolderBrowserDialog1.SelectedPath = variablevalue
                            End If
                        End If
                        If lineread.StartsWith("SAN_Database=") Then
                            SAN_Database.Text = variablevalue
                            If My.Computer.FileSystem.FileExists(variablevalue) = True Then
                                OpenFileDialog1.FileName = variablevalue
                            End If
                        End If
                        If lineread.StartsWith("SAN_FileZilla=") Then
                            SAN_FileZilla.Text = variablevalue
                            If My.Computer.FileSystem.FileExists(variablevalue) = True Then
                                OpenFileDialog2.FileName = variablevalue
                            End If
                        End If
                        If lineread.StartsWith("SAN_XML=") Then
                            SAN_XML.Text = variablevalue
                            If My.Computer.FileSystem.FileExists(variablevalue) = True Then
                                OpenFileDialog3.FileName = variablevalue
                            End If
                        End If
                        If lineread.StartsWith("SAN_MailServer=") Then
                            SAN_MailServer = variablevalue
                        End If
                        If lineread.StartsWith("SAN_FromAddress=") Then
                            SAN_FromAddress = variablevalue
                        End If
                        If lineread.StartsWith("SAN_ReportToAddress=") Then
                            SAN_ReportToAddress = variablevalue
                        End If
                        If lineread.StartsWith("SAN_ReportTime=") Then
                            SAN_ReportTime = variablevalue
                        End If
                    End If
                End While
                reader.Close()
                reader = Nothing
            End If

            If SAN_MailServer.Length < 1 Then
                SAN_MailServer = "mail.uct.ac.za"
            End If
            If SAN_FromAddress.Length < 1 Then
                SAN_FromAddress = "com-webmaster@uct.ac.za"
            End If
            If SAN_ReportToAddress.Length < 1 Then
                SAN_ReportToAddress = "com-webmaster@uct.ac.za"
            End If
            If SAN_ReportTime.Length < 1 Then
                SAN_ReportTime = "00:30:00"
            End If

            StatusLabel.Text = "Application Settings Loaded"
        Catch ex As Exception
            Error_Handler(ex, "Load Settings")
        End Try
    End Sub

    Private Sub SaveSettings()
        Try
            Dim configfile As String = (Application.StartupPath & "\config.sav").Replace("\\", "\")
            Dim writer As StreamWriter = New StreamWriter(configfile, False)
            writer.WriteLine("SAN_Folder=" & SAN_Folder.Text)
            writer.WriteLine("SAN_Database=" & SAN_Database.Text)
            writer.WriteLine("SAN_FileZilla=" & SAN_FileZilla.Text)
            writer.WriteLine("SAN_XML=" & SAN_XML.Text)
            writer.WriteLine("SAN_MailServer=" & SAN_MailServer)
            writer.WriteLine("SAN_FromAddress=" & SAN_FromAddress)
            writer.WriteLine("SAN_ReportToAddress=" & SAN_ReportToAddress)
            writer.WriteLine("SAN_ReportTime=" & SAN_ReportTime)
            writer.Flush()
            writer.Close()
            writer = Nothing
            StatusLabel.Text = "Application Settings Saved"
        Catch ex As Exception
            Error_Handler(ex, "Save Settings")
        End Try
    End Sub

    Private Sub Main_Screen_FormClosed(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles MyBase.FormClosed
        Try
            Timer1.Stop()
            SendNotificationEmail("Shutdown")
            SaveSettings()
            If AutoUpdate = True Then
                If My.Computer.FileSystem.FileExists((Application.StartupPath & "\AutoUpdate.exe").Replace("\\", "\")) = True Then
                    Dim startinfo As ProcessStartInfo = New ProcessStartInfo
                    startinfo.FileName = (Application.StartupPath & "\AutoUpdate.exe").Replace("\\", "\")
                    startinfo.Arguments = "force"
                    startinfo.CreateNoWindow = False
                    Process.Start(startinfo)
                End If
            End If
            StatusLabel.Text = "Application Shutting Down"
        Catch ex As Exception
            Error_Handler(ex, "Closing Application")
        End Try
    End Sub
    Private Sub NotifyIcon1_BalloonTipClicked(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NotifyIcon1.BalloonTipClicked
        Try
            Me.WindowState = FormWindowState.Normal
            Me.ShowInTaskbar = True
            NotifyIcon1.Visible = False
            Me.Refresh()
        Catch ex As Exception
            Error_Handler(ex, "Click on NotifyIcon")
        End Try
    End Sub


    Private Sub NotifyIcon1_MouseClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles NotifyIcon1.MouseClick
        Try
            Me.WindowState = FormWindowState.Normal
            Me.ShowInTaskbar = True
            NotifyIcon1.Visible = False
            Me.Refresh()
        Catch ex As Exception
            Error_Handler(ex, "Click on NotifyIcon")
        End Try
    End Sub


    Private Sub NotifyIcon1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NotifyIcon1.Click
        Try
            Me.WindowState = FormWindowState.Normal
            Me.ShowInTaskbar = True
            NotifyIcon1.Visible = False
            Me.Refresh()
        Catch ex As Exception
            Error_Handler(ex, "Click on NotifyIcon")
        End Try
    End Sub

    Private Sub Main_Screen_Resize(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Resize
        Try
            If Me.WindowState = FormWindowState.Minimized Then
                Me.ShowInTaskbar = False
                NotifyIcon1.Visible = True
                If shownminimizetip = False Then
                    NotifyIcon1.ShowBalloonTip(1)
                    shownminimizetip = True
                End If
            End If
        Catch ex As Exception
            Error_Handler(ex, "Change Window State")
        End Try
    End Sub

    Private Sub HelpToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles HelpToolStripMenuItem1.Click
        Try
            HelpBox1.ShowDialog()
            StatusLabel.Text = "Help Dialog Viewed"
        Catch ex As Exception
            Error_Handler(ex, "Display Help Screen")
        End Try
    End Sub

    Private Sub AutoUpdateToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AutoUpdateToolStripMenuItem.Click
        Try
            StatusLabel.Text = "AutoUpdate Requested"
            AutoUpdate = True
            Me.Close()
        Catch ex As Exception
            Error_Handler(ex, "AutoUpdate")
        End Try
    End Sub

    Private Sub AboutToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AboutToolStripMenuItem1.Click
        Try
            AboutBox1.ShowDialog()
            StatusLabel.Text = "About Dialog Viewed"
        Catch ex As Exception
            Error_Handler(ex, "Display About Screen")
        End Try
    End Sub

    Private Sub Control_Enabler(ByVal IsEnabled As Boolean)
        Try
            Select Case IsEnabled
                Case True
                    Button1.Enabled = True
                    Button2.Enabled = True
                    Button3.Enabled = True
                    Button4.Enabled = True
                    Button5.Enabled = True
                    Button_GenerateATempXXXAccounts.Enabled = True
                    Button_RemoveATempXXXAccounts.Enabled = True
                    MenuStrip1.Enabled = True
                    Label6.Enabled = True
                    Me.ControlBox = True
                    ProgressBar1.Enabled = False
                Case False
                    Label6.Enabled = False
                    Button1.Enabled = False
                    Button2.Enabled = False
                    Button3.Enabled = False
                    Button4.Enabled = False
                    Button5.Enabled = False
                    Button_GenerateATempXXXAccounts.Enabled = False
                    Button_RemoveATempXXXAccounts.Enabled = False
                    MenuStrip1.Enabled = False
                    Me.ControlBox = False
                    ProgressBar1.Enabled = True
            End Select
            StatusLabel.Text = "Control Enabler Run"
        Catch ex As Exception
            Error_Handler(ex, "Control Enabler")
        End Try
    End Sub


    Private Function DosShellCommand(ByVal AppToRun As String) As String
        Dim s As String = ""
        Try
            Dim myProcess As Process = New Process

            myProcess.StartInfo.FileName = "cmd.exe"
            myProcess.StartInfo.UseShellExecute = False
            myProcess.StartInfo.CreateNoWindow = True
            myProcess.StartInfo.RedirectStandardInput = True
            myProcess.StartInfo.RedirectStandardOutput = True
            myProcess.StartInfo.RedirectStandardError = True
            myProcess.Start()
            Dim sIn As StreamWriter = myProcess.StandardInput
            sIn.AutoFlush = True

            Dim sOut As StreamReader = myProcess.StandardOutput
            Dim sErr As StreamReader = myProcess.StandardError
            sIn.Write(AppToRun & _
               System.Environment.NewLine)
            sIn.Write("exit" & System.Environment.NewLine)
            s = sOut.ReadToEnd()
            If Not myProcess.HasExited Then
                myProcess.Kill()
            End If

            'MessageBox.Show("The 'dir' command window was closed at: " & myProcess.ExitTime & "." & System.Environment.NewLine & "Exit Code: " & myProcess.ExitCode)

            sIn.Close()
            sOut.Close()
            sErr.Close()
            myProcess.Close()
            'MessageBox.Show(s)
        Catch ex As Exception
            Error_Handler(ex, "DOS Shell Command")
        End Try
        Return s
    End Function

    Public Function TextMail(ByVal strTo As String, ByVal strSubj As String, ByVal strBody As String, ByVal requestidentifier As String, Optional ByRef strErrMsg As String = "") As Boolean
        Dim objMail As MailMessage
        Try
            Dim emailaddys As String()
            emailaddys = strTo.Split(";")
            Dim counter As Integer = 0
            For counter = 0 To emailaddys.Length - 1
                Try
                    objMail = New MailMessage
                    objMail.BodyFormat = MailFormat.Text
                    objMail.From = SAN_FromAddress
                    objMail.To = emailaddys(counter).Trim
                    objMail.Subject = strSubj
                    objMail.Body = strBody

                    SmtpMail.SmtpServer = SAN_MailServer
                    SmtpMail.Send(objMail)
                Catch ex As Exception
                    Error_Handler(ex, "Send Mail: " & emailaddys(counter).Trim & " (" & requestidentifier & ")")
                End Try
            Next
            TextMail = True
        Catch ex As Exception
            TextMail = False
            Error_Handler(ex, "Send Mail")
        End Try
    End Function

    Public Function TextMail(ByVal SmtpServer As String, ByVal strFrom As String, ByVal strTo As String, ByVal strSubj As String, ByVal strBody As String, ByVal requestidentifier As String, Optional ByRef strErrMsg As String = "") As Boolean
        Dim objMail As MailMessage
        Try
            Dim emailaddys As String()
            emailaddys = strTo.Split(";")

            Dim counter As Integer = 0
            For counter = 0 To emailaddys.Length - 1
                Try
                    objMail = New MailMessage
                    objMail.BodyFormat = MailFormat.Text
                    objMail.From = strFrom
                    objMail.To = emailaddys(counter).Trim
                    objMail.Subject = strSubj
                    objMail.Body = strBody

                    SmtpMail.SmtpServer = SmtpServer
                    SmtpMail.Send(objMail)
                Catch ex As Exception
                    Error_Handler(ex, "Send Mail: " & emailaddys(counter).Trim & " (" & requestidentifier & ")")
                End Try
            Next
            TextMail = True
        Catch ex As Exception
            TextMail = False
            Error_Handler(ex, "Send Mail")
        End Try
    End Function

    Private Function Send_Report(ByVal dt As Date, ByVal FileNamePrefix As String) As Boolean
        Dim objMail As MailMessage = New MailMessage
        Try
            If My.Computer.FileSystem.FileExists((Application.StartupPath & "\").Replace("\\", "\") & "Error Logs\" & FileNamePrefix & "_Error_Log.txt") = True Then
                My.Computer.FileSystem.CopyFile((Application.StartupPath & "\").Replace("\\", "\") & "Error Logs\" & FileNamePrefix & "_Error_Log.txt", (Application.StartupPath & "\").Replace("\\", "\") & "Error Logs\" & FileNamePrefix & "_Error_Log_Copy.txt", True)
            End If
            If My.Computer.FileSystem.FileExists((Application.StartupPath & "\").Replace("\\", "\") & "Activity Logs\" & FileNamePrefix & "_Activity_Log.txt") = True Then
                My.Computer.FileSystem.CopyFile((Application.StartupPath & "\").Replace("\\", "\") & "Activity Logs\" & FileNamePrefix & "_Activity_Log.txt", (Application.StartupPath & "\").Replace("\\", "\") & "Activity Logs\" & FileNamePrefix & "_Activity_Log_Copy.txt", True)
            End If
            Try
                objMail.BodyFormat = MailFormat.Text
                objMail.From = SAN_FromAddress
                objMail.To = SAN_ReportToAddress
                objMail.Subject = "SAN FMS 2: Daily Report"
                Dim body As String
                body = "Any activity and error logs for " & Format(dt, "dd MM yyyy") & " have been attached with this report."
                body = body & vbCrLf & vbCrLf & "******************************" & vbCrLf & vbCrLf & "This is an auto-generated email submitted from " & My.Application.Info.ProductName & " at " & Format(Now(), "dd/MM/yyyy hh:mm:ss tt") & ", running on:"
                body = body & vbCrLf & vbCrLf & "Machine Name: " + Environment.MachineName
                body = body & vbCrLf & "OS Version: " & Environment.OSVersion.ToString()
                body = body & vbCrLf & "User Name: " + Environment.UserName
                objMail.Body = body

                If My.Computer.FileSystem.FileExists((Application.StartupPath & "\").Replace("\\", "\") & "Activity Logs\" & FileNamePrefix & "_Activity_Log_Copy.txt") = True Then
                    Dim objAttach As MailAttachment = New MailAttachment((Application.StartupPath & "\").Replace("\\", "\") & "Activity Logs\" & FileNamePrefix & "_Activity_Log_Copy.txt")
                    objMail.Attachments.Add(objAttach)
                    objAttach = Nothing
                End If
                If My.Computer.FileSystem.FileExists((Application.StartupPath & "\").Replace("\\", "\") & "Error Logs\" & FileNamePrefix & "_Error_Log_Copy.txt") = True Then
                    Dim objAttach As MailAttachment = New MailAttachment((Application.StartupPath & "\").Replace("\\", "\") & "Error Logs\" & FileNamePrefix & "_Error_Log_Copy.txt")
                    objMail.Attachments.Add(objAttach)
                    objAttach = Nothing
                End If

                SmtpMail.SmtpServer = SAN_MailServer
                SmtpMail.Send(objMail)
                objMail = Nothing
                Send_Report = True
            Catch ex As Exception
                Send_Report = False
                objMail.Attachments.Clear()
                objMail = Nothing
                Error_Handler(ex, "Send Report Mail: " & SAN_ReportToAddress & " (Send Report)")
            End Try
            If My.Computer.FileSystem.FileExists((Application.StartupPath & "\").Replace("\\", "\") & "Activity Logs\" & FileNamePrefix & "_Activity_Log_Copy.txt") = True Then
                My.Computer.FileSystem.DeleteFile((Application.StartupPath & "\").Replace("\\", "\") & "Activity Logs\" & FileNamePrefix & "_Activity_Log_Copy.txt")
            End If
            If My.Computer.FileSystem.FileExists((Application.StartupPath & "\").Replace("\\", "\") & "Error Logs\" & FileNamePrefix & "_Error_Log_Copy.txt") = True Then
                My.Computer.FileSystem.DeleteFile((Application.StartupPath & "\").Replace("\\", "\") & "Error Logs\" & FileNamePrefix & "_Error_Log_Copy.txt")
            End If
        Catch ex As Exception
            Error_Handler(ex, "Send Report Mail: " & SAN_ReportToAddress & " (Send Report)")
        End Try
    End Function

    Private Sub BackgroundWorker1_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles BackgroundWorker1.DoWork
        Try
            If SAN_Database.Text.Length > 0 And SAN_Folder.Text.Length > 0 And SAN_FileZilla.Text.Length > 0 And SAN_XML.Text.Length > 0 Then
                If My.Computer.FileSystem.FileExists(SAN_Database.Text) = True And My.Computer.FileSystem.DirectoryExists(SAN_Folder.Text) = True And My.Computer.FileSystem.FileExists(SAN_FileZilla.Text) = True Then
                    emaillist = ""
                    emaillist2 = ""
                    emaillist3 = ""
                    StatusLabel.Text = "Removing Expired Temporary Accounts"
                    RemoveExpiredTemporaryAccounts()
                    ProgressBar1.Value = 5
                    StatusLabel.Text = "Removing Expired Accounts"
                    RemoveExpiredAccounts()
                    ProgressBar1.Value = 10
                    StatusLabel.Text = "Checking Database for Valid Entries"
                    'check that valid user isn't going over their size limit
                    CheckValidDatabaseEntries()
                    ProgressBar1.Value = 15
                    StatusLabel.Text = "Checking Root Folder for Orphaned Accounts"
                    RemoveOrphanedFolders()
                    ProgressBar1.Value = 30
                    StatusLabel.Text = "Generating New User Accounts"
                    CreateNewUserFolders()
                    ProgressBar1.Value = 45
                    StatusLabel.Text = "Generating FileZilla XML Config"
                    GenerateFileZillaXML()
                    ProgressBar1.Value = 60
                    StatusLabel.Text = "Reloading FileZilla Config File"
                    RunFileZillaConfigReload()
                    ProgressBar1.Value = 75
                    StatusLabel.Text = "Notifying New Account Holders"
                    EmailNewAccounts()
                    ProgressBar1.Value = 80
                    StatusLabel.Text = "Notifying Expiring Account Holders"
                    EmailExpiringAccounts()
                    ProgressBar1.Value = 90
                    StatusLabel.Text = "Notifying Removed Account Holders"
                    EmailDeletedAccounts()
                    'notify newly locked accounts
                    StatusLabel.Text = "Updating Database User Stats"
                    UpdateDatabaseUserStats()
                    StatusLabel.Text = "Updating ""Last Scan"" Variable"
                    UpdateLastScanVariable()
                    ProgressBar1.Value = 100
                    e.Result = "Success"
                Else
                    ProgressBar1.Value = 100
                    e.Result = "Failed"
                End If
            Else
                ProgressBar1.Value = 100
                e.Result = "Failed"
            End If
        Catch ex As Exception
            Error_Handler(ex, "SAN FMS Operation")
        End Try
    End Sub

    Private Sub BackgroundWorker1_RunWorkerCompleted(ByVal sender As System.Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles BackgroundWorker1.RunWorkerCompleted
        Try
            Control_Enabler(True)
            If e.Cancelled = False And e.Error Is Nothing Then
                StatusLabel.Text = "SAN FMS Operation Complete"
            Else
                StatusLabel.Text = "SAN FMS Operation Failed"
            End If
            Activity_Handler("--- Sweep Ended ---")
            busyworking = False
            Timer1.Start()
        Catch ex As Exception
            Error_Handler(ex, "SAN FMS Operation Complete")
        End Try
    End Sub

    Private Sub ActivityLogLink_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles ActivityLogLink.LinkClicked
        Try
            Dim dir As System.IO.DirectoryInfo = New System.IO.DirectoryInfo((Application.StartupPath & "\").Replace("\\", "\") & "Activity Logs")
            If dir.Exists = False Then
                dir.Create()
            End If
            dir = Nothing
            Process.Start((Application.StartupPath & "\").Replace("\\", "\") & "Activity Logs")
            StatusLabel.Text = "Showing Activity Logs"
        Catch ex As Exception
            Error_Handler(ex, "Activity Log Link")
        End Try
    End Sub

    Private Sub ErrorLogLink_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles ErrorLogLink.LinkClicked
        Try
            Dim dir As System.IO.DirectoryInfo = New System.IO.DirectoryInfo((Application.StartupPath & "\").Replace("\\", "\") & "Error Logs")
            If dir.Exists = False Then
                dir.Create()
            End If
            dir = Nothing
            Process.Start((Application.StartupPath & "\").Replace("\\", "\") & "Error Logs")
            StatusLabel.Text = "Showing Error Logs"
        Catch ex As Exception
            Error_Handler(ex, "Error Log Link")
        End Try
    End Sub

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        Try
            TimerLabel.Text = Integer.Parse(TimerLabel.Text) - 1
            TempTimerLabel.Text = Integer.Parse(TempTimerLabel.Text) - 1
            While TimerLabel.Text.Length < 4
                TimerLabel.Text = "0" & TimerLabel.Text
            End While
            While TempTimerLabel.Text.Length < 4
                TempTimerLabel.Text = "0" & TempTimerLabel.Text
            End While

            If TimerLabel.Text = "0000" Then
                Timer1.Stop()
                runworker()
                TimerLabel.Text = "0900"
            End If
            If TempTimerLabel.Text = "0000" Then
                Timer1.Stop()
                runtempworker()
                TempTimerLabel.Text = "0180"
            End If
            If busyworking = False Then
                If StatusLabel.Text <> "Waiting Mode Activated" Then
                    StatusLabel.Text = "Waiting Mode Activated"
                End If
            End If

            If busyworking = False Then
                Dim timearray As String()
                timearray = SAN_ReportTime.Split(":")
                Dim dt As Date = New Date(Now.Year, Now.Month, Now.Day, Integer.Parse(timearray(0)), Integer.Parse(timearray(1)), Integer.Parse(timearray(2)), 0, DateTimeKind.Local)
                If dt > LastReport Then
                    Timer1.Stop()
                    StatusLabel.Text = "Sending Daily Report"
                    Send_Report(Now, Format(LastReport, "yyyyMMdd"))
                    LastReport = dt
                    Timer1.Start()
                End If
                timearray = Nothing
            End If
        Catch ex As Exception
            Error_Handler(ex, "Timer Tick")
        End Try
    End Sub

    

    Private Sub runtempworker()
        Try
            If My.Computer.FileSystem.FileExists(SAN_Database.Text) = True Then
                Dim runop As Boolean = False
                StatusLabel.Text = "Checking for New Temporary Accounts"
                Dim dbconnection As OleDb.OleDbConnection = New OleDb.OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=""" & SAN_Database.Text & """")
                dbconnection.Open()
                Dim dbsql As OleDb.OleDbCommand = New OleDb.OleDbCommand
                dbsql.CommandText = "Select SAN_Username from SAN_User_Records where SAN_Active = 'False' and SAN_Tag = 'Temporary Account'"
                dbsql.Connection = dbconnection
                Dim dbreader As OleDb.OleDbDataReader = dbsql.ExecuteReader()
                If dbreader.HasRows = True Then
                    runop = True
                End If
                dbreader.Close()
                dbreader = Nothing
                dbsql.Dispose()
                dbsql = Nothing
                dbconnection.Close()
                dbconnection.Dispose()
                dbconnection = Nothing
                StatusLabel.Text = "New Temporary Accounts Located"
                If runop = True Then
                    If busyworking = False Then
                        busyworking = True
                        Control_Enabler(False)
                        Timer1.Stop()
                        StatusLabel.Text = "Initializing FMS Operation"
                        Activity_Handler("--- Sweep Started ---")
                        BackgroundWorker1.RunWorkerAsync()
                    Else
                        Timer1.Start()
                    End If
                Else
                    Timer1.Start()
                End If
            Else
                Timer1.Start()
            End If


        Catch ex As Exception
            Error_Handler(ex, "Run Worker")
        End Try
    End Sub

    Private Sub runworker()
        Try
            If busyworking = False Then
                busyworking = True
                Control_Enabler(False)
                Timer1.Stop()
                StatusLabel.Text = "Initializing FMS Operation"
                Activity_Handler("--- Sweep Started ---")
                BackgroundWorker1.RunWorkerAsync()
            Else
                Timer1.Start()
            End If
        Catch ex As Exception
            Error_Handler(ex, "Run Worker")
        End Try
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Try
            If Button1.Text = "Stop Timer" Then
                Timer1.Stop()
                TimerLabel.Text = "0900"
                Button1.Text = "Start Timer"
                StatusLabel.Text = "Monitoring is Disabled"
                StatusLabel.Text = "Timer Stopped"
            Else
                Timer1.Start()
                Button1.Text = "Stop Timer"
                StatusLabel.Text = "Waiting Mode Activated"
                StatusLabel.Text = "Timer Started"
            End If
        Catch ex As Exception
            Error_Handler(ex, "Stop Timer Click")
        End Try
    End Sub

    Private Sub Label6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label6.Click
        Try
            StatusLabel.Text = "FMS Operation Forced"
            Timer1.Stop()
            runworker()
            TimerLabel.Text = "0900"
        Catch ex As Exception
            Error_Handler(ex, "Force Monitor")
        End Try
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Try
            If FolderBrowserDialog1.ShowDialog = Windows.Forms.DialogResult.OK Then
                If My.Computer.FileSystem.DirectoryExists(FolderBrowserDialog1.SelectedPath) = True Then
                    SAN_Folder.Text = FolderBrowserDialog1.SelectedPath
                End If
            End If
        Catch ex As Exception
            Error_Handler(ex, "Select SAN Root Folder")
        End Try
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        Try
            If OpenFileDialog1.ShowDialog = Windows.Forms.DialogResult.OK Then
                If My.Computer.FileSystem.FileExists(OpenFileDialog1.FileName) = True Then
                    SAN_Database.Text = OpenFileDialog1.FileName
                End If
            End If
        Catch ex As Exception
            Error_Handler(ex, "Select SAN Database")
        End Try
    End Sub

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        Try
            If OpenFileDialog2.ShowDialog = Windows.Forms.DialogResult.OK Then
                If My.Computer.FileSystem.FileExists(OpenFileDialog2.FileName) = True Then
                    SAN_FileZilla.Text = OpenFileDialog2.FileName
                End If
            End If
        Catch ex As Exception
            Error_Handler(ex, "Select SAN FileZilla Executable")
        End Try
    End Sub

    Private Sub Button5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button5.Click
        Try
            If OpenFileDialog3.ShowDialog = Windows.Forms.DialogResult.OK Then
                If My.Computer.FileSystem.FileExists(OpenFileDialog3.FileName) = True Then
                    SAN_XML.Text = OpenFileDialog3.FileName
                End If
            End If
        Catch ex As Exception
            Error_Handler(ex, "Select SAN Config XML")
        End Try
    End Sub

    Private Sub GenerateFileZillaXML()
        Try
            StatusLabel.Text = "Generating FileZilla XML Config"
            If My.Computer.FileSystem.FileExists(SAN_XML.Text) = True Then
                If My.Computer.FileSystem.FileExists(SAN_XML.Text & ".Backup") = False Then
                    My.Computer.FileSystem.CopyFile(SAN_XML.Text, SAN_XML.Text & ".Backup")
                End If
                Dim filewriter As StreamWriter = New StreamWriter(SAN_XML.Text, False, System.Text.Encoding.UTF8)
                filewriter.WriteLine("<FileZillaServer>")
                filewriter.WriteLine("<Settings>")
                filewriter.WriteLine("<Item name=""Serverports"" type=""string"">21</Item>")
                filewriter.WriteLine("<Item name=""Number of Threads"" type=""numeric"">8</Item>")
                filewriter.WriteLine("<Item name=""Maximum user count"" type=""numeric"">0</Item>")
                filewriter.WriteLine("<Item name=""Timeout"" type=""numeric"">120</Item>")
                filewriter.WriteLine("<Item name=""No Transfer Timeout"" type=""numeric"">120</Item>")
                filewriter.WriteLine("<Item name=""Allow Incoming FXP"" type=""numeric"">0</Item>")
                filewriter.WriteLine("<Item name=""Allow outgoing FXP"" type=""numeric"">0</Item>")
                filewriter.WriteLine("<Item name=""No Strict In FXP"" type=""numeric"">0</Item>")
                filewriter.WriteLine("<Item name=""No Strict Out FXP"" type=""numeric"">0</Item>")
                filewriter.WriteLine("<Item name=""Login Timeout"" type=""numeric"">60</Item>")
                filewriter.WriteLine("<Item name=""Show Pass in Log"" type=""numeric"">1</Item>")
                filewriter.WriteLine("<Item name=""Custom PASV IP type"" type=""numeric"">0</Item>")
                filewriter.WriteLine("<Item name=""Custom PASV IP"" type=""string"">http://obe1.com.uct.ac.za</Item>")
                filewriter.WriteLine("<Item name=""Custom PASV min port"" type=""numeric"">5001</Item>")
                filewriter.WriteLine("<Item name=""Custom PASV max port"" type=""numeric"">5501</Item>")
                filewriter.WriteLine("<Item name=""Initial Welcome Message"" type=""string"">Welcome to the Commerce IT SAN FTP Server.")
                filewriter.WriteLine("Please note that all activity is logged and unauthorised")
                filewriter.WriteLine("access is forbidden. For more information, please visit")
                filewriter.WriteLine("Commerce IT's web site at:")
                filewriter.WriteLine("http://www.commerce.uct.ac.za/Commerce_IT</Item>")
                filewriter.WriteLine("<Item name=""Admin IP Bindings"" type=""string""></Item>")
                filewriter.WriteLine("<Item name=""Admin IP Addresses"" type=""string""></Item>")
                filewriter.WriteLine("<Item name=""Enable logging"" type=""numeric"">0</Item>")
                filewriter.WriteLine("<Item name=""Logsize limit"" type=""numeric"">0</Item>")
                filewriter.WriteLine("<Item name=""Logfile type"" type=""numeric"">0</Item>")
                filewriter.WriteLine("<Item name=""Logfile delete time"" type=""numeric"">0</Item>")
                filewriter.WriteLine("<Item name=""Use GSS Support"" type=""numeric"">0</Item>")
                filewriter.WriteLine("<Item name=""GSS Prompt for Password"" type=""numeric"">0</Item>")
                filewriter.WriteLine("<Item name=""Download Speedlimit Type"" type=""numeric"">0</Item>")
                filewriter.WriteLine("<Item name=""Upload Speedlimit Type"" type=""numeric"">0</Item>")
                filewriter.WriteLine("<Item name=""Download Speedlimit"" type=""numeric"">10</Item>")
                filewriter.WriteLine("<Item name=""Upload Speedlimit"" type=""numeric"">10</Item>")
                filewriter.WriteLine("<Item name=""Buffer Size"" type=""numeric"">32768</Item>")
                filewriter.WriteLine("<Item name=""Custom PASV IP server"" type=""string"">http://ip.filezilla-project.org/ip.php</Item>")
                filewriter.WriteLine("<Item name=""Use custom PASV ports"" type=""numeric"">1</Item>")
                filewriter.WriteLine("<Item name=""Mode Z Use"" type=""numeric"">0</Item>")
                filewriter.WriteLine("<Item name=""Mode Z min level"" type=""numeric"">1</Item>")
                filewriter.WriteLine("<Item name=""Mode Z max level"" type=""numeric"">9</Item>")
                filewriter.WriteLine("<Item name=""Mode Z allow local"" type=""numeric"">0</Item>")
                filewriter.WriteLine("<Item name=""Mode Z disallowed IPs"" type=""string""></Item>")
                filewriter.WriteLine("<Item name=""IP Bindings"" type=""string"">*</Item>")
                filewriter.WriteLine("<Item name=""IP Filter Allowed"" type=""string""></Item>")
                filewriter.WriteLine("<Item name=""IP Filter Disallowed"" type=""string""></Item>")
                filewriter.WriteLine("<Item name=""Hide Welcome Message"" type=""numeric"">1</Item>")
                filewriter.WriteLine("<Item name=""Enable SSL"" type=""numeric"">0</Item>")
                filewriter.WriteLine("<Item name=""Allow explicit SSL"" type=""numeric"">1</Item>")
                filewriter.WriteLine("<Item name=""SSL Key file"" type=""string""></Item>")
                filewriter.WriteLine("<Item name=""SSL Certificate file"" type=""string""></Item>")
                filewriter.WriteLine("<Item name=""Implicit SSL ports"" type=""string"">990</Item>")
                filewriter.WriteLine("<Item name=""Force explicit SSL"" type=""numeric"">0</Item>")
                filewriter.WriteLine("<Item name=""Network Buffer Size"" type=""numeric"">65536</Item>")
                filewriter.WriteLine("<Item name=""Force PROT P"" type=""numeric"">0</Item>")
                filewriter.WriteLine("<Item name=""SSL Key Password"" type=""string""></Item>")
                filewriter.WriteLine("<Item name=""Allow shared write"" type=""numeric"">0</Item>")
                filewriter.WriteLine("<Item name=""No External IP On Local"" type=""numeric"">1</Item>")
                filewriter.WriteLine("<Item name=""Active ignore local"" type=""numeric"">1</Item>")
                filewriter.WriteLine("<Item name=""Autoban enable"" type=""numeric"">0</Item>")
                filewriter.WriteLine("<Item name=""Autoban attempts"" type=""numeric"">5</Item>")
                filewriter.WriteLine("<Item name=""Autoban type"" type=""numeric"">0</Item>")
                filewriter.WriteLine("<Item name=""Autoban time"" type=""numeric"">1</Item>")
                filewriter.WriteLine("<Item name=""Admin port"" type=""numeric"">14147</Item>")
                filewriter.WriteLine("<SpeedLimits>")
                filewriter.WriteLine("<Download/>")
                filewriter.WriteLine("<Upload/>")
                filewriter.WriteLine("</SpeedLimits>")
                filewriter.WriteLine("</Settings>")
                filewriter.WriteLine("<Groups>")
                filewriter.WriteLine("<Group Name=""Staff"">")
                filewriter.WriteLine("<Option Name=""Bypass server userlimit"">0</Option>")
                filewriter.WriteLine("<Option Name=""User Limit"">0</Option>")
                filewriter.WriteLine("<Option Name=""IP Limit"">0</Option>")
                filewriter.WriteLine("<Option Name=""Enabled"">1</Option>")
                filewriter.WriteLine("<Option Name=""Comments""/>")
                filewriter.WriteLine("<Option Name=""ForceSsl"">0</Option>")
                filewriter.WriteLine("<IpFilter>")
                filewriter.WriteLine("<Disallowed/>")
                filewriter.WriteLine("<Allowed/>")
                filewriter.WriteLine("</IpFilter>")
                filewriter.WriteLine("<Permissions/>")
                filewriter.WriteLine("<SpeedLimits DlType=""1"" DlLimit=""10"" ServerDlLimitBypass=""0"" UlType=""1"" UlLimit=""10"" ServerUlLimitBypass=""0"">")
                filewriter.WriteLine("<Download/>")
                filewriter.WriteLine("<Upload/>")
                filewriter.WriteLine("</SpeedLimits>")
                filewriter.WriteLine("</Group>")
                filewriter.WriteLine("<Group Name=""Students"">")
                filewriter.WriteLine("<Option Name=""Bypass server userlimit"">0</Option>")
                filewriter.WriteLine("<Option Name=""User Limit"">0</Option>")
                filewriter.WriteLine("<Option Name=""IP Limit"">0</Option>")
                filewriter.WriteLine("<Option Name=""Enabled"">1</Option>")
                filewriter.WriteLine("<Option Name=""Comments""/>")
                filewriter.WriteLine("<Option Name=""ForceSsl"">0</Option>")
                filewriter.WriteLine("<IpFilter>")
                filewriter.WriteLine("<Disallowed/>")
                filewriter.WriteLine("<Allowed/>")
                filewriter.WriteLine("</IpFilter>")
                filewriter.WriteLine("<Permissions/>")
                filewriter.WriteLine("<SpeedLimits DlType=""1"" DlLimit=""10"" ServerDlLimitBypass=""0"" UlType=""1"" UlLimit=""10"" ServerUlLimitBypass=""0"">")
                filewriter.WriteLine("<Download/>")
                filewriter.WriteLine("<Upload/>")
                filewriter.WriteLine("</SpeedLimits>")
                filewriter.WriteLine("</Group>")
                filewriter.WriteLine("</Groups>")
                filewriter.WriteLine("<Users>")
                '****************
                Dim dbconnection As OleDb.OleDbConnection = New OleDb.OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=""" & SAN_Database.Text & """")
                dbconnection.Open()
                Dim dbsql As OleDb.OleDbCommand = New OleDb.OleDbCommand
                dbsql.CommandText = "Select SAN_Username, SAN_Password, SAN_Name, SAN_Account_Type from SAN_User_Records where SAN_Active = 'True'"
                dbsql.Connection = dbconnection
                Dim dbreader As OleDb.OleDbDataReader = dbsql.ExecuteReader()
                If dbreader.HasRows = True Then
                    While dbreader.Read = True
                        StatusLabel.Text = "Generating FileZilla XML Config: Adding " & dbreader.GetString(0)
                        filewriter.WriteLine("<User Name=""" & dbreader.GetString(0) & """>")
                        filewriter.WriteLine("<Option Name=""Pass"">" & dbreader.GetString(1) & "</Option>")
                        filewriter.WriteLine("<Option Name=""Group"">" & dbreader.GetString(3) & "</Option>")
                        filewriter.WriteLine("<Option Name=""Bypass server userlimit"">2</Option>")
                        filewriter.WriteLine("<Option Name=""User Limit"">0</Option>")
                        filewriter.WriteLine("<Option Name=""IP Limit"">0</Option>")
                        filewriter.WriteLine("<Option Name=""Enabled"">2</Option>")
                        filewriter.WriteLine("<Option Name=""Comments"">" & dbreader.GetString(2) & "</Option>")
                        filewriter.WriteLine("<Option Name=""ForceSsl"">2</Option>")
                        filewriter.WriteLine("<IpFilter>")
                        filewriter.WriteLine("<Disallowed/>")
                        filewriter.WriteLine("<Allowed/>")
                        filewriter.WriteLine("</IpFilter>")
                        filewriter.WriteLine("<Permissions>")
                        filewriter.WriteLine("<Permission Dir=""" & (SAN_Folder.Text & "\" & dbreader.GetString(3) & "\" & dbreader.GetString(0)).Replace("\\", "\") & """>")
                        filewriter.WriteLine("<Option Name=""FileRead"">1</Option>")
                        filewriter.WriteLine("<Option Name=""FileWrite"">1</Option>")
                        filewriter.WriteLine("<Option Name=""FileDelete"">1</Option>")
                        filewriter.WriteLine("<Option Name=""FileAppend"">0</Option>")
                        filewriter.WriteLine("<Option Name=""DirCreate"">1</Option>")
                        filewriter.WriteLine("<Option Name=""DirDelete"">1</Option>")
                        filewriter.WriteLine("<Option Name=""DirList"">1</Option>")
                        filewriter.WriteLine("<Option Name=""DirSubdirs"">1</Option>")
                        filewriter.WriteLine("<Option Name=""IsHome"">1</Option>")
                        filewriter.WriteLine("<Option Name=""AutoCreate"">0</Option>")
                        filewriter.WriteLine("</Permission>")
                        filewriter.WriteLine("</Permissions>")
                        filewriter.WriteLine("<SpeedLimits DlType=""0"" DlLimit=""10"" ServerDlLimitBypass=""2"" UlType=""0"" UlLimit=""10"" ServerUlLimitBypass=""2"">")
                        filewriter.WriteLine("<Download/>")
                        filewriter.WriteLine("<Upload/>")
                        filewriter.WriteLine("</SpeedLimits>")
                        filewriter.WriteLine("</User>")
                    End While
                End If
                dbreader.Close()
                dbreader = Nothing
                dbsql.Dispose()
                dbsql = Nothing
                dbconnection.Close()
                dbconnection.Dispose()
                dbconnection = Nothing
                '****************
                filewriter.WriteLine("</Users>")
                filewriter.WriteLine("</FileZillaServer>")
                filewriter.Flush()
                filewriter.Close()
                filewriter = Nothing
            End If
            StatusLabel.Text = "Generating FileZilla XML Config Completed"
        Catch ex As Exception
            Error_Handler(ex, "Generate FileZilla XML")
        End Try
    End Sub

    Private Sub RunFileZillaConfigReload()
        Try
            StatusLabel.Text = "Reloading FileZilla Config File"
            If My.Computer.FileSystem.FileExists(SAN_FileZilla.Text) = True Then
                Dim runprog As String = """" & SAN_FileZilla.Text & """ -reload-config"
                DosShellCommand(runprog)
            End If
            StatusLabel.Text = "Reloading FileZilla Config File Completed"
        Catch ex As Exception
            Error_Handler(ex, "Run FileZilla Config Reload")
        End Try
    End Sub

    Private Sub CreateNewUserFolders()
        Try
            StatusLabel.Text = "Generating New User Accounts"
            Dim dbconnection As OleDb.OleDbConnection = New OleDb.OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=""" & SAN_Database.Text & """")
            dbconnection.Open()
            Dim dbsql As OleDb.OleDbCommand = New OleDb.OleDbCommand
            dbsql.CommandText = "Select SAN_Username, SAN_Account_Type, SAN_Email_Address, SAN_Tag from SAN_User_Records where SAN_Active = 'False'"
            dbsql.Connection = dbconnection
            Dim dbreader As OleDb.OleDbDataReader = dbsql.ExecuteReader()
            If dbreader.HasRows = True Then
                While dbreader.Read = True
                    Try
                        StatusLabel.Text = "Generating New User Accounts: " & dbreader.GetString(0)
                        Activity_Handler("Generating New User Accounts: " & dbreader.GetString(0))
                        If My.Computer.FileSystem.DirectoryExists((SAN_Folder.Text & "\" & dbreader.GetString(1) & "\" & dbreader.GetString(0)).Replace("\\", "\")) = False Then
                            My.Computer.FileSystem.CreateDirectory((SAN_Folder.Text & "\" & dbreader.GetString(1) & "\" & dbreader.GetString(0)).Replace("\\", "\"))
                        End If
                        Dim dbsql2 As OleDb.OleDbCommand = New OleDb.OleDbCommand
                        dbsql2.Connection = dbconnection
                        dbsql2.CommandText = "Update SAN_User_Records set SAN_Active = 'True', SAN_Account_Created = '" & Format(Now(), "yyyyMMddhhmmss") & "' where SAN_Username = '" & dbreader.GetString(0) & "'"
                        dbsql2.ExecuteNonQuery()
                        dbsql2.Dispose()
                        dbsql2 = Nothing
                        If dbreader.GetString(3) <> "A-TEMPXXX" And dbreader.GetString(3) <> "Temporary Account" Then
                            emaillist = emaillist & ";" & dbreader.GetString(2)
                        End If
                        If emaillist.StartsWith(";") = True Then
                            emaillist = emaillist.Remove(0, 1)
                        End If
                    Catch ex As Exception
                        Error_Handler(ex, "Create New User Folders: " & dbreader.GetString(0))
                    End Try
                End While
            End If
            dbreader.Close()
            dbreader = Nothing
            dbsql.Dispose()
            dbsql = Nothing
            dbconnection.Close()
            dbconnection.Dispose()
            dbconnection = Nothing
            StatusLabel.Text = "Generating New User Accounts Completed"
        Catch ex As Exception
            Error_Handler(ex, "Create New User Folders")
        End Try
    End Sub

    Private Sub RemoveExpiredTemporaryAccounts()
        Try
            StatusLabel.Text = "Checking Database for Expired Temp Accounts"

            Dim dbconnection As OleDb.OleDbConnection = New OleDb.OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=""" & SAN_Database.Text & """")
            dbconnection.Open()
            Dim dbsql As OleDb.OleDbCommand = New OleDb.OleDbCommand
            dbsql.CommandText = "Select SAN_Username, SAN_Account_Created, SAN_Account_Type from SAN_User_Records where SAN_Tag = 'Temporary Account' and not SAN_Active = 'False' and not SAN_Active = 'Removed'"
            dbsql.Connection = dbconnection
            Dim dbreader As OleDb.OleDbDataReader = dbsql.ExecuteReader()
            If dbreader.HasRows = True Then
                While dbreader.Read = True
                    Try
                        StatusLabel.Text = "Checking Database for Expired Temp Accounts: " & dbreader.GetString(0) '
                        'check if account is expired based on SAN_Account_Created
                        Dim createddatestr As String
                        createddatestr = dbreader.GetString(1)
                        Dim createddate As Date = New Date(Integer.Parse(createddatestr.Substring(0, 4)), Integer.Parse(createddatestr.Substring(4, 2)), Integer.Parse(createddatestr.Substring(6, 2)), Integer.Parse(createddatestr.Substring(8, 2)), Integer.Parse(createddatestr.Substring(10, 2)), Integer.Parse(createddatestr.Substring(12, 2)), DateTimeKind.Local)
                        Dim nowdate As Date = Now
                        If DateDiff(DateInterval.Day, createddate, nowdate) > 14 Then
                            If My.Computer.FileSystem.DirectoryExists((SAN_Folder.Text & "\Removed").Replace("\\", "\")) = False Then
                                My.Computer.FileSystem.CreateDirectory((SAN_Folder.Text & "\Removed").Replace("\\", "\"))
                            End If
                            If My.Computer.FileSystem.DirectoryExists((SAN_Folder.Text & "\Removed\" & Format(Now, "yyyyMMdd")).Replace("\\", "\")) = False Then
                                My.Computer.FileSystem.CreateDirectory((SAN_Folder.Text & "\Removed\" & Format(Now, "yyyyMMdd")).Replace("\\", "\"))
                            End If
                            If My.Computer.FileSystem.DirectoryExists((SAN_Folder.Text & "\" & dbreader.GetString(2) & "\" & dbreader.GetString(0)).Replace("\\", "\")) = True Then
                                My.Computer.FileSystem.MoveDirectory((SAN_Folder.Text & "\" & dbreader.GetString(2) & "\" & dbreader.GetString(0)).Replace("\\", "\"), (SAN_Folder.Text & "\Removed\" & Format(Now, "yyyyMMdd") & "\" & dbreader.GetString(0)).Replace("\\", "\"))
                            End If
                            Dim dbsql2 As OleDb.OleDbCommand = New OleDb.OleDbCommand
                            dbsql2.CommandText = "Delete from SAN_User_Records where SAN_Username = '" & dbreader.GetString(0) & "'"
                            dbsql2.Connection = dbconnection
                            dbsql2.ExecuteNonQuery()
                            dbsql2.Dispose()
                            dbsql2 = Nothing
                            Activity_Handler("Expired Temporary Account Removed: " & dbreader.GetString(0))
                        End If
                    Catch ex As Exception
                        Error_Handler(ex, "Remove Expired Temp Accounts")
                    End Try
                End While
            End If
            dbreader.Close()
            dbreader = Nothing
            dbsql.Dispose()
            dbsql = Nothing
            dbconnection.Close()
            dbconnection.Dispose()
            dbconnection = Nothing


            StatusLabel.Text = "Checking Database for Expired Temp Accounts Completed"

        Catch ex As Exception
            Error_Handler(ex, "Remove Expired Temp Accounts")
        End Try
    End Sub


    Private Sub RemoveExpiredAccounts()
        Try
            StatusLabel.Text = "Checking Database for Unused Accounts"

            Dim dbconnection As OleDb.OleDbConnection = New OleDb.OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=""" & SAN_Database.Text & """")
            dbconnection.Open()
            Dim dbsql As OleDb.OleDbCommand = New OleDb.OleDbCommand
            dbsql.CommandText = "Select SAN_Username, SAN_Account_Created, SAN_Account_Type, SAN_Total_Files, SAN_Total_Folders, SAN_Email_Address from SAN_User_Records where not SAN_Tag = 'Temporary Account' and not SAN_Active = 'False' and not SAN_Active = 'Removed'"
            dbsql.Connection = dbconnection
            Dim dbreader As OleDb.OleDbDataReader = dbsql.ExecuteReader()
            If dbreader.HasRows = True Then
                While dbreader.Read = True
                    Try
                        StatusLabel.Text = "Checking Database for Unused Accounts: " & dbreader.GetString(0) '
                        'check if account is expired based on SAN_Account_Created
                        Dim createddatestr As String
                        createddatestr = dbreader.GetString(1)
                        Dim createddate As Date = New Date(Integer.Parse(createddatestr.Substring(0, 4)), Integer.Parse(createddatestr.Substring(4, 2)), Integer.Parse(createddatestr.Substring(6, 2)), Integer.Parse(createddatestr.Substring(8, 2)), Integer.Parse(createddatestr.Substring(10, 2)), Integer.Parse(createddatestr.Substring(12, 2)), DateTimeKind.Local)
                        Dim nowdate As Date = Now
                        If DateDiff(DateInterval.Day, createddate, nowdate) > 60 Then
                            If Long.Parse(dbreader.GetString(3)) = 0 And Long.Parse(dbreader.GetString(4)) = 0 Then
                                If My.Computer.FileSystem.DirectoryExists((SAN_Folder.Text & "\Removed").Replace("\\", "\")) = False Then
                                    My.Computer.FileSystem.CreateDirectory((SAN_Folder.Text & "\Removed").Replace("\\", "\"))
                                End If
                                If My.Computer.FileSystem.DirectoryExists((SAN_Folder.Text & "\Removed\" & Format(Now, "yyyyMMdd")).Replace("\\", "\")) = False Then
                                    My.Computer.FileSystem.CreateDirectory((SAN_Folder.Text & "\Removed\" & Format(Now, "yyyyMMdd")).Replace("\\", "\"))
                                End If
                                If My.Computer.FileSystem.DirectoryExists((SAN_Folder.Text & "\" & dbreader.GetString(2) & "\" & dbreader.GetString(0)).Replace("\\", "\")) = True Then
                                    My.Computer.FileSystem.MoveDirectory((SAN_Folder.Text & "\" & dbreader.GetString(2) & "\" & dbreader.GetString(0)).Replace("\\", "\"), (SAN_Folder.Text & "\Removed\" & Format(Now, "yyyyMMdd") & "\" & dbreader.GetString(0)).Replace("\\", "\"))
                                    emaillist3 = emaillist3 & ";" & dbreader.GetString(5)
                                    If emaillist3.StartsWith(";") = True Then
                                        emaillist3 = emaillist3.Remove(0, 1)
                                    End If
                                End If
                                Dim dbsql2 As OleDb.OleDbCommand = New OleDb.OleDbCommand
                                dbsql2.CommandText = "Update SAN_User_Records set SAN_Active = 'Removed' where SAN_Username = '" & dbreader.GetString(0) & "'"
                                dbsql2.Connection = dbconnection
                                dbsql2.ExecuteNonQuery()
                                dbsql2.Dispose()
                                dbsql2 = Nothing
                                Activity_Handler("Unused Account Removed: " & dbreader.GetString(0))
                            End If
                        End If
                    Catch ex As Exception
                        Error_Handler(ex, "Remove Unused Accounts")
                    End Try
                End While
            End If
            dbreader.Close()
            dbreader = Nothing
            dbsql.Dispose()
            dbsql = Nothing
            dbconnection.Close()
            dbconnection.Dispose()
            dbconnection = Nothing


            StatusLabel.Text = "Checking Database for Expired Temp Accounts Completed"

        Catch ex As Exception
            Error_Handler(ex, "Remove Expired Temp Accounts")
        End Try
    End Sub

    Private Sub RemoveOrphanedFolders()
        Try
            StatusLabel.Text = "Checking Root Folder for Orphaned Accounts"
            Dim dbconnection As OleDb.OleDbConnection = New OleDb.OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=""" & SAN_Database.Text & """")
            dbconnection.Open()

            Dim dinfo As DirectoryInfo
            dinfo = New DirectoryInfo((SAN_Folder.Text & "\Staff").Replace("\\", "\"))
            For Each folder As DirectoryInfo In dinfo.GetDirectories
                StatusLabel.Text = "Checking Root Folder for Orphaned Accounts: " & folder.Name
                Dim dbsql As OleDb.OleDbCommand = New OleDb.OleDbCommand
                dbsql.CommandText = "Select SAN_Username from SAN_User_Records where SAN_Username = '" & folder.Name & "'"
                dbsql.Connection = dbconnection
                Dim result As String = dbsql.ExecuteScalar()
                If result Is Nothing Then
                    If My.Computer.FileSystem.DirectoryExists((SAN_Folder.Text & "\Removed").Replace("\\", "\")) = False Then
                        My.Computer.FileSystem.CreateDirectory((SAN_Folder.Text & "\Removed").Replace("\\", "\"))
                    End If
                    If My.Computer.FileSystem.DirectoryExists((SAN_Folder.Text & "\Removed\" & Format(Now, "yyyyMMdd")).Replace("\\", "\")) = False Then
                        My.Computer.FileSystem.CreateDirectory((SAN_Folder.Text & "\Removed\" & Format(Now, "yyyyMMdd")).Replace("\\", "\"))
                    End If
                    My.Computer.FileSystem.MoveDirectory(folder.FullName, (SAN_Folder.Text & "\Removed\" & Format(Now, "yyyyMMdd") & "\" & folder.Name).Replace("\\", "\"))
                    Activity_Handler("Removed Orphan Folder: " & folder.Name)
                End If
                dbsql.Dispose()
                dbsql = Nothing
                folder = Nothing
            Next
            dinfo = Nothing
            dinfo = New DirectoryInfo((SAN_Folder.Text & "\Students").Replace("\\", "\"))
            For Each folder As DirectoryInfo In dinfo.GetDirectories
                StatusLabel.Text = "Checking Root Folder for Orphaned Accounts: " & folder.Name
                Dim dbsql As OleDb.OleDbCommand = New OleDb.OleDbCommand
                dbsql.CommandText = "Select SAN_Username from SAN_User_Records where SAN_Username = '" & folder.Name & "'"
                dbsql.Connection = dbconnection
                Dim result As String = dbsql.ExecuteScalar()
                If result Is Nothing Then
                    If My.Computer.FileSystem.DirectoryExists((SAN_Folder.Text & "\Removed").Replace("\\", "\")) = False Then
                        My.Computer.FileSystem.CreateDirectory((SAN_Folder.Text & "\Removed").Replace("\\", "\"))
                    End If
                    If My.Computer.FileSystem.DirectoryExists((SAN_Folder.Text & "\Removed\" & Format(Now, "yyyyMMdd")).Replace("\\", "\")) = False Then
                        My.Computer.FileSystem.CreateDirectory((SAN_Folder.Text & "\Removed\" & Format(Now, "yyyyMMdd")).Replace("\\", "\"))
                    End If
                    My.Computer.FileSystem.MoveDirectory(folder.FullName, (SAN_Folder.Text & "\Removed\" & Format(Now, "yyyyMMdd") & "\" & folder.Name).Replace("\\", "\"))
                    Activity_Handler("Removed Orphan Folder: " & folder.Name)
                End If
                dbsql.Dispose()
                dbsql = Nothing
                folder = Nothing
            Next
            dinfo = Nothing

            dbconnection.Close()
            dbconnection.Dispose()
            dbconnection = Nothing
            StatusLabel.Text = "Checking Root Folder for Orphaned Accounts Completed"
        Catch ex As Exception
            Error_Handler(ex, "Remove Orphaned Folders")
        End Try
    End Sub

    Private Sub EmailNewAccounts()
        Try
            If emaillist.Length > 0 Then
                Dim body As String
                body = "Thank you for registering your Commerce SAN account. Your account has been successfully generated and you should now be able to access your account by using the SAN Access software that can be downloaded from either the official Commerce SAN site (http://comsan.uct.ac.za) or from the Commerce IT website (http://www.commerce.uct.ac.za/Commerce_IT)."
                TextMail(SAN_MailServer, SAN_FromAddress, emaillist, "SAN Account Creation Complete", body, "Email New Accounts")
                StatusLabel.Text = "Notifying New Account Holders: " & emaillist
            End If
        Catch ex As Exception
            Error_Handler(ex, "Send Registration Complete Mail")
        End Try
    End Sub

    Private Sub EmailDeletedAccounts()
        Try
            If emaillist2.Length > 0 Then
                Dim body As String
                body = "The following notification email serves to inform you that you have been detected as no longer belonging to the Commerce Faculty and your relevant Commerce SAN folder has therefore been removed."
                TextMail(SAN_MailServer, SAN_FromAddress, emaillist2, "SAN Account Removed", body, "Email Deleted Accounts")
                StatusLabel.Text = "Notifying Removed Account Holders: " & emaillist2
            End If
        Catch ex As Exception
            Error_Handler(ex, "Send Deleted Account Notification Mail")
        End Try
    End Sub

    Private Sub EmailExpiringAccounts()
        Try
            If emaillist3.Length > 0 Then
                Dim body As String
                body = "The following notification email serves to inform you that your account has been detected as having been inactive and unused for over 30 days, meaning that your relevant Commerce SAN folder has subsequently been removed."
                TextMail(SAN_MailServer, SAN_FromAddress, emaillist3, "Inactive SAN Account Removed", body, "Email Expiring Accounts")
                StatusLabel.Text = "Notifying Expiring Account Holders: " & emaillist3
            End If
        Catch ex As Exception
            Error_Handler(ex, "Send Expiring Account Notification Mail")
        End Try
    End Sub

    Private Sub CheckValidDatabaseEntries()
        Try
            StatusLabel.Text = "Checking Database for Valid Entries"
            Dim dbconnection As OleDb.OleDbConnection = New OleDb.OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=""" & SAN_Database.Text & """")
            dbconnection.Open()
            Dim dbsql As OleDb.OleDbCommand = New OleDb.OleDbCommand
            dbsql.CommandText = "Select SAN_Username, SAN_Context, SAN_Account_Type, SAN_Email_Address from SAN_User_Records where not SAN_Active = 'False' and not SAN_Active = 'Removed' and not SAN_Tag = 'Temporary Account'"
            dbsql.Connection = dbconnection
            Dim dbreader As OleDb.OleDbDataReader = dbsql.ExecuteReader()
            If dbreader.HasRows = True Then
                While dbreader.Read = True
                    StatusLabel.Text = "Checking Database for Valid Entries: " & dbreader.GetString(0)
                    Dim tempdownloadname As String = (Application.StartupPath & "\isvaliduser").Replace("\\", "\")
                    My.Computer.Network.DownloadFile("http://comsan.uct.ac.za/Web/SAN%20Access/Services/isuservalid.php?SAN_Username=" & dbreader.GetString(0) & "&SAN_Context=" & dbreader.GetString(1), tempdownloadname, "", "", False, 100000, True)
                    If My.Computer.FileSystem.FileExists(tempdownloadname) = True Then
                        Dim result As String = My.Computer.FileSystem.ReadAllText(tempdownloadname)
                        If result.Trim = "0" Then
                            If My.Computer.FileSystem.DirectoryExists((SAN_Folder.Text & "\Removed").Replace("\\", "\")) = False Then
                                My.Computer.FileSystem.CreateDirectory((SAN_Folder.Text & "\Removed").Replace("\\", "\"))
                            End If
                            If My.Computer.FileSystem.DirectoryExists((SAN_Folder.Text & "\Removed\" & Format(Now, "yyyyMMdd")).Replace("\\", "\")) = False Then
                                My.Computer.FileSystem.CreateDirectory((SAN_Folder.Text & "\Removed\" & Format(Now, "yyyyMMdd")).Replace("\\", "\"))
                            End If
                            If My.Computer.FileSystem.DirectoryExists((SAN_Folder.Text & "\" & dbreader.GetString(2) & "\" & dbreader.GetString(0)).Replace("\\", "\")) = True Then
                                My.Computer.FileSystem.MoveDirectory((SAN_Folder.Text & "\" & dbreader.GetString(2) & "\" & dbreader.GetString(0)).Replace("\\", "\"), (SAN_Folder.Text & "\Removed\" & Format(Now, "yyyyMMdd") & "\" & dbreader.GetString(0)).Replace("\\", "\"))
                                emaillist2 = emaillist2 & ";" & dbreader.GetString(3)
                                If emaillist2.StartsWith(";") = True Then
                                    emaillist2 = emaillist2.Remove(0, 1)
                                End If
                            End If
                            Dim dbsql2 As OleDb.OleDbCommand = New OleDb.OleDbCommand
                            dbsql2.CommandText = "Update SAN_User_Records set SAN_Active = 'Removed' where SAN_Username = '" & dbreader.GetString(0) & "'"
                            dbsql2.Connection = dbconnection
                            dbsql2.ExecuteNonQuery()
                            dbsql2.Dispose()
                            dbsql2 = Nothing
                            Activity_Handler("Invalid Account Located and Removed: " & dbreader.GetString(0))
                        End If
                    End If
                End While
            End If
            dbreader.Close()
            dbreader = Nothing
            dbsql.Dispose()
            dbsql = Nothing
            dbconnection.Close()
            dbconnection.Dispose()
            dbconnection = Nothing


            StatusLabel.Text = "Checking Database for Valid Entries Completed"


        Catch ex As Exception
            Error_Handler(ex, "Check Valid Database Entries")
        End Try
    End Sub

    Private Sub SendNotificationEmail(ByVal StartOrClose As String)
        Try
            StatusLabel.Text = "Sending " & StartOrClose & " Notification"
            Dim body As String
            If StartOrClose = "Startup" Then
                body = "This is just a notification message to inform you that " & My.Application.Info.ProductName & " has been successfully started up."
            Else
                body = "This is just a notification message to inform you that " & My.Application.Info.ProductName & " has been shutdown."
            End If

            body = body & vbCrLf & vbCrLf & "******************************" & vbCrLf & vbCrLf & "This is an auto-generated email submitted from " & My.Application.Info.ProductName & " at " & Format(Now(), "dd/MM/yyyy hh:mm:ss tt") & ", running on:"
            body = body & vbCrLf & vbCrLf & "Machine Name: " + Environment.MachineName
            body = body & vbCrLf & "OS Version: " & Environment.OSVersion.ToString()
            body = body & vbCrLf & "User Name: " + Environment.UserName

            If StartOrClose = "Startup" Then
                TextMail(SAN_MailServer, SAN_FromAddress, SAN_ReportToAddress, "SAN FMS 2: Application Startup", body, "Application Startup")
            Else
                TextMail(SAN_MailServer, SAN_FromAddress, SAN_ReportToAddress, "SAN FMS 2: Application Shutdown", body, "Application Shutdown")
            End If

        Catch ex As Exception
            Error_Handler(ex, "Send Notification Email")
        End Try
    End Sub


    Private Sub UpdateLastScanVariable()
        Try
            StatusLabel.Text = "Updating ""Last Scan"" Variable"
            Dim dbconnection As OleDb.OleDbConnection = New OleDb.OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=""" & SAN_Database.Text & """")
            dbconnection.Open()

            Dim dbsql As OleDb.OleDbCommand = New OleDb.OleDbCommand
            dbsql.CommandText = "Update SAN_FMS set SAN_Last_Scan = '" & Format(Now, "dd/MM/yyyy hh:mm:ss") & "' where SAN_Instance = 'SAN FMS2'"
            dbsql.Connection = dbconnection
            dbsql.ExecuteNonQuery()
            dbsql.Dispose()
            dbsql = Nothing
            dbconnection.Close()
            dbconnection.Dispose()
            dbconnection = Nothing
            Activity_Handler("Last Scan Variable Successfully Set (" & Format(Now, "dd/MM/yyyy hh:mm:ss") & ")")
            StatusLabel.Text = """Last Scan"" Variable Updated"
        Catch ex As Exception
            Error_Handler(ex, "Updating ""Last Scan"" Variable")
        End Try
    End Sub

    Private Sub FolderCounter(ByVal targetdir As String)
        Try
            Dim dinfo As DirectoryInfo = New DirectoryInfo(targetdir)
            For Each finfo As FileInfo In dinfo.GetFiles
                If finfo.Name.ToLower = "thumbs.db" Or finfo.Name.ToLower = "desktop.ini" Then
                    finfo.Delete()
                Else
                    currentuserusage = currentuserusage + finfo.Length
                    currentuserfiles = currentuserfiles + 1
                End If
                finfo = Nothing
            Next
            For Each dinfo2 As DirectoryInfo In dinfo.GetDirectories
                If dinfo2.Name.StartsWith("_v") = False Then
                    currentuserfolders = currentuserfolders + 1
                    FolderCounter(dinfo2.FullName)
                End If
                dinfo2 = Nothing
            Next
            dinfo = Nothing
        Catch ex As Exception
            Error_Handler(ex, "Folder Counter (" & targetdir & ")")
        End Try
    End Sub

    Private Sub UpdateDatabaseUserStats()
        Try
            StatusLabel.Text = "Updating Database User Stats"
            Dim dbconnection As OleDb.OleDbConnection = New OleDb.OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=""" & SAN_Database.Text & """")
            dbconnection.Open()
            Dim stafflimit, studentlimit As Long
            stafflimit = 0
            studentlimit = 0
            Dim dbsql3 As OleDb.OleDbCommand = New OleDb.OleDbCommand
            dbsql3.CommandText = "Select SAN_Staff_Limit, SAN_Student_Limit from SAN_Global_Settings where SAN_Globals_Identifier = 'SAN1'"
            dbsql3.Connection = dbconnection
            Dim dbreader3 As OleDb.OleDbDataReader = dbsql3.ExecuteReader()
            If dbreader3.HasRows = True Then
                While dbreader3.Read = True
                    stafflimit = Long.Parse(dbreader3.GetString(0))
                    studentlimit = Long.Parse(dbreader3.GetString(1))
                End While
            End If
            dbreader3.Close()
            dbreader3 = Nothing
            dbsql3.Dispose()
            dbsql3 = Nothing

                    Dim dbsql As OleDb.OleDbCommand = New OleDb.OleDbCommand
            dbsql.CommandText = "Select SAN_Username, SAN_Account_Type, SAN_Additional_Limit from SAN_User_Records where not SAN_Active = 'Removed'"
                    dbsql.Connection = dbconnection
                    Dim dbreader As OleDb.OleDbDataReader = dbsql.ExecuteReader()
                    If dbreader.HasRows = True Then
                        While dbreader.Read = True
                            StatusLabel.Text = "Updating Database User Stats: " & dbreader.GetString(0)
                            currentuserfiles = 0
                            currentuserfolders = 0
                            currentuserusage = 0
                            If My.Computer.FileSystem.DirectoryExists((SAN_Folder.Text & "\" & dbreader.GetString(1) & "\" & dbreader.GetString(0)).Replace("\\", "\")) = True Then
                                FolderCounter((SAN_Folder.Text & "\" & dbreader.GetString(1) & "\" & dbreader.GetString(0)).Replace("\\", "\"))
                    End If
                    Dim currentuserlimit As Long = 0
                    If dbreader.GetString(1) = "Staff" Then
                        currentuserlimit = stafflimit + Long.Parse(dbreader.GetString(2))
                    Else
                        currentuserlimit = studentlimit + Long.Parse(dbreader.GetString(2))
                    End If
                            Dim dbsql2 As OleDb.OleDbCommand = New OleDb.OleDbCommand
                    dbsql2.CommandText = "Update SAN_User_Records set SAN_Total_Files = '" & currentuserfiles & "', SAN_Total_Folders = '" & currentuserfolders & "', SAN_Total_Usage = '" & currentuserusage & "', SAN_Total_Limit = '" & currentuserlimit & "' where SAN_Username = '" & dbreader.GetString(0) & "'"
                            dbsql2.Connection = dbconnection
                            dbsql2.ExecuteNonQuery()
                            dbsql2.Dispose()
                            dbsql2 = Nothing
                        End While
                    End If
                    dbreader.Close()
                    dbreader = Nothing
                    dbsql.Dispose()
                    dbsql = Nothing
                    dbconnection.Close()
                    dbconnection.Dispose()
                    dbconnection = Nothing

                    Activity_Handler("Database User Stats Successfully Set")
                    StatusLabel.Text = "Database User Stats Updated"

        Catch ex As Exception
            Error_Handler(ex, "Updating Database User Stats")
        End Try
    End Sub

    Private Sub Button_GenerateATempXXXAccounts_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button_GenerateATempXXXAccounts.Click
        Try
            Timer1.Stop()
            StatusLabel.Text = "Generate A-TEMPXXX Accounts Requested"
            Dim dialog As A_TempAccountCreation = New A_TempAccountCreation()
            dialog.a_tempaccountformat.Text = "A-TEMP"
            dialog.a_tempaccountstart.Value = 1
            dialog.a_tempaccountend.Value = 100
            dialog.a_tempaccountpassword.Text = "summer"
            If dialog.ShowDialog() = Windows.Forms.DialogResult.OK Then
                If dialog.a_tempaccountformat.Text.Length > 0 Then
                    If dialog.a_tempaccountend.Value >= dialog.a_tempaccountstart.Value Then
                        a_tempaccountformat = dialog.a_tempaccountformat.Text
                        a_tempaccountpassword = dialog.a_tempaccountpassword.Text
                        a_tempaccountstart = dialog.a_tempaccountstart.Value
                        a_tempaccountend = dialog.a_tempaccountend.Value
                        a_tempaccountnumber = (a_tempaccountend - a_tempaccountstart) + 1

                        runworker2()
                    Else
                        MsgBox("Please note that you need to specify a lower start value than end value for your requested temp accounts.", MsgBoxStyle.Information, "Incorrect Setting")
                        Timer1.Start()
                    End If
                Else
                    MsgBox("Please note that you need to specify a name for your requested temp accounts.", MsgBoxStyle.Information, "Incorrect Setting")
                    Timer1.Start()
                End If
            Else
                StatusLabel.Text = "Generate A-TEMPXXX Accounts Cancelled"
                Timer1.Start()
            End If
            dialog.Dispose()
            dialog = Nothing
        Catch ex As Exception
            Error_Handler(ex, "Generate A-TEMPXXX Accounts")
        End Try
    End Sub

    Private Sub runworker2()
        Try
            If busyworking = False Then
                busyworking = True
                Control_Enabler(False)
                Timer1.Stop()
                StatusLabel.Text = "Initializing A-TEMPXXX Accounts Generation"
                Activity_Handler("--- A-TEMPXXX Accounts Generation Started ---")
                BackgroundWorker2.RunWorkerAsync()
            End If
        Catch ex As Exception
            Error_Handler(ex, "Run Worker 2: A-TEMPXXX Accounts Generation")
        End Try
    End Sub

    Private Sub BackgroundWorker2_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles BackgroundWorker2.DoWork
        Try
            StatusLabel.Text = "Generating New A-TEMPXXX Accounts"
            CreateNewA_TEMPFolders()
            ProgressBar1.Value = 100
            e.Result = "Success"
        Catch ex As Exception
            Error_Handler(ex, "A-TEMPXXX Accounts Generation")
        End Try
    End Sub

    Private Sub BackgroundWorker2_RunWorkerCompleted(ByVal sender As System.Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles BackgroundWorker2.RunWorkerCompleted
        Try
            Control_Enabler(True)
            If e.Cancelled = False And e.Error Is Nothing Then
                StatusLabel.Text = "A-TEMPXXX Accounts Generation Operation Complete"
            Else
                StatusLabel.Text = "A-TEMPXXX Accounts Generation Failed"
            End If
            Activity_Handler("--- A-TEMPXXX Accounts Generation Ended ---")
            busyworking = False
            Timer1.Start()
            'Forcing operation to validate newly generated accounts
            StatusLabel.Text = "FMS Operation Forced"
            Timer1.Stop()
            runworker()
            TimerLabel.Text = "0900"
        Catch ex As Exception
            Error_Handler(ex, "A-TEMPXXX Accounts Generation Complete")
        End Try
    End Sub

    Function getMd5Hash(ByVal input As String) As String
        ' Create a new instance of the MD5 object.
        Dim md5Hasher As MD5 = MD5.Create()

        ' Convert the input string to a byte array and compute the hash.
        Dim data As Byte() = md5Hasher.ComputeHash(Encoding.Default.GetBytes(input))

        ' Create a new Stringbuilder to collect the bytes
        ' and create a string.
        Dim sBuilder As New StringBuilder()

        ' Loop through each byte of the hashed data 
        ' and format each one as a hexadecimal string.
        Dim i As Integer
        For i = 0 To data.Length - 1
            sBuilder.Append(data(i).ToString("x2"))
        Next i

        ' Return the hexadecimal string.
        Return sBuilder.ToString()

    End Function

    Private Sub CreateNewA_TEMPFolders()
        Try
            StatusLabel.Text = "Generating New A-TEMPXXX Accounts"
            Dim dbconnection As OleDb.OleDbConnection = New OleDb.OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=""" & SAN_Database.Text & """")
            dbconnection.Open()
            Dim dbsql As OleDb.OleDbCommand = New OleDb.OleDbCommand
            dbsql.Connection = dbconnection
            Dim dbsqlq As OleDb.OleDbCommand = New OleDb.OleDbCommand
            dbsqlq.Connection = dbconnection
            Dim accountscreated As Integer = 0

            Dim MD5accountpassword As String = getMd5Hash(a_tempaccountpassword)
            Dim tmpusername, tmpemailaddress, tmpcontext, tmpname, numberstring As String
            For counter As Integer = a_tempaccountstart To a_tempaccountend
                Try
                    numberstring = counter.ToString
                    While numberstring.Length < 3
                        numberstring = "0" & numberstring
                    End While
                    tmpusername = (a_tempaccountformat & numberstring).ToUpper
                    tmpemailaddress = tmpusername & "@uct.ac.za"
                    tmpcontext = tmpusername & ".Students.com.main.uct"
                    tmpname = tmpusername & " Temporary Account"


                    '****************
                    dbsqlq.CommandText = "Select SAN_Username from SAN_User_Records where SAN_Username = '" & tmpusername & "'"
                    Dim dbreader As OleDb.OleDbDataReader = dbsqlq.ExecuteReader()
                    If dbreader.HasRows = False Then
                        dbsql.CommandText = "Insert into SAN_User_Records (SAN_Username, SAN_Password, SAN_ClearPass, SAN_Active, SAN_Additional_Limit, SAN_Account_Type, SAN_Email_Address, SAN_Context, SAN_Name, SAN_Tag, SAN_Account_Created) values ('" & tmpusername & "','" & MD5accountpassword & "','" & a_tempaccountpassword & "','False','0','Students','" & tmpemailaddress & "','" & tmpcontext & "','" & tmpname & "','A-TEMPXXX','" & Format(Now(), "yyyyMMddhhmmss") & "')"
                        dbsql.ExecuteNonQuery()
                        accountscreated = accountscreated + 1
                        Activity_Handler("Registered account for: " & tmpusername)
                    End If
                    dbreader.Close()
                    dbreader = Nothing
                 
                    '****************

                    ProgressBar1.Value = Math.Round(((accountscreated / a_tempaccountnumber) * 100), 0)
                Catch ex As Exception
                    Error_Handler(ex, "Create New A-TEMPXXX Folders: " & tmpusername)
                End Try
            Next
            dbsqlq.Dispose()
            dbsqlq = Nothing
            dbsql.Dispose()
            dbsql = Nothing
            dbconnection.Close()
            dbconnection.Dispose()
            dbconnection = Nothing
            StatusLabel.Text = "Generating New A-TEMPXXX Accounts Completed"
        Catch ex As Exception
            Error_Handler(ex, "Create New A-TEMPXXX Folders")
        End Try
    End Sub

    Private Sub Button_RemoveATempXXXAccounts_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button_RemoveATempXXXAccounts.Click
        Try
            Timer1.Stop()
            StatusLabel.Text = "Remove A-TEMPXXX Accounts Requested"
            Dim dialog As A_TempAccountCreation = New A_TempAccountCreation()
            dialog.a_tempaccountformat.Text = "A-TEMP"
            dialog.a_tempaccountstart.Value = 1
            dialog.a_tempaccountend.Value = 100
            dialog.a_tempaccountpassword.Text = "summer"
            dialog.a_tempaccountpassword.Visible = False
            If dialog.ShowDialog() = Windows.Forms.DialogResult.OK Then
                If dialog.a_tempaccountformat.Text.Length > 0 Then
                    If dialog.a_tempaccountend.Value >= dialog.a_tempaccountstart.Value Then
                        a_tempaccountformat = dialog.a_tempaccountformat.Text
                        a_tempaccountpassword = dialog.a_tempaccountpassword.Text
                        a_tempaccountstart = dialog.a_tempaccountstart.Value
                        a_tempaccountend = dialog.a_tempaccountend.Value
                        a_tempaccountnumber = (a_tempaccountend - a_tempaccountstart) + 1
                        runworker3()
                    Else
                        MsgBox("Please note that you need to specify a lower start value than end value for your requested temp accounts.", MsgBoxStyle.Information, "Incorrect Setting")
                        Timer1.Start()
                    End If
                Else
                    MsgBox("Please note that you need to specify a name for your requested temp accounts.", MsgBoxStyle.Information, "Incorrect Setting")
                    Timer1.Start()
                End If
            Else
                StatusLabel.Text = "Remove A-TEMPXXX Accounts Cancelled"
                Timer1.Start()
            End If
            dialog.Dispose()
            dialog = Nothing
        Catch ex As Exception
            Error_Handler(ex, "Remove A-TEMPXXX Accounts")
        End Try
    End Sub

    Private Sub runworker3()
        Try
            If busyworking = False Then
                busyworking = True
                Control_Enabler(False)
                Timer1.Stop()
                StatusLabel.Text = "Initializing A-TEMPXXX Accounts Removal"
                Activity_Handler("--- A-TEMPXXX Accounts Removal Started ---")
                BackgroundWorker3.RunWorkerAsync()
            End If
        Catch ex As Exception
            Error_Handler(ex, "Run Worker 3: A-TEMPXXX Accounts Removal")
        End Try
    End Sub

    Private Sub BackgroundWorker3_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles BackgroundWorker3.DoWork
        Try
            StatusLabel.Text = "Removing Existing A-TEMPXXX Accounts"
            RemoveExistingA_TEMPFolders()
            ProgressBar1.Value = 100
            e.Result = "Success"
        Catch ex As Exception
            Error_Handler(ex, "A-TEMPXXX Accounts Removal")
        End Try
    End Sub

    Private Sub BackgroundWorker3_RunWorkerCompleted(ByVal sender As System.Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles BackgroundWorker3.RunWorkerCompleted
        Try
            Control_Enabler(True)
            If e.Cancelled = False And e.Error Is Nothing Then
                StatusLabel.Text = "A-TEMPXXX Accounts Removal Operation Complete"
            Else
                StatusLabel.Text = "A-TEMPXXX Accounts Removal Failed"
            End If
            Activity_Handler("--- A-TEMPXXX Accounts Removal Ended ---")
            busyworking = False
            Timer1.Start()
            'Forcing operation to validate newly generated accounts
            StatusLabel.Text = "FMS Operation Forced"
            Timer1.Stop()
            runworker()
            TimerLabel.Text = "0900"
        Catch ex As Exception
            Error_Handler(ex, "A-TEMPXXX Accounts Removal Complete")
        End Try
    End Sub

    Private Sub RemoveExistingA_TEMPFolders()
        Try
            StatusLabel.Text = "Removing Existing A-TEMPXXX Accounts"
            Dim dbconnection As OleDb.OleDbConnection = New OleDb.OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=""" & SAN_Database.Text & """")
            dbconnection.Open()
            Dim dbsql As OleDb.OleDbCommand = New OleDb.OleDbCommand
            dbsql.Connection = dbconnection
            Dim accountscreated As Integer = 0

            Dim tmpusername, numberstring As String
            For counter As Integer = a_tempaccountstart To a_tempaccountend
                Try
                    numberstring = counter.ToString
                    While numberstring.Length < 3
                        numberstring = "0" & numberstring
                    End While
                    tmpusername = a_tempaccountformat & numberstring
                    dbsql.CommandText = "Delete from SAN_User_Records where SAN_Username = '" & tmpusername & "'"
                    dbsql.ExecuteNonQuery()
                    accountscreated = accountscreated + 1
                    Activity_Handler("Removed account for: " & tmpusername)
                    ProgressBar1.Value = Math.Round(((accountscreated / a_tempaccountnumber) * 100), 0)
                Catch ex As Exception
                    Error_Handler(ex, "Remove Existing A-TEMPXXX Folders: " & tmpusername)
                End Try
            Next
            dbsql.Dispose()
            dbsql = Nothing
            dbconnection.Close()
            dbconnection.Dispose()
            dbconnection = Nothing
            StatusLabel.Text = "Removing Existing A-TEMPXXX Accounts Completed"
        Catch ex As Exception
            Error_Handler(ex, "Remove Existing A-TEMPXXX Folders")
        End Try
    End Sub
End Class
