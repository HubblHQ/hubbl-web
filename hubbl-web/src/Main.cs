using System;
using Nancy.Hosting.Self;
using System.Diagnostics;
using System.Threading;

using System.Threading.Tasks;
//using FluentAssertions;
using MongoDB.Driver.Core;
using MongoDB.Driver;
using MongoDB.Bson;

namespace hubbl.web {
	
	class MainClass {
		
		private static NancyHost host;

		protected static IMongoClient client;
		protected static IMongoDatabase database;

		public static void Main (string[] args) {

			client = new MongoClient();
			database = client.GetDatabase("test");

			var collection = database.GetCollection<BsonDocument>("restaurants");
			var filter = Builders<BsonDocument>.Filter.Eq("name", "Juni");
			var update = Builders<BsonDocument>.Update
				.Set("cuisine", "American (New)")
				.CurrentDate("lastModified");
			collection.UpdateOneAsync(filter, update).Wait();

			//System.Console.WriteLine(result.ToString());

			//result.MatchedCount.Should().Be(1);
			//if (result.IsModifiedCountAvailable)
			//{
				//result.ModifiedCount.Should().Be(1);
			//}



			var exitEvent = new ManualResetEvent(false);

			host = new NancyHost(
				new Uri(Settings.SERVER_URL)
			);

			host.Start();

			Console.CancelKeyPress += (sender, eventArgs) => {
				eventArgs.Cancel = true;
				exitEvent.Set();
			};
				
			Console.WriteLine("Nancy serving " + Settings.SERVER_URL);
			//Process.Start(Settings.SERVER_URL);
			exitEvent.WaitOne();
			host.Stop ();
		}

	}
}
