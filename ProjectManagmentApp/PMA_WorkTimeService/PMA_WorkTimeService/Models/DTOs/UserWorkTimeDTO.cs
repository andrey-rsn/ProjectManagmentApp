using System.ComponentModel.DataAnnotations;

namespace PMA_WorkTimeService.Models.DTOs
{
    public class UserWorkTimeDTO
    {
        public int UserWorkTimeId { get; set; }

        public int UserId { get; set; }

        public DateTime? StartTime { get; set; }

        public DateTime? EndTime { get; set; }
    }
}
