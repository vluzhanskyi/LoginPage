using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoginPage
{
    interface IConnect
    {
        User User { set; get; }

        string ConnectionString { set; get; }

        string CreateConnectionString(string server, string initialCatalog, string userId, string password);
    }
}
