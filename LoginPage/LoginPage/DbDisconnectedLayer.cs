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
      
        public DataSet GetDataFromDb()
        {
            DataSet Users = new DataSet();
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

                DataTable userTable = new DataTable();
                DataTable gameTable = new DataTable();
                SqlDataAdapter dataAdapter = new SqlDataAdapter();

                DataColumn userId = new DataColumn("UserId", typeof(int));
                DataColumn userNname = new DataColumn("UserName", typeof(string));
                DataColumn userPassword = new DataColumn("UserPassword", typeof(string));
                DataColumn gameName = new DataColumn("Game", typeof(string));
                DataColumn userScore = new DataColumn("Score", typeof(int));
                userTable.TableName = "Users";
                userTable.Columns.Add(userId);
                userTable.Columns.Add(userNname);
                userTable.Columns.Add(userPassword);
                userTable.PrimaryKey = new DataColumn[] { userId };
                gameTable.TableName = "Games";
                gameTable.Columns.Add(userId);
                gameTable.Columns.Add(gameName);
                gameTable.Columns.Add(userScore);
                gameTable.PrimaryKey = new DataColumn[] { gameName };

                using (SqlCommand command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = "SELECT * FROM [Test1].[dbo].[Users]";
                    dataAdapter.SelectCommand = command;
                    dataAdapter.Fill(userTable);
                    command.CommandText = "SELECT * FROM [Test1].[dbo].[UsersStat]";
                    dataAdapter.SelectCommand = command;
                    dataAdapter.Fill(gameTable);
                }
                Users.Tables.Add(userTable);
                Users.Tables.Add(gameTable);
                return Users;
            }
            
        }

        public bool GetUserStat(User user, DataSet data)
        {
            bool result = false;
            using(DataTableReader userReader = new DataTableReader(data.Tables["Users"]))
            {
                while (userReader.Read())
                {
                    if (user.UserName == userReader.GetString(1) && user.Password == userReader.GetString(2))
                    {
                        User.UserName = user.UserName;
                        User.UserId = userReader.GetInt32(0);
                        result = true;
                    }
                }
            }

            using(DataTableReader statReader = new DataTableReader(data.Tables["Games"]))
            {
                while (statReader.Read())
                {
                    if (User.UserId == statReader.GetInt32(0))
                    {
                        User.GamesScores.Add(statReader.GetString(1), statReader.GetInt32(2));
                    }
                }
            }
            return result;
        }

    }
}
