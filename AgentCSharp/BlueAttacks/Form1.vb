Public Class Form1
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'MsgBox(e.ToString)
    End Sub

    Private Sub Label1_Click(sender As Object, e As EventArgs) Handles Label1.Click

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        ' Read from C:\blueattackconfig
        Dim Serverip As String
        'Serverip = My.Computer.FileSystem.ReadAllText("C:\blueattackconfig\config.txt")
        'MsgBox(Serverip)
        'Serverip = "127.0.0.1"

        Dim username As String = TextBox1.Text
        Dim pass As String = TextBox2.Text
        ' call login API 
        'Net.ServicePointManager.ServerCertificateValidationCallback = Function(a, b, c, d) True

        Dim request As String = String.Format("https://" + Serverip + "/attacktesting/api/checkloginNew.php?email=" + username + "&pass=" + pass)
        Dim webClient As New System.Net.WebClient
        Dim result As String = webClient.DownloadString(request)

        If result.Equals("") Then
            MsgBox("Error ! Please Check Username or Password")
        Else
            'MsgBox("Good")
            ''get host IP 

            Dim MyForm As New afterlogin
            Me.Visible = False
            Dim userid As String = result
            Label1.Text = result
            MyForm.Show()

        End If


    End Sub

    Private Sub Form1_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        ' If (MessageBox.Show("Close?", "", MessageBoxButtons.YesNo) = Windows.Forms.DialogResult.No) Then
        'e.Cancel = True
        'MsgBox("bye")
        'End If
    End Sub


End Class
