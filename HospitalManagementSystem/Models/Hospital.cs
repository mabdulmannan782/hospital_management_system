using System.ComponentModel.DataAnnotations;

namespace HospitalManagementSystem.Models
{
    public class Hospital
    {
        [Key]
        public int HospitalID { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        [MaxLength(255)]
        public string Address { get; set; }

        [Required]
        [MaxLength(15)]
        public string Phone { get; set; }

        [Required]
        [MaxLength(100)]
        [EmailAddress]
        public string Email { get; set; }

        [MaxLength(100)]
        [Url]
        public string Website { get; set; }

        // Navigation Properties
        public ICollection<Department> Departments { get; set; }
        public ICollection<Staff> Staffs { get; set; }
        public ICollection<Room> Rooms { get; set; }
        public ICollection<Patient> Patients { get; set; }
        public ICollection<Doctor> Doctors { get; set; }
    }
}
