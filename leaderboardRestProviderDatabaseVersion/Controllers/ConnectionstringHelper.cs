using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace leaderboardRestProviderDatabaseVersion.Controllers
{
    public class ConnectionstringHelper
    {
        internal static string GetConnectionString()
        {
            StreamReader reader = new StreamReader("connectionstring.txt");
            return reader.ReadLine();
        }
    }
}
