namespace PMA_SagaService.Models
{
    public class CommentViewModelIn
    {
        public int commentId { get; set; }

        public int authorId { get; set; }

        public DateTime creationDate { get; set; }

        public string? commentText { get; set; }

        public int associatedTaskId { get; set; }
    }
}
