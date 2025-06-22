using HospitalManagementSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace HospitalManagementSystem.Data
{
    public class HospitalContext : DbContext
    {
        // Constructor to configure DbContext options
        public HospitalContext(DbContextOptions<HospitalContext> options) : base(options)
        {
        }

        // DbSet properties for tables
        public DbSet<Patient> Patient { get; set; }
        public DbSet<Login> Admins { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Bed> Beds { get; set; }
        public DbSet<Medication> Medications { get; set; }
        public DbSet<Prescription> Prescriptions { get; set; }
        public DbSet<MedicalRecord> MedicalRecords { get; set; }
        public DbSet<Bill> Bills { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Insurance> Insurances { get; set; }
        public DbSet<LabTest> LabTests { get; set; }
        public DbSet<TestResult> TestResults { get; set; }
        public DbSet<Diagnosis> Diagnoses { get; set; }
        public DbSet<TreatmentPlan> TreatmentPlans { get; set; }
        public DbSet<Nurse> Nurses { get; set; }
        public DbSet<Shift> Shifts { get; set; }
        public DbSet<Staff> Staff { get; set; }
        public DbSet<Surgery> Surgeries { get; set; }
        public DbSet<Visitor> Visitors { get; set; }
        public DbSet<Hospital> Hospitals { get; set; }

        // Model creation and configuration
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure Patient entity
            modelBuilder.Entity<Patient>(entity =>
            {
                entity.Property(p => p.FirstName).IsRequired().HasMaxLength(50);
                entity.Property(p => p.LastName).IsRequired().HasMaxLength(50);
                entity.Property(p => p.Phone).HasMaxLength(15);
                entity.Property(p => p.Email).HasMaxLength(100);
                entity.HasOne(p => p.Hospital).WithMany(h => h.Patients).HasForeignKey(p => p.HospitalID).OnDelete(DeleteBehavior.Cascade);
            });

            // Configure Doctor entity
            modelBuilder.Entity<Doctor>(entity =>
            {
                entity.Property(d => d.FirstName).IsRequired().HasMaxLength(50);
                entity.Property(d => d.LastName).IsRequired().HasMaxLength(50);
                entity.Property(d => d.Specialty).HasMaxLength(100);
                entity.HasOne(d => d.Hospital).WithMany(h => h.Doctors).HasForeignKey(d => d.HospitalID).OnDelete(DeleteBehavior.Cascade);
            });

            // Configure Appointment entity
            modelBuilder.Entity<Appointment>(entity =>
            {
                entity.HasOne(a => a.Patient).WithMany(p => p.Appointment).HasForeignKey(a => a.PatientID).OnDelete(DeleteBehavior.Cascade);
                entity.HasOne(a => a.Doctor).WithMany(d => d.Appointments).HasForeignKey(a => a.DoctorID).OnDelete(DeleteBehavior.Restrict);
                entity.Property(a => a.Reason).HasMaxLength(500);
                entity.Property(a => a.Status).HasMaxLength(20);
            });

            // Configure Department entity
            modelBuilder.Entity<Department>(entity =>
            {
                entity.Property(d => d.Name).IsRequired().HasMaxLength(100);
                entity.HasOne(d => d.Hospital).WithMany(h => h.Departments).HasForeignKey(d => d.HospitalID).OnDelete(DeleteBehavior.Cascade);
            });

            // Configure Room entity
            modelBuilder.Entity<Room>(entity =>
            {
                entity.Property(r => r.RoomNumber).IsRequired().HasMaxLength(10);
                entity.Property(r => r.Type).HasMaxLength(50);
                entity.HasOne(r => r.Department).WithMany(d => d.Rooms).HasForeignKey(r => r.DepartmentID).OnDelete(DeleteBehavior.Restrict);
                entity.HasOne(r => r.Hospital).WithMany(h => h.Rooms).HasForeignKey(r => r.HospitalID).OnDelete(DeleteBehavior.Cascade);
            });
            modelBuilder.Entity<Diagnosis>(entity =>
            {
                entity.HasOne(d => d.Patient)
                      .WithMany(p => p.Diagnoses)
                      .HasForeignKey(d => d.PatientID)
                      .OnDelete(DeleteBehavior.Restrict); // Use Restrict instead of Cascade
            });
            modelBuilder.Entity<Prescription>(entity =>
            {
                entity.HasOne(d => d.Patient)
                      .WithMany(p => p.Prescription)
                      .HasForeignKey(d => d.PatientID)
                      .OnDelete(DeleteBehavior.Restrict); // Use Restrict instead of Cascade
            });
            modelBuilder.Entity<Surgery>(entity =>
            {
                entity.HasOne(d => d.Patient)
                      .WithMany(p => p.Surgery)
                      .HasForeignKey(d => d.PatientID)
                      .OnDelete(DeleteBehavior.Restrict); // Use Restrict instead of Cascade
            });
            // Additional entities can be configured similarly
        }
    }
}
