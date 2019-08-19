using System;

namespace YAPMT.Api.Models
{
    public class ProjectTask
    {
        public string Id { get; set; }

        public string Description { get; set; }

        public string Owner { get; set; }

        public DateTime DueDate { get; set; }

        public bool Completed { get; set; }
    }
}