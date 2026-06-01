using System;
using System.Collections.Generic; 
using System.Data;
using System.Windows.Forms;

namespace XmlToDataBase
{
    public partial class Form1 : Form
    {
        private string xmlPath = string.Empty;
        List<DataRow> backupRows = new List<DataRow>(); 

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

                    try
                    {
                        DataSet dataSet = new DataSet();
                        dataSet.ReadXml(xmlPath);

                        if (dataSet.Tables.Count > 0)
                        {
                            dataGridView1.DataSource = dataSet.Tables[0];
                        }
                    }
                    catch
                    {
                    }
                }
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            string idToDelete = txtenterid.Text;
            try
            {
                
                DataTable dt = (DataTable)dataGridView1.DataSource;

                for (int i = dt.Rows.Count - 1; i >= 0; i--)
                {
                   
                    if (dt.Rows[i]["id"].ToString() == idToDelete)
                    {
                        DataRow backupRow = dt.NewRow();
                        backupRow.ItemArray = dt.Rows[i].ItemArray;
                        backupRows.Add(backupRow);

                        dt.Rows.RemoveAt(i);
                    }
                }

                dt.WriteXml(xmlPath);
            }
            catch
            {
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string idToRestore = txtenterid.Text;
            try
            {
               
                DataTable dt = (DataTable)dataGridView1.DataSource;

                for (int i = backupRows.Count - 1; i >= 0; i--)
                {
                    
                    if (backupRows[i]["id"].ToString() == idToRestore)
                    {
                        dt.Rows.Add(backupRows[i].ItemArray);
                        backupRows.RemoveAt(i);

                        dt.WriteXml(xmlPath);
                        break;
                    }
                }
            }
            catch
            {
            }
        }
    }
}