using System.ComponentModel.DataAnnotations;

namespace PMA_DocumentationService.Models
{
    public class Document
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string DocumentName { get; set; }

        [Required]
        public string DocumentDescription { get; set; }

        [Required]
        public string FileName { get; set; }

        [Required]
        public string FileUrl { get; set; }

        [Required]
        public DateTime UploadDate { get; set; }

        [Required]
        public int ProjectId { get; set; }

    }
}
