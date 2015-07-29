using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace PongDatabase.Mongo
{
	public class Player
	{
		[BsonId]
		public ObjectId Id { get; set; }
		public string Username { get; set; }
		public DateTime LastLogin { get; set; }
	}
}