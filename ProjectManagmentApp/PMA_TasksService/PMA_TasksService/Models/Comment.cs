using System.ComponentModel.DataAnnotations;

namespace PMA_TasksService.Models
{
    public class Comment
    {
        [Key]
        public int commentId { get; set; }

        [Required]
        public int authorId { get; set; }

        [Required]
        public DateTime creationDate { get; set; }

        public string? commentText { get; set; }

        [Required]
        public int associatedTaskId { get; set; }
    }
}
