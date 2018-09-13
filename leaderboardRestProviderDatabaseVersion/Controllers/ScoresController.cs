using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using leaderboardRestProviderDatabaseVersion.model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace leaderboardRestProviderDatabaseVersion.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ScoresController : ControllerBase
    {
        private const string ConnectionString =
            "Server=tcp:anbo-databaseserver.database.windows.net,1433;Initial Catalog=anbobase;Persist Security Info=False;User ID=anbo;Password=Secret12;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";

        // GET: api/Scores
        [HttpGet]
        [Route("")]
        public IEnumerable<Score> Get()
        {
            const string selectString = "select * from leaderboardscore order by point desc";
            using (SqlConnection databaseConnection = new SqlConnection(ConnectionString))
            {
                databaseConnection.Open();
                using (SqlCommand selectCommand = new SqlCommand(selectString, databaseConnection))
                {
                    using (SqlDataReader reader = selectCommand.ExecuteReader())
                    {
                        List<Score> scoreList = new List<Score>();
                        while (reader.Read())
                        {
                            Score user = ReadScore(reader);
                            scoreList.Add(user);
                        }
                        return scoreList;
                    }
                }
            }
        }

        internal static Score ReadScore(IDataRecord reader)
        {
            int id = reader.GetInt32(0);
            int userid = reader.GetInt32(1);
            int points = reader.GetInt32(2);
            DateTime? created;
            if (reader.IsDBNull(3)) created = null;
            else created = reader.GetDateTime(3);

            Score score = new Score
            {
                Id = id,
                UserId = userid,
                Points = points,
                Created = created
            };
            return score;
        }

        // GET: api/Scores/5
        [HttpGet("{scoreId}")]
        public string Get(int scoreId)
        {
            return "value";
        }

        // POST: api/Scores
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/Scores/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
