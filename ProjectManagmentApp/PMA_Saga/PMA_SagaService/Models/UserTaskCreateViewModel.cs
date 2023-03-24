namespace PMA_SagaService.Models
{
    public class UserTaskCreateViewModel
    {
        public int projectId { get; set; }
        public string name { get; set; }
        public int statusId { get; set; }
        public DateTime changeDate { get; set; } = DateTime.UtcNow;
        public int assignedUserId { get; set; }
        public int? priority { get; set; }
        public string description { get; set; }
        public int changedByUserId { get; set; }

        public IEnumerable<CommentViewModelIn> comments { get; set; }
    }
}
