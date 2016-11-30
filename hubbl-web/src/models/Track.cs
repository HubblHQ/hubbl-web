using System;

namespace hubbl.web.models {
	
	public class Track {

		public enum Status {
			PLAY = 0, PAUSE = 1, STOP = 2
		}

		public long id;
		public string title;
		public string author;
		public User publisher;
		public string providerURI;
		public int duration;
		public int likes = 0;
		public int dislikes = 0;
		public Status status = Status.STOP;
		
		public Track(string title, string author, User publisher, string providerURI, int duration) {
			this.id = Track.getNewID();
			this.title = title;
			this.author = author;
			this.publisher = publisher;
			this.providerURI = providerURI;
			this.duration = duration;
			this.save();
		}

		public Track(long id) {
			this.id = id;
			this.getByID();
		}

		public bool save() {
			return true;
		}

		private bool getByID() {
			return true;
		}

		private static long getNewID() {
			return 0;
		}

	}
}

