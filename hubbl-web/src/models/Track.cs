using System;

namespace hubbl.web.models {
	
	public class Track {

		public long id;
		public string title;
		public string artist;
		public User publisher;
		public string providerURI;
		public int duration;
		public int likes = 0;
		public int dislikes = 0;
		
		public Track(long id, string title, string artist, User publisher, string providerURI, int duration) {
			this.id = id;
			this.title = title;
			this.artist = artist;
			this.publisher = publisher;
			this.providerURI = providerURI;
			this.duration = duration;
		}

	}
}

