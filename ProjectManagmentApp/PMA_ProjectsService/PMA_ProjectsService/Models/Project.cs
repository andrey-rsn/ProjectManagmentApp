using System.ComponentModel.DataAnnotations;

namespace PMA_ProjectsService.Models
{
    public class Project
    {
        [Key]
        public int ProjectId { get; set; }

        [Required]
        public string Name { get; set; }

        public string Description { get; set; }
    }
}
