namespace PMA_DocumentationService.Models
{
    public class UploadDoucmentViewModel
    {
        public string DocumentName { get; set; }

        public string DocumentDescription { get; set; }

        public int ProjectId { get; set; }

        public IFormFile UploadedFile { get; set; }

    }
}
