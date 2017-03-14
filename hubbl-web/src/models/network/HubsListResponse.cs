using System.Collections.Generic;

namespace hubbl.web.models.network {

    public class HubsListResponse : BaseResponse {
        public List<HubInfo> hubs;

        public HubsListResponse(List<HubInfo> hubs) : base() {
            this.hubs = hubs;
        }
    }
}