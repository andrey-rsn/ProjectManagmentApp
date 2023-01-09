namespace PMA_IdentityService.Models.ViewModels
{
    public class UserRegistrationViewModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string Patronymic { get; set; }
        public string Role { get; set; }
    }
}
