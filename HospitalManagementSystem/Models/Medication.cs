using System.ComponentModel.DataAnnotations;

namespace HospitalManagementSystem.Models
{
    public class Medication
    {
        [Key]
        public int MedicationID { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        public string Description { get; set; }

        [MaxLength(50)]
        public string Dosage { get; set; }

        public string SideEffects { get; set; }
    }
}
