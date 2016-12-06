using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace hubbl.web.models {
	
	public class User {
	    [BsonId]
		public ObjectId id;
		public string login;
		public string password;
		public string name;

	    [BsonConstructor]
	    public User(string login, string password, string name) {
			this.login = login;
			this.password = Utility.md5(password);
			this.name = name;
		}

	}
}

