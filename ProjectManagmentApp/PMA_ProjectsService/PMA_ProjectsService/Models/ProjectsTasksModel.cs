using System.ComponentModel.DataAnnotations;

namespace PMA_ProjectsService.Models
{
    public class ProjectsTasksModel
    {
        [Key]
        public int ProjectsTasksId { get; set; }

        [Required]
        public int ProjectId { get; set; }

        [Required]
        public int TaskId { get; set; }
    }
}
