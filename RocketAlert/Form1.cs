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

        /// <summary>The selectet regions</summary>
        List<string> selectetRegions = new List<string>();
        /// <summary>The place under attack</summary>
        string placeUnderAttack = null;

        /// <summary>Initializes a new instance of the <see cref="Form1" /> class.</summary>
        public Form1()
        {
            InitializeComponent();
        }
        
        /// <summary>Handles the Load event of the Form1 control.</summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        private void Form1_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
            this.ShowInTaskbar = false;

            RocketAlert.ContextMenuStrip = contextMenuStrip1;
            timer1.Start();

            List<string> names = new List<string>();
            SettingForm settingForm;
            if (this.selectetRegions != null)
            {
                settingForm = new SettingForm(this.selectetRegions);
                names = selectetRegions;
            }
            else
            {
                settingForm = new SettingForm();
            }
            settingForm.Show();
            this.selectetRegions = settingForm.selectedRegions;
            if (!settingForm.Visible)
            {
                this.selectetRegions = names;
                settingForm.Dispose();
            }
            
        }

        /// <summary>Handles the FormClosing event of the Form1 control.</summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="FormClosingEventArgs" /> instance containing the event data.</param>
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
        }

        /// <summary>Handles the Click event of the settingsToolStripMenuItem control.</summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StartSettingsForm();
        }

        /// <summary>Handles the Click event of the exitToolStripMenuItem control.</summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            timer1.Stop();
            Application.Exit();
        }

        /// <summary>Handles the Tick event of the timer1 control.</summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        private void timer1_Tick(object sender, EventArgs e)
        {
            CheckNewRocketAlert();
            if (this.placeUnderAttack != null)
            {
                using (AlertScreen alertScreen = new AlertScreen())
                {
                    timer1.Stop();
                    alertScreen.placeName = this.placeUnderAttack;
                    alertScreen.Show();
                    timer1.Start();
                    this.placeUnderAttack = null;
                }
            }
        }

        /// <summary>Checks the new rocket alert.</summary>
        private async void CheckNewRocketAlert()
        {
            try
            {
                string filePath = "https://www.oref.org.il/WarningMessages/History/AlertsHistory.json";

                HttpClient client = new HttpClient();
                string jsonContent = await client.GetStringAsync(filePath);

                List<Alert> alerts = JsonConvert.DeserializeObject<List<Alert>>(jsonContent);
                
                List<string> matchingStrings = new List<string>();

                if(this.selectetRegions.Contains("בחר הכל"))
                {
                    foreach(Alert alert in alerts)
                    {
                        TimeSpan timedif = DateTime.Now - alert.AlertDate;
                        int diff = (int)timedif.TotalSeconds;
                        if (diff < 10)
                        {
                            matchingStrings.Add(alert.Data.ToString());
                        }
                    }
                }
                else
                {
                    foreach (Alert alert in alerts)
                    {
                        foreach (string searc in this.selectetRegions)
                        {
                            if (alert.Data.Contains(searc))
                            {
                                TimeSpan timedif = DateTime.Now - alert.AlertDate;
                                int diff = (int)timedif.TotalSeconds;
                                if (diff < 10)
                                {
                                    matchingStrings.Add(alert.Data.ToString());
                                }
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

        /// <summary>Handles the DoubleClick event of the RocketAlert control.</summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        private void RocketAlert_DoubleClick(object sender, EventArgs e)
        {
            StartSettingsForm();
        }

        /// <summary>Starts the settings form.</summary>
        private void StartSettingsForm()
        {
            SettingForm settingForm;
            if (this.selectetRegions != null)
            {
                settingForm = new SettingForm(this.selectetRegions);
            }
            else
            {
                settingForm = new SettingForm();
            }
            settingForm.ShowDialog();
            this.selectetRegions = settingForm.selectedRegions;
            if (!settingForm.Visible)
            {
                settingForm.Dispose();
            }
        }
    }
}
