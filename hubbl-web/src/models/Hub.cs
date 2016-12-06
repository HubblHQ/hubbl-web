using System;
using System.Collections.Generic;

namespace hubbl.web.models {
	
	public class Hub {

		public class DatabaseEntry {
			public long id;
			public string name;
			public long ownerId;

			public DatabaseEntry(long id) {
				this.id = id;
			}

			public DatabaseEntry(string name, long ownerId) {
				this.name = name;
				this.ownerId = ownerId;
			}
		}

		public long id;
		public string name;
		public User owner;
		public Player player;
		public List<User> users;

		public Hub(long id, string name, Player player, User owner) {
			this.name = name;
			this.owner = owner;
			this.player = player;
			this.users = new List<User>();
			this.users.Add(owner);
		}

	}
}

