using Market.Tools;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Market.DB
{
    public class DBTools
    {
        private static string _server, _database;

        static DBTools()
        {
            _server = IsLinux() ? @"localhost" : @".\sqlexpress";
            _database = "Market";
        }

        public static DataTable GetTable()
        {
            DataTable table = new DataTable();

            using (SqlConnection connection = Connect())
            {
                SqlCommand command = connection.CreateCommand();
                AddValues(command, AddParameters(command, "@CategoryName", "@ProductName"), null, null);
                command.CommandText = "select * from GetData(@CategoryName, @ProductName)";
                connection.Open();
                table.Load(command.ExecuteReader());
            }

            return table;
        }

        public static void InportData(List<Product> products)
        {
            using (SqlConnection connection = Connect())
            {
                SqlCommand command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "InportData";
                IEnumerable<string> parameters = AddParameters(command, "@CategoryName", "@ProductName", "@Price");
                connection.Open();
                SqlTransaction transaction = connection.BeginTransaction();
                command.Transaction = transaction;

                foreach (Product product in products)
                {
                    try
                    {   
                        AddValues(command, parameters, product.CategoryName, product.ProductName, product.Price);
                        command.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        try
                        {
                            transaction.Rollback();
                        }
                        catch (Exception exRollback)
                        {
                            throw new Exception(ex.Message, exRollback);
                        }

                        throw ex;
                    }
                }

                transaction.Commit();
            }
        }

        private static bool IsLinux() => PlatformID.Unix == Environment.OSVersion.Platform;

        private static SqlConnection Connect()
        {
            if (IsLinux())
                return new SqlConnection($"server={_server}; database={_database}; uid=hide; pwd=hide");

            return new SqlConnection($"server={_server}; database={_database}; Integrated Security=true");
        }

        private static IEnumerable<string> AddParameters(SqlCommand command, params string[] parameters)
        {
            for (int i = 0; i < parameters.Length; i++)
            {
                command.Parameters.Add(parameters[i]);
                yield return parameters[i];
            }
        }

        private static void AddValues(SqlCommand command, IEnumerable<string> parameters, params object[] values)
        {
            int index = 0;
            
            foreach (string parameter in parameters)
                command.Parameters[parameter].Value = values[index++] ?? DBNull.Value;
        }
    }
}
