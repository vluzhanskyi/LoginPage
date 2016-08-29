using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace LoginPage
{
    public class User
    {
        public int UserId { set; get; }
        public string UserName { set; get; }
        public string Password { set; get; }
        public Dictionary<string, int> GamesScores { set; get; }

        public User(int id, string name, string password)
        {
            UserName = name;
            Password = password;
            UserId = id;
        }
    }

    public class DbLogic
    {
        private User _user;
        private string CreateConnectionString(string server, string initialCatalog, string userId, string password)
        {
            var connectionString = new SqlConnectionStringBuilder
            {
                UserID = userId,
                Password = password,
                InitialCatalog = initialCatalog,
                DataSource = server
            };

            return connectionString.ConnectionString;
        }

        public bool ConectToDb(string userName, string password, out Exception exception)
        {
            var connectionString = CreateConnectionString("SQLSERVER", "Test1", "sa", "nicecti1!");
            exception = null;
            using (var connection = new SqlConnection(connectionString))
            {
                var command = new SqlCommand()
                {
                    Connection = connection,
                    CommandText =
                        string.Format(
                            "SELECT * FROM dbo.Users WHERE UserName = {userName} AND UserPassword = {password} ")
                };
                
                var result = false;
                try
                {
                    command.Connection.Open();
                    var myReader = command.ExecuteReader();

                    while (myReader.Read())
                    {
                        _user = new User(myReader.GetInt32(0), myReader.GetString(1).Trim(), myReader.GetString(2).Trim());
                        
                    }
                    command.Connection.Close();
                    result = true;
                }
                catch (Exception ex)
                {
                    exception = ex ;
                }
                
                return result;
            }
        }

        public bool AddNewUserToDb(string userId, string password, out Exception exception)
        {
            var result = false;
            exception = null;
            var connectionString = CreateConnectionString("SQLSERVER", "Test1", "sa", "nicecti1!");
            var query = string.Format("INSERT INTO dbo.Users VALUES ( '{userId}', '{password}')"); 
            using (var connection = new SqlConnection(connectionString))
            {
                try
                {
                    var command = new SqlCommand(query, connection);
                        
                    command.Connection.Open();     
                    command.Connection.Close();
                    result = true;
                }
                catch (Exception ex)
                {
                    exception = ex;
                }
                return result;
            }
        }


        public void CollectUserStatistics(out Exception exception)
        {
            var connectionString = CreateConnectionString("SQLSERVER", "Test1", "sa", "nicecti1!");
             exception = null;

            using (var connection = new SqlConnection(connectionString))
            {
                var command = new SqlCommand()
                {
                    
                    Connection = connection,
                    CommandText =
                        string.Format(
                            "SELECT * FROM dbo.UsersScores WHERE UserID = {user.UserId} ")
                };
                try
                {
                    command.Connection.Open();
                    var myReader = command.ExecuteReader();

                    while (myReader.Read())
                    {
                        _user.GamesScores.Add(myReader.GetString(1).Trim(), myReader.GetInt32(3));

                    }
                    command.Connection.Close();
                    
                } 
                catch(Exception ex)
                {
                    exception = ex;
                }

            } 
        }
    }
}
