using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PMA_ProjectsService.Models
{
    public class EmployeesAttachedToProjectsModel
    {
        [Key]
        public int EmployeesAttachedToProjectsId { get; set; }

        [Required]
        public int EmployeeId { get; set; }

        [Required]
        [ForeignKey(nameof(Project))]
        public int ProjectId { get; set; }
    }
}
