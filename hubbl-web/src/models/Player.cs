using System.Collections.Generic;

namespace hubbl.web.models {
	
	public class Player {

		public enum Status {
			PLAY = 0,
			PAUSE = 1,
			STOP = 2
		}

	    public long id;
	    public Status status;
		private List<long> playlist;
		private Dictionary<long,Track> tracks;
		public int position;
		public int volume;

	    public Player(long id, Status status, List<long> playlist, Dictionary<long,Track> tracks,
	        int position, int volume) {
	        this.id = id;
	        this.status = status;
	        this.playlist = playlist;
	        this.tracks = tracks;
	        this.position = position;
	        this.volume = volume;
	    }

	    public Player() {
	        this.id = Settings.getNextPlayerId();
	        this.status = Status.STOP;
	        this.playlist = new List<long>();
	        this.tracks = new Dictionary<long, Track>();
	        this.position = 0;
	        this.volume = 100;
	    }

	}
}

