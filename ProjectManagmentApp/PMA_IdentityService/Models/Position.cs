using System.ComponentModel.DataAnnotations;

namespace PMA_IdentityService.Models
{
    public class Position
    {
        [Key]
        public int Position_Id { get; set; }
        [Required]
        public string PositionName { get; set; }
    }
}
