using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PMA_IdentityService.Models
{
    public class User
    {
        [Key]
        public int User_Id { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Login { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string SecondName { get; set; }
        [Required]
        public string Patronymic { get; set; }
        [Required]
        public string Role { get; set; }
        [Required]
        [ForeignKey(nameof(Position))]
        public int Position_Id { get; set; }
    }
}
