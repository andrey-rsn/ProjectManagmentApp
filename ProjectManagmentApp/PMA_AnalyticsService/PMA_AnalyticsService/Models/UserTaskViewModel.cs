﻿namespace PMA_AnalyticsService.Models
{
    public class UserTaskViewModel
    {
        public int id { get; set; }
        public string name { get; set; }
        public string status { get; set; }
        public int statusId { get; set; }
        public DateTime changeDate { get; set; }
        public string assignedTo { get; set; }
        public int assignedUserId { get; set; }
        public int priority { get; set; }
        public string description { get; set; }
        public int changedByUserId { get; set; }
        public string changedBy { get; set; }
    }
}
