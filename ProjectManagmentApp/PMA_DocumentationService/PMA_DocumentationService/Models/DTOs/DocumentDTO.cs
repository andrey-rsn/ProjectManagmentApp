
namespace PMA_DocumentationService.Models.DTOs
{
    public class DocumentDTO
    {
        public int Id { get; set; }

        public string DocumentName { get; set; }

        public string DocumentDescription { get; set; }

        public string FileName { get; set; }

        public string FileUrl { get; set; }

        public DateTime UploadDate { get; set; }
        public int ProjectId { get; set; }
    }
}
