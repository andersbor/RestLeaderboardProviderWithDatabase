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
            return "Server=tcp:anbo-databaseserver.database.windows.net,1433;Initial Catalog=anbobase;Persist Security Info=False;User ID=anbo;Password=Hemmelig14;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
            /*
            StreamReader reader = new StreamReader("connectionstring.txt");
            string str = reader.ReadLine();
            reader.Close();
            return str;
            */
        }
    }
}
