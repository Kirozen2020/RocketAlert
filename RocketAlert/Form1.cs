using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

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
        /// The alert screen instance
        /// </summary>
        private AlertScreen alertScreenInstance;

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
                if (alertScreenInstance == null || alertScreenInstance.IsDisposed || alertScreenInstance.Visible != Visible)
                {
                    alertScreenInstance = new AlertScreen();
                    alertScreenInstance.placeName = this.placeUnderAttack;
                    alertScreenInstance.Show();
                }
                else
                {
                    alertScreenInstance.placeName = this.placeUnderAttack;
                }

                this.placeUnderAttack = null;
            }
        }

        /// <summary>
        /// Checks the new rocket alert.
        /// </summary>
        private async void CheckNewRocketAlert()
        {
            string filePath = "https://www.oref.org.il/WarningMessages/History/AlertsHistory.json";
            try
            {
                string jsonContent = await DownloadJsonAsync(filePath);

                List<MyDataModel> alerts = JsonConvert.DeserializeObject<List<MyDataModel>>(jsonContent);
                this.selectetRegions = this.selectetRegions.Select(s => s.Trim()).ToList();
                alerts.ForEach(item => item.Data = item.Data?.Trim());

                List<string> matchingsStrings = new List<string>();

                if (this.selectetRegions.Contains("בחר הכל"))
                {
                    foreach(MyDataModel alert in alerts)
                    {
                        TimeSpan timeDif = DateTime.Now - alert.AlertDate;
                        if((int)timeDif.TotalSeconds < 10)
                        {
                            matchingsStrings.Add(alert.Data.ToString());
                        }
                    }
                }
                else
                {
                    foreach (MyDataModel alert in alerts)
                    {
                        foreach (string searc in this.selectetRegions)
                        {
                            if (alert.Data.Equals(searc))
                            {
                                TimeSpan timeDif = DateTime.Now - alert.AlertDate;
                                if ((int)timeDif.TotalSeconds < 10)
                                {
                                    matchingsStrings.Add(alert.Data.ToString());
                                }
                            }
                        }
                    }
                }
                if(matchingsStrings.Count > 0)
                {
                    HashSet<string> uniq = new HashSet<string>(matchingsStrings);
                    matchingsStrings = uniq.OrderBy(item => item.Trim()).ToList();

                    this.placeUnderAttack = string.Join(", ", matchingsStrings);
                    //
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

        /// <summary>
        /// Downloads the json asynchronous.
        /// </summary>
        /// <param name="url">The URL.</param>
        /// <returns></returns>
        static async Task<string> DownloadJsonAsync(string url)
        {
            using (HttpClient client = new HttpClient())
            {
                // Download the content as a byte array
                byte[] content = await client.GetByteArrayAsync(url);

                // Decode the byte array using UTF-8 encoding
                return Encoding.UTF8.GetString(content);
            }
        }

        /// <summary>
        /// Custom Object for holding information about alerts
        /// </summary>
        public class MyDataModel
        {
            [JsonProperty("alertDate")]
            public DateTime AlertDate { get; set; }

            [JsonProperty("data")]
            public string Data { get; set; }
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

        /// <summary>
        /// Handles the Tick event of the UpdateData control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void UpdateData_Tick(object sender, EventArgs e)
        {
            WriteTextFile(this.selectetRegions, "RocketAlert", "SelectedRegionsSave");
        }
    }
}
