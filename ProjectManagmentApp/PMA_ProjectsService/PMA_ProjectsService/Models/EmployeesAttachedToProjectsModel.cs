using System.ComponentModel.DataAnnotations;

namespace PMA_ProjectsService.Models
{
    public class EmployeesAttachedToProjectsModel
    {
        [Key]
        public int EmployeesAttachedToProjectsId { get; set; }

        [Required]
        public int EmployeeId { get; set; }

        [Required]
        public int ProjectId { get; set; }
    }
}
