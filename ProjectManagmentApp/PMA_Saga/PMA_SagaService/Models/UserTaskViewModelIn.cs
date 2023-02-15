namespace PMA_SagaService.Models
{
    public class UserTaskViewModelIn
    {
        public int id { get; set; }
        public string name { get; set; }
        public string status { get; set; }
        public int statusId { get; set; }
        public DateTime changeDate { get; set; } = DateTime.UtcNow;
        public int assignedUserId { get; set; }
        public int? priority { get; set; }
        public string description { get; set; }
        public int changedByUserId { get; set; }

        public IEnumerable<CommentViewModel> comments { get; set; }
    }
}
