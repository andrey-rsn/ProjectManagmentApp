using System.ComponentModel.DataAnnotations;

namespace PMA_TasksService.Models.DTOs
{
    public class UserTaskStatusDTO
    {
        public int userTaskStatusId { get; set; }

        public string statusName { get; set; }
    }
}
