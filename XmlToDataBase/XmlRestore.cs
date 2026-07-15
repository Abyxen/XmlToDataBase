using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace XmlToDataBase
{
    public class XmlRestore
    {
        private string _connectionString { get; set; }

        public XmlRestore(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void RestoreBackup(string xmlFile)
        {
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(XmlBackup));

                XmlBackup backup;

                // Deserializa o XML
                using (FileStream fs = new FileStream(xmlFile, FileMode.Open))
                {
                    backup = (XmlBackup)serializer.Deserialize(fs);
                }

                // Conecta à  MySQL
                using (var connection = new MySqlConnection(_connectionString))
                {
                    connection.Open();

                    foreach (XmlTable table in backup.Tables)
                    {
                        this.RestoreTable(connection, table);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao restaurar o backup do ficheiro '{xmlFile}'", ex);
            }
        }

        private void RestoreTable(MySqlConnection connection, XmlTable table)
        {
            try
            {
                foreach (XmlTableRecord record in table.Records)
                {
                    List<string> columns = new List<string>();
                    List<string> parameters = new List<string>();

                    using (MySqlCommand command = new MySqlCommand())
                    {
                        command.Connection = connection;

                        
                        foreach (KeyValuePair<string, string> field in record.Fields)
                        {
                            columns.Add("`" + field.Key + "`");
                            parameters.Add("@" + field.Key);
                            command.Parameters.AddWithValue("@" + field.Key, field.Value);
                        }

                        // Cria e executa o INSERT
                        string sql = $"INSERT INTO `{table.TableName}` ({string.Join(", ", columns)}) VALUES ({string.Join(", ", parameters)});";

                        command.CommandText = sql;
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao restaurar a tabela '{table.TableName}'", ex);
            }
        }
    }
}
