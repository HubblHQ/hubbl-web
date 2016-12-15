namespace hubbl.web.models.network {

    public class IdResponse : BaseResponse {
        public string id;

        public IdResponse(string id) {
            this.status = Constants.Status.OK;
            this.id = id;
        }
    }
}