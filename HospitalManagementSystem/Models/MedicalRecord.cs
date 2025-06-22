using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace HospitalManagementSystem.Models
{
    public class MedicalRecord
    {
        [Key]
        public int MedicalRecordID { get; set; }

        [Required]
        [ForeignKey("Patient")]
        public int PatientID { get; set; }

        public string Allergies { get; set; }
        public string MedicalHistory { get; set; }
        public string CurrentMedications { get; set; }
        public string Immunizations { get; set; }

        public Patient Patient { get; set; }
    }
}
