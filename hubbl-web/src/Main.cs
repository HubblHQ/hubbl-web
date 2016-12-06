using System;
using Nancy.Hosting.Self;
using System.Diagnostics;
using System.Threading;

using System.Threading.Tasks;
using hubbl.web.models;
using MongoDB.Driver;

namespace hubbl.web {

	class MainClass {

		private static NancyHost host;

		public static void Main(string[] args)
		{
			var exitEvent = new ManualResetEvent(false);

		    var client = new MongoClient();
		    var db = client.GetDatabase("hubbl");

		    IMongoCollection<User> persons = db.GetCollection<User>("users");

		    persons.InsertOne(new User("user", "userr", "userrr"));

		    var result = persons.Find("{}").ToList();

		    foreach (var v in result) {
		        Console.WriteLine(v.id + v.login + " " + v.password + v.name);
		    }



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
			host.Stop();
		}

	}
}
