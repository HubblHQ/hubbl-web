namespace hubbl.web.models.network {
    public class UserResponse : BaseResponse {

        public string name;

        public UserResponse(User user) {
            this.name = user.name;
        }
    }
}