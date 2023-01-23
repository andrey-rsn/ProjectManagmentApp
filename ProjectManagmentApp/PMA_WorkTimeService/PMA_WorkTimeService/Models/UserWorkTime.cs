using System.ComponentModel.DataAnnotations;

namespace PMA_WorkTimeService.Models
{
    public class UserWorkTime
    {
        [Key]
        public int UserWorkTimeId { get; set; }

        [Required]
        public int UserId { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }
    }
}
