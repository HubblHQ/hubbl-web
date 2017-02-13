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
		//public List<string> users;

	    [BsonConstructor]
	    public Hub(string name, User owner, List<string> users) {
			this.name = name;
			this.owner = owner;
			//this.users = users;
		}

	    public Hub(string name, User owner) {
	        this.name = name;
	        this.owner = owner;
	        //this.users = new List<string> {owner.id.ToString()};
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
	            Hub hub = Hub.get(hubId);

	            if (user == null) return new ErrorResponse(300, Constants.NetMsg.FORBIDDEN).ToJson();
	            if (hub == null) return new ErrorResponse(300, Constants.NetMsg.KEY_NOT_FOUND).ToJson();

	            if(Realtime.getInstance().connectedUsers[user.id.ToString()] != null) return new ErrorResponse(300, Constants.NetMsg.ALREADY_CONNECTED).ToJson();

	            Realtime.getInstance().connectedUsers[user.id.ToString()] = hubId;

	            List<string> users;
	            if (Realtime.getInstance().hubUsers[hubId] == null) {
	                users = Realtime.getInstance().hubUsers[hubId] = new List<string>();
	            } else {
	                users = Realtime.getInstance().hubUsers[hubId];
	            }

	            users.Add(user.id.ToString());

	            return new EmptyResponse().ToJson();
	        } catch {
	            return new ErrorResponse(210, Constants.NetMsg.KEY_NOT_FOUND).ToJson();
	        }
	    }

	    public static string tryDisconnect(string token) {
	        try {
	            User user = User.getAutentification(token);

	            if (user == null) return new ErrorResponse(300, Constants.NetMsg.FORBIDDEN).ToJson();

	            if(Realtime.getInstance().connectedUsers[user.id.ToString()] == null) return new ErrorResponse(300, Constants.NetMsg.NOT_CONNECTED).ToJson();

	            string hubId;
	            Realtime.getInstance().connectedUsers.TryRemove(user.id.ToString(), out hubId);
	            Realtime.getInstance().hubUsers[hubId].Remove(user.id.ToString());

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

	        Hub hub = new Hub(hubName, user);
            hubs.InsertOne(hub);

	        //Player player = new Player(hub.id.ToString());
	        return new IdResponse(hub.id.ToString()).ToJson();
	    }

	    public static string getUsers(string token) {
	        User user = User.getAutentification(token);

	        if (user == null) return new ErrorResponse(300, Constants.NetMsg.FORBIDDEN).ToJson();
	        if(Realtime.getInstance().connectedUsers[user.id.ToString()] == null) return new ErrorResponse(300, Constants.NetMsg.NOT_CONNECTED).ToJson();

	        return new UsersResponse(Realtime.getInstance().hubUsers[user.id.ToString()]).ToJson();
	    }
	}
}

