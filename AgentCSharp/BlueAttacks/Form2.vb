Imports System
Imports System.IO

Public Class afterlogin
    Private Const V As Integer = 3
    Dim userid As String
    Dim strComputerName As String = Environment.MachineName.ToString()
    Dim t As New System.Timers.Timer(1000)

    Private Sub afterlogin_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim x As String = Form1.TextBox1.Text
        Label1.Text = "Welcom " + x
        ' create folder 
        Dim di As DirectoryInfo = New DirectoryInfo("c:\blueattacksfolder")

        Try
            ' Determine whether the directory exists.
            If di.Exists Then
                ' Indicate that it already exists.
                Console.WriteLine("That path exists already.")
                Return
            End If

            ' Try to create the directory.
            di.Create()
            Console.WriteLine("The directory was created successfully.")

            ' Delete the directory.

        Catch er As Exception
            Console.WriteLine("The process failed: {0}", er.ToString())
        End Try

    End Sub

    Public Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Button1.Enabled = False
        Button2.Enabled = True
        ' call the API Get Command each 1 sec to get any commend then exe
        Dim minutes As Integer = 1   ' change 5 into the count of minutes
        userid = Form1.Label1.Text
        'MsgBox(userid)
        AddHandler t.Elapsed, AddressOf t_Elapsed
        t.Start() ' start the timer
        'MsgBox("ok2")

    End Sub
    Public Sub t_Elapsed(sender As Object, e As System.Timers.ElapsedEventArgs)
        ' get host name 
        'get userid

        'ByPass SSL Certificate Validation Checking
        System.Net.ServicePointManager.ServerCertificateValidationCallback = Function(s, c, h, ee) True


        Dim Serverip As String
        Serverip = My.Computer.FileSystem.ReadAllText("C:\blueattackconfig\config.txt")


        Dim request As String = String.Format("https://" + Serverip + "/attacktesting/api/getcommand.php?hostname=" + strComputerName + "&userid=" + userid)
        Dim webClient As New System.Net.WebClient
        Dim result As String = webClient.DownloadString(request)
        Dim flag As String = "f"
        ' exe command from server 
        If Not result.Equals("") Then
            'MsgBox(result)
            ' MsgBox(result)

            Dim allstring As String() = result.Split(" _X_ ")
            '   MsgBox(allstring)   
            ' store command in bat file 
            Dim ln As Integer = allstring.Length()
            'MsgBox(ln)

            For i = 0 To ln - 2 Step 1
                Dim cmd As String = allstring(i)
                ' MsgBox(cmd)

                ' replace http://blueattack.com with IP from config file 
                cmd = cmd.Replace("http://blueattacks.com", "https://" + Serverip)

                Dim oProcess As New Process()

                Dim FILE_NAME As String = "C:\blueattacksfolder\" + allstring(ln - 1) + "_" + i.ToString + "c.bat"
                ' \\127.0.0.1\c$\blueattacksfolder 
                Dim foutput As String = "C:\blueattacksfolder\" + allstring(ln - 1) + "_" + i.ToString + "c.txt"
                Dim cmd2 As String
                Dim objWriter As New StreamWriter(FILE_NAME)
                cmd2 = cmd + "< NUL > " + foutput + " & type " + foutput
                objWriter.Write(cmd2)
                objWriter.WriteLine()
                objWriter.Write("exit")

                objWriter.Close()
                Try
                    Dim theProcessStartInfo As New ProcessStartInfo(FILE_NAME)
                    theProcessStartInfo.CreateNoWindow = True
                    theProcessStartInfo.UseShellExecute = False

                    theProcessStartInfo.WindowStyle = ProcessWindowStyle.Hidden
                    theProcessStartInfo.RedirectStandardOutput = True
                    theProcessStartInfo.RedirectStandardError = True


                    oProcess.StartInfo = theProcessStartInfo
                    oProcess.Start()



                    'MsgBox(std_err.ToString())
                    ' Display the results.
                    ' MsgBox("1")
                    Dim sOutput As String

                    Dim std_out As StreamReader = oProcess.StandardError()
                    ReadStandardError(std_out, allstring, ln)
                    'MsgBox("2")

                    ' Clean up.
                    ' std_out.Close()
                    'std_err.Close()
                    '  oProcess.Close()

                    ' MsgBox("out")
                    ' oProcess.Close()
                    'MsgBox("aaa")
                    'Dim xx As Int16 = 0
                    ' add new
                    Dim count = 0
                    'Dim sOutput As String
                    Using oStreamReader As System.IO.StreamReader = oProcess.StandardOutput

                        While True
                            'Threading.Thread.Sleep(1000)

                            sOutput = oStreamReader.ReadLine

                            '  MsgBox(sOutput)
                            'MsgBox(sOutput)
                            ' MsgBox(sOutput)
                            If sOutput Is Nothing Then

                                Exit While
                            End If
                            If sOutput.Contains("exit") Then

                                Exit While
                            Else
                                If sOutput.Contains("Copyright (C) Microsoft Corporation") Then
                                    System.Net.ServicePointManager.ServerCertificateValidationCallback = Function(s, c, h, ee) True
                                    'ignore SSL if the server is IP not domain 

                                    '  MsgBox(sOutput.ToString())

                                    '  Dim callback4 As String = "https://" + Serverip + "/attacktesting/api/print_cmd.php?networkattackid=" + allstring(ln - 1) + "&hostname=" + strComputerName + "&cmd=" + sOutput
                                    '  Dim webClient4 As New System.Net.WebClient
                                    '  Dim result4 As String = webClient.DownloadString(callback4)

                                    Exit While
                                End If
                                '  MsgBox(sOutput)
                                System.Net.ServicePointManager.ServerCertificateValidationCallback = Function(s, c, h, ee) True
                                'ignore SSL if the server is IP not domain 
                                ' MsgBox("alde")
                                '  MsgBox(sOutput.ToString())
                                '  Dim callback3 As String = "https://" + Serverip + "/attacktesting/api/print_cmd.php?networkattackid=" + allstring(ln - 1) + "&hostname=" + strComputerName + "&cmd=" + sOutput
                                ' Dim webClient3 As New System.Net.WebClient
                                '  Dim result3 As String = webClient.DownloadString(callback3)
                            End If
                        End While
                        '                        MsgBox("1")
                        webClient.UploadFile("https://" + Serverip + "/attacktesting/api/upload.php", foutput)
                        '                       MsgBox("2")

                        ' MsgBox(sOutput)
                    End Using





                    'Process.Start(FILE_NAME)
                    ' oProcess.Close()
                Catch ex As Exception
                    '   MsgBox("error")
                    '  MsgBox(ex.StackTrace
                    '     )
                    ' MsgBox("error1" + ex.Message)
                End Try
                ' Dim x As String = "ping google.com"

                ' call send cmd 
                ' Read from C:\blueattackconfig
                'Serverip = My.Computer.FileSystem.ReadAllText("C:\blueattackconfig\config.txt")
                System.Net.ServicePointManager.ServerCertificateValidationCallback = Function(s, c, h, ee) True
                'ignore SSL if the server is IP not domain 

                '   MsgBox("END")
                ' MsgBox("https://" + Serverip + "/attacktesting/api/sendback_cmd.php?networkattackid=" + allstring(ln - 1) + "&hostname=" + strComputerName + "&cmd=" + cmd)
                Dim cmdnew As String = ""
                cmdnew = cmd.Replace("&&", " %26%26 ")

                Dim callback As String = String.Format("https://" + Serverip + "/attacktesting/api/sendback_cmd.php?networkattackid=" + allstring(ln - 1) + "&hostname=" + strComputerName + "&cmd=" + cmdnew)
                Dim webClient2 As New System.Net.WebClient
                Dim result2 As String = webClient.DownloadString(callback)

            Next

        End If
        'Dim x As String = "ping google.com"
        'Process.Start("cmd", "/c " + x)

        ' send command 


    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        t.Stop()

        Button2.Enabled = False
        Button1.Enabled = True
        'MsgBox(strComputerName)
        'MsgBox(userid)
        ' Read from C:\blueattackconfig
        'ByPass SSL Certificate Validation Checking
        ' System.Net.ServicePointManager.ServerCertificateValidationCallback = Function(s, c, h, ee) True


        Dim Serverip As String
        Serverip = My.Computer.FileSystem.ReadAllText("C:\blueattackconfig\config.txt")

        Dim callback As String = String.Format("https://" + Serverip + "/attacktesting/api/update_agent.php?hostname=" + strComputerName + "&userid=" + userid)
        Dim webClient2 As New System.Net.WebClient
        Dim result2 As String = webClient2.DownloadString(callback)

    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        'MsgBox("logout")

        t.Stop()
        ' System.Net.ServicePointManager.ServerCertificateValidationCallback = Function(s, c, h, ee) True

        ' Read from C:\blueattackconfig
        Dim Serverip As String
        Serverip = My.Computer.FileSystem.ReadAllText("C:\blueattackconfig\config.txt")

        Dim callback As String = String.Format("https://" + Serverip + "/attacktesting/api/update_agent.php?hostname=" + strComputerName + "&userid=" + userid)
        Dim webClient2 As New System.Net.WebClient
        Dim result2 As String = webClient2.DownloadString(callback)
        'MsgBox(result2)
        Me.Visible = False
        Form1.Label1.Text = "User Name"
        Form1.Visible = True

    End Sub


    Private Sub afterlogin_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        ' If (MessageBox.Show("Close?", "", MessageBoxButtons.YesNo) = Windows.Forms.DialogResult.No) Then
        'e.Cancel = True
        'MsgBox("bye")
        'End If
        t.Stop()
        System.Net.ServicePointManager.ServerCertificateValidationCallback = Function(s, c, h, ee) True

        ' Read from C:\blueattackconfig
        Dim Serverip As String
        Serverip = My.Computer.FileSystem.ReadAllText("C:\blueattackconfig\config.txt")

        Dim callback As String = String.Format("https://" + Serverip + "/attacktesting/api/update_agent.php?hostname=" + strComputerName + "&userid=" + userid)
        Dim webClient2 As New System.Net.WebClient
        Dim result2 As String = webClient2.DownloadString(callback)
        'MsgBox("")

    End Sub

    Private Async Sub ReadStandardError(ByVal errorStream As StreamReader, all As String(), ln As Integer)
        Dim x As Double = 1
        Try

            '   MsgBox("before")
            Dim nextLine As String = Await errorStream.ReadLineAsync
            'MsgBox(nextLine)
            If nextLine IsNot Nothing Then
                ' MsgBox(nextLine)

                If nextLine.Contains("is not recognized as an internal or external command") = False And nextLine.Contains("to write to a nonexistent pipe") = False Then

                    System.Net.ServicePointManager.ServerCertificateValidationCallback = Function(s, c, h, ee) True

                    ' Read from C:\blueattackconfig
                    Dim Serverip As String
                    Serverip = My.Computer.FileSystem.ReadAllText("C:\blueattackconfig\config.txt")
                    ' MsgBox(all(ln - 1))
                    Dim callback As String = String.Format("https://" + Serverip + "/attacktesting/api/prevented.php?networkattackid=" + all(ln - 1) + "&prevented=yes")
                    Dim webClient2 As New System.Net.WebClient
                    Dim result2 As String = webClient2.DownloadString(callback)
                    ' MsgBox(result2)
                    'MsgBox("ok")
                    'ReadStandardError(errorStream, all, ln)

                End If
            End If
        Catch ex As Exception
            '   MsgBox("error2" + ex.Message)
        End Try

    End Sub


End Class