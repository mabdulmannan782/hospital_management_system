using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace HospitalManagementSystem.Models
{
    public class Insurance
    {
        [Key]
        public int InsuranceID { get; set; }

        [Required]
        [ForeignKey("Patient")]
        public int PatientID { get; set; }

        [Required]
        [MaxLength(100)]
        public string Provider { get; set; }

        [Required]
        [MaxLength(100)]
        public string PolicyNumber { get; set; }

        public string CoverageDetails { get; set; }

        public Patient Patient { get; set; }
    }
}
