using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace LoginPage
{
    class DbDisconnectedLayer : IConnect
    {
        public User User { get; set; }
        public string ConnectionString { get; set; }
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



    }
}
