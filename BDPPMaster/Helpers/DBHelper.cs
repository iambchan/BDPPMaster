using BDPPMaster.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;

namespace BDPPMaster.Helpers
{
    public static class DBHelper
    {
        private static readonly string _bdppmasterdb = ConfigurationManager.ConnectionStrings["bdppmasterdb"].ConnectionString;

        #region GET
        //required, one of: FirstName, LastName, ScreenName, BDLoginName, Email
        public static int GetPlayerIdByScreenLoginNames(string ScreenName, string BDLoginName) {
            var query = "SELECT PlayerId FROM [Players] WHERE ScreenName = @ScreenName OR BDLoginName = @BDLoginName";
            using (var connection = new SqlConnection(_bdppmasterdb))
            {
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ScreenName", ScreenName);
                    command.Parameters.AddWithValue("@BDLoginName", BDLoginName);
                    connection.Open();
                    var playerId = Convert.ToInt32(command.ExecuteScalar());
                    return playerId;
                }
            }
        }
        public static Player GetPlayerInfo(string FirstName, string LastName, string ScreenName, string BDLoginName, string Email, string RFID) {
            var queryParams = new StringBuilder();
            #region Append Conditions
            if (FirstName != null) { queryParams.Append("FirstName = @FirstName"); }
            if (LastName != null) {
                if (queryParams.Length > 0) {
                    queryParams.Append(" AND ");
                }
                queryParams.Append("LastName = @LastName");
            }
            if (ScreenName != null)
            {
                if (queryParams.Length > 0)
                {
                    queryParams.Append(" AND ");
                }
                queryParams.Append("ScreenName = @ScreenName");
            }
            if (BDLoginName != null)
            {
                if (queryParams.Length > 0)
                {
                    queryParams.Append(" AND ");
                }
                queryParams.Append("BDLoginName = @BDLoginName");
            }
            if (Email != null)
            {
                if (queryParams.Length > 0)
                {
                    queryParams.Append(" AND ");
                }
                queryParams.Append("Email = @Email");
            }
            if (RFID != null)
            {
                if (queryParams.Length > 0)
                {
                    queryParams.Append(" AND ");
                }
                queryParams.Append("RFID = @RFID");
            }
            #endregion

            var query = String.Format("SELECT * FROM [Players] WHERE {0};", queryParams.ToString());
            using (var connection = new SqlConnection(_bdppmasterdb))
            {
                using (var command = new SqlCommand(query.ToString(), connection))
                {
                    connection.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        if (!reader.HasRows) { return null; }
                        reader.Read(); //only need the first item
                        var player = new Player()
                        {
                            PlayerId = reader.GetInt32(reader.GetOrdinal("PlayerId")),
                            FirstName = reader["FirstName"].ToString(),
                            LastName = reader["LastName"].ToString(),
                            ScreenName = reader["screenName"].ToString(),
                            BDLoginName = reader["BDLoginName"].ToString(),
                            Email = reader["Email"].ToString(),
                            RFID = reader["RFID"].ToString(),
                            ImageNameWithExt = reader["ImageNameWithExt"].ToString()
                        };
                        return player;
                    }
                }
            }
        }
        public static Team GetTeamPlayersInfo(int TeamId)
        {
            var team = new Team();
            var query = @"SELECT t.TeamId, p.* 
                          FROM [Teams] t
                          INNER JOIN [Players] p
                          ON p.PlayerId IN (t.Player1_Id, t.Player2_Id)
                          WHERE t.TeamId = @TeamId;";
            using (var connection = new SqlConnection(_bdppmasterdb))
            {
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@TeamId", TeamId);
                    connection.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        if (!reader.HasRows) { return null; }
                        while (reader.Read()) {
                            team.TeamId = reader.GetInt32(reader.GetOrdinal("TeamId")); //TeamId is constant, overwrite is not a concern
                            var player = new Player()
                            {
                                PlayerId = reader.GetInt32(reader.GetOrdinal("PlayerId")),
                                FirstName = reader["FirstName"].ToString(),
                                LastName = reader["LastName"].ToString(),
                                ScreenName = reader["screenName"].ToString(),
                                BDLoginName = reader["BDLoginName"].ToString(),
                                Email = reader["Email"].ToString(),
                                RFID = reader["RFID"].ToString(),
                                ImageNameWithExt = reader["ImageNameWithExt"].ToString()
                            };
                            team.Players.Add(player);
                        }
                        return team;
                    }
                }
            }
        }
        public static Game GetGameInfo(int GameId)
        {
            var game = new Game() { 
                Teams = new List<Team>()
            };
            var team = new Team();
            var currentTeamId = 0;
            var nextTeamId = 0;
            var query = @"SELECT * FROM [Games] G
                          INNER JOIN [Teams] t ON t.TeamId IN (g.Team1_id, g.Team2_id)
                          INNER JOIN [Players] P ON p.PlayerId IN (t.Player1_id, t.Player2_id)
                          WHERE g.GameId = @GameId;";
            using (var connection = new SqlConnection(_bdppmasterdb))
            {
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@GameId", GameId);
                    connection.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        if (!reader.HasRows) { return null; }
                        while (reader.Read())
                        {
                            //Constants, overwrite is not a concern:
                            game.GameId = reader.GetInt32(reader.GetOrdinal("GameId"));
                            game.Team1_Score = reader.GetInt32(reader.GetOrdinal("Team1_Score"));
                            game.Team2_Score = reader.GetInt32(reader.GetOrdinal("Team2_Score"));
                            nextTeamId = reader.GetInt32(reader.GetOrdinal("TeamId"));

                            if (currentTeamId != nextTeamId) {
                                if(currentTeamId > 0){ game.Teams.Add(team); }
                                currentTeamId = nextTeamId;
                                team = new Team() {
                                    TeamId = currentTeamId,
                                    Players = new List<Player>()
                                };
                            }
                            team.Players.Add(new Player()
                            {
                                PlayerId = reader.GetInt32(reader.GetOrdinal("PlayerId")),
                                FirstName = reader["FirstName"].ToString(),
                                LastName = reader["LastName"].ToString(),
                                ScreenName = reader["screenName"].ToString(),
                                BDLoginName = reader["BDLoginName"].ToString(),
                                Email = reader["Email"].ToString(),
                                RFID = reader["RFID"].ToString(),
                                ImageNameWithExt = reader["ImageNameWithExt"].ToString()
                            });
                        }
                        return game;
                    }
                }
            }
        }
        #endregion

        #region CREATE
        //required, all of: FirstName, LastName, ScreenName, BDLoginName, Email
        public static Player CreateNewPlayer(string FirstName, string LastName, string ScreenName, string BDLoginName, string Email, string RFID) //returns int PlayerId
        {
            var query = @"INSERT INTO [Players] (FirstName, LastName, ScreenName, BDLoginName, Email, RFID)
                          OUTPUT Inserted.PlayerId
                          VALUES (@FirstName, @LastName, @ScreenName, @BDLoginName, @Email, @RFID);";
            using (var connection = new SqlConnection(_bdppmasterdb)) {
                using (var command = new SqlCommand(query, connection)) {
                    command.Parameters.AddWithValue("@FirstName", FirstName);
                    command.Parameters.AddWithValue("@LastName", LastName);
                    command.Parameters.AddWithValue("@ScreenName", ScreenName);
                    command.Parameters.AddWithValue("@BDLoginName", BDLoginName);
                    command.Parameters.AddWithValue("@Email", Email);
                    command.Parameters.AddWithValue("@RFID", RFID);
                    connection.Open();
                    using (var reader = command.ExecuteReader()) {
                        if (!reader.HasRows) { return null; }
                        reader.Read(); //only need the first item
                        var player = new Player()
                        {
                            PlayerId = reader.GetInt32(reader.GetOrdinal("PlayerId")),
                            FirstName = reader["FirstName"].ToString(),
                            LastName = reader["LastName"].ToString(),
                            ScreenName = reader["screenName"].ToString(),
                            BDLoginName = reader["BDLoginName"].ToString(),
                            Email = reader["Email"].ToString(),
                            RFID = reader["RFID"].ToString()
                        };

                        //if (!reader["ProfileImage"].Equals(DBNull.Value)) { 
                        //    //process image here
                        //}

                        return player;
                    }
                }
            }
        }
        public static int CreateNewTeam(int Player1_Id, int Player2_Id = 0) //returns int TeamId
        {
            var query = @"INSERT INTO [Teams] (Player1_Id, Player2_Id) 
                          OUTPUT Inserted.TeamId
                          VALUES (@Player1_Id, @Player2_Id);";
            using (var connection = new SqlConnection(_bdppmasterdb))
            {
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Player1_Id", Player1_Id);
                    command.Parameters.AddWithValue("@Player2_Id", Player2_Id);
                    connection.Open();
                    var teamId = Convert.ToInt32(command.ExecuteScalar());
                    return teamId;
                }
            }
        }
        public static int CreateNewGame(int Team1_id, int Team2_id) //returns int GameId
        {
            var query = @"INSERT INTO [Games] (Team1_id, Team2_id, StartDateTime) 
                          OUTPUT Inserted.GameId
                          VALUES (@Team1_id, @Team2_id, @StartDateTime);";
            using (var connection = new SqlConnection(_bdppmasterdb))
            {
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Team1_id", Team1_id);
                    command.Parameters.AddWithValue("@Team2_id", Team2_id);
                    command.Parameters.AddWithValue("@StartDateTime", DateTime.Now);
                    connection.Open();
                    var gameId = Convert.ToInt32(command.ExecuteScalar());
                    return gameId;
                }
            }
        }
        #endregion

   //     // Set the connection, command, and then execute the command with non query.
   //   public static Int32 ExecuteNonQuery(String connectionString, String commandText,
   //       CommandType commandType, params SqlParameter[] parameters) {
   //      using (SqlConnection conn = new SqlConnection(connectionString)) {
   //         using (SqlCommand cmd = new SqlCommand(commandText, conn)) {
   //            // There're three command types: StoredProcedure, Text, TableDirect. The TableDirect 
   //            // type is only for OLE DB.  
   //            cmd.CommandType = commandType;
   //            cmd.Parameters.AddRange(parameters);

   //            conn.Open();
   //            return cmd.ExecuteNonQuery();
   //         }
   //      }
   //   }

   //   // Set the connection, command, and then execute the command and only return one value.
   //   public static Object ExecuteScalar(String connectionString, String commandText,
   //       CommandType commandType, params SqlParameter[] parameters) {
   //      using (SqlConnection conn = new SqlConnection(connectionString)) {
   //         using (SqlCommand cmd = new SqlCommand(commandText, conn)) {
   //            cmd.CommandType = commandType;
   //            cmd.Parameters.AddRange(parameters);

   //            conn.Open();
   //            return cmd.ExecuteScalar();
   //         }
   //      }
   //   }

   //   // Set the connection, command, and then execute the command with query and return the reader.
   //   public static SqlDataReader ExecuteReader(String connectionString, String commandText,
   //       CommandType commandType, params SqlParameter[] parameters) {
   //      SqlConnection conn = new SqlConnection(connectionString);

   //      using (SqlCommand cmd = new SqlCommand(commandText, conn)) {
   //         cmd.CommandType = commandType;
   //         cmd.Parameters.AddRange(parameters);

   //         conn.Open();
   //         // When using CommandBehavior.CloseConnection, the connection will be closed when the 
   //         // IDataReader is closed.
   //         SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

   //         return reader;
   //      }
   //   }
   //}


        #region UPDATE
        #endregion
    }
}