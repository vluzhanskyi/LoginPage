using System;
using System.Data.SqlClient;

namespace LoginPage
{
   public class DbConectedLayer : IConnect
    {
        public string ConnectionString { set; get; }
        public User User { set; get; }

       public DbConectedLayer()
        {
            ConnectionString = CreateConnectionString("SQLSERVER\\SQLEXPRESS", "Test1", "sa", "nicecti1!");
        }

        public string CreateConnectionString(string server, string initialCatalog, string userId, string password)
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
            exception = null;
            using (var connection = new SqlConnection(ConnectionString))
            {
                var command = new SqlCommand()
                {
                    Connection = connection,
                    CommandText =
                        string.Format(
                            "SELECT * FROM dbo.Users WHERE UserName = '{0}' AND UserPassword = '{1}'", userName, password)
                };

                var result = false;
                try
                {
                    command.Connection.Open();
                    var myReader = command.ExecuteReader();

                    while (myReader.Read())
                    {
                        User = new User(myReader.GetInt32(0), myReader.GetString(1).Trim(), myReader.GetString(2).Trim());

                    }
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

        public bool AddNewUserToDb(string userId, string password, out Exception exception)
        {
            var result = false;
            exception = null;
            var query = string.Format("INSERT INTO dbo.Users VALUES ( '{0}', '{1}')", userId, password);
            using (var connection = new SqlConnection(ConnectionString))
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
            exception = null;

            using (var connection = new SqlConnection(ConnectionString))
            {
                var command = new SqlCommand()
                {

                    Connection = connection,
                    CommandText =
                        string.Format(
                            "SELECT * FROM dbo.UsersStat WHERE UserID = {0}", User.UserId)
                };
                try
                {
                    command.Connection.Open();
                    var myReader = command.ExecuteReader();

                    while (myReader.Read())
                    {
                        var GameName = myReader.GetString(1).Trim();
                        var res = myReader.GetInt32(3);
                        User.GamesScores.Add(GameName, res);

                    }
                    command.Connection.Close();

                }
                catch (Exception ex)
                {
                    exception = ex;
                }

            }
        }
    }
}
