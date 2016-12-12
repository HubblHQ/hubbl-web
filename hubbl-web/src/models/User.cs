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
	        this.password = Utility.sha256(password + salt + Settings.SERVER_KEY);
	        this.salt = salt;
	        this.name = name;
		}

	    public static bool authentificated(string token, string userId) {
	        try {
	            return DbHolder.getDb().GetDatabase(Constants.HUBBL_DB_NAME).GetCollection<User>(Constants.USERS_TABLE_NAME)
	                .Find(Builders<User>.Filter.Eq(Constants.ID, userId)).First().token == token;
	        } catch {
	            return false;
	        }
	    }

	    public static User tryLogin(string login, string password) {
	        try {
	            User user = DbHolder.getDb().GetDatabase(Constants.HUBBL_DB_NAME).GetCollection<User>(Constants.USERS_TABLE_NAME)
	                .Find(Builders<User>.Filter.Eq(Constants.USER_LOGIN, login)).First();
	            return user.password == Utility.sha256(password + user.salt + Settings.SERVER_KEY) ? user : null;
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

	        long count = users.Find(Builders<User>.Filter.Eq(Constants.USER_LOGIN, login)).Count();
	        if (count > 0) return null;

	        var salt = Utility.randomSalt()
	        User user = new User(login, Utility.sha256(password + salt + Settings.SERVER_KEY), salt, name, null);
	        users.InsertOne(user);
	        return user;
	    }

	    public static string toSignUpResponse(User user) {
	        if (user == null) return new ErrorResponse(202, Constants.NetMsg.SIGNUP_FAILED).ToJson();
	        return new EmptyResponse().ToJson();
	    }

	}
}

