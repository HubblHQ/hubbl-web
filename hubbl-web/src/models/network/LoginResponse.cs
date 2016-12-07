namespace hubbl.web.models.network
{
    public class LoginResponse : BaseResponse {
        public string id;
        public string name;
        public string sessionToken;

        public LoginResponse(string id, string name, string sessionToken) {
            this.status = Constants.Status.OK;
            this.id = id;
            this.name = name;
            this.sessionToken = sessionToken;
        }

    }
}