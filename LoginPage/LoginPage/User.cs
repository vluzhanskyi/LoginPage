using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoginPage
{
    class User
    {
        public void ConectToDB(string server, string userID, string password, string initialCatalog)
        {
            SqlConnectionStringBuilder connectionString = new SqlConnectionStringBuilder();
            connectionString.UserID = userID;
            connectionString.Password = password;
            connectionString.InitialCatalog = initialCatalog;
            connectionString.DataSource = server;
        }

        public void GetDbData(string connectionString, string queryString)
        {
            using (SqlConnection connection = new SqlConnection(
              connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Connection.Open();
                command.ExecuteNonQuery();
            }
        }
    }
}
