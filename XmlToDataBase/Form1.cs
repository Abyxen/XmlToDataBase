using System.Data;
using Microsoft.Data.Sqlite;

namespace XmlToDataBase
{
    public partial class Form1 : Form
    {
        private string dbPath = string.Empty;
        private const string MainTable = "records";
        private const string BackupTable = "records_backup";
        private string ConnectionString => $"Data Source={dbPath}";

        public Form1()
        {
            InitializeComponent();
        }

        private void BtnLoad_Click(object sender, EventArgs e)
        {
            using OpenFileDialog ofd = new()
            {
                Filter = "XML Files (*.xml)|*.xml|SQLite DB (*.db)|*.db",
                Title = "Seleciona XML (para importar) ou .db (para abrir)"
            };

            if (ofd.ShowDialog() != DialogResult.OK) return;

            string path = ofd.FileName;
            textBox1.Text = path;

            try
            {
                if (path.EndsWith(".xml", StringComparison.OrdinalIgnoreCase))
                {
                    dbPath = Path.ChangeExtension(path, ".db");

                    if (File.Exists(dbPath))
                    {
                        var r = MessageBox.Show(
                            $"O ficheiro {Path.GetFileName(dbPath)} ja existe.\nSubstituir?",
                            "Confirmar",
                            MessageBoxButtons.YesNo);

                        if (r != DialogResult.Yes)
                        {
                            dbPath = path;
                        }
                        else
                        {
                            File.Delete(dbPath);
                        }
                    }

                    if (!File.Exists(dbPath))
                    {
                        ImportXmlToSqlite(path, dbPath);
                    }
                }
                else
                {
                    dbPath = path;
                }

                RefreshGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro a carregar");
            }
        }

        private static void ImportXmlToSqlite(string xmlPath, string targetDb)
        {
            DataSet ds = new();
            ds.ReadXml(xmlPath);
            if (ds.Tables.Count == 0)
            {
                MessageBox.Show("O XML nao contem tabelas.", "Erro", MessageBoxButtons.OK);
                return;
            }

            DataTable src = ds.Tables[0];

            string columnDefs = string.Join(", ",
                src.Columns.Cast<DataColumn>()
                    .Select(c => $"\"{c.ColumnName}\" {MapType(c.DataType)}"));

            using SqliteConnection conn = new($"Data source={targetDb}");
            conn.Open();

            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText =
                    $"CREATE TABLE \"{MainTable}\" ({columnDefs});" +
                    $"CREATE TABLE \"{BackupTable}\" ({columnDefs});";
                cmd.ExecuteNonQuery();
            }

            using var tx = conn.BeginTransaction();
            using var insert = conn.CreateCommand();
            insert.Transaction = tx;

            string colList = string.Join(", ", src.Columns.Cast<DataColumn>().Select(c => $"\"{c.ColumnName}\""));
            string paramList = string.Join(", ", Enumerable.Range(0, src.Columns.Count).Select(i => $"@p{i}"));

            insert.CommandText =
                $"INSERT INTO \"{MainTable}\" ({colList}) VALUES ({paramList})";

            for (int i = 0; i < src.Columns.Count; i++)
            {
                insert.Parameters.Add(new SqliteParameter($"@p{i}", DBNull.Value));
            }

            foreach (DataRow row in src.Rows)
            {
                for (int i = 0; i < src.Columns.Count; i++)
                    insert.Parameters[i].Value = row[i] ?? DBNull.Value;
                insert.ExecuteNonQuery();
            }

            tx.Commit();
        }

        private void RefreshGrid()
        {
            if (string.IsNullOrEmpty(dbPath) || !File.Exists(dbPath)) return;

            using SqliteConnection conn = new(ConnectionString);
            conn.Open();

            using var cmd = conn.CreateCommand();
            cmd.CommandText = $"SELECT * FROM \"{MainTable}\";";

            using var reader = cmd.ExecuteReader();
            DataTable dt = new();
            dt.Load(reader);
            dataGridView1.DataSource = dt;
        }

        private static string MapType(Type t)
        {
            if (t == typeof(int) || t == typeof(long) || t == typeof(short))
                return "INTEGER";
            if (t == typeof(double) || t == typeof(float) || t == typeof(decimal))
                return "REAL";
            if (t == typeof(bool)) return "INTEGER";
            if (t == typeof(byte[])) return "BLOB";
            return "TEXT";
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            string id = txtenterid.Text.Trim();
            if (string.IsNullOrEmpty(id) || string.IsNullOrEmpty(dbPath)) return;

            try
            {
                using SqliteConnection conn = new(ConnectionString);
                conn.Open();
                using var tx = conn.BeginTransaction();

                using (var copy = conn.CreateCommand())
                {
                    copy.Transaction = tx;
                    copy.CommandText =
                        $"INSERT INTO \"{BackupTable}\" " +
                        $"SELECT * FROM \"{MainTable}\" WHERE id = @id;";
                    copy.Parameters.AddWithValue("@id", id);
                    copy.ExecuteNonQuery();
                }

                int removed;
                using (var del = conn.CreateCommand())
                {
                    del.Transaction = tx;
                    del.CommandText =
                        $"DELETE FROM \"{MainTable}\" WHERE id = @id;";
                    del.Parameters.AddWithValue("@id", id);
                    removed = del.ExecuteNonQuery();
                }

                tx.Commit();

                if (removed == 0)
                    MessageBox.Show($"Nenhum registo com id = {id}.");

                RefreshGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro a apagar");
            }
        }

        private void BtnRestore_Click(object sender, EventArgs e)
        {
            string id = txtenterid.Text.Trim();
            if (string.IsNullOrEmpty(id) || string.IsNullOrEmpty(dbPath)) return;

            try
            {
                using SqliteConnection conn = new(ConnectionString);
                conn.Open();
                using var tx = conn.BeginTransaction();

                using (var copy = conn.CreateCommand())
                {
                    copy.Transaction = tx;
                    copy.CommandText =
                        $"INSERT INTO \"{MainTable}\" " +
                        $"SELECT * FROM \"{BackupTable}\" WHERE id = @id;";
                    copy.Parameters.AddWithValue("@id", id);
                    copy.ExecuteNonQuery();
                }

                int restored;
                using (var del = conn.CreateCommand())
                {
                    del.Transaction = tx;
                    del.CommandText =
                        $"DELETE FROM \"{BackupTable}\" WHERE id = @id;";
                    del.Parameters.AddWithValue("@id", id);
                    restored = del.ExecuteNonQuery();
                }

                tx.Commit();

                if (restored == 0)
                    MessageBox.Show($"Nenhum registo no backup com id = {id}.");

                RefreshGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro a restaurar");
            }
        }
    }
}