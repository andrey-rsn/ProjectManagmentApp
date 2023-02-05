using System.ComponentModel.DataAnnotations;

namespace PMA_TasksService.Models.DTOs
{
    public class CommentDTO
    {
        public int commentId { get; set; }

        public int authorId { get; set; }

        public DateTime creationDate { get; set; }

        public string? commentText { get; set; }
    }
}
