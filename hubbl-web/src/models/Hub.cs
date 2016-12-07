using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace hubbl.web.models {

	public class Hub {

	    [BsonId]
	    public ObjectId id;
		public string name;
		public User owner;
		public Player player;
		public List<User> users;

	    [BsonConstructor]
	    public Hub(string name, User owner, Player player, List<User> users) {
			this.name = name;
			this.owner = owner;
			this.player = player;
			this.users = users;
		}

	    public Hub(string name, User owner, Player player) {
	        this.name = name;
	        this.owner = owner;
	        this.player = player;
	        this.users = new List<User> {owner};
	    }

	}
}

