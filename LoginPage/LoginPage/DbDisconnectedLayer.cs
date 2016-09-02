using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace LoginPage
{
    class DbDisconnectedLayer : IConnect
    {
        public User User { get; set; }
        public string ConnectionString { get; set; }

        public DbDisconnectedLayer(string server, string initialCatalog, string userId, string password)
        {
            var connectionString = new SqlConnectionStringBuilder
            {
                UserID = userId,
                Password = password,
                InitialCatalog = initialCatalog,
                DataSource = server
            };

            ConnectionString = connectionString.ConnectionString;
        }
      
        public DataTable GetDataFromDb()
        {

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                try
                {
                    connection.Open();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    Console.ReadLine();
                }

                DataTable dataTable = new DataTable();
                DataColumn userId = new DataColumn("UserId", typeof(int));
                userId.AutoIncrement = true;
                DataColumn userNname = new DataColumn("UserName", typeof(string));
                DataColumn userPassword = new DataColumn("UserPassword", typeof(string));
                dataTable.Columns.Add(userId);
                dataTable.Columns.Add(userNname);
                dataTable.Columns.Add(userPassword);
                dataTable.PrimaryKey = new DataColumn[] { userId };

                SqlDataAdapter dataAdapter = new SqlDataAdapter();
                using (SqlCommand command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = "SELECT * FROM dbo.Users";
                    dataAdapter.SelectCommand = command;
                    dataAdapter.Fill(dataTable);
                }
                return dataTable;

            }


        }

    }
}
