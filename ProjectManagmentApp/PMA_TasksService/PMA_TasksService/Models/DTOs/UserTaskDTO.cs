using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PMA_TasksService.Models.DTOs
{
    public class UserTaskDTO
    {
        public int taskId { get; set; }

        public string taskName { get; set; }

        public int userTaskStatusId { get; set; }

        public DateTime changeDate { get; set; }

        public int assignedUserId { get; set; }

        public int? priority { get; set; }

        public string? description { get; set; }
    }
}
