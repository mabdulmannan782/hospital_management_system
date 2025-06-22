using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace HospitalManagementSystem.Models
{
    public class Prescription
    {
        [Key]
        public int PrescriptionID { get; set; }

        [Required]
        [ForeignKey("Patient")]
        public int PatientID { get; set; }

        [Required]
        [ForeignKey("Doctor")]
        public int DoctorID { get; set; }

        [Required]
        [ForeignKey("Medication")]
        public int MedicationID { get; set; }

        [MaxLength(50)]
        public string Dosage { get; set; }

        [MaxLength(50)]
        public string Frequency { get; set; }

        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        public Patient Patient { get; set; }
        public Doctor Doctor { get; set; }
        public Medication Medication { get; set; }
    }
}
