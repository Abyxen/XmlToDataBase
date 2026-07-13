using System;
using System.Collections.Generic;
using System.Text;
using MySql.Data.MySqlClient;

namespace XmlToDataBase
{

    public class TableRecord
    {
        public Dictionary<string, string> Data { get; set; }

        public TableRecord()
        {
            Data = new Dictionary<string, string>();
        }
    }

    public class BackUp
    {
        private string _connectionString { get; set; }
        private int _association_id { get; set; }

        public BackUp(int associationID, string connectionString)
        {
            _association_id = associationID;
            _connectionString = connectionString;
        }
        public string GetXmlBackup()
        {
            var usersrecords = this.GetTableRecords("users", "association_id = " + _association_id);

            return "";
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


        public List<TableRecord> GetTableRecords(string tableName, string whereClause)
        {
            List<TableRecord> result = new List<TableRecord>();

            try
            {
                var tableColumns = GetTableColumns(tableName);

                // Constrói o SQL com proteção contra SQL injection
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
                            var record = new TableRecord();

                            foreach (string columnname in tableColumns)
                       
                            {
                                if (!string.IsNullOrWhiteSpace(columnname))
                                record.Data.Add(columnname, reader[columnname] != DBNull.Value ? reader[columnname].ToString() : null);
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