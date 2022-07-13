using System.Net;

namespace BlueAttackCSharp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            StreamWriter streamWriter = new StreamWriter("file path");
            //ServicePointManager.ServerCertificateValidationCallback = Func<s, c, h, ee>();
        }
    }
}