namespace PMA_ProjectsService.Models
{
    public class CreateProjectRequestModel
    {
        public int ProjectId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int AuthorId { get; set; }
    }
}
