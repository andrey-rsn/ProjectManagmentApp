namespace PMA_SagaService.Models
{
    public class UserTaskResponseViewModel
    {
        public int taskId { get; set; }

        public string taskName { get; set; }

        public int userTaskStatusId { get; set; }

        public DateTime changeDate { get; set; }

        public int assignedUserId { get; set; }

        public int? priority { get; set; }

        public string? description { get; set; }

        public int changedByUserId { get; set; }
    }
}
