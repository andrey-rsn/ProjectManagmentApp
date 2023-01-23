using System.ComponentModel.DataAnnotations;

namespace PMA_IdentityService.Models.DTOs
{
    public class UserDTO
    {
        public int User_Id { get; set; }
        public string Email { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string Patronymic { get; set; }
        public string Role { get; set; }
        public int Position_Id { get; set; }
    }
}

