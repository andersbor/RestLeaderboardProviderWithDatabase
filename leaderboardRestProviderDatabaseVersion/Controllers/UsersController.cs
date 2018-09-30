using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using leaderboardRestProviderDatabaseVersion.model;
using Microsoft.AspNetCore.Mvc;

namespace leaderboardRestProviderDatabaseVersion.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly string _connectionString = Connectionstring.GetConnectionString();

        // GET: api/Users
        [HttpGet]
        public IEnumerable<User> Get()
        {
            const string selectString = "select * from leaderboarduser order by id";
            using (SqlConnection databaseConnection = new SqlConnection(_connectionString))
            {
                databaseConnection.Open();
                using (SqlCommand selectCommand = new SqlCommand(selectString, databaseConnection))
                {
                    using (SqlDataReader reader = selectCommand.ExecuteReader())
                    {
                        List<User> userList = new List<User>();
                        while (reader.Read())
                        {
                            User user = ReadUser(reader);
                            userList.Add(user);
                        }
                        return userList;
                    }
                }
            }
        }

        [Route("{userId}/scores")]
        public IEnumerable<Score> GetScoresByUserId(int userId)
        {
            // TODO combine with GetAll from scoreController
            const string selectString = "select * from leaderboardscore where userid=@userid order by point desc";
            using (SqlConnection databaseConnection = new SqlConnection(_connectionString))
            {
                databaseConnection.Open();
                using (SqlCommand selectCommand = new SqlCommand(selectString, databaseConnection))
                {
                    selectCommand.Parameters.AddWithValue("@userid", userId);
                    using (SqlDataReader reader = selectCommand.ExecuteReader())
                    {
                        List<Score> scoreList = new List<Score>();
                        while (reader.Read())
                        {
                            Score score = ScoresController.ReadScore(reader);
                            scoreList.Add(score);
                        }
                        return scoreList;
                    }
                }
            }
        }


       
        private static User ReadUser(IDataRecord reader)
        {
            int id = reader.GetInt32(0);
            string username = reader.GetString(1);
            string passwordHashed = reader.IsDBNull(2) ? null : reader.GetString(2);
            // DateTime is a struct (not a class).
            // That's why we need to add the question mark to DateTime?
            DateTime? created;
            if (reader.IsDBNull(3)) created = null;
            else created = reader.GetDateTime(3);

            User user = new User
            {
                Id = id,
                Username = username,
                PasswordHashed = passwordHashed,
                Created = created
            };
            return user;
        }

        // GET: api/Users/5
        [HttpGet("{id}", Name = "Get")]
        public User Get(int id)
        {
            const string selectString = "select * from leaderboarduser where id=@id";
            using (SqlConnection databaseConnection = new SqlConnection(_connectionString))
            {
                databaseConnection.Open();
                using (SqlCommand selectCommand = new SqlCommand(selectString, databaseConnection))
                {
                    selectCommand.Parameters.AddWithValue("@id", id);
                    using (SqlDataReader reader = selectCommand.ExecuteReader())
                    {
                        if (!reader.HasRows) { return null; }
                        reader.Read(); // advance cursor to first row
                        return ReadUser(reader);
                    }
                }
            }
        }

        // POST: api/Users
        [HttpPost]
        public int Post([FromBody] User value)
        {
            // TODO how to get the object form the database (in a easy way?)
            const string insertStudent = "insert into leaderboarduser (username, passwordhashed) values (@username, @passwordhashed)";
            using (SqlConnection databaseConnection = new SqlConnection(_connectionString))
            {
                databaseConnection.Open();
                using (SqlCommand insertCommand = new SqlCommand(insertStudent, databaseConnection))
                {
                    insertCommand.Parameters.AddWithValue("@username", value.Username);
                    if (value.PasswordHashed == null)
                    {
                        insertCommand.Parameters.AddWithValue("@passwordhashed", DBNull.Value);
                    }
                    else
                    {
                        insertCommand.Parameters.AddWithValue("@passwordhashed", value.PasswordHashed);
                    }
                    int rowsAffected = insertCommand.ExecuteNonQuery();
                    return rowsAffected;
                }
            }
        }

        // PUT: api/Users/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] User value)
        {
            throw new NotImplementedException("PUT user not implemented");
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public int Delete(int id)
        {
            const string deleteString = "delete from leaderboarduser where id=@id";
            using (SqlConnection databaseConnection = new SqlConnection(_connectionString))
            {
                databaseConnection.Open();
                using (SqlCommand insertCommand = new SqlCommand(deleteString, databaseConnection))
                {
                    insertCommand.Parameters.AddWithValue("@id", id);
                    int rowsAffected = insertCommand.ExecuteNonQuery();
                    return rowsAffected;
                }
            }
        }
    }
}
