using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace HospitalManagementSystem.Models
{
    public class Visitor
    {
        [Key]
        public int VisitorID { get; set; }

        [Required]
        [ForeignKey("Patient")]
        public int PatientID { get; set; }

        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(50)]
        public string LastName { get; set; }

        [MaxLength(50)]
        public string Relationship { get; set; }

        [Required]
        public DateTime VisitDate { get; set; }

        [Required]
        public TimeSpan VisitTime { get; set; }

        public Patient Patient { get; set; }
    }
}
