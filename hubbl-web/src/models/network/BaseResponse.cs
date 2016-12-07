using Newtonsoft.Json;

namespace hubbl.web.models.network {
    public abstract class BaseResponse {
        public string status;

        public string toJson() {
            return JsonConvert.SerializeObject(this).ToString();
        }
    }
}