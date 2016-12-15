using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace hubbl.web.models {
	
	public class Track {

	    public enum Status {
	        PLAYING = 0,
	        ACCEPTED = 1,
	        PENDING = 2
	    }

	    public ObjectId id;
	    public string title;
		public string artist;
		public User publisher;
		public string providerURI;
		public int duration;
		public int likes;
		public int dislikes;
	    public Status status;

	    [BsonConstructor]
	    public Track(string title, string artist, User publisher, string providerURI, int duration, int likes, int dislikes, Status status) {
			this.title = title;
			this.artist = artist;
			this.publisher = publisher;
			this.providerURI = providerURI;
			this.duration = duration;
	        this.status = status;
	    }

	    public Track(string title, string artist, User publisher, string providerURI, int duration) {
	        this.title = title;
	        this.artist = artist;
	        this.publisher = publisher;
	        this.providerURI = providerURI;
	        this.duration = duration;
	        this.likes = 0;
	        this.dislikes = 0;
	        this.status = Status.PENDING;
	    }

	}
}

