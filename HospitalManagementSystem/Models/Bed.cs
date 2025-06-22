using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace HospitalManagementSystem.Models
{
    public class Bed
    {
        [Key]
        public int BedID { get; set; }

        [Required]
        [ForeignKey("Room")]
        public int RoomID { get; set; }

        [MaxLength(20)]
        public string Status { get; set; }

        public Room Room { get; set; }
    }
}
