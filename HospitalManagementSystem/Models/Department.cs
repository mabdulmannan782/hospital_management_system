using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace HospitalManagementSystem.Models
{
    public class Department
    {
        [Key]
        public int DepartmentID { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [ForeignKey("Doctor")]
        public int? Head { get; set; }

        [MaxLength(100)]
        public string Location { get; set; }

        [Required]
        [ForeignKey("Hospital")]
        public int HospitalID { get; set; }

        public Doctor Doctor { get; set; }
        public Hospital Hospital { get; set; }
        public ICollection<Room> Rooms { get; set; }
    }
}
