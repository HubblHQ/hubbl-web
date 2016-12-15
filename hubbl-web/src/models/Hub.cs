using System;
using System.Collections.Generic;
using hubbl.web.models.network;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;

namespace hubbl.web.models {

	public class Hub {

	    [BsonId]
	    public ObjectId id;
		public string name;
		public User owner;
		public long player;
		public List<string> users;

	    [BsonConstructor]
	    public Hub(string name, User owner, long player, List<string> users) {
			this.name = name;
			this.owner = owner;
			this.player = player;
			this.users = users;
		}

	    public Hub(string name, User owner, Player player) {
	        this.name = name;
	        this.owner = owner;
	        this.player = player.id;
	        this.users = new List<string> {owner.id.ToString()};
	    }

	    public HubInfo toHubInfo() {
	        return new HubInfo(this.id.ToString(), this.name, this.owner.name);
	    }

	    public static List<HubInfo> getAll() {
	        return DbHolder.getDb().GetDatabase(Constants.HUBBL_DB_NAME).GetCollection<Hub>(Constants.HUBS_TABLE_NAME)
	            .Find(_ => true).ToList().ConvertAll(el => new HubInfo(el.id.ToString(), el.name, el.owner.name));
	    }

	    public static List<HubInfo> find(string query) {
	        return DbHolder.getDb().GetDatabase(Constants.HUBBL_DB_NAME).GetCollection<Hub>(Constants.HUBS_TABLE_NAME)
	            .Find(Builders<Hub>.Filter.Regex(Constants.HUB_NAME, query)).ToList().ConvertAll(el => new HubInfo(el.id.ToString(), el.name, el.owner.name));
	    }

	    public static Hub get(string id) {
	        try {
	            return DbHolder.getDb().GetDatabase(Constants.HUBBL_DB_NAME).GetCollection<Hub>(Constants.HUBS_TABLE_NAME)
	                .Find(Builders<Hub>.Filter.Eq<ObjectId>(hub => hub.id, new ObjectId(id))).First();
	        } catch {
	            return null;
	        }
	    }

	    public static string getOrError(string id) {
	        Hub hub = get(id);
	        return hub != null ? hub.toHubInfo().ToJson() : new ErrorResponse(210, Constants.NetMsg.KEY_NOT_FOUND).ToJson();
	    }

	    public static string tryConnect(string hubId, string token) {
	        try {
	            User user = User.getAutentification(token);
	            if (user == null) return new ErrorResponse(300, Constants.NetMsg.FORBIDDEN).ToJson();

	            var collection = DbHolder.getDb().GetDatabase(Constants.HUBBL_DB_NAME).GetCollection<Hub>(Constants.HUBS_TABLE_NAME);

	            // Warning! no Exceptions will be thrown if there will be nothing to update. We need to use another aproach to add user to userlist
	            collection.FindOneAndUpdate(
	                Builders<Hub>.Filter.Eq<ObjectId>(hub => hub.id, new ObjectId(hubId)) & Builders<Hub>.Filter.AnyNe<String>(e => e.users, user.id.ToString()),
	                Builders<Hub>.Update.Push(e => e.users, user.id.ToString())
	            );
	            return new EmptyResponse().ToJson();
	        } catch {
	            return new ErrorResponse(210, Constants.NetMsg.KEY_NOT_FOUND).ToJson();
	        }
	    }

	    public static string tryCreate(string token, string hubName) {
	        User user = User.getAutentification(token);
	        if (user == null) return new ErrorResponse(300, Constants.NetMsg.FORBIDDEN).ToJson();

	        var hubs = DbHolder.getDb().GetDatabase(Constants.HUBBL_DB_NAME).GetCollection<Hub>(Constants.HUBS_TABLE_NAME);

	        long count = hubs.Find(Builders<Hub>.Filter.Eq<String>(e => e.name, hubName)).Count();
	        if (count > 0) return new ErrorResponse(220, Constants.NetMsg.HUB_ALREADY_EXISTS).ToJson();

	        Player player = new Player();
	        Hub hub = new Hub(hubName, user, player);
            hubs.InsertOne(hub);
	        return new IdResponse(hub.id.ToString()).ToJson();
	    }

	}
}

