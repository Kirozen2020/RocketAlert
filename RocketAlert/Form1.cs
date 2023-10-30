using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Http;
using System.Windows.Forms;
using static RocketAlert.JsonFileReader;

namespace RocketAlert
{
    public partial class Form1 : Form
    {
        List<string> selectetRegions = new List<string>();
        string placeUnderAttack = null;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
            this.ShowInTaskbar = false;

            RocketAlert.ContextMenuStrip = contextMenuStrip1;
            timer1.Start();

            using (SettingForm settingsForm = new SettingForm())
            {
                settingsForm.ShowDialog();
                this.selectetRegions = settingsForm.selectedRegions;
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
        }

        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using(SettingForm settingsForm = new SettingForm())
            {
                settingsForm.ShowDialog();
                this.selectetRegions = settingsForm.selectedRegions;
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            timer1.Stop();
            Application.Exit();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            CheckNewRocketAlert();
            if (this.placeUnderAttack != null)
            {
                using (AlertScreen alertScreen = new AlertScreen())
                {
                    timer1.Stop();
                    alertScreen.placeName = this.placeUnderAttack;
                    alertScreen.ShowDialog();
                    timer1.Start();
                    this.placeUnderAttack = null;
                }
            }
        }

        private async void CheckNewRocketAlert()
        {
            try
            {
                string filePath = "https://www.oref.org.il/WarningMessages/History/AlertsHistory.json";

                HttpClient client = new HttpClient();
                string jsonContent = await client.GetStringAsync(filePath);

                List<Alert> alerts = JsonConvert.DeserializeObject<List<Alert>>(jsonContent);
                
                List<string> matchingStrings = new List<string>();

                foreach(Alert alert in alerts)
                {
                    foreach(string searc in this.selectetRegions)
                    {
                        if (alert.Data.Contains(searc))
                        {
                            TimeSpan timedif = DateTime.Now - alert.AlertDate;
                            int diff = (int)timedif.TotalSeconds;
                            if(diff < 10)
                            {
                                matchingStrings.Add(alert.Data.ToString());
                            }
                        }
                    }
                }

                if (matchingStrings.Count > 0)
                {
                    HashSet<string> uniq = new HashSet<string>(matchingStrings);
                    matchingStrings = uniq.OrderBy(item => item).ToList();

                    this.placeUnderAttack = string.Join(", ", matchingStrings);
                }
                else
                {
                    this.placeUnderAttack = null;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error searching JSON file: " + ex.Message);
                this.placeUnderAttack = null;
            }
        }
    }
}
