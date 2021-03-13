using System;
using System.Data;
using System.Data.SqlClient;

namespace Market.DB
{
    public class DBTools
    {
        private static string _server, _database;

        static DBTools()
        {
            _server = @".\sqlexpress";
            _database = "Market";
        }

        public static DataTable GetTable()
        {
            DataTable table = new DataTable();

            using(SqlConnection connection = Connect())
            {
                connection.Open();
                SqlCommand command = connection.CreateCommand();
                AddParameters(command);
                command.CommandText = "select * from GetData(@CategoryName, @ProductName)";
                table.Load(command.ExecuteReader());
            }

            return table;

            void AddParameters(SqlCommand command)
            {
                command.Parameters.Add("@CategoryName", SqlDbType.NVarChar, 30).Value = DBNull.Value;
                command.Parameters.Add("@ProductName", SqlDbType.NVarChar, 30).Value = DBNull.Value;
            }
        }

        private static SqlConnection Connect() => new SqlConnection($"server={_server}; database={_database}; Integrated Security=true");
    }
}
