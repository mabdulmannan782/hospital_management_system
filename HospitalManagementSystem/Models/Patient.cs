using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HospitalManagementSystem.Models
{
    public class Patient
    {
        [Key]
        public int PatientID { get; set; }
        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; }
        [Required]
        [MaxLength(50)]
        public string LastName { get; set; }
        [Required]
        public DateTime DateOfBirth { get; set; }
        [MaxLength(10)]
        public string Gender { get; set; }
        [MaxLength(255)]
        public string Address { get; set; }
        [MaxLength(50)]
        public string Phone { get; set; }
        [MaxLength(100)]
        [EmailAddress]
        public string Email { get; set; }
        [MaxLength(100)]
        public string EmergencyContact { get; set; }
        [Required]
        [ForeignKey("Hospital")]
        public int HospitalID { get; set; }

        public Hospital Hospital { get; set; }
        public ICollection<Appointment> Appointment { get; set; }
        public ICollection<Diagnosis> Diagnoses { get; set; }
        public ICollection<Prescription> Prescription { get; set; }
        public ICollection<Surgery> Surgery { get; set; }
        //public object Appointment { get; internal set; }
    }
}
