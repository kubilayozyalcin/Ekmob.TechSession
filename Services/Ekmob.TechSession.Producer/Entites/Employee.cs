using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ekmob.TechSession.Producer.Entites
{
    public class Employee
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string Name { get; set; }
        public string IdentityNumber { get; set; }
        public string Email { get; set; }
        [BsonRepresentation(BsonType.ObjectId)]
        public string DepartmentId { get; set; }
        public string JobTitle { get; set; }

        [BsonIgnore]
        public Department Department { get; set; }
    }
}
