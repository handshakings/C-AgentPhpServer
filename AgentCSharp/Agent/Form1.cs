using System;
using System.Net;
using System.Net.Sockets;
using System.Net.WebSockets;
using System.Text;
using System.Windows.Forms;

namespace Agent
{
    public partial class Form1 : Form
    {
        Socket agent;
        IPAddress ipAddress = IPAddress.Parse("127.0.0.1");
        int port = 444;

        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            agent = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        }
        private void ClickEvent(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            if(button.Name == button1.Name)
            {
                Connnect();
            }
        }

        private async void Connnect()
        {      
            while (!agent.Connected)
            {
                try
                {
                    agent.Connect(ipAddress, port);
                    if (agent.Connected)
                    {
                        byte[] receiveBuffer = new byte[1024];
                        int byteReceived = agent.Receive(receiveBuffer);
                        Array.Resize(ref receiveBuffer, byteReceived);
                        string recMsg = Encoding.UTF8.GetString(receiveBuffer);
                        label1.Text = recMsg == "Connected" ? "Connected with " + ipAddress : "Failed to connnect";
                        string machineName = Environment.MachineName+" "+ipAddress;
                        byte[] sendBuffer = Encoding.UTF8.GetBytes(machineName);
                        agent.Send(sendBuffer, sendBuffer.Length, SocketFlags.None);

                    }
                }
                catch (Exception)
                {
                    continue;
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            WebSocket webSocket = new WebSocket(IPAddress.Any, 444);
        }
    }
}
