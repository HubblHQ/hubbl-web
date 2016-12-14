using System;
using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Priority_Queue;

namespace hubbl.web.models {
	
	public class Player {

		public enum Status {
			PLAY = 0,
			PAUSE = 1,
			STOP = 2
		}

	    public Status status;
		public long current;
		private ConcList<long> playlist;
		private List<long> pendingList;
		private Dictionary<long,Track> tracks;
		public int position;
		public int volume;

	    [BsonConstructor]
	    public Player(Status status, long current, List<long> playlist, List<long> pendingList, Dictionary<long,Track> tracks,
	        int position, int volume) {
	        this.status = status;
	        this.current = current;
	        this.playlist = playlist;
	        this.pendingList = pendingList;
	        this.tracks = tracks;
	        this.position = position;
	        this.volume = volume;

	        ;
	    }

	    public Player() {
	        this.status = Status.STOP;
	        this.current = 0;
	        this.playlist = new List<long>();
	        this.pendingList = new List<long>();
	        this.tracks = new Dictionary<long, Track>();
	        this.position = 0;
	        this.volume = 100;
	    }

	}
}

