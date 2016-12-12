using MongoDB.Driver;

namespace hubbl.web {
    public class DbHolder {

        private static MongoClient db;
        public static MongoClient getDb() {
            return db ?? (db = new MongoClient(Settings.MONGODB_CONNECTION_URL));
        }
    }
}