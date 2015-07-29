using System;
using System.Collections.Generic;
using System.Linq;

namespace PongServer
{
	public class Database
	{
		public Database()
		{
			data = new PongDataContext();
		}

		private readonly PongDataContext data;
		public bool IsConnected { get { return data != null; } }

		public void LoginPlayer(string username)
		{
			int playerId = GetPlayerId(username);
			if (playerId > 0)
				UpdateLastLogin(playerId);
			else
				CreatePlayer(username);
		}
		
		public int GetPlayerId(string username)
		{
			return
				(from player in data.Players
				 where player.Username == username
				 select player.Id).FirstOrDefault();
		}

		private void UpdateLastLogin(int playerId)
		{
			var player = data.Players.First(p => p.Id == playerId);
			player.LastLogin = DateTime.UtcNow;
			data.SubmitChanges();
		}

		private void CreatePlayer(string username)
		{
			data.Players.InsertOnSubmit(new Player { Username = username, LastLogin = DateTime.UtcNow });
			data.SubmitChanges();
		}

		public DateTime GetUserLastLogin(string username)
		{
			return (from player in data.Players
							where player.Username == username
							select player.LastLogin).First();
		}
		
		public void SaveScoreForPlayer(string username, int score)
		{
			var playerId = GetPlayerId(username);
			data.Games.InsertOnSubmit(new Game
			{
				PlayerId = playerId,
				Score = score,
				Played = DateTime.UtcNow
			});
			data.SubmitChanges();
			int currentHighScore =
				(from highScore in data.HighScores
				where highScore.PlayerId == playerId
				select highScore.Score).FirstOrDefault();
			if (score > currentHighScore)
				SaveNewHighscore(playerId, score, currentHighScore != 0);
		}

		private void SaveNewHighscore(int playerId, int score, bool hadHighScoreAlready)
		{
			if (hadHighScoreAlready)
				data.HighScores.First(h => h.PlayerId == playerId).Score = score;
			else
				data.HighScores.InsertOnSubmit(new HighScore
				{
					PlayerId = playerId,
					Score = score,
					PositionInRanking = 0
				});
			data.SubmitChanges();
			List<int> playerIds =
				(from highScore in data.HighScores
				orderby score descending 
				select highScore.PlayerId).ToList();
			int rank = 1;
			foreach (var id in playerIds)
				data.HighScores.First(h => h.PlayerId == id).PositionInRanking = rank++;
			data.SubmitChanges();
		}

		public int GetUserHighScore(string username, out int scoreRank)
		{
			var getHighScore =
				from highScore in data.HighScores
				where highScore.PlayerId == GetPlayerId(username)
				select new { highScore.Score, highScore.PositionInRanking };
			if (getHighScore.Any())
			{
				scoreRank = getHighScore.First().PositionInRanking;
				return getHighScore.First().Score;
			}
			scoreRank = int.MaxValue;
			return 0;
		}
	}
}