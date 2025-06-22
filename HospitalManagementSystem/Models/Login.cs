using System.ComponentModel.DataAnnotations;

namespace HospitalManagementSystem.Models
{
    public class Login
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(8)]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
