using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.IO;
using System.Windows.Forms;

namespace RocketAlert
{
    public class JsonFileReader
    {
        public class Alert
        {
            public DateTime AlertDate { get; set; }
            public string Title { get; set; }
            public string Data { get; set; }
            public int Category { get; set; }
        }

        public static List<Alert> ReadJsonFile(string filePath)
        {
            try
            {
                if (!File.Exists(filePath))
                {
                    throw new FileNotFoundException("File not found", filePath);
                }

                string jsonContent = File.ReadAllText(filePath);
                List<Alert> alerts = JsonConvert.DeserializeObject<List<Alert>>(jsonContent);

                return alerts;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Console.WriteLine("Error reading JSON file: " + ex.Message);
                return null;
            }
        }
    }
}
