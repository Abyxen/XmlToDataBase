using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.IO;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace XmlToDataBase
{
    public partial class Form1 : Form
    {
        private string connectionString =
            "server=localhost;port=3307;user=root;password=aeap2025;database=estagio;";

        private string backupPath = "backup.xml";

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            LoadData();
        }

      
        // LOAD GRID...
      
        private void LoadData()
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();

                    string query = "SELECT * FROM user_id";

                    MySqlDataAdapter adapter = new MySqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();

                    adapter.Fill(dt);

                    dataGridView1.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro: " + ex.Message);
            }
        }

       
        // SEARCH 
    
        private void BtnSearch_Click(object sender, EventArgs e)
        {
            
            string id = txtenterid.Text.Trim();

            if (string.IsNullOrEmpty(id))
            {
               
                LoadData();
                return;
            }

            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();

                    string query =
                        "SELECT * FROM user_id WHERE iduser_id = @id";

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@id", id);

                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                MessageBox.Show(
                                    "ID: " + reader["iduser_id"].ToString(),
                                    "Resultado da Pesquisa"
                                );
                            }
                            else
                            {
                                MessageBox.Show("Utilizador não encontrado.");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro: " + ex.Message);
            }
        }

    
        // BACKUP XML
    
        private void BtnBackup_Click(object sender, EventArgs e)
        {
            try
            {
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.Filter = "XML (*.xml)|*.xml";
                sfd.FileName = "Backup.xml";

                if (sfd.ShowDialog() != DialogResult.OK)
                    return;

                DataTable dt = new DataTable();

                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();

                    string query = "SELECT * FROM user_id";

                    MySqlDataAdapter adapter = new MySqlDataAdapter(query, conn);
                    adapter.Fill(dt);
                }

                DataSet ds = new DataSet("dados");
                dt.TableName = "user_id";
                ds.Tables.Add(dt);

                ds.WriteXml(sfd.FileName);

                MessageBox.Show("Backup feito com sucesso.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro backup: " + ex.Message);
            }
        }

        // RESTORE XML
      
        private void BtnRestore_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog ofd = new OpenFileDialog();
                ofd.Filter = "XML (*.xml)|*.xml";

                if (ofd.ShowDialog() != DialogResult.OK)
                    return;

                DataSet ds = new DataSet();
                ds.ReadXml(ofd.FileName);

                if (ds.Tables.Count == 0)
                {
                    MessageBox.Show("Backup vazio.");
                    return;
                }

                DataTable dt = ds.Tables[0];

                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();

                    foreach (DataRow row in dt.Rows)
                    {
                        string query =
                            @"INSERT INTO user_id (iduser_id)
                              VALUES (@id)
                              ON DUPLICATE KEY UPDATE iduser_id=@id;";

                        using (MySqlCommand cmd = new MySqlCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("@id", row["iduser_id"]);
                            cmd.ExecuteNonQuery();
                        }
                    }
                }

                LoadData();
                MessageBox.Show("Restaurado com sucesso.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro restore: " + ex.Message);
            }
        }
    }
}