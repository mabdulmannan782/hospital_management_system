using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace HospitalManagementSystem.Models
{
    public class Surgery
    {
        [Key]
        public int SurgeryID { get; set; }

        [Required]
        [ForeignKey("Patient")]
        public int PatientID { get; set; }

        [Required]
        [ForeignKey("Doctor")]
        public int DoctorID { get; set; }

        [Required]
        public DateTime SurgeryDate { get; set; }

        [Required]
        public TimeSpan SurgeryTime { get; set; }

        [MaxLength(500)]
        public string Description { get; set; }

        [MaxLength(500)]
        public string Outcome { get; set; }

        public Patient Patient { get; set; }
        public Doctor Doctor { get; set; }
    }
}
