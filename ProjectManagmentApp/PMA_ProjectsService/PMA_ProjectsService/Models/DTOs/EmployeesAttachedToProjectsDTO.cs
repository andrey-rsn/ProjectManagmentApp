using System.ComponentModel.DataAnnotations;

namespace PMA_ProjectsService.Models.DTOs
{
    public class EmployeesAttachedToProjectsDTO
    {
        public int EmployeesAttachedToProjectsId { get; set; }

        public int EmployeeId { get; set; }

        public int ProjectId { get; set; }
    }
}
