using System.ComponentModel.DataAnnotations;

namespace PMA_TasksService.Models
{
    public class UserTaskStatus
    {
        [Key]
        public int userTaskStatusId { get; set; }

        [Required]
        public string statusName { get; set; }
    }
}
