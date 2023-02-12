namespace PMA_SagaService.Models
{
    public class CommentViewModel
    {
        public string userAvatar { get; set; }
        public string author { get; set; }
        public DateTime creationDate { get; set; }
        public string text { get; set; }
    }
}
