using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RocketAlert
{
    public partial class SettingForm : Form
    {
        List<string> regionsNames;
        public SettingForm()
        {
            InitializeComponent();
        }

        private void SettingForm_Load(object sender, EventArgs e)
        {
            string filePath = "";
            regionsNames = new List<string>();
            List<JsonFileReader.Alert> alerts = JsonFileReader.ReadJsonFile(filePath);

            if (alerts != null)
            {
                foreach (var alert in alerts)
                {
                    regionsNames.Add(alert.Data.ToString());
                    Console.WriteLine($"Alert Date: {alert.AlertDate}, Title: {alert.Title}, Data: {alert.Data}, Category: {alert.Category}");
                }
            }
            else
            {
                Console.WriteLine("Error reading JSON file.");
            }
        }
    }
}
