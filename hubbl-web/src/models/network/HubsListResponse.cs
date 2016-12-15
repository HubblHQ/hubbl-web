using System.Collections.Generic;

namespace hubbl.web.models.network {

    public class HubsListResponse : BaseResponse {
        public List<HubInfo> hubs;

        public HubsListResponse(List<HubInfo> hubs) {
            this.status = Constants.Status.OK;
            this.hubs = hubs;
        }
    }
}