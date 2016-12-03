using System;
using System.Collections.Generic;

namespace hubbl.web.models {
	
	public class Hub {

		public string name;
		public User owner;
		public List<Track> playlist;
		public List<User> users;

		public Hub(string name, User owner) {
			this.name = name;
			this.owner = owner;
			this.playlist = new List<Track>();
			this.users = new List<User>();
			this.users.Add(owner);
			this.save();
		}

		public Hub(string name) {
			this.name = name;
			this.getByName();
		}

		public bool save() {
			return true;
		}

		private bool getByName() {
			return true;
		}

	}
}

