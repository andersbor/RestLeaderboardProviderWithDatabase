using System;

namespace leaderboardRestProviderDatabaseVersion.model
{
    public class Score
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int Points { get; set; }
        public DateTime? Created { get; set; }
    }
}
