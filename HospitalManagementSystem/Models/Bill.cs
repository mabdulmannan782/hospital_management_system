using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace HospitalManagementSystem.Models
{
    public class Bill
    {
        [Key]
        public int BillID { get; set; }

        [Required]
        [ForeignKey("Patient")]
        public int PatientID { get; set; }

        [Required]
        public decimal TotalAmount { get; set; }

        [Required]
        public DateTime DateIssued { get; set; }

        [MaxLength(20)]
        public string Status { get; set; }

        public Patient Patient { get; set; }
    }
}
