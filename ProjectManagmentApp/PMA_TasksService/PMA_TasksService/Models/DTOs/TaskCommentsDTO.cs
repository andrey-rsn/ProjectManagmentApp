using System.ComponentModel.DataAnnotations;

namespace PMA_TasksService.Models.DTOs
{
    public class TaskCommentsDTO
    {
        public int taskCommentsId { get; set; }

        public int taskId { get; set; }

        public int commentId { get; set; }
    }
}
