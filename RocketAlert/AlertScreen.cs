using System;
using System.Windows.Forms;

namespace RocketAlert
{
    public partial class AlertScreen : Form
    {

        /// <summary>
        /// Gets or sets the name of the place.
        /// </summary>
        /// <value>
        /// The name of the place.
        /// </value>
        public string placeName {  get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="AlertScreen"/> class.
        /// </summary>
        public AlertScreen()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Handles the Click event of the btnClose control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void btnClose_Click(object sender, EventArgs e)
        {
            timer1.Stop();
            this.Close();
        }

        /// <summary>Handles the Load event of the AlertScreen control.</summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        private void AlertScreen_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            label1.Left = (this.Width - label1.Width) / 2;
            lblNamePlace.Text = this.placeName;
            lblNamePlace.Left = (this.Width) / 2 - (lblNamePlace.Width)/2;
            timer1.Start();
        }

        /// <summary>Handles the Tick event of the timer1 control.</summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Stop();
            this.Close();
        }
    }
}
