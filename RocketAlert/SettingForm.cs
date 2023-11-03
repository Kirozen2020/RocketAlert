using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Windows.Forms;
using static RocketAlert.JsonFileReader;

namespace RocketAlert
{
    public partial class SettingForm : Form
    {
        /// <summary>The regions names</summary>
        private List<string> regionsNames = new List<string>();
        /// <summary>The selected regions</summary>
        public List<string> selectedRegions;
        /// <summary>The not selected regions</summary>
        private List<string> notSelectedRegions;
        /// <summary>The initial selected</summary>
        public List<string> initialSelected;
        /// <summary>The cancel action</summary>
        public bool cancelAction = false;

        /// <summary>Initializes a new instance of the <see cref="SettingForm" /> class.</summary>
        public SettingForm()
        {
            InitializeComponent();
            this.selectedRegions = null;
        }
        /// <summary>Initializes a new instance of the <see cref="SettingForm" /> class.</summary>
        /// <param name="selected">The selected.</param>
        public SettingForm(List<string> selected)
        {
            InitializeComponent();
            this.selectedRegions = selected;
            this.initialSelected = selected.ToList();
        }

        /// <summary>Handles the Load event of the SettingForm control.</summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        private void SettingForm_Load(object sender, EventArgs e)
        {
            this.cancelAction = false;
            InitListOfNames();
            if (this.selectedRegions.Count == 0)
            {
                this.selectedRegions = new List<string>();
                this.initialSelected = new List<string>();
                this.notSelectedRegions = this.regionsNames;
            }
            else
            {
                this.notSelectedRegions = GetDiffList(this.regionsNames, this.selectedRegions);
            }

            /*-----------------------------------------------------*/

            listBox1.ClearSelected();
            listBox1.Items.Clear();
            listBox1.SelectionMode = SelectionMode.MultiExtended;
            foreach(var item in this.notSelectedRegions)
            {
                listBox1.Items.Add(item.ToString());
            }

            /*-----------------------------------------------------*/

            listBox2.ClearSelected();
            listBox2.Items.Clear();
            listBox2.SelectionMode = SelectionMode.MultiExtended;
            foreach (var item in this.selectedRegions)
            {
                listBox2.Items.Add(item.ToString());
            }
        }

        /// <summary>Handles the Click event of the btnSave control.</summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            List<string> selected = new List<string>();
            foreach(var item in listBox2.Items)
            {
                selected.Add(item.ToString());
            }
            this.cancelAction = false;
            this.selectedRegions = selected.Distinct().ToList();
            this.initialSelected = this.selectedRegions.ToList();
            this.Visible = false;
        }

        /// <summary>Initializes the list of names.</summary>
        private void InitListOfNames()
        {
            var tmp = System.Reflection.IntrospectionExtensions.GetTypeInfo(typeof(SettingForm)).Assembly;

            System.IO.Stream s = tmp.GetManifestResourceStream("RocketAlert.Places.txt");
            System.IO.StreamReader sr = new System.IO.StreamReader(s);
            this.regionsNames = sr.ReadToEnd().Split('\n').ToList();
            sr.Close();

        }

        /// <summary>Filters the specified filter.</summary>
        /// <param name="filter">The filter.</param>
        /// <param name="value">The value.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        public List<string> Filter(string filter, List<string> value)
        {
            List<string> strings = new List<string>();
            foreach(string item in value)
            {
                if (item.Contains(filter))
                {
                    strings.Add(item);
                }
            }
            return strings;
        }

        /// <summary>Handles the Click event of the btnCancel control.</summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.cancelAction = true;
            this.selectedRegions = this.initialSelected.ToList();
            this.Visible = false;
        }

        /// <summary>Handles the Click event of the btnSelect control.</summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        private void btnSelect_Click(object sender, EventArgs e)
        {
            List<object> selectedItems = new List<object>();
            foreach(var item in listBox1.SelectedItems)
            {
                selectedItems.Add(item);
            }
            foreach(var item in selectedItems)
            {
                listBox1.Items.Remove(item.ToString());
                this.notSelectedRegions.Remove(item.ToString());
                this.selectedRegions.Add(item.ToString());
            }
            this.selectedRegions.Sort();
            foreach(var item in this.selectedRegions)
            {
                if (!listBox2.Items.Contains(item.ToString()))
                {
                    listBox2.Items.Add(item.ToString());
                }
            }
        }

        /// <summary>Handles the Click event of the btnUnselect control.</summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        private void btnUnselect_Click(object sender, EventArgs e)
        {
            List<object> selectedItems = new List<object>();
            foreach (var item in listBox2.SelectedItems)
            {
                selectedItems.Add(item);
            }
            foreach (var item in selectedItems)
            {
                listBox2.Items.Remove(item.ToString());
                this.selectedRegions.Remove(item.ToString());
                this.notSelectedRegions.Add(item.ToString());
            }
            this.notSelectedRegions.Sort();
            foreach(var item in this.notSelectedRegions)
            {
                if (!listBox1.Items.Contains(item.ToString()))
                {
                    listBox1.Items.Add(item.ToString());
                }
            }
        }

        /// <summary>Handles the TextChanged event of the tbSearchSelected control.</summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        private void tbSearchSelected_TextChanged(object sender, EventArgs e)
        {
            TextBox textBox = (TextBox)sender;

            List<string> names = Filter(textBox.Text, this.selectedRegions);

            if (names != null)
            {
                listBox2.ClearSelected();
                listBox2.Items.Clear();
                foreach (string name in names)
                {
                    listBox2.Items.Add(name);
                }
            }
        }

        /// <summary>Handles the TextChanged event of the tbSearchNotSelected control.</summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        private void tbSearchNotSelected_TextChanged(object sender, EventArgs e)
        {
            TextBox textBox = (TextBox)sender;

            List<string> names = Filter(textBox.Text, this.notSelectedRegions);

            if (names != null)
            {
                listBox1.ClearSelected();
                listBox1.Items.Clear();
                foreach (string name in names)
                {
                    listBox1.Items.Add(name);
                }
            }
        }

        /// <summary>Handles the FormClosing event of the SettingForm control.</summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="FormClosingEventArgs" /> instance containing the event data.</param>
        private void SettingForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.cancelAction = true;
            this.selectedRegions = this.initialSelected;
            this.Visible = false;
        }

        /// <summary>Gets the difference list.</summary>
        /// <param name="list1">The list1.</param>
        /// <param name="list2">The list2.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        private static List<string> GetDiffList(List<string> list1, List<string> list2)
        {
            List<string> difference = new List<string>();

            foreach (string item in list1)
            {
                bool foundMatch = false;

                List<string> words1 = SplitIntoWords(item);

                foreach (string item2 in list2)
                {
                    List<string> words2 = SplitIntoWords(item2);

                    if (words1.SequenceEqual(words2))
                    {
                        foundMatch = true;
                        break;
                    }
                }

                if (!foundMatch)
                {
                    difference.Add(item);
                }
            }

            return difference;
        }

        /// <summary>Splits the into words.</summary>
        /// <param name="input">The input.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        private static List<string> SplitIntoWords(string input)
        {
            return input.Split(new char[] { ' ', '\t', '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries).ToList();
        }
    }
}
