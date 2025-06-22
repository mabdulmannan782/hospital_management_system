using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace HospitalManagementSystem.Models
{
    public class Room
    {
        [Key]
        public int RoomID { get; set; }

        [Required]
        [MaxLength(10)]
        public string RoomNumber { get; set; }

        [MaxLength(50)]
        public string Type { get; set; }

        [ForeignKey("Department")]
        public int? DepartmentID { get; set; }

        public int? Capacity { get; set; }

        public bool Availability { get; set; }

        [Required]
        [ForeignKey("Hospital")]
        public int HospitalID { get; set; }

        public Department Department { get; set; }
        public Hospital Hospital { get; set; }

    }
}
