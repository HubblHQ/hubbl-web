using System.Collections.Generic;

namespace hubbl.web.models.network {

    public class PlaylistEntry {
        public long id;
        public string title;
        public string artist;
        public string publisher;
        public int duration;
        public int likes;
        public int dislikes;
    }

    public class PlaylistResponse : BaseResponse {

        public List<PlaylistEntry> playlist;

        public PlaylistResponse(List<PlaylistEntry> playlist) : base() {
            this.playlist = playlist;
        }
    }
}