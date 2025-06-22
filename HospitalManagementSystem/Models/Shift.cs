using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace HospitalManagementSystem.Models
{
    public class Shift
    {
        [Key]
        public int ShiftID { get; set; }

        [Required]
        [ForeignKey("Staff")]
        public int StaffID { get; set; }

        [Required]
        public TimeSpan StartTime { get; set; }

        [Required]
        public TimeSpan EndTime { get; set; }

        [MaxLength(50)]
        public string Role { get; set; }

        public Staff Staff { get; set; }
    }
}
