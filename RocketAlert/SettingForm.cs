using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static RocketAlert.JsonFileReader;

namespace RocketAlert
{
    public partial class SettingForm : Form
    {
        List<string> regionsNames;
        public List<string> selectedRegions;

        public SettingForm()
        {
            InitializeComponent();
        }

        private async void SettingForm_Load(object sender, EventArgs e)
        {
            string filePath = "https://www.oref.org.il/WarningMessages/History/AlertsHistory.json";
            regionsNames = new List<string>();

            var json = new WebClient().DownloadString(filePath);

            HttpClient client = new HttpClient();
            string jsonContent = await client.GetStringAsync(filePath);

            List<Alert> alerts = JsonConvert.DeserializeObject<List<Alert>>(jsonContent);

            if (alerts != null)
            {
                foreach (var alert in alerts)
                {
                    regionsNames.Add(alert.Data.ToString());
                }
            }
            else
            {
                Console.WriteLine("Error reading JSON file.");
            }

            HashSet<string> uniq = new HashSet<string>(this.regionsNames);
            this.regionsNames = uniq.OrderBy(item => item).ToList();

            listBox1.DataSource = this.regionsNames;
            listBox1.Refresh();
            listBox1.SelectionMode = SelectionMode.MultiExtended;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.selectedRegions = new List<string>();

            foreach(var selectedItem in listBox1.SelectedItems)
            {
                this.selectedRegions.Add(selectedItem.ToString());
            }
        }
    }
}
