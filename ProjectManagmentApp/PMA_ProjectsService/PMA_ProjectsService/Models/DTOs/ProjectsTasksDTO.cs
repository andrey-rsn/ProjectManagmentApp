using System.ComponentModel.DataAnnotations;

namespace PMA_ProjectsService.Models.DTOs
{
    public class ProjectsTasksDTO
    {
        public int ProjectsTasksId { get; set; }

        public int ProjectId { get; set; }

        public int TaskId { get; set; }
    }
}
