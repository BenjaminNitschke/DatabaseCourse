using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading;

namespace PongServer
{
	public class SqlDatabase
	{
		public SqlDatabase()
		{
			const string ConnectionString = "Data Source=localhost;" +
				"Initial Catalog=Pong;Persist Security Info=True;User ID=Pong;Password=abc123";
      connection = new SqlConnection(ConnectionString);
			connection.Open();
		}

		private readonly SqlConnection connection;
		public bool IsConnected
		{ get { return connection.State == ConnectionState.Open; } }

		public void LoginPlayer(string username)
		{
			var getPlayer =
				new SqlCommand("Select Id From Player Where Username=@Username", connection);
			getPlayer.Parameters.AddWithValue("@Username", username);
			var reader = getPlayer.ExecuteReader();
			if (reader.Read())
			{
				var playerId = (int)reader[0];
				reader.Close();
				UpdateLastLogin(playerId);
			}
			else
			{
				reader.Close();
				CreatePlayer(username);
			}
		}

		private void UpdateLastLogin(int playerId)
		{
			var updateLastLogin =
				new SqlCommand("Update Player Set LastLogin=@LastLogin Where Id="+playerId,
					connection);
			updateLastLogin.Parameters.AddWithValue("@LastLogin", DateTime.UtcNow);
			updateLastLogin.ExecuteNonQuery();
		}

		private void CreatePlayer(string username)
		{
			var addPlayer =
				new SqlCommand("Insert Into Player (Username, LastLogin) Values (@Username, @LastLogin)",
					connection);
			addPlayer.Parameters.AddWithValue("@Username", username);
			addPlayer.Parameters.AddWithValue("@LastLogin", DateTime.UtcNow);
			addPlayer.ExecuteNonQuery();
		}

		public DateTime GetUserLastLogin(string username)
		{
			var getUserLogin =
				new SqlCommand("Select LastLogin From Player Where Username=@Username", connection);
			getUserLogin.Parameters.AddWithValue("@Username", username);
			var reader = getUserLogin.ExecuteReader();
			if (!reader.HasRows)
				throw new UserDoesNotExist(username);
			reader.Read();
			return (DateTime)reader["LastLogin"];
		}

		public class UserDoesNotExist : Exception
		{
			public UserDoesNotExist(string username) : base(username) {}
		}

		public void SaveScoreForPlayer(string username, int score)
		{
			new Thread(() =>
			{
				var playerId = GetPlayerId(username);
				var setScore =
					new SqlCommand(
						"Insert Into Game (PlayerId, Score, Played) Values (@PlayerId, @Score, @Played)",
						connection);
				setScore.Parameters.AddWithValue("@PlayerId", playerId);
				setScore.Parameters.AddWithValue("@Score", score);
				setScore.Parameters.AddWithValue("@Played", DateTime.UtcNow);
				setScore.ExecuteNonQuery();
				var getHighscore = new SqlCommand(
					"Select Score From HighScore Where PlayerId=" + playerId, connection);
				var reader = getHighscore.ExecuteReader();
				int currentHighScore = 0;
				if (reader.Read())
					currentHighScore = (int)reader["Score"];
				reader.Close();
				if (score > currentHighScore)
					SaveNewHighscore(playerId, score, currentHighScore != 0);
			}).Start();
		}

		private void SaveNewHighscore(int playerId, int score, bool hadHighScoreAlready)
		{
			if (hadHighScoreAlready)
			{
				var updateHighScore =
					new SqlCommand(
						"Update HighScore Set Score=" + score +
						" Where PlayerId=" + playerId, connection);
				updateHighScore.ExecuteNonQuery();
			}
			else
			{
				var updateHighScore =
					new SqlCommand(
						"Insert Into HighScore (PlayerId, Score, PositionInRanking) Values (" + playerId + ", " +
						score + ", 0)", connection);
				updateHighScore.ExecuteNonQuery();
			}
			var updateRanks = new SqlCommand("Select PlayerId From HighScore Order By Score Desc",
				connection);
			var reader = updateRanks.ExecuteReader();
			List<int> playerIds = new List<int>();
			while (reader.Read())
				playerIds.Add((int)reader[0]);
			reader.Close();
			int rank = 1;
			foreach (var id in playerIds)
				UpdateRank(id, rank++);
		}

		private void UpdateRank(int playerId, int rank)
		{
			var updatePlayerRanks =
				new SqlCommand("Update HighScore Set PositionInRanking=" + rank +
				" Where PlayerId=" + playerId, connection);
			updatePlayerRanks.ExecuteNonQuery();
		}

		public int GetUserHighScore(string username, out int scoreRank)
		{
			var getHighScore =
				new SqlCommand(
					"Select Score, PositionInRanking From HighScore " +
					"Inner Join Player On Player.Id = HighScore.PlayerId " +
					"Where Player.Username=@Username",
					connection);
			getHighScore.Parameters.AddWithValue("@Username", username);
			var reader = getHighScore.ExecuteReader();
			if (reader.Read())
			{
				var score = (int)reader["Score"];
				scoreRank = (int)reader["PositionInRanking"];
				reader.Close();
				return score;
			}
			reader.Close();
			scoreRank = int.MaxValue;
			return 0;
		}

		public int GetPlayerId(string username)
		{
			var getPlayer = new SqlCommand("Select Id From Player Where Player.Username=@Username",
				connection);
			getPlayer.Parameters.AddWithValue("@Username", username);
			var reader = getPlayer.ExecuteReader();
			if (reader.Read())
			{
				var playerId = (int)reader["Id"];
				reader.Close();
				return playerId;
			}
			reader.Close();
			return 0;
		}
	}
}