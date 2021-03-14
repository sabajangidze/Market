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
            _server = @".\sqlexpress";
            _database = "Market";
        }

        public static DataTable GetTable()
        {
            DataTable table = new DataTable();

            using (SqlConnection connection = Connect())
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

        public static void InportData(List<Product> products)
        {
            using (SqlConnection connection = Connect())
            {
                connection.Open();
                SqlTransaction transaction = connection.BeginTransaction();
                SqlCommand command = connection.CreateCommand();
                command.Transaction = transaction;
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "InportData";
                AddParameters(command);

                foreach (Product product in products)
                {
                    try
                    {
                        AddValues(command, product);
                        command.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        try
                        {
                            transaction.Rollback();
                        }
                        catch(Exception exRollback)
                        {
                            throw new Exception(ex.Message, exRollback);
                        }

                        throw ex;
                    }
                }

                transaction.Commit();

                void AddParameters(SqlCommand command)
                {
                    command.Parameters.Add("@CategoryName", SqlDbType.NVarChar, 30);
                    command.Parameters.Add("@ProductName", SqlDbType.NVarChar, 30);
                    command.Parameters.Add("@Price", SqlDbType.Money);
                }

                void AddValues(SqlCommand command, Product product)
                {
                    command.Parameters["@CategoryName"].Value = product.CategoryName;
                    command.Parameters["@ProductName"].Value = product.ProductName;
                    command.Parameters["@Price"].Value = product.Price;
                }
            }
        }

        private static SqlConnection Connect() => new SqlConnection($"server={_server}; database={_database}; Integrated Security=true");
    }
}
