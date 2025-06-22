using System.ComponentModel.DataAnnotations;

namespace HospitalManagementSystem.Models
{
    public class LabTest
    {
        [Key]
        public int LabTestID { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        public string Description { get; set; }
        public string PreparationInstructions { get; set; }
    }
}
