using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PMA_TasksService.Models
{
    public class UserTask
    {
        [Key]
        public int taskId { get; set; }

        [Required]
        public string taskName { get; set; }

        [Required]
        [ForeignKey(nameof(UserTaskStatus))]
        public int userTaskStatusId { get; set; }

        [Required]
        public DateTime changeDate { get; set; }

        [Required]
        public int assignedUserId { get; set; }

        public int? priority { get; set; }

        public string? description { get; set; }

        public int changedByUserId { get; set; }


    }
}
