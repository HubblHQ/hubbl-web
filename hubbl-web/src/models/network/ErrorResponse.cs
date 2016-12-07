namespace hubbl.web.models.network {

    public class ErrorResponse : BaseResponse {
        public int code;
        public string msg;

        public ErrorResponse(int code, string msg) {
            this.status = Constants.Status.ERROR;
            this.code = code;
            this.msg = msg;
        }
    }

}