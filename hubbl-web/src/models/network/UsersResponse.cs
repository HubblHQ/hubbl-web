using System.Collections.Generic;

namespace hubbl.web.models.network {

    public class UsersResponse : BaseResponse {

        public List<User> users = new List<User>();

        public class User {
            public string id;
            public string name;

            public User(string id, string name) : base() {
                this.id = id;
                this.name = name;
            }

            public User(models.User user) {
                this.id = user.id.ToString();
                this.name = user.name;
            }
        }

        public UsersResponse(List<models.User> users) {
            this.users = users.ConvertAll(u => new User(u));
        }
    }
}