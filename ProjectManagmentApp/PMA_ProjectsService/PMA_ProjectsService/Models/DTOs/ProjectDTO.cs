using System.ComponentModel.DataAnnotations;

namespace PMA_ProjectsService.Models.DTOs
{
    public class ProjectDTO
    {
        public int ProjectId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }
    }
}
