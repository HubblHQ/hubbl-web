using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace hubbl.web.models {

    public class Setting {
        [BsonId]
        public ObjectId id;
        public long nextPlayerId;
        public string serverKey;

        [BsonConstructor]
        public Setting(long nextPlayerId, string serverKey) {
            this.nextPlayerId = nextPlayerId;
            this.serverKey = serverKey;
        }
    }
}