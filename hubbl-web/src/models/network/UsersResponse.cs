using System.Collections.Generic;

namespace hubbl.web.models.network {
    public class UsersResponse : BaseResponse {

        public List<SUser> users = new List<SUser>();

        public class SUser {
            public string id;
            public string name;

            public SUser(string id, string name) {
                this.id = id;
                this.name = name;
            }

            public SUser(User user) {
                this.id = user.id.ToString();
                this.name = user.name;
            }
        }

        public UsersResponse(List<string> users) {
            users.ForEach(u => { this.users.Add(new SUser(User.get(u))); });
        }
    }
}