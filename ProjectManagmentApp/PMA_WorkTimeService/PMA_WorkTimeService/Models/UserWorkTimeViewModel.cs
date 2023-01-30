namespace PMA_WorkTimeService.Models
{
    public class UserWorkTimeViewModel
    {
        public int UserId { get; set; }

        public DateTime? StartTime { get; set; }

        public DateTime? EndTime { get; set; }

        public bool IsStarted { 
            get
            {
                return StartTime!= null && EndTime == null;
            }
            set
            {
                this.IsStarted= value;
            }
        } 

    }
}
