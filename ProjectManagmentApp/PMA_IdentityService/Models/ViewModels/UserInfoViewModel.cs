namespace PMA_IdentityService.Models.ViewModels
{
    public class UserInfoViewModel
    {
        public int User_Id { get; set; }
        public string Email { get; set; }
        public string Login { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string Patronymic { get; set; }
        public string Role { get; set; }
        public string Position { get; set; }
    }
}
