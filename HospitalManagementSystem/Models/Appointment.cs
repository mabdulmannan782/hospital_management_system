using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HospitalManagementSystem.Models
{
    public class Appointment
    {
        [Key]
        public int AppointmentID { get; private set; }

        [Required]
        [ForeignKey("Patient")]
        public int PatientID { get; private set; }

        [Required]
        [ForeignKey("Doctor")]
        public int DoctorID { get; private set; }

        [Required]
        public DateTime AppointmentDate { get; private set; }

        [Required]
        public TimeSpan AppointmentTime { get; private set; }

        public string Reason { get; private set; }

        [MaxLength(20)]
        public string Status { get; private set; }

        // Navigation properties
        public virtual Patient Patient { get; private set; }
        public virtual Doctor Doctor { get; private set; }

        // Constructor
        public Appointment(int appointmentID, int patientID, int doctorID, DateTime appointmentDate, TimeSpan appointmentTime, string reason, string status)
        {
            AppointmentID = appointmentID;
            PatientID = patientID;
            DoctorID = doctorID;
            AppointmentDate = appointmentDate;
            AppointmentTime = appointmentTime;
            Reason = reason;
            Status = status;
        }
    }
}