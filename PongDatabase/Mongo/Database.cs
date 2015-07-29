using System;
using System.Linq;
using MongoDB.Bson;
using MongoDB.Driver;

namespace PongDatabase.Mongo
{
	/// <summary>
	/// MongoDB version of the database functions
	/// </summary>
	public class Database
	{
		public Database()
		{
			const string ConnectionString = "mongodb://localhost/Pong";
			var settings = MongoClientSettings.FromUrl(new MongoUrl(ConnectionString));
			var client = new MongoClient(settings);
			data = client.GetServer().GetDatabase("Pong");
			players = data.GetCollection<Player>("Player");
			games = data.GetCollection<Game>("Game");
			highscores = data.GetCollection<HighScore>("HighScore");
		}

		private readonly MongoDatabase data;
		private readonly MongoCollection<Player> players;
		private readonly MongoCollection<Game> games;
		private readonly MongoCollection<HighScore> highscores;
		public bool IsConnected { get { return data != null; } }

		public void LoginPlayer(string username)
		{
			var player = GetPlayer(username);
			if (player != null)
				UpdateLastLogin(player);
			else
				CreatePlayer(username);
		}
		
		public Player GetPlayer(string username)
		{
			return players.FindOne(new QueryDocument("Username", username));
		}

		private void UpdateLastLogin(Player player)
		{
			players.Update(new QueryDocument("Username", player.Username),
				new UpdateDocument { { "$set", new BsonDocument("LastLogin", DateTime.UtcNow) } });
		}

		private void CreatePlayer(string username)
		{
			players.Insert(new Player { Username = username, LastLogin = DateTime.UtcNow });
		}

		public DateTime GetUserLastLogin(string username)
		{
			return GetPlayer(username).LastLogin;
		}
		
		public void SaveScoreForPlayer(string username, int score)
		{
			var player = GetPlayer(username);
			games.Insert(new Game
			{
				PlayerId = player.Id,
				Score = score,
				Played = DateTime.UtcNow
			});
			var currentHighScore = highscores.FindOne(new QueryDocument("PlayerId", player.Id));
			if (currentHighScore == null || score > currentHighScore.Score)
				SaveNewHighscore(player, score, currentHighScore != null);
		}

		private void SaveNewHighscore(Player player, int score, bool hadHighScoreAlready)
		{
			if (hadHighScoreAlready)
				highscores.Update(new QueryDocument("PlayerId", player.Id),
					new UpdateDocument { { "$set", new BsonDocument("Score", score) } });
			else
				highscores.Insert(new HighScore
				{
					PlayerId = player.Id,
					Score = score,
					PositionInRanking = 0
				});
			var all = highscores.FindAll().OrderByDescending(hs => hs.Score);
			int rank = 1;
			foreach (var hs in all)
				highscores.Update(new QueryDocument("PlayerId", hs.PlayerId),
					new UpdateDocument { { "$set", new BsonDocument("PositionInRanking", rank++) } });
		}

		public int GetUserHighScore(string username, out int scoreRank)
		{
			var getHighScore = highscores.FindOne(new QueryDocument("PlayerId", GetPlayer(username).Id));
			if (getHighScore != null)
			{
				scoreRank = getHighScore.PositionInRanking;
				return getHighScore.Score;
			}
			scoreRank = int.MaxValue;
			return 0;
		}
	}
}