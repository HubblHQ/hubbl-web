using hubbl.web.models;
using MongoDB.Driver;

namespace hubbl.web {
	public class Settings {
		public const string SERVER_URL = "http://localhost:8080/";
	    public const string MONGODB_CONNECTION_URL = "mongodb://localhost/hubbl";

	    private static string serverKey;
	    public static string getServerKey() {
	        return serverKey;
	    }

	    private static long nextPlayerId;
	    public static long getNextPlayerId() {
	        return nextPlayerId++;
	    }

	    public static void init() {
            IMongoCollection<Setting> settings = DbHolder.getDb().GetDatabase(Constants.HUBBL_DB_NAME).GetCollection<Setting>(Constants.SETTINGS_TABLE_NAME);
            Setting setting = settings.Find(_ => true).First();
            serverKey = setting.serverKey;
            nextPlayerId = setting.nextPlayerId;
	    }

	    public static void save() {
	        IMongoCollection<Setting> settings = DbHolder.getDb().GetDatabase(Constants.HUBBL_DB_NAME).GetCollection<Setting>(Constants.SETTINGS_TABLE_NAME);
	        settings.FindOneAndUpdate(
	            _ => true,
	            Builders<Setting>.Update.Set(e => e.nextPlayerId, nextPlayerId).Set(e => e.serverKey, serverKey)
	        );
	    }
	}

    public class Constants {
        public const string HUBBL_DB_NAME = "hubbl";

        public const string SETTINGS_TABLE_NAME = "settings";
        public const string USERS_TABLE_NAME = "users";
        public const string HUBS_TABLE_NAME = "hubs";

        public const string ID = "id";
        public const string TOKEN = "token";

        public const string USER_LOGIN = "login";
        public const string USER_PASSWORD = "password";
        public const string USER_NAME = "name";

        public const string HUB_NAME = "name";

        public class Status {
            public const string OK = "ok";
            public const string ERROR = "error";
        }

        public class NetMsg {
            public const string LOGIN_FAILED = "Wrong login or password";
            public const string SIGNUP_FAILED = "Error while creating user";
            public const string FORBIDDEN = "Access denied";
            public const string KEY_NOT_FOUND = "Key not found";
            public const string HUB_ALREADY_EXISTS = "Hub already exists";
        }

    }
}
