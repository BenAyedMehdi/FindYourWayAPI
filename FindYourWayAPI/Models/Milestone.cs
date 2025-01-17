﻿using System.Text.Json.Serialization;

namespace FindYourWayAPI.Models
{
    public class Milestone
    {
        public int MilestoneId { get; set; }
        public string MilestoneName { get; set; }
        public List<Goal> Goals { get; set; }
        public Category Category { get; set; }
        public int CompanyId { get; set; }
        [JsonIgnore]
        public Company Company { get; set; }
    }
}
