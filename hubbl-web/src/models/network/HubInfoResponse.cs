namespace hubbl.web.models.network {
    public class HubInfo {
        public string id;
        public string name;
        public string author;

        public HubInfo(string id, string name, string author) {
            this.id = id;
            this.name = name;
            this.author = author;
        }
    }

    public class HubInfoResponse : BaseResponse {
        public string id;
        public string name;
        public string author;

        public HubInfoResponse(string id, string name, string author) : base() {
            this.id = id;
            this.name = name;
            this.author = author;
        }
    }
}