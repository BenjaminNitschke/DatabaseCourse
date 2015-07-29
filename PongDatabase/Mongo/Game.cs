using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace PongDatabase.Mongo
{
	public class Game
	{
		[BsonId]
		public ObjectId Id { get; set; }
		public ObjectId PlayerId { get; set; }
		public int Score { get; set; }
		public DateTime Played { get; set; }
	}
}