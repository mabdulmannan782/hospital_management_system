using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace HospitalManagementSystem.Models
{
    public class TreatmentPlan
    {
        [Key]
        public int TreatmentPlanID { get; set; }

        [Required]
        [ForeignKey("Diagnosis")]
        public int DiagnosisID { get; set; }

        public string Description { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        public Diagnosis Diagnosis { get; set; }
    }
}
