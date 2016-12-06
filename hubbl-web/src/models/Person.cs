using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace hubbl.web.models
{
    [BsonIgnoreExtraElements]
    public class Person
    {
        [BsonId]
        public ObjectId Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        [BsonConstructor]
        public Person(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;
        }
    }
}