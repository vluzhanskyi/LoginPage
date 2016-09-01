using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace LoginPage
{
    public class User
    {
        public int UserId { set; get; }
        public string UserName { set; get; }
        public string Password { set; get; }
        public Dictionary<string, int> GamesScores = new Dictionary<string, int>();

        public User(int id, string name, string password)
        {
            UserName = name;
            Password = password;
            UserId = id;
        }
    }

   
}
