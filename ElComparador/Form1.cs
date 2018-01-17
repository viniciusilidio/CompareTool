using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace ElComparador
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void textBox1_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                textBox1.Text = openFileDialog1.FileName;

                string[] headers = File.ReadAllText(textBox1.Text).Split('\n')[0].Split(';');

                List<Tuple<string, int>> checkListBoxData = new List<Tuple<string, int>>();
                for (int i = 0; i<headers.Length; i++)
                {
                    checkListBoxData.Add(Tuple.Create(headers[i], i));
                }

                checkedListBox1.DataSource = checkListBoxData;
            }
        }

        private void textBox2_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                textBox2.Text = openFileDialog1.FileName;

                string[] headers = File.ReadAllText(textBox2.Text).Split('\n')[0].Split(';');

                List<Tuple<string, int>> checkListBoxData = new List<Tuple<string, int>>();
                for (int i = 0; i < headers.Length; i++)
                {
                    checkListBoxData.Add(Tuple.Create(headers[i], i));
                }

                checkedListBox2.DataSource = checkListBoxData;
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < checkedListBox1.Items.Count; i++)
            {
                checkedListBox1.SetItemChecked(i, checkBox1.Checked);
            }
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < checkedListBox2.Items.Count; i++)
            {
                checkedListBox2.SetItemChecked(i, checkBox2.Checked);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            List<int> checkedIndices1 = new List<int>();
            foreach (var indice in checkedListBox1.CheckedIndices)
            {
                checkedIndices1.Add(int.Parse(indice.ToString ()));
            }

            List<int> checkedIndices2 = new List<int>();
            foreach (var indice in checkedListBox2.CheckedIndices)
            {
                checkedIndices2.Add(int.Parse(indice.ToString()));
            }

            richTextBox1.Text = new Main().Execute(textBox1.Text, textBox2.Text, checkedIndices1.ToArray (), checkedIndices2.ToArray ());
        }

        public string GetSelectedFieldName (int field, int box)
        {
            if (box == 1)
            {
                foreach (Tuple<string, int> item in checkedListBox1.CheckedItems)
                {
                    if (item.Item2 == field)
                        return item.Item1;
                }
            }

            if (box == 2)
            {
                foreach (Tuple<string, int> item in checkedListBox2.CheckedItems)
                {
                    if (item.Item2 == field)
                        return item.Item1;
                }
            }

            return null;
        }

        public void UpdateProgressBar (int total, int current)
        {
            int value = (int)((current / (float)total) * 100.0f);
            progressBar1.Value = value;
        }
    }
}
