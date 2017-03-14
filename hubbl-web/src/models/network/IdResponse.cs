namespace hubbl.web.models.network {

    public class IdResponse : BaseResponse {
        public string id;

        public IdResponse(string id) : base() {
            this.id = id;
        }
    }
}