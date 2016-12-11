using System;

namespace hubbl.web {
	public class Settings {
		public const string SERVER_URL = "http://localhost:8080/";
	    public const string MONGODB_CONNECTION_URL = "mongodb://localhost/hubbl";
	}

    public class Constants {
        public const string HUBBL_DB_NAME = "hubbl";

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

        public class NetErrorMessages {
            public const string LOGIN_FAILED = "Wrong login or password";
            public const string SIGNUP_FAILED = "Error while creating user";
            public const string FORBIDDEN = "Access denied";
        }

    }
}

