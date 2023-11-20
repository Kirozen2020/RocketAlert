using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.IO.Pipes;
using System.Linq;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using static RocketAlert.JsonFileReader;

namespace RocketAlert
{
    public partial class Form1 : Form
    {

        /// <summary>
        /// The selectet regions
        /// </summary>
        List<string> selectetRegions = new List<string>();
        /// <summary>
        /// The place under attack
        /// </summary>
        string placeUnderAttack = null;
        
        /// <summary>
        /// Initializes a new instance of the <see cref="Form1"/> class.
        /// </summary>
        public Form1()
        {
            InitializeComponent();
            InitForm();
            KillOtherInstances();
        }

        /// <summary>
        /// Initializes the form.
        /// </summary>
        private void InitForm()
        {
            List<string> temp = ReadTextFile("RocketAlert", "SelectedRegionsSave");
            temp.RemoveAll(string.IsNullOrWhiteSpace);
            if (temp.Count > 0)
            {
                this.selectetRegions = temp.ToList();
            }

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

        /// <summary>
        /// Kills the other instances.
        /// </summary>
        private void KillOtherInstances()
        {
            const string mutexName = "RocketAlertMutex";

            using (Mutex mutex = new Mutex(true, mutexName, out bool createdNew))
            {
                if (!createdNew)
                {
                    // Another instance is running, don't attempt to kill it
                    return;
                }

                // Attempt to find and close other instances
                Process currentProcess = Process.GetCurrentProcess();
                foreach (Process process in Process.GetProcessesByName(currentProcess.ProcessName))
                {
                    if (process.Id != currentProcess.Id)
                    {
                        // Close the other instance
                        process.CloseMainWindow();

                        // Wait for the process to exit
                        process.WaitForExit(5000);

                        // If the process hasn't exited, forcefully kill it
                        if (!process.HasExited)
                        {
                            process.Kill();
                        }
                    }
                }
            }
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
            WriteTextFile(this.selectetRegions, "RocketAlert", "SelectedRegionsSave");

            timer1.Stop();
            Environment.Exit(0);
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
                    alertScreen.placeName = this.placeUnderAttack;
                    alertScreen.Show();
                    this.placeUnderAttack = null;
                }
            }
            WriteTextFile(this.selectetRegions, "RocketAlert", "SelectedRegionsSave");
        }

        /// <summary>
        /// Checks the new rocket alert.
        /// </summary>
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

        /// <summary>
        /// Starts the settings form.
        /// </summary>
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
                if (!settingForm.cancelAction)
                {
                    this.selectetRegions = settingForm.initialSelected;
                }
                
                settingForm.Dispose();
            }
        }

        /// <summary>Writes the text file.</summary>
        /// <param name="data">The data.</param>
        /// <param name="customFolderName">Name of the custom folder.</param>
        /// <param name="fileName">Name of the file.</param>
        private static void WriteTextFile(List<string> data, string customFolderName, string fileName)
        {
            string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            string customFolderPath = Path.Combine(documentsPath, customFolderName);

            if (!Directory.Exists(customFolderPath))
            {
                Directory.CreateDirectory(customFolderPath);
            }

            string filePath = Path.Combine(customFolderPath, fileName);

            using (StreamWriter sw = new StreamWriter(filePath, false))
            {
                foreach (string line in data)
                {
                    sw.WriteLine(line);
                }
            }
        }

        /// <summary>Reads the text file.</summary>
        /// <param name="customFolderName">Name of the custom folder.</param>
        /// <param name="fileName">Name of the file.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        private static List<string> ReadTextFile(string customFolderName, string fileName)
        {
            List<string> data = new List<string>();

            string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            string filePath = Path.Combine(documentsPath, customFolderName, fileName);

            if (File.Exists(filePath))
            {
                using (StreamReader sr = new StreamReader(filePath))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        data.Add(line);
                    }
                }
            }

            return data;
        }
    }
}
