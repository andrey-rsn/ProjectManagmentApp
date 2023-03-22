namespace PMA_SagaService.Models
{
    public class AttachEmployeesToProjectRequestModel
    {
        public int projectId { get; set; }
        public int[] employeesIds { get; set; }

    }
}
