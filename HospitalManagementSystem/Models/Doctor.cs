using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HospitalManagementSystem.Models
{
    public class Doctor 
    {
        [Key]
        public int DoctorID { get; set; }
        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; }
        [Required]
        [MaxLength(50)]
        public string LastName { get; set; }
        [MaxLength(100)]
        public string Specialty { get; set; }

        [ForeignKey("Department")]
        public int DepartmentID { get; set; } 

        [MaxLength(15)]
        public string Phone { get; set; }

        [MaxLength(100)]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [ForeignKey("Hospital")]
        public int HospitalID { get; set; }

        public Hospital Hospital { get; set; }

        public ICollection<Appointment> Appointments { get; set; }
        public Department Department { get; set; }
    }
}
