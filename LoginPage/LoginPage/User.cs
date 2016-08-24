using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoginPage
{
    public class User
    {
      //  public Dictionary<string, string> Users { set; get; }
        public string UserName { set; get; }
        public string Password { set; get; }
        public User(string name, string password)
        {
            UserName = name;
            Password = password;
        }
    }

    public class DbLogic
    {
        List<User> Users = new List<User>();
        private string CreateConnectionString(string server, string initialCatalog, string userID, string password)
        {
            SqlConnectionStringBuilder connectionString = new SqlConnectionStringBuilder();
            connectionString.UserID = userID;
            connectionString.Password = password;
            connectionString.InitialCatalog = initialCatalog;
            connectionString.DataSource = server;
            
            return connectionString.ConnectionString;
        }

        public Exception ConectToDB(string query)
        {
            Exception result = null;
            string connectionString = CreateConnectionString("SQLSERVER", "Test1", "sa", "nicecti1!");

            using (SqlConnection connection = new SqlConnection(
              connectionString))
            {
                SqlCommand command = new SqlCommand(query , connection);
                try
                {
                    command.Connection.Open();
                    SqlDataReader myReader = command.ExecuteReader();

                    while (myReader.Read())
                    {
                        Users.Add(new User(myReader.GetString(1).Trim(), myReader.GetString(2).Trim()));
                    }
                    command.Connection.Close();
                }
                catch (Exception ex) 
                {
                    result = ex;
                }

                return result;                
            }
        }

        public Exception AddNewUserToDB(string userID, string password)
        {
            Exception result = null;
            string connectionString = CreateConnectionString("SQLSERVER", "Test1", "sa", "nicecti1!");
            string query = "INSERT INTO dbo.Users VALUES ( '" + userID + "', '" + password + "')";
            using (SqlConnection connection = new SqlConnection(
              connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                try
                {
                    command.Connection.Open();
                    SqlDataReader myReader = command.ExecuteReader();

                    while (myReader.Read())
                    {
                        Users.Add(new User(myReader.GetString(1).Trim(), myReader.GetString(2).Trim()));
                    }
                    command.Connection.Close();
                }
                catch (Exception ex)
                {
                    result = ex;
                }
                return result;
            }
        }

        public bool CheckUserCredentionals(string userName, string password)
        {
            bool result = false;
            foreach (var item in Users)
            {
                if (item.UserName.ToLower() == userName.ToLower() && item.Password.ToLower() == password.ToLower())
                    result = true;
            }
            return result;
        }
    }
}
