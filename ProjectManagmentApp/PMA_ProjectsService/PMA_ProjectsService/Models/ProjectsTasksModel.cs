using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PMA_ProjectsService.Models
{
    public class ProjectsTasksModel
    {
        [Key]
        public int ProjectsTasksId { get; set; }

        [Required]
        [ForeignKey(nameof(Project))]
        public int ProjectId { get; set; }

        [Required]
        public int TaskId { get; set; }
    }
}
