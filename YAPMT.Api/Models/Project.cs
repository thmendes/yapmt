using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;

namespace YAPMT.Api.Models
{
    public class Project
    {
        [BsonId]
        public ObjectId Id { get; set; }

        public string Name { get; set; }

        public DateTime CreatedAt { get; set; }

        public List<ProjectTask> Tasks { get; set; }
    }
}
