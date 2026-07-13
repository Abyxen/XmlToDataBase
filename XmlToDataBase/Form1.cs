using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Windows.Forms;

namespace XmlToDataBase
{
    public partial class Form1 : Form
    {
        // Ligação à base de dados MySQL
        private string connectionString =
            "server=localhost;port=3306;user=root;password=aeap2025;database=user;";

       
        private string backupPath = "backup.xml";

        public Form1()
        {
            InitializeComponent();
        }

       
        private void Form1_Load(object sender, EventArgs e)
        {
            LoadData();
        }


        

        private void LoadData()
        {
            // Cria a ligação à base de dados
            MySqlConnection conn = new MySqlConnection(connectionString);

            try
            {
                
                conn.Open();

                
                string query = "SELECT * FROM users";

                
                MySqlDataAdapter adapter = new MySqlDataAdapter(query, conn);
                DataTable dt = new DataTable();

               
                adapter.Fill(dt);

               
                dataGridView1.DataSource = dt;
            }
            catch (Exception ex)
            {
                
                MessageBox.Show("Erro: " + ex.Message);
            }
            finally
            {
                
                if (conn.State == ConnectionState.Open)
                    conn.Close();
            }
        }


        // PESQUISAR

        private void BtnSearch_Click(object sender, EventArgs e)
        {
          
            string id = txtenterid.Text.Trim();

           
            if (id == "")
            {
                LoadData();
                return;
            }

            MySqlConnection conn = new MySqlConnection(connectionString);

            try
            {
               
                conn.Open();

               
                string query = "SELECT * FROM users WHERE userid = @id";

                MySqlCommand cmd = new MySqlCommand(query, conn);

              
                cmd.Parameters.AddWithValue("@id", id);

                
                MySqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    
                    MessageBox.Show("ID: " + reader["userid"].ToString(), "Resultado da Pesquisa");
                }
                else
                {
                    
                    MessageBox.Show("Utilizador não encontrado.");
                }

                reader.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro: " + ex.Message);
            }
            finally
            {
                
                if (conn.State == ConnectionState.Open)
                    conn.Close();
            }
        }


        // CRIAR BACKUP EM XML

        private void BtnBackup_Click(object sender, EventArgs e)
        {
            BackUp backup = new BackUp (2, connectionString);
            /*var x = backup.GetTableColumns("users");*/
            var w = backup.GetXmlBackup();
            int y = 0; 

           
            
            //Janela para escolher onde guardar o ficheiro xml backup*//
            /*
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "XML (*.xml)|*.xml";
            sfd.FileName = "Backup.xml";

            if (sfd.ShowDialog() != DialogResult.OK)
                return;

            MySqlConnection conn = new MySqlConnection(connectionString);

            try
            {

                conn.Open();


                string query = "SELECT * FROM users";

                MySqlDataAdapter adapter = new MySqlDataAdapter(query, conn);
                DataTable dt = new DataTable();


                dt.TableName = "users";


                adapter.Fill(dt);


                DataSet ds = new DataSet("dados");
                ds.Tables.Add(dt);


                ds.WriteXml(sfd.FileName);

                MessageBox.Show("Backup feito com sucesso.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro backup: " + ex.Message);
            }
            finally
            {

                if (conn.State == ConnectionState.Open)
                    conn.Close();
            }*/
        }



        // RESTAURAR O BACKUP XML

        private void BtnRestore_Click(object sender, EventArgs e)
        {
            MySqlConnection conn = new MySqlConnection(connectionString);

            try
            {
                
                DataSet ds = new DataSet();
                ds.ReadXml(backupPath);

                
                DataTable dt = ds.Tables[0];

              
                conn.Open();

                
                foreach (DataRow row in dt.Rows)
                {
                    string query =
                        @"INSERT INTO users (userid)
                          VALUES (@id)
                          ON DUPLICATE KEY UPDATE userid = @id";

                    MySqlCommand cmd = new MySqlCommand(query, conn);

                   
                    cmd.Parameters.AddWithValue("@id", row["userid"]);

                   
                    cmd.ExecuteNonQuery();
                }

                MessageBox.Show("Restaurado com sucesso.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro: " + ex.Message);
            }
            finally
            {
                
                if (conn.State == ConnectionState.Open)
                    conn.Close();
            }

            
            LoadData();
        }


        // ELIMINAR UTILIZADOR

        private void button1_Click(object sender, EventArgs e)
        {
           
            string id = txtenterid.Text.Trim();

            if (id == "")
            {
                MessageBox.Show("Introduza um ID.");
                return;
            }

            MySqlConnection conn = new MySqlConnection(connectionString);

            
            bool deleted = false;

            try
            {
               
                conn.Open();

               
                string selectQuery = "SELECT * FROM users WHERE userid = @id";

                MySqlCommand selectCmd = new MySqlCommand(selectQuery, conn);
                selectCmd.Parameters.AddWithValue("@id", id);

               
                MySqlDataAdapter adapter = new MySqlDataAdapter(selectCmd);
                DataTable dt = new DataTable();

                adapter.Fill(dt);

                if (dt.Rows.Count == 0)
                {
                    MessageBox.Show("Utilizador não encontrado.");
                }
                else
                {
                    
                    dt.TableName = "users";
                    DataSet ds = new DataSet();
                    ds.Tables.Add(dt);
                    ds.WriteXml(backupPath);

                    // Elimina o utilizador
                    string deleteQuery = "DELETE FROM users WHERE userid = @id";

                    MySqlCommand deleteCmd = new MySqlCommand(deleteQuery, conn);
                    deleteCmd.Parameters.AddWithValue("@id", id);

                    deleteCmd.ExecuteNonQuery();

                    deleted = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro: " + ex.Message);
            }
            finally
            {
               
                if (conn.State == ConnectionState.Open)
                    conn.Close();
            }

           
            if (deleted)
            {
                LoadData();
                MessageBox.Show("Utilizador eliminado.");
            }
        }
    }
}