namespace PMA_SagaService.Models
{
    public class UserInfoViewModel
    {
        public int user_Id { get; set; }
        public string email { get; set; }
        public string login { get; set; }
        public string firstName { get; set; }
        public string secondName { get; set; }
        public string patronymic { get; set; }
        public string role { get; set; }
        public string position { get; set; }

        public string FullName
        {
            get
            {
                return $"{secondName} {firstName} {patronymic}";
            }
        }
    }
}
