namespace hubbl.web.models.network
{
    public class HubInfo
    {
        public string id;
        public string name;
        public string author;

        public HubInfo(string id, string name, string author) {
            this.id = id;
            this.name = name;
            this.author = author;
        }
    }
}