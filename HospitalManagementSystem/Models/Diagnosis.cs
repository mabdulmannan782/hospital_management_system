using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace HospitalManagementSystem.Models
{
    public class Diagnosis
    {
        [Key]
        public int DiagnosisID { get; set; }

        [Required]
        [ForeignKey("Patient")]
        public int PatientID { get; set; }

        [Required]
        [ForeignKey("Doctor")]
        public int DoctorID { get; set; }

        public string Description { get; set; }

        [Required]
        public DateTime Date { get; set; }

        public Patient Patient { get; set; }
        public Doctor Doctor { get; set; }
    }
}
