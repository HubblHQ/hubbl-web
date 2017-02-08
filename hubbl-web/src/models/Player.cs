using System.Collections.Concurrent;

namespace hubbl.web.models {
	
	public class Player {

		public enum Status {
			PLAY = 0,
			PAUSE = 1,
			STOP = 2
		}

	    public string hubId { get; private set; }

	    public Status status;

		private ConcurrentQueue<long> playlist;
		private ConcurrentDictionary<long,Track> tracks;

	    public int position;
	    public int volume;

	    public Player(string hubId, Status status, ConcurrentQueue<long> playlist,
	        ConcurrentDictionary<long,Track> tracks, int position, int volume) {
	        this.hubId = hubId;
	        this.status = status;
	        this.playlist = playlist;
	        this.tracks = tracks;
	        this.position = position;
	        this.volume = volume;
	    }

	    public Player(string hubId) {
	        this.hubId = hubId;
	        this.status = Status.STOP;
	        this.playlist = new ConcurrentQueue<long>();
	        this.tracks = new ConcurrentDictionary<long, Track>();
	        this.position = 0;
	        this.volume = 100;
	    }

	    public void addTrack(Track track) {
	        tracks[track.id] = track;
	        playlist.Enqueue(track.id);
	    }

	    public Track getTrack(long id) {
	        return tracks[id];
	    }

	    public void nextTrack() {
	        if (playlist.IsEmpty) return;
	        long curTrackId;
	        Track curTrack;
	        if (playlist.TryDequeue(out curTrackId) && tracks.TryRemove(curTrackId, out curTrack)) {
	            if (playlist.TryPeek(out curTrackId)) {
	                tracks[curTrackId].status = Track.Status.PLAYING;
	            } else {
	                this.status = Status.STOP;
	            }
	        } else {
                Logger.writeLine("Error while switching to the next track in Player.nextTrack()");
	        }
	    }

	}
}

