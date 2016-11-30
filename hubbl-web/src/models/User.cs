using System;

namespace hubbl.web.models {
	
	public class User {

		public string login;
		public string password;
		public string name;
		
		public User(string login, string password, string name) {
			this.login = login;
			this.password = Utility.md5(password);
			this.name = name;
			save();
		}

		public User(string login) {
			this.login = login;
			getByLogin();
		}

		public bool save() {
			return true;
		}

		private bool getByLogin() {
			return true;
		}

	}
}

