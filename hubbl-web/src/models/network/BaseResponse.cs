using Newtonsoft.Json;

namespace hubbl.web.models.network {
    public abstract class BaseResponse {
        public string status;

        public BaseResponse() {
            this.status = Constants.Status.OK;
        }
    }
}