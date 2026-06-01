using System;
using System.Windows.Forms;

namespace XmlToDataBase
{
    public partial class Form1 : Form
    {
        private string xmlPath = string.Empty;

        public Form1()
        {
            InitializeComponent();
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "XML Files (*.xml)|*.xml";
                openFileDialog.Title = "Select XML File";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    xmlPath = openFileDialog.FileName;
                    textBox1.Text = xmlPath;

                    MessageBox.Show("File loaded successfully!");
                }
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            
            xmlPath = string.Empty;
            textBox1.Clear();

            MessageBox.Show("Selection cleared.");
        }

        private void button3_Click(object sender, EventArgs e)
        {
           
            MessageBox.Show("Not implemented yet.");
        }
    }
}