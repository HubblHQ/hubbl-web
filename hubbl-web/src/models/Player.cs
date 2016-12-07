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

	    public ObjectId id;
	    public Status status = Status.STOP;
		public Track current = null;
		private SimplePriorityQueue<long> playlist = new SimplePriorityQueue<long>();
		private SimplePriorityQueue<long> pendingList = new SimplePriorityQueue<long>();
		private Dictionary<long,Track> tracks = new Dictionary<long,Track>();
		public int position = 0;
		public int volume = 100;

		public Player() {

		}



	}
}

