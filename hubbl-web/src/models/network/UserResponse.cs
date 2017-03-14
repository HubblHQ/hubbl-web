namespace hubbl.web.models.network {
    public class UserResponse : BaseResponse {

        public string name;

        public UserResponse(User user) : base() {
            this.name = user.name;
        }
    }
}