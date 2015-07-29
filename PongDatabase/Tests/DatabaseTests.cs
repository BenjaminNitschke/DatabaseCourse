using System;
using System.Linq;
using NUnit.Framework;
using PongDatabase.Mongo;

namespace PongDatabase.Tests
{
	public class DatabaseTests
	{
		[SetUp]
		public void ConnectionToDatabase()
		{
			database = new Database();
			Assert.That(database.IsConnected, Is.True);
		}

		private Database database;

		[Test]
		public void PlayingAroundWithLinq()
		{
			var list = new int[] { 1, 2, 5, 7 };
			var query =
				from number in list
				where number >= 5
				select number.ToString();
			Assert.That(query.First(), Is.EqualTo("5"));
		}

		[TestCase("User1")]
		[TestCase("User2")]
		public void LoginPlayer(string username)
		{
			database.LoginPlayer(username);
			Assert.That(database.GetUserLastLogin(username),
				Is.GreaterThan(DateTime.UtcNow.AddSeconds(-0.2f)));
		}

		[Test]
		public void CheckIfPlayerExists()
		{
			Assert.That(database.GetPlayer("User1"), Is.Not.Null);
		}

		[Test]
		public void SaveScoreAtEndOfGame()
		{
			database.SaveScoreForPlayer("User1", 10);
			database.SaveScoreForPlayer("User2", 6);
			database.SaveScoreForPlayer("User2", 8);
			database.SaveScoreForPlayer("User2", 4);
			int rankUser1, rankUser2;
			Assert.That(database.GetUserHighScore("User1", out rankUser1), Is.EqualTo(10));
			Assert.That(database.GetUserHighScore("User2", out rankUser2), Is.EqualTo(8));
			Assert.That(rankUser1, Is.LessThan(rankUser2));
		}
	}
}