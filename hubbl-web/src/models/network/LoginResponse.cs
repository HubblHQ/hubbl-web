namespace hubbl.web.models.network
{
    public class LoginResponse : BaseResponse {
        public string id;
        public string name;
        public string token;

        public LoginResponse(string id, string name, string token) {
            this.status = Constants.Status.OK;
            this.id = id;
            this.name = name;
            this.token = token;
        }

    }
}