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
		public string name;
	    public string token;

	    [BsonConstructor]
	    public User(string login, string password, string name, string token) {
			this.login = login;
			this.password = Utility.md5(password);
			this.name = name;
		}

	    public static User tryLogin(string login, string password) {
	        IMongoCollection<User> users = new MongoClient().GetDatabase(Constants.HUBBL_DB_NAME).GetCollection<User>(Constants.USERS_TABLE_NAME);

	        var builder = Builders<User>.Filter;
	        FilterDefinition<User> filter = builder.Eq(Constants.USER_LOGIN, login) & builder.Eq(Constants.USER_PASSWORD, password);

	        List<User> result = users.Find(filter).ToList();

	        return result.Count > 0 ? result[0] : null;
	    }

	    public static string toLoginResponse(User user) {
	        if (user == null) return new ErrorResponse(201, Constants.NetErrorMessages.LOGIN_FAILED).toJson();
	        return new LoginResponse(user.id.ToString(), user.name, user.token).toJson();
	    }

	    public static User trySignUp(string name, string login, string password) {
	        IMongoCollection<User> users = new MongoClient().GetDatabase(Constants.HUBBL_DB_NAME).GetCollection<User>(Constants.USERS_TABLE_NAME);

	        List<User> usersWithThatLogin = users.Find(Builders<User>.Filter.Eq(Constants.USER_LOGIN, login)).ToList();
	        if (usersWithThatLogin.Count > 0) return null;

	        User user = new User(login, password, name, null);
	        users.InsertOne(user);
	        return user;
	    }

	    public static string toSignUpResponse(User user) {
	        if (user == null) return new ErrorResponse(202, Constants.NetErrorMessages.SIGNUP_FAILED).toJson();
	        return new EmptyResponse().toJson();
	    }

	}
}

