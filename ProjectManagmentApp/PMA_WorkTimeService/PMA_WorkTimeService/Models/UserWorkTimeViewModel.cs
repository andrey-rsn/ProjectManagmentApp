namespace PMA_WorkTimeService.Models
{
    public class UserWorkTimeViewModel
    {
        public int UserId { get; set; }

        public DateTime? StartTime { get; set; }

        public DateTime? EndTime { get; set; }
    }
}
