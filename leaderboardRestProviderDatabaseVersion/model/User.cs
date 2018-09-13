using System;

namespace leaderboardRestProviderDatabaseVersion.model
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string PasswordHashed { get; set; }
        public DateTime? Created { get; set; }
    }
}
