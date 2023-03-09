using System.ComponentModel.DataAnnotations;

namespace PMA_TasksService.Models
{
    public class TaskComments
    {
        [Key]
        public int taskCommentsId { get; set; }

        [Required]
        public int taskId { get; set; }

        [Required]
        public int commentId { get; set; }


    }
}
