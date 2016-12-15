using System;
using Nancy.Hosting.Self;
using System.Threading;

namespace hubbl.web {

	class MainClass {

		private static NancyHost host;

		public static void Main(string[] args) {
			var exitEvent = new ManualResetEvent(false);

		    Settings.init();
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
		    Settings.save();
		}

	}
}
