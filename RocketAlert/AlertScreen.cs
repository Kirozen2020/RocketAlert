using System;
using System.Windows.Forms;

namespace RocketAlert
{
    public partial class AlertScreen : Form
    {
        public string placeName {  get; set; }

        public AlertScreen()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            timer1.Stop();
            this.Close();
        }

        private void AlertScreen_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            lblNamePlace.Text = placeName;
            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Stop();
            this.Close();
        }
    }
}
