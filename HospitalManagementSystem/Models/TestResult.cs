using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace HospitalManagementSystem.Models
{
    public class TestResult
    {
        [Key]
        public int TestResultID { get; set; }

        [Required]
        [ForeignKey("LabTest")]
        public int LabTestID { get; set; }

        [Required]
        [ForeignKey("Patient")]
        public int PatientID { get; set; }

        public string Result { get; set; }

        [Required]
        public DateTime Date { get; set; }

        public LabTest LabTest { get; set; }
        public Patient Patient { get; set; }
    }
}
