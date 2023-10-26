using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static RocketAlert.JsonFileReader;

namespace RocketAlert
{
    public partial class Form1 : Form
    {
        List<string> selectetRegions;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
            this.ShowInTaskbar = false;

            notifyIcon1.ContextMenuStrip = contextMenuStrip1;
            timer1.Start();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true; // Cancel the default action (closing the form)
            this.Hide(); // Hide the form instead of closing it
        }

        private void notifyIcon1_DoubleClick(object sender, EventArgs e)
        {
            this.Show();
            this.WindowState = FormWindowState.Normal;
        }

        private void updateLinkToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using(SettingForm settingsForm = new SettingForm())
            {
                settingsForm.ShowDialog();
                selectetRegions = settingsForm.selectedRegions;
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            timer1.Stop();
            Application.Exit();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (CheckNewRocketAlert())
            {

            }
        }

        private bool CheckNewRocketAlert()
        {
            try
            {
                string filePath = "path_to_your_json_file.json"; // Replace with the actual path
                if (!File.Exists(filePath))
                {
                    throw new FileNotFoundException("File not found", filePath);
                }

                string jsonContent = File.ReadAllText(filePath);
                List<Alert> alerts = JsonConvert.DeserializeObject<List<Alert>>(jsonContent);

                foreach (var searchString in this.selectetRegions)
                {
                    if (alerts.Any(alert => alert.Data.Contains(searchString)))
                    {
                        return true;
                    }
                }

                return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
