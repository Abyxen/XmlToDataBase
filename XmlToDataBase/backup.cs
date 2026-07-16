using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml.Serialization;

namespace XmlToDataBase
{
    public class BackUp
    {
        private string _connectionString { get; set; }
        private int _association_id { get; set; }

        public BackUp(int associationID, string connectionString)
        {
            _association_id = associationID;
            _connectionString = connectionString;
        }

        public string GetXmlBackup(string filePath)
        {
            XmlBackup backup = new XmlBackup();
            backup.AssociationID = _association_id;

            backup.Tables.Add(new XmlTable
            {
                TableName = "users",
                Records = this.GetTableRecords("users", "association_id = " + _association_id)
            });

            // backup.Tables.Add(new XmlTable
            // {
            //     TableName = "cars",
            //     Records = this.GetTableRecords("cars", "association_id = " + _association_id)
            // });

            XmlSerializer serializer = new XmlSerializer(typeof(XmlBackup));

            using (FileStream fs = new FileStream(filePath, FileMode.Create))
            {
                serializer.Serialize(fs, backup);
            }

            return filePath;
        }

        private List<string> GetTableColumns(string tableName)
        {
            List<string> result = new List<string>();

            try
            {
                const string sql = "SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = @tableName AND TABLE_SCHEMA = DATABASE() ORDER BY ORDINAL_POSITION;";

                using (var connection = new MySqlConnection(_connectionString))
                {
                    connection.Open();

                    MySqlCommand command = new MySqlCommand(sql, connection);
                    command.Parameters.AddWithValue("@tableName", tableName);

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            result.Add(reader.GetString("COLUMN_NAME"));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao obter colunas da tabela '{tableName}'", ex);
            }

            return result;
        }

        public List<XmlTableRecord> GetTableRecords(string tableName, string whereClause)
        {
            List<XmlTableRecord> result = new List<XmlTableRecord>();

            try
            {
                var tableColumns = GetTableColumns(tableName);

                string sql = $"SELECT `{string.Join("`, `", tableColumns)}` FROM `{tableName}`";

                if (!string.IsNullOrWhiteSpace(whereClause))
                {
                    sql += $" WHERE {whereClause}";
                }

                sql += ";";

                using (var connection = new MySqlConnection(_connectionString))
                {
                    connection.Open();

                    MySqlCommand command = new MySqlCommand(sql, connection);

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var record = new XmlTableRecord();

                            foreach (string columnname in tableColumns)
                            {
                                if (!string.IsNullOrWhiteSpace(columnname))
                                {
                                    record.Fields.Add(
                                        columnname,
                                        reader[columnname] != DBNull.Value ? reader[columnname].ToString() : null
                                    );
                                }
                            }

                            result.Add(record);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao obter registos da tabela '{tableName}'", ex);
            }

            return result;
        }
    }
}