using System;
using System.Collections.Generic;
using hubbl.web.models.network;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;

namespace hubbl.web.models {
	
	public class User {
	    [BsonId]
		public ObjectId id;
		public string login;
		public string password;
	    public string salt;
	    public string name;
	    public string token;

	    [BsonConstructor]
	    public User(string login, string password, string salt, string name, string token) {
			this.login = login;
	        this.password = password;
	        this.salt = salt;
	        this.name = name;
	        this.token = token;
	    }

	    public static User get(string id)
	    {
	        try {
	            var collection = DbHolder.getDb().GetDatabase(Constants.HUBBL_DB_NAME).GetCollection<User>(Constants.USERS_TABLE_NAME);
                User user = collection.Find(Builders<User>.Filter.Eq<String>(u => u.id.ToString(), id)).First();
                return user;
	        } catch {
	            return null;
	        }
	    }

	    public static string getUser(User user)
	    {
	        return user == null ? new ErrorResponse(210, Constants.NetMsg.KEY_NOT_FOUND).ToJson() : new UserResponse(user).ToJson();
	    }

	    public static User getAutentification(string token) {
	        try {
	            return DbHolder.getDb().GetDatabase(Constants.HUBBL_DB_NAME).GetCollection<User>(Constants.USERS_TABLE_NAME)
	            .Find(Builders<User>.Filter.Eq<String>(e => e.token, token)).First();
	        } catch {
	            return null;
	        }
	    }

	    public static bool authentificated(string token) {
	        return getAutentification(token) != null;
	    }

	    public static User tryLogin(string login, string password) {
	        try {
	            var collection = DbHolder.getDb().GetDatabase(Constants.HUBBL_DB_NAME).GetCollection<User>(Constants.USERS_TABLE_NAME);
	            User user = collection.Find(Builders<User>.Filter.Eq<String>(u => u.login, login)).First();
	            if (user.password != Utility.sha256(password + user.salt + Settings.getServerKey())) return null;
	            user.token = Guid.NewGuid().ToString();
	            collection.FindOneAndUpdate(
	                Builders<User>.Filter.Eq<ObjectId>(e => e.id, user.id),
	                Builders<User>.Update.Set(e => e.token, user.token)
	            );
	            return user;
	        } catch {
	            return null;
	        }
	    }

	    public static string toLoginResponse(User user) {
	        if (user == null) return new ErrorResponse(201, Constants.NetMsg.LOGIN_FAILED).ToJson();
	        return new LoginResponse(user.id.ToString(), user.name, user.token).ToJson();
	    }

	    public static User trySignUp(string name, string login, string password) {
	        IMongoCollection<User> users = DbHolder.getDb().GetDatabase(Constants.HUBBL_DB_NAME).GetCollection<User>(Constants.USERS_TABLE_NAME);

	        long count = users.Find(Builders<User>.Filter.Eq<String>(u => u.login, login)).Count();
	        if (count > 0) return null;

	        string salt = Utility.randomSalt();
	        User user = new User(login, Utility.sha256(password + salt + Settings.getServerKey()), salt, name, null);
	        users.InsertOne(user);
	        return user;
	    }

	    public static string toSignUpResponse(User user) {
	        return user == null ? new ErrorResponse(202, Constants.NetMsg.SIGNUP_FAILED).ToJson() : new EmptyResponse().ToJson();
	    }

	}
}

